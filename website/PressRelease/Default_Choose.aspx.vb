Imports System.Data

Partial Class PressRelease_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 10

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.PressRelease_FrontEnd.PressRelease_Default_Heading)
        End If

    End Sub

End Class
