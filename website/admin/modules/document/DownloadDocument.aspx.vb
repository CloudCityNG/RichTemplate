Imports System.Data
Imports System.IO

Partial Class admin_modules_document_DownloadDocument
    Inherits System.Web.UI.Page

    Dim ModuleTypeID As Integer = 3 ' Module Type: Document Library

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'Note as this is admin page, we do not need to check user access, like we would do in our front-page vcard.aspx
        'However we DO NEED TO CHECK ADMIN ACCESS
        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If
        End If

        If Not Request.QueryString("id") Is Nothing Then
            Dim intDocumentID As Integer = Convert.ToInt32(Request.QueryString("id"))
            DocumentDAL.DownloadDocument_ByDocumentID(intDocumentID, SiteDAL.GetCurrentSiteID_Admin())

        End If

    End Sub
End Class
