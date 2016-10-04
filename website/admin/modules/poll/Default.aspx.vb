Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_poll_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 9 ' Module Type: Poll

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If
        End If

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteLive, Resources.Poll_Admin.Poll_Default_GridDeleteButton_ConfirmationMessage)
        CommonWeb.SetupDeleteButton(btnDeleteArchive, Resources.Poll_Admin.Poll_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgPoll, "{4} {5} " & Resources.Poll_Admin.Poll_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Poll_Admin.Poll_Default_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadGrid(rgPollArchive, "{4} {5} " & Resources.Poll_Admin.Poll_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Poll_Admin.Poll_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Poll_Admin.Poll_Default_Header
        ucHeader.PageHelpID = 7 'Help Item for Polls

        'Check we need to show comments
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" AndAlso AdminUserDAL.CheckAdminUserModuleAccess(2, SiteDAL.GetCurrentSiteID_Admin()) Then
                'show the commentCount column
                Dim gcCommentCount As GridColumn = rgPoll.Columns.FindByUniqueName("comments")
                gcCommentCount.Visible = True

                Dim gcCommentCountArchive As GridColumn = rgPollArchive.Columns.FindByUniqueName("comments")
                gcCommentCountArchive.Visible = True

            End If
        Next

    End Sub

    Public Sub rgPoll_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgPoll.NeedDataSource

        Dim dtPoll As DataTable = PollDAL.GetPoll_ByStatusAndSiteID(True, SiteDAL.GetCurrentSiteID_Admin())
        rgPoll.DataSource = dtPoll
    End Sub

    Public Sub rgPollArchive_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgPollArchive.NeedDataSource

        Dim dtPoll As DataTable = PollDAL.GetPoll_ByStatusAndSiteID(False, SiteDAL.GetCurrentSiteID_Admin())
        rgPollArchive.DataSource = dtPoll
    End Sub

    Private Sub rgPolls_ItemDataBound(ByVal source As Object, ByVal e As GridItemEventArgs) Handles rgPoll.ItemDataBound, rgPollArchive.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            Dim intPollID As Integer = Convert.ToInt32(drItem("ID"))

            'Update number of answers text
            Dim intAnswerCount As String = drItem("answerCount")
            Dim hLinkAnswers As HyperLink = DirectCast(item("answers").Controls(0), HyperLink)
            hLinkAnswers.Text = Resources.Poll_Admin.Poll_Default_GridAnswers & " (" & intAnswerCount & ")"

            'Update submission text
            Dim intSubmissionCount As String = drItem("submissionCount")
            Dim hLinkSubmissions As HyperLink = DirectCast(item("submissions").Controls(0), HyperLink)
            hLinkSubmissions.Text = Resources.Poll_Admin.Poll_Default_GridSubmissions & " (" & intSubmissionCount & ")"

            'Update comment text
            Dim intCommentCountApproved As Integer = Convert.ToInt32(drItem("commentCountApproved"))
            Dim intCommentCountPending As Integer = Convert.ToInt32(drItem("commentCountPending"))

            Dim sbCommentText As New StringBuilder()
            sbCommentText.Append(If(intCommentCountApproved > 0, "<span class='commentTextApproved'>" & intCommentCountApproved.ToString() & "</span>", ""))
            If (intCommentCountApproved > 0) AndAlso (intCommentCountPending > 0) Then
                sbCommentText.Append(" / ")
            End If
            sbCommentText.Append(If(intCommentCountPending > 0, "<span class='commentTextPending'>" & intCommentCountPending.ToString() & "</span>", ""))

            Dim strCommentText As String = sbCommentText.ToString()
            If strCommentText.Length > 0 Then
                Dim hLinkComments As HyperLink = DirectCast(item("comments").Controls(0), HyperLink)
                hLinkComments.NavigateUrl = "/admin/modules/Comments/defaultModule.aspx?mtID=" & ModuleTypeID & "&recordID=" & intPollID
                hLinkComments.Text = Resources.PressRelease_Admin.PressRelease_Default_GridComments & " (" & strCommentText & ")"
            End If

            'Before we show this linkbutton to view comments, we check if this poll record, was created for a different site, in which case its a read-only poll record, and you can not view comments from this site
            Dim intSiteID As Integer = Convert.ToInt32(drItem("SiteID"))
            If Not intSiteID = SiteDAL.GetCurrentSiteID_Admin() Then
                Dim hLinkComments As HyperLink = DirectCast(item("comments").Controls(0), HyperLink)
                hLinkComments.Visible = False
            End If

            'setup Edit link
            Dim aPollEdit As HtmlAnchor = DirectCast(item.FindControl("aPollEdit"), HtmlAnchor)
            aPollEdit.HRef = "editAdd.aspx?ID=" + intPollID.ToString
        End If
    End Sub

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgPoll.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("ID")

                PollDAL.DeletePoll_ByID(intRecordId)
            End If
        Next
        rgPoll.Rebind()

    End Sub


    Protected Sub btnDeleteArchive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgPollArchive.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("ID")

                PollDAL.DeletePoll_ByID(intRecordId)
            End If
        Next
        rgPollArchive.Rebind()

    End Sub

End Class
