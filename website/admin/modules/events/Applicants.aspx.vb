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
        CommonWeb.SetupDeleteButton(btnDeleteRegistration, Resources.Event_Admin.Event_Applicants_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgEventRegistrations, "{4} {5} " & Resources.Event_Admin.Event_Applicants_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Event_Admin.Event_Applicants_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Event_Admin.Event_Applicants_Header

    End Sub

    Protected Sub rgEventRegistrations_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEventRegistrations.NeedDataSource
        'Load event applicants
        If Not Request.QueryString("eventID") Is Nothing Then
            Dim intEventID As Integer = Convert.ToInt32(Request.QueryString("eventID"))
            rgEventRegistrations.DataSource = EventDAL.GetEventSubmissions_ByEventIDAndSiteID(intEventID, SiteDAL.GetCurrentSiteID_Admin())
        End If

    End Sub

    Protected Sub btnDeleteRegistration_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgEventRegistrations.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("subID")
                EventDAL.DeleteEventSubmission_BySubID(intRecordId)
            End If
        Next
        rgEventRegistrations.Rebind()
        
    End Sub

End Class
