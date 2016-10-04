Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_emails_editAdd
    Inherits RichTemplateLanguagePage

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Setup Rad Controls
        CommonWeb.SetupRadEditor(Page, txt_BodyHtml, SiteDAL.GetCurrentSiteID_Admin)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Email.Email_AddEdit_Header
        ucHeader.PageHelpID = 15 'Help Item for Email Administration

        'Check the logged in user can view this page
        Dim intAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intAdminUserAccess > 1 Then
            'perhaps do something
        Else
            Response.Redirect("~/richadmin/")
        End If

        If Not Page.IsPostBack Then

            If Not Request.QueryString("ID") Is Nothing Then

                Dim emailTemplateID As Integer = Convert.ToInt32(Request.QueryString("ID"))

                Dim dtEmailTemplate As DataTable = EmailDAL.GetEmailTemplate_ByEmailTemplateIDAndSiteID(emailTemplateID, SiteDAL.GetCurrentSiteID_Admin)
                If dtEmailTemplate.Rows.Count > 0 Then
                    Dim drEmailTemplate As DataRow = dtEmailTemplate.Rows(0)

                    btnAddEdit.Text = Resources.Email.Email_AddEdit_ButtonUpdate
                    'If data is found, fill textboxes
                    Active.SelectedValue = drEmailTemplate("active").ToString
                    txt_Name.Text = drEmailTemplate("Name")
                    txt_Description.Text = drEmailTemplate("Description")
                    txt_SenderEmailAddress.Text = drEmailTemplate("SenderEmailAddress")

                    If Not drEmailTemplate("SenderName").ToString() = "" Then
                        txt_SenderName.Text = drEmailTemplate("SenderName").ToString()
                    End If

                    If Not drEmailTemplate("ReplyToEmailAddress").ToString() = "" Then
                        txt_ReplyToEmailAddress.Text = drEmailTemplate("ReplyToEmailAddress").ToString()
                    End If

                    If Not drEmailTemplate("ReplyToName").ToString() = "" Then
                        txt_ReplyToName.Text = drEmailTemplate("ReplyToName").ToString()
                    End If

                    txt_Subject.Text = drEmailTemplate("Subject")

                    If Not drEmailTemplate("EmailBodyParameterList") Is Nothing Then
                        Dim strEmailBodyAvailableParameterList As String = drEmailTemplate("EmailBodyParameterList").ToString()
                        litEmailBodyAvailableParameterList.Text = If(strEmailBodyAvailableParameterList.Length > 0, strEmailBodyAvailableParameterList, Resources.Email.Email_AddEdit_EmailBody_AvailableParameters_NoneAvailable)
                    Else
                        litEmailBodyAvailableParameterList.Text = Resources.Email.Email_AddEdit_EmailBody_AvailableParameters_NoneAvailable
                    End If
                    trEmailBodyAvailableParameters.Visible = True

                    txt_BodyText.Text = drEmailTemplate("BodyText")

                    If Not drEmailTemplate("BodyHtml").ToString() = "" Then
                        txt_BodyHtml.Content = drEmailTemplate("BodyHtml").ToString()
                    End If

                    'If this Type of Email is for Administrator Emails, show the RecipientEmailAddress
                    Dim boolIsAdministrationEmail As Boolean = Convert.ToBoolean(drEmailTemplate("IsAdministrationEmail"))
                    If boolIsAdministrationEmail Then
                        If Not drEmailTemplate("RecipientEmailAddress").ToString() = "" Then
                            txt_RecipientEmailAddress.Text = drEmailTemplate("RecipientEmailAddress").ToString()
                        End If
                    End If
                    trRecipientEmailAddressHeading.Visible = boolIsAdministrationEmail
                    trRecipientEmailAddressValue.Visible = boolIsAdministrationEmail

                Else
                    'This Email Template ID does not exist, so redirect back the the default page
                    Response.Redirect("default.aspx")
                End If

            Else
                btnAddEdit.Text = Resources.Email.Email_AddEdit_ButtonAdd
                Active.SelectedValue = True
            End If

        End If

    End Sub


    Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelButton.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click

        addUpdateRecord()
        Response.Redirect("default.aspx")


    End Sub

    Protected Sub addUpdateRecord()

        If Request.QueryString("ID") Is Nothing Then

            'When Adding Additional Email Templates, we default the EmailTypeID to 0, as we want the user to add email templates, but developers must also create a new EmailType and set it up so it sends the email when a specific user-defined event occurs
            Dim intEmailTypeID As Integer = 0
            Dim intSiteID As Integer = SiteDAL.GetCurrentSiteID_Admin()

            Dim strName As String = txt_Name.Text.Trim()
            Dim strDescription As String = txt_Description.Text.Trim()

            Dim strSenderEmailAddress As String = txt_SenderEmailAddress.Text.Trim()
            Dim strSenderName As String = txt_SenderName.Text.Trim()

            Dim strReplyToEmailAddress As String = txt_ReplyToEmailAddress.Text.Trim()
            Dim strReplyToName As String = txt_ReplyToName.Text.Trim()

            Dim strSubject As String = txt_Subject.Text.Trim()

            Dim strBodyText As String = txt_BodyText.Text.Trim()
            Dim strBodyHtml As String = txt_BodyHtml.Content.ToString()

            Dim strRecipientEmailAddress As String = txt_RecipientEmailAddress.Text.Trim()

            Dim boolActive As Boolean = Convert.ToBoolean(Active.SelectedValue)

            EmailDAL.InsertEmailTemplate(intEmailTypeID, intSiteID, strName, strDescription, strSenderEmailAddress, strSenderName, strReplyToEmailAddress, strReplyToName, strSubject, strBodyText, strBodyHtml, strRecipientEmailAddress, boolActive)
        Else

            Dim emailTemplateID As Integer = Request.QueryString("ID")

            Dim strName As String = txt_Name.Text.Trim()
            Dim strDescription As String = txt_Description.Text.Trim()

            Dim strSenderEmailAddress As String = txt_SenderEmailAddress.Text.Trim()
            Dim strSenderName As String = txt_SenderName.Text.Trim()

            Dim strReplyToEmailAddress As String = txt_ReplyToEmailAddress.Text.Trim()
            Dim strReplyToName As String = txt_ReplyToName.Text.Trim()

            Dim strSubject As String = txt_Subject.Text.Trim()

            Dim strBodyText As String = txt_BodyText.Text.Trim()
            Dim strBodyHtml As String = txt_BodyHtml.Content.ToString()

            Dim strRecipientEmailAddress As String = txt_RecipientEmailAddress.Text.Trim()

            Dim boolActive As Boolean = Convert.ToBoolean(Active.SelectedValue)

            EmailDAL.UpdateEmailTemplate_ByEmailTemplateID(emailTemplateID, strName, strDescription, strSenderEmailAddress, strSenderName, strReplyToEmailAddress, strReplyToName, strSubject, strBodyText, strBodyHtml, strRecipientEmailAddress, boolActive)
        End If

    End Sub
End Class
