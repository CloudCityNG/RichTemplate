Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class LanguageDAL

#Region "Language"
    Public Shared Function GetLanguage_ByID(ByVal ID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Language_Select_ByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID

            Dim da As New SqlDataAdapter(sqlComm)
            sqlConn.Open()
            da.Fill(dt)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dt
    End Function

    Public Shared Function GetLanguageList() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Language_SelectList"
            sqlComm.Connection = sqlConn

            Dim da As New SqlDataAdapter(sqlComm)
            sqlConn.Open()
            da.Fill(dt)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dt
    End Function

    Public Shared Function GetCurrentLanguageCode_BySiteID(ByVal SiteID As Integer) As String
        Dim strLanguagePreference As String = String.Empty
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) AndAlso (Not HttpContext.Current.Session("LanguagePreference") Is Nothing) Then
            'Check if we can load the language code from session
            strLanguagePreference = HttpContext.Current.Session("LanguagePreference")
        End If

        'Get the Language that this site has been set to
        If strLanguagePreference = String.Empty Then
            'Get the Current Site's Default Langauge
            Dim dtSite As DataTable = SiteDAL.GetSite_ByID(SiteID)
            If dtSite.Rows.Count > 0 Then
                Dim drSite As DataRow = dtSite.Rows(0)
                Dim intLanguageID_Default As Integer = Convert.ToInt32(drSite("LanguageID_Default"))
                'Now we have the default language ID for the site, so try and get this language string
                Dim dtLanguage As DataTable = LanguageDAL.GetLanguage_ByID(intLanguageID_Default)
                If dtLanguage.Rows.Count > 0 Then
                    Dim drLanguage As DataRow = dtLanguage.Rows(0)
                    strLanguagePreference = drLanguage("Code").ToString()
                    SetCurrentLanguage = strLanguagePreference
                End If
            End If
        End If

        'If we still can not find the Default Language, default to United States English
        If strLanguagePreference = String.Empty Then
            'If there is no language in session and we can not find the default language for this supplied site, we set the default overall application language to "en-US"
            strLanguagePreference = "en-US"
            SetCurrentLanguage = strLanguagePreference
        End If

        Return strLanguagePreference

    End Function

    Public Shared WriteOnly Property SetCurrentLanguage() As String
        Set(ByVal value As String)

            'If a language is supplied, set the current language to this, otherwise, set the language to default Sites language
            If Not HttpContext.Current.Session Is Nothing Then
                HttpContext.Current.Session("LanguagePreference") = value
            End If
        End Set
    End Property

    Public Shared Function GetResourceValueForCurrentLanuage(ByVal strResourceFile As String, ByVal strResourceKey As String) As String

        ' Retrieve the value of the string resource based on the strResourceKey, using HttpContext
        ' This will retrieve the value of the localized resource using the caller's current culture setting.
        Dim strResourceValue As String = HttpContext.GetGlobalResourceObject(strResourceFile, strResourceKey)
        Return strResourceValue

    End Function

    Public Shared Function InsertLanguage(ByVal Language As String, ByVal Code As String, ByVal Description As String) As Integer
        Dim intLanguageID As Integer = Integer.MinValue

        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Language_InsertLanguage"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@Language", SqlDbType.NVarChar).Value = Language

            sqlComm.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Code

            If CommonWeb.stripHTML(Description).Trim() = String.Empty Then
                sqlComm.Parameters.Add("@Description", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description
            End If

            sqlConn.Open()
            intLanguageID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try

        Return intLanguageID
    End Function

    Public Shared Sub UpdateLanguage_ByID(ByVal LanguageID As Integer, ByVal Language As String, ByVal Code As String, ByVal Description As String)
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Language_UpdateLanguage_ByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = LanguageID

            sqlComm.Parameters.Add("@Language", SqlDbType.NVarChar).Value = Language

            sqlComm.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Code

            If CommonWeb.stripHTML(Description).Trim() = String.Empty Then
                sqlComm.Parameters.Add("@Description", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description
            End If

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub DeleteLanguage_ByLanguageID(ByVal LanguageID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Language_Delete_ByLanguageID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@LanguageID", SqlDbType.Int).Value = LanguageID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

#End Region

#Region "LanguageLetters"

    Public Shared Function GetLanguageLetters_ByLanguageID(ByVal LanguageID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_LanguageLetters_SelectList_ByLanguageID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@LanguageID", SqlDbType.Int).Value = LanguageID

            Dim da As New SqlDataAdapter(sqlComm)
            sqlConn.Open()
            da.Fill(dt)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dt
    End Function

    Public Shared Function GetLanguageLetters_ByLanguageCode(ByVal LanguageCode As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_LanguageLetters_SelectList_ByLanguageCode"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@LanguageCode", SqlDbType.NVarChar).Value = LanguageCode

            Dim da As New SqlDataAdapter(sqlComm)
            sqlConn.Open()
            da.Fill(dt)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dt
    End Function

    Public Shared Function InsertLanguageLetters(ByVal LanguageID As Integer, ByVal LetterLowerCase As String, ByVal LetterUpperCase As String) As Integer
        Dim ID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_LanguageLetter_InsertLanguageLetter"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@LanguageID", SqlDbType.Int).Value = LanguageID

            sqlComm.Parameters.Add("@LetterLowerCase", SqlDbType.NVarChar).Value = LetterLowerCase
            sqlComm.Parameters.Add("@LetterUpperCase", SqlDbType.NVarChar).Value = LetterUpperCase

            sqlConn.Open()
            ID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return ID
    End Function

    Public Shared Sub DeleteLanguageLetters_ByLanguageID(ByVal LanguageID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_LanguageLetters_Delete_ByLanguageID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@LanguageID", SqlDbType.Int).Value = LanguageID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub
#End Region

#Region "Language Resource File Template"
    Public Shared Function LanguageResourceFile_Header() As String
        Dim strLanguageResourceFileSchemaElement As String = "" & _
"  <xsd:schema id=""root"" xmlns="""" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:msdata=""urn:schemas-microsoft-com:xml-msdata"">" & Environment.NewLine & _
"    <xsd:import namespace=""http://www.w3.org/XML/1998/namespace"" />" & Environment.NewLine & _
"    <xsd:element name=""root"" msdata:IsDataSet=""true"">" & Environment.NewLine & _
"      <xsd:complexType>" & Environment.NewLine & _
"        <xsd:choice maxOccurs=""unbounded"">" & Environment.NewLine & _
"          <xsd:element name=""metadata"">" & Environment.NewLine & _
"            <xsd:complexType>" & Environment.NewLine & _
"              <xsd:sequence>" & Environment.NewLine & _
"                <xsd:element name=""value"" type=""xsd:string"" minOccurs=""0"" />" & Environment.NewLine & _
"              </xsd:sequence>" & Environment.NewLine & _
"              <xsd:attribute name=""name"" use=""required"" type=""xsd:string"" />" & Environment.NewLine & _
"              <xsd:attribute name=""type"" type=""xsd:string"" />" & Environment.NewLine & _
"              <xsd:attribute name=""mimetype"" type=""xsd:string"" />" & Environment.NewLine & _
"              <xsd:attribute ref=""xml:space"" />" & Environment.NewLine & _
"            </xsd:complexType>" & Environment.NewLine & _
"          </xsd:element>" & Environment.NewLine & _
"          <xsd:element name=""assembly"">" & Environment.NewLine & _
"            <xsd:complexType>" & Environment.NewLine & _
"              <xsd:attribute name=""alias"" type=""xsd:string"" />" & Environment.NewLine & _
"              <xsd:attribute name=""name"" type=""xsd:string"" />" & Environment.NewLine & _
"            </xsd:complexType>" & Environment.NewLine & _
"          </xsd:element>" & Environment.NewLine & _
"          <xsd:element name=""data"">" & Environment.NewLine & _
"            <xsd:complexType>" & Environment.NewLine & _
"              <xsd:sequence>" & Environment.NewLine & _
"                <xsd:element name=""value"" type=""xsd:string"" minOccurs=""0"" msdata:Ordinal=""1"" />" & Environment.NewLine & _
"                <xsd:element name=""comment"" type=""xsd:string"" minOccurs=""0"" msdata:Ordinal=""2"" />" & Environment.NewLine & _
"              </xsd:sequence>" & Environment.NewLine & _
"              <xsd:attribute name=""name"" type=""xsd:string"" use=""required"" msdata:Ordinal=""1"" />" & Environment.NewLine & _
"              <xsd:attribute name=""type"" type=""xsd:string"" msdata:Ordinal=""3"" />" & Environment.NewLine & _
"              <xsd:attribute name=""mimetype"" type=""xsd:string"" msdata:Ordinal=""4"" />" & Environment.NewLine & _
"              <xsd:attribute ref=""xml:space"" />" & Environment.NewLine & _
"            </xsd:complexType>" & Environment.NewLine & _
"          </xsd:element>" & Environment.NewLine & _
"          <xsd:element name=""resheader"">" & Environment.NewLine & _
"            <xsd:complexType>" & Environment.NewLine & _
"              <xsd:sequence>" & Environment.NewLine & _
"                <xsd:element name=""value"" type=""xsd:string"" minOccurs=""0"" msdata:Ordinal=""1"" />" & Environment.NewLine & _
"              </xsd:sequence>" & Environment.NewLine & _
"              <xsd:attribute name=""name"" type=""xsd:string"" use=""required"" />" & Environment.NewLine & _
"            </xsd:complexType>" & Environment.NewLine & _
"          </xsd:element>" & Environment.NewLine & _
"        </xsd:choice>" & Environment.NewLine & _
"      </xsd:complexType>" & Environment.NewLine & _
"    </xsd:element>" & Environment.NewLine & _
"  </xsd:schema>" & Environment.NewLine & _
"  <resheader name=""resmimetype"">" & Environment.NewLine & _
"    <value>text/microsoft-resx</value>" & Environment.NewLine & _
"  </resheader>" & Environment.NewLine & _
"  <resheader name=""version"">" & Environment.NewLine & _
"    <value>2.0</value>" & Environment.NewLine & _
"  </resheader>" & Environment.NewLine & _
"  <resheader name=""reader"">" & Environment.NewLine & _
"    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>" & Environment.NewLine & _
"  </resheader>" & Environment.NewLine & _
"  <resheader name=""writer"">" & Environment.NewLine & _
"    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>" & Environment.NewLine & _
"  </resheader>"

        Return strLanguageResourceFileSchemaElement
    End Function
#End Region
End Class



