
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Partial Class admin_modules_access_editAddGroup
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue
    Dim ModuleTypeID As Integer = 8 ' Module Type: Members

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Setup Rad Controls
        CommonWeb.SetupRadDatePicker(expirationDate, intSiteID)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Member_Admin.Member_AddEdit_Group_Header

        'If there are more than 1 site, then show the site RadioButtonList, so the AdminUser can associate this group to just this site or All Sites, we default this to THIS SITE ONLY
        trSiteAccess.Visible = SiteDAL.GetSiteList().Rows.Count > 1 ' Only allow the AdminUser to control if this group should be for just this site OR All Sites, only if there is more than one site

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, intSiteID) Then
                Response.Redirect("/richadmin")
            End If

            If Not Request.QueryString("ID") Is Nothing Then

                Dim intGroupID As Integer = Convert.ToInt32(Request.QueryString("ID"))

                Dim dtGroup As DataTable = GroupDAL.GetGroup_ByGroupIDAndSiteID(intGroupID, SiteDAL.GetCurrentSiteID_Admin())
                If dtGroup.Rows.Count > 0 Then
                    Dim drGroup As DataRow = dtGroup.Rows(0)

                    btnAddEdit.Text = Resources.Member_Admin.Member_AddEdit_Group_ButtonUpdate

                    Dim boolGroupImportedFromActiveDirectory As Boolean = False
                    If ((Not drGroup("ActiveDirectory_Identifier") Is Nothing) AndAlso (drGroup("ActiveDirectory_Identifier").ToString().Trim().Length > 0)) Then
                        boolGroupImportedFromActiveDirectory = True
                    End If

                    'If data is found, fill textboxes
                    Me.groupName.Text = drGroup("groupName").ToString()
                    Me.groupDescription.Text = drGroup("groupDescription").ToString()

                    Me.groupPassword.Text = drGroup("groupPassword").ToString()

                    If Not drGroup("expirationDate").ToString() = "" Then
                        Me.expirationDate.SelectedDate = drGroup("expirationDate").ToString()
                    End If
                    Me.groupActive.SelectedValue = drGroup("groupActive").ToString()

                    'Finally Check if we should make this Group READ-ONLY, it would be read-only if its is available to all sites, and created by a site other than this site
                    'OR the site was imported from active directory
                    Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(drGroup("AvailableToAllSites"))
                    rblSite.SelectedValue = boolAvailableToAllSites.ToString().ToLower()
                    If boolAvailableToAllSites Then
                        Dim intSiteID_CurrentRow As Integer = Convert.ToInt32(drGroup("SiteID"))
                        If Not intSiteID = intSiteID_CurrentRow Then
                            MakeGroupReadOnly(intSiteID_CurrentRow)
                        End If
                    End If

                    'If this group is an active directory group, Therefore this should be made readonly
                    If boolGroupImportedFromActiveDirectory Then
                        SetGroupReadOnlyActiveDirectoryFields()
                    End If

                Else
                    'Can not find this group so redirect to the default page
                    Response.Redirect("default.aspx")
                End If

            Else
                btnAddEdit.Text = Resources.Member_Admin.Member_AddEdit_Group_ButtonAdd
                groupActive.SelectedValue = True

            End If
        End If

    End Sub

    Private Sub MakeGroupReadOnly(ByVal SiteID As Integer)

        divAssociateWithSite.Visible = False
        divAssociateWithSite_PublicMessage.Visible = True

        'Get the site that created this entry
        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(SiteID)
        If dtSite.Rows.Count > 0 Then
            litSiteName.Text = dtSite.Rows(0)("SiteName")
        End If

        'Prevent AdminUser from updating this record
        btnAddEdit.Visible = False

    End Sub

    Private Sub SetGroupReadOnlyActiveDirectoryFields()
        'For all input fields that are stored in active directory, hide their OUTER DIVS and show literals in their place
        divActiveDirectoryGroupMessage.Visible = True

        divGroupActive.Visible = False
        litGroupActive.Text = groupActive.SelectedItem.Text
        divGroupActive_ReadOnly.Visible = True

        divGroupName.Visible = False
        litGroupName.Text = groupName.Text
        divGroupName_ReadOnly.Visible = True

        divGroupDescription.Visible = False
        litGroupDescription.Text = groupDescription.Text
        divGroupDescription_ReadOnly.Visible = True

        trSiteAccess.Visible = False


    End Sub

    Protected Sub addUpdateAccess()

        If Request.QueryString("ID") Is Nothing Then

            Dim strGroupName As String = groupName.Text.Trim()
            Dim strGroupDescription As String = groupDescription.Text.Trim()
            Dim strGroupPassword As String = groupPassword.Text.Trim()

            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not expirationDate.SelectedDate.ToString() = "" Then
                dtExpiration = expirationDate.SelectedDate
            End If

            Dim boolGroupActive As Boolean = groupActive.SelectedValue

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim groupID As Integer = GroupDAL.InsertGroup(strGroupName, strGroupDescription, intSiteID, boolAvailableToAllSites, strGroupPassword, dtExpiration, boolGroupActive)

        Else

            Dim intGroupID As Integer = Convert.ToInt32(Request.QueryString("ID"))

            Dim strGroupName As String = groupName.Text.Trim()
            Dim strGroupDescription As String = groupDescription.Text.Trim()

            Dim boolAvailableToAllSites As Boolean = Convert.ToBoolean(rblSite.SelectedValue)

            Dim strGroupPassword As String = groupPassword.Text.Trim()

            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not expirationDate.SelectedDate.ToString() = "" Then
                dtExpiration = expirationDate.SelectedDate
            End If

            Dim boolGroupActive As Boolean = groupActive.SelectedValue

            GroupDAL.UpdateGroup_ByGroupID(intGroupID, strGroupName, strGroupDescription, intSiteID, boolAvailableToAllSites, strGroupPassword, dtExpiration, boolGroupActive)
        End If

    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click

        addUpdateAccess()
        Response.Redirect("default.aspx")

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("default.aspx")
    End Sub


End Class
