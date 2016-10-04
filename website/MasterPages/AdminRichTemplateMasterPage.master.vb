Imports System.Data
Imports System.Data.SqlClient

Partial Class MasterPages_AdminRichTemplateMasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'Check if the user has access
            Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
            If intAdminUserID = 0 Then
                Response.Redirect("/richadmin/")
            End If
        End If

    End Sub

End Class

