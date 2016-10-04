Imports System
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Data

Partial Class _Default
    Inherits RichTemplateLanguagePage

    Dim hashMaxColumnCount As New Hashtable
    Dim intMaxPageLevel As Integer = 2
    Dim boolUseThreeColumnLayout As Boolean = False
    Dim boolWebpage_PublicSection_EnableGroupsAndUsers As Boolean = False

    Dim intWebInfoID_HomePage As Integer = Integer.MinValue
    Dim intWebInfoID_Header As Integer = Integer.MinValue
    Dim intWebInfoID_Footer As Integer = Integer.MinValue

    'This is the HOME PAGE, so secure members and secure education will ALWAYS be False
    Dim boolSecureMembers As Boolean = False
    Dim boolSecureEducation As Boolean = False

    Dim intSiteID As Integer = Integer.MinValue
    Dim strSiteName As String = String.Empty
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID and MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        Dim strCompanyName As String = ""
        If Not Page.IsPostBack Then

            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.WebInfo_FrontEnd.HomePage_HeaderTitleDefault

            'Once we have binded data nodes to our rad menu, check if we are using our 3 column layout, and if we are using additional groups and user permissions
            Dim dtSite As DataTable = SiteDAL.GetSite_ByID(intSiteID)
            If dtSite.Rows.Count > 0 Then
                Dim drSite As DataRow = dtSite.Rows(0)
                strSiteName = drSite("SiteName").ToString()
                boolUseThreeColumnLayout = Convert.ToBoolean(drSite("UseThreeColumnLayout"))
                If boolUseThreeColumnLayout Then
                    intMaxPageLevel = 2 ' if we are using the 3 column navigation the site depth must remain at 2
                Else
                    intMaxPageLevel = Convert.ToInt32(drSite("SiteDepth"))
                End If

                'Give user access to see all pages in the Radmenu, even if they have no access, access is checked when they try to view the actual page
                'boolWebpage_PublicSection_EnableGroupsAndUsers = Convert.ToBoolean(drSite("Webpage_PublicSection_EnableGroupsAndUsers"))
                boolWebpage_PublicSection_EnableGroupsAndUsers = False

                intWebInfoID_HomePage = Convert.ToInt32(drSite("WebInfoID_HomePage"))
                intWebInfoID_Header = Convert.ToInt32(drSite("WebInfoID_Header"))
                intWebInfoID_Footer = Convert.ToInt32(drSite("WebInfoID_Footer"))

                If Not drSite("companyName") Is DBNull.Value Then
                    strCompanyName = drSite("companyName").ToString().ToUpper()
                End If
                ' --- SET COMPANY DETAILS - NOT NEEDED ---
                ''Set the company statement
                'If Not drSite("CompanyStatement") Is DBNull.Value Then
                '    litCompanyStatement.Text = drSite("CompanyStatement").ToString()
                'End If

                ''Set the Phone Number
                'If Not drSite("PhoneNumber") Is DBNull.Value Then
                '    litPhoneNumber.Text = drSite("PhoneNumber").ToString()
                'End If

                ''Set the Fax Number
                'If Not drSite("FaxNumber") Is DBNull.Value Then
                '    litFaxNumber.Text = drSite("FaxNumber").ToString()
                'End If

                ''Get the Address
                'Dim strAddress As String = String.Empty
                'Dim strCity As String = String.Empty
                'Dim strStateName As String = String.Empty
                'Dim strZipCode As String = String.Empty
                'Dim strCountryName As String = String.Empty

                'If Not drSite("Address") Is DBNull.Value Then
                '    strAddress = drSite("Address").ToString()
                'End If
                'If Not drSite("City") Is DBNull.Value Then
                '    strCity = drSite("City").ToString()
                'End If
                'If Not drSite("StateName") Is DBNull.Value Then
                '    strStateName = drSite("StateName").ToString()
                'End If
                'If Not drSite("ZipCode") Is DBNull.Value Then
                '    strZipCode = drSite("ZipCode").ToString()
                'End If
                'If Not drSite("CountryName").ToString() = "" Then
                '    'This will stay empty, as we do not need to show country name
                '    'strCountryName = drSite("CountryName").ToString()
                'End If

                ''Set our Address using our Address Formatter
                'litAddressFull.Text = CommonWeb.FormatAddress(strAddress, strCity, strStateName, strZipCode, strCountryName, CommonWeb.AddressFormat.OneLine)

                'Get the Banner Image - By First Checking if we have a PageID for this page, if not, check if this page is a module page
                Dim bytesBannerImage() As Byte = CommonWeb.GetBannerImage(intWebInfoID_HomePage, intSiteID)
                If Not bytesBannerImage Is Nothing AndAlso bytesBannerImage.Count > 0 Then
                    radBannerImage.DataValue = bytesBannerImage
                    radBannerImage.Visible = True
                    divHeaderHomeBanner.Visible = True
                Else
                    Dim sHomePageWelcomeMessage_LangaugeSpecific As String = LanguageDAL.GetResourceValueForCurrentLanuage("WebInfo_FrontEnd", "HomePage_WelcomeHeading")
                    litBannerText.Text = sHomePageWelcomeMessage_LangaugeSpecific.Replace("{SITE_NAME}", drSite("SiteName").ToString())
                    divHeaderHomeText.Visible = True
                End If

                DisplayMainContent()

                LoadRadMenu()
                LoadHeaderLinks()
                LoadFooterLinks()

                DisplayFeaturedAffiliate()
                DisplayAssociations()

                ''Finally as this is the Default Page we show the site name as a bread-crumb, with no need to load it for the radmenu, header or footer like we do for master pages
                'Dim listBreadCrumbs As New List(Of KeyValuePair(Of String, String))
                'listBreadCrumbs.Add(New KeyValuePair(Of String, String)(strSiteName, "/"))
                'ucBreadCrumbsControl.LoadBreadCrumbs(listBreadCrumbs)

            End If

        End If
    End Sub

    Protected Sub LoadRadMenu()

        'Use the datatable to bind to the rad menu, and also before we start we clear out our HashTable
        hashMaxColumnCount.Clear()

        Dim dtWebInfoList As DataTable
        If boolUseThreeColumnLayout Then
            dtWebInfoList = If(boolWebpage_PublicSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfoList_FrontEnd_WithColumLayoutAndAccess(intMaxPageLevel, False, False, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), WebInfoDAL.GetWebInfoList_FrontEnd_WithColumLayout(intMaxPageLevel, False, False, intSiteID))
        Else
            dtWebInfoList = If(boolWebpage_PublicSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfoList_FrontEndAndAccess(intMaxPageLevel, False, False, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), WebInfoDAL.GetWebInfoList_FrontEnd(intMaxPageLevel, False, False, intSiteID))
        End If

        'Before we bind our data, if we are using groups and user permissions then we check each row, for access
        'If boolWebpage_PublicSection_EnableGroupsAndUsers Then
        'Now we loop though all datarows, and check they have corresponding parent, if not remove them
        dtWebInfoList = WebInfoDAL.RemoveRowsWithNoExistingParentID(dtWebInfoList)
        'End If

        'Load all menu items
        rmNavMenu.DataSource = dtWebInfoList
        rmNavMenu.DataBind()

        'PrependCountriesRadMenuItem()

        'If we are using our 3 column layout, we setup our RadNavMenu for this
        If boolUseThreeColumnLayout Then
            CommonWeb.SetupRadNavMenu_For_ThreeColumnLayout(rmNavMenu, hashMaxColumnCount)
        End If

        '' We do not want to add the last seperator, because it won't be seperating anying
        'For index = rmNavMenu.Items.Count - 1 To 0 Step -1
        '    If rmNavMenu.Items(index).Visible Then
        '        Dim rmItem As New RadMenuItem("|")
        '        rmItem.CssClass = "rmSeperator"
        '        rmNavMenu.Items.Insert(index + 1, rmItem)
        '    End If
        'Next
    End Sub

    Private Sub PrependCountriesRadMenuItem()
        'Prepend a Countries Node into this RadMenu
        Dim rdCountries As New RadMenuItem("Countries")
        rmNavMenu.Items.Insert(0, rdCountries)
        Dim dtSite As DataTable = SiteDAL.GetSiteList_FrontEnd()
        For Each drSite As DataRow In dtSite.Rows
            Dim sSiteName As String = drSite("SiteName")
            Dim sSiteUrl As String = "http://" + drSite("Domain")
            Dim sSiteIdentifier As String = ""
            If Not drSite("SiteIdentifier_LDAP") Is DBNull.Value Then
                sSiteIdentifier = drSite("SiteIdentifier_LDAP").ToString.ToUpper()
            End If
            Dim rdCountry As New RadMenuItem(sSiteName, sSiteUrl)

            'Now decide if we put this as the first item or we append it to the list
            rdCountries.Items.Add(rdCountry)
        Next
    End Sub

    Protected Sub LoadHeaderLinks()
        'Load all Header Items
        Dim dtWebInfoList_Header As DataTable = If(boolWebpage_PublicSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfoList_ByParentIDAndAccess_FrontEnd(intWebInfoID_Header, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID), WebInfoDAL.GetWebInfoList_ByParentID_FrontEnd(intWebInfoID_Header))
        rptHeader.DataSource = dtWebInfoList_Header
        rptHeader.DataBind()

        rptHeader.Visible = dtWebInfoList_Header.Rows.Count > 0

    End Sub

    Protected Sub LoadFooterLinks()
        'Load all Footer Items
        Dim dtWebInfoList_Footer As DataTable = If(boolWebpage_PublicSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfoList_ByParentIDAndAccess_FrontEnd(intWebInfoID_Footer, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID), WebInfoDAL.GetWebInfoList_ByParentID_FrontEnd(intWebInfoID_Footer))
        rptFooter.DataSource = dtWebInfoList_Footer
        rptFooter.DataBind()

        rptFooter.Visible = dtWebInfoList_Footer.Rows.Count > 0

    End Sub

    Protected Sub rmNavMenu_ItemBound(ByVal sender As Object, ByVal e As RadMenuEventArgs)
        If Not Page.IsPostBack Then

            Dim drvWebInfo As DataRowView = e.Item.DataItem
            Dim drWebInfo As DataRow = drvWebInfo.Row

            Dim iWebInfoID As Integer = Convert.ToInt32(drWebInfo("ID"))
            Dim iWebInfoID_Parent As Integer = Integer.MinValue
            If Not drWebInfo("ParentID") Is DBNull.Value Then
                iWebInfoID_Parent = Convert.ToInt32(drWebInfo("ParentID"))
            End If

            Dim strPageName As String = drWebInfo("Name")

            'Check if we are dealing with the home page, the header pagethe footer page
            Dim boolShowInMenu As Boolean = True ' By Default we show all nodes in the top menu, unless its a homepage or footer

            If iWebInfoID = intWebInfoID_HomePage Then
                strPageName = Resources.WebInfo_FrontEnd.HomePage_HomePageName
                'Comment below to show the homepage in the navigation
                boolShowInMenu = False
            ElseIf iWebInfoID = intWebInfoID_Header Or iWebInfoID = intWebInfoID_Footer Then
                boolShowInMenu = False
            ElseIf iWebInfoID_Parent = intWebInfoID_Header Or iWebInfoID_Parent = intWebInfoID_Footer Then
                'So we are now dealing with either a header item or a footer item, we do not show this in our main menu, we bind the header and footer next
                boolShowInMenu = False
            End If

            If boolShowInMenu Then ' If we allow this row to be shown in the menu we continue

                'If we are using a 3 column layout we populate a hashtable which we use in our LoadRadMenu, once we have binded our data
                If boolUseThreeColumnLayout Then
                    'Get the page level, if the page level is 1 then add a new key to the hashtable that contains an int array

                    Dim iPageLevel As Integer = Convert.ToInt32(drWebInfo("PageLevel"))
                    'If the web info is page level 1 then we create an integer array with 3 ints representing the rows in each column

                    If iPageLevel = 2 Then
                        'Now we get the int array and depend on the webinfos designated column we add it to the appropriate index of the array
                        Dim iNavigationColumnIndex As Integer = Convert.ToInt32(drWebInfo("NavigationColumnIndex"))
                        Dim iListMaxColumnCount() As Integer = hashMaxColumnCount(iWebInfoID_Parent)
                        If iListMaxColumnCount Is Nothing Then
                            ReDim iListMaxColumnCount(2)
                        End If
                        'Adjust the value and re-add it to the hashtable
                        Select Case iNavigationColumnIndex
                            Case 1
                                iListMaxColumnCount(0) = iListMaxColumnCount(0) + 1
                            Case 2
                                iListMaxColumnCount(1) = iListMaxColumnCount(1) + 1
                            Case 3
                                iListMaxColumnCount(2) = iListMaxColumnCount(2) + 1
                        End Select
                        hashMaxColumnCount(iWebInfoID_Parent) = iListMaxColumnCount
                    End If

                End If

                e.Item.Text = strPageName
                e.Item.Value = iWebInfoID

                If Not drWebInfo("linkURL") Is DBNull.Value Then

                    e.Item.NavigateUrl = drWebInfo("linkURL").ToString()
                    'If there is a link saved in the db then don't build a link 
                    If Not drWebInfo("linkFrame") Is DBNull.Value Then
                        e.Item.Target = drWebInfo("linkFrame").ToString()
                    End If
                Else
                    'Build the link 
                    Dim strPageName_Parent As String = drWebInfo("parentName").ToString()
                    e.Item.NavigateUrl = WebInfoDAL.GetWebInfoUrl(strPageName, strPageName_Parent, If(boolSecureMembers, "Member", If(boolSecureEducation, "Education", String.Empty)))

                End If

            Else
                e.Item.Visible = False
            End If
        End If

    End Sub

    Protected Sub rptHeader_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptHeader.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim drvWebInfo As DataRowView = e.Item.DataItem
            Dim drWebInfo As DataRow = drvWebInfo.Row

            Dim iWebInfoID As Integer = Convert.ToInt32(drWebInfo("ID"))
            Dim iWebInfoID_Parent As Integer = Integer.MinValue

            If Not drWebInfo("ParentID") Is DBNull.Value Then
                iWebInfoID_Parent = Convert.ToInt32(drWebInfo("ParentID"))
            End If

            Dim strPageName As String = drWebInfo("Name")
            Dim strPageName_Parent As String = String.Empty ' This is always empty as we are processing header pages, and we want them to sit in the root of the site
            Dim sNameLink As String = String.Empty
            Dim sNameLinkTarget As String = String.Empty

            If Not drWebInfo("linkURL") Is DBNull.Value Then

                sNameLink = drWebInfo("linkURL").ToString()
                'If there is a link saved in the db then don't build a link 
                If Not drWebInfo("linkFrame") Is DBNull.Value Then
                    sNameLinkTarget = drWebInfo("linkFrame").ToString()
                End If
            Else
                'Build the link 
                sNameLink = WebInfoDAL.GetWebInfoUrl(strPageName, strPageName_Parent, If(boolSecureMembers, "Member", If(boolSecureEducation, "Education", String.Empty)))
            End If

            'Now we have the header link's name and url
            Dim aHeaderLink As HtmlAnchor = e.Item.FindControl("aHeaderLink")
            aHeaderLink.HRef = sNameLink
            aHeaderLink.Target = If(sNameLinkTarget.Length > 0, "_blank", "")

            Dim litHeaderLink As Literal = e.Item.FindControl("litHeaderLink")
            litHeaderLink.Text = strPageName
        End If

    End Sub

    Protected Sub rptFooter_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptFooter.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim drvWebInfo As DataRowView = e.Item.DataItem
            Dim drWebInfo As DataRow = drvWebInfo.Row

            Dim iWebInfoID As Integer = Convert.ToInt32(drWebInfo("ID"))
            Dim iWebInfoID_Parent As Integer = Integer.MinValue

            If Not drWebInfo("ParentID") Is DBNull.Value Then
                iWebInfoID_Parent = Convert.ToInt32(drWebInfo("ParentID"))
            End If

            Dim strPageName As String = drWebInfo("Name")
            Dim strPageName_Parent As String = String.Empty ' This is always empty as we are processing footer pages, and we want them to sit in the root of the site
            Dim sNameLink As String = String.Empty
            Dim sNameLinkTarget As String = String.Empty

            If Not drWebInfo("linkURL") Is DBNull.Value Then

                sNameLink = drWebInfo("linkURL").ToString()
                'If there is a link saved in the db then don't build a link 
                If Not drWebInfo("linkFrame") Is DBNull.Value Then
                    sNameLinkTarget = drWebInfo("linkFrame").ToString()
                End If
            Else
                'Build the link 
                sNameLink = WebInfoDAL.GetWebInfoUrl(strPageName, strPageName_Parent, If(boolSecureMembers, "Member", If(boolSecureEducation, "Education", String.Empty)))
            End If

            'Now we have the footer link's name and url
            Dim aFooterLink As HtmlAnchor = e.Item.FindControl("aFooterLink")
            aFooterLink.HRef = sNameLink
            aFooterLink.Target = If(sNameLinkTarget.Length > 0, "_blank", "")

            Dim litFooterLink As Literal = e.Item.FindControl("litFooterLink")
            litFooterLink.Text = strPageName
        End If

    End Sub

    Protected Sub DisplayMainContent()

        'Use this to populate the MainContent Literal with the Message from the below PageID
        Dim dtWebInfo_MainContent As DataTable = If(boolWebpage_PublicSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfo_ByIDAndAccess_FrontEnd(intWebInfoID_HomePage, MemberDAL.GetCurrentMemberGroupIDs, intMemberID), WebInfoDAL.GetWebInfo_ByID_FrontEnd(intWebInfoID_HomePage))
        If dtWebInfo_MainContent.Rows.Count > 0 Then
            Dim drWebInfo_MainContent As DataRow = dtWebInfo_MainContent.Rows(0)

            'If the message is not null add it to the page
            If Not drWebInfo_MainContent("Message") Is DBNull.Value Then
                litMainContent.Text = drWebInfo_MainContent("Message")
            Else
                litMainContent.Text = Resources.WebInfo_FrontEnd.HomePage_NoContentCreatedYet
            End If

            'set the page title
            Page.Header.Title = SiteDAL.GetCompanyName()
            If Not drWebInfo_MainContent("metaTitle") Is DBNull.Value Then
                Dim strMetaTitle As String = drWebInfo_MainContent("metaTitle").ToString()
                If strMetaTitle.Length > 0 Then
                    Page.Header.Title = strMetaTitle
                End If
            End If

            'set the page's keywords
            If Not drWebInfo_MainContent("metaKeyword") Is DBNull.Value Then
                Dim keywordsHtmlMeta As New HtmlMeta()
                keywordsHtmlMeta.Name = "keywords"
                keywordsHtmlMeta.Content = drWebInfo_MainContent("metaKeyword").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(keywordsHtmlMeta)
            End If

            'set the page's description
            If Not drWebInfo_MainContent("metaDesc") Is DBNull.Value Then
                Dim descriptionHtmlMeta As New HtmlMeta()
                descriptionHtmlMeta.Name = "description"
                descriptionHtmlMeta.Content = drWebInfo_MainContent("metaDesc").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(descriptionHtmlMeta)
            End If

        Else
            'Redirect to the login page
            Dim strSiteWideLoginUrl As String = "~/login/"
            If Not ConfigurationManager.AppSettings("SiteWideLoginURL") Is Nothing Then
                strSiteWideLoginUrl = ConfigurationManager.AppSettings("SiteWideLoginURL")
            End If
            Response.Redirect(strSiteWideLoginUrl)

        End If

    End Sub

    Protected Sub DisplayFeaturedAffiliate()

        ''Use this to populate the FeaturedAffiliateContent Literal with the Message from the below PageID
        'Dim dtWebInfo_FeaturedAffiliateContent As DataTable = WebInfoDAL.GetWebInfo_ByID(FeaturedAffiliateContent_WebInfoID)
        'If dtWebInfo_FeaturedAffiliateContent.Rows.Count > 0 Then
        '    Dim drWebInfo_FeaturedAffiliateContent As DataRow = dtWebInfo_FeaturedAffiliateContent.Rows(0)
        '    If Not drWebInfo_FeaturedAffiliateContent("Message") Is DBNull.Value Then
        '        litFeaturedAffiliateContent.Text = drWebInfo_FeaturedAffiliateContent("Message")
        '    End If
        'End If

    End Sub

    Protected Sub DisplayAssociations()

        ''Use this to populate the AssociationContent Literal with the Message from the below PageID
        'Dim dtWebInfo_AssociationContent As DataTable = WebInfoDAL.GetWebInfo_ByID(AssociationContent_WebInfoID)
        'If dtWebInfo_AssociationContent.Rows.Count > 0 Then
        '    Dim drWebInfo_AssociationContent As DataRow = dtWebInfo_AssociationContent.Rows(0)
        '    If Not drWebInfo_AssociationContent("Message") Is DBNull.Value Then
        '        litAssociationContent.Text = drWebInfo_AssociationContent("Message")

        '    End If
        'End If

    End Sub

End Class

