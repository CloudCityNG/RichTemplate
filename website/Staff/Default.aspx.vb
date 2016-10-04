Imports System.IO
Imports System.Data
Imports Telerik.Web.UI

Partial Class staff_Default_StaffSearch
    Inherits RichTemplateLanguagePage

    Private strServerPath As String

    Dim boolAllowArchive As Boolean = False
    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Public ModuleTypeID As Integer = 12

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgStaff, "{4} {5} " & Resources.Staff_FrontEnd.Staff_DefaultSearch_StaffSearchResults_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Staff_FrontEnd.Staff_DefaultSearch_StaffSearchResults_Pager_PagerTextFormat_Page)

        'Get module information, to check if we will show its introduction
        Dim dtModule As DataTable = ModuleDAL.GetModule_ByModuleTypeIDAndSiteID_FrontEnd(ModuleTypeID, intSiteID)
        If dtModule.Rows.Count > 0 Then
            Dim drModule As DataRow = dtModule.Rows(0)
            If Not drModule("ModuleContentHTML") Is DBNull.Value Then
                Dim strModuleContentHTML As String = drModule("ModuleContentHTML")

                litModuleDynamicContent.Text = strModuleContentHTML
                divModuleDynamicContent.Visible = True
            End If
        Else
            'We do not have an Active Staff Module For the Front-End, so redirect to the homepage
            Response.Redirect("/")
        End If

        'Check we need to show the staff member archive
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()

            If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                boolAllowArchive = True

            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True

            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_rss" Then
                divModuleHeadingRssFeed.Visible = True

            End If
        Next

        If boolAllowArchive Then
            'Sets up or active/archived picker
            divActiveArchive.Visible = True
            If Request.QueryString("archive") = 1 Then
                aStaff_Active.Visible = True
                litStaff_Active.Visible = False

                aStaff_Archive.Visible = False
                litStaff_Archive.Visible = True
            Else
                aStaff_Active.Visible = False
                litStaff_Active.Visible = True

                aStaff_Archive.Visible = True
                litStaff_Archive.Visible = False
            End If
        End If


        If Not Page.IsPostBack Then

            'Set the Page Title
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Staff_FrontEnd.Staff_DefaultSearch_HeaderTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Staff_FrontEnd.Staff_DefaultSearch_Heading)

            'Bind data to our drop down lists and listboxs
            BindStateData()
            BindStaffPositionListBoxData()

            'Sets up our staff grid sorting
            Dim sortLastName As New GridSortExpression()
            sortLastName.SortOrder = GridSortOrder.Ascending
            sortLastName.FieldName = "lastName"

            'INITIALLY sort on last name first
            rgStaff.MasterTableView.SortExpressions.AddSortExpression(sortLastName)


            rgStaff.Rebind()
        End If
    End Sub 'Page_Load

    Public Sub BindStateData()
        Dim dtStates As DataTable = StateDAL.GetStateList()
        ddlStates.Items.Clear()
        ddlStates.Items.Add(New ListItem("--" + Resources.Staff_FrontEnd.Staff_DefaultSearch_StaffSearch_State_PleaseSelect + "--", "0"))
        ddlStates.DataSource = dtStates
        ddlStates.DataTextField = "StateName"
        ddlStates.DataValueField = "ID"
        ddlStates.DataBind()
    End Sub

    Protected Sub BindStaffPositionListBoxData()
        Dim dtStaffPositions As DataTable = StaffDAL.GetStaffPositionList_BySiteID(intSiteID)
        lbStaffPositions.Items.Clear()
        For Each drStaffPositions As DataRow In dtStaffPositions.Rows
            Dim staffPositionID As String = drStaffPositions("staffPositionID").ToString()
            Dim staffPosition As String = drStaffPositions("StaffPosition").ToString()
            lbStaffPositions.Items.Add(New ListItem(staffPosition, staffPositionID))
        Next
    End Sub

    Protected Sub rgStaff_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgStaff.NeedDataSource
        Dim boolStatus As Boolean = True
        If Request.QueryString("archive") = 1 Then
            boolStatus = False
        End If
        Dim strFirstName As String = txtFirstName.Text.Trim()
        Dim strLastName As String = txtLastName.Text.Trim()
        Dim strCompany As String = txtCompany.Text.Trim()

        Dim intStateID As Integer = Convert.ToInt32(ddlStates.SelectedValue)

        Dim listStaffPositionIDs As New List(Of Integer)
        For Each liStaffPositionID As ListItem In lbStaffPositions.Items
            If liStaffPositionID.Selected Then
                listStaffPositionIDs.Add(Convert.ToInt32(liStaffPositionID.Value))
            End If
        Next

        'if we are loading archive records, before we bind the grid, check we are allowed to show archive records
        If boolStatus = True Or boolAllowArchive = True Then
            Dim dtStaff As DataTable = If(boolEnableGroupsAndUserAccess, StaffDAL.GetStaffList_ByMultipleColumnsAndAccess_FrontEnd(boolStatus, strFirstName, strLastName, strCompany, intStateID, listStaffPositionIDs, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), StaffDAL.GetStaffList_ByMultipleColumns_FrontEnd(boolStatus, strFirstName, strLastName, strCompany, intStateID, listStaffPositionIDs, intSiteID))
            rgStaff.DataSource = dtStaff
        End If

    End Sub

    Protected Sub rgStaff_SortCommand(ByVal sender As Object, ByVal e As GridSortCommandEventArgs) Handles rgStaff.SortCommand

        Dim sortExpr As New GridSortExpression
        Select Case e.OldSortOrder

            Case GridSortOrder.Ascending
                sortExpr.FieldName = e.SortExpression
                sortExpr.SortOrder = If(rgStaff.MasterTableView.AllowNaturalSort, GridSortOrder.None, GridSortOrder.Descending)
                e.Item.OwnerTableView.SortExpressions.AddAt(0, sortExpr)
            Case GridSortOrder.Descending
                sortExpr.FieldName = e.SortExpression
                sortExpr.SortOrder = GridSortOrder.Ascending
                e.Item.OwnerTableView.SortExpressions.AddAt(0, sortExpr)
            Case Else
                sortExpr.FieldName = e.SortExpression
                sortExpr.SortOrder = GridSortOrder.Descending
        End Select
        e.Canceled = True
        rgStaff.Rebind()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        rgStaff.Rebind()
    End Sub

End Class
