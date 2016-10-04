
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_event_preview
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 5 ' Module Type: Event

    Public Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim strEventID As String = btnCancel.CommandArgument
        Response.Redirect("editAdd.aspx?id=" & strEventID.ToString())
    End Sub

    Public Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("default.aspx")
    End Sub

    Private Sub BindSalutationDropDown()

        'Populate Salutation Titles
        ddlSalutation.Items.Clear()

        Dim dtSalutation As DataTable = SalutationDAL.GetSalutationList()
        For Each drSalutation As DataRow In dtSalutation.Rows
            Dim intSalutationID As Integer = drSalutation("ID")
            Dim strSalutation_LanguageProperty As String = drSalutation("Salutation_LanguageProperty")

            Dim strSalutation_LangaugeSpecific As String = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)

            Dim liSalutation As New ListItem(strSalutation_LangaugeSpecific, intSalutationID)
            ddlSalutation.Items.Add(liSalutation)
        Next

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a rollback confirmation to our rollback button
        CommonWeb.SetupRollBackButton(btnRollBack, Resources.Event_Admin.Event_Preview_RollBack_ConfirmationMessage)

        'Set the Header UserControls Title and Help Item if it exists
        CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Event_Admin.Event_Preview_ModuleName)
        ucHeader.PageName = Resources.Event_Admin.Event_AddEdit_Header

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            BindSalutationDropDown()
            If Not Request.QueryString("archiveID") Is Nothing Then

                Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

                Dim dtEventArchive As DataTable = EventDAL.GetEventArchive_ByArchiveIDAndSiteID(intArchiveID, SiteDAL.GetCurrentSiteID_Admin())
                If dtEventArchive.Rows.Count > 0 Then
                    Dim drEventArchive As DataRow = dtEventArchive.Rows(0)

                    'Set the cancel button to go back to the orginial event record in question
                    Dim intEventID As Integer = Convert.ToInt32(drEventArchive("eventID"))

                    'Now we must also check the record that the archive corresponds to actually exists, also as we don't store the images with this record in the archive table, we may also need to load the current images to show with this preview.
                    Dim dtEvent As DataTable = EventDAL.GetEvent_ByEventID(intEventID)
                    If dtEvent.Rows.Count > 0 Then
                        Dim drEvent As DataRow = dtEvent.Rows(0)


                        btnCancel.CommandArgument = intEventID.ToString()

                        'Populate the preview pages info box
                        Dim intEventVersion As Integer = Convert.ToInt32(drEvent("version"))
                        Dim intEventArchiveVersion As Integer = Convert.ToInt32(drEventArchive("version"))

                        'If the archived version is the same as the most recent version, we represent this by prepending text to it saying it is also the current version
                        litInformationBox_Version.Text = intEventArchiveVersion.ToString()
                        If intEventVersion = intEventArchiveVersion Then
                            litInformationBox_Version.Text = Resources.Event_Admin.Event_Preview_InformationBox_Version_Current & " (" & litInformationBox_Version.Text & ")"
                        End If

                        litInformationBox_DateCreated.Text = drEventArchive("dateTimeStamp")

                        litInformationBox_AuthorName.Text = Resources.Event_Admin.Event_Preview_InformationBox_AuthorDefault
                        If Not drEventArchive("authorID_member") Is DBNull.Value Then
                            Dim intAuthorID_member As Integer = drEventArchive("authorID_member")
                            Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intAuthorID_member)
                            If dtMember.Rows.Count > 0 Then
                                Dim drMember As DataRow = dtMember.Rows(0)

                                Dim strAuthorName As String = drMember("FirstName") & " " & drMember("LastName")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        ElseIf Not drEventArchive("authorID_admin") Is DBNull.Value Then
                            Dim intAuthorID_admin As Integer = drEventArchive("authorID_admin")
                            Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(intAuthorID_admin)
                            If dtAdminUser.Rows.Count > 0 Then
                                Dim drAdminUser As DataRow = dtAdminUser.Rows(0)

                                Dim strAuthorName As String = drAdminUser("First_Name") & " " & drAdminUser("Last_Name")
                                litInformationBox_AuthorName.Text = strAuthorName
                            End If
                        End If

                        Dim strCategoryName As String = Resources.Event_Admin.Event_Preview_InformationBox_UnCategorized
                        If Not drEventArchive("CategoryName") Is DBNull.Value Then
                            strCategoryName = drEventArchive("CategoryName")
                        End If
                        litInformationBox_Category.Text = strCategoryName

                        Dim boolStatus As Boolean = Convert.ToBoolean(drEventArchive("Status"))
                        litInformationBox_Status.Text = If(boolStatus, Resources.Event_Admin.Event_Preview_InformationBox_StatusActive, Resources.Event_Admin.Event_Preview_InformationBox_StatusArchive)

                        If Not drEventArchive("publicationDate").ToString() = "" Then
                            litInformationBox_PublicationDate.Text = drEventArchive("publicationDate").ToString()
                            divInformationBox_PublicationDate.Visible = True
                        End If

                        If Not drEventArchive("expirationDate").ToString() = "" Then
                            Dim dtExpirationDate As DateTime = Convert.ToDateTime(drEventArchive("expirationDate"))
                            litInformationBox_ExpirationDate.Text = dtExpirationDate.ToString()
                            divInformationBox_ExpirationDate.Visible = True

                            If dtExpirationDate < DateTime.Now Then
                                divExpired.Visible = True
                            End If

                        End If
                        If Not drEventArchive("Summary") Is DBNull.Value Then
                            litInformationBox_Summary.Text = drEventArchive("Summary")
                            divInformationBox_Summary.Visible = True
                        End If


                        'Populate the preview page such that it is similar to the front-end version

                        ' Check our modules Configuration settings
                        Dim boolAllowOnlineRegistration As Boolean = False
                        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())
                        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                            If drModuleConfig("fieldName").ToString().ToLower() = "allow_online_registrations" Then
                                boolAllowOnlineRegistration = True
                            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "use_captcha" Then
                                trRadCaptcha.Visible = True
                            End If

                        Next

                        'Check if the event is supposed to direct to an external url
                        Dim strExternalLinkUrl As String = ""
                        If Not drEventArchive("externalLinkUrl") Is DBNull.Value AndAlso drEventArchive("externalLinkUrl").ToString().Trim.Length > 0 Then
                            litExternalURL.Text = drEventArchive("externalLinkUrl").ToString()
                            divExternalEvent.Visible = True

                        End If

                        'Show StartTime if entered
                        If Not IsDBNull(drEventArchive("startDate")) Then
                            litEventDateTime.Text = Convert.ToDateTime(drEventArchive("startDate")).ToString("D")
                            If Not IsDBNull(drEventArchive("startTime")) Then
                                litEventDateTime.Text += " " & Convert.ToDateTime(drEventArchive("startTime")).ToString("h:mm tt")
                            End If

                            If Not IsDBNull(drEventArchive("endDate")) Then
                                If Convert.ToDateTime(drEventArchive("startDate")) = Convert.ToDateTime(drEventArchive("endDate")) Then
                                    If Not IsDBNull(drEventArchive("endTime")) Then
                                        litEventDateTime.Text += " - " & Convert.ToDateTime(drEventArchive("endTime")).ToString("h:mm tt")
                                    End If
                                Else
                                    litEventDateTime.Text += " - " & Convert.ToDateTime(drEventArchive("endDate")).ToString("D")
                                    If Not IsDBNull(drEventArchive("endTime")) Then
                                        litEventDateTime.Text += " " + Convert.ToDateTime(drEventArchive("endTime")).ToString("h:mm tt")
                                    End If
                                End If
                            Else
                                If Not IsDBNull(drEventArchive("endTime")) Then
                                    litEventDateTime.Text += " - " + Convert.ToDateTime(drEventArchive("endTime")).ToString("h:mm tt")
                                End If
                            End If

                            litEventDateTime.Visible = True

                        End If

                        'set the title and body
                        Dim strTitle As String = drEventArchive("Title")
                        litTitle.Text = strTitle

                        Dim strBody As String = drEventArchive("Body")
                        litBody.Text = strBody

                        'Check we need to show the contact person
                        If Not drEvent("contactPerson") Is DBNull.Value AndAlso drEvent("contactPerson").ToString().Trim().Length > 0 Then
                            Dim strContactPerson As String = drEvent("contactPerson").ToString()
                            litContactPerson.Text = "<a href='mailto:" & strContactPerson & "'>" & strContactPerson & "</a>"
                            divContactPerson.Visible = True
                        End If

                        'Show location if entered
                        If Not IsDBNull(drEventArchive("Address")) Then
                            litLocation.Text = drEventArchive("Address").ToString() & "<br/>"
                            If Not IsDBNull(drEventArchive("City")) Then
                                litLocation.Text += drEventArchive("City").ToString() & ", "
                            End If
                            If Not IsDBNull(drEventArchive("stateName")) Then
                                litLocation.Text += drEventArchive("stateName").ToString() & " "
                            End If
                            If Not IsDBNull(drEventArchive("ZipCode")) Then
                                litLocation.Text += drEventArchive("ZipCode").ToString()
                            End If
                            If Not IsDBNull(drEventArchive("CountryName")) Then
                                litLocation.Text += "<br/>" & drEventArchive("CountryName").ToString()
                            End If
                            litLocation.Visible = True
                        End If


                        If Not IsDBNull(drEventArchive("geoLocation")) Then
                            If drEventArchive("geolocation") = True Then
                                Dim latitude As String = ""
                                Dim longitude As String = ""
                                If Not IsDBNull(drEventArchive("locationLatitude")) Then
                                    latitude = drEventArchive("locationLatitude")
                                End If
                                If Not IsDBNull(drEventArchive("locationLongitude")) Then
                                    longitude = drEventArchive("locationLongitude")
                                End If
                                If latitude.Length > 0 And longitude.Length > 0 Then
                                    ucGoogleMap.Latitude = latitude
                                    ucGoogleMap.Longitude = longitude
                                    ucGoogleMap.ZoomLevel = 15
                                End If

                            End If
                        End If

                        If (boolAllowOnlineRegistration) AndAlso (Not IsDBNull(drEventArchive("onlineSignup"))) Then
                            If drEventArchive("onlineSignup") = True Then
                                signUpPanel.Visible = True
                            End If
                        End If

                        'set the author name and posted by date
                        Dim strAuthorUsername As String = Resources.Event_Admin.Event_Preview_PostedByDefault
                        If Not drEventArchive("author_username") Is DBNull.Value Then
                            strAuthorUsername = drEventArchive("author_username").ToString()
                        End If
                        litPostedBy.Text = strAuthorUsername

                        Dim dtViewDate As DateTime = Convert.ToDateTime(drEventArchive("viewDate"))
                        litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
                        litViewDateTime.Text = dtViewDate.ToString("h:mm tt")



                        'Load in this event records search tags
                        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin(), intEventID)
                        If dtSearchTags.Rows.Count > 0 Then
                            divModuleSearchTagList.Visible = True
                            rptSearchTags.DataSource = dtSearchTags
                            rptSearchTags.DataBind()
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

    Protected Sub btn_Signup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Signup.Click
        If Page.IsValid Then
            addSignupInformation()
        End If
    End Sub

    Private Sub addSignupInformation()

        'set the return to Employment Opportunity Link
        Dim intArchiveID As Integer = Request.QueryString("archiveID").ToString()
        aReturnToEventDetail.HRef = "?archiveID=" & intArchiveID

        'hide/show appropriate divs/panels
        divEventItem.Visible = False
        signUpPanel.Visible = False
        confirmationPanel.Visible = True
    End Sub

    Public Sub btnRollBack_OnClick(ByVal sender As Object, ByVal e As EventArgs)

        If Not Request.QueryString("archiveID") Is Nothing Then

            Dim intArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

            EventDAL.RollbackEvent(intArchiveID)
            pnlConfirmation.Visible = True
            pnlPreview.Visible = False
        End If
    End Sub

End Class
