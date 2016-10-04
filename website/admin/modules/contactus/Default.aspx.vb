Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_contactus_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 15 ' Module Type: Contact US

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If
        End If

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteLive, Resources.ContactUs_Admin.ContactUs_Default_GridDeleteButton_ConfirmationMessage)
        CommonWeb.SetupDeleteButton(btnDeleteArchive, Resources.ContactUs_Admin.ContactUs_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgContactUs, "{4} {5} " & Resources.ContactUs_Admin.ContactUs_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.ContactUs_Admin.ContactUs_Default_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadGrid(rgContactUsArchive, "{4} {5} " & Resources.ContactUs_Admin.ContactUs_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.ContactUs_Admin.ContactUs_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.ContactUs_Admin.ContactUs_Default_Header

    End Sub

    Public Sub rgContactUs_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgContactUs.NeedDataSource

        Dim dtContactUs As DataTable = ContactUsDAL.GetContactUsList_ByStatusAndSiteID(True, SiteDAL.GetCurrentSiteID_Admin())
        rgContactUs.DataSource = dtContactUs
    End Sub

    Public Sub rgContactUsArchive_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgContactUsArchive.NeedDataSource

        Dim dtContactUs As DataTable = ContactUsDAL.GetContactUsList_ByStatusAndSiteID(False, SiteDAL.GetCurrentSiteID_Admin())
        rgContactUsArchive.DataSource = dtContactUs
    End Sub

    Private Sub rgContactUs_ItemDataBound(ByVal source As Object, ByVal e As GridItemEventArgs) Handles rgContactUs.ItemDataBound, rgContactUsArchive.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            'setup Edit link
            Dim intContactUsID As Integer = Convert.ToInt32(drItem("ID"))

            Dim aContactUsEdit As HtmlAnchor = DirectCast(item.FindControl("aContactUsEdit"), HtmlAnchor)
            aContactUsEdit.HRef = "editAdd.aspx?ID=" + intContactUsID.ToString
        End If
    End Sub

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgContactUs.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("ID")
                ContactUsDAL.DeleteContactUs_ByID(intRecordId)
            End If
        Next
        rgContactUs.Rebind()

    End Sub

    Protected Sub btnDeleteArchive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgContactUsArchive.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("ID")
                ContactUsDAL.DeleteContactUs_ByID(intRecordId)
            End If
        Next
        rgContactUsArchive.Rebind()

    End Sub

End Class
