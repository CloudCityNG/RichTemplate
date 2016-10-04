Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports Telerik.Web.UI
Imports Subgurim.Controles

Partial Class Faq_FaqDetail
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 6

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            If Not Request.QueryString("id") Is Nothing Then
                Dim intFaqID As Integer = Convert.ToInt32(Request.QueryString("id"))
                LoadFaq(intFaqID)
            End If
        End If


    End Sub

    Protected Sub LoadFaq(ByVal intFaqID As Integer)
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

        'If we find out the faq is an archived faq, but we do not allow achives then redirect to default page
        If boolAllowArchive = False And boolStatus = False Then
            Response.Redirect("default.aspx")
        End If

        'Load the FAQ By ID
        Dim dtFaq As DataTable = If(boolEnableGroupsAndUserAccess, FaqDAL.GetFaq_ByFaqIDAndStatusAndAccess_FrontEnd(intFaqID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), FaqDAL.GetFaq_ByFaqIDAndStatus_FrontEnd(intFaqID, boolStatus, intSiteID))
        If dtFaq.Rows.Count > 0 Then
            Dim drFaq As DataRow = dtFaq.Rows(0)

            'set the qestion and answer
            Dim strQuestion As String = drFaq("Question")

            'Set the page title Default
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Faq_FrontEnd.Faq_FaqDetail_HeaderTitle + " - " + strQuestion

            litQuestion.Text = strQuestion

            Dim strAnswer As String = drFaq("Answer")
            litAnswer.Text = strAnswer

            'set the author name and posted by date
            Dim strAuthorUsername As String = Resources.Faq_FrontEnd.Faq_FaqDetail_PostedByDefault
            If Not drFaq("author_username") Is DBNull.Value Then
                strAuthorUsername = drFaq("author_username").ToString()
            End If
            litPostedBy.Text = strAuthorUsername

            Dim dtViewDate As DateTime = Convert.ToDateTime(drFaq("viewDate"))

            litFaqDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
            litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
            litViewDateTime.Text = dtViewDate.ToString("h:mm tt")

            'Finally show the edit link if the faq was uploaded by the currently logged in user
            If boolAllowOnlineSubmissions Then
                If Not drFaq("authorID_member") Is DBNull.Value Then
                    Dim intAuthorID_Member As Integer = Convert.ToInt32(drFaq("authorID_member"))
                    If intMemberID > 0 And intMemberID = intAuthorID_Member Then
                        divEditFaq.Visible = True
                    End If

                End If
            End If

            'set the page title
            If Not drFaq("metaTitle") Is DBNull.Value Then
                Dim strMetaTitle As String = drFaq("metaTitle").ToString()
                If strMetaTitle.Length > 0 Then
                    Page.Header.Title = strMetaTitle
                End If
            End If

            'set the page's keywords
            If Not drFaq("metaKeywords") Is DBNull.Value Then
                Dim keywordsHtmlMeta As New HtmlMeta()
                keywordsHtmlMeta.Name = "keywords"
                keywordsHtmlMeta.Content = drFaq("metaKeywords").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(keywordsHtmlMeta)
            End If

            'set the page's description
            If Not drFaq("metaDescription") Is DBNull.Value Then
                Dim descriptionHtmlMeta As New HtmlMeta()
                descriptionHtmlMeta.Name = "description"
                descriptionHtmlMeta.Content = drFaq("metaDescription").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(descriptionHtmlMeta)
            End If

            'Load in this faq's search tags
            Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intFaqID)
            If dtSearchTags.Rows.Count > 0 Then
                divModuleSearchTagList.Visible = True
                rptSearchTags.DataSource = dtSearchTags
                rptSearchTags.DataBind()
            End If

            'Load Comments
            If boolAllowComments Then
                'setup the comments list, if we have enabled public comment submissions, then the member does not require to login to post a comment
                Dim intSiteID_ForFaq As Integer = Convert.ToInt32(drFaq("siteID"))
                ucCommentsModule.SetupCommentModule(intSiteID_ForFaq, ModuleTypeID, intFaqID, intMemberID, boolStatus, boolEnablePublicCommentSubmission)
            End If
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

End Class
