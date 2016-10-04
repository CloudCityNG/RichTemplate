Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Xml

Partial Class admin_modules_event_editAdd
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Dim ModuleTypeID As Integer = 5 ' Module Type: Event

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteHistory, Resources.Event_Admin.Event_AddEdit_Tab_History_DeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadDatePicker(publicationDate, intSiteID)
        CommonWeb.SetupRadDatePicker(ExpirationDate, intSiteID)
        CommonWeb.SetupRadDatePicker(startDate, intSiteID)
        CommonWeb.SetupRadDatePicker(endDate, intSiteID)
        CommonWeb.SetupRadEditor(Page, txtBody, SiteDAL.GetCurrentSiteID_Admin)
        CommonWeb.SetupRadGrid(rgHistory, "{4} {5} " & Resources.Event_Admin.Event_AddEdit_Tab_History_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Event_Admin.Event_AddEdit_Tab_History_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadProgressArea(rpAreaEvent, intSiteID)
        CommonWeb.SetupRadTimePicker(startTime, intSiteID)
        CommonWeb.SetupRadTimePicker(endTime, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadEventImage, intSiteID)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Event_Admin.Event_AddEdit_Header

        'If there are more than 1 site, then show the site RadioButtonList, so the AdminUser can associate this event to just this site or All Sites, we default this to THIS SITE ONLY
        trSiteAccess.Visible = SiteDAL.GetSiteList().Rows.Count > 1 ' Only show if there is more than one site

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, intSiteID) Then
                Response.Redirect("/richadmin")
            End If

            'Check we need to show the 'online signup' section of event add/update
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "allow_online_registrations" Then
                    'show the online signup section
                    trOnlineSignup.Visible = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                    rtsEvent.Tabs.FindTabByValue("3").Visible = True ' Show the Groups & Users Tab, then bind both the group list and member list
                End If
            Next

            'Check if this AdminUser has access to Create/Update/Delete SearchTags where SearchTags ModuleTypeID = 11
            divAddSearchTags.Visible = AdminUserDAL.CheckAdminUserModuleAccess(11, intSiteID)

            'Load all checkbox lists and dropdowns
            BindGroupCheckBoxListData()
            BindUserCheckBoxListData()
            BindCategoryDropDownListData()
            BindSearchTagsCheckBoxListData()

            If Not Request.QueryString("ID") Is Nothing Then

                Dim intEventID As Integer = Convert.ToInt32(Request.QueryString("ID"))
                Dim dtEvent As DataTable = EventDAL.GetEvent_ByEventIDAndSiteID(intEventID, intSiteID)
                If dtEvent.Rows.Count > 0 Then
                    Dim drEvent As DataRow = dtEvent.Rows(0)

                    btnAddEdit.Text = Resources.Event_Admin.Event_AddEdit_ButtonUpdate
                    'If data is found, fill textboxes

                    Me.txtTitle.Text = drEvent("Title").ToString()

                    Me.txtSummary.Text = drEvent("Summary").ToString()
                    Me.txtBody.Content = drEvent("Body").ToString()
                    Me.Status.SelectedValue = drEvent("Status").ToString
                    Me.metaTitle.Text = drEvent("metaTitle").ToString()
                    Me.metaKeywords.Text = drEvent("metaKeywords").ToString()
                    Me.metaDescription.Text = drEvent("metaDescription").ToString()

                    Me.txtExternalLinkUrl.Text = drEvent("externalLinkUrl").ToString()

                    If (drEvent("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Event_Admin.Event_AddEdit_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drEvent("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drEvent("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Event_Admin.Event_AddEdit_UnCategorized, ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Event_Admin.Event_AddEdit_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    End If


                    If Not drEvent("startDate").ToString() = "" Then
                        Me.startDate.SelectedDate = drEvent("startDate").ToString()
                    End If
                    If Not drEvent("endDate").ToString() = "" Then
                        Me.endDate.SelectedDate = drEvent("endDate").ToString()
                    End If
                    If Not drEvent("startTime").ToString() = "" Then
                        Me.startTime.SelectedDate = drEvent("startTime").ToString()
                    End If
                    If Not drEvent("endTime").ToString() = "" Then
                        Me.endTime.SelectedDate = drEvent("endTime").ToString()
                    End If

                    If Not drEvent("publicationDate").ToString() = "" Then
                        Me.publicationDate.SelectedDate = drEvent("publicationDate").ToString()
                    End If

                    If Not drEvent("expirationDate").ToString() = "" Then
                        Dim dtExpirationDate As DateTime = Convert.ToDateTime(drEvent("expirationDate"))
                        Me.ExpirationDate.SelectedDate = dtExpirationDate.ToString()
                        If dtExpirationDate < DateTime.Now Then
                            spanExpired.Visible = True
                        End If
                    End If

                    If Not drEvent("Address").ToString() = "" Then
                        ucAddress.LocationStreet = drEvent("Address").ToString()
                    End If

                    If Not drEvent("City").ToString() = "" Then
                        ucAddress.LocationCity = drEvent("City").ToString()
                    End If

                    If Not drEvent("StateID").ToString() = "" Then
                        ucAddress.LocationState = drEvent("StateID").ToString()
                    End If

                    If Not drEvent("ZipCode").ToString() = "" Then
                        ucAddress.LocationZipCode = drEvent("ZipCode").ToString()
                    End If

                    If Not drEvent("CountryID").ToString() = "" Then
                        ucAddress.LocationCountry = drEvent("CountryID").ToString()
                    End If

                    geolocation.SelectedValue = drEvent("geolocation").ToString

                    version.Text = drEvent("Version").ToString

                    onlineSignup.SelectedValue = drEvent("onlineSignup").ToString

                    txtContactPerson.Text = drEvent("contactPerson").ToString

                    Me.eventImage.Visible = False
                    Me.lnkDeleteImage.Visible = False
                    If Not drEvent("thumbnail") Is DBNull.Value Then
                        If Not drEvent("thumbnail").ToString() = "" Then

                            Me.eventImage.DataValue = drEvent("thumbnail")
                            Me.eventImage.Visible = True
                            Me.lnkDeleteImage.Visible = True
                        End If
                    End If

                    Dim dtModuleAccess As DataTable = ModuleDAL.GetModuleAccessList_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intEventID)
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

                    Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intEventID)

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

                    'Finally Check if we should make this Event READ-ONLY
                    Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(drEvent("AvailableToAllSites"))
                    rblSite.SelectedValue = boolAvailableToAllSites.ToString().ToLower()
                    If boolAvailableToAllSites Then
                        Dim intSiteID_Event As Integer = Convert.ToInt32(drEvent("SiteID"))
                        If Not intSiteID = intSiteID_Event Then
                            MakeEventReadOnly(intSiteID_Event)
                        End If
                    End If

                Else
                    'Cant find this record so send the AdminUser back to the default page
                    Response.Redirect("default.aspx")
                End If

            Else
                btnAddEdit.Text = Resources.Event_Admin.Event_AddEdit_ButtonAdd
                Dim liAuthenticatedGroup As ListItem = cblGroupList.Items.FindByValue("0")
                If Not liAuthenticatedGroup Is Nothing Then
                    liAuthenticatedGroup.Selected = True
                End If
                Status.SelectedValue = True
                onlineSignup.SelectedValue = False
                Me.geolocation.SelectedValue = False

                'Set category drop-down to 'Select'
                rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Event_Admin.Event_AddEdit_UnCategorized, ""))
                rcbCategoryID.SelectedValue = ""

            End If
        End If

    End Sub

    Private Sub MakeEventReadOnly(ByVal SiteID As Integer)

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
        For Each rdTab As RadTab In rtsEvent.Tabs
            If Convert.ToInt32(rdTab.Value) > 0 Then
                rdTab.Visible = False
            End If
        Next

    End Sub

    Public Sub BindGroupCheckBoxListData()

        Dim dtGroups As DataTable = GroupDAL.GetGroupList_BySiteID(intSiteID)

        cblGroupList.Items.Add(New ListItem(Resources.Event_Admin.Event_AddEdit_Tab_UserGroups_GroupsUnAuthenticated & "<br/><span class='graySubText'>" & Resources.Event_Admin.Event_AddEdit_Tab_UserGroups_GroupsUnAuthenticatedDescription & "</span>", "-1"))
        cblGroupList.Items.Add(New ListItem(Resources.Event_Admin.Event_AddEdit_Tab_UserGroups_GroupsAuthenticated & "<br/><span class='graySubText'>" & Resources.Event_Admin.Event_AddEdit_Tab_UserGroups_GroupsAuthenticatedDescription & "</span>", "0"))

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

    Public Sub BindSearchTagsCheckBoxListData()
        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsList_BySiteID(intSiteID)

        For Each drSearchTag As DataRow In dtSearchTags.Rows()
            Dim uid As String = drSearchTag("searchTagID").ToString()
            Dim uname As String = (drSearchTag("searchTagName").ToString() & "<br/><span class='graySubText'>") + drSearchTag("searchTagDescription").ToString() & "</span>"
            cblSearchTags.Items.Add(New ListItem(uname, uid))
        Next
    End Sub

    Protected Sub addUpdateRecord()

        'Need to pre-pend http:// if it doesn't exist in the url for external link
        txtExternalLinkUrl.Text = CommonWeb.FormatURL(txtExternalLinkUrl.Text)

        If Request.QueryString("ID") Is Nothing Then

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim strTitle As String = Me.txtTitle.Text.Trim().ToString()
            Dim strSummary As String = Me.txtSummary.Text.Trim().ToString()

            Dim strExternalLinkUrl As String = txtExternalLinkUrl.Text.Trim()

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If
            Dim dtStart As DateTime = DateTime.MinValue
            If Not startDate.SelectedDate.ToString() = "" Then
                dtStart = startDate.SelectedDate
            End If
            Dim dtEnd As DateTime = DateTime.MinValue
            If Not endDate.SelectedDate.ToString() = "" Then
                dtEnd = endDate.SelectedDate
            End If


            Dim tStart As DateTime = DateTime.MinValue
            If Not startTime.SelectedDate.ToString = "" Then
                tStart = startTime.SelectedDate
            End If
            Dim tEnd As DateTime = DateTime.MinValue
            If Not endTime.SelectedDate.ToString = "" Then
                tEnd = endTime.SelectedDate
            End If
            Dim strBodyContent As String = txtBody.Content.ToString()
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

            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry
            Dim pairLocationLatitude As Pair = ucAddress.LocationLatitudeLongitude
            Dim strLocationLatitude As String = pairLocationLatitude.First
            Dim strLocationLongitude As String = pairLocationLatitude.Second

            Dim boolGeoLocation As Boolean = Convert.ToBoolean(geolocation.SelectedValue.ToString())

            Dim dtPublication As DateTime = DateTime.MinValue
            If Not publicationDate.SelectedDate.ToString = "" Then
                dtPublication = publicationDate.SelectedDate
            End If

            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not ExpirationDate.SelectedDate.ToString = "" Then
                dtExpiration = ExpirationDate.SelectedDate
            End If

            Dim boolOnlineSignup As Boolean = Convert.ToBoolean(onlineSignup.SelectedValue)

            Dim strContactPerson As String = txtContactPerson.Text.Trim()

            Dim intAuthorID_member As Integer = Integer.MinValue
            Dim intAuthorID_admin As Integer = Convert.ToInt32(Session("adminID"))

            Dim intModifiedID_member As Integer = Integer.MinValue
            Dim intModifiedID_admin As Integer = Integer.MinValue

            Dim intEventID As Integer = EventDAL.InsertEvent(intSiteID, boolAvailableToAllSites, strTitle, strSummary, intCategoryID, dtStart, dtEnd, tStart, tEnd, strBodyContent, strExternalLinkUrl, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, dtDateTimeStamp, intVersion, strAddress, strCity, intStateID, strZipCode, intCountryID, strLocationLatitude, strLocationLongitude, boolGeoLocation, dtPublication, dtExpiration, boolOnlineSignup, strContactPerson, intAuthorID_member, intAuthorID_admin, intModifiedID_member, intModifiedID_admin)

            'Add event image if it exists
            If RadUploadEventImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadEventImage.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesEventImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesEventImage, 0, file.InputStream.Length)
                EventDAL.UpdateEvent_EventImage_ByEventID(intEventID, strThumbnailName, bytesEventImage)

            End If

            'Read all GroupIDs for the modules access
            For Each liGroupID As ListItem In cblGroupList.Items
                If liGroupID.Selected Then
                    Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intEventID, intGroupID, Integer.MinValue)
                End If
            Next

            'Read all MemberIDs for the modules access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intEventID, Integer.MinValue, intMemberID)
                End If
            Next

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intEventID)
                End If
            Next

        Else

            Dim intEventID As Integer = Request.QueryString("ID")

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim strTitle As String = Me.txtTitle.Text.Trim().ToString()
            Dim strSummary As String = Me.txtSummary.Text.Trim().ToString()
            Dim strExternalLinkUrl As String = txtExternalLinkUrl.Text.Trim()

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If
            Dim dtStart As DateTime = DateTime.MinValue
            If Not startDate.SelectedDate.ToString() = "" Then
                dtStart = startDate.SelectedDate
            End If
            Dim dtEnd As DateTime = DateTime.MinValue
            If Not endDate.SelectedDate.ToString() = "" Then
                dtEnd = endDate.SelectedDate
            End If


            Dim tStart As DateTime = DateTime.MinValue
            If Not startTime.SelectedDate.ToString = "" Then
                tStart = startTime.SelectedDate
            End If
            Dim tEnd As DateTime = DateTime.MinValue
            If Not endTime.SelectedDate.ToString = "" Then
                tEnd = endTime.SelectedDate
            End If
            Dim strBodyContent As String = txtBody.Content.ToString()
            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)
            Dim strMetaTitle As String = metaTitle.Text.Trim()
            Dim strMetaKeywords As String = metaKeywords.Text.Trim()
            Dim strMetaDescription As String = metaDescription.Text.Trim()
            Dim strMetaOther As String = ""

            Dim listGroupIDs As String = String.Empty
            Dim listMemberIDs As String = String.Empty
            Dim strSearchTagID As String = String.Empty

            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry
            Dim pairLocationLatitude As Pair = ucAddress.LocationLatitudeLongitude
            Dim strLocationLatitude As String = pairLocationLatitude.First
            Dim strLocationLongitude As String = pairLocationLatitude.Second

            Dim boolGeoLocation As Boolean = Convert.ToBoolean(geolocation.SelectedValue.ToString())

            Dim dtPublication As DateTime = DateTime.MinValue
            If Not publicationDate.SelectedDate.ToString = "" Then
                dtPublication = publicationDate.SelectedDate
            End If

            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not ExpirationDate.SelectedDate.ToString = "" Then
                dtExpiration = ExpirationDate.SelectedDate
            End If

            Dim boolOnlineSignup As Boolean = Convert.ToBoolean(onlineSignup.SelectedValue)

            Dim strContactPerson As String = txtContactPerson.Text.Trim()

            Dim intVersion As Integer = 1
            If Not IsDBNull(version.Text.Trim()) And version.Text.Trim() <> "" Then
                intVersion = Convert.ToInt32(version.Text.Trim())
                intVersion = intVersion + 1
            End If

            Dim intModifiedID_member As Integer = Integer.MinValue
            Dim intModifiedID_admin As Integer = Convert.ToInt32(Session("adminID"))

            EventDAL.UpdateEvent_ByEventID(intEventID, intSiteID, boolAvailableToAllSites, strTitle, strSummary, intCategoryID, dtStart, dtEnd, tStart, tEnd, strBodyContent, strExternalLinkUrl, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, strAddress, strCity, intStateID, strZipCode, intCountryID, strLocationLatitude, strLocationLongitude, boolGeoLocation, dtPublication, dtExpiration, boolOnlineSignup, strContactPerson, intModifiedID_member, intModifiedID_admin)

            'Add event image if it exists
            If RadUploadEventImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadEventImage.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesEventImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesEventImage, 0, file.InputStream.Length)
                EventDAL.UpdateEvent_EventImage_ByEventID(intEventID, strThumbnailName, bytesEventImage)

            End If

            'First remove existing module access, so we can cleanly overwrite module access settings
            ModuleDAL.DeleteModuleAccess_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intEventID)

            'Read all GroupIDs for the modules access
            For Each liGroupID As ListItem In cblGroupList.Items
                If liGroupID.Selected Then
                    Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intEventID, intGroupID, Integer.MinValue)
                End If
            Next

            'Read all MemberIDs for the modules access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intEventID, Integer.MinValue, intMemberID)
                End If
            Next

            'Remove all tags before entering new list
            SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intEventID)

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intEventID)
                End If
            Next
        End If

    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click
        If IsValid Then
            addUpdateRecord()
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub btnDeleteHistory_Click(ByVal sender As Object, ByVal e As EventArgs)

        For Each grdItem As GridDataItem In rgHistory.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("archiveID")
                EventDAL.DeleteEventArchive_ByArchiveID(intRecordId)
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

    Public Sub rgHistory_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgHistory.NeedDataSource

        If Not Request.QueryString("ID") Is Nothing Then

            Dim intEventID As Integer = Convert.ToInt32(Request.QueryString("ID"))
            Dim dtHistory As DataTable = EventDAL.GetEventArchiveList_ByEventID(intEventID)
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
                e.Item.Cells(4).Text = "<b>" & Resources.Event_Admin.Event_AddEdit_Tab_History_GridVersion_Current & " (" & intVersion & ")</b>"
            End If

            Dim aPreview As HtmlAnchor = DirectCast(e.Item.FindControl("aPreview"), HtmlAnchor)
            aPreview.HRef = String.Format("preview.aspx?archiveID={0}", intArchiveID)

        End If
    End Sub

#Region "Event Image"
    Protected Sub lnkDeleteImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteImage.Click
        Dim eventID As Integer = Convert.ToInt32(Request.QueryString("ID"))
        Dim bytesEventImage() As Byte
        EventDAL.UpdateEvent_EventImage_ByEventID(eventID, String.Empty, bytesEventImage)

        'Hide the event image and the delete link
        eventImage.Visible = False
        lnkDeleteImage.Visible = False
    End Sub

    Protected Sub customValEventImageSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add event image if it exists, must have a file size less than 100kb, but we give them a 5kb buffer
        If RadUploadEventImage.UploadedFiles.Count > 0 Then
            For Each file As UploadedFile In RadUploadEventImage.UploadedFiles
                If file.InputStream.Length > 112400 Then
                    'Invalid Submission - so set the IsValid flag to false and show the tab that caused the validation error
                    e.IsValid = False

                    rtsEvent.SelectedIndex = 0
                    rpvEvent.Selected = True
                End If
            Next
        End If
    End Sub

#End Region
End Class
