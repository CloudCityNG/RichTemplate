Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class member_Interior
    Inherits RichTemplateLanguagePage

    Dim boolWebpage_MemberSection_EnableGroupsAndUsers As Boolean = False
    Dim intWebInfoID_Header As Integer = Integer.MinValue
    Dim intWebInfoID_Footer As Integer = Integer.MinValue

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim _PageID As Integer = 0
    Public Property PageID() As Integer
        Get
            If _PageID = 0 Then
                _PageID = CommonWeb.GetCurrentPageID(intWebInfoID_Header, intWebInfoID_Footer, True, False)
            End If

            Return _PageID
        End Get
        Set(value As Integer)
            _PageID = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'THIS SITE USES DOES NOT MEMBER SECTION, so if they reached this page, they got here illegally, redirect them to the sitewide login page
        Dim strSiteWideLoginURL As String = ConfigurationManager.AppSettings("SiteWideLoginURL").ToString()
        Response.Redirect(strSiteWideLoginURL)

        'First we set the SiteID and MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        If intMemberID > 0 Then

            If Not Page.IsPostBack Then
                'Check Access - ALSO THIS MUST BE THE FIRST THING WE DO, as we set both the intWebInfoID_Header and the intWebInfoID_Footer here, which we would use when calling the PageID property
                Dim dtSite As DataTable = SiteDAL.GetSite_ByID(intSiteID)
                If dtSite.Rows.Count > 0 Then
                    Dim drSite As DataRow = dtSite.Rows(0)
                    boolWebpage_MemberSection_EnableGroupsAndUsers = Convert.ToBoolean(drSite("Webpage_MemberSection_EnableGroupsAndUsers"))
                    intWebInfoID_Header = Convert.ToInt32(drSite("WebInfoID_Header"))
                    intWebInfoID_Footer = Convert.ToInt32(drSite("WebInfoID_Footer"))
                End If

                Page.Header.Title = SiteDAL.GetCompanyName() & " | " & Resources.WebInfo_FrontEnd.Interior_Member_HtmlTitleDefault
                DisplayMainContent()

            End If

            'Setup our Comments for WebInfo Pages - NOTE User must be logged in to post a comment in the Members Section
            'ucCommentsWebInfo.SetupCommentWebInfo(PageID, intSiteID, intMemberID, False)
        Else

            Response.Redirect("~/member/default.aspx")
        End If
    End Sub


    Protected Sub DisplayMainContent()

        Dim dtWebInfo As DataTable = If(boolWebpage_MemberSection_EnableGroupsAndUsers, WebInfoDAL.GetWebInfo_ByIDAndAccess_FrontEnd(PageID, MemberDAL.GetCurrentMemberGroupIDs, intMemberID), WebInfoDAL.GetWebInfo_ByID_FrontEnd(PageID))
        If dtWebInfo.Rows.Count > 0 Then
            Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

            'If this page is an 'inside link', we swap-out the data for this page with the actual inside page
            'We do this here, such that we check permissions against the CREATED Page, not the permissions of the actual inside page link, as this would get confusing for the end-user
            While ((Not drWebInfo("linkURL") Is DBNull.Value) AndAlso (drWebInfo("linkURL").ToString().StartsWith("/")))

                'If there is a link saved in the db then don't build a link 
                Dim strPageAndParentName_InsideLink As String = drWebInfo("linkURL").ToString()
                Dim listPageAndParentName_InsideLink As String() = strPageAndParentName_InsideLink.Split("/")

                'Now try and get the webinfoID for this page, and update drWebInfo, by getting the PageName and the Parent PageName if it exists
                Dim strPageName_InsideLink As String = String.Empty
                If listPageAndParentName_InsideLink.Length > 0 Then
                    strPageName_InsideLink = listPageAndParentName_InsideLink(listPageAndParentName_InsideLink.Length - 1)
                End If
                Dim strPageNameParent_InsideLink As String = String.Empty
                If listPageAndParentName_InsideLink.Length > 1 Then
                    'If the folder name in the parent position is member or education, then this is not the parent page it is the module level members area or education area.
                    strPageNameParent_InsideLink = If(((listPageAndParentName_InsideLink(listPageAndParentName_InsideLink.Length - 2).ToLower() = "member") Or (listPageAndParentName_InsideLink(listPageAndParentName_InsideLink.Length - 2).ToLower() = "education")), String.Empty, listPageAndParentName_InsideLink(listPageAndParentName_InsideLink.Length - 2))
                End If
                'Update the PageID - Check if the link is a member or education page, based on its url
                Dim boolSecureMember As Boolean = strPageAndParentName_InsideLink.ToLower().StartsWith("/member/")
                Dim boolSecureEducation As Boolean = strPageAndParentName_InsideLink.ToLower().StartsWith("/education/")
                PageID = CommonWeb.GetCurrentPageID(strPageName_InsideLink, strPageNameParent_InsideLink, intWebInfoID_Header, intWebInfoID_Footer, boolSecureMember, boolSecureEducation)
                dtWebInfo = WebInfoDAL.GetWebInfo_ByID_FrontEnd(PageID)

                'If we can find this page, then overrite our webInfo Row, otherwise, we can not find this inside link, send a 404 ERROR
                If dtWebInfo.Rows.Count > 0 Then
                    drWebInfo = dtWebInfo.Rows(0)
                Else
                    Response.Redirect("/404.aspx")
                End If
            End While

            Dim strPageName As String = drWebInfo("Name").ToString()

            'set the Page Header
            CommonWeb.SetMasterPageBannerText(Me.Master, strPageName)

            'set the page content
            If drWebInfo("message") Is DBNull.Value Then
                mainContent.Text = Resources.WebInfo_FrontEnd.Interior_Member_PageNotPublished
            Else
                mainContent.Text = drWebInfo("message").ToString()
            End If

            'set the page title
            Page.Header.Title = SiteDAL.GetCompanyName() & " | " & strPageName
            If Not drWebInfo("metaTitle") Is DBNull.Value Then
                Dim strMetaTitle As String = drWebInfo("metaTitle").ToString()
                If strMetaTitle.Length > 0 Then
                    Page.Header.Title = strMetaTitle
                End If
            End If

            'set the page's keywords
            If Not drWebInfo("metaKeyword") Is DBNull.Value Then
                Dim keywordsHtmlMeta As New HtmlMeta()
                keywordsHtmlMeta.Name = "keywords"
                keywordsHtmlMeta.Content = drWebInfo("metaKeyword").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(keywordsHtmlMeta)
            End If

            'set the page's description
            If Not drWebInfo("metaDesc") Is DBNull.Value Then
                Dim descriptionHtmlMeta As New HtmlMeta()
                descriptionHtmlMeta.Name = "description"
                descriptionHtmlMeta.Content = drWebInfo("metaDesc").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(descriptionHtmlMeta)
            End If
        Else
            Response.Redirect("/404.aspx")
        End If

    End Sub

End Class
