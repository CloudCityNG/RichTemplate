Imports System.Data

Partial Class Member_Default
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim ModuleTypeID As Integer = 8

    Private _memberHomePage As String
    Public ReadOnly Property MemberHomepage() As String
        Get
            If _memberHomePage = "" Then
                _memberHomePage = WebInfoDAL.GetWebInfo_FirstSecurePageLinkURL_MemberSection()
            End If
            Return _memberHomePage
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'THIS SITE USES THE /login/default.aspx to login, so if they reached this page, just redirect them to the member search page
        Dim strSiteWideLoginURL As String = ConfigurationManager.AppSettings("SiteWideLoginURL").ToString()
        Server.Transfer("~/member/membersearch.aspx")

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        If Not Page.IsPostBack Then

            Page.Header.Title = SiteDAL.GetCompanyName() & " | " & Resources.Member_FrontEnd.Member_Default_HeaderTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Member_FrontEnd.Member_Default_Login_ExistingMembers)

            If intMemberID = 0 Then
                loginPanel.Visible = True
                divNewMemberPanel.Visible = True

            Else
                'Such that if the user is logged in, we can automatically send them to the members home page
                'Response.Redirect(MemberHomepage)
                authenticatedPanel.Visible = True
            End If


        End If

    End Sub

    Protected Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        'First thing we do is logout any existing logged in user
        MemberDAL.LogoutCurrentMember()

        'hide/reset the error and authenticated panels and show the login panel
        pnlSiteAccessDeclined.Visible = False
        spanInvalidUsernamePassword.Visible = False
        spanNoAccessToThisSite.Visible = False
        authenticatedPanel.Visible = False
        loginPanel.Visible = True
        divNewMemberPanel.Visible = True

        Dim email As String
        Dim userPassword As String

        email = Replace(Me.email.Text, "'", "''")
        userPassword = Replace(Me.userPassword.Text, "'", "''")

        Dim intMemberID As Integer = 0
        Dim strLanguagePreference As String = String.Empty

        Dim dtMember As DataTable = MemberDAL.GetMemberList_ByEmail_FrontEnd(email)
        For Each drMember As DataRow In dtMember.Rows()
            Dim password As String = drMember("Password").ToString()
            If CommonWeb.VerifyHash(userPassword, password) Then
                'The user is authenticated
                intMemberID = Convert.ToInt32(drMember("id"))
                strLanguagePreference = drMember("LanguageCode").ToString()
                Exit For
            End If
        Next

        'If the member ID is greater than 0, then the user should be authenticated
        If intMemberID > 0 Then

            'We have a valid member, now we check if this member has access to this site or not
            Dim boolHasAccessToSite As Boolean = False
            Dim sbSiteIdList As New StringBuilder()
            Dim dtSiteAccess As DataTable = SiteDAL.GetSiteAccessList_ForMember_ByMemberID(intMemberID)
            For Each drSiteAccess As DataRow In dtSiteAccess.Rows
                Dim intSiteID_SiteAccess As Integer = Convert.ToInt32(drSiteAccess("ID"))
                sbSiteIdList.Append(If(sbSiteIdList.Length = 0, intSiteID_SiteAccess.ToString(), "," & intSiteID_SiteAccess.ToString()))
                If intSiteID = intSiteID_SiteAccess Or intSiteID_SiteAccess = 0 Then ' access of ZERO implies access to all sites
                    boolHasAccessToSite = True
                End If
            Next

            If boolHasAccessToSite Then
                'Get the members group access
                Dim sbGroupIdList As New StringBuilder()

                Dim dtMemberGroupAccess As DataTable = MemberDAL.GetMemberGroupList_ByMemberID(intMemberID)
                For Each drMemberGroupAccess As DataRow In dtMemberGroupAccess.Rows
                    sbGroupIdList.Append(If(sbGroupIdList.Length = 0, drMemberGroupAccess("GroupID").ToString(), "," & drMemberGroupAccess("GroupID").ToString()))
                Next

                'now set the current logged in user
                MemberDAL.LoginMember(intMemberID, sbGroupIdList.ToString(), sbSiteIdList.ToString(), strLanguagePreference)

                'To ensure the login control registers the newly logged in user we must redirect to the same page
                'But when we redirect the page loads for the logged in user
                Response.Redirect("default.aspx")
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
