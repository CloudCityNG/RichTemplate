Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports Telerik.Web.UI

Partial Public Class SiteMap
    Inherits RichTemplateLanguagePage

    Dim intMaxPageLevel As Integer = 2
    Dim boolWebpage_PublicSection_EnableGroupsAndUsers As Boolean = False
    Dim boolWebpage_MemberSection_EnableGroupsAndUsers As Boolean = False

    Dim intWebInfoID_HomePage As Integer = Integer.MinValue
    Dim intWebInfoID_Header As Integer = Integer.MinValue
    Dim intWebInfoID_Footer As Integer = Integer.MinValue
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'Set the site maps browser title
        Page.Header.Title = SiteDAL.GetCompanyName() & " | " & Resources.SiteMap_FrontEnd.SiteMap_HeaderTitle

        'Find out if we are using a 3 columns layout, in which case maxPageLevel must be 2, and also check if our webpages are using groups/user permissions
        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(SiteDAL.GetCurrentSiteID_FrontEnd())
        If dtSite.Rows.Count > 0 Then
            Dim drSite As DataRow = dtSite.Rows(0)
            Dim boolUseThreeColumnLayout As Boolean = Convert.ToBoolean(drSite("UseThreeColumnLayout"))
            If boolUseThreeColumnLayout Then
                intMaxPageLevel = 2 ' if we are using the 3 column navigation the site depth must remain at 2
            Else
                intMaxPageLevel = Convert.ToInt32(drSite("SiteDepth"))
            End If

            'Give user access to see all pages in the Radmenu, even if they have no access, access is checked when they try to view the actual page
            'boolWebpage_PublicSection_EnableGroupsAndUsers = Convert.ToBoolean(drSite("Webpage_PublicSection_EnableGroupsAndUsers"))
            boolWebpage_PublicSection_EnableGroupsAndUsers = False

            boolWebpage_MemberSection_EnableGroupsAndUsers = Convert.ToBoolean(drSite("Webpage_MemberSection_EnableGroupsAndUsers"))

            intWebInfoID_HomePage = Convert.ToInt32(drSite("WebInfoID_HomePage"))
            intWebInfoID_Header = Convert.ToInt32(drSite("WebInfoID_Header"))
            intWebInfoID_Footer = Convert.ToInt32(drSite("WebInfoID_Footer"))
        End If

        'First Clear all nodes from our tree, so we start with a fresh tree
        rtvSiteMap.Nodes.Clear()

        'Now get the public pages
        Dim dtWebInfo_PublicSection As DataTable = If(boolWebpage_PublicSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfoList_FrontEndAndAccess_ForSiteMap(intMaxPageLevel, False, False, MemberDAL.GetCurrentMemberGroupIDs(), MemberDAL.GetCurrentMemberID(), SiteDAL.GetCurrentSiteID_FrontEnd()), WebInfoDAL.GetWebInfoList_FrontEnd_ForSiteMap(intMaxPageLevel, False, False, SiteDAL.GetCurrentSiteID_FrontEnd()))

        'Before we bind our data, if we are using groups and user permissions then we check each row, for access
        If boolWebpage_PublicSection_EnableGroupsAndUsers Then
            'Now we loop though all datarows, and check they have corresponding parent, if not remove them
            dtWebInfo_PublicSection = WebInfoDAL.RemoveRowsWithNoExistingParentID(dtWebInfo_PublicSection)
        End If

        'Populate our public section
        PopulateRadTree(dtWebInfo_PublicSection)

        'Now populte any header pages
        Dim dtWebInfo_HeaderSection As DataTable = If(boolWebpage_PublicSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfoList_ByParentIDAndAccess_FrontEnd(intWebInfoID_Header, MemberDAL.GetCurrentMemberGroupIDs(), MemberDAL.GetCurrentMemberID()), WebInfoDAL.GetWebInfoList_ByParentID_FrontEnd(intWebInfoID_Header))

        'Populate our Header Container
        PopulateRadTree(dtWebInfo_HeaderSection)

        'Now populte any footer pages
        Dim dtWebInfo_FooterSection As DataTable = If(boolWebpage_PublicSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfoList_ByParentIDAndAccess_FrontEnd(intWebInfoID_Footer, MemberDAL.GetCurrentMemberGroupIDs(), MemberDAL.GetCurrentMemberID()), WebInfoDAL.GetWebInfoList_ByParentID_FrontEnd(intWebInfoID_Footer))

        'Populate our Footer Container
        PopulateRadTree(dtWebInfo_FooterSection)

        'Now get the member pages, if the user is logged in
        If MemberDAL.GetCurrentMemberID() > 0 Then
            Dim dtWebInfo_MemberSection As DataTable = If(boolWebpage_MemberSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfoList_FrontEndAndAccess_ForSiteMap(intMaxPageLevel, True, False, MemberDAL.GetCurrentMemberGroupIDs(), MemberDAL.GetCurrentMemberID(), SiteDAL.GetCurrentSiteID_FrontEnd()), WebInfoDAL.GetWebInfoList_FrontEnd_ForSiteMap(intMaxPageLevel, True, False, SiteDAL.GetCurrentSiteID_FrontEnd()))

            'Before we bind our data, if we are using groups and user permissions then we check each row, for access
            If boolWebpage_MemberSection_EnableGroupsAndUsers Then
                'Now we loop though all datarows, and check they have corresponding parent, if not remove them
                dtWebInfo_MemberSection = WebInfoDAL.RemoveRowsWithNoExistingParentID(dtWebInfo_MemberSection)
            End If

            'Populate our members section
            PopulateRadTree(dtWebInfo_MemberSection)
        End If

    End Sub

    Private Sub PopulateRadTree(ByVal dtWebInfoList As DataTable)

        For Each drWebInfo As DataRow In dtWebInfoList.Rows

            Dim intWebInfoID As Integer = Convert.ToInt32(drWebInfo("ID"))

            Dim intWebInfoID_Parent As Integer = Integer.MinValue
            If Not drWebInfo("ParentID") Is DBNull.Value Then
                intWebInfoID_Parent = Convert.ToInt32(drWebInfo("ParentID"))
            End If

            'We show the homepage in this sitemap, but if the parentID of the webpage we are processing is the homepage, then do not add any of their children, we process the the footer at the END of the public section
            If intWebInfoID_Parent <> intWebInfoID_HomePage Then

                Dim strWebInfoPageName As String = String.Empty
                strWebInfoPageName = If(intWebInfoID = intWebInfoID_HomePage, Resources.SiteMap_FrontEnd.SiteMap_HomePageName, drWebInfo("Name").ToString())

                Dim strWebInfoURL As String = String.Empty

                If Not drWebInfo("LinkURL") Is DBNull.Value Then
                    strWebInfoURL = drWebInfo("linkURL").ToString()

                Else
                    'Build the link 
                    Dim strWebInfoPageNameParent As String = drWebInfo("ParentName").ToString()

                    'Note if its a member page we append member/, if its an education page append education/
                    Dim boolIsSecureMember As Boolean = drWebInfo("secure_Members")
                    Dim boolIsSecureEducation As Boolean = drWebInfo("secure_Education")
                    Dim strWebInfoURL_Prefix As String = If(boolIsSecureMember, "Member", If(boolIsSecureEducation, "Education", String.Empty))

                    strWebInfoURL = WebInfoDAL.GetWebInfoUrl(If(intWebInfoID = intWebInfoID_HomePage, String.Empty, strWebInfoPageName), If(intWebInfoID = intWebInfoID_Header Or intWebInfoID = intWebInfoID_Footer, String.Empty, strWebInfoPageNameParent), strWebInfoURL_Prefix)

                End If

                'Now with our page information, create a siteMapNode that we will add to our SiteMap Tree, all nodes are fully expanded, and can not be compressed
                Dim rtvSiteMapNode As New RadTreeNode(strWebInfoPageName, intWebInfoID, strWebInfoURL)
                rtvSiteMapNode.Expanded = True
                rtvSiteMapNode.Target = "_blank"
                rtvSiteMapNode.ExpandMode = TreeNodeExpandMode.ServerSide

                'Try and find the current pages parent node, if it exists
                Dim rtvSiteMapNodeParent As RadTreeNode = Nothing
                If intWebInfoID_Parent > Integer.MinValue Then
                    rtvSiteMapNodeParent = rtvSiteMap.FindNodeByValue(intWebInfoID_Parent)
                End If

                'If the current page has no parent node, then we simply add it to the tree root, else we add the new node to the parent node
                If rtvSiteMapNodeParent Is Nothing Then
                    rtvSiteMap.Nodes.Add(rtvSiteMapNode)
                Else
                    rtvSiteMapNodeParent.Nodes.Add(rtvSiteMapNode)
                End If
            End If

        Next

    End Sub

End Class