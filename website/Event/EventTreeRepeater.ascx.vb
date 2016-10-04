Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Net.Mail

Partial Class Event_EventTreeRepeater
    Inherits System.Web.UI.UserControl

    Public ModuleTypeID As Integer = 5

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolStatus As Boolean = True
    Dim boolCommentsRequireApproval As Boolean = False
    Dim boolAllowComments As Boolean = False
    Dim boolAllowOnlineRegistration As Boolean = False
    Dim boolShowAddThis As Boolean = False
    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            If Request.QueryString("archive") = "1" Then
                boolStatus = False
            End If

            'Check we need to show the comments, captcha and comment approval
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" Then
                    'Set up comment module
                    boolAllowComments = True

                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "show_add_this" Then
                    boolShowAddThis = True

                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_registrations" Then
                    boolAllowOnlineRegistration = True

                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                    boolEnableGroupsAndUserAccess = True

                End If
            Next

            If Not IsPostBack Then

                'Finally load the events
                rlvEvent_LoadWithSortExpression("startDate", "startTime", RadListViewSortOrder.Ascending)
            End If
        End If

    End Sub

    Protected Sub rlvEvent_LoadWithSortExpression(ByVal strSortString As String, ByVal rlvSortOrder As RadListViewSortOrder)
        'Clear the current sort expressions
        rlvEvent.SortExpressions.Clear()

        If Not strSortString Is String.Empty Then

            'Set up our RadListView Grid Sort Expression
            Dim rlvSortExpression As New RadListViewSortExpression()
            rlvSortExpression.SortOrder = rlvSortOrder
            rlvSortExpression.FieldName = strSortString

            rlvEvent.SortExpressions.AddSortExpression(rlvSortExpression)

        End If

        'Bind our list
        rlvEvent.Rebind()
    End Sub

    Protected Sub rlvEvent_LoadWithSortExpression(ByVal strSortString As String, ByVal strSortStringSecondary As String, ByVal rlvSortOrder As RadListViewSortOrder)
        'Clear the current sort expressions
        rlvEvent.SortExpressions.Clear()

        'Set up our RadListView Grid Sort Expression
        If Not strSortString Is String.Empty Then

            rlvEvent.AllowNaturalSort = True
            rlvEvent.SortExpressions.AllowNaturalSort = True

            rlvEvent.AllowMultiItemSelection = True
            rlvEvent.SortExpressions.AllowMultiFieldSorting = True

            'Set up our RadListView Grid Sort Expression
            Dim rlvSortExpression As New RadListViewSortExpression()
            rlvSortExpression.SortOrder = rlvSortOrder
            rlvSortExpression.FieldName = strSortString

            Dim rlvSortExpressionSecondary As New RadListViewSortExpression()
            rlvSortExpressionSecondary.SortOrder = rlvSortOrder
            rlvSortExpressionSecondary.FieldName = strSortStringSecondary

            rlvEvent.SortExpressions.AddSortExpression(rlvSortExpression)
            rlvEvent.SortExpressions.AddSortExpression(rlvSortExpressionSecondary)
        End If

        'Bind our list
        rlvEvent.Rebind()

    End Sub

    Protected Sub rlvEvent_NeedDataSource(ByVal sender As Object, ByVal e As RadListViewNeedDataSourceEventArgs) Handles rlvEvent.NeedDataSource

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
        Dim dtEvent As New DataTable

        'we are loading a list of events based on category, year and month
        If paramSearchTagID > 0 Then
            dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEventList_BySearchTagIDAndStatusAndAccess_FrontEnd(paramSearchTagID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEventList_BySearchTagIDAndStatus_FrontEnd(paramSearchTagID, boolStatus, intSiteID))

        ElseIf Not Request.QueryString("catid") Is Nothing Then
            'We load events based on categories, year and month
            If paramCategory = 0 Then
                'categroyID is null events are uncategorized
                If paramYear > 0 And paramMonth > 0 Then
                    'Load by category, year, month
                    dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByCategoryNullAndYearAndMonthAndStatusAndAccess_FrontEnd(paramYear, paramMonth, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByCategoryNullAndYearAndMonthAndStatus_FrontEnd(paramYear, paramMonth, boolStatus, intSiteID))
                ElseIf paramYear > 0 Then
                    'Load by category, year
                    dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByCategoryNullAndYearAndStatusAndAccess_FrontEnd(paramYear, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByCategoryNullAndYearAndStatus_FrontEnd(paramYear, boolStatus, intSiteID))
                Else
                    'Load by Category
                    dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByCategoryNullAndStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByCategoryNullAndStatus_FrontEnd(boolStatus, intSiteID))
                End If
            Else
                'categoryID > 0 (i.e the events that are categorized)
                If paramYear > 0 And paramMonth > 0 Then
                    'Load by category, year, month
                    dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByCategoryIDAndYearAndMonthAndStatusAndAccess_FrontEnd(paramCategory, paramYear, paramMonth, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByCategoryIDAndYearAndMonthAndStatus_FrontEnd(paramCategory, paramYear, paramMonth, boolStatus, intSiteID))
                ElseIf paramYear > 0 Then
                    'Load by category, year
                    dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByCategoryIDAndYearAndStatusAndAccess_FrontEnd(paramCategory, paramYear, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByCategoryIDAndYearAndStatus_FrontEnd(paramCategory, paramYear, boolStatus, intSiteID))
                Else
                    'Load by Category
                    dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByCategoryIDAndStatusAndAccess_FrontEnd(paramCategory, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByCategoryIDAndStatus_FrontEnd(paramCategory, boolStatus, intSiteID))
                End If
            End If

        Else
            'Load All
            dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByStatus_FrontEnd(boolStatus, intSiteID))
        End If

        rlvEvent.DataSource = dtEvent

        'Note if we have no rows, we clear sort expressions
        If dtEvent.Rows.Count = 0 Then
            rlvEvent.SortExpressions.Clear()
        End If
    End Sub

    Protected Sub rlvEvent_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListViewItemEventArgs) Handles rlvEvent.ItemDataBound
        If TypeOf e.Item Is RadListViewDataItem Then
            Dim item As RadListViewDataItem = TryCast(e.Item, RadListViewDataItem)


            'Show StartTime if entered
            If Not IsDBNull(DataBinder.Eval(item.DataItem, "startDate")) Then
                Dim lblText As String = Nothing
                Dim lbl As Label = CType(e.Item.FindControl("eventDateTime"), Label)

                lblText = DataBinder.Eval(item.DataItem, "StartDate", "{0:D}").ToString()
                If Not DataBinder.Eval(item.DataItem, "startTime").ToString() Is Nothing Then
                    lblText += " " & DataBinder.Eval(item.DataItem, "startTime", "{0:h:mm tt}").ToString
                End If

                If DataBinder.Eval(item.DataItem, "startDate").ToString() = DataBinder.Eval(item.DataItem, "endDate").ToString() Then

                    If Not IsDBNull(DataBinder.Eval(item.DataItem, "endTime")) Then
                        lblText += " - " & DataBinder.Eval(item.DataItem, "endTime", "{0:h:mm tt}").ToString
                    End If

                Else
                    If Not IsDBNull(DataBinder.Eval(item.DataItem, "endDate")) Then
                        lblText += " - " & DataBinder.Eval(item.DataItem, "endDate", "{0:D}").ToString()
                        If Not IsDBNull(DataBinder.Eval(item.DataItem, "endTime").ToString()) Then
                            lblText += " " & DataBinder.Eval(item.DataItem, "endTime", "{0:h:mm tt}").ToString
                        End If
                    Else
                        If Not IsDBNull(DataBinder.Eval(item.DataItem, "endTime")) Then
                            lblText += " - " & DataBinder.Eval(item.DataItem, "endTime", "{0:h:mm tt}").ToString
                        End If

                    End If

                End If
                lbl.Text = lblText
                lbl.Visible = True
            End If

            'Check we need to show the contact person
            If Not DataBinder.Eval(item.DataItem, "contactPerson") Is DBNull.Value AndAlso DataBinder.Eval(item.DataItem, "contactPerson").ToString().Trim().Length > 0 Then
                Dim divContactPerson As HtmlGenericControl = item.FindControl("divContactPerson")
                Dim litContactPerson As Literal = item.FindControl("litContactPerson")

                Dim strContactPerson As String = DataBinder.Eval(item.DataItem, "contactPerson").ToString()
                litContactPerson.Text = "<a href='mailto:" & strContactPerson & "'>" & strContactPerson & "</a>"
                divContactPerson.Visible = True
            End If

            'Load in this events search tags
            Dim intEventID As Integer = Convert.ToInt32(DataBinder.Eval(item.DataItem, "eventID"))
            Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intEventID)
            If dtSearchTags.Rows.Count > 0 Then
                Dim divModuleSearchTagList As HtmlGenericControl = e.Item.FindControl("divModuleSearchTagList")
                divModuleSearchTagList.Visible = True
                Dim rptSearchTags As Repeater = e.Item.FindControl("rptSearchTags")
                rptSearchTags.DataSource = dtSearchTags
                rptSearchTags.DataBind()
            End If

            'Check we need to show the signup and comments placeholder
            Dim boolIsUsingExternalLinkUrl As Boolean = False
            If Not DataBinder.Eval(item.DataItem, "externalLinkUrl") Is DBNull.Value AndAlso DataBinder.Eval(item.DataItem, "externalLinkUrl").ToString().Trim().Length > 0 Then
                boolIsUsingExternalLinkUrl = True
            End If

            'Check we need to show the Online Signup placeholder
            If (boolStatus) AndAlso (boolAllowOnlineRegistration) AndAlso (Not boolIsUsingExternalLinkUrl) Then
                Dim boolOnlineSignup As Boolean = Convert.ToBoolean(DataBinder.Eval(item.DataItem, "onlineSignup"))
                If boolOnlineSignup Then
                    Dim plcOnlineSignup As PlaceHolder = e.Item.FindControl("plcOnlineSignup")
                    plcOnlineSignup.Visible = True

                End If
            End If

            'Check we need to show the 'bookmark this' placeholder
            If boolShowAddThis Then
                item.FindControl("plcAddThis").Visible = True
            End If


            If (boolAllowComments) And (Not boolIsUsingExternalLinkUrl) Then
                item.FindControl("plcComments").Visible = True
                'If we are showing active records then show the 'add comment' link
                If boolStatus Then
                    item.FindControl("plcAddCommentLink").Visible = True
                End If
            End If

        End If
    End Sub


    Protected Sub rlvEvent_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles rlvEvent.DataBound
        Dim pager As RadDataPager = DirectCast(rlvEvent.FindControl("rdPagerEvent"), RadDataPager)
        pager.Visible = (pager.PageSize < pager.TotalRowCount)

    End Sub

    Protected Sub imgSortUpTitle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortUpTitle.Click
        rlvEvent_LoadWithSortExpression("Title", RadListViewSortOrder.Ascending)
    End Sub

    Protected Sub imgSortDownTitle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortDownTitle.Click
        rlvEvent_LoadWithSortExpression("Title", RadListViewSortOrder.Descending)
    End Sub

    Protected Sub imgSortUpStartDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortUpStartDate.Click
        rlvEvent_LoadWithSortExpression("startDate", "startTime", RadListViewSortOrder.Ascending)
    End Sub

    Protected Sub imgSortDownStartDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortDownStartDate.Click
        rlvEvent_LoadWithSortExpression("startDate", "startTime", RadListViewSortOrder.Descending)
    End Sub
End Class
