Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Partial Class searchTags_SearchResults
    Inherits System.Web.UI.UserControl

    Dim boolWebpage_PublicSection_EnableGroupsAndUsers As Boolean = False
    Dim boolWebpage_MemberSection_EnableGroupsAndUsers As Boolean = False

    Dim intWebInfoID_HomePage As Integer = Integer.MinValue
    Dim intWebInfoID_Footer As Integer = Integer.MinValue

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        If Not IsPostBack Then

        End If

    End Sub

    Protected Sub rlvSearchResults_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.RadListViewNeedDataSourceEventArgs) Handles rlvSearchResults.NeedDataSource

        If Not Request.QueryString("search") Is Nothing Then
            Dim strSearchTagIDList_Encoded As String = Request.QueryString("search")
            Dim strSearchTagIDList As String = CommonWeb.DecodeBase64String(strSearchTagIDList_Encoded)

            If strSearchTagIDList.Length > 0 Then
                'Populate the search tag names
                Dim sbSearchTagNameList As New StringBuilder()
                Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsList_BySearchTagIDs(strSearchTagIDList)
                For Each drSearchTag As DataRow In dtSearchTags.Rows
                    Dim strSearchTagName As String = drSearchTag("SearchTagName")
                    sbSearchTagNameList.Append(If(sbSearchTagNameList.Length > 0, ", " & strSearchTagName, strSearchTagName))
                Next
                litResults.Text = Resources.Search_FrontEnd.SearchTags_SearchResults_SearchTagsToSearch & ": <b>" & sbSearchTagNameList.ToString() & "</b><br /><br/>"
                litResults.Visible = True

                Dim hashModuleAndRecordsForDuplicateCheck As New Hashtable()
                Dim hashModuleAndRecordsUnique As New Hashtable()
                'gives us a list of all modules/records that have a searchTag of what we are searching for
                Dim dtSearchTagsXRef As DataTable = SearchTagDAL.GetSearchTagsXRef_List_WithSearchTagIDs(strSearchTagIDList)
                For Each drSearchTagsXRef As DataRow In dtSearchTagsXRef.Rows
                    Dim intModuleTypeID As Integer = Convert.ToInt32(drSearchTagsXRef("moduleTypeID"))
                    Dim intRecordID As Integer = Convert.ToInt32(drSearchTagsXRef("recordID"))

                    'If this moduleTypeID and record ID is not yet in our hashtable we should add it, otherwise its a duplicate we do not process this
                    If hashModuleAndRecordsForDuplicateCheck(intModuleTypeID & ":" & intRecordID) Is Nothing Then
                        hashModuleAndRecordsForDuplicateCheck(intModuleTypeID & ":" & intRecordID) = "" 'set this as empty string, as we are only using this to check for duplicates

                        'Also as this record is not added to our unique list we add it too
                        Dim strRecordIDList As String = ""
                        If Not hashModuleAndRecordsUnique(intModuleTypeID) Is Nothing Then
                            strRecordIDList = hashModuleAndRecordsUnique(intModuleTypeID) & ","
                        End If
                        hashModuleAndRecordsUnique(intModuleTypeID) = strRecordIDList & intRecordID

                    End If

                Next

                'Now go through each module, getting the records we need to process
                Dim dtSearchInformation As DataTable = GetSearchInformationForModuleRecords(hashModuleAndRecordsUnique)


                rlvSearchResults.DataSource = dtSearchInformation

            End If
        End If

    End Sub

    Private Function GetSearchInformationForModuleRecords(ByVal hashModuleAndRecordsUnique As Hashtable) As DataTable

        Dim dtSearchInformation As New DataTable
        dtSearchInformation.Columns.Add("ModuleTypeID")
        dtSearchInformation.Columns.Add("ModuleHeading")
        dtSearchInformation.Columns.Add("RecordID")
        dtSearchInformation.Columns.Add("RecordTitle")
        dtSearchInformation.Columns.Add("RecordSummary")
        dtSearchInformation.Columns.Add("RecordUrl")
        dtSearchInformation.Columns.Add("RecordViewDate")

        Dim dtModules As DataTable = ModuleDAL.GetModuleList_BySiteID_FrontEnd(intSiteID)

        'Add Web Page as moduleTypeID = 0
        Dim strModuleName_Public As String = Resources.Search_FrontEnd.SearchTags_SearchResults_Module_WebPage_Public
        Dim strModuleName_Member As String = Resources.Search_FrontEnd.SearchTags_SearchResults_Module_WebPage_Member 'Change this to 'Web Page(s)' if you want to list both public and members pages into one integrated order list
        Dim drWebPage As DataRow = dtModules.NewRow()
        drWebPage("ModuleTypeID") = 0
        dtModules.Rows.InsertAt(drWebPage, 0)
        For Each drModule As DataRow In dtModules.Rows
            Dim intModuleTypeID As Integer = Convert.ToInt32(drModule("moduleTypeID"))

            'Note as the WebPage and Member WebPage are not actually a module, we pre-populate its moduleName using our resource files
            'however it has no moduleLanguageFileName_FrontEnd, so we default the language specific module name to the moduleName, all others will have language specific module name
            Dim strModuleName_LanguageSpecific As String = String.Empty
            If Not drModule("moduleLanguageFilename_FrontEnd") Is DBNull.Value Then
                'Get the name of the module, based on the users preferred language
                Dim strModuleLanguageFilename_FrontEnd As String = drModule("moduleLanguageFilename_FrontEnd")
                strModuleName_LanguageSpecific = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_FrontEnd, "_SiteWide_SearchTags_ModuleName")
            End If



            'Get the records for each module and create their datarow's 
            If Not hashModuleAndRecordsUnique(intModuleTypeID) Is Nothing Then

                Dim strRecordIDList As String = hashModuleAndRecordsUnique(intModuleTypeID)
                'Get all active Front-End records for this particular module in a datatable, then filter by recordID
                Select Case intModuleTypeID

                    Case 0 ' WebInfo

                        'When looking at the we infos, we only show the pages upto a certail max level
                        Dim intMaxPageLevel As Integer = 2
                        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(intSiteID)
                        If dtSite.Rows.Count > 0 Then
                            Dim drSite As DataRow = dtSite.Rows(0)
                            intMaxPageLevel = Convert.ToInt32(drSite("SiteDepth"))

                            'Give user access to see all pages in the Radmenu, even if they have no access, access is checked when they try to view the actual page
                            'boolWebpage_PublicSection_EnableGroupsAndUsers = Convert.ToBoolean(drSite("Webpage_PublicSection_EnableGroupsAndUsers"))
                            boolWebpage_PublicSection_EnableGroupsAndUsers = False

                            boolWebpage_MemberSection_EnableGroupsAndUsers = Convert.ToBoolean(drSite("Webpage_MemberSection_EnableGroupsAndUsers"))

                            intWebInfoID_HomePage = Convert.ToInt32(drSite("WebInfoID_HomePage"))
                            intWebInfoID_Footer = Convert.ToInt32(drSite("WebInfoID_Footer"))

                        End If

                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        ' HANDLE Public PAGES - If you want public pages and member pages to be integrated inside one ordered list remove this section, and use WebInfoDAL.GetWebInfoList once for public then do it again for members and do a MERGE, also change the webpages heading from 'strModuleName_Member' to 'strModuleName_Public'
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        'Get the list of web infos
                        Dim dtWebInfo_Public As DataTable = If(boolWebpage_PublicSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfoList_FrontEndAndAccess(intMaxPageLevel, False, False, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), WebInfoDAL.GetWebInfoList_FrontEnd(intMaxPageLevel, False, False, intSiteID))
                        Dim dvWebInfo_Public As New DataView(dtWebInfo_Public)

                        'Only include records in this recordID list
                        dvWebInfo_Public.RowFilter = "ID In (" & strRecordIDList & ")"
                        dvWebInfo_Public.Sort = "Name"

                        'Loop through all records adding rows to dtSearchInformation
                        For intRowIndex = 0 To dvWebInfo_Public.Count - 1
                            Dim drWebInfo As DataRow = dvWebInfo_Public(intRowIndex).Row

                            Dim intWebInfo As Integer = Convert.ToInt32(drWebInfo("ID"))
                            Dim strMessageSummary As String = drWebInfo("Message")

                            'Only continue, if the we have content in our message field, as it my not yet be published yet
                            If strMessageSummary.Length > 0 Then

                                Dim strWebpageName As String = If(intWebInfo = intWebInfoID_HomePage, Resources.Search_FrontEnd.SearchTags_SearchResults_Module_WebPage_Public_HomePageName, drWebInfo("Name").ToString())
                                Dim strWebpageName_Parent As String = String.Empty
                                If Not drWebInfo("ParentName") Is DBNull.Value Then
                                    strWebpageName_Parent = drWebInfo("ParentName").ToString()
                                End If

                                Dim drSearchInformation As DataRow = dtSearchInformation.NewRow()
                                drSearchInformation("ModuleTypeID") = intModuleTypeID
                                drSearchInformation("ModuleHeading") = If(intRowIndex = 0, strModuleName_Public, "") 'Only added if its the first row
                                drSearchInformation("RecordID") = intWebInfo
                                drSearchInformation("RecordTitle") = strWebpageName
                                drSearchInformation("RecordSummary") = CommonWeb.stripHTMLandLimitWordCount(strMessageSummary, 25)
                                drSearchInformation("RecordUrl") = WebInfoDAL.GetWebInfoUrl(If(intWebInfo = intWebInfoID_HomePage, String.Empty, strWebpageName), strWebpageName_Parent, String.Empty) ' NOTE if the page is the homepage, we just set the url to the root of the site
                                drSearchInformation("RecordViewDate") = If(Not drWebInfo("Last_Modified") Is Nothing, Convert.ToDateTime(drWebInfo("Last_Modified")).ToString(), "")

                                dtSearchInformation.Rows.Add(drSearchInformation)
                            End If

                        Next

                        'Also check footer pages, and if any footer pages contain the required search tags, then add it to this list to show directly below the public pages
                        Dim dtWebInfo_Footer As DataTable = If(boolWebpage_PublicSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfoList_ByParentIDAndAccess_FrontEnd(intWebInfoID_Footer, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID), WebInfoDAL.GetWebInfoList_ByParentID_FrontEnd(intWebInfoID_Footer))
                        Dim dvWebInfo_Footer As New DataView(dtWebInfo_Footer)

                        'Only include records in this recordID list
                        dvWebInfo_Footer.RowFilter = "ID In (" & strRecordIDList & ")"
                        dvWebInfo_Footer.Sort = "Name"

                        'Loop through all records adding rows to dtSearchInformation
                        For intRowIndex = 0 To dvWebInfo_Footer.Count - 1
                            Dim drWebInfo As DataRow = dvWebInfo_Footer(intRowIndex).Row

                            Dim intWebInfo As Integer = Convert.ToInt32(drWebInfo("ID"))
                            Dim strMessageSummary As String = drWebInfo("Message")

                            'Only continue, if the we have content in our message field, as it my not yet be published yet
                            If strMessageSummary.Length > 0 Then

                                Dim strWebpageName As String = drWebInfo("Name").ToString()
                                Dim strWebpageName_Parent As String = String.Empty ' BECAUSE WE ARE populating footer pages, we do not include the parentName in the url, as we REQUIRE footer url's to be at the root of the site

                                Dim drSearchInformation As DataRow = dtSearchInformation.NewRow()
                                drSearchInformation("ModuleTypeID") = intModuleTypeID
                                drSearchInformation("ModuleHeading") = If(dvWebInfo_Public.Count = 0 AndAlso intRowIndex = 0, strModuleName_Public, "") 'Only added if its the first row, and we do not have any rows in our dvWebInfo_Publlic dataVIEW above, as we are combining our public pages with our footer page results and we don't want the heading to appera twice
                                drSearchInformation("RecordID") = intWebInfo
                                drSearchInformation("RecordTitle") = strWebpageName
                                drSearchInformation("RecordSummary") = CommonWeb.stripHTMLandLimitWordCount(strMessageSummary, 25)
                                drSearchInformation("RecordUrl") = WebInfoDAL.GetWebInfoUrl(strWebpageName, strWebpageName_Parent, String.Empty)
                                drSearchInformation("RecordViewDate") = If(Not drWebInfo("Last_Modified") Is Nothing, Convert.ToDateTime(drWebInfo("Last_Modified")).ToString(), "")

                                dtSearchInformation.Rows.Add(drSearchInformation)
                            End If

                        Next


                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        ' HANDLE Member PAGES
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        'Get the list of web infos but only if the member is logged int
                        If intMemberID > 0 Then
                            Dim dtWebInfo_Members As DataTable = If(boolWebpage_MemberSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfoList_FrontEndAndAccess(intMaxPageLevel, True, False, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), WebInfoDAL.GetWebInfoList_FrontEnd(intMaxPageLevel, True, False, intSiteID))
                            Dim dvWebInfo_Members As New DataView(dtWebInfo_Members)
                            dvWebInfo_Members.RowFilter = "ID In (" & strRecordIDList & ") And PageLevel <= " & intMaxPageLevel
                            dvWebInfo_Members.Sort = "Name"

                            'Loop through all records adding rows to dtSearchInformation
                            For intRowIndex = 0 To dvWebInfo_Members.Count - 1
                                Dim drWebInfo As DataRow = dvWebInfo_Members(intRowIndex).Row

                                Dim intWebInfo As Integer = Convert.ToInt32(drWebInfo("ID"))
                                Dim strMessageSummary As String = drWebInfo("Message")

                                'Only continue, if the we have content in our message field, as it my not yet be published yet
                                If strMessageSummary.Length > 0 Then

                                    Dim boolIsSecureMember As Boolean = Convert.ToBoolean(drWebInfo("secure_members")) 'always true, unless we are integrating both public and members pages into one ordered list

                                    'Finally before we add this webinfo page, check the user has access to this page
                                    Dim strRecordPrependedUrl As String = ""
                                    Dim boolHasAccess As Boolean = True 'default to true, but if its a secure page, we check access
                                    If boolIsSecureMember Then
                                        strRecordPrependedUrl = "Member"
                                    End If

                                    Dim strWebpageName As String = drWebInfo("Name").ToString()
                                    Dim strWebpageName_Parent As String = String.Empty
                                    If Not drWebInfo("ParentName") Is DBNull.Value Then
                                        strWebpageName_Parent = drWebInfo("ParentName").ToString()
                                    End If

                                    Dim drSearchInformation As DataRow = dtSearchInformation.NewRow()
                                    drSearchInformation("ModuleTypeID") = intModuleTypeID
                                    drSearchInformation("ModuleHeading") = If(intRowIndex = 0, strModuleName_Member, "") 'Only added if its the first row
                                    drSearchInformation("RecordID") = intWebInfo
                                    drSearchInformation("RecordTitle") = drWebInfo("Name")
                                    drSearchInformation("RecordSummary") = CommonWeb.stripHTMLandLimitWordCount(strMessageSummary, 25)
                                    drSearchInformation("RecordUrl") = WebInfoDAL.GetWebInfoUrl(strWebpageName, strWebpageName_Parent, strRecordPrependedUrl)
                                    drSearchInformation("RecordViewDate") = If(Not drWebInfo("Last_Modified") Is Nothing, Convert.ToDateTime(drWebInfo("Last_Modified")).ToString(), "")

                                    dtSearchInformation.Rows.Add(drSearchInformation)
                                End If

                            Next
                        End If


                    Case 1 ' This is the Blog Module

                        'First check if the blog module has user/group permissions enabled
                        Dim boolEnableUserGroupPermissions_Blog As Boolean = False
                        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(intModuleTypeID, intSiteID)
                        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                                boolEnableUserGroupPermissions_Blog = True
                            End If
                        Next

                        'Get a list of ALL ACTIVE blogs, the filter based on the recordID's from our search
                        Dim dtBlog As DataTable = If(boolEnableUserGroupPermissions_Blog, BlogDAL.GetBlog_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlog_ByStatus_FrontEnd(True, intSiteID))
                        Dim dvBlog As New DataView(dtBlog)
                        dvBlog.RowFilter = "blogID In (" & strRecordIDList & ")"

                        'Loop through all records adding rows to dtSearchInformation
                        For intRowIndex = 0 To dvBlog.Count - 1
                            Dim drBlog As DataRow = dvBlog(intRowIndex).Row

                            Dim intBlogID As Integer = Convert.ToInt32(drBlog("blogID"))
                            Dim strModuleHeading As String = If(intRowIndex = 0, strModuleName_LanguageSpecific, "") 'Only added if its the first row
                            Dim strBlogTitle As String = drBlog("Title")
                            'Dim strBlogSummary As String = drBlog("Summary") 'NOTE, blogs don't really use Summary
                            Dim strBlogBody As String = drBlog("Body") 'NOTE, blogs don't really use Summary
                            Dim strBlogURL As String = "/Blog/?id=" & intBlogID
                            Dim strBlogViewDate As String = drBlog("ViewDate")

                            'Create a new SearchInformation row and add our blog details to the row
                            Dim drSearchInformation As DataRow = dtSearchInformation.NewRow()
                            drSearchInformation("ModuleTypeID") = intModuleTypeID
                            drSearchInformation("ModuleHeading") = strModuleHeading
                            drSearchInformation("RecordID") = intBlogID
                            drSearchInformation("RecordTitle") = strBlogTitle
                            drSearchInformation("RecordSummary") = CommonWeb.stripHTMLandLimitWordCount(strBlogBody, 25)
                            drSearchInformation("RecordUrl") = strBlogURL
                            drSearchInformation("RecordViewDate") = strBlogViewDate

                            dtSearchInformation.Rows.Add(drSearchInformation)
                        Next


                    Case 2 ' This is the comment Module, We do not include comments in the search results

                    Case 3 ' This is the Document Library Module

                        'First check if the document library module has user/group permissions enabled
                        Dim boolEnableUserGroupPermissions_DocumentLibrary As Boolean = False
                        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(intModuleTypeID, intSiteID)
                        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                                boolEnableUserGroupPermissions_DocumentLibrary = True
                            End If
                        Next

                        'Get a list of ALL ACTIVE documents, the filter based on the recordID's from our search
                        Dim dtDocument As DataTable = If(boolEnableUserGroupPermissions_DocumentLibrary, DocumentDAL.GetDocumentList_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), DocumentDAL.GetDocumentList_ByStatus_FrontEnd(True, intSiteID))
                        Dim dvDocument As New DataView(dtDocument)
                        dvDocument.RowFilter = "documentID In (" & strRecordIDList & ")"

                        'Loop through all records adding rows to dtSearchInformation
                        For intRowIndex = 0 To dvDocument.Count - 1
                            Dim drDocument As DataRow = dvDocument(intRowIndex).Row

                            Dim intDocumentID As Integer = Convert.ToInt32(drDocument("documentID"))
                            Dim strModuleHeading As String = If(intRowIndex = 0, strModuleName_LanguageSpecific, "") 'Only added if its the first row
                            Dim strDocumentTitle As String = drDocument("fileTitle")
                            Dim strDocumentSummary As String = drDocument("fileDescription")
                            Dim strDocumentURL As String = "/DocumentLibrary/DocumentDetail.aspx?id=" & intDocumentID
                            Dim strDocumentViewDate As String = drDocument("ViewDate")

                            'Create a new SearchInformation row and add our document details to the row
                            Dim drSearchInformation As DataRow = dtSearchInformation.NewRow()
                            drSearchInformation("ModuleTypeID") = intModuleTypeID
                            drSearchInformation("ModuleHeading") = strModuleHeading
                            drSearchInformation("RecordID") = intDocumentID
                            drSearchInformation("RecordTitle") = strDocumentTitle
                            drSearchInformation("RecordSummary") = CommonWeb.stripHTMLandLimitWordCount(strDocumentSummary, 25)
                            drSearchInformation("RecordUrl") = strDocumentURL
                            drSearchInformation("RecordViewDate") = strDocumentViewDate

                            dtSearchInformation.Rows.Add(drSearchInformation)
                        Next

                    Case 4 ' This is the Employment Module

                        'First check if the employment module has user/group permissions enabled
                        Dim boolEnableUserGroupPermissions_Employment As Boolean = False
                        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(intModuleTypeID, intSiteID)
                        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                                boolEnableUserGroupPermissions_Employment = True
                            End If
                        Next

                        'Get a list of ALL ACTIVE Employment Items, the filter based on the recordID's from our search
                        Dim dtEmployment As DataTable = If(boolEnableUserGroupPermissions_Employment, EmploymentDAL.GetEmployment_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByStatus_FrontEnd(True, intSiteID))
                        Dim dvEmployment As New DataView(dtEmployment)
                        dvEmployment.RowFilter = "employmentID In (" & strRecordIDList & ")"

                        'Loop through all records adding rows to dtSearchInformation
                        For intRowIndex = 0 To dvEmployment.Count - 1
                            Dim drEmployment As DataRow = dvEmployment(intRowIndex).Row

                            Dim intEmploymentID As Integer = Convert.ToInt32(drEmployment("employmentID"))
                            Dim strModuleHeading As String = If(intRowIndex = 0, strModuleName_LanguageSpecific, "") 'Only added if its the first row
                            Dim strEmploymentTitle As String = drEmployment("Title")
                            Dim strEmploymentSummary As String = drEmployment("Summary")
                            Dim strEmploymentURL As String = "/employment/?id=" & intEmploymentID
                            Dim strEmploymentViewDate As String = drEmployment("ViewDate")

                            'Create a new SearchInformation row and add our employment details to the row
                            Dim drSearchInformation As DataRow = dtSearchInformation.NewRow()
                            drSearchInformation("ModuleTypeID") = intModuleTypeID
                            drSearchInformation("ModuleHeading") = strModuleHeading
                            drSearchInformation("RecordID") = intEmploymentID
                            drSearchInformation("RecordTitle") = strEmploymentTitle
                            drSearchInformation("RecordSummary") = strEmploymentSummary
                            drSearchInformation("RecordUrl") = strEmploymentURL
                            drSearchInformation("RecordViewDate") = strEmploymentViewDate

                            dtSearchInformation.Rows.Add(drSearchInformation)
                        Next


                    Case 5 ' This is the Event Module

                        'First check if the event module has user/group permissions enabled
                        Dim boolEnableUserGroupPermissions_Event As Boolean = False
                        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(intModuleTypeID, intSiteID)
                        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                                boolEnableUserGroupPermissions_Event = True
                            End If
                        Next

                        'Get a list of ALL ACTIVE events, the filter based on the recordID's from our search
                        Dim dtEvent As DataTable = If(boolEnableUserGroupPermissions_Event, EventDAL.GetEvent_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByStatus_FrontEnd(True, intSiteID))
                        Dim dvEvent As New DataView(dtEvent)
                        dvEvent.RowFilter = "eventID In (" & strRecordIDList & ")"

                        'Loop through all records adding rows to dtSearchInformation
                        For intRowIndex = 0 To dvEvent.Count - 1
                            Dim drEvent As DataRow = dvEvent(intRowIndex).Row

                            Dim intEventID As Integer = Convert.ToInt32(drEvent("eventID"))
                            Dim strModuleHeading As String = If(intRowIndex = 0, strModuleName_LanguageSpecific, "") 'Only added if its the first row
                            Dim strEventTitle As String = drEvent("Title")
                            Dim strEventSummary As String = drEvent("Summary")
                            Dim strEventURL As String = "/Event/?id=" & intEventID
                            Dim strEventViewDate As String = drEvent("ViewDate")

                            'Create a new SearchInformation row and add our event details to the row
                            Dim drSearchInformation As DataRow = dtSearchInformation.NewRow()
                            drSearchInformation("ModuleTypeID") = intModuleTypeID
                            drSearchInformation("ModuleHeading") = strModuleHeading
                            drSearchInformation("RecordID") = intEventID
                            drSearchInformation("RecordTitle") = strEventTitle
                            drSearchInformation("RecordSummary") = strEventSummary
                            drSearchInformation("RecordUrl") = strEventURL
                            drSearchInformation("RecordViewDate") = strEventViewDate

                            dtSearchInformation.Rows.Add(drSearchInformation)
                        Next

                    Case 6 ' This is the FAQ Module

                        'First check if the faq module has user/group permissions enabled
                        Dim boolEnableUserGroupPermissions_Faq As Boolean = False
                        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(intModuleTypeID, intSiteID)
                        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                                boolEnableUserGroupPermissions_Faq = True
                            End If
                        Next

                        'Get a list of ALL ACTIVE FAQ Items, the filter based on the recordID's from our search
                        Dim dtFaq As DataTable = If(boolEnableUserGroupPermissions_Faq, FaqDAL.GetFaq_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), FaqDAL.GetFaq_ByStatus_FrontEnd(True, intSiteID))
                        Dim dvFaq As New DataView(dtFaq)
                        dvFaq.RowFilter = "faqID In (" & strRecordIDList & ")"

                        'Loop through all records adding rows to dtSearchInformation
                        For intRowIndex = 0 To dvFaq.Count - 1
                            Dim drFaq As DataRow = dvFaq(intRowIndex).Row

                            Dim intFaqID As Integer = Convert.ToInt32(drFaq("faqID"))
                            Dim strModuleHeading As String = If(intRowIndex = 0, strModuleName_LanguageSpecific, "") 'Only added if its the first row
                            Dim strFaqQuestion As String = drFaq("Question")
                            Dim strFaqAnswer As String = drFaq("Answer")
                            Dim strFaqURL As String = "/faq/?id=" & intFaqID
                            Dim strFaqViewDate As String = drFaq("ViewDate")

                            'Create a new SearchInformation row and add our faq details to the row
                            Dim drSearchInformation As DataRow = dtSearchInformation.NewRow()
                            drSearchInformation("ModuleTypeID") = intModuleTypeID
                            drSearchInformation("ModuleHeading") = strModuleHeading
                            drSearchInformation("RecordID") = intFaqID
                            drSearchInformation("RecordTitle") = strFaqQuestion
                            drSearchInformation("RecordSummary") = strFaqAnswer
                            drSearchInformation("RecordUrl") = strFaqURL
                            drSearchInformation("RecordViewDate") = strFaqViewDate

                            dtSearchInformation.Rows.Add(drSearchInformation)
                        Next

                    Case 7 ' This is the Link Module

                    Case 8 ' This is the Members Access Module, We do not included members with search tags, therefore we don't put this in the search results control


                    Case 9 ' This is the Poll Module

                        'First check if the poll module has user/group permissions enabled
                        Dim boolEnableUserGroupPermissions_Poll As Boolean = False
                        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(intModuleTypeID, intSiteID)
                        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                                boolEnableUserGroupPermissions_Poll = True
                            End If
                        Next

                        'Get a list of ALL ACTIVE poll, the filter based on the recordID's from our search
                        Dim dtPoll As DataTable = If(boolEnableUserGroupPermissions_Poll, PollDAL.GetPoll_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), MemberDAL.GetCurrentMemberID, intSiteID), PollDAL.GetPoll_ByStatus_FrontEnd(True, intSiteID))
                        Dim dvPoll As New DataView(dtPoll)
                        dvPoll.RowFilter = "ID In (" & strRecordIDList & ")"

                        'Loop through all records adding rows to dtSearchInformation
                        For intRowIndex = 0 To dvPoll.Count - 1
                            Dim drPoll As DataRow = dvPoll(intRowIndex).Row

                            Dim intPollID As Integer = Convert.ToInt32(drPoll("ID"))
                            Dim strModuleHeading As String = If(intRowIndex = 0, strModuleName_LanguageSpecific, "") 'Only added if its the first row
                            Dim strPollQuestion As String = drPoll("Question")

                            Dim sbPollAnswers As New StringBuilder()
                            Dim dtPollAnswers As DataTable = PollDAL.GetPollAnswerList_ByPollID_FrontEnd(intPollID)
                            For Each drPollAnswer As DataRow In dtPollAnswers.Rows
                                sbPollAnswers.Append(drPollAnswer("Answer").ToString() & "<br/>")
                            Next

                            Dim strPollURL As String = "/Poll/?id=" & intPollID
                            Dim strViewDate As String = drPoll("ViewDate")

                            'Create a new SearchInformation row and add our poll details to the row
                            Dim drSearchInformation As DataRow = dtSearchInformation.NewRow()
                            drSearchInformation("ModuleTypeID") = intModuleTypeID
                            drSearchInformation("ModuleHeading") = strModuleHeading
                            drSearchInformation("RecordID") = intPollID
                            drSearchInformation("RecordTitle") = strPollQuestion
                            drSearchInformation("RecordSummary") = sbPollAnswers.ToString()
                            drSearchInformation("RecordUrl") = strPollURL
                            drSearchInformation("RecordViewDate") = strViewDate

                            dtSearchInformation.Rows.Add(drSearchInformation)
                        Next

                    Case 10 'This is the Press Release Module

                        'First check if the press release module has user/group permissions enabled
                        Dim boolEnableUserGroupPermissions_PressRelease As Boolean = False
                        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(intModuleTypeID, intSiteID)
                        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                                boolEnableUserGroupPermissions_PressRelease = True
                            End If
                        Next

                        'Get a list of ALL ACTIVE Press Release Items, the filter based on the recordID's from our search
                        Dim dtPressRelease As DataTable = If(boolEnableUserGroupPermissions_PressRelease, PressReleaseDAL.GetPressRelease_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PressReleaseDAL.GetPressRelease_ByStatus_FrontEnd(True, intSiteID))
                        Dim dvPressRelease As New DataView(dtPressRelease)
                        dvPressRelease.RowFilter = "prID In (" & strRecordIDList & ")"

                        'Loop through all records adding rows to dtSearchInformation
                        For intRowIndex = 0 To dvPressRelease.Count - 1
                            Dim drPressRelease As DataRow = dvPressRelease(intRowIndex).Row

                            Dim intPressRelease As Integer = Convert.ToInt32(drPressRelease("prID"))
                            Dim strModuleHeading As String = If(intRowIndex = 0, strModuleName_LanguageSpecific, "") 'Only added if its the first row
                            Dim strPressReleaseTitle As String = drPressRelease("Title")
                            Dim strPressReleaseSummary As String = drPressRelease("Summary")
                            Dim strPressReleaseURL As String = "/PressRelease/?id=" & intPressRelease
                            Dim strPressReleaseViewDate As String = drPressRelease("ViewDate")

                            'Create a new SearchInformation row and add our press release details to the row
                            Dim drSearchInformation As DataRow = dtSearchInformation.NewRow()
                            drSearchInformation("ModuleTypeID") = intModuleTypeID
                            drSearchInformation("ModuleHeading") = strModuleHeading
                            drSearchInformation("RecordID") = intPressRelease
                            drSearchInformation("RecordTitle") = strPressReleaseTitle
                            drSearchInformation("RecordSummary") = strPressReleaseSummary
                            drSearchInformation("RecordUrl") = strPressReleaseURL
                            drSearchInformation("RecordViewDate") = strPressReleaseViewDate

                            dtSearchInformation.Rows.Add(drSearchInformation)
                        Next

                    Case 11 ' This is the Search Tags Module, we do not search on this

                    Case 12 ' This is the Staff Members Module, However we do not yet attach search tags to staff members

                        'First check if the staff module has user/group permissions enabled
                        Dim boolEnableUserGroupPermissions_Staff As Boolean = False
                        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(intModuleTypeID, intSiteID)
                        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                                boolEnableUserGroupPermissions_Staff = True
                            End If
                        Next

                        'Get a list of ALL ACTIVE staffs, the filter based on the recordID's from our search
                        Dim dtStaff As DataTable = If(boolEnableUserGroupPermissions_Staff, StaffDAL.GetStaff_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), StaffDAL.GetStaff_ByStatus_FrontEnd(True, intSiteID))
                        Dim dvStaff As New DataView(dtStaff)
                        dvStaff.RowFilter = "staffID In (" & strRecordIDList & ")"

                        'Loop through all records adding rows to dtSearchInformation
                        For intRowIndex = 0 To dvStaff.Count - 1
                            Dim drStaff As DataRow = dvStaff(intRowIndex).Row

                            Dim intStaffID As Integer = Convert.ToInt32(drStaff("staffID"))
                            Dim strModuleHeading As String = If(intRowIndex = 0, strModuleName_LanguageSpecific, "") 'Only added if its the first row

                            Dim strSalutation_LangaugeSpecific As String = String.Empty
                            If Not drStaff("Salutation_LanguageProperty") Is DBNull.Value Then
                                Dim strSalutation_LanguageProperty As String = drStaff("Salutation_LanguageProperty")
                                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
                            End If

                            Dim strStaffFirstAndLastName As String = CommonWeb.FormatName(strSalutation_LangaugeSpecific, drStaff("FirstName"), drStaff("LastName"))
                            Dim strStaffSummary As String = drStaff("body")
                            Dim strStaffURL As String = "/Staff/?id=" & intStaffID
                            Dim strStaffViewDate As String = drStaff("ViewDate")

                            'Create a new SearchInformation row and add our staff details to the row
                            Dim drSearchInformation As DataRow = dtSearchInformation.NewRow()
                            drSearchInformation("ModuleTypeID") = intModuleTypeID
                            drSearchInformation("ModuleHeading") = strModuleHeading
                            drSearchInformation("RecordID") = intStaffID
                            drSearchInformation("RecordTitle") = strStaffFirstAndLastName
                            drSearchInformation("RecordSummary") = CommonWeb.stripHTMLandLimitWordCount(strStaffSummary, 25)
                            drSearchInformation("RecordUrl") = strStaffURL
                            drSearchInformation("RecordViewDate") = strStaffViewDate

                            dtSearchInformation.Rows.Add(drSearchInformation)
                        Next

                    Case 13 ' This is the Suggestinos Module, we may show Suggestions in the future, but not right now.

                    Case 14 ' This is the Topics Module

                End Select

            End If
        Next

        Return dtSearchInformation

    End Function


    Protected Sub rlvSearchResults_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListViewItemEventArgs) Handles rlvSearchResults.ItemDataBound

        If TypeOf e.Item Is RadListViewDataItem Then
            Dim item As RadListViewDataItem = TryCast(e.Item, RadListViewDataItem)

            Dim intModuleTypeID As Integer = Convert.ToInt32(DataBinder.Eval(item.DataItem, "ModuleTypeID"))
            Dim intRecordID As Integer = Convert.ToInt32(DataBinder.Eval(item.DataItem, "RecordID"))

            Dim strModuleHeading As String = DataBinder.Eval(item.DataItem, "ModuleHeading")
            If strModuleHeading.Length > 0 Then
                Dim divModuleHeading As HtmlGenericControl = e.Item.FindControl("divModuleHeading")
                Dim litModuleHeading As Literal = e.Item.FindControl("litModuleHeading")

                divModuleHeading.Visible = True
                litModuleHeading.Text = strModuleHeading
            End If

            'Setup the records heading and anchor link
            Dim aHeading As HtmlAnchor = e.Item.FindControl("aHeading")
            Dim litRecordHeading As Literal = e.Item.FindControl("litRecordHeading")

            aHeading.HRef = DataBinder.Eval(item.DataItem, "RecordUrl")
            litRecordHeading.Text = DataBinder.Eval(item.DataItem, "RecordTitle")

            'Setup the date if it exists
            Dim strViewDate As String = DataBinder.Eval(item.DataItem, "RecordViewDate")
            If strViewDate.Length > 0 Then
                Dim divDate As HtmlGenericControl = e.Item.FindControl("divDate")
                divDate.Visible = True
            End If

            'Setup the Records Summary Content
            Dim litRecordSummary As Literal = e.Item.FindControl("litRecordSummary")
            litRecordSummary.Text = DataBinder.Eval(item.DataItem, "RecordSummary")

            'Finally Populate the Search Tags
            Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(intModuleTypeID, intSiteID, intRecordID)
            If dtSearchTags.Rows.Count > 0 Then
                Dim rptSearchTags As Repeater = e.Item.FindControl("rptSearchTags")
                rptSearchTags.DataSource = dtSearchTags
                rptSearchTags.DataBind()
            End If
        End If
    End Sub

    Protected Sub rlvSearchResults_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles rlvSearchResults.DataBound
        Dim pager As RadDataPager = DirectCast(rlvSearchResults.FindControl("rdPagerSearchResults"), RadDataPager)
        pager.Visible = (pager.PageSize < pager.TotalRowCount)

    End Sub


End Class
