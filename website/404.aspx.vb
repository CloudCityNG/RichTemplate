
Partial Class _404
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Page.Header.Title = Resources.Error_FrontEnd.PageNotFound_HeaderTitle
    End Sub

End Class
