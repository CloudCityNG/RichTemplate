Imports System.Security.Principal

Partial Class admin_usercontrols_FindBug
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub Show()
        'Reset the panel
        txtBugDesc.Text = ""
        txtEmail.Text = ""
        txtPhone.Text = ""

        divFindBug.Visible = True
        divFindBugSubmitted.Visible = False

        'Show the panel and the tooltip
        pnl_FindBug.Visible = True
        rtt_FindBug.Show()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
       
        'Get Form Data
        Dim strBugDescription As String = txtBugDesc.Text.Trim()
        Dim strContactEmail As String = txtEmail.Text.Trim()
        Dim strContactPhone As String = txtPhone.Text.Trim()

        'Log Bug that the user submitted data
        Dim intBugID As Integer = RichTemplateCentralDAL.Bug_LogBug(strBugDescription, strContactEmail, strContactPhone)


        'Send Email to richtemplate staff
        SendBugReportEmail()

        'Hide the Find Bug Submission Panel and show the Submitted form
        divFindBug.Visible = False
        divFindBugSubmitted.Visible = True
        rtt_FindBug.Show()

    End Sub

    Public Sub SendBugReportEmail()

        Dim strDomainName As String = Request.Url.Host.ToString()

        Dim strBugDescription As String = txtBugDesc.Text.Trim()

        'Note Contact Email and Contact Phone can be empty
        Dim strContactEmail As String = "Not-Supplied"
        If strContactEmail.Length > 0 Then
            strContactEmail = txtEmail.Text.Trim()
        End If

        Dim strContactPhone As String = "Not-Supplied"
        If strContactPhone.Length > 0 Then
            strContactPhone = txtPhone.Text.Trim()
        End If



        'Create our swapout hashtable
        Dim EmailSwapoutData As New Hashtable()

        'Add the site domain, bug description, contact Email and Contact Phone number to our email swapout data
        EmailSwapoutData("SiteDomain") = strDomainName
        EmailSwapoutData("BugDescription") = strBugDescription
        EmailSwapoutData("ContactEmail") = strContactEmail
        EmailSwapoutData("ContactPhone") = strContactPhone

        'Send our Email using the EmailDAL using Admin Find a Bug - Sent to RichTemplate Email Type (ID=10) -> This is an Administrator Email
        EmailDAL.SendEmail(10, SiteDAL.GetCurrentSiteID_Admin(), EmailSwapoutData)

    End Sub
End Class
