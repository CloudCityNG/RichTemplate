Imports System.IO
Imports System.Data
Imports Telerik.Web.UI

Partial Class location_Default
    Inherits RichTemplateLanguagePage

    Public ModuleTypeID As Integer = 16

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim strCityCodes As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load


        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgLocation, "{4} {5} " & Resources.Location_FrontEnd.Location_Default_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Location_FrontEnd.Location_Default_Pager_PagerTextFormat_Page)

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
            'We do not have an active location module For the Front-End, so redirect to the homepage
            Response.Redirect("/")
        End If
        If Not Page.IsPostBack Then

            'Bind our CityCodes
            BindCityCodes()


            'Set the Page Title
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Location_FrontEnd.Location_Default_HeaderTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Location_FrontEnd.Location_Default_Heading)

            'Sets up our location grid sorting
            Dim sortCity As New GridSortExpression()
            sortCity.SortOrder = GridSortOrder.Ascending
            sortCity.FieldName = "city"

            'INITIALLY sort on last name first
            rgLocation.MasterTableView.SortExpressions.AddSortExpression(sortCity)
            rgLocation.Rebind()

        End If

    End Sub 'Page_Load

    Private Sub BindCityCodes()
        Dim strCurrentCityCode As String = ""
        If (Not Request.Params("city") Is Nothing) AndAlso (Request.Params("city").ToString().Length > 0) Then
            strCurrentCityCode = Request.Params("city").ToString().ToUpper()
        End If
        Dim listCityCodes As New List(Of String)

        For Each strCityCode As String In strCityCodes
            If Not strCityCode = strCurrentCityCode Then
                listCityCodes.Add(String.Format("<a href='?city={0}'>{0}</a>", strCityCode))
            Else
                listCityCodes.Add(String.Format("<span>{0}</span>", strCityCode))
            End If
        Next

        rptCityLettersTop.DataSource = listCityCodes
        rptCityLettersTop.DataBind()

        rptCityLettersBottom.DataSource = listCityCodes
        rptCityLettersBottom.DataBind()
    End Sub
    Protected Sub rgLocation_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgLocation.NeedDataSource

        If (Not Request.Params("city") Is Nothing) AndAlso (Request.Params("city").ToString().Length > 0) Then
            Dim strCityLetter As String = Request.Params("city")
            Dim dtLocations As DataTable = LocationDAL.GetLocation_ByCityLetter(strCityLetter.Substring(0, 1))
            rgLocation.DataSource = dtLocations

        Else
            Dim dtLocations As DataTable = LocationDAL.GetLocationList()
            rgLocation.DataSource = dtLocations

        End If

    End Sub

End Class
