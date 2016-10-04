Imports Telerik.Web.UI
Imports System.Data

Partial Class admin_site_Default
    Inherits RichTemplateLanguagePage


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgSite, "{4} {5} " & Resources.Site_Admin.Site_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Site_Admin.Site_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Site_Admin.Site_Default_Header

        'Check the logged in user can view this page
        Dim intAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intAdminUserAccess > 2 Then 'This AdminUser is a Master Administrator, so allow them to Add New Sites
            'But first check if the number of allowed created sites has no exceeded the threshold
            Dim intSiteCount As Integer = SiteDAL.GetSiteList().Rows.Count
            Dim intSiteMaxCount As Integer = Convert.ToInt32(ConfigurationManager.AppSettings("SiteMaxCount"))
            rgSite.MasterTableView.CommandItemDisplay = If(intSiteCount < intSiteMaxCount, GridCommandItemDisplay.Top, GridCommandItemDisplay.None)

        ElseIf intAdminUserAccess > 1 Then
            'do something perhaps
        Else
            Response.Redirect("~/richadmin/")
        End If

        If Not Page.IsPostBack Then

        End If

    End Sub

    Protected Sub rgSite_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgSite.NeedDataSource
        'If the current AdminUser is a Master Administrator, list ALL Sites, else list only the sites the user has access to
        Dim dtSite As DataTable
        If AdminUserDAL.GetCurrentAdminUserAccessLevel() > 2 Then
            dtSite = SiteDAL.GetSiteList()
        Else
            'Check if this adminUser has access to All Sites, if so get all sites, otherwise just get the sites they have access to
            Dim dtSiteList_AllSites As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID_Active(0, AdminUserDAL.GetCurrentAdminUserID)
            If dtSiteList_AllSites.Rows.Count > 0 Then
                dtSite = SiteDAL.GetSiteList() ' So retrieve ALL Sites
            Else
                dtSite = SiteDAL.GetSiteAccessList_ForAdminUser_ByAdminUserID_Active(AdminUserDAL.GetCurrentAdminUserID) 'Only get Sites user has access to
            End If
        End If

        rgSite.DataSource = dtSite
    End Sub

    Private Sub rgSite_ItemDataBound(ByVal source As Object, ByVal e As GridItemEventArgs) Handles rgSite.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            'setup Edit link
            Dim intSiteID As Integer = Convert.ToInt32(drItem("ID"))

            Dim aSiteEdit As HtmlAnchor = DirectCast(item.FindControl("aSiteEdit"), HtmlAnchor)
            aSiteEdit.HRef = "editAdd.aspx?ID=" + intSiteID.ToString

        End If
    End Sub
End Class

