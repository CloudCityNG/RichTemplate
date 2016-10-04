Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_suggestion_editAdd
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Dim ModuleTypeID As Integer = 13 ' Module Type: Suggestion

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteHistory, Resources.Suggestion_Admin.Suggestion_AddEdit_Tab_History_DeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadDatePicker(PublicationDate, intSiteID)
        CommonWeb.SetupRadDatePicker(ExpirationDate, intSiteID)
        CommonWeb.SetupRadGrid(rgHistory, "{4} {5} " & Resources.Suggestion_Admin.Suggestion_AddEdit_Tab_History_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Suggestion_Admin.Suggestion_AddEdit_Tab_History_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadProgressArea(rpAreaSuggestion, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadSuggestionImage, intSiteID)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Suggestion_Admin.Suggestion_AddEdit_Header

        'If there are more than 1 site, then show the site RadioButtonList, so the AdminUser can associate this suggestion to just this site or All Sites, we default this to THIS SITE ONLY
        trSiteAccess.Visible = SiteDAL.GetSiteList().Rows.Count > 1 ' Only show if there is more than one site

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, intSiteID) Then
                Response.Redirect("/richadmin")
            End If

            'Load all checkbox lists and dropdowns
            BindGroupCheckBoxListData()
            BindUserCheckBoxListData()
            BindCategoryDropDownListData()
            BindSearchTagsCheckBoxListData()

            'Check if this AdminUser has access to Create/Update/Delete SearchTags where SearchTags ModuleTypeID = 11
            divAddSearchTags.Visible = AdminUserDAL.CheckAdminUserModuleAccess(11, intSiteID)

            If Not Request.QueryString("ID") Is Nothing Then
                Dim intSuggestionID As Integer = Convert.ToInt32(Request.QueryString("ID"))
                Dim dtSuggestion As DataTable = SuggestionDAL.GetSuggestion_ByIDAndSiteID(intSuggestionID, intSiteID)
                If dtSuggestion.Rows.Count > 0 Then

                    Dim drSuggestion As DataRow = dtSuggestion.Rows(0)

                    btnAddEdit.Text = Resources.Suggestion_Admin.Suggestion_AddEdit_ButtonUpdate

                    If Not drSuggestion("MemberID").ToString() = "" Then
                        txtMemberID.Text = drSuggestion("MemberID").ToString()
                    End If

                    Me.txtEmailAddress.Text = drSuggestion("EmailAddress").ToString()
                    Me.txtSuggestion.Text = drSuggestion("Suggestion").ToString()
                    Me.Status.SelectedValue = drSuggestion("Status").ToString()


                    If (drSuggestion("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Suggestion_Admin.Suggestion_AddEdit_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drSuggestion("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drSuggestion("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Suggestion_Admin.Suggestion_AddEdit_UnCategorized, ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Suggestion_Admin.Suggestion_AddEdit_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    End If

                    If Not drSuggestion("publicationDate").ToString() = "" Then
                        Me.PublicationDate.SelectedDate = drSuggestion("publicationDate").ToString()
                    End If

                    If Not drSuggestion("expirationDate").ToString() = "" Then
                        Dim dtExpirationDate As DateTime = Convert.ToDateTime(drSuggestion("expirationDate"))
                        Me.ExpirationDate.SelectedDate = dtExpirationDate.ToString()
                        If dtExpirationDate < DateTime.Now Then
                            spanExpired.Visible = True
                        End If
                    End If

                    Me.metaTitle.Text = drSuggestion("metaTitle").ToString()
                    Me.metaKeywords.Text = drSuggestion("metaKeywords").ToString()
                    Me.metaDescription.Text = drSuggestion("metaDescription").ToString()

                    txtVersion.Text = drSuggestion("Version").ToString

                    Me.suggestionImage.Visible = False
                    Me.lnkDeleteImage.Visible = False
                    If Not drSuggestion("thumbnail") Is DBNull.Value Then
                        If Not drSuggestion("thumbnail").ToString() = "" Then

                            Me.suggestionImage.DataValue = drSuggestion("thumbnail")
                            Me.suggestionImage.Visible = True
                            Me.lnkDeleteImage.Visible = True
                        End If
                    End If


                    Dim dtModuleAccess As DataTable = ModuleDAL.GetModuleAccessList_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intSuggestionID)
                    For Each drModuleAccess As DataRow In dtModuleAccess.Rows

                        'Either select the list item from the group list or user list depending on the type of module access row we get returned
                        If Not drModuleAccess("groupID") Is DBNull.Value Then
                            Dim intGroupID As Integer = Convert.ToInt32(drModuleAccess("groupID"))
                            Dim liGroup As ListItem = cblGroupList.Items.FindByValue(intGroupID)
                            liGroup.Selected = True
                        ElseIf Not drModuleAccess("memberID") Is DBNull.Value Then
                            Dim intMemberID As Integer = Convert.ToInt32(drModuleAccess("memberID"))
                            Dim liMember As ListItem = cblMemberList.Items.FindByValue(intMemberID)
                            liMember.Selected = True
                        End If

                    Next

                    '*** Populate search tags cbl ***
                    Dim chkbx As CheckBoxList
                    chkbx = CType(cblSearchTags, CheckBoxList)

                    Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intSuggestionID)

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

                    'Finally Check if we should make this Suggestion READ-ONLY
                    Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(drSuggestion("AvailableToAllSites"))
                    rblSite.SelectedValue = boolAvailableToAllSites.ToString().ToLower()
                    If boolAvailableToAllSites Then
                        Dim intSiteID_Suggestion As Integer = Convert.ToInt32(drSuggestion("SiteID"))
                        If Not intSiteID = intSiteID_Suggestion Then
                            MakeSuggestionReadOnly(intSiteID_Suggestion)
                        End If
                    End If

                Else
                    'Cant find this record so send the AdminUser back to the default page
                    Response.Redirect("default.aspx")
                End If

            Else
                btnAddEdit.Text = Resources.Suggestion_Admin.Suggestion_AddEdit_ButtonAdd
                Dim liAuthenticatedGroup As ListItem = cblGroupList.Items.FindByValue("0")
                If Not liAuthenticatedGroup Is Nothing Then
                    liAuthenticatedGroup.Selected = True
                End If
                Status.SelectedValue = True

                'Set category drop-down to 'Select'
                rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Suggestion_Admin.Suggestion_AddEdit_UnCategorized, ""))
                rcbCategoryID.SelectedValue = ""

            End If
        End If
    End Sub

    Private Sub MakeSuggestionReadOnly(ByVal SiteID As Integer)

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
        For Each rdTab As RadTab In rtsSuggestion.Tabs
            If Convert.ToInt32(rdTab.Value) > 0 Then
                rdTab.Visible = False
            End If
        Next

    End Sub

    Public Sub BindGroupCheckBoxListData()

        Dim dtGroups As DataTable = GroupDAL.GetGroupList_BySiteID(intSiteID)

        cblGroupList.Items.Add(New ListItem(Resources.Suggestion_Admin.Suggestion_AddEdit_Tab_UserGroups_GroupsUnAuthenticated & "<br/><span class='graySubText'>" & Resources.Suggestion_Admin.Suggestion_AddEdit_Tab_UserGroups_GroupsUnAuthenticatedDescription & "</span>", "-1"))
        cblGroupList.Items.Add(New ListItem(Resources.Suggestion_Admin.Suggestion_AddEdit_Tab_UserGroups_GroupsAuthenticated & "<br/><span class='graySubText'>" & Resources.Suggestion_Admin.Suggestion_AddEdit_Tab_UserGroups_GroupsAuthenticatedDescription & "</span>", "0"))

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
            Dim intSearchTagID As String = drSearchTag("searchTagID").ToString()
            Dim strSearchTagName As String = (drSearchTag("searchTagName").ToString() & "<br/><span class='graySubText'>") + drSearchTag("searchTagDescription").ToString() & "</span>"
            cblSearchTags.Items.Add(New ListItem(strSearchTagName, intSearchTagID))
        Next
    End Sub


    Protected Sub addUpdateSuggestion()

        If Request.QueryString("ID") Is Nothing Then

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim strSuggestion As String = Me.txtSuggestion.Text.Trim()
            Dim strEmailAddress As String = Me.txtEmailAddress.Text.Trim()

            Dim intMemberID As Integer = Integer.MinValue
            If txtMemberID.Text.Trim().Length > 0 Then
                intMemberID = Convert.ToInt32(txtMemberID.Text.Trim())
            End If

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If

            Dim dtPublication As DateTime = DateTime.MinValue
            If Not PublicationDate.SelectedDate.ToString = "" Then
                dtPublication = PublicationDate.SelectedDate
            End If

            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not ExpirationDate.SelectedDate.ToString = "" Then
                dtExpiration = ExpirationDate.SelectedDate
            End If

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

            Dim intSuggestionID As Integer = SuggestionDAL.InsertSuggestion(intSiteID, boolAvailableToAllSites, strSuggestion, strEmailAddress, intMemberID, intCategoryID, dtPublication, dtExpiration, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, dtDateTimeStamp, intAuthorID_member, intAuthorID_admin, intModifiedID_member, intModifiedID_admin)

            'Add suggestion image if it exists
            If RadUploadSuggestionImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadSuggestionImage.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesSuggestionImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesSuggestionImage, 0, file.InputStream.Length)
                SuggestionDAL.UpdateSuggestion_SuggestionImage_ByID(intSuggestionID, strThumbnailName, bytesSuggestionImage)

            End If

            'Read all GroupIDs for the modules access
            For Each liGroupID As ListItem In cblGroupList.Items
                If liGroupID.Selected Then
                    Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intSuggestionID, intGroupID, Integer.MinValue)
                End If
            Next

            'Read all MemberIDs for the modules access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    intMemberID = Convert.ToInt32(liMemberID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intSuggestionID, Integer.MinValue, intMemberID)
                End If
            Next

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intSuggestionID)
                End If
            Next

        Else

            Dim intSuggestionID As Integer = Request.QueryString("ID")

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim strSuggestion As String = Me.txtSuggestion.Text.Trim()
            Dim strEmailAddress As String = Me.txtEmailAddress.Text.Trim()

            Dim intMemberID As Integer = Integer.MinValue
            If txtMemberID.Text.Trim().Length > 0 Then
                intMemberID = Convert.ToInt32(txtMemberID.Text.Trim())
            End If

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If

            Dim dtPublication As DateTime = DateTime.MinValue
            If Not PublicationDate.SelectedDate.ToString = "" Then
                dtPublication = PublicationDate.SelectedDate
            End If

            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not ExpirationDate.SelectedDate.ToString = "" Then
                dtExpiration = ExpirationDate.SelectedDate
            End If

            Dim strMetaTitle As String = metaTitle.Text.Trim()
            Dim strMetaKeywords As String = metaKeywords.Text.Trim()
            Dim strMetaDescription As String = metaDescription.Text.Trim()
            Dim strMetaOther As String = ""

            Dim listGroupIDs As String = String.Empty
            Dim listMemberIDs As String = String.Empty
            Dim strSearchTagID As String = String.Empty

            Dim intVersion As Integer = 1
            If Not IsDBNull(txtVersion.Text.Trim()) And txtVersion.Text.Trim() <> "" Then
                intVersion = Convert.ToInt32(txtVersion.Text.Trim())
                intVersion = intVersion + 1
            End If

            Dim intModifiedID_member As Integer = Integer.MinValue
            Dim intModifiedID_admin As Integer = Convert.ToInt32(Session("adminID"))

            SuggestionDAL.UpdateSuggestion_ByID(intSuggestionID, intSiteID, boolAvailableToAllSites, strSuggestion, strEmailAddress, intMemberID, intCategoryID, dtPublication, dtExpiration, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, intModifiedID_member, intModifiedID_admin)

            'Add suggestion image if it exists
            If RadUploadSuggestionImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadSuggestionImage.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesSuggestionImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesSuggestionImage, 0, file.InputStream.Length)
                SuggestionDAL.UpdateSuggestion_SuggestionImage_ByID(intSuggestionID, strThumbnailName, bytesSuggestionImage)

            End If

            'First remove existing module access, so we can cleanly overwrite module access settings
            ModuleDAL.DeleteModuleAccess_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intSuggestionID)

            'Read all GroupIDs for the modules access
            For Each liGroupID As ListItem In cblGroupList.Items
                If liGroupID.Selected Then
                    Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intSuggestionID, intGroupID, Integer.MinValue)
                End If
            Next

            'Read all MemberIDs for the modules access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    intMemberID = Convert.ToInt32(liMemberID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intSuggestionID, Integer.MinValue, intMemberID)
                End If
            Next

            'Remove all tags before entering new list
            SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intSuggestionID)

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intSuggestionID)
                End If
            Next

        End If
    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click
        If IsValid Then
            addUpdateSuggestion()
            Response.Redirect("default.aspx")
        End If
    End Sub

    Protected Sub btnDeleteHistory_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgHistory.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("archiveID")
                SuggestionDAL.DeleteSuggestionArchive_ByArchiveID(intRecordId)
            End If
        Next
        rgHistory.Rebind()

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub lnkAddSearchTags_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'Save progress and send to category page
        If Page.IsValid Then
            addUpdateSuggestion()
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

            Dim intSuggestionID As Integer = Convert.ToInt32(Request.QueryString("ID"))
            Dim dtHistory As DataTable = SuggestionDAL.GetSuggestionArchiveList_BySuggestionID(intSuggestionID)
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
            Dim intVersion_Current As Integer = Convert.ToInt32(txtVersion.Text)
            If intVersion = intVersion_Current Then
                e.Item.Cells(4).Text = "<b>" & Resources.Suggestion_Admin.Suggestion_AddEdit_Tab_History_GridVersion_Current & " (" & intVersion & ")</b>"
            End If

            Dim aPreview As HtmlAnchor = DirectCast(e.Item.FindControl("aPreview"), HtmlAnchor)
            aPreview.HRef = String.Format("preview.aspx?archiveID={0}", intArchiveID)

        End If
    End Sub

    Protected Sub customValMemberExist_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)

        'If a memberID is added we check it exists
        Dim intMemberID As Integer = Integer.MinValue
        Dim strMemberID As String = txtMemberID.Text.Trim()

        'Only check the member id if it is entered
        If strMemberID.Length > 0 Then
            If Integer.TryParse(strMemberID, intMemberID) Then
                Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intMemberID)
                If dtMember.Rows.Count = 0 Then
                    'MemberID does not exists in our members table
                    'Invalid Submission - so set the IsValid flag to false and show the tab that caused the validation error
                    e.IsValid = False

                    rtsSuggestion.SelectedIndex = 0
                    rpvSuggestion.Selected = True
                End If
            Else
                'Member ID is not an integer
                'Invalid Submission - so set the IsValid flag to false and show the tab that caused the validation error
                e.IsValid = False

                rtsSuggestion.SelectedIndex = 0
                rpvSuggestion.Selected = True
            End If
        End If

    End Sub

#Region "Suggestion Image"
    Protected Sub lnkDeleteImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteImage.Click
        Dim intSuggestionID As Integer = Convert.ToInt32(Request.QueryString("ID"))
        Dim bytesSuggestionImage() As Byte
        SuggestionDAL.UpdateSuggestion_SuggestionImage_ByID(intSuggestionID, String.Empty, bytesSuggestionImage)

        'Hide the suggestion image and the delete link
        suggestionImage.Visible = False
        lnkDeleteImage.Visible = False
    End Sub

    Protected Sub customValSuggestionImageSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add suggestion image if it exists, must have a file size less than 100kb, but we give them a 5kb buffer
        If RadUploadSuggestionImage.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = RadUploadSuggestionImage.UploadedFiles(0)
            If file.InputStream.Length > 112400 Then
                'Invalid Submission - so set the IsValid flag to false and show the tab that caused the validation error
                e.IsValid = False

                rtsSuggestion.SelectedIndex = 0
                rpvSuggestion.Selected = True
            End If
        End If
    End Sub

#End Region

End Class
