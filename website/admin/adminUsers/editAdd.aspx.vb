Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_AdminUsers_editAdd
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Setup Rad Controls
        CommonWeb.SetupRadDatePicker(dtExpirationDate, intSiteID)


        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = "Add/Edit Admin User"
        ucHeader.PageHelpID = 13 'Help Item for User Administration

	Dim intCurrentAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID
        Dim bAllAdminUserAccess As Boolean = False
        Dim dtCurrentAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intCurrentAdminUserID)
        If dtCurrentAdminUser.Rows.Count > 0 Then
            Dim drCurrentAdminUser As DataRow = dtCurrentAdminUser.Rows(0)
            bAllAdminUserAccess = drCurrentAdminUser("AllAdminUserView") IsNot DBNull.Value AndAlso Convert.ToBoolean(drCurrentAdminUser("AllAdminUserView"))
        End If

        'Check the logged in user can view this page
        Dim intCurrentAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intCurrentAdminUserAccess > 2 Then
            'If the current user is a master administrator they can set site access and permissions
            rtsAdminUser.Tabs.FindTabByValue("1").Visible = True
            trSiteAccess.Visible = SiteDAL.GetSiteList().Rows.Count
            divAdminUserPermissions.Visible = True

		divAllAdminUserAccess.Visible = True

        ElseIf intCurrentAdminUserAccess > 1 Then
            'perhaps do something
        Else
            Response.Redirect("~/richadmin/")
        End If

        If Not Page.IsPostBack Then

            BindSalutationDropDown()
            BindAccessLevelDropDown()
            BindLanguageDropDown()
            BindModulesForUserCheckboxList()
            BindSiteDropDownCheckboxList()

            'Then populate the list of modules available, if no modules are available we hide the module div
            'Only for users with access level > 1 'Domain Administrator', they only see the active modules
	
            If intCurrentAdminUserAccess > 1 Then
                divModulePermission.Visible = True

                If cblModulesForUser.Items.Count > 0 Then
                    divModulePermission_List.Visible = True
                Else
                    divModulePermission_NotAvailable.Visible = True
                End If
            End If
            If Not Request.QueryString("ID") Is Nothing Then

                Dim intAdminUserID As Integer = Convert.ToInt32(Request.QueryString("ID"))

                Dim dtAdminUser As New DataTable()
                If intCurrentAdminUserAccess > 2 OR intCurrentAdminUserID = intAdminUserID Then ' This admin user is a master administrator and can view ALL ADMIN USERS in ALL SITES
                    dtAdminUser = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
                ElseIf bAllAdminUserAccess
                    dtAdminUser = AdminUserDAL.GetAdminUser_ByIDAndSiteID(intAdminUserID, intSiteID)
		Else 
			Response.Redirect("~/richadmin/")
                End If

                If dtAdminUser.Rows.Count > 0 Then
                    Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                    'First the current AdminUser can only view this user if this user has the same or lesser value for the Access_Level
                    Dim intAccessLevel As Integer = Convert.ToInt32(drAdminUser("Access_Level"))
                    If intAccessLevel <= intCurrentAdminUserAccess Then
                        btnAddEdit.Text = Resources.AdminUser_Admin.AdminUser_AddEdit_ButtonUpdate

                        'If data is found, fill in data
                        'GENERAL INFORMATION
                        Me.ddlSalutation.SelectedValue = drAdminUser("SalutationID").ToString()
                        txtFirstName.Text = drAdminUser("First_Name")
                        txtLastName.Text = drAdminUser("Last_Name")
                        txtEmail.Text = drAdminUser("Email")

                        'As we are updating a user we show the divPassword_PlaceHolder
                        divPassword_PlaceHolder.Visible = True
                        divPassword_Reset.Visible = False

                        If Not drAdminUser("Address").ToString() = "" Then
                            ucAddress.LocationStreet = drAdminUser("Address").ToString()
                        End If

                        If Not drAdminUser("City").ToString() = "" Then
                            ucAddress.LocationCity = drAdminUser("City").ToString()
                        End If

                        If Not drAdminUser("State").ToString() = "" Then
                            ucAddress.LocationState = drAdminUser("State").ToString()
                        End If

                        If Not drAdminUser("ZipCode").ToString() = "" Then
                            ucAddress.LocationZipCode = drAdminUser("ZipCode").ToString()
                        End If

                        If Not drAdminUser("CountryID").ToString() = "" Then
                            ucAddress.LocationCountry = drAdminUser("CountryID").ToString()
                        End If

                        If Not drAdminUser("Phone").ToString() = "" Then
                            txtPhone.Text = drAdminUser("Phone").ToString()
                        End If

                        If Not drAdminUser("LanguageID").ToString() = "" Then
                            ddlLanguage.SelectedValue = drAdminUser("LanguageID").ToString()
                        End If

                        txtUserName.Text = drAdminUser("UserName")

                        If Not drAdminUser("Expiration_Date").ToString() = "" Then
                            dtExpirationDate.SelectedDate = drAdminUser("Expiration_Date").ToString()
                        End If

                        txtLoginLimit.Text = drAdminUser("Login_Limit")

                        If Not drAdminUser("IP_Address").ToString() = "" Then
                            txtIpAddress.Text = drAdminUser("IP_Address")
                        End If

                        chkActive.Checked = Convert.ToBoolean(drAdminUser("active"))

                        ddlAccessLevel.SelectedValue = intAccessLevel

                        If intAccessLevel > 2 Then 'This is a Master Administrator
                            'Show the 'This is a Master Administrator Message in Site Access
                            divMasterAdministrator.Visible = True

                        End If

                        If Not drAdminUser("Notes").ToString() = "" Then
                            txtNotes.Text = drAdminUser("Notes")
                        End If

                        'Before we Handle permissions we check if this adminUser's permissions are at a SiteLevel or not
                        Dim boolUseSiteLevelAccess As Boolean = Convert.ToBoolean(drAdminUser("UseSiteLevelAccess"))
                        Dim drSiteAccess_AdminUser As DataRow = Nothing
                        If boolUseSiteLevelAccess Then
                            Dim dtSiteAccess_AdminUser As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID(intSiteID, intAdminUserID)
                            If dtSiteAccess_AdminUser.Rows.Count > 0 Then
                                drSiteAccess_AdminUser = dtSiteAccess_AdminUser.Rows(0)
                            End If
                        End If

                        'SPECIFIC PERMISSIONS
                        rblSite.SelectedValue = boolUseSiteLevelAccess

                        rblWebContent.SelectedValue = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_WebContent")), Convert.ToBoolean(drAdminUser("Allow_WebContent_AllSites")))

                        rblSectionAdd.SelectedValue = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Section_Add")), Convert.ToBoolean(drAdminUser("Allow_Section_Add_AllSites")))
                        rblPageAdd.SelectedValue = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Page_Add")), Convert.ToBoolean(drAdminUser("Allow_Page_Add_AllSites")))

                        rblSectionEdit.SelectedValue = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Section_Edit")), Convert.ToBoolean(drAdminUser("Allow_Section_Edit_AllSites")))
                        rblPageEdit.SelectedValue = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Page_Edit")), Convert.ToBoolean(drAdminUser("Allow_Page_Edit_AllSites")))

                        rblSectionDelete.SelectedValue = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Section_Delete")), Convert.ToBoolean(drAdminUser("Allow_Section_Delete_AllSites")))
                        rblPageDelete.SelectedValue = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Page_Delete")), Convert.ToBoolean(drAdminUser("Allow_Page_Delete_AllSites")))

                        rblSectionRename.SelectedValue = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Section_Rename")), Convert.ToBoolean(drAdminUser("Allow_Section_Rename_AllSites")))
                        rblPageRename.SelectedValue = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Page_Rename")), Convert.ToBoolean(drAdminUser("Allow_Page_Rename_AllSites")))

                        rblPublishLive.SelectedValue = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Publish")), Convert.ToBoolean(drAdminUser("Allow_Publish_AllSites")))


                        'MODULE PERMISSIONS
                        Dim strAllowModules As String = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, String.Empty, drSiteAccess_AdminUser("Allow_Modules")), drAdminUser("Allow_Modules_AllSites").ToString())
                        strAllowModules = strAllowModules.Replace(" ", "")
                        'If they can view the modules they can view the conference so Add 'Conference Content'
                        Dim listAllowModules As String() = strAllowModules.Split(",")

                        'setup user access to the available modules
                        For Each liModuleForUser As ListItem In cblModulesForUser.Items
                            Dim strModuleTypeID As String = liModuleForUser.Value
                            If listAllowModules.Contains(strModuleTypeID) Then
                                liModuleForUser.Selected = True
                            End If
                        Next

                        'setup Admin User Site Access
                        If intAccessLevel > 2 Then ' If the AdminUser we are viewing is a Master Administrator, also automatically check this box and disable this, as they get access to ALL SITES by default
                            For Each liSiteAccess As ListItem In cblSiteList.Items
                                liSiteAccess.Selected = True
                                liSiteAccess.Enabled = False
                            Next
                        Else
                            Dim dtSiteAccess As DataTable = SiteDAL.GetSiteAccessList_ForAdminUser_ByAdminUserID_IncludeAllSites_Active(intAdminUserID)
                            For Each drSite As DataRow In dtSiteAccess.Rows
                                Dim intSiteID_CurrentRow As Integer = Convert.ToInt32(drSite("ID"))
                                Dim liSite As ListItem = cblSiteList.Items.FindByValue(intSiteID_CurrentRow.ToString())
                                If Not liSite Is Nothing Then
                                    liSite.Selected = True
                                End If
                            Next
                        End If

                        'Check if this user has All AdminUserAccess
                        chkAllAdminUserView.Checked = drAdminUser("AllAdminUserView") ISNOT DBNull.Value AndAlso Convert.ToBoolean(drAdminUser("AllAdminUserView"))
                    Else
                        'This AdminUser is not part of this SiteID, and should only be updated when viewing the Site the AdminUser is part of
                        Response.Redirect("default.aspx")

                    End If
                Else
                    'This AdminUser does not exist
                    Response.Redirect("default.aspx")
                End If

            Else
                btnAddEdit.Text = Resources.AdminUser_Admin.AdminUser_AddEdit_ButtonAdd

                'As we are Adding a new AdminUser, we must hide the divPassword_PlaceHolder, and force the user to create a password by showing the divPassword_Reset and hiding the 'Cancel' button
                divPassword_PlaceHolder.Visible = False
                divPassword_Reset.Visible = True
                lnkResetPasswordCancel.Visible = False

                'Set the default site in our sitelist to the currentSite
                Dim liSite As ListItem = cblSiteList.Items.FindByValue(intSiteID)
                If Not liSite Is Nothing Then
                    liSite.Selected = True
                End If
            End If

        End If

    End Sub

    Private Sub BindSalutationDropDown()

        'Populate Salutation Titles
        ddlSalutation.Items.Clear()

        Dim dtSalutation As DataTable = SalutationDAL.GetSalutationList()
        For Each drSalutation As DataRow In dtSalutation.Rows
            Dim intSalutationID As Integer = drSalutation("ID")

            Dim strSalutation_LanguageProperty As String = drSalutation("Salutation_LanguageProperty")
            Dim strSalutation_LangaugeSpecific As String = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)

            Dim liSalutation As New ListItem(strSalutation_LangaugeSpecific, intSalutationID)
            ddlSalutation.Items.Add(liSalutation)
        Next

    End Sub

    Private Sub BindAccessLevelDropDown()

        'First populate the possible account types a user can be, note, can only see the options that are equal or less then the current admins' access level
        Dim intAdminUserAccessLevel As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        ddlAccessLevel.Items.Clear()
        Dim dtAccountType As DataTable = AdminUserDAL.GetAdminUserAccountType_List()
        For Each drAccountType As DataRow In dtAccountType.Rows
            Dim intAccountID As Integer = Convert.ToInt32(drAccountType("AccountID"))

            'You can only set an admin user to the same access level as the logged in user or lower
            If intAccountID <= intAdminUserAccessLevel Then
                Dim strAccountType_LanguageProperty As String = drAccountType("AccountType_LanguageProperty").ToString()
                Dim strAccountType_LangaugeSpecific As String = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strAccountType_LanguageProperty)

                Dim liAccessLevel As New ListItem(strAccountType_LangaugeSpecific, intAccountID)
                ddlAccessLevel.Items.Add(liAccessLevel)
            End If
        Next

    End Sub

    Private Sub BindLanguageDropDown()

        'Populate the possible languages our site supports
        Dim dtLanguage As DataTable = LanguageDAL.GetLanguageList()
        ddlLanguage.Items.Clear()
        ddlLanguage.DataValueField = "ID"
        ddlLanguage.DataTextField = "Language"
        ddlLanguage.DataSource = dtLanguage
        ddlLanguage.DataBind()

        ddlLanguage.Items.Insert(0, New ListItem("--Please Select--", ""))
    End Sub

    Public Sub BindSiteDropDownCheckboxList()

        'First add the Every Site list item
        cblSiteList.Items.Add(New ListItem(Resources.AdminUser_Admin.AdminUser_AddEdit_Tab_AdminUser_Access_Sites_AllSites, "0"))

        Dim dtSite As DataTable = SiteDAL.GetSiteList()
        For Each drSite In dtSite.Rows()
            Dim intSiteID_CurrentRow As String = drSite("id").ToString()
            Dim strSiteName As String = drSite("SiteName").ToString()

            cblSiteList.Items.Add(New ListItem(strSiteName, intSiteID_CurrentRow))
        Next

    End Sub

    Private Sub BindModulesForUserCheckboxList()

        cblModulesForUser.Items.Clear()

        'Populate the possible modules this admin can access
        Dim dtModules As DataTable = ModuleDAL.GetModuleList_BySiteID_FrontEnd(intSiteID)
        For Each drModule As DataRow In dtModules.Rows
            Dim intModuleTypeID As Integer = Convert.ToInt32(drModule("ModuleTypeID"))

            Dim strModuleLanguageFileName_Admin As String = drModule("moduleLanguageFileName_Admin")

            'Get the name of the module, based on the users preferred language
            Dim strModuleName_LanguageSpecific As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFileName_Admin, "_SiteWide_AdminUser_AddEdit_ModuleName")

            Dim liModule As New ListItem(strModuleName_LanguageSpecific, intModuleTypeID)
            cblModulesForUser.Items.Add(liModule)
        Next

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click
        If IsValid Then
            addUpdateRecord()
            CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, "/admin/adminusers", "/admin/richtemplate_list_administration.aspx?sel=user", "/admin/richtemplate_list_contents.aspx?sel=administration", String.Empty)
        End If

    End Sub

    Protected Sub addUpdateRecord()

        If Request.QueryString("ID") Is Nothing Then

            'Get GENERAL INFORMATION
            Dim intSalutationID As Integer = Convert.ToInt32(ddlSalutation.SelectedValue)
            Dim strFirstName As String = txtFirstName.Text.Trim()
            Dim strLastName As String = txtLastName.Text.Trim()

            Dim strUserName As String = txtUserName.Text.Trim()
            Dim strPassword As String = txtPassword.Text.Trim()
            Dim strPassword_Hashed As String = CommonWeb.ComputeHash(strPassword)

            Dim intAccessLevel As Integer = Convert.ToInt32(ddlAccessLevel.SelectedValue)

            Dim strNotes As String = txtNotes.Text.Trim()

            Dim boolAdmin As Boolean = False
            Dim boolActive As Boolean = chkActive.Checked

            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not dtExpirationDate.SelectedDate.ToString() = "" Then
                dtExpiration = dtExpirationDate.SelectedDate
            End If

            Dim strEmail As String = txtEmail.Text.Trim()

            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry

            Dim intLanguageID As Integer = Convert.ToInt32(ddlLanguage.SelectedValue)

            Dim strPhone As String = txtPhone.Text.Trim()

            Dim longCounter As Long = 0
            Dim dtLastAccess As DateTime = DateTime.MinValue
            Dim longLoginLimit As Long = Convert.ToInt64(txtLoginLimit.Text.Trim())

            Dim strCustom1 As String = ""
            Dim strCustom2 As String = ""
            Dim strCustom3 As String = ""
            Dim strCustom4 As String = ""
            Dim strCustom5 As String = ""
            Dim strCustom6 As String = ""

            Dim strIpAddress As String = txtIpAddress.Text.Trim()

            'Get SPECIFIC PERMISIONS
            Dim boolUseSiteLevelAccess As Boolean = Convert.ToBoolean(rblSite.SelectedValue)
            Dim boolAllowWebContent As Boolean = Convert.ToBoolean(rblWebContent.SelectedValue)

            Dim boolAllowSectionAdd As Boolean = Convert.ToBoolean(rblSectionAdd.SelectedValue)
            Dim boolAllowPageAdd As Boolean = Convert.ToBoolean(rblPageAdd.SelectedValue)

            Dim boolAllowSectionEdit As Boolean = Convert.ToBoolean(rblSectionEdit.SelectedValue)
            Dim boolAllowPageEdit As Boolean = Convert.ToBoolean(rblPageEdit.SelectedValue)

            Dim boolAllowSectionDelete As Boolean = Convert.ToBoolean(rblSectionDelete.SelectedValue)
            Dim boolAllowPageDelete As Boolean = Convert.ToBoolean(rblPageDelete.SelectedValue)

            Dim boolAllowSectionRename As Boolean = Convert.ToBoolean(rblSectionRename.SelectedValue)
            Dim boolAllowPageRename As Boolean = Convert.ToBoolean(rblPageRename.SelectedValue)

            Dim boolAllowPublish As Boolean = Convert.ToBoolean(rblPublishLive.SelectedValue)


            'setup user access to the available modules
            Dim sbModulesAvailableForUser As New StringBuilder()
            For Each liModuleForUser As ListItem In cblModulesForUser.Items
                If liModuleForUser.Selected Then
                    Dim strModuleTypeID As String = liModuleForUser.Value
                    sbModulesAvailableForUser.Append(If(sbModulesAvailableForUser.Length = 0, strModuleTypeID, "," & strModuleTypeID))
                End If
            Next
            Dim strAllowModules As String = sbModulesAvailableForUser.ToString()


            'Insert Admin User
            Dim intAdminUserID As Integer = AdminUserDAL.InsertAdminUser(intSalutationID, strFirstName, strLastName, strUserName, strPassword_Hashed, intAccessLevel, strNotes, boolAdmin, boolActive, dtExpiration, strEmail, strAddress, strCity, strZipCode, intStateID, intCountryID, intLanguageID, strPhone, longCounter, longLoginLimit, strIpAddress, boolUseSiteLevelAccess, boolAllowWebContent, boolAllowSectionAdd, boolAllowPageAdd, boolAllowSectionEdit, boolAllowPageEdit, boolAllowSectionDelete, boolAllowPageDelete, boolAllowSectionRename, boolAllowPageRename, boolAllowPublish, strAllowModules, strCustom1, strCustom2, strCustom3, strCustom4, strCustom5, strCustom6)

		'Update the AllAdminUserView Access
		Dim boolAllAdminUserView As Boolean = chkAllAdminUserView.Checked
		AdminUserDAL.UpdateAdminUser_AllAdminUserView(intAdminUserID, boolAllAdminUserView )

            'If the AdminUser is not a Master Administrator then add Site Access, Master Administrators do not need site access, they automatically have access to all sites
            Dim boolAllSitesSelected As Boolean = False
            Dim liSite_AllSites As ListItem = cblSiteList.Items.FindByValue("0")
            If Not liSite_AllSites Is Nothing Then
                boolAllSitesSelected = liSite_AllSites.Selected
            End If
            For Each liSite As ListItem In cblSiteList.Items
                Dim intSiteID_CurrentItem As Integer = Convert.ToInt32(liSite.Value)
                Dim boolSiteAccessActive As Boolean = liSite.Selected Or boolAllSitesSelected Or intAccessLevel > 2 'If this Admin User has Access to 'ALL SITES' or this Admin User is a Master Administrator Then they always get access to all sites
                addUpdateSiteAccess(intSiteID_CurrentItem, intAdminUserID, boolUseSiteLevelAccess, boolAllowWebContent, boolAllowSectionAdd, boolAllowPageAdd, boolAllowSectionEdit, boolAllowPageEdit, boolAllowSectionDelete, boolAllowPageDelete, boolAllowSectionRename, boolAllowPageRename, boolAllowPublish, strAllowModules, boolSiteAccessActive)
            Next
        Else

            'Get GENERAL INFORMATION
            Dim intAdminUserID As Integer = Request.QueryString("ID")

            Dim intSalutationID As Integer = Convert.ToInt32(ddlSalutation.SelectedValue)
            Dim strFirstName As String = txtFirstName.Text.Trim()
            Dim strLastName As String = txtLastName.Text.Trim()

            Dim strUserName As String = txtUserName.Text.Trim()

            Dim intAccessLevel As Integer = Convert.ToInt32(ddlAccessLevel.SelectedValue)

            Dim strNotes As String = txtNotes.Text.Trim()

            Dim boolAdmin As Boolean = False
            Dim boolActive As Boolean = chkActive.Checked

            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not dtExpirationDate.SelectedDate.ToString() = "" Then
                dtExpiration = dtExpirationDate.SelectedDate
            End If

            Dim strEmail As String = txtEmail.Text.Trim()

            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry

            Dim intLanguageID As Integer = Convert.ToInt32(ddlLanguage.SelectedValue)

            Dim strPhone As String = txtPhone.Text.Trim()

            Dim longLoginLimit As Long = Convert.ToInt64(txtLoginLimit.Text.Trim())

            Dim strCustom1 As String = ""
            Dim strCustom2 As String = ""
            Dim strCustom3 As String = ""
            Dim strCustom4 As String = ""
            Dim strCustom5 As String = ""
            Dim strCustom6 As String = ""

            Dim strIpAddress As String = txtIpAddress.Text.Trim()

            'Get SPECIFIC PERMISIONS
            Dim boolUseSiteLevelAccess As Boolean = Convert.ToBoolean(rblSite.SelectedValue)
            Dim boolAllowWebContent As Boolean = Convert.ToBoolean(rblWebContent.SelectedValue)

            Dim boolAllowSectionAdd As Boolean = Convert.ToBoolean(rblSectionAdd.SelectedValue)
            Dim boolAllowPageAdd As Boolean = Convert.ToBoolean(rblPageAdd.SelectedValue)

            Dim boolAllowSectionEdit As Boolean = Convert.ToBoolean(rblSectionEdit.SelectedValue)
            Dim boolAllowPageEdit As Boolean = Convert.ToBoolean(rblPageEdit.SelectedValue)

            Dim boolAllowSectionDelete As Boolean = Convert.ToBoolean(rblSectionDelete.SelectedValue)
            Dim boolAllowPageDelete As Boolean = Convert.ToBoolean(rblPageDelete.SelectedValue)

            Dim boolAllowSectionRename As Boolean = Convert.ToBoolean(rblSectionRename.SelectedValue)
            Dim boolAllowPageRename As Boolean = Convert.ToBoolean(rblPageRename.SelectedValue)

            Dim boolAllowPublish As Boolean = Convert.ToBoolean(rblPublishLive.SelectedValue)


            'setup user access to the available modules
            Dim sbModulesAvailableForUser As New StringBuilder()
            For Each liModuleForUser As ListItem In cblModulesForUser.Items
                If liModuleForUser.Selected Then
                    Dim strModuleTypeID As String = liModuleForUser.Value
                    sbModulesAvailableForUser.Append(If(sbModulesAvailableForUser.Length = 0, strModuleTypeID, "," & strModuleTypeID))
                End If
            Next
            Dim strAllowModules As String = sbModulesAvailableForUser.ToString()


            'Update Admin User
            AdminUserDAL.UpdateAdminUser(intAdminUserID, intSalutationID, strFirstName, strLastName, strUserName, intAccessLevel, strNotes, boolAdmin, boolActive, dtExpiration, strEmail, strAddress, strCity, strZipCode, intStateID, intCountryID, intLanguageID, strPhone, longLoginLimit, strIpAddress, boolUseSiteLevelAccess, boolAllowWebContent, boolAllowSectionAdd, boolAllowPageAdd, boolAllowSectionEdit, boolAllowPageEdit, boolAllowSectionDelete, boolAllowPageDelete, boolAllowSectionRename, boolAllowPageRename, boolAllowPublish, strAllowModules, strCustom1, strCustom2, strCustom3, strCustom4, strCustom5, strCustom6)


		'Update the AllAdminUserView Access
		Dim boolAllAdminUserView As Boolean = chkAllAdminUserView.Checked
		AdminUserDAL.UpdateAdminUser_AllAdminUserView(intAdminUserID, boolAllAdminUserView )

            'If the password field is visible, AND the password field has text in it, we use this text to generate a NEW PASSWORD HASH
            If txtPassword.Visible = True And txtPassword.Text.Trim().Length > 0 Then
                Dim strPassword As String = txtPassword.Text.Trim()
                Dim strPassword_Hashed As String = CommonWeb.ComputeHash(strPassword)
                AdminUserDAL.UpdateAdminUser_Password(intAdminUserID, strPassword_Hashed)
            End If

            'If the AdminUser is not a Master Administrator then add Site Access, Master Administrators do not need site access, they automatically have access to all sites
            Dim boolAllSitesSelected As Boolean = False
            Dim liSite_AllSites As ListItem = cblSiteList.Items.FindByValue("0")
            If Not liSite_AllSites Is Nothing Then
                boolAllSitesSelected = liSite_AllSites.Selected
            End If
            For Each liSite As ListItem In cblSiteList.Items
                Dim intSiteID_CurrentItem As Integer = Convert.ToInt32(liSite.Value)
                Dim boolSiteAccessActive As Boolean = liSite.Selected Or boolAllSitesSelected Or intAccessLevel > 2 'If this Admin User has Access to 'ALL SITES' or this Admin User is a Master Administrator Then they always get access to all sites
                addUpdateSiteAccess(intSiteID_CurrentItem, intAdminUserID, boolUseSiteLevelAccess, boolAllowWebContent, boolAllowSectionAdd, boolAllowPageAdd, boolAllowSectionEdit, boolAllowPageEdit, boolAllowSectionDelete, boolAllowPageDelete, boolAllowSectionRename, boolAllowPageRename, boolAllowPublish, strAllowModules, boolSiteAccessActive)
            Next

            'If the currently logged in admin user is the same as the adminuser we are updating, then as they are updating their own profile, we update their preferred Language for this session
            Dim intCurrentAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
            If intAdminUserID = intCurrentAdminUserID Then

                Dim strLanguageCode As String = LanguageDAL.GetCurrentLanguageCode_BySiteID(intSiteID)
                Dim dtLanguage As DataTable = LanguageDAL.GetLanguage_ByID(intLanguageID)
                If dtLanguage.Rows.Count > 0 Then
                    strLanguageCode = dtLanguage(0)("Code").ToString()
                End If

                'Perform the same functions as we do when we login the user, so all of their session data are updated, e.g. modules, AllowWebContent, accessLevel and language preference
                AdminUserDAL.LoginCurrentAdminUser(intAdminUserID, intSiteID, intAccessLevel, strAllowModules, boolAllowWebContent, strLanguageCode)

            End If
        End If


    End Sub

    Private Sub addUpdateSiteAccess(ByVal SiteID As Integer, ByVal AdminUserID As Integer, ByVal UseSiteLevelAccess As Boolean, ByVal AllowWebContent As Boolean, ByVal AllowSectionAdd As Boolean, ByVal AllowPageAdd As Boolean, ByVal AllowSectionEdit As Boolean, ByVal AllowPageEdit As Boolean, ByVal AllowSectionDelete As Boolean, ByVal AllowPageDelete As Boolean, ByVal AllowSectionRename As Boolean, ByVal AllowPageRename As Boolean, ByVal AllowPublish As Boolean, ByVal AllowModules As String, ByVal SiteAccessActive As Boolean)
        Dim dtSiteAccess_ForAdminUser As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID(SiteID, AdminUserID)

        If UseSiteLevelAccess AndAlso (SiteID = intSiteID) Then
            'So we do want to insert/update permissions as we are using site level access and this is the current site we are working with
            If dtSiteAccess_ForAdminUser.Rows.Count = 0 Then
                'Insert siteAccess and permissions for this site
                SiteDAL.InsertSiteAccess_ForAdminUser(SiteID, AdminUserID, AllowWebContent, AllowSectionAdd, AllowPageAdd, AllowSectionEdit, AllowPageEdit, AllowSectionDelete, AllowPageDelete, AllowSectionRename, AllowPageRename, AllowPublish, AllowModules, SiteAccessActive)
            Else
                'Update SiteAccess and Permissions
                SiteDAL.UpdateSiteAccess_ForAdminUser(SiteID, AdminUserID, AllowWebContent, AllowSectionAdd, AllowPageAdd, AllowSectionEdit, AllowPageEdit, AllowSectionDelete, AllowPageDelete, AllowSectionRename, AllowPageRename, AllowPublish, AllowModules, SiteAccessActive)
            End If
        Else
            'Just insert/update the SiteLevelAccess do not touch access permissions, as the permissions are set a global level
            If dtSiteAccess_ForAdminUser.Rows.Count = 0 Then
                'Inserting siteAccess with default permissions
                SiteDAL.InsertSiteAccess_ForAdminUser(SiteID, AdminUserID, False, False, False, False, False, False, False, False, False, False, String.Empty, SiteAccessActive)
            Else
                'Get Current SiteAccess Values
                AllowWebContent = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_WebContent"))

                AllowSectionAdd = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Section_Add"))
                AllowPageAdd = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Page_Add"))

                AllowSectionEdit = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Section_Edit"))
                AllowPageEdit = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Page_Edit"))

                AllowSectionDelete = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Section_Delete"))
                AllowPageDelete = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Page_Delete"))

                AllowSectionRename = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Section_Rename"))
                AllowPageRename = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Page_Rename"))

                AllowPublish = Convert.ToBoolean(dtSiteAccess_ForAdminUser.Rows(0)("Allow_Publish"))
                AllowModules = dtSiteAccess_ForAdminUser.Rows(0)("Allow_Modules").ToString()

                SiteDAL.UpdateSiteAccess_ForAdminUser(SiteID, AdminUserID, AllowWebContent, AllowSectionAdd, AllowPageAdd, AllowSectionEdit, AllowPageEdit, AllowSectionDelete, AllowPageDelete, AllowSectionRename, AllowPageRename, AllowPublish, AllowModules, SiteAccessActive)
            End If
        End If

    End Sub

    Protected Sub lnkResetPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkResetPassword.Click
        divPassword_PlaceHolder.Visible = False
        divPassword_Reset.Visible = True
        txtPassword.Visible = True
    End Sub

    Protected Sub lnkResetPasswordCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkResetPasswordCancel.Click
        divPassword_PlaceHolder.Visible = True
        divPassword_Reset.Visible = False
        txtPassword.Visible = False
        txtPassword.Text = ""
    End Sub

#Region "Validation"
    Protected Sub customValEmailAddress_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'If this email address is different to the AdminUsers existing email address, then they are trying to change this
        'So check this new email address is not already taken
        Dim strEmailAddress As String = txtEmail.Text.Trim()
        Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByEmailAddress(strEmailAddress)
        If dtAdminUser.Rows.Count > 0 Then
            If Not Request.QueryString("ID") Is Nothing Then
                Dim intAdminUserID_Current As Integer = Convert.ToInt32(Request.QueryString("ID"))
                For Each drAdminUser As DataRow In dtAdminUser.Rows
                    Dim intAdminUserID As Integer = Convert.ToInt32(drAdminUser("ID"))
                    If intAdminUserID <> intAdminUserID_Current Then
                        'This email exists, and is used by another user
                        e.IsValid = False

                    End If
                Next
            Else
                'we try to create a new member, but the email address already exists with another user
                e.IsValid = False
            End If

        End If
    End Sub

    Protected Sub cusValUserName_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'If this username is different to the AdminUsers existing email address, then they are trying to change this
        'So check this new email address is not already taken
        Dim strUsername As String = txtUserName.Text.Trim()
        Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByUsername(strUsername)
        If dtAdminUser.Rows.Count > 0 Then
            If Not Request.QueryString("ID") Is Nothing Then
                Dim intAdminUserID_Current As Integer = Convert.ToInt32(Request.QueryString("ID"))
                For Each drAdminUser As DataRow In dtAdminUser.Rows
                    Dim intAdminUserID As Integer = Convert.ToInt32(drAdminUser("ID"))
                    If intAdminUserID <> intAdminUserID_Current Then
                        'This username exists, and is used by another user
                        e.IsValid = False

                    End If
                Next
            Else
                'we try to create a new member, but the username already exists with another user
                e.IsValid = False
            End If

        End If
    End Sub

#End Region
End Class
