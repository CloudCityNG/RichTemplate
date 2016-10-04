Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Xml

Partial Class admin_modules_staff_editAdd
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Dim ModuleTypeID As Integer = 12 ' Module Type: Staff Member

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteHistory, Resources.Staff_Admin.Staff_AddEdit_Tab_History_DeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadDatePicker(StartDate, intSiteID)
        CommonWeb.SetupRadDatePicker(EndDate, intSiteID)
        CommonWeb.SetupRadEditor(Page, Body, SiteDAL.GetCurrentSiteID_Admin)
        CommonWeb.SetupRadGrid(rgHistory, "{4} {5} " & Resources.Staff_Admin.Staff_AddEdit_Tab_History_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Staff_Admin.Staff_AddEdit_Tab_History_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadProgressArea(progressArea1, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadStaffImage, intSiteID)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Staff_Admin.Staff_AddEdit_Header

        'If there are more than 1 site, then show the site RadioButtonList, so the AdminUser can associate this staff member to just this site or All Sites, we default this to THIS SITE ONLY
        trSiteAccess.Visible = SiteDAL.GetSiteList().Rows.Count > 1 ' Only show if there is more than one site

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, intSiteID) Then
                Response.Redirect("/richadmin")
            End If

            'Load all checkbox lists and dropdowns
            BindSalutationDropDown()
            BindGroupCheckBoxListData()
            BindUserCheckBoxListData()
            BindSearchTagsCheckBoxListData()

            BindCategoryDropDownListData()
            BindJobTitleDropDownListData()

            'Check we need to include staff groups and user permissions
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then

                    rtsStaff.Tabs.FindTabByValue("3").Visible = True ' Show the Groups & Users Tab, then bind both the group list and member list

                End If
            Next

            'Check if this AdminUser has access to Create/Update/Delete SearchTags where SearchTags ModuleTypeID = 11
            divAddSearchTags.Visible = AdminUserDAL.CheckAdminUserModuleAccess(11, intSiteID)

            If Not Request.QueryString("ID") Is Nothing Then

                Dim intStaffID As Integer = Convert.ToInt32(Request.QueryString("ID"))

                Dim dtStaff As DataTable = StaffDAL.GetStaff_ByStaffIDAndSiteID(intStaffID, intSiteID)
                If dtStaff.Rows.Count > 0 Then
                    Dim drStaff As DataRow = dtStaff.Rows(0)

                    btnAddEdit.Text = Resources.Staff_Admin.Staff_AddEdit_ButtonUpdate
                    'If data is found, fill textboxes

                    If Not drStaff("SalutationID") Is DBNull.Value Then
                        Me.ddlSalutation.SelectedValue = drStaff("SalutationID").ToString()
                    End If
                    Me.firstName.Text = drStaff("FirstName").ToString()
                    Me.lastName.Text = drStaff("LastName").ToString()
                    Me.emailAddress.Text = drStaff("EmailAddress").ToString()
                    Me.company.Text = drStaff("company").ToString()

                    If (drStaff("staffPositionID").ToString = "") Then
                        rcbJobPosition.Items.Add(New RadComboBoxItem(Resources.Staff_Admin.Staff_AddEdit_StaffPosition_NotSpecified, ""))
                        rcbJobPosition.SelectedValue = ""
                    ElseIf rcbJobPosition.FindItemByValue(drStaff("staffPositionID")) IsNot Nothing Then
                        rcbJobPosition.SelectedValue = drStaff("staffPositionID").ToString
                        rcbJobPosition.Items.Add(New RadComboBoxItem(Resources.Staff_Admin.Staff_AddEdit_StaffPosition_NotSpecified, ""))
                    Else
                        rcbJobPosition.Items.Add(New RadComboBoxItem(Resources.Staff_Admin.Staff_AddEdit_StaffPosition_NotSpecified, ""))
                        rcbJobPosition.SelectedValue = ""
                    End If

                    Me.Body.Content = drStaff("Body").ToString()
                    Me.personalurl.Text = drStaff("PersonalURL").ToString()
                    Me.favouriteurl.Text = drStaff("FavouriteURL").ToString()



                    Me.officePhone.Text = drStaff("officePhone").ToString()
                    Me.mobilePhone.Text = drStaff("mobilePhone").ToString()

                    If Not drStaff("AddressOfficeNumber").ToString() = "" Then
                        txtAddressOfficeNumber.Text = drStaff("AddressOfficeNumber").ToString()
                    End If

                    If Not drStaff("Address").ToString() = "" Then
                        ucAddress.LocationStreet = drStaff("Address").ToString()
                    End If

                    If Not drStaff("City").ToString() = "" Then
                        ucAddress.LocationCity = drStaff("City").ToString()
                    End If

                    If Not drStaff("StateID").ToString() = "" Then
                        ucAddress.LocationState = drStaff("StateID").ToString()
                    End If

                    If Not drStaff("ZipCode").ToString() = "" Then
                        ucAddress.LocationZipCode = drStaff("ZipCode").ToString()
                    End If

                    If Not drStaff("CountryID").ToString() = "" Then
                        ucAddress.LocationCountry = drStaff("CountryID").ToString()
                    End If

                    geolocation.SelectedValue = drStaff("geolocation").ToString

                    Me.Status.SelectedValue = drStaff("Status").ToString
                    Me.metaTitle.Text = drStaff("metaTitle").ToString()
                    Me.metaKeywords.Text = drStaff("metaKeywords").ToString()
                    Me.metaDescription.Text = drStaff("metaDescription").ToString()

                    If (drStaff("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Staff_Admin.Staff_AddEdit_Uncategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drStaff("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drStaff("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Staff_Admin.Staff_AddEdit_Uncategorized, ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Staff_Admin.Staff_AddEdit_Uncategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    End If

                    version.Text = drStaff("Version")

                    If Not drStaff("StartDate").ToString() = "" Then
                        Me.StartDate.SelectedDate = drStaff("StartDate").ToString()
                    End If
                    If Not drStaff("EndDate").ToString() = "" Then
                        Dim dtEndDate As DateTime = Convert.ToDateTime(drStaff("EndDate"))
                        Me.EndDate.SelectedDate = dtEndDate.ToString()
                        If dtEndDate < DateTime.Now Then
                            spanExpired.Visible = True
                        End If
                    End If

                    Me.staffImage.Visible = False
                    Me.lnkDeleteImage.Visible = False
                    If Not drStaff("thumbnail") Is DBNull.Value Then
                        If Not drStaff("thumbnail").ToString() = "" Then

                            Me.staffImage.DataValue = drStaff("thumbnail")
                            Me.staffImage.Visible = True
                            Me.lnkDeleteImage.Visible = True
                        End If
                    End If

                    Dim dtModuleAccess As DataTable = ModuleDAL.GetModuleAccessList_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intStaffID)
                    For Each drModuleAccess As DataRow In dtModuleAccess.Rows

                        'Either select the list item from the group list or user list depending on the type of module access row we get returned
                        If Not drModuleAccess("groupID") Is DBNull.Value Then
                            Dim intGroupID As Integer = Convert.ToInt32(drModuleAccess("groupID"))
                            Dim liGroup As ListItem = cblGroupList.Items.FindByValue(intGroupID)
                            If Not liGroup Is Nothing Then
                                liGroup.Selected = True
                            End If
                        ElseIf Not drModuleAccess("memberID") Is DBNull.Value Then
                            Dim intMemberID As Integer = Convert.ToInt32(drModuleAccess("memberID"))
                            Dim liMember As ListItem = cblMemberList.Items.FindByValue(intMemberID)
                            If Not liMember Is Nothing Then
                                liMember.Selected = True
                            End If
                        End If

                    Next


                    '*** Populate search tags cbl ***
                    Dim chkbx As CheckBoxList
                    chkbx = CType(cblSearchTags, CheckBoxList)

                    Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intStaffID)

                    For Each drSearchTag In dtSearchTags.Rows
                        Dim currentCheckBox As ListItem
                        currentCheckBox = chkbx.Items.FindByValue(drSearchTag("searchTagID").ToString())
                        If currentCheckBox IsNot Nothing Then
                            currentCheckBox.Selected = True
                        End If
                    Next

                    '*** set inital sort for the History Grid to be version descending
                    Dim gSortExpression As New GridSortExpression()
                    gSortExpression.SortOrder = GridSortOrder.Descending
                    gSortExpression.FieldName = "version"

                    rgHistory.MasterTableView.SortExpressions.AddSortExpression(gSortExpression)

                    'Finally Check if we should make this Staff Member READ-ONLY
                    Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(drStaff("AvailableToAllSites"))
                    rblSite.SelectedValue = boolAvailableToAllSites.ToString().ToLower()
                    If boolAvailableToAllSites Then
                        Dim intSiteID_Staff As Integer = Convert.ToInt32(drStaff("SiteID"))
                        If Not intSiteID = intSiteID_Staff Then
                            MakeStaffMemberReadOnly(intSiteID_Staff)
                        End If
                    End If

                Else
                    'Cant find this record so send the AdminUser back to the default page
                    Response.Redirect("default.aspx")
                End If

            Else
                btnAddEdit.Text = Resources.Staff_Admin.Staff_AddEdit_ButtonAdd
                Dim liAuthenticatedGroup As ListItem = cblGroupList.Items.FindByValue("0")
                If Not liAuthenticatedGroup Is Nothing Then
                    liAuthenticatedGroup.Selected = True
                End If
                Me.Status.SelectedValue = True
                Me.geolocation.SelectedValue = False

                rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Staff_Admin.Staff_AddEdit_Uncategorized, ""))
                rcbCategoryID.SelectedValue = ""

                rcbJobPosition.Items.Add(New RadComboBoxItem(Resources.Staff_Admin.Staff_AddEdit_StaffPosition_NotSpecified, ""))
                rcbJobPosition.SelectedValue = ""

            End If

        End If

    End Sub

    Private Sub MakeStaffMemberReadOnly(ByVal SiteID As Integer)

        divAssociateWithSite.Visible = False
        divAssociateWithSite_PublicMessage.Visible = True

        'Get the site that created this entry
        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(SiteID)
        If dtSite.Rows.Count > 0 Then
            litSiteName.Text = dtSite.Rows(0)("SiteName")
        End If

        'Prevent AdminUser from updating this record
        lnkDeleteImage.Visible = False
        btnAddEdit.Visible = False

        'Can only view the first tab with general details
        For Each rdTab As RadTab In rtsStaff.Tabs
            If Convert.ToInt32(rdTab.Value) > 0 Then
                rdTab.Visible = False
            End If
        Next

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

    Public Sub BindGroupCheckBoxListData()

        Dim dtGroups As DataTable = GroupDAL.GetGroupList_BySiteID(intSiteID)

        cblGroupList.Items.Add(New ListItem(Resources.Staff_Admin.Staff_AddEdit_Tab_UserGroups_GroupsUnAuthenticated & "<br/><span class='graySubText'>" & Resources.Staff_Admin.Staff_AddEdit_Tab_UserGroups_GroupsUnAuthenticatedDescription & "</span>", "-1"))
        cblGroupList.Items.Add(New ListItem(Resources.Staff_Admin.Staff_AddEdit_Tab_UserGroups_GroupsAuthenticated & "<br/><span class='graySubText'>" & Resources.Staff_Admin.Staff_AddEdit_Tab_UserGroups_GroupsAuthenticatedDescription & "</span>", "0"))

        For Each drGroup As DataRow In dtGroups.Rows
            Dim intGroupID As String = drGroup("groupID").ToString()
            Dim strGroupName As String = (drGroup("groupName").ToString() & "<br/><span class='graySubText'>") & drGroup("groupDescription").ToString() & "</span>"
            cblGroupList.Items.Add(New ListItem(strGroupName, intGroupID))
        Next

    End Sub

    Public Sub BindUserCheckBoxListData()
        Dim dtMembers As DataTable = MemberDAL.GetMemberList_BySiteID(intSiteID)
        For Each drMember In dtMembers.Rows()
            Dim intMemberID As String = drMember("id").ToString()

            Dim strSalutation_LangaugeSpecific As String = String.Empty
            If Not drMember("Salutation_LanguageProperty") Is DBNull.Value Then
                Dim strSalutation_LanguageProperty As String = drMember("Salutation_LanguageProperty")
                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
            End If

            Dim strMemberName As String = CommonWeb.FormatName(strSalutation_LangaugeSpecific, drMember("firstName").ToString(), drMember("lastName"))
            cblMemberList.Items.Add(New ListItem(strMemberName, intMemberID))
        Next

    End Sub

    Public Sub BindSearchTagsCheckBoxListData()
        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsList_BySiteID(intSiteID)

        For Each drSearchTag As DataRow In dtSearchTags.Rows()
            Dim intSearchTagID As String = drSearchTag("searchTagID").ToString()
            Dim strSearchTagName As String = (drSearchTag("searchTagName").ToString() & "<br/><span class='graySubText'>") + drSearchTag("searchTagDescription").ToString() & "</span>"
            cblSearchTags.Items.Add(New ListItem(strSearchTagName, intSearchTagID))
        Next
    End Sub

    Public Sub BindJobTitleDropDownListData()
        'Here we bind the dropdown list to staff positions
        Dim dtStaffPositions As DataTable = StaffDAL.GetStaffPositionList_BySiteID(intSiteID)

        With rcbJobPosition
            .DataSource = dtStaffPositions
            .DataTextField = "StaffPosition"
            .DataValueField = "StaffPositionID"
        End With
        rcbJobPosition.DataBind()

    End Sub

    Protected Sub rgHistory_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgHistory.NeedDataSource
        'Load staff history
        If Not Request.QueryString("ID") Is Nothing Then
            Dim intStaffID As Integer = Convert.ToInt32(Request.QueryString("ID"))
            Dim dtHistory As DataTable = StaffDAL.GetStaffArchive_ByStaffID(intStaffID)
            rgHistory.DataSource = dtHistory
        End If

    End Sub

    Public Sub rgHistory_ItemDataBound(ByVal source As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgHistory.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            Dim intArchiveID As Integer = Convert.ToInt32(drItem("archiveID"))
            Dim intVersion As Integer = Convert.ToInt32(drItem("version"))
            Dim intVersion_Current As Integer = Convert.ToInt32(version.Text)
            If intVersion = intVersion_Current Then
                e.Item.Cells(4).Text = "<b>" & Resources.Staff_Admin.Staff_AddEdit_Tab_History_GridVersion_Current & " (" & intVersion & ")</b>"
            End If

            Dim aPreview As HtmlAnchor = DirectCast(e.Item.FindControl("aPreview"), HtmlAnchor)
            aPreview.HRef = String.Format("preview.aspx?archiveID={0}", intArchiveID)

        End If
    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click

        If Page.IsValid Then
            addUpdateRecord()
            Response.Redirect("default.aspx")
        End If


    End Sub

    Protected Sub addUpdateRecord()

        'Need to pre-pend http:// if it doesn't exist in the url for homepage or favourite url
        personalurl.Text = CommonWeb.FormatURL(personalurl.Text)
        favouriteurl.Text = CommonWeb.FormatURL(favouriteurl.Text)

        If Request.QueryString("ID") Is Nothing Then

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim intSalutationID As Integer = Integer.MinValue
            If ddlSalutation.SelectedValue.Length > 0 Then
                intSalutationID = Convert.ToInt32(ddlSalutation.SelectedValue)
            End If
            Dim strFirstName As String = firstName.Text.Trim()
            Dim strLastName As String = lastName.Text.Trim()
            Dim strEmail As String = emailAddress.Text.Trim()
            Dim strCompany As String = company.Text.Trim()

            Dim intStaffPositionID As Integer = Integer.MinValue
            If Not rcbJobPosition.SelectedValue = "" Then
                intStaffPositionID = Convert.ToInt32(rcbJobPosition.SelectedValue)
            End If

            Dim strBodyContent As String = Body.Content.ToString()
            Dim strPersonalURL As String = personalurl.Text.Trim()
            Dim strFavouriteURL As String = favouriteurl.Text.Trim()

            Dim strOfficePhone As String = officePhone.Text.Trim()
            Dim strMobilePhone As String = mobilePhone.Text.Trim()

            Dim strAddressOfficeNumber As String = txtAddressOfficeNumber.Text.Trim()

            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry

            Dim pairLocationLatitude As Pair = ucAddress.LocationLatitudeLongitude
            Dim strLocationLatitude As String = pairLocationLatitude.First
            Dim strLocationLongitude As String = pairLocationLatitude.Second

            Dim boolGeoLocation As Boolean = Convert.ToBoolean(geolocation.SelectedValue.ToString())

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If

            Dim dtStart As DateTime = DateTime.MinValue
            If Not StartDate.SelectedDate.ToString() = "" Then
                dtStart = StartDate.SelectedDate
            End If
            Dim dtEnd As DateTime = DateTime.MinValue
            If Not EndDate.SelectedDate.ToString() = "" Then
                dtEnd = EndDate.SelectedDate
            End If

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)
            Dim strMetaTitle As String = metaTitle.Text.Trim()
            Dim strMetaKeywords As String = metaKeywords.Text.Trim()
            Dim strMetaDescription As String = metaDescription.Text.Trim()
            Dim strMetaOther As String = ""
            Dim listGroupIDs As String = String.Empty
            Dim listMemberIDs As String = String.Empty

            Dim strSearchTagID As String = String.Empty

            Dim intVersion As Integer = 1
            Dim dtDateTimeStamp As DateTime = DateTime.Now

            Dim intAuthorID_member As Integer = Integer.MinValue
            Dim intAuthorID_admin As Integer = Convert.ToInt32(Session("adminID"))

            Dim intModifiedID_member As Integer = Integer.MinValue
            Dim intModifiedID_admin As Integer = Integer.MinValue

            Dim intStaffID As Integer = StaffDAL.InsertStaff(intSiteID, boolAvailableToAllSites, intSalutationID, strFirstName, strLastName, strEmail, strCompany, intStaffPositionID, strBodyContent, strPersonalURL, strFavouriteURL, strOfficePhone, strMobilePhone, strAddressOfficeNumber, strAddress, strCity, intStateID, strZipCode, intCountryID, strLocationLatitude, strLocationLongitude, boolGeoLocation, intCategoryID, dtStart, dtEnd, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, dtDateTimeStamp, intAuthorID_member, intAuthorID_admin, intModifiedID_member, intModifiedID_admin)

            'Add staff image if it exists
            If RadUploadStaffImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadStaffImage.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesStaffImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesStaffImage, 0, file.InputStream.Length)
                StaffDAL.UpdateStaff_StaffImage_ByStaffID(intStaffID, strThumbnailName, bytesStaffImage)
            End If

            'Read all GroupIDs for the modules access
            For Each liGroupID As ListItem In cblGroupList.Items
                Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                ModuleDAL.InsertModuleAccess(ModuleTypeID, intStaffID, intGroupID, Integer.MinValue)
            Next

            'Read all MemberIDs for the modules access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intStaffID, Integer.MinValue, intMemberID)
                End If
            Next

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intStaffID)
                End If
            Next

        Else

            Dim intStaffID As Integer = Request.QueryString("ID")

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim intSalutationID As Integer = Integer.MinValue
            If ddlSalutation.SelectedValue.Length > 0 Then
                intSalutationID = Convert.ToInt32(ddlSalutation.SelectedValue)
            End If
            Dim strFirstName As String = firstName.Text.Trim()
            Dim strLastName As String = lastName.Text.Trim()
            Dim strEmail As String = emailAddress.Text.Trim()
            Dim strCompany As String = company.Text.Trim()

            Dim intStaffPositionID As Integer = Integer.MinValue
            If Not rcbJobPosition.SelectedValue = "" Then
                intStaffPositionID = Convert.ToInt32(rcbJobPosition.SelectedValue)
            End If

            Dim strBodyContent As String = Body.Content.ToString()
            Dim strPersonalURL As String = personalurl.Text.Trim()
            Dim strFavouriteURL As String = favouriteurl.Text.Trim()

            Dim strOfficePhone As String = officePhone.Text.Trim()
            Dim strMobilePhone As String = mobilePhone.Text.Trim()

            Dim strAddressOfficeNumber As String = txtAddressOfficeNumber.Text.Trim()

            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry

            Dim pairLocationLatitude As Pair = ucAddress.LocationLatitudeLongitude
            Dim strLocationLatitude As String = pairLocationLatitude.First
            Dim strLocationLongitude As String = pairLocationLatitude.Second

            Dim boolGeoLocation As Boolean = Convert.ToBoolean(geolocation.SelectedValue.ToString())


            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If

            Dim dtStart As DateTime = DateTime.MinValue
            If Not StartDate.SelectedDate.ToString() = "" Then
                dtStart = StartDate.SelectedDate
            End If
            Dim dtEnd As DateTime = DateTime.MinValue
            If Not EndDate.SelectedDate.ToString() = "" Then
                dtEnd = EndDate.SelectedDate
            End If

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)
            Dim strMetaTitle As String = metaTitle.Text.Trim()
            Dim strMetaKeywords As String = metaKeywords.Text.Trim()
            Dim strMetaDescription As String = metaDescription.Text.Trim()
            Dim strMetaOther As String = ""
            Dim listGroupIDs As String = String.Empty
            Dim listMemberIDs As String = String.Empty


            Dim strSearchTagID As String = String.Empty

            Dim intVersion As Integer = 1
            If Not IsDBNull(version.Text.Trim()) And version.Text.Trim() <> "" Then
                intVersion = Convert.ToInt32(version.Text.Trim())
                intVersion = intVersion + 1
            End If

            Dim dtDateTimeStamp As DateTime = DateTime.Now

            Dim intModifiedID_member As Integer = Integer.MinValue
            Dim intModifiedID_admin As Integer = Convert.ToInt32(Session("adminID"))

            StaffDAL.UpdateStaff_ByStaffID(intStaffID, intSiteID, boolAvailableToAllSites, intSalutationID, strFirstName, strLastName, strEmail, strCompany, intStaffPositionID, strBodyContent, strPersonalURL, strFavouriteURL, strOfficePhone, strMobilePhone, strAddressOfficeNumber, strAddress, strCity, intStateID, strZipCode, intCountryID, strLocationLatitude, strLocationLongitude, boolGeoLocation, intCategoryID, dtStart, dtEnd, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, intModifiedID_member, intModifiedID_admin)

            'Add staff image if it exists
            If RadUploadStaffImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadStaffImage.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesStaffImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesStaffImage, 0, file.InputStream.Length)
                StaffDAL.UpdateStaff_StaffImage_ByStaffID(intStaffID, strThumbnailName, bytesStaffImage)
            End If

            'First remove existing module access, so we can cleanly overwrite module access settings
            ModuleDAL.DeleteModuleAccess_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intStaffID)

            'Read all GroupIDs for the modules access
            For Each liGroupID As ListItem In cblGroupList.Items
                If liGroupID.Selected Then
                    Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intStaffID, intGroupID, Integer.MinValue)
                End If
            Next

            'Read all MemberIDs for the modules access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intStaffID, Integer.MinValue, intMemberID)
                End If
            Next

            'Remove all tags before entering new list
            SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intStaffID)

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intStaffID)
                End If
            Next

        End If

    End Sub

    Protected Sub lnkDeleteImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteImage.Click
        Dim staffID As Integer = Convert.ToInt32(Request.QueryString("ID"))
        Dim bytesStaffImage() As Byte
        StaffDAL.UpdateStaff_StaffImage_ByStaffID(staffID, String.Empty, bytesStaffImage)

        'Hide the faceImage and the delete link
        staffImage.Visible = False
        lnkDeleteImage.Visible = False
    End Sub

    Protected Sub btnDeleteHistory_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgHistory.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("archiveID")
                StaffDAL.DeleteStaffArchive_ByArchiveID(intRecordId)
            End If
        Next
        rgHistory.Rebind()
    End Sub

    Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelButton.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub lnkAddSearchTags_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'Save progress and send to category page
        If Page.IsValid Then
            addUpdateRecord()
            Dim qs As String
            If Request.QueryString("ID") <> "" Then
                qs = "?ID=" & Request.QueryString("ID")
            Else
                qs = ""
            End If
            Response.Redirect("~/admin/modules/searchtags/default.aspx?mtid=" & ModuleTypeID.ToString() & "&rp=" & Request.Path.ToString.ToLower & qs)
        End If
    End Sub

    Protected Sub customValStaffImageSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add staff image if it exists, must have a file size less than 100kb, but we give them a 5kb buffer
        If RadUploadStaffImage.UploadedFiles.Count > 0 Then
            For Each file As UploadedFile In RadUploadStaffImage.UploadedFiles
                If file.InputStream.Length > 112400 Then
                    'Invalid Submission - so set the IsValid flag to false and show the tab that caused the validation error
                    e.IsValid = False

                    rtsStaff.SelectedIndex = 0
                    rpvStaffContent.Selected = True
                End If
            Next
        End If
    End Sub

End Class

