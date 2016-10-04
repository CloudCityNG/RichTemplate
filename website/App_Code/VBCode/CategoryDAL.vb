Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Principal


Public Class CategoryDAL

    Public Shared Function GetCategory_ByCategoryID(ByVal CategoryID As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Category_Select_ByCategoryID"
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

    Public Shared Function GetCategory_ByCategoryIDAndModuleTypeIDAndSiteID(ByVal CategoryID As Integer, ByVal ModuleTypeID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Category_Select_ByCategoryIDAndModuleTypeIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID
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

    Public Shared Function GetCategoryList_ByModuleTypeIDAndSiteID(ByVal ModuleTypeID As String, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Category_SelectList_ByModuleTypeIDAndSiteID"
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

    Public Shared Sub UpdateCategoryOrderIndex(ByVal categoryID_DraggedRow As Integer, ByVal categoryOrderIndex_New As Integer, ByVal moduleTypeID As Integer, ByVal SiteID As Integer)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Category_UpdateCategoryOrderIndex"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID_DraggedRow
            sqlComm.Parameters.Add("@OrderIndex_New", SqlDbType.Int).Value = categoryOrderIndex_New
            sqlComm.Parameters.Add("@moduleTypeID", SqlDbType.Int).Value = moduleTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub


    Public Shared Sub DeleteCategory_ByCategoryID(ByVal CategoryID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Category_Delete_ByCategoryID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub


#Region "Get Categories with module specific count"
    'Note each module that uses this must have a status, publicationDate and Release, otherwise you will need to use this function as a guide to customize
    Public Shared Function GetCategoryList_WithCount_ByModuleTypeIDAndStatus(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer, ByVal Status As Boolean, ByVal StartDate_FieldName As String, ByVal EndDate_FieldName As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Category_SelectList_WithCount_ByModuleTypeIDAndSiteIDAndStatus"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@StartDate_FieldName", SqlDbType.NVarChar).Value = StartDate_FieldName
            sqlComm.Parameters.Add("@EndDate_FieldName", SqlDbType.NVarChar).Value = EndDate_FieldName

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
    'Note each module that uses this must have a status, publicationDate and Release, otherwise you will need to use this function as a guide to customize
    Public Shared Function GetCategoryList_WithCount_ByModuleTypeIDAndStatusAndAccess(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer, ByVal Status As Boolean, ByVal PrimaryKey_FieldName As String, ByVal StartDate_FieldName As String, ByVal EndDate_FieldName As String, ByVal listGroupIDs As String, ByVal memberID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Category_SelectList_WithCount_ByModuleTypeIDAndSiteIDAndStatusAndAccess"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@PrimaryKey_FieldName", SqlDbType.NVarChar).Value = PrimaryKey_FieldName
            sqlComm.Parameters.Add("@StartDate_FieldName", SqlDbType.NVarChar).Value = StartDate_FieldName
            sqlComm.Parameters.Add("@EndDate_FieldName", SqlDbType.NVarChar).Value = EndDate_FieldName
            sqlComm.Parameters.Add("@GroupIds", SqlDbType.NVarChar).Value = listGroupIDs
            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = memberID


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
End Class
