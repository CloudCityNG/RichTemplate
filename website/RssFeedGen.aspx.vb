Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text
Imports System.Web
Imports System.Xml


Partial Class RssFeedGen
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Load this master page if the current user is logged into this site OR this page DOES NOT require a Logged in Member
        If intMemberID >= 0 Or (Not CommonWeb.PageRequiresLoggedInMember()) Then
            If Not Page.IsPostBack Then

                Dim strCompanyName As String = SiteDAL.GetCompanyName()
                Dim strRssFeedHeading_Prefix = strCompanyName & " - "

                'Create a Reponse and an XmlTextWriter
                Response.Clear()
                Response.ContentType = "text/xml"
                Dim rssXmlTextWriter As XmlTextWriter = Nothing
                Try
                    rssXmlTextWriter = New XmlTextWriter(Response.OutputStream, Encoding.UTF8)
                    rssXmlTextWriter.WriteStartDocument()

                    rssXmlTextWriter.WriteProcessingInstruction("xml-stylesheet", "type='text/css' href='/css/rssStyle.css'")

                    rssXmlTextWriter.WriteStartElement("rss")
                    rssXmlTextWriter.WriteAttributeString("version", "2.0")
                    rssXmlTextWriter.WriteStartElement("channel")

                    'Create Starting elements
                    rssXmlTextWriter.WriteElementString("copyright", "(c) " & DateTime.Now.Year.ToString() & " " & strCompanyName & ".")
                    rssXmlTextWriter.WriteElementString("ttl", "5")

                    'Get our base url, and remove the www. from the url if it exists
                    Dim strModuleRssLink As String = Request.Url.AbsoluteUri.Replace("www.", "")
                    If Request.Url.Query.Length > 0 Then
                        strModuleRssLink = strModuleRssLink.Replace(Request.Url.Query, String.Empty)
                    End If

                    Dim strRssFeedType As String = ""
                    Dim intCategoryID As Integer = Integer.MinValue
                    If Not Request.QueryString("rss") Is Nothing Then
                        strRssFeedType = Request.QueryString("rss").ToUpper()

                        'So we have an rss parameter, now we check if we have a categoryName
                        If Not Request.QueryString("catid") Is Nothing Then
                            intCategoryID = Convert.ToInt32(Request.QueryString("catid"))
                        End If
                    End If

                    Dim intModuleTypeID As Integer = 0
                    Select Case strRssFeedType

                        Case "BLOG"

                            intModuleTypeID = 1
                            Dim strRssTitle As String = strRssFeedHeading_Prefix & Resources.Blog_FrontEnd.RssFeedGen_Title
                            Dim strRssDescription As String = Resources.Blog_FrontEnd.RssFeedGen_Description
                            Dim strRssLink As String = strModuleRssLink & "?rss=blog"
                            If Not intCategoryID = Integer.MinValue Then
                                'we get the category by ID AND moduleTypeID as we don't want someone free typing an integer into the url to produce an incorrect heading. So we make sure the categoryID corresponds to this module
                                If Not intCategoryID = 0 Then
                                    Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryIDAndModuleTypeIDAndSiteID(intCategoryID, intModuleTypeID, intSiteID)
                                    If dtCategory.Rows.Count > 0 Then
                                        strRssTitle += " - " & dtCategory.Rows(0)("CategoryName")
                                        strRssLink += "&catid=" & intCategoryID
                                    End If
                                Else
                                    strRssTitle += " - " & Resources.Blog_FrontEnd.RssFeedGen_UnCategorized
                                    strRssLink += "&catid=" & intCategoryID
                                End If
                            End If
                            rssXmlTextWriter.WriteElementString("title", strRssTitle)
                            rssXmlTextWriter.WriteElementString("description", strRssDescription)
                            rssXmlTextWriter.WriteElementString("link", strRssLink)

                            LoadRssFeed(intModuleTypeID, rssXmlTextWriter) 'Blog Feed


                        Case "DOCUMENTLIBRARY"

                            intModuleTypeID = 3
                            Dim strRssTitle As String = strRssFeedHeading_Prefix & Resources.DocumentLibrary_FrontEnd.RssFeedGen_Title
                            Dim strRssDescription As String = Resources.DocumentLibrary_FrontEnd.RssFeedGen_Description
                            Dim strRssLink As String = strModuleRssLink & "?rss=documentlibrary"
                            If Not intCategoryID = Integer.MinValue Then
                                'we get the category by ID AND moduleTypeID as we don't want someone free typing an integer into the url to produce an incorrect heading. So we make sure the categoryID corresponds to this module
                                If Not intCategoryID = 0 Then
                                    Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryIDAndModuleTypeIDAndSiteID(intCategoryID, intModuleTypeID, intSiteID)
                                    If dtCategory.Rows.Count > 0 Then
                                        strRssTitle += " - " & dtCategory.Rows(0)("CategoryName")
                                        strRssLink += "&catid=" & intCategoryID
                                    End If
                                Else
                                    strRssTitle += " - " & Resources.DocumentLibrary_FrontEnd.RssFeedGen_UnCategorized
                                    strRssLink += "&catid=" & intCategoryID
                                End If
                            End If
                            rssXmlTextWriter.WriteElementString("title", strRssTitle)
                            rssXmlTextWriter.WriteElementString("description", strRssDescription)
                            rssXmlTextWriter.WriteElementString("link", strRssLink)

                            LoadRssFeed(intModuleTypeID, rssXmlTextWriter) 'Document Library Feed


                        Case "EMPLOYMENT"

                            intModuleTypeID = 4
                            Dim strRssTitle As String = strRssFeedHeading_Prefix & Resources.Employment_FrontEnd.RssFeedGen_Title
                            Dim strRssDescription As String = Resources.Employment_FrontEnd.RssFeedGen_Description
                            Dim strRssLink As String = strModuleRssLink & "?rss=employment"
                            If Not intCategoryID = Integer.MinValue Then
                                'we get the category by ID AND moduleTypeID as we don't want someone free typing an integer into the url to produce an incorrect heading. So we make sure the categoryID corresponds to this module
                                If Not intCategoryID = 0 Then
                                    Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryIDAndModuleTypeIDAndSiteID(intCategoryID, intModuleTypeID, intSiteID)
                                    If dtCategory.Rows.Count > 0 Then
                                        strRssTitle += " - " & dtCategory.Rows(0)("CategoryName")
                                        strRssLink += "&catid=" & intCategoryID
                                    End If
                                Else
                                    strRssTitle += " - " & Resources.Employment_FrontEnd.RssFeedGen_UnCategorized
                                    strRssLink += "&catid=" & intCategoryID
                                End If
                            End If
                            rssXmlTextWriter.WriteElementString("title", strRssTitle)
                            rssXmlTextWriter.WriteElementString("description", strRssDescription)
                            rssXmlTextWriter.WriteElementString("link", strRssLink)

                            LoadRssFeed(intModuleTypeID, rssXmlTextWriter) 'Employment Feed


                        Case "EVENT"

                            intModuleTypeID = 5
                            Dim strRssTitle As String = strRssFeedHeading_Prefix & Resources.Event_FrontEnd.RssFeedGen_Title
                            Dim strRssDescription As String = Resources.Event_FrontEnd.RssFeedGen_Description
                            Dim strRssLink As String = strModuleRssLink & "?rss=event"
                            If Not intCategoryID = Integer.MinValue Then
                                'we get the category by ID AND moduleTypeID as we don't want someone free typing an integer into the url to produce an incorrect heading. So we make sure the categoryID corresponds to this module
                                If Not intCategoryID = 0 Then
                                    Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryIDAndModuleTypeIDAndSiteID(intCategoryID, intModuleTypeID, intSiteID)
                                    If dtCategory.Rows.Count > 0 Then
                                        strRssTitle += " - " & dtCategory.Rows(0)("CategoryName")
                                        strRssLink += "&catid=" & intCategoryID
                                    End If
                                Else
                                    strRssTitle += " - " & Resources.Event_FrontEnd.RssFeedGen_UnCategorized
                                    strRssLink += "&catid=" & intCategoryID
                                End If
                            End If
                            rssXmlTextWriter.WriteElementString("title", strRssTitle)
                            rssXmlTextWriter.WriteElementString("description", strRssDescription)
                            rssXmlTextWriter.WriteElementString("link", strRssLink)

                            LoadRssFeed(intModuleTypeID, rssXmlTextWriter) 'Event Feed


                        Case "FAQ"

                            intModuleTypeID = 6
                            Dim strRssTitle As String = strRssFeedHeading_Prefix & Resources.Faq_FrontEnd.RssFeedGen_Title
                            Dim strRssDescription As String = Resources.Faq_FrontEnd.RssFeedGen_Description
                            Dim strRssLink As String = strModuleRssLink & "?rss=faq"
                            If Not intCategoryID = Integer.MinValue Then
                                'we get the category by ID AND moduleTypeID as we don't want someone free typing an integer into the url to produce an incorrect heading. So we make sure the categoryID corresponds to this module
                                If Not intCategoryID = 0 Then
                                    Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryIDAndModuleTypeIDAndSiteID(intCategoryID, intModuleTypeID, intSiteID)
                                    If dtCategory.Rows.Count > 0 Then
                                        strRssTitle += " - " & dtCategory.Rows(0)("CategoryName")
                                        strRssLink += "&catid=" & intCategoryID
                                    End If
                                Else
                                    strRssTitle += " - " & Resources.Faq_FrontEnd.RssFeedGen_UnCategorized
                                    strRssLink += "&catid=" & intCategoryID
                                End If
                            End If
                            rssXmlTextWriter.WriteElementString("title", strRssTitle)
                            rssXmlTextWriter.WriteElementString("description", strRssDescription)
                            rssXmlTextWriter.WriteElementString("link", strRssLink)

                            LoadRssFeed(intModuleTypeID, rssXmlTextWriter) 'Faq Feed

                        Case "POLL"

                            intModuleTypeID = 9
                            Dim strRssTitle As String = strRssFeedHeading_Prefix & Resources.Poll_FrontEnd.RssFeedGen_Title
                            Dim strRssDescription As String = Resources.Poll_FrontEnd.RssFeedGen_Description
                            Dim strRssLink As String = strModuleRssLink & "?rss=poll"
                            If Not intCategoryID = Integer.MinValue Then
                                'we get the category by ID AND moduleTypeID as we don't want someone free typing an integer into the url to produce an incorrect heading. So we make sure the categoryID corresponds to this module
                                If Not intCategoryID = 0 Then
                                    Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryIDAndModuleTypeIDAndSiteID(intCategoryID, intModuleTypeID, intSiteID)
                                    If dtCategory.Rows.Count > 0 Then
                                        strRssTitle += " - " & dtCategory.Rows(0)("CategoryName")
                                        strRssLink += "&catid=" & intCategoryID
                                    End If
                                Else
                                    strRssTitle += " - " & Resources.Poll_FrontEnd.RssFeedGen_UnCategorized
                                    strRssLink += "&catid=" & intCategoryID
                                End If
                            End If
                            rssXmlTextWriter.WriteElementString("title", strRssTitle)
                            rssXmlTextWriter.WriteElementString("description", strRssDescription)
                            rssXmlTextWriter.WriteElementString("link", strRssLink)

                            LoadRssFeed(intModuleTypeID, rssXmlTextWriter) 'Poll Feed

                        Case "PRESSRELEASE"

                            intModuleTypeID = 10
                            Dim strRssTitle As String = strRssFeedHeading_Prefix & Resources.PressRelease_FrontEnd.RssFeedGen_Title
                            Dim strRssDescription As String = Resources.PressRelease_FrontEnd.RssFeedGen_Description
                            Dim strRssLink As String = strModuleRssLink & "?rss=pressrelease"
                            If Not intCategoryID = Integer.MinValue Then
                                'we get the category by ID AND moduleTypeID as we don't want someone free typing an integer into the url to produce an incorrect heading. So we make sure the categoryID corresponds to this module
                                If Not intCategoryID = 0 Then
                                    Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryIDAndModuleTypeIDAndSiteID(intCategoryID, intModuleTypeID, intSiteID)
                                    If dtCategory.Rows.Count > 0 Then
                                        strRssTitle += " - " & dtCategory.Rows(0)("CategoryName")
                                        strRssLink += "&catid=" & intCategoryID
                                    End If
                                Else
                                    strRssTitle += " - " & Resources.PressRelease_FrontEnd.RssFeedGen_UnCategorized
                                    strRssLink += "&catid=" & intCategoryID
                                End If

                            End If
                            rssXmlTextWriter.WriteElementString("title", strRssTitle)
                            rssXmlTextWriter.WriteElementString("description", strRssDescription)
                            rssXmlTextWriter.WriteElementString("link", strRssLink)

                            LoadRssFeed(intModuleTypeID, rssXmlTextWriter) 'Press Release Feed


                        Case "STAFF"

                            intModuleTypeID = 12
                            Dim strRssTitle = strRssFeedHeading_Prefix & Resources.Staff_FrontEnd.RssFeedGen_Title
                            Dim strRssDescription As String = Resources.Staff_FrontEnd.RssFeedGen_Description
                            Dim strRssLink As String = strModuleRssLink & "?rss=staff"
                            If Not intCategoryID = Integer.MinValue Then
                                'we get the category by ID AND moduleTypeID as we don't want someone free typing an integer into the url to produce an incorrect heading. So we make sure the categoryID corresponds to this module
                                If Not intCategoryID = 0 Then
                                    Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryIDAndModuleTypeIDAndSiteID(intCategoryID, intModuleTypeID, intSiteID)
                                    If dtCategory.Rows.Count > 0 Then
                                        strRssTitle += " - " & dtCategory.Rows(0)("CategoryName")
                                        strRssLink += "&catid=" & intCategoryID
                                    End If
                                Else
                                    strRssTitle += " - " & Resources.Staff_FrontEnd.RssFeedGen_UnCategorized
                                    strRssLink += "&catid=" & intCategoryID
                                End If
                            End If
                            rssXmlTextWriter.WriteElementString("title", strRssTitle)
                            rssXmlTextWriter.WriteElementString("description", strRssDescription)
                            rssXmlTextWriter.WriteElementString("link", strRssLink)

                            LoadRssFeed(intModuleTypeID, rssXmlTextWriter) 'Staff Feed


                        Case Else

                            rssXmlTextWriter.WriteElementString("title", strRssFeedHeading_Prefix & Resources.RssFeed_FrontEnd.RssFeedGen_HeadingDefault)
                            rssXmlTextWriter.WriteElementString("description", Resources.RssFeed_FrontEnd.RssFeedGen_DescriptionDefault)
                            rssXmlTextWriter.WriteElementString("link", strModuleRssLink)

                            'Load all rss feeds
                            LoadRssFeed(1, rssXmlTextWriter) 'Blog Feed
                            LoadRssFeed(3, rssXmlTextWriter) 'Document Library Feed
                            LoadRssFeed(4, rssXmlTextWriter) 'Employment Feed
                            LoadRssFeed(5, rssXmlTextWriter) 'Event Feed
                            LoadRssFeed(6, rssXmlTextWriter) 'Faq Feed
                            LoadRssFeed(9, rssXmlTextWriter) 'Poll Feed
                            LoadRssFeed(10, rssXmlTextWriter) 'Press Release Feed
                            LoadRssFeed(12, rssXmlTextWriter) 'Staff Feed

                    End Select

                    'Close our Response and XmlTextWriter
                    rssXmlTextWriter.WriteEndElement()
                    rssXmlTextWriter.WriteEndElement()
                    rssXmlTextWriter.WriteEndDocument()

                Catch ex As Exception
                    ' Code that runs when an unhandled error occurs
                    RichTemplateCentralDAL.Error_LogError(ex)

                Finally
                    If Not rssXmlTextWriter Is Nothing Then
                        rssXmlTextWriter.Flush()
                        rssXmlTextWriter.Close()
                    End If
                    Dim strTest As String = rssXmlTextWriter.ToString()
                    Response.End()
                End Try


            End If

        End If
    End Sub

    Private Function LoadRssFeed(ByVal ModuleTypeID As Integer, ByVal rssXmlTextWriter As XmlTextWriter) As XmlTextWriter

        Dim dtModule As DataTable = ModuleDAL.GetModule_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        If dtModule.Rows.Count > 0 Then
            Dim drModule As DataRow = dtModule.Rows(0)

            Dim strModuleLocation_FrontEnd = drModule("ModuleLocation_FrontEnd").ToString()

            'Check if we have enabled User/Group permissions
            Dim boolEnableGroupsAndUserAccess As Boolean = False
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows
                If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                    boolEnableGroupsAndUserAccess = True
                End If
            Next

            'Load in all records that the user has permission for
            Dim dtModuleRssFeed As New DataTable

            Dim dcModuleRssFeed_ID As New DataColumn("ID", GetType(Integer))
            Dim dcModuleRssFeed_Title As New DataColumn("Title", GetType(String))
            Dim dcModuleRssFeed_Description As New DataColumn("Description", GetType(String))
            Dim dcModuleRssFeed_Category As New DataColumn("Category", GetType(String))
            Dim dcModuleRssFeed_Link As New DataColumn("Link", GetType(String))
            Dim dcModuleRssFeed_AuthorUsername As New DataColumn("Author_UserName", GetType(String))
            Dim dcModuleRssFeed_ViewDate As New DataColumn("ViewDate", GetType(DateTime))

            dtModuleRssFeed.Columns.AddRange(New DataColumn() {dcModuleRssFeed_ID, dcModuleRssFeed_Title, dcModuleRssFeed_Description, dcModuleRssFeed_Category, dcModuleRssFeed_Link, dcModuleRssFeed_AuthorUsername, dcModuleRssFeed_ViewDate})


            Dim intModuleRssFeed_ID As Integer = 0
            Dim strModuleRssFeed_Title As String = String.Empty
            Dim strModuleRssFeed_Description As String = String.Empty
            Dim strModuleRssFeed_Category As String = String.Empty
            Dim strModuleRssFeed_Link As String = String.Empty
            Dim strModuleRssFeed_AuthorUserName As String = String.Empty
            Dim dtViewDate As DateTime = DateTime.MinValue

            Select Case ModuleTypeID

                Case 1 ' Blog

                    Dim dtBlog As DataTable
                    If Not Request.QueryString("catid") Is Nothing Then
                        Dim intCategoryID As Integer = Convert.ToInt32(Request.QueryString("catid"))
                        If intCategoryID = 0 Then
                            'If categoryID is 0 then we are loading all records that are uncategorized
                            dtBlog = If(boolEnableGroupsAndUserAccess, BlogDAL.GetBlog_ByCategoryNullAndStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlog_ByCategoryNullAndStatus_FrontEnd(True, intSiteID))

                        Else
                            'Else we are loading all records that are part of a specific category
                            dtBlog = If(boolEnableGroupsAndUserAccess, BlogDAL.GetBlog_ByCategoryIDAndStatusAndAccess_FrontEnd(intCategoryID, True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlog_ByCategoryIDAndStatus_FrontEnd(intCategoryID, True, intSiteID))

                        End If
                    Else
                        dtBlog = If(boolEnableGroupsAndUserAccess, BlogDAL.GetBlog_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), BlogDAL.GetBlog_ByStatus_FrontEnd(True, intSiteID))

                    End If

                    For Each drBlog As DataRow In dtBlog.Rows
                        intModuleRssFeed_ID = Convert.ToInt32(drBlog("blogID"))
                        strModuleRssFeed_Title = drBlog("Title")
                        strModuleRssFeed_Description = drBlog("Body") & " <br/><i><a href='" & strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID & "'>" & Resources.Blog_FrontEnd.RssFeedGen_ClickHereForMoreInformation & "</a></i><br/><hr />" 'NOTE, blogs don't really use Summary
                        strModuleRssFeed_Category = If(drBlog("CategoryName") Is DBNull.Value, Resources.Blog_FrontEnd.RssFeedGen_UnCategorized, drBlog("CategoryName"))
                        strModuleRssFeed_Link = strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID
                        strModuleRssFeed_AuthorUserName = If(drBlog("Author_Username") Is DBNull.Value, Resources.Blog_FrontEnd.RssFeedGen_PostedByDefault, drBlog("Author_Username"))
                        dtViewDate = Convert.ToDateTime(drBlog("ViewDate"))

                        'Now write our blog record to our rss stream
                        rssXmlTextWriter.WriteStartElement("item")
                        rssXmlTextWriter.WriteElementString("title", strModuleRssFeed_Title)
                        rssXmlTextWriter.WriteElementString("description", strModuleRssFeed_Description)
                        rssXmlTextWriter.WriteElementString("category", strModuleRssFeed_Category)
                        rssXmlTextWriter.WriteElementString("link", strModuleRssFeed_Link)
                        rssXmlTextWriter.WriteElementString("author", strModuleRssFeed_AuthorUserName)
                        rssXmlTextWriter.WriteElementString("pubDate", dtViewDate)
                        rssXmlTextWriter.WriteEndElement()

                    Next

                Case 3 ' Document Library

                    Dim dtDocument As DataTable
                    If Not Request.QueryString("catid") Is Nothing Then
                        Dim intCategoryID As Integer = Convert.ToInt32(Request.QueryString("catid"))
                        If intCategoryID = 0 Then
                            'If categoryID is 0 then we are loading all records that are uncategorized
                            dtDocument = If(boolEnableGroupsAndUserAccess, DocumentDAL.GetDocument_ByCategoryNullAndStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), DocumentDAL.GetDocument_ByCategoryNullAndStatus_FrontEnd(True, intSiteID))
                        Else
                            'Else we are loading all records that are part of a specific category
                            dtDocument = If(boolEnableGroupsAndUserAccess, DocumentDAL.GetDocument_ByCategoryIDAndStatusAndAccess_FrontEnd(intCategoryID, True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), DocumentDAL.GetDocument_ByCategoryIDAndStatus_FrontEnd(intCategoryID, True, intSiteID))
                        End If
                    Else
                        dtDocument = If(boolEnableGroupsAndUserAccess, DocumentDAL.GetDocumentList_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), DocumentDAL.GetDocumentList_ByStatus_FrontEnd(True, intSiteID))
                    End If

                    For Each drDocument As DataRow In dtDocument.Rows
                        intModuleRssFeed_ID = Convert.ToInt32(drDocument("documentID"))
                        strModuleRssFeed_Title = drDocument("fileTitle")
                        strModuleRssFeed_Description = drDocument("fileDescription") & " <br/><i><a href='" & strModuleLocation_FrontEnd & "DocumentDetail.aspx?id=" & intModuleRssFeed_ID & "'>" & Resources.DocumentLibrary_FrontEnd.RssFeedGen_ClickHereForMoreInformation & "</a></i><br/><hr />"
                        strModuleRssFeed_Category = If(drDocument("CategoryName") Is DBNull.Value, Resources.DocumentLibrary_FrontEnd.RssFeedGen_UnCategorized, drDocument("CategoryName"))
                        strModuleRssFeed_Link = strModuleLocation_FrontEnd & "DocumentDetail.aspx?id=" & intModuleRssFeed_ID
                        strModuleRssFeed_AuthorUserName = If(drDocument("Author_Username") Is DBNull.Value, Resources.DocumentLibrary_FrontEnd.RssFeedGen_PostedByDefault, drDocument("Author_Username"))
                        dtViewDate = Convert.ToDateTime(drDocument("ViewDate"))

                        'Now write our document record to our rss stream
                        rssXmlTextWriter.WriteStartElement("item")
                        rssXmlTextWriter.WriteElementString("title", strModuleRssFeed_Title)
                        rssXmlTextWriter.WriteElementString("description", strModuleRssFeed_Description)
                        rssXmlTextWriter.WriteElementString("category", strModuleRssFeed_Category)
                        rssXmlTextWriter.WriteElementString("link", strModuleRssFeed_Link)
                        rssXmlTextWriter.WriteElementString("author", strModuleRssFeed_AuthorUserName)
                        rssXmlTextWriter.WriteElementString("pubDate", dtViewDate)
                        rssXmlTextWriter.WriteEndElement()

                    Next


                Case 4 ' Employment Opportunity

                    Dim dtEmployment As DataTable
                    If Not Request.QueryString("catid") Is Nothing Then
                        Dim intCategoryID As Integer = Convert.ToInt32(Request.QueryString("catid"))
                        If intCategoryID = 0 Then
                            'If categoryID is 0 then we are loading all records that are uncategorized
                            dtEmployment = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmployment_ByCategoryNullAndStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByCategoryNullAndStatus_FrontEnd(True, intSiteID))

                        Else
                            'Else we are loading all records that are part of a specific category
                            dtEmployment = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmployment_ByCategoryIDAndStatusAndAccess_FrontEnd(intCategoryID, True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByCategoryIDAndStatus_FrontEnd(intCategoryID, True, intSiteID))

                        End If
                    Else
                        dtEmployment = If(boolEnableGroupsAndUserAccess, EmploymentDAL.GetEmployment_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs, intMemberID, intSiteID), EmploymentDAL.GetEmployment_ByStatus_FrontEnd(True, intSiteID))

                    End If

                    For Each drEmployment As DataRow In dtEmployment.Rows
                        intModuleRssFeed_ID = Convert.ToInt32(drEmployment("employmentID"))
                        strModuleRssFeed_Title = drEmployment("Title")
                        strModuleRssFeed_Description = drEmployment("Summary") & " <br/><i><a href='" & strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID & "'>" & Resources.Employment_FrontEnd.RssFeedGen_ClickHereForMoreInformation & "</a></i><br/><hr />"
                        strModuleRssFeed_Category = If(drEmployment("CategoryName") Is DBNull.Value, Resources.Employment_FrontEnd.RssFeedGen_UnCategorized, drEmployment("CategoryName"))
                        strModuleRssFeed_Link = strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID
                        strModuleRssFeed_AuthorUserName = If(drEmployment("Author_Username") Is DBNull.Value, Resources.Employment_FrontEnd.RssFeedGen_PostedByDefault, drEmployment("Author_Username"))
                        dtViewDate = Convert.ToDateTime(drEmployment("ViewDate"))

                        'Now write our employment record to our rss stream
                        rssXmlTextWriter.WriteStartElement("item")
                        rssXmlTextWriter.WriteElementString("title", strModuleRssFeed_Title)
                        rssXmlTextWriter.WriteElementString("description", strModuleRssFeed_Description)
                        rssXmlTextWriter.WriteElementString("category", strModuleRssFeed_Category)
                        rssXmlTextWriter.WriteElementString("link", strModuleRssFeed_Link)
                        rssXmlTextWriter.WriteElementString("author", strModuleRssFeed_AuthorUserName)
                        rssXmlTextWriter.WriteElementString("pubDate", dtViewDate)
                        rssXmlTextWriter.WriteEndElement()

                    Next

                Case 5 ' Event

                    Dim dtEvent As DataTable
                    If Not Request.QueryString("catid") Is Nothing Then
                        Dim intCategoryID As Integer = Convert.ToInt32(Request.QueryString("catid"))
                        If intCategoryID = 0 Then
                            'If categoryID is 0 then we are loading all records that are uncategorized
                            dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByCategoryNullAndStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByCategoryNullAndStatus_FrontEnd(True, intSiteID))

                        Else
                            'Else we are loading all records that are part of a specific category
                            dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByCategoryIDAndStatusAndAccess_FrontEnd(intCategoryID, True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByCategoryIDAndStatus_FrontEnd(intCategoryID, True, intSiteID))

                        End If
                    Else
                        dtEvent = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs, intMemberID, intSiteID), EventDAL.GetEvent_ByStatus_FrontEnd(True, intSiteID))

                    End If

                    For Each drEvent As DataRow In dtEvent.Rows
                        intModuleRssFeed_ID = Convert.ToInt32(drEvent("eventID"))
                        strModuleRssFeed_Title = drEvent("Title")
                        strModuleRssFeed_Description = drEvent("Summary") & " <br/><i><a href='" & strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID & "'>" & Resources.Event_FrontEnd.RssFeedGen_ClickHereForMoreInformation & "</a></i><br/><hr />"
                        strModuleRssFeed_Category = If(drEvent("CategoryName") Is DBNull.Value, Resources.Event_FrontEnd.RssFeedGen_UnCategorized, drEvent("CategoryName"))
                        strModuleRssFeed_Link = strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID
                        strModuleRssFeed_AuthorUserName = If(drEvent("Author_Username") Is DBNull.Value, Resources.Event_FrontEnd.RssFeedGen_PostedByDefault, drEvent("Author_Username"))
                        dtViewDate = Convert.ToDateTime(drEvent("ViewDate"))

                        'Now write our event record to our rss stream
                        rssXmlTextWriter.WriteStartElement("item")
                        rssXmlTextWriter.WriteElementString("title", strModuleRssFeed_Title)
                        rssXmlTextWriter.WriteElementString("description", strModuleRssFeed_Description)
                        rssXmlTextWriter.WriteElementString("category", strModuleRssFeed_Category)
                        rssXmlTextWriter.WriteElementString("link", strModuleRssFeed_Link)
                        rssXmlTextWriter.WriteElementString("author", strModuleRssFeed_AuthorUserName)
                        rssXmlTextWriter.WriteElementString("pubDate", dtViewDate)
                        rssXmlTextWriter.WriteEndElement()

                    Next

                Case 6 ' Faq

                    Dim dtFaq As DataTable
                    If Not Request.QueryString("catid") Is Nothing Then
                        Dim intCategoryID As Integer = Convert.ToInt32(Request.QueryString("catid"))
                        If intCategoryID = 0 Then
                            'If categoryID is 0 then we are loading all records that are uncategorized
                            dtFaq = If(boolEnableGroupsAndUserAccess, FaqDAL.GetFaq_ByCategoryNullAndStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), FaqDAL.GetFaq_ByCategoryNullAndStatus_FrontEnd(True, intSiteID))

                        Else
                            'Else we are loading all records that are part of a specific category
                            dtFaq = If(boolEnableGroupsAndUserAccess, FaqDAL.GetFaq_ByCategoryIDAndStatusAndAccess_FrontEnd(intCategoryID, True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), FaqDAL.GetFaq_ByCategoryIDAndStatus_FrontEnd(intCategoryID, True, intSiteID))

                        End If
                    Else
                        dtFaq = If(boolEnableGroupsAndUserAccess, FaqDAL.GetFaq_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs, intMemberID, intSiteID), FaqDAL.GetFaq_ByStatus_FrontEnd(True, intSiteID))

                    End If

                    For Each drFaq As DataRow In dtFaq.Rows
                        intModuleRssFeed_ID = Convert.ToInt32(drFaq("faqID"))
                        strModuleRssFeed_Title = drFaq("Question")
                        strModuleRssFeed_Description = drFaq("Answer") & " <br/><i><a href='" & strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID & "'>" & Resources.Faq_FrontEnd.RssFeedGen_ClickHereForMoreInformation & "</a></i><br/><hr />"
                        strModuleRssFeed_Category = If(drFaq("CategoryName") Is DBNull.Value, Resources.Faq_FrontEnd.RssFeedGen_UnCategorized, drFaq("CategoryName"))
                        strModuleRssFeed_Link = strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID
                        strModuleRssFeed_AuthorUserName = If(drFaq("Author_Username") Is DBNull.Value, Resources.Faq_FrontEnd.RssFeedGen_PostedByDefault, drFaq("Author_Username"))
                        dtViewDate = Convert.ToDateTime(drFaq("ViewDate"))

                        'Now write our faq record to our rss stream
                        rssXmlTextWriter.WriteStartElement("item")
                        rssXmlTextWriter.WriteElementString("title", strModuleRssFeed_Title)
                        rssXmlTextWriter.WriteElementString("description", strModuleRssFeed_Description)
                        rssXmlTextWriter.WriteElementString("category", strModuleRssFeed_Category)
                        rssXmlTextWriter.WriteElementString("link", strModuleRssFeed_Link)
                        rssXmlTextWriter.WriteElementString("author", strModuleRssFeed_AuthorUserName)
                        rssXmlTextWriter.WriteElementString("pubDate", dtViewDate)
                        rssXmlTextWriter.WriteEndElement()

                    Next

                Case 9 ' Poll

                    Dim dtPoll As DataTable
                    If Not Request.QueryString("catid") Is Nothing Then
                        Dim intCategoryID As Integer = Convert.ToInt32(Request.QueryString("catid"))
                        If intCategoryID = 0 Then
                            'If categoryID is 0 then we are loading all records that are uncategorized
                            dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_ByCategoryNullAndStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPoll_ByCategoryNullAndStatus_FrontEnd(True, intSiteID))
                        Else
                            'Else we are loading all records that are part of a specific category
                            dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_ByCategoryIDAndStatusAndAccess_FrontEnd(intCategoryID, True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPoll_ByCategoryIDAndStatus_FrontEnd(intCategoryID, True, intSiteID))
                        End If
                    Else
                        dtPoll = If(boolEnableGroupsAndUserAccess, PollDAL.GetPoll_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PollDAL.GetPoll_ByStatus_FrontEnd(True, intSiteID))
                    End If

                    For Each drPoll As DataRow In dtPoll.Rows
                        intModuleRssFeed_ID = Convert.ToInt32(drPoll("ID"))
                        strModuleRssFeed_Title = drPoll("Question")

                        Dim sbPollAnswers As New StringBuilder()
                        Dim dtPollAnswers As DataTable = PollDAL.GetPollAnswerList_ByPollID_FrontEnd(intModuleRssFeed_ID)
                        For Each drPollAnswer As DataRow In dtPollAnswers.Rows
                            sbPollAnswers.Append(drPollAnswer("Answer").ToString() & "<br/>")
                        Next
                        strModuleRssFeed_Description = sbPollAnswers.ToString() & " <br/><i><a href='" & strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID & "'>" & Resources.Poll_FrontEnd.RssFeedGen_ClickHereForMoreInformation & "</a></i><br/><hr />"
                        strModuleRssFeed_Category = If(drPoll("CategoryName") Is DBNull.Value, Resources.Poll_FrontEnd.RssFeedGen_UnCategorized, drPoll("CategoryName"))
                        strModuleRssFeed_Link = strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID
                        strModuleRssFeed_AuthorUserName = If(drPoll("Author_Username") Is DBNull.Value, Resources.Poll_FrontEnd.RssFeedGen_PostedByDefault, drPoll("Author_Username"))
                        dtViewDate = Convert.ToDateTime(drPoll("ViewDate"))

                        'Now write our poll record to our rss stream
                        rssXmlTextWriter.WriteStartElement("item")
                        rssXmlTextWriter.WriteElementString("title", strModuleRssFeed_Title)
                        rssXmlTextWriter.WriteElementString("description", strModuleRssFeed_Description)
                        rssXmlTextWriter.WriteElementString("category", strModuleRssFeed_Category)
                        rssXmlTextWriter.WriteElementString("link", strModuleRssFeed_Link)
                        rssXmlTextWriter.WriteElementString("author", strModuleRssFeed_AuthorUserName)
                        rssXmlTextWriter.WriteElementString("pubDate", dtViewDate)
                        rssXmlTextWriter.WriteEndElement()

                    Next

                Case 10 ' Press Release

                    Dim dtPressRelease As DataTable
                    If Not Request.QueryString("catid") Is Nothing Then
                        Dim intCategoryID As Integer = Convert.ToInt32(Request.QueryString("catid"))
                        If intCategoryID = 0 Then
                            'If categoryID is 0 then we are loading all records that are uncategorized
                            dtPressRelease = If(boolEnableGroupsAndUserAccess, PressReleaseDAL.GetPressRelease_ByCategoryNullAndStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PressReleaseDAL.GetPressRelease_ByCategoryNullAndStatus_FrontEnd(True, intSiteID))

                        Else
                            'Else we are loading all records that are part of a specific category
                            dtPressRelease = If(boolEnableGroupsAndUserAccess, PressReleaseDAL.GetPressRelease_ByCategoryIDAndStatusAndAccess_FrontEnd(intCategoryID, True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PressReleaseDAL.GetPressRelease_ByCategoryIDAndStatus_FrontEnd(intCategoryID, True, intSiteID))

                        End If

                    Else
                        dtPressRelease = If(boolEnableGroupsAndUserAccess, PressReleaseDAL.GetPressRelease_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PressReleaseDAL.GetPressRelease_ByStatus_FrontEnd(True, intSiteID))

                    End If

                    For Each drPressRelease As DataRow In dtPressRelease.Rows
                        intModuleRssFeed_ID = Convert.ToInt32(drPressRelease("prID"))
                        strModuleRssFeed_Title = drPressRelease("Title")
                        strModuleRssFeed_Description = drPressRelease("Summary") & " <br/><i><a href='" & strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID & "'>" & Resources.PressRelease_FrontEnd.RssFeedGen_ClickHereForMoreInformation & "</a></i><br/><hr />"
                        strModuleRssFeed_Category = If(drPressRelease("CategoryName") Is DBNull.Value, Resources.PressRelease_FrontEnd.RssFeedGen_UnCategorized, drPressRelease("CategoryName"))
                        strModuleRssFeed_Link = strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID
                        strModuleRssFeed_AuthorUserName = If(drPressRelease("Author_Username") Is DBNull.Value, Resources.PressRelease_FrontEnd.RssFeedGen_PostedByDefault, drPressRelease("Author_Username"))
                        dtViewDate = Convert.ToDateTime(drPressRelease("ViewDate"))

                        'Now write our press release record to our rss stream
                        rssXmlTextWriter.WriteStartElement("item")
                        rssXmlTextWriter.WriteElementString("title", strModuleRssFeed_Title)
                        rssXmlTextWriter.WriteElementString("description", strModuleRssFeed_Description)
                        rssXmlTextWriter.WriteElementString("category", strModuleRssFeed_Category)
                        rssXmlTextWriter.WriteElementString("link", strModuleRssFeed_Link)
                        rssXmlTextWriter.WriteElementString("author", strModuleRssFeed_AuthorUserName)
                        rssXmlTextWriter.WriteElementString("pubDate", dtViewDate)

                        rssXmlTextWriter.WriteEndElement()
                    Next

                Case 12 ' Staff

                    Dim dtStaff As DataTable
                    If Not Request.QueryString("catid") Is Nothing Then
                        Dim intCategoryID As Integer = Convert.ToInt32(Request.QueryString("catid"))
                        If intCategoryID = 0 Then
                            'If categoryID is 0 then we are loading all records that are uncategorized
                            dtStaff = If(boolEnableGroupsAndUserAccess, StaffDAL.GetStaff_ByCategoryIDNullAndStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), StaffDAL.GetStaff_ByCategoryIDNullAndStatus_FrontEnd(True, intSiteID))

                        Else
                            'Else we are loading all records that are part of a specific category
                            dtStaff = If(boolEnableGroupsAndUserAccess, StaffDAL.GetStaff_ByCategoryIDAndStatusAndAccess_FrontEnd(intCategoryID, True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), StaffDAL.GetStaff_ByCategoryIDAndStatus_FrontEnd(intCategoryID, True, intSiteID))

                        End If
                    Else
                        dtStaff = If(boolEnableGroupsAndUserAccess, StaffDAL.GetStaff_ByStatusAndAccess_FrontEnd(True, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), StaffDAL.GetStaff_ByStatus_FrontEnd(True, intSiteID))

                    End If

                    For Each drStaff As DataRow In dtStaff.Rows
                        intModuleRssFeed_ID = Convert.ToInt32(drStaff("staffID"))

                        Dim strSalutation_LangaugeSpecific As String = String.Empty
                        If Not drStaff("Salutation_LanguageProperty") Is DBNull.Value Then
                            Dim strSalutation_LanguageProperty As String = drStaff("Salutation_LanguageProperty")
                            strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
                        End If
                        strModuleRssFeed_Title = CommonWeb.FormatName(strSalutation_LangaugeSpecific, drStaff("FirstName"), drStaff("LastName"))
                        strModuleRssFeed_Description = drStaff("body") & " <br/><i><a href='" & strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID & "'>" & Resources.Staff_FrontEnd.RssFeedGen_ClickHereForMoreInformation & "</a></i><br/><hr />"
                        strModuleRssFeed_Category = If(drStaff("CategoryName") Is DBNull.Value, Resources.Staff_FrontEnd.RssFeedGen_UnCategorized, drStaff("CategoryName"))
                        strModuleRssFeed_Link = strModuleLocation_FrontEnd & "?id=" & intModuleRssFeed_ID
                        strModuleRssFeed_AuthorUserName = Resources.Staff_FrontEnd.RssFeedGen_PostedByDefault ' we don't show the person who created a staff member, as its totally irrelevant
                        dtViewDate = Convert.ToDateTime(drStaff("ViewDate"))

                        'Now write our stafff record to our rss stream
                        rssXmlTextWriter.WriteStartElement("item")
                        rssXmlTextWriter.WriteElementString("title", strModuleRssFeed_Title)
                        rssXmlTextWriter.WriteElementString("description", strModuleRssFeed_Description)
                        rssXmlTextWriter.WriteElementString("category", strModuleRssFeed_Category)
                        rssXmlTextWriter.WriteElementString("link", strModuleRssFeed_Link)
                        rssXmlTextWriter.WriteElementString("author", strModuleRssFeed_AuthorUserName)
                        rssXmlTextWriter.WriteElementString("pubDate", dtViewDate)
                        rssXmlTextWriter.WriteEndElement()

                    Next

            End Select

        End If

        Return rssXmlTextWriter

    End Function


End Class
