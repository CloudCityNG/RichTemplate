
Partial Class admin_Default
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Check the user exists
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        If intAdminUserID = 0 Then
            Response.Redirect("~/richadmin/")
        End If
    End Sub

End Class
