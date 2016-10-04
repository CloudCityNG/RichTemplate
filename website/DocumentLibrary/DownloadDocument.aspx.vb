Imports System.Data
Imports System.IO

Partial Class DocumentLibrary_DownloadDocument
    Inherits System.Web.UI.Page

    Dim ModuleTypeID As Integer = 3

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Load this master page if the current user is logged into this site OR this page DOES NOT require a Logged in Member
        If Not Request.QueryString("id") Is Nothing Then
            Dim intDocumentID As Integer = Convert.ToInt32(Request.QueryString("id"))

            'Check we need to show this document, if its an archived document, be check if we allow archived documents to be viewed
            Dim boolStatus As Boolean = True
            Dim boolAllowArchive As Boolean = False
            If Request.QueryString("archive") = 1 Then
                boolStatus = False
            End If

            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                    boolAllowArchive = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                    boolEnableGroupsAndUserAccess = True
                End If
            Next

            'If we find out the document is an archived document, but we do not allow achives then redirect to default page
            If boolAllowArchive = False And boolStatus = False Then
                Response.Redirect("default.aspx")
            End If

            'Use this to check if the user has access to this document, if no document is returned, then the user does not have access
            Dim dtDocuments As DataTable = If(boolEnableGroupsAndUserAccess, DocumentDAL.GetDocument_ByDocumentIDAndStatusAndAccess_FrontEnd(intDocumentID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), DocumentDAL.GetDocument_ByDocumentIDAndStatus_FrontEnd(intDocumentID, boolStatus, intSiteID))
            If dtDocuments.Rows.Count > 0 Then
                DocumentDAL.DownloadDocument_ByDocumentID(intDocumentID, intSiteID)

            Else
                Response.Redirect("default.aspx")
            End If
        End If

    End Sub
End Class
