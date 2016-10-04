
Partial Class logout_Default
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        LogoutUser()
    End Sub

    Private Sub LogoutUser()
        'Logout member from main site
        MemberDAL.LogoutCurrentMember()

        'Logout member from forum
        FormsAuthentication.SignOut()

        'YAF Forum Only - Clearing user cache with permissions data and active users cache...
        'PageContext.Cache.Remove(YafCache.GetBoardCacheKey(YAF.Classes.Constants.Cache.ActiveUserLazyData.FormatWith(PageContext.PageUserID)))
        'PageContext.Cache.Remove(YafCache.GetBoardCacheKey(YAF.Classes.Constants.Cache.UsersOnlineStatus))
        'Session.Abandon()

        'Response.Redirect("/Login/")

        Dim strSiteWideLoginURL As String = ConfigurationManager.AppSettings("SiteWideLoginURL").ToString()
        Response.Redirect(strSiteWideLoginURL)
    End Sub

End Class
