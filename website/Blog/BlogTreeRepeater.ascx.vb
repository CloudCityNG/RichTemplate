Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Net.Mail

Partial Class Blog_BlogTreeRepeater
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 1

    Dim boolCommentsRequireApproval As Boolean = False
    Dim boolAllowComments As Boolean = False
    Dim boolShowAddThis As Boolean = False
    Dim boolStatus As Boolean = True
    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            If Request.QueryString("archive") = "1" Then
                boolStatus = False
            End If

            'Check we need to show the comments and bookmark this
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "show_add_this" Then
                    boolShowAddThis = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" Then
                    boolAllowComments = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                    boolEnableGroupsAndUserAccess = True
                End If
            Next

            If Not IsPostBack Then

                'Finally load the blogs
                rlvBlog_LoadWithSortExpression("viewDate", RadListViewSortOrder.Descending)
            End If
        End If

    End Sub
    Protected Sub rlvBlog_LoadWithSortExpression(ByVal strSortString As String, ByVal rlvSortOrder As RadListViewSortOrder)
        'Clear the current sort expressions
        rlvBlog.SortExpressions.Clear()

        If Not strSortString Is String.Empty Then

            'Set up our RadListView Grid Sort Expression
            Dim rlvSortExpression As New RadListViewSortExpression()
            rlvSortExpression.SortOrder = rlvSortOrder
            rlvSortExpression.FieldName = strSortString

            rlvBlog.SortExpressions.AddSortExpression(rlvSortExpression)

        End If

        'Bind our list
        rlvBlog.Rebind()
    End Sub
    Protected Sub rlvBlog_NeedDataSource(ByVal sender As Object, ByVal e As RadListViewNeedDataSourceEventArgs) Handles rlvBlog.NeedDataSource

        Dim paramCategory As Integer = 0
        Dim paramYear As Integer = 0
        Dim paramMonth As Integer = 0
        Dim paramSearchTagID As Integer = 0

        If Not Request.QueryString("catid") Is Nothing Then
            paramCategory = Convert.ToInt32(Request.QueryString("catID"))
        End If
        If Not Request.QueryString("year") Is Nothing Then
            paramYear = Convert.ToInt32(Request.QueryString("year"))
        End If
        If Not Request.QueryString("month") Is Nothing Then
            paramMonth = Convert.ToInt32(Request.QueryString("month"))
        End If
        If Not Request.QueryString("sTag") Is Nothing Then
            Dim strSearchTag As String = HttpUtility.UrlDecode(Request.QueryString("sTag"))
            Dim dtSearchTag As DataTable = SearchTagDAL.GetSearchTag_BySearchTagNameAndSiteID(strSearchTag, intSiteID)
            If dtSearchTag.Rows.Count > 0 Then
                paramSearchTagID = dtSearchTag.Rows(0)("SearchTagID")
            End If
        End If

        'Define default category ID And Sort Order
        Dim dtBlog As New DataTable

        'we are loading a list of blogs based on category, year and month
        If paramSearchTagID > 0 Then
            dtBlog = If(boolEnableGroupsAndUserAccess, BlogDAL.GetBlogList_BySearchTagIDAndStatusAndAccess_FrontEnd(paramSearchTagID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlogList_BySearchTagIDAndStatus_FrontEnd(paramSearchTagID, boolStatus, intSiteID))

        ElseIf Not Request.QueryString("catid") Is Nothing Then
            'We load blogs based on categories, year and month
            If paramCategory = 0 Then
                'categroyID is null blogs are uncategorized
                If paramYear > 0 And paramMonth > 0 Then
                    'Load by category, year, month
                    dtBlog = If(boolEnableGroupsAndUserAccess, BlogDAL.GetBlog_ByCategoryNullAndYearAndMonthAndStatusAndAccess_FrontEnd(paramYear, paramMonth, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlog_ByCategoryNullAndYearAndMonthAndStatus_FrontEnd(paramYear, paramMonth, boolStatus, intSiteID))
                ElseIf paramYear > 0 Then
                    'Load by category, year
                    dtBlog = If(boolEnableGroupsAndUserAccess, BlogDAL.GetBlog_ByCategoryNullAndYearAndStatusAndAccess_FrontEnd(paramYear, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlog_ByCategoryNullAndYearAndStatus_FrontEnd(paramYear, boolStatus, intSiteID))
                Else
                    'Load by Category
                    dtBlog = If(boolEnableGroupsAndUserAccess, BlogDAL.GetBlog_ByCategoryNullAndStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlog_ByCategoryNullAndStatus_FrontEnd(boolStatus, intSiteID))
                End If
            Else
                'categoryID > 0 (i.e the blogs that are categorized)
                If paramYear > 0 And paramMonth > 0 Then
                    'Load by category, year, month
                    dtBlog = If(boolEnableGroupsAndUserAccess, BlogDAL.GetBlog_ByCategoryIDAndYearAndMonthAndStatusAndAccess_FrontEnd(paramCategory, paramYear, paramMonth, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlog_ByCategoryIDAndYearAndMonthAndStatus_FrontEnd(paramCategory, paramYear, paramMonth, boolStatus, intSiteID))
                ElseIf paramYear > 0 Then
                    'Load by category, year
                    dtBlog = If(boolEnableGroupsAndUserAccess, BlogDAL.GetBlog_ByCategoryIDAndYearAndStatusAndAccess_FrontEnd(paramCategory, paramYear, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlog_ByCategoryIDAndYearAndStatus_FrontEnd(paramCategory, paramYear, boolStatus, intSiteID))
                Else
                    'Load by Category
                    dtBlog = If(boolEnableGroupsAndUserAccess, BlogDAL.GetBlog_ByCategoryIDAndStatusAndAccess_FrontEnd(paramCategory, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlog_ByCategoryIDAndStatus_FrontEnd(paramCategory, boolStatus, intSiteID))
                End If
            End If

        Else
            'Load All
            dtBlog = If(boolEnableGroupsAndUserAccess, BlogDAL.GetBlog_ByStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlog_ByStatus_FrontEnd(boolStatus, intSiteID))
        End If

        rlvBlog.DataSource = dtBlog

        'Note if we have no rows, we clear sort expressions
        If dtBlog.Rows.Count = 0 Then
            rlvBlog.SortExpressions.Clear()
        End If
    End Sub

    Protected Sub rlvBlog_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListViewItemEventArgs) Handles rlvBlog.ItemDataBound
        If TypeOf e.Item Is RadListViewDataItem Then
            Dim item As RadListViewDataItem = TryCast(e.Item, RadListViewDataItem)

            'Load in this blogs search tags
            Dim intBlogID As Integer = Convert.ToInt32(DataBinder.Eval(item.DataItem, "blogID"))
            Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intBlogID)
            If dtSearchTags.Rows.Count > 0 Then
                Dim divModuleSearchTagList As HtmlGenericControl = e.Item.FindControl("divModuleSearchTagList")
                divModuleSearchTagList.Visible = True
                Dim rptSearchTags As Repeater = e.Item.FindControl("rptSearchTags")
                rptSearchTags.DataSource = dtSearchTags
                rptSearchTags.DataBind()
            End If

            If boolShowAddThis Then
                item.FindControl("plcAddThis").Visible = True
            End If

            'Check we need to show the comments placeholder
            If Request.QueryString("id") Is Nothing And boolAllowComments Then
                item.FindControl("plcComments").Visible = True
                'If we are showing active records then show the 'add comment' link
                If boolStatus Then
                    item.FindControl("plcAddCommentLink").Visible = True
                End If
            End If

        End If
    End Sub

    Protected Sub rlvBlog_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles rlvBlog.DataBound
        Dim pager As RadDataPager = DirectCast(rlvBlog.FindControl("RadDataPagerBlog"), RadDataPager)
        pager.Visible = (pager.PageSize < pager.TotalRowCount)
    End Sub

    Protected Sub imgSortUpTitle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortUpTitle.Click
        rlvBlog_LoadWithSortExpression("Title", RadListViewSortOrder.Ascending)
    End Sub

    Protected Sub imgSortDownTitle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortDownTitle.Click
        rlvBlog_LoadWithSortExpression("Title", RadListViewSortOrder.Descending)
    End Sub

    Protected Sub imgSortUpDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortUpViewDate.Click
        rlvBlog_LoadWithSortExpression("viewDate", RadListViewSortOrder.Ascending)
    End Sub

    Protected Sub imgSortDownDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortDownViewDate.Click
        rlvBlog_LoadWithSortExpression("viewDate", RadListViewSortOrder.Descending)
    End Sub

End Class
