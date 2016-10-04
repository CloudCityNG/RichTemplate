Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports Telerik.Web.UI
Imports Subgurim.Controles
Imports System.Xml

Partial Class Employment_EmploymentDetail
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 4

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            'Setup Rad Controls
            CommonWeb.SetupRadDatePicker(startDate, intSiteID)
            CommonWeb.SetupRadEditor(Me.Page, txtResume, "~/editorConfig/toolbars/ToolsFileFrontEnd.xml", intSiteID)
            CommonWeb.SetupRadProgressArea(rpDocument1, intSiteID)
            CommonWeb.SetupRadProgressArea(rpDocument2, intSiteID)
            CommonWeb.SetupRadProgressArea(rpDocument3, intSiteID)
            CommonWeb.SetupRadUpload(RadUploadDocument1, intSiteID)
            CommonWeb.SetupRadUpload(RadUploadDocument2, intSiteID)
            CommonWeb.SetupRadUpload(RadUploadDocument3, intSiteID)

            If Not IsPostBack Then
                BindSalutationDropDown()
            End If

            If Not Request.QueryString("id") Is Nothing Then
                Dim intEmploymentID As Integer = Convert.ToInt32(Request.QueryString("id"))
                LoadEmployment(intEmploymentID)
            End If
        End If

    End Sub

    Protected Sub LoadEmployment(ByVal intEmploymentID As Integer)
        Dim boolStatus As Boolean = True
        Dim boolAllowArchive As Boolean = False
        Dim boolAllowComments As Boolean = False
        Dim boolEnablePublicCommentSubmission As Boolean = False
        Dim boolAllowOnlineSubmissions As Boolean = False
        Dim boolAllowOnlineRegistration As Boolean = False
        Dim boolEnableGroupsAndUserAccess As Boolean = False

        If Request.QueryString("archive") = 1 Then
            boolStatus = False
        End If

        ' Check our modules Configuration settings
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                boolAllowArchive = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "show_add_this" Then
                plcAddThis.Visible = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_submissions" Then
                boolAllowOnlineSubmissions = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_registrations" Then
                boolAllowOnlineRegistration = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "use_captcha" Then
                trRadCaptcha_Heading.Visible = True
                trRadCaptcha_Code.Visible = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" Then
                boolAllowComments = True
            ElseIf drModuleConfig("fieldName").ToString.ToLower() = "enable_public_comment_submission" Then
                boolEnablePublicCommentSubmission = True
            End If
        Next

        'If we find out the employment is an archived employment, but we do not allow achives then redirect to default page
        If boolAllowArchive = False And boolStatus = False Then
            Response.Redirect("default.aspx")
        End If

        'Load in this employments search tags
        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intEmploymentID)
        If dtSearchTags.Rows.Count > 0 Then
            divModuleSearchTagList.Visible = True
            rptSearchTags.DataSource = dtSearchTags
            rptSearchTags.DataBind()
        End If

        'Load the Employment by ID
        Dim dtEmployment As DataTable = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmployment_ByEmploymentIDAndStatusAndAccess_FrontEnd(intEmploymentID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByEmploymentIDAndStatus_FrontEnd(intEmploymentID, boolStatus, intSiteID))
        If dtEmployment.Rows.Count > 0 Then
            Dim drEmployment As DataRow = dtEmployment.Rows(0)

            'Check if the employment record is supposed to direct to an external url
            Dim strExternalLinkUrl As String = ""
            If Not drEmployment("externalLinkUrl") Is DBNull.Value AndAlso drEmployment("externalLinkUrl").ToString().Trim.Length > 0 Then
                Response.Redirect(drEmployment("externalLinkUrl").ToString())
            End If



            'set the title and body
            Dim strTitle As String = drEmployment("Title")

            'Set the page title Default
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Employment_FrontEnd.Employment_EmploymentDetail_HeaderTitle + " - " + strTitle

            litTitle.Text = strTitle

            Dim strBody As String = drEmployment("Body")
            litBody.Text = strBody

            Dim strSalaryRange As String = ""
            If Not drEmployment("SalaryMin") Is DBNull.Value Then
                'we have a value for the minimum salary
                If Not drEmployment("SalaryMax") Is DBNull.Value Then
                    'we also have a value for max salary so show both min and max salary
                    strSalaryRange = (Convert.ToDecimal(drEmployment("SalaryMin")).ToString("C")).Replace(".00", "") & " - " & (Convert.ToDecimal(drEmployment("SalaryMax")).ToString("C")).Replace(".00", "") & " "
                Else
                    'we don't have a value for max salary, so we only show the min salary
                    strSalaryRange = (Convert.ToDecimal(drEmployment("SalaryMin")).ToString("C")).Replace(".00", "") & " " & Resources.Employment_FrontEnd.Employment_EmploymentDetail_Salary_Above & " "
                End If

            Else
                'we do not have a minimum salary
                If Not drEmployment("SalaryMax") Is DBNull.Value Then
                    'we do have a max salary, so just show the max salaray
                    strSalaryRange = Resources.Employment_FrontEnd.Employment_EmploymentDetail_Salary_Below & " " & (Convert.ToDecimal(drEmployment("SalaryMax")).ToString("C")).Replace(".00", "") & " "
                Else
                    'we have neither a min nor a max, so show nothing
                End If
            End If

            'Check if we have any notes for the salary
            If Not drEmployment("SalaryNote") Is DBNull.Value Then
                strSalaryRange = strSalaryRange & drEmployment("SalaryNote").ToString()
            End If

            'Finally if we have a salaryRange with greater than 0 length pre-pend 'Salary: '
            If strSalaryRange.Length > 0 Then
                litSalaryRange.Text = strSalaryRange
                divSalaryRange.Visible = True
            End If

            'Check we need to show the clearance required
            If Not drEmployment("clearance") Is DBNull.Value AndAlso drEmployment("clearance").ToString().Trim().Length > 0 Then
                litClearance.Text = drEmployment("clearance").ToString()
                divClearance.Visible = True
            End If

            'Check we need to show the contact person
            If Not drEmployment("contactPerson") Is DBNull.Value AndAlso drEmployment("contactPerson").ToString().Trim().Length > 0 Then
                Dim strContactPerson As String = drEmployment("contactPerson").ToString()
                litContactPerson.Text = "<a href='mailto:" & strContactPerson & "'>" & strContactPerson & "</a>"
                divContactPerson.Visible = True
            End If

            'Show location if entered
            Dim strAddress As String = ""
            If Not drEmployment("Address") Is DBNull.Value Then
                strAddress = drEmployment("Address").ToString().Trim()
            End If
            Dim strCity As String = ""
            If Not drEmployment("City") Is DBNull.Value Then
                strCity = drEmployment("City").ToString().Trim()
            End If
            Dim intStateID As Integer = Integer.MinValue
            If Not drEmployment("StateID") Is DBNull.Value Then
                intStateID = Convert.ToInt32(drEmployment("StateID"))
            End If
            Dim strZipCode As String = ""
            If Not drEmployment("ZipCode") Is DBNull.Value Then
                strZipCode = drEmployment("ZipCode").ToString().Trim()
            End If
            Dim intCountryID As Integer = Integer.MinValue
            If Not drEmployment("CountryID") Is DBNull.Value Then
                intCountryID = Convert.ToInt32(drEmployment("CountryID"))
            End If
            Dim strHtmlLocationInformation As String = CommonWeb.FormatAddress(strAddress, strCity, intStateID, strZipCode, intCountryID, CommonWeb.AddressFormat.MultipleLinesForHTML)
            If strHtmlLocationInformation.Length > 0 Then

                litLocation.Text = strHtmlLocationInformation
                divLocation.Visible = True
            End If


            If Not IsDBNull(drEmployment("geoLocation")) Then
                If drEmployment("geolocation") = True Then
                    Dim latitude As String = ""
                    Dim longitude As String = ""
                    If Not IsDBNull(drEmployment("locationLatitude")) Then
                        latitude = drEmployment("locationLatitude")
                    End If
                    If Not IsDBNull(drEmployment("locationLongitude")) Then
                        longitude = drEmployment("locationLongitude")
                    End If
                    If latitude.Length > 0 And longitude.Length > 0 Then
                        ucGoogleMap.Latitude = latitude
                        ucGoogleMap.Longitude = longitude
                        ucGoogleMap.ZoomLevel = 15

                        'So we have latitude and longitude, so show the google map div
                        divGoogleMap.Visible = True
                    End If

                End If
            End If

            If (boolStatus) AndAlso (boolAllowOnlineRegistration) AndAlso (Not IsDBNull(drEmployment("onlineSignup"))) Then
                If drEmployment("onlineSignup") = True Then
                    signUpPanel.Visible = True
                End If
            End If


            'set the author name and posted by date
            Dim strAuthorUsername As String = Resources.Employment_FrontEnd.Employment_EmploymentDetail_PostedByDefault
            If Not drEmployment("author_username") Is DBNull.Value Then
                strAuthorUsername = drEmployment("author_username").ToString()
            End If
            litPostedBy.Text = strAuthorUsername


            Dim dtViewDate As DateTime = Convert.ToDateTime(drEmployment("viewDate"))
            litEmploymentDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
            litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
            litViewDateTime.Text = dtViewDate.ToString("h:mm tt")

            'Finally show the edit link if the employment was uploaded by the currently logged in user
            If boolAllowOnlineSubmissions Then
                If Not drEmployment("authorID_member") Is DBNull.Value Then
                    Dim intAuthorID_Member As Integer = Convert.ToInt32(drEmployment("authorID_member"))
                    If intMemberID > 0 And intMemberID = intAuthorID_Member Then
                        divEditEmployment.Visible = True
                    End If

                End If
            End If

            'set the page title
            If Not drEmployment("metaTitle") Is DBNull.Value Then
                Dim strMetaTitle As String = drEmployment("metaTitle").ToString()
                If strMetaTitle.Length > 0 Then
                    Page.Header.Title = strMetaTitle
                End If
            End If

            'set the page's keywords

            If Not drEmployment("metaKeywords") Is DBNull.Value Then
                Dim keywordsHtmlMeta As New HtmlMeta()
                keywordsHtmlMeta.Name = "keywords"
                keywordsHtmlMeta.Content = drEmployment("metaKeywords").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(keywordsHtmlMeta)
            End If

            'set the page's description
            If Not drEmployment("metaDescription") Is DBNull.Value Then
                Dim descriptionHtmlMeta As New HtmlMeta()
                descriptionHtmlMeta.Name = "description"
                descriptionHtmlMeta.Content = drEmployment("metaDescription").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(descriptionHtmlMeta)
            End If

            If boolAllowComments Then
                'setup the comments list, if we have enabled public comment submissions, then the member does not require to login to post a comment
                Dim intSiteID_ForEmployment As Integer = Convert.ToInt32(drEmployment("siteID"))
                ucCommentsModule.SetupCommentModule(intSiteID_ForEmployment, ModuleTypeID, intEmploymentID, intMemberID, boolStatus, boolEnablePublicCommentSubmission)
            End If
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub btn_Signup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Signup.Click
        If Page.IsValid Then
            addSignupInformation()
        End If
    End Sub

    Private Sub addSignupInformation()
        'Insert Employment Submission
        Dim intEmploymentID As Integer = Convert.ToInt32(Request.QueryString("id"))

        Dim intSalutationID As Integer = Integer.MinValue
        If ddlSalutation.SelectedValue.Length > 0 Then
            intSalutationID = Convert.ToInt32(ddlSalutation.SelectedValue)
        End If
        Dim strFirstName As String = txtFirstName.Text.Trim()
        Dim strLastName As String = txtLastName.Text.Trim()
        Dim strEmail As String = txtEmailAddress.Text.Trim()

        Dim strPhoneNumber As String = txtPhoneNumber.Text.Trim()

        If intMemberID = 0 Then
            intMemberID = Integer.MinValue
        End If

        Dim strAddress As String = ucAddress.LocationStreet
        Dim strCity As String = ucAddress.LocationCity
        Dim intStateID As Integer = ucAddress.LocationState
        Dim strZipCode As String = ucAddress.LocationZipCode
        Dim intCountryID As Integer = ucAddress.LocationCountry
        Dim pairLocationLatitude As Pair = ucAddress.LocationLatitudeLongitude
        Dim strLocationLatitude As String = pairLocationLatitude.First
        Dim strLocationLongitude As String = pairLocationLatitude.Second

        Dim boolStatus As Boolean = True ' default to true

        Dim strCoverLetter As String = txtCoverLetter.Text.Trim()
        Dim strResume As String = txtResume.Text.Trim()

        Dim dtStartDate As DateTime = DateTime.MinValue
        If Not startDate.SelectedDate.ToString() = "" Then
            dtStartDate = startDate.SelectedDate
        End If

        Dim strYearsExperience As String = txtYearsExperience.Text.Trim()
        Dim strLastTitle As String = txtLastTitle.Text.Trim()
        Dim strEduLevel As String = txtEducationLevel.Text.Trim()
        Dim strProjExpertise As String = txtProjExpertise.Text.Trim()
        Dim strSkills As String = txtSkills.Text.Trim()
        Dim strSalary As String = txtSalary.Text.Trim()

        Dim strIpAddress As String = HttpContext.Current.Request.UserHostAddress
        Dim dtDateTimeStamp As DateTime = DateTime.Now

        Dim intEmploymentSubmissionID As Integer = EmploymentDAL.InsertEmploymentSubmission(intEmploymentID, intMemberID, intSalutationID, strFirstName, strLastName, strEmail, strPhoneNumber, boolStatus, strAddress, strCity, intStateID, strZipCode, intCountryID, strLocationLatitude, strLocationLongitude, strCoverLetter, strResume, dtStartDate, strYearsExperience, strLastTitle, strEduLevel, strProjExpertise, strSkills, strSalary, strIpAddress, dtDateTimeStamp)

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

        'set the return to Employment Opportunity Link
        aReturnToEmploymentDetail.HRef = "?id=" & intEmploymentID

        'hide/show appropriate divs/panels
        divEmploymentItem.Visible = False
        signUpPanel.Visible = False
        confirmationPanel.Visible = True

        'send the confirmation email
        sendConfirmation(strEmail)
    End Sub

    Public Sub sendConfirmation(ByVal email As String)

        'Use EmailDAL to send confirmation email
        If Not Request.QueryString("id") Is Nothing Then
            Dim employmentID As String = Request.QueryString("id")
            Dim dtEmployment As DataTable = EmploymentDAL.GetEmployment_ByEmploymentID(employmentID)
            If dtEmployment.Rows.Count > 0 Then
                Dim drEmployment As DataRow = dtEmployment.Rows(0)

                'SEND EMAIL CONFIRMATION TO APPLICANT
                Dim EmailSwapoutData_Applicant As New Hashtable()

                'Add the first/last name to our email to the applicant
                Dim strSalutation As String = String.Empty
                If ddlSalutation.SelectedValue.Length > 0 Then
                    strSalutation = ddlSalutation.SelectedItem.Text
                End If
                EmailSwapoutData_Applicant("Salutation") = strSalutation
                EmailSwapoutData_Applicant("FirstName") = txtFirstName.Text.Trim()
                EmailSwapoutData_Applicant("LastName") = txtLastName.Text.Trim()

                'Add the job title (used by both text and html versions)
                EmailSwapoutData_Applicant("EmploymentTitle") = drEmployment("Title")

                'Populate a list of recipient 'to' addresses
                Dim listRecipientEmailAddresses_Applicant As New ArrayList()
                listRecipientEmailAddresses_Applicant.Add(email)

                'Send this information to our email DAL with EmailTypeID = 1 -> employment confimation to applicant
                EmailDAL.SendEmail(listRecipientEmailAddresses_Applicant, 1, intSiteID, EmailSwapoutData_Applicant)

                'Send Email to HR
                'Create our swapout hashtable
                Dim EmailSwapoutData_Administrator As New Hashtable()

                'Add the Employment Title to our email swapout data
                EmailSwapoutData_Administrator("Salutation") = strSalutation
                EmailSwapoutData_Administrator("FirstName") = txtFirstName.Text.Trim()
                EmailSwapoutData_Administrator("LastName") = txtLastName.Text.Trim()
                EmailSwapoutData_Administrator("EmploymentTitle") = drEmployment("Title").ToString()




                Dim strAddress As String = ""
                If Not drEmployment("Address") Is DBNull.Value Then
                    strAddress = drEmployment("Address").ToString().Trim()
                End If
                Dim strCity As String = ""
                If Not drEmployment("City") Is DBNull.Value Then
                    strCity = drEmployment("City").ToString().Trim()
                End If
                Dim intStateID As Integer = Integer.MinValue
                If Not drEmployment("StateID") Is DBNull.Value Then
                    intStateID = Convert.ToInt32(drEmployment("StateID"))
                End If
                Dim strZipCode As String = ""
                If Not drEmployment("ZipCode") Is DBNull.Value Then
                    strZipCode = drEmployment("ZipCode").ToString().Trim()
                End If
                Dim intCountryID As Integer = Integer.MinValue
                If Not drEmployment("CountryID") Is DBNull.Value Then
                    intCountryID = Convert.ToInt32(drEmployment("CountryID"))
                End If

                'Add Location to the TextLocationInformation only used in the bodyText version
                Dim strTextLocationInformation As String = CommonWeb.FormatAddress(strAddress, strCity, intStateID, strZipCode, intCountryID, CommonWeb.AddressFormat.MultipleLines)
                EmailSwapoutData_Administrator("TextLocationInformation") = strTextLocationInformation

                'Add Location to the HtmlLocationInformation only used in the bodyHtml version
                Dim strHtmlLocationInformation As String = CommonWeb.FormatAddress(strAddress, strCity, intStateID, strZipCode, intCountryID, CommonWeb.AddressFormat.MultipleLinesForHTML)
                EmailSwapoutData_Administrator("HtmlLocationInformation") = strHtmlLocationInformation


                'Also if this employment row, invovles an additional contact person we will also send them the same email we will send to the administrator
                Dim listRecipientEmailAddresses_Administrator As New ArrayList()
                If Not drEmployment("ContactPerson") Is DBNull.Value Then
                    Dim strContactPerson As String = drEmployment("ContactPerson").ToString()
                    If strContactPerson.Length > 0 Then
                        listRecipientEmailAddresses_Administrator.Add(strContactPerson)
                    End If
                End If

                'Send our Email using the EmailDAL using Employment Application to Administrator Email Type (ID=2) -> This is an Administrator Email, but we also want to send this Administrator Email to the contact person
                EmailDAL.SendEmail(listRecipientEmailAddresses_Administrator, 2, intSiteID, EmailSwapoutData_Administrator)
            End If


        End If


    End Sub

#Region "Employment Submission Documents"

    Protected Sub customValDocument1SizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add employment supporting document if it exists, must have a file size less than 5MB, but we give them a 5kb buffer
        If RadUploadDocument1.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = RadUploadDocument1.UploadedFiles(0)
            If file.InputStream.Length > 5500000 Then
                e.IsValid = False
            End If
        End If
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

    Protected Sub customValDocument3SizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add employment supporting document if it exists, must have a file size less than 5MB, but we give them a 5kb buffer
        If RadUploadDocument3.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = RadUploadDocument3.UploadedFiles(0)
            If file.InputStream.Length > 5500000 Then
                e.IsValid = False
            End If
        End If
    End Sub

    Protected Sub customValResumeOrDocument_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'To add an employment submission you must provide atleast either a resume in text form or as a file
        If (txtResume.Text.Trim().Length = 0) And (RadUploadDocument1.UploadedFiles.Count = 0 And RadUploadDocument2.UploadedFiles.Count = 0 And RadUploadDocument3.UploadedFiles.Count = 0) Then
            e.IsValid = False
        End If

    End Sub

#End Region
End Class
