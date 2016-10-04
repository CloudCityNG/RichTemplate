Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Data

Partial Class Employment_EmploymentCategoryListingArchive
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 4

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            getCategories()
        End If

    End Sub

    Private Sub getCategories()

        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True
            End If
        Next

        Dim dtCategory As DataTable = If(boolEnableGroupsAndUserAccess, CategoryDAL.GetCategoryList_WithCount_ByModuleTypeIDAndStatusAndAccess(ModuleTypeID, intSiteID, False, "employmentID", "PublicationDate", "ExpirationDate", MemberDAL.GetCurrentMemberGroupIDs(), intMemberID), CategoryDAL.GetCategoryList_WithCount_ByModuleTypeIDAndStatus(ModuleTypeID, intSiteID, False, "PublicationDate", "ExpirationDate"))

        catRepeater.DataSource = dtCategory
        catRepeater.DataBind()
    End Sub



    Protected Sub catRepeater_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles catRepeater.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then

            If e.Item.DataItem("catcount") = 0 Then
                e.Item.Visible = False
            End If

        ElseIf e.Item.ItemType = ListItemType.Footer Then

            'get the number of employment entries that are not in a category, if all active employments are in a category, hide the uncategorized category
            Dim dtEmployment_Uncategorized As DataTable = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmployment_ByCategoryNullAndStatusAndAccess_FrontEnd(False, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByCategoryNullAndStatus_FrontEnd(False, intSiteID))
            If dtEmployment_Uncategorized.Rows.Count > 0 Then
                e.Item.FindControl("liUnCategorized").Visible = True
            End If

        End If

    End Sub

End Class

