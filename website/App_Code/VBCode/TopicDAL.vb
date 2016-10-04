Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class TopicDAL

#Region "Topic"
    Public Shared Function GetTopic_ByTopicID(ByVal TopicID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Topic_Select_ByTopicID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@TopicID", SqlDbType.Int).Value = TopicID

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

    Public Shared Function GetTopicList() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Topic_SelectList"
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

    Public Shared Function GetTopicList_ByCategoryID(ByVal CategoryID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Topic_SelectList_ByCategoryID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID

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



    Public Shared Function InsertTopic(ByVal TopicName As String, ByVal CategoryID As Integer, ByVal TopicBody As String, ByVal Status As Boolean, ByVal dateTimeStamp As DateTime) As Integer
        Dim TopicID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Topic_InsertTopic"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@TopicName", SqlDbType.VarChar).Value = TopicName

            If CategoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID
            End If

            sqlComm.Parameters.Add("@TopicBody", SqlDbType.VarChar).Value = TopicBody

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status

            sqlComm.Parameters.Add("@dateTimeStamp", SqlDbType.SmallDateTime).Value = dateTimeStamp

            sqlConn.Open()
            TopicID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return TopicID
    End Function

    Public Shared Function UpdateTopic_ByTopicID(ByVal TopicID As Integer, ByVal TopicName As String, ByVal CategoryID As Integer, ByVal TopicBody As String, ByVal Status As Boolean, ByVal dateTimeStamp As DateTime) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Topic_UpdateTopic"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@TopicID", SqlDbType.Int).Value = TopicID

            sqlComm.Parameters.Add("@TopicName", SqlDbType.VarChar).Value = TopicName

            If CategoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID
            End If

            sqlComm.Parameters.Add("@TopicBody", SqlDbType.VarChar).Value = TopicBody

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status

            sqlComm.Parameters.Add("@dateTimeStamp", SqlDbType.SmallDateTime).Value = dateTimeStamp

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return TopicID
    End Function

    Public Shared Sub DeleteTopic_ByTopicID(ByVal TopicID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Topic_Delete_ByTopicID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@TopicID", SqlDbType.Int).Value = TopicID

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



