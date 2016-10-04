Imports System.Data

Partial Class admin_richtemplate_top_row
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Check the user exists
            Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
            If intAdminUserID > 0 Then

                'Need to check what richtemplate package this is, using the "packageID" variable from session
                Dim dtSite As DataTable = SiteDAL.GetSite_ByID(SiteDAL.GetCurrentSiteID_Admin())
                If dtSite.Rows.Count > 0 Then
                    Dim drSite As DataRow = dtSite.Rows(0)
                    Dim intPackageTypeID As Integer = Convert.ToInt32(drSite("PackageTypeID"))

                    Select Case intPackageTypeID
                        Case 1
                            divRichTemplateLite.Visible = True
                        Case 2
                            divRichTemplateGold.Visible = True
                        Case 3
                            divRichTemplatePlatinum.Visible = True
                        Case Else
                            divRichTemplateDefault.Visible = True
                    End Select
                Else
                    Response.Redirect("~/richadmin")
                End If


                'If we have more than 1 site, then we show the list of possible sites, the Master Administrator can view ALL, whereas other Administrators can ony view sites they have access to
                Dim dtSiteList As DataTable
                Dim intAdminUserAccessLevel As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
                If intAdminUserAccessLevel > 2 Then 'Then they are at least a Master Administrator
                    dtSiteList = SiteDAL.GetSiteList() ' So retrieve ALL Sites
                Else
                    'Check if this adminUser has access to All Sites, if so get all sites, otherwise just get the sites they have access to
                    Dim dtSiteList_AllSites As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID_Active(0, intAdminUserID)
                    If dtSiteList_AllSites.Rows.Count > 0 Then
                        dtSiteList = SiteDAL.GetSiteList() ' So retrieve ALL Sites
                    Else
                        dtSiteList = SiteDAL.GetSiteAccessList_ForAdminUser_ByAdminUserID_Active(intAdminUserID) 'Only get Sites user has access to
                    End If
                End If
                If dtSiteList.Rows.Count > 1 Then
                    ddlSelectSite.Items.Clear()
                    For Each drSiteList In dtSiteList.Rows
                        Dim intSiteID As Integer = drSiteList("ID")
                        Dim strSiteName As String = drSiteList("SiteName")
                        ddlSelectSite.Items.Add(New ListItem(strSiteName, intSiteID.ToString()))
                    Next
                    ddlSelectSite.SelectedValue = SiteDAL.GetCurrentSiteID_Admin()

                    'And then show the 'select a site' drop down list
                    divSelectSite.Visible = True
                End If

            Else
                Response.Redirect("~/richadmin/")
            End If
        End If

    End Sub

    Protected Sub ddlSelectSite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelectSite.SelectedIndexChanged
        'Get the current site, set this as the current site, and redirect the user to the home page
        Dim intSiteID As Integer = Convert.ToInt32(ddlSelectSite.SelectedValue)

        'Set the site's PackageTypeID - needed for the /admin/default.aspx page to decide on header image
        Dim intPackageTypeID As Integer = 0
        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(intSiteID)
        If dtSite.Rows.Count > 0 Then
            Dim drSite As DataRow = dtSite.Rows(0)
            intPackageTypeID = Convert.ToInt32(drSite("PackageTypeID"))
        End If

        SiteDAL.SetCurrentSiteID_Admin(intSiteID)

        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByIDAndSiteID(intAdminUserID, intSiteID)
        If dtAdminUser.Rows.Count > 0 Then
            Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

            Dim intAccessLevel As Integer = Convert.ToInt32(drAdminUser("Access_Level"))
            'Before we Handle permissions we check if this adminUser's permissions are at a SiteLevel or not
            Dim boolUseSiteLevelAccess As Boolean = Convert.ToBoolean(drAdminUser("UseSiteLevelAccess"))

            Dim drSiteAccess_AdminUser As DataRow = Nothing
            If boolUseSiteLevelAccess Then
                Dim dtSiteAccess_AdminUser As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID_Active(intSiteID, intAdminUserID)
                If dtSiteAccess_AdminUser.Rows.Count > 0 Then
                    drSiteAccess_AdminUser = dtSiteAccess_AdminUser.Rows(0)
                End If
            End If

            'The Admin User has access to at least one site, so log then in
            Dim strAllowModules As String = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, String.Empty, drSiteAccess_AdminUser("Allow_Modules")), drAdminUser("Allow_Modules_AllSites").ToString())
            Dim boolAllowWebContent As Boolean = If(boolUseSiteLevelAccess, If(drSiteAccess_AdminUser Is Nothing, False, drSiteAccess_AdminUser("Allow_WebContent")), Convert.ToBoolean(drAdminUser("Allow_WebContent_AllSites")))

            'Get the users preferred Language and obtain the language code from the Language DB
            Dim strLanguagePreference As String = drAdminUser("LanguageCode").ToString()

            'Perform the same functions as we do when we login the user, so all of their session data are updated, e.g. modules, AllowWebContent, accessLevel and language preference
            AdminUserDAL.LoginCurrentAdminUser(intAdminUserID, intSiteID, intAccessLevel, strAllowModules, boolAllowWebContent, strLanguagePreference)

            'Get the Base Frame
            Dim strBaseFrame As String = ""
            If boolAllowWebContent Then
                strBaseFrame = "/admin/richtemplate_welcome.aspx?mode=forms"
            Else
                strBaseFrame = "/admin/richtemplate_welcome.aspx?mode=modules"
            End If

            'Get the Tree Frame
            Dim strTreeFrame As String = ""
            If boolAllowWebContent Then
                strTreeFrame = "/admin/richtemplate_list_sections.aspx"
            ElseIf strAllowModules.Length > 0 Then
                strTreeFrame = "/admin/richtemplate_list_modules.aspx"
            Else
                strTreeFrame = "/admin/richtemplate_list_images.aspx"
            End If

            'Get the Contents Frame
            Dim strContentsFrame As String = "/admin/richtemplate_list_contents.aspx"

            CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, strBaseFrame, strTreeFrame, strContentsFrame, String.Empty)
        End If

    End Sub

End Class
