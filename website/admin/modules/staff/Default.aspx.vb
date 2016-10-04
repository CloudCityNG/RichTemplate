Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_staff_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 12 ' Module Type: Staff Member

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteLive, Resources.Staff_Admin.Staff_Default_GridDeleteButton_ConfirmationMessage)
        CommonWeb.SetupDeleteButton(btnDeleteArchive, Resources.Staff_Admin.Staff_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgStaff, "{4} {5} " & Resources.Staff_Admin.Staff_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Staff_Admin.Staff_Default_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadGrid(rgStaffArchive, "{4} {5} " & Resources.Staff_Admin.Staff_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Staff_Admin.Staff_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Staff_Admin.Staff_Default_Header

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            'Set the last tab as active
            Dim selectedValue As Integer = 0
            If Not Session("selectedEditTabRoot") Is DBNull.Value And Not Session("selectedEditTabRoot") = "" Then
                selectedValue = Session("selectedEditTabRoot")
            End If
            rtsStaff.SelectedIndex = selectedValue
            rmpStaff.SelectedIndex = selectedValue
        End If

    End Sub

    Public Sub rgStaff_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgStaff.NeedDataSource

        Dim dtStaff As DataTable = StaffDAL.GetStaff_ByStatusAndSiteID(True, SiteDAL.GetCurrentSiteID_Admin())
        rgStaff.DataSource = dtStaff
    End Sub

    Public Sub rgStaffArchive_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgStaffArchive.NeedDataSource

        Dim dtStaff As DataTable = StaffDAL.GetStaff_ByStatusAndSiteID(False, SiteDAL.GetCurrentSiteID_Admin())
        rgStaffArchive.DataSource = dtStaff
    End Sub

    Private Sub rgStaff_ItemDataBound(ByVal source As Object, ByVal e As GridItemEventArgs) Handles rgStaff.ItemDataBound, rgStaffArchive.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            'setup Edit link
            Dim intStaffID As Integer = Convert.ToInt32(drItem("staffID"))

            Dim aStaffEdit As HtmlAnchor = DirectCast(item.FindControl("aStaffEdit"), HtmlAnchor)
            aStaffEdit.HRef = "editAdd.aspx?ID=" + intStaffID.ToString
        End If
    End Sub

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgStaff.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("staffID")

                StaffDAL.DeleteStaff_ByStaffID(intRecordId)
                StaffDAL.DeleteStaffArchive_ByStaffID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        rgStaff.Rebind()

    End Sub

    Protected Sub btnDeleteArchive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgStaffArchive.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("staffID")

                StaffDAL.DeleteStaff_ByStaffID(intRecordId)
                StaffDAL.DeleteStaffArchive_ByStaffID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        rgStaffArchive.Rebind()

    End Sub

End Class
