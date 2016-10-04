Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Data
Imports Telerik.Web.UI

Partial Class staff_StaffCategoryListingArchive_List
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 12

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolShowRssFeed As Boolean
    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            GetCategories()
        End If

    End Sub

    Private Sub GetCategories()

        'Check we need to handle group/user permissions
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True
            End If
        Next
        Dim dtCategory As DataTable = If(boolEnableGroupsAndUserAccess, CategoryDAL.GetCategoryList_WithCount_ByModuleTypeIDAndStatusAndAccess(ModuleTypeID, intSiteID, False, "staffID", "StartDate", "EndDate", MemberDAL.GetCurrentMemberGroupIDs(), intMemberID), CategoryDAL.GetCategoryList_WithCount_ByModuleTypeIDAndStatus(ModuleTypeID, intSiteID, False, "StartDate", "EndDate"))

        catRepeater.DataSource = dtCategory
        catRepeater.DataBind()
    End Sub

    Protected Sub catRepeater_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles catRepeater.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then

            If e.Item.DataItem("catcount") = 0 Then
                e.Item.Visible = False
            End If
        ElseIf e.Item.ItemType = ListItemType.Footer Then

            'get the number of staff members that are not in a category, if all active members are in a category, hide the uncategorized category
            Dim dtStaff_Uncategorized As DataTable = If(boolEnableGroupsAndUserAccess, StaffDAL.GetStaff_ByCategoryIDNullAndStatusAndAccess_FrontEnd(False, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), StaffDAL.GetStaff_ByCategoryIDNullAndStatus_FrontEnd(False, intSiteID))
            If dtStaff_Uncategorized.Rows.Count > 0 Then
                e.Item.FindControl("liUncategorized").Visible = True
            End If
        End If

    End Sub


End Class
