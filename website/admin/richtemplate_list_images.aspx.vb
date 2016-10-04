Imports System.Data.SqlClient
Imports System.Data
Imports Telerik.Web.UI
Imports System.Drawing

Partial Class admin_richtemplate_list_images
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Check thes user exists
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        If intAdminUserID > 0 Then

            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
            If dtAdminUser.Rows.Count > 0 Then
                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                'Load Images
                LoadImages()

            Else
                Response.Redirect("~/richadmin/")
            End If
        Else
            Response.Redirect("~/richadmin/")
        End If

    End Sub

    Protected Sub LoadImages()


    End Sub

End Class
