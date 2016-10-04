Imports System.Data

Partial Class UserController_SiteSelector
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Only show on front-end pages
        Dim strPageName As String = HttpContext.Current.Request.Url.AbsolutePath.ToLower()
        If strPageName.StartsWith("/admin/") Then
            divSelectSite.Visible = False
        End If

        If Me.Visible AndAlso divSelectSite.Visible Then

            If Not IsPostBack Then
                'LoadSiteList_AvailableForMember()
                LoadSiteList_AllSites()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Use this function to populate the site selector drop down list with ALL SITES
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadSiteList_AllSites()

        'Now only setup and show this dropdown list if the member has access to more than 1 site, if its just one site we do not need to show this
        ddlSelectSite.Items.Clear()
        Dim dtSiteList As DataTable = SiteDAL.GetSiteList_FrontEnd()
        If dtSiteList.Rows.Count > 1 Then
            For Each drSite As DataRow In dtSiteList.Rows
                Dim intSiteID_Current As Integer = drSite("ID")

                Dim strSiteName As String = drSite("SiteName")
                ddlSelectSite.Items.Add(New ListItem(strSiteName, intSiteID_Current))
            Next
        End If

        'Only show select site if current user has access to more than 1 site
        If ddlSelectSite.Items.Count > 1 Then
            'Now if this user can access more than 1 site we show this dropdown list and set the current site as the selected site in the drop-down list
            Dim intSiteID As Integer = SiteDAL.GetCurrentSiteID_FrontEnd()
            ddlSelectSite.SelectedValue = intSiteID.ToString()
            divSelectSite.Visible = True
        Else
            divSelectSite.Visible = False
        End If


    End Sub

    ''' <summary>
    ''' Use this function to populate the site selector drop down list with only the sites the CURRENT MEMBER HAS ACCESS TO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadSiteList_AvailableForMember()
        Dim strCurrentSiteAccess As String() = MemberDAL.GetCurrentMemberSiteIDs().Split(",")

        'Now only setup and show this dropdown list if the member has access to more than 1 site, if its just one site we do not need to show this
        If strCurrentSiteAccess.Count > 1 Then

            ddlSelectSite.Items.Clear()
            Dim dtSiteList As DataTable = SiteDAL.GetSiteList_FrontEnd()
            For Each drSite As DataRow In dtSiteList.Rows
                Dim intSiteID_Current As Integer = drSite("ID")

                'Before we add this site to the site list, we check if this user has access to this site (SiteID = 0 implies ALL SITES)
                If strCurrentSiteAccess.Contains("0") Or strCurrentSiteAccess.Contains(intSiteID_Current.ToString()) Then
                    Dim strSiteName As String = drSite("SiteName")
                    ddlSelectSite.Items.Add(New ListItem(strSiteName, intSiteID_Current))
                End If
            Next

        End If

        If ddlSelectSite.Items.Count > 1 Then
            'Now if this user can access more than 1 site we show this dropdown list and set the current site as the selected site in the drop-down list
            Dim intSiteID As Integer = SiteDAL.GetCurrentSiteID_FrontEnd()
            ddlSelectSite.SelectedValue = intSiteID.ToString()
            divSelectSite.Visible = True
        Else
            divSelectSite.Visible = False
        End If

    End Sub

    Protected Sub ddlSelectSite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelectSite.SelectedIndexChanged
        'Get the current site, set this as the current site, and redirect the user to the home page
        Dim intSiteID As Integer = Convert.ToInt32(ddlSelectSite.SelectedValue)

        Dim strRedirect As String = "~"
        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(intSiteID)
        If dtSite.Rows.Count > 0 Then
            Dim drSite As DataRow = dtSite.Rows(0)
            strRedirect = "http://" & drSite("Domain").ToString()

            Dim strCompanyName As String = drSite("CompanyName")
            SiteDAL.SetCurrentSiteID_FrontEnd(intSiteID, strCompanyName)

        End If

        Response.Redirect(strRedirect) 'Redirect to the Home Page
    End Sub

End Class
