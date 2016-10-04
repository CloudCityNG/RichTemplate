Imports System
Imports System.IO
Imports System.Collections
Imports System.Collections.Specialized
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

Partial Class document_DocumentTree
    Inherits RichTemplateLanguagePage

    Dim boolAllowArchive As Boolean = False
    Dim boolEnableGroupsAndUserAccess As Boolean = False
    Dim ShowDirectoriesThenFiles As Boolean = False

    Dim strServerPath As String
    Dim strSelectedNodeId As String = ""

    Public ModuleTypeID As Integer = 3

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        strServerPath = CommonWeb.GetServerPath()

        If Not Page.IsPostBack Then

            'Set the Page Title
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentTree_HeaderTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentTree_Heading)

            'Get module information, to check if we will show its introduction
            Dim dtModule As DataTable = ModuleDAL.GetModule_ByModuleTypeIDAndSiteID_FrontEnd(ModuleTypeID, intSiteID)
            If dtModule.Rows.Count > 0 Then
                Dim drModule As DataRow = dtModule.Rows(0)
                If Not drModule("ModuleContentHTML") Is DBNull.Value Then
                    Dim strModuleContentHTML As String = drModule("ModuleContentHTML")

                    litModuleDynamicContent.Text = strModuleContentHTML
                    divModuleDynamicContent.Visible = True
                End If
            Else
                'We do not have an Active Document Module For the Front-End, so redirect to the homepage
                Response.Redirect("/")
            End If

            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                    boolAllowArchive = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_submissions" Then
                    If intMemberID > 0 Then
                        divAddDocument.Visible = True
                    End If
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                    boolEnableGroupsAndUserAccess = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_rss" Then
                    divModuleHeadingRssFeed.Visible = True
                End If
            Next

            If (Not boolAllowArchive) And (Request.QueryString("archive") = 1) Then
                ' Code to hide archive if we do not allow archive viewing
                rtvDocuments.Visible = False
            End If


            If Not Request.Params("ID") Is Nothing Then
                strSelectedNodeId = Request.Params("ID")
            End If

            BindSubNodesToDirectory()
        End If

    End Sub 'Page_Load

    Private Function CreateFolderRadTreeNode(ByVal nodeText As String, ByVal nodeValue As String, ByVal nodeExpanded As Boolean) As RadTreeNode
        Dim node As New RadTreeNode(nodeText, nodeValue)
        node.Category = "Folder"
        node.Expanded = nodeExpanded
        node.Text = "<img src='/images/open_folder_full.png' class='rtImg' alt='" & nodeText & "'><span class='inner_rtIn'>" & nodeText & "</span>"

        'If we have group and user permissions enabled, then we first ensure all directories are hidden, this is so the user will not see directories such as Staff Salaries, if the user is not allowed to see any documents inside this directory
        ' if there is a file in the directory that the user has access then we set the node an all its parents to visible = true
        If boolEnableGroupsAndUserAccess Then
            node.Visible = False
        End If
        Return node
    End Function

    Private Function CreateFileRadTreeNode(ByVal nodeParent As RadTreeNode, ByVal nodeText As String, ByVal nodeValue As String, ByVal nodeNavigateURL As String) As RadTreeNode
        Dim node As New RadTreeNode(nodeText, nodeValue)

        node.Value = nodeValue
        node.Category = "File"
        node.Text = nodeText
        node.NavigateUrl = nodeNavigateURL

        'If we have group and user permissions enabled, then this user can see this file, in which case they should beable to see this directory and its parent directories
        If boolEnableGroupsAndUserAccess Then
            Dim currentNode As RadTreeNode = nodeParent
            While (Not currentNode Is Nothing) AndAlso (Not currentNode.Visible) ' Note if the parent node is already visible, then we don't need to move up to the parent as it has already been made visible all the way to root from another file node
                currentNode.Visible = True
                currentNode = currentNode.ParentNode
            End While
        End If

        'If this file node is the same as the requested node, then expand the parent
        If strSelectedNodeId = nodeValue Then
            'set this node to expanded and also do all parent nodes
            node.Selected = True

            Dim currentNode As RadTreeNode = nodeParent
            While Not currentNode Is Nothing
                currentNode.Expanded = True
                currentNode = currentNode.ParentNode
            End While

        End If

        'Finally add the node to the parent
        nodeParent.Nodes.Add(node)

        Return node
    End Function

    Private Function GetDocumentsList() As Hashtable
        'Check DB for record
        Dim hashDocuments As New Hashtable()

        Dim boolStatus As Boolean = True
        If Request.QueryString("archive") = "1" Then
            boolStatus = False
        End If

        Dim dtDocuments As DataTable = If(boolEnableGroupsAndUserAccess, DocumentDAL.GetDocumentList_ByStatusAndAccess_FrontEnd(boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), DocumentDAL.GetDocumentList_ByStatus_FrontEnd(boolStatus, intSiteID))

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
        Dim rootFolder As String = strServerPath & ConfigurationManager.AppSettings("DocumentModuleRootDirectory")


        Dim hashDocuments As Hashtable = GetDocumentsList()
        Dim hashNodes As New Hashtable()

        'First bind the root node
        Dim rootNode As RadTreeNode = CreateFolderRadTreeNode(Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentTree_RootFolder, "", True)
        rtvDocuments.Nodes.Add(rootNode)
        hashNodes.Add("", rootNode)
        If ShowDirectoriesThenFiles = False Then
            'Now we add files to this node
            BindFilesToDirectory(rootFolder, hashDocuments, rootNode)
        End If

        'Get all directories and sub-directories
        'We also must ensure the Site Specific Directory Directory Exists
        Dim rootFolderSiteSpecific As String = rootFolder & "Site_" & intSiteID.ToString() & "\"

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

            ' Dim parentRadTreeNode As RadTreeNode = RadTree1.FindNodeByValue(indDirectoryParent)
            Dim parentRadTreeNode As RadTreeNode = hashNodes(indDirectoryParent)

            'Note we ignore '_vti_cnf' directories
            If indDirectoryName.ToLower() <> "_vti_cnf" Then

                'Create the new child node
                Dim subNode As RadTreeNode = CreateFolderRadTreeNode(indDirectoryName, indDirectorySubPath, False)
                parentRadTreeNode.Nodes.Add(subNode)
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
                Dim indFileUploadDate As String = Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentTree_Document_PublishDate & ": " & FormatDateTime(strPublicationDate, DateFormat.ShortDate)

                If Not drDocument("ExpirationDate") Is DBNull.Value Then
                    indFileUploadDate = indFileUploadDate & " " & Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentTree_Document_PublishExpirationSeperator & " " & FormatDateTime(drDocument("ExpirationDate").ToString(), DateFormat.ShortDate)
                End If

                'File Title and Description
                Dim indFileTitle As String = drDocument("fileTitle").ToString()
                If indFileTitle = "" Then
                    indFileTitle = indFileName
                End If
                Dim indFileDescription As String = Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentTree_Document_Description & ": " & drDocument("fileDescription").ToString()

 

                'Finally get the node name and its webpath
                Dim indFileWebPath = indFileFullPath.Replace(strServerPath, "").Replace("\", "/")

                Dim indFileNodeName As String = "<img src='" & CommonWeb.GetFileTypeImage_ByFilePath(indFileFullPath) & "' class='rtImg' alt='" & indFileTitle & "'><span class='inner_rtIn'><span class='fileTitle'>" & indFileTitle & "</span></a><span class='fileSize'>(" & CommonWeb.GetFileSize_ByFilepath(indFileFullPath) & ")" & "</span>&nbsp; &nbsp;<a href='downloadDocument.aspx?id=" & intDocumentID & If(Request.Params("archive") = 1, "&archive=1", "") & "' class='save' target='_blank'><img src='/images/save.png' /></a>" & "<div class='leftPad30Desc'>" & indFileUploadDate & "<br/>" & indFileDescription & "</div><a class='dNone'></span>"

                'Add this node
                CreateFileRadTreeNode(subNode, indFileNodeName, intDocumentID, "documentDetail.aspx?id=" & intDocumentID.ToString() & If(Request.Params("archive") = 1, "&archive=1", ""))


            End If
        Next
    End Sub

End Class
