
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_blog_preview
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 1 ' Module Type: Blogs

    Public Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim intBlogID As String = btnCancel.CommandArgument
        Response.Redirect("editAdd.aspx?id=" & intBlogID.ToString())
    End Sub

    Public Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("default.aspx")
    End Sub


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a rollback confirmation to our rollback button
        CommonWeb.SetupRollBackButton(btnRollBack, Resources.Blog_Admin.Blog_Preview_RollBack_ConfirmationMessage)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Blog_Admin.Blog_Preview_Header
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Blog_Admin.Blog_Preview_ModuleName)

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            If Not Request.QueryString("archiveID") Is Nothing Then
                Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

                Dim dtBlogArchive As DataTable = BlogDAL.GetBlogArchive_ByArchiveIDAndSiteID(intArchiveID, SiteDAL.GetCurrentSiteID_Admin())
                If dtBlogArchive.Rows.Count > 0 Then
                    Dim drBlogArchive As DataRow = dtBlogArchive.Rows(0)

                    Dim intBlogID As Integer = Convert.ToInt32(drBlogArchive("blogID"))

                    'Now we must also check the record that the archive corresponds to actually exists, also as we don't store the images with this record in the archive table, we may also need to load the current images to show with this preview.
                    Dim dtBlog As DataTable = BlogDAL.GetBlog_ByBlogIDAndSiteID(intBlogID, SiteDAL.GetCurrentSiteID_Admin())
                    If dtBlog.Rows.Count > 0 Then
                        Dim drBlog As DataRow = dtBlog.Rows(0)


                        'Set the cancel button to go back to the orginial blog in question
                        btnCancel.CommandArgument = intBlogID.ToString()

                        'Populate the preview pages info box
                        Dim intBlogVersion As Integer = Convert.ToInt32(drBlog("version"))
                        Dim intBlogArchiveVersion As Integer = Convert.ToInt32(drBlogArchive("version"))

                        'If the archived version is the same as the most recent version, we represent this by prepending text to it saying it is also the current version
                        litInformationBox_Version.Text = intBlogArchiveVersion.ToString()
                        If intBlogVersion = intBlogArchiveVersion Then
                            litInformationBox_Version.Text = Resources.Blog_Admin.Blog_Preview_InformationBox_Version_Current & " (" & litInformationBox_Version.Text & ")"
                        End If

                        litInformationBox_DateCreated.Text = drBlogArchive("dateTimeStamp")

                        litInformationBox_AuthorName.Text = Resources.Blog_Admin.Blog_Preview_InformationBox_AuthorDefault
                        If Not drBlogArchive("authorID_member") Is DBNull.Value Then
                            Dim intAuthorID_member As Integer = drBlogArchive("authorID_member")
                            Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intAuthorID_member)
                            If dtMember.Rows.Count > 0 Then
                                Dim drMember As DataRow = dtMember.Rows(0)

                                Dim strAuthorName As String = drMember("FirstName") & " " & drMember("LastName")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        ElseIf Not drBlogArchive("authorID_admin") Is DBNull.Value Then
                            Dim intAuthorID_admin As Integer = drBlogArchive("authorID_admin")
                            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAuthorID_admin)
                            If dtAdminUser.Rows.Count > 0 Then
                                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                                Dim strAuthorName As String = drAdminUser("First_Name") & " " & drAdminUser("Last_Name")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        End If

                        Dim strCategoryName As String = Resources.Blog_Admin.Blog_Preview_InformationBox_UnCategorized
                        If Not drBlogArchive("CategoryName") Is DBNull.Value Then
                            strCategoryName = drBlogArchive("CategoryName")
                        End If
                        litInformationBox_Category.Text = strCategoryName

                        Dim boolStatus As Boolean = Convert.ToBoolean(drBlogArchive("Status"))
                        litInformationBox_Status.Text = If(boolStatus, Resources.Blog_Admin.Blog_Preview_InformationBox_StatusActive, Resources.Blog_Admin.Blog_Preview_InformationBox_StatusArchive)

                        If Not drBlogArchive("publicationDate").ToString() = "" Then
                            litInformationBox_PublicationDate.Text = drBlogArchive("publicationDate").ToString()
                            divInformationBox_PublicationDate.Visible = True
                        End If

                        If Not drBlogArchive("expirationDate").ToString() = "" Then
                            Dim dtExpirationDate As DateTime = Convert.ToDateTime(drBlogArchive("expirationDate"))
                            litInformationBox_ExpirationDate.Text = dtExpirationDate.ToString()
                            divInformationBox_ExpirationDate.Visible = True

                            If dtExpirationDate < DateTime.Now Then
                                divExpired.Visible = True
                            End If

                        End If

                        If Not drBlogArchive("Summary") Is DBNull.Value Then
                            litInformationBox_Summary.Text = drBlogArchive("Summary")
                            divInformationBox_Summary.Visible = True
                        End If

                        'Populate the preview page such that it is similar to the front-end version

                        'set the title and body
                        Dim strTitle As String = drBlogArchive("Title")
                        litTitle.Text = strTitle

                        Dim strBody As String = drBlogArchive("Body")
                        litBody.Text = strBody

                        'set the author name and posted by date
                        Dim strAuthorUsername As String = Resources.Blog_Admin.Blog_Preview_PostedByDefault
                        If Not drBlogArchive("author_username") Is DBNull.Value Then
                            strAuthorUsername = drBlogArchive("author_username").ToString()
                        End If


                        litPostedBy.Text = strAuthorUsername

                        Dim dtViewDate As DateTime = Convert.ToDateTime(drBlogArchive("viewDate"))

                        litBlogDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDateTime.Text = dtViewDate.ToString("h:mm tt")

                        'Load in this blog search tags
                        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin(), intBlogID)
                        If dtSearchTags.Rows.Count > 0 Then
                            divModuleSearchTagList.Visible = True
                            rptSearchTags.DataSource = dtSearchTags
                            rptSearchTags.DataBind()
                        End If
                    Else
                        Response.Redirect("default.aspx")
                    End If
                Else
                    Response.Redirect("default.aspx")
                End If
            Else
                Response.Redirect("default.aspx")
            End If

        End If

    End Sub

    Public Sub btnRollBack_OnClick(ByVal sender As Object, ByVal e As EventArgs)

        If Not Request.QueryString("archiveID") Is Nothing Then

            Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

            BlogDAL.RollbackBlog(intArchiveID)
            pnlConfirmation.Visible = True
            pnlPreview.Visible = False
        End If
    End Sub

End Class
