Imports System.Data.SqlClient
Imports System.Data
Imports Telerik.Web.UI
Imports System.Drawing

Partial Class admin_richtemplate_list_sections
    Inherits RichTemplateLanguagePage

    Dim boolAllowSectionAdd As Boolean = False
    Dim boolAllowPublish As Boolean = False

    Dim boolSecureMembers As Boolean = False
    Dim boolSecureEducation As Boolean = False

    Dim intAccessLevel As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Check thes user exists and Only give the ability to add and view sections it the user has access
        Dim intSiteID As Integer = SiteDAL.GetCurrentSiteID_Admin()
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()

        Dim boolAllowWebContent As Boolean = AdminUserDAL.GetCurrentAdminUserAllowWebContent()
        If intAdminUserID > 0 AndAlso boolAllowWebContent Then

            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAdminUserID)
            If dtAdminUser.Rows.Count > 0 Then
                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                'Get admin user information
                intAccessLevel = AdminUserDAL.GetCurrentAdminUserAccessLevel()

                'Get admin user information
                Dim boolUseSiteLevelAccess As Boolean = Convert.ToBoolean(drAdminUser("UseSiteLevelAccess"))
                Dim drSiteAccess_AdminUser As DataRow = Nothing
                If boolUseSiteLevelAccess Then
                    Dim dtSiteAccess_AdminUser As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID_Active(intSiteID, intAdminUserID)
                    If dtSiteAccess_AdminUser.Rows.Count > 0 Then
                        drSiteAccess_AdminUser = dtSiteAccess_AdminUser.Rows(0)
                    End If
                End If

                'Get Admin User Permissions
                boolAllowSectionAdd = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Section_Add")), Convert.ToBoolean(drAdminUser("Allow_Section_Add_AllSites")))
                boolAllowPublish = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_Publish")), Convert.ToBoolean(drAdminUser("Allow_Publish_AllSites")))

                'Check if Admin User has SectionAdd Permissions
                '' Now only Super Admins can Add Sections
                If boolAllowSectionAdd AndAlso intAccessLevel > 2 Then
                    divAddSection.Visible = True
                End If

                'Check if Admin User has Publish Permissions, if so allow them to use drag and drop
                ' NOW only super admins can re-order sections
                If boolAllowPublish AndAlso intAccessLevel > 2 Then
                    RadTreeSections.EnableDragAndDrop = True
                End If

                'Show the main divListSections DIV
                divListSections.Visible = True

                'set the SecureMembers and SecureEducation booleans
                If Not Request.QueryString("secure_members") Is Nothing Then
                    If Request.QueryString("secure_members").ToLower() = "yes" Then
                        boolSecureMembers = True
                    End If
                ElseIf Not Request.QueryString("secure_education") Is Nothing Then
                    If Request.QueryString("secure_education").ToLower() = "yes" Then
                        boolSecureEducation = True
                    End If
                End If

                'Load Sections
                LoadSections()
            Else
                'AdminUser ID exists but can not find their row in the db
                Response.Redirect("~/richadmin/")
            End If

        Else
            'AdminUser ID does not exist
            Response.Redirect("~/richadmin/")
        End If

    End Sub

    Protected Sub LoadSections()


        Dim intSectionID_Request As Integer = 0
        If Not Request.QueryString("sectionID") Is Nothing Then
            intSectionID_Request = Convert.ToInt32(Request.QueryString("sectionID"))
        End If

        'Load section specific information (links and tree)
        Dim dtSections As New DataTable()
        If boolSecureMembers Then
            dtSections = WebInfoDAL.GetWebInfoList_SectionsOnly(True, False, SiteDAL.GetCurrentSiteID_Admin())
            anchorNewSection_SecureMembers.Visible = True

        ElseIf boolSecureEducation Then
            dtSections = WebInfoDAL.GetWebInfoList_SectionsOnly(False, True, SiteDAL.GetCurrentSiteID_Admin())
            anchorNewSection_SecureEducation.Visible = True

        Else
            dtSections = WebInfoDAL.GetWebInfoList_SectionsOnly(False, False, SiteDAL.GetCurrentSiteID_Admin())
            anchorNewSection.Visible = True

        End If

        'Populate our section tree
        RadTreeSections.Nodes.Clear()
        'Create a node for each section then adds it to our tree

        For rowIndex = 0 To dtSections.Rows.Count - 1
            Dim drSection As DataRow = dtSections.Rows(rowIndex)

            Dim intSectionID As Integer = Convert.ToInt32(drSection("ID"))
            Dim strSectionName As String = drSection("Name").ToString()
            Dim boolPending As Boolean = Convert.ToBoolean(drSection("Pending"))
            'Note sections can never be 'LinkOnly' but here just in-case of future customization
            Dim boolLinkOnly As Boolean = Convert.ToBoolean(drSection("LinkOnly"))

            Dim boolIsHomePageSection As Boolean = (rowIndex = 0)
            Dim strSectionText As String = ""

            'If we are dealing with the first section, then this is the homepage, otherwise its a regular section
            If boolIsHomePageSection AndAlso (Not boolSecureMembers) AndAlso (Not boolSecureEducation) Then
                'Only the Main Homepage, uses a resources file to get the language specific name
                strSectionText = "<div class='" + If(boolAllowPublish, "dndUpDown", "dndUpDownDisabled") & "'></div>&nbsp;<img src='/admin/images/icon_homepage_large.png' class='rtImg' alt='" & Resources.RichTemplate_List_Sections.RichTemplate_List_Sections_Grid_HomePage & "'><span class='inner_rtIn'><a href='/admin/richtemplate_list_pages.aspx?sectionID=" & intSectionID.ToString() & "' target='basefrm'><font size='1' color='#000080'><b>&nbsp;" & Resources.RichTemplate_List_Sections.RichTemplate_List_Sections_Grid_HomePage & "</b></font></a></span>"
            Else
                'Only show the Drag 'n Drop dndUpDown div if the current Admin User has Publish permissions
                If boolPending Then
                    strSectionText = "<div class='" + If(boolAllowPublish, "dndUpDown", "dndUpDownDisabled") & "'></div>&nbsp;<img src='/admin/images/folder_full_sm.png' class='rtImg' alt='" & strSectionName & "'><span class='inner_rtIn'><a href='/admin/richtemplate_list_pages.aspx?sectionID=" & intSectionID.ToString() & "' target='basefrm'><font size='1' color='#808080'><b>&nbsp;" & strSectionName & "</b></font></a></span>"
                ElseIf boolLinkOnly Then
                    strSectionText = "<div class='" + If(boolAllowPublish, "dndUpDown", "dndUpDownDisabled") & "'></div>&nbsp;<img src='/admin/images/folder_full_sm.png' class='rtImg' alt='" & strSectionName & "'><span class='inner_rtIn'><a href='/admin/richtemplate_list_pages.aspx?sectionID=" & intSectionID.ToString() & "' target='basefrm'><font size='1' color='#3055A9'><b>&nbsp;" & strSectionName & "</b></font></a></span>"
                Else
                    strSectionText = "<div class='" + If(boolAllowPublish, "dndUpDown", "dndUpDownDisabled") & "'></div>&nbsp;<img src='/admin/images/folder_full_sm.png' class='rtImg' alt='" & strSectionName & "'><span class='inner_rtIn'><a href='/admin/richtemplate_list_pages.aspx?sectionID=" & intSectionID.ToString() & "' target='basefrm'><font size='1'><b>&nbsp;" & strSectionName & "</b></font></a></span>"
                End If
            End If

            Dim rtnSection As New RadTreeNode(strSectionText, intSectionID.ToString())
            rtnSection.Category = "Folder"
            rtnSection.Expanded = True

            'Only Super Admins can drag/drop sections
            If intAccessLevel > 2 Then
                rtnSection.AllowDrag = True
                rtnSection.AllowDrop = True
            Else
                rtnSection.AllowDrag = False
                rtnSection.AllowDrop = False
            End If

            'if the section ID exists in the request, then if this section has the same id, set it as selected
            If intSectionID_Request = intSectionID Then
                rtnSection.Selected = True
            End If

            RadTreeSections.Nodes.Add(rtnSection)
        Next
    End Sub

    Protected Sub RadTreeSections_NodeDrop(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeDragDropEventArgs)

        If e.DraggedNodes.Count > 0 Then
            If Not e.DestDragNode Is Nothing Then


                'Get the WebInfoID's of our Dragged Row and Destination Row
                Dim webinfoID_DraggedNode As Integer = Convert.ToInt32(e.DraggedNodes(0).Value)
                Dim webinfoID_DestNode As Integer = Convert.ToInt32(e.DestDragNode.Value)

                'Get the WebInfoRows from our dataset
                Dim dtWebInfo_DraggedNode As DataTable = WebInfoDAL.GetWebInfo_ByID(webinfoID_DraggedNode)
                Dim dtWebInfo_DestNode As DataTable = WebInfoDAL.GetWebInfo_ByID(webinfoID_DestNode)

                Dim drDraggedNode As DataRow = Nothing
                If dtWebInfo_DraggedNode.Rows.Count > 0 Then
                    drDraggedNode = dtWebInfo_DraggedNode.Rows(0)
                End If

                Dim drDestNode As DataRow = Nothing
                If dtWebInfo_DestNode.Rows.Count > 0 Then
                    drDestNode = dtWebInfo_DestNode.Rows(0)
                End If

                If (Not drDraggedNode Is Nothing) And (Not drDestNode Is Nothing) Then

                    'Before we update our WebInfo Ranks, we must check both the Dragged Item and Dropped Item correspond to the same page level
                    'As currently we can only drag and drop rows belonging a page at the same page level
                    'NOTE: this is carried over from richtemplate_list_pages.aspx where items can multiple depths (SHOULD'NT BE NEEDED BUT HERE IN CASE!)
                    Dim boolDragAndDestSamePageLevel As Boolean = False

                    'Both Dragged Item and Destination Item have same parentID
                    Dim intDraggedPageLevelID As Integer = Convert.ToInt32(drDraggedNode("PageLevel"))
                    Dim intDroppedPageLevelID As Integer = Convert.ToInt32(drDestNode("PageLevel"))

                    Dim intDraggedParentID As Integer = Integer.MinValue
                    If Not drDraggedNode("ParentID") Is DBNull.Value Then
                        intDraggedParentID = Convert.ToInt32(drDraggedNode("ParentID"))
                    End If

                    Dim intDroppedParentID As Integer = Integer.MinValue
                    If Not drDestNode("ParentID") Is DBNull.Value Then
                        intDroppedParentID = Convert.ToInt32(drDestNode("ParentID"))
                    End If

                    If intDraggedPageLevelID = intDroppedPageLevelID Then
                        boolDragAndDestSamePageLevel = True
                    End If

                    'Check drag 'n drop contraints - NOTE IF IN THE FUTURE WE ALLOW THE HOME PAGE TO CHANGE POSITION, E.G HAVE IT AS THE LAST SECTION, then we can comment out the first to IF/ELSEIF Checks
                    'We allow the first page of the members and education sections to be dragged and dropped as, these are controled with access, as such the HomePage is the first page, that the user has access to, rather than the first page in the member/education list
                    Dim intDestIndex As Integer = e.DestDragNode.Index
                    Dim intDraggedIndex As Integer = e.DraggedNodes(0).Index

                    'NOTE we do not allow dragging the MAIN PUBLIC HOME PAGE, but we do allow dragging and droping Member and Education Home Pages, as the home page for the Members/Education section is the first page the user has access to, not the first page in the list, like it is in the Public site
                    If intDraggedIndex = 0 AndAlso (Not boolSecureMembers) AndAlso (Not boolSecureEducation) Then
                        'Can not drag the homepage to a new location, the homepage must always be the first section
                        Dim strDragDropError As String = "alert('" & Resources.RichTemplate_List_Sections.RichTemplate_List_Sections_Grid_DragDropError_HomePage & "');"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "drag_drop_Error", strDragDropError, True)

                    ElseIf intDestIndex = 0 AndAlso (Not boolSecureMembers) AndAlso (Not boolSecureEducation) Then
                        'Can not drop a section into the first position, as this is reserved for the home page section, the homepage must always be the first section
                        Dim strDragDropError As String = "alert('" & Resources.RichTemplate_List_Sections.RichTemplate_List_Sections_Grid_DragDropError_InFrontOfHomePage & "');"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "drag_drop_Error", strDragDropError, True)

                    ElseIf Not boolDragAndDestSamePageLevel Then 'Note this is carried over from richtemplate_list_pages.aspx where items can multiple depths (SHOULD'NT BE NEEDED BUT HERE IN CASE!)
                        'Do nothing perhaps show 'Sorry, this page can only be moved within its same page level'
                        'Register our javascript for inside our update panel
                        'AGAIN NOTE: This method was pulled over from richtemplate_list_pages.aspx and should never need to be called as all sections are on the same level
                        Dim strDragDropError As String = "alert('" & Resources.RichTemplate_List_Sections.RichTemplate_List_Sections_Grid_DragDropError_SamePageLevel & "');"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "drag_drop_Error", strDragDropError, True)
                    Else
                        'Get the Current Rank and the New Rank, these may need updating by one depending on how your selecting dropped reference row
                        Dim drDest_Rank As Integer = drDestNode("Rank")


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
                        'If dragging between another category


                        'Perform the acutal update of our WebInfo rows
                        WebInfoDAL.UpdateWebInfo_Rank(webinfoID_DraggedNode, intDroppedParentID, drDest_Rank, SiteDAL.GetCurrentSiteID_Admin())

                        'Rebind our TreeView by refreshing the iframe
                        'Use javascript to reload the welcome page and tree
                        Dim strTreeExtendedQueryString As String = ""
                        If boolSecureMembers Then
                            strTreeExtendedQueryString = "?secure_members=yes"
                        ElseIf boolSecureEducation Then
                            strTreeExtendedQueryString = "?secure_education=yes"
                        End If
                        Response.Redirect("/admin/richtemplate_list_sections.aspx" & strTreeExtendedQueryString)

                    End If
                End If
            End If
        End If

    End Sub
End Class
