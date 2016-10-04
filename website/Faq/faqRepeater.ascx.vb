Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class Faq_FaqRepeater
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 6

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolStatus As Boolean = True
    Dim boolAllowComments As Boolean = False
    Dim boolShowAddThis As Boolean = False
    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'Only load the repeater if its visible
        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            'Only set this control up if its visible, e.g. no id supplied, if id is supplied we load the details page
            'Check we need to show the comments, 'Add This' component
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" Then
                    boolAllowComments = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "show_add_this" Then
                    boolShowAddThis = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                    boolEnableGroupsAndUserAccess = True
                End If
            Next

            'Finally load faq's
            If Not IsPostBack Then
                rlvFaq_LoadWithSortExpression("question", RadListViewSortOrder.Ascending)
            End If
        End If

    End Sub

    Protected Sub rlvFaq_LoadWithSortExpression(ByVal strSortString As String, ByVal rlvSortOrder As RadListViewSortOrder)
        'Clear the current sort expressions
        rlvFaq.SortExpressions.Clear()

        If Not strSortString Is String.Empty Then

            'Set up our RadListView Grid Sort Expression
            Dim rlvSortExpression As New RadListViewSortExpression()
            rlvSortExpression.SortOrder = rlvSortOrder
            rlvSortExpression.FieldName = strSortString

            rlvFaq.SortExpressions.AddSortExpression(rlvSortExpression)

        End If

        'Bind our list
        rlvFaq.Rebind()
    End Sub

    Protected Sub rlvFaq_NeedDataSource(ByVal sender As Object, ByVal e As RadListViewNeedDataSourceEventArgs) Handles rlvFaq.NeedDataSource
        If Request.QueryString("archive") = "1" Then
            boolStatus = False
        End If

        'Define default category ID and Sort Order
        Dim dtFaq As New DataTable

        'Check if we should load faq records based on Search Tags
        Dim paramSearchTagID As Integer = 0
        If Not Request.QueryString("sTag") Is Nothing Then
            Dim strSearchTag As String = HttpUtility.UrlDecode(Request.QueryString("sTag"))
            Dim dtSearchTag As DataTable = SearchTagDAL.GetSearchTag_BySearchTagNameAndSiteID(strSearchTag, intSiteID)
            If dtSearchTag.Rows.Count > 0 Then
                paramSearchTagID = dtSearchTag.Rows(0)("SearchTagID")
            End If
        End If

        'populate our dataset with faq's
        If paramSearchTagID > 0 Then
            dtFaq = If(boolEnableGroupsAndUserAccess, FaqDAL.GetFaqList_BySearchTagIDAndStatusAndAccess_FrontEnd(paramSearchTagID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), FaqDAL.GetFaqList_BySearchTagIDAndStatus_FrontEnd(paramSearchTagID, boolStatus, intSiteID))
        ElseIf Request.QueryString("catID") <> "" Then
            Dim intCatID As Integer = Convert.ToInt32(Request.QueryString("catID"))
            If intCatID = 0 Then
                'Load faq's that are un-categorized
                dtFaq = If(boolEnableGroupsAndUserAccess, FaqDAL.GetFaq_ByCategoryNullAndStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), FaqDAL.GetFaq_ByCategoryNullAndStatus_FrontEnd(boolStatus, intSiteID))

            Else
                'Load faq's that are part of a specific category
                dtFaq = If(boolEnableGroupsAndUserAccess, FaqDAL.GetFaq_ByCategoryIDAndStatusAndAccess_FrontEnd(intCatID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), FaqDAL.GetFaq_ByCategoryIDAndStatus_FrontEnd(intCatID, boolStatus, intSiteID))
            End If
        Else
            'We want all faq's from all categories
            dtFaq = If(boolEnableGroupsAndUserAccess, FaqDAL.GetFaq_ByStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), FaqDAL.GetFaq_ByStatus_FrontEnd(boolStatus, intSiteID))
        End If

        rlvFaq.DataSource = dtFaq

        'Note if we have no rows, we clear sort expressions
        If dtFaq.Rows.Count = 0 Then
            rlvFaq.SortExpressions.Clear()
        End If

    End Sub


    Protected Sub rlvFaq_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles rlvFaq.DataBound
        Dim pager As RadDataPager = DirectCast(rlvFaq.FindControl("rdPagerFaq"), RadDataPager)
        pager.Visible = (pager.PageSize < pager.TotalRowCount)

    End Sub

    Protected Sub rlvFaq_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListViewItemEventArgs) Handles rlvFaq.ItemDataBound
        If TypeOf e.Item Is RadListViewDataItem Then
            Dim item As RadListViewDataItem = TryCast(e.Item, RadListViewDataItem)

            'Load in this faq's search tags
            Dim intFaqID As Integer = Convert.ToInt32(DataBinder.Eval(item.DataItem, "faqID"))

            Dim faqDateTime As Literal = CType(e.Item.FindControl("faqDateTime"), Literal)
            faqDateTime.Text = DataBinder.Eval(item.DataItem, "viewdate", "{0:D}").ToString()
            faqDateTime.Visible = True

            Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intFaqID)
            If dtSearchTags.Rows.Count > 0 Then
                Dim divModuleSearchTagList As HtmlGenericControl = e.Item.FindControl("divModuleSearchTagList")
                divModuleSearchTagList.Visible = True
                Dim rptSearchTags As Repeater = e.Item.FindControl("rptSearchTags")
                rptSearchTags.DataSource = dtSearchTags
                rptSearchTags.DataBind()
            End If

            'Check we need to show the 'bookmark this' placeholder
            If boolShowAddThis Then
                item.FindControl("plcAddThis").Visible = True
            End If

            If boolAllowComments Then
                item.FindControl("plcComments").Visible = True
                'If we are showing active records then show the 'add comment' link
                If boolStatus Then
                    item.FindControl("plcAddCommentLink").Visible = True
                End If
            End If

        End If
    End Sub

    Protected Sub imgSortUpQuestion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortUpQuestion.Click
        rlvFaq_LoadWithSortExpression("question", RadListViewSortOrder.Ascending)
    End Sub

    Protected Sub imgSortDownQuestion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortDownQuestion.Click
        rlvFaq_LoadWithSortExpression("question", RadListViewSortOrder.Descending)
    End Sub

    Protected Sub imgSortUpViewDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortUpViewDate.Click
        rlvFaq_LoadWithSortExpression("viewDate", RadListViewSortOrder.Ascending)
        rlvFaq.Rebind()
    End Sub

    Protected Sub imgSortDownViewDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortDownViewDate.Click
        rlvFaq_LoadWithSortExpression("viewDate", RadListViewSortOrder.Descending)
    End Sub

End Class
