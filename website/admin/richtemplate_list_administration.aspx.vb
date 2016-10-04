Imports System.Data.SqlClient
Imports System.Data
Imports Telerik.Web.UI
Imports System.Drawing

Partial Class admin_richtemplate_list_administration
    Inherits RichTemplateLanguagePage

    Dim intAdminUserAccess As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Check the logged in user can view this page
        intAdminUserAccess = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intAdminUserAccess > 0 Then
            'Then the user can view this page, but only the help button will show up
            LoadWebsiteAdministration()
        Else
            Response.Redirect("~/richadmin/")
        End If


    End Sub

    Protected Sub LoadWebsiteAdministration()

        Dim strIntialSelection As String = ""
        If Not Request.QueryString("sel") Is Nothing Then
            strIntialSelection = Request.QueryString("sel").ToUpper()
        End If

        'First create our table of administration sections
        Dim dtWebsiteAdministration As New DataTable()
        Dim dcWebsiteAdministrationName As New DataColumn("WebsiteAdministrationName", GetType(String))
        Dim dcWebsiteAdministrationLocation As New DataColumn("WebsiteAdministrationLocation", GetType(String))
        Dim dcWebsiteAdministrationIsSelected As New DataColumn("WebsiteAdministrationIsSelelected", GetType(Boolean))

        dtWebsiteAdministration.Columns.AddRange(New DataColumn() {dcWebsiteAdministrationName, dcWebsiteAdministrationLocation, dcWebsiteAdministrationIsSelected})

        If intAdminUserAccess > 1 Then
            'Load the Email Administration menu
            Dim drWebsiteAdministration_EmailAdministration As DataRow = dtWebsiteAdministration.NewRow()
            drWebsiteAdministration_EmailAdministration("WebsiteAdministrationName") = Resources.RichTemplate_List_Administration.RichTemplate_List_Administration_EmailAdministration
            drWebsiteAdministration_EmailAdministration("WebsiteAdministrationLocation") = "/admin/emails/default.aspx"
            drWebsiteAdministration_EmailAdministration("WebsiteAdministrationIsSelelected") = If(strIntialSelection = "EMAIL", True, False)
            dtWebsiteAdministration.Rows.Add(drWebsiteAdministration_EmailAdministration)
        End If

        'Load the Help Item Administration menu
        Dim drWebsiteAdministration_Help As DataRow = dtWebsiteAdministration.NewRow()
        drWebsiteAdministration_Help("WebsiteAdministrationName") = Resources.RichTemplate_List_Administration.RichTemplate_List_Administration_HelpAdministration
        drWebsiteAdministration_Help("WebsiteAdministrationLocation") = "/admin/help/default.aspx"
        drWebsiteAdministration_Help("WebsiteAdministrationIsSelelected") = If(strIntialSelection = "HELP", True, False)
        dtWebsiteAdministration.Rows.Add(drWebsiteAdministration_Help)

        If intAdminUserAccess >2 Then


            'Load the Language Administartion, to only allow Master Administrators to view this change the condition above to 2 (Master Administrators)
            Dim drWebsiteAdministration_LanuageAdministration As DataRow = dtWebsiteAdministration.NewRow()
            drWebsiteAdministration_LanuageAdministration("WebsiteAdministrationName") = Resources.RichTemplate_List_Administration.RichTemplate_List_Administration_LanguageAdministration
            drWebsiteAdministration_LanuageAdministration("WebsiteAdministrationLocation") = "/admin/language/default.aspx"
            drWebsiteAdministration_LanuageAdministration("WebsiteAdministrationIsSelelected") = If(strIntialSelection = "LANGUAGE", True, False)
            dtWebsiteAdministration.Rows.Add(drWebsiteAdministration_LanuageAdministration)
        
            'Load the Site Administartion, to only allow Master Administrators to view this change the condition above to 2 (Master Administrators)
            Dim drWebsiteAdministration_SiteAdministration As DataRow = dtWebsiteAdministration.NewRow()
            drWebsiteAdministration_SiteAdministration("WebsiteAdministrationName") = Resources.RichTemplate_List_Administration.RichTemplate_List_Administration_SiteAdministration
            drWebsiteAdministration_SiteAdministration("WebsiteAdministrationLocation") = "/admin/site/default.aspx"
            drWebsiteAdministration_SiteAdministration("WebsiteAdministrationIsSelelected") = If(strIntialSelection = "SITE", True, False)
            dtWebsiteAdministration.Rows.Add(drWebsiteAdministration_SiteAdministration)
        End If

        If intAdminUserAccess > 1 Then
            'Load User Administration menu
            Dim drWebsiteAdministration_UserAdministration As DataRow = dtWebsiteAdministration.NewRow()
            drWebsiteAdministration_UserAdministration("WebsiteAdministrationName") = Resources.RichTemplate_List_Administration.RichTemplate_List_Administration_UserAdministration
            drWebsiteAdministration_UserAdministration("WebsiteAdministrationLocation") = "/admin/adminusers/default.aspx"
            drWebsiteAdministration_UserAdministration("WebsiteAdministrationIsSelelected") = If(strIntialSelection = "USER", True, False)
            dtWebsiteAdministration.Rows.Add(drWebsiteAdministration_UserAdministration)
        End If

        'Populate our website administration tree
        RadTreeWebsiteAdministration.Nodes.Clear()
        'Create a node for each website administration sectin then adds it to our tree
        For Each drWebsiteAdministration As DataRow In dtWebsiteAdministration.Rows
            Dim strWebsiteAdministrationName As String = drWebsiteAdministration("WebsiteAdministrationName").ToString()
            Dim strWebsiteAdministrationLocation As String = drWebsiteAdministration("WebsiteAdministrationLocation").ToString()
            Dim boolWebsiteAdministrationIsSelected As Boolean = Convert.ToBoolean(drWebsiteAdministration("WebsiteAdministrationIsSelelected"))

            Dim strWebsiteAdministrationText As String = "&nbsp;<img src='/images/folder_full_sm.png' class='rtImg' alt='" & strWebsiteAdministrationName & "'><span class='inner_rtIn'><a href='" & strWebsiteAdministrationLocation & "' target='basefrm'><font size='1'><b>&nbsp;" & strWebsiteAdministrationName & "</b></font></a></span>"

            Dim rtnWebsiteAdministration As New RadTreeNode(strWebsiteAdministrationText, strWebsiteAdministrationLocation)
            rtnWebsiteAdministration.Category = "Folder"
            rtnWebsiteAdministration.Expanded = True
            rtnWebsiteAdministration.AllowDrag = False
            rtnWebsiteAdministration.AllowDrop = False
            rtnWebsiteAdministration.Selected = boolWebsiteAdministrationIsSelected

            RadTreeWebsiteAdministration.Nodes.Add(rtnWebsiteAdministration)
        Next

    End Sub

    Protected Sub lnkWebsiteAdministration_Click(ByVal sender As Object, ByVal args As EventArgs) Handles lnkWebsiteAdministration.Click
        'Reload the moduls nav and main panels
        CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, "/admin/richtemplate_welcome.aspx?mode=administration", "/admin/richtemplate_list_administration.aspx", String.Empty, String.Empty)
    End Sub
End Class
