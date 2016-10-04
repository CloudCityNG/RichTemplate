Imports System.Data

Partial Class staff_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 12

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Staff_FrontEnd.Staff_Default_Heading)
        End If

    End Sub


End Class
