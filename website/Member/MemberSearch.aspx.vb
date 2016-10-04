Imports System.IO
Imports System.Data
Imports Telerik.Web.UI

Partial Class member_MemberSearch
    Inherits RichTemplateLanguagePage

    Private strServerPath As String
    Public ModuleTypeID As Integer = 8
    Public ModuleTypeID_Locations As Integer = 16

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim strMemberLetters As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load


        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()
        'CHECK IF MEMBER IS LOGGED IN
        'If intMemberID > 0 Then
        'Check if we are showing the Member Detail Control
        If Request.QueryString("id") Is Nothing Then

            'Setup Rad Controls
            CommonWeb.SetupRadGrid(rgMember, "{4} {5} " & Resources.Member_FrontEnd.Member_MemberSearch_MemberSearchResults_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Member_FrontEnd.Member_MemberSearch_MemberSearchResults_Pager_PagerTextFormat_Page)

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
            If Not Page.IsPostBack Then

                'Set the Page Title
                Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Member_FrontEnd.Member_MemberSearch_HeaderTitle
                CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Member_FrontEnd.Member_MemberSearch_Heading)
                'Bind data to our drop down lists and listboxs
                'Rather than Bind Department and State, we BIND Location Categories, Location and Site
                'BindDepartmentData()
                'BindStateData()

                BindLocationCategoryData()
                BindLocationData()
                BindSiteData()
                BindMemberLetters()

                'Sets up our member grid sorting
                Dim sortLastName As New GridSortExpression()
                sortLastName.SortOrder = GridSortOrder.Ascending
                sortLastName.FieldName = "lastName"

                'INITIALLY sort on last name first
                rgMember.MasterTableView.SortExpressions.AddSortExpression(sortLastName)
                rgMember.Rebind()

            End If
        Else
            divMemberSearch.Visible = False
            ucMemberDetail.Visible = True
        End If
        'IF MEMBER IS NOT LOGGED IN RE-DIRECT TO DEFAULT/LOGIN PAGE
        'Else
        '    Response.Redirect("default.aspx")
        'End If

    End Sub 'Page_Load

    Private Sub BindMemberLetters()
        Dim strCurrentMemberLetter As String = ""
        If (Not Request.Params("letter") Is Nothing) AndAlso (Request.Params("letter").ToString().Length > 0) Then
            strCurrentMemberLetter = Request.Params("letter").ToString().ToUpper()
        End If
        Dim listMemberLetters As New List(Of String)

        For Each strMemberLetter As String In strMemberLetters
            If Not strMemberLetter = strCurrentMemberLetter Then
                listMemberLetters.Add(String.Format("<a href='?letter={0}'>{0}</a>", strMemberLetter))
            Else
                listMemberLetters.Add(String.Format("<span>{0}</span>", strMemberLetter))
            End If
        Next

        rptMemberLettersTop.DataSource = listMemberLetters
        rptMemberLettersTop.DataBind()

        rptMemberLettersBottom.DataSource = listMemberLetters
        rptMemberLettersBottom.DataBind()

        'Finally pre-populate the last name field with this letter, so when we rebind to our data table, it will use this letter in the last name search
        If strCurrentMemberLetter.Length > 0 Then
            txtLastName.Text = strCurrentMemberLetter
        End If


    End Sub

    Public Sub BindLocationCategoryData()
        Dim dtLocationCategories As DataTable = CategoryDAL.GetCategoryList_ByModuleTypeIDAndSiteID(ModuleTypeID_Locations, intSiteID)
        ddlLocationCategory.Items.Clear()
        ddlLocationCategory.Items.Add(New ListItem("--" + Resources.Member_FrontEnd.Member_MemberSearch_MemberSearch_Department_PleaseSelect + "--", "0"))
        For Each drLocationCategory As DataRow In dtLocationCategories.Rows
            Dim iCategoryID As Integer = Convert.ToInt32(drLocationCategory("CategoryID"))
            Dim sCategoryName As String = drLocationCategory("CategoryName")
            ddlLocationCategory.Items.Add(New ListItem(sCategoryName, iCategoryID.ToString()))
        Next
    End Sub

    Public Sub BindLocationData()
        Dim dtLocation As DataTable = LocationDAL.GetLocationList_Distinct()
        ddlLocation.Items.Clear()
        ddlLocation.Items.Add(New ListItem("--" + Resources.Member_FrontEnd.Member_MemberSearch_MemberSearch_Location_PleaseSelect + "--", "0"))
        For Each drLocation As DataRow In dtLocation.Rows
            Dim sLocation As String = drLocation("Location")
            ddlLocation.Items.Add(New ListItem(sLocation, sLocation))
        Next
    End Sub

    Public Sub BindSiteData()
        Dim dtSite As DataTable = SiteDAL.GetSiteList_FrontEnd()
        ddlSite.Items.Clear()
        ddlSite.Items.Add(New ListItem("--" + Resources.Member_FrontEnd.Member_MemberSearch_MemberSearch_Location_PleaseSelect + "--", "0"))
        For Each drSite As DataRow In dtSite.Rows
            Dim iSiteID As Integer = Convert.ToInt32(drSite("ID"))
            Dim sSiteName As String = drSite("SiteName")
            ddlSite.Items.Add(New ListItem(sSiteName, iSiteID.ToString()))

        Next
    End Sub

    Protected Sub rgMember_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMember.NeedDataSource

        Dim strFirstName As String = txtFirstName.Text.Trim()
        Dim strLastName As String = txtLastName.Text.Trim()

        Dim intCategoryID As Integer = Int32.MinValue
        If ddlLocationCategory.SelectedIndex > 0 Then
            intCategoryID = Convert.ToInt32(ddlLocationCategory.SelectedValue)
        End If

        Dim strLocation As String = String.Empty
        If ddlLocation.SelectedIndex > 0 Then
            strLocation = ddlLocation.SelectedValue.ToString()
        End If

        Dim intSiteID As Integer = Int32.MinValue
        If ddlSite.SelectedIndex > 0 Then
            intSiteID = Convert.ToInt32(ddlSite.SelectedValue)
        End If

        'if we are loading archive records, before we bind the grid, check we are allowed to show archive records
        Dim dtMember As DataTable = MemberDAL.GetMemberList_ByMultipleColumns_FrontEnd(strFirstName, strLastName, intCategoryID, strLocation, intSiteID)
        rgMember.DataSource = dtMember

    End Sub

    Protected Sub rgMember_SortCommand(ByVal sender As Object, ByVal e As GridSortCommandEventArgs) Handles rgMember.SortCommand

        Dim sortExpr As New GridSortExpression
        Select Case e.OldSortOrder

            Case GridSortOrder.Ascending
                sortExpr.FieldName = e.SortExpression
                sortExpr.SortOrder = If(rgMember.MasterTableView.AllowNaturalSort, GridSortOrder.None, GridSortOrder.Descending)
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
        rgMember.Rebind()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        rgMember.Rebind()
    End Sub

End Class
