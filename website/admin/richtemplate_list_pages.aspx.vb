Imports System.Data.SqlClient
Imports System.Data
Imports Telerik.Web.UI
Imports System.Drawing

Partial Class admin_richtemplate_list_pages
    Inherits RichTemplateLanguagePage
    Dim strUserName As String = ""
    Dim intAllowedSiteDepth As Integer = 2
    Dim boolUseThreeColumnLayout As Boolean = False

    Dim boolSecureMembers As Boolean = False
    Dim boolSecureEducation As Boolean = False

    Dim boolAllowSectionAdd As Boolean = False
    Dim boolAllowPageAdd As Boolean = False
    Dim boolAllowSectionEdit As Boolean = False
    Dim boolAllowPageEdit As Boolean = False
    Dim boolAllowSectionDelete As Boolean = False
    Dim boolAllowPageDelete As Boolean = False
    Dim boolAllowSectionRename As Boolean = False
    Dim boolAllowPageRename As Boolean = False
    Dim boolAllowPublish As Boolean = False

    Dim listExpandedNodeID As New ArrayList
    Dim dtWebInfoPages As DataTable

    Dim intSiteID As Integer = Integer.MinValue
    Dim intWebInfoID_HomePage As Integer = Integer.MinValue
    Dim intWebInfoID_Header As Integer = Integer.MinValue
    Dim intWebInfoID_Footer As Integer = Integer.MinValue
    Dim boolWebpage_PublicSection_EnableGroupsAndUsers As Boolean = False
    Dim boolWebpage_MemberSection_EnableGroupsAndUsers As Boolean = False
    Dim listWebInfoAdminUserAccess As New List(Of Integer)
    Dim intAccessLevel As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Check thes user exists, and that they can view web content
        intSiteID = SiteDAL.GetCurrentSiteID_Admin()
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        Dim boolAllowWebContent As Boolean = AdminUserDAL.GetCurrentAdminUserAllowWebContent()
        If intAdminUserID > 0 AndAlso boolAllowWebContent Then

            'First get the number of sub levels allowed
            Dim dtSite As DataTable = SiteDAL.GetSite_ByID(intSiteID)
            If dtSite.Rows.Count > 0 Then
                Dim drSite As DataRow = dtSite.Rows(0)
                intAllowedSiteDepth = Convert.ToInt32(drSite("SiteDepth"))
                boolUseThreeColumnLayout = Convert.ToBoolean(drSite("UseThreeColumnLayout"))

                boolWebpage_PublicSection_EnableGroupsAndUsers = Convert.ToBoolean(drSite("Webpage_PublicSection_EnableGroupsAndUsers"))
                boolWebpage_MemberSection_EnableGroupsAndUsers = Convert.ToBoolean(drSite("Webpage_MemberSection_EnableGroupsAndUsers"))

                intWebInfoID_HomePage = Convert.ToInt32(drSite("WebInfoID_HomePage"))
                intWebInfoID_Header = Convert.ToInt32(drSite("WebInfoID_Header"))
                intWebInfoID_Footer = Convert.ToInt32(drSite("WebInfoID_Footer"))
            End If

            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
            If dtAdminUser.Rows.Count > 0 Then
                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                intAccessLevel = AdminUserDAL.GetCurrentAdminUserAccessLevel()

                'Get all the WebInfo pages this AdminUser can use, but only if they are not already a SUPER ADMIN
                '' When checking access if the user is not a super admin we check this list of WebInfo Access

                If intAccessLevel <= 2 Then
                    Dim dtWebInfoAdminUserAccess = WebInfoDAL.GetWebInfoAdminUserAccessList_ByUserID(intAdminUserID)
                    For Each drWebInfoAdminUserAccess As DataRow In dtWebInfoAdminUserAccess.Rows
                        listWebInfoAdminUserAccess.Add(drWebInfoAdminUserAccess("WebInfoID"))
                    Next
                End If

                'Get admin user information
                Dim boolUseSiteLevelAccess As Boolean = Convert.ToBoolean(drAdminUser("UseSiteLevelAccess"))
                Dim drSiteAccess_AdminUser As DataRow = Nothing
                If boolUseSiteLevelAccess Then
                    Dim dtSiteAccess_AdminUser As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID_Active(intSiteID, intAdminUserID)
                    If dtSiteAccess_AdminUser.Rows.Count > 0 Then
                        drSiteAccess_AdminUser = dtSiteAccess_AdminUser.Rows(0)
                    End If
                End If

                boolAllowSectionAdd = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Section_Add")), Convert.ToBoolean(drAdminUser("Allow_Section_Add_AllSites")))
                boolAllowPageAdd = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Page_Add")), Convert.ToBoolean(drAdminUser("Allow_Page_Add_AllSites")))
                boolAllowSectionEdit = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Section_Edit")), Convert.ToBoolean(drAdminUser("Allow_Section_Edit_AllSites")))
                boolAllowPageEdit = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Page_Edit")), Convert.ToBoolean(drAdminUser("Allow_Page_Edit_AllSites")))
                boolAllowSectionDelete = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Section_Delete")), Convert.ToBoolean(drAdminUser("Allow_Section_Delete_AllSites")))
                boolAllowPageDelete = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Page_Delete")), Convert.ToBoolean(drAdminUser("Allow_Page_Delete_AllSites")))
                boolAllowSectionRename = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Section_Rename")), Convert.ToBoolean(drAdminUser("Allow_Section_Rename_AllSites")))
                boolAllowPageRename = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Page_Rename")), Convert.ToBoolean(drAdminUser("Allow_Page_Rename_AllSites")))
                boolAllowPublish = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Publish")), Convert.ToBoolean(drAdminUser("Allow_Publish_AllSites")))

                'Ensure this sectionID corresponds to this Site
                'get current section from request string
                Dim intSectionID As Integer = Request.QueryString("sectionID")
                If intSectionID = 0 Then
                    'Then prepare this page for Adding a new section, only if this adminuser has access to this section
                    'Only SUper Admin can add a section
                    If intAccessLevel > 2 Then

                        'We find-out if we are adding a Members/Education section by looking at the request string
                        If Not Request.QueryString("secure_members") Is Nothing Then
                            If Request.QueryString("secure_members").ToLower() = "yes" Then
                                boolSecureMembers = True
                            End If
                        ElseIf Not Request.QueryString("secure_education") Is Nothing Then
                            If Request.QueryString("secure_education").ToLower() = "yes" Then
                                boolSecureEducation = True
                            End If
                        End If

                        'Set our Section name as New Section, and hide the Main divListPages, so we get a blank canvas to show the popup on.
                        lit_SectionName.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_SectionName_NewSection
                        divListPages.Visible = False

                        'Register our javascript for inside our update panel
                        Dim strDropDownSelectPageScript As String = GetDropDownSelectPageScript()
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "dynamic scripts", strDropDownSelectPageScript, True)

                        'Also set our modal popup to automatically adjust its size when the loaded
                        CommonWeb.GeneratePopupResizeJsScript(Me.Page, New String() {"div_ScrollerEditAddPage"}, New Integer() {800}, New Integer() {80}, False)

                        If Not IsPostBack Then
                            SetupEditAddPage(Integer.MinValue, Integer.MinValue)
                        End If

                    Else
                        'Load the welcome screen
                        Response.Redirect("/admin/richtemplate_welcome.aspx?mode=forms")
                    End If

                Else
                    'Otherwise this page will contain the tree of pages for this section
                    Dim dtSection As DataTable = WebInfoDAL.GetWebInfo_ByIDAndSiteID(intSectionID, intSiteID)
                    If dtSection.Rows.Count > 0 Then
                        Dim drSection As DataRow = dtSection.Rows(0)
                        Dim strWebInfoName As String = String.Empty
                        If intSectionID = intWebInfoID_HomePage Then
                            'If we are dealing with the HomePage section, we use the resource language file to get the HomePage text, and also set the intAllowedSiteDepth to 3, so we can add pages to the Header Page and FOOTER Page
                            strWebInfoName = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_HomePage
                            intAllowedSiteDepth = 3
                        Else
                            strWebInfoName = drSection("Name")
                        End If

                        'As we have a section, we use this base page to check if we are add/updating a member/education page
                        boolSecureMembers = drSection("secure_members")
                        boolSecureEducation = drSection("secure_education")

                        lit_SectionName.Text = strWebInfoName


                        RadTreePages.EnableDragAndDrop = boolAllowPublish

                        strUserName = drAdminUser("Username").ToString()

                        'Only give the ability to check in all pages to users with access level greater than 2
                        If intAccessLevel > 2 Then
                            lnkCheckInAll.Visible = True
                        End If

                        'Set the header
                        ucHeader.PageName = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Heading & " " & strWebInfoName
                        ucHeader.PageHelpID = 1

                        'Load our RadTree of Pages
                        RadTreePages_BindRadTree()

                        'Register our javascript for inside our update panel
                        Dim strHighlightScript As String = GetHighlightScript()
                        Dim strDropDownSelectPageScript As String = GetDropDownSelectPageScript()
                        Dim strCollapsibleTreeScript As String = GetCollapsibleTreeScript()

                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "dynamic scripts", strHighlightScript & " " & strDropDownSelectPageScript & " " & strCollapsibleTreeScript, True)

                        'Also set our modal popup to automatically adjust its size when the loaded
                        CommonWeb.GeneratePopupResizeJsScript(Me.Page, New String() {"div_ScrollerEditAddPage"}, New Integer() {800}, New Integer() {80}, False)
                    Else
                        Response.Redirect("~/richadmin/")
                    End If
                End If
            Else
                Response.Redirect("~/richadmin/")
            End If
        Else
            Response.Redirect("~/richadmin/")
        End If

    End Sub

    Private Function GetHighlightScript() As String
        Dim strHighlightScript As String = ""
        strHighlightScript = strHighlightScript & _
        "$( function()" & _
        "{ " & _
        "  $('.tblWebInfos tr').hover(" & _
        "   function() " & _
        "   {" & _
        "    $(this).addClass('highlight');" & _
        "   }," & _
        "   function()" & _
        "  {" & _
        "   $(this).removeClass('highlight');" & _
        "  }" & _
        " )" & _
        " }" & _
        ")"
        Return strHighlightScript
    End Function

    Private Function GetDropDownSelectPageScript() As String
        Dim strDropDownSelectPageScript As String = Environment.NewLine & _
"$(document).ready(function()" & Environment.NewLine & _
"{" & Environment.NewLine & _
    "$('.ddlSelectPage').change(function() {" & Environment.NewLine & _
    " var strSelectedValue = $('.ddlSelectPage :selected').val();" & Environment.NewLine & _
    " $('.lblLinkURL_InsidePage').text(strSelectedValue);" & Environment.NewLine & _
    "});" & Environment.NewLine & _
"});"

        Return strDropDownSelectPageScript
    End Function

    Private Function GetCollapsibleTreeScript() As String
        Dim strCollapsibleTreeScript As String = Environment.NewLine & _
        "$(document).ready(function()" & Environment.NewLine & _
        "{" & Environment.NewLine & _
        "   var rtPlus = $('span.rtPlus');" & Environment.NewLine & _
        "   $(rtPlus).each(function(index) {" & Environment.NewLine & _
        "       var parentDIV = $(this).parent();" & Environment.NewLine & _
        "       var spacerDIV = $(parentDIV).find('div.spacer')[0];" & Environment.NewLine & _
        "       var spacerDIVwidth = $(spacerDIV).css('width').replace('px','');" & Environment.NewLine & _
        "       var newCollapseImgLeft = (parseInt(spacerDIVwidth) + 5) + 'px';" & Environment.NewLine & _
        "       $(this).css('left', newCollapseImgLeft);" & Environment.NewLine & _
        "   });" & Environment.NewLine & _
        "   var rtMinus = $('span.rtMinus');" & Environment.NewLine & _
        "   $(rtMinus).each(function(index) {" & Environment.NewLine & _
        "       var parentDIV = $(this).parent();" & Environment.NewLine & _
        "       var spacerDIV = $(parentDIV).find('div.spacer')[0];" & Environment.NewLine & _
        "       var spacerDIVwidth = $(spacerDIV).css('width').replace('px','');" & Environment.NewLine & _
        "       var newCollapseImgLeft = (parseInt(spacerDIVwidth) + 5) + 'px';" & Environment.NewLine & _
        "       $(this).css('left', newCollapseImgLeft);" & Environment.NewLine & _
        "   });" & Environment.NewLine & _
        "});"
        Return strCollapsibleTreeScript
    End Function


#Region "Rad Tree DataBound Properties"

    Protected Sub PopulateWebpageDataTable()
        'Get the section id and the pages for this sectinoID
        Dim intSectionID As Integer = Request.QueryString("sectionID")

        'Load section specific information (links and tree)
        dtWebInfoPages = WebInfoDAL.GetWebInfoList_BySectionID(intSectionID)
        dtWebInfoPages.PrimaryKey = New DataColumn() {dtWebInfoPages.Columns("ID")}
    End Sub

    Protected Sub RadTreePages_BindRadTree()
        'First go through all nodes and check if they are expanded
        'If a node is expanded add it to the hash table
        For Each rtn As RadTreeNode In RadTreePages.GetAllNodes()
            If rtn.Expanded Then
                listExpandedNodeID.Add(Convert.ToInt32(rtn.Value))
            End If
        Next

        PopulateWebpageDataTable()
        RadTreePages.DataSource = dtWebInfoPages
        RadTreePages.DataBind()

    End Sub

    Protected Sub RadTreePages_NodeDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles RadTreePages.NodeDataBound

        Dim lit_Spacer As Literal = e.Node.FindControl("lit_Spacer")
        Dim img_WebpageIcon As HtmlImage = e.Node.FindControl("img_WebpageIcon")
        Dim lit_WebpageName As Literal = e.Node.FindControl("lit_WebpageName")

        Dim drvWebInfo As DataRowView = e.Node.DataItem
        Dim drWebInfo As DataRow = drvWebInfo.Row

        Dim intWebInfoID As Integer = drWebInfo("id")
        Dim intWebInfoSectionID As Integer = drWebInfo("sectionID")

        Dim intWebInfoID_Parent As Integer = Integer.MinValue
        If Not drWebInfo("ParentID") Is DBNull.Value Then
            intWebInfoID_Parent = Convert.ToInt32(drWebInfo("ParentID"))
        End If

        'Use the page level to determine if this is the 'root page' and for the width of the page spacer
        Dim intWebInfo_PageLevel As Integer = Convert.ToInt32(drWebInfo("PageLevel"))

        'Get any checked-in information
        Dim strCheckedID As String = ""
        If Not drWebInfo("Checked_ID") Is DBNull.Value Then
            strCheckedID = drWebInfo("Checked_ID").ToString()
        End If
        Dim boolCheckedOut As Boolean = Convert.ToBoolean(drWebInfo("Checked_Out"))

        'Get Needed Boolean information
        Dim boolIsHomePage As Boolean = (intWebInfoID = intWebInfoID_HomePage)
        Dim boolIsHeaderPage As Boolean = (intWebInfoID = intWebInfoID_Header)
        Dim boolIsFooterPage As Boolean = (intWebInfoID = intWebInfoID_Footer)
        Dim boolIsSection As Boolean = (intWebInfoID = intWebInfoSectionID)

        Dim boolIsParent_Header As Boolean = (intWebInfoID_Parent = intWebInfoID_Header)
        Dim boolIsParent_Footer As Boolean = (intWebInfoID_Parent = intWebInfoID_Footer)

        Dim boolHasWebInfoAdminUserAccess As Boolean = (intAccessLevel > 2 Or listWebInfoAdminUserAccess.Contains(intWebInfoID))

        'If we are dealing with a root page we set the image source accordingly
        If intWebInfo_PageLevel = 1 Then
            If boolIsHomePage Then
                img_WebpageIcon.Src = "/admin/images/icon_homepage_small.png"
            Else
                img_WebpageIcon.Src = "/admin/images/icon_root_page.gif"
            End If
        Else
            If boolIsHeaderPage Or boolIsFooterPage Then
                img_WebpageIcon.Src = "/admin/images/icon_folder.png"
            Else
                img_WebpageIcon.Src = "/admin/images/icon_subpage.gif"
            End If
        End If

        'Create the spacer gap based on the pages 'PageLevel' (NOTE no spacer for the first node)
        lit_Spacer.Text = "<div class='spacer' style='width:" + Convert.ToString(intWebInfo_PageLevel * 15) + "px;'>&nbsp;</div>"

        'Node Text/Webpage name style adjusts depending on if the webpage is pending, link-only or the root node
        Dim strWebInfoName As String = String.Empty
        If (Not boolIsHomePage) AndAlso (Not boolIsHeaderPage) AndAlso (Not boolIsFooterPage) Then
            strWebInfoName = drWebInfo("Name").ToString()
        ElseIf boolIsHomePage Then
            strWebInfoName = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_HomePage
        ElseIf boolIsHeaderPage Then
            strWebInfoName = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_HeaderFolder
        ElseIf boolIsFooterPage Then
            strWebInfoName = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_FooterFolder
        End If

        img_WebpageIcon.Alt = strWebInfoName

        Dim boolPending As Boolean = Convert.ToBoolean(drWebInfo("Pending"))
        'Note sections can never be 'LinkOnly' but here just in-case of future customization
        Dim boolLinkOnly As Boolean = Convert.ToBoolean(drWebInfo("LinkOnly"))

        Dim strRtnCategory As String = ""
        If intWebInfo_PageLevel = 1 And boolIsHomePage Then
            lit_WebpageName.Text = "<span class='root_page_text'>" & Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_PageIsRoot & " (" + strWebInfoName + ")</span>"
            strRtnCategory = "Root"
        ElseIf boolPending Then
            lit_WebpageName.Text = "<span class='sub_page_text'>" + strWebInfoName + " - (" & Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_PageIsOffline & ")</span>"
            strRtnCategory = "Pending"
        ElseIf boolLinkOnly Then
            lit_WebpageName.Text = "<span class='sub_page_text'>" + strWebInfoName + " - (" & Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_PageIsLinkOnly & ")</span>"
            strRtnCategory = "LinkOnly"
        Else
            lit_WebpageName.Text = "<span class='sub_page_text'>" + strWebInfoName + "</span>"
            strRtnCategory = "Subpage"
        End If

        'find the parent node and set this node's parent node to the found node
        e.Node.Category = strRtnCategory

        If listExpandedNodeID.Contains(intWebInfoID) Then
            e.Node.Expanded = True
        Else
            e.Node.Expanded = False
        End If

        'Only AdminUsers can with 'Publish' Permission can drag'n drop pages And now must be a super admin
        If (Not boolIsHomePage) AndAlso (Not boolIsHeaderPage) AndAlso (Not boolIsFooterPage) AndAlso boolAllowPublish AndAlso intAccessLevel > 2 Then
            e.Node.AllowDrag = True
            e.Node.AllowDrop = True
        Else
            e.Node.AllowDrag = False
            e.Node.AllowDrop = False
        End If


        'Setup righthand columns

        'Archive COLUMN
        'First we only do this column if the AdminUser is Allowed to Publish Pages
        If boolAllowPublish Then
            ' Only Admin Users can Drag 'n Drop
            If intAccessLevel > 2 Then
                'First show the divDragAndDropTreeNodeColumn div
                Dim divDragAndDropTreeNodeColumnEnabled As HtmlGenericControl = e.Node.FindControl("divDragAndDropTreeNodeColumnEnabled")
                divDragAndDropTreeNodeColumnEnabled.Visible = True
            Else
                'Admin User is NOT allowed to publish pages, so we hide and show DISABLED Divs
                Dim divDragAndDropTreeNodeColumnDisabled As HtmlGenericControl = e.Node.FindControl("divDragAndDropTreeNodeColumnDisabled")
                divDragAndDropTreeNodeColumnDisabled.Visible = True
            End If


            'Admin User is allowed to publish pages, so we hide and show ENABLED Divs
            If (Not boolIsHomePage) AndAlso (Not boolIsHeaderPage) AndAlso (Not boolIsFooterPage) AndAlso (boolHasWebInfoAdminUserAccess) Then
                If boolPending Then

                    'You can only make a page LIVE if we have a live version of this page
                    If (boolLinkOnly) Or (Not drWebInfo("Message") Is DBNull.Value) Then
                        Dim divMakeLiveEnabled As HtmlGenericControl = e.Node.FindControl("divMakeLiveEnabled")
                        Dim lnkMakeLive As LinkButton = e.Node.FindControl("lnkMakeLive")

                        divMakeLiveEnabled.Visible = True
                        lnkMakeLive.CommandArgument = intWebInfoID.ToString()
                    Else
                        Dim divMakeLiveDisabled As HtmlGenericControl = e.Node.FindControl("divMakeLiveDisabled")
                        divMakeLiveDisabled.Visible = True
                    End If

                Else

                    Dim divTakeOfflineEnabled As HtmlGenericControl = e.Node.FindControl("divTakeOfflineEnabled")
                    Dim lnkTakeOffline As LinkButton = e.Node.FindControl("lnkTakeOffline")

                    divTakeOfflineEnabled.Visible = True
                    lnkTakeOffline.CommandArgument = intWebInfoID.ToString()
                End If
            End If
        Else
            'Admin User is NOT allowed to publish pages, so we hide and show DISABLED Divs
            Dim divDragAndDropTreeNodeColumnDisabled As HtmlGenericControl = e.Node.FindControl("divDragAndDropTreeNodeColumnDisabled")
            divDragAndDropTreeNodeColumnDisabled.Visible = True

            If (Not boolIsHomePage) AndAlso (Not boolIsHeaderPage) AndAlso (Not boolIsFooterPage) Then
                If boolPending Then
                    'Only subpages can be taken offline or made live
                    If intWebInfo_PageLevel > 1 Then
                        Dim divMakeLiveDisabled As HtmlGenericControl = e.Node.FindControl("divMakeLiveDisabled")
                        divMakeLiveDisabled.Visible = True
                    End If
                Else
                    'Only subpages can be taken offline or made live
                    If intWebInfo_PageLevel > 1 Then
                        Dim divTakeOfflineDisabled As HtmlGenericControl = e.Node.FindControl("divTakeOfflineDisabled")
                        divTakeOfflineDisabled.Visible = True
                    End If
                End If
            End If
        End If

        If (Not boolIsHeaderPage) AndAlso (Not boolIsFooterPage) Then

            'To Edit a LIVE Page, you must first have edit section or edit page permissions, and the AdminUser Must also beable to PUBLISH a Page Live, and a live version must also exist
            ''  They must also have WebInfo AdminUserAccess to this page
            If (Not boolLinkOnly) AndAlso boolAllowPublish AndAlso (Not drWebInfo("Message") Is DBNull.Value) AndAlso ((boolIsSection AndAlso boolAllowSectionEdit) Or ((Not boolIsSection) AndAlso boolAllowPageEdit)) AndAlso (boolHasWebInfoAdminUserAccess) Then
                'Now check if the page is checked out
                If boolCheckedOut And strCheckedID.Length > 0 Then
                    Dim divEditLiveDisabled As HtmlGenericControl = e.Node.FindControl("divEditLiveDisabled")
                    divEditLiveDisabled.Visible = True
                Else
                    Dim divEditLiveEnabled As HtmlGenericControl = e.Node.FindControl("divEditLiveEnabled")
                    divEditLiveEnabled.Visible = True
                End If
            ElseIf boolLinkOnly And boolAllowPublish AndAlso ((boolIsSection AndAlso boolAllowSectionEdit) Or ((Not boolIsSection) AndAlso boolAllowPageEdit)) AndAlso (boolHasWebInfoAdminUserAccess) Then
                'If the web info is a LINK then we can show the'edit link' link button, so its columns and location can be editted
                Dim divEditLiveEnabled_Link As HtmlGenericControl = e.Node.FindControl("divEditLiveEnabled_Link")
                divEditLiveEnabled_Link.Visible = True
            Else
                If boolLinkOnly Then
                    'Then they don't have permissions to edit this 'link'
                    Dim divEditLiveDisabled_Link As HtmlGenericControl = e.Node.FindControl("divEditLiveDisabled_Link")
                    divEditLiveDisabled_Link.Visible = True
                Else
                    'Then they don't have permissions to edit this 'page'
                    Dim divEditLiveDisabled As HtmlGenericControl = e.Node.FindControl("divEditLiveDisabled")
                    divEditLiveDisabled.Visible = True
                End If

            End If

            'To Edit a DRAFT Page, you must first have edit section or edit page permissions
            '' And now they must have WebInfo AdminUserAccess
            If (Not boolLinkOnly) AndAlso ((boolIsSection AndAlso boolAllowSectionEdit) Or ((Not boolIsSection) AndAlso boolAllowPageEdit)) AndAlso (boolHasWebInfoAdminUserAccess) Then
                'Now check if the page is checked out
                If boolCheckedOut And strCheckedID.Length > 0 Then
                    Dim divEditDraftDisabled As HtmlGenericControl = e.Node.FindControl("divEditDraftDisabled")
                    divEditDraftDisabled.Visible = True
                Else
                    Dim divEditDraftEnabled As HtmlGenericControl = e.Node.FindControl("divEditDraftEnabled")
                    divEditDraftEnabled.Visible = True
                End If
            ElseIf (Not boolLinkOnly) Then

                Dim divEditDraftDisabled As HtmlGenericControl = e.Node.FindControl("divEditDraftDisabled")
                divEditDraftDisabled.Visible = True
            Else
                'We do not allow editing of Link-Only Pages as they only contain 1 version the LIVE VERSION, If its link only we do not show anything in this column
            End If

            'Checked Out Column
            If boolCheckedOut And strCheckedID.Length > 0 Then
                ''Now the admin or anyone with access to this page can ALSO CHECK IN a file
                If strCheckedID = strUserName Or boolHasWebInfoAdminUserAccess Then
                    Dim lnkCheckIn As LinkButton = e.Node.FindControl("lnkCheckIn")
                    lnkCheckIn.Visible = True
                    lnkCheckIn.CommandArgument = intWebInfoID.ToString()
                Else
                    Dim divCheckOut_DifferentUser As HtmlGenericControl = e.Node.FindControl("divCheckOut_DifferentUser")
                    divCheckOut_DifferentUser.Visible = True
                End If
            Else
                Dim divCheckIn As HtmlGenericControl = e.Node.FindControl("divCheckIn")
                divCheckIn.Visible = True
            End If

            'Checked out author column
            If boolCheckedOut And strCheckedID.Length > 0 Then
                Dim lit_CheckedOutAuthor As Literal = e.Node.FindControl("lit_CheckedOutAuthor")
                lit_CheckedOutAuthor.Text = strCheckedID
                lit_CheckedOutAuthor.Visible = True
            End If
        End If

        'Before we didn't allow pages to be created under a link only page, now we do, however we do not allow sub pages under the footer page only header page if its not a link
        'If (Not boolIsHomePage) And (Not boolLinkOnly) Then
        If (Not boolIsHomePage) AndAlso (Not boolIsParent_Footer) Then

            'Add Column but only if the new subpage has a page level within out, and the AdminUser has add page permissions
            Dim intNewPageLevel As Integer = intWebInfo_PageLevel + 1

            'Now if we are adding a page to a header, we must increase the allowed site depth byby 2, as the Home page level and Header level do not cound
            ''NOW only a SUPER ADMIN can add sub pages
            If (intNewPageLevel <= intAllowedSiteDepth Or (boolIsParent_Header AndAlso (Not boolLinkOnly) AndAlso intNewPageLevel <= (intAllowedSiteDepth + 2))) AndAlso boolAllowPageAdd AndAlso intAccessLevel > 2 Then
                Dim divAddSubPageEnabled As HtmlGenericControl = e.Node.FindControl("divAddSubPageEnabled")
                divAddSubPageEnabled.Visible = True

                'Get the lnkAddPage and set its command argument
                Dim lnkAddPage As LinkButton = e.Node.FindControl("lnkAddPage")
                lnkAddPage.CommandArgument = intWebInfoID
            Else
                Dim divAddSubPageDisabled As HtmlGenericControl = e.Node.FindControl("divAddSubPageDisabled")
                divAddSubPageDisabled.Visible = True
            End If
        End If

        If (Not boolIsHomePage) AndAlso (Not boolIsHeaderPage) AndAlso (Not boolIsFooterPage) Then
            'Delete Column only if the AdminUser has delete a section or delete a page permission
            If boolIsSection Then
                'If the current page is a section
                '' and the current AdminUser is a SUPER USER
                If boolAllowSectionDelete AndAlso intAccessLevel > 2 Then
                    'Now check if the page is checked out
                    If boolCheckedOut And strCheckedID.Length > 0 Then
                        ' The Adminuser does not have access to delete this page as it is currently CHECKED-OUT
                        Dim divDeleteDisabled As HtmlGenericControl = e.Node.FindControl("divDeleteDisabled")
                        divDeleteDisabled.Visible = True

                    Else
                        ' If the admin user has access to delete a section
                        Dim divDeleteEnabled_Section As HtmlGenericControl = e.Node.FindControl("divDeleteEnabled_Section")
                        divDeleteEnabled_Section.Visible = True

                        Dim lnkDeleteSection As LinkButton = e.Node.FindControl("lnkDeleteSection")
                        lnkDeleteSection.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_DeleteSectionLink
                        lnkDeleteSection.OnClientClick = "return confirm('" & Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_DeleteSection_PopupMessage & "');"
                        lnkDeleteSection.CommandArgument = intWebInfoID.ToString()
                    End If

                Else
                    ' Else the admin user does not have access to delete a section
                    Dim divDeleteDisabled As HtmlGenericControl = e.Node.FindControl("divDeleteDisabled")
                    divDeleteDisabled.Visible = True
                End If
            Else
                'The current page is not a section
                '' Also check the User is a SUPER ADMIN as they can only delete pages now
                If boolAllowPageDelete AndAlso intAccessLevel > 2 Then
                    'Now check if the page is checked out
                    If boolCheckedOut And strCheckedID.Length > 0 Then
                        ' An admin user does not have access to delete the page as the page is currently CHECKED-OUT
                        Dim divDeleteDisabled As HtmlGenericControl = e.Node.FindControl("divDeleteDisabled")
                        divDeleteDisabled.Visible = True
                    Else
                        ' If the admin user has access to delete a page
                        Dim divDeleteEnabled_Page As HtmlGenericControl = e.Node.FindControl("divDeleteEnabled_Page")
                        divDeleteEnabled_Page.Visible = True

                        Dim lnkDeletePage As LinkButton = e.Node.FindControl("lnkDeletePage")
                        lnkDeletePage.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_DeletePageLink
                        lnkDeletePage.OnClientClick = "return confirm('" & Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_DeletePage_PopupMessage & "');"
                        lnkDeletePage.CommandArgument = intWebInfoID.ToString()

                    End If

                Else
                    ' Else the admin user does not have access to delete a page
                    Dim divDeleteDisabled As HtmlGenericControl = e.Node.FindControl("divDeleteDisabled")
                    divDeleteDisabled.Visible = True
                End If
            End If
        End If

        'Now Check if we should have any webpage intially selected
        If Not IsPostBack Then
            If Not Request.Params("wi") Is Nothing Then
                If intWebInfoID.ToString() = Request.Params("wi").ToString() Then
                    'Select this node, then set all its parent nodes to be expanded
                    e.Node.Selected = True
                    e.Node.Expanded = True

                    Dim rtnParent As RadTreeNode = e.Node.ParentNode
                    While Not rtnParent Is Nothing
                        rtnParent.Expanded = True
                        rtnParent = rtnParent.ParentNode
                    End While
                End If
            End If
        End If
    End Sub

#End Region

    Protected Sub SetupEditAddPage(ByVal intWebInfoID As Integer, ByVal intWebInfoID_Parent As Integer)

        'contruct our Page Editor URL
        Dim strPageEditorUrl As String = "/admin/richtemplate_page_editor.aspx"
        Dim strPageEditorUrl_QueryString As String = ""
        If intWebInfoID_Parent > Integer.MinValue Then
            strPageEditorUrl_QueryString = If(strPageEditorUrl_QueryString.Length = 0, "?parentID=" & intWebInfoID_Parent.ToString(), "&parentID=" & intWebInfoID_Parent.ToString())
        End If
        If boolSecureMembers Then
            strPageEditorUrl_QueryString = If(strPageEditorUrl_QueryString.Length = 0, "?secure_members=yes", "&secure_members=yes")
        ElseIf boolSecureEducation Then
            strPageEditorUrl_QueryString = If(strPageEditorUrl_QueryString.Length = 0, "?secure_education=yes", "&secure_education=yes")
        End If

        lnkPageEditor.HRef = strPageEditorUrl & strPageEditorUrl_QueryString

        'Setup the internal webpages
        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfoList(intSiteID)

        'reset the target drop down lists, txtName and txtURL and buttons
        ddlTargetFrame_OutsidePage.SelectedIndex = 0
        ddlTargetFrame_InsidePage.SelectedIndex = 0
        txtLinkName.Text = ""
        txtLinkURL_OutsidePage.Text = ""

        'Clear the items in this dropdown
        lblLinkURL_InsidePage.Text = ""
        ddlSelectPage.Items.Clear()
        'Insert the initial item
        ddlSelectPage.Items.Add(New ListItem("--Choose a Page--", ""))
        'Add the pages to this drop down - ONLY HANDLE WEBPAGES, not HomePage, and not Header/Footer Pages (these are done in the next loop)
        For Each drWebInfo As DataRow In dtWebInfo.Rows

            'First check if this page is a link, rather than a content page, if so do not add this
            Dim boolLinkOnly As Boolean = Convert.ToBoolean(drWebInfo("LinkOnly"))
            Dim boolPending As Boolean = Convert.ToBoolean(drWebInfo("Pending"))
            If (Not boolLinkOnly) AndAlso (Not boolPending) Then
                Dim intWebInfoID_Current As Integer = Convert.ToInt32(drWebInfo("ID"))

                Dim intWebInfoID_Parent_Current As Integer = Integer.MinValue
                If Not drWebInfo("ParentID") Is DBNull.Value Then
                    intWebInfoID_Parent_Current = Convert.ToInt32(drWebInfo("ParentID"))
                End If

                'dont include the homepage, and we don't include the header/footer container, we also handle header and footer pages at the end
                If intWebInfoID_Current <> intWebInfoID_HomePage AndAlso intWebInfoID_Current <> intWebInfoID_Header AndAlso intWebInfoID_Parent_Current <> intWebInfoID_Header AndAlso intWebInfoID_Current <> intWebInfoID_Footer AndAlso intWebInfoID_Parent_Current <> intWebInfoID_Footer Then
                    Dim strWebInfoName As String = drWebInfo("Name")
                    Dim strWebInfoName_Parent As String = ""
                    If Not drWebInfo("Parentname") Is DBNull.Value AndAlso intWebInfoID_Parent_Current <> intWebInfoID_Header AndAlso intWebInfoID_Parent_Current <> intWebInfoID_Footer Then
                        strWebInfoName_Parent = drWebInfo("ParentName").ToString()
                    End If

                    Dim boolSecureMembers_CurrentPage As Boolean = Convert.ToBoolean(drWebInfo("secure_members"))
                    Dim boolSecureEducation_CurrentPage As Boolean = Convert.ToBoolean(drWebInfo("secure_education"))

                    Dim strPageUrl As String = WebInfoDAL.GetWebInfoUrl(strWebInfoName, strWebInfoName_Parent, If(boolSecureMembers_CurrentPage, "Member", If(boolSecureEducation_CurrentPage, "Education", String.Empty)))

                    Dim intPageLevel As Integer = drWebInfo("PageLevel")
                    While intPageLevel > 1
                        strWebInfoName = "-- " & strWebInfoName
                        intPageLevel = intPageLevel - 1
                    End While



                    ddlSelectPage.Items.Add(New ListItem(strWebInfoName, strPageUrl))
                End If
            End If

        Next

        Dim dtWebInfo_Header As DataTable = WebInfoDAL.GetWebInfoList_ByParentID(intWebInfoID_Header)
        'Now we add the header pages
        For Each drWebInfo_Header As DataRow In dtWebInfo_Header.Rows

            'First check if this page is a link, rather than a content page, if so do not add this
            Dim boolLinkOnly As Boolean = Convert.ToBoolean(drWebInfo_Header("LinkOnly"))
            Dim boolPending As Boolean = Convert.ToBoolean(drWebInfo_Header("Pending"))
            If (Not boolLinkOnly) AndAlso (Not boolPending) Then
                Dim intWebInfoID_Current As Integer = Convert.ToInt32(drWebInfo_Header("ID"))

                Dim strWebInfoName As String = drWebInfo_Header("Name")
                Dim strWebInfoName_Parent As String = "" 'we ensure the header is at the root of the site

                Dim strPageUrl As String = WebInfoDAL.GetWebInfoUrl(strWebInfoName, strWebInfoName_Parent, String.Empty)

                ddlSelectPage.Items.Add(New ListItem(strWebInfoName, strPageUrl))
            End If
        Next

        Dim dtWebInfo_Footer As DataTable = WebInfoDAL.GetWebInfoList_ByParentID(intWebInfoID_Footer)
        'Now we add the footer pages
        For Each drWebInfo_Footer As DataRow In dtWebInfo_Footer.Rows

            'First check if this page is a link, rather than a content page, if so do not add this
            Dim boolLinkOnly As Boolean = Convert.ToBoolean(drWebInfo_Footer("LinkOnly"))
            Dim boolPending As Boolean = Convert.ToBoolean(drWebInfo_Footer("Pending"))
            If (Not boolLinkOnly) AndAlso (Not boolPending) Then
                Dim intWebInfoID_Current As Integer = Convert.ToInt32(drWebInfo_Footer("ID"))

                Dim strWebInfoName As String = drWebInfo_Footer("Name")
                Dim strWebInfoName_Parent As String = "" 'we ensure the footer is at the root of the site

                Dim strPageUrl As String = WebInfoDAL.GetWebInfoUrl(strWebInfoName, strWebInfoName_Parent, String.Empty)

                ddlSelectPage.Items.Add(New ListItem(strWebInfoName, strPageUrl))
            End If
        Next



        'setup both the rdNavigationLayout for and outside page and also for an inside page
        'get the webinfo for this parent web info id, if the parents page level is 1, then this page level must be 2, so show the navigation dropdown lists
        rdNavigationLayout_OutsidePage.SelectedValue = "1"
        rdNavigationLayout_InsidePage.SelectedValue = "1"

        If intWebInfoID_Parent > Integer.MinValue Then
            Dim dtWebInfoParent As DataTable = WebInfoDAL.GetWebInfo_ByID(intWebInfoID_Parent)
            If dtWebInfoParent.Rows.Count > 0 Then
                Dim drWebInfoParent As DataRow = dtWebInfoParent.Rows(0)

                Dim intWebInfoParent_PageLevel As Integer = Convert.ToInt32(drWebInfoParent("PageLevel"))
                If boolUseThreeColumnLayout And intWebInfoParent_PageLevel = 1 Then
                    trNavigationLayout_OutsidePage.Visible = True
                    trNavigationLayout_InsidePage.Visible = True
                Else
                    trNavigationLayout_OutsidePage.Visible = False
                    trNavigationLayout_InsidePage.Visible = False
                End If
            End If
        End If

        'Populate data for STEP ONE of this popup
        divStepOne.Visible = True
        divStepTwo.Visible = False

        If intWebInfoID > Integer.MinValue Then
            'Show update buttons and since you can only use this panel for updating link-only we hide the wysiwyg link and prepopulate the outside link/inside link
            divCreatePageWYSIWYG.Visible = False
            btnCreatePage_OutsidePage.Visible = False
            btnUpdatePage_OutsidePage.Visible = True
            btnCreatePage_InsidePage.Visible = False
            btnUpdatePage_InsidePage.Visible = True

            Dim dtWebInfo_Current As DataTable = WebInfoDAL.GetWebInfo_ByID(intWebInfoID)
            If dtWebInfo_Current.Rows.Count > 0 Then
                Dim drWebInfo_Current As DataRow = dtWebInfo_Current.Rows(0)

                intWebInfoID_Parent = If(drWebInfo_Current("parentID") Is DBNull.Value, Integer.MinValue, Convert.ToInt32(drWebInfo_Current("parentID")))

                'We have a webInfoID, so we are updating a page, but are we updating a sub-page or a section? so we check the webinfoID_Parent
                If intWebInfoID_Parent = Integer.MinValue Then
                    'Page has no parent, so we are updating a SECTION
                    litEditAddHeading.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_UpdateSection
                Else
                    'Page has a parent, so we are updating a PAGE
                    litEditAddHeading.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_UpdateSubPage
                End If
                Dim strLinkName As String = drWebInfo_Current("Name").ToString()
                Dim strLinkUrl As String = drWebInfo_Current("LinkURL").ToString()
                Dim strLinkFrame As String = drWebInfo_Current("LinkFrame").ToString()
                Dim intNavigationColumnIndex As Integer = Convert.ToInt32(drWebInfo_Current("NavigationColumnIndex"))

                Dim drWebInfo_Current_PageLevel As Integer = Convert.ToInt32(drWebInfo_Current("PageLevel"))
                If boolUseThreeColumnLayout And drWebInfo_Current_PageLevel = 2 Then
                    trNavigationLayout_OutsidePage.Visible = True
                    trNavigationLayout_InsidePage.Visible = True
                Else
                    trNavigationLayout_OutsidePage.Visible = False
                    trNavigationLayout_InsidePage.Visible = False
                End If
                'Now we go through all inside pages in ddlInsidepages, to see if we have the same linkurl, if so we set the inital link url and the link name and target in the inside panel
                'Else we would set the link as an outside page, along with its link name and link target
                Dim boolFoundInsidePage As Boolean = False
                For Each liSelectPage As ListItem In ddlSelectPage.Items
                    Dim strSelectPage As String = liSelectPage.Value.ToLower()
                    If strSelectPage = strLinkUrl.ToLower() Then
                        ddlSelectPage.SelectedValue = liSelectPage.Value
                        lblLinkURL_InsidePage.Text = liSelectPage.Value
                        ddlTargetFrame_InsidePage.SelectedValue = strLinkFrame
                        rdNavigationLayout_InsidePage.SelectedValue = intNavigationColumnIndex.ToString()
                        boolFoundInsidePage = True
                    End If
                Next
                If boolFoundInsidePage = False Then
                    'Set the page as an outside page
                    txtLinkName.Text = strLinkName
                    txtLinkURL_OutsidePage.Text = strLinkUrl
                    ddlTargetFrame_OutsidePage.SelectedValue = strLinkFrame
                    rdNavigationLayout_OutsidePage.SelectedValue = intNavigationColumnIndex.ToString()
                End If
            End If

        Else

            'We DO NOT have a webInfoID, so we are adding a page, but are we adding a sub-page or a section? so we check the webinfoID_Parent
            If intWebInfoID_Parent = Integer.MinValue Then
                'Page has no parent, so we are ADDING a SECTION
                litEditAddHeading.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_AddSection
            Else
                'Page has a parent, so we are ADDING a PAGE
                litEditAddHeading.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_AddSubPage
            End If

            'Show create buttons, and inital default values
            divCreatePageWYSIWYG.Visible = True
            btnCreatePage_OutsidePage.Visible = True
            btnUpdatePage_OutsidePage.Visible = False
            btnCreatePage_InsidePage.Visible = True
            btnUpdatePage_InsidePage.Visible = False

            'reset all inputs
            ddlTargetFrame_InsidePage.SelectedIndex = 0
            ddlTargetFrame_OutsidePage.SelectedIndex = 0
            txtLinkName.Text = ""
            txtLinkURL_OutsidePage.Text = ""
            rdNavigationLayout_InsidePage.SelectedIndex = 0
            rdNavigationLayout_OutsidePage.SelectedIndex = 0

        End If

        'Finally bind our groups and users
        'slightly different to the way modules bind groups and users, as here we want to show heirarcy check if the parent is also in the same group
        'if the parent is not in a group that the child is in, we show a message, stating that this page will also not been seen by this group, as its parent is not in the group
        BindGroupCheckBoxListData(boolSecureMembers Or boolSecureEducation, intWebInfoID, intWebInfoID_Parent, intWebInfoID_HomePage, intWebInfoID_Header, intWebInfoID_Footer)
        BindUserCheckBoxListData(intWebInfoID, intWebInfoID_Parent, intWebInfoID_HomePage, intWebInfoID_Header, intWebInfoID_Footer)

        'If the webinfoID is passed in we must be updating a page, else we add a page
        hdn_WebInfoID.Value = intWebInfoID.ToString()
        hdn_WebInfoParentID.Value = intWebInfoID_Parent.ToString()

        'Show the panel and the tooltip
        pnl_EditAddPage.Visible = True
        rtt_EditAddPage.Show()
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

#Region "Events"

    Protected Sub lnkCheckInAll_Click(ByVal sender As Object, ByVal args As EventArgs) Handles lnkCheckInAll.Click
        Dim intSectionID As Integer = Request.QueryString("sectionID")

        'Get all pages in this section
        Dim dtWebInfoList As DataTable = WebInfoDAL.GetWebInfoList_BySectionID(intSectionID)
        'Go through each page in this section and check if it needs checking in
        'Note we dont' do all pages in the sections, as some pages may already be checked in by other users, we want to retain this information, 
        ' rather than updating it with the current username
        For Each drWebInfo As DataRow In dtWebInfoList.Rows
            Dim boolCheckedOut As Boolean = Convert.ToBoolean(drWebInfo("Checked_Out"))
            If boolCheckedOut Then
                'Check in this page
                Dim intWebInfoID As Integer = Convert.ToInt32(drWebInfo("ID"))
                WebInfoDAL.UpdateWebInfo_CheckInCheckOut_ByID(intWebInfoID, Not boolCheckedOut, strUserName)
            End If
        Next

        'Rebind our tree
        RadTreePages_BindRadTree()
    End Sub

    Protected Sub lnkTakeOfflineOrMakeLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkTakeOfflineOrMakeLive As LinkButton = sender
        Dim intWebInfoID As Integer = Convert.ToInt32(lnkTakeOfflineOrMakeLive.CommandArgument)

        'Change this pending value
        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByID(intWebInfoID)
        Dim drWebInfo As DataRow = dtWebInfo.Rows(0)
        Dim boolPending As Boolean = Convert.ToBoolean(drWebInfo("pending"))

        'call our recursive method that changes the pending flag for all WebInfo objects with a particular parent ID
        If boolPending Then
            UpdatePendingRecursive(intWebInfoID, False)
        Else
            UpdatePendingRecursive(intWebInfoID, True)
        End If

        'Rebind our grid
        RadTreePages_BindRadTree()

        'If this page is a section then refresh the webpage
        Dim intSectionID As Integer = Convert.ToInt32(drWebInfo("SectionID"))
        If intWebInfoID = intSectionID Then

            'Use javascript to reload the welcome page and tree
            Dim strTreeExtendedQueryString As String = ""
            If boolSecureMembers Then
                strTreeExtendedQueryString = "?secure_members=yes"
            ElseIf boolSecureEducation Then
                strTreeExtendedQueryString = "?secure_education=yes"
            End If
            CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, String.Empty, "/admin/richtemplate_list_sections.aspx" & strTreeExtendedQueryString, String.Empty, String.Empty)
        End If
    End Sub

    Protected Sub UpdatePendingRecursive(ByVal webInfoID As Integer, ByVal boolPending As Boolean)
        WebInfoDAL.UpdateWebInfo_Pending_ByID(webInfoID, boolPending)

        'Check to see if this webInfo page has children, if so run this method on each of the children
        'Note we only process children if we are to take a page offline, e.g. boolPending = True
        If boolPending = True Then
            Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfoList_ByParentID(webInfoID)
            For Each drWebInfo As DataRow In dtWebInfo.Rows
                Dim intWebInfoID As Integer = drWebInfo("id")
                UpdatePendingRecursive(intWebInfoID, boolPending)
            Next
        End If
    End Sub

    Protected Sub lnkAddPage_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkAddPage As LinkButton = sender
        Dim intWebInfoParentID As Integer = Convert.ToInt32(lnkAddPage.CommandArgument)

        SetupEditAddPage(Integer.MinValue, intWebInfoParentID)
    End Sub

    Protected Sub lnkEditLiveEnabled_Link_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkEditLiveEnabled_Link As LinkButton = sender
        Dim intWebInfoID As Integer = Convert.ToInt32(lnkEditLiveEnabled_Link.CommandArgument)

        SetupEditAddPage(intWebInfoID, Integer.MinValue)
    End Sub

    Protected Sub btnCreateOrUpdateInsideOrOutsidePage(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreatePage_OutsidePage.Click, btnUpdatePage_OutsidePage.Click, btnCreatePage_InsidePage.Click, btnUpdatePage_InsidePage.Click

        If Page.IsValid Then
            'Hide Step One and check if we should show step two or just save the page
            divStepOne.Visible = False




            Dim btnSender As Button = sender
            If btnSender Is btnCreatePage_OutsidePage Then
                If boolWebpage_MemberSection_EnableGroupsAndUsers Then
                    'The members section is using Groups and User Permissions so show Step 2
                    litStepTwoHeading.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateOutsidePage_Heading
                    btnBack.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateOutsidePage_ButtonBack
                    btnFinish.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateOutsidePage_ButtonFinish
                    btnFinish.CommandArgument = "OUTSIDE_PAGE"
                    divStepTwo.Visible = True
                    rtt_EditAddPage.Show()
                ElseIf ((Not boolSecureMembers And Not boolSecureEducation) And boolWebpage_PublicSection_EnableGroupsAndUsers) Then
                    'The public section is using Groups and User Permissions so show Step 2
                    litStepTwoHeading.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateOutsidePage_Heading
                    btnBack.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateOutsidePage_ButtonBack
                    btnFinish.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateOutsidePage_ButtonFinish
                    btnFinish.CommandArgument = "OUTSIDE_PAGE"
                    divStepTwo.Visible = True
                    rtt_EditAddPage.Show()
                Else
                    CreateOutsidePage()
                End If

            ElseIf btnSender Is btnUpdatePage_OutsidePage Then
                If boolWebpage_MemberSection_EnableGroupsAndUsers Then
                    'The members section is using Groups and User Permissions so show Step 2
                    litStepTwoHeading.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateOutsidePage_Heading
                    btnBack.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateOutsidePage_ButtonBack
                    btnFinish.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateOutsidePage_ButtonFinish
                    btnFinish.CommandArgument = "OUTSIDE_PAGE"
                    divStepTwo.Visible = True
                    rtt_EditAddPage.Show()
                ElseIf ((Not boolSecureMembers And Not boolSecureEducation) And boolWebpage_PublicSection_EnableGroupsAndUsers) Then
                    'The public section is using Groups and User Permissions so show Step 2
                    litStepTwoHeading.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateOutsidePage_Heading
                    btnBack.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateOutsidePage_ButtonBack
                    btnFinish.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateOutsidePage_ButtonFinish
                    btnFinish.CommandArgument = "OUTSIDE_PAGE"
                    divStepTwo.Visible = True
                    rtt_EditAddPage.Show()
                Else
                    UpdateOutsidePage()
                End If

            ElseIf btnSender Is btnCreatePage_InsidePage Then
                If ((boolSecureMembers Or boolSecureEducation) And boolWebpage_MemberSection_EnableGroupsAndUsers) Then
                    'The members section is using Groups and User Permissions so show Step 2
                    litStepTwoHeading.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateInternalPage_Heading
                    btnBack.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateInternalPage_ButtonBack
                    btnFinish.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateInternalPage_ButtonFinish
                    btnFinish.CommandArgument = "INSIDE_PAGE"
                    divStepTwo.Visible = True
                    rtt_EditAddPage.Show()
                ElseIf ((Not boolSecureMembers And Not boolSecureEducation) And boolWebpage_PublicSection_EnableGroupsAndUsers) Then
                    'The public section is using Groups and User Permissions so show Step 2
                    litStepTwoHeading.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateInternalPage_Heading
                    btnBack.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateInternalPage_ButtonBack
                    btnFinish.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateInternalPage_ButtonFinish
                    btnFinish.CommandArgument = "INSIDE_PAGE"
                    divStepTwo.Visible = True
                    rtt_EditAddPage.Show()
                Else
                    CreateInsidePage()
                End If

            ElseIf btnSender Is btnUpdatePage_InsidePage Then
                If ((boolSecureMembers Or boolSecureEducation) And boolWebpage_MemberSection_EnableGroupsAndUsers) Then
                    'The members section is using Groups and User Permissions so show Step 2
                    litStepTwoHeading.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateInternalPage_Heading
                    btnBack.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateInternalPage_ButtonBack
                    btnFinish.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateInternalPage_ButtonFinish
                    btnFinish.CommandArgument = "INSIDE_PAGE"
                    divStepTwo.Visible = True
                    rtt_EditAddPage.Show()
                ElseIf ((Not boolSecureMembers And Not boolSecureEducation) And boolWebpage_PublicSection_EnableGroupsAndUsers) Then
                    'The public section is using Groups and User Permissions so show Step 2
                    litStepTwoHeading.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateInternalPage_Heading
                    btnBack.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateInternalPage_ButtonBack
                    btnFinish.Text = Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_CreateInternalPage_ButtonFinish
                    btnFinish.CommandArgument = "INSIDE_PAGE"
                    divStepTwo.Visible = True
                    rtt_EditAddPage.Show()
                Else
                    UpdateInsidePage()
                End If

            End If
        Else
            rtt_EditAddPage.Show()
        End If
    End Sub

    Private Sub CreateOutsidePage()
        'Format the link url
        txtLinkURL_OutsidePage.Text = CommonWeb.FormatURL(txtLinkURL_OutsidePage.Text)

        Dim strTargetFrame As String = ddlTargetFrame_OutsidePage.SelectedValue
        Dim strLinkName As String = txtLinkName.Text.Trim()
        Dim strLinkUrl As String = txtLinkURL_OutsidePage.Text.Trim()

        Dim boolDefaultPage As Boolean = False
        Dim strMessage As String = ""
        Dim strMessage2 As String = ""

        Dim intPageLevel As Integer = 1

        'Get the current user and set the author, they didn't actually write this page but they did create it in our website
        Dim strAuthor As String = ""
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
        If dtAdminUser.Rows.Count > 0 Then
            Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

            strAuthor = drAdminUser("username").ToString()
        End If

        Dim dtLastModified As DateTime = DateTime.MinValue

        Dim boolPending As Boolean = True ' By Default the Section or Page must be Made Live
        Dim boolCheckedOut As Boolean = False
        Dim strCheckedID As String = ""

        Dim boolLinkOnly As Boolean = True

        Dim intSectionID As Integer = Integer.MinValue

        Dim strMetaDesc As String = ""
        Dim strMetaTitle As String = ""
        Dim strMetaKeyword As String = ""

        Dim strPage_LinkName As String = "interior.aspx"
        Dim boolSearchable As Boolean = False
        Dim intLanguage As Integer = Integer.MinValue
        Dim strUrlPath As String = ""

        Dim strSearchTags As String = ""

        Dim intNavigationColumnIndex As Integer = Convert.ToInt32(rdNavigationLayout_OutsidePage.SelectedValue)

        Dim boolInheritBannerImage As Boolean = False 'Creating a section link, so we don't use a banner

        Dim intWebInfoParentID As Integer = Convert.ToInt32(hdn_WebInfoParentID.Value)

        'Get the parent page, so we can inherit properties from this parent
        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByID(intWebInfoParentID)
        If dtWebInfo.Rows.Count > 0 Then
            Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

            intPageLevel = Convert.ToInt32(drWebInfo("PageLevel")) + 1
            intSectionID = Convert.ToInt32(drWebInfo("sectionID"))

            boolInheritBannerImage = True 'We are creating a child page inside a section, so by default we inherit the banner image of the section

        End If
        Dim intWebInfoID = WebInfoDAL.InsertWebInfo(strLinkName, intWebInfoParentID, intSiteID, intPageLevel, boolDefaultPage, strMessage, strMessage2, strAuthor, dtLastModified, boolPending, boolCheckedOut, strCheckedID, boolLinkOnly, strLinkUrl, strTargetFrame, intSectionID, strMetaTitle, strMetaDesc, strMetaKeyword, strPage_LinkName, boolSearchable, intLanguage, strUrlPath, boolSecureMembers, boolSecureEducation, strSearchTags, intNavigationColumnIndex, boolInheritBannerImage)
        If intSectionID = Integer.MinValue Then
            intSectionID = intWebInfoID
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

        'If the outside page is a section, reload all frames, else just refresh the current page
        If intWebInfoID = intSectionID Then
            'Use javascript to reload the listpages page in the main iframe and reload the tree
            'Use javascript to reload the welcome page and tree
            Dim strTreeExtendedQueryString As String = ""
            If boolSecureMembers Then
                strTreeExtendedQueryString = "?secure_members=yes"
            ElseIf boolSecureEducation Then
                strTreeExtendedQueryString = "?secure_education=yes"
            End If
            CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, "/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionID.ToString() & "&wi=" & intWebInfoID, "/admin/richtemplate_list_sections.aspx" & strTreeExtendedQueryString, String.Empty, String.Empty)
        Else
            Response.Redirect("/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionID.ToString() & "&wi=" & intWebInfoID)

        End If
    End Sub

    Private Sub UpdateOutsidePage()

        'Format the link url
        txtLinkURL_OutsidePage.Text = CommonWeb.FormatURL(txtLinkURL_OutsidePage.Text)

        Dim intWebInfoID As Integer = Convert.ToInt32(hdn_WebInfoID.Value)

        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByID(intWebInfoID)
        If dtWebInfo.Rows.Count > 0 Then

            'Get the webinfo and load the page
            Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

            Dim intSectionID As Integer = Convert.ToInt32(drWebInfo("sectionID"))

            Dim strTargetFrame As String = ddlTargetFrame_OutsidePage.SelectedValue
            Dim strLinkName As String = txtLinkName.Text.Trim()
            Dim strLinkUrl As String = txtLinkURL_OutsidePage.Text.Trim()

            Dim intNavigationColumnIndex As Integer = Convert.ToInt32(rdNavigationLayout_OutsidePage.SelectedValue)

            Dim dtLastModified As DateTime = DateTime.Now

            Dim boolCheckedOut As Boolean = False 'Can not checkout a 'Link Only' page

            Dim strAuthor As String = ""
            Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
            If dtAdminUser.Rows.Count > 0 Then
                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                strAuthor = drAdminUser("username").ToString()
            End If

            WebInfoDAL.UpdateWebInfo_CheckInCheckOut_ByID(intWebInfoID, boolCheckedOut, strAuthor)
            WebInfoDAL.UpdateWebInfo_LinkOnly(intWebInfoID, strLinkName, strLinkUrl, strTargetFrame, intNavigationColumnIndex, dtLastModified)

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

            'If the outside page is a section, reload all frames, else just refresh the current page
            If intWebInfoID = intSectionID Then
                'Use javascript to reload the listpages page in the main iframe and reload the tree
                'Use javascript to reload the welcome page and tree
                Dim strTreeExtendedQueryString As String = ""
                If boolSecureMembers Then
                    strTreeExtendedQueryString = "?secure_members=yes"
                ElseIf boolSecureEducation Then
                    strTreeExtendedQueryString = "?secure_education=yes"
                End If
                CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, "/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionID.ToString() & "&wi=" & intWebInfoID, "/admin/richtemplate_list_sections.aspx" & strTreeExtendedQueryString, String.Empty, String.Empty)
            Else
                Response.Redirect("/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionID.ToString() & "&wi=" & intWebInfoID)
            End If


        End If

    End Sub

    Private Sub CreateInsidePage()

        Dim intWebInfoParentID As Integer = Convert.ToInt32(hdn_WebInfoParentID.Value)

        Dim strTargetFrame As String = ddlTargetFrame_InsidePage.SelectedValue
        Dim strLinkUrl As String = ddlSelectPage.SelectedItem.Value.Trim()
        Dim strName As String = ddlSelectPage.SelectedItem.Value.Trim()

        'This strName is in a /parent/page.aspx format so we must get the string form the last slash
        strName = strName.Substring(strName.LastIndexOf("/") + 1)
        'Now remove everything after the last '.'
        Dim intLastIndexDot As Integer = strName.LastIndexOf(".")
        strName = strName.Substring(0, strName.LastIndexOf("."))
        strName = CommonWeb.decodeHyperlink(strName)

        Dim intPageLevel As Integer = 1

        Dim boolDefaultPage As Boolean = False
        Dim strMessage As String = ""
        Dim strMessage2 As String = ""
        'Get the current user and set the author, they didn't actually write this page but they did create it in our website
        Dim strAuthor As String = ""
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
        If dtAdminUser.Rows.Count > 0 Then
            Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

            strAuthor = drAdminUser("username").ToString()
        End If

        Dim dtLastModified As DateTime = DateTime.MinValue

        Dim boolPending As Boolean = True ' By Default the Section or Page must be Made Live
        Dim boolCheckedOut As Boolean = False
        Dim strCheckedID As String = ""

        Dim boolLinkOnly As Boolean = True

        Dim intSectionID As Integer = Integer.MinValue
        Dim strMetaDesc As String = ""
        Dim strMetaTitle As String = ""
        Dim strMetaKeyword As String = ""

        Dim strPage_LinkName As String = "interior.aspx"
        Dim boolSearchable As Boolean = False
        Dim intLanguage As Integer = Integer.MinValue
        Dim strUrlPath As String = ""

        Dim strSearchTags As String = ""

        Dim intNavigationColumnIndex As Integer = Convert.ToInt32(rdNavigationLayout_InsidePage.SelectedValue)
        Dim boolInheritBannerImage As Boolean = False 'Creating a section link, so we don't use a banner

        'Get the parent page, so we can inherit properties from this parent
        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByID(intWebInfoParentID)
        If dtWebInfo.Rows.Count > 0 Then
            Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

            intPageLevel = Convert.ToInt32(drWebInfo("PageLevel")) + 1
            intSectionID = Convert.ToInt32(drWebInfo("sectionID"))

        End If

        Dim intWebInfoID = WebInfoDAL.InsertWebInfo(strName, intWebInfoParentID, intSiteID, intPageLevel, boolDefaultPage, strMessage, strMessage2, strAuthor, dtLastModified, boolPending, boolCheckedOut, strCheckedID, boolLinkOnly, strLinkUrl, strTargetFrame, intSectionID, strMetaTitle, strMetaDesc, strMetaKeyword, strPage_LinkName, boolSearchable, intLanguage, strUrlPath, boolSecureMembers, boolSecureEducation, strSearchTags, intNavigationColumnIndex, boolInheritBannerImage)
        If intSectionID = Integer.MinValue Then
            intSectionID = intWebInfoID
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

        'If the outside page is a section, reload all frames, else just refresh the current page
        If intWebInfoID = intSectionID Then
            'Use javascript to reload the listpages page in the main iframe and reload the tree
            'Use javascript to reload the welcome page and tree
            Dim strTreeExtendedQueryString As String = ""
            If boolSecureMembers Then
                strTreeExtendedQueryString = "?secure_members=yes"
            ElseIf boolSecureEducation Then
                strTreeExtendedQueryString = "?secure_education=yes"
            End If
            CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, "/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionID.ToString() & "&wi=" & intWebInfoID, "/admin/richtemplate_list_sections.aspx" & strTreeExtendedQueryString, String.Empty, String.Empty)
        Else
            Response.Redirect("/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionID.ToString() & "&wi=" & intWebInfoID)
        End If
    End Sub

    Private Sub UpdateInsidePage()
        'Format the link url
        txtLinkURL_OutsidePage.Text = CommonWeb.FormatURL(txtLinkURL_OutsidePage.Text)

        Dim intWebInfoID As Integer = Convert.ToInt32(hdn_WebInfoID.Value)

        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByID(intWebInfoID)
        If dtWebInfo.Rows.Count > 0 Then

            'Get the webinfo and load the page
            Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

            Dim intSectionID As Integer = Convert.ToInt32(drWebInfo("sectionID"))

            Dim strTargetFrame As String = ddlTargetFrame_InsidePage.SelectedValue
            Dim strLinkUrl As String = ddlSelectPage.SelectedItem.Value.Trim()
            Dim strName As String = ddlSelectPage.SelectedItem.Value.Trim()

            'This strName is in a /parent/page.aspx format so we must get the string form the last slash
            strName = strName.Substring(strName.LastIndexOf("/") + 1)
            'Now remove everything after the last '.'
            Dim intLastIndexDot As Integer = strName.LastIndexOf(".")
            strName = strName.Substring(0, strName.LastIndexOf("."))
            strName = CommonWeb.decodeHyperlink(strName)

            Dim intNavigationColumnIndex As Integer = Convert.ToInt32(rdNavigationLayout_InsidePage.SelectedValue)

            Dim dtLastModified As DateTime = DateTime.Now

            Dim boolCheckedOut As Boolean = False 'Can not checkout a 'Link Only' page

            Dim strAuthor As String = ""
            Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
            If dtAdminUser.Rows.Count > 0 Then
                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                strAuthor = drAdminUser("username").ToString()
            End If

            WebInfoDAL.UpdateWebInfo_CheckInCheckOut_ByID(intWebInfoID, boolCheckedOut, strAuthor)
            WebInfoDAL.UpdateWebInfo_LinkOnly(intWebInfoID, strName, strLinkUrl, strTargetFrame, intNavigationColumnIndex, dtLastModified)

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

            'If the outside page is a section, reload all frames, else just refresh the current page
            If intWebInfoID = intSectionID Then
                'Use javascript to reload the listpages page in the main iframe and reload the tree
                'Use javascript to reload the welcome page and tree
                Dim strTreeExtendedQueryString As String = ""
                If boolSecureMembers Then
                    strTreeExtendedQueryString = "?secure_members=yes"
                ElseIf boolSecureEducation Then
                    strTreeExtendedQueryString = "?secure_education=yes"
                End If
                CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, "/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionID.ToString() & "&wi=" & intWebInfoID, "/admin/richtemplate_list_sections.aspx" & strTreeExtendedQueryString, String.Empty, String.Empty)
            Else
                Response.Redirect("/admin/RichTemplate_List_Pages.aspx?sectionID=" & intSectionID.ToString() & "&wi=" & intWebInfoID)
            End If

        End If

    End Sub

    Protected Sub btnFinish_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFinish.Click
        Dim intWebInfoID As Integer = Convert.ToInt32(hdn_WebInfoID.Value)
        If intWebInfoID = Integer.MinValue Then
            'We must be adding/creating a new page
            If btnFinish.CommandArgument.ToUpper() = "OUTSIDE_PAGE" Then
                CreateOutsidePage()
            ElseIf btnFinish.CommandArgument.ToUpper() = "INSIDE_PAGE" Then
                CreateInsidePage()
            End If
        Else

            'We have a WebInfoID so we must be updating a page
            If btnFinish.CommandArgument.ToUpper() = "OUTSIDE_PAGE" Then
                UpdateOutsidePage()
            ElseIf btnFinish.CommandArgument.ToUpper() = "INSIDE_PAGE" Then
                UpdateInsidePage()
            End If
        End If

    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        'We go back to the first step, so Hide Step Two, and show Step One
        lblLinkURL_InsidePage.Text = ddlSelectPage.SelectedValue
        divStepTwo.Visible = False
        divStepOne.Visible = True
        rtt_EditAddPage.Show()
    End Sub

    Protected Sub lnkClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkClose.Click
        'get current section from request string, if the EditAdd Panel we are showing, if for adding a section, then return the user back to the welcome page
        Dim intSectionID As Integer = Request.QueryString("sectionID")
        If intSectionID = 0 Then
            'Use javascript to reload the welcome page and tree
            Dim strTreeExtendedQueryString As String = ""
            If boolSecureMembers Then
                strTreeExtendedQueryString = "?secure_members=yes"
            ElseIf boolSecureEducation Then
                strTreeExtendedQueryString = "?secure_education=yes"
            End If
            CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, "/admin/richtemplate_welcome.aspx?mode=forms", "/admin/richtemplate_list_sections.aspx" & strTreeExtendedQueryString, String.Empty, String.Empty)

        End If
    End Sub

    Protected Sub lnkDeleteSection_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkDeleteSection As LinkButton = sender
        Dim intWebInfoID As Integer = Convert.ToInt32(lnkDeleteSection.CommandArgument)

        WebInfoDAL.DeleteWebInfo_BySectionID(intWebInfoID)

        'Use javascript to reload the welcome page and tree
        Dim strTreeExtendedQueryString As String = ""
        If boolSecureMembers Then
            strTreeExtendedQueryString = "?secure_members=yes"
        ElseIf boolSecureEducation Then
            strTreeExtendedQueryString = "?secure_education=yes"
        End If
        CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, "/admin/richtemplate_welcome.aspx?mode=forms", "/admin/richtemplate_list_sections.aspx" & strTreeExtendedQueryString, String.Empty, String.Empty)

    End Sub

    Protected Sub lnkDeletePage_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkDeletePage As LinkButton = sender
        Dim intWebInfoID As Integer = Convert.ToInt32(lnkDeletePage.CommandArgument)

        'Delete this page and all of its sub pages Recursively
        DeletePageRecursive(intWebInfoID)

        'Rebind our tree
        RadTreePages_BindRadTree()
    End Sub

    Protected Sub DeletePageRecursive(ByVal webInfoID As Integer)
        'Check to see if this webInfo page has children, if so run this method on each of the children FIRST!!
        'we want to delete the children first, as the parent is associated to the child through the parentID
        ' -> BECAUSE we don't want to delete the parent first and have a child page associated to a parentId that does not exist
        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfoList_ByParentID(webInfoID)
        For Each drWebInfo As DataRow In dtWebInfo.Rows
            Dim intWebInfoID As Integer = drWebInfo("id")
            DeletePageRecursive(intWebInfoID)
        Next

        'Finally delete the page once all children are deleted
        WebInfoDAL.DeleteWebInfo_ByID(webInfoID)
    End Sub

    Protected Sub lnkCheckIn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkCheckIn As LinkButton = sender
        Dim intWebInfoID As Integer = Convert.ToInt32(lnkCheckIn.CommandArgument)

        'Check in this page
        Dim boolCheckedOut As Boolean = False
        WebInfoDAL.UpdateWebInfo_CheckInCheckOut_ByID(intWebInfoID, boolCheckedOut, strUserName)

        'Rebind our tree
        RadTreePages_BindRadTree()

    End Sub

    Protected Sub RadTreePages_NodeDrop(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeDragDropEventArgs)

        If e.DraggedNodes.Count > 0 Then
            If Not e.DestDragNode Is Nothing Then


                'Get the WebInfoID's of our Dragged Row and Destination Row
                Dim webinfoID_DraggedNode As Integer = Convert.ToInt32(e.DraggedNodes(0).Value)
                Dim webinfoID_DestNode As Integer = Convert.ToInt32(e.DestDragNode.Value)

                'Get the WebInfoRows from our dataset
                PopulateWebpageDataTable()
                Dim drDraggedNode As DataRow = dtWebInfoPages.Rows.Find(webinfoID_DraggedNode)
                Dim drDestNode As DataRow = dtWebInfoPages.Rows.Find(webinfoID_DestNode)

                If (Not drDraggedNode Is Nothing) And (Not drDestNode Is Nothing) Then

                    'Both Dragged Item and Destination Item have same parentID
                    Dim intDraggedPageLevel As Integer = Convert.ToInt32(drDraggedNode("PageLevel"))
                    Dim intDroppedPageLevel As Integer = Convert.ToInt32(drDestNode("PageLevel"))

                    Dim intDraggedParentID As Integer = Integer.MinValue
                    If Not drDraggedNode("ParentID") Is DBNull.Value Then
                        intDraggedParentID = Convert.ToInt32(drDraggedNode("ParentID"))
                    End If

                    Dim intDroppedParentID As Integer = Integer.MinValue


                    'By default, the new node will be positioned as the last sibling
                    ' We give it a rank of Null and the db will see a null rank and give it a rank of the 'total number of items + 1' for its parent
                    Dim drDest_Rank As Integer = Integer.MinValue

                    If e.DropPosition = RadTreeViewDropPosition.Over Then
                        intDroppedParentID = Convert.ToInt32(drDestNode("ID"))
                        intDroppedPageLevel = intDroppedPageLevel + 1

                    Else

                        If Not drDestNode("ParentID") Is DBNull.Value Then
                            intDroppedParentID = Convert.ToInt32(drDestNode("ParentID"))
                        End If
                        'Get the Current Rank and the New Rank, these may need updating by one depending on how your selecting dropped reference row
                        drDest_Rank = drDestNode("Rank")
                        Dim intDestIndex As Integer = e.DestDragNode.Index
                        Dim intDraggedIndex As Integer = e.DraggedNodes(0).Index

                        If intDraggedParentID = intDroppedParentID Then
                            If e.DropPosition = RadTreeViewDropPosition.Below And intDestIndex < intDraggedIndex Then
                                drDest_Rank += 1
                            ElseIf e.DropPosition = RadTreeViewDropPosition.Above And intDestIndex > intDraggedIndex Then
                                drDest_Rank -= 1
                            End If
                        Else
                            'Else we are dragging to a new category so we disreguard the index in the current category
                            If e.DropPosition = RadTreeViewDropPosition.Below Then
                                drDest_Rank += 1
                            End If
                        End If
                    End If

                    'CONSTRAINT ONE
                    'Before we update our WebInfo Ranks, we must check that the Dragged Item is not placed above
                    'Note if the node is to be dragged under the root node we give it a rank of 1 and the parent id of the root node, not a sibling of the root node 
                    ' => special case as we can not move a node to be on the same level as a root node, e.g. make it another ROOT node
                    'CONSTRAINT TWO
                    ' when dropping a node onto another node check that if it was created, it would not violate our max page depth
                    'CONSTRAINT THREE
                    ' If the dragged node belongs to the Header Page or Footer Page, i.e. it has a intWebInfoID_Parent = intWebInfoID_Header or intWebInfoID_Parent = intWebInfoID_Footer, then this node can only be dropped inside the header/footer node, otherwise display error
                    Dim strDragDropConstraintError As String = ""
                    'Constraint one
                    'So the node is dropped either above or below, so check if the destination node is the root (e.g. page level 1) then show error
                    ' You can not drag a node to such that its at the same level as the root node
                    If e.DropPosition = RadTreeViewDropPosition.Above Or e.DropPosition = RadTreeViewDropPosition.Below Then
                        If intDroppedPageLevel = 1 Then
                            strDragDropConstraintError = Resources.RichTemplate_List_Pages.RichTemplate_List_Sections_Grid_DragDropError_NotAboveOrBelowRootPage
                        End If
                    End If

                    'Constriant Two
                    'Go through each row in our table of pages, find our dragged row
                    'From this dragged row, as all pages are in seqential order, we start from the dragged row, and continue until we get back to a sibling node (e.g. another node at the same page level
                    Dim intMaxPageLevel = intDraggedPageLevel

                    'Get index of dragged node
                    Dim indexOfDraggedWebInfo As Integer = 0

                    For i As Integer = 0 To dtWebInfoPages.Rows.Count - 1
                        Dim intCurrentWebInfoID As Integer = Convert.ToInt32(dtWebInfoPages.Rows(i)("ID"))
                        If webinfoID_DraggedNode = intCurrentWebInfoID Then
                            indexOfDraggedWebInfo = i
                            Exit For
                        End If
                    Next
                    'Get the max page level starting at our Dragged Node index, keep iterating through until we reach a row at the same page level
                    For i As Integer = indexOfDraggedWebInfo + 1 To dtWebInfoPages.Rows.Count - 1
                        Dim intCurrentWebInfoPageLevel As Integer = Convert.ToInt32(dtWebInfoPages.Rows(i)("PageLevel"))
                        If intCurrentWebInfoPageLevel > intDraggedPageLevel Then
                            'We are at a child node of the dragged node so compare its page level to our max
                            If intCurrentWebInfoPageLevel > intMaxPageLevel Then
                                intMaxPageLevel = intCurrentWebInfoPageLevel
                            End If
                        Else
                            Exit For 'As we have reach the dragged nodes sibling or its parents, e.g. no more child nodes
                        End If
                    Next

                    'Now we have the max page level of this node, we must add it to the destination parents page level to get the overall page level
                    Dim newMaxPageLevel As Integer = intDroppedPageLevel + (intMaxPageLevel - intDraggedPageLevel)
                    If newMaxPageLevel > intAllowedSiteDepth Then
                        strDragDropConstraintError = Resources.RichTemplate_List_Pages.RichTemplate_List_Sections_Grid_DragDropError_ExceedsMaximumPageDepth
                    End If

                    'Constraint Three, ensure if the dragged node's parent ID equals the Header ID or the Footer ID, then the dropped node's parent id must also equal the Header ID or Footer ID respectively, otherwise show error
                    If intDraggedParentID = intWebInfoID_Header AndAlso intDraggedParentID <> intDroppedParentID Then
                        strDragDropConstraintError = Resources.RichTemplate_List_Pages.RichTemplate_List_Sections_Grid_DragDropError_HeaderNotInDroppedInHeaderContainer
                    ElseIf intDraggedParentID = intWebInfoID_Footer AndAlso intDraggedParentID <> intDroppedParentID Then
                        strDragDropConstraintError = Resources.RichTemplate_List_Pages.RichTemplate_List_Sections_Grid_DragDropError_FooterNotInDroppedInFooterContainer

                    End If


                    'Now perform the drag'n drop or show the error message
                    If strDragDropConstraintError = "" Then
                        'Perform the acutal update of our WebInfo rows
                        WebInfoDAL.UpdateWebInfo_Rank(webinfoID_DraggedNode, intDroppedParentID, drDest_Rank, intSiteID)

                        'Rebind our Tree
                        RadTreePages_BindRadTree()
                    Else
                        'Do nothing perhaps show 'Sorry, this page can only be moved within its same page level'
                        'Register our javascript for inside our update panel
                        Dim strDragDropError As String = "alert('" & strDragDropConstraintError & "');"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "drag_drop_Error", strDragDropError, True)
                    End If

                End If
            End If
        End If

    End Sub

#End Region

End Class
