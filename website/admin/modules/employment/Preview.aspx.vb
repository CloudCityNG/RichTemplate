
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_employment_preview
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Dim ModuleTypeID As Integer = 4 ' Module Type: Employment

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

    Public Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim strEmploymentID As String = btnCancel.CommandArgument
        Response.Redirect("editAdd.aspx?id=" & strEmploymentID.ToString())
    End Sub

    Public Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("default.aspx")
    End Sub


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Attach a rollback confirmation to our rollback button
        CommonWeb.SetupRollBackButton(btnRollBack, Resources.Employment_Admin.Employment_Preview_RollBack_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadDatePicker(startDate, intSiteID)
        CommonWeb.SetupRadEditor(Page, txtResume, SiteDAL.GetCurrentSiteID_Admin)
        CommonWeb.SetupRadProgressArea(rpDocument1, intSiteID)
        CommonWeb.SetupRadProgressArea(rpDocument2, intSiteID)
        CommonWeb.SetupRadProgressArea(rpDocument3, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadDocument1, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadDocument2, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadDocument3, intSiteID)


        'Set the Header UserControls Title and Help Item if it exists
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Employment_Admin.Employment_Preview_ModuleName)
        ucHeader.PageName = Resources.Employment_Admin.Employment_AddEdit_Header
        ucHeader.PageHelpID = 5 'Help Item for Employment/Job Opportunities

        ' Check our modules Configuration settings
        Dim boolAllowOnlineRegistration As Boolean = False
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "allow_online_registrations" Then
                boolAllowOnlineRegistration = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "use_captcha" Then
                trRadCaptcha_Heading.Visible = True
                trRadCaptcha_Code.Visible = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_registrations" Then
                boolAllowOnlineRegistration = True
            End If
        Next

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, intSiteID) Then
                Response.Redirect("/richadmin")
            End If

            BindSalutationDropDown()
            If Not Request.QueryString("archiveID") Is Nothing Then
                Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

                Dim dtEmploymentArchive As DataTable = EmploymentDAL.GetEmploymentArchive_ByArchiveIDAndSiteID(intArchiveID, intSiteID)
                If dtEmploymentArchive.Rows.Count > 0 Then
                    Dim drEmploymentArchive As DataRow = dtEmploymentArchive.Rows(0)

                    'Set the cancel button to go back to the orginial employment record in question
                    Dim intEmploymentID As Integer = Convert.ToInt32(drEmploymentArchive("employmentID"))

                    'Now we must also check the record that the archive corresponds to actually exists, also as we don't store the images with this record in the archive table, we may also need to load the current images to show with this preview.
                    Dim dtEmployment As DataTable = EmploymentDAL.GetEmployment_ByEmploymentID(intEmploymentID)
                    If dtEmployment.Rows.Count > 0 Then
                        Dim drEmployment As DataRow = dtEmployment.Rows(0)


                        btnCancel.CommandArgument = intEmploymentID.ToString()

                        'Populate the preview pages info box
                        Dim intEmploymentVersion As Integer = Convert.ToInt32(drEmployment("version"))
                        Dim intEmploymentArchiveVersion As Integer = Convert.ToInt32(drEmploymentArchive("version"))

                        'If the archived version is the same as the most recent version, we represent this by prepending text to it saying it is also the current version
                        litInformationBox_Version.Text = intEmploymentArchiveVersion.ToString()
                        If intEmploymentVersion = intEmploymentArchiveVersion Then
                            litInformationBox_Version.Text = Resources.Employment_Admin.Employment_Preview_InformationBox_Version_Current & " (" & litInformationBox_Version.Text & ")"
                        End If

                        litInformationBox_DateCreated.Text = drEmploymentArchive("dateTimeStamp")

                        litInformationBox_AuthorName.Text = Resources.Employment_Admin.Employment_Preview_InformationBox_AuthorDefault
                        If Not drEmploymentArchive("authorID_member") Is DBNull.Value Then
                            Dim intAuthorID_member As Integer = drEmploymentArchive("authorID_member")
                            Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intAuthorID_member)
                            If dtMember.Rows.Count > 0 Then
                                Dim drMember As DataRow = dtMember.Rows(0)

                                Dim strAuthorName As String = drMember("FirstName") & " " & drMember("LastName")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        ElseIf Not drEmploymentArchive("authorID_admin") Is DBNull.Value Then
                            Dim intAuthorID_admin As Integer = drEmploymentArchive("authorID_admin")
                            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAuthorID_admin)
                            If dtAdminUser.Rows.Count > 0 Then
                                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                                Dim strAuthorName As String = drAdminUser("First_Name") & " " & drAdminUser("Last_Name")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        End If

                        Dim strCategoryName As String = Resources.Employment_Admin.Employment_Preview_InformationBox_UnCategorized
                        If Not drEmploymentArchive("CategoryName") Is DBNull.Value Then
                            strCategoryName = drEmploymentArchive("CategoryName")
                        End If
                        litInformationBox_Category.Text = strCategoryName

                        Dim boolStatus As Boolean = Convert.ToBoolean(drEmploymentArchive("Status"))
                        litInformationBox_Status.Text = If(boolStatus, Resources.Employment_Admin.Employment_Preview_InformationBox_StatusActive, Resources.Employment_Admin.Employment_Preview_InformationBox_StatusArchive)

                        If Not drEmploymentArchive("publicationDate").ToString() = "" Then
                            litInformationBox_PublicationDate.Text = drEmploymentArchive("publicationDate").ToString()
                            divInformationBox_PublicationDate.Visible = True
                        End If

                        If Not drEmploymentArchive("expirationDate").ToString() = "" Then
                            Dim dtExpirationDate As DateTime = Convert.ToDateTime(drEmploymentArchive("expirationDate"))
                            litInformationBox_ExpirationDate.Text = dtExpirationDate.ToString()
                            divInformationBox_ExpirationDate.Visible = True

                            If dtExpirationDate < DateTime.Now Then
                                divExpired.Visible = True
                            End If

                        End If
                        If Not drEmploymentArchive("Summary") Is DBNull.Value Then
                            litInformationBox_Summary.Text = drEmploymentArchive("Summary")
                            divInformationBox_Summary.Visible = True
                        End If


                        'Populate the preview page such that it is similar to the front-end version
                        'Check if the employment is supposed to direct to an external url
                        Dim strExternalLinkUrl As String = ""
                        If Not drEmploymentArchive("externalLinkUrl") Is DBNull.Value AndAlso drEmploymentArchive("externalLinkUrl").ToString().Trim.Length > 0 Then
                            litExternalURL.Text = drEmploymentArchive("externalLinkUrl").ToString()
                            divExternalEmployment.Visible = True

                        End If

                        'set the title and body
                        Dim strTitle As String = drEmploymentArchive("Title")
                        litTitle.Text = strTitle

                        Dim strBody As String = drEmploymentArchive("Body")
                        litBody.Text = strBody

                        Dim strSalaryRange As String = ""
                        If Not drEmploymentArchive("SalaryMin") Is DBNull.Value Then
                            'we have a value for the minimum salary
                            If Not drEmploymentArchive("SalaryMax") Is DBNull.Value Then
                                'we also have a value for max salary so show both min and max salary
                                strSalaryRange = (Convert.ToDecimal(drEmploymentArchive("SalaryMin")).ToString("C")).Replace(".00", "") & " - " & (Convert.ToDecimal(drEmploymentArchive("SalaryMax")).ToString("C")).Replace(".00", "") & " "
                            Else
                                'we don't have a value for max salary, so we only show the min salary
                                strSalaryRange = (Convert.ToDecimal(drEmploymentArchive("SalaryMin")).ToString("C")).Replace(".00", "") & " " & Resources.Employment_Admin.Employment_Preview_Salary_Above & " "
                            End If

                        Else
                            'we do not have a minimum salary
                            If Not drEmploymentArchive("SalaryMax") Is DBNull.Value Then
                                'we do have a max salary, so just show the max salaray
                                strSalaryRange = Resources.Employment_Admin.Employment_Preview_Salary_Below & " " & (Convert.ToDecimal(drEmploymentArchive("SalaryMax")).ToString("C")).Replace(".00", "") & " "
                            Else
                                'we have neither a min nor a max, so show nothing
                            End If
                        End If

                        'Check if we have any notes for the salary
                        If Not drEmploymentArchive("SalaryNote") Is DBNull.Value Then
                            strSalaryRange = strSalaryRange & drEmploymentArchive("SalaryNote").ToString()
                        End If

                        'Finally if we have a salaryRange with greater than 0 length pre-pend 'Salary: '
                        If strSalaryRange.Length > 0 Then
                            litSalaryRange.Text = strSalaryRange
                            divSalaryRange.Visible = True
                        End If

                        'Check we need to show the clearance required
                        If Not drEmploymentArchive("clearance") Is DBNull.Value AndAlso drEmploymentArchive("clearance").ToString().Trim().Length > 0 Then
                            litClearance.Text = drEmploymentArchive("clearance").ToString()
                            divClearance.Visible = True
                        End If

                        'Check we need to show the contact person
                        If Not drEmploymentArchive("contactPerson") Is DBNull.Value AndAlso drEmploymentArchive("contactPerson").ToString().Trim().Length > 0 Then
                            Dim strContactPerson As String = drEmploymentArchive("contactPerson").ToString()
                            litContactPerson.Text = "<a href='mailto:" & strContactPerson & "'>" & strContactPerson & "</a>"
                            divContactPerson.Visible = True
                        End If

                        'Show location if entered
                        If Not IsDBNull(drEmploymentArchive("Address")) Then
                            litLocation.Text = drEmploymentArchive("Address").ToString() & "<br/>"
                            If Not IsDBNull(drEmploymentArchive("City")) Then
                                litLocation.Text += drEmploymentArchive("City").ToString() & ", "
                            End If
                            If Not IsDBNull(drEmploymentArchive("stateName")) Then
                                litLocation.Text += drEmploymentArchive("stateName").ToString() & " "
                            End If
                            If Not IsDBNull(drEmploymentArchive("ZipCode")) Then
                                litLocation.Text += drEmploymentArchive("ZipCode").ToString()
                            End If
                            If Not IsDBNull(drEmploymentArchive("CountryName")) Then
                                litLocation.Text += "<br/>" & drEmploymentArchive("CountryName").ToString()
                            End If
                            litLocation.Visible = True
                            divLocation.Visible = True
                        End If


                        If Not IsDBNull(drEmploymentArchive("geoLocation")) Then
                            If drEmploymentArchive("geolocation") = True Then
                                Dim latitude As String = ""
                                Dim longitude As String = ""
                                If Not IsDBNull(drEmploymentArchive("locationLatitude")) Then
                                    latitude = drEmploymentArchive("locationLatitude")
                                End If
                                If Not IsDBNull(drEmploymentArchive("locationLongitude")) Then
                                    longitude = drEmploymentArchive("locationLongitude")
                                End If
                                If latitude.Length > 0 And longitude.Length > 0 Then
                                    ucGoogleMap.Latitude = latitude
                                    ucGoogleMap.Longitude = longitude
                                    ucGoogleMap.ZoomLevel = 15
                                End If

                            End If
                        End If

                        'set the author name and posted by date
                        Dim strAuthorUsername As String = Resources.Employment_Admin.Employment_Preview_PostedByDefault
                        If Not drEmploymentArchive("author_username") Is DBNull.Value Then
                            strAuthorUsername = drEmploymentArchive("author_username").ToString()
                        End If
                        litPostedBy.Text = strAuthorUsername

                        Dim dtViewDate As DateTime = Convert.ToDateTime(drEmploymentArchive("viewDate"))
                        litEmploymentDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDateTime.Text = dtViewDate.ToString("h:mm tt")



                        'Load in this employment records search tags
                        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intEmploymentID)
                        If dtSearchTags.Rows.Count > 0 Then
                            divModuleSearchTagList.Visible = True
                            rptSearchTags.DataSource = dtSearchTags
                            rptSearchTags.DataBind()
                        End If


                        If (boolStatus) AndAlso (boolAllowOnlineRegistration) AndAlso (Not IsDBNull(drEmploymentArchive("onlineSignup"))) Then
                            If drEmploymentArchive("onlineSignup") = True Then
                                signUpPanel.Visible = True
                            End If
                        End If

                    Else
                        Response.Redirect("default.aspx")
                    End If
                Else
                    Response.Redirect("default.aspx")
                End If
            Else
                Response.Redirect("default.aspx")
            End If

        End If

    End Sub

    Protected Sub btn_Signup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Signup.Click
        If Page.IsValid Then
            addSignupInformation()
        End If
    End Sub

    Private Sub addSignupInformation()
        'set the return to Employment Opportunity Link
        Dim intArchiveID As Integer = Request.QueryString("archiveID").ToString()
        aReturnToEmploymentDetail.HRef = "?archiveID=" & intArchiveID

        'hide/show appropriate divs/panels
        divEmploymentItem.Visible = False
        signUpPanel.Visible = False
        confirmationPanel.Visible = True


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

    Public Sub btnRollBack_OnClick(ByVal sender As Object, ByVal e As EventArgs)

        If Not Request.QueryString("archiveID") Is Nothing Then

            Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

            EmploymentDAL.RollbackEmployment(intArchiveID)
            pnlConfirmation.Visible = True
            pnlPreview.Visible = False
        End If
    End Sub

End Class
