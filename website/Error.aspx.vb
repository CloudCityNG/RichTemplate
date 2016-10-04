Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class _Error
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        If Not Request.QueryString("throwerror") Is Nothing Then
            Throw New Exception(Resources.Error_FrontEnd.Error_ManualExceptionThrownMessage)
        End If
        If Not Page.IsPostBack Then
            Page.Header.Title = Resources.Error_FrontEnd.Error_HeaderTitle

        End If

    End Sub

  

End Class
