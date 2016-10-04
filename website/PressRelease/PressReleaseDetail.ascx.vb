Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports Telerik.Web.UI
Imports Subgurim.Controles

Partial Class PressRelease_PressReleaseDetail
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 10

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            If Not Request.QueryString("id") Is Nothing Then
                Dim intPressReleaseID As Integer = Convert.ToInt32(Request.QueryString("id"))
                LoadPressRelease(intPressReleaseID)
            End If
        End If


    End Sub

    Protected Sub LoadPressRelease(ByVal intPressReleaseID As Integer)
        Dim boolStatus As Boolean = True
        Dim boolAllowArchive As Boolean = False
        Dim boolAllowComments As Boolean = False
        Dim boolEnablePublicCommentSubmission As Boolean = False
        Dim boolAllowOnlineSubmissions As Boolean = False
        Dim boolEnableGroupsAndUserAccess As Boolean = False

        If Request.QueryString("archive") = 1 Then
            boolStatus = False
        End If

        ' Check our modules Configuration settings
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                boolAllowArchive = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "show_add_this" Then
                plcAddThis.Visible = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" Then
                boolAllowComments = True
            ElseIf drModuleConfig("fieldName").ToString.ToLower() = "enable_public_comment_submission" Then
                boolEnablePublicCommentSubmission = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_submissions" Then
                boolAllowOnlineSubmissions = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True

            End If
        Next

        'If we find out the press release is an archived press release, but we do not allow achives then redirect to default page
        If boolAllowArchive = False And boolStatus = False Then
            Response.Redirect("default.aspx")
        End If

        'Load the Press Release By ID
        Dim dtPressRelease As DataTable = If(boolEnableGroupsAndUserAccess, PressReleaseDAL.GetPressRelease_ByPressReleaseIDAndStatusAndAccess_FrontEnd(intPressReleaseID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PressReleaseDAL.GetPressRelease_ByPressReleaseIDAndStatus_FrontEnd(intPressReleaseID, boolStatus, intSiteID))
        If dtPressRelease.Rows.Count > 0 Then
            Dim drPressRelease As DataRow = dtPressRelease.Rows(0)

            'Check if the press release is supposed to direct to an external url
            Dim strExternalLinkUrl As String = ""
            If Not drPressRelease("externalLinkUrl") Is DBNull.Value AndAlso drPressRelease("externalLinkUrl").ToString().Trim.Length > 0 Then
                Response.Redirect(drPressRelease("externalLinkUrl").ToString())
            End If

            'set the title and body
            Dim strTitle As String = drPressRelease("Title")

            'Set the page title Default
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.PressRelease_FrontEnd.PressRelease_PressReleaseDetail_HeaderTitle + " - " + strTitle

            litTitle.Text = strTitle

            Dim strBody As String = drPressRelease("Body")
            litBody.Text = strBody

            'set the author name and posted by date
            Dim strAuthorUsername As String = Resources.PressRelease_FrontEnd.PressRelease_PressReleaseDetail_PostedByDefault
            If Not drPressRelease("author_username") Is DBNull.Value Then
                strAuthorUsername = drPressRelease("author_username").ToString()
            End If
            litPostedBy.Text = strAuthorUsername

            Dim dtViewDate As DateTime = Convert.ToDateTime(drPressRelease("viewDate"))

            litPressReleaseDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
            litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
            litViewDateTime.Text = dtViewDate.ToString("h:mm tt")

            'Load in this press release search tags
            Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intPressReleaseID)
            If dtSearchTags.Rows.Count > 0 Then
                divModuleSearchTagList.Visible = True
                rptSearchTags.DataSource = dtSearchTags
                rptSearchTags.DataBind()
            End If

            'Finally show the edit link if the press release was uploaded by the currently logged in user
            If boolAllowOnlineSubmissions Then
                If Not drPressRelease("authorID_member") Is DBNull.Value Then
                    Dim intAuthorID_Member As Integer = Convert.ToInt32(drPressRelease("authorID_member"))
                    If intMemberID > 0 And intMemberID = intAuthorID_Member Then
                        divEditPressRelease.Visible = True
                    End If

                End If
            End If

            'set the page title
            If Not drPressRelease("metaTitle") Is DBNull.Value Then
                Dim strMetaTitle As String = drPressRelease("metaTitle").ToString()
                If strMetaTitle.Length > 0 Then
                    Page.Header.Title = strMetaTitle
                End If
            End If

            'set the page's keywords
            If Not drPressRelease("metaKeywords") Is DBNull.Value Then
                Dim keywordsHtmlMeta As New HtmlMeta()
                keywordsHtmlMeta.Name = "keywords"
                keywordsHtmlMeta.Content = drPressRelease("metaKeywords").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(keywordsHtmlMeta)
            End If

            'set the page's description
            If Not drPressRelease("metaDescription") Is DBNull.Value Then
                Dim descriptionHtmlMeta As New HtmlMeta()
                descriptionHtmlMeta.Name = "description"
                descriptionHtmlMeta.Content = drPressRelease("metaDescription").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(descriptionHtmlMeta)
            End If



            'Load Comments
            If boolAllowComments Then
                'setup the comments list, if we have enabled public comment submissions, then the member does not require to login to post a comment
                Dim intSiteID_ForPressRelease As Integer = Convert.ToInt32(drPressRelease("siteID"))
                ucCommentsModule.SetupCommentModule(intSiteID_ForPressRelease, ModuleTypeID, intPressReleaseID, intMemberID, boolStatus, boolEnablePublicCommentSubmission)
            End If
        Else
            Response.Redirect("default.aspx")
        End If
    End Sub

End Class
