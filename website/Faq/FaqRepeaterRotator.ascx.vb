Imports System.Data

Partial Class Faq_FaqRepeaterRotator
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 6

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
            SetupFaqRotator()
        End If

    End Sub

    Protected Sub SetupFaqRotator()
        Dim dtFaqShort As DataTable = If(boolEnableGroupsAndUserAccess, FaqDAL.GetFaq_ActiveList_FrontEnd_ByAccessAndTopN(5, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), FaqDAL.GetFaq_ActiveList_FrontEnd_ByTopN(5, intSiteID))
        RadRotator1.DataSource = dtFaqShort
        RadRotator1.DataBind()
    End Sub

End Class
