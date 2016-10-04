Imports System.Data
Imports System.Xml
Imports System.Drawing

Partial Class UserController_Address
    Inherits System.Web.UI.UserControl

#Region "Address Properties"


    Enum AddressLayoutEnum
        Vertical_TextAbove = 1
        Vertical_TextLeft = 2
        Horizontal = 3
    End Enum

    Enum AddressValidationLayoutEnum
        None = 1
        SideBySide_AbsoluteListPosition = 2
        SideBySide_RelativeListPosition = 3
        Below = 4
    End Enum
    Private _addressLayout As AddressLayoutEnum = AddressLayoutEnum.Horizontal
    Public Property AddressLayout() As AddressLayoutEnum
        Get
            Return _addressLayout
        End Get
        Set(ByVal value As AddressLayoutEnum)
            _addressLayout = value
        End Set
    End Property

    Private _addressValidationLayout As AddressValidationLayoutEnum = AddressValidationLayoutEnum.None
    Public Property AddressValidationLayout() As AddressValidationLayoutEnum
        Get
            Return _addressValidationLayout
        End Get
        Set(ByVal value As AddressValidationLayoutEnum)
            _addressValidationLayout = value
        End Set
    End Property

    Private _errorMessage As String
    Public Property ErrorMessage() As String
        Get
            Return _errorMessage
        End Get
        Set(ByVal value As String)
            _errorMessage = value
        End Set
    End Property

    Private _required As Boolean
    Public Property Required() As Boolean
        Get
            Return _required
        End Get
        Set(ByVal value As Boolean)
            _required = value
        End Set
    End Property

    Private _validationGroup As String
    Public Property ValidationGroup As String
        Get
            Return _validationGroup
        End Get
        Set(ByVal value As String)
            _validationGroup = value
        End Set
    End Property

    Public Property Width() As Unit
        Get
            Return pnlAddress.Width
        End Get
        Set(ByVal value As Unit)
            pnlAddress.Width = value
        End Set
    End Property

    Public Property LocationStreet As String
        Get
            Return txtLocationStreet.Text.Trim()
        End Get
        Set(ByVal value As String)
            txtLocationStreet.Text = value
        End Set
    End Property

    Public Property LocationCity As String
        Get
            Return txtLocationCity.Text.Trim()
        End Get
        Set(ByVal value As String)
            txtLocationCity.Text = value
        End Set
    End Property

    Public Property LocationState As Integer
        Get
            Dim intStateID As Integer = Integer.MinValue
            If ddlLocationState.SelectedValue.Length > 0 Then
                intStateID = Convert.ToInt32(ddlLocationState.SelectedValue)
            End If
            Return intStateID
        End Get
        Set(ByVal value As Integer)
            ddlLocationState.SelectedValue = value
        End Set
    End Property

    Public Property LocationCountry As Integer
        Get
            Dim intCountryID As Integer = Integer.MinValue
            If ddlLocationCountry.SelectedValue.Length > 0 Then
                intCountryID = Convert.ToInt32(ddlLocationCountry.SelectedValue)
            End If
            Return intCountryID
        End Get
        Set(ByVal value As Integer)
            ddlLocationCountry.SelectedValue = value
        End Set
    End Property


    Public Property LocationZipCode As String
        Get
            Return txtLocationZip.Text.Trim()
        End Get
        Set(ByVal value As String)
            txtLocationZip.Text = value
        End Set
    End Property

    Public Property LocationLatitudeLongitude As Pair
        Get
            'First we need to re-generate the latitude longitude, but without doing FULL check address
            Return RegenerateLatitudeLongitude()
        End Get
        Set(ByVal value As Pair)

            hdnLatitude.Value = value.First.ToString()
            hdnLongitude.Value = value.Second.ToString()
        End Set
    End Property

#End Region
    Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        'Load all checkbox lists and dropdowns
        BindStateData()
        BindCountryData()
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

            InitializeControl()
            RegenerateLatitudeLongitude()

        End If
    End Sub

    Public Sub MakeReadOnly()
        divLocationStreet.Visible = False
        litLocationStreet.Text = txtLocationStreet.Text
        divLocationStreet_ReadOnly.Visible = True

        divLocationCity.Visible = False
        litLocationCity.Text = txtLocationCity.Text
        divLocationCity_ReadOnly.Visible = True

        divLocationState.Visible = False
        litLocationState.Text = ddlLocationState.SelectedItem.Text
        divLocationState_ReadOnly.Visible = True

        divLocationZip.Visible = False
        litLocationZip.Text = txtLocationZip.Text
        divLocationZip_ReadOnly.Visible = True

        divLocationCountry.Visible = False
        litLocationCountry.Text = ddlLocationCountry.SelectedItem.Text
        divLocationCountry_ReadOnly.Visible = True

        divAddressValidationContainer.Visible = False
    End Sub

    Private Sub InitializeControl()
        'Set Inital Control State
        pnlAddressValidation.Visible = (AddressValidationLayout <> AddressValidationLayoutEnum.None)

        If Required Then
            'setup required fields if we are using them
            divReqLocationStreet.Visible = True
            divReqLocationCity.Visible = True
            divReqLocationState.Visible = True
            divReqLocationCountry.Visible = True

            reqLocationStreet.Enabled = True
            reqLocationCity.Enabled = True
            reqLocationState.Enabled = True
            reqLocationCountry.Enabled = True

            reqLocationStreet.ErrorMessage = If(String.IsNullOrEmpty(ErrorMessage), Resources.Address_UserControl.Address_Required, ErrorMessage)
            reqLocationCity.ErrorMessage = If(String.IsNullOrEmpty(ErrorMessage), Resources.Address_UserControl.Address_Required, ErrorMessage)
            reqLocationState.ErrorMessage = If(String.IsNullOrEmpty(ErrorMessage), Resources.Address_UserControl.Address_Required, ErrorMessage)
            reqLocationCountry.ErrorMessage = If(String.IsNullOrEmpty(ErrorMessage), Resources.Address_UserControl.Address_Required, ErrorMessage)

            reqLocationStreet.ValidationGroup = ValidationGroup
            reqLocationCity.ValidationGroup = ValidationGroup
            reqLocationState.ValidationGroup = ValidationGroup
            reqLocationCountry.ValidationGroup = ValidationGroup

            reqLocationStreet.Visible = True
            reqLocationCity.Visible = True
            reqLocationState.Visible = True
            reqLocationCountry.Visible = True

        End If

        'setup address layout
        pnlAddress.CssClass = "pnlAddress " & "Address_" & AddressLayout.ToString() & "_Layout "
        pnlAddressValidation.CssClass = "pnlAddressValidation " & "AddressValidation_" & AddressValidationLayout.ToString() & "_Layout"
    End Sub

    Private Sub BindStateData()
        Dim dtStates As DataTable = StateDAL.GetStateList()
        ddlLocationState.Items.Clear()
        ddlLocationState.Items.Add(New ListItem(Resources.Address_UserControl.Address_AddressState_PleaseSelect, ""))
        ddlLocationState.Items.Add(New ListItem(Resources.Address_UserControl.Address_AddressState_NotAvailable, "0"))
        For Each drState As DataRow In dtStates.Rows
            Dim intStateID As Integer = drState("ID")
            Dim strStateName As String = drState("StateName")
            Dim strStateAbbreviation As String = drState("StateAbbreviation")
            ddlLocationState.Items.Add(New ListItem(strStateAbbreviation & " - " & strStateName, intStateID.ToString()))
        Next
    End Sub

    Private Sub BindCountryData()
        Dim dtCountry As DataTable = CountryDAL.GetCountryList()
        ddlLocationCountry.Items.Clear()
        ddlLocationCountry.Items.Add(New ListItem(Resources.Address_UserControl.Address_AddressCountry_PleaseSelect, ""))
        For Each drCountry As DataRow In dtCountry.Rows
            Dim intCountryID As Integer = drCountry("CountryID")
            Dim strCountryName As String = drCountry("CountryName")
            ddlLocationCountry.Items.Add(New ListItem(strCountryName, intCountryID.ToString()))
        Next
    End Sub

    Protected Sub lnkAddressValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkAddressValidate.Click
        'First reset latitude and longitude
        hdnLatitude.Value = ""
        hdnLongitude.Value = ""

        'First hide all google messages
        divAddressNotValidated.Visible = False
        divAddressValid.Visible = False
        divGoogleAddressPossibilities.Visible = False

        If Page.IsValid Then
            ValidateAddress()
        End If
    End Sub

    Protected Sub lnkContinueUnvalidated_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkContinueUnvalidated.Click
        divGoogleAddressPossibilities.Visible = False
        divAddressNotValidated.Visible = True
    End Sub

    Protected Sub rptGoogleAddresses_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptGoogleAddresses.ItemDataBound
        Dim lit_Address As Literal = e.Item.FindControl("lit_Address")
        Dim lnk_Address As LinkButton = e.Item.FindControl("lnk_Address")

        Dim genericPair As Generic.KeyValuePair(Of String, XmlNode) = e.Item.DataItem
        lit_Address.Text = genericPair.Key

        Dim node As XmlNode = genericPair.Value

        Dim strStreetNumber As String = String.Empty
        If Not node.SelectSingleNode("address_component[type='street_number']/long_name") Is Nothing Then
            strStreetNumber = node.SelectSingleNode("address_component[type='street_number']/long_name").InnerText
        End If

        Dim strStreetName As String = String.Empty
        If Not node.SelectSingleNode("address_component[type='route']/long_name") Is Nothing Then
            strStreetName = node.SelectSingleNode("address_component[type='route']/long_name").InnerText
        End If

        Dim strStreet As String = String.Format("{0} {1}", strStreetNumber, strStreetName)

        'If the City can not be found, then try use the SubAdministrativeArea
        Dim strCity As String = String.Empty
        If Not node.SelectSingleNode("address_component[type='locality']/long_name") Is Nothing Then
            strCity = node.SelectSingleNode("address_component[type='locality']/long_name").InnerText
        Else
            strCity = node.SelectSingleNode("address_component[type='administrative_area_level_2']/long_name").InnerText
        End If

        Dim strState As String = node.SelectSingleNode("address_component[type='administrative_area_level_1']/short_name").InnerText

        'If the Zip can not be found, then try use the SubAdministrativeArea
        Dim strZip As String = String.Empty
        If Not node.SelectSingleNode("address_component[type='postal_code']/long_name") Is Nothing Then
            strZip = node.SelectSingleNode("address_component[type='postal_code']/long_name").InnerText
        End If
        Dim strCountry As String = node.SelectSingleNode("address_component[type='country']/long_name").InnerText

        'sometimes the street number will not be stored in the returned xml, so we find the index of the street in our litAddress
        Dim intStreetIndex As Integer = genericPair.Key.IndexOf(strStreet)
        If intStreetIndex > 0 Then
            'Then the returned street is not at the start of our full address string e.g. address = '1 Beach Rd, Auckland central 1010, New Zealand
            'but our street node returns Beach Road, which does not have a starting index of 0, it has a starting index of 2 -> so prepend everything before this
            strStreet = genericPair.Key.Substring(0, intStreetIndex) & strStreet
        End If
        lnk_Address.Attributes.Add("street", strStreet)
        lnk_Address.Attributes.Add("city", strCity)
        lnk_Address.Attributes.Add("state", strState)
        lnk_Address.Attributes.Add("zip", strZip)
        lnk_Address.Attributes.Add("country", strCountry)

    End Sub

    Protected Sub lnk_Address_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk_Address As LinkButton = sender
        txtLocationStreet.Text = lnk_Address.Attributes("street")
        txtLocationCity.Text = lnk_Address.Attributes("city")
        ddlLocationState.SelectedValue = "0"
        Dim strState As String = lnk_Address.Attributes("state")
        Dim dtState As DataTable = StateDAL.GetState_ByStateAbbreviation(strState)
        If dtState.Rows.Count > 0 Then
            ddlLocationState.SelectedValue = dtState.Rows(0)("ID").ToString()
        ElseIf txtLocationCity.Text.ToLower <> strState.ToLower() Then
            'This state or area-name is not a us state, so just append this to our city if its different to our city
            txtLocationCity.Text = txtLocationCity.Text & ", " & strState
        End If

        txtLocationZip.Text = lnk_Address.Attributes("zip")
        Dim strCountry As String = lnk_Address.Attributes("country")
        Dim dtCountry As DataTable = CountryDAL.GetCountry_ByCountryName(strCountry)
        If dtCountry.Rows.Count > 0 Then
            ddlLocationCountry.SelectedValue = dtCountry.Rows(0)("CountryID").ToString()
        End If

        ValidateAddress()

    End Sub

    Private Function ValidateAddress() As Boolean

        Dim boolValidateAddress As Boolean = False

        'Hide validation error
        divAddressNotValidated.Visible = False
        divGoogleAddressPossibilities.Visible = False

        'Check address is validated
        Dim strSelectedStateName As String = ""
        Dim strSelectedStateAbbreviation As String = ""
        Dim strSelectedCountryName As String = ""


        'include the state if it has been selected
        If ddlLocationState.SelectedValue.Trim().Length > 0 Then
            Dim dtState As DataTable = StateDAL.GetState_ByID(Convert.ToInt32(ddlLocationState.SelectedValue))
            If dtState.Rows.Count > 0 Then
                strSelectedStateName = dtState.Rows(0)("StateName")
                strSelectedStateAbbreviation = dtState.Rows(0)("StateAbbreviation")
            End If
        End If

        'include the country
        If ddlLocationCountry.SelectedValue.Trim().Length > 0 Then
            Dim dtCountry As DataTable = CountryDAL.GetCountry_ByCountryID(Convert.ToInt32(ddlLocationCountry.SelectedValue))
            If dtCountry.Rows.Count > 0 Then
                strSelectedCountryName = dtCountry.Rows(0)("CountryName")
            End If
        End If

        Dim fullAddress As String = Server.UrlEncode(txtLocationStreet.Text.Trim() & "," & txtLocationCity.Text.Trim() & "," & strSelectedStateName & "," & txtLocationZip.Text.Trim())
        Dim gAddresses As SortedDictionary(Of String, XmlNode) = GoogleMap.ValidateGoogleAddress(fullAddress, strSelectedCountryName)
        If gAddresses.Count = 0 Then
            divAddressNotValidated.Visible = True
        ElseIf gAddresses.Count > 1 Then
            'Setup a list of possible addresses
            rptGoogleAddresses.DataSource = gAddresses
            rptGoogleAddresses.DataBind()
            divGoogleAddressPossibilities.Visible = True
            divAddressNotValidated.Visible = True

        Else
            'If street, city, state and zip are correct, else load the Multiple address possibilities panel
            Dim node As XmlNode = gAddresses.Values()(0)

            Dim strStreetNumber As String = String.Empty
            If Not node.SelectSingleNode("address_component[type='street_number']/long_name") Is Nothing Then
                strStreetNumber = node.SelectSingleNode("address_component[type='street_number']/long_name").InnerText
            End If

            Dim strStreetName As String = String.Empty
            If Not node.SelectSingleNode("address_component[type='route']/long_name") Is Nothing Then
                strStreetName = node.SelectSingleNode("address_component[type='route']/long_name").InnerText
            End If

            Dim strStreet As String = String.Format("{0} {1}", strStreetNumber, strStreetName)

            'If the City can not be found, then try use the SubAdministrativeArea
            Dim strCity As String = String.Empty
            If Not node.SelectSingleNode("address_component[type='locality']/long_name") Is Nothing Then
                strCity = node.SelectSingleNode("address_component[type='locality']/long_name").InnerText
            Else
                strCity = node.SelectSingleNode("address_component[type='administrative_area_level_2']/long_name").InnerText
            End If

            Dim strState As String = node.SelectSingleNode("address_component[type='administrative_area_level_1']/short_name").InnerText

            'If the Zip can not be found, then try use the SubAdministrativeArea
            Dim strZip As String = String.Empty
            If Not node.SelectSingleNode("address_component[type='postal_code']/long_name") Is Nothing Then
                strZip = node.SelectSingleNode("address_component[type='postal_code']/long_name").InnerText
            End If
            Dim strCountry As String = node.SelectSingleNode("address_component[type='country']/long_name").InnerText

            'sometimes the street number will not be stored in the returned xml, so we find the index of the street in our litAddress
            Dim intStreetIndex As Integer = txtLocationStreet.Text.Trim().IndexOf(strStreet)
            If intStreetIndex > 0 Then
                'Then the returned street is not at the start of our full address string e.g. address = '1 Beach Rd, Auckland central 1010, New Zealand
                'but our street node returns Beach Road, which does not have a starting index of 0, it has a starting index of 2 -> so prepend everything before this
                strStreet = txtLocationStreet.Text.Trim().Substring(0, intStreetIndex) & strStreet
            End If

            'set the latitude and longitude values and return true, and populate the textboxes
            hdnLatitude.Value = node.SelectSingleNode("geometry/location/lat").InnerText
            hdnLongitude.Value = node.SelectSingleNode("geometry/location/lng").InnerText

            txtLocationStreet.Text = strStreet
            txtLocationCity.Text = strCity
            ddlLocationState.SelectedValue = "0"
            If strState.Length > 0 Then
                Dim dtState As DataTable = StateDAL.GetState_ByStateAbbreviation(strState)
                If dtState.Rows.Count > 0 Then
                    Dim intStateID As Integer = Convert.ToInt32(dtState.Rows(0)("ID"))
                    ddlLocationState.SelectedValue = intStateID
                ElseIf strCity.ToLower <> strState.ToLower() Then
                    'This state or area-name is not a us state, so just append this to our city if its different to our city
                    txtLocationCity.Text = strCity & ", " & strState
                End If
            End If

            Dim dtCountry As DataTable = CountryDAL.GetCountry_ByCountryNameOrCountryCode(strCountry)
            If dtCountry.Rows.Count > 0 Then
                Dim intCountryID As Integer = Convert.ToInt32(dtCountry.Rows(0)("CountryID"))
                ddlLocationCountry.SelectedValue = intCountryID
            End If
            txtLocationZip.Text = strZip

            divAddressValid.Visible = True

        End If

        Return boolValidateAddress
    End Function

    'Similar function to validate address, but without updating the text boxes, just gives upto date latitude and longitude values
    Private Function RegenerateLatitudeLongitude() As Pair
        Dim boolAddressValid As Boolean = False

        'First reset latitude and longitude
        hdnLatitude.Value = ""
        hdnLongitude.Value = ""


        'Check address is validated
        Dim strSelectedStateName As String = ""
        Dim strSelectedStateAbbreviation As String = ""
        Dim strSelectedCountryName As String = ""

        'include the state if it has been selected
        If ddlLocationState.SelectedValue.Trim().Length > 0 Then
            Dim dtState As DataTable = StateDAL.GetState_ByID(Convert.ToInt32(ddlLocationState.SelectedValue))
            If dtState.Rows.Count > 0 Then
                strSelectedStateName = dtState.Rows(0)("StateName")
                strSelectedStateAbbreviation = dtState.Rows(0)("StateAbbreviation")
            End If
        End If

        'include the country
        If ddlLocationCountry.SelectedValue.Trim().Length > 0 Then
            Dim dtCountry As DataTable = CountryDAL.GetCountry_ByCountryID(Convert.ToInt32(ddlLocationCountry.SelectedValue))
            If dtCountry.Rows.Count > 0 Then
                strSelectedCountryName = dtCountry.Rows(0)("CountryName")
            End If
        End If

        Dim fullAddress As String = Server.UrlEncode(txtLocationStreet.Text.Trim() & "," & txtLocationCity.Text.Trim() & "," & strSelectedStateName & "," & txtLocationZip.Text.Trim())
        Dim gAddresses As SortedDictionary(Of String, XmlNode) = GoogleMap.ValidateGoogleAddress(fullAddress, strSelectedCountryName)
        If gAddresses.Count = 1 Then 'Then we may have a valid address, next we check the textboxes against this google address

            'If street, city, state and zip are correct, else load the Multiple address possibilities panel
            Dim node As XmlNode = gAddresses.Values()(0)
            'If the Street can not be found, then try use the SubAdministrativeArea

            Dim strStreetNumber As String = String.Empty
            If Not node.SelectSingleNode("address_component[type='street_number']/long_name") Is Nothing Then
                strStreetNumber = node.SelectSingleNode("address_component[type='street_number']/long_name").InnerText
            End If

            Dim strStreetName As String = String.Empty
            If Not node.SelectSingleNode("address_component[type='route']/long_name") Is Nothing Then
                strStreetName = node.SelectSingleNode("address_component[type='route']/long_name").InnerText
            End If

            Dim strStreet As String = String.Format("{0} {1}", strStreetNumber, strStreetName)

            'If the City can not be found, then try use the SubAdministrativeArea
            Dim strCity As String = String.Empty
            If Not node.SelectSingleNode("address_component[type='locality']/long_name") Is Nothing Then
                strCity = node.SelectSingleNode("address_component[type='locality']/long_name").InnerText
            Else
                strCity = node.SelectSingleNode("address_component[type='administrative_area_level_2']/long_name").InnerText
            End If

            Dim strState As String = node.SelectSingleNode("address_component[type='administrative_area_level_1']/short_name").InnerText

            'If the Zip can not be found, then try use the SubAdministrativeArea
            Dim strZip As String = String.Empty
            If Not node.SelectSingleNode("address_component[type='postal_code']/long_name") Is Nothing Then
                strZip = node.SelectSingleNode("address_component[type='postal_code']/long_name").InnerText
            End If
            Dim strCountry As String = node.SelectSingleNode("address_component[type='country']/long_name").InnerText

            'sometimes the street number will not be stored in the returned xml, so we find the index of the street in our litAddress
            Dim intStreetIndex As Integer = txtLocationStreet.Text.Trim().IndexOf(strStreet)
            If intStreetIndex > 0 Then
                'Then the returned street is not at the start of our full address string e.g. address = '1 Beach Rd, Auckland central 1010, New Zealand
                'but our street node returns Beach Road, which does not have a starting index of 0, it has a starting index of 2 -> so prepend everything before this
                strStreet = txtLocationStreet.Text.Trim().Substring(0, intStreetIndex) & strStreet
            End If

            Dim intStateID As Integer = 0
            Dim dtState As DataTable = StateDAL.GetState_ByStateAbbreviation(strState)
            If dtState.Rows.Count > 0 Then
                intStateID = Convert.ToInt32(dtState.Rows(0)("ID"))
            Else
                strCity = strCity & ", " & strState
            End If

            Dim intCountryID As Integer = 0
            Dim dtCountry As DataTable = CountryDAL.GetCountry_ByCountryNameOrCountryCode(strCountry)
            If dtCountry.Rows.Count > 0 Then
                intCountryID = Convert.ToInt32(dtCountry.Rows(0)("CountryID"))
            End If

            If txtLocationStreet.Text.Trim().ToLower = strStreet.ToLower() And txtLocationCity.Text.Trim().ToLower = strCity.ToLower() And ddlLocationState.SelectedValue = intStateID.ToString() And ddlLocationCountry.SelectedValue = intCountryID.ToString() And txtLocationZip.Text.Trim().ToLower() = strZip.ToLower() Then
                'So the users entered address is a valid address
                'set the latitude and longitude values and return true, and populate the textboxes
                boolAddressValid = True
                hdnLatitude.Value = node.SelectSingleNode("geometry/location/lat").InnerText
                hdnLongitude.Value = node.SelectSingleNode("geometry/location/lng").InnerText

            End If

        End If
        divAddressNotValidated.Visible = Not boolAddressValid
        divAddressValid.Visible = boolAddressValid

        Return New Pair(hdnLatitude.Value, hdnLongitude.Value)
    End Function

    Protected Sub customValAddressValidate_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Before validating an address, both the street and country must be supplied
        If txtLocationStreet.Text.Trim().Length = 0 Or ddlLocationCountry.SelectedValue.Length = 0 Then
            e.IsValid = False
        End If
    End Sub
End Class
