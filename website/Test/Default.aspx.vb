
Imports System.Xml

Partial Class Test_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ValidateGoogleAddress("100 Main Street", "USA")
    End Sub

    Public Shared Function ValidateGoogleAddress(ByVal AddressToValidate As String, ByVal CountryToValidate As String) As SortedDictionary(Of String, XmlNode)
        Dim sortedGoogleAddresses As New SortedDictionary(Of String, XmlNode)
        Try
            Dim xmlDoc As New XmlDocument()
            If AddressToValidate.IndexOf(CountryToValidate) < 0 Then
                AddressToValidate += "," + CountryToValidate
            End If

            Dim strUrlQuery As String = "address=" + AddressToValidate + "&output=xml&sensor=fales&key=" + ConfigurationManager.AppSettings("GoogleMapAPIKey")

            Dim httpResponseText = GoogleMap.RequestResponse("https://maps.googleapis.com/maps/api/geocode/xml", strUrlQuery, False)

            xmlDoc.LoadXml(httpResponseText)

            'Parse XML
            Dim xmlRoot As XmlElement = xmlDoc.DocumentElement
            Dim intNumResults = xmlRoot.SelectNodes("/GeocodeResponse/result").Count

            If intNumResults > 0 Then
                For Each xmlNode As XmlNode In xmlRoot.SelectNodes("/GeocodeResponse/result")
                    Dim xmlNode_Address As XmlNode = xmlNode.SelectSingleNode("formatted_address")

                    'First check their is a street name
                    Dim xmlNode_Street = xmlNode.SelectSingleNode("address_component[type='route']/long_name")
                    Dim xmlNode_City = xmlNode.SelectSingleNode("address_component[type='locality']/long_name")
                    'If the City can not be found, then try use the SubAdministrativeArea
                    If xmlNode_City Is Nothing Then
                        xmlNode_City = xmlNode.SelectSingleNode("address_component[type='administrative_area_level_2']/long_name")
                    End If
                    Dim xmlNode_State = xmlNode.SelectSingleNode("address_component[type='administrative_area_level_1']/short_name")
                    Dim xmlNode_Zip = xmlNode.SelectSingleNode("address_component[type='postal_code']/long_name")

                    Dim xmlNode_Country = xmlNode.SelectSingleNode("address_component[type='country']/long_name")

                    'Need to make sure the address has a street, city, state and zip
                    If (Not xmlNode_Street Is Nothing) AndAlso (Not xmlNode_City Is Nothing) AndAlso (Not xmlNode_State Is Nothing) AndAlso (Not xmlNode_Zip Is Nothing) AndAlso (Not xmlNode_Country Is Nothing) Then
                        If xmlNode_Street.InnerText.Length > 0 AndAlso xmlNode_City.InnerText.Length > 0 AndAlso xmlNode_State.InnerText.Length > 0 AndAlso xmlNode_Zip.InnerText.Length > 0 AndAlso xmlNode_Country.InnerText.Length > 0 Then
                            Dim strGoogleAddressPossibility As String = xmlNode_Address.InnerText
                            If Not sortedGoogleAddresses.ContainsKey(strGoogleAddressPossibility) Then
                                sortedGoogleAddresses.Add(strGoogleAddressPossibility, xmlNode)
                            End If
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            RichTemplateCentralDAL.Error_LogError(ex)
        End Try

        Return sortedGoogleAddresses
    End Function
End Class
