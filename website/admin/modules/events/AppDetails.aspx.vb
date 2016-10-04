Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Xml

Partial Class admin_modules_employment_AppDetails
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 5 ' Module Type: Event

    Dim SubmissionID As Integer
    Dim intEventID As Integer

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

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Event_Admin.Event_AddEditApplicants_Header

        If Not Request.QueryString("eventID") Is Nothing Then

            intEventID = Convert.ToInt32(Request.QueryString("eventID"))

            If Not Page.IsPostBack Then

                'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
                If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                    Response.Redirect("/richadmin")
                End If

                aBackToRegistrations.HRef = "applicants.aspx?eventID=" & intEventID
                BindSalutationDropDown()

                If Not Request.QueryString("subID") Is Nothing Then

                    Dim intSubmissionID As Integer = Convert.ToInt32(Request.QueryString("subID"))

                   

                    Dim dtEventSubmission As DataTable = EventDAL.GetEventSubmissions_BySubIDAndSiteID(intSubmissionID, SiteDAL.GetCurrentSiteID_Admin())
                    If dtEventSubmission.Rows.Count > 0 Then
                        Dim drEventSubmission As DataRow = dtEventSubmission.Rows(0)

                        If Not drEventSubmission("MemberID") Is DBNull.Value Then
                            txtMemberID.Text = drEventSubmission("MemberID")
                        End If

                        Status.SelectedValue = drEventSubmission("Status").ToString

                        If Not drEventSubmission("SalutationID") Is DBNull.Value Then
                            Me.ddlSalutation.SelectedValue = drEventSubmission("SalutationID").ToString()
                        End If
                        Me.txtFirstName.Text = drEventSubmission("FirstName").ToString()
                        Me.txtLastName.Text = drEventSubmission("LastName").ToString()
                        Me.txtEmailAddress.Text = drEventSubmission("Email").ToString()

                        Me.txtPhoneNumber.Text = drEventSubmission("PhoneNumber").ToString()

                        If Not drEventSubmission("Address").ToString() = "" Then
                            ucAddress.LocationStreet = drEventSubmission("Address").ToString()
                        End If

                        If Not drEventSubmission("City").ToString() = "" Then
                            ucAddress.LocationCity = drEventSubmission("City").ToString()
                        End If

                        If Not drEventSubmission("StateID").ToString() = "" Then
                            ucAddress.LocationState = drEventSubmission("StateID").ToString()
                        End If

                        If Not drEventSubmission("ZipCode").ToString() = "" Then
                            ucAddress.LocationZipCode = drEventSubmission("ZipCode").ToString()
                        End If

                        If Not drEventSubmission("CountryID").ToString() = "" Then
                            ucAddress.LocationCountry = drEventSubmission("CountryID").ToString()
                        End If

                        btnAddEdit.Text = Resources.Event_Admin.Event_AddEditApplicants_ButtonUpdate

                        'Finally Check if we should make this Event READ-ONLY
                        Dim intSiteID As Integer = Convert.ToInt32(drEventSubmission("SiteID"))
                        If Not SiteDAL.GetCurrentSiteID_Admin() = intSiteID Then
                            MakeEventReadOnly(intSiteID)
                        End If

                    Else
                        'sub ID row not found, so redirect to main event page
                        Response.Redirect("Applicants.aspx?eventID=" & intEventID)
                    End If
                Else
                    btnAddEdit.Text = Resources.Event_Admin.Event_AddEditApplicants_ButtonAdd

                    Status.SelectedValue = True
                End If

            End If
        Else
            'NO EVENT ID found, so redirect to main event page
            Response.Redirect("Default.aspx")
        End If

    End Sub

    Private Sub MakeEventReadOnly(ByVal SiteID As Integer)

        'Prevent AdminUser from updating this record
        btnAddEdit.Visible = False

    End Sub

    Protected Sub addUpdateRecord()
        If Request.QueryString("subID") Is Nothing Then
            'add application registration
            Dim intMemberID As Integer = Integer.MinValue
            If txtMemberID.Text.Length > 0 Then
                intMemberID = Convert.ToInt32(txtMemberID.Text)
            End If

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)

            Dim intSalutationID As Integer = Integer.MinValue
            If ddlSalutation.SelectedValue.Length > 0 Then
                intSalutationID = Convert.ToInt32(ddlSalutation.SelectedValue)
            End If
            Dim strFirstName As String = txtFirstName.Text.Trim()
            Dim strLastName As String = txtLastName.Text.Trim()
            Dim strEmail As String = txtEmailAddress.Text.Trim()

            Dim strPhoneNumber As String = txtPhoneNumber.Text.Trim()

            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry
            Dim pairLocationLatitude As Pair = ucAddress.LocationLatitudeLongitude
            Dim strLocationLatitude As String = pairLocationLatitude.First
            Dim strLocationLongitude As String = pairLocationLatitude.Second

            Dim strIpAddress As String = HttpContext.Current.Request.UserHostAddress
            Dim dtDateTimeStamp As DateTime = DateTime.Now

            Dim intEventSubmissionID As Integer = EventDAL.InsertEventSubmission(intEventID, intMemberID, intSalutationID, strFirstName, strLastName, strEmail, strPhoneNumber, strAddress, strCity, intStateID, strZipCode, intCountryID, strLocationLatitude, strLocationLongitude, boolStatus, strIpAddress, dtDateTimeStamp)

        Else
            'update application registration
            Dim intSubmissionID As Integer = Convert.ToInt32(Request.QueryString("subID"))

            Dim intMemberID As Integer = Integer.MinValue
            If txtMemberID.Text.Length > 0 Then
                intMemberID = Convert.ToInt32(txtMemberID.Text.Trim())
            End If

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)

            Dim intSalutationID As Integer = Integer.MinValue
            If ddlSalutation.SelectedValue.Length > 0 Then
                intSalutationID = Convert.ToInt32(ddlSalutation.SelectedValue)
            End If
            Dim strFirstName As String = txtFirstName.Text.Trim()
            Dim strLastName As String = txtLastName.Text.Trim()
            Dim strEmail As String = txtEmailAddress.Text.Trim()

            Dim strPhoneNumber As String = txtPhoneNumber.Text.Trim()

            Dim strAddress As String = ucAddress.LocationStreet
            Dim strCity As String = ucAddress.LocationCity
            Dim intStateID As Integer = ucAddress.LocationState
            Dim strZipCode As String = ucAddress.LocationZipCode
            Dim intCountryID As Integer = ucAddress.LocationCountry
            Dim pairLocationLatitude As Pair = ucAddress.LocationLatitudeLongitude
            Dim strLocationLatitude As String = pairLocationLatitude.First
            Dim strLocationLongitude As String = pairLocationLatitude.Second

            Dim intEventSubmissionID As Integer = EventDAL.UpdateEventSubmission(intSubmissionID, intMemberID, intSalutationID, strFirstName, strLastName, strEmail, strPhoneNumber, strAddress, strCity, intStateID, strZipCode, intCountryID, strLocationLatitude, strLocationLongitude, boolStatus)

        End If
    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click
        If IsValid Then
            addUpdateRecord()


            Response.Redirect("Applicants.aspx?eventID=" & intEventID)
        End If

    End Sub

    Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelButton.Click

        Response.Redirect("Applicants.aspx?eventID=" & intEventID)

    End Sub

    Protected Sub customValMemberExist_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)

        'If a memberID is added we check it exists
        Dim intMemberID As Integer = Integer.MinValue
        Dim strMemberID As String = txtMemberID.Text.Trim()

        'Only check the member id if it is entered
        If strMemberID.Length > 0 Then
            If Integer.TryParse(strMemberID, intMemberID) Then
                Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberID(intMemberID)
                If dtMember.Rows.Count = 0 Then
                    'MemberID does not exists in our members table
                    e.IsValid = False
                End If
            Else
                'Member ID is not an integer
                e.IsValid = False
            End If
        End If

    End Sub

End Class
