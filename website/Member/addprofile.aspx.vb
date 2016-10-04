Imports System.Data
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports Telerik.Web.UI
Imports System.Xml

Partial Class Member_AddProfile
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        'THIS SITE USES DOES NOT ALLOW CREATING MEMBERS, so if they reached this page, they got here illegally, redirect them to the sitewide login page
        Dim strSiteWideLoginURL As String = ConfigurationManager.AppSettings("SiteWideLoginURL").ToString()
        Response.Redirect(strSiteWideLoginURL)

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        Page.Header.Title = SiteDAL.GetCompanyName() & " | " & Resources.Member_FrontEnd.Member_AddProfile_HeaderTitle
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Member_FrontEnd.Member_AddProfile_Heading)

        'Setup Rad Controls
        CommonWeb.SetupRadProgressArea(RadProgressAreaMember, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadMemberImage, intSiteID)

        If Not Page.IsPostBack Then

            'If user is already logged in redirect them to the update profile page
            If intMemberID > 0 Then
                Response.Redirect("updateprofile.aspx")
            End If

            'Bind the list of languages and security questions
            BindSalutationDropDown()
            BindLanguageDropDown()
            BindSecurityQuestionsListData()

            'Finally set the current language dropdown value to the Sites Default LanguageID
            Dim dtSite As DataTable = SiteDAL.GetSite_ByID(intSiteID)
            If dtSite.Rows.Count > 0 Then
                Dim drSite As DataRow = dtSite.Rows(0)
                Dim intLanguageID_Default As Integer = Convert.ToInt32(drSite("LanguageID_Default"))
                Dim liLangauge As ListItem = ddlLanguage.Items.FindByValue(intLanguageID_Default)
                If Not liLangauge Is Nothing Then
                    liLangauge.Selected = True
                End If
            End If

        End If

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

    Private Sub BindLanguageDropDown()

        'Populate the possible languages our site supports
        Dim dtLanguage As DataTable = LanguageDAL.GetLanguageList()
        ddlLanguage.Items.Clear()
        ddlLanguage.DataValueField = "ID"
        ddlLanguage.DataTextField = "Language"
        ddlLanguage.DataSource = dtLanguage
        ddlLanguage.DataBind()

        ddlLanguage.Items.Insert(0, New ListItem("--" & Resources.Member_FrontEnd.Member_AddProfile_Language_PleaseSelect & "--", ""))
    End Sub


    Protected Sub btnAddProfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddProfile.Click

        If IsValid Then

            AddProfile()
        End If

    End Sub

    Protected Sub AddProfile()

        'You can have the functionality that initally puts the member into no group (hence will not be active)
        'Or you can default the new user to the 'Basic Member' group (groupID 1), in which case the user is initially active
        Dim strAllowedGroups As String = String.Empty 'Now attach member groups once the member is created

        'APPROACH #1 - user is active depending on its status
        Dim boolStatus As Boolean = True 'or False, if member's status is decided by the admin
        'APPROACH #2 - user is active if they belong to a group, if you change this, also change this in the front-end (/WEBSITE/member/addprofile.aspx.vb)
        'Dim boolActive As Boolean = False
        'If groupIDArray.Length > 0 Then
        '    boolActive = True
        'End If

        'Insert this new member
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

        Dim strPassword As String = txtPwd.Text.Trim()
        Dim strPassword_Hashed As String = CommonWeb.ComputeHash(strPassword)

        Dim strSecurityQuestion As String = ddlsecurityQuestion.SelectedValue
        Dim strSecurityAnswer As String = securityAnswer.Text.Trim()

        Dim dtDateCreated As DateTime = DateTime.Now

        Dim strCompany As String = txtCompany.Text.Trim()
        Dim strCompanyDepartment As String = txtDepartment.Text.Trim()
        Dim strJobTitle As String = txtTitle.Text.Trim()
        Dim intCompanyLocationID As Integer = Int32.MinValue 'We just put a default of null here, as we have no ability to Add a member

        intMemberID = MemberDAL.InsertMember(intSalutationID, strFirstName, strLastName, strAddress, strCity, intStateID, strZipCode, intCountryID, intLanguageID, dtLastLogin, strDaytimePhone, strEveningPhone, strEmail, strPassword_Hashed, strSecurityQuestion, strSecurityAnswer, strAllowedGroups, dtDateCreated, boolStatus, strCompany, strCompanyDepartment, strJobTitle, strCompanyOffice, intCompanyLocationID)

        'Setup initial Site Access Defaults, either MEMBER has access to THIS SITE ONLY or ALL SITES depending on the value for intDefaultSiteID below
        Dim boolMember_SiteAccessEveryoneDefault As Boolean = False
        If Not ConfigurationManager.AppSettings("Member_SiteAccessEveryoneDefault") Is Nothing Then
            boolMember_SiteAccessEveryoneDefault = Convert.ToBoolean(ConfigurationManager.AppSettings("Member_SiteAccessEveryoneDefault"))
        End If
        Dim intSiteAccess As Integer = If(boolMember_SiteAccessEveryoneDefault, 0, intSiteID)
        SiteDAL.InsertSiteAccess_ForMember(intSiteAccess, intMemberID)


        'Give the Member Basic Access 1 by default, keep in a comma seperated list for the LoginMember Call, To INITIALLY GIVE NO ACCESS, until the admin user wants to specific access, comment the below condition out
        Dim strMemberGroupIDs As String = "1"
        For Each strMemberGroup As String In strMemberGroupIDs.Split(",")
            MemberDAL.InsertMemberGroup(intMemberID, strMemberGroup)
        Next

        'Add member image if it exists
        If RadUploadMemberImage.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = RadUploadMemberImage.UploadedFiles(0)
            Dim strThumbnailName As String = file.GetName
            Dim bytesStaffImage(file.InputStream.Length - 1) As Byte
            file.InputStream.Read(bytesStaffImage, 0, file.InputStream.Length)
            MemberDAL.UpdateMember_MemberImage_ByMemberID(intMemberID, strThumbnailName, bytesStaffImage)

        End If

        'Send Profile Created Email to new member and also to the site administrator
        sendEmail()

        'Finally we set the current language to the member specified language
        Dim strLanguageCode As String = String.Empty
        Dim dtLanguage As DataTable = LanguageDAL.GetLanguage_ByID(intLanguageID)
        If dtLanguage.Rows.Count > 0 Then
            strLanguageCode = dtLanguage(0)("Code").ToString()
            LanguageDAL.SetCurrentLanguage = strLanguageCode
        End If

        'Login the new user - If you login like this, you would want to redirect to a 'Thank-you for registering page', such that our LoginPanel at top of page refreshes
        'MemberDAL.LoginMember(intMemberID, strMemberGroupIDs, strLanguageCode)

        Response.Redirect("AddProfileSuccessful.aspx")

    End Sub

    Protected Sub sendEmail()

        ' Send Registration Confirmation to new member
        Dim EmailSwapoutData_NewMember As New Hashtable()

        'Add the members salutation, firstname and last name to this email
        EmailSwapoutData_NewMember("Salutation") = ddlSalutation.SelectedItem.Text.Trim()
        EmailSwapoutData_NewMember("FirstName") = FirstName.Text.Trim()
        EmailSwapoutData_NewMember("LastName") = LastName.Text.Trim()

        'Populate the list of recipients
        Dim listRecipientEmailAddress_NewMember As New ArrayList()
        listRecipientEmailAddress_NewMember.Add(email.Text.Trim())

        'Send this information to our email DAL with Email Type ID = 6 -> Member Profile Created - Sent to new member
        EmailDAL.SendEmail(listRecipientEmailAddress_NewMember, 6, intSiteID, EmailSwapoutData_NewMember)


        'Send Email to site administrator informing them of a new member
        Dim EmailSwapoutData_Administrator As New Hashtable()

        'Add the members salutation, firstname and last name and to this email
        EmailSwapoutData_Administrator("Salutation") = ddlSalutation.SelectedItem.Text.Trim()
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

        'Send this information to our email DAL with Email Type ID = 7 -> Member Profile Created - Sent to administrator -> This is an Administrator Email
        EmailDAL.SendEmail(7, intSiteID, EmailSwapoutData_Administrator)

    End Sub


#Region "Member Email Address and Image Validation"
    Protected Sub customValEmailAddress_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Check this email address does not already exist, if so add the new member else show an error message
        Dim strEmailAddress As String = email.Text.Trim()
        Dim dtMemberList As DataTable = MemberDAL.GetMemberList_ByEmail(strEmailAddress)
        If dtMemberList.Rows.Count > 0 Then
            e.IsValid = False
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


End Class
