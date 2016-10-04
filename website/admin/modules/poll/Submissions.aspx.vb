Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_poll_Submissions
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 9 ' Module Type: Poll

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgPollSubmissions, "{4} {5} " & Resources.Poll_Admin.Poll_Submissions_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Poll_Admin.Poll_Submissions_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Poll_Admin.Poll_Submissions_Header
        ucHeader.PageHelpID = 7 'Help Item for Polls

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            'we must have at least a pollID, otherwise redirect to default page
            If Not Request.QueryString("pollID") Is Nothing Then
                Dim intPollID As Integer = Convert.ToInt32(Request.QueryString("pollID"))
                Dim dtPoll As DataTable = PollDAL.GetPoll_ByIDAndSiteID(intPollID, SiteDAL.GetCurrentSiteID_Admin())
                If dtPoll.Rows.Count > 0 Then
                    Dim strQuestion As String = dtPoll.Rows(0)("QuestionHtml").ToString()
                    litQuestion.Text = strQuestion
                Else
                    'Else this poll does not exist for this site
                    Response.Redirect("default.aspx")
                End If

                'Check if we are loading submissions for a particular poll answer, or for the entire poll
                Dim dtPollSubmissions As DataTable
                If Not Request.QueryString("pollAnswerID") Is Nothing Then

                    Dim intPollAnswerID As Integer = Convert.ToInt32(Request.QueryString("pollAnswerID"))
                    dtPollSubmissions = PollDAL.GetPollSubmissionRollup_ByPollAnswerID(intPollAnswerID)

                    'The body heading and back button is based on if we are showing submissions for the entire poll, or just the specific poll answer
                    aBackToPollsOrPollAnswer.HRef = "answers.aspx?pollID=" & intPollID
                    litBackToPollsOrPollAnswers.Text = Resources.Poll_Admin.Poll_Submissions_BackToPollAnswers
                    litBodyHeaderByPollOrPollAnswer.Text = Resources.Poll_Admin.Poll_Submissions_BodyHeadingByPollAnswer

                    'Get the answer and show it in our submission for pollAnswer literal
                    Dim dtPollAnswer As DataTable = PollDAL.GetPollAnswer_ByIDAndSiteID(intPollAnswerID, SiteDAL.GetCurrentSiteID_Admin())
                    If dtPollAnswer.Rows.Count > 0 Then
                        Dim strPollAnswer As String = dtPollAnswer.Rows(0)("Answer").ToString()
                        litAnswer.Text = strPollAnswer
                        divPollAnswer.Visible = True

                    End If

                Else
                    dtPollSubmissions = PollDAL.GetPollSubmissionRollup_ByPollIDAndSiteID(intPollID, SiteDAL.GetCurrentSiteID_Admin())
                    'The body heading and back button is based on if we are showing submissions for the entire poll, or just the specific poll answer
                    aBackToPollsOrPollAnswer.HRef = "default.aspx"
                    litBackToPollsOrPollAnswers.Text = Resources.Poll_Admin.Poll_Submissions_BackToPolls
                    litBodyHeaderByPollOrPollAnswer.Text = Resources.Poll_Admin.Poll_Submissions_BodyHeadingByPoll

                End If

                'Bind the datagrid using this datatable of submissions
                rgPollSubmissions.DataSource = dtPollSubmissions
                rgPollSubmissions.DataBind()
            Else
                'This poll does not exist for this site, so redirect user to the default page
                Response.Redirect("default.aspx")
            End If
        End If
    End Sub

End Class
