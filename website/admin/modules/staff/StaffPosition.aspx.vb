Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports Telerik.Web.UI
Partial Class admin_modules_staff_StaffPosition
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 12 ' Module Type: Staff Member
    Dim intSiteCount As Integer = 0
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

        End If

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDelete, Resources.Staff_Admin.Staff_StaffPosition_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgStaffPosition, Resources.Staff_Admin.Staff_StaffPosition_AddNewEntry, "{4} {5} " & Resources.Staff_Admin.Staff_StaffPosition_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Staff_Admin.Staff_StaffPosition_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Staff_Admin.Staff_StaffPosition_AddEdit_Header

        'Set DataSource parameters
        dsStaffPositionList.SelectParameters("SiteID").DefaultValue = SiteDAL.GetCurrentSiteID_Admin()
        dsStaffPositionList.InsertParameters("SiteID").DefaultValue = SiteDAL.GetCurrentSiteID_Admin()

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim rowIndex As Integer = 0
        Dim strRecordIds As String = [String].Empty

        For Each grdItem As GridDataItem In rgStaffPosition.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                rowIndex = grdItem.RowIndex
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("StaffPositionID")
                StaffDAL.DeleteStaffPosition_ByStaffPositionID(intRecordId)
            End If
        Next
        rgStaffPosition.Rebind()
    End Sub


End Class
