Imports System.Data

Partial Class Employment_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 4

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Employment_FrontEnd.Employment_Default_Heading)
        End If

    End Sub
End Class
