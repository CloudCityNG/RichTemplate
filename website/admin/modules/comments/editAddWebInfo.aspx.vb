
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_comments_editAddWebInfo
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 2 ' Module Type: Comments

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Comment_Admin.Comment_AddEditWebInfo_HeadingBody

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            'If the url specified contains a webinfoID, that allows the user to go back to that specific webinfo page FROM THE DEFAULT, we first ensure they have acces to webcontent
            ' otherwise we don't set the webinfoID and it won't get passed to the default page
            Dim intWebInfoID As Integer = 0
            If Not Request.Params("webinfoID") Is Nothing Then
                intWebInfoID = Convert.ToInt32(Request.Params("webinfoID"))
            End If

            If Not Request.QueryString("id") Is Nothing Then
                Dim intCommentID As Integer = Convert.ToInt32(Request.QueryString("id"))
                Dim dtComment As DataTable = CommentDAL.GetComment_ByIDAndSiteID_ForWebInfo(intCommentID, SiteDAL.GetCurrentSiteID_Admin())
                If dtComment.Rows.Count > 0 Then
                    Dim drComment As DataRow = dtComment.Rows(0)
                    Status.SelectedValue = drComment("active").ToString()
                    txtComment.Text = drComment("Comment")
                    rrComment.Value = drComment("Rating")

                    btnEditAdd.Text = Resources.Comment_Admin.Comment_AddEditModule_ButtonUpdate
                Else
                    Dim sbReturnURL As New StringBuilder
                    sbReturnURL.Append("defaultWebInfo.aspx")

                    If intWebInfoID > 0 Then
                        sbReturnURL.Append("?webinfoID=" & intWebInfoID)
                    End If
                    Response.Redirect(sbReturnURL.ToString())
                End If
            Else
                Dim sbReturnURL As New StringBuilder
                sbReturnURL.Append("defaultWebInfo.aspx")

                If intWebInfoID > 0 Then
                    sbReturnURL.Append("?webinfoID=" & intWebInfoID)
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
        sbReturnURL.Append("defaultWebInfo.aspx")

        Dim intWebInfoID As Integer = 0
        If Not Request.Params("webinfoID") Is Nothing Then
            intWebInfoID = Convert.ToInt32(Request.Params("webinfoID"))
        End If
        If intWebInfoID > 0 Then
            sbReturnURL.Append("?webinfoID=" & Request.Params("webinfoID"))
        End If
        Response.Redirect(sbReturnURL.ToString())
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim sbReturnURL As New StringBuilder
        sbReturnURL.Append("defaultWebInfo.aspx")

        Dim intWebInfoID As Integer = 0
        If Not Request.Params("webinfoID") Is Nothing Then
            intWebInfoID = Convert.ToInt32(Request.Params("webinfoID"))
        End If
        If intWebInfoID > 0 Then
            sbReturnURL.Append("?webinfoID=" & Request.Params("webinfoID"))
        End If
        Response.Redirect(sbReturnURL.ToString())
    End Sub
End Class
