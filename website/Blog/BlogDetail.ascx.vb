Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports Telerik.Web.UI

Partial Class blog_BlogDetail
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 1

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            If Not Request.QueryString("id") Is Nothing Then
                Dim intBlogID As Integer = Convert.ToInt32(Request.QueryString("id"))
                LoadBlog(intBlogID)
            End If
        End If

    End Sub

    Protected Sub LoadBlog(ByVal intBlogID As Integer)
        Dim boolStatus As Boolean = True
        Dim boolAllowArchive As Boolean = False
        Dim boolAllowComments As Boolean = False
        Dim boolEnablePublicCommentSubmission As Boolean = False
        Dim boolAllowOnlineSubmissions As Boolean = False
        Dim boolEnableGroupsAndUserAccess As Boolean = False

        If Request.QueryString("archive") = 1 Then
            boolStatus = False
        End If

        'Check we need to show the 'add this link', and other blog config settings but only if its active
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                boolAllowArchive = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "show_add_this" Then
                If boolStatus Then
                    plcAddThis.Visible = True
                End If
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

        'If we find out the blog is an archived blog, but we do not allow achives then redirect to default page
        If boolAllowArchive = False And boolStatus = False Then
            Response.Redirect("default.aspx")
        End If

        'We have a blog id so try and load this blog entry
        Dim dtBlog As DataTable = If(boolEnableGroupsAndUserAccess, BlogDAL.GetBlog_ByBlogIDAndStatusAndAccess_FrontEnd(intBlogID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlog_ByBlogIDAndStatus_FrontEnd(intBlogID, boolStatus, intSiteID))
        If dtBlog.Rows.Count > 0 Then

            'Now we have the blog, so we load this dtBlog
            Dim drBlog As DataRow = dtBlog.Rows(0)


            Dim dtViewDate As DateTime = Convert.ToDateTime(drBlog("viewDate"))
            litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
            litBlogDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
            litViewDateTime.Text = dtViewDate.ToString("h:mm tt")

            Dim strTitle As String = drBlog("Title")
            litTitle.Text = strTitle

            Dim strBody As String = drBlog("Body")
            litBody.Text = strBody

            'set the author name and posted by date
            Dim strAuthorUsername As String = Resources.Blog_FrontEnd.Blog_BlogDetail_PostedByDefault
            If Not drBlog("author_username") Is DBNull.Value Then
                strAuthorUsername = drBlog("author_username").ToString()
            End If
            litPostedBy.Text = strAuthorUsername

            'Finally show the edit link if the document was uploaded by the currently logged in user
            If boolAllowOnlineSubmissions Then
                If Not drBlog("authorID_member") Is DBNull.Value Then
                    Dim intAuthorID_Member As Integer = Convert.ToInt32(drBlog("authorID_member"))
                    If intMemberID > 0 And intMemberID = intAuthorID_Member Then
                        divEditBlog.Visible = True
                    End If

                End If
            End If

            'Set the page title Default
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Blog_FrontEnd.Blog_BlogDetail_HeaderTitle + " - " + strTitle

            'set the page title
            If Not drBlog("metaTitle") Is DBNull.Value Then
                Dim strMetaTitle As String = drBlog("metaTitle").ToString()
                If strMetaTitle.Length > 0 Then
                    Page.Header.Title = strMetaTitle
                End If
            End If

            'set the page's keywords
            If Not drBlog("metaKeywords") Is DBNull.Value Then
                Dim keywordsHtmlMeta As New HtmlMeta()
                keywordsHtmlMeta.Name = "keywords"
                keywordsHtmlMeta.Content = drBlog("metaKeywords").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(keywordsHtmlMeta)
            End If

            'set the page's description
            If Not drBlog("metaDescription") Is DBNull.Value Then
                Dim descriptionHtmlMeta As New HtmlMeta()
                descriptionHtmlMeta.Name = "description"
                descriptionHtmlMeta.Content = drBlog("metaDescription").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(descriptionHtmlMeta)
            End If

            'Load in this events search tags
            Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intBlogID)
            If dtSearchTags.Rows.Count > 0 Then
                divModuleSearchTagList.Visible = True
                rptSearchTags.DataSource = dtSearchTags
                rptSearchTags.DataBind()
            End If


            If boolAllowComments Then
                'setup the comments list, if we have enabled public comment submissions, then the member does not require to login to post a comment
                Dim intSiteID_ForBlog As Integer = Convert.ToInt32(drBlog("siteID"))
                ucCommentsModule.SetupCommentModule(intSiteID_ForBlog, ModuleTypeID, intBlogID, intMemberID, boolStatus, boolEnablePublicCommentSubmission)
            End If
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

End Class
