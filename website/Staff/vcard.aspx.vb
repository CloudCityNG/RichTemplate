Imports System.Data
Imports System.IO

Partial Class Staff_vcard
    Inherits System.Web.UI.Page

    Dim ModuleTypeID As Integer = 12

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Request.QueryString("id") Is Nothing Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            'Load this master page if the current user is logged into this site OR this page DOES NOT require a Logged in Member
            If intMemberID > 0 Or (Not CommonWeb.PageRequiresLoggedInMember()) Then
                Dim intStaffID As Integer = Convert.ToInt32(Request.QueryString("id"))

                'Check we need to show the book this link, but only if its an active staff member
                Dim boolStatus As Boolean = True
                Dim boolAllowArchive As Boolean = False
                If Request.QueryString("archive") = 1 Then
                    boolStatus = False
                End If

                Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
                For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                    If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                        boolAllowArchive = True
                    ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                        boolEnableGroupsAndUserAccess = True
                    End If
                Next

                'If we find out the staff is an archived staff, but we do not allow achives then redirect to default page
                If boolAllowArchive = False And boolStatus = False Then
                    Response.Redirect("default.aspx")
                End If

                'Use this to check if the user has access to this staff member, if no document is returned, then the user does not have access
                Dim dtStaff As DataTable = If(boolEnableGroupsAndUserAccess, StaffDAL.GetStaff_ByStaffIDAndStatusAndAccess_FrontEnd(intStaffID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), StaffDAL.GetStaff_ByStaffIDAndStatus_FrontEnd(intStaffID, boolStatus, intSiteID))
                If dtStaff.Rows.Count > 0 Then
                    StaffDAL.DownloadVCard_ByStaffIDAndSiteID(intStaffID, intSiteID)

                Else
                    Response.Redirect("default.aspx")
                End If
            End If
        End If

    End Sub
End Class
