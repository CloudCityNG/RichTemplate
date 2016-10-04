Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Net.Mail

Partial Class Employment_EmploymentTreeRepeater
    Inherits System.Web.UI.UserControl

    Public ModuleTypeID As Integer = 4

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

            'Check we need to show the 'add this' and online registrations
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "show_add_this" Then
                    boolShowAddThis = True

                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_registrations" Then
                    boolAllowOnlineRegistration = True

                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                    boolEnableGroupsAndUserAccess = True

                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" Then
                    boolAllowComments = True

                End If
            Next

            If Not IsPostBack Then

                'Finally load the employments
                rlvEmployment_LoadWithSortExpression("viewDate", RadListViewSortOrder.Descending)
            End If
        End If

    End Sub

    Protected Sub rlvEmployment_LoadWithSortExpression(ByVal strSortString As String, ByVal rlvSortOrder As RadListViewSortOrder)
        'Clear the current sort expressions
        rlvEmployment.SortExpressions.Clear()

        If Not strSortString Is String.Empty Then

            'Set up our RadListView Grid Sort Expression
            Dim rlvSortExpression As New RadListViewSortExpression()
            rlvSortExpression.SortOrder = rlvSortOrder
            rlvSortExpression.FieldName = strSortString

            rlvEmployment.SortExpressions.AddSortExpression(rlvSortExpression)

        End If

        'Bind our list
        rlvEmployment.Rebind()
    End Sub

    Protected Sub rlvEmployment_NeedDataSource(ByVal sender As Object, ByVal e As RadListViewNeedDataSourceEventArgs) Handles rlvEmployment.NeedDataSource

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
        Dim dtEmployment As New DataTable

        'we are loading a list of employments based on category, year and month
        If paramSearchTagID > 0 Then
            dtEmployment = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmploymentList_BySearchTagIDAndStatusAndAccess_FrontEnd(paramSearchTagID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmploymentList_BySearchTagIDAndStatus_FrontEnd(paramSearchTagID, boolStatus, intSiteID))

        ElseIf Not Request.QueryString("catid") Is Nothing Then
            'We load employments based on categories, year and month
            If paramCategory = 0 Then
                'categroyID is null employments are uncategorized
                If paramYear > 0 And paramMonth > 0 Then
                    'Load by category, year, month
                    dtEmployment = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmployment_ByCategoryNullAndYearAndMonthAndStatusAndAccess_FrontEnd(paramYear, paramMonth, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByCategoryNullAndYearAndMonthAndStatus_FrontEnd(paramYear, paramMonth, boolStatus, intSiteID))
                ElseIf paramYear > 0 Then
                    'Load by category, year
                    dtEmployment = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmployment_ByCategoryNullAndYearAndStatusAndAccess_FrontEnd(paramYear, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByCategoryNullAndYearAndStatus_FrontEnd(paramYear, boolStatus, intSiteID))
                Else
                    'Load by Category
                    dtEmployment = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmployment_ByCategoryNullAndStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByCategoryNullAndStatus_FrontEnd(boolStatus, intSiteID))
                End If
            Else
                'categoryID > 0 (i.e the employments that are categorized)
                If paramYear > 0 And paramMonth > 0 Then
                    'Load by category, year, month
                    dtEmployment = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmployment_ByCategoryIDAndYearAndMonthAndStatusAndAccess_FrontEnd(paramCategory, paramYear, paramMonth, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByCategoryIDAndYearAndMonthAndStatus_FrontEnd(paramCategory, paramYear, paramMonth, boolStatus, intSiteID))
                ElseIf paramYear > 0 Then
                    'Load by category, year
                    dtEmployment = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmployment_ByCategoryIDAndYearAndStatusAndAccess_FrontEnd(paramCategory, paramYear, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByCategoryIDAndYearAndStatus_FrontEnd(paramCategory, paramYear, boolStatus, intSiteID))
                Else
                    'Load by Category
                    dtEmployment = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmployment_ByCategoryIDAndStatusAndAccess_FrontEnd(paramCategory, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByCategoryIDAndStatus_FrontEnd(paramCategory, boolStatus, intSiteID))
                End If
            End If

        Else
            'Load All
            dtEmployment = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmployment_ByStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByStatus_FrontEnd(boolStatus, intSiteID))
        End If

        rlvEmployment.DataSource = dtEmployment

        'Note if we have no rows, we clear sort expressions
        If dtEmployment.Rows.Count = 0 Then
            rlvEmployment.SortExpressions.Clear()
        End If
    End Sub

    Protected Sub rlvEmployment_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListViewItemEventArgs) Handles rlvEmployment.ItemDataBound
        If TypeOf e.Item Is RadListViewDataItem Then
            Dim item As RadListViewDataItem = TryCast(e.Item, RadListViewDataItem)

            Dim employmentDateTime As Literal = CType(e.Item.FindControl("employmentDateTime"), Literal)
            employmentDateTime.Text = DataBinder.Eval(item.DataItem, "viewdate", "{0:D}").ToString()
            employmentDateTime.Visible = True

            Dim litSummary As Literal = item.FindControl("litSummary")
            Dim strSummary As String = DataBinder.Eval(item.DataItem, "Summary").ToString()
            litSummary.Text = strSummary

            Dim strCityAndState As String = ""
            If Not DataBinder.Eval(item.DataItem, "City") Is DBNull.Value Then
                strCityAndState = strCityAndState & DataBinder.Eval(item.DataItem, "City").ToString()
            End If
            If Not DataBinder.Eval(item.DataItem, "StateName") Is DBNull.Value Then
                'add a comma seperator if we already have text
                If strCityAndState.Trim().Length > 0 Then
                    strCityAndState = strCityAndState & ", "
                End If
                strCityAndState = strCityAndState & DataBinder.Eval(item.DataItem, "StateName").ToString()
            End If
            If strCityAndState.Length > 0 Then
                Dim divCityAndState As HtmlGenericControl = item.FindControl("divCityAndState")
                Dim litCityAndState As Literal = item.FindControl("litCityAndState")

                litCityAndState.Text = strCityAndState
                divCityAndState.Visible = True
            End If

            Dim strSalaryRange As String = ""
            If Not DataBinder.Eval(item.DataItem, "SalaryMin") Is DBNull.Value Then
                'we have a value for the minimum salary
                If Not DataBinder.Eval(item.DataItem, "SalaryMax") Is DBNull.Value Then
                    'we also have a value for max salary so show both min and max salary
                    strSalaryRange = (Convert.ToDecimal(DataBinder.Eval(item.DataItem, "SalaryMin")).ToString("C")).Replace(".00", "") & " - " & (Convert.ToDecimal(DataBinder.Eval(item.DataItem, "SalaryMax")).ToString("C")).Replace(".00", "") & " "
                Else
                    'we don't have a value for max salary, so we only show the min salary
                    strSalaryRange = (Convert.ToDecimal(DataBinder.Eval(item.DataItem, "SalaryMin")).ToString("C")).Replace(".00", "") & " " & Resources.Employment_FrontEnd.Employment_EmploymentTreeRepeater_Salary_Above & " "
                End If

            Else
                'we do not have a minimum salary
                If Not DataBinder.Eval(item.DataItem, "SalaryMax") Is DBNull.Value Then
                    'we do have a max salary, so just show the max salaray
                    strSalaryRange = Resources.Employment_FrontEnd.Employment_EmploymentTreeRepeater_Salary_Below & " " & (Convert.ToDecimal(DataBinder.Eval(item.DataItem, "SalaryMax")).ToString("C")).Replace(".00", "") & " "
                Else
                    'we have neither a min nor a max, so show nothing
                End If
            End If

            'Check if we have any notes for the salary
            If Not DataBinder.Eval(item.DataItem, "SalaryNote") Is DBNull.Value Then
                strSalaryRange = strSalaryRange & DataBinder.Eval(item.DataItem, "SalaryNote").ToString()
            End If

            'Finally if we have a salaryRange with greater than 0 length pre-pend 'Salary: '
            If strSalaryRange.Length > 0 Then
                Dim divSalaryRange As HtmlGenericControl = item.FindControl("divSalaryRange")
                Dim litSalaryRange As Literal = item.FindControl("litSalaryRange")

                litSalaryRange.Text = strSalaryRange
                divSalaryRange.Visible = True
            End If

            'Check we need to show the clearance required
            If Not DataBinder.Eval(item.DataItem, "clearance") Is DBNull.Value AndAlso DataBinder.Eval(item.DataItem, "clearance").ToString().Trim().Length > 0 Then
                Dim divClearance As HtmlGenericControl = item.FindControl("divClearance")
                Dim litClearance As Literal = item.FindControl("litClearance")

                litClearance.Text = DataBinder.Eval(item.DataItem, "clearance").ToString()
                divClearance.Visible = True
            End If

            'Check we need to show the contact person
            If Not DataBinder.Eval(item.DataItem, "contactPerson") Is DBNull.Value AndAlso DataBinder.Eval(item.DataItem, "contactPerson").ToString().Trim().Length > 0 Then
                Dim divContactPerson As HtmlGenericControl = item.FindControl("divContactPerson")
                Dim litContactPerson As Literal = item.FindControl("litContactPerson")

                Dim strContactPerson As String = DataBinder.Eval(item.DataItem, "contactPerson").ToString()
                litContactPerson.Text = "<a href='mailto:" & strContactPerson & "'>" & strContactPerson & "</a>"
                divContactPerson.Visible = True
            End If

            'Load in this employments search tags
            Dim intEmploymentID As Integer = Convert.ToInt32(DataBinder.Eval(item.DataItem, "employmentID"))
            Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intEmploymentID)
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


    Protected Sub rlvEmployment_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles rlvEmployment.DataBound
        Dim pager As RadDataPager = DirectCast(rlvEmployment.FindControl("rdPagerEmployment"), RadDataPager)
        pager.Visible = (pager.PageSize < pager.TotalRowCount)

    End Sub

    Protected Sub imgSortUpTitle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortUpTitle.Click
        rlvEmployment_LoadWithSortExpression("Title", RadListViewSortOrder.Ascending)
    End Sub

    Protected Sub imgSortDownTitle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortDownTitle.Click
        rlvEmployment_LoadWithSortExpression("Title", RadListViewSortOrder.Descending)
    End Sub

    Protected Sub imgSortUpViewDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortUpViewDate.Click
        rlvEmployment_LoadWithSortExpression("viewDate", RadListViewSortOrder.Ascending)
    End Sub

    Protected Sub imgSortDownViewDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSortDownViewDate.Click
        rlvEmployment_LoadWithSortExpression("viewDate", RadListViewSortOrder.Descending)
    End Sub

End Class
