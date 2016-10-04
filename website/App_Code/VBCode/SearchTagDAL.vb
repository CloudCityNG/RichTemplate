Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class SearchTagDAL

#Region "Search Tag"

    Public Shared Function GetSearchTag_BySearchTagNameAndSiteID(ByVal SearchTagName As String, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SearchTag_Select_BySearchTagNameAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SearchTagName", SqlDbType.NVarChar).Value = SearchTagName
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

    Public Shared Function GetSearchTagsList_BySiteID(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SearchTag_SelectList_BySiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetSearchTagsList_BySearchTagIDs(ByVal SearchTagIDList As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SearchTag_SelectList_WithSearchTagIDs"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SearchTagIDList", SqlDbType.NVarChar).Value = SearchTagIDList

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

    Public Shared Sub DeleteSearchTag_BySearchTagID(ByVal SearchTagID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SearchTag_Delete_BySearchTagID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SearchTagID", SqlDbType.Int).Value = SearchTagID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub


#End Region

#Region "Search Tag XRef"

    Public Shared Function GetSearchTagsXRef_List_BySiteID(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SearchTagXRef_Select_List_BySiteID"
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

    Public Shared Function GetSearchTagsXRef_List_WithSearchTagIDs(ByVal SearchTagIDList As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SearchTagXRef_Select_List_WithSearchTagIDs"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SearchTagIDList", SqlDbType.NVarChar).Value = SearchTagIDList

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

    Public Shared Function GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ByVal ModuleTypeID As Integer, ByVal siteID As Integer, ByVal recordID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SearchTagXRef_Select_ByModuleTypeIDAndSiteIDAndRecordID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = siteID
            sqlComm.Parameters.Add("@recordID", SqlDbType.Int).Value = recordID

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

    Public Shared Sub InsertSearchTagXRef(ByVal searchTagID As Integer, ByVal ModuleTypeID As Integer, ByVal RecordID As Integer)

        Dim guidSearchTagXRefID As Guid = Guid.NewGuid

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SearchTagXRef_Insert"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = guidSearchTagXRefID
            sqlComm.Parameters.Add("@searchTagID", SqlDbType.Int).Value = searchTagID
            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
            sqlComm.Parameters.Add("@RecordID", SqlDbType.Int).Value = RecordID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ByVal ModuleTypeID As Integer, ByVal RecordID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SearchTagXRef_Delete_ByModuleTypeIDAndRecordID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
            sqlComm.Parameters.Add("@RecordID", SqlDbType.Int).Value = RecordID


            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub DeleteSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer, ByVal RecordID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SearchTagXRef_Delete_ByModuleTypeIDAndSiteIDAndRecordID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@RecordID", SqlDbType.Int).Value = RecordID


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



