Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports Telerik.Web.UI

Partial Class Poll_PollDetail
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 9

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue
    Dim boolAllowPublicVotes As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            If Not Request.QueryString("id") Is Nothing Then

                If Not IsPostBack Then
                    hdnPollGuid.Value = Guid.NewGuid().ToString()
                End If
                Dim intPollID As Integer = Convert.ToInt32(Request.QueryString("id"))
                LoadPoll(intPollID)
            End If
        End If


    End Sub

    Protected Sub LoadPoll(ByVal intPollID As Integer)
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
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_public_vote_submission" Then
                boolAllowPublicVotes = True

            End If
        Next

        'If we find out the poll is an archived poll, but we do not allow achives then redirect to default page
        If boolAllowArchive = False And boolStatus = False Then
            Response.Redirect("default.aspx")
        End If


        'Load the Poll, and if the user is logged in, their previous answer
        Dim dtPoll As New DataTable
        If intMemberID > 0 Then
            dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_ByPollIDAndStatusAndAccess_FrontEnd_WithMemberSubmission(intPollID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPoll_ByPollIDAndStatus_FrontEnd_WithMemberSubmission(intPollID, boolStatus, intMemberID, intSiteID))
        Else
            dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_ByPollIDAndStatusAndAccess_FrontEnd(intPollID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPoll_ByPollIDAndStatus_FrontEnd(intPollID, boolStatus, intSiteID))
        End If
        If dtPoll.Rows.Count > 0 Then
            Dim drPoll As DataRow = dtPoll.Rows(0)

            'set the title and body
            Dim strQuestion As String = drPoll("QuestionHtml")

            'Set the page title Default
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Poll_FrontEnd.Poll_PollDetail_HeaderTitle + " - " + strQuestion

            litQuestion.Text = strQuestion


            'Load in the pollAnswers
            Dim boolQuestionRandomized As Boolean = Convert.ToBoolean(drPoll("AnswersRandomized"))
            Dim dtPollAnswers As DataTable
            If boolQuestionRandomized Then
                dtPollAnswers = PollDAL.GetPollAnswerList_ByPollID_FrontEnd_Random(intPollID)
            Else
                dtPollAnswers = PollDAL.GetPollAnswerList_ByPollID_FrontEnd(intPollID)
            End If

            rblPollAnswers.ID = rblPollAnswers.ID & "_" & hdnPollGuid.Value

            'if the user has already answered this question, then show their previous answer
            If Not IsPostBack Then

                rblPollAnswers.DataSource = dtPollAnswers
                rblPollAnswers.DataBind()

                If Not IsDBNull(drPoll("PollAnswerID")) Then
                    rblPollAnswers.SelectedValue = Convert.ToInt32(drPoll("PollAnswerID"))

                    'as the user has already voted on this question rename the vote button
                    btnVote.Text = Resources.Poll_FrontEnd.Poll_PollDetail_VoteUpdate
                Else
                    'user has not voted on this poll, so set the vote button text
                    btnVote.Text = Resources.Poll_FrontEnd.Poll_PollDetail_VoteAdd
                End If
            End If


            'set the author name and posted by date
            Dim strAuthorUsername As String = Resources.Poll_FrontEnd.Poll_PollDetail_PostedByDefault
            If Not drPoll("author_username") Is DBNull.Value Then
                strAuthorUsername = drPoll("author_username").ToString()
            End If
            litPostedBy.Text = strAuthorUsername

            Dim dtViewDate As DateTime = Convert.ToDateTime(drPoll("viewDate"))

            litPollDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
            litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
            litViewDateTime.Text = dtViewDate.ToString("h:mm tt")

            'Load in this polls search tags
            Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intPollID)
            If dtSearchTags.Rows.Count > 0 Then
                divModuleSearchTagList.Visible = True
                rptSearchTags.DataSource = dtSearchTags
                rptSearchTags.DataBind()
            End If

            'Finally show the edit link if the poll was uploaded by the currently logged in user
            If boolAllowOnlineSubmissions Then
                If Not drPoll("authorID_member") Is DBNull.Value Then
                    Dim intAuthorID_Member As Integer = Convert.ToInt32(drPoll("authorID_member"))
                    If intMemberID > 0 And intMemberID = intAuthorID_Member Then
                        divEditPoll.Visible = True
                    End If

                End If
            End If

            'set the page title
            If Not drPoll("metaTitle") Is DBNull.Value Then
                Dim strMetaTitle As String = drPoll("metaTitle").ToString()
                If strMetaTitle.Length > 0 Then
                    Page.Header.Title = strMetaTitle
                End If
            End If

            'set the page's keywords
            If Not drPoll("metaKeywords") Is DBNull.Value Then
                Dim keywordsHtmlMeta As New HtmlMeta()
                keywordsHtmlMeta.Name = "keywords"
                keywordsHtmlMeta.Content = drPoll("metaKeywords").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(keywordsHtmlMeta)
            End If

            'set the page's description
            If Not drPoll("metaDescription") Is DBNull.Value Then
                Dim descriptionHtmlMeta As New HtmlMeta()
                descriptionHtmlMeta.Name = "description"
                descriptionHtmlMeta.Content = drPoll("metaDescription").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(descriptionHtmlMeta)
            End If




            'Load Comments
            If boolAllowComments Then
                'setup the comments list, if we have enabled public comment submissions, then the member does not require to login to post a comment
                Dim intSiteID_ForPoll As Integer = Convert.ToInt32(drPoll("siteID"))
                ucCommentsModule.SetupCommentModule(intSiteID_ForPoll, ModuleTypeID, intPollID, intMemberID, boolStatus, boolEnablePublicCommentSubmission)
            End If
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub btnVote_Click(ByVal sender As Object, ByVal e As EventArgs)

        'Log Vote
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

                        litPollSubmissionMsg.Text = "<b>" + Resources.Poll_FrontEnd.Poll_PollDetail_VoteSubmitted_SuccessMessage + "</b><br/>"
                        litPollSubmissionMsg.Visible = True

                        'Change the button, to Change Vote, as the user has now voted! - Only if the member is logged in, otherwise, hide this button
                        If intMemberID > 0 Then
                            btnVote.Text = Resources.Poll_FrontEnd.Poll_PollRepeater_VoteUpdate
                        Else
                            btnVote.Visible = False
                        End If
                    Else
                        'User has choosen a an answer, but the question doesn't exist (should never happen)
                        litPollSubmissionMsg.Text = "<b>" + Resources.Poll_FrontEnd.Poll_PollDetail_VoteSubmitted_ErrorQuestionAnswerMessage + "</b><br/>"
                        litPollSubmissionMsg.Visible = True
                    End If

                Else
                    'User has choosen a non-existant answer (probably from hacking)
                    litPollSubmissionMsg.Text = "<b>" + Resources.Poll_FrontEnd.Poll_PollDetail_VoteSubmitted_ErrorAnswerMessage + "</b><br/>"
                    litPollSubmissionMsg.Visible = True
                End If

            Else
                litPollSubmissionMsg.Text = "<b>" + Resources.Poll_FrontEnd.Poll_PollDetail_VoteSubmitted_ErrorSelectAnswer + "</b><br/>"
                litPollSubmissionMsg.Visible = True
            End If
        Else
            litPollSubmissionMsg.Text = Resources.Poll_FrontEnd.Poll_PollDetail_VoteSubmitted_LoginToVote + "<br/><a href='/login/'>" + Resources.Poll_FrontEnd.Poll_PollDetail_VoteSubmitted_ClickHereToLogin + "</a><br/>"
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
