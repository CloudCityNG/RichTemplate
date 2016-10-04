Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Partial Class admin_modules_comments_defaultModule
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 2 ' Module Type: Comments

    Dim ModuleTypeID_ToView As Integer = 0
    Dim RecordID_ToView As Integer = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteComment, Resources.Comment_Admin.Comment_DefaultModule_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgModuleComments, "{4} {5} " & Resources.Comment_Admin.Comment_DefaultModule_Grid_Pager_PagerTextFormat_ItemsInDefault & " {1} " & Resources.Comment_Admin.Comment_DefaultModule_Grid_Pager_PagerTextFormat_PageDefault)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Comment_Admin.Comment_DefaultModule_HeadingDefault

        If Not Request.Params("mtid") Is Nothing Then
            ModuleTypeID_ToView = Convert.ToInt32(Request.Params("mtid"))
        End If
        If Not Request.Params("RecordID") Is Nothing Then
            RecordID_ToView = Convert.ToInt32(Request.Params("RecordID"))
        End If

        SetupModuleInfo()
        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this COMMENT module and if ModuleTypeID_ToView > 0, Check if this user has access to this module also, so if you can not view blogs, you should not view blog comments
            ' If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin/")
            End If

            rgModuleComments.Rebind()
        End If
    End Sub

    Private Sub SetupModuleInfo()
        If ModuleTypeID_ToView > 0 Then
            Dim dtModule As DataTable = ModuleDAL.GetModule_ByModuleTypeIDAndSiteID_FrontEnd(ModuleTypeID_ToView, SiteDAL.GetCurrentSiteID_Admin())
            If dtModule.Rows.Count > 0 Then

                Dim drModule As DataRow = dtModule.Rows(0)

                Dim strModuleLocation As String = drModule("moduleLocation_Admin")
                Dim strModuleLanguageFilename_Admin As String = drModule("moduleLanguageFilename_Admin")

                'Attach a delete confirmation to our delete button, based on the type of module
                Dim strDeleteConfirmationMessage As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Comment_DefaultModule_GridDeleteButton_ConfirmationMessage")
                CommonWeb.SetupDeleteButton(btnDeleteComment, strDeleteConfirmationMessage)

                'set the grid's PagerTextFormat
                Dim strPagerTextFormatItemsIn As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Comment_DefaultModule_Grid_Pager_PagerTextFormat_ItemsIn")
                Dim strPagerTextFormatPage As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Comment_DefaultModule_Grid_Pager_PagerTextFormat_Page")
                rgModuleComments.PagerStyle.PagerTextFormat = "{4} {5} " & strPagerTextFormatItemsIn & " {1} " & strPagerTextFormatPage

                'setup the comment module heading
                Dim strCommentHeading As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Comment_DefaultModule_Heading")
                ucHeader.PageName = strCommentHeading

                'setup the comment module body heading
                Dim strCommentHeadingBody As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Comment_DefaultModule_HeadingBody")
                litModuleCommentHeadingBody.Text = strCommentHeadingBody

                'Does the AdminUser have access to this module? if so show the return to Module links
                Dim listAllowModules As String() = AdminUserDAL.GetCurrentAdminUserAllowModules().Split(",")
                If listAllowModules.Contains(ModuleTypeID_ToView.ToString()) Then
                    'setup the return to module link and literal
                    Dim strReturnToModule As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Comment_DefaultModule_ReturnToModule")
                    litReturnToModule.Text = strReturnToModule

                    Me.aReturnToModule_Img.HRef = strModuleLocation
                    Me.aReturnToModule.HRef = strModuleLocation

                    Me.divReturnToModule.Visible = True
                End If

            End If
        Else
            'WE DO NOT PROVIDE COMMENTS FOR WEB PAGES IN THIS SITE
            'Me.divViewWebPageComments.Visible = True
        End If
    End Sub

    Protected Sub rgModuleComments_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgModuleComments.NeedDataSource
        Dim dtComments As DataTable = Nothing
        If ModuleTypeID_ToView > 0 Then

            If RecordID_ToView > 0 Then
                dtComments = CommentDAL.GetCommentList_ByModuleTypeIDAndRecordIDAndSiteID(ModuleTypeID_ToView, RecordID_ToView, SiteDAL.GetCurrentSiteID_Admin())
            ElseIf ModuleTypeID_ToView > 0 Then
                dtComments = CommentDAL.GetCommentList_ByModuleTypeIDAndSiteID(ModuleTypeID_ToView, SiteDAL.GetCurrentSiteID_Admin())
                rgModuleComments.Columns(2).Visible = True
            End If
            rgModuleComments.DataSource = dtComments

        Else

            dtComments = CommentDAL.GetCommentList_ForAllModules_BySiteID(SiteDAL.GetCurrentSiteID_Admin())
            rgModuleComments.Columns(1).Visible = True
            rgModuleComments.Columns(2).Visible = True

            rgModuleComments.DataSource = dtComments

        End If

    End Sub


    Protected Sub btnDeleteComment_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgModuleComments.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("ID")
                CommentDAL.DeleteCommentModule_ByCommentID(intRecordId)
                CommentDAL.DeleteComment(intRecordId)
            End If
        Next
        rgModuleComments.Rebind()

    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkEdit As LinkButton = sender
        Dim intCommentID As Integer = Convert.ToInt32(lnkEdit.CommandArgument)

        Dim strReturnUrl As String = "commentID=" & intCommentID
        If ModuleTypeID_ToView > 0 Then
            strReturnUrl &= "&mtid=" & ModuleTypeID_ToView
            If RecordID_ToView > 0 Then
                strReturnUrl &= "&RecordID=" & RecordID_ToView
            End If
        End If
        strReturnUrl = "/admin/modules/comments/editModule.aspx?" & strReturnUrl
        Response.Redirect("/admin/modules/comments/editModule.aspx?commentID=" & intCommentID & "&returnURL=" & CommonWeb.encodeHyperlink(strReturnUrl))

    End Sub

End Class
