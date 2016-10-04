Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Data
Imports Telerik.Web.UI

Partial Class Event_EventTree
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 5

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolEnableGroupsAndUserAccess As Boolean = False
    Private intTotalEvents As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            GetCategories()
        End If
    End Sub

    Private Sub GetCategories()

        Dim boolStatus As Boolean = True
        If Request.QueryString("archive") = "1" Then
            boolStatus = False
        End If

        'Check we need to show the event RSS FEED, only show if we are showing active records
        Dim boolShowEventRssFeed As Boolean = False
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "enable_rss" Then
                If boolStatus Then
                    boolShowEventRssFeed = True
                    'set the category list class, to allow for the rss image
                    pnlCategoryListEvent.CssClass = "divCategoryList_withRSS"
                End If

            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True
            End If
        Next

        Dim paramEventID As Integer = 0
        Dim paramCategory As Integer = 0
        Dim paramYear As Integer = 0
        Dim paramMonth As Integer = 0


        'Reset the event count and tree node view
        intTotalEvents = 0
        rtvEvent.Nodes.Clear()

        If Not Request.QueryString("id") Is Nothing Then
            paramEventID = Convert.ToInt32(Request.QueryString("id"))
            Dim dtEvent As DataTable = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByEventIDAndStatusAndAccess_FrontEnd(paramEventID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByEventIDAndStatus_FrontEnd(paramEventID, boolStatus, intSiteID))
            If dtEvent.Rows.Count > 0 Then
                Dim drEvent As DataRow = dtEvent.Rows(0)
                Dim dtStartDate As DateTime = drEvent("startDate")
                If Not drEvent("startTime") Is DBNull.Value Then
                    Dim dtStartTime As DateTime = drEvent("startTime")
                    dtStartDate.AddTicks(dtStartTime.Ticks)
                End If

                If Not drEvent("ActualCategoryID") Is DBNull.Value Then
                    paramCategory = Convert.ToInt32(drEvent("ActualCategoryID"))
                End If
                paramYear = dtStartDate.Year
                paramMonth = dtStartDate.Month
            End If
        Else
            If Not Request.QueryString("catid") Is Nothing Then
                paramCategory = Convert.ToInt32(Request.QueryString("catID"))
            End If
            If Not Request.QueryString("year") Is Nothing Then
                paramYear = Convert.ToInt32(Request.QueryString("year"))
            End If
            If Not Request.QueryString("month") Is Nothing Then
                paramMonth = Convert.ToInt32(Request.QueryString("month"))
            End If
        End If

        'Get the Categories for the event module
        Dim dtCategory As DataTable = If(boolEnableGroupsAndUserAccess, CategoryDAL.GetCategoryList_WithCount_ByModuleTypeIDAndStatusAndAccess(ModuleTypeID, intSiteID, boolStatus, "eventID", "PublicationDate", "ExpirationDate", MemberDAL.GetCurrentMemberGroupIDs(), intMemberID), CategoryDAL.GetCategoryList_WithCount_ByModuleTypeIDAndStatus(ModuleTypeID, intSiteID, boolStatus, "PublicationDate", "ExpirationDate"))

        'Add the categories as root nodes to our rad tree view
        For Each drCategory As DataRow In dtCategory.Rows()

            Dim categoryID As Integer = Convert.ToInt32(drCategory("categoryID"))
            Dim categoryName As String = drCategory("categoryName").ToString()

            'Add the root node and only include the RSS Feed if the module config allows us to
            Dim rssFeed As String = ""
            If boolShowEventRssFeed Then
                rssFeed = "<a href='/RssFeedGen.aspx?rss=event&catid=" & categoryID & "'><img style='float:left; margin-left:-35px; margin-top:4px;' src='/images/feed-icon-14x14.png' /></a>"
            End If

            Dim rtnCategory As New RadTreeNode(categoryName + " &nbsp;<b>(0)</b>" & rssFeed)
            rtnCategory.Category = "Category"
            rtnCategory.NavigateUrl = "?catid=" & categoryID.ToString() & If(Request.QueryString("archive") = 1, "&archive=1", "")
            rtnCategory.Value = categoryID

            'set this node to invisible, note if we have entries for this node we set it to visible
            rtnCategory.Visible = False
            rtvEvent.Nodes.Add(rtnCategory)

            'Generate rest of tree
            GenerateTree(categoryID, categoryName, rtnCategory, paramCategory, paramYear, paramMonth, boolStatus)
        Next

        'Create the Un-categorized Node
        Dim categoryID_Uncategorized As Integer = 0
        Dim categoryName_Uncategorized As String = Resources.Event_FrontEnd.Event_EventTree_UnCategorized

        'Add the root node and only include the RSS Feed if the module config allows us to
        Dim rssFeed_Uncategorized As String = ""
        If boolShowEventRssFeed Then
            rssFeed_Uncategorized = "<a href='/RssFeedGen.aspx?rss=event&catid=" & categoryID_Uncategorized & "'><img style='float:left; margin-left:-35px; margin-top:4px;' src='/images/feed-icon-14x14.png' /></a>"
        End If

        Dim rtnCategory_Uncategorized As New RadTreeNode(categoryName_Uncategorized + " &nbsp;<b>(0)</b>" & rssFeed_Uncategorized)
        rtnCategory_Uncategorized.Category = "Category"
        rtnCategory_Uncategorized.NavigateUrl = "?catid=" & categoryID_Uncategorized.ToString() & If(Request.QueryString("archive") = 1, "&archive=1", "")
        rtnCategory_Uncategorized.Value = categoryID_Uncategorized

        'set this node to invisible, note if we have entries for this node we set it to visible
        rtnCategory_Uncategorized.Visible = False
        rtvEvent.Nodes.Add(rtnCategory_Uncategorized)

        'Generate rest of tree
        GenerateTree(categoryID_Uncategorized, categoryName_Uncategorized, rtnCategory_Uncategorized, paramCategory, paramYear, paramMonth, boolStatus)

        'Create the Show All Node
        Dim rssFeed_ShowAll As String = ""
        If boolShowEventRssFeed Then
            rssFeed_ShowAll = "<a target='_blank' href='/RssFeedGen.aspx?rss=event'><img style='float:left; margin-left:-35px; margin-top:4px;' src='/images/feed-icon-14x14.png' /></a>"
        End If
        Dim plusIcon As String = "<span class='rtPlusFake' style='float: left; margin-left: -18px; margin-top: 6px;'></span>"
        Dim rtn_ShowAll As New RadTreeNode(Resources.Event_FrontEnd.Event_EventTree_ShowAll & " &nbsp;<b>(" & intTotalEvents.ToString() & ")</b>" & rssFeed_ShowAll & plusIcon)
        rtn_ShowAll.NavigateUrl = "default.aspx" & If(Request.QueryString("archive") = 1, "?archive=1", "")
        'If no nodes are selected then we select show all, as we must be have some node selected
        If rtvEvent.SelectedNodes.Count = 0 Then
            'rtn_ShowAll.Selected = True
        End If
        rtvEvent.Nodes.Add(rtn_ShowAll)

    End Sub

    Protected Sub GenerateTree(ByVal categoryID As Integer, ByVal categoryName As String, ByRef rtnCategory As RadTreeNode, ByVal paramCategory As Integer, ByVal paramYear As Integer, ByVal paramMonth As Integer, ByVal boolStatus As Boolean)

        Dim dtEvent As New DataTable()
        If categoryID = 0 Then
            dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByCategoryNullAndStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByCategoryNullAndStatus_FrontEnd(boolStatus, intSiteID))
        Else
            dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByCategoryIDAndStatusAndAccess_FrontEnd(categoryID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByCategoryIDAndStatus_FrontEnd(categoryID, boolStatus, intSiteID))
        End If
        If dtEvent.Rows.Count > 0 Then
            Dim dvEvent As New DataView(dtEvent)
            dvEvent.Sort = "StartDate, StartTime"

            'First convert the min and max event dates, so they only include Year and Month
            Dim drEventFirst As DataRow = dvEvent(0).Row
            Dim drEventLast As DataRow = dvEvent(dvEvent.Count - 1).Row

            Dim dtMinDateToShow As DateTime = drEventFirst("startDate")
            If Not drEventFirst("startTime") Is DBNull.Value Then
                Dim dtMinDateToShow_Time As DateTime = drEventFirst("startTime")
                dtMinDateToShow.AddTicks(dtMinDateToShow_Time.Ticks)
            End If

            Dim dtMaxDateToShow As DateTime = drEventLast("startDate")
            If Not drEventLast("startTime") Is DBNull.Value Then
                Dim dtMaxDateToShow_Time As DateTime = drEventLast("startTime")
                dtMaxDateToShow.AddTicks(dtMaxDateToShow_Time.Ticks)
            End If

            Dim dtMinDateToShow_YearAndMonth As New DateTime(dtMinDateToShow.Year, dtMinDateToShow.Month, 1)
            Dim dtMaxDateToShow_YearAndMonth As New DateTime(dtMaxDateToShow.Year, dtMaxDateToShow.Month, 1)

            For dvIndex As Integer = 0 To dvEvent.Count - 1
                Dim drEvent As DataRow = dvEvent(dvIndex).Row

                'For each row we need to put this into its year/month category structure
                Dim dtStartDate As DateTime = Convert.ToDateTime(drEvent("startDate"))
                If Not drEvent("startTime") Is DBNull.Value Then
                    Dim dtStartTime As DateTime = drEvent("startTime")
                    dtStartDate.AddTicks(dtStartTime.Ticks)
                End If
                'If this category node has no nodes yet we use the first value
                If rtnCategory.Nodes.Count = 0 Then
                    'First show this category node
                    rtnCategory.Visible = True

                    'If this node has the same value as the currently selected category, or we are showing an individual event expand and select this node
                    If (Not Request.QueryString("catid") Is Nothing) Or (Not Request.QueryString("id") Is Nothing) Then
                        If rtnCategory.Value = paramCategory Then
                            rtnCategory.Selected = True
                            rtnCategory.Expanded = True
                        End If
                    End If

                    'Check if we have a year node
                    Dim lowestYear As Integer = dtMinDateToShow.Year.ToString()
                    Dim highestYear As Integer = dtMaxDateToShow.Year.ToString()
                    'For each year starting at the lowest year to the current year, we add this year then add its months
                    For iYear As Integer = lowestYear To highestYear
                        Dim rtnYear As New RadTreeNode(iYear.ToString() + " &nbsp;<b>(0)</b>", iYear.ToString())
                        rtnYear.Category = "Year"
                        rtnYear.NavigateUrl = "?catid=" & categoryID.ToString() & "&year=" & iYear.ToString() & If(Request.QueryString("archive") = 1, "&archive=1", "")

                        'If this node has the same value as the currently selected year, expand and select this node
                        If rtnYear.Value = paramYear And rtnCategory.Value = paramCategory Then
                            rtnYear.Selected = True
                            rtnYear.Expanded = True
                        End If

                        rtnCategory.Nodes.Insert(0, rtnYear)

                        'Now for each month of the year add it to the year node
                        For iMonth As Integer = 1 To 12
                            Dim dtTemp As New DateTime(iYear, iMonth, 1)
                            Dim dtCurrent As DateTime = DateTime.Now

                            'Only show months that we have data for
                            If dtTemp >= dtMinDateToShow_YearAndMonth And dtTemp <= dtMaxDateToShow_YearAndMonth Then
                                Dim rtnMonth As New RadTreeNode(dtTemp.ToString("MMMM") + " &nbsp;<b>(0)</b>", iMonth.ToString())
                                rtnMonth.ImageUrl = "/images/arrow_selected.jpg"
                                rtnMonth.Category = "Month"
                                rtnMonth.NavigateUrl = "?catid=" & categoryID.ToString() & "&year=" & iYear.ToString() & "&month=" & iMonth.ToString() & If(Request.QueryString("archive") = 1, "&archive=1", "")

                                'If this node has the same value as the currently selected month, expand and select this node
                                If rtnMonth.Value = paramMonth And rtnYear.Value = paramYear And rtnCategory.Value = paramCategory Then
                                    rtnMonth.Selected = True
                                    rtnMonth.Expanded = True
                                End If

                                rtnYear.Nodes.Insert(0, rtnMonth)
                            End If
                        Next
                    Next
                End If

                'Now continue to add each node
                Dim yearPublicationDate As Integer = dtStartDate.Year
                Dim monthPublicationDate As Integer = dtStartDate.Month
                Dim yearNode As RadTreeNode = rtnCategory.Nodes.FindNodeByValue(yearPublicationDate)
                Dim monthNode As RadTreeNode = yearNode.Nodes.FindNodeByValue(monthPublicationDate)

                'add 1 to its count for both the month, year and category
                IncreaseNodeCount(rtnCategory)
                IncreaseNodeCount(yearNode)
                IncreaseNodeCount(monthNode)
                intTotalEvents = intTotalEvents + 1
            Next
        End If
    End Sub

    Public Sub IncreaseNodeCount(ByRef rtnCurrent As RadTreeNode)
        If Not rtnCurrent Is Nothing Then
            Dim strCount As String = Regex.Match(rtnCurrent.Text, "\(.*\)").Value
            strCount = strCount.Substring(1, strCount.Length - 2)
            Dim intCurrentCount As Integer = Convert.ToInt32(strCount)
            rtnCurrent.Text = Regex.Replace(rtnCurrent.Text, "\(.*\)", "(" & intCurrentCount + 1 & ")")
        Else
            Dim strTest As String = ""
            strTest = "?"
        End If

    End Sub

End Class
