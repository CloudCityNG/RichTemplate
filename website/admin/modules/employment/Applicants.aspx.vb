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
        CommonWeb.SetupDeleteButton(btnDeleteRegistration, Resources.Employment_Admin.Employment_Applicants_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgEmploymentRegistrations, "{4} {5} " & Resources.Employment_Admin.Employment_Applicants_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Employment_Admin.Employment_Applicants_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Employment_Admin.Employment_Applicants_Header
        ucHeader.PageHelpID = 5 'Help Item for Employment/Job Opportunities 

    End Sub

    Protected Sub rgEmploymentRegistrations_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgEmploymentRegistrations.NeedDataSource
        'Load employment applicants
        If Not Request.QueryString("employmentID") Is Nothing Then
            Dim intEmploymentID As Integer = Convert.ToInt32(Request.QueryString("employmentID"))
            rgEmploymentRegistrations.DataSource = EmploymentDAL.GetEmploymentSubmissions_ByEmploymentIDAndSiteID(intEmploymentID, SiteDAL.GetCurrentSiteID_Admin())
        End If

    End Sub

    Protected Sub btnDeleteRegistration_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgEmploymentRegistrations.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("subID")
                EmploymentDAL.DeleteEmploymentSubmission_BySubID(intRecordId)
            End If
        Next
        rgEmploymentRegistrations.Rebind()

    End Sub


End Class
