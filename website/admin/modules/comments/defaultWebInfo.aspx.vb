Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Partial Class admin_modules_comments_DefaultWebInfo
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 2 ' Module Type: Comments

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteComment, Resources.Comment_Admin.Comment_DefaultWebInfo_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgComments, "{4} {5} " & Resources.Comment_Admin.Comment_DefaultWebInfo_Grid_Pager_PagerTextFormat_ItemsInDefault & " {1} " & Resources.Comment_Admin.Comment_DefaultWebInfo_Grid_Pager_PagerTextFormat_PageDefault)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Comment_Admin.Comment_DefaultWebInfo_Heading

        If Not Page.IsPostBack Then

            Dim intSiteID As Integer = SiteDAL.GetCurrentSiteID_Admin()
            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, intSiteID) Then
                Response.Redirect("/richadmin")
            End If

            'If the url specified contains a webinfoID, that allows the user to go back to that specific webinfo page, we first ensure they have acces to webcontent, before showing these urls
            If Not Request.Params("webinfoID") Is Nothing Then
                Dim intWebInfoID As Integer = Convert.ToInt32(Request.Params("webinfoID"))
                Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfo_ByIDAndSiteID(intWebInfoID, intSiteID)
                If dtWebInfo.Rows.Count > 0 Then
                    If AdminUserDAL.GetCurrentAdminUserAllowWebContent() Then

                        'setup the return to webinfo link and literal
                        Me.aReturnToWebInfo.HRef = "/admin/richtemplate_list_pages.aspx?webinfoID=" & intWebInfoID
                        Me.aReturnToWebInfo.HRef = "/admin/richtemplate_list_pages.aspx?webinfoID=" & intWebInfoID

                        Me.divReturnToWebInfo.Visible = True
                    End If
                Else
                    'Can not find this webinfo ID so redirect to the default page
                    Response.Redirect("defaultWebInfo.aspx")
                End If
            Else
                'Show the view Module Comments Link
                Me.divViewModuleComments.Visible = True
            End If


            rgComments.Rebind()
        End If
    End Sub


    Protected Sub RadGrid1_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgComments.NeedDataSource
        Dim dtComments As DataTable = Nothing
        If Not Request.Params("webinfoID") Is Nothing Then
            Dim intWebInfoID As Integer = Convert.ToInt32(Request.Params("webinfoID"))
            dtComments = CommentDAL.GetCommentList_ByWebInfoIDAndSiteID(intWebInfoID, SiteDAL.GetCurrentSiteID_Admin())
        Else
            dtComments = CommentDAL.GetCommentList_ForAllWebInfoPages(SiteDAL.GetCurrentSiteID_Admin())
            rgComments.Columns(1).Visible = True
        End If
        rgComments.DataSource = dtComments

    End Sub


    Protected Sub btnDeleteComment_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgComments.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("ID")
                CommentDAL.DeleteComment(intRecordId)
                CommentDAL.DeleteCommentWebInfo_ByCommentID(intRecordId)
            End If
        Next
        rgComments.Rebind()

    End Sub


End Class
