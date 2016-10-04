Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_suggestion_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 13 ' Module Type: Suggestion

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If
        End If

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteLive, Resources.Suggestion_Admin.Suggestion_Default_GridDeleteButton_ConfirmationMessage)
        CommonWeb.SetupDeleteButton(btnDeleteArchive, Resources.Suggestion_Admin.Suggestion_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgSuggestion, "{4} {5} " & Resources.Suggestion_Admin.Suggestion_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Suggestion_Admin.Suggestion_Default_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadGrid(rgSuggestionArchive, "{4} {5} " & Resources.Suggestion_Admin.Suggestion_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Suggestion_Admin.Suggestion_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Suggestion_Admin.Suggestion_Default_Header

    End Sub

    Public Sub rgSuggestion_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgSuggestion.NeedDataSource

        Dim dtSuggestion As DataTable = SuggestionDAL.GetSuggestionList_ByStatusAndSiteID(True, SiteDAL.GetCurrentSiteID_Admin())
        rgSuggestion.DataSource = dtSuggestion
    End Sub

    Public Sub rgSuggestionArchive_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgSuggestionArchive.NeedDataSource

        Dim dtSuggestion As DataTable = SuggestionDAL.GetSuggestionList_ByStatusAndSiteID(False, SiteDAL.GetCurrentSiteID_Admin())
        rgSuggestionArchive.DataSource = dtSuggestion
    End Sub

    Private Sub rgSuggestion_ItemDataBound(ByVal source As Object, ByVal e As GridItemEventArgs) Handles rgSuggestion.ItemDataBound, rgSuggestionArchive.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            'setup Edit link
            Dim intSuggestionID As Integer = Convert.ToInt32(drItem("ID"))

            Dim aSuggestionEdit As HtmlAnchor = DirectCast(item.FindControl("aSuggestionEdit"), HtmlAnchor)
            aSuggestionEdit.HRef = "editAdd.aspx?ID=" + intSuggestionID.ToString
        End If
    End Sub

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgSuggestion.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("ID")
                SuggestionDAL.DeleteSuggestion_ByID(intRecordId)
            End If
        Next
        rgSuggestion.Rebind()

    End Sub

    Protected Sub btnDeleteArchive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgSuggestionArchive.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("ID")
                SuggestionDAL.DeleteSuggestion_ByID(intRecordId)
            End If
        Next
        rgSuggestionArchive.Rebind()

    End Sub

End Class
