Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_blog_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 1 ' Module Type: Blogs

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDelete, Resources.Blog_Admin.Blog_Default_GridDeleteButton_ConfirmationMessage)
        CommonWeb.SetupDeleteButton(btnDeleteArchive, Resources.Blog_Admin.Blog_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgBlogs, "{4} {5} " & Resources.Blog_Admin.Blog_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Blog_Admin.Blog_Default_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadGrid(rgBlogsArchive, "{4} {5} " & Resources.Blog_Admin.Blog_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Blog_Admin.Blog_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Blog_Admin.Blog_Default_Header

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            'Check we need to show the book this link, but only if its active
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" AndAlso AdminUserDAL.CheckAdminUserModuleAccess(2, SiteDAL.GetCurrentSiteID_Admin()) Then
                    'show the commentCount column
                    Dim gcComments As GridColumn = rgBlogs.Columns.FindByUniqueName("comments")
                    gcComments.Visible = True

                    Dim gcCommentsArchive As GridColumn = rgBlogsArchive.Columns.FindByUniqueName("comments")
                    gcCommentsArchive.Visible = True
                End If
            Next
        End If

    End Sub

    Public Sub rgBlogs_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgBlogs.NeedDataSource

        Dim dtBlog As DataTable = BlogDAL.GetBlogList_ByStatusAndSiteID(True, SiteDAL.GetCurrentSiteID_Admin())
        rgBlogs.DataSource = dtBlog
    End Sub

    Public Sub rgBlogsArchive_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgBlogsArchive.NeedDataSource

        Dim dtBlog As DataTable = BlogDAL.GetBlogList_ByStatusAndSiteID(False, SiteDAL.GetCurrentSiteID_Admin())
        rgBlogsArchive.DataSource = dtBlog
    End Sub

    Private Sub rgBlogs_ItemDataBound(ByVal source As Object, ByVal e As GridItemEventArgs) Handles rgBlogs.ItemDataBound, rgBlogsArchive.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            Dim intBlogID As Integer = Convert.ToInt32(drItem("blogID"))

            'Update comment text
            Dim intCommentCountApproved As Integer = Convert.ToInt32(drItem("commentCountApproved"))
            Dim intCommentCountPending As Integer = Convert.ToInt32(drItem("commentCountPending"))

            Dim sbCommentText As New StringBuilder()
            sbCommentText.Append(If(intCommentCountApproved > 0, "<span class='commentTextApproved'>" & intCommentCountApproved.ToString() & "</span>", ""))
            If (intCommentCountApproved > 0) AndAlso (intCommentCountPending > 0) Then
                sbCommentText.Append(" / ")
            End If
            sbCommentText.Append(If(intCommentCountPending > 0, "<span class='commentTextPending'>" & intCommentCountPending.ToString() & "</span>", ""))

            Dim strCommentText As String = sbCommentText.ToString()
            If strCommentText.Length > 0 Then
                Dim hLinkComments As HyperLink = DirectCast(item("comments").Controls(0), HyperLink)
                hLinkComments.NavigateUrl = "/admin/modules/Comments/defaultModule.aspx?mtID=" & ModuleTypeID & "&recordID=" & intBlogID
                hLinkComments.Text = Resources.Blog_Admin.Blog_Default_GridComments & " (" & strCommentText & ")"
            End If

            'Before we show this linkbutton to view comments, we check if this blog record, was created for a different site, in which case its a read-only blog record, and you can not view comments from this site
            Dim intSiteID As Integer = Convert.ToInt32(drItem("SiteID"))
            If Not intSiteID = SiteDAL.GetCurrentSiteID_Admin() Then
                Dim hLinkComments As HyperLink = DirectCast(item("comments").Controls(0), HyperLink)
                hLinkComments.Visible = False
            End If

            'setup Edit link
            Dim aBlogEdit As HtmlAnchor = DirectCast(item.FindControl("aBlogEdit"), HtmlAnchor)
            aBlogEdit.HRef = "editAdd.aspx?ID=" + intBlogID.ToString

        End If
    End Sub

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgBlogs.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("blogID")

                BlogDAL.DeleteBlog_ByBlogID(intRecordId)
                BlogDAL.DeleteBlogArchive_ByBlogID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        rgBlogs.Rebind()

    End Sub

    Protected Sub btnDeleteArchive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgBlogsArchive.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("blogID")

                BlogDAL.DeleteBlog_ByBlogID(intRecordId)
                BlogDAL.DeleteBlogArchive_ByBlogID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        rgBlogsArchive.Rebind()

    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnkAdd As LinkButton = DirectCast(sender, LinkButton)
        Session("selectedEditTab") = 0
        Response.Redirect("editAdd.aspx")
    End Sub

    Protected Sub lnkComments_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkComments As LinkButton = sender
        Dim intBlogID As Integer = Convert.ToInt32(lnkComments.CommandArgument)

        Response.Redirect("/admin/modules/comments/defaultModule.aspx?mtID=" & ModuleTypeID & "&recordID=" & intBlogID)
    End Sub

End Class
