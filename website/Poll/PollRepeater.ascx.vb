Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI


Partial Class Poll_PollRepeater
    Inherits System.Web.UI.UserControl

    Public ModuleTypeID As Integer = 9

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolStatus As Boolean = True
    Dim boolAllowComments As Boolean = False
    Dim boolShowAddThis As Boolean = False
    Dim boolEnableGroupsAndUserAccess As Boolean = False
    Dim boolAllowPublicVotes As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'Only load the repeater if its visible
        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            'Only set this control up if its visible, e.g. no id supplied, if id is supplied we load the details page
            'Check we need to show the comments, 'Add This' component
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" Then
                    boolAllowComments = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "show_add_this" Then
                    boolShowAddThis = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                    boolEnableGroupsAndUserAccess = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_public_vote_submission" Then
                    boolAllowPublicVotes = True
                End If
            Next

            'Finally load polls
            If Not IsPostBack Then
                rlvPoll_LoadWithSortExpression("viewDate", RadListViewSortOrder.Descending)
            End If

            'We usually don't rebind the poll list here, but this control, is slightly different and needs to be re-worked in the future
            rlvPoll.Rebind()

        End If

    End Sub

    Protected Sub rlvPoll_LoadWithSortExpression(ByVal strSortString As String, ByVal rlvSortOrder As RadListViewSortOrder)

        'Clear the current sort expressions
        rlvPoll.SortExpressions.Clear()

        If Not strSortString Is String.Empty And rlvPoll.Items.Count > 0 Then

            'Set up our RadListView Grid Sort Expression
            Dim rlvSortExpression As New RadListViewSortExpression()
            rlvSortExpression.SortOrder = rlvSortOrder
            rlvSortExpression.FieldName = strSortString

            rlvPoll.SortExpressions.AddSortExpression(rlvSortExpression)

        End If
        'We do not rebind the poll list, this control, is slightly different and needs to be re-worked in the future
        'rlvPoll.Rebind()
    End Sub

    Protected Sub rlvPoll_NeedDataSource(ByVal sender As Object, ByVal e As RadListViewNeedDataSourceEventArgs) Handles rlvPoll.NeedDataSource

        'First we check if we have PollAnswerHashTable in view state, if not add it to view state
        If ViewState("PollAnswerHashTable") Is Nothing Then
            ViewState("PollAnswerHashTable") = New Hashtable()
        End If
        If Request.QueryString("archive") = "1" Then
            boolStatus = False
        End If

        'Define default category ID and Sort Order
        Dim dtPoll As New DataTable

        'Check if we should load poll records based on Search Tags
        Dim paramSearchTagID As Integer = 0
        If Not Request.QueryString("sTag") Is Nothing Then
            Dim strSearchTag As String = HttpUtility.UrlDecode(Request.QueryString("sTag"))
            Dim dtSearchTag As DataTable = SearchTagDAL.GetSearchTag_BySearchTagNameAndSiteID(strSearchTag, intSiteID)
            If dtSearchTag.Rows.Count > 0 Then
                paramSearchTagID = dtSearchTag.Rows(0)("SearchTagID")
            End If
        End If

        'populate our dataset with polls, taking into account the membersID if logged in
        If paramSearchTagID > 0 Then
            If intMemberID > 0 Then
                dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPollList_BySearchTagIDAndStatusAndAccess_FrontEnd_WithMemberSubmission(paramSearchTagID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPollList_BySearchTagIDAndStatus_FrontEnd_WithMemberSubmission(paramSearchTagID, boolStatus, intMemberID, intSiteID))
            Else
                dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPollList_BySearchTagIDAndStatusAndAccess_FrontEnd(paramSearchTagID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPollList_BySearchTagIDAndStatus_FrontEnd(paramSearchTagID, boolStatus, intSiteID))
            End If
        ElseIf Request.QueryString("catID") <> "" Then
            Dim intCatID As Integer = Convert.ToInt32(Request.QueryString("catID"))
            If intCatID = 0 Then
                'Load polls that are un-categorized
                If intMemberID > 0 Then
                    dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_ByCategoryNullAndStatusAndAccess_FrontEnd_WithMemberSubmission(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPoll_ByCategoryNullAndStatus_FrontEnd_WithMemberSubmission(boolStatus, intMemberID, intSiteID))
                Else
                    dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_ByCategoryNullAndStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPoll_ByCategoryNullAndStatus_FrontEnd(boolStatus, intSiteID))
                End If

            Else
                'Load polls that are part of a specific category
                If intMemberID > 0 Then
                    dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_ByCategoryIDAndStatusAndAccess_FrontEnd_WithMemberSubmission(intCatID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPoll_ByCategoryIDAndStatus_FrontEnd_WithMemberSubmission(intCatID, boolStatus, intMemberID, intSiteID))
                Else
                    dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_ByCategoryIDAndStatusAndAccess_FrontEnd(intCatID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPoll_ByCategoryIDAndStatus_FrontEnd(intCatID, boolStatus, intSiteID))
                End If
            End If
        Else
            'We want all polls from all categories
            If intMemberID > 0 Then
                dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_ByStatusAndAccess_FrontEnd_WithMemberSubmission(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPoll_ByStatus_FrontEnd_WithMemberSubmission(boolStatus, intMemberID, intSiteID))
            Else
                dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_ByStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPoll_ByStatus_FrontEnd(boolStatus, intSiteID))
            End If

        End If

        rlvPoll.DataSource = dtPoll

        'Note if we have no rows, we clear sort expressions
        If dtPoll.Rows.Count = 0 Then
            rlvPoll.SortExpressions.Clear()
        End If
    End Sub

    'Protected Sub rlvPoll_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles rlvPoll.DataBound
    '    Dim pager As RadDataPager = DirectCast(rlvPoll.FindControl("rdPagerPoll"), RadDataPager)
    '    pager.Visible = (pager.PageSize < pager.TotalRowCount)

    'End Sub

    Protected Sub rlvPoll_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListViewItemEventArgs) Handles rlvPoll.ItemDataBound
        If TypeOf e.Item Is RadListViewDataItem Then
            Dim item As RadListViewDataItem = TryCast(e.Item, RadListViewDataItem)


            'Load in this polls search tags
            Dim intPollID As Integer = Convert.ToInt32(DataBinder.Eval(item.DataItem, "ID"))

            Dim pollDateTime As Literal = CType(e.Item.FindControl("pollDateTime"), Literal)
            pollDateTime.Text = DataBinder.Eval(item.DataItem, "viewdate", "{0:D}").ToString()
            pollDateTime.Visible = True

            'Load in the pollAnswers
            Dim boolAnswersRandomized As Boolean = Convert.ToBoolean(DataBinder.Eval(item.DataItem, "AnswersRandomized"))
            Dim dtPollAnswers As DataTable
            If boolAnswersRandomized Then
                dtPollAnswers = GenerateOrderedAnswerList(intPollID)
            Else
                dtPollAnswers = PollDAL.GetPollAnswerList_ByPollID_FrontEnd(intPollID)
            End If

            Dim rblPollAnswers As RadioButtonList = CType(e.Item.FindControl("rblPollAnswers"), RadioButtonList)

            rblPollAnswers.DataSource = dtPollAnswers
            rblPollAnswers.DataBind()

            Dim btnVote As Button = CType(e.Item.FindControl("btnVote"), Button)
            If Not IsDBNull(DataBinder.Eval(item.DataItem, "PollAnswerID")) Then
                If Not IsPostBack Then
                    rblPollAnswers.SelectedValue = Convert.ToInt32(DataBinder.Eval(item.DataItem, "PollAnswerID"))
                End If

                'as the user has already voted on this question rename the vote button
                btnVote.Text = Resources.Poll_FrontEnd.Poll_PollRepeater_VoteUpdate
            Else
                'user has not voted on this poll, so set the vote button text
                btnVote.Text = Resources.Poll_FrontEnd.Poll_PollRepeater_VoteAdd
            End If

            Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intPollID)
            If dtSearchTags.Rows.Count > 0 Then
                Dim divModuleSearchTagList As HtmlGenericControl = e.Item.FindControl("divModuleSearchTagList")
                divModuleSearchTagList.Visible = True
                Dim rptSearchTags As Repeater = e.Item.FindControl("rptSearchTags")
                rptSearchTags.DataSource = dtSearchTags
                rptSearchTags.DataBind()
            End If

            'Check we need to show the 'bookmark this' placeholder
            If boolShowAddThis Then
                item.FindControl("plcAddThis").Visible = True
            End If

            'Check we need to show the comments placeholder
            If boolAllowComments Then
                item.FindControl("plcComments").Visible = True
                'If we are showing active records then show the 'add comment' link
                If boolStatus Then
                    item.FindControl("plcAddCommentLink").Visible = True
                End If
            End If

        End If
    End Sub

    Private Function GenerateOrderedAnswerList(ByVal intPollID As Integer) As DataTable
        'Now we want to store the answer orders in our hashtable
        Dim PollAnswerHashTable As Hashtable = ViewState("PollAnswerHashTable")
        Dim dtPollAnswers As DataTable = PollDAL.GetPollAnswerList_ByPollID_FrontEnd_Random(intPollID)

        If PollAnswerHashTable(intPollID) Is Nothing Then
            'Then we populate this with our list of answers
            Dim intPollAnswerIDs As New List(Of Integer)
            For Each drPollAnswers As DataRow In dtPollAnswers.Rows
                intPollAnswerIDs.Add(Convert.ToInt32(drPollAnswers("ID")))
            Next
            PollAnswerHashTable(intPollID) = intPollAnswerIDs
            Return dtPollAnswers
        Else
            'we change the order of these answers to match the original order, by cloning this datatable and adding rows to it
            Dim intPollAnswerIDs As List(Of Integer) = PollAnswerHashTable(intPollID)
            Dim dtPollAnswers_Clone As DataTable = dtPollAnswers.Clone

            For Each intPollAnswerID As Integer In intPollAnswerIDs
                'Get the row with this answerID and add it to our cloned table
                Dim drPollAnswer_Clone As DataRow = dtPollAnswers_Clone.NewRow()
                For Each drPollAnswer As DataRow In dtPollAnswers.Rows
                    Dim intPollAnswerID_CurrentRow As Integer = Convert.ToInt32(drPollAnswer("ID"))
                    If intPollAnswerID_CurrentRow = intPollAnswerID Then
                        drPollAnswer_Clone.ItemArray = drPollAnswer.ItemArray
                        dtPollAnswers_Clone.Rows.Add(drPollAnswer_Clone)
                        Exit For
                    End If
                Next
            Next
            Return dtPollAnswers_Clone
        End If

    End Function

    Protected Sub btnVote_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btnVote As Button = sender
        Dim radListViewDataItem As RadListViewDataItem = btnVote.Parent

        'Log Vote
        'reset any existing error message
        Dim litPollSubmissionMsg As Literal = radListViewDataItem.FindControl("litPollSubmissionMsg")
        litPollSubmissionMsg.Text = ""
        litPollSubmissionMsg.Visible = False

        'First check is the user must be a logged in member to vote or this module allows public votes
        If boolAllowPublicVotes Or intMemberID > 0 Then

            Dim rblPollAnswers As RadioButtonList = radListViewDataItem.FindControl("rblPollAnswers")
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

                        litPollSubmissionMsg.Text = "<b>" + Resources.Poll_FrontEnd.Poll_PollRepeater_VoteSubmitted_SuccessMessage + "</b><br/>"
                        litPollSubmissionMsg.Visible = True

                        'Change the button, to Change Vote, as the user has now voted! - Only if the member is logged in, otherwise, hide this button
                        If intMemberID > 0 Then
                            btnVote.Text = Resources.Poll_FrontEnd.Poll_PollRepeater_VoteUpdate
                        Else
                            btnVote.Visible = False
                        End If

                    Else
                        'User has choosen a an answer, but the question doesn't exist (should never happen)
                        litPollSubmissionMsg.Text = "<b>" + Resources.Poll_FrontEnd.Poll_PollRepeater_VoteSubmitted_ErrorQuestionAnswerMessage + "</b><br/>"
                        litPollSubmissionMsg.Visible = True
                    End If

                    Else
                        'User has choosen a non-existant answer (probably from hacking)
                        litPollSubmissionMsg.Text = "<b>" + Resources.Poll_FrontEnd.Poll_PollRepeater_VoteSubmitted_ErrorAnswerMessage + "</b><br/>"
                        litPollSubmissionMsg.Visible = True
                    End If

            Else
                litPollSubmissionMsg.Text = "<b>" + Resources.Poll_FrontEnd.Poll_PollRepeater_VoteSubmitted_ErrorSelectAnswer + "</b><br/>"
                litPollSubmissionMsg.Visible = True
            End If
        Else
            litPollSubmissionMsg.Text = Resources.Poll_FrontEnd.Poll_PollRepeater_VoteSubmitted_LoginToVote + "<br/><a href='/login/'>" + Resources.Poll_FrontEnd.Poll_PollRepeater_VoteSubmitted_ClickHereToLogin + "</a><br/>"
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


    Protected Sub imgSortUpQuestion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortUpQuestion.Click
        rlvPoll_LoadWithSortExpression("Question", RadListViewSortOrder.Ascending)

        'We usually don't rebind the poll list here, but this control, is slightly different and needs to be re-worked in the future
        rlvPoll.Rebind()
    End Sub

    Protected Sub imgSortDownQuestion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortDownQuestion.Click
        rlvPoll_LoadWithSortExpression("Question", RadListViewSortOrder.Descending)

        'We usually don't rebind the poll list here, but this control, is slightly different and needs to be re-worked in the future
        rlvPoll.Rebind()
    End Sub

    Protected Sub imgSortUpViewDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortUpViewDate.Click
        rlvPoll_LoadWithSortExpression("viewDate", RadListViewSortOrder.Ascending)

        'We usually don't rebind the poll list here, but this control, is slightly different and needs to be re-worked in the future
        rlvPoll.Rebind()
    End Sub

    Protected Sub imgSortDownViewDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortDownViewDate.Click
        rlvPoll_LoadWithSortExpression("viewDate", RadListViewSortOrder.Descending)

        'We usually don't rebind the poll list here, but this control, is slightly different and needs to be re-worked in the future
        rlvPoll.Rebind()
    End Sub
End Class
