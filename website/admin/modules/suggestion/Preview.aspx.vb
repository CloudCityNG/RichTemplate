
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_suggestion_preview
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 13 ' Module Type: Suggestion

    Public Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim intSuggestionID As String = btnCancel.CommandArgument
        Response.Redirect("editAdd.aspx?id=" & intSuggestionID.ToString())
    End Sub

    Public Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("default.aspx")
    End Sub


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a rollback confirmation to our rollback button
        CommonWeb.SetupRollBackButton(btnRollBack, Resources.Staff_Admin.Staff_Preview_RollBack_ConfirmationMessage)

        'Attach a rollback confirmation to our rollback button
        CommonWeb.SetupRollBackButton(btnRollBack, Resources.Suggestion_Admin.Suggestion_Preview_RollBack_ConfirmationMessage)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Suggestion_Admin.Suggestion_AddEdit_Header
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Suggestion_Admin.Suggestion_Preview_ModuleName)

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            If Not Request.QueryString("archiveID") Is Nothing Then
                Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

                Dim dtSuggestionArchive As DataTable = SuggestionDAL.GetSuggestionArchive_ByArchiveIDAndSiteID(intArchiveID, SiteDAL.GetCurrentSiteID_Admin())
                If dtSuggestionArchive.Rows.Count > 0 Then
                    Dim drSuggestionArchive As DataRow = dtSuggestionArchive.Rows(0)

                    'Set the cancel button to go back to the orginial suggestionID in question
                    Dim intSuggestionID As Integer = Convert.ToInt32(drSuggestionArchive("ID"))

                    Dim dtSuggestion As DataTable = SuggestionDAL.GetSuggestion_ByID(intSuggestionID)
                    If dtSuggestion.Rows.Count > 0 Then
                        Dim drSuggestion As DataRow = dtSuggestion.Rows(0)

                        btnCancel.CommandArgument = intSuggestionID.ToString()

                        'Populate the preview pages info box
                        Dim intSuggestionVersion As Integer = Convert.ToInt32(drSuggestion("version"))
                        Dim intSuggestionArchiveVersion As Integer = Convert.ToInt32(drSuggestionArchive("version"))

                        'If the archived version is the same as the most recent version, we represent this by prepending text to it saying it is also the current version
                        litInformationBox_Version.Text = intSuggestionArchiveVersion.ToString()
                        If intSuggestionVersion = intSuggestionArchiveVersion Then
                            litInformationBox_Version.Text = Resources.Suggestion_Admin.Suggestion_Preview_InformationBox_Version_Current & " (" & litInformationBox_Version.Text & ")"
                        End If

                        litInformationBox_DateCreated.Text = drSuggestionArchive("dateTimeStamp")


                        If Not drSuggestionArchive("authorID_member") Is DBNull.Value Then
                            Dim intAuthorID_member As Integer = drSuggestionArchive("authorID_member")
                            Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intAuthorID_member)
                            If dtMember.Rows.Count > 0 Then
                                Dim drMember As DataRow = dtMember.Rows(0)

                                Dim strAuthorName As String = drMember("FirstName") & " " & drMember("LastName")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        ElseIf Not drSuggestionArchive("authorID_admin") Is DBNull.Value Then
                            Dim intAuthorID_admin As Integer = drSuggestionArchive("authorID_admin")
                            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAuthorID_admin)
                            If dtAdminUser.Rows.Count > 0 Then
                                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                                Dim strAuthorName As String = drAdminUser("First_Name") & " " & drAdminUser("Last_Name")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        Else
                            litInformationBox_AuthorName.Text = Resources.Suggestion_Admin.Suggestion_Preview_InformationBox_Author_NotAvailable
                        End If

                        Dim strCategoryName As String = Resources.Suggestion_Admin.Suggestion_Preview_InformationBox_UnCategorized
                        If Not drSuggestionArchive("CategoryName") Is DBNull.Value Then
                            strCategoryName = drSuggestionArchive("CategoryName")
                        End If
                        litInformationBox_Category.Text = strCategoryName

                        Dim boolStatus As Boolean = Convert.ToBoolean(drSuggestionArchive("Status"))
                        litInformationBox_Status.Text = If(boolStatus, Resources.Suggestion_Admin.Suggestion_Preview_InformationBox_StatusActive, Resources.Suggestion_Admin.Suggestion_Preview_InformationBox_StatusArchive)

                        If Not drSuggestionArchive("publicationDate").ToString() = "" Then
                            litInformationBox_PublicationDate.Text = drSuggestionArchive("publicationDate").ToString()
                            divInformationBox_PublicationDate.Visible = True
                        End If

                        If Not drSuggestionArchive("expirationDate").ToString() = "" Then
                            Dim dtExpirationDate As DateTime = Convert.ToDateTime(drSuggestionArchive("expirationDate"))
                            litInformationBox_ExpirationDate.Text = dtExpirationDate.ToString()
                            divInformationBox_ExpirationDate.Visible = True

                            If dtExpirationDate < DateTime.Now Then
                                divExpired.Visible = True
                            End If

                        End If

                        'default related member to 'Not-Available'
                        litInformationBox_ContributedBy.Text = Resources.Suggestion_Admin.Suggestion_Preview_InformationBox_ContributedByNotAvailable
                        If Not drSuggestionArchive("memberID") Is DBNull.Value Then
                            Dim intMemberID As Integer = drSuggestionArchive("memberID")
                            Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intMemberID)
                            If dtMember.Rows.Count > 0 Then
                                Dim drMember As DataRow = dtMember.Rows(0)

                                Dim strMemberName As String = drMember("FirstName") & " " & drMember("LastName")
                                litInformationBox_ContributedBy.Text = strMemberName
                            End If
                        End If

                        litInformationBox_ContributedBy_EmailAddress.Text = Resources.Suggestion_Admin.Suggestion_Preview_InformationBox_ContributedBy_EmailAddressNotAvailable
                        If Not drSuggestionArchive("EmailAddress") Is DBNull.Value Then
                            litInformationBox_ContributedBy_EmailAddress.Text = drSuggestionArchive("EmailAddress")
                        End If

                        'Populate the preview page such that it is similar to the front-end version

                        'set the title and body
                        Dim strSuggestion As String = drSuggestionArchive("Suggestion")
                        litSuggestion.Text = strSuggestion

                        'set the author name and posted by date
                        Dim strAuthorUsername As String = drSuggestionArchive("author_username")
                        litPostedBy.Text = strAuthorUsername

                        Dim dtViewDate As DateTime = Convert.ToDateTime(drSuggestionArchive("viewDate"))

                        litSuggestionDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDateTime.Text = dtViewDate.ToString("h:mm tt")

                        'Load in this suggestion search tags
                        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin(), intSuggestionID)
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

            SuggestionDAL.RollbackSuggestion(intArchiveID)
            pnlConfirmation.Visible = True
            pnlPreview.Visible = False
        End If
    End Sub

End Class
