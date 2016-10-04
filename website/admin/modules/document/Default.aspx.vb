Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Telerik.Web.UI
Partial Class admin_modules_document_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 3 ' Module Type: Document Library

    Dim strDocumentModuleRootDirectory As String = ""
    Dim strSiteDirectory As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim intSiteID As Integer = SiteDAL.GetCurrentSiteID_Admin()

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, intSiteID) Then
                Response.Redirect("/richadmin")
            End If
        End If

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteLive, Resources.DocumentLibrary_Admin.Document_Default_GridDeleteButton_ConfirmationMessage)
        CommonWeb.SetupDeleteButton(btnDeleteArchive, Resources.DocumentLibrary_Admin.Document_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgDocuments, "{4} {5} " & Resources.DocumentLibrary_Admin.Document_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.DocumentLibrary_Admin.Document_Default_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadGrid(rgDocumentsArchive, "{4} {5} " & Resources.DocumentLibrary_Admin.Document_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.DocumentLibrary_Admin.Document_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.DocumentLibrary_Admin.Document_Default_Header

        strDocumentModuleRootDirectory = ConfigurationManager.AppSettings("DocumentModuleRootDirectory")
        strSiteDirectory = "Site_" & intSiteID.ToString() + "\"

        'Check we need to show the book this link, but only if its active
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" AndAlso AdminUserDAL.CheckAdminUserModuleAccess(2, intSiteID) Then
                'show the commentCount column
                Dim gcCommentCount As GridColumn = rgDocuments.Columns.FindByUniqueName("comments")
                gcCommentCount.Visible = True

                Dim gcCommentCountArchive As GridColumn = rgDocumentsArchive.Columns.FindByUniqueName("comments")
                gcCommentCountArchive.Visible = True
            End If
        Next

    End Sub

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim rootFolder As String = CommonWeb.GetServerPath()
        For Each grdItem As GridDataItem In rgDocuments.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("documentID")

                'First we must get the documents filename and path, then use this to delete it from the File System
                Dim dtDocument As DataTable = DocumentDAL.GetDocument_ByDocumentID(intRecordId)
                If dtDocument.Rows.Count > 0 Then
                    Dim drDocument As DataRow = dtDocument.Rows(0)

                    Dim strRelPath As String = rootFolder & drDocument("filePath").ToString().Replace("/", "\")
                    Dim fileName_old As String = drDocument("FileName")
                    
                    'now we can delete it
                    Dim FilePathComplete_Old As String = strRelPath & fileName_old

                    If File.Exists(FilePathComplete_Old) Then
                        File.Delete(FilePathComplete_Old)
                    End If
                End If

                'Then Remove this from the DB, and Remove its assoicated SearchTags
                DocumentDAL.DeleteDocument_ByDocumentID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        rgDocuments.Rebind()
    End Sub

    Protected Sub btnDeleteArchive_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim rootFolder As String = CommonWeb.GetServerPath()
        For Each grdItem As GridDataItem In rgDocumentsArchive.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("documentID")

                'First we must get the documents filename and path, then use this to delete it from the File System
                Dim dtDocument As DataTable = DocumentDAL.GetDocument_ByDocumentID(intRecordId)
                If dtDocument.Rows.Count > 0 Then
                    Dim drDocument As DataRow = dtDocument.Rows(0)

                    Dim strRelPath As String = rootFolder & drDocument("filePath").ToString().Replace("/", "\")
                    Dim fileName_old As String = drDocument("FileName")

                    'now we can delete it
                    Dim FilePathComplete_Old As String = strRelPath & fileName_old

                    If File.Exists(FilePathComplete_Old) Then
                        File.Delete(FilePathComplete_Old)
                    End If
                End If

                'Then Remove this from the DB, and Remove its assoicated SearchTags
                DocumentDAL.DeleteDocument_ByDocumentID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        rgDocumentsArchive.Rebind()
    End Sub

    Public Sub rgDocuments_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgDocuments.NeedDataSource

        Dim dtDocument As DataTable = DocumentDAL.GetDocumentList_ByStatusAndSiteID(True, SiteDAL.GetCurrentSiteID_Admin())
        rgDocuments.DataSource = dtDocument
    End Sub

    Public Sub rgDocumentsArchive_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgDocumentsArchive.NeedDataSource

        Dim dtDocument As DataTable = DocumentDAL.GetDocumentList_ByStatusAndSiteID(False, SiteDAL.GetCurrentSiteID_Admin())
        rgDocumentsArchive.DataSource = dtDocument
    End Sub

    Protected Sub rgDocuments_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgDocuments.ItemDataBound, rgDocumentsArchive.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvDocument As DataRowView = item.DataItem
            Dim drDocument As DataRow = drvDocument.Row

            Dim intDocumentID As Integer = Convert.ToInt32(drDocument("documentID"))

            'Update comment text
            Dim intCommentCountApproved As Integer = Convert.ToInt32(drDocument("commentCountApproved"))
            Dim intCommentCountPending As Integer = Convert.ToInt32(drDocument("commentCountPending"))

            Dim sbCommentText As New StringBuilder()
            sbCommentText.Append(If(intCommentCountApproved > 0, "<span class='commentTextApproved'>" & intCommentCountApproved.ToString() & "</span>", ""))
            If (intCommentCountApproved > 0) AndAlso (intCommentCountPending > 0) Then
                sbCommentText.Append(" / ")
            End If
            sbCommentText.Append(If(intCommentCountPending > 0, "<span class='commentTextPending'>" & intCommentCountPending.ToString() & "</span>", ""))
            Dim strCommentText As String = sbCommentText.ToString()

            If strCommentText.Length > 0 Then
                Dim hLinkComments As HyperLink = DirectCast(item("comments").Controls(0), HyperLink)
                hLinkComments.NavigateUrl = "/admin/modules/Comments/defaultModule.aspx?mtID=" & ModuleTypeID & "&recordID=" & intDocumentID
                hLinkComments.Text = Resources.PressRelease_Admin.PressRelease_Default_GridComments & " (" & strCommentText & ")"
            End If

            'Before we show this linkbutton to view comments, we check if this document record, was created for a different site, in which case its a read-only document record, and you can not view comments from this site
            Dim intSiteID As Integer = Convert.ToInt32(drDocument("SiteID"))
            If Not intSiteID = SiteDAL.GetCurrentSiteID_Admin() Then
                Dim hLinkComments As HyperLink = DirectCast(item("comments").Controls(0), HyperLink)
                hLinkComments.Visible = False
            End If

            'Construct the FilePathAndName Literal
            Dim litFilePathAndName As Literal = DirectCast(e.Item.FindControl("litFilePathAndName"), Literal)

            Dim strFilePath As String = drDocument("FilePath").ToString().Replace("/", "\")
            Dim strFileName As String = drDocument("FileName").ToString
            Dim strFriendlyFileName As String = drDocument("FriendlyFileName").ToString

            Dim strRelFilePath = ""
            Dim intRelFilePathIndex As Integer = strFilePath.IndexOf(strDocumentModuleRootDirectory)
            If intRelFilePathIndex >= 0 Then
                intRelFilePathIndex = intRelFilePathIndex + strDocumentModuleRootDirectory.Length + strSiteDirectory.Length
                strRelFilePath = strFilePath.Substring(intRelFilePathIndex)
            End If

            Dim strFilePathAndName As String = "\\" & strRelFilePath & "<b>" & strFriendlyFileName & "</b>"
            litFilePathAndName.Text = strFilePathAndName

            'Construct the fileType Image
            Dim imgFileType As HtmlImage = DirectCast(e.Item.FindControl("imgFileType"), HtmlImage)
            imgFileType.Alt = strFriendlyFileName
            imgFileType.Src = CommonWeb.GetFileTypeImage_ByFilePath(strFileName)

            'setup Edit link
            Dim aDocumentEdit As HtmlAnchor = DirectCast(e.Item.FindControl("aDocumentEdit"), HtmlAnchor)
            aDocumentEdit.HRef = "editAdd.aspx?ID=" + intDocumentID.ToString

        End If
    End Sub

End Class
