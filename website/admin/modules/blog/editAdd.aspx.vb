Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_blog_editAdd
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue
    Dim ModuleTypeID As Integer = 1 ' Module Type: Blogs

    Public Sub BindGroupCheckBoxListData()

        Dim dtGroups As DataTable = GroupDAL.GetGroupList_BySiteID(intSiteID)

        cblGroupList.Items.Add(New ListItem(Resources.Blog_Admin.Blog_AddEdit_Tab_UserGroups_GroupsUnAuthenticated & "<br/><span class='graySubText'>" & Resources.Blog_Admin.Blog_AddEdit_Tab_UserGroups_GroupsUnAuthenticatedDescription & "</span>", "-1"))
        cblGroupList.Items.Add(New ListItem(Resources.Blog_Admin.Blog_AddEdit_Tab_UserGroups_GroupsAuthenticated & "<br/><span class='graySubText'>" & Resources.Blog_Admin.Blog_AddEdit_Tab_UserGroups_GroupsAuthenticatedDescription & "</span>", "0"))

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteHistory, Resources.Blog_Admin.Blog_AddEdit_Tab_History_DeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadDatePicker(PublicationDate, intSiteID)
        CommonWeb.SetupRadDatePicker(ExpirationDate, intSiteID)
        CommonWeb.SetupRadEditor(Page, txtBody, SiteDAL.GetCurrentSiteID_Admin)
        CommonWeb.SetupRadGrid(rgHistory, "{4} {5} " & Resources.Blog_Admin.Blog_AddEdit_Tab_History_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Blog_Admin.Blog_AddEdit_Tab_History_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Blog_Admin.Blog_AddEdit_Header

        'If there are more than 1 site, then show the site RadioButtonList, so the AdminUser can associate this blog to just this site or All Sites, we default this to THIS SITE ONLY
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

            'Check we need to include blog groups and user permissions
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then

                    rtsBlog.Tabs.FindTabByValue("3").Visible = True ' Show the Groups & Users Tab, then bind both the group list and member list

                End If
            Next

            'Check if this AdminUser has access to Create/Update/Delete SearchTags where SearchTags ModuleTypeID = 11
            divAddSearchTags.Visible = AdminUserDAL.CheckAdminUserModuleAccess(11, intSiteID)

            If Not Request.QueryString("ID") Is Nothing Then

                Dim intBlogID As Integer = Convert.ToInt32(Request.QueryString("ID"))

                Dim dtBlog As DataTable = BlogDAL.GetBlog_ByBlogIDAndSiteID(intBlogID, intSiteID)
                If dtBlog.Rows.Count > 0 Then
                    Dim drBlog As DataRow = dtBlog.Rows(0)

                    btnAddEdit.Text = Resources.Blog_Admin.Blog_AddEdit_ButtonUpdate

                    'If data is found, fill textboxes
                    Me.txtTitle.Text = drBlog("Title").ToString()

                    Me.txtBody.Content = drBlog("Body").ToString()
                    Me.Status.SelectedValue = drBlog("Status").ToString
                    Me.metaTitle.Text = drBlog("metaTitle").ToString()
                    Me.metaKeywords.Text = drBlog("metaKeywords").ToString()
                    Me.metaDescription.Text = drBlog("metaDescription").ToString()

                    If (drBlog("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Blog_Admin.Blog_AddEdit_Uncategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drBlog("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drBlog("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Blog_Admin.Blog_AddEdit_Uncategorized, ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Blog_Admin.Blog_AddEdit_Uncategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    End If

                    If Not drBlog("PublicationDate").ToString() = "" Then
                        Me.PublicationDate.SelectedDate = drBlog("PublicationDate").ToString()
                    End If
                    If Not drBlog("ExpirationDate").ToString() = "" Then
                        Dim dtExpirationDate As DateTime = Convert.ToDateTime(drBlog("ExpirationDate"))
                        Me.ExpirationDate.SelectedDate = dtExpirationDate.ToString()
                        If dtExpirationDate < DateTime.Now Then
                            spanExpired.Visible = True
                        End If
                    End If


                    version.Text = drBlog("Version")

                    Dim dtModuleAccess As DataTable = ModuleDAL.GetModuleAccessList_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intBlogID)
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

                    Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intBlogID)
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

                    'Finally Check if we should make this Blog READ-ONLY
                    Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(drBlog("AvailableToAllSites"))
                    rblSite.SelectedValue = boolAvailableToAllSites.ToString().ToLower()
                    If boolAvailableToAllSites Then
                        Dim intSiteID_Blog As Integer = Convert.ToInt32(drBlog("SiteID"))
                        If Not intSiteID = intSiteID_Blog Then
                            MakeBlogReadOnly(intSiteID_Blog)
                        End If
                    End If

                Else
                    'Can not find this blog so redirect to the default page
                    Response.Redirect("default.aspx")
                End If

            Else
                btnAddEdit.Text = Resources.Blog_Admin.Blog_AddEdit_ButtonAdd
                Dim liAuthenticatedGroup As ListItem = cblGroupList.Items.FindByValue("0")
                If Not liAuthenticatedGroup Is Nothing Then
                    liAuthenticatedGroup.Selected = True
                End If
                Me.Status.SelectedValue = True

                rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Blog_Admin.Blog_AddEdit_Uncategorized, ""))
                rcbCategoryID.SelectedValue = ""

            End If
        End If

    End Sub

    Private Sub MakeBlogReadOnly(ByVal SiteID As Integer)

        divAssociateWithSite.Visible = False
        divAssociateWithSite_PublicMessage.Visible = True

        'Get the site that created this entry
        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(SiteID)
        If dtSite.Rows.Count > 0 Then
            litSiteName.Text = dtSite.Rows(0)("SiteName")
        End If

        'Prevent AdminUser from updating this record
        btnAddEdit.Visible = False

        'Can only view the first tab with general details
        For Each rdTab As RadTab In rtsBlog.Tabs
            If Convert.ToInt32(rdTab.Value) > 0 Then
                rdTab.Visible = False
            End If
        Next

    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click
        If Page.IsValid Then
            addUpdateRecord()
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub addUpdateRecord()

        If Request.QueryString("ID") Is Nothing Then

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim strTitle As String = Me.txtTitle.Text.Trim().ToString()
            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If
            Dim dtPublication As DateTime = DateTime.MinValue
            If Not PublicationDate.SelectedDate.ToString() = "" Then
                dtPublication = PublicationDate.SelectedDate
            End If
            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not ExpirationDate.SelectedDate.ToString() = "" Then
                dtExpiration = ExpirationDate.SelectedDate
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

            Dim intAuthorID_member As Integer = Integer.MinValue
            Dim intAuthorID_admin As Integer = Convert.ToInt32(Session("adminID"))

            Dim intModifiedID_member As Integer = Integer.MinValue
            Dim intModifiedID_admin As Integer = Integer.MinValue

            Dim intBlogID As Integer = BlogDAL.InsertBlog(intSiteID, boolAvailableToAllSites, strTitle, intCategoryID, dtPublication, dtExpiration, strBodyContent, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, dtDateTimeStamp, intAuthorID_member, intAuthorID_admin, intModifiedID_member, intModifiedID_admin)

            'Read all GroupIDs for the modules access
            For Each liGroupID As ListItem In cblGroupList.Items
                If liGroupID.Selected Then
                    Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intBlogID, intGroupID, Integer.MinValue)
                End If
            Next

            'Read all MemberIDs for the modules access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intBlogID, Integer.MinValue, intMemberID)
                End If
            Next

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intBlogID)
                End If
            Next

        Else

            Dim intBlogID As Integer = Request.QueryString("ID")

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim strTitle As String = Me.txtTitle.Text.Trim().ToString()

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If

            Dim dtPublication As DateTime = DateTime.MinValue
            If Not PublicationDate.SelectedDate.ToString() = "" Then
                dtPublication = PublicationDate.SelectedDate
            End If
            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not ExpirationDate.SelectedDate.ToString() = "" Then
                dtExpiration = ExpirationDate.SelectedDate
            End If

            Dim strBodyContent As String = txtBody.Content.ToString()
            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)
            Dim strMetaTitle As String = metaTitle.Text.Trim()
            Dim strMetaKeywords As String = metaKeywords.Text.Trim()
            Dim strMetaDescription As String = metaDescription.Text.Trim()
            Dim strMetaOther As String = ""

            Dim listGroupIDs As String = String.Empty
            Dim listMemberIDs As String = String.Empty
            Dim strSearchTagID As String = ""

            Dim intVersion As Integer = 1
            If Not IsDBNull(version.Text.Trim()) And version.Text.Trim() <> "" Then
                intVersion = Convert.ToInt32(version.Text.Trim())
                intVersion = intVersion + 1
            End If

            Dim intModifiedID_member As Integer = Integer.MinValue
            Dim intModifiedID_admin As Integer = Convert.ToInt32(Session("adminID"))

            BlogDAL.UpdateBlog_ByBlogID(intBlogID, intSiteID, boolAvailableToAllSites, strTitle, intCategoryID, dtPublication, dtExpiration, strBodyContent, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, intModifiedID_member, intModifiedID_admin)

            'First remove existing module access, so we can cleanly overwrite module access settings
            ModuleDAL.DeleteModuleAccess_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intBlogID)

            'Read all GroupIDs for the modules access
            For Each liGroupID As ListItem In cblGroupList.Items
                If liGroupID.Selected Then
                    Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intBlogID, intGroupID, Integer.MinValue)
                End If
            Next

            'Read all MemberIDs for the modules access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intBlogID, Integer.MinValue, intMemberID)
                End If
            Next

            'Remove all tags before entering new list
            SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intBlogID)
            SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intBlogID)

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intBlogID)
                End If
            Next

        End If

    End Sub

    Protected Sub btnDeleteHistory_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgHistory.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("archiveID")
                BlogDAL.DeleteBlogArchive_ByArchiveID(intRecordId)
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

            Dim intBlogID As Integer = Convert.ToInt32(Request.QueryString("ID"))
            Dim dtHistory As DataTable = BlogDAL.GetBlogArchiveList_ByBlogID(intBlogID)
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
                e.Item.Cells(4).Text = "<b>" & Resources.Blog_Admin.Blog_AddEdit_Tab_History_GridVersion_Current & " (" & intVersion & ")</b>"
            End If

            Dim aPreview As HtmlAnchor = DirectCast(e.Item.FindControl("aPreview"), HtmlAnchor)
            aPreview.HRef = String.Format("preview.aspx?archiveID={0}", intArchiveID)

        End If
    End Sub
End Class
