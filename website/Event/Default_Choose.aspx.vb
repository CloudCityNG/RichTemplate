Imports System.Data

Partial Class Event_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 5

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Event_FrontEnd.Event_Default_Heading)
        End If

    End Sub

End Class
