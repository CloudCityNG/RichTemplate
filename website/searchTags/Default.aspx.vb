Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Partial Class searchTags_Default
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Set the Page Title
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Search_FrontEnd.SearchTags_HeaderTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Search_FrontEnd.SearchTags_SearchBox_Heading)
        End If
    End Sub

End Class
