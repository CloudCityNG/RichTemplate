Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Partial Class admin_modules_searchtags_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 11 ' Module Type: Search Tags

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDelete, Resources.SearchTag_Admin.SearchTag_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgSearchTags, Resources.SearchTag_Admin.SearchTag_Default_Grid_AddNewEntry, "{4} {5} " & Resources.SearchTag_Admin.SearchTag_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.SearchTag_Admin.SearchTag_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.SearchTag_Admin.SearchTag_Default_Header

        'Set the SiteID for dsSearchTags
        dsSearchTags.SelectParameters("SiteID").DefaultValue = SiteDAL.GetCurrentSiteID_Admin()
        dsSearchTags.InsertParameters("SiteID").DefaultValue = SiteDAL.GetCurrentSiteID_Admin()

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this Search Tags module and if ModuleTypeID_ToView > 0, Check if this user has access to this module also, so if you can not view blogs, you should not view blog comments
            ' If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin/")
            End If

            If Not Request.QueryString("mtid") Is Nothing Then
                If Request.QueryString("mtid").ToString().Length > 0 Then

                    Dim intModuleTypeID_ToView As Integer = Convert.ToInt32(Request.QueryString("mtid"))

                    Dim dtModules As DataTable = ModuleDAL.GetModule_ByModuleTypeIDAndSiteID_FrontEnd(intModuleTypeID_ToView, SiteDAL.GetCurrentSiteID_Admin())
                    If dtModules.Rows.Count > 0 Then

                        'Does the AdminUser have access to this module?
                        Dim listAllowModules As String() = AdminUserDAL.GetCurrentAdminUserAllowModules().Split(",")
                        If listAllowModules.Contains(intModuleTypeID_ToView.ToString()) Then

                            Dim drModule As DataRow = dtModules.Rows(0)

                            Dim strModuleLocation As String = drModule("ModuleLocation_Admin")
                            Dim strModuleLanguageFilename_Admin As String = drModule("moduleLanguageFilename_Admin")

                            Dim strReturnToModule As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_SearchTags_ReturnToModule")
                            lnkBackToModule.Text = strReturnToModule
                            lnkBackToModule.PostBackUrl = strModuleLocation

                            divBackToModule.Visible = True
                        Else
                            'Dont have access to this module, so url hacked, redirect to login page
                            Response.Redirect("/richadmin/")
                        End If
                    Else
                        'Dont have access to this module, so url hacked, redirect to login page
                        Response.Redirect("/richadmin/")
                    End If

                End If
            ElseIf Not Request.QueryString("wid") Is Nothing Then
                If Request.QueryString("wid").ToString().Length > 0 Then

                    Dim intWebInfo_ToView As Integer = Convert.ToInt32(Request.QueryString("wid"))

                    Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByIDAndSiteID(intWebInfo_ToView, SiteDAL.GetCurrentSiteID_Admin())
                    If dtWebInfo.Rows.Count > 0 Then

                        'Does the AdminUser have access to web content?
                        If AdminUserDAL.GetCurrentAdminUserAllowWebContent() Then

                            lnkBackToWebPage.PostBackUrl = "/admin/richtemplate_page_editor.aspx?pageID=" & intWebInfo_ToView.ToString() & "&pageStatus=offline"
                            divBackToWebPage.Visible = True
                        Else
                            'Dont have access to this module, so url hacked, redirect to login page
                            Response.Redirect("/richadmin/")
                        End If
                    Else
                        'Dont have access to this module, so url hacked, redirect to login page
                        Response.Redirect("/richadmin/")
                    End If

                End If

            End If
        End If

        If Not Request.QueryString("rp") Is Nothing Then
            lnkBackToModule.PostBackUrl = Request.QueryString("rp")
        End If

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgSearchTags.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("searchTagID")
                SearchTagDAL.DeleteSearchTag_BySearchTagID(intRecordId)
            End If
        Next
        rgSearchTags.Rebind()
    End Sub

End Class
