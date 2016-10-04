
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_poll_preview
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 9 ' Module Type: Poll

    Public Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim strPollID As String = btnCancel.CommandArgument
        Response.Redirect("editAdd.aspx?id=" & strPollID.ToString())
    End Sub

    Public Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("default.aspx")
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a rollback confirmation to our rollback button
        CommonWeb.SetupRollBackButton(btnRollBack, Resources.Poll_Admin.Poll_Preview_RollBack_ConfirmationMessage)

        'Set the Header UserControls Title and Help Item if it exists
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Poll_Admin.Poll_Preview_ModuleName)
        ucHeader.PageName = Resources.Poll_Admin.Poll_Preview_Header
        ucHeader.PageHelpID = 7 'Help Item for Polls

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            If Not Request.QueryString("archiveID") Is Nothing Then
                Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

                Dim dtPollArchive As DataTable = PollDAL.GetPollArchive_ByArchiveIDAndSiteID(intArchiveID, SiteDAL.GetCurrentSiteID_Admin())
                If dtPollArchive.Rows.Count > 0 Then

                    Dim drPollArchive As DataRow = dtPollArchive.Rows(0)

                    'Set the cancel button to go back to the orginial poll entry we are dealing with
                    Dim intPollID As Integer = Convert.ToInt32(drPollArchive("ID"))

                    'Now we must also check the record that the archive corresponds to actually exists, also as we don't store the images with this record in the archive table, we may also need to load the current images to show with this preview.
                    Dim dtPoll As DataTable = PollDAL.GetPoll_ByID(intPollID)
                    If dtPoll.Rows.Count > 0 Then
                        Dim drPoll As DataRow = dtPoll.Rows(0)

                        btnCancel.CommandArgument = intPollID.ToString()

                        'Populate the preview pages info box
                        Dim intPollVersion As Integer = Convert.ToInt32(drPoll("version"))
                        Dim intPollArchiveVersion As Integer = Convert.ToInt32(drPollArchive("version"))

                        'If the archived version is the same as the most recent version, we represent this by prepending text to it saying it is also the current version
                        litInformationBox_Version.Text = intPollArchiveVersion.ToString()
                        If intPollVersion = intPollArchiveVersion Then
                            litInformationBox_Version.Text = Resources.Poll_Admin.Poll_Preview_InformationBox_Version_Current & " (" & litInformationBox_Version.Text & ")"
                        End If

                        litInformationBox_DateCreated.Text = drPollArchive("dateTimeStamp")

                        litInformationBox_AuthorName.Text = Resources.Poll_Admin.Poll_Preview_InformationBox_AuthorDefault
                        If Not drPollArchive("authorID_member") Is DBNull.Value Then
                            Dim intAuthorID_member As Integer = drPollArchive("authorID_member")
                            Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intAuthorID_member)
                            If dtMember.Rows.Count > 0 Then
                                Dim drMember As DataRow = dtMember.Rows(0)

                                Dim strAuthorName As String = drMember("FirstName") & " " & drMember("LastName")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        ElseIf Not drPollArchive("authorID_admin") Is DBNull.Value Then
                            Dim intAuthorID_admin As Integer = drPollArchive("authorID_admin")
                            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAuthorID_admin)
                            If dtAdminUser.Rows.Count > 0 Then
                                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                                Dim strAuthorName As String = drAdminUser("First_Name") & " " & drAdminUser("Last_Name")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        End If

                        Dim strCategoryName As String = Resources.Poll_Admin.Poll_Preview_InformationBox_UnCategorized
                        If Not drPollArchive("CategoryName") Is DBNull.Value Then
                            strCategoryName = drPollArchive("CategoryName")
                        End If
                        litInformationBox_Category.Text = strCategoryName

                        Dim boolStatus As Boolean = Convert.ToBoolean(drPollArchive("Status"))
                        litInformationBox_Status.Text = If(boolStatus, Resources.Poll_Admin.Poll_Preview_InformationBox_StatusActive, Resources.Poll_Admin.Poll_Preview_InformationBox_StatusArchive)

                        Dim bollAnswersRandomized As Boolean = Convert.ToBoolean(drPollArchive("AnswersRandomized"))
                        litInformationBox_AnswersRandomized.Text = If(bollAnswersRandomized, Resources.Poll_Admin.Poll_Preview_InformationBox_AnswersRandomizedTrue, Resources.Poll_Admin.Poll_Preview_InformationBox_AnswersRandomizedFalse)

                        If Not drPollArchive("publicationDate").ToString() = "" Then
                            litInformationBox_PublicationDate.Text = drPollArchive("publicationDate").ToString()
                            divInformationBox_PublicationDate.Visible = True
                        End If

                        If Not drPollArchive("expirationDate").ToString() = "" Then
                            Dim dtExpirationDate As DateTime = Convert.ToDateTime(drPollArchive("expirationDate"))
                            litInformationBox_ExpirationDate.Text = dtExpirationDate.ToString()
                            divInformationBox_ExpirationDate.Visible = True

                            If dtExpirationDate < DateTime.Now Then
                                divExpired.Visible = True
                            End If

                        End If
                        If Not drPollArchive("Description") Is DBNull.Value Then
                            litInformationBox_Description.Text = drPollArchive("Description").ToString()
                            divInformationBox_Description.Visible = True
                        End If
                        'Populate the preview page such that it is similar to the front-end version

                        'set the title and body
                        Dim strQuestion As String = drPollArchive("QuestionHtml")
                        litQuestion.Text = strQuestion

                        'Load in the pollAnswers
                        Dim dtPollAnswers As DataTable
                        If bollAnswersRandomized Then
                            dtPollAnswers = PollDAL.GetPollAnswerList_ByPollID_FrontEnd_Random(intPollID)
                        Else
                            dtPollAnswers = PollDAL.GetPollAnswerList_ByPollID_FrontEnd(intPollID)
                        End If

                        rblPollAnswers.DataSource = dtPollAnswers
                        rblPollAnswers.DataBind()

                        btnVote.Text = Resources.Poll_Admin.Poll_Preview_VoteAdd

                        'set the author name and posted by date
                        Dim strAuthorUsername As String = Resources.Poll_Admin.Poll_Preview_PostedByDefault
                        If Not drPollArchive("author_username") Is DBNull.Value Then
                            strAuthorUsername = drPollArchive("author_username").ToString()
                        End If
                        litPostedBy.Text = strAuthorUsername

                        Dim dtViewDate As DateTime = Convert.ToDateTime(drPollArchive("viewDate"))

                        litPollDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDateTime.Text = dtViewDate.ToString("h:mm tt")

                        'Load in this polls search tags
                        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin(), intPollID)
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

    Protected Sub btnVote_Click(ByVal sender As Object, ByVal e As EventArgs)
        litPollSubmissionMsg.Text = "<b>" + Resources.Poll_Admin.Poll_Preview_VoteSubmitted_SuccessMessage + "</b><br/>"
        litPollSubmissionMsg.Visible = True
    End Sub


    Public Sub btnRollBack_OnClick(ByVal sender As Object, ByVal e As EventArgs)

        If Not Request.QueryString("archiveID") Is Nothing Then

            Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

            PollDAL.RollbackPoll(intArchiveID)
            pnlConfirmation.Visible = True
            pnlPreview.Visible = False
        End If
    End Sub

End Class
