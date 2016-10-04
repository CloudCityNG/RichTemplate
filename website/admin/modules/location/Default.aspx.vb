Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_Location_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 16 ' Module Type: Location

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If
        End If

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteLive, Resources.Location_Admin.Location_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgLocation, "{4} {5} " & Resources.Location_Admin.Location_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Location_Admin.Location_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Location_Admin.Location_Default_Header

    End Sub

    Public Sub rgLocation_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgLocation.NeedDataSource

        Dim dtLocation As DataTable = LocationDAL.GetLocationList()
        rgLocation.DataSource = dtLocation
    End Sub

    Private Sub rgLocation_ItemDataBound(ByVal source As Object, ByVal e As GridItemEventArgs) Handles rgLocation.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            'setup Edit link
            Dim intLocationID As Integer = Convert.ToInt32(drItem("ID"))

            Dim aLocationEdit As HtmlAnchor = DirectCast(item.FindControl("aLocationEdit"), HtmlAnchor)
            aLocationEdit.HRef = "editAdd.aspx?ID=" + intLocationID.ToString
        End If
    End Sub

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgLocation.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("ID")
                LocationDAL.DeleteLocation_ByID(intRecordId)
            End If
        Next
        rgLocation.Rebind()

    End Sub

End Class
