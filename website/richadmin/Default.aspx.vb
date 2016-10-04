Imports System.Data

Partial Class richadmin_Default
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'First thing to do is remove any logged-in trace, so the user starts this page with a clean session
        AdminUserDAL.LogoutCurrentAdminUser()

        'If the session has timed out, show the timed out message
        If Not Request.QueryString("timeout") Is Nothing Then
            If Request.QueryString("timeout").ToLower = "true" Then

                divErrorMessage.Visible = True
                litErrorMessage.Text = Resources.richadmin.richadmin_Default_SessionTimeout
            End If

        End If
        If Not Request.QueryString("username") Is Nothing Then
            Dim strUsername As String = Request.QueryString("username").ToString()
            txtUsername.Text = strUsername
        End If
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        'reset the error message
        divErrorMessage.Visible = False
        litErrorMessage.Text = ""

        Dim strUsername As String = txtUsername.Text.Trim()
        Dim strPassword As String = txtPassword.Text.Trim()

        'Get all admin users with the same username
        Dim drFoundAdminUser As DataRow = Nothing
        Dim dtAdminUsers As DataTable = AdminUserDAL.GetAdminUser_ByUsername(strUsername)
        For Each drAdminUser As DataRow In dtAdminUsers.Rows
            Dim strFoundPassword As String = drAdminUser("Password")
            If CommonWeb.VerifyHash(strPassword, strFoundPassword) Then
                drFoundAdminUser = drAdminUser
                Exit For
            End If
        Next
        If drFoundAdminUser Is Nothing Then
            'No user exists
            divErrorMessage.Visible = True
            litErrorMessage.Text = Resources.richadmin.richadmin_Default_AccessDenied_InvalidUsernamePassword
        Else
            'Else the user does exists so now we must do additional checks
            Dim intAdminUserID As Integer = Convert.ToInt32(drFoundAdminUser("ID"))
            Dim boolActive As Boolean = Convert.ToBoolean(drFoundAdminUser("Active"))
            Dim dtExpirationDate As DateTime = DateTime.MaxValue
            If Not drFoundAdminUser("Expiration_Date") Is DBNull.Value Then
                dtExpirationDate = Convert.ToDateTime(drFoundAdminUser("Expiration_Date"))
            End If

            Dim intLoginCounter As Int64 = Convert.ToInt64(drFoundAdminUser("Counter"))
            Dim intLoginLimit As Int64 = Convert.ToInt64(drFoundAdminUser("Login_Limit"))

            If Not boolActive Then
                'Then this user is not active
                divErrorMessage.Visible = True
                litErrorMessage.Text = Resources.richadmin.richadmin_Default_AccessDenied_NotActive

            ElseIf DateTime.Now > dtExpirationDate Then
                'Then this users expiration date has been reached
                divErrorMessage.Visible = True
                litErrorMessage.Text = Resources.richadmin.richadmin_Default_AccessDenied_Expired

            ElseIf intLoginCounter > intLoginLimit Then
                'Then this user has reached their login limit
                divErrorMessage.Visible = True
                litErrorMessage.Text = Resources.richadmin.richadmin_Default_AccessDenied_LoginLimitReached

            Else

                Dim intAccessLevel As Integer = Convert.ToInt32(drFoundAdminUser("Access_Level"))

                'AdminUser is Valid, now check they have access to 1 or more sites
                Dim intSiteID As Integer = SiteDAL.GetDefaultSiteIDForAdminUser(intAdminUserID, intAccessLevel)
                'If a valid siteID is found we set the siteID for the Admin and log them into the admin with this siteID, else show an error message
                If intSiteID > Integer.MinValue Then

                    SiteDAL.SetCurrentSiteID_Admin(intSiteID)

                    'Before we Handle permissions we check if this adminUser's permissions are at a SiteLevel or not
                    Dim boolUseSiteLevelAccess As Boolean = Convert.ToBoolean(drFoundAdminUser("UseSiteLevelAccess"))

                    Dim drSiteAccess_AdminUser As DataRow = Nothing
                    If boolUseSiteLevelAccess Then
                        Dim dtSiteAccess_AdminUser As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID(intSiteID, intAdminUserID)
                        If dtSiteAccess_AdminUser.Rows.Count > 0 Then
                            drSiteAccess_AdminUser = dtSiteAccess_AdminUser.Rows(0)
                        End If
                    End If

                    'The Admin User has access to at least one site, so log then in
                    Dim strAllowModules As String = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, String.Empty, drSiteAccess_AdminUser("Allow_Modules")), drFoundAdminUser("Allow_Modules_AllSites").ToString())
                    Dim boolAllowWebContent As Boolean = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_WebContent")), Convert.ToBoolean(drFoundAdminUser("Allow_WebContent_AllSites")))

                    'Get the users preferred Language and obtain the language code from the Language DB
                    Dim strLanguagePreference As String = drFoundAdminUser("LanguageCode").ToString()

                    AdminUserDAL.LoginCurrentAdminUser(intAdminUserID, intSiteID, intAccessLevel, strAllowModules, boolAllowWebContent, strLanguagePreference)

                    Response.Redirect("/admin/")
                Else
                    'Show the "This admin user has not been assigned access to any sites
                    litErrorMessage.Text = Resources.richadmin.richadmin_Default_AccessDenied_AdminUserNotAssignedPermissions
                    divErrorMessage.Visible = True
                End If

            End If
        End If
    End Sub
End Class
