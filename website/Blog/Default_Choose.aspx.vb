Imports System.Data
Imports System.Data.SqlClient

Partial Class Blog_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 1
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Blog_FrontEnd.Blog_Default_Heading)
            
        End If

    End Sub

End Class
