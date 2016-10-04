Imports System
Imports System.IO
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Telerik.Web.UI

Partial Class admin_modules_document_FolderAdmin
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 3 ' Module Type: Document Library

    Dim ShowDirectoriesThenFiles As Boolean = False

    Dim strServerPath As String
    Dim strRelPath As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.DocumentLibrary_Admin.Document_FolderAdmin_Heading

        strServerPath = CommonWeb.GetServerPath()

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            'Set the relative path, such that we can have the most recently affected directory highlighted to show its change
            If Not Request.QueryString("subpath") Is Nothing Then
                strRelPath = Request.QueryString("subpath").ToLower()
            End If

            BindSubNodesToDirectory()
        End If
    End Sub 'Page_Load
    Protected Overrides Sub OnInit(ByVal e As EventArgs)
        '
        ' CODEGEN: This call is required by the ASP.NET Web Form Designer.
        '
        InitializeComponent()
        MyBase.OnInit(e)
    End Sub 'OnInit


    '/        Required method for Designer support - do not modify
    '/        the contents of this method with the code editor.
    '/ </summary>
    Private Sub InitializeComponent()
    End Sub 'InitializeComponent

    Private Function CreateFolderRadTreeNode(ByVal nodeText As String, ByVal nodeValue As String, ByVal nodeExpanded As Boolean) As RadTreeNode
        Dim node As New RadTreeNode()

        node.Category = "Folder"
        node.Expanded = nodeExpanded
        node.Text = "<img src='/images/open_folder_full.png' class='rtImg' alt='Folder'><span class='inner_rtIn'>" & nodeText & "</span>"
        node.Value = nodeValue

        Return node
    End Function

    Private Function CreateFileRadTreeNode(ByVal nodeText As String, ByVal nodeValue As String, ByVal nodeImageURL As String, ByVal nodeNavigateURL As String) As RadTreeNode
        Dim node As New RadTreeNode()

        node.Category = "File"
        node.Text = nodeText
        node.Value = nodeValue
        node.NavigateUrl = nodeNavigateURL

        Return node
    End Function

    Private Function GetDocumentsList() As Hashtable
        'Check DB for record
        Dim intSiteID As Integer = SiteDAL.GetCurrentSiteID_Admin()
        Dim hashDocuments As New Hashtable()
        Dim dtDocuments As DataTable = DocumentDAL.GetDocumentList_ByStatusAndSiteID(True, intSiteID)

        For Each drDocument As DataRow In dtDocuments.Rows
            'Get File Path and Name
            Dim strFilePath As String = drDocument("filePath").ToString().Replace("/", "\")
            Dim strFileName As String = drDocument("fileName")

            'Get FileInfo
            Dim strFileFullPath As String = (strServerPath & strFilePath & strFileName).ToLower()

            If hashDocuments(strFileFullPath) Is Nothing Then
                hashDocuments.Add(strFileFullPath, drDocument)
            End If

        Next

        Return hashDocuments
    End Function

    Private Sub BindSubNodesToDirectory()
        Dim intSiteID As Integer = SiteDAL.GetCurrentSiteID_Admin()
        Dim rootFolder As String = strServerPath & ConfigurationManager.AppSettings("DocumentModuleRootDirectory")

        Dim hashDocuments As Hashtable = GetDocumentsList()
        Dim hashNodes As New Hashtable()

        'First bind the root node
        Dim strNodeText As String = "<span id='repRoot'>" & Resources.DocumentLibrary_Admin._SiteWide_RichTemplate_List_Modules_ModuleName & "</span>&nbsp;&nbsp|&nbsp;&nbsp<a href='editAdd.aspx?subpath='>" & Resources.DocumentLibrary_Admin.Document_FolderAdmin_UploadHere & "</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href='folderActions.aspx?action=add&subpath='>" & Resources.DocumentLibrary_Admin.Document_FolderAdmin_AddFolder & "</a>"
        Dim rootNode As RadTreeNode = CreateFolderRadTreeNode(strNodeText, "", True)
        rtvDocuments.Nodes.Add(rootNode)
        hashNodes.Add("", rootNode)

        'Before we get the files, we ensure the root directory exists, if not we create a root directory
        If Not Directory.Exists(rootFolder) Then
            Directory.CreateDirectory(rootFolder)
        End If
        'We also must ensure the Site Specific Directory Directory Exists
        Dim rootFolderSiteSpecific As String = rootFolder & "Site_" & intSiteID.ToString() & "\"
        If Not Directory.Exists(rootFolderSiteSpecific) Then
            Directory.CreateDirectory(rootFolderSiteSpecific)
        End If

        If ShowDirectoriesThenFiles = False Then
            'Now we add files to this node
            BindFilesToDirectory(rootFolderSiteSpecific, hashDocuments, rootNode)
        End If

        'Get all directories and sub-directories
        Dim listDirectories As String() = Directory.GetDirectories(rootFolderSiteSpecific, "*.*", SearchOption.AllDirectories)

        For i As Integer = 0 To listDirectories.Count - 1
            Dim indDirectoryFullPath As String = listDirectories(i)
            Dim indDirectorySubPath As String = indDirectoryFullPath.Substring(rootFolderSiteSpecific.Length)
            Dim indDirectoryName As String = indDirectorySubPath
            If indDirectoryName.IndexOf("\") >= 0 Then
                indDirectoryName = indDirectorySubPath.Substring(indDirectorySubPath.LastIndexOf("\") + 1)
            End If

            Dim indDirectoryParent As String = ""
            If indDirectorySubPath.IndexOf("\") >= 0 Then
                indDirectoryParent = indDirectorySubPath.Substring(0, indDirectorySubPath.LastIndexOf("\"))
            End If

            Dim parentRadTreeNode As RadTreeNode = hashNodes(indDirectoryParent)

            'Note we ignore '_vti_cnf' directories
            If indDirectoryName.ToLower() <> "_vti_cnf" Then

                'Create the new child node
                strNodeText = indDirectoryName & "&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;<a href='editAdd.aspx?subpath=" & indDirectorySubPath & "'>" & Resources.DocumentLibrary_Admin.Document_FolderAdmin_UploadHere & "</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href='folderActions.aspx?action=add&subpath=" & indDirectorySubPath & "'>" & Resources.DocumentLibrary_Admin.Document_FolderAdmin_AddFolder & "</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href='folderActions.aspx?action=edit&subpath=" & indDirectorySubPath & "'>" & Resources.DocumentLibrary_Admin.Document_FolderAdmin_RenameFolder & "</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href='folderActions.aspx?action=delete&subpath=" & indDirectorySubPath & "'>" & Resources.DocumentLibrary_Admin.Document_FolderAdmin_DeleteFolder & "</a>"
                Dim subNode As RadTreeNode = CreateFolderRadTreeNode(strNodeText, indDirectorySubPath, False)
                parentRadTreeNode.Nodes.Add(subNode)

                If indDirectorySubPath.ToLower() = strRelPath Then
                    subNode.Selected = True
                End If
                'Must expand this node if this relative path appears in the selected relative path, as this folder must therefore be a parent
                If strRelPath.StartsWith(indDirectorySubPath.ToLower()) Then
                    subNode.Expanded = True
                End If

                hashNodes.Add(indDirectorySubPath, subNode)

                If ShowDirectoriesThenFiles = False Then
                    'Now we add files to this node
                    BindFilesToDirectory(indDirectoryFullPath, hashDocuments, subNode)
                End If

            End If
        Next

        'Now Bind all Files
        If ShowDirectoriesThenFiles Then
            'Then load files for this root node
            BindFilesToDirectory(rootFolderSiteSpecific, hashDocuments, rootNode)
            'Then bind all files inside the child directory
            For i As Integer = 0 To listDirectories.Count - 1
                Dim indDirectoryFullPath As String = listDirectories(i)
                Dim indDirectorySubPath As String = indDirectoryFullPath.Substring(rootFolderSiteSpecific.Length)
                Dim subNode As RadTreeNode = hashNodes(indDirectorySubPath)
                BindFilesToDirectory(indDirectoryFullPath, hashDocuments, subNode)
            Next
        End If
    End Sub 'BindTreeToDirectory

    Private Sub BindFilesToDirectory(ByVal indDirectoryFullPath As String, ByVal hashDocuments As Hashtable, ByVal subNode As RadTreeNode)
        Dim listFiles As String() = Directory.GetFiles(indDirectoryFullPath, "*.*", SearchOption.TopDirectoryOnly)
        For j As Integer = 0 To listFiles.Count - 1
            Dim indFileFullPath As String = listFiles(j)

            'Now check our module documents to see if this file exists in our database
            If Not hashDocuments(indFileFullPath.ToLower()) Is Nothing Then

                Dim drDocument As DataRow = hashDocuments(indFileFullPath.ToLower())
                Dim intDocumentID As String = drDocument("documentID")

                Dim indFileName As String = indFileFullPath.Substring(indFileFullPath.LastIndexOf("\") + 1)

                'File Upload Date
                Dim strPublicationDate As String = drDocument("fileUploadDate").ToString()
                If Not drDocument("PublicationDate") Is DBNull.Value Then
                    strPublicationDate = drDocument("publicationDate").ToString()
                End If
                Dim indFileUploadDate As String = Resources.DocumentLibrary_Admin.Document_FolderAdmin_PublishedDate & ": " & FormatDateTime(strPublicationDate, DateFormat.ShortDate) & ""
                If Not drDocument("ExpirationDate") Is DBNull.Value Then
                    indFileUploadDate = indFileUploadDate & " to " & FormatDateTime(drDocument("ExpirationDate").ToString(), DateFormat.ShortDate)
                End If

                'File Title and Description
                Dim indFileTitle As String = drDocument("fileTitle").ToString()
                If indFileTitle = "" Then
                    indFileTitle = indFileName
                End If
                Dim indFileDescription As String = Resources.DocumentLibrary_Admin.Document_FolderAdmin_Description & ": " & drDocument("fileDescription").ToString()

                'Get the File Size and FileTypeImage
                Dim strFileSize As String = CommonWeb.GetFileSize_ByFilepath(indFileFullPath)
                Dim strFileImageURL As String = CommonWeb.GetFileTypeImage_ByFilePath(indFileFullPath)

                'Finally get the node name
                Dim indFileWebPath = indFileFullPath.Replace(strServerPath, "").Replace("\", "/")
                Dim indFileNodeName As String = "<img src='" & strFileImageURL & "' class='rtImg' alt='" & indFileTitle & "'><span class='inner_rtIn'><span class='fileTitle'>" & indFileTitle & "</span></a><span class='fileSize'>(" & strFileSize & ")" & "</span>" & "</span>&nbsp; &nbsp;<a id='" & intDocumentID.ToString() & "' href='DownloadDocument.aspx?id=" + intDocumentID + "' class='save' target='_blank'><img src='/images/save.png' /></a>" & "<div class='leftPad30Desc'>" & indFileUploadDate & "<a id='" & intDocumentID.ToString() & "'></a><br/>" & indFileDescription & "</div><a>"

                'Add this node
                Dim fileRadTreeNode As RadTreeNode = CreateFileRadTreeNode(indFileNodeName, intDocumentID, strFileImageURL, "editAdd.aspx?id=" & intDocumentID.ToString())
                subNode.Nodes.Add(fileRadTreeNode)

            End If
        Next
    End Sub

End Class