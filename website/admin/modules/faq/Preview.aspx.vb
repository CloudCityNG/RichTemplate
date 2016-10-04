
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_faq_preview
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 6 ' Module Type: FAQ

    Public Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim intFaqID As String = btnCancel.CommandArgument
        Response.Redirect("editAdd.aspx?id=" & intFaqID.ToString())
    End Sub

    Public Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("default.aspx")
    End Sub


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a rollback confirmation to our rollback button
        CommonWeb.SetupRollBackButton(btnRollBack, Resources.Faq_Admin.Faq_Preview_RollBack_ConfirmationMessage)

        'Set the Header UserControls Title and Help Item if it exists
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Faq_Admin.Faq_Preview_ModuleName)
        ucHeader.PageName = Resources.Faq_Admin.Faq_Preview_Header
        ucHeader.PageHelpID = 6 'Help Item for FAQs

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            If Not Request.QueryString("archiveID") Is Nothing Then
                Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

                Dim dtFaqArchive As DataTable = FaqDAL.GetFaqArchive_ByArchiveIDAndSiteID(intArchiveID, SiteDAL.GetCurrentSiteID_Admin())
                If dtFaqArchive.Rows.Count > 0 Then
                    Dim drFaqArchive As DataRow = dtFaqArchive.Rows(0)

                    'Set the cancel button to go back to the orginial faq in question
                    Dim intFaqID As Integer = Convert.ToInt32(drFaqArchive("FaqID"))

                    'Now we must also check the record that the archive corresponds to actually exists, also as we don't store the images with this record in the archive table, we may also need to load the current images to show with this preview.
                    Dim dtFaq As DataTable = FaqDAL.GetFaq_ByFaqID(intFaqID)
                    If dtFaq.Rows.Count > 0 Then
                        Dim drFaq As DataRow = dtFaq.Rows(0)

                        btnCancel.CommandArgument = intFaqID.ToString()

                        'Populate the preview pages info box
                        Dim intFaqVersion As Integer = Convert.ToInt32(drFaq("version"))
                        Dim intFaqArchiveVersion As Integer = Convert.ToInt32(drFaqArchive("version"))

                        'If the archived version is the same as the most recent version, we represent this by prepending text to it saying it is also the current version
                        litInformationBox_Version.Text = intFaqArchiveVersion.ToString()
                        If intFaqVersion = intFaqArchiveVersion Then
                            litInformationBox_Version.Text = Resources.Faq_Admin.Faq_Preview_InformationBox_Version_Current & " (" & litInformationBox_Version.Text & ")"
                        End If

                        litInformationBox_DateCreated.Text = drFaqArchive("dateTimeStamp")

                        litInformationBox_AuthorName.Text = Resources.Faq_Admin.Faq_Preview_InformationBox_AuthorDefault
                        If Not drFaqArchive("authorID_member") Is DBNull.Value Then
                            Dim intAuthorID_member As Integer = drFaqArchive("authorID_member")
                            Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intAuthorID_member)
                            If dtMember.Rows.Count > 0 Then
                                Dim drMember As DataRow = dtMember.Rows(0)

                                Dim strAuthorName As String = drMember("FirstName") & " " & drMember("LastName")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        ElseIf Not drFaqArchive("authorID_admin") Is DBNull.Value Then
                            Dim intAuthorID_admin As Integer = drFaqArchive("authorID_admin")
                            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAuthorID_admin)
                            If dtAdminUser.Rows.Count > 0 Then
                                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                                Dim strAuthorName As String = drAdminUser("First_Name") & " " & drAdminUser("Last_Name")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        End If

                        Dim strCategoryName As String = Resources.Faq_Admin.Faq_Preview_InformationBox_UnCategorized
                        If Not drFaqArchive("CategoryName") Is DBNull.Value Then
                            strCategoryName = drFaqArchive("CategoryName")
                        End If
                        litInformationBox_Category.Text = strCategoryName

                        Dim boolStatus As Boolean = Convert.ToBoolean(drFaqArchive("Status"))
                        litInformationBox_Status.Text = If(boolStatus, Resources.Faq_Admin.Faq_Preview_InformationBox_StatusActive, Resources.Faq_Admin.Faq_Preview_InformationBox_StatusArchive)

                        If Not drFaqArchive("publicationDate").ToString() = "" Then
                            litInformationBox_PublicationDate.Text = drFaqArchive("publicationDate").ToString()
                            divInformationBox_PublicationDate.Visible = True
                        End If

                        If Not drFaqArchive("expirationDate").ToString() = "" Then
                            Dim dtExpirationDate As DateTime = Convert.ToDateTime(drFaqArchive("expirationDate"))
                            litInformationBox_ExpirationDate.Text = dtExpirationDate.ToString()
                            divInformationBox_ExpirationDate.Visible = True

                            If dtExpirationDate < DateTime.Now Then
                                divExpired.Visible = True
                            End If

                        End If


                        'Populate the preview page such that it is similar to the front-end version

                        'set the question and asnwer
                        Dim strQuestion As String = drFaqArchive("Question")
                        litQuestion.Text = strQuestion

                        Dim strAnswer As String = drFaqArchive("Answer")
                        litAnswer.Text = strAnswer

                        'set the author name and posted by date
                        Dim strAuthorUsername As String = Resources.Faq_Admin.Faq_Preview_PostedByDefault
                        If Not drFaqArchive("author_username") Is DBNull.Value Then
                            strAuthorUsername = drFaqArchive("author_username").ToString()
                        End If

                        litPostedBy.Text = strAuthorUsername

                        Dim dtViewDate As DateTime = Convert.ToDateTime(drFaqArchive("viewDate"))

                        litFaqDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDateTime.Text = dtViewDate.ToString("h:mm tt")

                        'Load in this faq search tags
                        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin(), intFaqID)
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

            FaqDAL.RollbackFaq(intArchiveID)
            pnlConfirmation.Visible = True
            pnlPreview.Visible = False
        End If
    End Sub

End Class
