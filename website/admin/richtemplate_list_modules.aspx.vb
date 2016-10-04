Imports System.Data.SqlClient
Imports System.Data
Imports Telerik.Web.UI
Imports System.Drawing

Partial Class admin_richtemplate_list_modules
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Check thes user exists
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        If intAdminUserID > 0 Then

            Dim strAllowModules As String = AdminUserDAL.GetCurrentAdminUserAllowModules()
            If strAllowModules.Length > 0 Then
                'Load Modules
                LoadModules(strAllowModules)

            Else
                Response.Redirect("~/richadmin/")
            End If
        Else
            Response.Redirect("~/richadmin/")
        End If

    End Sub

    Protected Sub LoadModules(ByVal strAllowModules As String)

        'Populate our section tree
        RadTreeModules.Nodes.Clear()

        If strAllowModules.Length > 0 Then
            Dim listAllowModules As String() = strAllowModules.Split(",")

            Dim dtModules As DataTable = ModuleDAL.GetModuleList_BySiteID(SiteDAL.GetCurrentSiteID_Admin())

            'We need to create a new datatable with the same dimensions as our dtModules table, then only add rows to our new dataTable if the user is allowed access to that module
            'Loop through all available modules and if the user has access add to dtAllowModules
            Dim dtAllowModules As DataTable = dtModules.Clone()
            For Each drModule As DataRow In dtModules.Rows
                Dim intModuleTypeID_Current As Integer = Convert.ToInt32(drModule("ModuleTypeID"))
                For Each strAllowModuleTypeID As String In listAllowModules
                    Dim intAllowModuleTypeID As Integer = Convert.ToInt32(strAllowModuleTypeID)
                    If intModuleTypeID_Current = intAllowModuleTypeID Then

                        'Add this row to our new data table of allowed modules
                        Dim drAllowModule As DataRow = dtAllowModules.NewRow()
                        drAllowModule.ItemArray = drModule.ItemArray
                        dtAllowModules.Rows.Add(drAllowModule)
                        Exit For
                    End If
                Next
            Next

            'Get the current moduleTypeID from the request string, if it exists
            Dim intModuleTypeID As Integer = 0
            If Not Request.Params("mtID") Is Nothing Then
                intModuleTypeID = Convert.ToInt32(Request.Params("mtID"))
            End If
            'Create a node for each section then adds it to our tree
            For Each drAllowModule As DataRow In dtAllowModules.Rows
                Dim intModuleTypeID_Current As Integer = Convert.ToInt32(drAllowModule("ModuleTypeID"))
                Dim strModuleLocation As String = drAllowModule("ModuleLocation_Admin").ToString()
                Dim strModuleLanguageFilename_Admin As String = drAllowModule("moduleLanguageFilename_Admin")

                'Get the name of the module, based on the users preferred language
                Dim strModuleName_LanguageSpecific As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_RichTemplate_List_Modules_ModuleName")

                Dim boolModuleActive As Boolean = Convert.ToBoolean(drAllowModule("active"))

                Dim strModuleText As String = ""
                If boolModuleActive Then
                    strModuleText = "&nbsp;<img src='/images/folder_full_sm.png' class='rtImg' alt='" & strModuleName_LanguageSpecific & "'><span class='inner_rtIn'><a href='" & strModuleLocation & "' target='basefrm'><font size='1'><b>&nbsp;" & strModuleName_LanguageSpecific & "</b></font></a></span>"
                Else
                    strModuleText = "&nbsp;<img src='/images/folder_full_sm.png' class='rtImg inactive' alt='" & strModuleName_LanguageSpecific & "'><span class='inner_rtIn inactive'><font size='1' color='#808080'><b>&nbsp;" & strModuleName_LanguageSpecific & "</b></font></span>"
                End If


                Dim rtnModule As New RadTreeNode(strModuleText, intModuleTypeID_Current.ToString())
                rtnModule.Category = "Folder"
                rtnModule.Expanded = True
                rtnModule.AllowDrag = False
                rtnModule.AllowDrop = False

                If intModuleTypeID_Current = intModuleTypeID Then
                    rtnModule.Selected = True
                End If

                RadTreeModules.Nodes.Add(rtnModule)
            Next
        End If
    End Sub

    Protected Sub lnkModules_Click(ByVal sender As Object, ByVal args As EventArgs) Handles lnkModules.Click
        'Reload the moduls nav and main panels
        CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, "/admin/richtemplate_welcome.aspx?mode=modules", "/admin/richtemplate_list_modules.aspx", String.Empty, String.Empty)
    End Sub
End Class
