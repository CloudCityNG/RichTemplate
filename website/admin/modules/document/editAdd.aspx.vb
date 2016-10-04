Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Class admin_modules_document_editAdd
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Dim ModuleTypeID As Integer = 3 ' Module Type: Document Library

    'Get server path and remove last slash
    Dim strServerPath As String = ""
    Dim strDocumentModuleRootDirectory As String = ""

    Public Sub BindGroupCheckBoxListData()

        Dim dtGroups As DataTable = GroupDAL.GetGroupList_BySiteID(intSiteID)

        cblGroupList.Items.Add(New ListItem(Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_UserGroups_GroupsUnAuthenticated & "<br/><span class='graySubText'>" & Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_UserGroups_GroupsUnAuthenticatedDescription & "</span>", "-1"))
        cblGroupList.Items.Add(New ListItem(Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_UserGroups_GroupsAuthenticated & "<br/><span class='graySubText'>" & Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_UserGroups_GroupsAuthenticatedDescription & "</span>", "0"))


        For Each drGroup As DataRow In dtGroups.Rows
            Dim intGroupID As String = drGroup("groupID").ToString()
            Dim strGroupName As String = (drGroup("groupName").ToString() & "<br/><span class='graySubText'>") & drGroup("groupDescription").ToString() & "</span>"
            cblGroupList.Items.Add(New ListItem(strGroupName, intGroupID))
        Next

    End Sub

    Public Sub BindUserCheckBoxListData()
        Dim dtMembers As DataTable = MemberDAL.GetMemberList_BySiteID(intSiteID)
        For Each drMember In dtMembers.Rows()
            Dim intMemberID As String = drMember("id").ToString()

            Dim strSalutation_LangaugeSpecific As String = String.Empty
            If Not drMember("Salutation_LanguageProperty") Is DBNull.Value Then
                Dim strSalutation_LanguageProperty As String = drMember("Salutation_LanguageProperty")
                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
            End If

            Dim strMemberName As String = CommonWeb.FormatName(strSalutation_LangaugeSpecific, drMember("firstName").ToString(), drMember("lastName"))
            cblMemberList.Items.Add(New ListItem(strMemberName, intMemberID))
        Next

    End Sub

    Public Sub BindSearchTagsCheckBoxListData()
        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsList_BySiteID(intSiteID)

        For Each drSearchTag As DataRow In dtSearchTags.Rows()
            Dim uid As String = drSearchTag("searchTagID").ToString()
            Dim uname As String = (drSearchTag("searchTagName").ToString() & "<br/><span class='graySubText'>") + drSearchTag("searchTagDescription").ToString() & "</span>"
            cblSearchTags.Items.Add(New ListItem(uname, uid))
        Next
    End Sub

    Public Sub BindCategoryDropDownListData()
        'Here we bind the dropdown list to categories
        Dim dtCategory As DataTable = CategoryDAL.GetCategoryList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)

        With rcbCategoryID
            .DataSource = dtCategory
            .DataValueField = "categoryID"
            .DataTextField = "categoryName"

        End With
        rcbCategoryID.DataBind()

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Setup Rad Controls
        CommonWeb.SetupRadDatePicker(publicationDate, intSiteID)
        CommonWeb.SetupRadDatePicker(expirationDate, intSiteID)
        CommonWeb.SetupRadEditor(Page, txtDescription, SiteDAL.GetCurrentSiteID_Admin)
        CommonWeb.SetupRadProgressArea(radProgressAreaDocument, intSiteID)
        CommonWeb.SetupRadUpload(ruDocument, intSiteID)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.DocumentLibrary_Admin.Document_AddEdit_Header

        'If there are more than 1 site, then show the site RadioButtonList, so the AdminUser can associate this document to just this site or All Sites, we default this to THIS SITE ONLY
        trSiteAccess.Visible = SiteDAL.GetSiteList().Rows.Count > 1 ' Only show if there is more than one site

        strDocumentModuleRootDirectory = ConfigurationManager.AppSettings("DocumentModuleRootDirectory")
        strServerPath = CommonWeb.GetServerPath()

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, intSiteID) Then
                Response.Redirect("/richadmin")
            End If

            BindUserCheckBoxListData()
            BindGroupCheckBoxListData()
            BindSearchTagsCheckBoxListData()
            BindCategoryDropDownListData()

            'Check we need to include document library groups and user permissions
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then

                    rtsDocument.Tabs.FindTabByValue("3").Visible = True ' Show the Groups & Users Tab, then bind both the group list and member list

                End If
            Next

            'Check if this AdminUser has access to Create/Update/Delete SearchTags where SearchTags ModuleTypeID = 11
            divAddSearchTags.Visible = AdminUserDAL.CheckAdminUserModuleAccess(11, intSiteID)

            If Not Request.QueryString("ID") Is Nothing Then
                Dim intDocumentID As String = Request.QueryString("ID")

                Dim dtDocument As DataTable = DocumentDAL.GetDocument_ByDocumentIDAndSiteID(intDocumentID, intSiteID)
                If dtDocument.Rows.Count > 0 Then

                    btnAddEdit.Text = Resources.DocumentLibrary_Admin.Document_AddEdit_ButtonUpdate

                    trUploadFile.Visible = False

                    Dim drDocument As DataRow = dtDocument.Rows(0)

                    Dim strSiteDirectory As String = "Site_" & drDocument("SiteID").ToString() + "\"


                    Me.status.SelectedValue = drDocument("status")

                    fileTitle.Text = drDocument("fileTitle").ToString
                    txtDescription.Content = drDocument("filedescription").ToString

                    Me.metaTitle.Text = drDocument("metaTitle").ToString()
                    Me.metaKeywords.Text = drDocument("metaKeywords").ToString()
                    Me.metaDescription.Text = drDocument("metaDescription").ToString()

                    If Not drDocument("publicationDate").ToString() = "" Then
                        Me.publicationDate.SelectedDate = drDocument("publicationDate").ToString()
                    End If

                    If Not drDocument("ExpirationDate").ToString() = "" Then
                        Dim dtExpirationDate As DateTime = Convert.ToDateTime(drDocument("ExpirationDate"))
                        Me.expirationDate.SelectedDate = dtExpirationDate.ToString()
                        If dtExpirationDate < DateTime.Now Then
                            spanExpired.Visible = True
                        End If
                    End If

                    If (drDocument("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.DocumentLibrary_Admin.Document_AddEdit_Uncategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drDocument("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drDocument("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.DocumentLibrary_Admin.Document_AddEdit_Uncategorized, ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.DocumentLibrary_Admin.Document_AddEdit_Uncategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    End If

                    'Show document file type image and link to d/l document
                    'File Size
                    Dim strFileTitle As String = drDocument("FileTitle")
                    Dim strFilePath_Relative As String = drDocument("FilePath").ToString()
                    Dim strFilePath_FileSystem As String = strFilePath_Relative.Replace("/", "\")
                    Dim intRelFilePathIndex As Integer = strFilePath_FileSystem.IndexOf(strDocumentModuleRootDirectory)
                    If intRelFilePathIndex >= 0 Then
                        intRelFilePathIndex = intRelFilePathIndex + strDocumentModuleRootDirectory.Length + strSiteDirectory.Length
                        strFilePath_FileSystem = strFilePath_FileSystem.Substring(intRelFilePathIndex)
                    End If
                    Dim strFileName As String = drDocument("FileName")
                    Dim strFileName_Friendly As String = drDocument("FriendlyFileName")

                    'File Upload Date
                    Dim strPublicationDate As String = drDocument("fileUploadDate").ToString()
                    If Not drDocument("PublicationDate") Is DBNull.Value Then
                        strPublicationDate = drDocument("publicationDate").ToString()
                    End If
                    Dim indFileUploadDate As String = FormatDateTime(strPublicationDate, DateFormat.ShortDate)

                    Dim indFilePath_FileSystem As String = strServerPath & strDocumentModuleRootDirectory & strSiteDirectory & strFilePath_FileSystem & strFileName
                    Dim indFilePath_Relative As String = strFilePath_Relative & strFileName

                    'Finally get the node name
                    imgFileType.Src = CommonWeb.GetFileTypeImage_ByFilePath(indFilePath_FileSystem)
                    litDocumentFileLocation.Text = "<a href='DownloadDocument.aspx?id=" & intDocumentID & "' target='_blank'>" & strFileName_Friendly & "</a>"
                    litDocumentFileSize.Text = "(" & CommonWeb.GetFileSize_ByFilepath(indFilePath_FileSystem) & ")"

                    divDocumentFileAndLocation.Visible = True


                    Dim dtModuleAccess As DataTable = ModuleDAL.GetModuleAccessList_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intDocumentID)
                    For Each drModuleAccess As DataRow In dtModuleAccess.Rows

                        'Either select the list item from the group list or user list depending on the type of module access row we get returned
                        If Not drModuleAccess("groupID") Is DBNull.Value Then
                            Dim intGroupID As Integer = Convert.ToInt32(drModuleAccess("groupID"))
                            Dim liGroup As ListItem = cblGroupList.Items.FindByValue(intGroupID)
                            If Not liGroup Is Nothing Then
                                liGroup.Selected = True
                            End If
                        ElseIf Not drModuleAccess("memberID") Is DBNull.Value Then
                            Dim intMemberID As Integer = Convert.ToInt32(drModuleAccess("memberID"))
                            Dim liMember As ListItem = cblMemberList.Items.FindByValue(intMemberID)
                            If Not liMember Is Nothing Then
                                liMember.Selected = True
                            End If
                        End If

                    Next

                    '*** Populate search tags cbl ***
                    Dim chkbx As CheckBoxList
                    chkbx = CType(cblSearchTags, CheckBoxList)

                    Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intDocumentID)

                    For Each drSearchTag In dtSearchTags.Rows
                        Dim currentCheckBox As ListItem
                        currentCheckBox = chkbx.Items.FindByValue(drSearchTag("searchTagID").ToString())
                        If currentCheckBox IsNot Nothing Then
                            currentCheckBox.Selected = True
                        End If
                    Next

                    'Finally Check if we should make this Document READ-ONLY
                    Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(drDocument("AvailableToAllSites"))
                    rblSite.SelectedValue = boolAvailableToAllSites.ToString().ToLower()
                    If boolAvailableToAllSites Then
                        Dim intSiteID_CurrentDocument As Integer = Convert.ToInt32(drDocument("SiteID"))
                        If Not intSiteID = intSiteID_CurrentDocument Then
                            MakeDocumentReadOnly(intSiteID)
                        End If
                    End If
                Else
                    'Cant find this record so send the AdminUser back to the default page
                    Response.Redirect("default.aspx")

                End If
            Else
                btnAddEdit.Text = Resources.DocumentLibrary_Admin.Document_AddEdit_ButtonAdd
                Dim liAuthenticatedGroup As ListItem = cblGroupList.Items.FindByValue("0")
                If Not liAuthenticatedGroup Is Nothing Then
                    liAuthenticatedGroup.Selected = True
                End If
				
				Dim liUnAuthenticatedGroup As ListItem = cblGroupList.Items.FindByValue("-1")
                If Not liUnAuthenticatedGroup Is Nothing Then
                    liUnAuthenticatedGroup.Selected = True
                End If
				
                trUploadFile.Visible = True

                Me.status.SelectedValue = True

                rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.DocumentLibrary_Admin.Document_AddEdit_Uncategorized, ""))
                rcbCategoryID.SelectedValue = ""

            End If

        End If

    End Sub

    Private Sub MakeDocumentReadOnly(ByVal SiteID As Integer)

        divAssociateWithSite.Visible = False
        divAssociateWithSite_PublicMessage.Visible = True

        'Get the site that created this entry
        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(SiteID)
        If dtSite.Rows.Count > 0 Then
            litSiteName.Text = dtSite.Rows(0)("SiteName")
        End If

        'Prevent AdminUser from updating this record
        btnAddEdit.Visible = False

        'Can only view the first tab with general details
        For Each rdTab As RadTab In rtsDocument.Tabs
            If Convert.ToInt32(rdTab.Value) > 0 Then
                rdTab.Visible = False
            End If
        Next

    End Sub

    Protected Sub lnkAddSearchTags_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If Page.IsValid Then

            'Save progress and send to search-tag page
            addUpdateRecord()
            Dim qs As String
            If Request.QueryString("ID") <> "" Then
                qs = "?ID=" & Request.QueryString("ID")
            Else
                qs = ""
            End If
            Response.Redirect("~/admin/modules/searchtags/default.aspx?mtid=" & ModuleTypeID.ToString() & "&rp=" & Request.Path.ToString.ToLower & qs)
        End If
    End Sub

    Protected Sub addUpdateRecord()

        If Request("ID") Is Nothing Then

            If ruDocument.UploadedFiles.Count > 0 Then
                Dim validFile As UploadedFile = ruDocument.UploadedFiles(0)

                Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

                Dim strSubPath As String = ""
                If Not Request.QueryString("subpath") Is Nothing Then
                    strSubPath = Request.QueryString("subpath")
                End If

                Dim strSiteDirectory As String = "Site_" & intSiteID.ToString() + "\"

                Dim targetFolder As String = strServerPath & strDocumentModuleRootDirectory & strSiteDirectory
                Dim strFilePath As String = strDocumentModuleRootDirectory & strSiteDirectory

                If strSubPath.Length > 0 Then
                    targetFolder = targetFolder & strSubPath & "\"
                    strFilePath = strFilePath & strSubPath & "\"

                End If

                strFilePath = strFilePath.Replace("\", "/")

                Dim dtFileUploadDate = DateTime.Now()

                Dim strFileType As String = validFile.GetExtension.ToString()
                Dim strFriendlyFileName As String = validFile.GetName()
                Dim strFileName As String = validFile.GetNameWithoutExtension() & "_" & dtFileUploadDate.ToString("yyyyMMdd'T'HHmmss") & strFileType

                validFile.SaveAs(Path.Combine(targetFolder, strFileName), True)

                Dim strFileTitle As String = Me.fileTitle.Text.Trim()
                Dim strFileDescription As String = Me.txtDescription.Text.Trim()

                Dim dtPublicationDate As DateTime = DateTime.MinValue
                If Not publicationDate.SelectedDate.ToString() = "" Then
                    dtPublicationDate = publicationDate.SelectedDate
                End If
                Dim dtExpirationDate As DateTime = DateTime.MinValue
                If Not expirationDate.SelectedDate.ToString() = "" Then
                    dtExpirationDate = expirationDate.SelectedDate
                End If

                Dim intCategoryID As Integer = Integer.MinValue
                If Not rcbCategoryID.SelectedValue = "" Then
                    intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
                End If

                Dim strSearchTagID As String = ""
                Dim strMetaTitle As String = metaTitle.Text.Trim()
                Dim strMetaKeywords As String = metaKeywords.Text.Trim()
                Dim strMetaDescription As String = metaDescription.Text.Trim()
                Dim strMetaOther As String = ""

                Dim boolStatus As Boolean = Convert.ToBoolean(status.SelectedValue)

                Dim intAuthorID_member As Integer = Integer.MinValue
                Dim intAuthorID_admin As Integer = Convert.ToInt32(Session("adminID"))

                Dim intModifiedID_member As Integer = Integer.MinValue
                Dim intModifiedID_admin As Integer = Integer.MinValue

                Dim intDocumentID As Integer = DocumentDAL.InsertDocument(intSiteID, boolAvailableToAllSites, strFileTitle, strFileDescription, strFileName, strFriendlyFileName, strFilePath, strFileType, dtFileUploadDate, dtPublicationDate, dtExpirationDate, intCategoryID, strSearchTagID, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, boolStatus, intAuthorID_member, intAuthorID_admin, intModifiedID_member, intModifiedID_admin)

                'Read all GroupIDs for the modules access
                For Each liGroupID As ListItem In cblGroupList.Items
                    If liGroupID.Selected Then
                        Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                        ModuleDAL.InsertModuleAccess(ModuleTypeID, intDocumentID, intGroupID, Integer.MinValue)
                    End If
                Next

                'Read all MemberIDs for the modules access
                For Each liMemberID As ListItem In cblMemberList.Items
                    If liMemberID.Selected Then
                        Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                        ModuleDAL.InsertModuleAccess(ModuleTypeID, intDocumentID, Integer.MinValue, intMemberID)
                    End If
                Next

                'Enter all new tags
                For Each liSearchTagID As ListItem In cblSearchTags.Items
                    If liSearchTagID.Selected Then
                        Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                        SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intDocumentID)
                    End If
                Next
                Response.Redirect("default.aspx")

            End If


        Else
            Dim intDocumentID As String = Request.QueryString("ID")

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim strFileTitle As String = Me.fileTitle.Text.Trim()
            Dim strFileDescription As String = Me.txtDescription.Text.Trim()

            Dim dtPublicationDate As DateTime = DateTime.MinValue
            If Not publicationDate.SelectedDate.ToString() = "" Then
                dtPublicationDate = publicationDate.SelectedDate
            End If
            Dim dtExpirationDate As DateTime = DateTime.MinValue
            If Not expirationDate.SelectedDate.ToString() = "" Then
                dtExpirationDate = expirationDate.SelectedDate
            End If

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If

            Dim strSearchTagID As String = ""
            Dim strMetaTitle As String = metaTitle.Text.Trim()
            Dim strMetaKeywords As String = metaKeywords.Text.Trim()
            Dim strMetaDescription As String = metaDescription.Text.Trim()
            Dim strMetaOther As String = ""

            Dim boolStatus As Boolean = Convert.ToBoolean(status.SelectedValue)

            Dim intModifiedID_member As Integer = Integer.MinValue
            Dim intModifiedID_admin As Integer = Convert.ToInt32(Session("adminID"))

            DocumentDAL.UpdateDocument_ByDocumentID(intDocumentID, intSiteID, boolAvailableToAllSites, strFileTitle, strFileDescription, dtPublicationDate, dtExpirationDate, intCategoryID, strSearchTagID, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, boolStatus, intModifiedID_member, intModifiedID_admin)
            'First remove existing module access, so we can cleanly overwrite module access settings
            ModuleDAL.DeleteModuleAccess_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intDocumentID)

            'Read all GroupIDs for the modules access
            For Each liGroupID As ListItem In cblGroupList.Items
                If liGroupID.Selected Then
                    Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intDocumentID, intGroupID, Integer.MinValue)
                End If
            Next

            'Read all MemberIDs for the modules access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intDocumentID, Integer.MinValue, intMemberID)
                End If
            Next

            'Remove all tags before entering new list
            SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intDocumentID)

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intDocumentID)
                End If
            Next
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click
        If Page.IsPostBack Then
            If IsValid Then
                addUpdateRecord()
            End If

        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub customValFileSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        If ruDocument.UploadedFiles.Count > 0 Then
            For Each file As UploadedFile In ruDocument.UploadedFiles
                If file.InputStream.Length > 20480000 Then

                    'Invalid Submission - so set the IsValid flag to false and show the tab that caused the validation error
                    e.IsValid = False

                    rtsDocument.SelectedIndex = 0
                    rpvDocument.Selected = True
                End If
            Next
        End If
    End Sub

    Protected Sub customValDocumentRequired_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        If Request("ID") Is Nothing Then
            If ruDocument.UploadedFiles.Count = 0 Then
                'Invalid Submission - so set the IsValid flag to false and show the tab that caused the validation error
                e.IsValid = False

                rtsDocument.SelectedIndex = 0
                rpvDocument.Selected = True
            End If
        End If
    End Sub
End Class
