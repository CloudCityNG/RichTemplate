Imports System.Data
Imports Telerik.Web.UI

Partial Class admin_richtemplate_page_editor
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 0

    Dim intSiteID As Integer = Integer.MinValue
    Dim intAdminUserID As Integer = Integer.MinValue

    Dim intWebInfoID_HomePage As Integer = Integer.MinValue
    Dim intWebInfoID_Header As Integer = Integer.MinValue
    Dim intWebInfoID_Footer As Integer = Integer.MinValue
    Dim boolWebpage_PublicSection_EnableGroupsAndUsers As Boolean = False
    Dim boolWebpage_MemberSection_EnableGroupsAndUsers As Boolean = False

    Const MODE_ADD_SECTION As String = "ADD_SECTION"
    Const MODE_ADD_PAGE As String = "ADD_PAGE"
    Const MODE_EDIT_PAGE As String = "EDIT_PAGE"

    Dim strEditorMode As String = ""

    Dim boolUseThreeColumnLayout As Boolean = False
    Dim intAllowedSiteDepth As Integer = 1

    Dim boolAllowSectionAdd As Boolean = False
    Dim boolAllowPageAdd As Boolean = False
    Dim boolAllowSectionEdit As Boolean = False
    Dim boolAllowPageEdit As Boolean = False
    Dim boolAllowSectionRename As Boolean = False
    Dim boolAllowPageRename As Boolean = False
    Dim boolAllowPublish As Boolean = False

    Dim boolSecureMembers As Boolean = False
    Dim boolSecureEducation As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()
        intAdminUserID = AdminUserDAL.GetCurrentAdminUserID()

        'Setup Rad Controls
        CommonWeb.SetupRadEditor(Me.Page, txtMessage, "~/editorConfig/toolbars/ToolsFileCustom_Webpage.xml", intSiteID)
        CommonWeb.SetupRadUpload(RadUploadBannerImage, intSiteID)
        txtMessage.CssFiles.Add("~/css/editorStyle.css")

        'Set the header
        ucHeader.PageName = Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Heading

        'Check thes user exists and Only give the ability to add,edit and view sections/pages it the user has access
        Dim boolAllowWebContent As Boolean = AdminUserDAL.GetCurrentAdminUserAllowWebContent()
        If intAdminUserID > 0 AndAlso boolAllowWebContent Then

            Dim intAdminUserAccessLevel As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()

            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
            If dtAdminUser.Rows.Count > 0 Then
                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                'Before we Handle permissions we check if this adminUser's permissions are at a SiteLevel or not
                Dim boolUseSiteLevelAccess As Boolean = Convert.ToBoolean(drAdminUser("UseSiteLevelAccess"))
                Dim drSiteAccess_AdminUser As DataRow = Nothing
                If boolUseSiteLevelAccess Then
                    Dim dtSiteAccess_AdminUser As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID_Active(intSiteID, intAdminUserID)
                    If dtSiteAccess_AdminUser.Rows.Count > 0 Then
                        drSiteAccess_AdminUser = dtSiteAccess_AdminUser.Rows(0)
                    End If
                End If

                'Check if this AdminUser has access to Create/Update/Delete SearchTags where SearchTags ModuleTypeID = 11
                divAddSearchTags.Visible = AdminUserDAL.CheckAdminUserModuleAccess(11, intSiteID)

                boolAllowSectionAdd = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Section_Add")), Convert.ToBoolean(drAdminUser("Allow_Section_Add_AllSites")))
                boolAllowPageAdd = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Page_Add")), Convert.ToBoolean(drAdminUser("Allow_Page_Add_AllSites")))
                boolAllowSectionEdit = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Section_Edit")), Convert.ToBoolean(drAdminUser("Allow_Section_Edit_AllSites")))
                boolAllowPageEdit = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Page_Edit")), Convert.ToBoolean(drAdminUser("Allow_Page_Edit_AllSites")))
                boolAllowSectionRename = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Section_Rename")), Convert.ToBoolean(drAdminUser("Allow_Section_Rename_AllSites")))
                boolAllowPageRename = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Page_Rename")), Convert.ToBoolean(drAdminUser("Allow_Page_Rename_AllSites")))
                boolAllowPublish = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Publish")), Convert.ToBoolean(drAdminUser("Allow_Publish_AllSites")))

                'If the user is allowed to publish pages live, we show the publish live link
                If boolAllowPublish Then
                    divSaveLive.Visible = True
                End If


                If Not Request.QueryString("pageID") Is Nothing Then
                    'we check if there is a supplied pageID, such that we must be editing a page
                    strEditorMode = MODE_EDIT_PAGE
                ElseIf Not Request.QueryString("parentID") Is Nothing Then
                    'Load the Editor so we can create a new page
                    strEditorMode = MODE_ADD_PAGE
                Else
                    'Load the Editor so we can create a new section
                    strEditorMode = MODE_ADD_SECTION
                End If

                'When creating a new section we get this from the request parameter
                If Not Request.QueryString("secure_members") Is Nothing Then
                    If Request.QueryString("secure_members").ToLower = "yes" Then
                        boolSecureMembers = True
                    End If
                End If

                If Not Request.QueryString("secure_education") Is Nothing Then
                    If Request.QueryString("secure_education").ToLower = "yes" Then
                        boolSecureEducation = True
                    End If
                End If

                Dim sSiteIdentifier_LDAP As String = String.Empty
                Dim dtSite As DataTable = SiteDAL.GetSite_ByID(intSiteID)
                If dtSite.Rows.Count > 0 Then
                    Dim drSite As DataRow = dtSite.Rows(0)
                    boolUseThreeColumnLayout = Convert.ToBoolean(drSite("UseThreeColumnLayout"))
                    intAllowedSiteDepth = Convert.ToInt32(drSite("SiteDepth"))
                    boolWebpage_PublicSection_EnableGroupsAndUsers = Convert.ToBoolean(drSite("Webpage_PublicSection_EnableGroupsAndUsers"))
                    boolWebpage_MemberSection_EnableGroupsAndUsers = Convert.ToBoolean(drSite("Webpage_MemberSection_EnableGroupsAndUsers"))

                    intWebInfoID_HomePage = Convert.ToInt32(drSite("WebInfoID_HomePage"))
                    intWebInfoID_Header = Convert.ToInt32(drSite("WebInfoID_Header"))
                    intWebInfoID_Footer = Convert.ToInt32(drSite("WebInfoID_Footer"))

                    'Also set the sSiteIdentifier_LDAP, as we need to use this to check the site is the corporate intranet, if so we can show the divBanner
                    If Not drSite("SiteIdentifier_LDAP") Is DBNull.Value Then
                        sSiteIdentifier_LDAP = drSite("SiteIdentifier_LDAP").ToString().ToUpper()
                    End If
                End If


                If Not IsPostBack Then

                    BindSearchTagsCheckBoxListData()

                    If strEditorMode = MODE_ADD_SECTION Then
                        'Only Super Admins can Add a Section
                        If intAdminUserAccessLevel <= 2 Then
                            Response.Redirect("~/richadmin/")
                        End If

                        'We are adding a new section, so we have no parentID, pageID and no page title/content yet
                        'Set the group panel, if we are dealing with a secure_section

                        'We are adding a section the first thing we do is check if the user can Add a Section
                        If boolAllowSectionAdd Then


                            'This page is a Section Page, so we show the sections specific banner image message, and thus hide the child page specific banner image message. ALSO Hide the 'inheritBannerImage' From Section checkbox
                            spanUploadMessageForSection.Visible = True
                            spanUploadMessageForPage.Visible = False
                            divInheritBannerImageFromSection.Visible = False
                            chkInheritBannerImageFromSection.Checked = False ' This is a section, so this should not be checked, as this page is already a section

                            If boolSecureMembers Then
                                If boolWebpage_MemberSection_EnableGroupsAndUsers Then

                                    'show users and groups tab
                                    rtsWebInfo.Tabs.FindTabByValue("3").Visible = True
                                End If

                            Else
                                If boolWebpage_PublicSection_EnableGroupsAndUsers Then

                                    'show users and groups tab
                                    rtsWebInfo.Tabs.FindTabByValue("3").Visible = True
                                End If
                            End If

                            BindGroupCheckBoxListData(boolSecureMembers Or boolSecureEducation, Integer.MinValue, Integer.MinValue, intWebInfoID_HomePage, intWebInfoID_Header, intWebInfoID_Footer)
                            BindUserCheckBoxListData(Integer.MinValue, Integer.MinValue, intWebInfoID_HomePage, intWebInfoID_Header, intWebInfoID_Footer)

                            'Now set up the AdminUserAccess Tab, and only make it visible if the user is a SuperAdmin
                            BindAdminUserAccessCheckBoxListData(Integer.MinValue)
                            If intAdminUserAccessLevel > 2 Then
                                'show AdminUser Access Tab
                                rtsWebInfo.Tabs.FindTabByValue("4").Visible = True
                            End If
                        Else
                            'Load the welcome screen
                            Response.Redirect("/admin/richtemplate_welcome.aspx?mode=forms")
                        End If

                    ElseIf strEditorMode = MODE_ADD_PAGE Then

                        'Only Super Admins can Add a Section
                        If intAdminUserAccessLevel <= 2 Then
                            Response.Redirect("~/richadmin/")
                        End If

                        'We are adding a new page, so we have a parentID of the page we want to add to but and no page title/content yet
                        Dim intWebInfoID_Parent As Integer = Convert.ToInt32(Request.QueryString("parentID"))

                        'Get the webinfo and load the page
                        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByIDAndSiteID(intWebInfoID_Parent, intSiteID)
                        If dtWebInfo.Rows.Count > 0 Then
                            Dim drWebInfo As DataRow = dtWebInfo.Rows(0)


                            Dim intSectionID As Integer = Convert.ToInt32(drWebInfo("SectionID").ToString())

                            'First Check If the User has Access to Add A New Page
                            'We are adding a page the first thing we do is check if the user can Add a Page
                            If boolAllowPageAdd Then

                                'If we are adding a page to the Header Container OR Footer Container, then the MAX Page Level is ALWAYS 3, so you can not add another page to the header PAGE or footer PAGE only add pages to header CONTAINER or footer CONTAINER
                                If intWebInfoID_Parent = intWebInfoID_Header Or intWebInfoID_Parent = intWebInfoID_Footer Then
                                    intAllowedSiteDepth = 3
                                End If

                                Dim newWebInfoPageLevel As Integer = Convert.ToInt32(drWebInfo("PageLevel")) + 1
                                If newWebInfoPageLevel > intAllowedSiteDepth Then
                                    'If the pagelevel has been violated
                                    'Get the section id and redirect them to the listpages page

                                    Response.Redirect("~/admin/richtemplate_list_pages.aspx?sectionid=" & intSectionID.ToString())

                                End If

                                'If we are using our 3 column layout and the new page level is 2, then show the navigation layout
                                rdNavigationLayout.SelectedValue = "1"
                                If boolUseThreeColumnLayout And newWebInfoPageLevel = 2 Then
                                    divSecondLevelPage.Visible = True
                                Else
                                    divSecondLevelPage.Visible = False
                                End If

                                'Setup our Banner Image if it exists - HOWEVER, as we do not have a webinfoID for this page yet, we use the pages SECTION ID,
                                ' ALSO, as we are ADDING A PAGE, no image for this page exists yet, so HIDE the delete image link and show the section banner default message that may have been shown from the SetupBannerImage If the section has a banner image
                                SetupBannerImage(intSectionID)
                                lnkDeleteImage.Visible = False
                                spanCurrentSectionBannerImageMessage.Visible = bannerImage.Visible

                                'This page is a Child Page, so we hide the sections specific banner image message, and just show the child page specific banner image message. ALSO Show the 'inheritBannerImage' From Section checkbox
                                spanUploadMessageForSection.Visible = False
                                spanUploadMessageForPage.Visible = True
                                divInheritBannerImageFromSection.Visible = True
                                chkInheritBannerImageFromSection.Checked = True


                                boolSecureMembers = Convert.ToInt32(drWebInfo("secure_members"))

                                If boolSecureMembers Then
                                    If boolWebpage_MemberSection_EnableGroupsAndUsers Then

                                        'show users and groups tab
                                        rtsWebInfo.Tabs.FindTabByValue("3").Visible = True
                                    End If

                                Else
                                    If boolWebpage_PublicSection_EnableGroupsAndUsers Then

                                        'show users and groups tab
                                        rtsWebInfo.Tabs.FindTabByValue("3").Visible = True
                                    End If
                                End If

                                'slightly different to the way modules bind groups and users, as here we want to show heirarcy check if the parent is also in the same group
                                'if the parent is not in a group that the child is in, we show a message, stating that this page will also not been seen by this group, as its parent is not in the group
                                BindGroupCheckBoxListData(boolSecureMembers Or boolSecureEducation, Integer.MinValue, intWebInfoID_Parent, intWebInfoID_HomePage, intWebInfoID_Header, intWebInfoID_Footer)
                                BindUserCheckBoxListData(Integer.MinValue, intWebInfoID_Parent, intWebInfoID_HomePage, intWebInfoID_Header, intWebInfoID_Footer)

                                'Now set up the AdminUserAccess Tab, and only make it visible if the user is a SuperAdmin
                                BindAdminUserAccessCheckBoxListData(Integer.MinValue)
                                If intAdminUserAccessLevel > 2 Then
                                    'show AdminUser Access Tab
                                    rtsWebInfo.Tabs.FindTabByValue("4").Visible = True
                                End If

                            Else
                                'Go to that section within the RichTemplate_List_Pages.aspx
                                Response.Redirect("~/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionID.ToString() & "&wi=" & intWebInfoID_Parent)
                            End If

                        Else
                            Response.Redirect("~/richadmin/")
                        End If


                    ElseIf strEditorMode = MODE_EDIT_PAGE Then

                        Dim intWebInfoID As Integer = Convert.ToInt32(Request.QueryString("pageID"))
                        'If the Admin User DOES NOT have access to EDIT this Page we Redirect them From this Page
                        If Not WebInfoDAL.HasWebInfoAdminUserAccess(intAdminUserAccessLevel, intAdminUserID, intWebInfoID) Then
                            Response.Redirect("~/richadmin/")
                        End If

                        'Load the editor so we can edit the specified page

                        'Get the webinfo and load the page
                        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByIDAndSiteID(intWebInfoID, intSiteID)
                        If dtWebInfo.Rows.Count > 0 Then
                            Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

                            'Get site config data, which we use in this function
                            Dim intWebInfoID_Parent As Integer = If(drWebInfo("PARENTID") Is DBNull.Value, Integer.MinValue, Convert.ToInt32(drWebInfo("PARENTID")))
                            Dim intPageLevel As Integer = Convert.ToInt32(drWebInfo("PageLevel"))

                            'Set the page name
                            Dim strWebpageName As String = drWebInfo("Name")
                            txtWebPageName.Text = strWebpageName

                            'Check if we are updating a section
                            Dim intSectionID As Integer = Convert.ToInt32(drWebInfo("SectionID"))
                            Dim boolIsSection As Boolean = (intWebInfoID = intSectionID)

                            'Finally before we load the EDIT Section or Edit Page, we check if the user has access to Edit the Section or Page
                            If (boolIsSection AndAlso boolAllowSectionEdit) Or (Not boolIsSection AndAlso boolAllowPageEdit) Then

                                Dim bCanRenameWebPage As Boolean = True
                                'check if we are editing the homepage or the header/footer, if so we dont' allow anyone to change this, as its pulled from the language resource file
                                If intWebInfoID = intWebInfoID_HomePage Then
                                    strWebpageName = Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_WebpageName_HomePage
                                    bCanRenameWebPage = False
                                ElseIf intWebInfoID = intWebInfoID_Header Then
                                    strWebpageName = Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_WebpageName_HeaderFolder
                                    bCanRenameWebPage = False
                                ElseIf intWebInfoID = intWebInfoID_Footer Then
                                    strWebpageName = Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_WebpageName_FooterFolder
                                    bCanRenameWebPage = False
                                Else
                                    'Only users who are allowed to 'Rename' Sections are allowed to change the page name for the section, likewise only users who are allowed to rename pages can change the page name for a sub page
                                    If (boolIsSection AndAlso (Not boolAllowSectionRename)) Or ((Not boolIsSection) AndAlso (Not boolAllowPageRename)) Then
                                        'The user is not allowed to rename a section, so show the divWebpageNameDisabled instead
                                        bCanRenameWebPage = False
                                    End If
                                End If

                                'If we do not allow the page to be renamed, we hide the textbox and show the literal representation
                                If Not bCanRenameWebPage Then
                                    litWebPageName.Text = strWebpageName
                                    divWebPageNameDisabled.Visible = True
                                    divWebPageNameEnabled.Visible = False

                                End If



                                'Set the page content
                                'Only load the draft message is the page status exists and is offline
                                If Not Request.QueryString("pageStatus") Is Nothing Then
                                    If Request.QueryString("pageStatus").ToLower() = "offline" Then
                                        If Not drWebInfo("message2") Is DBNull.Value Then
                                            txtMessage.Content = drWebInfo("message2")
                                        End If
                                    Else
                                        If Not drWebInfo("message") Is DBNull.Value Then
                                            txtMessage.Content = drWebInfo("message")
                                        End If
                                    End If
                                Else
                                    If Not drWebInfo("message") Is DBNull.Value Then
                                        txtMessage.Content = drWebInfo("message")
                                    End If

                                End If

                                'If the page we are editing page level is 2, then show the navigation layout and select the proper navigation layout column
                                If boolUseThreeColumnLayout And intPageLevel = 2 Then
                                    divSecondLevelPage.Visible = True
                                    Dim intNavigationColumnIndex As Integer = Convert.ToInt32(drWebInfo("NavigationColumnIndex"))
                                    rdNavigationLayout.SelectedValue = intNavigationColumnIndex.ToString()
                                Else
                                    divSecondLevelPage.Visible = False
                                    rdNavigationLayout.SelectedValue = "1"
                                End If

                                'Setup our Banner Image if it exists
                                SetupBannerImage(intWebInfoID)
                                'If this page is a section, then show the Upload Message For Section, else show the Upload Message For the Child Page. ALSO Show the 'inheritBannerImage' From Section checkbox only if we are not updating a section page
                                spanUploadMessageForSection.Visible = If(intWebInfoID = intSectionID, True, False)
                                spanUploadMessageForPage.Visible = If(intWebInfoID = intSectionID, False, True)

                                Dim boolInheritBannerImage As Boolean = Convert.ToBoolean(drWebInfo("InheritBannerImage"))
                                divInheritBannerImageFromSection.Visible = If(intWebInfoID = intSectionID, False, True)
                                chkInheritBannerImageFromSection.Checked = boolInheritBannerImage


                                'Set the group panel, if we are dealing with a secure_section
                                'Check if we are editing a section or a subpage
                                Dim intWebInfoParentID As Integer = 0
                                If Not drWebInfo("parentID") Is DBNull.Value Then
                                    intWebInfoParentID = Convert.ToInt32(drWebInfo("parentID"))
                                End If

                                boolSecureMembers = Convert.ToInt32(drWebInfo("secure_members"))

                                If boolSecureMembers Then
                                    If boolWebpage_MemberSection_EnableGroupsAndUsers Then

                                        'show users and groups tab
                                        rtsWebInfo.Tabs.FindTabByValue("3").Visible = True
                                    End If

                                Else
                                    If boolWebpage_PublicSection_EnableGroupsAndUsers Then

                                        'show users and groups tab
                                        rtsWebInfo.Tabs.FindTabByValue("3").Visible = True
                                    End If
                                End If

                                'Set meta information
                                'Only show meta information for public pages
                                If (Not boolSecureMembers) Then
                                    'Setup our Meta Data Section
                                    If Not drWebInfo("MetaTitle") Is DBNull.Value Then
                                        txtMetaTitle.Text = drWebInfo("MetaTitle").ToString()
                                    End If
                                    If Not drWebInfo("MetaKeyword") Is DBNull.Value Then
                                        txtMetaKeywords.Text = drWebInfo("MetaKeyword").ToString()
                                    End If
                                    If Not drWebInfo("MetaDesc") Is DBNull.Value Then
                                        txtMetaDescription.Text = drWebInfo("MetaDesc").ToString()
                                    End If
                                End If

                                '*** Populate search tags cbl ***
                                Dim chkbx As CheckBoxList
                                chkbx = CType(cblSearchTags, CheckBoxList)

                                Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intWebInfoID)

                                For Each drSearchTag In dtSearchTags.Rows
                                    Dim currentCheckBox As ListItem
                                    currentCheckBox = chkbx.Items.FindByValue(drSearchTag("searchTagID").ToString())
                                    If currentCheckBox IsNot Nothing Then
                                        currentCheckBox.Selected = True
                                    End If
                                Next

                                'slightly different to the way modules bind groups and users, as here we want to show heirarcy check if the parent is also in the same group
                                'if the parent is not in a group that the child is in, we show a message, stating that this page will also not been seen by this group, as its parent is not in the group
                                BindGroupCheckBoxListData(boolSecureMembers Or boolSecureEducation, intWebInfoID, intWebInfoID_Parent, intWebInfoID_HomePage, intWebInfoID_Header, intWebInfoID_Footer)
                                BindUserCheckBoxListData(intWebInfoID, intWebInfoID_Parent, intWebInfoID_HomePage, intWebInfoID_Header, intWebInfoID_Footer)


                                'Finally get the current user and set the page as 'CHECKED OUT'
                                Dim strUsername As String = drAdminUser("username").ToString()
                                Dim boolCheckedOut As Boolean = True
                                WebInfoDAL.UpdateWebInfo_CheckInCheckOut_ByID(intWebInfoID, boolCheckedOut, strUsername)

                                'Now set up the AdminUserAccess Tab, and only make it visible if the user is a SuperAdmin
                                BindAdminUserAccessCheckBoxListData(intWebInfoID)
                                If intAdminUserAccessLevel > 2 Then
                                    'show AdminUser Access Tab
                                    rtsWebInfo.Tabs.FindTabByValue("4").Visible = True
                                End If
                            Else

                                'Get the current user and set the documents checkedOut as false and set its checkedID
                                Dim boolCheckedOut As Boolean = False

                                Dim strAuthor As String = drAdminUser("username").ToString()
                                WebInfoDAL.UpdateWebInfo_CheckInCheckOut_ByID(intWebInfoID, False, strAuthor)

                                'Go to that section within the RichTemplate_List_Pages.aspx
                                Response.Redirect("~/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionID.ToString() & "&wi=" & intWebInfoID)
                            End If
                        Else
                            Response.Redirect("~/richadmin/")
                        End If
                    End If

                    'Finally if the admin users access level is a super administrator (i.e. AccessLevel > 2) Show all upload banner image relate panels
                    If intAdminUserAccessLevel > 2 Then
                        'Now check if this is the Corporate Intranet, only this site can change the page banner
                        divBannerImage.Visible = True
                    End If


                End If
            Else
                Response.Redirect("~/richadmin/")
            End If
        Else
            Response.Redirect("~/richadmin/")
        End If
    End Sub

    Public Sub BindSearchTagsCheckBoxListData()
        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsList_BySiteID(intSiteID)

        For Each drSearchTag As DataRow In dtSearchTags.Rows()
            Dim intSearchTagID As String = drSearchTag("searchTagID").ToString()
            Dim strSearchTagName As String = (drSearchTag("searchTagName").ToString() & "<br/><span class='graySubText'>") + drSearchTag("searchTagDescription").ToString() & "</span>"
            cblSearchTags.Items.Add(New ListItem(strSearchTagName, intSearchTagID))
        Next
    End Sub

    Public Sub BindGroupCheckBoxListData(ByVal boolIsSecureMemberOrEducationPage As Boolean, ByVal intWebInfoID As Integer, ByVal intWebInfoID_Parent As Integer, ByVal intWebInfoID_HomePage As Integer, ByVal intWebInfoID_Header As Integer, ByVal intWebInfoID_Footer As Integer)

        'Bind our WebAccess to our Group List, by getting our group and its permission information
        Dim dtWebInfoAccess As DataTable = Nothing
        If intWebInfoID = Integer.MinValue AndAlso (intWebInfoID_Parent = Integer.MinValue Or intWebInfoID_Parent = intWebInfoID_Header Or intWebInfoID_Parent = intWebInfoID_Footer) Then
            'We are esentially ADDING A SECTION page or a page that is under the header or footer navs, in both cases we set the webinfo access permissions to be the same as the HOME PAGE
            dtWebInfoAccess = WebInfoDAL.GetWebInfoAccessList_ForGroupsSectionAdd_ByWebInfoIDHomePageAndSiteID(intWebInfoID_HomePage, intSiteID)
        ElseIf intWebInfoID = Integer.MinValue Then
            'Then we are ADDING SUB PAGE, by default we set the webinfo access permissions to be the same as its parent
            dtWebInfoAccess = WebInfoDAL.GetWebInfoAccessList_ForGroupsSubPageAdd_ByWebInfoIDParentAndSiteID(intWebInfoID_Parent, intSiteID)
        ElseIf (intWebInfoID_Parent = Integer.MinValue Or intWebInfoID_Parent = intWebInfoID_Header Or intWebInfoID_Parent = intWebInfoID_Footer) Then
            'Then we are EDITTING A SECTION PAGE, we just load its permissions and do not show any parent permissions messages
            dtWebInfoAccess = WebInfoDAL.GetWebInfoAccessList_ForGroupsSectionEdit_ByWebInfoIDAndSiteID(intWebInfoID, intSiteID)
        Else
            'Finally we must be EDITTING A SUB PAGE
            dtWebInfoAccess = WebInfoDAL.GetWebInfoAccessList_ForGroupsSubPageEdit_ByWebInfoIDAndWebInfoIDParentAndSiteID(intWebInfoID, intWebInfoID_Parent, intSiteID)
        End If

        'First clear the existing items, and re-build our group list
        cblGroupList.Items.Clear()
        For Each drWebInfoAccess As DataRow In dtWebInfoAccess.Rows
            Dim intGroupID As Integer = Convert.ToInt32(drWebInfoAccess("groupID"))
            Dim boolIsPageForGroup As Boolean = Convert.ToBoolean(drWebInfoAccess("IsPageForGroup"))
            Dim boolIsParentPageForGroup As Boolean = Convert.ToBoolean(drWebInfoAccess("IsParentPageForGroup"))

            If intGroupID = -1 Then
                'This is the Un-Authenticated Users Group and only used if we are not talking about the MEMBERS SECTION
                If Not boolIsSecureMemberOrEducationPage Then
                    Dim liUnAuthenticatedGroup As ListItem = New ListItem(Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_UserGroups_GroupsUnAuthenticated_PublicPage & If(boolIsParentPageForGroup, "", " <span class='errorStyle'>" & Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_UserGroups_GroupNotSelectedInParentPage & "</span>") & "<br/><span class='graySubText'>" & Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_UserGroups_GroupsUnAuthenticatedDescription_PublicPage & "</span>", "-1")
                    liUnAuthenticatedGroup.Selected = boolIsPageForGroup
                    cblGroupList.Items.Add(liUnAuthenticatedGroup)
                End If

            ElseIf intGroupID = 0 Then
                'This is the Authenticated Members Group
                Dim liAuthenticatedGroup As ListItem = New ListItem(Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_UserGroups_GroupsAuthenticated_MemberPage & If(boolIsParentPageForGroup, "", " <span class='errorStyle'>" & Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_UserGroups_GroupNotSelectedInParentPage & "</span>") & "<br/><span class='graySubText'>" & Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_UserGroups_GroupsAuthenticatedDescription_MemberPage & "</span>", "0")
                liAuthenticatedGroup.Selected = boolIsPageForGroup
                cblGroupList.Items.Add(liAuthenticatedGroup)

            Else
                'These are adminuser-created groups
                Dim strGroupName As String = drWebInfoAccess("GroupName")
                Dim strGroupDescription As String = drWebInfoAccess("GroupDescription")
                Dim liGroup As New ListItem(strGroupName & If(boolIsParentPageForGroup, "", " <span class='errorStyle'>" & Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_UserGroups_GroupNotSelectedInParentPage & "</span>") & "<br/><span class='graySubText'>" & strGroupDescription & "</span>", intGroupID)
                liGroup.Selected = boolIsPageForGroup
                cblGroupList.Items.Add(liGroup)

            End If
        Next

    End Sub

    Public Sub BindUserCheckBoxListData(ByVal intWebInfoID As Integer, ByVal intWebInfoID_Parent As Integer, ByVal intWebInfoID_HomePage As Integer, ByVal intWebInfoID_Header As Integer, ByVal intWebInfoID_Footer As Integer)
        'Bind our WebAccess to our Member List, by getting our members for this site and its permission information
        Dim dtWebInfoAccess_Members As DataTable = Nothing
        If intWebInfoID = Integer.MinValue AndAlso (intWebInfoID_Parent = Integer.MinValue Or intWebInfoID_Parent = intWebInfoID_Header Or intWebInfoID_Parent = intWebInfoID_Footer) Then
            'We are esentially ADDING A SECTION page or a page that is under the header or footer navs, in both cases we set the webinfo access permissions to be the same as the HOME PAGE
            dtWebInfoAccess_Members = WebInfoDAL.GetWebInfoAccessList_ForUsersSectionAdd_ByWebInfoIDHomePageAndSiteID(intWebInfoID_HomePage, intSiteID)
        ElseIf intWebInfoID = Integer.MinValue Then
            'Then we are ADDING SUB PAGE, by default we set the webinfo access permissions to be the same as its parent
            dtWebInfoAccess_Members = WebInfoDAL.GetWebInfoAccessList_ForUsersSubPageAdd_ByWebInfoIDParentAndSiteID(intWebInfoID_Parent, intSiteID)
        ElseIf (intWebInfoID_Parent = Integer.MinValue Or intWebInfoID_Parent = intWebInfoID_Header Or intWebInfoID_Parent = intWebInfoID_Footer) Then
            'Then we are EDITTING A SECTION PAGE, we just load its permissions and do not show any parent permissions messages
            dtWebInfoAccess_Members = WebInfoDAL.GetWebInfoAccessList_ForUsersSectionEdit_ByWebInfoIDAndSiteID(intWebInfoID, intSiteID)
        Else
            'Finally we must be EDITTING A SUB PAGE
            dtWebInfoAccess_Members = WebInfoDAL.GetWebInfoAccessList_ForusersSubPageEdit_ByWebInfoIDAndWebInfoIDParentAndSiteID(intWebInfoID, intWebInfoID_Parent, intSiteID)
        End If

        'First clear the existing items, and re-build our group list
        cblMemberList.Items.Clear()
        For Each drWebInfoAccess_Members As DataRow In dtWebInfoAccess_Members.Rows

            Dim intMemberID As String = drWebInfoAccess_Members("ID").ToString()

            Dim strSalutation_LangaugeSpecific As String = String.Empty
            If Not drWebInfoAccess_Members("Salutation_LanguageProperty") Is DBNull.Value Then
                Dim strSalutation_LanguageProperty As String = drWebInfoAccess_Members("Salutation_LanguageProperty")
                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
            End If
            Dim strMemberName As String = CommonWeb.FormatName(strSalutation_LangaugeSpecific, drWebInfoAccess_Members("firstName").ToString(), drWebInfoAccess_Members("lastName").ToString())

            Dim boolIsPageForUser As Boolean = Convert.ToBoolean(drWebInfoAccess_Members("IsPageForUser"))
            Dim boolIsParentPageForUser As Boolean = Convert.ToBoolean(drWebInfoAccess_Members("IsParentPageForUser"))

            Dim liMember As New ListItem(strMemberName & If(boolIsParentPageForUser, "", " <span class='errorStyle'>" & Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_UserGroups_UserNotSelectedInParentPage & "</span>"), intMemberID)
            liMember.Selected = boolIsPageForUser
            cblMemberList.Items.Add(liMember)

        Next
    End Sub

    Public Sub BindAdminUserAccessCheckBoxListData(ByVal intWebInfoID As Integer)
        'Bind our AdminUserAccess
        Dim dtAdminUsers As DataTable = AdminUserDAL.GetAdminUserList_BySiteIDAndAccessLevel(SiteDAL.GetCurrentSiteID_Admin(), 2) 'Only want AdminUsers who are NOT SUPER ADMIN USERS

        'Only pre-populate this list with their access if the intWebInfoID is Not Int.Minvalue
        Dim listAdminUserAccess As New List(Of Integer)

        If intWebInfoID > 0 Then
            Dim dtWebInfoAdminUserAccess = WebInfoDAL.GetWebInfoAdminUserAccessList_ByWebInfoID(intWebInfoID)
            For Each drWebInfoAdminUserAccess As DataRow In dtWebInfoAdminUserAccess.Rows
                listAdminUserAccess.Add(Convert.ToInt32(drWebInfoAdminUserAccess("UserID")))
            Next
        End If

        'Generate a list of AdminUsers with access to this Page

        'First clear the existing items, and re-build our AdminUserList
        cblAdminUserList.Items.Clear()
        For Each drAdminUsers As DataRow In dtAdminUsers.Rows

            Dim intUserID As Integer = Convert.ToInt32(drAdminUsers("ID"))

            Dim strSalutation_LangaugeSpecific As String = String.Empty
            If Not drAdminUsers("Salutation_LanguageProperty") Is DBNull.Value Then
                Dim strSalutation_LanguageProperty As String = drAdminUsers("Salutation_LanguageProperty")
                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
            End If
            Dim sAdminUserName As String = CommonWeb.FormatName(strSalutation_LangaugeSpecific, drAdminUsers("First_Name").ToString(), drAdminUsers("Last_Name").ToString())

            Dim boolHasAdminUserAccess As Boolean = listAdminUserAccess.Contains(intUserID)

            Dim liAdminUser As New ListItem(sAdminUserName, intUserID.ToString())
            liAdminUser.Selected = boolHasAdminUserAccess
            cblAdminUserList.Items.Add(liAdminUser)

        Next
    End Sub

#Region "Events"

    Protected Sub lnkSaveAsDraft_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkSaveAsDraft.Click
        If IsValid Then
            Dim intWebInfoID As Integer = SaveAsDraftVersion()

            Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByIDAndSiteID(intWebInfoID, intSiteID)
            If dtWebInfo.Rows.Count > 0 Then
                Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

                Dim intSectionID As Integer = drWebInfo("SectionID")
                boolSecureMembers = Convert.ToBoolean(drWebInfo("secure_members"))
                boolSecureEducation = Convert.ToBoolean(drWebInfo("secure_education"))

                'Use javascript to reload the listpages page in the main iframe and reload the tree
                'Use javascript to reload the welcome page and tree
                Dim strTreeExtendedQueryString As String = ""
                If boolSecureMembers Then
                    strTreeExtendedQueryString = "?secure_members=yes"
                ElseIf boolSecureEducation Then
                    strTreeExtendedQueryString = "?secure_education=yes"
                End If
                CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, "/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionID.ToString() & "&wi=" & intWebInfoID, "/admin/richtemplate_list_sections.aspx" & strTreeExtendedQueryString, String.Empty, String.Empty)
            End If
        End If

    End Sub

    Protected Sub lnkPublishLive_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkPublishLive.Click
        If IsValid Then
            Dim intWebInfoID As Integer = SaveAsLiveVersion()

            Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByIDAndSiteID(intWebInfoID, intSiteID)
            If dtWebInfo.Rows.Count > 0 Then
                Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

                Dim intSectionID As Integer = drWebInfo("SectionID")
                boolSecureMembers = Convert.ToBoolean(drWebInfo("secure_members"))
                boolSecureEducation = Convert.ToBoolean(drWebInfo("secure_education"))

                'Use javascript to reload the listpages page in the main iframe and reload the tree
                'Use javascript to reload the welcome page and tree
                Dim strTreeExtendedQueryString As String = ""
                If boolSecureMembers Then
                    strTreeExtendedQueryString = "?secure_members=yes"
                ElseIf boolSecureEducation Then
                    strTreeExtendedQueryString = "?secure_education=yes"
                End If
                CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, "/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionID.ToString() & "&wi=" & intWebInfoID, "/admin/richtemplate_list_sections.aspx" & strTreeExtendedQueryString, String.Empty, String.Empty)
            End If
        End If

    End Sub

    Protected Sub lnkAddSearchTags_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'Save progress and send to category page
        If Page.IsValid Then
            Dim intWebInfoID As Integer = SaveAsDraftVersion()
            Response.Redirect("~/admin/modules/searchtags/default.aspx?wid=" & intWebInfoID.ToString())
        End If

    End Sub

    Protected Sub lnkCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkCancel.Click
        'Need to get the section that this page corresponds to
        If strEditorMode = MODE_ADD_SECTION Then
            'Load the welcome screen
            Response.Redirect("/admin/richtemplate_welcome.aspx?mode=forms")
        ElseIf strEditorMode = MODE_ADD_PAGE Then
            'Load the editor so we can edit the specified page
            Dim intWebInfoParentID As Integer = Convert.ToInt32(Request.QueryString("parentID"))

            Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByIDAndSiteID(intWebInfoParentID, intSiteID)
            If dtWebInfo.Rows.Count > 0 Then

                'Get the webinfo and load the page
                Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

                'Get the section for this page
                Dim intSectionId As Integer = drWebInfo("SectionID")

                'Go to that section within the RichTemplate_List_Pages.aspx
                Response.Redirect("~/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionId.ToString() & "&wi=" & intWebInfoParentID)

            End If
        ElseIf strEditorMode = MODE_EDIT_PAGE Then
            'Load the editor so we can edit the specified page
            Dim intWebInfoID As Integer = Convert.ToInt32(Request.QueryString("pageID"))

            Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByIDAndSiteID(intWebInfoID, intSiteID)
            If dtWebInfo.Rows.Count > 0 Then

                'Get the webinfo and load the page
                Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

                'Get the section for this page
                Dim intSectionId As Integer = drWebInfo("SectionID")

                'Get the current user and set the documents checkedOut as false and set its checkedID
                Dim boolCheckedOut As Boolean = False

                Dim strAuthor As String = ""
                Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
                Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
                If dtAdminUser.Rows.Count > 0 Then
                    Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                    strAuthor = drAdminUser("username").ToString()
                End If
                WebInfoDAL.UpdateWebInfo_CheckInCheckOut_ByID(intWebInfoID, False, strAuthor)


                'Go to that section within the RichTemplate_List_Pages.aspx
                Response.Redirect("~/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionId.ToString() & "&wi=" & intWebInfoID)

            End If
        End If

    End Sub

#End Region

    Private Function SaveAsDraftVersion() As Integer
        'Need to get the section that this page corresponds to
        Dim intWebInfo As Integer = 0
        If strEditorMode = MODE_ADD_SECTION Then
            intWebInfo = AddNewSection(True)

        ElseIf strEditorMode = MODE_ADD_PAGE Then
            'Add the page as a sub page to the current parent
            Dim intWebInfoParentID As Integer = Convert.ToInt32(Request.QueryString("parentID"))
            intWebInfo = AddSubPage(intWebInfoParentID, True)
        ElseIf strEditorMode = MODE_EDIT_PAGE Then
            'Load the editor so we can edit the specified page
            Dim intWebInfoID As Integer = Convert.ToInt32(Request.QueryString("pageID"))
            intWebInfo = UpdatePage(intWebInfoID, True)

        End If

        Return intWebInfo
    End Function

    Private Function SaveAsLiveVersion() As Integer
        'Need to get the section that this page corresponds to
        Dim intWebInfo As Integer = 0
        If strEditorMode = MODE_ADD_SECTION Then
            intWebInfo = AddNewSection(False)

        ElseIf strEditorMode = MODE_ADD_PAGE Then
            'Add the page as a sub page to the current parent
            Dim intWebInfoParentID As Integer = Convert.ToInt32(Request.QueryString("parentID"))
            intWebInfo = AddSubPage(intWebInfoParentID, False)
        ElseIf strEditorMode = MODE_EDIT_PAGE Then
            'Load the editor so we can edit the specified page
            Dim intWebInfoID As Integer = Convert.ToInt32(Request.QueryString("pageID"))
            intWebInfo = UpdatePage(intWebInfoID, False)
        End If

        Return intWebInfo
    End Function

    Protected Function AddNewSection(ByVal isDraft As Boolean) As Integer

        Dim intWebInfoID As Integer = Integer.MinValue
        'For Save as draft we use the Message2 field and empty string for Message field
        Dim strName As String = txtWebPageName.Text.Trim()
        Dim intParentID As Integer = Integer.MinValue
        Dim intPageLevel As Integer = 1
        Dim boolDefaultPage As Boolean = True

        Dim strMessage As String = ""
        Dim strMessage2 As String = ""
        If isDraft Then
            strMessage2 = txtMessage.Content
        Else
            strMessage = txtMessage.Content
        End If

        'Get the current user and set the author
        Dim strAuthor As String = ""
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
        If dtAdminUser.Rows.Count > 0 Then
            Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

            strAuthor = drAdminUser("username").ToString()
        End If

        Dim dtLastModified As DateTime = DateTime.Now()
        Dim boolPending As Boolean = True
        Dim boolCheckedOut As Boolean = True
        Dim strCheckedID As String = strAuthor

        Dim boolLinkOnly As Boolean = False
        Dim strLinkURL As String = ""
        Dim strLinkFrame As String = ""
        Dim intSectionID As Integer = Integer.MinValue 'This gets passed in as null, such that the stored proceedure will fill this in
        Dim strMetaDesc As String = txtMetaDescription.Text.Trim()
        Dim strMetaTitle As String = txtMetaTitle.Text.Trim()
        Dim strMetaKeyword As String = txtMetaKeywords.Text.Trim()
        Dim strPage_LinkName As String = "interior.aspx"
        Dim boolSearchable As Boolean = False
        Dim intLanguage As Integer = Integer.MinValue
        Dim strUrlPath As String = ""

        Dim strSearchTags As String = ""

        Dim intNavigationIndex As Integer = Convert.ToInt32(rdNavigationLayout.SelectedValue)
        Dim boolInheritBannerImage As Boolean = chkInheritBannerImageFromSection.Checked 'As we are creating a section page, we do not inherit the banner image from the section page, as this page is ALREADY THE SECTION page

        intWebInfoID = WebInfoDAL.InsertWebInfo(strName, intParentID, intSiteID, intPageLevel, boolDefaultPage, strMessage, strMessage2, strAuthor, dtLastModified, boolPending, boolCheckedOut, strCheckedID, boolLinkOnly, strLinkURL, strLinkFrame, intSectionID, strMetaTitle, strMetaDesc, strMetaKeyword, strPage_LinkName, boolSearchable, intLanguage, strUrlPath, boolSecureMembers, boolSecureEducation, strSearchTags, intNavigationIndex, boolInheritBannerImage)

        'Add banner image if it exists
        If RadUploadBannerImage.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = RadUploadBannerImage.UploadedFiles(0)
            Dim strBannerName As String = file.GetName
            Dim bytesBannerImage(file.InputStream.Length - 1) As Byte
            file.InputStream.Read(bytesBannerImage, 0, file.InputStream.Length)
            WebInfoDAL.UpdateWebInfo_BannerImage_ByID(intWebInfoID, strBannerName, bytesBannerImage)

        End If

        'Add groups that we want to associate with this web page
        'Read all GroupIDs for the webinfo access
        For Each liGroupID As ListItem In cblGroupList.Items
            If liGroupID.Selected Then
                Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                WebInfoDAL.InsertWebInfoAccess(intWebInfoID, intGroupID, Integer.MinValue)
            End If
        Next

        'Read all MemberIDs for the webinfo's access
        For Each liMemberID As ListItem In cblMemberList.Items
            If liMemberID.Selected Then
                Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                WebInfoDAL.InsertWebInfoAccess(intWebInfoID, Integer.MinValue, intMemberID)
            End If
        Next

        'Read all Search Tags and create Xref for the selected search tags
        'Enter all new tags
        For Each liSearchTagID As ListItem In cblSearchTags.Items
            If liSearchTagID.Selected Then
                Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intWebInfoID)
            End If
        Next

        'Read all AdminUserAccess for the webinfo's access
        For Each liAdminUser As ListItem In cblAdminUserList.Items
            If liAdminUser.Selected Then
                Dim liAdminUserID As Integer = Convert.ToInt32(liAdminUser.Value)
                WebInfoDAL.InsertWebInfoAdminUserAccess(liAdminUserID, intWebInfoID)
            End If
        Next

        Return intWebInfoID

    End Function

    Protected Function AddSubPage(ByVal intWebInfoParentID As Integer, ByVal isDraft As Boolean) As Integer
        'Get the parent page, so we can inherit properties from this parent
        Dim intWebInfoID As Integer = Integer.MinValue
        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByIDAndSiteID(intWebInfoParentID, intSiteID)
        If dtWebInfo.Rows.Count > 0 Then
            Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

            'For Save as draft we use the Message2 field and empty string for Message field
            Dim strName As String = txtWebPageName.Text.Trim()

            Dim intPageLevel As Integer = Convert.ToInt32(drWebInfo("PageLevel")) + 1
            Dim boolDefaultPage As Boolean = False

            Dim strMessage As String = ""
            Dim strMessage2 As String = ""
            If isDraft Then
                strMessage2 = txtMessage.Content
            Else
                strMessage = txtMessage.Content
            End If

            'Get the current user and set the author
            Dim strAuthor As String = ""
            Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
            If dtAdminUser.Rows.Count > 0 Then
                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                strAuthor = drAdminUser("username").ToString()
            End If

            Dim dtLastModified As DateTime = DateTime.Now()
            Dim boolPending As Boolean = True
            Dim boolCheckedOut As Boolean = False
            Dim strCheckedID As String = ""

            Dim boolLinkOnly As Boolean = False
            Dim strLinkURL As String = ""
            Dim strLinkFrame As String = ""
            Dim intSectionID As Integer = Convert.ToInt32(drWebInfo("sectionID"))
            Dim strMetaDesc As String = txtMetaDescription.Text.Trim()
            Dim strMetaTitle As String = txtMetaTitle.Text.Trim()
            Dim strMetaKeyword As String = txtMetaKeywords.Text.Trim()
            Dim strPage_LinkName As String = "interior.aspx"
            Dim boolSearchable As Boolean = False
            Dim intLanguage As Integer = Integer.MinValue
            Dim strUrlPath As String = ""


            'Check if this page is for secure members OR secure education, this is inherited from its parent
            boolSecureMembers = Convert.ToBoolean(drWebInfo("secure_members"))
            boolSecureEducation = Convert.ToBoolean(drWebInfo("secure_education"))

            Dim strSearchTags As String = ""

            Dim intNavigationIndex As Integer = Convert.ToInt32(rdNavigationLayout.SelectedValue)
            Dim boolInheritBannerImage As Boolean = chkInheritBannerImageFromSection.Checked 'As we are creating a child page, inside an existing section page, we set InheritBannerImage from the section as our DEFAULT

            intWebInfoID = WebInfoDAL.InsertWebInfo(strName, intWebInfoParentID, intSiteID, intPageLevel, boolDefaultPage, strMessage, strMessage2, strAuthor, dtLastModified, boolPending, boolCheckedOut, strCheckedID, boolLinkOnly, strLinkURL, strLinkFrame, intSectionID, strMetaTitle, strMetaDesc, strMetaKeyword, strPage_LinkName, boolSearchable, intLanguage, strUrlPath, boolSecureMembers, boolSecureEducation, strSearchTags, intNavigationIndex, boolInheritBannerImage)

            'Add banner image if it exists
            If RadUploadBannerImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadBannerImage.UploadedFiles(0)
                Dim strBannerName As String = file.GetName
                Dim bytesBannerImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesBannerImage, 0, file.InputStream.Length)
                WebInfoDAL.UpdateWebInfo_BannerImage_ByID(intWebInfoID, strBannerName, bytesBannerImage)

            End If

            'Add groups that we want to associate with this web page
            'Read all GroupIDs for the webinfo access
            For Each liGroupID As ListItem In cblGroupList.Items
                If liGroupID.Selected Then
                    Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                    WebInfoDAL.InsertWebInfoAccess(intWebInfoID, intGroupID, Integer.MinValue)
                End If
            Next

            'Read all MemberIDs for the webinfo's access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                    WebInfoDAL.InsertWebInfoAccess(intWebInfoID, Integer.MinValue, intMemberID)
                End If
            Next


            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intWebInfoID)
                End If
            Next

            'Read all AdminUserAccess for the webinfo's access
            For Each liAdminUser As ListItem In cblAdminUserList.Items
                If liAdminUser.Selected Then
                    Dim liAdminUserID As Integer = Convert.ToInt32(liAdminUser.Value)
                    WebInfoDAL.InsertWebInfoAdminUserAccess(liAdminUserID, intWebInfoID)
                End If
            Next

        End If

        Return intWebInfoID
    End Function

    Protected Function UpdatePage(ByVal intWebInfoID As Integer, ByVal isDraft As Boolean) As Integer

        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByIDAndSiteID(intWebInfoID, intSiteID)
        If dtWebInfo.Rows.Count > 0 Then

            'Get the webinfo and load the page
            Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

            'Get the section for this page
            Dim intSectionID As Integer = drWebInfo("SectionID")

            'Update the pages name and MESSAGE (live) content
            Dim strName As String = txtWebPageName.Text.Trim()

            Dim strMessage As String = ""
            Dim strMessage2 As String = ""
            If isDraft Then
                'We are updating the draft version 'Message2'
                If Not drWebInfo("message") Is DBNull.Value Then
                    strMessage = drWebInfo("Message")
                End If
                strMessage2 = txtMessage.Content
            Else
                'We are updating the live version 'Message'
                strMessage = txtMessage.Content
                If Not drWebInfo("Message2") Is DBNull.Value Then
                    strMessage2 = drWebInfo("Message2")
                End If
            End If

            Dim dtLastModified As DateTime = DateTime.Now

            Dim strMetaDesc As String = txtMetaDescription.Text.Trim()
            Dim strMetaTitle As String = txtMetaTitle.Text.Trim()
            Dim strMetaKeyword As String = txtMetaKeywords.Text.Trim()

            boolSecureMembers = Convert.ToBoolean(drWebInfo("secure_members"))
            boolSecureEducation = Convert.ToBoolean(drWebInfo("secure_education"))

            'Get the current user and set the documents checkedOut as false and set its checkedID
            Dim boolCheckedOut As Boolean = False

            Dim strAuthor As String = ""
            Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
            If dtAdminUser.Rows.Count > 0 Then
                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                strAuthor = drAdminUser("username").ToString()
            End If

            Dim intNavigationIndex As Integer = Convert.ToInt32(rdNavigationLayout.SelectedValue)
            Dim boolInheritBannerImage As Boolean = chkInheritBannerImageFromSection.Checked

            WebInfoDAL.UpdateWebInfo_CheckInCheckOut_ByID(intWebInfoID, boolCheckedOut, strAuthor)
            WebInfoDAL.UpdateWebInfo_NameAndContent(intWebInfoID, strName, strMessage, strMessage2, intNavigationIndex, dtLastModified, boolInheritBannerImage)
            WebInfoDAL.UpdateWebInfo_MetaInformation(intWebInfoID, strMetaTitle, strMetaDesc, strMetaKeyword)

            'Add banner image if it exists
            If RadUploadBannerImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadBannerImage.UploadedFiles(0)
                Dim strBannerName As String = file.GetName
                Dim bytesBannerImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesBannerImage, 0, file.InputStream.Length)
                WebInfoDAL.UpdateWebInfo_BannerImage_ByID(intWebInfoID, strBannerName, bytesBannerImage)

            End If

            'Delete all groups associated with this web page
            WebInfoDAL.DeleteWebInfoAccess_ByWebInfoID(intWebInfoID)

            'Add groups that we want to associate with this web page
            'Read all GroupIDs for the webinfo access
            For Each liGroupID As ListItem In cblGroupList.Items
                If liGroupID.Selected Then
                    Dim intGroupID As Integer = Convert.ToInt32(liGroupID.Value)
                    WebInfoDAL.InsertWebInfoAccess(intWebInfoID, intGroupID, Integer.MinValue)
                End If
            Next

            'Read all MemberIDs for the webinfo's access
            For Each liMemberID As ListItem In cblMemberList.Items
                If liMemberID.Selected Then
                    Dim intMemberID As Integer = Convert.ToInt32(liMemberID.Value)
                    WebInfoDAL.InsertWebInfoAccess(intWebInfoID, Integer.MinValue, intMemberID)
                End If
            Next

            'Remove all tags before entering new list
            SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intWebInfoID)

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, intWebInfoID)
                End If
            Next

            'Delete all AdminUserAccess associated with this web page
            WebInfoDAL.DeleteWebInfoAdminUserAccess_ByWebInfoID(intWebInfoID)

        'Read all AdminUserAccess for the webinfo's access
            For Each liAdminUser As ListItem In cblAdminUserList.Items
                If liAdminUser.Selected Then
                    Dim liAdminUserID As Integer = Convert.ToInt32(liAdminUser.Value)
                    WebInfoDAL.InsertWebInfoAdminUserAccess(liAdminUserID, intWebInfoID)
                End If
            Next

        End If

        Return intWebInfoID
    End Function

#Region "Banner Image"
    Protected Sub lnkDeleteImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteImage.Click
        Dim intWebInfoID As Integer = Convert.ToInt32(Request.QueryString("pageID"))
        Dim bytesBannerImage() As Byte
        WebInfoDAL.UpdateWebInfo_BannerImage_ByID(intWebInfoID, String.Empty, bytesBannerImage)

        'As the Banner Image has been deleted we update our banner image
        SetupBannerImage(intWebInfoID)

    End Sub

    Protected Sub customValBannerImageSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add banner image if it exists, must have a file size less than 100kb, but we give them a 5kb buffer
        If RadUploadBannerImage.UploadedFiles.Count > 0 Then
            For Each file As UploadedFile In RadUploadBannerImage.UploadedFiles
                If file.InputStream.Length > 1124000 Then
                    'Invalid Submission - so set the IsValid flag to false and show the tab that caused the validation error
                    e.IsValid = False

                    rtsWebInfo.SelectedIndex = 0
                    rpvWebInfo.Selected = True
                End If
            Next
        End If
    End Sub

    Protected Sub SetupBannerImage(ByVal intWebInfoID As Integer)

        'First reset all banner image controls
        bannerImage.Visible = False
        lnkDeleteImage.Visible = False
        spanCurrentSectionBannerImageMessage.Visible = False

        Dim dtWebInfo_BannerImage As DataTable = WebInfoDAL.GetWebInfoBannerImage_ByID(intWebInfoID)
        If dtWebInfo_BannerImage.Rows.Count > 0 Then
            Dim drWebInfo_BannerImage As DataRow = dtWebInfo_BannerImage.Rows(0)

            'First check if we have a banner image for the current webinfo page, else check the Banner Image for the section
            Dim boolBannerImageFound As Boolean = False
            If (Not drWebInfo_BannerImage("BannerImage") Is DBNull.Value) AndAlso (drWebInfo_BannerImage("BannerImage").ToString().Length > 0) Then
                bannerImage.DataValue = drWebInfo_BannerImage("BannerImage")
                bannerImage.Visible = True
                lnkDeleteImage.Visible = True
            ElseIf (Not drWebInfo_BannerImage("BannerImage_ForSection") Is DBNull.Value) AndAlso (drWebInfo_BannerImage("BannerImage_ForSection").ToString().Length > 0) Then
                bannerImage.DataValue = drWebInfo_BannerImage("BannerImage_ForSection")
                bannerImage.Visible = True
                spanCurrentSectionBannerImageMessage.Visible = True
            End If
        End If

    End Sub

#End Region

#Region "Validation"
    Protected Sub WebPageName_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)

        Dim intWebInfoID As Integer = Integer.MinValue
        If Not Request.Params("pageID") Is Nothing Then
            intWebInfoID = Convert.ToInt32(Request.Params("pageID"))
        End If

        'Get the parentID if it exists, if it doesn't exist get the parentID From loading the current WebInfoID
        Dim intWebInfoID_Parent As Integer = Integer.MinValue
        If Not Request.Params("parentID") Is Nothing Then
            intWebInfoID_Parent = Convert.ToInt32(Request.Params("parentID"))
        ElseIf intWebInfoID > Integer.MinValue Then
            'If the parentID is does not exist in the query string but we have a webInfoID, then try and get the parent ID From the WebInfo Row, also get the boolSecureMember/boolSecureEducation (Need this for UPDATING a page)s
            Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByID(intWebInfoID)
            If dtWebInfo.Rows.Count > 0 Then
                Dim drWebInfo As DataRow = dtWebInfo.Rows(0)
                If Not drWebInfo("parentID") Is DBNull.Value Then
                    intWebInfoID_Parent = Convert.ToInt32(drWebInfo("parentID"))
                End If
                boolSecureMembers = Convert.ToBoolean(drWebInfo("secure_members"))
                boolSecureEducation = Convert.ToBoolean(drWebInfo("secure_education"))
            End If
        End If
        Dim strPageName As String = txtWebPageName.Text.Trim()

        'If we have a parentID, then try and get the page-name, and secure_members/secure_education (need this if we are ADDING a sub-page)
        Dim strPageNameParent As String = String.Empty
        If (intWebInfoID_Parent <> Integer.MinValue) AndAlso (intWebInfoID_Parent <> intWebInfoID_Header) AndAlso (intWebInfoID_Parent <> intWebInfoID_Footer) Then
            Dim dtWebInfoParent As DataTable = WebInfoDAL.GetWebInfo_ByID(intWebInfoID_Parent)
            If dtWebInfoParent.Rows.Count > 0 Then
                Dim drWebInfoParent As DataRow = dtWebInfoParent.Rows(0)
                strPageNameParent = drWebInfoParent("Name")
                boolSecureMembers = Convert.ToBoolean(drWebInfoParent("secure_members"))
                boolSecureEducation = Convert.ToBoolean(drWebInfoParent("secure_education"))
            End If

        End If

        Dim boolDoesWebPageAlreadyExist As Boolean = CommonWeb.DoesWebPageAlreadyExist(strPageName, strPageNameParent, intWebInfoID, intWebInfoID_Header, intWebInfoID_Footer, boolSecureMembers, boolSecureEducation, intSiteID)
        If boolDoesWebPageAlreadyExist Then
            cusValWebPageName.ErrorMessage = If(strPageNameParent.Length = 0, Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_WebpageName_SectionAlreadyExists, Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_WebpageName_SubPageAlreadyExists)
            e.IsValid = False
        End If
    End Sub
#End Region
End Class
