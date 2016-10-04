Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_employment_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 4 ' Module Type: Employment

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If
        End If

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteLive, Resources.Employment_Admin.Employment_Default_GridDeleteButton_ConfirmationMessage)
        CommonWeb.SetupDeleteButton(btnDeleteArchive, Resources.Employment_Admin.Employment_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgEmployment, "{4} {5} " & Resources.Employment_Admin.Employment_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Employment_Admin.Employment_Default_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadGrid(rgEmploymentArchive, "{4} {5} " & Resources.Employment_Admin.Employment_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Employment_Admin.Employment_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Employment_Admin.Employment_Default_Header
        ucHeader.PageHelpID = 5 'Help Item for Employment/Job Opportunities

        'Check we need to show comments and online registration columns
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "allow_online_registrations" Then
                'show the registration column
                Dim gcApplicantsCount As GridColumn = rgEmployment.Columns.FindByUniqueName("applicants")
                gcApplicantsCount.Visible = True

                Dim gcApplicantsCountArchive As GridColumn = rgEmploymentArchive.Columns.FindByUniqueName("applicants")
                gcApplicantsCountArchive.Visible = True

            End If
        Next

    End Sub

    Public Sub rgEmployment_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmployment.NeedDataSource

        Dim dtEmployment As DataTable = EmploymentDAL.GetEmploymentList_ByStatusAndSiteID(True, SiteDAL.GetCurrentSiteID_Admin())
        rgEmployment.DataSource = dtEmployment
    End Sub

    Public Sub rgEmploymentArchive_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmploymentArchive.NeedDataSource

        Dim dtEmployment As DataTable = EmploymentDAL.GetEmploymentList_ByStatusAndSiteID(False, SiteDAL.GetCurrentSiteID_Admin())
        rgEmploymentArchive.DataSource = dtEmployment
    End Sub

    Private Sub rgEmployment_ItemDataBound(ByVal source As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployment.ItemDataBound, rgEmploymentArchive.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            Dim intEmploymentID As Integer = Convert.ToInt32(drItem("employmentID"))

            'update registration text
            Dim boolOnlineSignup As Boolean = Convert.ToBoolean(drItem("OnlineSignup"))
            If boolOnlineSignup Then
                Dim intAppCount As String = Convert.ToInt32(drItem("appCount"))
                Dim hLinkApplicantCount As HyperLink = DirectCast(item("applicants").Controls(0), HyperLink)
                hLinkApplicantCount.Text = Resources.Employment_Admin.Employment_Default_GridApplications & " (" & intAppCount & ")"
            End If

            'setup Edit link
            Dim aEmploymentEdit As HtmlAnchor = DirectCast(item.FindControl("aEmploymentEdit"), HtmlAnchor)
            aEmploymentEdit.HRef = "editAdd.aspx?ID=" + intEmploymentID.ToString

        End If
    End Sub

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgEmployment.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("employmentID")

                EmploymentDAL.DeleteEmployment_ByEmploymentID(intRecordId)
                EmploymentDAL.DeleteEmploymentArchive_ByEmploymentID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)

            End If
        Next
        rgEmployment.Rebind()

    End Sub

    Protected Sub btnDeleteArchive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgEmploymentArchive.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("employmentID")

                EmploymentDAL.DeleteEmployment_ByEmploymentID(intRecordId)
                EmploymentDAL.DeleteEmploymentArchive_ByEmploymentID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        rgEmploymentArchive.Rebind()

    End Sub

End Class
