
Partial Class _AccessDenined
    Inherits RichTemplateLanguagePage


    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'If the user arrived at this page, but is not logged in, then send them to the login page
        If intMemberID = 0 Then
            Response.Redirect("/login/")
        End If

        Page.Header.Title = Resources.Error_FrontEnd.AccessDenined_HeaderTitle
    End Sub

End Class
