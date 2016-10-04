
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_comments_editAddModule
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 2 ' Module Type: Comments

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Comment_Admin.Comment_AddEditModule_HeadingDefault

        If Not Page.IsPostBack Then

            If Not Request.QueryString("id") Is Nothing Then

                'Check Access, Does the current AdminUser have access to this COMMENT module and if ModuleTypeID_ToView > 0, Check if this user has access to this module also, so if you can not view blogs, you should not view blog comments
                ' If not Log them out and send them to login page
                If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                    Response.Redirect("/richadmin/")
                End If

                Dim intCommentID As Integer = Convert.ToInt32(Request.QueryString("id"))
                Dim dtComment As DataTable = CommentDAL.GetComment_ByIDAndSiteID_ForModules(intCommentID, SiteDAL.GetCurrentSiteID_Admin())
                If dtComment.Rows.Count > 0 Then
                    Dim drComment As DataRow = dtComment.Rows(0)

                    Status.SelectedValue = drComment("active").ToString()
                    txtComment.Text = drComment("Comment")
                    rrComment.Value = drComment("Rating")

                    btnEditAdd.Text = Resources.Comment_Admin.Comment_AddEditModule_ButtonUpdate

                    'setup the comment module heading and body heading, only if the moduleTypeID exists
                    If Not Request.QueryString("mtID") Is Nothing Then
                        Dim intModuleTypeID As Integer = Request.QueryString("mtID")
                        Dim dtModules As DataTable = ModuleDAL.GetModule_ByModuleTypeIDAndSiteID_FrontEnd(intModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())
                        If dtModules.Rows.Count > 0 Then

                            Dim drModule As DataRow = dtModules.Rows(0)

                            Dim strModuleLanguageFilename_Admin As String = drModule("moduleLanguageFilename_Admin")

                            'setup the comment module heading
                            Dim strCommentHeading As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Comment_AddEditModule_Heading")
                            ucHeader.PageName = strCommentHeading

                            'setup the comment module body heading
                            Dim strCommentHeadingBody As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Comment_AddEditModule_HeadingBody")
                            litModuleCommentHeadingBody.Text = strCommentHeadingBody


                        Else
                            'A moduleTypeID was supplied that does not exist, so url was hacked
                            Response.Redirect("/richadmin/")
                        End If

                    End If
                Else
                    Dim sbReturnURL As New StringBuilder
                    sbReturnURL.Append("defaultModule.aspx")

                    If Not Request.Params("mtID") Is Nothing Then
                        sbReturnURL.Append("?mtID=" & Request.Params("mtID"))
                        If Not Request.Params("recordID") Is Nothing Then
                            sbReturnURL.Append("&recordID=" & Request.Params("recordID"))
                        End If
                    End If

                    Response.Redirect(sbReturnURL.ToString())
                End If
            Else
                Dim sbReturnURL As New StringBuilder
                sbReturnURL.Append("defaultModule.aspx")

                If Not Request.Params("mtID") Is Nothing Then
                    sbReturnURL.Append("?mtID=" & Request.Params("mtID"))
                    If Not Request.Params("recordID") Is Nothing Then
                        sbReturnURL.Append("&recordID=" & Request.Params("recordID"))
                    End If
                End If

                Response.Redirect(sbReturnURL.ToString())
            End If

        End If

    End Sub

    Protected Sub addUpdateRecord()

        If Not Request.QueryString("id") Is Nothing Then
            Dim intCommentID As Integer = Convert.ToInt32(Request.QueryString("id"))

            Dim strComment As String = txtComment.Text.Trim()
            Dim decRating As Decimal = rrComment.Value
            Dim boolActive As Boolean = Convert.ToBoolean(Status.SelectedValue)

            CommentDAL.UpdateComment(intCommentID, strComment, decRating, boolActive)
        End If

    End Sub


    Protected Sub btnEditAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditAdd.Click
        addUpdateRecord()

        Dim sbReturnURL As New StringBuilder
        sbReturnURL.Append("defaultModule.aspx")

        If Not Request.Params("mtID") Is Nothing Then
            sbReturnURL.Append("?mtID=" & Request.Params("mtID"))
            If Not Request.Params("recordID") Is Nothing Then
                sbReturnURL.Append("&recordID=" & Request.Params("recordID"))
            End If
        End If

        Response.Redirect(sbReturnURL.ToString())
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Dim sbReturnURL As New StringBuilder
        sbReturnURL.Append("defaultModule.aspx")

        If Not Request.Params("mtID") Is Nothing Then
            sbReturnURL.Append("?mtID=" & Request.Params("mtID"))
            If Not Request.Params("recordID") Is Nothing Then
                sbReturnURL.Append("&recordID=" & Request.Params("recordID"))
            End If
        End If

        Response.Redirect(sbReturnURL.ToString())

    End Sub
End Class
