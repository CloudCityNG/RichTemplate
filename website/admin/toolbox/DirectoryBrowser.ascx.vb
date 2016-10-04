Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Class admin_DirectoryBrowser
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Call PageStartup()

        If Page.IsPostBack = False Then

            Dim rootNode As New RadTreeNode(rootNodeName)
            rootNode.Value = _rootNodeFolder
            rootNode.ImageUrl = _rootNodeImageUrl
            rootNode.Expanded = True
            rootNode.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
            RadTreeView1.Nodes.Add(rootNode)

        End If

    End Sub
    Private _iconUnknown As String = ""
    Public Property iconUnknown() As String
        Get
            Return _iconUnknown
        End Get
        Set(ByVal value As String)
            _iconUnknown = value
        End Set
    End Property
    Private _iconPath As String = ""
    Public Property iconPath() As String
        Get
            Return _iconPath
        End Get
        Set(ByVal value As String)
            _iconPath = value
        End Set
    End Property
    Private _iconFileType As String = ""
    Public Property iconFileType() As String
        Get
            Return _iconFileType
        End Get
        Set(ByVal value As String)
            _iconFileType = value
        End Set
    End Property
    Private _iconFilePrefix As String = ""
    Public Property iconFilePrefix() As String
        Get
            Return _iconFilePrefix
        End Get
        Set(ByVal value As String)
            _iconFilePrefix = value
        End Set
    End Property

    Private _rootNodeName As String = ""
    Public Property rootNodeName() As String
        Get
            Return _rootNodeName
        End Get
        Set(ByVal value As String)
            _rootNodeName = value
        End Set
    End Property
    Private _rootNodeFolder As String = ""
    Public Property rootNodeFolder() As String
        Get
            Return _rootNodeFolder
        End Get
        Set(ByVal value As String)
            _rootNodeFolder = value
        End Set
    End Property
    Private _rootNodeImageUrl As String = ""
    Public Property rootNodeImageUrl() As String
        Get
            Return _rootNodeImageUrl
        End Get
        Set(ByVal value As String)
            _rootNodeImageUrl = value
        End Set
    End Property
    Private _NodeImageUrl As String = ""
    Public Property NodeImageUrl() As String
        Get
            Return _NodeImageUrl
        End Get
        Set(ByVal value As String)
            _NodeImageUrl = value
        End Set
    End Property
    Private _GoodExtensions As String = ""
    Public Property GoodExtensions() As String
        Get
            Return _GoodExtensions
        End Get
        Set(ByVal value As String)
            _GoodExtensions = value
        End Set
    End Property
    Private _ExcludeDirectories As String = ""
    Public Property ExcludeDirectories() As String
        Get
            Return _ExcludeDirectories
        End Get
        Set(ByVal value As String)
            _ExcludeDirectories = value
        End Set
    End Property

    Public Property FolderBrowseImage() As String
        Get
            Return imgBrowse.ImageUrl
        End Get
        Set(ByVal value As String)
            imgBrowse.ImageUrl = value
        End Set
    End Property
    Public Property iFieldWidth() As Integer
        Get
            Return CType(txtDocument.Width.Value, Integer)
        End Get
        Set(ByVal value As Integer)
            txtDocument.Width = Unit.Pixel(value)
        End Set
    End Property
    Public Property lblTextContent() As String
        Get
            Return lblText.Text
        End Get
        Set(ByVal value As String)
            lblText.Text = value
        End Set
    End Property
    Public Property PanelHeight() As String
        Get
            Return "-1"
        End Get
        Set(ByVal value As String)
            divPanel.Style.Add("height", value)
        End Set
    End Property
    Public Property PanelWidth() As String
        Get
            Return "-1"
        End Get
        Set(ByVal value As String)
            divPanel.Style.Add("width", value)
        End Set
    End Property
    Public Property PanelBackgroundColor() As String
        Get
            Return ""
        End Get
        Set(ByVal value As String)
            divPanel.Style.Add("background-color", value)
        End Set
    End Property

    Public Property TreeViewHeight() As Integer
        Get
            Return -1
        End Get
        Set(ByVal value As Integer)
            RadTreeView1.Height = Unit.Pixel(value)
        End Set
    End Property
    Public Property TreeViewWidth() As Integer
        Get
            Return -1
        End Get
        Set(ByVal value As Integer)
            RadTreeView1.Width = Unit.Pixel(value)
        End Set
    End Property
    Public Property TreeViewBackgroundColor() As String
        Get
            Return ""
        End Get
        Set(ByVal value As String)
            RadTreeView1.Style.Add("background-color", value)
        End Set
    End Property
    Public Property HelpText() As String
        Get
            Return txtHelpText.Text
        End Get
        Set(ByVal value As String)
            txtHelpText.Text = value
        End Set
    End Property


    Private Sub PageStartup()

        If RadTreeView1.SelectedValue <> "" Then
            txtDocument.Text = RadTreeView1.SelectedNode.FullPath
        End If

    End Sub
    'Private ReadOnly KnownExtensions As String() = New String() {GoodExtensions.Replace("'","""")}

    Private Sub BindTreeToDirectory(ByVal virtualPath As String, ByVal parentNode As RadTreeNode)
        Dim physicalPath As String = Server.MapPath(virtualPath)
        Dim directories As String() = Directory.GetDirectories(physicalPath)
        Dim aExcludeDirectories As String() = ExcludeDirectories.Split(",")
        Dim x As Integer
        Dim bDontShow As Boolean
        For Each directory As String In directories
            bDontShow = False
            For x = 0 To aExcludeDirectories.Length - 1
                If directory.IndexOf(aExcludeDirectories(x)) > -1 Then bDontShow = True
            Next
            If bDontShow = False Then
                Dim node As New RadTreeNode(Path.GetFileName(directory))
                node.Value = virtualPath + "/" + Path.GetFileName(directory)
                node.ImageUrl = _rootNodeImageUrl
                node.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
                parentNode.Nodes.Add(node)
            End If
        Next

        Dim files As String() = Directory.GetFiles(physicalPath)
        For Each file As String In files
            Dim node As New RadTreeNode(Path.GetFileName(file))
            Dim extension As String = Path.GetExtension(file).ToLower().TrimStart("."c)
            'GoodExtensions = GoodExtensions.Replace("'", """")
            'Dim KnownExtensions As String() = New String() {GoodExtensions}

            If GoodExtensions.IndexOf(extension) > -1 Then
                'node.ImageUrl = "~/TreeView/Img/Vista/" + extension + ".png"

                node.ImageUrl = iconPath & iconFilePrefix & extension & iconFileType

            Else
                node.ImageUrl = iconUnknown  'NodeImageUrl
            End If

            parentNode.Nodes.Add(node)
        Next

        divPanel.Style.Add("width", "500px")
        RadTreeView1.Style.Add("width", "500px")

    End Sub


    Protected Sub RadTreeView1_NodeExpand(ByVal sender As Object, ByVal e As RadTreeNodeEventArgs)
        BindTreeToDirectory(e.Node.Value, e.Node)
    End Sub

    Private Sub fnClickOK()
        txtDocument.Text = RadTreeView1.SelectedValue
    End Sub

End Class
