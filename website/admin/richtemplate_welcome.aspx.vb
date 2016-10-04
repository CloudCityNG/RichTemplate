
Partial Class admin_RichTemplate_Welcome
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Check the user exists
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        If intAdminUserID > 0 Then

            'Get the current admin mode, e.g. forms, members, modules etc.
            Dim strMode As String = ""
            If Not Request.QueryString("mode") Is Nothing Then
                strMode = Request.QueryString("mode").ToUpper()
            End If

            'Using the current mode, we set the Header's ID, and its Page Title
            Dim intPageHelpID As Integer = 1 'This loads the 'Welcome to RichTemplateTM help item
            Dim strPageName As String = ""
            Select Case strMode
                Case "FORMS"
                    strPageName = Resources.RichTemplate_Welcome.RichTemplate_Welcome_PageName_Forms

                Case "MEMBERS"
                    strPageName = Resources.RichTemplate_Welcome.RichTemplate_Welcome_PageName_Members

                Case "EDUCATION"
                    strPageName = Resources.RichTemplate_Welcome.RichTemplate_Welcome_PageName_Education

                Case "MODULES"
                    strPageName = Resources.RichTemplate_Welcome.RichTemplate_Welcome_PageName_Modules

                Case "IMAGES"
                    strPageName = Resources.RichTemplate_Welcome.RichTemplate_Welcome_PageName_Images

                Case "ADMINISTRATION"
                    strPageName = Resources.RichTemplate_Welcome.RichTemplate_Welcome_PageName_Administration

                Case Else
                    strPageName = Resources.RichTemplate_Welcome.RichTemplate_Welcome_PageName_Default

            End Select

            ucHeader.PageHelpID = intPageHelpID
            ucHeader.PageName = strPageName

        Else
            Response.Redirect("~/richadmin/")
        End If
    End Sub
End Class
