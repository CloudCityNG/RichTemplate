Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Data

Partial Class PressRelease_PressReleaseCategoryListing
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 10

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolShowRssFeed As Boolean = False
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

        'Check we need to show the press release RSS FEED, only show if we are showing active records
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "enable_rss" Then
                boolShowRssFeed = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True

            End If
        Next

        'Get Categories with Count for FrontEnd
        Dim dtCategory As DataTable = If(boolEnableGroupsAndUserAccess, CategoryDAL.GetCategoryList_WithCount_ByModuleTypeIDAndStatusAndAccess(ModuleTypeID, intSiteID, True, "prID", "PublicationDate", "ExpirationDate", MemberDAL.GetCurrentMemberGroupIDs(), intMemberID), CategoryDAL.GetCategoryList_WithCount_ByModuleTypeIDAndStatus(ModuleTypeID, intSiteID, True, "PublicationDate", "ExpirationDate"))

        catRepeater.DataSource = dtCategory
        catRepeater.DataBind()
    End Sub



    Protected Sub catRepeater_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles catRepeater.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then

            If e.Item.DataItem("catcount") = 0 Then
                e.Item.Visible = False
            End If

            'Check we need to show the press release RSS FEED
            If boolShowRssFeed Then
                e.Item.FindControl("rssPressRelease").Visible = True
            End If

        End If
        If e.Item.ItemType = ListItemType.Footer Then

            'Check we need to show the press release RSS FEED
            If boolShowRssFeed Then
                e.Item.FindControl("rssUnCategorized").Visible = True
                e.Item.FindControl("rssShowAll").Visible = True
            End If

            'get the number of press release entries that are not in a category, if all active press releases are in a category, hide the uncategorized category
            Dim dtPressRelease_Uncategorized As DataTable = If(boolEnableGroupsAndUserAccess, PressReleaseDAL.GetPressRelease_ByCategoryNullAndStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PressReleaseDAL.GetPressRelease_ByCategoryNullAndStatus_FrontEnd(True, intSiteID))
            If dtPressRelease_Uncategorized.Rows.Count > 0 Then
                e.Item.FindControl("liUnCategorized").Visible = True
            End If

        End If

    End Sub

End Class
