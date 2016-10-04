Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_poll_Answers
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 9 ' Module Type: Poll

    Dim intPollSubmissionTotalCount As Integer = 0
    Dim boolAllowDragDrop As Boolean = True
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteLive, Resources.Poll_Admin.Poll_Answers_GridDeleteButton_ConfirmationMessage)


        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgPollAnswers, "{4} {5} " & Resources.Poll_Admin.Poll_Answers_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Poll_Admin.Poll_Answers_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Poll_Admin.Poll_Answers_Header
        ucHeader.PageHelpID = 7 'Help Item for Polls



        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            If Not Request.QueryString("pollID") Is Nothing Then
                Dim intPollID As Integer = Request.QueryString("pollID")
                Dim dtPoll As DataTable = PollDAL.GetPoll_ByIDAndSiteID(intPollID, SiteDAL.GetCurrentSiteID_Admin())

                'Get the total number of submissions
                If dtPoll.Rows.Count > 0 Then
                    Dim strQuestion As String = dtPoll.Rows(0)("QuestionHtml").ToString()
                    litQuestion.Text = strQuestion

                    Dim boolAnswersRandomized As Boolean = Convert.ToBoolean(dtPoll.Rows(0)("AnswersRandomized").ToString())
                    spanAnswersRandomized.Visible = boolAnswersRandomized

                    'Get the number of submissions for this question
                    Dim dtPollSubmissions As DataTable = PollDAL.GetPollSubmissionRollup_ByPollIDAndSiteID(intPollID, SiteDAL.GetCurrentSiteID_Admin())
                    intPollSubmissionTotalCount = dtPollSubmissions.Rows.Count

                    'Load/Bind our list of poll Answers
                    rgPollAnswers_BindRadGrid()

                    Dim intSiteID As Integer = Convert.ToInt32(dtPoll.Rows(0)("SiteID"))
                    If Not intSiteID = SiteDAL.GetCurrentSiteID_Admin() Then
                        MakePollAnswersReadOnly(intSiteID)
                    End If

                Else
                    Response.Redirect("default.aspx")
                End If
            Else
                Response.Redirect("default.aspx")
            End If

        End If

        'Finally setup our Drag Drop Script
        RegisterDragAndDropScript()
    End Sub

    Private Sub MakePollAnswersReadOnly(ByVal SiteID As Integer)

        'Hide the Drag-Drop Column
        Dim gcDragAndDrop As GridColumn = rgPollAnswers.Columns.FindByUniqueName("DragAndDrop")
        gcDragAndDrop.Visible = False

        'Prevent AdminUser from updating this record
        divDragDropMessage.Visible = False

    End Sub

    Protected Sub rgPollAnswers_BindRadGrid()
        'Load Poll Answers for this poll, and set its primary key
        Dim intPollID As Integer = Request.QueryString("pollID")
        Dim dtPollAnswers As DataTable = PollDAL.GetPollAnswerList_ByPollID(intPollID)
        dtPollAnswers.PrimaryKey = New DataColumn() {dtPollAnswers.Columns("ID")}

        rgPollAnswers.DataSource = dtPollAnswers
        rgPollAnswers.DataBind()
    End Sub

    Private Sub rgPollAnswers_ItemDataBound(ByVal source As Object, ByVal e As GridItemEventArgs) Handles rgPollAnswers.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            Dim intPollAnswerID As Integer = Convert.ToInt32(drItem("ID"))

            'Update submission text
            Dim intSubmissionCount As String = drItem("submissionCount")
            Dim hLinkSubmissions As HyperLink = DirectCast(item("submissions").Controls(0), HyperLink)
            If intPollSubmissionTotalCount = 0 Then
                hLinkSubmissions.Text = Resources.Poll_Admin.Poll_Default_GridSubmissions & " (0)"
            Else
                hLinkSubmissions.Text = Resources.Poll_Admin.Poll_Default_GridSubmissions & " (" & intSubmissionCount & " " & Resources.Poll_Admin.Poll_Answers_GridAnswerSubmissionSeperator & " " & intPollSubmissionTotalCount & ")"
            End If

            'setup Edit link
            Dim aPollAnswerEdit As HtmlAnchor = DirectCast(item.FindControl("aPollAnswerEdit"), HtmlAnchor)
            aPollAnswerEdit.HRef = "editAddAnswer.aspx?ID=" + intPollAnswerID.ToString
        End If
    End Sub

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgPollAnswers.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("ID")

                PollDAL.DeletePollAnswer_ByID(intRecordId)
            End If
        Next
        rgPollAnswers.Rebind()

    End Sub

#Region "Poll Answers - Drag and Drop"
    Private Sub RegisterDragAndDropScript()
        'Register our javascript for inside our update panel
        Dim strHighlightScript As String = ""
        strHighlightScript = strHighlightScript & _
        "$( function()" & _
        "{ " & _
        "  $('.tblPollAnswers tr').hover(" & _
        "   function() " & _
        "   {" & _
        "    $(this).addClass('highlight');" & _
        "   }," & _
        "   function()" & _
        "  {" & _
        "   $(this).removeClass('highlight');" & _
        "  }" & _
        " )" & _
        " }" & _
        ")"
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "dynamic scripts", strHighlightScript, True)

    End Sub

    Protected Sub rgPollAnswers_RowDrop(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridDragDropEventArgs) Handles rgPollAnswers.RowDrop

        'Before we perform any drag drop operation, we check if this poll is part of this site or not. If not we do not allow drag-drop to re-arrange answers as its READ-ONLY
        Dim intPollID As Integer = Request.QueryString("pollID")
        Dim dtPoll As DataTable = PollDAL.GetPoll_ByIDAndSiteID(intPollID, SiteDAL.GetCurrentSiteID_Admin())
        If dtPoll.Rows.Count > 0 Then
            Dim intSiteID As Integer = Convert.ToInt32(dtPoll.Rows(0)("SiteID"))

            If intSiteID = SiteDAL.GetCurrentSiteID_Admin() Then

                If String.IsNullOrEmpty(e.HtmlElement) Then
                    If e.DraggedItems.Count > 0 And e.DraggedItems(0).OwnerGridID = rgPollAnswers.ClientID Then
                        If Not e.DestDataItem Is Nothing And e.DestDataItem.OwnerGridID = rgPollAnswers.ClientID Then
                            'Then its a valid drop so reorder items in RadGrid

                            'Get the Poll Answer of our Dragged Row and Destination Row
                            Dim intPollAnswerID_DraggedRow = Convert.ToInt32(e.DraggedItems(0).GetDataKeyValue("ID").ToString())
                            Dim intPollAnswerID_DestRow = Convert.ToInt32(e.DestDataItem.GetDataKeyValue("ID").ToString())

                            'Get the Poll Answers from our dataset
                            Dim dtPollAnswers As DataTable = PollDAL.GetPollAnswerList_ByPollID(intPollID)

                            dtPollAnswers.PrimaryKey = New DataColumn() {dtPollAnswers.Columns("ID")}

                            Dim drDraggedItem As DataRow = dtPollAnswers.Rows.Find(intPollAnswerID_DraggedRow)
                            Dim drDestItem As DataRow = dtPollAnswers.Rows.Find(intPollAnswerID_DestRow)

                            'Get the Current Rank and the New Rank, these may need updating by one depending on how your selecting dropped reference row
                            Dim drDest_SortOrder As Integer = drDestItem("SortOrder")
                            Dim intDestItemIndex As Integer = e.DestDataItem.ItemIndex
                            Dim intDraggedItemIndex As Integer = e.DraggedItems(0).ItemIndex

                            If e.DropPosition = GridItemDropPosition.Below And e.DestDataItem.ItemIndex < intDraggedItemIndex Then
                                drDest_SortOrder += 1
                            ElseIf e.DropPosition = GridItemDropPosition.Above And e.DestDataItem.ItemIndex > intDraggedItemIndex Then
                                drDest_SortOrder -= 1
                            End If

                            'Perform the acutal update of our Poll Answers
                            PollDAL.UpdatePollAnswer_SortOrder_ByID(intPollAnswerID_DraggedRow, drDest_SortOrder)

                            'Load Poll Answer Information
                            rgPollAnswers_BindRadGrid()

                        End If
                    End If
                End If
            End If
        End If

    End Sub
#End Region
End Class
