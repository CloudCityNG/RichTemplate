Imports Subgurim.Controles
Partial Class UserController_GoogleMap
    Inherits System.Web.UI.UserControl
    Private _Latitude As String = ""
    Public Property Latitude() As String
        Get
            Return _Latitude
        End Get
        Set(ByVal value As String)
            _Latitude = value
        End Set
    End Property
    Private _Longitude As String = ""
    Public Property Longitude() As String
        Get
            Return _Longitude
        End Get
        Set(ByVal value As String)
            _Longitude = value
        End Set
    End Property
    Private _ZoomLevel As String = "1"
    Public Property ZoomLevel() As String
        Get
            Return _ZoomLevel
        End Get
        Set(ByVal value As String)
            _ZoomLevel = value
        End Set
    End Property

    Private _Width As String = ""
    Public Property Width() As String
        Get
            Return _Width
        End Get
        Set(ByVal value As String)
            _Width = value
        End Set
    End Property

    Private _Height As String = ""
    Public Property Height() As String
        Get
            Return _Height
        End Get
        Set(ByVal value As String)
            _Height = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        SetupGoogleMap()
    End Sub

    Private Sub SetupGoogleMap()
        'Before we setup the map, we check if a latitude/longitude have been specified
        If Latitude.Length > 0 And Longitude.Length > 0 Then

            'convert the latitude and longitude into a type double, using the english culture where a '.' is a decimal seperator
            Dim formatterEnglish As New System.Globalization.NumberFormatInfo()
            formatterEnglish.NumberDecimalSeparator = "."

            Dim dblLatitude As Decimal = Convert.ToDouble(Latitude, formatterEnglish)
            Dim dblLongitude As Decimal = Convert.ToDouble(Longitude, formatterEnglish)

            'Reset google maps
            ucGoogleMap.Visible = True
            ucGoogleMap.reset()

            'To ensure no markers are placed on this global map view, we load in a new marker manager
            Dim gMarkerManager As New MarkerManager()
            ucGoogleMap.markerManager = gMarkerManager

            Dim gLatLong As New GLatLng(dblLatitude, dblLongitude)
            ucGoogleMap.setCenter(gLatLong, ZoomLevel)

            Dim gMarkerOptions As New GMarkerOptions(New GIcon())
            Dim gMarker As New GMarker(gLatLong, gMarkerOptions)
            ucGoogleMap.markerManager.Add(gMarker, 1)

            If Width.Length > 0 Then
                ucGoogleMap.Width = Width().ToLower.Replace("px", "")
            End If
            If Height.Length > 0 Then
                ucGoogleMap.Height = Height().ToLower.Replace("px", "")
            End If
        Else
            'We hide ou google map
            ucGoogleMap.Visible = False
        End If
    End Sub
End Class
