Imports System.Data

Partial Class UserController_LoginControl
    Inherits System.Web.UI.UserControl

    Private _memberHomePage As String
    Public ReadOnly Property MemberHomepage() As String
        Get
            If _memberHomePage = "" Then
                _memberHomePage = WebInfoDAL.GetWebInfo_FirstSecurePageLinkURL_MemberSection()
            End If
            Return _memberHomePage
        End Get
    End Property

    Private _UseImages As Boolean
    Public Property UseImages() As Boolean
        Get
            Return _UseImages
        End Get
        Set(ByVal value As Boolean)
            _UseImages = value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        divPublicNotLoggedIn.Visible = False
        divPublicSectionLoggedIn.Visible = False

        LoadControlWithoutImages()
    End Sub

    Protected Sub LoadControlWithoutImages()
        Dim memberID As Integer = MemberDAL.GetCurrentMemberID()
        If memberID = 0 Then
            divPublicNotLoggedIn.Visible = True
        Else
            Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(memberID)
            Dim drMember As DataRow = dtMember.Rows(0)

            Dim strCurrentPath As String = Request.Url.AbsolutePath.ToString.ToLower()
            lit_MemberName_Public_Text.Text = Resources.Login_UserControl.LoginUserControl_Text_MyProfile
            divPublicSectionLoggedIn.Visible = True

            'If we are loading the the member, who logged in with forms authentication, then show the logout button
            If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
                If (Not HttpContext.Current.Session("LoginType") Is Nothing) AndAlso HttpContext.Current.Session("LoginType") = "forms" Then
                    spanLogout.Visible = True
                End If
            End If

        End If
    End Sub

End Class
