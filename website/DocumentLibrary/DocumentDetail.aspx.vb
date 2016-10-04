Imports System.Data
Imports System.IO

Partial Class DocumentLibrary_DocumentDetail
    Inherits RichTemplateLanguagePage

    Dim boolAllowArchive As Boolean = False
    Dim strServerPath As String
    Dim intDocumentID As Integer = 0

    Dim ModuleTypeID As Integer = 3

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'set document path
        strServerPath = CommonWeb.GetServerPath()

        If Not Request.QueryString("id") Is Nothing Then
            intDocumentID = Convert.ToInt32(Request.QueryString("id"))
            LoadDocument()

        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub LoadDocument()

        Dim boolStatus As Boolean = True
        Dim boolAllowComments As Boolean = False
        Dim boolEnablePublicCommentSubmission As Boolean = False
        Dim boolAllowOnlineSubmissions As Boolean = False
        Dim boolEnableGroupsAndUserAccess As Boolean = False

        If Request.QueryString("archive") = 1 Then
            boolStatus = False
        End If

        'Check we need to show the book this link, but only if its active
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                boolAllowArchive = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "show_add_this" Then
                If boolStatus Then
                    plcAddThis.Visible = True
                End If

            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" Then
                boolAllowComments = True

            ElseIf drModuleConfig("fieldName").ToString.ToLower() = "enable_public_comment_submission" Then
                boolEnablePublicCommentSubmission = True

            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_submissions" Then
                boolAllowOnlineSubmissions = True

            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True

            End If
        Next


        'If we find out the document is an archived document, but we do not allow achives then redirect to default page
        If boolAllowArchive = False And boolStatus = False Then
            Response.Redirect("default.aspx")
        End If

        'Load the document
        Dim dtDocuments As DataTable = If(boolEnableGroupsAndUserAccess, DocumentDAL.GetDocument_ByDocumentIDAndStatusAndAccess_FrontEnd(intDocumentID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), DocumentDAL.GetDocument_ByDocumentIDAndStatus_FrontEnd(intDocumentID, boolStatus, intSiteID))
        If dtDocuments.Rows.Count > 0 Then
            Dim drDocument As DataRow = dtDocuments.Rows(0)

            'File Size
            Dim indFileFullPath As String = strServerPath & drDocument("filePath").ToString() & drDocument("fileName").ToString()
            indFileFullPath = indFileFullPath.Replace("\", "/")

            'Construct the fileType Image and size
            imgFileType.Src = CommonWeb.GetFileTypeImage_ByFilePath(indFileFullPath)
            litFileSize.Text = "<span class='grayText'>(" & CommonWeb.GetFileSize_ByFilepath(indFileFullPath) & ")</span>"

            'Set the file title, file description, and author
            Dim strFileTitle As String = drDocument("fileTitle").ToString()

            'Set the page title Default
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentDetail_HeaderTitle + " - " + strFileTitle

            litFileTitle.Text = strFileTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentDetail_Heading & ": " & strFileTitle)

            Dim strCategory As String = ""
            If Not drDocument("categoryName") Is DBNull.Value Then
                strCategory = drDocument("categoryName")
                If strCategory.Length > 0 Then
                    divCategory.Visible = True
                    litCategory.Text = strCategory
                End If
            End If

            Dim strDescription As String = ""
            If Not drDocument("fileDescription") Is DBNull.Value Then
                strDescription = drDocument("fileDescription")
                If strDescription.Length > 0 Then
                    divDescription.Visible = True
                    litDescription.Text = strDescription
                End If
            End If

            'Set the file uploader and viewing date
            'set the author name and posted by date
            Dim strAuthorUsername As String = Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentDetail_PostedByDefault
            If Not drDocument("author_username") Is DBNull.Value Then
                strAuthorUsername = drDocument("author_username").ToString()
            End If
            litPostedBy.Text = strAuthorUsername

            Dim dtViewDate As DateTime = Convert.ToDateTime(drDocument("viewDate"))
            litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
            litViewDateTime.Text = dtViewDate.ToString("h:mm tt")

            'Finally show the edit link if the document was uploaded by the currently logged in user
            If boolAllowOnlineSubmissions Then
                If Not drDocument("authorID_member") Is DBNull.Value Then
                    Dim intAuthorID_Member As Integer = Convert.ToInt32(drDocument("authorID_member"))
                    If intMemberID > 0 And intMemberID = intAuthorID_Member Then
                        divEditDocument.Visible = True
                    End If

                End If
            End If

            'set the page title
            If Not drDocument("metaTitle") Is DBNull.Value Then
                Dim strMetaTitle As String = drDocument("metaTitle").ToString()
                If strMetaTitle.Length > 0 Then
                    Page.Header.Title = strMetaTitle
                End If
            End If

            'set the page's keywords

            If Not drDocument("metaKeywords") Is DBNull.Value Then
                Dim keywordsHtmlMeta As New HtmlMeta()
                keywordsHtmlMeta.Name = "keywords"
                keywordsHtmlMeta.Content = drDocument("metaKeywords").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(keywordsHtmlMeta)
            End If

            'set the page's description
            If Not drDocument("metaDescription") Is DBNull.Value Then
                Dim descriptionHtmlMeta As New HtmlMeta()
                descriptionHtmlMeta.Name = "description"
                descriptionHtmlMeta.Content = drDocument("metaDescription").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(descriptionHtmlMeta)
            End If

            If boolAllowComments Then

                'Set the documents average rating
                radAveRating.Value = Convert.ToDecimal(drDocument("aveRating"))
                divRating.Visible = True

                Dim dtDocumentComments As DataTable = CommentDAL.GetCommentList_ByModuleTypeIDAndRecordIDAndSiteID_Front(ModuleTypeID, intDocumentID, intSiteID)
                If dtDocumentComments.Rows.Count > 0 Then
                    litCommentCount.Text = dtDocumentComments.Rows.Count & " " & Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentDetail_CommentCount & If(dtDocumentComments.Rows.Count > 1, "s", "")
                End If

                'setup the comments list
                Dim intSiteID_ForDocument As Integer = Convert.ToInt32(drDocument("siteID"))
                ucCommentsModule.SetupCommentModule(intSiteID_ForDocument, ModuleTypeID, intDocumentID, intMemberID, boolStatus, boolEnablePublicCommentSubmission)


                If Not Request.QueryString("r") Is Nothing Then
                    ucCommentsModule.InitialRating = Convert.ToDecimal(Request.QueryString("r"))
                End If
            End If

        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub ucCommentsModule_CommentAdded(ByVal sender As Object, ByVal e As EventArgs) Handles ucCommentsModule.CommentAdded
        DocumentDAL.UpdateDocument_AveRating_ByDocumentID(intDocumentID)
        LoadDocument()
    End Sub

End Class
