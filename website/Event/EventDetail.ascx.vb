Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports Telerik.Web.UI
Imports Subgurim.Controles
Imports System.Xml

Partial Class Event_EventDetail
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 5

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Visible Then

            'First we set the SiteID and current MemberID
            intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
            intMemberID = MemberDAL.GetCurrentMemberID()

            If Not IsPostBack Then
                BindSalutationDropDown()
            End If

            If Not Request.QueryString("id") Is Nothing Then
                Dim intEventID As Integer = Convert.ToInt32(Request.QueryString("id"))
                LoadEvent(intEventID)
            End If
        End If

    End Sub


    Protected Sub LoadEvent(ByVal intEventID As Integer)
        Dim boolStatus As Boolean = True
        Dim boolAllowArchive As Boolean = False
        Dim boolAllowComments As Boolean = False
        Dim boolEnablePublicCommentSubmission As Boolean = False
        Dim boolAllowOnlineSubmissions As Boolean = False
        Dim boolAllowOnlineRegistration As Boolean = False
        Dim boolEnableGroupsAndUserAccess As Boolean = False

        If Request.QueryString("archive") = 1 Then
            boolStatus = False
        End If

        ' Check our modules Configuration settings
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                boolAllowArchive = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "show_add_this" Then
                plcAddThis.Visible = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" Then
                boolAllowComments = True
            ElseIf drModuleConfig("fieldName").ToString.ToLower() = "enable_public_comment_submission" Then
                boolEnablePublicCommentSubmission = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_submissions" Then
                boolAllowOnlineSubmissions = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_registrations" Then
                boolAllowOnlineRegistration = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "use_captcha" Then
                trRadCaptcha.Visible = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True
            End If
        Next

        'If we find out the event is an archived event, but we do not allow achives then redirect to default page
        If boolAllowArchive = False And boolStatus = False Then
            Response.Redirect("default.aspx")
        End If

        'Load the Event by ID
        Dim dtEvent As DataTable = If(boolEnableGroupsAndUserAccess, EventDAL.GetEvent_ByEventIDAndStatusAndAccess_FrontEnd(intEventID, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), EventDAL.GetEvent_ByEventIDAndStatus_FrontEnd(intEventID, boolStatus, intSiteID))
        If dtEvent.Rows.Count > 0 Then
            Dim drEvent As DataRow = dtEvent.Rows(0)

            'Check if the event is supposed to direct to an external url
            Dim strExternalLinkUrl As String = ""
            If Not drEvent("externalLinkUrl") Is DBNull.Value AndAlso drEvent("externalLinkUrl").ToString().Trim.Length > 0 Then
                Response.Redirect(drEvent("externalLinkUrl").ToString())
            End If

            'Show StartTime if entered
            If Not IsDBNull(drEvent("startDate")) Then
                litEventDateTime.Text = Convert.ToDateTime(drEvent("startDate")).ToString("D")
                If Not IsDBNull(drEvent("startTime")) Then
                    litEventDateTime.Text += " " & Convert.ToDateTime(drEvent("startTime")).ToString("h:mm tt")
                End If

                If Not IsDBNull(drEvent("endDate")) Then
                    If Convert.ToDateTime(drEvent("startDate")) = Convert.ToDateTime(drEvent("endDate")) Then
                        If Not IsDBNull(drEvent("endTime")) Then
                            litEventDateTime.Text += " - " & Convert.ToDateTime(drEvent("endTime")).ToString("h:mm tt")
                        End If
                    Else
                        litEventDateTime.Text += " - " & Convert.ToDateTime(drEvent("endDate")).ToString("D")
                        If Not IsDBNull(drEvent("endTime")) Then
                            litEventDateTime.Text += " " + Convert.ToDateTime(drEvent("endTime")).ToString("h:mm tt")
                        End If
                    End If
                Else
                    If Not IsDBNull(drEvent("endTime")) Then
                        litEventDateTime.Text += " - " + Convert.ToDateTime(drEvent("endTime")).ToString("h:mm tt")
                    End If
                End If

                litEventDateTime.Visible = True

            End If

            'set the title and body
            Dim strTitle As String = drEvent("Title")

            'Set the page title Default
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Event_FrontEnd.Event_EventDetail_HeaderTitle + " - " + strTitle

            litTitle.Text = strTitle

            Dim strBody As String = drEvent("Body")
            litBody.Text = strBody

            'Check we need to show the contact person
            If Not drEvent("contactPerson") Is DBNull.Value AndAlso drEvent("contactPerson").ToString().Trim().Length > 0 Then
                Dim strContactPerson As String = drEvent("contactPerson").ToString()
                litContactPerson.Text = "<a href='mailto:" & strContactPerson & "'>" & strContactPerson & "</a>"
                divContactPerson.Visible = True
            End If

            'Show location if entered
            Dim strAddress As String = ""
            If Not drEvent("Address") Is DBNull.Value Then
                strAddress = drEvent("Address").ToString().Trim()
            End If
            Dim strCity As String = ""
            If Not drEvent("City") Is DBNull.Value Then
                strCity = drEvent("City").ToString().Trim()
            End If
            Dim intStateID As Integer = Integer.MinValue
            If Not drEvent("StateID") Is DBNull.Value Then
                intStateID = Convert.ToInt32(drEvent("StateID"))
            End If
            Dim strZipCode As String = ""
            If Not drEvent("ZipCode") Is DBNull.Value Then
                strZipCode = drEvent("ZipCode").ToString().Trim()
            End If
            Dim intCountryID As Integer = Integer.MinValue
            If Not drEvent("CountryID") Is DBNull.Value Then
                intCountryID = Convert.ToInt32(drEvent("CountryID"))
            End If
            Dim strHtmlLocationInformation As String = CommonWeb.FormatAddress(strAddress, strCity, intStateID, strZipCode, intCountryID, CommonWeb.AddressFormat.MultipleLinesForHTML)
            If strHtmlLocationInformation.Length > 0 Then

                litLocation.Text = strHtmlLocationInformation
                divLocation.Visible = True
            End If

            If Not IsDBNull(drEvent("geoLocation")) Then
                If drEvent("geolocation") = True Then
                    Dim latitude As String = ""
                    Dim longitude As String = ""
                    If Not IsDBNull(drEvent("locationLatitude")) Then
                        latitude = drEvent("locationLatitude")
                    End If
                    If Not IsDBNull(drEvent("locationLongitude")) Then
                        longitude = drEvent("locationLongitude")
                    End If
                    If latitude.Length > 0 And longitude.Length > 0 Then
                        ucGoogleMap.Latitude = latitude
                        ucGoogleMap.Longitude = longitude
                        ucGoogleMap.ZoomLevel = 15

                        'So we have latitude and longitude, so show the google map div
                        divGoogleMap.Visible = True
                    End If

                End If
            End If

            If (boolStatus) AndAlso (boolAllowOnlineRegistration) AndAlso (Not IsDBNull(drEvent("onlineSignup"))) Then
                If drEvent("onlineSignup") = True Then
                    signUpPanel.Visible = True
                End If
            End If


            'set the author name and posted by date
            Dim strAuthorUsername As String = Resources.Event_FrontEnd.Event_EventDetail_PostedByDefault
            If Not drEvent("author_username") Is DBNull.Value Then
                strAuthorUsername = drEvent("author_username").ToString()
            End If
            litPostedBy.Text = strAuthorUsername

            Dim dtViewDate As DateTime = Convert.ToDateTime(drEvent("viewDate"))
            litViewDate.Text = dtViewDate.ToString("dddd, dd MMMM, yyyy")
            litViewDateTime.Text = dtViewDate.ToString("h:mm tt")

            'Finally show the edit link if the event was uploaded by the currently logged in user
            If boolAllowOnlineSubmissions Then
                If Not drEvent("authorID_member") Is DBNull.Value Then
                    Dim intAuthorID_Member As Integer = Convert.ToInt32(drEvent("authorID_member"))
                    If intMemberID > 0 And intMemberID = intAuthorID_Member Then
                        divEditEvent.Visible = True
                    End If

                End If
            End If

            'set the page title
            If Not drEvent("metaTitle") Is DBNull.Value Then
                Dim strMetaTitle As String = drEvent("metaTitle").ToString()
                If strMetaTitle.Length > 0 Then
                    Page.Header.Title = strMetaTitle
                End If
            End If

            'set the page's keywords

            If Not drEvent("metaKeywords") Is DBNull.Value Then
                Dim keywordsHtmlMeta As New HtmlMeta()
                keywordsHtmlMeta.Name = "keywords"
                keywordsHtmlMeta.Content = drEvent("metaKeywords").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(keywordsHtmlMeta)
            End If

            'set the page's description
            If Not drEvent("metaDescription") Is DBNull.Value Then
                Dim descriptionHtmlMeta As New HtmlMeta()
                descriptionHtmlMeta.Name = "description"
                descriptionHtmlMeta.Content = drEvent("metaDescription").ToString()
                ' Add the HtmlMeta object to the page header's controls. 
                Page.Header.Controls.Add(descriptionHtmlMeta)
            End If

            'Load in this events search tags
            Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, intSiteID, intEventID)
            If dtSearchTags.Rows.Count > 0 Then
                divModuleSearchTagList.Visible = True
                rptSearchTags.DataSource = dtSearchTags
                rptSearchTags.DataBind()
            End If

            'Load Comments
            If boolAllowComments Then
                'setup the comments list, if we have enabled public comment submissions, then the member does not require to login to post a comment
                Dim intSiteID_ForEvent As Integer = Convert.ToInt32(drEvent("siteID"))
                ucCommentsModule.SetupCommentModule(intSiteID_ForEvent, ModuleTypeID, intEventID, intMemberID, boolStatus, boolEnablePublicCommentSubmission)
            End If

        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub btn_Signup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Signup.Click
        If Page.IsValid Then
            addSignupInformation()
        End If
    End Sub

    Private Sub addSignupInformation()
        'Insert Event Submission
        Dim intEventID As Integer = Convert.ToInt32(Request.QueryString("id"))

        Dim intSalutationID As Integer = Integer.MinValue
        If ddlSalutation.SelectedValue.Length > 0 Then
            intSalutationID = Convert.ToInt32(ddlSalutation.SelectedValue)
        End If
        Dim strFirstName As String = txtFirstName.Text.Trim()
        Dim strLastName As String = txtLastName.Text.Trim()
        Dim strEmail As String = txtEmailAddress.Text.Trim()
        Dim strIpAddress As String = Request.ServerVariables("LOCAL_ADDR")
        Dim dtTimeStamp As DateTime = DateTime.Now()

        Dim strPhoneNumber As String = txtPhoneNumber.Text.Trim()

        If intMemberID = 0 Then
            intMemberID = Integer.MinValue
        End If

        Dim strAddress As String = ucAddress.LocationStreet
        Dim strCity As String = ucAddress.LocationCity
        Dim intStateID As Integer = ucAddress.LocationState
        Dim strZipCode As String = ucAddress.LocationZipCode
        Dim intCountryID As Integer = ucAddress.LocationCountry
        Dim pairLocationLatitude As Pair = ucAddress.LocationLatitudeLongitude
        Dim strLocationLatitude As String = pairLocationLatitude.First
        Dim strLocationLongitude As String = pairLocationLatitude.Second



        Dim intEventSubmissionID As Integer = EventDAL.InsertEventSubmission(intEventID, intMemberID, intSalutationID, strFirstName, strLastName, strEmail, strPhoneNumber, strAddress, strCity, intStateID, strZipCode, intCountryID, strLocationLatitude, strLocationLongitude, True, strIpAddress, dtTimeStamp)

        'set the return to Employment Opportunity Link
        aReturnToEventDetail.HRef = "?id=" & intEventID

        'hide/show appropriate divs/panels
        divEventItem.Visible = False
        signUpPanel.Visible = False
        confirmationPanel.Visible = True
        sendConfirmation(strEmail)
    End Sub

    Public Sub sendConfirmation(ByVal email As String)

        'Use EmailDAL to send confirmation email
        If Not Request.QueryString("id") Is Nothing Then
            Dim eventID As String = Request.QueryString("id")
            Dim dtEvent As DataTable = EventDAL.GetEvent_ByEventID(eventID)
            If dtEvent.Rows.Count > 0 Then
                Dim drEvent As DataRow = dtEvent.Rows(0)

                'Create our swapout hashtable
                Dim EmailSwapoutData As New Hashtable()

                Dim strSalutation As String = String.Empty
                If ddlSalutation.SelectedValue.Length > 0 Then
                    strSalutation = ddlSalutation.SelectedItem.Text
                End If

                'Add the Event Title to our email swapout data
                EmailSwapoutData("EventTitle") = drEvent("Title").ToString()

                'Get the start and end date/time  string, which we add to our email swapout hashtable
                Dim strStartAndEndDateString As String = ""
                strStartAndEndDateString += Format(drEvent("StartDate"), "D").ToString()

                If Not drEvent("startTime").ToString() = "" Then
                    strStartAndEndDateString += " " & Format(drEvent("startTime"), "h:mm tt").ToString
                End If

                If drEvent("startDate").ToString() = drEvent("endDate").ToString() Then

                    If Not IsDBNull(drEvent("endTime")) Then
                        strStartAndEndDateString += " - " & Format(drEvent("endTime"), "h:mm tt").ToString
                    End If

                Else
                    If Not IsDBNull(drEvent("endDate")) Then
                        strStartAndEndDateString += " - " & Format(drEvent("endDate"), "D").ToString()
                        If Not IsDBNull(drEvent("endTime").ToString()) Then
                            strStartAndEndDateString += " " & Format(drEvent("endTime"), "h:mm tt").ToString
                        End If
                    Else
                        If Not IsDBNull(drEvent("endTime")) Then
                            strStartAndEndDateString += " - " & Format(drEvent("endTime"), "h:mm tt").ToString
                        End If

                    End If

                End If

                'Add Start/End Date to our email swapout list
                EmailSwapoutData("StartAndEndDate") = strStartAndEndDateString

                Dim strAddress As String = ""
                If Not drEvent("Address") Is DBNull.Value Then
                    strAddress = drEvent("Address").ToString().Trim()
                End If
                Dim strCity As String = ""
                If Not drEvent("City") Is DBNull.Value Then
                    strCity = drEvent("City").ToString().Trim()
                End If
                Dim intStateID As Integer = Integer.MinValue
                If Not drEvent("StateID") Is DBNull.Value Then
                    intStateID = Convert.ToInt32(drEvent("StateID"))
                End If
                Dim strZipCode As String = ""
                If Not drEvent("ZipCode") Is DBNull.Value Then
                    strZipCode = drEvent("ZipCode").ToString().Trim()
                End If
                Dim intCountryID As Integer = Integer.MinValue
                If Not drEvent("CountryID") Is DBNull.Value Then
                    intCountryID = Convert.ToInt32(drEvent("CountryID"))
                End If
                Dim strTextLocationInformation As String = CommonWeb.FormatAddress(strAddress, strCity, intStateID, strZipCode, intCountryID, CommonWeb.AddressFormat.MultipleLines)
                Dim strHtmlLocationInformation As String = CommonWeb.FormatAddress(strAddress, strCity, intStateID, strZipCode, intCountryID, CommonWeb.AddressFormat.MultipleLinesForHTML)


                'Add Location to the TextLocationInformation only used in the bodyText version
                EmailSwapoutData("TextLocationInformation") = strTextLocationInformation

                'Add Location to the HtmlLocationInformation only used in the bodyHtml version
                EmailSwapoutData("HtmlLocationInformation") = strHtmlLocationInformation


                'Populate a list of recipient 'to' addresses
                Dim listRecipientEmailAddresses As New ArrayList()
                listRecipientEmailAddresses.Add(email)

                'Send our Email using the EmailDAL using Event Application Email Type (ID=3)
                EmailDAL.SendEmail(listRecipientEmailAddresses, 3, intSiteID, EmailSwapoutData)


                'Send Email to HR
                'Create our swapout hashtable
                Dim EmailSwapoutData_Administrator As New Hashtable()

                'Add the Employment Title to our email swapout data
                EmailSwapoutData_Administrator("Salutation") = strSalutation
                EmailSwapoutData_Administrator("FirstName") = txtFirstName.Text.Trim()
                EmailSwapoutData_Administrator("LastName") = txtLastName.Text.Trim()
                EmailSwapoutData_Administrator("EventTitle") = drEvent("Title").ToString()


                'Add Location to the TextLocationInformation only used in the bodyText version
                EmailSwapoutData_Administrator("TextLocationInformation") = strTextLocationInformation

                'Add Location to the HtmlLocationInformation only used in the bodyHtml version
                EmailSwapoutData_Administrator("HtmlLocationInformation") = strHtmlLocationInformation


                'Also if this event row, invovles an additional contact person we will also send them the same email we will send to the administrator
                Dim listRecipientEmailAddresses_Administrator As New ArrayList()
                If Not drEvent("ContactPerson") Is DBNull.Value Then
                    Dim strContactPerson As String = drEvent("ContactPerson").ToString()
                    If strContactPerson.Length > 0 Then
                        listRecipientEmailAddresses_Administrator.Add(strContactPerson)
                    End If
                End If

                'Send our Email using the EmailDAL using Event Application to Administrator Email Type (ID=4) -> This is an Administrator Email, but we also want to send this Administrator Email to the contact person
                EmailDAL.SendEmail(listRecipientEmailAddresses_Administrator, 4, intSiteID, EmailSwapoutData_Administrator)
            End If


        End If


    End Sub

End Class
