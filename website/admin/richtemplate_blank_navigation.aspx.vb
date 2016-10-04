Imports System.Data.SqlClient
Imports System.Data
Imports Telerik.Web.UI
Imports System.Drawing

Partial Class admin_richtemplate_blank_navigation
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Check the user exists
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        If intAdminUserID = 0 Then
            Response.Redirect("~/richadmin/")
        End If
    End Sub
End Class
