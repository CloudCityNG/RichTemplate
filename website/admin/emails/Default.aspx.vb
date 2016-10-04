Imports Telerik.Web.UI
Imports System.Data

Partial Class admin_emails_Default
    Inherits RichTemplateLanguagePage


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgEmailTemplate, "{4} {5} " & Resources.Email.Email_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Email.Email_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Email.Email_Default_Header
        ucHeader.PageHelpID = 15 'Help Item for Email Administration

        'Check the logged in user can view this page
        Dim intAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intAdminUserAccess > 1 Then
            'perhaps do something
        Else
            Response.Redirect("~/richadmin/")
        End If

        If Not Page.IsPostBack Then

        End If

    End Sub

    Public Sub rgEmailTemplate_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmailTemplate.NeedDataSource

        Dim intSiteID As Integer = SiteDAL.GetCurrentSiteID_Admin()
        If intSiteID > 0 Then
            Dim dtEmailTemplateList As DataTable = EmailDAL.GetEmailTemplateList_BySiteID(intSiteID)
            rgEmailTemplate.DataSource = dtEmailTemplateList
        End If

    End Sub

    Private Sub rgEmailTemplate_ItemDataBound(ByVal source As Object, ByVal e As GridItemEventArgs) Handles rgEmailTemplate.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            'setup Edit link
            Dim intEmailTemplateID As Integer = Convert.ToInt32(drItem("EmailTemplateID"))

            Dim aEmailTemplateEdit As HtmlAnchor = DirectCast(item.FindControl("aEmailTemplateEdit"), HtmlAnchor)
            aEmailTemplateEdit.HRef = "editAdd.aspx?ID=" + intEmailTemplateID.ToString

        End If
    End Sub

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgEmailTemplate.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("EmailTemplateID")
                EmailDAL.DeleteEmailTemplate_ByEmailTemplateID(intRecordId)
            End If
        Next
        rgEmailTemplate.Rebind()

    End Sub

End Class

