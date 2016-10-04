Imports System.Data
Imports LDAP_ClassLibrary

Partial Class Login_Default
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        'Page.Header.Title = SiteDAL.GetCompanyName() & " | " & Resources.Login_FrontEnd.Login_HeaderTitle

        'We only have one login type, thats for a member only, so automatically redirect to /member/ login panel
        ''Response.Redirect("/Member/")

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        Page.Header.Title = Resources.Login_FrontEnd.Login_HeaderTitle
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Login_FrontEnd.Login_HeaderTitle)

        If intMemberID > 0 Then
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
        Else
            'First thing we do is logout any existing logged in user
            MemberDAL.LogoutCurrentMember()
        End If

    End Sub

    ''' <summary>
    '''  This is different to the Member Login, as this page does not check if the logged in user has access to THIS Site, as once they are logged in we then determine what site to send them to
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        'First thing we do is logout any existing logged in user
        MemberDAL.LogoutCurrentMember()

        Dim boolLoginSuccessful As Boolean = False
        'hide/reset the error and authenticated panels and show the login panel
        pnlSiteAccessDeclined.Visible = False
        spanInvalidUsernamePassword.Visible = False
        spanNoAccessToThisSite.Visible = False
        loginPanel.Visible = True

        Dim email As String = Replace(Me.email.Text, "'", "''")
        Dim strPassword As String = Replace(Me.userPassword.Text, "'", "''")

        Dim intMemberID As Integer = 0
        Dim strLanguagePreference As String = String.Empty

        Dim strSiteIdList As String = String.Empty

        Dim dtMember As DataTable = MemberDAL.GetMemberList_ByEmail_FrontEnd(email)
        For Each drMember As DataRow In dtMember.Rows()

            'Get the users username, and try log them into Active Directory
            intMemberID = drMember("ID")

            'Get this members site access
            'We have a valid member, now we check if this member has access to this site or not
            Dim sbSiteIdList As New StringBuilder()
            Dim dtSiteAccess As DataTable = SiteDAL.GetSiteAccessList_ForMember_ByMemberID(intMemberID)

            Dim intSiteID_SiteAccess As Integer = Integer.MinValue
            For Each drSiteAccess As DataRow In dtSiteAccess.Rows
                intSiteID_SiteAccess = Convert.ToInt32(drSiteAccess("ID"))
                sbSiteIdList.Append(If(sbSiteIdList.Length = 0, intSiteID_SiteAccess.ToString(), "," & intSiteID_SiteAccess.ToString()))
            Next

            strSiteIdList = sbSiteIdList.ToString()

            If intSiteID_SiteAccess > Integer.MinValue Then

                'Login the user using their ActiveDirectoryAccount if their ActiveDirectory Identifier is not NULL
                If Not drMember("ActiveDirectory_Identifier") Is DBNull.Value Then

                    '-------------------------------------------------------------------------------
                    'THIS IS AN ACTIVE DIRECTORY USER, SO LOG THEM IN USING WINDOWS AUTHENTICATION
                    '-------------------------------------------------------------------------------

                    Dim strActiveDirectory_Identifier As String = drMember("ActiveDirectory_Identifier").ToString()
                    Dim dtActiveDirectoryUser As DataTable = LDAP.AD_GetUser(strActiveDirectory_Identifier)
                    If dtActiveDirectoryUser.Rows.Count > 0 Then
                        Dim drActiveDirectoryUser As DataRow = dtActiveDirectoryUser.Rows(0)
                        Dim strUserDomain As String = drActiveDirectoryUser("domain").ToString()
                        Dim strUsername As String = drActiveDirectoryUser("sAMAccountName").ToString()
                        If LDAP.LogonUser_ByDistinguishedNameAndPassword(strUsername, strUserDomain, strPassword) Then

                            strLanguagePreference = drMember("LanguageCode").ToString()

                            'Set the LoginType, so we know the user logged in via forms authentication, so we can show logout button
                            HttpContext.Current.Session("LoginType") = "forms"

                            boolLoginSuccessful = True
                            Exit For
                        End If
                    End If
                Else

                    '-------------------------------------------------------------------------------
                    'THIS IS A Rich Template MEMBER, SO LOG THEM IN BY CHECKING THEIR SUPPLIED PASSWORD
                    '-------------------------------------------------------------------------------

                    'Check their password, they might be a Rich Template Member
                    Dim password As String = drMember("Password").ToString()

                    If CommonWeb.VerifyHash(strPassword, password) Then
                        'The user is authenticated
                        strLanguagePreference = drMember("LanguageCode").ToString()

                        'Set the LoginType, so we know the user logged in via forms authentication, so we can show logout button
                        HttpContext.Current.Session("LoginType") = "forms"

                        boolLoginSuccessful = True
                        Exit For
                    End If
                End If

            End If

        Next

        'If the member ID is greater than 0, then the user should be authenticated
        If boolLoginSuccessful Then

            If strSiteIdList.Length > 0 Then
                'Get the members group access
                Dim sbGroupIdList As New StringBuilder()

                Dim dtMemberGroupAccess As DataTable = MemberDAL.GetMemberGroupList_ByMemberID(intMemberID)
                For Each drMemberGroupAccess As DataRow In dtMemberGroupAccess.Rows
                    sbGroupIdList.Append(If(sbGroupIdList.Length = 0, drMemberGroupAccess("GroupID").ToString(), "," & drMemberGroupAccess("GroupID").ToString()))
                Next

                'now set the current logged in user
                MemberDAL.LoginMember(intMemberID, sbGroupIdList.ToString(), strSiteIdList, strLanguagePreference)

                'To ensure the login control registers the newly logged in user we must redirect to the same page
                'But when we redirect the page loads for the logged in user
                'There is already a member logged in, so redirect them to the first public page for their site, by getting their first
                Dim intSiteID_ToLoginFirst As Integer = Integer.MinValue
                Dim listSiteAccessList As String() = strSiteIdList.Split(",")
                If listSiteAccessList.Contains("0") Or listSiteAccessList.Contains(intSiteID) Then
                    intSiteID_ToLoginFirst = intSiteID
                Else
                    'Get the first site id in the list
                    intSiteID_ToLoginFirst = Convert.ToInt32(listSiteAccessList(0))
                End If
                SiteDAL.SetCurrentSiteID_FrontEnd(intSiteID_ToLoginFirst)

                'There is already a member logged in, so redirect them to the first public page for their site, by getting their first
                Dim strHomePage As String = WebInfoDAL.GetWebInfo_HomePage(intSiteID_ToLoginFirst)
                FormsAuthentication.SetAuthCookie(intMemberID, False)
                Response.Redirect(strHomePage)
            Else
                'Show the error panel - This is a valid member, but they do not have access to this site
                Dim dtEmailTemplate As DataTable = EmailDAL.GetEmailTemplate_ByEmailTypeIDAndSiteID_FrontEnd(8, intSiteID) 'EmailTypeID=8 corresponds to Member Updated Emails
                If dtEmailTemplate.Rows.Count > 0 Then
                    Dim drEmailTemplate As DataRow = dtEmailTemplate.Rows(0)
                    Dim strSmtpToMailExistingMember As String = drEmailTemplate("RecipientEmailAddress").ToString()
                    If strSmtpToMailExistingMember.Length > 0 Then
                        Dim strSmtpToMailExistingMemberFirst As String = strSmtpToMailExistingMember.Split(",").First()
                        aNoAccessToThisSite.HRef = "mailto:" & strSmtpToMailExistingMemberFirst
                        litNoAccessToThisSite.Text = strSmtpToMailExistingMemberFirst
                    End If
                End If

                pnlSiteAccessDeclined.Visible = True
                spanNoAccessToThisSite.Visible = True
            End If

        Else
            'Show the error panel
            pnlSiteAccessDeclined.Visible = True
            spanInvalidUsernamePassword.Visible = True
        End If


    End Sub

End Class
