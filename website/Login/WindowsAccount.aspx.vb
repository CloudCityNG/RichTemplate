Imports System.Data
Imports System.Security.Principal
Imports LDAP_ClassLibrary

Partial Class login_WindowsAccount
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        '-------------------------------------------------------------------------------------------------------------
        'THIS PAGE PERFORMS AUTOMATIC AD LOGIN, if it Can not login, we direct to the Forms Authection Login Page
        '-------------------------------------------------------------------------------------------------------------
        'First signout the current forms authentiation user
        FormsAuthentication.SignOut()

        Dim wi As WindowsIdentity = Context.Request.LogonUserIdentity
        If Not wi Is Nothing Then
            'User is authenticated using his/her windows account
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            Dim intMemberID As Integer = 0
            Dim strUsername As String = wi.Name.ToString().Trim().ToLower()
            Dim strAccountSID As String = String.Empty
            If wi.User.IsAccountSid AndAlso wi.User.Value.Length > 0 Then
                strAccountSID = wi.User.Value
            End If
            Dim strAppPoolName As String = ConfigurationManager.AppSettings("ActiveDirectory_AppPoolName").ToString().Trim().ToLower()

            'Log this Login Event -> For Debugging Purposes ONLY (turn it off if not using, it will clog up the db table)
            'ActiveDirectoryDAL.InsertActiveDirectoryEvent_LoginEvent(strUsername & ": Attempted to login via /Login/WindowsAccount.aspx", intSiteID)\

            Dim strLanguagePreference As String = String.Empty
            If strUsername.Length > 0 AndAlso (Not strUsername = "iusr") AndAlso (Not strUsername = strAppPoolName) Then
                'So get there Active DirectoryID and log them in using a redirect to default page
                Dim dtMember As DataTable = MemberDAL.GetMemberList_ByActiveDirectory_SID(strAccountSID)
                If dtMember.Rows.Count > 0 Then
                    Dim drMember As DataRow = dtMember.Rows(0)
                    Dim boolActive As Boolean = Convert.ToBoolean(drMember("active"))
                    If boolActive Then
                        intMemberID = Convert.ToInt32(drMember("ID"))
                        strLanguagePreference = drMember("LanguageCode").ToString()
                    End If
                End If
            End If

            If intMemberID > 0 Then

                'We have a valid member, now we check if this member has access to this site or not
                Dim sbSiteIdList As New StringBuilder()
                Dim dtSiteAccess As DataTable = SiteDAL.GetSiteAccessList_ForMember_ByMemberID(intMemberID)
                'For Each drSiteAccess As DataRow In dtSiteAccess.Rows
                '    Dim intSiteID_SiteAccess As Integer = Convert.ToInt32(drSiteAccess("ID"))
                '    sbSiteIdList.Append(If(sbSiteIdList.Length = 0, intSiteID_SiteAccess.ToString(), "," & intSiteID_SiteAccess.ToString()))
                'Next
                'User only has access to one site ONLY
                If dtSiteAccess.Rows.Count > 0 Then
                    Dim drSiteAccess As DataRow = dtSiteAccess.Rows(0)
                    Dim intSiteID_SiteAccess As Integer = Convert.ToInt32(drSiteAccess("ID"))
                    If intSiteID_SiteAccess = "0" Then
                        sbSiteIdList.Append(intSiteID)
                    Else
                        sbSiteIdList.Append(intSiteID_SiteAccess)
                    End If
                End If
                If sbSiteIdList.Length > 0 Then
                    'Get the members group access
                    Dim sbGroupIdList As New StringBuilder()
                    Dim dtMemberGroupAccess As DataTable = MemberDAL.GetMemberGroupList_ByMemberID(intMemberID)
                    For Each drMemberGroupAccess As DataRow In dtMemberGroupAccess.Rows
                        sbGroupIdList.Append(If(sbGroupIdList.Length = 0, drMemberGroupAccess("GroupID").ToString(), "," & drMemberGroupAccess("GroupID").ToString()))
                    Next

                    MemberDAL.LoginMember(intMemberID, sbGroupIdList.ToString(), sbSiteIdList.ToString(), strLanguagePreference)

                    Dim intSiteID_ToLoginFirst As Integer = Integer.MinValue
                    Dim listSiteAccessList As String() = sbSiteIdList.ToString().Split(",")
                    If listSiteAccessList.Contains("0") Or listSiteAccessList.Contains(intSiteID) Then
                        intSiteID_ToLoginFirst = intSiteID
                    Else
                        'Get the first site id in the list
                        intSiteID_ToLoginFirst = Convert.ToInt32(listSiteAccessList(0))
                    End If
                    SiteDAL.SetCurrentSiteID_FrontEnd(intSiteID_ToLoginFirst)

                    'There is already a member logged in, so redirect them to the first public page for their site, by getting their first
                    'Set the LoginType, so we know the user logged in via forms authentication, so we can show logout button
                    HttpContext.Current.Session("LoginType") = "windows"

                    Dim strHomePage As String = WebInfoDAL.GetWebInfo_HomePage(intSiteID_ToLoginFirst)
                    FormsAuthentication.SetAuthCookie(intMemberID, False)
                    Response.Redirect(strHomePage)
                Else
                    'User can not be authenticated using windows authentication so the login panel is shown
                    Response.ClearContent()
                    Dim strLoginPage_FormsAuthentication As String = ConfigurationManager.AppSettings("ActiveDirectory_LoginPage_FormsAuthentication").ToString()
                    Server.Execute(strLoginPage_FormsAuthentication)
                End If

            Else
                'User can not be authenticated using windows authentication so the login panel is shown
                MemberDAL.LogoutCurrentMember()
                Response.ClearContent()
                Dim strLoginPage_FormsAuthentication As String = ConfigurationManager.AppSettings("ActiveDirectory_LoginPage_FormsAuthentication").ToString()
                Server.Execute(strLoginPage_FormsAuthentication)
            End If
        End If

    End Sub

End Class
