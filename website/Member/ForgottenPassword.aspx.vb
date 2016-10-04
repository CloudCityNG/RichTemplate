Imports System.Data

Partial Class Member_ForgottenPassword
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'THIS SITE USES DOES NOT ALLOW CREATING MEMBERS, or retrieving Passwords as its controlled by Active directory,so if they reached this page, they got here illegally, redirect them to the sitewide login page
        Dim strSiteWideLoginURL As String = ConfigurationManager.AppSettings("SiteWideLoginURL").ToString()
        Response.Redirect(strSiteWideLoginURL)

        'First we set the SiteID and MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        Page.Header.Title = SiteDAL.GetCompanyName() & " | " & Resources.Member_FrontEnd.Member_ForgottenPassword_HeaderTitle
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Member_FrontEnd.Member_ForgottenPassword_Heading)

    End Sub


    Protected Sub submitStepOne_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submitStepOne.Click
        'reset error message
        panelForgottenPasswordError.Visible = False
        spanAccountNotFound.Visible = False
        spanNoAccessToThisSite.Visible = False
        If Page.IsPostBack Then

            Dim strEmail As String = Replace(txtEmail.Text.Trim(), "'", "''")

            Dim dtMember As DataTable = MemberDAL.GetMemberList_ByEmail(strEmail)
            If dtMember.Rows.Count > 0 Then
                Dim drMember As DataRow = dtMember.Rows(0)

                Dim intMemberID As Integer = Convert.ToInt32(drMember("ID"))

                'We have a valid member, now we check if this member has access to this site or not
                Dim boolHasAccessToSite As Boolean = False
                Dim sbSiteIdList As New StringBuilder()
                Dim dtSiteAccess As DataTable = SiteDAL.GetSiteAccess_ForMember_ByMemberIDAndSiteID(intMemberID, intSiteID)
                If dtSiteAccess.Rows.Count > 0 Then

                    'Email Address has been found, so hide the panelForgotPassword and show STEP TWO
                    divForgottenPassword_StepOne.Visible = False
                    divForgottenPassword_StepTwo.Visible = True

                    'Set the users security question
                    litSecurityQuestion.Text = drMember("securityQuestion").ToString()
                    litEmailAddress.Text = drMember("email").ToString()

                Else
                    'Email Address was found, but this member does not have access to this site
                    panelForgottenPasswordError.Visible = True

                    spanNoAccessToThisSite.Visible = True

                    Dim dtEmailTemplate As DataTable = EmailDAL.GetEmailTemplate_ByEmailTypeIDAndSiteID_FrontEnd(8, intSiteID) 'EmailTypeID=8 corresponds to Member Updated Emails
                    If dtEmailTemplate.Rows.Count > 0 Then
                        Dim drEmailTemplate As DataRow = dtEmailTemplate.Rows(0)
                        Dim strSmtpToMailExistingMember As String = drEmailTemplate("RecipientEmailAddress").ToString()
                        If strSmtpToMailExistingMember.Length > 0 Then
                            Dim strSmtpToMailExistingMemberFirst As String = strSmtpToMailExistingMember.Split(",").First()
                            aContactUs.HRef = "mailto:" & strSmtpToMailExistingMemberFirst
                            litContactUs.Text = strSmtpToMailExistingMemberFirst
                        End If
                    End If

                End If

            Else
                'Email Address cannot be found, so show the error panel
                panelForgottenPasswordError.Visible = True

                spanAccountNotFound.Visible = True

                Dim dtEmailTemplate As DataTable = EmailDAL.GetEmailTemplate_ByEmailTypeIDAndSiteID_FrontEnd(8, intSiteID) 'EmailTypeID=8 corresponds to Member Updated Emails
                If dtEmailTemplate.Rows.Count > 0 Then
                    Dim drEmailTemplate As DataRow = dtEmailTemplate.Rows(0)
                    Dim strSmtpToMailExistingMember As String = drEmailTemplate("RecipientEmailAddress").ToString()
                    If strSmtpToMailExistingMember.Length > 0 Then
                        Dim strSmtpToMailExistingMemberFirst As String = strSmtpToMailExistingMember.Split(",").First()
                        aContactUs.HRef = "mailto:" & strSmtpToMailExistingMemberFirst
                        litContactUs.Text = strSmtpToMailExistingMemberFirst
                    End If
                End If
            End If

        End If

    End Sub

    Protected Sub btnSubmitSecurityAnswer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitSecurityAnswer.Click
        'First reset the sercurity question error
        panelSecurityQuestionError.Visible = False

        'Check the users answer to the security question
        Dim strEmail As String = Replace(txtEmail.Text.Trim(), "'", "''")

        Dim dtMember As DataTable = MemberDAL.GetMemberList_ByEmail(strEmail)
        If dtMember.Rows.Count > 0 Then
            Dim drMember As DataRow = dtMember.Rows(0)
            Dim currentSecurityAnswer As String = drMember("securityAnswer").ToString().ToLower()

            If securityAnswer.Text.Trim().ToLower = currentSecurityAnswer Then
                'If this is correct show this user is valid, so log them in and send them to the update profile page so they can update their password
                Dim intMemberID As Integer = Convert.ToInt32(drMember("ID"))

                'Get the members group access
                Dim sbGroupIdList As New StringBuilder()

                Dim dtMemberGroupAccess As DataTable = MemberDAL.GetMemberGroupList_ByMemberID(intMemberID)
                For Each drMemberGroupAccess As DataRow In dtMemberGroupAccess.Rows
                    sbGroupIdList.Append(If(sbGroupIdList.Length = 0, drMemberGroupAccess("GroupID").ToString(), "," & drMemberGroupAccess("GroupID").ToString()))
                Next

                Dim sbSiteIdList As New StringBuilder()
                Dim dtSiteAccess As DataTable = SiteDAL.GetSiteAccessList_ForMember_ByMemberID(intMemberID)
                For Each drSiteAccess As DataRow In dtSiteAccess.Rows
                    sbSiteIdList.Append(If(sbSiteIdList.Length = 0, drSiteAccess("ID").ToString(), "," & drSiteAccess("ID").ToString()))
                Next

                'Get the members language Code
                Dim intLanguageID As Integer = Integer.MinValue
                If Not drMember("LanguageID") Is Nothing Then
                    intLanguageID = Convert.ToInt32(drMember("LanguageID"))
                End If
                Dim strLanguageCode As String = String.Empty
                Dim dtLanguage As DataTable = LanguageDAL.GetLanguage_ByID(intLanguageID)
                If dtLanguage.Rows.Count > 0 Then
                    strLanguageCode = dtLanguage(0)("Code").ToString()
                End If

                MemberDAL.LoginMember(intMemberID, sbGroupIdList.ToString(), sbSiteIdList.ToString(), strLanguageCode)
                Response.Redirect("UpdateProfile.aspx")
            Else
                'Else show the panelSecurityQuestionError
                panelSecurityQuestionError.Visible = True
            End If

        Else
            'Can not find the email address so go back to div 1 and show error message, also hide div step2
            divForgottenPassword_StepTwo.Visible = False
            divForgottenPassword_StepOne.Visible = True
            panelForgottenPasswordError.Visible = True
        End If

    End Sub

    Protected Sub SendEmail() Handles btn_EmailDetails1.Click, btn_EmailDetails2.Click

        'First reset the sercurity question error
        panelSecurityQuestionError.Visible = False

        'Check the users answer to the security question
        Dim strEmail As String = Replace(txtEmail.Text.Trim(), "'", "''")

        Dim dtMember As DataTable = MemberDAL.GetMemberList_ByEmail(strEmail)
        If dtMember.Rows.Count > 0 Then
            Dim drMember As DataRow = dtMember.Rows(0)
            Dim memberID As Integer = Convert.ToInt32(drMember("ID"))

            Dim strSalutation_LangaugeSpecific As String = String.Empty
            If Not drMember("Salutation_LanguageProperty") Is DBNull.Value Then
                Dim strSalutation_LanguageProperty As String = drMember("Salutation_LanguageProperty")
                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
            End If

            Dim strFirstName As String = drMember("FirstName")
            Dim strLastName As String = drMember("LastName")

            'Need to generate a new random password that can not be easily guessed
            Dim strRandomPassword As String = CommonWeb.GetRandomCode(10)
            Dim strRandomPassword_Hashed As String = CommonWeb.ComputeHash(strRandomPassword)

            'Update the users password
            MemberDAL.UpdateMember_Password_ByMemberID(memberID, strRandomPassword_Hashed)

            'Construct the 'reset password' email and send it using our Email DAL
            Dim EmailSwapoutData_ResetEmail As New Hashtable()

            'Add the members salutation, firstname and last name and to this email
            EmailSwapoutData_ResetEmail("Salutation") = strSalutation_LangaugeSpecific
            EmailSwapoutData_ResetEmail("FirstName") = strFirstName
            EmailSwapoutData_ResetEmail("LastName") = strLastName

            'Add the members login details to this email
            EmailSwapoutData_ResetEmail("LoginName") = strEmail
            EmailSwapoutData_ResetEmail("LoginPassword") = strRandomPassword

            'Populate the list of recipients
            Dim listRecipientEmailAddress_ResetEmail As New ArrayList()
            listRecipientEmailAddress_ResetEmail.Add(strEmail)

            'Send this information to our email DAL with Email Type ID = 9 -> Members new Login Information
            EmailDAL.SendEmail(listRecipientEmailAddress_ResetEmail, 9, intSiteID, EmailSwapoutData_ResetEmail)

            'Show the successfull password reset message
            divForgottenPassword_StepTwo.Visible = False
            divForgottenPassword_StepThree.Visible = True

            'load the email address into div's literal
            litEmailAddress2.Text = strEmail

        Else
            'Can not find the email address so go back to div 1 and show error message, also hide div step2
            divForgottenPassword_StepTwo.Visible = False
            divForgottenPassword_StepOne.Visible = True
            panelForgottenPasswordError.Visible = True
        End If

    End Sub
End Class
