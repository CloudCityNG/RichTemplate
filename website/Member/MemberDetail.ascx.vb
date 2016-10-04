Imports System.Data

Partial Class Member_MemberDetail
    Inherits System.Web.UI.UserControl

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        If Visible Then
            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            If Not Page.IsPostBack Then

                If Not Request.QueryString("id") Is Nothing Then
                    Dim intMemberID_Current As Integer = Convert.ToInt32(Request.QueryString("id"))
                    LoadMember(intMemberID_Current)
                Else
                    Response.Redirect("MemberSearch.aspx")
                End If
            End If
        End If

    End Sub 'Page_Load

    Protected Sub LoadMember(ByVal intMemberID_Current As Integer)

        'We have a member id so try and load this member
        'NOTE this is for the corporate site, as such we should beable to see ALL MEMBERS no matter the siteID
        'Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberIDAndSiteID_WithThumbnail(intMemberID_Current, intSiteID)
        Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID_WithThumbnail(intMemberID_Current)
        If dtMember.Rows.Count > 0 Then

            'Now we have the member, so we load this member
            Dim drMember As DataRow = dtMember.Rows(0)

            Dim strSalutation_LangaugeSpecific As String = String.Empty
            If Not drMember("Salutation_LanguageProperty") Is DBNull.Value Then
                Dim strSalutation_LanguageProperty As String = drMember("Salutation_LanguageProperty")
                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
            End If
            Dim strSalutationFirstAndLastName As String = CommonWeb.FormatName(strSalutation_LangaugeSpecific, drMember("firstName").ToString(), drMember("lastName").ToString())

            'Set the page title Default
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Member_FrontEnd.Member_MemberDetail_HeaderTitle + " - " + strSalutationFirstAndLastName
            CommonWeb.SetMasterPageBannerText(Me.Page.Master, strSalutationFirstAndLastName)

            litSalutationFirstAndLastName.Text = strSalutationFirstAndLastName

            If Not drMember("JobTitle") Is DBNull.Value Then
                If Not drMember("JobTitle") = "" Then
                    litTitle.Text = drMember("JobTitle")
                    divMemberTitle.Visible = True
                End If
            End If

            If Not drMember("DaytimePhone") Is DBNull.Value Then
                If Not drMember("DaytimePhone") = "" Then
                    litDaytimePhone.Text = drMember("DaytimePhone")
                    divDaytimePhone.Visible = True
                End If
            End If

            If Not drMember("EveningPhone") Is DBNull.Value Then
                If Not drMember("EveningPhone") = "" Then
                    litEveningPhone.Text = drMember("EveningPhone")
                    divEveningPhone.Visible = True
                End If
            End If

            If Not drMember("Email") Is DBNull.Value Then
                If Not drMember("Email") = "" Then
                    Dim strEmailAddress As String = drMember("Email")
                    litEmail.Text = "<a class='mailto' href='mailto:" & strEmailAddress & "'>" & strEmailAddress & "</a>"
                    divEmail.Visible = True
                End If
            End If

            'We have geo location for this member so include their location
            Dim strAddress As String = ""
            If Not drMember("Location") Is DBNull.Value Then
                Dim strLocation As String = String.Empty
                If Not drMember("CategoryName") Is DBNull.Value Then
                    strLocation = drMember("CategoryName")
                End If
                litLocation.Text = strLocation & If(strLocation.Length = 0, "", " - ") & drMember("Location").ToString()
                divLocation.Visible = True

                If Not drMember("LocationAddress1") Is DBNull.Value Then
                    litLocationAddress1.Text = drMember("LocationAddress1")
                    divLocationAddress1.Visible = True
                End If
                If Not drMember("LocationAddress2") Is DBNull.Value Then
                    litLocationAddress2.Text = drMember("LocationAddress2")
                    divLocationAddress2.Visible = True
                End If
                If Not drMember("LocationAddress3") Is DBNull.Value Then
                    litLocationAddress3.Text = drMember("LocationAddress3")
                    divLocationAddress3.Visible = True
                End If

                Dim strCity As String = ""
                If Not drMember("LocationCity") Is DBNull.Value Then
                    strCity = drMember("LocationCity").ToString().Trim()
                End If


                Dim strState As String = String.Empty
                If Not drMember("LocationStateProvince") Is DBNull.Value Then
                    strState = drMember("LocationStateProvince")
                End If

                Dim strZip As String = String.Empty
                If Not drMember("LocationZip") Is DBNull.Value Then
                    strZip = drMember("LocationZip").ToString().Trim()
                End If
                Dim strCityStateZip As String = CommonWeb.FormatAddress(String.Empty, strCity, strState, strZip, String.Empty, CommonWeb.AddressFormat.MultipleLinesForHTML)
                If strCityStateZip.Length > 0 Then
                    litCityStateZip.Text = strCityStateZip
                    divCityStateZip.Visible = True
                End If
            End If

            'Finally load the members image if they have uploaded one, else use the default face placeholder
            If Not drMember("thumbnail").ToString() = "" Then
                radMemberImage.DataValue = drMember("thumbnail")

                'Add the alternate text
                If Not drMember("thumbnailName") Is DBNull.Value Then
                    radMemberImage.AlternateText = drMember("thumbnailName")
                Else
                    radMemberImage.AlternateText = Resources.Member_FrontEnd.Member_MemberDetail_Thumbnail_NoImageAvailable
                End If
            End If

        Else
            Response.Redirect("/Member/")
        End If
    End Sub
End Class
