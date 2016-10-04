
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_poll_previewanswer
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 9 ' Module Type: Poll

    Public Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim strPollAnswerID As String = btnCancel.CommandArgument
        Response.Redirect("editAddAnswer.aspx?id=" & strPollAnswerID.ToString())
    End Sub

    Public Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Dim strPollID As String = btnClose.CommandArgument
        Response.Redirect("answers.aspx?PollID=" & strPollID.ToString())
    End Sub


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a rollback confirmation to our rollback button
        CommonWeb.SetupRollBackButton(btnRollBack, Resources.Poll_Admin.Poll_PreviewAnswer_RollBack_ConfirmationMessage)

        'Set the Header UserControls Title and Help Item if it exists
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Poll_Admin.Poll_PreviewAnswer_ModuleName)
        ucHeader.PageName = Resources.Poll_Admin.Poll_PreviewAnswer_Header
        ucHeader.PageHelpID = 7 'Help Item for Polls

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            If Not Request.QueryString("archiveID") Is Nothing Then
                Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

                Dim dtPollAnswerArchive As DataTable = PollDAL.GetPollAnswerArchive_ByArchiveIDAndSiteID(intArchiveID, SiteDAL.GetCurrentSiteID_Admin())
                If dtPollAnswerArchive.Rows.Count > 0 Then

                    Dim drPollAnswerArchive As DataRow = dtPollAnswerArchive.Rows(0)

                    'Set the cancel button to go back to the orginial poll entry we are dealing with
                    Dim intPollAnswerID As Integer = Convert.ToInt32(drPollAnswerArchive("ID"))

                    'Now we must also check the record that the archive corresponds to actually exists, also as we don't store the images with this record in the archive table, we may also need to load the current images to show with this preview.
                    Dim dtPollAnswer As DataTable = PollDAL.GetPollAnswer_ByIDAndSiteID(intPollAnswerID, SiteDAL.GetCurrentSiteID_Admin())
                    If dtPollAnswer.Rows.Count > 0 Then
                        Dim drPollAnswer As DataRow = dtPollAnswer.Rows(0)


                        btnCancel.CommandArgument = intPollAnswerID.ToString()

                        'Now get the poll this poll answer corresponds to so we can show what the answer would look like in the actual poll
                        Dim intPollID As Integer = Convert.ToInt32(drPollAnswerArchive("PollID"))
                        btnClose.CommandArgument = intPollID.ToString()

                        Dim dtPoll As DataTable = PollDAL.GetPoll_ByID(intPollID)
                        If dtPoll.Rows.Count > 0 Then

                            'Populate the preview pages info box with detail on the pollArchive
                            Dim intPollAnswerVersion As Integer = Convert.ToInt32(drPollAnswer("version"))
                            Dim intPollAnswerArchiveVersion As Integer = Convert.ToInt32(drPollAnswerArchive("version"))

                            'If the archived version is the same as the most recent version, we represent this by prepending text to it saying it is also the current version
                            litInformationBox_Version.Text = intPollAnswerArchiveVersion.ToString()
                            If intPollAnswerVersion = intPollAnswerArchiveVersion Then
                                litInformationBox_Version.Text = Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_Version_Current & " (" & litInformationBox_Version.Text & ")"
                            End If

                            litInformationBox_DateCreated.Text = drPollAnswerArchive("dateTimeStamp")

                            litInformationBox_AuthorName.Text = Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_AuthorDefault
                            If Not drPollAnswerArchive("authorID_member") Is DBNull.Value Then
                                Dim intAuthorID_member As Integer = drPollAnswerArchive("authorID_member")
                                Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intAuthorID_member)
                                If dtMember.Rows.Count > 0 Then
                                    Dim drMember As DataRow = dtMember.Rows(0)

                                    Dim strAuthorName As String = drMember("FirstName") & " " & drMember("LastName")
                                    litInformationBox_AuthorName.Text = strAuthorName
                                End If
                            ElseIf Not drPollAnswerArchive("authorID_admin") Is DBNull.Value Then
                                Dim intAuthorID_admin As Integer = drPollAnswerArchive("authorID_admin")
                                Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAuthorID_admin)
                                If dtAdminUser.Rows.Count > 0 Then
                                    Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                                    Dim strAuthorName As String = drAdminUser("First_Name") & " " & drAdminUser("Last_Name")
                                    litInformationBox_AuthorName.Text = strAuthorName
                                End If
                            End If

                            Dim boolStatus As Boolean = Convert.ToBoolean(drPollAnswerArchive("Status"))
                            litInformationBox_Status.Text = If(boolStatus, Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_StatusActive, Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_StatusArchive)

                            Dim boolAnswerIsCorrect As Boolean = Convert.ToBoolean(drPollAnswerArchive("IsCorrect"))
                            litInformationBox_AnswerCorrect.Text = If(boolAnswerIsCorrect, Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_AnswerCorrectTrue, Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_AnswerCorrectFalse)

                            If Not drPollAnswerArchive("publicationDate").ToString() = "" Then
                                litInformationBox_PublicationDate.Text = drPollAnswerArchive("publicationDate").ToString()
                                divInformationBox_PublicationDate.Visible = True
                            End If

                            If Not drPollAnswerArchive("expirationDate").ToString() = "" Then
                                Dim dtExpirationDate As DateTime = Convert.ToDateTime(drPollAnswerArchive("expirationDate"))
                                litInformationBox_ExpirationDate.Text = dtExpirationDate.ToString()
                                divInformationBox_ExpirationDate.Visible = True

                                If dtExpirationDate < DateTime.Now Then
                                    divExpired.Visible = True
                                End If

                            End If

                            If Not drPollAnswerArchive("Description") Is DBNull.Value Then
                                litInformationBox_Description.Text = drPollAnswerArchive("Description").ToString()
                                divInformationBox_Description.Visible = True
                            End If


                            'Populate the preview page with the current poll question and answers, so it resembles the front end, and we replace the archived poll answer with the answer from the actual poll
                            Dim drPoll As DataRow = dtPoll.Rows(0)

                            'set the title and body
                            Dim strQuestion As String = drPoll("QuestionHtml")
                            litQuestion.Text = strQuestion

                            'Load in the archived poll answer
                            Dim strPollAnswer As String = drPollAnswerArchive("Answer")
                            rblPollAnswers.Items.Add(New ListItem(strPollAnswer, intPollAnswerID))


                            btnVote.Text = Resources.Poll_Admin.Poll_PreviewAnswer_VoteAdd

                            'set the author name and posted by date
                            Dim strAuthorUsername As String = Resources.Poll_Admin.Poll_PreviewAnswer_PostedByDefault
                            If Not drPoll("author_username") Is DBNull.Value Then
                                strAuthorUsername = drPoll("author_username").ToString()
                            End If
                            litPostedBy.Text = strAuthorUsername

                            Dim dtViewDate As DateTime = Convert.ToDateTime(drPoll("viewDate"))

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
        litPollSubmissionMsg.Text = "<b>" + Resources.Poll_Admin.Poll_PreviewAnswer_VoteSubmitted_SuccessMessage + "</b><br/>"
        litPollSubmissionMsg.Visible = True
    End Sub


    Public Sub btnRollBack_OnClick(ByVal sender As Object, ByVal e As EventArgs)

        If Not Request.QueryString("archiveID") Is Nothing Then

            Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

            PollDAL.RollbackPollAnswer(intArchiveID)
            pnlConfirmation.Visible = True
            pnlPreview.Visible = False
        End If
    End Sub

End Class
