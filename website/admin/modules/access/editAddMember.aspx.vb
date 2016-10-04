Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Xml

Imports LDAP_ClassLibrary

Partial Class admin_modules_access_editAddMember
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 8 ' Module Type: Members

    Dim intSiteID As Integer = Integer.MinValue

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Setup Rad Controls
        CommonWeb.SetupRadProgressArea(RadProgressAreaMember, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadMemberImage, intSiteID)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Member_Admin.Member_AddEdit_Member_Header

        'If this User is a Master Administrator then they can view Site Access for this Member
        Dim intCurrenAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intCurrenAdminUserAccess > 2 Then
            rtsMember.Tabs.FindTabByValue("1").Visible = True
        End If

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, intSiteID) Then
                Response.Redirect("/richadmin")
            End If

            BindSalutationDropDown()
            BindLocationDropDown()
            BindLanguageDropDown()
            BindGroupCheckBoxListData()
            BindSecurityQuestionsListData()
            BindSiteDropDownCheckboxList()

            If Not Request.QueryString("ID") Is Nothing Then

                Dim intMemberID As Integer = Convert.ToInt32(Request.QueryString("ID"))
                Dim dtMember As DataTable
                If AdminUserDAL.GetCurrentAdminUserAccessLevel() > 2 Then ' This admin user is a master administrator and can view everyone in ALL SITES
                    dtMember = MemberDAL.GetMember_ByMemberID_WithThumbnail(intMemberID)
                Else 'The AdminUser can only view members in the currently selected site
                    dtMember = MemberDAL.GetMember_ByMemberIDAndSiteID_WithThumbnail(intMemberID, intSiteID)
                End If

                If dtMember.Rows.Count > 0 Then
                    Dim drMember As DataRow = dtMember.Rows(0)

                    btnAddEdit.Text = Resources.Member_Admin.Member_AddEdit_Member_ButtonUpdate

                    Dim boolMemberImportedFromActiveDirectory As Boolean = False
                    If ((Not drMember("ActiveDirectory_Identifier") Is Nothing) AndAlso (drMember("ActiveDirectory_Identifier").ToString().Trim().Length > 0)) Then
                        boolMemberImportedFromActiveDirectory = True
                    End If

                    'If data is found, fill textboxes
                    Me.status.SelectedValue = drMember("active").ToString

                    If Not drMember("SalutationID") Is DBNull.Value Then
                        Me.ddlSalutation.SelectedValue = drMember("SalutationID").ToString()
                    End If

                    Me.firstName.Text = drMember("firstName").ToString()
                    Me.lastName.Text = drMember("lastName").ToString()
                    Me.emailAddress.Text = drMember("email").ToString()

                    'As we are updating a user we show the divPassword_PlaceHolder
                    divPassword_PlaceHolder.Visible = True
                    divPassword_Reset.Visible = False

                    If Not drMember("username") Is DBNull.Value Then
                        Me.litUsername.Text = drMember("username").ToString
                        trUsernameLabel.Visible = True
                        trUsernameValue.Visible = True
                    End If

                    If Not drMember("CompanyOffice").ToString() = "" Then
                        txtOffice.Text = drMember("CompanyOffice").ToString()
                    End If

                    If Not drMember("Address").ToString() = "" Then
                        ucAddress.LocationStreet = drMember("Address").ToString()
                    End If

                    If Not drMember("City").ToString() = "" Then
                        ucAddress.LocationCity = drMember("City").ToString()
                    End If

                    If Not drMember("StateID").ToString() = "" Then
                        ucAddress.LocationState = drMember("StateID").ToString()
                    End If

                    If Not drMember("ZipCode").ToString() = "" Then
                        ucAddress.LocationZipCode = drMember("ZipCode").ToString()
                    End If

                    If Not drMember("CountryID").ToString() = "" Then
                        ucAddress.LocationCountry = drMember("CountryID").ToString()
                    End If

                    Me.ddlLanguage.SelectedValue = drMember("languageID").ToString()

                    Me.daytimePhone.Text = drMember("daytimePhone").ToString()
                    Me.eveningPhone.Text = drMember("eveningPhone").ToString()
                    Me.ddlsecurityQuestion.SelectedValue = drMember("securityQuestion").ToString()
                    Me.securityAnswer.Text = drMember("securityAnswer").ToString()
                    Dim secQuestion As String = drMember("securityQuestion").ToString()
                    ddlsecurityQuestion.SelectedValue = secQuestion

                    If Not drMember("Company").ToString() = "" Then
                        txtCompany.Text = drMember("Company").ToString()
                    End If

                    If Not drMember("CompanyDepartment").ToString() = "" Then
                        txtDepartment.Text = drMember("CompanyDepartment").ToString()
                    End If

                    If Not drMember("JobTitle").ToString() = "" Then
                        txtTitle.Text = drMember("JobTitle").ToString()
                    End If


                    If Not drMember("CompanyLocationID") Is DBNull.Value Then
                        ddlLocation.SelectedValue = drMember("CompanyLocationID").ToString()
                    End If

                    'Handle the members image
                    Me.memberImage.Visible = False
                    Me.lnkDeleteImage.Visible = False
                    If Not drMember("thumbnail") Is DBNull.Value Then
                        If Not drMember("thumbnail").ToString() = "" Then

                            Me.memberImage.DataValue = drMember("thumbnail")
                            Me.memberImage.Visible = True
                            Me.lnkDeleteImage.Visible = True
                        End If
                    End If

                    'Here we set the members group access
                    Dim dtMemberGroups As DataTable = MemberDAL.GetMemberGroupList_ByMemberIDAndSiteID(intMemberID, intSiteID)
                    For Each drMemberGroup As DataRow In dtMemberGroups.Rows
                        Dim liGroup As ListItem = cblGroupList.Items.FindByValue(drMemberGroup("GroupID").ToString())
                        If liGroup IsNot Nothing Then
                            liGroup.Selected = True
                        End If

                    Next

                    'Here we set the members Site Access
                    Dim dtSiteAccess As DataTable = SiteDAL.GetSiteAccessList_ForMember_ByMemberID(intMemberID)
                    For Each drSiteAccess As DataRow In dtSiteAccess.Rows
                        Dim liSite As ListItem = cblSiteList.Items.FindByValue(drSiteAccess("ID").ToString())
                        If liSite IsNot Nothing Then
                            liSite.Selected = True
                        End If
                    Next

                    'If this member is an active directory member, then set some fields as readonly
                    If boolMemberImportedFromActiveDirectory Then
                        SetMemberReadOnlyActiveDirectoryFields()
                    End If
                Else
                    Response.Redirect("default.aspx")
                End If
            Else
                btnAddEdit.Text = Resources.Member_Admin.Member_AddEdit_Member_ButtonAdd
                Me.status.SelectedValue = True

                'As we are Adding a new AdminUser, we must hide the divPassword_PlaceHolder, and force the user to create a password by showing the divPassword_Reset and hiding the 'Cancel' button
                divPassword_PlaceHolder.Visible = False
                divPassword_Reset.Visible = True
                lnkResetPasswordCancel.Visible = False
                reqPassword.Enabled = True 'So the Required Validator Works


                'Setup initial Site Access Defaults, either MEMBER has access to THIS SITE ONLY or ALL SITES depending on the value for intDefaultSiteID below
                Dim boolMember_SiteAccessEveryoneDefault As Boolean = False
                If Not ConfigurationManager.AppSettings("Member_SiteAccessEveryoneDefault") Is Nothing Then
                    boolMember_SiteAccessEveryoneDefault = Convert.ToBoolean(ConfigurationManager.AppSettings("Member_SiteAccessEveryoneDefault"))
                End If

                Dim intDefaultSiteID As Integer = If(boolMember_SiteAccessEveryoneDefault, 0, intSiteID)
                Dim liSite As ListItem = cblSiteList.Items.FindByValue(intDefaultSiteID.ToString())
                If Not liSite Is Nothing Then
                    liSite.Selected = True
                End If

                'Finally set the current language dropdown value to the Sites Default LanguageID
                Dim dtSite As DataTable = SiteDAL.GetSite_ByID(intSiteID)
                If dtSite.Rows.Count > 0 Then
                    Dim drSite As DataRow = dtSite.Rows(0)
                    Dim intLanguageID_Default As Integer = Convert.ToInt32(drSite("LanguageID_Default"))
                    Dim liLangauge As ListItem = ddlLanguage.Items.FindByValue(intLanguageID_Default)
                    If Not liLangauge Is Nothing Then
                        liLangauge.Selected = True
                    End If
                End If

            End If

        End If

    End Sub

    Private Sub SetMemberReadOnlyActiveDirectoryFields()
        'For all input fields that are stored in active directory, hide their OUTER DIVS and show literals in their place
        divActiveDirectoryMemberMessage.Visible = True
        divActiveDirectoryMemberMessage_SiteAccess.Visible = True

        'Not only do we make salutation read-only, we set hide the entire salutation label and value TR tags, as active directory does not return us a salutation ALSO CLEAR THE Salutation Dropdownvalues, so we get null for updates
        'Sets the salutation to read-only, use this if active directory pulls the members salutation.
        divSalutation.Visible = False
        litSalutation.Text = ddlSalutation.SelectedItem.Text
        divSalutation_ReadOnly.Visible = True

        ' NOTE: if active directory does return salutation, then comment the next 3 lines out!
        trSalutation_Label.Visible = False
        trSalutation_Value.Visible = False
        ddlSalutation.Items.Clear()


        divFirstName.Visible = False
        litFirstName.Text = FirstName.Text
        divFirstName_ReadOnly.Visible = True

        divLastName.Visible = False
        litLastName.Text = LastName.Text
        divLastName_ReadOnly.Visible = True

        divEmailAddress.Visible = False
        litEmailAddress.Text = emailAddress.Text
        divEmailAddress_ReadOnly.Visible = True

        trPassword_Label.Visible = False
        trPassword_Value.Visible = False

        trSecurityQuestion_Label.Visible = False
        trSecurityQuestion_Value.Visible = False

        trSecurityAnswer_Label.Visible = False
        trSecurityAnswer_Value.Visible = False

        divOffice.Visible = False
        litOffice.Text = txtOffice.Text
        divOffice_ReadOnly.Visible = True

        ucAddress.MakeReadOnly()

        divDaytimePhone.Visible = False
        litDaytimePhone.Text = daytimePhone.Text
        divDaytimePhone_ReadOnly.Visible = True

        divCompany.Visible = False
        litCompany.Text = txtCompany.Text
        divCompany_ReadOnly.Visible = True

        divDepartment.Visible = False
        litDepartment.Text = txtDepartment.Text
        divDepartment_ReadOnly.Visible = True

        divTitle.Visible = False
        litTitle.Text = txtTitle.Text
        divTitle_ReadOnly.Visible = True

        'If the user was imported from active directory, and so too was the group, then set the group as READONLY
        ' ALSO NOTE, if the user was imported from active directory but the group WAS NOT, you can still attach a user to the non-active directory group
        ' ALSO NOTE, if the user WAS NOT imported from active directory but the group was, you can still attach a non-active directory user to the active directory group
        Dim dtActiveDirectoryGroups As DataTable = LDAP.DBGroup_GetGroupList_BySiteID_Active(intSiteID)
        For Each drActiveDirectoryGroup As DataRow In dtActiveDirectoryGroups.Rows
            Dim intGroupID As Integer = Convert.ToInt32(drActiveDirectoryGroup("GroupID"))
            Dim liGroup As ListItem = cblGroupList.Items.FindByValue(intGroupID)
            If Not liGroup Is Nothing Then
                liGroup.Enabled = False
            End If
        Next

        divSiteList.Visible = False
        Dim sbSiteAccess As New StringBuilder()
        For Each liSiteAccess As ListItem In cblSiteList.Items
            If liSiteAccess.Selected Then
                sbSiteAccess.Append(If(sbSiteAccess.Length = 0, liSiteAccess.Text, "<br/>" & liSiteAccess.Text))
            End If
        Next
        litSiteAccess.Text = sbSiteAccess.ToString()
        divSiteList_ReadOnly.Visible = True

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

    Private Sub BindLanguageDropDown()

        'Populate the possible languages our site supports
        Dim dtLanguage As DataTable = LanguageDAL.GetLanguageList()
        ddlLanguage.Items.Clear()
        ddlLanguage.DataValueField = "ID"
        ddlLanguage.DataTextField = "Language"
        ddlLanguage.DataSource = dtLanguage
        ddlLanguage.DataBind()

        ddlLanguage.Items.Insert(0, New ListItem("-- " & Resources.Member_Admin.Member_AddEdit_Member_Language_PleaseSelect & " --", ""))
    End Sub

    Private Sub BindLocationDropDown()

        'Populate the possible locations our site supports
        ddlLocation.Items.Clear()
        ddlLocation.Items.Insert(0, New ListItem("-- " & Resources.Member_Admin.Member_AddEdit_Member_Location_PleaseSelect & " --", ""))

        Dim dvLocation As New DataView(LocationDAL.GetLocationList())
        dvLocation.Sort = "CategoryName, Location"
        For Each drvLocation As DataRowView In dvLocation
            Dim drLocation As DataRow = drvLocation.Row
            ddlLocation.Items.Add(New ListItem(If(drLocation("CategoryName") Is DBNull.Value, Resources.Member_Admin.Member_AddEdit_Member_Location_UnCategorized_Prefix, drLocation("categoryName") & " - ") & drLocation("Location"), drLocation("ID").ToString()))
        Next

    End Sub

    Public Sub BindSecurityQuestionsListData()
        Dim dtMemberSecurityQuestions As DataTable = MemberDAL.GetMemberSecurityQuestionsList()
        ddlsecurityQuestion.DataSource = dtMemberSecurityQuestions
        ddlsecurityQuestion.DataTextField = "SecurityQuestion"
        ddlsecurityQuestion.DataValueField = "SecurityQuestion"
        ddlsecurityQuestion.DataBind()
    End Sub

    Public Sub BindGroupCheckBoxListData()

        Dim dtGroups As DataTable = GroupDAL.GetGroupList_BySiteID(SiteDAL.GetCurrentSiteID_Admin)
        For Each drGroup As DataRow In dtGroups.Rows
            Dim intGroupID As String = drGroup("groupID").ToString()
            Dim strGroupName As String = (drGroup("groupName").ToString() & "<br/><span class='graySubText'>") & drGroup("groupDescription").ToString() & "</span>"
            cblGroupList.Items.Add(New ListItem(strGroupName, intGroupID))
        Next

    End Sub

    Public Sub BindSiteDropDownCheckboxList()
        'First add the Every Site list item
        cblSiteList.Items.Add(New ListItem(Resources.Member_Admin.Member_AddEdit_Tab_Member_Acess_Sites_AllSites, "0"))

        Dim dtSite As DataTable = SiteDAL.GetSiteList()
        For Each drSite In dtSite.Rows()
            Dim intSiteID As String = drSite("id").ToString()
            Dim strSiteName As String = drSite("SiteName").ToString()

            cblSiteList.Items.Add(New ListItem(strSiteName, intSiteID))
        Next

    End Sub

    Protected Sub addUpdateRecord()

        If Request.QueryString("ID") Is Nothing Then

            'Read all checkboxes and create string
            Dim sbGroup As StringBuilder = New StringBuilder()
            For Each liGroup As ListItem In cblGroupList.Items
                If liGroup.Selected Then
                    sbGroup.Append(If(sbGroup.Length = 0, liGroup.Value, "," & liGroup.Value))
                End If
            Next
            Dim strGroupList As String = String.Empty 'Now we use the MemberGroups table

            Dim intSalutationID As Integer = Integer.MinValue
            If ddlSalutation.SelectedValue.Length > 0 Then
                intSalutationID = Convert.ToInt32(ddlSalutation.SelectedValue)
            End If

            Dim strFirstName As String = firstName.Text.Trim()
            Dim strLastName As String = lastName.Text.Trim()

            Dim strCompanyOffice As String = txtOffice.Text.Trim()
            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry

            Dim intLanguageID As Integer = Convert.ToInt32(ddlLanguage.SelectedValue)

            Dim strDaytimePhone As String = daytimePhone.Text.Trim()
            Dim strEveningPhone As String = eveningPhone.Text.Trim()
            Dim dtLastLogin As DateTime = DateTime.MinValue

            Dim strPassword As String = txtPassword.Text.Trim()
            Dim strPassword_Hashed As String = CommonWeb.ComputeHash(strPassword)

            Dim strEmail As String = emailAddress.Text.Trim()
            Dim strSecurityQuestion As String = ddlsecurityQuestion.SelectedValue
            Dim strSecurityAnswer As String = securityAnswer.Text.Trim()

            Dim strCompany As String = txtCompany.Text.Trim()
            Dim strCompanyDepartment As String = txtDepartment.Text.Trim()
            Dim strJObTitle As String = txtTitle.Text.Trim()
            Dim intCompanyLocationID As Integer = Int32.MinValue
            If ddlLocation.SelectedValue.Length > 0 Then
                intCompanyLocationID = Convert.ToInt32(ddlLocation.SelectedValue)
            End If


            Dim dtDateCreated As DateTime = DateTime.Now

            'APPROACH #1 - user is active depending on its status
            Dim boolStatus As Boolean = Convert.ToBoolean(status.SelectedValue)
            'APPROACH #2 - user is active if they belong to a group, if you change this, also change this in the front-end (/WEBSITE/member/addprofile.aspx.vb)
            'Dim boolActive As Boolean = False
            'If groupIDArray.Length > 0 Then
            '    boolActive = True
            'End If


            Dim intMemberID As Integer = MemberDAL.InsertMember(intSalutationID, strFirstName, strLastName, strAddress, strCity, intStateID, strZipCode, intCountryID, intLanguageID, dtLastLogin, strDaytimePhone, strEveningPhone, strEmail, strPassword_Hashed, strSecurityQuestion, strSecurityAnswer, strGroupList, dtDateCreated, boolStatus, strCompany, strCompanyDepartment, strJObTitle, strCompanyOffice, intCompanyLocationID)

            'Here we attach the groups this member has access to
            For Each liGroup As ListItem In cblGroupList.Items
                If liGroup.Selected Then
                    MemberDAL.InsertMemberGroup(intMemberID, Convert.ToInt32(liGroup.Value))
                End If
            Next

            'Here we attach the Sites that this member has access to
            For Each liSite As ListItem In cblSiteList.Items
                If liSite.Selected Then
                    SiteDAL.InsertSiteAccess_ForMember(Convert.ToInt32(liSite.Value), intMemberID)
                End If
            Next

            'Add member image if it exists
            If RadUploadMemberImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadMemberImage.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesStaffImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesStaffImage, 0, file.InputStream.Length)
                MemberDAL.UpdateMember_MemberImage_ByMemberID(intMemberID, strThumbnailName, bytesStaffImage)

            End If

        Else

            Dim strGroupList As String = String.Empty 'Now we use the MemberGroups table

            Dim intMemberID As Integer = Convert.ToInt32(Request.QueryString("ID"))

            Dim intSalutationID As Integer = Integer.MinValue
            If ddlSalutation.SelectedValue.Length > 0 Then
                intSalutationID = Convert.ToInt32(ddlSalutation.SelectedValue)
            End If

            Dim strFirstName As String = firstName.Text.Trim()
            Dim strLastName As String = lastName.Text.Trim()

            Dim strCompanyOffice As String = txtOffice.Text.Trim()
            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry

            Dim intLanguageID As Integer = Convert.ToInt32(ddlLanguage.SelectedValue)

            Dim strDaytimePhone As String = daytimePhone.Text.Trim()
            Dim strEveningPhone As String = eveningPhone.Text.Trim()

            Dim strEmail As String = emailAddress.Text.Trim()
            Dim strSecurityQuestion As String = ddlsecurityQuestion.SelectedValue
            Dim strSecurityAnswer As String = securityAnswer.Text.Trim()

            Dim strCompany As String = txtCompany.Text.Trim()
            Dim strCompanyDepartment As String = txtDepartment.Text.Trim()
            Dim strJobTitle As String = txtTitle.Text.Trim()
            Dim intCompanyLocationID As Integer = Int32.MinValue
            If ddlLocation.SelectedValue.Length > 0 Then
                intCompanyLocationID = Convert.ToInt32(ddlLocation.SelectedValue)
            End If


            'APPROACH #1 - user is active depending on its status
            Dim boolStatus As Boolean = Convert.ToBoolean(status.SelectedValue)
            'APPROACH #2 - user is active if they belong to a group, if you change this, also change this in the front-end (/WEBSITE/member/addprofile.aspx.vb)
            'Dim boolActive As Boolean = False
            'If groupIDArray.Length > 0 Then
            '    boolActive = True
            'End If

            MemberDAL.UpdateMember(intMemberID, intSalutationID, strFirstName, strLastName, strAddress, strCity, intStateID, strZipCode, intCountryID, intLanguageID, strDaytimePhone, strEveningPhone, strEmail, strSecurityQuestion, strSecurityAnswer, strGroupList, boolStatus, strCompany, strCompanyDepartment, strJobTitle, strCompanyOffice, intCompanyLocationID)

            'If the password field is visible, AND the password field has text in it, we use this text to generate a NEW PASSWORD HASH
            If txtPassword.Visible = True And txtPassword.Text.Trim().Length > 0 Then
                Dim strPassword As String = txtPassword.Text.Trim()
                Dim strPassword_Hashed As String = CommonWeb.ComputeHash(strPassword)
                MemberDAL.UpdateMember_Password_ByMemberID(intMemberID, strPassword_Hashed)
            End If

            'First we remove all groups this member belongs to, then we re-populate this with fresh groups
            MemberDAL.DeleteMemberGroups_ByMemberIDAndSiteID(intMemberID, intSiteID)

            'Here we attach the groups this member has access to
            For Each liGroup As ListItem In cblGroupList.Items
                If liGroup.Selected Then
                    MemberDAL.InsertMemberGroup(intMemberID, Convert.ToInt32(liGroup.Value))
                End If
            Next

            'First we remove all sites this member belongs to, then we re-populate this with fresh list of sites
            SiteDAL.DeleteSiteAccess_ForMember_ByMemberID(intMemberID)

            'Here we attach the Sites that this member has access to
            For Each liSite As ListItem In cblSiteList.Items
                If liSite.Selected Then
                    SiteDAL.InsertSiteAccess_ForMember(Convert.ToInt32(liSite.Value), intMemberID)
                End If
            Next


            'Add member image if it exists
            If RadUploadMemberImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadMemberImage.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesStaffImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesStaffImage, 0, file.InputStream.Length)
                MemberDAL.UpdateMember_MemberImage_ByMemberID(intMemberID, strThumbnailName, bytesStaffImage)

            End If


            End If


    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click

        If IsValid Then
            addUpdateRecord()
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub lnkResetPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkResetPassword.Click
        divPassword_PlaceHolder.Visible = False
        divPassword_Reset.Visible = True
        txtPassword.Visible = True

        'Also set the required Validator to ENABLED so it works
        reqPassword.Enabled = True

    End Sub

    Protected Sub lnkResetPasswordCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkResetPasswordCancel.Click
        divPassword_PlaceHolder.Visible = True
        divPassword_Reset.Visible = False
        txtPassword.Visible = False
        txtPassword.Text = ""

        'Also set the required Validator to DISABLED (Enabled=False) so it wont cause validation errors for a TextBox that is HIDDEN
        reqPassword.Enabled = False
    End Sub


#Region "Validation"
    Protected Sub customValEmailAddress_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'If this email address is different to the users existing email address, then they are trying to change this
        'So check this new email address is not already taken
        Dim strEmailAddress As String = emailAddress.Text.Trim()
        Dim dtMemberList As DataTable = MemberDAL.GetMemberList_ByEmail(strEmailAddress)
        If dtMemberList.Rows.Count > 0 Then
            If Not Request.QueryString("ID") Is Nothing Then
                Dim intCurrentMemberID As Integer = Convert.ToInt32(Request.QueryString("ID"))
                For Each drMemberList As DataRow In dtMemberList.Rows
                    Dim intMemberID As Integer = Convert.ToInt32(drMemberList("ID"))
                    If intMemberID <> intCurrentMemberID Then
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
#End Region

#Region "Member Image"
    Protected Sub lnkDeleteImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteImage.Click
        Dim memberID As Integer = Convert.ToInt32(Request.QueryString("ID"))
        Dim bytesMemberImage() As Byte
        MemberDAL.UpdateMember_MemberImage_ByMemberID(memberID, String.Empty, bytesMemberImage)

        'Hide the memberImage and the delete link
        memberImage.Visible = False
        lnkDeleteImage.Visible = False
    End Sub

    Protected Sub customValMemberImageSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add members image if it exists, must have a file size less than 100kb, but we give them a 5kb buffer
        If RadUploadMemberImage.UploadedFiles.Count > 0 Then
            For Each file As UploadedFile In RadUploadMemberImage.UploadedFiles
                If file.InputStream.Length > 112400 Then
                    e.IsValid = False
                End If
            Next
        End If
    End Sub

#End Region

End Class
