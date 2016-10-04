Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class Event_EventRepeaterShort
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 5

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
            LoadEvents()
        End If
    End Sub

   

    Protected Sub LoadEvents()
        Dim dtEventShort As DataTable = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ActiveList_FrontEnd_ByAccessAndTopN(3, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ActiveList_FrontEnd_ByTopN(3, intSiteID))
        If dtEventShort.Rows.Count > 0 Then
            rptEvents.DataSource = dtEventShort
            rptEvents.DataBind()

            rptEvents.Visible = True
            divNoEventsAvailable.Visible = False
        Else
            rptEvents.Visible = False
            divNoEventsAvailable.Visible = True
        End If

    End Sub

End Class
