Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class LinkDAL

#Region "Link"
    Public Shared Function GetLink_ByLinkID(ByVal linkID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Link_Select_ByLinkID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@linkID", SqlDbType.Int).Value = linkID

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

    Public Shared Function GetLink_ByCategoryNullAndStatus_FrontEnd(ByVal status As Boolean) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Link_SelectList_ByCategoryIDNULLAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = status

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

    Public Shared Function GetLink_ByCategoryIDAndStatus_FrontEnd(ByVal categoryID As Integer, ByVal status As Boolean) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Link_SelectList_ByCategoryIDAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = categoryID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = status

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

    Public Shared Function GetLink_Random_FrontEnd() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Link_Select_Random_FrontEnd"
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

    Public Shared Function InsertLink(ByVal LinkName As String, ByVal LinkDescription As String, ByVal LinkURL As String, ByVal LinkImage As Byte(), ByVal categoryID As Integer, ByVal ReleaseDate As DateTime, ByVal ExpirationDate As DateTime, ByVal Status As Boolean, ByVal metaTitle As String, ByVal metaKeywords As String, ByVal metaDescription As String, ByVal metaOther As String, ByVal groupID As String, ByVal userID As String, ByVal searchTagID As String, ByVal Version As Integer, ByVal dateTimeStamp As DateTime, ByVal author As Integer, ByVal modifiedBy As Integer) As Integer
        Dim linkID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Link_InsertLink"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@LinkName", SqlDbType.VarChar).Value = LinkName
            sqlComm.Parameters.Add("@LinkURL", SqlDbType.VarChar).Value = LinkURL
            sqlComm.Parameters.Add("@LinkDescription", SqlDbType.VarChar).Value = LinkDescription

            If LinkImage Is Nothing Then
                sqlComm.Parameters.Add("@LinkImage", SqlDbType.Binary).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@LinkImage", SqlDbType.Binary).Value = LinkImage
            End If

            If categoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID
            End If

            If ReleaseDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@ReleaseDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ReleaseDate", SqlDbType.SmallDateTime).Value = ReleaseDate
            End If

            If ExpirationDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@ExpirationDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ExpirationDate", SqlDbType.SmallDateTime).Value = ExpirationDate
            End If

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@metaTitle", SqlDbType.VarChar).Value = metaTitle
            sqlComm.Parameters.Add("@metaKeywords", SqlDbType.VarChar).Value = metaKeywords
            sqlComm.Parameters.Add("@metaDescription", SqlDbType.VarChar).Value = metaDescription
            sqlComm.Parameters.Add("@metaOther", SqlDbType.VarChar).Value = metaOther

            If groupID.Length = 0 Then
                sqlComm.Parameters.Add("@groupID", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@groupID", SqlDbType.VarChar).Value = groupID
            End If

            If userID.Length = 0 Then
                sqlComm.Parameters.Add("@userID", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@userID", SqlDbType.VarChar).Value = userID
            End If

            If searchTagID.Length = 0 Then
                sqlComm.Parameters.Add("@searchTagID", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@searchTagID", SqlDbType.VarChar).Value = searchTagID
            End If

            sqlComm.Parameters.Add("@Version", SqlDbType.Int).Value = Version
            sqlComm.Parameters.Add("@dateTimeStamp", SqlDbType.SmallDateTime).Value = dateTimeStamp

            sqlComm.Parameters.Add("@author", SqlDbType.Int).Value = author

            If modifiedBy = Integer.MinValue Then
                sqlComm.Parameters.Add("@modifiedBy", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@modifiedBy", SqlDbType.Int).Value = modifiedBy
            End If


            sqlConn.Open()
            linkID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return linkID
    End Function

    Public Shared Function UpdateLink_ByLinkID(ByVal linkID As Integer, ByVal LinkName As String, ByVal LinkDescription As String, ByVal LinkURL As String, ByVal LinkImage As Byte(), ByVal categoryID As Integer, ByVal ReleaseDate As DateTime, ByVal ExpirationDate As DateTime, ByVal Status As Boolean, ByVal metaTitle As String, ByVal metaKeywords As String, ByVal metaDescription As String, ByVal metaOther As String, ByVal groupID As String, ByVal userID As String, ByVal searchTagID As String, ByVal Version As Integer, ByVal dateTimeStamp As DateTime, ByVal author As Integer, ByVal modifiedBy As Integer) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Link_UpdateLink"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@linkID", SqlDbType.Int).Value = linkID

            sqlComm.Parameters.Add("@LinkName", SqlDbType.VarChar).Value = LinkName
            sqlComm.Parameters.Add("@LinkURL", SqlDbType.VarChar).Value = LinkURL
            sqlComm.Parameters.Add("@LinkDescription", SqlDbType.VarChar).Value = LinkDescription

            If LinkImage Is Nothing Then
                sqlComm.Parameters.Add("@LinkImage", SqlDbType.Binary).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@LinkImage", SqlDbType.Binary).Value = LinkImage
            End If

            If categoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID
            End If

            If ReleaseDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@ReleaseDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ReleaseDate", SqlDbType.SmallDateTime).Value = ReleaseDate
            End If

            If ExpirationDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@ExpirationDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ExpirationDate", SqlDbType.SmallDateTime).Value = ExpirationDate
            End If

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@metaTitle", SqlDbType.VarChar).Value = metaTitle
            sqlComm.Parameters.Add("@metaKeywords", SqlDbType.VarChar).Value = metaKeywords
            sqlComm.Parameters.Add("@metaDescription", SqlDbType.VarChar).Value = metaDescription
            sqlComm.Parameters.Add("@metaOther", SqlDbType.VarChar).Value = metaOther

            If groupID.Length = 0 Then
                sqlComm.Parameters.Add("@groupID", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@groupID", SqlDbType.VarChar).Value = groupID
            End If

            If userID.Length = 0 Then
                sqlComm.Parameters.Add("@userID", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@userID", SqlDbType.VarChar).Value = userID
            End If

            If searchTagID.Length = 0 Then
                sqlComm.Parameters.Add("@searchTagID", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@searchTagID", SqlDbType.VarChar).Value = searchTagID
            End If

            sqlComm.Parameters.Add("@Version", SqlDbType.Int).Value = Version
            sqlComm.Parameters.Add("@dateTimeStamp", SqlDbType.SmallDateTime).Value = dateTimeStamp

            sqlComm.Parameters.Add("@author", SqlDbType.Int).Value = author

            If modifiedBy = Integer.MinValue Then
                sqlComm.Parameters.Add("@modifiedBy", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@modifiedBy", SqlDbType.Int).Value = modifiedBy
            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return linkID
    End Function

    Public Shared Sub DeleteLink_ByLinkID(ByVal LinkID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Link_Delete_ByLinkID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@LinkID", SqlDbType.Int).Value = LinkID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub RollbackLink(ByVal archiveID As Integer)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Link_RollBackLink"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ArchiveID", SqlDbType.Int).Value = archiveID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub
#End Region

#Region "Link Archive"
    Public Shared Function GetLinkArchive_ByArchiveID(ByVal ArchiveID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_LinkArchive_Select_ByArchiveID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ArchiveID", SqlDbType.Int).Value = ArchiveID

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

    Public Shared Sub DeleteLinkArchive_ByArchiveID(ByVal ArchiveID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_LinkArchive_Delete_ByArchiveID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ArchiveID", SqlDbType.Int).Value = ArchiveID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub DeleteLinkArchive_ByLinkID(ByVal LinkID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_LinkArchive_Delete_ByLinkID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@LinkID", SqlDbType.Int).Value = LinkID

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



