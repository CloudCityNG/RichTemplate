Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class _SiteNotAvailable
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then
            Page.Header.Title = Resources.SiteNotAvailable_FrontEnd.SiteNotAvailable_HeaderTitle

        End If

    End Sub



End Class
