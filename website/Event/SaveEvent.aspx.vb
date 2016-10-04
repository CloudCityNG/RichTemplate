Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Xml

Partial Class Event_SaveEvent
    Inherits RichTemplateLanguagePage

    'Change this if you want to automatically approve Event Submissions
    Const bAutomaticApproval As Boolean = False

    Dim ModuleTypeID As Integer = 5

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Setup Rad Controls
        CommonWeb.SetupRadDatePicker(startDate, intSiteID)
        CommonWeb.SetupRadDatePicker(endDate, intSiteID)
        CommonWeb.SetupRadEditor(Me.Page, txtBody, "~/editorConfig/toolbars/ToolsFileFrontEnd.xml", intSiteID)
        CommonWeb.SetupRadProgressArea(radProgressAreaEventImage, intSiteID)
        CommonWeb.SetupRadTimePicker(startTime, intSiteID)
        CommonWeb.SetupRadTimePicker(endTime, intSiteID)
        CommonWeb.SetupRadUpload(radUploadEventImage, intSiteID)

        'Check access and setup back button
        If Not IsPostBack Then

            Dim boolAllowOnlineSubmissions As Boolean = False
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "allow_online_submissions" Then
                    boolAllowOnlineSubmissions = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_registrations" Then
                    'show the online signup section
                    divAllowOnlineSignup.Visible = True
                End If
            Next

            If boolAllowOnlineSubmissions AndAlso intMemberID > 0 Then
                BindCategoryDropDownListData()

                LoadEvent()
            Else
                'User is not logged in or we do not allow online submissions so redirect them to the event listing
                Response.Redirect("default.aspx")
            End If

        End If
    End Sub

    Public Sub BindCategoryDropDownListData()
        'Here we bind the dropdown list to categories
        Dim dtCategory As DataTable = CategoryDAL.GetCategoryList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)

        With rcbCategoryID
            .DataSource = dtCategory
            .DataValueField = "categoryID"
            .DataTextField = "categoryName"

        End With
        rcbCategoryID.DataBind()

    End Sub

    Private Sub LoadEvent()

        'Set the default header title
        Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Event_FrontEnd.Event_SaveEvent_HeaderTitle

        If Not Request.QueryString("ID") Is Nothing Then
            Dim intEventID As Integer = Convert.ToInt32(Request.QueryString("ID"))

            Dim dtEvent As DataTable = EventDAL.GetEvent_ByEventIDAndSiteID(intEventID, intSiteID)
            If dtEvent.Rows.Count > 0 Then
                CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Event_FrontEnd.Event_SaveEvent_HeadingUpdate)
                btnAddEditEvent.Text = Resources.Event_FrontEnd.Event_SaveEvent_ButtonUpdate

                Dim drEvent As DataRow = dtEvent.Rows(0)

                'First we check if the current user was the user who actually created this event
                Dim boolUserCreatedThis As Boolean = False
                If Not drEvent("authorID_member") Is DBNull.Value Then
                    Dim intAuthorID_Member As Integer = Convert.ToInt32(drEvent("authorID_member"))
                    If intMemberID > 0 And intMemberID = intAuthorID_Member Then
                        boolUserCreatedThis = True
                    End If
                End If

                'Only continue populating this Save Event page if the user actually created this event, if not then redirect them to the event detail page for this event
                If boolUserCreatedThis Then
                    aBack.HRef = "Default.aspx?id=" & intEventID.ToString()

                    Dim strTitle As String = drEvent("title").ToString()
                    'Set the Page Title
                    Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Event_FrontEnd.Event_SaveEvent_HeaderTitle + " - " + strTitle

                    txtTitle.Text = strTitle
                    txtSummary.Text = drEvent("Summary").ToString()
                    txtBody.Content = drEvent("Body").ToString()

                    If (drEvent("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Event_FrontEnd.Event_SaveEvent_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drEvent("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drEvent("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Event_FrontEnd.Event_SaveEvent_UnCategorized, ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Event_FrontEnd.Event_SaveEvent_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    End If

                    If Not drEvent("startDate").ToString() = "" Then
                        Me.startDate.SelectedDate = drEvent("startDate").ToString()
                    End If
                    If Not drEvent("endDate").ToString() = "" Then
                        Me.endDate.SelectedDate = drEvent("endDate").ToString()
                    End If
                    If Not drEvent("startTime").ToString() = "" Then
                        Me.startTime.SelectedDate = drEvent("startTime").ToString()
                    End If
                    If Not drEvent("endTime").ToString() = "" Then
                        Me.endTime.SelectedDate = drEvent("endTime").ToString()
                    End If
                    If Not drEvent("Address").ToString() = "" Then
                        ucAddress.LocationStreet = drEvent("Address").ToString()
                    End If

                    If Not drEvent("City").ToString() = "" Then
                        ucAddress.LocationCity = drEvent("City").ToString()
                    End If

                    If Not drEvent("StateID").ToString() = "" Then
                        ucAddress.LocationState = drEvent("StateID").ToString()
                    End If

                    If Not drEvent("ZipCode").ToString() = "" Then
                        ucAddress.LocationZipCode = drEvent("ZipCode").ToString()
                    End If

                    If Not drEvent("CountryID").ToString() = "" Then
                        ucAddress.LocationCountry = drEvent("CountryID").ToString()
                    End If

                    version.Text = drEvent("Version").ToString

                    onlineSignup.SelectedValue = drEvent("onlineSignup").ToString

                    Me.eventImage.Visible = False
                    Me.lnkDeleteImage.Visible = False
                    If Not drEvent("thumbnail") Is DBNull.Value Then
                        If Not drEvent("thumbnail").ToString() = "" Then

                            Me.eventImage.DataValue = drEvent("thumbnail")
                            Me.eventImage.Visible = True
                            Me.lnkDeleteImage.Visible = True
                            Me.divEventImage.Visible = True

                            Me.divUploadImage.Visible = False

                        End If
                    End If
                Else
                    'User did not create this event, so redirect to the event the detail page
                    Response.Redirect("Default.aspx?id=" & intEventID)
                End If

            Else
                'ID was supplied but could not be found, so redirect to the modules default page
                Response.Redirect("Default.aspx")
            End If
        Else
            aBack.HRef = "Default.aspx"

            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Event_FrontEnd.Event_SaveEvent_HeadingAdd)
            btnAddEditEvent.Text = Resources.Event_FrontEnd.Event_SaveEvent_ButtonAdd


            rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Event_FrontEnd.Event_SaveEvent_UnCategorized, ""))
            rcbCategoryID.SelectedValue = ""

            onlineSignup.SelectedValue = False

            divUploadImage.Visible = True
        End If
    End Sub

    Protected Sub addEditEvent()
        If intMemberID > 0 Then
            If Request("ID") Is Nothing Then

                Dim strTitle As String = Me.txtTitle.Text.Trim().ToString()
                Dim strSummary As String = Me.txtSummary.Text.Trim().ToString()

                Dim intCategoryID As Integer = Integer.MinValue
                If Not rcbCategoryID.SelectedValue = "" Then
                    intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
                End If

                Dim dtStart As DateTime = DateTime.MinValue
                If Not startDate.SelectedDate.ToString() = "" Then
                    dtStart = startDate.SelectedDate
                End If
                Dim dtEnd As DateTime = DateTime.MinValue
                If Not endDate.SelectedDate.ToString() = "" Then
                    dtEnd = endDate.SelectedDate
                End If

                Dim tStart As DateTime = DateTime.MinValue
                If Not startTime.SelectedDate.ToString = "" Then
                    tStart = startTime.SelectedDate
                End If
                Dim tEnd As DateTime = DateTime.MinValue
                If Not endTime.SelectedDate.ToString = "" Then
                    tEnd = endTime.SelectedDate
                End If

                Dim strBodyContent As String = txtBody.Content.ToString()

                Dim strAddress As String = ucAddress.LocationStreet
                Dim strCity As String = ucAddress.LocationCity
                Dim intStateID As Integer = ucAddress.LocationState
                Dim strZipCode As String = ucAddress.LocationZipCode
                Dim intCountryID As Integer = ucAddress.LocationCountry
                Dim pairLocationLatitude As Pair = ucAddress.LocationLatitudeLongitude
                Dim strLocationLatitude As String = pairLocationLatitude.First
                Dim strLocationLongitude As String = pairLocationLatitude.Second

                Dim boolOnlineSignup As Boolean = Convert.ToBoolean(onlineSignup.SelectedValue)
                Dim strContactPerson As String = txtContactPerson.Text.Trim()

                Dim authorID_member As Integer = intMemberID
                Dim authorID_admin As Integer = Integer.MinValue

                Dim intModifiedID_member As Integer = Integer.MinValue
                Dim intModifiedID_admin As Integer = Integer.MinValue


                'Default values for user submitted events
                Dim boolStatus As Boolean = True 'default for user submitted events
                Dim boolGeoLocation As Boolean = True
                Dim strExternalLink As String = String.Empty

                Dim dtPublication As DateTime = DateTime.MinValue
                Dim dtExpiration As DateTime = DateTime.MinValue

                Dim boolAvailableToAllSites As Boolean = False ' Default this member-created event to only be viewable for this site

                Dim strMetaTitle As String = String.Empty
                Dim strMetaKeywords As String = String.Empty
                Dim strMetaDescription As String = String.Empty
                Dim strMetaOther As String = String.Empty


                Dim listGroupIDs As String = String.Empty
                Dim listMemberIDs As String = String.Empty
                Dim strSearchTagID As String = String.Empty

                Dim intVersion As Integer = 1
                Dim dtDateTimeStamp As DateTime = DateTime.Now

                'INSERT EVENT
                Dim intEventID As Integer = EventDAL.InsertEvent(intSiteID, boolAvailableToAllSites, strTitle, strSummary, intCategoryID, dtStart, dtEnd, tStart, tEnd, strBodyContent, strExternalLink, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, dtDateTimeStamp, intVersion, strAddress, strCity, intStateID, strZipCode, intCountryID, strLocationLatitude, strLocationLongitude, boolGeoLocation, dtPublication, dtExpiration, boolOnlineSignup, strContactPerson, authorID_member, authorID_admin, intModifiedID_member, intModifiedID_admin)

                'Add event image if it exists
                If radUploadEventImage.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = radUploadEventImage.UploadedFiles(0)
                    Dim strThumbnailName As String = file.GetName
                    Dim bytesEventImage(file.InputStream.Length - 1) As Byte
                    file.InputStream.Read(bytesEventImage, 0, file.InputStream.Length)
                    EventDAL.UpdateEvent_EventImage_ByEventID(intEventID, strThumbnailName, bytesEventImage)

                End If

                SendEmail()

                If Not bAutomaticApproval Then
                    'Show Event Record has been submitted, and awaiting approval Message
                    divModuleContentMain.Visible = False
                    divModuleContentSubmitted.Visible = True
                Else
                    'Then set access for this record to EVERYONE, and Send the user to this newly created event
                    ModuleDAL.InsertModuleAccess(ModuleTypeID, intEventID, 0, Integer.MinValue)
                    Response.Redirect("default.aspx?id=" & intEventID)
                End If

            Else
                Dim intEventID As Integer = Request.QueryString("ID")

                Dim strTitle As String = Me.txtTitle.Text.Trim().ToString()
                Dim strSummary As String = Me.txtSummary.Text.Trim().ToString()

                Dim intCategoryID As Integer = Integer.MinValue
                If Not rcbCategoryID.SelectedValue = "" Then
                    intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
                End If

                Dim dtStart As DateTime = DateTime.MinValue
                If Not startDate.SelectedDate.ToString() = "" Then
                    dtStart = startDate.SelectedDate
                End If
                Dim dtEnd As DateTime = DateTime.MinValue
                If Not endDate.SelectedDate.ToString() = "" Then
                    dtEnd = endDate.SelectedDate
                End If

                Dim tStart As DateTime = DateTime.MinValue
                If Not startTime.SelectedDate.ToString = "" Then
                    tStart = startTime.SelectedDate
                End If
                Dim tEnd As DateTime = DateTime.MinValue
                If Not endTime.SelectedDate.ToString = "" Then
                    tEnd = endTime.SelectedDate
                End If

                Dim strBodyContent As String = txtBody.Content.ToString()

                Dim strAddress As String = ucAddress.LocationStreet
                Dim strCity As String = ucAddress.LocationCity
                Dim intStateID As Integer = ucAddress.LocationState
                Dim strZipCode As String = ucAddress.LocationZipCode
                Dim intCountryID As Integer = ucAddress.LocationCountry
                Dim pairLocationLatitude As Pair = ucAddress.LocationLatitudeLongitude
                Dim strLocationLatitude As String = pairLocationLatitude.First
                Dim strLocationLongitude As String = pairLocationLatitude.Second

                Dim boolOnlineSignup As Boolean = Convert.ToBoolean(onlineSignup.SelectedValue)

                Dim intVersion As Integer = 1
                If Not IsDBNull(version.Text.Trim()) And version.Text.Trim() <> "" Then
                    intVersion = Convert.ToInt32(version.Text.Trim())
                    intVersion = intVersion + 1
                End If

                Dim intModifiedID_member As Integer = Integer.MinValue
                Dim intModifiedID_admin As Integer = Integer.MinValue

                EventDAL.UpdateEvent_ByEventID_FrontEnd(intEventID, strTitle, strSummary, intCategoryID, dtStart, dtEnd, tStart, tEnd, strBodyContent, intVersion, strAddress, strCity, intStateID, strZipCode, intCountryID, strLocationLatitude, strLocationLongitude, boolOnlineSignup, intModifiedID_member, intModifiedID_admin)

                'Add event image if it exists
                If radUploadEventImage.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = radUploadEventImage.UploadedFiles(0)
                    Dim strThumbnailName As String = file.GetName
                    Dim bytesEventImage(file.InputStream.Length - 1) As Byte
                    file.InputStream.Read(bytesEventImage, 0, file.InputStream.Length)
                    EventDAL.UpdateEvent_EventImage_ByEventID(intEventID, strThumbnailName, bytesEventImage)

                End If

                Response.Redirect("default.aspx?id=" & intEventID)
            End If
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

    Protected Sub btnAddEditEvent_Click(ByVal sender As Object, ByVal e As EventArgs)
        If IsValid Then
            addEditEvent()
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not Request.QueryString("ID") Is Nothing Then
            Dim eventID As String = Request.QueryString("ID")
            Response.Redirect("Default.aspx?id=" & eventID.ToString())
        Else
            Response.Redirect("default.aspx")
        End If

    End Sub

#Region "Event Image"
    Protected Sub lnkDeleteImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteImage.Click
        Dim eventID As Integer = Convert.ToInt32(Request.QueryString("ID"))
        Dim bytesEventImage() As Byte
        EventDAL.UpdateEvent_EventImage_ByEventID(eventID, String.Empty, bytesEventImage)

        'Hide the faceImage and the delete link
        eventImage.Visible = False
        lnkDeleteImage.Visible = False
        divEventImage.Visible = False

        divUploadImage.Visible = True
    End Sub

    Protected Sub customValEventImageSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add event image if it exists, must have a file size less than 100kb, but we give them a 5kb buffer
        If radUploadEventImage.UploadedFiles.Count > 0 Then
            For Each file As UploadedFile In radUploadEventImage.UploadedFiles
                If file.InputStream.Length > 102400 Then
                    e.IsValid = False
                End If
            Next
        End If
    End Sub

#End Region

    Protected Sub SendEmail()

        ' Send Event Confirmation to user
        'First get the member who submitted this event record
        Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intMemberID)
        If dtMember.Rows.Count > 0 Then
            Dim drMembrer As DataRow = dtMember.Rows(0)

            Dim strEmailAddress As String = drMembrer("Email").ToString()
            Dim strEventTitle As String = txtTitle.Text.Trim()
            Dim strEventSummary As String = txtSummary.Text.Trim()

            'Get the start and end date/time  string, which we add to our email swapout hashtable
            Dim strStartAndEndDateString As String = ""
            If Not startDate.SelectedDate.ToString() = "" Then
                strStartAndEndDateString += Format(startDate.SelectedDate, "D").ToString()
            End If


            If Not startTime.SelectedDate.ToString() = "" Then
                strStartAndEndDateString += " " & Format(startTime.SelectedDate, "h:mm tt").ToString
            End If

            If startDate.SelectedDate.ToString() = endDate.SelectedDate.ToString() Then

                If Not endTime.SelectedDate.ToString() = "" Then
                    strStartAndEndDateString += " - " & Format(endTime.SelectedDate, "h:mm tt").ToString
                End If

            Else
                If Not endDate.SelectedDate.ToString() = "" Then
                    strStartAndEndDateString += " - " & Format(endDate.SelectedDate, "D").ToString()
                    If Not endTime.SelectedDate.ToString() = "" Then
                        strStartAndEndDateString += " " & Format(endTime.SelectedDate, "h:mm tt").ToString
                    End If
                Else
                    If Not endTime.SelectedDate.ToString() = "" Then
                        strStartAndEndDateString += " - " & Format(endTime.SelectedDate, "h:mm tt").ToString
                    End If

                End If
            End If

            'Get the Event Location Information
            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry

            Dim strTextLocationInformation As String = CommonWeb.FormatAddress(strAddress, strCity, intStateID, strZipCode, intCountryID, CommonWeb.AddressFormat.MultipleLines)
            If strTextLocationInformation.Trim.Length = 0 Then
                strTextLocationInformation = Resources.Event_FrontEnd.Event_SaveEvent_LocationNotAvailable
            End If
            Dim strHtmlLocationInformation As String = CommonWeb.FormatAddress(strAddress, strCity, intStateID, strZipCode, intCountryID, CommonWeb.AddressFormat.MultipleLinesForHTML)
            If strHtmlLocationInformation.Trim.Length = 0 Then
                strHtmlLocationInformation = Resources.Event_FrontEnd.Event_SaveEvent_LocationNotAvailable
            End If

            'Get the Event Category Information
            Dim strCategoryName As String = Resources.Event_FrontEnd.Event_SaveEvent_UnCategorized
            If rcbCategoryID.SelectedValue.Length > 0 Then
                Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryID(rcbCategoryID.SelectedValue)
                If dtCategory.Rows.Count > 0 Then
                    Dim drCategory As DataRow = dtCategory.Rows(0)
                    strCategoryName = drCategory("CategoryName").ToString()
                End If
            End If

            'We have no swap out data for this email
            Dim EmailSwapoutData_User As New Hashtable()
            EmailSwapoutData_User("EventTitle") = strEventTitle

            'Populate the list of recipients
            Dim listRecipientEmailAddress_User As New ArrayList()
            listRecipientEmailAddress_User.Add(strEmailAddress)

            'Send this information to our email DAL with Email Type ID = 18 -> Event Submitted - Sent To User
            EmailDAL.SendEmail(listRecipientEmailAddress_User, 18, intSiteID, EmailSwapoutData_User)


            'Always send an email to the appropriate administrator
            'Send Email to site administrator informing them of a new event entry
            Dim EmailSwapoutData_Administrator As New Hashtable()

            'Add the members ID and email address to this email
            EmailSwapoutData_Administrator("EmailAddress") = strEmailAddress
            EmailSwapoutData_Administrator("EventTitle") = strEventTitle
            EmailSwapoutData_Administrator("EventSummary") = strEventSummary
            EmailSwapoutData_Administrator("EventStartAndEndDate") = strStartAndEndDateString
            EmailSwapoutData_Administrator("TextLocationInformation") = strTextLocationInformation
            EmailSwapoutData_Administrator("HtmlLocationInformation") = strHtmlLocationInformation
            EmailSwapoutData_Administrator("EventCategory") = strCategoryName

            'Send this information to our email DAL with Email Type ID = 19 -> Event Submitted - Sent To Administrator -> This is an Administrator Email
            EmailDAL.SendEmail(19, intSiteID, EmailSwapoutData_Administrator)
        End If
    End Sub
End Class
