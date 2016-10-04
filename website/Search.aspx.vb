Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports Telerik.Web.UI
Imports RUDcontrols


Partial Class Search
    Inherits RichTemplateLanguagePage
    Public rudlist As RUDcontrols.RUDlist

    Dim intSiteID As Integer = 0
    Dim strSiteIdentifier As String = ""
    Dim litSearchResultInformation As Literal
    Dim litSearchResultPager As Literal
    Dim intSearchResultCount As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID and the site prefix
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(intSiteID)
        If dtSite.Rows.Count > 0 Then
            Dim drSite As DataRow = dtSite.Rows(0)

            strSiteIdentifier = drSite("SiteIdentifier")

            'Set the Page Title
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Search_FrontEnd.Search_HeaderTitle

            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Search_FrontEnd.Search_Heading)

            'If we have no usterms query parameter then we are not performing a search, so show search input instead
            'If Request.QueryString("usterms") Is Nothing Then
            'divSearchInput.Visible = True
            'Else
            If Not Request.QueryString("usterms") Is Nothing Then
                'Make a call to setup our Ultimate Search Output so it can handle multiple languages
                CommonWeb.SetupUltimateSearchOutput(ucUltimateSearchOutput)

                rudlist = New RUDlist
                rudlist.DefaultSortColumn = "StartDate"
                rudlist.DefaultSortOrder = "ASC"
                rudlist.FieldId = "PageTitle"
                rudlist.StoredProcedureSelect = "SP_RUDSearch"
                rudlist.StoredProceduresFields = New String() {"@Search"}
                rudlist.StoredProceduresValues = New String() {Request.QueryString("usterms")}
                rudlist.PageListName = "/Search.aspx"
                rudlist.QueryString = "usterms=" & Request.QueryString("usterms")
                rudlist.NumberOfCharacters = 300
                rudlist.PageNumber = Request.QueryString("page")
			
                ' rudlist.PageNumber = "1"
                rudlist.RowSeperator = "<hr class='dotted'>"
                rudlist.FieldId = "ID"
            End If
			ucUltimateSearchOutput.FilterResultsByCategory="http://" & strSiteIdentifier
        End If
		
		
    End Sub

    'Private Sub OnAfterSearch(ByVal sender As Object, ByVal e As AfterSearchEventArgs)
    '    ' You can change search results and sort order right after the search operation.
    '    Dim searchTerms As String = e.SearchTerms
    '    Dim searchType As String = e.SearchType
    '    Dim dv As DataView = e.SearchResults
    '    Dim dt As DataTable = dv.Table
    '    If (e.SearchTerms = "rice") Then
    '        '' Just to show you how you can retrieve each field in each row
    '        'For Each dr As DataRow In dt.Rows
    '        '    Dim counter As Integer = Convert.ToInt32(dr(0))
    '        '    Dim url As String = dr(1).ToString
    '        '    Dim title As String = dr(2).ToString
    '        '    Dim text As String = dr(3).ToString
    '        '    Dim score As Integer = Convert.ToInt32(dr(4).ToString)
    '        '    Dim lastModified As DateTime = Convert.ToDateTime(dr(5).ToString)

    '    End If
    'End Sub

    'Protected Sub ucUltimateSearchOutput_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles ucUltimateSearchOutput.ItemCreated
    '    Dim oTest As DataListItem = e.Item

    '    Dim litSearchRow As Literal = oTest.Controls(0)

    '    If oTest.ItemIndex = 0 Then
    '        litResultInformation = litSearchRow
    '    End If



    '    Dim sSearchRowText As String = litSearchRow.Text
    '    If (Not sSearchRowText.Contains("://www" + strSiteIdentifier)) And (Not sSearchRowText.Contains("://www" + strSiteIdentifier)) Then
    '        litSearchRow.Visible = False
    '    End If

    'End Sub

    'Protected Sub ucUltimateSearchOutput_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataListItemEventArgs) Handles ucUltimateSearchOutput.ItemDataBound
    '    Dim oTest As DataListItem = e.Item

    '    Dim litSearchRow As Literal = oTest.Controls(0)

    '    If oTest.ItemIndex = -1 Then
    '        litSearchResultInformation = litSearchRow
    '    Else
    '        Dim sSearchRowText As String = litSearchRow.Text
    '        If (sSearchRowText.Contains("://www" + strSiteIdentifier)) Or (sSearchRowText.Contains("://www" + strSiteIdentifier)) Then
    '            'Increment the search counter
    '            intSearchResultCount &= 1
    '        Else

    '            litSearchRow.Visible = False
    '        End If
    '        litSearchResultPager = litSearchRow
    '    End If


    'End Sub

End Class
