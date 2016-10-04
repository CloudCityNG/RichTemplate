Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Principal


Public Class CommentDAL
#Region "Select"
    Public Shared Function GetComment_ByIDAndSiteID(ByVal ID As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_SelectCommentByID"
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

    Public Shared Function GetComment_ByIDAndSiteID_ForModules(ByVal ID As String, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_SelectCommentByIDAndSiteID_ForModules"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID
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

    Public Shared Function GetComment_ByIDAndSiteID_ForWebInfo(ByVal ID As String, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_SelectCommentByIDAndSiteID_ForWebInfo"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID
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
    Public Shared Function GetCommentList_ByWebInfoIDAndSiteID(ByVal webinfoID As String, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_SelectCommentList_ByWebInfoIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@webinfoID", SqlDbType.Int).Value = webinfoID
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

    Public Shared Function GetCommentList_ByWebInfoID_Front(ByVal webinfoID As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_SelectCommentList_ByWebInfoID_Front"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@webinfoID", SqlDbType.Int).Value = webinfoID

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

    Public Shared Function GetCommentList_ForAllWebInfoPages(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_SelectCommentList_AllWebInfoPages_BySiteID"
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

    Public Shared Function GetCommentList_ByModuleTypeIDAndSiteID(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_SelectCommentList_ByModuleTypeIDAndSiteID"
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

    Public Shared Function GetCommentList_ByModuleTypeIDAndRecordIDAndSiteID(ByVal moduleTypeID As Integer, ByVal recordID As Integer, ByVal SiteID_CurrentRecord As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_SelectCommentList_ByModuleTypeIDAndRecordIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@moduleTypeID", SqlDbType.Int).Value = moduleTypeID
            sqlComm.Parameters.Add("@recordID", SqlDbType.Int).Value = recordID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID_CurrentRecord 'This is the SiteID for this record, NOT the current Site!

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

    Public Shared Function GetCommentList_ByModuleTypeIDAndRecordIDAndSiteID_Front(ByVal moduleTypeID As Integer, ByVal recordID As Integer, ByVal SiteID_CurrentRecord As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_SelectCommentList_ByModuleTypeIDAndRecordIDAndSiteID_Front"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@moduleTypeID", SqlDbType.Int).Value = moduleTypeID
            sqlComm.Parameters.Add("@recordID", SqlDbType.Int).Value = recordID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID_CurrentRecord 'This is the SiteID for this record, NOT the current Site!

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

    Public Shared Function GetCommentList_ForAllModules_BySiteID(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_SelectCommentList_AllModules_BySiteID"
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

#End Region
#Region "Insert"
    Public Shared Function InsertComment(ByVal Comment As String, ByVal Rating As Decimal, ByVal MemberID As Integer, ByVal Active As Boolean) As Integer
        Dim intCommentID As Integer = 0
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_InsertComment"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = Comment
            sqlComm.Parameters.Add("@Rating", SqlDbType.Decimal).Value = Rating
            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@Active", SqlDbType.Bit).Value = Active
            sqlComm.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateTime.Now

            sqlConn.Open()
            intCommentID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return intCommentID
    End Function

    Public Shared Function InsertCommentWebInfo(ByVal commentID As Integer, ByVal webInfoID As Integer) As Integer
        Dim intCommentWebInfoID As Integer = 0
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_InsertCommentWebInfo"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@commentID", SqlDbType.Int).Value = commentID
            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = webInfoID

            sqlConn.Open()
            intCommentWebInfoID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return intCommentWebInfoID

    End Function

    Public Shared Function InsertCommentModule(ByVal commentID As Integer, ByVal ModuleTypeID As Integer, ByVal SiteID As Integer, ByVal RecordID As Integer) As Integer
        Dim intCommentModuleID As Integer = 0
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_InsertCommentModule"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@commentID", SqlDbType.Int).Value = commentID
            sqlComm.Parameters.Add("@ModuleTypeID", SqlDbType.Int).Value = ModuleTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@RecordID", SqlDbType.Int).Value = RecordID

            sqlConn.Open()
            intCommentModuleID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return intCommentModuleID

    End Function
#End Region

#Region "Update"
    Public Shared Sub UpdateComment(ByVal ID As Integer, ByVal Comment As String, ByVal Rating As Decimal, ByVal Active As Boolean)
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_UpdateComment"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID
            sqlComm.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = Comment
            sqlComm.Parameters.Add("@Rating", SqlDbType.Decimal).Value = Rating
            sqlComm.Parameters.Add("@Active", SqlDbType.Bit).Value = Active


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

    Public Shared Sub DeleteComment(ByVal ID As Integer)
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_DeleteComment"
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

    Public Shared Sub DeleteCommentWebInfo(ByVal ID As Integer)
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_DeleteCommentWebInfo"
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

    Public Shared Sub DeleteCommentWebInfo_ByCommentID(ByVal CommentID As Integer)
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_DeleteCommentWebInfo_ByCommentID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@CommentID", SqlDbType.Int).Value = CommentID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub DeleteCommentModule(ByVal ID As Integer)
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_DeleteCommentModule"
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

    Public Shared Sub DeleteCommentModule_ByCommentID(ByVal CommentID As Integer)
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Comment_DeleteCommentModule_ByCommentID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@CommentID", SqlDbType.Int).Value = CommentID

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
