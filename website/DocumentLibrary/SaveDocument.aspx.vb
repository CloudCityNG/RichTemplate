Imports System.Data
Imports System.IO
Imports Telerik.Web.UI

Partial Class DocumentLibrary_SaveDocument
    Inherits RichTemplateLanguagePage

    'Change this if you want to automatically approve Document Submissions
    Const bAutomaticApproval As Boolean = False

    Dim ModuleTypeID As Integer = 3

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    'Get server path and remove last slash
    Dim strServerPath As String = ""
    Dim strDocumentModuleRootDirectory As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Setup Rad Controls
        CommonWeb.SetupRadEditor(Me.Page, txtFileDescription, "~/editorConfig/toolbars/ToolsFileFrontEnd.xml", intSiteID)
        CommonWeb.SetupRadProgressArea(radProgressAreaDocument, intSiteID)
        CommonWeb.SetupRadUpload(radUpDocument, intSiteID)

        'set document path
        strDocumentModuleRootDirectory = ConfigurationManager.AppSettings("DocumentModuleRootDirectory")
        strServerPath = CommonWeb.GetServerPath()

        'Check access and setup back button
        If Not IsPostBack Then

            Dim boolAllowOnlineSubmissions As Boolean = False
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "allow_online_submissions" Then
                    boolAllowOnlineSubmissions = True

                End If
            Next


            If boolAllowOnlineSubmissions AndAlso intMemberID > 0 Then

                BindCategoryDropDownListData()

                LoadDocument()

            Else
                'User is not logged in so redirect them to the document listing
                Response.Redirect("default.aspx")
            End If
        End If
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

    Private Sub LoadDocument()

        'Set the default header title
        Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_HeaderTitle


        If Not Request.QueryString("ID") Is Nothing Then
            Dim documentID As String = Request.QueryString("ID")

            Dim dtDocument As DataTable = DocumentDAL.GetDocument_ByDocumentIDAndSiteID(documentID, intSiteID)
            If dtDocument.Rows.Count > 0 Then

                CommonWeb.SetMasterPageBannerText(Me.Master, Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_HeadingUpdate)
                btnAddEditDocument.Text = Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_ButtonUpdate

                Dim drDocument As DataRow = dtDocument.Rows(0)

                'First we check if the current user was the user who actually created this Document
                Dim boolUserCreatedThis As Boolean = False
                If Not drDocument("authorID_member") Is DBNull.Value Then
                    Dim intAuthorID_Member As Integer = Convert.ToInt32(drDocument("authorID_member"))
                    If intMemberID > 0 And intMemberID = intAuthorID_Member Then
                        boolUserCreatedThis = True
                    End If
                End If

                'Only continue populating this Save document page if the user actually created this document, if not then redirect them to the document detail page for this document
                If boolUserCreatedThis Then
                    aBack.HRef = "DocumentDetail.aspx?id=" & documentID.ToString()

                    txtFileTitle.Text = drDocument("fileTitle").ToString
                    txtFileDescription.Content = drDocument("filedescription").ToString

                    If (drDocument("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drDocument("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drDocument("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_UnCategorized, ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    End If

                    'Show document file type image and link to d/l document
                    'File Size
                    Dim strFileTitle As String = drDocument("FileTitle")

                    Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_HeaderTitle + " - " + strFileTitle

                    Dim strFilePath_Relative As String = drDocument("FilePath").ToString()
                    Dim strFilePath_FileSystem As String = strFilePath_Relative.Replace("/", "\")
                    Dim intRelFilePathIndex As Integer = strFilePath_FileSystem.IndexOf(strDocumentModuleRootDirectory)
                    If intRelFilePathIndex >= 0 Then
                        intRelFilePathIndex = intRelFilePathIndex + strDocumentModuleRootDirectory.Length
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

                    Dim indFilePath_FileSystem As String = strServerPath & strDocumentModuleRootDirectory & strFilePath_FileSystem & strFileName
                    Dim indFilePath_Relative As String = strFilePath_Relative & strFileName
                    Dim fiDocument As New FileInfo(indFilePath_FileSystem)

                    'Construct the fileType Image and size
                    imgFileType.Src = CommonWeb.GetFileTypeImage_ByFilePath(indFilePath_FileSystem)

                    'Finally get the node name
                    litDocumentFileLocation.Text = "<a href='" & indFilePath_Relative & "' target='_blank'>" & strFileName_Friendly & "</a>"
                    litDocumentFileSize.Text = "(" & CommonWeb.GetFileSize_ByFilepath(indFilePath_FileSystem) & ")"

                    divDocumentFileAndLocation.Visible = True
                    divUploadFile.Visible = False
                Else
                    'User did not create this document, so redirect to the documents the detail page
                    Response.Redirect("DocumentDetail.aspx?id=" & documentID)
                End If
            Else
                'ID was supplied but could not be found, so redirect to the modules default page
                Response.Redirect("Default.aspx")
            End If
        Else
            aBack.HRef = "Default.aspx"

            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_HeadingAdd)
            btnAddEditDocument.Text = Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_ButtonAdd

            rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_UnCategorized, ""))
            rcbCategoryID.SelectedValue = ""

            divDocumentFileAndLocation.Visible = False
            divUploadFile.Visible = True
        End If
    End Sub

    Protected Sub customValFileSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        If radUpDocument.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = radUpDocument.UploadedFiles(0)
            If file.InputStream.Length - 1 >= 20480000 Then
                e.IsValid = False
            End If
        End If
    End Sub

    Protected Sub customValDocumentRequired_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        If radUpDocument.UploadedFiles.Count = 0 Then
            e.IsValid = False
        End If
    End Sub

    Protected Sub btnAddEditDocument_Click(ByVal sender As Object, ByVal e As EventArgs)
        If IsValid Then
            addEditDocument()
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not Request.QueryString("ID") Is Nothing Then
            Dim documentID As String = Request.QueryString("ID")
            Response.Redirect("DocumentDetail.aspx?id=" & documentID.ToString())
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub addEditDocument()
        If intMemberID > 0 Then
            If Request("ID") Is Nothing Then
                If radUpDocument.UploadedFiles.Count > 0 Then
                    Dim validFile As UploadedFile = radUpDocument.UploadedFiles(0)

                    Dim strSubPath As String = "MemberDocs"

                    Dim targetFolder As String = strServerPath & strDocumentModuleRootDirectory
                    Dim strFilePath As String = strDocumentModuleRootDirectory

                    If strSubPath.Length > 0 Then
                        targetFolder = targetFolder & strSubPath & "\"
                        strFilePath = strFilePath & strSubPath & "\"

                    End If

                    strFilePath = strFilePath.Replace("\", "/")

                    Dim dtFileUploadDate = DateTime.Now()

                    Dim strFileType As String = validFile.GetExtension.ToString()
                    Dim strFriendlyFileName As String = validFile.GetName()
                    Dim strFileName As String = validFile.GetNameWithoutExtension() & "_" & dtFileUploadDate.ToString("yyyyMMdd'T'HHmmss") & strFileType

                    If Not Directory.Exists(targetFolder) Then
                        Directory.CreateDirectory(targetFolder)
                    End If
                    validFile.SaveAs(Path.Combine(targetFolder, strFileName), True)

                    Dim strFileTitle As String = Me.txtFileTitle.Text.Trim()
                    Dim strFileDescription As String = Me.txtFileDescription.Text.Trim()

                    Dim intCategoryID As Integer = Integer.MinValue
                    If Not rcbCategoryID.SelectedValue = "" Then
                        intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
                    End If

                    Dim dtPublicationDate As DateTime = DateTime.MinValue
                    Dim dtExpirationDate As DateTime = DateTime.MinValue

                    Dim boolAvailableToAllSites As Boolean = False ' Default this member-created documents to only be viewable for this site

                    Dim strSearchTagID As String = String.Empty

                    Dim strMetaTitle As String = String.Empty
                    Dim strMetaKeywords As String = String.Empty
                    Dim strMetaDescription As String = String.Empty
                    Dim strMetaOther As String = String.Empty

                    Dim boolStatus As Boolean = True

                    Dim authorID_member As Integer = intMemberID
                    Dim authorID_admin As Integer = Integer.MinValue

                    Dim intModifiedID_member As Integer = Integer.MinValue
                    Dim intModifiedID_admin As Integer = Integer.MinValue

                    Dim intDocumentID As Integer = DocumentDAL.InsertDocument(intSiteID, boolAvailableToAllSites, strFileTitle, strFileDescription, strFileName, strFriendlyFileName, strFilePath, strFileType, dtFileUploadDate, dtPublicationDate, dtExpirationDate, intCategoryID, strSearchTagID, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, boolStatus, authorID_member, authorID_admin, intModifiedID_member, intModifiedID_admin)

                    SendEmail()

                    If Not bAutomaticApproval Then
                        'Show Document Record has been submitted, and awaiting approval Message
                        divModuleContentMain.Visible = False
                        divModuleContentSubmitted.Visible = True
                    Else
                        'Then set access for this record to EVERYONE, and Send the user to this newly created document
                        ModuleDAL.InsertModuleAccess(ModuleTypeID, intDocumentID, 0, Integer.MinValue)
                        Response.Redirect("documentDetail.aspx?id=" & intDocumentID)
                    End If


                End If

            Else
                Dim documentID As String = Request.QueryString("ID")

                Dim strFileTitle As String = Me.txtFileTitle.Text.Trim()
                Dim strFileDescription As String = Me.txtFileDescription.Text.Trim()

                Dim intCategoryID As Integer = Integer.MinValue
                If Not rcbCategoryID.SelectedValue = "" Then
                    intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
                End If

                Dim intModifiedID_member As Integer = intMemberID
                Dim intModifiedID_admin As Integer = Integer.MinValue
                DocumentDAL.UpdateDocument_ByDocumentID_FrontEnd(documentID, strFileTitle, strFileDescription, intCategoryID, intModifiedID_member, intModifiedID_admin)

                Response.Redirect("documentDetail.aspx?id=" & documentID)
            End If

        Else
            Response.Redirect("default.aspx")
        End If

    End Sub
    Protected Sub SendEmail()

        ' Send Document Confirmation to user
        'First get the member who submitted this document record
        Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intMemberID)
        If dtMember.Rows.Count > 0 Then
            Dim drMembrer As DataRow = dtMember.Rows(0)

            Dim strEmailAddress As String = drMembrer("Email").ToString()
            Dim strDocumentTitle As String = txtFileTitle.Text.Trim()

            Dim strCategoryName As String = Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_UnCategorized
            If rcbCategoryID.SelectedValue.Length > 0 Then
                Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryID(rcbCategoryID.SelectedValue)
                If dtCategory.Rows.Count > 0 Then
                    Dim drCategory As DataRow = dtCategory.Rows(0)
                    strCategoryName = drCategory("CategoryName").ToString()
                End If
            End If

            'We have no swap out data for this email
            Dim EmailSwapoutData_User As New Hashtable()
            EmailSwapoutData_User("DocumentTitle") = strDocumentTitle

            'Populate the list of recipients
            Dim listRecipientEmailAddress_User As New ArrayList()
            listRecipientEmailAddress_User.Add(strEmailAddress)

            'Send this information to our email DAL with Email Type ID = 16 -> Document Submitted - Sent To User
            EmailDAL.SendEmail(listRecipientEmailAddress_User, 16, intSiteID, EmailSwapoutData_User)


            'Always send an email to the appropriate administrator
            'Send Email to site administrator informing them of a new document entry
            Dim EmailSwapoutData_Administrator As New Hashtable()

            'Add the members ID and email address to this email
            EmailSwapoutData_Administrator("EmailAddress") = strEmailAddress
            EmailSwapoutData_Administrator("DocumentTitle") = strDocumentTitle
            EmailSwapoutData_Administrator("DocumentCategory") = strCategoryName

            'Send this information to our email DAL with Email Type ID = 17 -> Document Submitted - Sent To Administrator -> This is an Administrator Email
            EmailDAL.SendEmail(17, intSiteID, EmailSwapoutData_Administrator)
        End If
    End Sub
End Class
