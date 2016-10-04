Imports System.Data

Partial Class Faq_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 6

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Faq_FrontEnd.Faq_Default_Heading)
        End If

    End Sub

End Class
