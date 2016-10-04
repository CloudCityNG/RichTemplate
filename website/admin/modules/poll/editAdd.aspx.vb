Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_poll_editAdd
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Dim ModuleTypeID As Integer = 9 ' Module Type: Poll

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteHistory, Resources.Poll_Admin.Poll_AddEdit_Tab_History_DeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadDatePicker(PublicationDate, intSiteID)
        CommonWeb.SetupRadDatePicker(ExpirationDate, intSiteID)
        CommonWeb.SetupRadEditor(Page, txtQuestion, SiteDAL.GetCurrentSiteID_Admin)
        CommonWeb.SetupRadGrid(rgHistory, "{4} {5} " & Resources.Poll_Admin.Poll_AddEdit_Tab_History_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Poll_Admin.Poll_AddEdit_Tab_History_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadProgressArea(rpAreaPoll, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadPollImage, intSiteID)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Poll_Admin.Poll_AddEdit_Header
        ucHeader.PageHelpID = 7 'Help Item for Polls

        'If there are more than 1 site, then show the site RadioButtonList, so the AdminUser can associate this poll to just this site or All Sites, we default this to THIS SITE ONLY
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

            'Check we need to include poll groups and user permissions
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then

                    rtsPoll.Tabs.FindTabByValue("3").Visible = True ' Show the Groups & Users Tab, then bind both the group list and member list

                End If
            Next

            'Check if this AdminUser has access to Create/Update/Delete SearchTags where SearchTags ModuleTypeID = 11
            divAddSearchTags.Visible = AdminUserDAL.CheckAdminUserModuleAccess(11, intSiteID)

            If Not Request.QueryString("ID") Is Nothing Then
                Dim intPollID As Integer = Convert.ToInt32(Request.QueryString("ID"))
                Dim dtPoll As DataTable = PollDAL.GetPoll_ByIDAndSiteID(intPollID, intSiteID)
                If dtPoll.Rows.Count > 0 Then

                    Dim drPoll As DataRow = dtPoll.Rows(0)

                    btnAddEdit.Text = Resources.Poll_Admin.Poll_AddEdit_ButtonUpdate

                    Me.txtQuestion.Content = drPoll("QuestionHtml").ToString()
                    Me.txtDescription.Text = drPoll("Description").ToString()
                    Me.Status.SelectedValue = drPoll("Status").ToString()


                    Dim boolAnswersRandomized As Boolean = Convert.ToBoolean(drPoll("AnswersRandomized"))
                    If boolAnswersRandomized Then
                        chkAnswersRandomized.Checked = True
                    End If

                    If (drPoll("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Poll_Admin.Poll_AddEdit_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drPoll("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drPoll("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Poll_Admin.Poll_AddEdit_UnCategorized, ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Poll_Admin.Poll_AddEdit_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    End If

                    If Not drPoll("publicationDate").ToString() = "" Then
                        Me.PublicationDate.SelectedDate = drPoll("publicationDate").ToString()
                    End If

                    If Not drPoll("expirationDate").ToString() = "" Then
                        Dim dtExpirationDate As DateTime = Convert.ToDateTime(drPoll("expirationDate"))
                        Me.ExpirationDate.SelectedDate = dtExpirationDate.ToString()
                        If dtExpirationDate < DateTime.Now Then
                            spanExpired.Visible = True
                        End If
                    End If

                    Me.metaTitle.Text = drPoll("metaTitle").ToString()
                    Me.metaKeywords.Text = drPoll("metaKeywords").ToString()
                    Me.metaDescription.Text = drPoll("metaDescription").ToString()

                    txtVersion.Text = drPoll("Version").ToString

                    Me.pollImage.Visible = False
                    Me.lnkDeleteImage.Visible = False
                    If Not drPoll("thumbnail") Is DBNull.Value Then
                        If Not drPoll("thumbnail").ToString() = "" Then

                            Me.pollImage.DataValue = drPoll("thumbnail")
                            Me.pollImage.Visible = True
                            Me.lnkDeleteImage.Visible = True
                        End If
                    End If

                    Dim dtModuleAccess As DataTable = ModuleDAL.GetModuleAccessList_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intPollID)
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

                    Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intPollID)

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

                    'Finally Check if we should make this Poll Record READ-ONLY
                    Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(drPoll("AvailableToAllSites"))
                    rblSite.SelectedValue = boolAvailableToAllSites.ToString().ToLower()
                    If boolAvailableToAllSites Then
                        Dim intSiteID_Poll As Integer = Convert.ToInt32(drPoll("SiteID"))
                        If Not intSiteID = intSiteID_Poll Then
                            MakePollReadOnly(intSiteID_Poll)
                        End If
                    End If
                Else
                    'Cant find this record so send the AdminUser back to the default page
                    Response.Redirect("default.aspx")
                End If

            Else
                btnAddEdit.Text = Resources.Poll_Admin.Poll_AddEdit_ButtonAdd
                Dim liAuthenticatedGroup As ListItem = cblGroupList.Items.FindByValue("0")
                If Not liAuthenticatedGroup Is Nothing Then
                    liAuthenticatedGroup.Selected = True
                End If
                Status.SelectedValue = True

                'Set category drop-down to 'Select'
                rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Poll_Admin.Poll_AddEdit_UnCategorized, ""))
                rcbCategoryID.SelectedValue = ""

            End If
        End If
    End Sub


    Private Sub MakePollReadOnly(ByVal SiteID As Integer)

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
        For Each rdTab As RadTab In rtsPoll.Tabs
            If Convert.ToInt32(rdTab.Value) > 0 Then
                rdTab.Visible = False
            End If
        Next

    End Sub

    Public Sub BindGroupCheckBoxListData()

        Dim dtGroups As DataTable = GroupDAL.GetGroupList_BySiteID(intSiteID)

        cblGroupList.Items.Add(New ListItem(Resources.Poll_Admin.Poll_AddEdit_Tab_UserGroups_GroupsUnAuthenticated & "<br/><span class='graySubText'>" & Resources.Poll_Admin.Poll_AddEdit_Tab_UserGroups_GroupsUnAuthenticatedDescription & "</span>", "-1"))
        cblGroupList.Items.Add(New ListItem(Resources.Poll_Admin.Poll_AddEdit_Tab_UserGroups_GroupsAuthenticated & "<br/><span class='graySubText'>" & Resources.Poll_Admin.Poll_AddEdit_Tab_UserGroups_GroupsAuthenticatedDescription & "</span>", "0"))

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

    Protected Sub addUpdatePoll()

        If Request.QueryString("ID") Is Nothing Then

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim strQuestion As String = CommonWeb.stripHTML(Me.txtQuestion.Content.Trim().ToString())
            Dim strQuestionHtml As String = Me.txtQuestion.Content.Trim().ToString()
            Dim strDescription As String = txtDescription.Text.Trim().ToString()

            Dim boolAnswersRandomized As Boolean = chkAnswersRandomized.Checked

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

            Dim intPollID As Integer = PollDAL.InsertPoll(intSiteID, boolAvailableToAllSites, strQuestion, strQuestionHtml, strDescription, boolAnswersRandomized, intCategoryID, dtPublication, dtExpiration, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, dtDateTimeStamp, intAuthorID_member, intAuthorID_admin, intModifiedID_member, intModifiedID_admin)

            'Add poll image if it exists
            If RadUploadPollImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadPollImage.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesPollImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesPollImage, 0, file.InputStream.Length)
                PollDAL.UpdatePoll_PollImage_ByID(intPollID, strThumbnailName, bytesPollImage)

            End If

            'Read all GroupIDs for the modules access
            For Each liGroupID As ListItem In cblGroupList.Items
                If liGroupID.Selected Then
                    Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intPollID, intGroupID, Integer.MinValue)
                End If
            Next

            'Read all MemberIDs for the modules access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intPollID, Integer.MinValue, intMemberID)
                End If
            Next

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intPollID)
                End If
            Next

        Else

            Dim intPollID As Integer = Request.QueryString("ID")

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim strQuestion As String = CommonWeb.stripHTML(Me.txtQuestion.Content.Trim().ToString())
            Dim strQuestionHtml As String = Me.txtQuestion.Content.Trim().ToString()

            Dim strDescription As String = txtDescription.Text.Trim().ToString()

            Dim boolAnswersRandomized As Boolean = chkAnswersRandomized.Checked

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

            PollDAL.UpdatePoll_ByID(intPollID, intSiteID, boolAvailableToAllSites, strQuestion, strQuestionHtml, strDescription, boolAnswersRandomized, intCategoryID, dtPublication, dtExpiration, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, intModifiedID_member, intModifiedID_admin)

            'Add poll image if it exists
            If RadUploadPollImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadPollImage.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesPollImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesPollImage, 0, file.InputStream.Length)
                PollDAL.UpdatePoll_PollImage_ByID(intPollID, strThumbnailName, bytesPollImage)

            End If

            'First remove existing module access, so we can cleanly overwrite module access settings
            ModuleDAL.DeleteModuleAccess_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intPollID)

            'Read all GroupIDs for the modules access
            For Each liGroupID As ListItem In cblGroupList.Items
                If liGroupID.Selected Then
                    Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intPollID, intGroupID, Integer.MinValue)
                End If
            Next

            'Read all MemberIDs for the modules access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intPollID, Integer.MinValue, intMemberID)
                End If
            Next

            'Remove all tags before entering new list
            SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intPollID)

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intPollID)
                End If
            Next

        End If
    End Sub

    Protected Sub btnAddEditPoll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click
        If IsValid Then
            addUpdatePoll()
            Response.Redirect("default.aspx")
        End If
    End Sub

    Protected Sub btnDeleteHistory_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgHistory.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("archiveID")
                PollDAL.DeletePollArchive_ByArchiveID(intRecordId)
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
            addUpdatePoll()
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

            Dim intPollID As Integer = Convert.ToInt32(Request.QueryString("ID"))
            Dim dtHistory As DataTable = PollDAL.GetPollArchiveList_ByPollID(intPollID)
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
                e.Item.Cells(4).Text = "<b>" & Resources.Poll_Admin.Poll_AddEdit_Tab_History_GridVersion_Current & " (" & intVersion & ")</b>"
            End If

            Dim aPreview As HtmlAnchor = DirectCast(e.Item.FindControl("aPreview"), HtmlAnchor)
            aPreview.HRef = String.Format("preview.aspx?archiveID={0}", intArchiveID)

        End If
    End Sub

#Region "Poll Image"
    Protected Sub lnkDeleteImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteImage.Click
        Dim intPollID As Integer = Convert.ToInt32(Request.QueryString("ID"))
        Dim bytesPollImage() As Byte
        PollDAL.UpdatePoll_PollImage_ByID(intPollID, String.Empty, bytesPollImage)

        'Hide the poll image and the delete link
        pollImage.Visible = False
        lnkDeleteImage.Visible = False
    End Sub

    Protected Sub customValPollImageSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add poll image if it exists, must have a file size less than 100kb, but we give them a 5kb buffer
        If RadUploadPollImage.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = RadUploadPollImage.UploadedFiles(0)
            If file.InputStream.Length > 112400 Then
                'Invalid Submission - so set the IsValid flag to false and show the tab that caused the validation error
                e.IsValid = False

                rtsPoll.SelectedIndex = 0
                rpvPoll.Selected = True
            End If
        End If
    End Sub

#End Region

End Class
