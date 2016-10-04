Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class GroupDAL
#Region "Select"
    Public Shared Function GetGroup_ByGroupIDAndSiteID(ByVal groupID As String, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Group_SelectGroupByGroupIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@GroupID", SqlDbType.Int).Value = groupID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetGroupList_BySiteID(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Group_SelectGroupList_BySiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetGroupList_ByMemberIDAndSiteID(ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Group_SelectGroupListByMemberIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetGroupList_ByPageID(ByVal pageID As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Group_SelectGroupListByPageID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@pageID", SqlDbType.Int).Value = pageID

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

#Region "Add"
    Public Shared Function InsertGroup(ByVal GroupName As String, ByVal GroupDescription As String, ByVal SiteID As Integer, ByVal AvailableToAllSites As Boolean, ByVal GroupPassword As String, ByVal ExpirationDate As DateTime, ByVal GroupActive As Boolean) As Integer

        Dim groupID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Group_InsertGroup"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@GroupName", SqlDbType.NVarChar).Value = GroupName
            sqlComm.Parameters.Add("@GroupDescription", SqlDbType.NVarChar).Value = GroupDescription
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AvailableToAllSites", SqlDbType.Bit).Value = AvailableToAllSites
            sqlComm.Parameters.Add("@GroupPassword", SqlDbType.NVarChar).Value = GroupPassword

            If ExpirationDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@ExpirationDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ExpirationDate", SqlDbType.SmallDateTime).Value = ExpirationDate
            End If

            sqlComm.Parameters.Add("@GroupActive", SqlDbType.Bit).Value = GroupActive

            sqlConn.Open()
            groupID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return groupID
    End Function


#End Region

#Region "Update"
    Public Shared Function UpdateGroup_ByGroupID(ByVal GroupID As Integer, ByVal GroupName As String, ByVal GroupDescription As String, ByVal SiteID As Integer, ByVal AvailableToAllSites As Boolean, ByVal GroupPassword As String, ByVal ExpirationDate As DateTime, ByVal GroupActive As Boolean) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Group_UpdateGroup"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@GroupID", SqlDbType.Int).Value = GroupID
            sqlComm.Parameters.Add("@GroupName", SqlDbType.NVarChar).Value = GroupName
            sqlComm.Parameters.Add("@GroupDescription", SqlDbType.NVarChar).Value = GroupDescription
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AvailableToAllSites", SqlDbType.Bit).Value = AvailableToAllSites
            sqlComm.Parameters.Add("@GroupPassword", SqlDbType.NVarChar).Value = GroupPassword

            If ExpirationDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@ExpirationDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ExpirationDate", SqlDbType.SmallDateTime).Value = ExpirationDate
            End If

            sqlComm.Parameters.Add("@GroupActive", SqlDbType.Bit).Value = GroupActive

            sqlConn.Open()
            GroupID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return GroupID
    End Function

#End Region

#Region "Delete"
    Public Shared Sub DeleteGroup_ByGroupID(ByVal GroupID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Group_Delete_ByGroupID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@GroupID", SqlDbType.Int).Value = GroupID

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
