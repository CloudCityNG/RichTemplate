Imports System.Data

Partial Class richadmin_ForgottenPassword
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        'First hide all error messages, if any are set
        divErrorUsernameOrEmailAddressRequired.Visible = False
        divErrorNoEmailExists.Visible = False
        divErrorNoUsernameExists.Visible = False

        'Check if the form is valid
        Dim strEmailAddress As String = txtEmailAddress.Text.Trim()
        Dim strUsername As String = txtUsername.Text.Trim()

        'if either email address or user is supplied, so now we check the email address or username to see if its valid, if so show the ForgottenPassword step two and send email
        Dim dtAdminUser As New DataTable
        If strEmailAddress.Length = 0 And strUsername.Length = 0 Then
            divErrorUsernameOrEmailAddressRequired.Visible = True
        ElseIf strEmailAddress.Length > 0 Then

            dtAdminUser = AdminUserDAL.GetAdminUser_ByEmailAddress(strEmailAddress)
            If dtAdminUser.Rows.Count = 0 Then
                divErrorNoEmailExists.Visible = True
            End If
        ElseIf strUsername.Length > 0 Then
            dtAdminUser = AdminUserDAL.GetAdminUser_ByUsername(strUsername)
            If dtAdminUser.Rows.Count = 0 Then
                divErrorNoUsernameExists.Visible = True
            End If
        End If

        'If we have found an email address, then send the 'Forgotten Password' email, and setup/show the 2nd step of the forgotten password page
        If dtAdminUser.Rows.Count > 0 Then
            Dim intAdminUserID As Integer = Convert.ToInt32(dtAdminUser.Rows(0)("ID"))
            Dim strAdminUserEmailAddress As String = dtAdminUser.Rows(0)("Email")
            Dim strAdminUserUsername As String = dtAdminUser.Rows(0)("Username")

            'Dim strAdminUserPassword As String = dtAdminUser.Rows(0)("Password")

            'Generate a new Code, Update the AdminUser's Password and email the user password
            Dim strAdminUserPassword As String = CommonWeb.GetRandomCode(8, 10)
            Dim strAdminUserPassword_Hashed As String = CommonWeb.ComputeHash(strAdminUserPassword)
            AdminUserDAL.UpdateAdminUser_Password(intAdminUserID, strAdminUserPassword_Hashed)
            SendForgottenPasswordEmail(strAdminUserEmailAddress, strAdminUserUsername, strAdminUserPassword)

            litFoundEmailAddress.Text = strAdminUserEmailAddress
            divForgottenPasswordStepOne.Visible = False
            divForgottenPasswordStepTwo.Visible = True
        End If
    End Sub

    Private Sub SendForgottenPasswordEmail(ByVal strEmailAddress As String, ByVal strUsername As String, ByVal strPassword As String)

        'Construct the 'reset password' email and send it using our Email DAL
        Dim EmailSwapoutData_ResetEmail As New Hashtable()

        'Add the admin users username, password and the site url to this email
        EmailSwapoutData_ResetEmail("AdminLoginURL") = "http://" & Request.Url.Host & "/richadmin/?username="
        EmailSwapoutData_ResetEmail("Username") = strUsername
        EmailSwapoutData_ResetEmail("Password") = strPassword


        'Populate the list of recipients
        Dim listRecipientEmailAddress_ResetEmail As New ArrayList()
        listRecipientEmailAddress_ResetEmail.Add(strEmailAddress)

        'Send this information to our email DAL with Email Type ID = 11 -> Admin User Reset Password Email -> This is going to an Administrator, However this is NOT an Administrator Email, as no need to tell any adminstrator of a reset password email
        EmailDAL.SendEmail(listRecipientEmailAddress_ResetEmail, 11, SiteDAL.GetCurrentSiteID_FrontEnd(), EmailSwapoutData_ResetEmail)

    End Sub
End Class
