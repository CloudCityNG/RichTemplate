
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Class admin_modules_staff_preview
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 12 ' Module Type: Staff Member

    Public Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim strStaffID As String = btnCancel.CommandArgument
        Response.Redirect("editAdd.aspx?id=" & strStaffID.ToString())
    End Sub

    Public Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("default.aspx")
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a rollback confirmation to our rollback button
        CommonWeb.SetupRollBackButton(btnRollBack, Resources.Staff_Admin.Staff_Preview_RollBack_ConfirmationMessage)

        'Set the Header UserControls Title and Help Item if it exists
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Staff_Admin.Staff_Preview_ModuleName)
        ucHeader.PageName = Resources.Staff_Admin.Staff_Preview_Header
        
        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            If Not Request.QueryString("archiveID") Is Nothing Then
                Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

                Dim dtStaffArchive As DataTable = StaffDAL.GetStaffArchive_ByArchiveIDAndSiteID(intArchiveID, SiteDAL.GetCurrentSiteID_Admin())
                If dtStaffArchive.Rows.Count > 0 Then
                    Dim drStaffArchive As DataRow = dtStaffArchive.Rows(0)

                    'Set the cancel button to go back to the orginial staff member in question
                    Dim intStaffID As Integer = Convert.ToInt32(drStaffArchive("staffID"))

                    Dim dtStaff As DataTable = StaffDAL.GetStaff_ByStaffID(intStaffID)
                    If dtStaff.Rows.Count > 0 Then
                        Dim drStaff As DataRow = dtStaff.Rows(0)

                        btnCancel.CommandArgument = intStaffID.ToString()

                        'Populate the preview pages info box
                        Dim intStaffVersion As Integer = Convert.ToInt32(drStaff("version"))
                        Dim intStaffArchiveVersion As Integer = Convert.ToInt32(drStaffArchive("version"))

                        'If the archived version is the same as the most recent version, we represent this by prepending text to it saying it is also the current version
                        litInformationBox_Version.Text = intStaffArchiveVersion.ToString()
                        If intStaffVersion = intStaffArchiveVersion Then
                            litInformationBox_Version.Text = Resources.Staff_Admin.Staff_Preview_InformationBox_Version_Current & " (" & litInformationBox_Version.Text & ")"
                        End If

                        litInformationBox_DateCreated.Text = drStaffArchive("dateTimeStamp")


                        If Not drStaffArchive("authorID_member") Is DBNull.Value Then
                            Dim intAuthorID_member As Integer = drStaffArchive("authorID_member")
                            Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intAuthorID_member)
                            If dtMember.Rows.Count > 0 Then
                                Dim drMember As DataRow = dtMember.Rows(0)

                                Dim strAuthorName As String = drMember("FirstName") & " " & drMember("LastName")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        ElseIf Not drStaffArchive("authorID_admin") Is DBNull.Value Then
                            Dim intAuthorID_admin As Integer = drStaffArchive("authorID_admin")
                            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAuthorID_admin)
                            If dtAdminUser.Rows.Count > 0 Then
                                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                                Dim strAuthorName As String = drAdminUser("First_Name") & " " & drAdminUser("Last_Name")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        End If

                        Dim strCategoryName As String = Resources.Staff_Admin.Staff_Preview_InformationBox_UnCategorized
                        If Not drStaffArchive("CategoryName") Is DBNull.Value Then
                            strCategoryName = drStaffArchive("CategoryName")
                        End If
                        litInformationBox_Category.Text = strCategoryName

                        Dim boolStatus As Boolean = Convert.ToBoolean(drStaffArchive("Status"))
                        litInformationBox_Status.Text = If(boolStatus, Resources.Staff_Admin.Staff_Preview_InformationBox_StatusActive, Resources.Staff_Admin.Staff_Preview_InformationBox_StatusArchive)


                        If Not drStaffArchive("StartDate").ToString() = "" Then
                            litInformationBox_StartDate.Text = drStaffArchive("StartDate").ToString()
                            divInformationBox_StartDate.Visible = True
                        End If

                        If Not drStaffArchive("EndDate").ToString() = "" Then
                            Dim dtEndDate As DateTime = Convert.ToDateTime(drStaffArchive("EndDate"))
                            litInformationBox_EndDate.Text = dtEndDate.ToString()
                            divInformationBox_EndDate.Visible = True

                            If dtEndDate < DateTime.Now Then
                                divExpired.Visible = True
                            End If

                        End If

                        Dim strSalutation_LangaugeSpecific As String = String.Empty
                        If Not drStaffArchive("Salutation_LanguageProperty") Is DBNull.Value Then
                            Dim strSalutation_LanguageProperty As String = drStaffArchive("Salutation_LanguageProperty")
                            strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
                        End If
                        Dim strSalutationFirstAndLastName As String = CommonWeb.FormatName(strSalutation_LangaugeSpecific, drStaffArchive("firstName").ToString(), drStaffArchive("lastName").ToString())
                        litSalutationFirstAndLastName.Text = strSalutationFirstAndLastName

                        Dim dtStartDate As DateTime = drStaffArchive("dateTimeStamp")
                        If Not drStaffArchive("startDate") Is DBNull.Value Then
                            dtStartDate = Convert.ToDateTime(drStaffArchive("startDate"))
                        End If
                        litStartDate.Text = dtStartDate.ToString("dd MMMM yyyy")

                        If Not drStaffArchive("endDate") Is DBNull.Value Then
                            Dim dtEndDate As DateTime = Convert.ToDateTime(drStaffArchive("endDate"))
                            litEndDate.Text = " " & Resources.Staff_FrontEnd.Staff_StaffDetail_StartDateEndDateSeperator & " " & dtEndDate.ToString("dd MMMM yyyy")
                            litEndDate.Visible = True
                        End If

                        If Not drStaffArchive("StaffPosition") Is DBNull.Value Then
                            If Not drStaffArchive("StaffPosition") = "" Then
                                litStaffPosition.Text = drStaffArchive("StaffPosition")
                                divStaffPosition.Visible = True
                            End If
                        End If

                        If Not drStaffArchive("Company") Is DBNull.Value Then
                            If Not drStaffArchive("Company") = "" Then
                                litCompany.Text = drStaffArchive("Company")
                                divCompany.Visible = True
                            End If
                        End If

                        If Not drStaffArchive("OfficePhone") Is DBNull.Value Then
                            If Not drStaffArchive("OfficePhone") = "" Then
                                litOfficePhone.Text = drStaffArchive("OfficePhone")
                                divOfficePhone.Visible = True
                            End If
                        End If

                        If Not drStaffArchive("MobilePhone") Is DBNull.Value Then
                            If Not drStaffArchive("MobilePhone") = "" Then
                                litMobilePhone.Text = drStaffArchive("MobilePhone")
                                divMobilePhone.Visible = True
                            End If
                        End If

                        If Not drStaffArchive("EmailAddress") Is DBNull.Value Then
                            If Not drStaffArchive("EmailAddress") = "" Then
                                Dim strEmailAddress As String = drStaffArchive("EmailAddress")
                                litEmail.Text = "<a class='mailto' href='mailto:" & strEmailAddress & "'>" & strEmailAddress & "</a>"
                                divEmail.Visible = True
                            End If
                        End If

                        If Not drStaffArchive("personalURL") Is DBNull.Value Then
                            If Not drStaffArchive("personalURL") = "" Then
                                Dim strPersonalURL As String = drStaffArchive("personalURL")
                                litPersonalURL.Text = "<a class='link' href='" & strPersonalURL & "'>" & strPersonalURL & "</a>"
                                divPersonalURL.Visible = True
                            End If
                        End If

                        If Not drStaffArchive("favouriteURL") Is DBNull.Value Then
                            If Not drStaffArchive("favouriteURL") = "" Then
                                Dim strFavouriteURL As String = drStaffArchive("favouriteURL")
                                litFavouriteURL.Text = "<a class='link' href='" & strFavouriteURL & "'>" & strFavouriteURL & "</a>"
                                divFavouriteURL.Visible = True
                            End If
                        End If

                        If Not drStaffArchive("Body") Is DBNull.Value Then
                            If Not drStaffArchive("Body") = "" Then
                                litBio.Text = drStaffArchive("Body")
                                divBio.Visible = True
                            End If
                        End If


                        If Not drStaffArchive("AddressOfficeNumber") Is DBNull.Value Then
                            If Not drStaffArchive("AddressOfficeNumber") = "" Then
                                litOffice.Text = drStaffArchive("AddressOfficeNumber")
                                divOffice.Visible = True
                            End If
                        End If

                        'We have geo location for this staff member so include their location
                        Dim strAddressStreetAndCity As String = ""
                        If Not drStaffArchive("Address") Is DBNull.Value Then
                            If Not drStaffArchive("Address") = "" Then
                                strAddressStreetAndCity = drStaffArchive("Address").ToString()
                            End If
                        End If

                        If Not drStaffArchive("City") Is DBNull.Value Then
                            If Not drStaffArchive("City") = "" Then
                                strAddressStreetAndCity = strAddressStreetAndCity & If(strAddressStreetAndCity.Length > 0, ", " & drStaffArchive("City"), drStaffArchive("City"))
                            End If
                        End If

                        If strAddressStreetAndCity.Length > 0 Then
                            litAddressStreetAndCity.Text = strAddressStreetAndCity & "<br/>"
                            divAddress.Visible = True
                        End If


                        Dim strAddressStateAndZip As String = ""
                        If Not drStaffArchive("StateName") Is DBNull.Value Then
                            If Not drStaffArchive("StateName") = "" Then
                                strAddressStateAndZip = drStaffArchive("StateName").ToString()
                            End If
                        End If

                        If Not drStaffArchive("ZipCode") Is DBNull.Value Then
                            If Not drStaffArchive("ZipCode") = "" Then
                                strAddressStateAndZip = strAddressStateAndZip & If(strAddressStateAndZip.Length > 0, ", " & drStaffArchive("ZipCode"), drStaffArchive("ZipCode"))
                            End If
                        End If

                        If strAddressStateAndZip.Length > 0 Then
                            litAddressStateAndZip.Text = strAddressStateAndZip & "<br/>"
                            divAddress.Visible = "true"
                        End If

                        If Not drStaffArchive("geolocation") Is DBNull.Value Then
                            Dim boolGeoLocation As Boolean = Convert.ToBoolean(drStaffArchive("geolocation"))
                            If boolGeoLocation Then
                                'Load in the google map
                                If (Not drStaffArchive("locationLatitude") Is DBNull.Value) And (Not drStaffArchive("locationLongitude") Is DBNull.Value) Then
                                    Dim strLocationLatitude As String = drStaffArchive("locationLatitude")
                                    Dim strLocationLongitude As String = drStaffArchive("locationLongitude")
                                    If strLocationLatitude.Length > 0 And strLocationLongitude.Length > 0 Then
                                        ucGoogleMap.Latitude = strLocationLatitude
                                        ucGoogleMap.Longitude = strLocationLongitude
                                        ucGoogleMap.ZoomLevel = 15
                                    End If
                                End If
                            End If
                        End If

                        'Finally load the staff members image if they have uploaded one, else use the default face placeholder
                        If Not drStaffArchive("thumbnail").ToString() = "" Then
                            radStaffImage.DataValue = drStaffArchive("thumbnail")

                            'Add the alternate text
                            If Not drStaffArchive("thumbnailName") Is DBNull.Value Then
                                radStaffImage.AlternateText = drStaffArchive("thumbnailName")
                            Else
                                radStaffImage.AlternateText = Resources.Staff_FrontEnd.Staff_StaffDetail_Thumbnail_NoImageAvailable
                            End If
                        End If

                    Else
                        Response.Redirect("default.aspx")
                    End If
                Else
                    Response.Redirect("default.aspx")
                End If
            Else
                Response.Redirect("default.aspx")
            End If

        End If

    End Sub

    Public Sub btnRollBack_OnClick(ByVal sender As Object, ByVal e As EventArgs)

        If Not Request.QueryString("archiveID") Is Nothing Then

            Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

            StaffDAL.RollbackStaff(intArchiveID)
            pnlConfirmation.Visible = True
            pnlPreview.Visible = False
        End If
    End Sub

End Class
