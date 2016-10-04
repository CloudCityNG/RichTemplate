Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Xml

Partial Class PressRelease_SavePressRelease
    Inherits RichTemplateLanguagePage

    'Change this if you want to automatically approve Press Release Submissions
    Const bAutomaticApproval As Boolean = False

    Dim ModuleTypeID As Integer = 10

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Setup Rad Controls
        CommonWeb.SetupRadEditor(Me.Page, txtBody, "~/editorConfig/toolbars/ToolsFileFrontEnd.xml", intSiteID)
        CommonWeb.SetupRadProgressArea(radProgressArePressReleaseImage, intSiteID)
        CommonWeb.SetupRadUpload(radUploadPressReleaseImage, intSiteID)

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

                LoadPressRelease()
            Else
                'User is not logged in or we do not allow online submissions so redirect them to the press release listing
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

    Private Sub LoadPressRelease()

        'Set the default header title
        Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_HeaderTitle

        If Not Request.QueryString("ID") Is Nothing Then
            Dim intPressReleaseID As Integer = Convert.ToInt32(Request.QueryString("ID"))

            Dim dtPressRelease As DataTable = PressReleaseDAL.GetPressRelease_ByPressReleaseIDAndSiteID(intPressReleaseID, intSiteID)
            If dtPressRelease.Rows.Count > 0 Then
                CommonWeb.SetMasterPageBannerText(Me.Master, Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_HeadingUpdate)
                btnAddEditPressRelease.Text = Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_ButtonUpdate

                Dim drPressRelease As DataRow = dtPressRelease.Rows(0)

                'First we check if the current user was the user who actually created this press release
                Dim boolUserCreatedThis As Boolean = False
                If Not drPressRelease("authorID_member") Is DBNull.Value Then
                    Dim intAuthorID_Member As Integer = Convert.ToInt32(drPressRelease("authorID_member"))
                    If intMemberID > 0 And intMemberID = intAuthorID_Member Then
                        boolUserCreatedThis = True
                    End If
                End If

                'Only continue populating this Save press release page if the user actually created this press release, if not then redirect them to the press release detail page for this press release
                If boolUserCreatedThis Then
                    aBack.HRef = "Default.aspx?id=" & intPressReleaseID.ToString()

                    Dim strTitle = drPressRelease("title")

                    'Set the Page Title
                    Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_HeaderTitle + " - " + strTitle

                    txtTitle.Text = strTitle
                    txtSummary.Text = drPressRelease("Summary").ToString()
                    txtBody.Content = drPressRelease("Body").ToString()

                    If (drPressRelease("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drPressRelease("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drPressRelease("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_UnCategorized, ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    End If

                    version.Text = drPressRelease("Version").ToString

                    Me.pressReleaseImage.Visible = False
                    Me.lnkDeleteImage.Visible = False
                    If Not drPressRelease("thumbnail") Is DBNull.Value Then
                        If Not drPressRelease("thumbnail").ToString() = "" Then

                            Me.pressReleaseImage.DataValue = drPressRelease("thumbnail")
                            Me.pressReleaseImage.Visible = True
                            Me.lnkDeleteImage.Visible = True
                            Me.divPressReleaseImage.Visible = True

                            Me.divUploadImage.Visible = False

                        End If
                    End If

                Else
                    'User did not create this Fpress releaseAQ, so redirect to the press release's the detail page
                    Response.Redirect("Default.aspx?id=" & intPressReleaseID)
                End If

            Else
                'ID was supplied but could not be found, so redirect to the modules default page
                Response.Redirect("Default.aspx")
            End If
        Else
            aBack.HRef = "Default.aspx"
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_HeadingAdd)
            btnAddEditPressRelease.Text = Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_ButtonAdd

            rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_UnCategorized, ""))
            rcbCategoryID.SelectedValue = ""

            divUploadImage.Visible = True
        End If
    End Sub

    Protected Sub addEditPressRelease()
        If intMemberID > 0 Then
            If Request("ID") Is Nothing Then

                Dim strTitle As String = Me.txtTitle.Text.Trim().ToString()
                Dim strSummary As String = Me.txtSummary.Text.Trim().ToString()

                Dim intCategoryID As Integer = Integer.MinValue
                If Not rcbCategoryID.SelectedValue = "" Then
                    intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
                End If

                Dim strBodyContent As String = txtBody.Content.ToString()

                Dim authorID_member As Integer = intMemberID
                Dim authorID_admin As Integer = Integer.MinValue

                Dim intModifiedID_member As Integer = Integer.MinValue
                Dim intModifiedID_admin As Integer = Integer.MinValue


                'Default values for user submitted press releases
                Dim boolStatus As Boolean = True 'default for user submitted press releases
                Dim strExternalLink As String = String.Empty

                Dim dtPublication As DateTime = DateTime.MinValue
                Dim dtExpiration As DateTime = DateTime.MinValue

                Dim boolAvailableToAllSites As Boolean = False ' Default this member-created press release to only be viewable for this site

                Dim strMetaTitle As String = String.Empty
                Dim strMetaKeywords As String = String.Empty
                Dim strMetaDescription As String = String.Empty
                Dim strMetaOther As String = String.Empty


                Dim listGroupIDs As String = String.Empty
                Dim listMemberIDs As String = String.Empty
                Dim strSearchTagID As String = String.Empty

                Dim intVersion As Integer = 1
                Dim dtDateTimeStamp As DateTime = DateTime.Now

                'INSERT PRESS RELEASE
                Dim intPressReleaseID As Integer = PressReleaseDAL.InsertPressRelease(intSiteID, boolAvailableToAllSites, strTitle, strSummary, intCategoryID, dtPublication, dtExpiration, strBodyContent, boolStatus, strExternalLink, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, dtDateTimeStamp, authorID_member, authorID_admin, intModifiedID_member, intModifiedID_admin)
                
                'Add press release image if it exists
                If radUploadPressReleaseImage.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = radUploadPressReleaseImage.UploadedFiles(0)
                    Dim strThumbnailName As String = file.GetName
                    Dim bytesPressReleaseImage(file.InputStream.Length - 1) As Byte
                    file.InputStream.Read(bytesPressReleaseImage, 0, file.InputStream.Length)
                    PressReleaseDAL.UpdatePressRelease_PressReleaseImage_ByPressReleaseID(intPressReleaseID, strThumbnailName, bytesPressReleaseImage)

                End If

                SendEmail()

                If Not bAutomaticApproval Then
                    'Show Press Release Record has been submitted, and awaiting approval Message
                    divModuleContentMain.Visible = False
                    divModuleContentSubmitted.Visible = True
                Else
                    'Then set access for this record to EVERYONE, and Send the user to this newly created press releaes
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intPressReleaseID, 0, Integer.MinValue)
                    Response.Redirect("default.aspx?id=" & intPressReleaseID)
                End If

            Else
                Dim intPressReleaseID As Integer = Request.QueryString("ID")

                Dim strTitle As String = Me.txtTitle.Text.Trim().ToString()
                Dim strSummary As String = Me.txtSummary.Text.Trim().ToString()

                Dim intCategoryID As Integer = Integer.MinValue
                If Not rcbCategoryID.SelectedValue = "" Then
                    intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
                End If

                Dim strBodyContent As String = txtBody.Content.ToString()

                Dim intVersion As Integer = 1
                If Not IsDBNull(version.Text.Trim()) And version.Text.Trim() <> "" Then
                    intVersion = Convert.ToInt32(version.Text.Trim())
                    intVersion = intVersion + 1
                End If

                Dim intModifiedID_member As Integer = Integer.MinValue
                Dim intModifiedID_admin As Integer = Integer.MinValue

                PressReleaseDAL.UpdatePressRelease_ByPressReleaseID_FrontEnd(intPressReleaseID, strTitle, strSummary, intCategoryID, strBodyContent, intVersion, intModifiedID_member, intModifiedID_admin)

                'Add press release image if it exists
                If radUploadPressReleaseImage.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = radUploadPressReleaseImage.UploadedFiles(0)
                    Dim strThumbnailName As String = file.GetName
                    Dim bytesPressReleaseImage(file.InputStream.Length - 1) As Byte
                    file.InputStream.Read(bytesPressReleaseImage, 0, file.InputStream.Length)

                    PressReleaseDAL.UpdatePressRelease_PressReleaseImage_ByPressReleaseID(intPressReleaseID, strThumbnailName, bytesPressReleaseImage)
                End If

                Response.Redirect("default.aspx?id=" & intPressReleaseID)
            End If
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub btnAddEditPressRelease_Click(ByVal sender As Object, ByVal e As EventArgs)
        If IsValid Then
            addEditPressRelease()
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not Request.QueryString("ID") Is Nothing Then
            Dim intPressReleaseID As String = Request.QueryString("ID")
            Response.Redirect("Default.aspx?id=" & intPressReleaseID.ToString())
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

#Region "Press Release Image"
    Protected Sub lnkDeleteImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteImage.Click
        Dim intPressReleaseID As Integer = Convert.ToInt32(Request.QueryString("ID"))
        Dim bytesPressReleaseImage() As Byte
        PressReleaseDAL.UpdatePressRelease_PressReleaseImage_ByPressReleaseID(intPressReleaseID, String.Empty, bytesPressReleaseImage)

        'Hide the press release image and the delete link
        pressReleaseImage.Visible = False
        lnkDeleteImage.Visible = False
        divPressReleaseImage.Visible = False

        divUploadImage.Visible = True
    End Sub

    Protected Sub customValPressReleaseImageSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add press release image if it exists, must have a file size less than 100kb, but we give them a 5kb buffer
        If radUploadPressReleaseImage.UploadedFiles.Count > 0 Then
            For Each file As UploadedFile In radUploadPressReleaseImage.UploadedFiles
                If file.InputStream.Length > 102400 Then
                    e.IsValid = False
                End If
            Next
        End If
    End Sub

#End Region

    Protected Sub SendEmail()

        ' Send Press Release Confirmation to user
        'First get the member who submitted this press release record
        Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intMemberID)
        If dtMember.Rows.Count > 0 Then
            Dim drMembrer As DataRow = dtMember.Rows(0)

            Dim strEmailAddress As String = drMembrer("Email").ToString()
            Dim strPressReleaseTitle As String = txtTitle.Text.Trim()
            Dim strPressReleaseSummary As String = txtSummary.Text.Trim()

            Dim strCategoryName As String = Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_UnCategorized
            If rcbCategoryID.SelectedValue.Length > 0 Then
                Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryID(rcbCategoryID.SelectedValue)
                If dtCategory.Rows.Count > 0 Then
                    Dim drCategory As DataRow = dtCategory.Rows(0)
                    strCategoryName = drCategory("CategoryName").ToString()
                End If
            End If

            'We have no swap out data for this email
            Dim EmailSwapoutData_User As New Hashtable()
            EmailSwapoutData_User("PressReleaseTitle") = strPressReleaseTitle

            'Populate the list of recipients
            Dim listRecipientEmailAddress_User As New ArrayList()
            listRecipientEmailAddress_User.Add(strEmailAddress)

            'Send this information to our email DAL with Email Type ID = 22 -> Press Release Submitted - Sent To User
            EmailDAL.SendEmail(listRecipientEmailAddress_User, 22, intSiteID, EmailSwapoutData_User)


            'Always send an email to the appropriate administrator
            'Send Email to site administrator informing them of a new press release entry
            Dim EmailSwapoutData_Administrator As New Hashtable()

            'Add the members ID and email address to this email
            EmailSwapoutData_Administrator("EmailAddress") = strEmailAddress
            EmailSwapoutData_Administrator("PressReleaseTitle") = strPressReleaseTitle
            EmailSwapoutData_Administrator("PressReleaseSummary") = strPressReleaseSummary
            EmailSwapoutData_Administrator("PressReleaseCategory") = strCategoryName

            'Send this information to our email DAL with Email Type ID = 23 -> Press Release Submitted - Sent To Administrator -> This is an Administrator Email
            EmailDAL.SendEmail(23, intSiteID, EmailSwapoutData_Administrator)
        End If
    End Sub
End Class
