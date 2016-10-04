Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Class admin_site_editAdd
    Inherits RichTemplateLanguagePage


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim intSiteID = Integer.MinValue
        If Not Request.QueryString("ID") Is Nothing Then
            intSiteID = Convert.ToInt32(Request.QueryString("ID"))
        End If

        'Setup Rad Controls
        CommonWeb.SetupRadEditor(Page, txt_SiteDescription, intSiteID)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Site_Admin.Site_AddEdit_Header

        'Check the logged in user can view this page
        Dim intAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intAdminUserAccess > 2 Then
            ' Then show the Site Access Tab
            rtsSite.Tabs.FindTabByValue("1").Visible = True
        ElseIf intAdminUserAccess > 1 Then
            'Then the user is allowed to look at sites, but not Site Access
        Else
            Response.Redirect("~/richadmin/")
        End If

        If Not Page.IsPostBack Then

            BindAdminUserCheckBoxListData()
            BindAvailableModulesForSite()
            BindLanguageDropDown()
            BindPackageTypeDropDown()
            BindSiteDepthDropDown()

            If intSiteID > Integer.MinValue Then

                Dim dtSite As DataTable = SiteDAL.GetSite_ByID(intSiteID)
                If dtSite.Rows.Count > 0 Then
                    Dim drSite As DataRow = dtSite.Rows(0)

                    'Master Administrators have access to all sites, however other administrators only have access to sites they are allowed to view
                    If intAdminUserAccess < 3 Then 'If the current adminuser is not a Master Administrator then check they have access to this site
                        Dim dtSiteAccess_CurrentAdminUser As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID_Active(intSiteID, AdminUserDAL.GetCurrentAdminUserID())
                        If dtSiteAccess_CurrentAdminUser.Rows.Count = 0 Then
                            'So this AdminUser does not have access to this site, redirect to the default page
                            Response.Redirect("Default.aspx")
                        End If
                    End If

                    btnAddEdit.Text = Resources.Site_Admin.Site_AddEdit_ButtonUpdate
                    'If data is found, fill textboxes
                    rblStatus.SelectedValue = drSite("status").ToString

                    txt_Name.Text = drSite("SiteName")

                    If Not drSite("SiteDescription") Is DBNull.Value Then
                        txt_SiteDescription.Content = drSite("SiteDescription").ToString()
                    End If

                    txt_CompanyName.Text = drSite("CompanyName")

                    If Not drSite("CompanyStatement") Is DBNull.Value Then
                        txt_CompanyStatement.Text = drSite("CompanyStatement").ToString()
                    End If

                    Me.ddlLanguage.SelectedValue = drSite("LanguageID_Default").ToString()

                    If Not drSite("Address").ToString() = "" Then
                        ucAddress.LocationStreet = drSite("Address").ToString()
                    End If

                    If Not drSite("City").ToString() = "" Then
                        ucAddress.LocationCity = drSite("City").ToString()
                    End If

                    If Not drSite("StateID").ToString() = "" Then
                        ucAddress.LocationState = drSite("StateID").ToString()
                    End If

                    If Not drSite("ZipCode").ToString() = "" Then
                        ucAddress.LocationZipCode = drSite("ZipCode").ToString()
                    End If

                    If Not drSite("CountryID").ToString() = "" Then
                        ucAddress.LocationCountry = drSite("CountryID").ToString()
                    End If

                    If Not drSite("PhoneNumber") Is DBNull.Value Then
                        txt_PhoneNumber.Text = drSite("PhoneNumber").ToString()
                    End If

                    If Not drSite("FaxNumber") Is DBNull.Value Then
                        txt_FaxNumber.Text = drSite("FaxNumber").ToString()
                    End If

                    If Not drSite("EmailAddress") Is DBNull.Value Then
                        txt_EmailAddress.Text = drSite("EmailAddress").ToString()
                    End If

                    txt_DomainName.Text = drSite("Domain")

                    If Not drSite("SiteIdentifier") Is DBNull.Value Then
                        txt_SiteIdentifier.Text = drSite("SiteIdentifier").ToString()
                    End If

                    If Not drSite("SiteIdentifier_LDAP") Is DBNull.Value Then
                        txt_SiteIdentifierLDAP.Text = drSite("SiteIdentifier_LDAP").ToString()
                    End If

                    rcbPackageType.SelectedValue = drSite("PackageTypeID").ToString()
                    ddlSiteDepth.SelectedValue = drSite("SiteDepth").ToString()
                    rblUseThreeColumnLayout.SelectedValue = drSite("UseThreeColumnLayout").ToString

                    rblGroupsAndUsersPublicSection.SelectedValue = drSite("Webpage_PublicSection_EnableGroupsAndUsers").ToString
                    rblGroupsAndUsersMemberSection.SelectedValue = drSite("Webpage_MemberSection_EnableGroupsAndUsers").ToString

                    ' Populate the Site's Admin User Access
                    Dim dtSiteAccess As DataTable = SiteDAL.GetSiteAccessList_ForAdminUser_BySiteID_Active(intSiteID)
                    For Each drSiteAccess As DataRow In dtSiteAccess.Rows

                        'Select the AdminUser from our AdminUser Checkbox list
                        If Not drSiteAccess("AdminUserID") Is DBNull.Value Then
                            Dim intAdminUserID As Integer = Convert.ToInt32(drSiteAccess("AdminUserID"))
                            Dim liAdminUser As ListItem = cblAdminUserList.Items.FindByValue(intAdminUserID)
                            If Not liAdminUser Is Nothing Then
                                liAdminUser.Selected = True
                            End If
                        End If

                    Next

                    ' Populate the sites available modules
                    Dim dtModules As DataTable = ModuleDAL.GetModuleList_BySiteID_FrontEnd(intSiteID)
                    For Each drModule As DataRow In dtModules.Rows
                        Dim intModuleTypeID As Integer = Convert.ToInt32(drModule("ModuleTypeID"))
                        Dim liModule As ListItem = cblModulesForSite.Items.FindByValue(intModuleTypeID)
                        If Not liModule Is Nothing Then
                            liModule.Selected = True
                        End If
                    Next

                    'Finally as we are updating the site, we want to show and set some neccessary caution messages
                    spanCautionMessageDomainName.Visible = True
                    spanCautionMessageSiteIdentifier.Visible = True
                    spanCautionMessageSiteIdentifierLDAP.Visible = True
                    txt_DomainName.Enabled = False
                    txt_SiteIdentifier.Enabled = False
                    txt_SiteIdentifierLDAP.Enabled = False
                    lnkAllowChangeDomainName.Visible = True
                    lnkAllowChangeSiteIdentifier.Visible = True
                    lnkAllowChangeSiteIdentifierLDAP.Visible = True
                Else
                    'The siteID does not exist so redirect them to the default page
                    Response.Redirect("default.aspx")
                End If

            Else
                'Only Master Administrators have access to add sites
                'But first check if the number of allowed created sites has no exceeded the threshold
                Dim intSiteCount As Integer = SiteDAL.GetSiteList().Rows.Count
                Dim intSiteMaxCount As Integer = Convert.ToInt32(ConfigurationManager.AppSettings("SiteMaxCount"))
                If intAdminUserAccess > 2 AndAlso intSiteCount < intSiteMaxCount Then 'If the current adminuser is not a Master Administrator then redirect them to the default page.

                    btnAddEdit.Text = Resources.Site_Admin.Site_AddEdit_ButtonAdd
                    rblStatus.SelectedValue = True
                    rblGroupsAndUsersPublicSection.SelectedValue = False
                    rblGroupsAndUsersMemberSection.SelectedValue = False

                    rcbPackageType.SelectedValue = "3" 'Default to Platinum, this will change once we have actually taken the packageType into account
                    rblUseThreeColumnLayout.SelectedValue = False

                    'Finally as we are add the site, we want to hide all caution messages, as they may need setting without the admin having doubt
                    spanCautionMessageDomainName.Visible = True 'show the admin that caution is needed
                    spanCautionMessageSiteIdentifier.Visible = True 'show the admin that caution is needed
                    spanCautionMessageSiteIdentifierLDAP.Visible = True 'show the admin that caution is needed
                    txt_DomainName.Enabled = True
                    txt_SiteIdentifier.Enabled = True
                    txt_SiteIdentifierLDAP.Enabled = True
                    lnkAllowChangeDomainName.Visible = False
                    lnkAllowChangeSiteIdentifier.Visible = False
                    lnkAllowChangeSiteIdentifierLDAP.Visible = False
                Else
                    Response.Redirect("Default.aspx")
                End If

            End If

        End If

    End Sub

    Private Sub BindLanguageDropDown()

        'Populate the possible languages our site supports
        Dim dtLanguage As DataTable = LanguageDAL.GetLanguageList()
        ddlLanguage.Items.Clear()
        ddlLanguage.DataValueField = "ID"
        ddlLanguage.DataTextField = "Language"
        ddlLanguage.DataSource = dtLanguage
        ddlLanguage.DataBind()

        ddlLanguage.Items.Insert(0, New ListItem("-- " & Resources.Site_Admin.Site_AddEdit_Language_PleaseSelect & " --", ""))
    End Sub

    Public Sub BindAdminUserCheckBoxListData()

        Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUserList_ByAccessLevel(2) ' Note we only want to show all users that are not Master Administrators, as Master Administrators have access to all sites irrespective of their SiteAccess
        For Each drAdminUser In dtAdminUser.Rows()
            Dim intAdminUser As String = drAdminUser("id").ToString()

            Dim strSalutation_LangaugeSpecific As String = String.Empty
            If Not drAdminUser("Salutation_LanguageProperty") Is DBNull.Value Then
                Dim strSalutation_LanguageProperty As String = drAdminUser("Salutation_LanguageProperty")
                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
            End If
            Dim strAdminUserName As String = CommonWeb.FormatName(strSalutation_LangaugeSpecific, drAdminUser("first_Name").ToString(), drAdminUser("last_Name"))
            cblAdminUserList.Items.Add(New ListItem(strAdminUserName, intAdminUser))
        Next

    End Sub

    Private Sub BindPackageTypeDropDown()

        'Populate Package Types
        rcbPackageType.Items.Clear()
        Dim dtPackageType As DataTable = PackageDAL.GetPackageTypeList()
        For Each drPackageType As DataRow In dtPackageType.Rows
            Dim intPackageTypeID As Integer = drPackageType("PackageTypeID")

            Dim strPackage_LanguageProperty As String = drPackageType("PackageType_LanguageProperty")
            Dim strPackage_LangaugeSpecific As String = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strPackage_LanguageProperty)

            Dim liPackage As New RadComboBoxItem(strPackage_LangaugeSpecific, intPackageTypeID)
            rcbPackageType.Items.Add(liPackage)
        Next

    End Sub

    Private Sub BindSiteDepthDropDown()

        'Populate Site Depth Options
        ddlSiteDepth.Items.Clear()
        For index = 1 To 5
            Dim liSiteDepth As New ListItem(index.ToString(), index.ToString())
            ddlSiteDepth.Items.Add(liSiteDepth)
        Next

    End Sub

    Private Sub BindAvailableModulesForSite()
        cblModulesForSite.Items.Clear()

        'Populate the possible modules this admin can access
        Dim dtModules As DataTable = ModuleDAL.GetModuleTypeList()
        For Each drModule As DataRow In dtModules.Rows
            Dim intModuleTypeID As Integer = Convert.ToInt32(drModule("ID"))
            Dim boolModuleActive As Boolean = Convert.ToBoolean(drModule("active"))
            If boolModuleActive Then

                'Get the name of the module, based on the users preferred language
                Dim strModuleLanguageFileName_Admin As String = drModule("moduleLanguageFileName_Admin")
                Dim strModuleName_LanguageSpecific As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFileName_Admin, "_SiteWide_AdminUser_Default_ModuleName")

                Dim liModule As New ListItem(strModuleName_LanguageSpecific, intModuleTypeID)
                cblModulesForSite.Items.Add(liModule)
            End If

        Next
    End Sub

    Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelButton.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click

        addUpdateRecord()

        'Now we must reload the basefrm, treefrm and contents frame AND Banner frames site dropdown, to reflect the site change
        CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, "/admin/site", "/admin/richtemplate_list_administration.aspx?sel=site", "/admin/richtemplate_list_contents.aspx?sel=administration", "/admin/richtemplate_top_row.aspx")

    End Sub

    Protected Sub lnkAllowChangeDomainName_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAllowChangeDomainName.Click
        txt_DomainName.Enabled = Not txt_DomainName.Enabled
    End Sub

    Protected Sub lnkAllowChangeSiteIdentifier_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAllowChangeSiteIdentifier.Click
        txt_SiteIdentifier.Enabled = Not txt_SiteIdentifier.Enabled
    End Sub

    Protected Sub lnkAllowChangeSiteIdentifierLDAP_OnClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAllowChangeSiteIdentifierLDAP.Click
        txt_SiteIdentifierLDAP.Enabled = Not txt_SiteIdentifierLDAP.Enabled
    End Sub

    Protected Sub addUpdateRecord()

        If Request.QueryString("ID") Is Nothing Then

            'First check if the number of allowed created sites has no exceeded the threshold, as the time the AdminUser clicks add new site, we do a check that this limit has not yet been reached.
            Dim intSiteCount As Integer = SiteDAL.GetSiteList().Rows.Count
            Dim intSiteMaxCount As Integer = Convert.ToInt32(ConfigurationManager.AppSettings("SiteMaxCount"))
            If intSiteCount < intSiteMaxCount Then
                Dim strSiteName As String = txt_Name.Text.Trim()
                Dim strSiteDescription As String = txt_SiteDescription.Content.Trim()

                Dim strCompanyName As String = txt_CompanyName.Text.Trim()
                Dim strCompanyStatement As String = txt_CompanyStatement.Text.Trim()

                Dim intLanguageID_Default As Integer = Convert.ToInt32(ddlLanguage.SelectedValue)

                Dim strAddress As String = ucAddress.LocationStreet
                Dim strCity As String = ucAddress.LocationCity
                Dim intStateID As Integer = ucAddress.LocationState
                Dim strZipCode As String = ucAddress.LocationZipCode
                Dim intCountryID As Integer = ucAddress.LocationCountry

                Dim strPhoneNumber As String = txt_PhoneNumber.Text.Trim()
                Dim strFaxNumber As String = txt_FaxNumber.Text.Trim()
                Dim strEmailAddress As String = txt_EmailAddress.Text.Trim()

                Dim strDomain As String = txt_DomainName.Text.Trim()
                Dim strSiteIdentifier As String = txt_SiteIdentifier.Text
                Dim strSiteIdentifierLDAP As String = txt_SiteIdentifierLDAP.Text

                Dim boolStatus As Boolean = Convert.ToBoolean(rblStatus.SelectedValue)
                Dim boolEnableGroupsAndUsers_PublicSection As Boolean = Convert.ToBoolean(rblGroupsAndUsersPublicSection.SelectedValue)
                Dim boolEnableGroupsAndUsers_MemberSection As Boolean = Convert.ToBoolean(rblGroupsAndUsersMemberSection.SelectedValue)

                Dim intPackageTypeID As Integer = Convert.ToInt32(rcbPackageType.SelectedValue)
                Dim intSiteDepth As Integer = Convert.ToInt32(ddlSiteDepth.SelectedValue)
                Dim boolUseThreeColumnLayout As Integer = Convert.ToBoolean(rblUseThreeColumnLayout.SelectedValue)

                Dim dtDateCreated As DateTime = DateTime.Now

                'Add the new Site row
                Dim intWebInfoID_HomePage As Integer = Integer.MinValue
                Dim intWebInfoID_Header As Integer = Integer.MinValue
                Dim intWebInfoID_Footer As Integer = Integer.MinValue
                Dim intSiteID As Integer = SiteDAL.InsertSite(strSiteName, strSiteDescription, strCompanyName, strCompanyStatement, intLanguageID_Default, strAddress, strCity, intStateID, strZipCode, intCountryID, strPhoneNumber, strFaxNumber, strEmailAddress, strDomain, intPackageTypeID, intSiteDepth, boolUseThreeColumnLayout, boolEnableGroupsAndUsers_PublicSection, boolEnableGroupsAndUsers_MemberSection, intWebInfoID_HomePage, intWebInfoID_Header, intWebInfoID_Footer, strSiteIdentifier, strSiteIdentifierLDAP, boolStatus, dtDateCreated)

                'Setup default datarows for our new Site, by dynamically creating essential multi-site records, e.g. email rows in the emailTemplate table etc.
                SetupNewSiteData(intSiteID)

                'Read all AdminUserIDs for this site and Insert SiteAccess Information
                For Each liAdminUser As ListItem In cblAdminUserList.Items
                    Dim intAdminUserID As Integer = Convert.ToInt32(liAdminUser.Value)

                    'Default Site access to False, for all permissions, the current Admin User can change these by viewing the AdminUser Administration Menu
                    Dim boolSiteAccessActive As Boolean = liAdminUser.Selected
                    SiteDAL.InsertSiteAccess_ForAdminUser(intSiteID, intAdminUserID, False, False, False, False, False, False, False, False, False, False, String.Empty, boolSiteAccessActive)
                Next

                'Finally Add The Site Binding
                'SiteDAL.InsertSiteBindingIfDoesNotExist(strDomain)


            Else
                'From the time the AdminUser clicks add new site, we do a check that this limit has not yet been reached.
                Response.Redirect("default.aspx")
            End If


        Else

            Dim intSiteID As Integer = Convert.ToInt32(Request.QueryString("ID"))

            Dim strSiteName As String = txt_Name.Text.Trim()
            Dim strSiteDescription As String = txt_SiteDescription.Content.Trim()

            Dim strCompanyName As String = txt_CompanyName.Text.Trim()
            Dim strCompanyStatement As String = txt_CompanyStatement.Text.Trim()

            Dim intLanguageID_Default As Integer = Convert.ToInt32(ddlLanguage.SelectedValue)

            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry

            Dim strPhoneNumber As String = txt_PhoneNumber.Text.Trim()
            Dim strFaxNumber As String = txt_FaxNumber.Text.Trim()
            Dim strEmailAddress As String = txt_EmailAddress.Text.Trim()

            Dim strDomain As String = txt_DomainName.Text.Trim()
            Dim strSiteIdentifier As String = txt_SiteIdentifier.Text
            Dim strSiteIdentifierLDAP As String = txt_SiteIdentifierLDAP.Text

            Dim boolStatus As Boolean = Convert.ToBoolean(rblStatus.SelectedValue)
            Dim boolEnableGroupsAndUsers_PublicSection As Boolean = Convert.ToBoolean(rblGroupsAndUsersPublicSection.SelectedValue)
            Dim boolEnableGroupsAndUsers_MemberSection As Boolean = Convert.ToBoolean(rblGroupsAndUsersMemberSection.SelectedValue)

            Dim intPackageTypeID As Integer = Convert.ToInt32(rcbPackageType.SelectedValue)
            Dim intSiteDepth As Integer = Convert.ToInt32(ddlSiteDepth.SelectedValue)
            Dim boolUseThreeColumnLayout As Integer = Convert.ToBoolean(rblUseThreeColumnLayout.SelectedValue)

            'Update this site
            SiteDAL.UpdateSite_ByID(intSiteID, strSiteName, strSiteDescription, strCompanyName, strCompanyStatement, intLanguageID_Default, strAddress, strCity, intStateID, strZipCode, intCountryID, strPhoneNumber, strFaxNumber, strEmailAddress, strDomain, intPackageTypeID, intSiteDepth, boolUseThreeColumnLayout, boolEnableGroupsAndUsers_PublicSection, boolEnableGroupsAndUsers_MemberSection, strSiteIdentifier, strSiteIdentifierLDAP, boolStatus)

            'Update the modules available for the site
            For Each liModuleForSite As ListItem In cblModulesForSite.Items
                Dim intModuleTypeID As Integer = Convert.ToInt32(liModuleForSite.Value)
                Dim boolModuleActive As Boolean = liModuleForSite.Selected

                'Before we set this moduleType to be active for this site, check if we already have this moduleType as either active/not active for this site, if not insert the row, else update it
                Dim dtModuleActive As DataTable = ModuleDAL.GetModule_ByModuleTypeIDAndSiteID(intModuleTypeID, intSiteID)
                If dtModuleActive.Rows.Count = 0 Then
                    ModuleDAL.InsertModule(intSiteID, intModuleTypeID, String.Empty, boolModuleActive, Integer.MinValue)
                Else
                    ModuleDAL.UpdateModuleActive_ByModuleTypeIDAndSiteID(intModuleTypeID, intSiteID, boolModuleActive)
                End If

            Next

            'Update Site Access
            For Each liAdminUser As ListItem In cblAdminUserList.Items
                Dim boolSiteAccessActive As Boolean = liAdminUser.Selected
                Dim intAdminUserID As Integer = Convert.ToInt32(liAdminUser.Value)
                addUpdateSiteAccess(intSiteID, intAdminUserID, boolSiteAccessActive)
            Next

            'Finally Add The Site Binding, if it does not exists
            'SiteDAL.InsertSiteBindingIfDoesNotExist(strDomain)

            'As we are updating a site, if the site we are updating is the same as the currently access site, then we must re-login the current user, as the Site's available modules may have changed, as such the current logged in user's AllowModules must be updated
            Dim intCurrentSiteID As Integer = SiteDAL.GetCurrentSiteID_Admin()
            If intSiteID = intCurrentSiteID Then
                LoginAdminUser()
            End If

        End If

    End Sub

    Private Sub SetupNewSiteData(ByVal SiteID As Integer)

        'Setup site specific data directories, used for storing files in site specific locations
        SetupNewSiteData_DataDirectories(SiteID)
        'Setup a new batch of EmailTemplates based on our default list of templates with SiteID=0
        SetupNewSiteData_EmailTemplates(SiteID)

        'Setup a new batch of Modules based on our default list of modules with SiteID=0
        SetupNewSiteData_Module(SiteID)

        'Setup our inital WebInfo Pages Home Page and Footer
        Dim intWebInfoID_HomePage As Integer = SetupNewSiteData_WebInfoPages(Integer.MinValue, SiteID) 'HOME PAGE
        Dim intWebInfoID_Header As Integer = SetupNewSiteData_WebInfoPages(intWebInfoID_HomePage, SiteID) 'HEADER CONTAINER
        Dim intWebInfoID_Footer As Integer = SetupNewSiteData_WebInfoPages(intWebInfoID_HomePage, SiteID) 'FOOTER CONTAINER

        'Now we can set the Site's Homepage and footer, so we attach this to our new site, so our site has its own home page and footer
        SiteDAL.UpdateSite_WebInfoID_HomeAndHeaderAndFooter_ByID(SiteID, intWebInfoID_HomePage, intWebInfoID_Header, intWebInfoID_Footer)


    End Sub

    Private Sub SetupNewSiteData_DataDirectories(ByVal SiteID As Integer)
        Dim strServerPath As String = CommonWeb.GetServerPath()
        Dim rootFolder_Data As String = strServerPath & ConfigurationManager.AppSettings("DocumentVirtualDirectory")
        Dim rootFolder_DocumentModule As String = strServerPath & ConfigurationManager.AppSettings("DocumentModuleRootDirectory")

        Dim strDocumentFolder_Files As String = rootFolder_Data & "files\Site_" & SiteID.ToString() & "\"
        Dim strDocumentFolder_Images As String = rootFolder_Data & "images\Site_" & SiteID.ToString() & "\"
        Dim strDocumentFolder_Media As String = rootFolder_Data & "media\Site_" & SiteID.ToString() & "\"

        Dim strDocumentFolder_DocumentModule As String = rootFolder_DocumentModule & "Site_" & SiteID.ToString() & "\"

        'Create the data/files site-specific directory
        If Not Directory.Exists(strDocumentFolder_Files) Then
            Directory.CreateDirectory(strDocumentFolder_Files)
        End If

        'Create the data/images site-specific directory
        If Not Directory.Exists(strDocumentFolder_Images) Then
            Directory.CreateDirectory(strDocumentFolder_Images)
        End If

        'Create the data/media site-specific directory
        If Not Directory.Exists(strDocumentFolder_Media) Then
            Directory.CreateDirectory(strDocumentFolder_Media)
        End If

        'Create the data/DocumentModule site-specific directory
        If Not Directory.Exists(strDocumentFolder_DocumentModule) Then
            Directory.CreateDirectory(strDocumentFolder_DocumentModule)
        End If
    End Sub

    Private Sub SetupNewSiteData_EmailTemplates(ByVal SiteID As Integer)
        'Go through each EmailType and use this to create Email Templates for this new site
        Dim dtEmailType As DataTable = EmailDAL.GetEmailTypeList()
        For Each drEmailType As DataRow In dtEmailType.Rows
            Dim intEmailTypeID As Integer = Convert.ToInt32(drEmailType("EmailTypeID"))

            Dim strName As String = drEmailType("EmailTypeName").ToString()
            Dim strDescription As String = drEmailType("EmailTypeDescription").ToString()

            Dim strSenderEmailAddress As String = "sitename@sitedomain.com"
            Dim strSenderName As String = String.Empty

            Dim strReplyToEmailAddress As String = String.Empty
            Dim strReplyToName As String = String.Empty

            Dim strSubject As String = drEmailType("Subject_Default")
            Dim strBodyText As String = drEmailType("BodyText_Default")
            Dim strBodyHtml As String = drEmailType("BodyHtml_Default")

            Dim boolIsAdministrationEmail As Boolean = Convert.ToBoolean(drEmailType("IsAdministrationEmail"))

            'If this email Template's Type is an Administrator email, then set the RecipientEmailAddress
            Dim strRecipientEmailAddress As String = If(boolIsAdministrationEmail, "admin@sitedomain.com", String.Empty)

            Dim boolActive As Boolean = False

            EmailDAL.InsertEmailTemplate(intEmailTypeID, SiteID, strName, strDescription, strSenderEmailAddress, strSenderName, strReplyToEmailAddress, strReplyToName, strSubject, strBodyText, strBodyHtml, strRecipientEmailAddress, boolActive)

        Next

    End Sub

    Private Sub SetupNewSiteData_Module(ByVal SiteID As Integer)
        Dim dtModuleType As DataTable = ModuleDAL.GetModuleTypeList()
        Dim intOrderIndex As Integer = 0
        For Each drModuleType As DataRow In dtModuleType.Rows
            intOrderIndex = intOrderIndex + 1

            Dim intModuleTypeID As Integer = Convert.ToInt32(drModuleType("ID"))
            Dim strModuleContentHTML As String = String.Empty
            Dim boolActive As Boolean = False

            'Default module access to false, unless we find it is selected in our checkbox list of available modules for this site
            Dim liModule As ListItem = cblModulesForSite.Items.FindByValue(intModuleTypeID)
            If Not liModule Is Nothing AndAlso liModule.Selected Then
                boolActive = True
            End If

            ModuleDAL.InsertModule(SiteID, intModuleTypeID, strModuleContentHTML, boolActive, intOrderIndex)

        Next
    End Sub

    Private Function SetupNewSiteData_WebInfoPages(ByVal WebInfoID_Parent As Integer, ByVal SiteID As Integer) As Integer

        Dim intPageLevel As Integer = If(WebInfoID_Parent = Integer.MinValue, 1, 2)

        'Setup the Default Values
        Dim strPageName As String = "-" 'Note, we don't use a name for both home page and footer, as we'll use resource files language specific name
        Dim boolDefaultPage As Boolean = False
        Dim strMessage As String = String.Empty
        Dim strMessage2 As String = "<br/>" 'By Default we must supply a message here, so the message2 is not null, and the Admin User can then edit this. Otherwise an empty string causes the message2 to be null, hence the 'Edit' link in RichTemplate_List_Pages will be disabled

        'Get the current user and set the author, they didn't actually write this page but they did create the site
        Dim strAuthor As String = String.Empty
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
        If dtAdminUser.Rows.Count > 0 Then
            Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

            strAuthor = drAdminUser("username").ToString()
        End If

        Dim dtLastModified As DateTime = DateTime.MinValue

        Dim boolPending As Boolean = False
        Dim boolCheckedOut As Boolean = False
        Dim strCheckedID As String = String.Empty

        Dim boolLinkOnly As Boolean = False
        Dim strLinkURL As String = String.Empty
        Dim strLinkFrame As String = String.Empty

        Dim intSectionID As Integer = WebInfoID_Parent
        Dim strMetaDesc As String = String.Empty
        Dim strMetaTitle As String = String.Empty
        Dim strMetaKeyword As String = String.Empty

        Dim strPage_LinkName As String = "interior.aspx"
        Dim boolSearchable As Boolean = False
        Dim intLanguage As Integer = Integer.MinValue
        Dim strUrlPath As String = String.Empty

        Dim boolSecureMembers As Boolean = False
        Dim boolSecureEducation As Boolean = False

        Dim strSearchTags As String = ""
        Dim intNavigationColumnIndex As Integer = 1

        Dim boolInheritBannerImage As Boolean = If(WebInfoID_Parent = Integer.MinValue, False, True) 'If we have NO Parent ID then we are dealing with the Section/HomePage, InheritBannerImage is false, else we are dealing with a child footer page of the homepage (section) set InheritBannerImage = True

        Dim intWebInfoID As Integer = WebInfoDAL.InsertWebInfo(strPageName, WebInfoID_Parent, SiteID, intPageLevel, boolDefaultPage, strMessage, strMessage2, strAuthor, dtLastModified, boolPending, boolCheckedOut, strCheckedID, boolLinkOnly, strLinkURL, strLinkFrame, intSectionID, strMetaTitle, strMetaDesc, strMetaKeyword, strPage_LinkName, boolSearchable, intLanguage, strUrlPath, boolSecureMembers, boolSecureEducation, strSearchTags, intNavigationColumnIndex, boolInheritBannerImage)

        'Now add the 'EVERYONE' Access for this page as default access, which can not be changed, as this is used for home page and footer folder, rather than normal webinfo pages
        WebInfoDAL.InsertWebInfoAccess(intWebInfoID, 0, Integer.MinValue)

        Return intWebInfoID
    End Function

    Private Sub addUpdateSiteAccess(ByVal SiteID As Integer, ByVal AdminUserID As Integer, ByVal SiteAccessActive As Boolean)
        Dim dtSiteAccess_ForAdminUser As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID(SiteID, AdminUserID)

        'Just insert/update the SiteLevelAccess do not touch access permissions, as the permissions are set a global level
        If dtSiteAccess_ForAdminUser.Rows.Count = 0 Then
            'Inserting siteAccess with default permissions
            SiteDAL.InsertSiteAccess_ForAdminUser(SiteID, AdminUserID, False, False, False, False, False, False, False, False, False, False, String.Empty, SiteAccessActive)
        Else
            'Get Current SiteAccess Values
            Dim AllowWebContent As Boolean = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_WebContent"))

            Dim AllowSectionAdd As Boolean = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Section_Add"))
            Dim AllowPageAdd As Boolean = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Page_Add"))

            Dim AllowSectionEdit As Boolean = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Section_Edit"))
            Dim AllowPageEdit As Boolean = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Page_Edit"))

            Dim AllowSectionDelete As Boolean = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Section_Delete"))
            Dim AllowPageDelete As Boolean = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Page_Delete"))

            Dim AllowSectionRename As Boolean = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Section_Rename"))
            Dim AllowPageRename As Boolean = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Page_Rename"))

            Dim AllowPublish As Boolean = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Publish"))
            Dim AllowModules As String = dtSiteAccess_ForAdminUser.Rows(0)("Allow_Modules").ToString()

            SiteDAL.UpdateSiteAccess_ForAdminUser(SiteID, AdminUserID, AllowWebContent, AllowSectionAdd, AllowPageAdd, AllowSectionEdit, AllowPageEdit, AllowSectionDelete, AllowPageDelete, AllowSectionRename, AllowPageRename, AllowPublish, AllowModules, SiteAccessActive)
        End If

    End Sub

    Private Sub LoginAdminUser()
        'So get the current AdminUser's permissions
        Dim intCurrentSiteID As Integer = SiteDAL.GetCurrentSiteID_Admin()
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
        If dtAdminUser.Rows.Count > 0 Then
            Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

            Dim intAccessLevel As Integer = Convert.ToInt32(drAdminUser("Access_Level"))

            'Before we Handle permissions we check if this adminUser's permissions are at a SiteLevel or not
            Dim boolUseSiteLevelAccess As Boolean = Convert.ToBoolean(drAdminUser("UseSiteLevelAccess"))
            Dim drSiteAccess_AdminUser As DataRow = Nothing
            If boolUseSiteLevelAccess Then
                Dim dtSiteAccess_AdminUser As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID_Active(intCurrentSiteID, intAdminUserID)
                If dtSiteAccess_AdminUser.Rows.Count > 0 Then
                    drSiteAccess_AdminUser = dtSiteAccess_AdminUser.Rows(0)
                End If
            End If

            'The Admin User has access to at least one site, so log then in
            Dim strAllowModules As String = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, String.Empty, drSiteAccess_AdminUser("Allow_Modules")), drAdminUser("Allow_Modules_AllSites").ToString())
            Dim boolAllowWebContent As Boolean = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_WebContent")), Convert.ToBoolean(drAdminUser("Allow_WebContent_AllSites")))

            'Get the users preferred Language and obtain the language code from the Language DB
            Dim strLanguagePreference As String = drAdminUser("LanguageCode").ToString()

            'Login this AdminUser
            AdminUserDAL.LoginCurrentAdminUser(intAdminUserID, intCurrentSiteID, intAccessLevel, strAllowModules, boolAllowWebContent, strLanguagePreference)

        End If
    End Sub
End Class
