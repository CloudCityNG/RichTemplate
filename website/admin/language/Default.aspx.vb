Imports Telerik.Web.UI
Imports System.Data

Partial Class admin_language_Default
    Inherits RichTemplateLanguagePage


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDelete, Resources.Language_Admin.Language_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgLanguage, "{4} {5} " & Resources.Language_Admin.Language_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Language_Admin.Language_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Language_Admin.Language_Default_Header

        'Check the logged in user can view this page
        Dim intAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intAdminUserAccess < 3 Then 'This AdminUser is a Master Administrator, so allow them to Add New Languages
            Response.Redirect("~/richadmin/")
        End If

        If Not Page.IsPostBack Then

        End If

    End Sub

    Protected Sub rgLanguage_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgLanguage.NeedDataSource
        'If the current AdminUser is a Master Administrator, list ALL Languages
        Dim dtLanguage As DataTable = LanguageDAL.GetLanguageList()
        rgLanguage.DataSource = dtLanguage
    End Sub

    Private Sub rgLanguage_ItemDataBound(ByVal source As Object, ByVal e As GridItemEventArgs) Handles rgLanguage.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            'setup Edit link
            Dim intLanguageID As Integer = Convert.ToInt32(drItem("ID"))

            Dim aLanguageEdit As HtmlAnchor = DirectCast(item.FindControl("aLanguageEdit"), HtmlAnchor)
            aLanguageEdit.HRef = "editAdd.aspx?ID=" + intLanguageID.ToString

            Dim aLanguageEditLanguageFiles As HtmlAnchor = DirectCast(item.FindControl("aLanguageEditLanguageFiles"), HtmlAnchor)
            aLanguageEditLanguageFiles.HRef = "editLanguageFiles.aspx?ID=" + intLanguageID.ToString

        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        For Each grdItem As GridDataItem In rgLanguage.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("ID")
                LanguageDAL.DeleteLanguage_ByLanguageID(intRecordID)
            End If
        Next
        rgLanguage.Rebind()

    End Sub

End Class

