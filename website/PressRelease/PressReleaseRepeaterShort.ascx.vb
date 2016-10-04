Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class PressRelease_PressReleaseRepeaterShort
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 10

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Check we need to use group and user access
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True
            End If
        Next
        If Not IsPostBack Then
            LoadPressReleases()
        End If

    End Sub

    

    Protected Sub LoadPressReleases()

        Dim dtPressReleaseShort As DataTable = If(boolEnableGroupsAndUserAccess, PressReleaseDAL.GetPressRelease_ActiveList_FrontEnd_ByAccessAndTopN(3, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), PressReleaseDAL.GetPressRelease_ActiveList_FrontEnd_ByTopN(3, intSiteID))
        If dtPressReleaseShort.Rows.Count > 0 Then
            rptPressReleases.DataSource = dtPressReleaseShort
            rptPressReleases.DataBind()

            rptPressReleases.Visible = True
            divNoPressReleasesAvailable.Visible = False
        Else
            rptPressReleases.Visible = False
            divNoPressReleasesAvailable.Visible = True
        End If

    End Sub

End Class
