Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class BlogDAL

#Region "Blog"
    Public Shared Function GetBlog_ByBlogID(ByVal BlogID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Blog_Select_ByBlogID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@blogID", SqlDbType.Int).Value = BlogID

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

    Public Shared Function GetBlog_ByBlogIDAndSiteID(ByVal BlogID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Blog_Select_ByBlogIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@blogID", SqlDbType.Int).Value = BlogID
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = siteID

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

    Public Shared Function GetBlog_ByBlogIDAndStatus_FrontEnd(ByVal BlogID As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_Select_ByBlogIDAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@blogID", SqlDbType.Int).Value = BlogID
            sqlComm.Parameters.Add("@status", SqlDbType.Bit).Value = Status
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

    Public Shared Function GetBlog_ByBlogIDAndStatusAndAccess_FrontEnd(ByVal BlogID As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_Select_ByBlogIDAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@blogID", SqlDbType.Int).Value = BlogID
            sqlComm.Parameters.Add("@status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetBlogList_ByStatusAndSiteID(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByStatusAndSiteID"
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
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


    Public Shared Function GetBlog_ByStatus_FrontEnd(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByStatus_FrontEnd"
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
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

    Public Shared Function GetBlog_ByStatusAndAccess_FrontEnd(ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetBlogList_BySearchTagIDAndStatus_FrontEnd(ByVal SearchTagID As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dtBlogs As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Blog_SelectList_BySearchTagIDAndStatus_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SearchTagID", SqlDbType.Int).Value = SearchTagID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

            Dim da As New SqlDataAdapter(sqlComm)
            sqlConn.Open()
            da.Fill(dtBlogs)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dtBlogs
    End Function

    Public Shared Function GetBlogList_BySearchTagIDAndStatusAndAccess_FrontEnd(ByVal SearchTagID As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dtBlogs As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Blog_SelectList_BySearchTagIDAndStatusAndAccess_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SearchTagID", SqlDbType.Int).Value = SearchTagID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

            Dim da As New SqlDataAdapter(sqlComm)
            sqlConn.Open()
            da.Fill(dtBlogs)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dtBlogs
    End Function

    Public Shared Function GetBlog_ByCategoryNullAndStatus_FrontEnd(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByCategoryIDNULLAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
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

    Public Shared Function GetBlog_ByCategoryNullAndStatusAndAccess_FrontEnd(ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByCategoryIDNULLAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetBlog_ByCategoryNullAndYearAndStatus_FrontEnd(ByVal Year As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByCategoryIDNULLAndYearAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = Year
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
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

    Public Shared Function GetBlog_ByCategoryNullAndYearAndStatusAndAccess_FrontEnd(ByVal Year As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByCategoryIDNULLAndYearAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = Year
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetBlog_ByCategoryNullAndYearAndMonthAndStatus_FrontEnd(ByVal Year As Integer, ByVal Month As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByCategoryIDNULLAndYearAndMonthAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = Year
            sqlComm.Parameters.Add("@month", SqlDbType.Int).Value = Month
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
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

    Public Shared Function GetBlog_ByCategoryNullAndYearAndMonthAndStatusAndAccess_FrontEnd(ByVal Year As Integer, ByVal Month As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByCategoryIDNULLAndYearAndMonthAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = Year
            sqlComm.Parameters.Add("@month", SqlDbType.Int).Value = Month
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetBlog_ByCategoryIDAndStatus_FrontEnd(ByVal CategoryID As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByCategoryIDAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = CategoryID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
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

    Public Shared Function GetBlog_ByCategoryIDAndStatusAndAccess_FrontEnd(ByVal CategoryID As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByCategoryIDAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = CategoryID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetBlog_ByCategoryIDAndYearAndStatus_FrontEnd(ByVal CategoryID As Integer, ByVal Year As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByCategoryIDAndYearAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = CategoryID
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = Year
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
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

    Public Shared Function GetBlog_ByCategoryIDAndYearAndStatusAndAccess_FrontEnd(ByVal CategoryID As Integer, ByVal Year As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByCategoryIDAndYearAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = CategoryID
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = Year
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetBlog_ByCategoryIDAndYearAndMonthAndStatus_FrontEnd(ByVal CategoryID As Integer, ByVal Year As Integer, ByVal Month As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByCategoryIDAndYearAndMonthAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = CategoryID
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = Year
            sqlComm.Parameters.Add("@month", SqlDbType.Int).Value = Month
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
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

    Public Shared Function GetBlog_ByCategoryIDAndYearAndMonthAndStatusAndAccess_FrontEnd(ByVal CategoryID As Integer, ByVal Year As Integer, ByVal Month As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Blog_SelectList_ByCategoryIDAndYearAndMonthAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = CategoryID
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = Year
            sqlComm.Parameters.Add("@month", SqlDbType.Int).Value = Month
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetBlog_ActiveList_FrontEnd_ByTopN(ByVal TopN As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Blog_SelectListActive_FrontEnd_ByTopN"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@TopN", SqlDbType.Int).Value = TopN
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

    Public Shared Function GetBlog_ActiveList_FrontEnd_ByAccessAndTopN(ByVal TopN As Integer, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Blog_SelectListActive_FrontEnd_ByAccessAndTopN"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@TopN", SqlDbType.Int).Value = TopN
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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


    Public Shared Function InsertBlog(ByVal SiteID As Integer, ByVal AvailableToAllSites As Boolean, ByVal Title As String, ByVal CategoryID As Integer, ByVal PublicationDate As DateTime, ByVal ExpirationDate As DateTime, ByVal Body As String, ByVal Status As Boolean, ByVal metaTitle As String, ByVal metaKeywords As String, ByVal metaDescription As String, ByVal metaOther As String, ByVal groupID As String, ByVal userID As String, ByVal searchTagID As String, ByVal Version As Integer, ByVal dateTimeStamp As DateTime, ByVal authorID_member As Integer, ByVal authorID_admin As Integer, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim blogID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Blog_InsertBlog"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AvailableToAllSites", SqlDbType.Bit).Value = AvailableToAllSites

            sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title

            If categoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID
            End If

            If PublicationDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@PublicationDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@PublicationDate", SqlDbType.SmallDateTime).Value = PublicationDate
            End If

            If ExpirationDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@ExpirationDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ExpirationDate", SqlDbType.SmallDateTime).Value = ExpirationDate
            End If

            sqlComm.Parameters.Add("@Body", SqlDbType.NVarChar).Value = Body
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status

            sqlComm.Parameters.Add("@metaTitle", SqlDbType.NVarChar).Value = metaTitle
            sqlComm.Parameters.Add("@metaKeywords", SqlDbType.NVarChar).Value = metaKeywords
            sqlComm.Parameters.Add("@metaDescription", SqlDbType.NVarChar).Value = metaDescription
            sqlComm.Parameters.Add("@metaOther", SqlDbType.NVarChar).Value = metaOther

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

            If authorID_member = Integer.MinValue Then
                sqlComm.Parameters.Add("@authorID_member", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@authorID_member", SqlDbType.Int).Value = authorID_member
            End If

            If authorID_admin = Integer.MinValue Then
                sqlComm.Parameters.Add("@authorID_admin", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@authorID_admin", SqlDbType.Int).Value = authorID_admin
            End If

            If modifiedID_member = Integer.MinValue Then
                sqlComm.Parameters.Add("@modifiedID_member", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@modifiedID_member", SqlDbType.Int).Value = modifiedID_member
            End If

            If modifiedID_admin = Integer.MinValue Then
                sqlComm.Parameters.Add("@modifiedID_admin", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@modifiedID_admin", SqlDbType.Int).Value = modifiedID_admin
            End If

            sqlConn.Open()
            blogID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return blogID
    End Function

    Public Shared Function UpdateBlog_ByBlogID(ByVal BlogID As Integer, ByVal SiteID As Integer, ByVal AvailableToAllSites As Boolean, ByVal Title As String, ByVal CategoryID As Integer, ByVal PublicationDate As DateTime, ByVal ExpirationDate As DateTime, ByVal Body As String, ByVal Status As Boolean, ByVal metaTitle As String, ByVal metaKeywords As String, ByVal metaDescription As String, ByVal metaOther As String, ByVal groupID As String, ByVal userID As String, ByVal searchTagID As String, ByVal Version As Integer, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Blog_UpdateBlog"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@blogID", SqlDbType.Int).Value = BlogID

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AvailableToAllSites", SqlDbType.Bit).Value = AvailableToAllSites

            sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title

            If categoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID
            End If

            If PublicationDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@PublicationDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@PublicationDate", SqlDbType.SmallDateTime).Value = PublicationDate
            End If

            If ExpirationDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@ExpirationDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ExpirationDate", SqlDbType.SmallDateTime).Value = ExpirationDate
            End If

            sqlComm.Parameters.Add("@Body", SqlDbType.NVarChar).Value = Body
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@metaTitle", SqlDbType.NVarChar).Value = metaTitle
            sqlComm.Parameters.Add("@metaKeywords", SqlDbType.NVarChar).Value = metaKeywords
            sqlComm.Parameters.Add("@metaDescription", SqlDbType.NVarChar).Value = metaDescription
            sqlComm.Parameters.Add("@metaOther", SqlDbType.NVarChar).Value = metaOther

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

            If modifiedID_member = Integer.MinValue Then
                sqlComm.Parameters.Add("@modifiedID_member", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@modifiedID_member", SqlDbType.Int).Value = modifiedID_member
            End If

            If modifiedID_admin = Integer.MinValue Then
                sqlComm.Parameters.Add("@modifiedID_admin", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@modifiedID_admin", SqlDbType.Int).Value = modifiedID_admin
            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return blogID
    End Function

    Public Shared Function UpdateBlog_ByBlogID_FrontEnd(ByVal BlogID As Integer, ByVal Title As String, ByVal CategoryID As Integer, ByVal Body As String, ByVal Version As Integer, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Blog_UpdateBlog_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@blogID", SqlDbType.Int).Value = BlogID
            sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title
            If categoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID
            End If

            sqlComm.Parameters.Add("@Body", SqlDbType.NVarChar).Value = Body

            sqlComm.Parameters.Add("@version", SqlDbType.Int).Value = Version

            If modifiedID_member = Integer.MinValue Then
                sqlComm.Parameters.Add("@modifiedID_member", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@modifiedID_member", SqlDbType.Int).Value = modifiedID_member
            End If

            If modifiedID_admin = Integer.MinValue Then
                sqlComm.Parameters.Add("@modifiedID_admin", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@modifiedID_admin", SqlDbType.Int).Value = modifiedID_admin
            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return blogID
    End Function

    Public Shared Sub DeleteBlog_ByBlogID(ByVal BlogID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Blog_Delete_ByBlogID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@BlogID", SqlDbType.Int).Value = BlogID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub RollbackBlog(ByVal archiveID As Integer)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Blog_RollBackBlog"
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

#Region "Blog Archive"
    Public Shared Function GetBlogArchive_ByArchiveIDAndSiteID(ByVal ArchiveID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_BlogArchive_Select_ByArchiveIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ArchiveID", SqlDbType.Int).Value = ArchiveID
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

    Public Shared Function GetBlogArchiveList_ByBlogID(ByVal BlogID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_BlogArchive_SelectList_ByBlogID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@blogID", SqlDbType.Int).Value = BlogID

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

    Public Shared Sub DeleteBlogArchive_ByArchiveID(ByVal ArchiveID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_BlogArchive_Delete_ByArchiveID"
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

    Public Shared Sub DeleteBlogArchive_ByBlogID(ByVal BlogID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_BlogArchive_Delete_ByBlogID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@BlogID", SqlDbType.Int).Value = BlogID

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



