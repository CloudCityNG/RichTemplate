Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports Telerik.Web.UI
Imports Subgurim.Controles

Partial Class staff_StaffDetail
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 12

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            If Not Page.IsPostBack Then


                If Not Request.QueryString("id") Is Nothing Then
                    Dim intStaffID As Integer = Convert.ToInt32(Request.QueryString("id"))
                    LoadStaff(intStaffID)
                End If

            End If
        End If

    End Sub

    Protected Sub LoadStaff(ByVal intStaffID As Integer)

        'Check we need to show the book this link, but only if its an active staff member
        Dim boolStatus As Boolean = True
        Dim boolAllowArchive As Boolean = False
        Dim boolEnableGroupsAndUserAccess As Boolean = False

        If Request.QueryString("archive") = 1 Then
            boolStatus = False
        End If

        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                boolAllowArchive = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "show_add_this" Then
                If boolStatus Then
                    addThisPlaceholder.Visible = True
                End If
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True
            End If
        Next

        'If we find out the staff is an archived staff, but we do not allow achives then redirect to default page
        If boolAllowArchive = False And boolStatus = False Then
            Response.Redirect("default.aspx")
        End If

        'We have a staff id so try and load this staff member
        Dim dtStaff As DataTable = If(boolEnableGroupsAndUserAccess, StaffDAL.GetStaff_ByStaffIDAndStatusAndAccess_FrontEnd(intStaffID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), StaffDAL.GetStaff_ByStaffIDAndStatus_FrontEnd(intStaffID, boolStatus, intSiteID))
        If dtStaff.Rows.Count > 0 Then

            'Now we have the staff, so we load this staff
            Dim drStaff As DataRow = dtStaff.Rows(0)

            Dim strSalutation_LangaugeSpecific As String = String.Empty
            If Not drStaff("Salutation_LanguageProperty") Is DBNull.Value Then
                Dim strSalutation_LanguageProperty As String = drStaff("Salutation_LanguageProperty")
                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
            End If

            Dim strSalutationFirstAndLastName As String = CommonWeb.FormatName(strSalutation_LangaugeSpecific, drStaff("firstName").ToString(), drStaff("lastName").ToString())

            'Set the page title Default
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Staff_FrontEnd.Staff_StaffDetail_HeaderTitle + " - " + strSalutationFirstAndLastName

            litSalutationFirstAndLastName.Text = strSalutationFirstAndLastName

            Dim dtStartDate As DateTime = drStaff("dateTimeStamp")
            If Not drStaff("startDate") Is DBNull.Value Then
                dtStartDate = Convert.ToDateTime(drStaff("startDate"))
            End If
            litStartDate.Text = dtStartDate.ToString("dd MMMM yyyy")

            If Not drStaff("endDate") Is DBNull.Value Then
                Dim dtEndDate As DateTime = Convert.ToDateTime(drStaff("endDate"))
                litEndDate.Text = " " & Resources.Staff_FrontEnd.Staff_StaffDetail_StartDateEndDateSeperator & " " & dtEndDate.ToString("dd MMMM yyyy")
                litEndDate.Visible = True
            End If

            If Not drStaff("StaffPosition") Is DBNull.Value Then
                If Not drStaff("StaffPosition") = "" Then
                    litStaffPosition.Text = drStaff("StaffPosition")
                    divStaffPosition.Visible = True
                End If
            End If

            If Not drStaff("Company") Is DBNull.Value Then
                If Not drStaff("Company") = "" Then
                    litCompany.Text = drStaff("Company")
                    divCompany.Visible = True
                End If
            End If

            If Not drStaff("OfficePhone") Is DBNull.Value Then
                If Not drStaff("OfficePhone") = "" Then
                    litOfficePhone.Text = drStaff("OfficePhone")
                    divOfficePhone.Visible = True
                End If
            End If

            If Not drStaff("MobilePhone") Is DBNull.Value Then
                If Not drStaff("MobilePhone") = "" Then
                    litMobilePhone.Text = drStaff("MobilePhone")
                    divMobilePhone.Visible = True
                End If
            End If

            If Not drStaff("EmailAddress") Is DBNull.Value Then
                If Not drStaff("EmailAddress") = "" Then
                    Dim strEmailAddress As String = drStaff("EmailAddress")
                    litEmail.Text = "<a class='mailto' href='mailto:" & strEmailAddress & "'>" & strEmailAddress & "</a>"
                    divEmail.Visible = True
                End If
            End If

            If Not drStaff("personalURL") Is DBNull.Value Then
                If Not drStaff("personalURL") = "" Then
                    Dim strPersonalURL As String = drStaff("personalURL")
                    litPersonalURL.Text = "<a class='link' href='" & strPersonalURL & "'>" & strPersonalURL & "</a>"
                    divPersonalURL.Visible = True
                End If
            End If

            If Not drStaff("favouriteURL") Is DBNull.Value Then
                If Not drStaff("favouriteURL") = "" Then
                    Dim strFavouriteURL As String = drStaff("favouriteURL")
                    litFavouriteURL.Text = "<a class='link' href='" & strFavouriteURL & "'>" & strFavouriteURL & "</a>"
                    divFavouriteURL.Visible = True
                End If
            End If

            If Not drStaff("Body") Is DBNull.Value Then
                If Not drStaff("Body") = "" Then
                    litBio.Text = drStaff("Body")
                    divBio.Visible = True
                End If
            End If


            If Not drStaff("AddressOfficeNumber") Is DBNull.Value Then
                If Not drStaff("AddressOfficeNumber") = "" Then
                    litOffice.Text = drStaff("AddressOfficeNumber")
                    divOffice.Visible = True
                End If
            End If

            'We have geo location for this staff member so include their location
            Dim strAddressStreetAndCity As String = ""
            If Not drStaff("Address") Is DBNull.Value Then
                If Not drStaff("Address") = "" Then
                    strAddressStreetAndCity = drStaff("Address").ToString()
                End If
            End If

            If Not drStaff("City") Is DBNull.Value Then
                If Not drStaff("City") = "" Then
                    strAddressStreetAndCity = strAddressStreetAndCity & If(strAddressStreetAndCity.Length > 0, ", " & drStaff("City"), drStaff("City"))
                End If
            End If

            If strAddressStreetAndCity.Length > 0 Then
                litAddressStreetAndCity.Text = strAddressStreetAndCity & "<br/>"
                divAddress.Visible = True
            End If


            Dim strAddressStateAndZip As String = ""
            If Not drStaff("StateName") Is DBNull.Value Then
                If Not drStaff("StateName") = "" Then
                    strAddressStateAndZip = drStaff("StateName").ToString()
                End If
            End If

            If Not drStaff("ZipCode") Is DBNull.Value Then
                If Not drStaff("ZipCode") = "" Then
                    strAddressStateAndZip = strAddressStateAndZip & If(strAddressStateAndZip.Length > 0, ", " & drStaff("ZipCode"), drStaff("ZipCode"))
                End If
            End If

            If strAddressStateAndZip.Length > 0 Then
                litAddressStateAndZip.Text = strAddressStateAndZip & "<br/>"
                divAddress.Visible = "true"
            End If

            If Not drStaff("geolocation") Is DBNull.Value Then
                Dim boolGeoLocation As Boolean = Convert.ToBoolean(drStaff("geolocation"))
                If boolGeoLocation Then
                    'Load in the google map
                    If (Not drStaff("locationLatitude") Is DBNull.Value) And (Not drStaff("locationLongitude") Is DBNull.Value) Then
                        Dim strLocationLatitude As String = drStaff("locationLatitude")
                        Dim strLocationLongitude As String = drStaff("locationLongitude")
                        If strLocationLatitude.Length > 0 And strLocationLongitude.Length > 0 Then
                            ucGoogleMap.Latitude = strLocationLatitude
                            ucGoogleMap.Longitude = strLocationLongitude
                            ucGoogleMap.ZoomLevel = 15
                        End If
                    End If
                End If
            End If

            'Finally load the staff members image if they have uploaded one, else use the default face placeholder
            If Not drStaff("thumbnail").ToString() = "" Then
                radStaffImage.DataValue = drStaff("thumbnail")

                'Add the alternate text
                If Not drStaff("thumbnailName") Is DBNull.Value Then
                    radStaffImage.AlternateText = drStaff("thumbnailName")
                Else
                    radStaffImage.AlternateText = Resources.Staff_FrontEnd.Staff_StaffDetail_Thumbnail_NoImageAvailable
                End If
            End If

            'set the page title
            If Not drStaff("metaTitle") Is DBNull.Value Then
                Dim strMetaTitle As String = drStaff("metaTitle").ToString()
                If strMetaTitle.Length > 0 Then
                    Page.Header.Title = strMetaTitle
                End If
            End If

            'set the page's keywords

            If Not drStaff("metaKeywords") Is DBNull.Value Then
                Dim keywordsHtmlMeta As New HtmlMeta()
                keywordsHtmlMeta.Name = "keywords"
                keywordsHtmlMeta.Content = drStaff("metaKeywords").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(keywordsHtmlMeta)
            End If

            'set the page's description
            If Not drStaff("metaDescription") Is DBNull.Value Then
                Dim descriptionHtmlMeta As New HtmlMeta()
                descriptionHtmlMeta.Name = "description"
                descriptionHtmlMeta.Content = drStaff("metaDescription").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(descriptionHtmlMeta)
            End If
        Else
            Response.Redirect("default.aspx")
        End If
    End Sub

End Class
