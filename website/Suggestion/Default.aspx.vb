Imports System.Net.Mail
Imports System.Data
Imports Telerik.Web.UI

Partial Class _Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 13

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'First we set the SiteID and current MemberID

        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Get module information, to check if we will show its introduction
        Dim dtModule As DataTable = ModuleDAL.GetModule_ByModuleTypeIDAndSiteID_FrontEnd(ModuleTypeID, intSiteID)
        If dtModule.Rows.Count > 0 Then
            Dim drModule As DataRow = dtModule.Rows(0)
            If Not drModule("ModuleContentHTML") Is DBNull.Value Then
                Dim strModuleContentHTML As String = drModule("ModuleContentHTML")

                litModuleDynamicContent.Text = strModuleContentHTML
                divModuleDynamicContent.Visible = True
            End If
        Else
            'We do not have an Active Suggestion Module For the Front-End, so redirect to the homepage
            Response.Redirect("/")
        End If

        ' Check our modules Configuration settings
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "use_captcha" Then
                divRadCaptcha.Visible = True
            End If
        Next

        If Not Page.IsPostBack Then

            'Set the Page Title
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Suggestion_FrontEnd.Suggestion_Default_HeaderTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Suggestion_FrontEnd.Suggestion_Default_Heading)

            'Load the category dropdown list
            BindCategoryDropDownListData()

            'Set category drop-down to 'Select'
            rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Suggestion_FrontEnd.Suggestion_Default_Uncategorized, ""))
            rcbCategoryID.SelectedValue = ""
        End If

    End Sub

    Public Sub BindCategoryDropDownListData()
        'Here we bind the dropdown list to categories
        Dim dtCategory As DataTable = CategoryDAL.GetCategoryList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)

        With rcbCategoryID
            .DataSource = dtCategory
            .DataValueField = "categoryID"
            .DataTextField = "categoryName"

        End With
        rcbCategoryID.DataBind()

    End Sub

    Private Sub addSuggestionRecord()

        If intMemberID = 0 Then
            intMemberID = Integer.MinValue
        End If

        Dim intCategoryID As Integer = Integer.MinValue
        If Not rcbCategoryID.SelectedValue = "" Then
            intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
        End If

        Dim strEmailAddress As String = txtEmailAddress.Text.Trim()

        Dim strSuggestion As String = txtSuggestion.Text.Trim()

        'Suggestion Defaults
        Dim boolAvailableToAllSites As Boolean = False
        Dim boolStatus As Boolean = True

        Dim dtPublication As DateTime = DateTime.MinValue

        Dim dtExpiration As DateTime = DateTime.MinValue

        Dim strMetaTitle As String = String.Empty
        Dim strMetaKeywords As String = String.Empty
        Dim strMetaDescription As String = String.Empty
        Dim strMetaOther As String = String.Empty

        Dim listGroupIDs As String = String.Empty
        Dim listMemberIDs As String = String.Empty
        Dim strSearchTagID As String = String.Empty

        Dim intVersion As Integer = 1
        Dim dtDateTimeStamp As DateTime = DateTime.Now

        Dim intAuthorID_member As Integer = intMemberID
        Dim intAuthorID_admin As Integer = Integer.MinValue

        Dim intModifiedID_member As Integer = Integer.MinValue
        Dim intModifiedID_admin As Integer = Integer.MinValue

        SuggestionDAL.InsertSuggestion(intSiteID, boolAvailableToAllSites, strSuggestion, strEmailAddress, intMemberID, intCategoryID, dtPublication, dtExpiration, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, dtDateTimeStamp, intAuthorID_member, intAuthorID_admin, intModifiedID_member, intModifiedID_admin)

        sendEmail()

        pnlForm.Visible = False
        pnlThanks.Visible = True
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        If Page.IsValid Then
            addSuggestionRecord()
        End If

    End Sub

    Protected Sub sendEmail()

        ' Send Suggestion Confirmation to user
        If txtEmailAddress.Text.Trim().Length > 0 Then

            'We have no swap out data for this email
            Dim EmailSwapoutData_User As New Hashtable()

            'Populate the list of recipients
            Dim listRecipientEmailAddress_User As New ArrayList()
            listRecipientEmailAddress_User.Add(txtEmailAddress.Text.Trim())

            'Send this information to our email DAL with Email Type ID = 12 -> Suggestion Email - Sent To User
            EmailDAL.SendEmail(listRecipientEmailAddress_User, 12, intSiteID, EmailSwapoutData_User)

        End If

        'Always send an email to the appropriate administrator
        'Send Email to site administrator informing them of a new member
        Dim EmailSwapoutData_Administrator As New Hashtable()

        'Add the members salutation, firstname and last name and to this email
        Dim strEmailAddress As String
        If txtEmailAddress.Text.Trim().Length > 0 Then
            strEmailAddress = txtEmailAddress.Text.Trim()
        Else
            strEmailAddress = Resources.Suggestion_FrontEnd.Suggestion_Default_EmailAddress_NotSupplied
        End If
        EmailSwapoutData_Administrator("EmailAddress") = strEmailAddress

        EmailSwapoutData_Administrator("Suggestion") = txtSuggestion.Text.Trim()


        'Send this information to our email DAL with Email Type ID = 13 -> Suggestion Email - Sent To Administrator -> This is an Administrator Email
        EmailDAL.SendEmail(13, intSiteID, EmailSwapoutData_Administrator)

    End Sub

End Class
