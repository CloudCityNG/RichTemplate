Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Principal
Imports Microsoft.Web.Administration


Public Class LocationDAL

#Region "Select"

    Public Shared Function GetLocation_ByID(ByVal LocationID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Location_Select_ByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = LocationID

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

    Public Shared Function GetLocation_ByCityLetter(ByVal CityLetter As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Location_SelectList_ByCityLetter"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@CityLetter", SqlDbType.NVarChar).Value = CityLetter

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

    Public Shared Function GetLocationList() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Location_SelectList"
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

    Public Shared Function GetLocationList_Distinct() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Location_SelectList_Distinct"
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

#End Region

#Region "Insert"
    Public Shared Function InsertLocation(ByVal CategoryID As Integer, ByVal Location As String, ByVal Address1 As String, ByVal Address2 As String, ByVal Address3 As String, ByVal City As String, ByVal State_Province As String, ByVal Zip As String) As Integer
        Dim intLocationID As Integer = Integer.MinValue

        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Location_InsertLocation"
            sqlComm.Connection = sqlConn

            If CategoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID
            End If

            sqlComm.Parameters.Add("@Location", SqlDbType.NVarChar).Value = Location

            If Address1.Length = 0 Then
                sqlComm.Parameters.Add("@Address1", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Address1", SqlDbType.NVarChar).Value = Address1
            End If

            If Address2.Length = 0 Then
                sqlComm.Parameters.Add("@Address2", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Address2", SqlDbType.NVarChar).Value = Address2
            End If

            If Address3.Length = 0 Then
                sqlComm.Parameters.Add("@Address3", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Address3", SqlDbType.NVarChar).Value = Address3
            End If

            If City.Length = 0 Then
                sqlComm.Parameters.Add("@City", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@City", SqlDbType.NVarChar).Value = City
            End If

            If State_Province.Length = 0 Then
                sqlComm.Parameters.Add("@State_Province", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@State_Province", SqlDbType.NVarChar).Value = State_Province
            End If

            If Zip.Length = 0 Then
                sqlComm.Parameters.Add("@Zip", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Zip", SqlDbType.NVarChar).Value = Zip
            End If

            sqlConn.Open()
            intLocationID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try

        Return intLocationID
    End Function

#End Region

#Region "Update"

    Public Shared Sub UpdateLocation_ByID(ByVal ID As Integer, ByVal CategoryID As Integer, ByVal Location As String, ByVal Address1 As String, ByVal Address2 As String, ByVal Address3 As String, ByVal City As String, ByVal State_Province As String, ByVal Zip As String)
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Location_UpdateLocation"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID

            If CategoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID
            End If

            sqlComm.Parameters.Add("@Location", SqlDbType.NVarChar).Value = Location


            If Address1.Length = 0 Then
                sqlComm.Parameters.Add("@Address1", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Address1", SqlDbType.NVarChar).Value = Address1
            End If

            If Address2.Length = 0 Then
                sqlComm.Parameters.Add("@Address2", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Address2", SqlDbType.NVarChar).Value = Address2
            End If

            If Address3.Length = 0 Then
                sqlComm.Parameters.Add("@Address3", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Address3", SqlDbType.NVarChar).Value = Address3
            End If

            If City.Length = 0 Then
                sqlComm.Parameters.Add("@City", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@City", SqlDbType.NVarChar).Value = City
            End If

            If State_Province.Length = 0 Then
                sqlComm.Parameters.Add("@State_Province", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@State_Province", SqlDbType.NVarChar).Value = State_Province
            End If

            If Zip.Length = 0 Then
                sqlComm.Parameters.Add("@Zip", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Zip", SqlDbType.NVarChar).Value = Zip
            End If

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

#End Region

#Region "Delete"
    Public Shared Sub DeleteLocation_ByID(ByVal ID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Location_DeleteLocation"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

#End Region

End Class
