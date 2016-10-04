Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class ModuleDAL

#Region "Module"

    Public Shared Function GetModule_ByModuleTypeIDAndSiteID(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Module_Select_ByModuleTypeIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
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

    Public Shared Function GetModule_ByModuleTypeIDAndSiteID_FrontEnd(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Module_Select_ByModuleTypeIDAndSiteID_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
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

    Public Shared Function GetModuleList_BySiteID(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Module_SelectList_BySiteID"
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

    Public Shared Function GetModuleList_BySiteID_FrontEnd(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Module_SelectList_BySiteID_FrontEnd"
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

    Public Shared Function GetModuleList_ByModuleLocationFrontEndAndSiteID(ByVal ModuleLocationFrontEnd As String, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Module_Select_ByModuleLocationFrontEndAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleLocation_FrontEnd", SqlDbType.NVarChar).Value = ModuleLocationFrontEnd
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

    Public Shared Sub InsertModule(ByVal SiteID As Integer, ByVal ModuleTypeID As Integer, ByVal ModuleContentHTML As String, ByVal Active As Boolean, ByVal OrderIndex As Integer)
        Dim intSiteID As Integer = Integer.MinValue

        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Module_InsertModule"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID

            If ModuleContentHTML = String.Empty Then
                sqlComm.Parameters.Add("@ModuleContentHTML", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ModuleContentHTML", SqlDbType.NVarChar).Value = ModuleContentHTML
            End If

            sqlComm.Parameters.Add("@Active", SqlDbType.Bit).Value = Active

            If OrderIndex = Integer.MinValue Then
                sqlComm.Parameters.Add("@OrderIndex", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@OrderIndex", SqlDbType.Int).Value = OrderIndex
            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try

    End Sub

    Public Shared Sub UpdateModuleContent_ByModuleTypeIDAndSiteID(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer, ByVal ModuleContentHTML As String)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Module_UpdateModuleContent_ByModuleTypeIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            If ModuleContentHTML.Length = 0 Then
                sqlComm.Parameters.Add("@ModuleContentHTML", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ModuleContentHTML", SqlDbType.NVarChar).Value = ModuleContentHTML

            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateModuleActive_ByModuleTypeIDAndSiteID(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer, ByVal Active As Boolean)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Module_UpdateModuleActive_ByModuleTypeIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@Active", SqlDbType.Bit).Value = Active

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateModuleBannerImage_ByModuleTypeIDAndSiteID(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer, ByVal ModuleBannerName As String, ByVal ModuleBannerImage As Byte())
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Module_UpdateModuleBannerImage_ByModuleTypeIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            If ModuleBannerName.Length = 0 Then
                sqlComm.Parameters.Add("@ModuleBannerName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ModuleBannerName", SqlDbType.NVarChar).Value = ModuleBannerName
            End If

            If ModuleBannerImage Is Nothing Then
                sqlComm.Parameters.Add("@ModuleBannerImage", SqlDbType.Binary).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ModuleBannerImage", SqlDbType.Binary).Value = ModuleBannerImage
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

#Region "Module Type"
    Public Shared Function GetModuleTypeList() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ModuleType_SelectList"
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

#Region "ModuleConfig"
    Public Shared Function GetModuleConfigList_ByModuleTypeIDAndSiteID(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ModuleConfig_SelectList_ByModuleTypeIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
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

    Public Shared Sub InsertModuleConfig(ByVal ModuleConfigTypeID As Integer, ByVal SiteID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ModuleConfig_Insert"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleConfigTypeID", SqlDbType.Int).Value = ModuleConfigTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub DeleteModuleConfig_ByModuleTypeIDAndSiteID(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ModuleConfig_Delete_ByModuleTypeIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub
#End Region

#Region "ModuleConfigType"
    Public Shared Function GetModuleConfigTypeList_ByModuleTypeID(ByVal ModuleTypeID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ModuleConfigType_SelectList_ByModuleTypeID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID

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

#Region "Module Access"

    Public Shared Function GetModuleAccessList_ByModuleTypeIDAndSiteIDAndRecordID(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer, ByVal RecordID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ModuleAccess_SelectList_ByModuleTypeIDAndSiteIDAndRecordID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@RecordID", SqlDbType.Int).Value = RecordID

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

    Public Shared Sub InsertModuleAccess(ByVal ModuleTypeID As Integer, ByVal RecordID As Integer, ByVal GroupID As Integer, ByVal MemberID As Integer)

        Dim guidModuleAccessID As Guid = Guid.NewGuid()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ModuleAccess_Insert"
            sqlComm.Connection = sqlConn


            sqlComm.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = guidModuleAccessID
            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID

            sqlComm.Parameters.Add("@RecordID", SqlDbType.Int).Value = RecordID

            If GroupID = Integer.MinValue Then
                sqlComm.Parameters.Add("@GroupID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@GroupID", SqlDbType.Int).Value = GroupID
            End If

            If MemberID = Integer.MinValue Then
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub DeleteModuleAccess_ByModuleTypeIDAndSiteIDAndRecordID(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer, ByVal RecordID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ModuleAccess_Delete_ByModuleTypeIDAndSiteIDAndRecordID"
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
