Imports System.Data
Imports Telerik.Web.UI
Imports System.Xml

Partial Class Member_UpdateProfile
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Private _memberHomePage As String
    Public ReadOnly Property MemberHomepage() As String
        Get
            If _memberHomePage = "" Then
                _memberHomePage = WebInfoDAL.GetWebInfo_FirstSecurePageLinkURL_MemberSection()
            End If
            Return _memberHomePage
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Setup Rad Controls
        CommonWeb.SetupRadProgressArea(RadProgressAreaMember, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadMemberImage, intSiteID)

        Page.Header.Title = SiteDAL.GetCompanyName() & " | " & Resources.Member_FrontEnd.Member_UpdateProfile_HeaderTitle
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Member_FrontEnd.Member_UpdateProfile_Heading)

        If Not Page.IsPostBack Then

            If intMemberID > 0 Then
                Dim userPassword As String = Replace(Me.txtPwd.Text, "'", "''")

                Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID_WithThumbnail(intMemberID)
                If dtMember.Rows.Count > 0 Then
                    Dim drMember As DataRow = dtMember.Rows(0)

                    Dim boolMemberImportedFromActiveDirectory As Boolean = False
                    If ((Not drMember("ActiveDirectory_Identifier") Is Nothing) AndAlso (drMember("ActiveDirectory_Identifier").ToString().Trim().Length > 0)) Then
                        boolMemberImportedFromActiveDirectory = True
                    End If

                    'Bind the list of languages and security questions
                    BindSalutationDropDown()
                    BindLanguageDropDown()
                    BindSecurityQuestionsListData()


                    'Load currently logged in user
                    If Not drMember("SalutationID") Is DBNull.Value Then
                        Me.ddlSalutation.SelectedValue = drMember("SalutationID").ToString()
                    End If
                    FirstName.Text = drMember("firstName").ToString
                    LastName.Text = drMember("lastName").ToString
                    email.Text = drMember("email").ToString

                    If Not drMember("CompanyOffice").ToString() = "" Then
                        txtOffice.Text = drMember("CompanyOffice").ToString()
                    End If

                    If Not drMember("Address").ToString() = "" Then
                        ucAddress.LocationStreet = drMember("Address").ToString()
                    End If

                    If Not drMember("City").ToString() = "" Then
                        ucAddress.LocationCity = drMember("City").ToString()
                    End If

                    If Not drMember("StateID").ToString() = "" Then
                        ucAddress.LocationState = drMember("StateID").ToString()
                    End If

                    If Not drMember("ZipCode").ToString() = "" Then
                        ucAddress.LocationZipCode = drMember("ZipCode").ToString()
                    End If

                    If Not drMember("CountryID").ToString() = "" Then
                        ucAddress.LocationCountry = drMember("CountryID").ToString()

                    End If
                    ddlLanguage.SelectedValue = drMember("languageID").ToString()

                    daytimePhone.Text = drMember("daytimePhone").ToString
                    eveningPhone.Text = drMember("eveningPhone").ToString
                    ddlsecurityQuestion.SelectedValue = drMember("securityQuestion").ToString
                    securityAnswer.Text = drMember("securityAnswer").ToString

                    If Not drMember("Company").ToString() = "" Then
                        txtCompany.Text = drMember("Company").ToString()
                    End If

                    If Not drMember("CompanyDepartment").ToString() = "" Then
                        txtDepartment.Text = drMember("CompanyDepartment").ToString()
                    End If

                    If Not drMember("JobTitle").ToString() = "" Then
                        txtTitle.Text = drMember("JobTitle").ToString()
                    End If

                    'Handle the members image
                    Me.memberImage.Visible = False
                    Me.lnkDeleteImage.Visible = False
                    If Not drMember("thumbnail") Is DBNull.Value Then
                        If Not drMember("thumbnail").ToString() = "" Then

                            Me.memberImage.DataValue = drMember("thumbnail")
                            Me.memberImage.Visible = True
                            Me.lnkDeleteImage.Visible = True
                        End If
                    End If

                    'Finally if this member is an active directory member, then set some fields as readonly
                    If boolMemberImportedFromActiveDirectory Then
                        SetMemberReadOnlyActiveDirectoryFields()
                    End If
                Else
                    Response.Redirect("default.aspx")
                End If
            Else
                Response.Redirect("default.aspx")
            End If

        End If

    End Sub

    Private Sub SetMemberReadOnlyActiveDirectoryFields()
        'For all input fields that are stored in active directory, hide their OUTER DIVS and show literals in their place
        divActiveDirectoryMemberMessage.Visible = True

        'Not only do we make salutation read-only, we set hide the entire salutation label and value TR tags, as active directory does not return us a salutation ALSO CLEAR THE Salutation Dropdownvalues, so we get null for updates
        'Sets the salutation to read-only, use this if active directory pulls the members salutation.
        divSalutation.Visible = False
        litSalutation.Text = ddlSalutation.SelectedItem.Text
        divSalutation_ReadOnly.Visible = True

        ' NOTE: if active directory does return salutation, then comment the next 2 lines out!
        trSalutation.Visible = False
        ddlSalutation.Items.Clear()

        divFirstName.Visible = False
        litFirstName.Text = FirstName.Text
        divFirstName_ReadOnly.Visible = True

        divLastName.Visible = False
        litLastName.Text = LastName.Text
        divLastName_ReadOnly.Visible = True

        divEmailAddress.Visible = False
        litEmailAddress.Text = email.Text
        divEmailAddress_ReadOnly.Visible = True

        trPasswordNew.Visible = False
        trPasswordConfirm.Visible = False
        trSecurityQuestion.Visible = False
        trSecurityAnswer.Visible = False

        divOffice.Visible = False
        litOffice.Text = txtOffice.Text
        divOffice_ReadOnly.Visible = True

        ucAddress.MakeReadOnly()

        divDaytimePhone.Visible = False
        litDaytimePhone.Text = daytimePhone.Text
        divDaytimePhone_ReadOnly.Visible = True

        divCompany.Visible = False
        litCompany.Text = txtCompany.Text
        divCompany_ReadOnly.Visible = True

        divDepartment.Visible = False
        litDepartment.Text = txtDepartment.Text
        divDepartment_ReadOnly.Visible = True

        divTitle.Visible = False
        litTitle.Text = txtTitle.Text
        divTitle_ReadOnly.Visible = True

    End Sub

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

    Private Sub BindLanguageDropDown()

        'Populate the possible languages our site supports
        Dim dtLanguage As DataTable = LanguageDAL.GetLanguageList()
        ddlLanguage.Items.Clear()
        ddlLanguage.DataValueField = "ID"
        ddlLanguage.DataTextField = "Language"
        ddlLanguage.DataSource = dtLanguage
        ddlLanguage.DataBind()

        ddlLanguage.Items.Insert(0, New ListItem("--" & Resources.Member_FrontEnd.Member_UpdateProfile_Language_PleaseSelect & "--", ""))
    End Sub

    Public Sub BindSecurityQuestionsListData()
        'note the data value is not the id, as we want to store the actual security question TEXT that was selected by the user
        'So if the admin changes the security question for a particular ID, the user will still be answering the SAME security question TEXT that they last created
        'As opposed to answering a sercurity question that is based on the question ID where the question text for this id may change, hence confuse the user
        Dim dtMemberSecurityQuestions As DataTable = MemberDAL.GetMemberSecurityQuestionsList()
        ddlsecurityQuestion.DataSource = dtMemberSecurityQuestions
        ddlsecurityQuestion.DataTextField = "SecurityQuestion"
        ddlsecurityQuestion.DataValueField = "SecurityQuestion"
        ddlsecurityQuestion.DataBind()
    End Sub

    Protected Sub btnUpdateProfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateProfile.Click

        If IsValid Then
            UpdateProfile()
        End If


    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'First find the first siteID this user can access, by getting a list of all sites the member can access, and either if this is in the list, return this siteID, else return the first in the list
        Dim intSiteID_ToLoginFirst As Integer = Integer.MinValue

        'Finally check this member has access to the current site, by checking if the site access contains ZERO for ALL SITES, or check the site access contains the current Site
        Dim strSiteAccessList As String = MemberDAL.GetCurrentMemberSiteIDs()
        If strSiteAccessList.Length > 0 Then
            Dim listSiteAccessList As String() = strSiteAccessList.Split(",")
            If listSiteAccessList.Contains("0") Or listSiteAccessList.Contains(intSiteID) Then
                intSiteID_ToLoginFirst = intSiteID
            Else
                'Get the first site id in the list
                intSiteID_ToLoginFirst = Convert.ToInt32(listSiteAccessList(0))
            End If

            'There is already a member logged in, so redirect them to the first public page for their site, by getting their first
            Dim strHomePage As String = WebInfoDAL.GetWebInfo_HomePage(intSiteID_ToLoginFirst)
            Response.Redirect(strHomePage)
        End If
    End Sub

    Protected Sub UpdateProfile()

        If intMemberID > 0 Then
            Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intMemberID)
            If dtMember.Rows.Count > 0 Then
                Dim drMember As DataRow = dtMember.Rows(0)


                'Previous Values from the current member, that we use for the update
                Dim strGroupIDs As String = drMember("GroupID").ToString()
                Dim boolStatus As Boolean = Convert.ToBoolean(drMember("active"))
                Dim intCompanyLocationID As Integer = Int32.MinValue
                If Not drMember("CompanyLocationID") Is DBNull.Value Then
                    intCompanyLocationID = Convert.ToInt32(drMember("CompanyLocationID"))
                End If


                'Update this existing member
                Dim intSalutationID As Integer = Integer.MinValue
                If ddlSalutation.SelectedValue.Length > 0 Then
                    intSalutationID = Convert.ToInt32(ddlSalutation.SelectedValue)
                End If
                Dim strFirstName As String = FirstName.Text.Trim()
                Dim strLastName As String = LastName.Text.Trim()

                Dim strCompanyOffice As String = txtOffice.Text.Trim()

                Dim strAddress As String = ucAddress.LocationStreet
                Dim strCity As String = ucAddress.LocationCity
                Dim intStateID As Integer = ucAddress.LocationState
                Dim strZipCode As String = ucAddress.LocationZipCode
                Dim intCountryID As Integer = ucAddress.LocationCountry

                Dim intLanguageID As Integer = Convert.ToInt32(ddlLanguage.SelectedValue)

                Dim strDaytimePhone As String = daytimePhone.Text.Trim()
                Dim strEveningPhone As String = eveningPhone.Text.Trim()
                Dim dtLastLogin As DateTime = DateTime.Now()

                Dim strEmail As String = email.Text.Trim()

                Dim strSecurityQuestion As String = ddlsecurityQuestion.SelectedValue
                Dim strSecurityAnswer As String = securityAnswer.Text.Trim()

                Dim strCompany As String = txtCompany.Text.Trim()
                Dim strCompanyDepartment As String = txtDepartment.Text.Trim()
                Dim strJobTitle As String = txtTitle.Text.Trim()

                MemberDAL.UpdateMember(intMemberID, intSalutationID, strFirstName, strLastName, strAddress, strCity, intStateID, strZipCode, intCountryID, intLanguageID, strDaytimePhone, strEveningPhone, strEmail, strSecurityQuestion, strSecurityAnswer, strGroupIDs, boolStatus, strCompany, strCompanyDepartment, strJobTitle, strCompanyOffice, intCompanyLocationID)
                'If the password field is visible, AND the password field has text in it, we use this text to generate a NEW PASSWORD HASH
                If trPasswordNew.Visible Then
                    Dim strPassword As String = txtPwd.Text.Trim()
                    Dim strPassword_Hashed As String = CommonWeb.ComputeHash(strPassword)
                    MemberDAL.UpdateMember_Password_ByMemberID(intMemberID, strPassword_Hashed)
                End If

                'Add member image if it exists
                If RadUploadMemberImage.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = RadUploadMemberImage.UploadedFiles(0)
                    Dim strThumbnailName As String = file.GetName
                    Dim bytesStaffImage(file.InputStream.Length - 1) As Byte
                    file.InputStream.Read(bytesStaffImage, 0, file.InputStream.Length)
                    MemberDAL.UpdateMember_MemberImage_ByMemberID(intMemberID, strThumbnailName, bytesStaffImage)

                End If

                'Finally we set the current language to the member specified language
                Dim dtLanguage As DataTable = LanguageDAL.GetLanguage_ByID(intLanguageID)
                If dtLanguage.Rows.Count > 0 Then
                    Dim strLanguageCode As String = dtLanguage(0)("Code").ToString()
                    LanguageDAL.SetCurrentLanguage = strLanguageCode
                End If

                SendEmail()

                'Finally Redirect the user to the UpdateSuccessfull.aspx page, notice we could use a div that we show once member has updated their profile, however, we want to load the successful message in their language of choice. So full post-back is required
                Response.Redirect("UpdateProfileSuccessful.aspx")
            Else
                'member does not exists
                Response.Redirect("default.aspx")
            End If
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub SendEmail()
        'Send Email to site administrator informing them of an existing member updating their registration
        Dim EmailSwapoutData_Administrator As New Hashtable()

        'Add the members salutation, firstname and last name and to this email
        EmailSwapoutData_Administrator("Salutation") = ddlSalutation.Text.Trim()
        EmailSwapoutData_Administrator("FirstName") = FirstName.Text.Trim()
        EmailSwapoutData_Administrator("LastName") = LastName.Text.Trim()
        EmailSwapoutData_Administrator("Email") = email.Text.Trim()

        'Add the phone numbers and addresses to the email
        EmailSwapoutData_Administrator("DaytimePhoneNumber") = daytimePhone.Text.Trim()
        EmailSwapoutData_Administrator("EveningPhoneNumber") = eveningPhone.Text.Trim()

        Dim strAddress As String = ucAddress.LocationStreet
        Dim strCity As String = ucAddress.LocationCity
        Dim intStateID As Integer = ucAddress.LocationState
        Dim strZipCode As String = ucAddress.LocationZipCode
        Dim intCountryID As Integer = ucAddress.LocationCountry

        Dim strTextLocationInformation As String = CommonWeb.FormatAddress(strAddress, strCity, intStateID, strZipCode, intCountryID, CommonWeb.AddressFormat.MultipleLines)
        Dim strHtmlLocationInformation As String = CommonWeb.FormatAddress(strAddress, strCity, intStateID, strZipCode, intCountryID, CommonWeb.AddressFormat.MultipleLinesForHTML)

        EmailSwapoutData_Administrator("TextLocationInformation") = strTextLocationInformation
        EmailSwapoutData_Administrator("HtmlLocationInformation") = strHtmlLocationInformation

        'Send this information to our email DAL with Email Type ID = 8 -> Member Profile Updated - Sent to administrator -> This is an Administrator Email
        EmailDAL.SendEmail(8, intSiteID, EmailSwapoutData_Administrator)
    End Sub

#Region "Member Email Address and Image Validation"
    Protected Sub customValEmailAddress_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'If this email address is different to the users existing email address, then they are trying to change this
        'So check this new email address is not already taken
        Dim strEmailAddress As String = email.Text.Trim()
        Dim dtMemberList As DataTable = MemberDAL.GetMemberList_ByEmail(strEmailAddress)
        If dtMemberList.Rows.Count > 0 Then
            For Each drMemberList As DataRow In dtMemberList.Rows
                Dim intMemberID_WithSameEmailAddress As Integer = Convert.ToInt32(drMemberList("ID"))
                If intMemberID_WithSameEmailAddress <> intMemberID Then
                    'This email exists, and is used by another user
                    e.IsValid = False

                End If
            Next
        End If
    End Sub

    Protected Sub customValMemberImageSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add members image if it exists, must have a file size less than 100kb, but we give them a 5kb buffer
        If RadUploadMemberImage.UploadedFiles.Count > 0 Then
            For Each file As UploadedFile In RadUploadMemberImage.UploadedFiles
                If file.InputStream.Length > 102400 Then
                    e.IsValid = False
                End If
            Next
        End If
    End Sub
#End Region

    Protected Sub lnkDeleteImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteImage.Click
        Dim memberID As Integer = Convert.ToInt32(Request.QueryString("ID"))
        Dim bytesMemberImage() As Byte
        MemberDAL.UpdateMember_MemberImage_ByMemberID(memberID, String.Empty, bytesMemberImage)

        'Hide the memberImage and the delete link
        memberImage.Visible = False
        lnkDeleteImage.Visible = False
    End Sub
End Class
