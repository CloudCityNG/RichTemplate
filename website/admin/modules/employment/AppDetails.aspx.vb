Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Xml

Partial Class admin_modules_employment_AppDetails
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Dim ModuleTypeID As Integer = 4 ' Module Type: Employment

    Dim intEmploymentID As Integer

    Private Sub BindSalutationDropDown()

        'Populate Salutation Titles
        ddlSalutation.Items.Clear()

        Dim dtSalutation As DataTable = SalutationDAL.GetSalutationList()
        For Each drSalutation As DataRow In dtSalutation.Rows
            Dim intSalutationID As Integer = drSalutation("ID")
            Dim strSalutation_LanguageProperty As String = drSalutation("Salutation_LanguageProperty")

            Dim strSalutation_LangaugeSpecific As String = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)

            Dim liSalutation As New ListItem(strSalutation_LangaugeSpecific, intSalutationID)
            ddlSalutation.Items.Add(liSalutation)
        Next

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Setup Rad Controls
        CommonWeb.SetupRadDatePicker(startDate, intSiteID)
        CommonWeb.SetupRadEditor(Page, txtResume, intSiteID)
        CommonWeb.SetupRadProgressArea(rpDocument1, intSiteID)
        CommonWeb.SetupRadProgressArea(rpDocument2, intSiteID)
        CommonWeb.SetupRadProgressArea(rpDocument3, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadDocument1, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadDocument2, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadDocument3, intSiteID)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Employment_Admin.Employment_AddEditApplicants_Header
        ucHeader.PageHelpID = 5 'Help Item for Employment/Job Opportunities

        If Not Request.QueryString("employmentID") Is Nothing Then

            intEmploymentID = Convert.ToInt32(Request.QueryString("employmentID"))

            If Not Page.IsPostBack Then

                'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
                If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, intSiteID) Then
                    Response.Redirect("/richadmin")
                End If

                aBackToRegistrations.HRef = "applicants.aspx?employmentID=" & intEmploymentID
                BindSalutationDropDown()

                If Not Request.QueryString("subID") Is Nothing Then
                    Dim intSubmissionID As Integer = Convert.ToInt32(Request.QueryString("subID"))

                    Dim dtEmploymentSubmission As DataTable = EmploymentDAL.GetEmploymentSubmissions_BySubIDAndSiteID(intSubmissionID, intSiteID)
                    If dtEmploymentSubmission.Rows.Count > 0 Then
                        Dim drEmploymentSubmission As DataRow = dtEmploymentSubmission.Rows(0)

                        If Not drEmploymentSubmission("MemberID") Is DBNull.Value Then
                            txtMemberID.Text = drEmploymentSubmission("MemberID")
                        End If

                        Status.SelectedValue = drEmploymentSubmission("Status").ToString

                        If Not drEmploymentSubmission("SalutationID") Is DBNull.Value Then
                            Me.ddlSalutation.SelectedValue = drEmploymentSubmission("SalutationID").ToString()
                        End If
                        Me.txtFirstName.Text = drEmploymentSubmission("FirstName").ToString()
                        Me.txtLastName.Text = drEmploymentSubmission("LastName").ToString()
                        Me.txtEmailAddress.Text = drEmploymentSubmission("Email").ToString()

                        Me.txtPhoneNumber.Text = drEmploymentSubmission("PhoneNumber").ToString()
                        If Not drEmploymentSubmission("Address").ToString() = "" Then
                            ucAddress.LocationStreet = drEmploymentSubmission("Address").ToString()
                        End If

                        If Not drEmploymentSubmission("City").ToString() = "" Then
                            ucAddress.LocationCity = drEmploymentSubmission("City").ToString()
                        End If

                        If Not drEmploymentSubmission("StateID").ToString() = "" Then
                            ucAddress.LocationState = drEmploymentSubmission("StateID").ToString()
                        End If

                        If Not drEmploymentSubmission("ZipCode").ToString() = "" Then
                            ucAddress.LocationZipCode = drEmploymentSubmission("ZipCode").ToString()
                        End If

                        If Not drEmploymentSubmission("CountryID").ToString() = "" Then
                            ucAddress.LocationCountry = drEmploymentSubmission("CountryID").ToString()
                        End If

                        If Not drEmploymentSubmission("coverLetterText") Is DBNull.Value Then
                            txtCoverLetter.Text = drEmploymentSubmission("coverLetterText")
                        End If


                        If Not drEmploymentSubmission("resumeText") Is DBNull.Value Then
                            txtResume.Content = drEmploymentSubmission("resumeText")
                        End If

                        If Not drEmploymentSubmission("startDate").ToString() = "" Then
                            Me.startDate.SelectedDate = drEmploymentSubmission("startDate").ToString()
                        End If

                        If Not drEmploymentSubmission("yrsExperience") Is DBNull.Value Then
                            txtYearsExperience.Text = drEmploymentSubmission("yrsExperience")
                        End If

                        If Not drEmploymentSubmission("LastTitle") Is DBNull.Value Then
                            txtLastTitle.Text = drEmploymentSubmission("LastTitle")
                        End If

                        If Not drEmploymentSubmission("EduLevel") Is DBNull.Value Then
                            txtEduLevel.Text = drEmploymentSubmission("EduLevel")
                        End If

                        If Not drEmploymentSubmission("ProjExpertise") Is DBNull.Value Then
                            txtProjExpertise.Text = drEmploymentSubmission("ProjExpertise")
                        End If

                        If Not drEmploymentSubmission("skills") Is DBNull.Value Then
                            txtSkills.Text = drEmploymentSubmission("skills")
                        End If

                        If Not drEmploymentSubmission("salary") Is DBNull.Value Then
                            txtSalary.Text = drEmploymentSubmission("salary")
                        End If

                        'Load employment supporting documents
                        divDocumentFileAndLocation1.Visible = False

                        divDocumentFileAndLocation2.Visible = False

                        divDocumentFileAndLocation3.Visible = False


                        If Not drEmploymentSubmission("upload1Name") Is DBNull.Value Then
                            Dim strEmploymentDocumentName1 As String = drEmploymentSubmission("upload1Name")
                            Dim bytesEmploymentDocument1() As Byte
                            If Not drEmploymentSubmission("upload1") Is DBNull.Value Then
                                bytesEmploymentDocument1 = drEmploymentSubmission("upload1")
                            End If
                            LoadEmploymentDocument(strEmploymentDocumentName1, bytesEmploymentDocument1, 1)
                        End If

                        If Not drEmploymentSubmission("upload2Name") Is DBNull.Value Then
                            Dim strEmploymentDocumentName2 As String = drEmploymentSubmission("upload2Name")
                            Dim bytesEmploymentDocument2() As Byte
                            If Not drEmploymentSubmission("upload2") Is DBNull.Value Then
                                bytesEmploymentDocument2 = drEmploymentSubmission("upload2")
                            End If
                            LoadEmploymentDocument(strEmploymentDocumentName2, bytesEmploymentDocument2, 2)
                        End If

                        If Not drEmploymentSubmission("upload3Name") Is DBNull.Value Then
                            Dim strEmploymentDocumentName3 As String = drEmploymentSubmission("upload3Name")
                            Dim bytesEmploymentDocument3() As Byte
                            If Not drEmploymentSubmission("upload3") Is DBNull.Value Then
                                bytesEmploymentDocument3 = drEmploymentSubmission("upload3")
                            End If
                            LoadEmploymentDocument(strEmploymentDocumentName3, bytesEmploymentDocument3, 3)
                        End If
                        btnAddEdit.Text = Resources.Employment_Admin.Employment_AddEditApplicants_ButtonUpdate

                        'Finally Check if we should make this Employment Opportunity READ-ONLY
                        Dim intSiteID_Employment As Integer = Convert.ToInt32(drEmploymentSubmission("SiteID"))
                        If Not intSiteID = intSiteID_Employment Then
                            MakeEmploymentReadOnly(intSiteID_Employment)
                        End If
                    Else
                        'sub ID row not found, so redirect to main employment page
                        Response.Redirect("Applicants.aspx?employmentID=" & intEmploymentID)
                    End If
                Else
                    btnAddEdit.Text = Resources.Employment_Admin.Employment_AddEditApplicants_ButtonAdd
                    Status.SelectedValue = True
                End If

            End If
        Else
            'NO employment ID found, so redirect to main employment page
            Response.Redirect("Default.aspx")
        End If

    End Sub

    Private Sub MakeEmploymentReadOnly(ByVal SiteID As Integer)

        'Prevent AdminUser from updating this record
        lnkDeleteDocument1.Visible = False
        lnkDeleteDocument2.Visible = False
        lnkDeleteDocument3.Visible = False
        btnAddEdit.Visible = False

    End Sub

    Protected Sub LoadEmploymentDocument(ByVal strEmploymentDocumentName As String, ByVal bytesEmploymentDocument() As Byte, ByVal intDocumentNumber As Integer)
        'First check if the document has a valid length, else show an upload panel
        Dim strFileTypeImageUrl As String = CommonWeb.GetFileTypeImage_ByFilepath(strEmploymentDocumentName)
        Dim strFileSize As String = CommonWeb.GetFileSize(bytesEmploymentDocument)

        'we load this image and file into first available divDocument, and hide its corresponding file upload control

        Select Case intDocumentNumber
            Case 1
                If strEmploymentDocumentName.Length > 0 Then

                    lnkDocument1.Text = strEmploymentDocumentName
                    litDocumentFileSize1.Text = strFileSize
                    imgFileType1.Src = strFileTypeImageUrl

                    divDocumentFileAndLocation1.Visible = True
                    divUploadDocument1.Visible = False
                Else
                    divUploadDocument1.Visible = True
                    divUploadDocument1.Visible = False
                End If

            Case 2
                If strEmploymentDocumentName.Length > 0 Then

                    lnkDocument2.Text = strEmploymentDocumentName
                    litDocumentFileSize2.Text = strFileSize
                    imgFileType2.Src = strFileTypeImageUrl

                    divDocumentFileAndLocation2.Visible = True
                    divUploadDocument2.Visible = False
                Else
                    divUploadDocument2.Visible = True
                    divUploadDocument2.Visible = False
                End If
            Case 3
                If strEmploymentDocumentName.Length > 0 Then

                    lnkDocument3.Text = strEmploymentDocumentName
                    litDocumentFileSize3.Text = strFileSize
                    imgFileType3.Src = strFileTypeImageUrl

                    divUploadDocument3.Visible = False
                    divDocumentFileAndLocation3.Visible = True
                Else
                    divUploadDocument3.Visible = True
                    divDocumentFileAndLocation3.Visible = False
                End If
        End Select

        'If all documents 1 through 3 are used, then show the Only 3 Documents are allowed message
        If Not divUploadDocument1.Visible And Not divUploadDocument2.Visible And Not divUploadDocument3.Visible Then
            divUploadDocumentMaxReached.Visible = True
        End If

    End Sub

    Protected Sub addUpdateRecord()
        If Request.QueryString("subID") Is Nothing Then
            'add application registration
            Dim intMemberID As Integer = Integer.MinValue
            If txtMemberID.Text.Length > 0 Then
                intMemberID = Convert.ToInt32(txtMemberID.Text)
            End If

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)

            Dim intSalutationID As Integer = Integer.MinValue
            If ddlSalutation.SelectedValue.Length > 0 Then
                intSalutationID = Convert.ToInt32(ddlSalutation.SelectedValue)
            End If
            Dim strFirstName As String = txtFirstName.Text.Trim()
            Dim strLastName As String = txtLastName.Text.Trim()
            Dim strEmail As String = txtEmailAddress.Text.Trim()

            Dim strPhoneNumber As String = txtPhoneNumber.Text.Trim()

            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry
            Dim pairLocationLatitude As Pair = ucAddress.LocationLatitudeLongitude
            Dim strLocationLatitude As String = pairLocationLatitude.First
            Dim strLocationLongitude As String = pairLocationLatitude.Second

            Dim strCoverLetter As String = txtCoverLetter.Text.Trim()
            Dim strResumeText As String = txtResume.Text.Trim()

            Dim dtStartDate As DateTime = DateTime.MinValue
            If Not startDate.SelectedDate.ToString() = "" Then
                dtStartDate = startDate.SelectedDate
            End If

            Dim strYearsExperience As String = txtYearsExperience.Text.Trim()
            Dim strLastTitle As String = txtLastTitle.Text.Trim()
            Dim strEduLevel As String = txtEduLevel.Text.Trim()
            Dim strProjExpertise As String = txtProjExpertise.Text.Trim()
            Dim strSkills As String = txtSkills.Text.Trim()
            Dim strSalary As String = txtSalary.Text.Trim()


            Dim strIpAddress As String = HttpContext.Current.Request.UserHostAddress
            Dim dtDateTimeStamp As DateTime = DateTime.Now

            Dim intEmploymentSubmissionID As Integer = EmploymentDAL.InsertEmploymentSubmission(intEmploymentID, intMemberID, intSalutationID, strFirstName, strLastName, strEmail, strPhoneNumber, boolStatus, strAddress, strCity, intStateID, strZipCode, intCountryID, strLocationLatitude, strLocationLongitude, strCoverLetter, strResumeText, dtStartDate, strYearsExperience, strLastTitle, strEduLevel, strProjExpertise, strSkills, strSalary, strIpAddress, dtDateTimeStamp)

            'Add employment documents if supplied
            If RadUploadDocument1.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadDocument1.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesEmploymentDocument(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesEmploymentDocument, 0, file.InputStream.Length)
                EmploymentDAL.UpdateEmploymentSubmission_EmploymentSubmissionDocument1_ByEmploymentSubmissionID(intEmploymentSubmissionID, strThumbnailName, bytesEmploymentDocument)

            End If

            If RadUploadDocument2.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadDocument2.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesEmploymentDocument(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesEmploymentDocument, 0, file.InputStream.Length)
                EmploymentDAL.UpdateEmploymentSubmission_EmploymentSubmissionDocument2_ByEmploymentSubmissionID(intEmploymentSubmissionID, strThumbnailName, bytesEmploymentDocument)

            End If

            If RadUploadDocument3.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadDocument3.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesEmploymentDocument(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesEmploymentDocument, 0, file.InputStream.Length)
                EmploymentDAL.UpdateEmploymentSubmission_EmploymentSubmissionDocument3_ByEmploymentSubmissionID(intEmploymentSubmissionID, strThumbnailName, bytesEmploymentDocument)

            End If

        Else
            'update application registration
            Dim intEmploymentSubmissionID As Integer = Convert.ToInt32(Request.QueryString("subID"))

            Dim intMemberID As Integer = Integer.MinValue
            If txtMemberID.Text.Length > 0 Then
                intMemberID = Convert.ToInt32(txtMemberID.Text.Trim())
            End If

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)

            Dim intSalutationID As Integer = Integer.MinValue
            If ddlSalutation.SelectedValue.Length > 0 Then
                intSalutationID = Convert.ToInt32(ddlSalutation.SelectedValue)
            End If
            Dim strFirstName As String = txtFirstName.Text.Trim()
            Dim strLastName As String = txtLastName.Text.Trim()
            Dim strEmail As String = txtEmailAddress.Text.Trim()

            Dim strPhoneNumber As String = txtPhoneNumber.Text.Trim()

            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry
            Dim pairLocationLatitude As Pair = ucAddress.LocationLatitudeLongitude
            Dim strLocationLatitude As String = pairLocationLatitude.First
            Dim strLocationLongitude As String = pairLocationLatitude.Second

            Dim strCoverLetter As String = txtCoverLetter.Text.Trim()
            Dim strResumeText As String = txtResume.Text.Trim()

            Dim dtStartDate As DateTime = DateTime.MinValue
            If Not startDate.SelectedDate.ToString() = "" Then
                dtStartDate = startDate.SelectedDate
            End If

            Dim strYearsExperience As String = txtYearsExperience.Text.Trim()
            Dim strLastTitle As String = txtLastTitle.Text.Trim()
            Dim strEduLevel As String = txtEduLevel.Text.Trim()
            Dim strProjExpertise As String = txtProjExpertise.Text.Trim()
            Dim strSkills As String = txtSkills.Text.Trim()
            Dim strSalary As String = txtSalary.Text.Trim()

            EmploymentDAL.UpdateEmploymentSubmission(intEmploymentSubmissionID, intMemberID, intSalutationID, strFirstName, strLastName, strEmail, strPhoneNumber, boolStatus, strAddress, strCity, intStateID, strZipCode, intCountryID, strLocationLatitude, strLocationLongitude, strCoverLetter, strResumeText, dtStartDate, strYearsExperience, strLastTitle, strEduLevel, strProjExpertise, strSkills, strSalary)

            'Add employment documents if supplied
            If RadUploadDocument1.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadDocument1.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesEmploymentDocument(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesEmploymentDocument, 0, file.InputStream.Length)
                EmploymentDAL.UpdateEmploymentSubmission_EmploymentSubmissionDocument1_ByEmploymentSubmissionID(intEmploymentSubmissionID, strThumbnailName, bytesEmploymentDocument)

            End If

            If RadUploadDocument2.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadDocument2.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesEmploymentDocument(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesEmploymentDocument, 0, file.InputStream.Length)
                EmploymentDAL.UpdateEmploymentSubmission_EmploymentSubmissionDocument2_ByEmploymentSubmissionID(intEmploymentSubmissionID, strThumbnailName, bytesEmploymentDocument)

            End If

            If RadUploadDocument3.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadDocument3.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesEmploymentDocument(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesEmploymentDocument, 0, file.InputStream.Length)
                EmploymentDAL.UpdateEmploymentSubmission_EmploymentSubmissionDocument3_ByEmploymentSubmissionID(intEmploymentSubmissionID, strThumbnailName, bytesEmploymentDocument)

            End If
        End If
    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click
        If IsValid Then
            addUpdateRecord()

            Response.Redirect("Applicants.aspx?employmentID=" & intEmploymentID)
        End If

    End Sub

    Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelButton.Click

        Response.Redirect("Applicants.aspx?employmentID=" & intEmploymentID)

    End Sub

    Protected Sub customValMemberExist_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)

        'If a memberID is added we check it exists
        Dim intMemberID As Integer = Integer.MinValue
        Dim strMemberID As String = txtMemberID.Text.Trim()

        'Only check the member id if it is entered
        If strMemberID.Length > 0 Then
            If Integer.TryParse(strMemberID, intMemberID) Then
                Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intMemberID)
                If dtMember.Rows.Count = 0 Then
                    'MemberID does not exists in our members table
                    e.IsValid = False
                End If
            Else
                'Member ID is not an integer
                e.IsValid = False
            End If
        End If

    End Sub

#Region "Employment Submission Documents"
    Protected Sub lnkDocument1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDocument1.Click
        Dim intSubmissionID As Integer = Convert.ToInt32(Request.QueryString("subID"))

        Dim dtEmploymentSubmission As DataTable = EmploymentDAL.GetEmploymentSubmissions_BySubIDAndSiteID(intSubmissionID, intSiteID)
        If dtEmploymentSubmission.Rows.Count > 0 Then
            Dim drEmploymentSubmission As DataRow = dtEmploymentSubmission.Rows(0)

            Dim strEmploymentDocument1Name As String = drEmploymentSubmission("upload1Name")
            Dim bytesEmploymentDocument1() As Byte = drEmploymentSubmission("upload1")


            'Show download document link
            CommonWeb.DownloadDocument_ByBytes(strEmploymentDocument1Name, bytesEmploymentDocument1)
        End If
    End Sub

    Protected Sub lnkDeleteDocument1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteDocument1.Click
        Dim intSubmissionID As Integer = Convert.ToInt32(Request.QueryString("subID"))
        Dim bytesEmploymentDocument1() As Byte

        EmploymentDAL.UpdateEmploymentSubmission_EmploymentSubmissionDocument1_ByEmploymentSubmissionID(intSubmissionID, String.Empty, bytesEmploymentDocument1)

        'Hide the employment document and show its associated upload control
        divDocumentFileAndLocation1.Visible = False
        divUploadDocument1.Visible = True

        divUploadDocumentMaxReached.Visible = False
    End Sub

    Protected Sub customValDocument1SizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add employment supporting document if it exists, must have a file size less than 5MB, but we give them a 5kb buffer
        If RadUploadDocument1.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = RadUploadDocument1.UploadedFiles(0)
            If file.InputStream.Length > 5500000 Then
                e.IsValid = False
            End If
        End If
    End Sub


    Protected Sub lnkDocument2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDocument2.Click
        Dim intSubmissionID As Integer = Convert.ToInt32(Request.QueryString("subID"))

        Dim dtEmploymentSubmission As DataTable = EmploymentDAL.GetEmploymentSubmissions_BySubIDAndSiteID(intSubmissionID, intSiteID)
        If dtEmploymentSubmission.Rows.Count > 0 Then
            Dim drEmploymentSubmission As DataRow = dtEmploymentSubmission.Rows(0)

            Dim strEmploymentDocument2Name As String = drEmploymentSubmission("upload2Name")
            Dim bytesEmploymentDocument2() As Byte = drEmploymentSubmission("upload2")


            'Show download document link
            CommonWeb.DownloadDocument_ByBytes(strEmploymentDocument2Name, bytesEmploymentDocument2)
        End If
    End Sub

    Protected Sub lnkDeleteDocument2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteDocument2.Click
        Dim intSubmissionID As Integer = Convert.ToInt32(Request.QueryString("subID"))
        Dim bytesEmploymentDocument2() As Byte

        EmploymentDAL.UpdateEmploymentSubmission_EmploymentSubmissionDocument2_ByEmploymentSubmissionID(intSubmissionID, String.Empty, bytesEmploymentDocument2)

        'Hide the employment document and show its associated upload control
        divDocumentFileAndLocation2.Visible = False
        divUploadDocument2.Visible = True

        divUploadDocumentMaxReached.Visible = False
    End Sub

    Protected Sub customValDocument2SizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add employment supporting document if it exists, must have a file size less than 5MB, but we give them a 5kb buffer
        If RadUploadDocument2.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = RadUploadDocument2.UploadedFiles(0)
            If file.InputStream.Length > 5500000 Then
                e.IsValid = False
            End If
        End If
    End Sub


    Protected Sub lnkDocument3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDocument3.Click
        Dim intSubmissionID As Integer = Convert.ToInt32(Request.QueryString("subID"))

        Dim dtEmploymentSubmission As DataTable = EmploymentDAL.GetEmploymentSubmissions_BySubIDAndSiteID(intSubmissionID, intSiteID)
        If dtEmploymentSubmission.Rows.Count > 0 Then
            Dim drEmploymentSubmission As DataRow = dtEmploymentSubmission.Rows(0)

            Dim strEmploymentDocument3Name As String = drEmploymentSubmission("upload3Name")
            Dim bytesEmploymentDocument3() As Byte = drEmploymentSubmission("upload3")


            'Show download document link
            CommonWeb.DownloadDocument_ByBytes(strEmploymentDocument3Name, bytesEmploymentDocument3)
        End If
    End Sub

    Protected Sub lnkDeleteDocument3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteDocument3.Click
        Dim intSubmissionID As Integer = Convert.ToInt32(Request.QueryString("subID"))
        Dim bytesEmploymentDocument3() As Byte

        EmploymentDAL.UpdateEmploymentSubmission_EmploymentSubmissionDocument3_ByEmploymentSubmissionID(intSubmissionID, String.Empty, bytesEmploymentDocument3)

        'Hide the employment document and show its associated upload control
        divDocumentFileAndLocation3.Visible = False
        divUploadDocument3.Visible = True

        divUploadDocumentMaxReached.Visible = False
    End Sub

    Protected Sub customValDocument3SizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add employment supporting document if it exists, must have a file size less than 5MB, but we give them a 5kb buffer
        If RadUploadDocument3.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = RadUploadDocument3.UploadedFiles(0)
            If file.InputStream.Length > 5500000 Then
                e.IsValid = False
            End If
        End If
    End Sub

#End Region
End Class
