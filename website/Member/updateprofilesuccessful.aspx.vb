Imports System.Data
Imports Telerik.Web.UI
Imports System.Xml

Partial Class Member_UpdateProfileSuccessful
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()

        If Not Page.IsPostBack Then

            Page.Header.Title = SiteDAL.GetCompanyName() & " | " & Resources.Member_FrontEnd.Member_UpdateProfileSuccessful_HeaderTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Member_FrontEnd.Member_UpdateProfileSuccessful_ProfileUpdated_Heading)

            Dim MemberID As Integer = MemberDAL.GetCurrentMemberID()
            If MemberID > 0 Then

                Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(MemberID)
                If dtMember.Rows.Count > 0 Then
                    'Then the member exists, otherwise redirect user to the default page
                Else
                    Response.Redirect("default.aspx")
                End If
            Else
                Response.Redirect("default.aspx")
            End If

        End If

    End Sub

End Class
