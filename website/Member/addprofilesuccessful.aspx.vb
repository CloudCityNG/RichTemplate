Imports System.Data
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports Telerik.Web.UI
Imports System.Xml

Partial Class Member_AddProfileSuccessful
    Inherits RichTemplateLanguagePage

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        'THIS SITE USES DOES NOT ALLOW CREATING MEMBERS, so if they reached this page, they got here illegally, redirect them to the sitewide login page
        Dim strSiteWideLoginURL As String = ConfigurationManager.AppSettings("SiteWideLoginURL").ToString()
        Response.Redirect(strSiteWideLoginURL)

        If Not Page.IsPostBack Then
            Page.Header.Title = SiteDAL.GetCompanyName() & " | " & Resources.Member_FrontEnd.Member_AddProfileSuccessful_HeaderTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Member_FrontEnd.Member_AddProfileSuccessful_ProfileCreated_Heading)
        End If

    End Sub

End Class
