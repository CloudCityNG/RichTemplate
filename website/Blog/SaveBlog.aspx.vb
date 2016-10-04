Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Xml

Partial Class Blog_SaveBlog
    Inherits RichTemplateLanguagePage

    'Change this if you want to automatically approve Blog Submissions
    Const bAutomaticApproval As Boolean = False

    Dim ModuleTypeID As Integer = 1

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Setup Rad Controls
        CommonWeb.SetupRadEditor(Me.Page, txtBody, "~/editorConfig/toolbars/ToolsFileFrontEnd.xml", intSiteID)

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

                LoadBlog()
            Else
                'User is not logged in or we do not allow online submissions so redirect them to the blog listing
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

    Private Sub LoadBlog()

        'Set the default header title
        Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Blog_FrontEnd.Blog_SaveBlog_HeaderTitle

        If Not Request.QueryString("ID") Is Nothing Then
            Dim intBlogID As Integer = Convert.ToInt32(Request.QueryString("ID"))

            Dim dtBlog As DataTable = BlogDAL.GetBlog_ByBlogIDAndSiteID(intBlogID, intSiteID)
            If dtBlog.Rows.Count > 0 Then
                CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Blog_FrontEnd.Blog_SaveBlog_HeadingUpdate)
                btnAddEditBlog.Text = Resources.Blog_FrontEnd.Blog_SaveBlog_ButtonUpdate

                Dim drBlog As DataRow = dtBlog.Rows(0)

                'First we check if the current user was the user who actually created this Blog
                Dim boolUserCreatedThis As Boolean = False
                If Not drBlog("authorID_member") Is DBNull.Value Then
                    Dim intAuthorID_Member As Integer = Convert.ToInt32(drBlog("authorID_member"))
                    If intMemberID > 0 And intMemberID = intAuthorID_Member Then
                        boolUserCreatedThis = True
                    End If
                End If

                'Only continue populating this Save Blog page if the user actually created this blog, if not then redirect them to the BLOG detail page for this blog
                If boolUserCreatedThis Then
                    aBack.HRef = "Default.aspx?id=" & intBlogID.ToString()

                    Dim strTitle As String = drBlog("title").ToString()

                    'Set the Page Title
                    Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Blog_FrontEnd.Blog_SaveBlog_HeaderTitle + " - " + strTitle

                    txtTitle.Text = strTitle
                    txtBody.Content = drBlog("Body").ToString()

                    If (drBlog("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Blog_FrontEnd.Blog_SaveBlog_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drBlog("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drBlog("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Blog_FrontEnd.Blog_SaveBlog_UnCategorized, ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Blog_FrontEnd.Blog_SaveBlog_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    End If

                    version.Text = drBlog("Version").ToString
                Else
                    'User did not create this Blog, so redirect to the blogs detail page
                    Response.Redirect("Default.aspx?id=" & intBlogID)
                End If
            Else
                'ID was supplied but could not be found for this record, so redirect to the modules default page
                Response.Redirect("Default.aspx")
            End If
        Else
            aBack.HRef = "Default.aspx"
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Blog_FrontEnd.Blog_SaveBlog_HeadingAdd)
            btnAddEditBlog.Text = Resources.Blog_FrontEnd.Blog_SaveBlog_ButtonAdd

            rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Blog_FrontEnd.Blog_SaveBlog_UnCategorized, ""))
            rcbCategoryID.SelectedValue = ""

        End If
    End Sub

    Protected Sub addEditBlog()
        If intMemberID > 0 Then
            If Request("ID") Is Nothing Then

                Dim strTitle As String = Me.txtTitle.Text.Trim().ToString()

                Dim intCategoryID As Integer = Integer.MinValue
                If Not rcbCategoryID.SelectedValue = "" Then
                    intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
                End If

                Dim strBodyContent As String = txtBody.Content.ToString()

                Dim authorID_member As Integer = intMemberID
                Dim authorID_admin As Integer = Integer.MinValue

                Dim intModifiedID_member As Integer = Integer.MinValue
                Dim intModifiedID_admin As Integer = Integer.MinValue


                'Default values for user submitted blogs
                Dim boolStatus As Boolean = True 'default for user submitted blogs

                Dim dtPublication As DateTime = DateTime.MinValue
                Dim dtExpiration As DateTime = DateTime.MinValue

                Dim boolAvailableToAllSites As Boolean = False ' Default this member-created blog to only be viewable for this site

                Dim strMetaTitle As String = String.Empty
                Dim strMetaKeywords As String = String.Empty
                Dim strMetaDescription As String = String.Empty
                Dim strMetaOther As String = String.Empty


                Dim listGroupIDs As String = String.Empty
                Dim listMemberIDs As String = String.Empty
                Dim strSearchTagID As String = String.Empty

                Dim intVersion As Integer = 1
                Dim dtDateTimeStamp As DateTime = DateTime.Now

                'INSERT Blog
                Dim intBlogID As Integer = BlogDAL.InsertBlog(intSiteID, boolAvailableToAllSites, strTitle, intCategoryID, dtPublication, dtExpiration, strBodyContent, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, dtDateTimeStamp, authorID_member, authorID_admin, intModifiedID_member, intModifiedID_admin)

                SendEmail()

                If Not bAutomaticApproval Then
                    'Show Blog Record has been submitted, and awaiting approval Message
                    divModuleContentMain.Visible = False
                    divModuleContentSubmitted.Visible = True
                Else
                    'Then set access for this record to EVERYONE, and Send the user to this newly created blog
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intBlogID, 0, Integer.MinValue)
                    Response.Redirect("default.aspx?id=" & intBlogID)
                End If

            Else
                Dim intBlogID As Integer = Request.QueryString("ID")

                Dim strTitle As String = Me.txtTitle.Text.Trim().ToString()

                Dim intCategoryID As Integer = Integer.MinValue
                If Not rcbCategoryID.SelectedValue = "" Then
                    intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
                End If

                Dim strBodyContent As String = txtBody.Content.ToString()

                Dim intVersion As Integer = 1
                If Not IsDBNull(version.Text.Trim()) And version.Text.Trim() <> "" Then
                    intVersion = Convert.ToInt32(version.Text.Trim())
                    intVersion = intVersion + 1
                End If

                Dim intModifiedID_member As Integer = Integer.MinValue
                Dim intModifiedID_admin As Integer = Integer.MinValue

                BlogDAL.UpdateBlog_ByBlogID_FrontEnd(intBlogID, strTitle, intCategoryID, strBodyContent, intVersion, intModifiedID_member, intModifiedID_admin)

                Response.Redirect("default.aspx?id=" & intBlogID)
            End If
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub btnAddEditBlog_Click(ByVal sender As Object, ByVal e As EventArgs)
        If IsValid Then
            addEditBlog()
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not Request.QueryString("ID") Is Nothing Then
            Dim intBlogID As String = Request.QueryString("ID")
            Response.Redirect("default.aspx?id=" & intBlogID.ToString())
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub SendEmail()

        ' Send Blog Confirmation to user
        'First get the member who submitted this blog record
        Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intMemberID)
        If dtMember.Rows.Count > 0 Then
            Dim drMembrer As DataRow = dtMember.Rows(0)

            Dim strEmailAddress As String = drMembrer("Email").ToString()
            Dim strBlogTitle As String = txtTitle.Text.Trim()

            'We have no swap out data for this email
            Dim EmailSwapoutData_User As New Hashtable()
            EmailSwapoutData_User("BlogTitle") = strBlogTitle

            'Populate the list of recipients
            Dim listRecipientEmailAddress_User As New ArrayList()
            listRecipientEmailAddress_User.Add(strEmailAddress)

            'Send this information to our email DAL with Email Type ID = 14 -> Blog Submitted - Sent To User
            EmailDAL.SendEmail(listRecipientEmailAddress_User, 14, intSiteID, EmailSwapoutData_User)


            'Always send an email to the appropriate administrator
            'Send Email to site administrator informing them of a new blog entry
            Dim EmailSwapoutData_Administrator As New Hashtable()

            'Add the members ID and email address to this email
            EmailSwapoutData_Administrator("EmailAddress") = strEmailAddress
            EmailSwapoutData_Administrator("BlogTitle") = strBlogTitle

            Dim strCategoryName As String = Resources.Blog_FrontEnd.Blog_SaveBlog_UnCategorized
            If rcbCategoryID.SelectedValue.Length > 0 Then
                Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryID(rcbCategoryID.SelectedValue)
                If dtCategory.Rows.Count > 0 Then
                    Dim drCategory As DataRow = dtCategory.Rows(0)
                    strCategoryName = drCategory("CategoryName").ToString()
                End If
            End If
            EmailSwapoutData_Administrator("BlogCategory") = strCategoryName

            'Send this information to our email DAL with Email Type ID = 15 -> Blog Submitted - Sent To Administrator -> This is an Administrator Email
            EmailDAL.SendEmail(15, intSiteID, EmailSwapoutData_Administrator)
        End If
    End Sub
End Class
