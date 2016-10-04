Imports System.Data

Partial Class Polls_PollRepeaterShort_Random
    Inherits System.Web.UI.UserControl

    Public ModuleTypeID As Integer = 9

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolEnableGroupsAndUserAccess As Boolean = False
    Dim boolAllowPublicVotes As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        If Not IsPostBack Then
            LoadLatestPollQuestionAndAnswers()
        End If

    End Sub

    Private Sub LoadLatestPollQuestionAndAnswers()

        'Check we need to handle group/user permissions
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_public_vote_submission" Then
                boolAllowPublicVotes = True
            End If

        Next
        'First get the lastPollID From Session, if exists.
        Dim intPollID_Last As Integer = 0
        If Not Session("LastPollID") Is Nothing Then
            intPollID_Last = Convert.ToInt32(Session("LastPollID"))
        End If
        Dim dtPollQuestion As DataTable
        If intMemberID > 0 Then
            'only if we have shown a poll before, we use NOT LAST POLL, else we just show a random poll
            If intPollID_Last > 0 Then
                dtPollQuestion = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_RandomAndAccess_FrontEnd_ButNotLastPoll_WithMemberSubmission(intPollID_Last, MemberDAL.GetCurrentMemberGroupIDs, intMemberID, intSiteID), PollDAL.GetPoll_Random_FrontEnd_ButNotLastPoll_WithMemberSubmission(intPollID_Last, intMemberID, intSiteID))
            Else
                dtPollQuestion = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_RandomAndAccess_FrontEnd_WithMemberSubmission(MemberDAL.GetCurrentMemberGroupIDs, intMemberID, intSiteID), PollDAL.GetPoll_Random_FrontEnd_WithMemberSubmission(intMemberID, intSiteID))
            End If

        Else
            'only if we have shown a poll before, we use NOT LAST POLL, else we just show a random poll
            If intPollID_Last > 0 Then
                dtPollQuestion = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_RandomAndAccess_FrontEnd_ButNotLastPoll(intPollID_Last, MemberDAL.GetCurrentMemberGroupIDs, intMemberID, intSiteID), PollDAL.GetPoll_Random_FrontEnd_ButNotLastPoll(intPollID_Last, intSiteID))
            Else
                dtPollQuestion = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_RandomAndAccess_FrontEnd(MemberDAL.GetCurrentMemberGroupIDs, intMemberID, intSiteID), PollDAL.GetPoll_Random_FrontEnd(intSiteID))
            End If
        End If


        If dtPollQuestion.Rows.Count > 0 Then
            Dim drPollQuestion As DataRow = dtPollQuestion.Rows(0)

            Dim intPollQuestionID As Integer = Convert.ToInt32(drPollQuestion("ID"))

            'We have the poll question id, so set our session variable, such that the next request will get a different poll question
            Session("LastPollID") = intPollQuestionID
            litQuestion.Text = drPollQuestion("QuestionHtml").ToString()

            Dim boolAnswersRandomized As Boolean = Convert.ToBoolean(drPollQuestion("AnswersRandomized"))
            Dim dtPollAnswers As DataTable
            If boolAnswersRandomized Then
                dtPollAnswers = PollDAL.GetPollAnswerList_ByPollID_FrontEnd_Random(intPollQuestionID)
            Else
                dtPollAnswers = PollDAL.GetPollAnswerList_ByPollID_FrontEnd(intPollQuestionID)
            End If
            If dtPollAnswers.Rows.Count > 0 Then
                'Now we have an existing Question and it has at least 1 answer, so we can show this poll panel
                divPoll.Visible = True


                'Load the answers into our radio button list
                rblPollAnswers.DataSource = dtPollAnswers
                rblPollAnswers.DataBind()

                'Check if this user has answered this question, if so set the vote button to "Change Vote", and pre-populate the answer
                If intMemberID > 0 Then
                    Dim boolPollSubmitted As Boolean = False
                    If Not drPollQuestion("PollAnswerID") Is DBNull.Value Then
                        Dim intPollSubmissionAnswerID As Integer = drPollQuestion("PollAnswerID")
                        rblPollAnswers.SelectedValue = intPollSubmissionAnswerID
                        'as the user has already voted on this question rename the vote button
                        btnVote.Text = Resources.Poll_FrontEnd.Poll_PollRepeaterShort_Random_VoteUpdate
                    Else
                        'user has not voted on this poll, so set the vote button text
                        btnVote.Text = Resources.Poll_FrontEnd.Poll_PollRepeaterShort_Random_VoteAdd

                    End If

                End If
            End If
        End If
    End Sub

    Protected Sub btnVote_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVote.Click
        'reset any existing error message
        litPollSubmissionMsg.Text = ""
        litPollSubmissionMsg.Visible = False

        'First check is the user must be a logged in member to vote or this module allows public votes
        If boolAllowPublicVotes Or intMemberID > 0 Then

            'Check the user has selected an answer, if not show message
            If rblPollAnswers.SelectedValue.Length > 0 Then

                'Get the selected poll answer
                Dim intPollAnswerID As Integer = Convert.ToInt32(rblPollAnswers.SelectedValue)
                Dim dtPollAnswer As DataTable = PollDAL.GetPollAnswer_ByIDAndSiteID(intPollAnswerID, intSiteID)
                If dtPollAnswer.Rows.Count > 0 Then
                    'Log the users vote
                    Dim drPollAnswer As DataRow = dtPollAnswer.Rows(0)
                    Dim intPollID As Integer = Convert.ToInt32(drPollAnswer("PollID"))
                    Dim strPollAnswer As String = drPollAnswer("Answer").ToString()
                    Dim boolPollAnswerIsCorrect = Convert.ToBoolean(drPollAnswer("IsCorrect"))

                    'Now get some additional question information
                    Dim dtPoll As DataTable = PollDAL.GetPoll_ByID(intPollID)
                    If dtPoll.Rows.Count > 0 Then
                        Dim drPoll As DataRow = dtPoll.Rows(0)

                        Dim intPollCategoryID As Integer = Integer.MinValue
                        If Not drPoll("CategoryID") Is DBNull.Value Then
                            intPollCategoryID = Convert.ToInt32(drPoll("CategoryID"))
                        End If

                        Dim strPollQuestion As String = drPoll("QuestionHtml").ToString()
                        Dim strIpAddress As String = Request.ServerVariables("LOCAL_ADDR")
                        Dim dtDateTimeStamp As DateTime = DateTime.Now

                        LogPollAnswerSubmission(intMemberID, intPollCategoryID, intPollID, strPollQuestion, intPollAnswerID, strPollAnswer, boolPollAnswerIsCorrect, strIpAddress, dtDateTimeStamp)

                        litPollSubmissionMsg.Text = "<b>" + Resources.Poll_FrontEnd.Poll_PollRepeaterShort_Random_SuccessMessage + "</b><br/>"
                        litPollSubmissionMsg.Visible = True

                        'Change the button, to Change Vote, as the user has now voted! - Only if the member is logged in, otherwise, hide this button
                        If intMemberID > 0 Then
                            btnVote.Text = Resources.Poll_FrontEnd.Poll_PollRepeaterShort_Random_VoteUpdate
                        Else
                            btnVote.Visible = False
                        End If
                    Else
                        'User has choosen a an answer, but the question doesn't exist (should never happen)
                        litPollSubmissionMsg.Text = "<b>" + Resources.Poll_FrontEnd.Poll_PollRepeaterShort_Random_ErrorQuestionAnswerMessage + "</b><br/>"
                        litPollSubmissionMsg.Visible = True
                    End If

                Else
                    'User has choosen a non-existant answer (probably from hacking)
                    litPollSubmissionMsg.Text = "<b>" + Resources.Poll_FrontEnd.Poll_PollRepeaterShort_Random_ErrorAnswerMessage + "</b><br/>"
                    litPollSubmissionMsg.Visible = True
                End If

            Else
                litPollSubmissionMsg.Text = "<b>" + Resources.Poll_FrontEnd.Poll_PollRepeaterShort_Random_ErrorSelectAnswer + "</b><br/>"
                litPollSubmissionMsg.Visible = True
            End If
        Else
            litPollSubmissionMsg.Text = Resources.Poll_FrontEnd.Poll_PollRepeaterShort_Random_LoginToVote + "<br/><a href='/login/'>" + Resources.Poll_FrontEnd.Poll_PollRepeaterShort_Random_VoteSubmitted_ClickHereToLogin + "</a><br/>"
            litPollSubmissionMsg.Visible = True
        End If

    End Sub

    Private Sub LogPollAnswerSubmission(ByVal MemberID As Integer, ByVal PollCategoryID As Integer, ByVal PollID As Integer, ByVal PollQuestion As String, ByVal PollAnswerID As Integer, ByVal PollAnswer As String, ByVal PollAnswerIsCorrect As Boolean, ByVal IpAddress As String, ByVal DateTimeStamp As DateTime)
        'Then we have found the users answer and its corresponding quesiton is valid

        'We don't want the user submitting multiple votes, so before we save their selection, we check if they have already made a selection.
        'If they have not already made a selection, then add their vote as a new selection, otherwise update their existing selection 
        Dim dtPollAnswerSubmission As DataTable = PollDAL.GetPollSubmissionRollup_ByMemberIDAndPollID(MemberID, PollID)
        If dtPollAnswerSubmission.Rows.Count = 0 Then
            'this is the users first vote for this poll question
            Dim intPollAnswerSubmissionID As Integer = PollDAL.InsertPollSubmissionRollup(MemberID, PollCategoryID, PollID, PollQuestion, PollAnswerID, PollAnswer, PollAnswerIsCorrect, IpAddress, DateTimeStamp)
        Else
            'the user has already voted on this poll question, so update their vote
            Dim drPollAnswerSubmission As DataRow = dtPollAnswerSubmission.Rows(0)
            Dim intPollAnswerSubmissionID As Integer = Convert.ToInt32(drPollAnswerSubmission("ID"))

            PollDAL.UpdatePollSubmissionRollup_ByID(intPollAnswerSubmissionID, MemberID, PollCategoryID, PollID, PollQuestion, PollAnswerID, PollAnswer, PollAnswerIsCorrect, IpAddress)
        End If

    End Sub

End Class
