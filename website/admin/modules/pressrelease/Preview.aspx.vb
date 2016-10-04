
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_pressrelease_preview
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 10 ' Module Type: Press Release

    Public Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim strPressReleaseID As String = btnCancel.CommandArgument
        Response.Redirect("editAdd.aspx?id=" & strPressReleaseID.ToString())
    End Sub

    Public Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("default.aspx")
    End Sub


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a rollback confirmation to our rollback button
        CommonWeb.SetupRollBackButton(btnRollBack, Resources.PressRelease_Admin.PressRelease_Preview_RollBack_ConfirmationMessage)

        'Set the Header UserControls Title and Help Item if it exists
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.PressRelease_Admin.PressRelease_Preview_ModuleName)
        ucHeader.PageName = Resources.PressRelease_Admin.PressRelease_Preview_Header
        ucHeader.PageHelpID = 4 'Help Item for Press Releases

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            If Not Request.QueryString("archiveID") Is Nothing Then
                Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

                Dim dtPressReleaseArchive As DataTable = PressReleaseDAL.GetPressReleaseArchive_ByArchiveIDAndSiteID(intArchiveID, SiteDAL.GetCurrentSiteID_Admin())
                If dtPressReleaseArchive.Rows.Count > 0 Then
                    Dim drPressReleaseArchive As DataRow = dtPressReleaseArchive.Rows(0)

                    'Set the cancel button to go back to the orginial press release in question
                    Dim intPressReleaseID As Integer = Convert.ToInt32(drPressReleaseArchive("prID"))

                    Dim dtPressRelease As DataTable = PressReleaseDAL.GetPressRelease_ByPressReleaseID(intPressReleaseID)
                    If dtPressRelease.Rows.Count > 0 Then
                        Dim drPressRelease As DataRow = dtPressRelease.Rows(0)


                        btnCancel.CommandArgument = intPressReleaseID.ToString()

                        'Populate the preview pages info box
                        Dim intPressReleaseVersion As Integer = Convert.ToInt32(drPressRelease("version"))
                        Dim intPressReleaseArchiveVersion As Integer = Convert.ToInt32(drPressReleaseArchive("version"))

                        'If the archived version is the same as the most recent version, we represent this by prepending text to it saying it is also the current version
                        litInformationBox_Version.Text = intPressReleaseArchiveVersion.ToString()
                        If intPressReleaseVersion = intPressReleaseArchiveVersion Then
                            litInformationBox_Version.Text = Resources.PressRelease_Admin.PressRelease_Preview_InformationBox_Version_Current & " (" & litInformationBox_Version.Text & ")"
                        End If

                        litInformationBox_DateCreated.Text = drPressReleaseArchive("dateTimeStamp")

                        litInformationBox_AuthorName.Text = Resources.PressRelease_Admin.PressRelease_Preview_InformationBox_AuthorDefault
                        If Not drPressReleaseArchive("authorID_member") Is DBNull.Value Then
                            Dim intAuthorID_member As Integer = drPressReleaseArchive("authorID_member")
                            Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intAuthorID_member)
                            If dtMember.Rows.Count > 0 Then
                                Dim drMember As DataRow = dtMember.Rows(0)

                                Dim strAuthorName As String = drMember("FirstName") & " " & drMember("LastName")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        ElseIf Not drPressReleaseArchive("authorID_admin") Is DBNull.Value Then
                            Dim intAuthorID_admin As Integer = drPressReleaseArchive("authorID_admin")
                            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAuthorID_admin)
                            If dtAdminUser.Rows.Count > 0 Then
                                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                                Dim strAuthorName As String = drAdminUser("First_Name") & " " & drAdminUser("Last_Name")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        End If

                        Dim strCategoryName As String = Resources.PressRelease_Admin.PressRelease_Preview_InformationBox_UnCategorized
                        If Not drPressReleaseArchive("CategoryName") Is DBNull.Value Then
                            strCategoryName = drPressReleaseArchive("CategoryName")
                        End If
                        litInformationBox_Category.Text = strCategoryName

                        Dim boolStatus As Boolean = Convert.ToBoolean(drPressReleaseArchive("Status"))
                        litInformationBox_Status.Text = If(boolStatus, Resources.PressRelease_Admin.PressRelease_Preview_InformationBox_StatusActive, Resources.PressRelease_Admin.PressRelease_Preview_InformationBox_StatusArchive)

                        If Not drPressReleaseArchive("publicationDate").ToString() = "" Then
                            litInformationBox_PublicationDate.Text = drPressReleaseArchive("publicationDate").ToString()
                            divInformationBox_PublicationDate.Visible = True
                        End If

                        If Not drPressReleaseArchive("expirationDate").ToString() = "" Then
                            Dim dtExpirationDate As DateTime = Convert.ToDateTime(drPressReleaseArchive("expirationDate"))
                            litInformationBox_ExpirationDate.Text = dtExpirationDate.ToString()
                            divInformationBox_ExpirationDate.Visible = True

                            If dtExpirationDate < DateTime.Now Then
                                divExpired.Visible = True
                            End If

                        End If
                        If Not drPressReleaseArchive("Summary") Is DBNull.Value Then
                            litInformationBox_Summary.Text = drPressReleaseArchive("Summary")
                            divInformationBox_Summary.Visible = True
                        End If


                        'Populate the preview page such that it is similar to the front-end version

                        'set the title and body
                        Dim strTitle As String = drPressReleaseArchive("Title")
                        litTitle.Text = strTitle

                        Dim strBody As String = drPressReleaseArchive("Body")
                        litBody.Text = strBody

                        'set the author name and posted by date
                        Dim strAuthorUsername As String = Resources.PressRelease_Admin.PressRelease_Preview_PostedByDefault
                        If Not drPressReleaseArchive("author_username") Is DBNull.Value Then
                            strAuthorUsername = drPressReleaseArchive("author_username").ToString()
                        End If
                        litPostedBy.Text = strAuthorUsername

                        Dim dtViewDate As DateTime = Convert.ToDateTime(drPressReleaseArchive("viewDate"))

                        litPressReleaseDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDateTime.Text = dtViewDate.ToString("h:mm tt")



                        'Load in this press release search tags
                        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin(), intPressReleaseID)
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

            PressReleaseDAL.RollbackPressRelease(intArchiveID)
            pnlConfirmation.Visible = True
            pnlPreview.Visible = False
        End If
    End Sub

End Class
