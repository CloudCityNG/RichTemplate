Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Partial Class admin_modules_document_FolderActions
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 3 ' Module Type: Document Library

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'Set the Help Item if it exists, the Page name is set based on the action below

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            Dim strAction As String = ""
            If Not Request.QueryString("action") Is Nothing Then
                strAction = Request.QueryString("action")

                If strAction.ToLower() = "add" Then
                    ucHeader.PageName = Resources.DocumentLibrary_Admin.Document_FolderActions_Heading_AddFolder
                    litFolderAction.Text = Resources.DocumentLibrary_Admin.Document_FolderActions_BodyHeading_AddFolder

                    editAddDeleteFolder.Text = Resources.DocumentLibrary_Admin.Document_FolderActions_ButtonAdd
                    trFolderName.Visible = True

                ElseIf strAction.ToLower() = "edit" Then
                    ucHeader.PageName = Resources.DocumentLibrary_Admin.Document_FolderActions_Heading_RenameFolder
                    litFolderAction.Text = Resources.DocumentLibrary_Admin.Document_FolderActions_BodyHeading_RenameFolder

                    editAddDeleteFolder.Text = Resources.DocumentLibrary_Admin.Document_FolderActions_ButtonUpdate
                    trFolderName.Visible = True
                ElseIf strAction.ToLower() = "delete" Then
                    ucHeader.PageName = Resources.DocumentLibrary_Admin.Document_FolderActions_Heading_DeleteFolder
                    litFolderAction.Text = Resources.DocumentLibrary_Admin.Document_FolderActions_BodyHeading_DeleteFolder

                    If DirectoryHasFiles() = False Then
                        editAddDeleteFolder.Text = Resources.DocumentLibrary_Admin.Document_FolderActions_ButtonDelete
                    Else
                        editAddDeleteFolder.Visible = False
                        divDeleteErrorMessage.Visible = True
                    End If
                Else
                    Response.Redirect("default.aspx")
                End If

            Else
                Response.Redirect("default.aspx")
            End If

            'If a sub path is not provided then we are adding to the root directory
            Dim strSubPath As String = ""
            lit_FolderAddEditDelete.Text = Resources.DocumentLibrary_Admin.Document_FolderActions_RootFolder & "\<b>*</b>"

            If Not Request.QueryString("subpath") Is Nothing Then
                If Request.QueryString("subpath").Length > 0 Then
                    strSubPath = Request.QueryString("subpath")

                    'If the action is add we include a slash *
                    If strAction.ToLower() = "add" Then
                        lit_FolderAddEditDelete.Text = Resources.DocumentLibrary_Admin.Document_FolderActions_RootFolder & "\" & strSubPath & "\<b>*</b>"
                    ElseIf strAction.ToLower() = "edit" Then
                        Dim strDirectoryName As String = Path.GetFileName(strSubPath)
                        lit_FolderAddEditDelete.Text = Resources.DocumentLibrary_Admin.Document_FolderActions_RootFolder & "\" & strSubPath.Substring(0, strSubPath.LastIndexOf(strDirectoryName)) & "<b>" & strDirectoryName & "<b/>"
                    ElseIf strAction.ToLower() = "delete" Then
                        Dim strDirectoryName As String = Path.GetFileName(strSubPath)
                        lit_FolderAddEditDelete.Text = Resources.DocumentLibrary_Admin.Document_FolderActions_RootFolder & "\" & strSubPath.Substring(0, strSubPath.LastIndexOf(strDirectoryName)) & "<b>" & strDirectoryName & "<b/>"
                    End If
                End If
            End If

            'Note, we dis-allow editting of the root node
            'So if the subpath is the root directory and the action is edit, re-direct them to the default.aspx page
            If (strAction.ToLower() = "edit" Or strAction.ToLower() = "delete") And strSubPath = "" Then
                Response.Redirect("default.aspx")
            End If

        End If
    End Sub

    Protected Function DirectoryHasFiles() As Boolean
        Dim boolDirectoryHasFiles As Boolean = False
        Dim intSiteID As Integer = SiteDAL.GetCurrentSiteID_Admin()
        'Get server path and remove last slash
        Dim strServerPath As String = CommonWeb.GetServerPath()

        Dim DocumentModuleRootDirectory As String = ConfigurationManager.AppSettings("DocumentModuleRootDirectory")
        Dim SiteDirectory As String = "Site_" & intSiteID & "\"
        Dim strSubPath As String = Request.QueryString("subpath")
        Dim action As String = Request.QueryString("action")

        Dim strFilePathFull = strServerPath & DocumentModuleRootDirectory & SiteDirectory & strSubPath

        Dim strDirectoryName_Old As String = Path.GetFileName(strSubPath)
        Dim strFilePathFull_New As String = strFilePathFull.Substring(0, strFilePathFull.LastIndexOf(strDirectoryName_Old)) & Me.folderName.Text
        If Directory.Exists(strFilePathFull) Then
            'Get a list of all documents, and if its filepath is the same as path we want to change ('rp') then update that path to the new path
            Dim dtDocument As DataTable = DocumentDAL.GetDocumentList_BySiteID(intSiteID)
            For Each drDocument As DataRow In dtDocument.Rows
                If Not drDocument("FilePath") Is DBNull.Value Then
                    Dim strFilePath_Current As String = drDocument("FilePath").ToString().Replace("/", "\")
                    strFilePath_Current = strFilePath_Current.Substring(DocumentModuleRootDirectory.Length + SiteDirectory.Length)

                    'we must change the file path of the current level and all child levels
                    If strFilePath_Current.ToLower().StartsWith(strSubPath.ToLower() + "\") Then
                        boolDirectoryHasFiles = True
                    End If
                End If
            Next
        End If

        Return boolDirectoryHasFiles

    End Function

    Private Sub MakeDirectoryIfExists(ByVal NewDirectory As String)
        Try
            ' Check if directory exists
            If Not Directory.Exists(NewDirectory) Then
                ' Create the directory.
                Directory.CreateDirectory(NewDirectory)
            End If
        Catch _ex As IOException
            Response.Write(_ex.Message)
        End Try
    End Sub

    Protected Sub editAddFolder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles editAddDeleteFolder.Click

        'Get server path and remove last slash
        Dim intSiteID As Integer = SiteDAL.GetCurrentSiteID_Admin()
        Dim strServerPath As String = CommonWeb.GetServerPath()
        Dim DocumentModuleRootDirectory As String = ConfigurationManager.AppSettings("DocumentModuleRootDirectory")
        Dim SiteDirectory As String = "Site_" & intSiteID & "\"

        Dim strSubPath As String = Request.QueryString("subpath")
        Dim action As String = Request.QueryString("action")

        Dim strFilePathFull = strServerPath & DocumentModuleRootDirectory & SiteDirectory & strSubPath

        Dim strRelPathNew = ""

        If action = "add" Then
            Dim NewDir As String = strFilePathFull & "\" & Me.folderName.Text

            ' Call function for creating a directory
            MakeDirectoryIfExists(NewDir)
            If strSubPath.Length = 0 Then
                strRelPathNew = "?subpath=" & Me.folderName.Text
            Else
                strRelPathNew = "?subpath=" & strSubPath & "\" & Me.folderName.Text
            End If

        ElseIf action = "edit" Then
            Dim strDirectoryName_Old As String = Path.GetFileName(strSubPath)
            Dim strFilePathFull_New As String = strFilePathFull.Substring(0, strFilePathFull.LastIndexOf(strDirectoryName_Old)) & Me.folderName.Text
            If Directory.Exists(strFilePathFull) Then

                'Get a list of all documents, and if its filepath is the same as path we want to change ('rp') then update that path to the new path
                Dim dtDocument As DataTable = DocumentDAL.GetDocumentList_BySiteID(intSiteID)
                For Each drDocument As DataRow In dtDocument.Rows
                    If Not drDocument("FilePath") Is DBNull.Value Then
                        Dim strFilePath_Current As String = drDocument("FilePath").ToString().Replace("/", "\")
                        strFilePath_Current = strFilePath_Current.Substring(DocumentModuleRootDirectory.Length + SiteDirectory.Length)

                        'we must change the file path of the current level and all child levels
                        If strFilePath_Current.ToLower().StartsWith(strSubPath.ToLower() + "\") Then
                            'The Document has a file path that we want to change to the new path
                            strFilePath_Current = strServerPath & DocumentModuleRootDirectory & SiteDirectory & strFilePath_Current
                            Dim newPath_ThisFile As String = strFilePath_Current.Replace(strFilePathFull, strFilePathFull_New)
                            newPath_ThisFile = newPath_ThisFile.Substring(strServerPath.Length).Replace("\", "/")
                            Dim intDocumentID As Integer = Convert.ToInt32(drDocument("documentID"))
                            DocumentDAL.UpdateDocument_FilePath_ByDocumentID(intDocumentID, newPath_ThisFile)
                        End If
                    End If
                Next

                FileIO.FileSystem.RenameDirectory(strFilePathFull, Me.folderName.Text)
                strRelPathNew = "?subpath=" & strFilePathFull_New.Substring(strServerPath.Length + DocumentModuleRootDirectory.Length + SiteDirectory.Length)

            End If
        ElseIf action = "delete" Then
            Dim strDirectoryName_Old As String = Path.GetFileName(strSubPath)
            Dim strFilePathFull_New As String = strFilePathFull.Substring(0, strFilePathFull.LastIndexOf(strDirectoryName_Old)) & Me.folderName.Text
            If Directory.Exists(strFilePathFull) Then
                Directory.Delete(strFilePathFull, True)
            End If
        End If
        Response.Redirect("/admin/modules/Document/folderAdmin.aspx" & strRelPathNew)

    End Sub

    Protected Sub Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cancel.Click

        Dim strSubPath As String = ""

        If Not Request.QueryString("subpath") Is Nothing Then
            If Request.QueryString("subpath").Length > 0 Then
                strSubPath = Request.QueryString("subpath")
            End If
        End If

        Response.Redirect("folderAdmin.aspx?subpath=" & strSubPath)

    End Sub
End Class
