Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_event_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 5 ' Module Type: Event

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If
        End If

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteLive, Resources.Event_Admin.Event_Default_GridDeleteButton_ConfirmationMessage)
        CommonWeb.SetupDeleteButton(btnDeleteArchive, Resources.Event_Admin.Event_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgEvents, "{4} {5} " & Resources.Event_Admin.Event_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Event_Admin.Event_Default_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadGrid(rgEventsArchive, "{4} {5} " & Resources.Event_Admin.Event_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Event_Admin.Event_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Event_Admin.Event_Default_Header

        'Check we need to show comments and online registration columns
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" AndAlso AdminUserDAL.CheckAdminUserModuleAccess(2, SiteDAL.GetCurrentSiteID_Admin()) Then
                'show the commentCount column
                Dim gcCommentCount As GridColumn = rgEvents.Columns.FindByUniqueName("comments")
                gcCommentCount.Visible = True

                Dim gcCommentCountArchive As GridColumn = rgEventsArchive.Columns.FindByUniqueName("comments")
                gcCommentCountArchive.Visible = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_registrations" Then
                'show the registration column
                Dim gcApplicantsCount As GridColumn = rgEvents.Columns.FindByUniqueName("applicants")
                gcApplicantsCount.Visible = True

                Dim gcApplicantsCountArchive As GridColumn = rgEventsArchive.Columns.FindByUniqueName("applicants")
                gcApplicantsCountArchive.Visible = True

            End If
        Next

    End Sub

    Public Sub rgEvents_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEvents.NeedDataSource

        Dim dtEvent As DataTable = EventDAL.GetEvent_ByStatusAndSiteID(True, SiteDAL.GetCurrentSiteID_Admin())
        rgEvents.DataSource = dtEvent
    End Sub

    Public Sub rgEventsArchive_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEventsArchive.NeedDataSource

        Dim dtEvent As DataTable = EventDAL.GetEvent_ByStatusAndSiteID(False, SiteDAL.GetCurrentSiteID_Admin())
        rgEventsArchive.DataSource = dtEvent
    End Sub

    Private Sub rgEvents_ItemDataBound(ByVal source As Object, ByVal e As GridItemEventArgs) Handles rgEvents.ItemDataBound, rgEventsArchive.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            Dim intEventID As Integer = Convert.ToInt32(drItem("eventID"))

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
                hLinkComments.NavigateUrl = "/admin/modules/Comments/defaultModule.aspx?mtID=" & ModuleTypeID & "&recordID=" & intEventID
                hLinkComments.Text = Resources.Event_Admin.Event_Default_GridComments & " (" & strCommentText & ")"
            End If

            'Before we show this linkbutton to view comments, we check if this event record, was created for a different site, in which case its a read-only event record, and you can not view comments from this site
            Dim intSiteID As Integer = Convert.ToInt32(drItem("SiteID"))
            If Not intSiteID = SiteDAL.GetCurrentSiteID_Admin() Then
                Dim hLinkComments As HyperLink = DirectCast(item("comments").Controls(0), HyperLink)
                hLinkComments.Visible = False
            End If

            'update registration text
            Dim boolOnlineSignup As Boolean = Convert.ToBoolean(drItem("OnlineSignup"))
            If boolOnlineSignup Then
                Dim intAppCount As String = Convert.ToInt32(drItem("appCount"))
                Dim hLinkApplicantCount As HyperLink = DirectCast(item("applicants").Controls(0), HyperLink)
                hLinkApplicantCount.Text = Resources.Event_Admin.Event_Default_GridRegistrations & " (" & intAppCount & ")"
            End If

            'setup Edit link
            Dim aEventEdit As HtmlAnchor = DirectCast(item.FindControl("aEventEdit"), HtmlAnchor)
            aEventEdit.HRef = "editAdd.aspx?ID=" + intEventID.ToString
        End If
    End Sub

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgEvents.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("eventID")

                EventDAL.DeleteEvent_ByEventID(intRecordId)
                EventDAL.DeleteEventArchive_ByEventID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        rgEvents.Rebind()

    End Sub


    Protected Sub btnDeleteArchive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgEventsArchive.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("eventID")

                EventDAL.DeleteEvent_ByEventID(intRecordId)
                EventDAL.DeleteEventArchive_ByEventID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        rgEventsArchive.Rebind()

    End Sub

End Class
