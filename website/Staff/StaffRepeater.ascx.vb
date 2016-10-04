Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI


Partial Class staff_StaffRepeater
    Inherits System.Web.UI.UserControl

    Public ModuleTypeID As Integer = 12

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolStatus As Boolean = True
    Dim boolShowAddThis As Boolean = False
    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            If IsPostBack = False Then

                'Finally load the staff member list
                rlvStaff_LoadWithSortExpression("LastName", "FirstName", RadListViewSortOrder.Ascending)

            End If
        End If

    End Sub

    Protected Sub rlvStaff_LoadWithSortExpression(ByVal strSortString As String, ByVal rlvSortOrder As RadListViewSortOrder)
        'Clear the current sort expressions
        rlvStaff.SortExpressions.Clear()

        If Not strSortString Is String.Empty Then

            'Set up our RadListView Grid Sort Expression
            Dim rlvSortExpression As New RadListViewSortExpression()
            rlvSortExpression.SortOrder = rlvSortOrder
            rlvSortExpression.FieldName = strSortString

            rlvStaff.SortExpressions.AddSortExpression(rlvSortExpression)

        End If

        'Bind our list
        rlvStaff.Rebind()

    End Sub

    Protected Sub rlvStaff_LoadWithSortExpression(ByVal strSortString As String, ByVal strSortStringSecondary As String, ByVal rlvSortOrder As RadListViewSortOrder)
        'Clear the current sort expressions
        rlvStaff.SortExpressions.Clear()

        'Set up our RadListView Grid Sort Expression
        If Not strSortString Is String.Empty Then

            rlvStaff.AllowNaturalSort = True
            rlvStaff.SortExpressions.AllowNaturalSort = True

            rlvStaff.AllowMultiItemSelection = True
            rlvStaff.SortExpressions.AllowMultiFieldSorting = True

            'Set up our RadListView Grid Sort Expression
            Dim rlvSortExpression As New RadListViewSortExpression()
            rlvSortExpression.SortOrder = rlvSortOrder
            rlvSortExpression.FieldName = strSortString

            Dim rlvSortExpressionSecondary As New RadListViewSortExpression()
            rlvSortExpressionSecondary.SortOrder = rlvSortOrder
            rlvSortExpressionSecondary.FieldName = strSortStringSecondary

            rlvStaff.SortExpressions.AddSortExpression(rlvSortExpression)
            rlvStaff.SortExpressions.AddSortExpression(rlvSortExpressionSecondary)
        End If

        'Bind our list
        rlvStaff.Rebind()
    End Sub

    Protected Sub rlvStaff_NeedDataSource(ByVal sender As Object, ByVal e As RadListViewNeedDataSourceEventArgs) Handles rlvStaff.NeedDataSource
        If Request.QueryString("archive") = "1" Then
            boolStatus = False
        End If

        'Check we need to handle group/user permissions
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "show_add_this" Then
                boolShowAddThis = True

            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True
            End If
        Next

        'Define default category ID And Sort Order
        Dim dtStaff As New DataTable
        If Request.QueryString("catID") <> "" Then
            Dim intCatID As Integer = Convert.ToInt32(Request.QueryString("catID"))
            If intCatID = 0 Then
                'Load staff members that are un-categorized
                dtStaff = If(boolEnableGroupsAndUserAccess, StaffDAL.GetStaff_ByCategoryIDNullAndStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), StaffDAL.GetStaff_ByCategoryIDNullAndStatus_FrontEnd(boolStatus, intSiteID))
            Else
                'Load staff members that are part of a specific category
                dtStaff = If(boolEnableGroupsAndUserAccess, StaffDAL.GetStaff_ByCategoryIDAndStatusAndAccess_FrontEnd(intCatID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), StaffDAL.GetStaff_ByCategoryIDAndStatus_FrontEnd(intCatID, boolStatus, intSiteID))
            End If
        Else
            'We want all Staff members from all categories
            dtStaff = If(boolEnableGroupsAndUserAccess, StaffDAL.GetStaff_ByStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), StaffDAL.GetStaff_ByStatus_FrontEnd(boolStatus, intSiteID))
        End If

        rlvStaff.DataSource = dtStaff

        'Note if we have no rows, we clear sort expressions
        If dtStaff.Rows.Count = 0 Then
            rlvStaff.SortExpressions.Clear()
        End If
    End Sub

    Protected Sub rlvStaff_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListViewItemEventArgs) Handles rlvStaff.ItemDataBound
        If TypeOf e.Item Is RadListViewDataItem Then
            Dim item As RadListViewDataItem = TryCast(e.Item, RadListViewDataItem)

            'Now we have the staff, so we load this staff
            Dim drvStaff As DataRowView = item.DataItem
            Dim drStaff As DataRow = drvStaff.Row

            Dim intStaffID As Integer = Convert.ToInt32(drStaff("staffID"))

            Dim litSalutationFirstAndLastName As Literal = item.FindControl("litSalutationFirstAndLastName")

            Dim strSalutation_LangaugeSpecific As String = String.Empty
            If Not drStaff("Salutation_LanguageProperty") Is DBNull.Value Then
                Dim strSalutation_LanguageProperty As String = drStaff("Salutation_LanguageProperty")
                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
            End If

            Dim strSalutationFirstAndLastName As String = CommonWeb.FormatName(strSalutation_LangaugeSpecific, drStaff("firstName").ToString(), drStaff("lastName").ToString())

            litSalutationFirstAndLastName.Text = strSalutationFirstAndLastName

            If Not drStaff("StaffPosition") Is DBNull.Value Then
                If Not drStaff("StaffPosition") = "" Then
                    Dim spanStaffPosition As HtmlGenericControl = item.FindControl("spanStaffPosition")
                    Dim litStaffPosition As Literal = item.FindControl("litStaffPosition")
                    litStaffPosition.Text = drStaff("StaffPosition")
                    spanStaffPosition.Visible = True
                End If
            End If

            If Not drStaff("EmailAddress") Is DBNull.Value Then
                If Not drStaff("EmailAddress") = "" Then
                    Dim strEmailAddress As String = drStaff("EmailAddress").ToString()
                    Dim divEmail As HtmlGenericControl = item.FindControl("divEmail")
                    Dim litEmail As Literal = item.FindControl("litEmail")
                    litEmail.Text = "<a class='mailtoSml' href='mailto:" & strEmailAddress & "'>" & strEmailAddress & "</a>"
                    divEmail.Visible = True
                End If
            End If

            If Not drStaff("Body") Is DBNull.Value Then
                If Not drStaff("Body") = "" Then
                    Dim divBio As HtmlGenericControl = item.FindControl("divBio")
                    Dim litBio As Literal = item.FindControl("litBio")
                    litBio.Text = drStaff("Body")
                    divBio.Visible = True
                End If
            End If

            'Finally load the staff members image if they have uploaded one, else use the default face placeholder
            If Not drStaff("thumbnail").ToString() = "" Then
                Dim radStaffImage As RadBinaryImage = item.FindControl("radStaffImage")
                radStaffImage.DataValue = drStaff("thumbnail")

                'Add the alternate text
                If Not drStaff("thumbnailName") Is DBNull.Value Then
                    radStaffImage.AlternateText = drStaff("thumbnailName")
                Else
                    radStaffImage.AlternateText = Resources.Staff_FrontEnd.Staff_StaffRepeater_Thumbnail_NoImageAvailable
                End If
            End If
            'Check we need to show the comments and bookmark this
            If boolShowAddThis Then
                item.FindControl("addThisPlaceholder").Visible = True
            End If

        End If
    End Sub

    Protected Sub rlvStaff_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles rlvStaff.DataBound
        Dim pager As RadDataPager = DirectCast(rlvStaff.FindControl("RadDataPagerStaff"), RadDataPager)
        pager.Visible = (pager.PageSize < pager.TotalRowCount)

    End Sub

    Protected Sub imgSortUpName_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortUpName.Click
        rlvStaff_LoadWithSortExpression("LastName", "FirstName", RadListViewSortOrder.Ascending)
    End Sub

    Protected Sub imgSortDownName_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortDownName.Click
        rlvStaff_LoadWithSortExpression("LastName", "FirstName", RadListViewSortOrder.Descending)
    End Sub

    Protected Sub imgSortUpPosition_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortUpPosition.Click
        rlvStaff_LoadWithSortExpression("StaffPosition", RadListViewSortOrder.Ascending)
    End Sub

    Protected Sub imgSortDownPosition_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortDownPosition.Click
        rlvStaff_LoadWithSortExpression("StaffPosition", RadListViewSortOrder.Descending)
    End Sub
End Class
