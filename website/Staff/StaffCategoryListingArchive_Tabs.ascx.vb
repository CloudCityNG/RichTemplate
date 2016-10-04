Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Data

Imports Telerik.Web.UI

Partial Class staff_StaffCategoryListingArchive_Tabs
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 12
    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            If IsPostBack = False Then
                GetCategories()
            End If
        End If

    End Sub
    Private Sub GetCategories()
        rtsStaffArchive.Tabs.Clear()
        Dim selectedCategory As Integer = -1 '-1 we show all tab, 0 we show uncategorized tab
        If Request.QueryString("catID") <> "" Then
            selectedCategory = Convert.ToInt32(Request.QueryString("catID"))
        End If

        'Check we need to handle group/user permissions
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True
            End If
        Next

        Dim dtCategory As DataTable = If(boolEnableGroupsAndUserAccess, CategoryDAL.GetCategoryList_WithCount_ByModuleTypeIDAndStatusAndAccess(ModuleTypeID, intSiteID, False, "staffID", "StartDate", "EndDate", MemberDAL.GetCurrentMemberGroupIDs(), intMemberID), CategoryDAL.GetCategoryList_WithCount_ByModuleTypeIDAndStatus(ModuleTypeID, intSiteID, False, "StartDate", "EndDate"))

        'Add the 'Show all' tab
        Dim tabShowAll As New RadTab(Resources.Staff_FrontEnd.Staff_StaffCategoryListingArchive_Tabs_ShowAll, "")
        If selectedCategory = -1 Then
            tabShowAll.Selected = True
            tabShowAll.NavigateUrl = "default.aspx?archive=1"
        End If
        rtsStaffArchive.Tabs.Add(tabShowAll)

        For Each drCategory As DataRow In dtCategory.Rows
            Dim categoryName As String = drCategory("CategoryName")
            Dim categoryID As Integer = Convert.ToInt32(drCategory("CategoryID"))
            Dim categoryCount As String = drCategory("catCount").ToString()
            Dim tabCategory As New RadTab(categoryName & " (" & categoryCount & ")", categoryID)
            tabCategory.NavigateUrl = "?archive=1&catID=" & categoryID.ToString()

            If selectedCategory = categoryID Then
                tabCategory.Selected = True
            End If

            rtsStaffArchive.Tabs.Add(tabCategory)

        Next

    End Sub

End Class
