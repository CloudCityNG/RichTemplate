Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_pressrelease_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 10 ' Module Type: Press Release

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If
        End If

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteLive, Resources.PressRelease_Admin.PressRelease_Default_GridDeleteButton_ConfirmationMessage)
        CommonWeb.SetupDeleteButton(btnDeleteArchive, Resources.PressRelease_Admin.PressRelease_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgPressReleases, "{4} {5} " & Resources.PressRelease_Admin.PressRelease_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.PressRelease_Admin.PressRelease_Default_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadGrid(rgPressReleasesArchive, "{4} {5} " & Resources.PressRelease_Admin.PressRelease_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.PressRelease_Admin.PressRelease_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.PressRelease_Admin.PressRelease_Default_Header
        ucHeader.PageHelpID = 4 'Help Item for Press Releases

        'Check we need to show comments
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" AndAlso AdminUserDAL.CheckAdminUserModuleAccess(2, SiteDAL.GetCurrentSiteID_Admin()) Then
                'show the commentCount column
                Dim gcCommentCount As GridColumn = rgPressReleases.Columns.FindByUniqueName("comments")
                gcCommentCount.Visible = True

                Dim gcCommentCountArchive As GridColumn = rgPressReleasesArchive.Columns.FindByUniqueName("comments")
                gcCommentCountArchive.Visible = True

            End If
        Next

    End Sub

    Public Sub rgPressReleases_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgPressReleases.NeedDataSource

        Dim dtPressRelease As DataTable = PressReleaseDAL.GetPressRelease_ByStatusAndSiteID(True, SiteDAL.GetCurrentSiteID_Admin())
        rgPressReleases.DataSource = dtPressRelease
    End Sub

    Public Sub rgPressReleasesArchive_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgPressReleasesArchive.NeedDataSource

        Dim dtPressRelease As DataTable = PressReleaseDAL.GetPressRelease_ByStatusAndSiteID(False, SiteDAL.GetCurrentSiteID_Admin())
        rgPressReleasesArchive.DataSource = dtPressRelease
    End Sub

    Private Sub rgPressReleases_ItemDataBound(ByVal source As Object, ByVal e As GridItemEventArgs) Handles rgPressReleases.ItemDataBound, rgPressReleasesArchive.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            Dim intPressReleaseID As Integer = Convert.ToInt32(drItem("prID"))

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
                hLinkComments.NavigateUrl = "/admin/modules/Comments/defaultModule.aspx?mtID=" & ModuleTypeID & "&recordID=" & intPressReleaseID
                hLinkComments.Text = Resources.PressRelease_Admin.PressRelease_Default_GridComments & " (" & strCommentText & ")"
            End If

            'Before we show this linkbutton to view comments, we check if this press release record, was created for a different site, in which case its a read-only press release record, and you can not view comments from this site
            Dim intSiteID As Integer = Convert.ToInt32(drItem("SiteID"))
            If Not intSiteID = SiteDAL.GetCurrentSiteID_Admin() Then
                Dim hLinkComments As HyperLink = DirectCast(item("comments").Controls(0), HyperLink)
                hLinkComments.Visible = False
            End If

            'setup Edit link
            Dim aPressReleaseEdit As HtmlAnchor = DirectCast(item.FindControl("aPressReleaseEdit"), HtmlAnchor)
            aPressReleaseEdit.HRef = "editAdd.aspx?ID=" + intPressReleaseID.ToString
        End If
    End Sub

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgPressReleases.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then

                Dim intRecordId As Integer = grdItem.GetDataKeyValue("prID")
                PressReleaseDAL.DeletePressRelease_ByPressReleaseID(intRecordId)
                PressReleaseDAL.DeletePressReleaseArchive_ByPressReleaseID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        rgPressReleases.Rebind()

    End Sub

    Protected Sub btnDeleteArchive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgPressReleasesArchive.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("prID")

                PressReleaseDAL.DeletePressRelease_ByPressReleaseID(intRecordId)
                PressReleaseDAL.DeletePressReleaseArchive_ByPressReleaseID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        rgPressReleasesArchive.Rebind()

    End Sub

End Class
