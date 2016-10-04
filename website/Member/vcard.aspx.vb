Imports System.Data
Imports System.IO

Partial Class Member_vcard
    Inherits System.Web.UI.Page

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Request.QueryString("id") Is Nothing Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            Dim intMemberID_Current As Integer = Convert.ToInt32(Request.QueryString("id"))

            'Use this to check if the user has access to this staff member, if no document is returned, then the user does not have access
            Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberIDAndSiteID(intMemberID_Current, intSiteID)
            If dtMember.Rows.Count > 0 Then
                MemberDAL.DownloadVCard_ByMemberIDAndSiteID(intMemberID_Current, intSiteID)

            Else
                Response.Redirect("default.aspx")
            End If
        End If

    End Sub
End Class
