Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Xml

Partial Class Faq_SaveFaq
    Inherits RichTemplateLanguagePage

    'Change this if you want to automatically approve Faq Submissions
    Const bAutomaticApproval As Boolean = False

    Dim ModuleTypeID As Integer = 6

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Setup Rad Controls
        CommonWeb.SetupRadEditor(Me.Page, txtAnswer, "~/editorConfig/toolbars/ToolsFileFrontEnd.xml", intSiteID)

        'Check access and setup back button
        If Not IsPostBack Then

            Dim boolAllowOnlineSubmissions As Boolean = False
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "allow_online_submissions" Then
                    boolAllowOnlineSubmissions = True
                End If
            Next

            If boolAllowOnlineSubmissions AndAlso intMemberID > 0 Then
                BindCategoryDropDownListData()

                LoadFaq()
            Else
                'User is not logged in or we do not allow online submissions so redirect them to the faq listing
                Response.Redirect("default.aspx")
            End If

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

    Private Sub LoadFaq()

        'Set the default header title
        Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Faq_FrontEnd.Faq_SaveFaq_HeaderTitle

        If Not Request.QueryString("ID") Is Nothing Then
            Dim intFaqID As Integer = Convert.ToInt32(Request.QueryString("ID"))

            Dim dtFaq As DataTable = FaqDAL.GetFaq_ByFaqIDAndSiteID(intFaqID, intSiteID)
            If dtFaq.Rows.Count > 0 Then
                CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Faq_FrontEnd.Faq_SaveFaq_HeadingUpdate)
                btnAddEditFaq.Text = Resources.Faq_FrontEnd.Faq_SaveFaq_ButtonUpdate

                Dim drFaq As DataRow = dtFaq.Rows(0)

                'First we check if the current user was the user who actually created this FAQ
                Dim boolUserCreatedThis As Boolean = False
                If Not drFaq("authorID_member") Is DBNull.Value Then
                    Dim intAuthorID_Member As Integer = Convert.ToInt32(drFaq("authorID_member"))
                    If intMemberID > 0 And intMemberID = intAuthorID_Member Then
                        boolUserCreatedThis = True
                    End If
                End If

                'Only continue populating this Save Faq page if the user actually created this faq, if not then redirect them to the FAQ detail page for this faq
                If boolUserCreatedThis Then
                    aBack.HRef = "Default.aspx?id=" & intFaqID.ToString()

                    Dim strQuestion As String = drFaq("question")

                    'Set the Page Title
                    Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Faq_FrontEnd.Faq_SaveFaq_HeaderTitle + " - " + strQuestion

                    txtQuestion.Text = strQuestion
                    txtAnswer.Content = drFaq("answer").ToString()

                    If (drFaq("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Faq_FrontEnd.Faq_SaveFaq_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drFaq("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drFaq("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Faq_FrontEnd.Faq_SaveFaq_UnCategorized, ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Faq_FrontEnd.Faq_SaveFaq_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    End If

                    version.Text = drFaq("Version").ToString
                Else
                    'User did not create this FAQ, so redirect to the faqs the detail page
                    Response.Redirect("Default.aspx?id=" & intFaqID)
                End If

            Else
                'ID was supplied but could not be found, so redirect to the modules default page
                Response.Redirect("Default.aspx")
            End If
        Else
            aBack.HRef = "Default.aspx"
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Faq_FrontEnd.Faq_SaveFaq_HeadingAdd)
            btnAddEditFaq.Text = Resources.Faq_FrontEnd.Faq_SaveFaq_ButtonAdd


            rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Faq_FrontEnd.Faq_SaveFaq_UnCategorized, ""))
            rcbCategoryID.SelectedValue = ""

        End If
    End Sub

    Protected Sub addEditFaq()
        If intMemberID > 0 Then
            If Request("ID") Is Nothing Then

                Dim strQuestion As String = Me.txtQuestion.Text.Trim().ToString()

                Dim intCategoryID As Integer = Integer.MinValue
                If Not rcbCategoryID.SelectedValue = "" Then
                    intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
                End If

                Dim strAnswerContent As String = txtAnswer.Content.ToString()

                Dim authorID_member As Integer = intMemberID
                Dim authorID_admin As Integer = Integer.MinValue

                Dim intModifiedID_member As Integer = Integer.MinValue
                Dim intModifiedID_admin As Integer = Integer.MinValue


                'Default values for user submitted faqs
                Dim boolStatus As Boolean = True 'default for user submitted faqs

                Dim dtPublication As DateTime = DateTime.MinValue
                Dim dtExpiration As DateTime = DateTime.MinValue

                Dim boolAvailableToAllSites As Boolean = False ' Default this member-created FAQ to only be viewable for this site

                Dim strMetaTitle As String = String.Empty
                Dim strMetaKeywords As String = String.Empty
                Dim strMetaDescription As String = String.Empty
                Dim strMetaOther As String = String.Empty


                Dim listGroupIDs As String = String.Empty
                Dim listMemberIDs As String = String.Empty
                Dim strSearchTagID As String = String.Empty

                Dim intVersion As Integer = 1
                Dim dtDateTimeStamp As DateTime = DateTime.Now

                'INSERT FAQ
                Dim intFaqID As Integer = FaqDAL.InsertFaq(intSiteID, boolAvailableToAllSites, strQuestion, strAnswerContent, intCategoryID, dtPublication, dtExpiration, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, dtDateTimeStamp, authorID_member, authorID_admin, intModifiedID_member, intModifiedID_admin)

                SendEmail()

                If Not bAutomaticApproval Then
                    'Show FAQ Record has been submitted, and awaiting approval Message
                    divModuleContentMain.Visible = False
                    divModuleContentSubmitted.Visible = True
                Else
                    'Then set access for this record to EVERYONE, and Send the user to this newly created faq
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intFaqID, 0, Integer.MinValue)
                    Response.Redirect("default.aspx?id=" & intFaqID)
                End If

            Else
                Dim intFaqID As Integer = Request.QueryString("ID")

                Dim strQuestion As String = Me.txtQuestion.Text.Trim().ToString()

                Dim intCategoryID As Integer = Integer.MinValue
                If Not rcbCategoryID.SelectedValue = "" Then
                    intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
                End If

                Dim strAnswerContent As String = txtAnswer.Content.ToString()

                Dim intVersion As Integer = 1
                If Not IsDBNull(version.Text.Trim()) And version.Text.Trim() <> "" Then
                    intVersion = Convert.ToInt32(version.Text.Trim())
                    intVersion = intVersion + 1
                End If

                Dim intModifiedID_member As Integer = Integer.MinValue
                Dim intModifiedID_admin As Integer = Integer.MinValue

                FaqDAL.UpdateFaq_ByFaqID_FrontEnd(intFaqID, strQuestion, intCategoryID, strAnswerContent, intVersion, intModifiedID_member, intModifiedID_admin)

                Response.Redirect("default.aspx?id=" & intFaqID)
            End If
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub btnAddEditFaq_Click(ByVal sender As Object, ByVal e As EventArgs)
        If IsValid Then
            addEditFaq()
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not Request.QueryString("ID") Is Nothing Then
            Dim intFaqID As String = Request.QueryString("ID")
            Response.Redirect("Default.aspx?id=" & intFaqID.ToString())
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub SendEmail()

        ' Send FAQ Confirmation to user
        'First get the member who submitted this faq record
        Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intMemberID)
        If dtMember.Rows.Count > 0 Then
            Dim drMembrer As DataRow = dtMember.Rows(0)

            Dim strEmailAddress As String = drMembrer("Email").ToString()
            Dim strFaqQuestion As String = txtQuestion.Text.Trim()

            Dim strCategoryName As String = Resources.Faq_FrontEnd.Faq_SaveFaq_UnCategorized
            If rcbCategoryID.SelectedValue.Length > 0 Then
                Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryID(rcbCategoryID.SelectedValue)
                If dtCategory.Rows.Count > 0 Then
                    Dim drCategory As DataRow = dtCategory.Rows(0)
                    strCategoryName = drCategory("CategoryName").ToString()
                End If
            End If

            'We have no swap out data for this email
            Dim EmailSwapoutData_User As New Hashtable()
            EmailSwapoutData_User("FaqQuestion") = strFaqQuestion

            'Populate the list of recipients
            Dim listRecipientEmailAddress_User As New ArrayList()
            listRecipientEmailAddress_User.Add(strEmailAddress)

            'Send this information to our email DAL with Email Type ID = 20 -> FAQ Submitted - Sent To User
            EmailDAL.SendEmail(listRecipientEmailAddress_User, 20, intSiteID, EmailSwapoutData_User)


            'Always send an email to the appropriate administrator
            'Send Email to site administrator informing them of a new FAQ entry
            Dim EmailSwapoutData_Administrator As New Hashtable()

            'Add the members ID and email address to this email
            EmailSwapoutData_Administrator("EmailAddress") = strEmailAddress
            EmailSwapoutData_Administrator("FaqQuestion") = strFaqQuestion
            EmailSwapoutData_Administrator("FaqCategory") = strCategoryName

            'Send this information to our email DAL with Email Type ID = 21 -> FAQ - Sent To Administrator -> This is an Administrator Email
            EmailDAL.SendEmail(21, intSiteID, EmailSwapoutData_Administrator)
        End If
    End Sub
End Class
