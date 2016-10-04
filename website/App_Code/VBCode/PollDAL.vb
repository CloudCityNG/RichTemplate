Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Principal


Public Class PollDAL

#Region "Poll"
    Public Shared Function GetPoll_ByID(ByVal ID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_Select_ByID"
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

    Public Shared Function GetPoll_ByIDAndSiteID(ByVal ID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_Select_ByIDAndSiteID"
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

    Public Shared Function GetPoll_ByPollIDAndStatus_FrontEnd(ByVal pollID As Integer, ByVal status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Poll_Select_ByPollIDAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = pollID
            sqlComm.Parameters.Add("@status", SqlDbType.Bit).Value = status
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

    Public Shared Function GetPoll_ByPollIDAndStatusAndAccess_FrontEnd(ByVal pollID As Integer, ByVal status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Poll_Select_ByPollIDAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = pollID
            sqlComm.Parameters.Add("@status", SqlDbType.Bit).Value = status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetPoll_ByPollIDAndStatus_FrontEnd_WithMemberSubmission(ByVal pollID As Integer, ByVal status As Boolean, ByVal memberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Poll_Select_ByPollIDAndStatus_FrontEnd_WithMemberSubmission"
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = pollID
            sqlComm.Parameters.Add("@status", SqlDbType.Bit).Value = status
            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = memberID
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

    Public Shared Function GetPoll_ByPollIDAndStatusAndAccess_FrontEnd_WithMemberSubmission(ByVal pollID As Integer, ByVal status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Poll_Select_ByPollIDAndStatusAndAccess_FrontEnd_WithMemberSubmission"
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = pollID
            sqlComm.Parameters.Add("@status", SqlDbType.Bit).Value = status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetPoll_ByStatus_FrontEnd(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Poll_SelectList_ByStatus_FrontEnd"
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

    Public Shared Function GetPoll_ByStatusAndSiteID(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Poll_SelectList_ByStatusAndSiteID"
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

    Public Shared Function GetPoll_ByStatusAndAccess_FrontEnd(ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Poll_SelectList_ByStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetPoll_ByStatus_FrontEnd_WithMemberSubmission(ByVal Status As Boolean, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_ByStatus_FrontEnd_WithMemberSubmission"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
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

    Public Shared Function GetPoll_ByStatusAndAccess_FrontEnd_WithMemberSubmission(ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_ByStatusAndAccess_FrontEnd_WithMemberSubmission"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetPoll_ByCategoryNullAndStatus_FrontEnd(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Poll_SelectList_ByCategoryIDNULLAndStatus_FrontEnd"
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

    Public Shared Function GetPoll_ByCategoryNullAndStatusAndAccess_FrontEnd(ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Poll_SelectList_ByCategoryIDNULLAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetPoll_ByCategoryNullAndStatus_FrontEnd_WithMemberSubmission(ByVal Status As Boolean, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_ByCategoryIDNULLAndStatus_FrontEnd_WithMemberSubmission"

            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
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

    Public Shared Function GetPoll_ByCategoryNullAndStatusAndAccess_FrontEnd_WithMemberSubmission(ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_ByCategoryIDNULLAndStatusAndAccess_FrontEnd_WithMemberSubmission"

            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetPoll_ByCategoryIDAndStatus_FrontEnd(ByVal categoryID As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Poll_SelectList_ByCategoryIDAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = categoryID
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

    Public Shared Function GetPoll_ByCategoryIDAndStatusAndAccess_FrontEnd(ByVal categoryID As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Poll_SelectList_ByCategoryIDAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = categoryID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetPoll_ByCategoryIDAndStatus_FrontEnd_WithMemberSubmission(ByVal CategoryID As Integer, ByVal Status As Boolean, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_ByCategoryIDAndStatus_FrontEnd_WithMemberSubmission"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
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

    Public Shared Function GetPoll_ByCategoryIDAndStatusAndAccess_FrontEnd_WithMemberSubmission(ByVal CategoryID As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_ByCategoryIDAndStatusAndAccess_FrontEnd_WithMemberSubmission"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetPoll_Latest_FrontEnd(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectLatestPoll"
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

    Public Shared Function GetPoll_LatestAndAccess_FrontEnd(ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectLatestPollAndAccess"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetPoll_Latest_FrontEnd_WithMemberSubmission(ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectLatestPoll_WithMemberSubmission"
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

    Public Shared Function GetPoll_LatestAndAccess_FrontEnd_WithMemberSubmission(ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectLatestPollAndAccess_WithMemberSubmission"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetPoll_Random_FrontEnd(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_FrontEnd_Random"
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

    Public Shared Function GetPoll_RandomAndAccess_FrontEnd(ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_FrontEnd_RandomAndAccess"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetPoll_Random_FrontEnd_WithMemberSubmission(ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_FrontEnd_Random_WithMemberSubmission"
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

    Public Shared Function GetPoll_RandomAndAccess_FrontEnd_WithMemberSubmission(ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_FrontEnd_RandomAndAccess_WithMemberSubmission"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
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

    Public Shared Function GetPoll_Random_FrontEnd_ButNotLastPoll(ByVal PollID_Last As Integer, ByVal SiteID As Integer) As DataTable
        Dim dtRandomPoll_ButNotLastPoll As DataTable = GetPoll_Random_FrontEnd(SiteID)
        'Get the row with the pollID = PollID_Last, and put it at the end
        Dim drRandomPoll_Last As DataRow = dtRandomPoll_ButNotLastPoll.NewRow()

        For Each drRandomPoll_ButNotLastPoll As DataRow In dtRandomPoll_ButNotLastPoll.Rows
            Dim intPollID As Integer = drRandomPoll_ButNotLastPoll("ID")
            If intPollID = PollID_Last Then
                drRandomPoll_Last.ItemArray = drRandomPoll_ButNotLastPoll.ItemArray
                dtRandomPoll_ButNotLastPoll.Rows.Remove(drRandomPoll_ButNotLastPoll)
                dtRandomPoll_ButNotLastPoll.Rows.Add(drRandomPoll_Last)
                Exit For
            End If
        Next
        Return dtRandomPoll_ButNotLastPoll

    End Function

    Public Shared Function GetPoll_RandomAndAccess_FrontEnd_ButNotLastPoll(ByVal PollID_Last As Integer, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dtRandomPoll_ButNotLastPoll As DataTable = GetPoll_RandomAndAccess_FrontEnd(GroupIDs, MemberID, SiteID)
        'Get the row with the pollID = PollID_Last, and put it at the end
        Dim drRandomPoll_Last As DataRow = dtRandomPoll_ButNotLastPoll.NewRow()

        For Each drRandomPoll_ButNotLastPoll As DataRow In dtRandomPoll_ButNotLastPoll.Rows
            Dim intPollID As Integer = drRandomPoll_ButNotLastPoll("ID")
            If intPollID = PollID_Last Then
                drRandomPoll_Last.ItemArray = drRandomPoll_ButNotLastPoll.ItemArray
                dtRandomPoll_ButNotLastPoll.Rows.Remove(drRandomPoll_ButNotLastPoll)
                dtRandomPoll_ButNotLastPoll.Rows.Add(drRandomPoll_Last)
                Exit For
            End If
        Next
        Return dtRandomPoll_ButNotLastPoll

    End Function

    Public Shared Function GetPoll_Random_FrontEnd_ButNotLastPoll_WithMemberSubmission(ByVal PollID_Last As Integer, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dtRandomPoll_ButNotLastPoll As DataTable = GetPoll_Random_FrontEnd_WithMemberSubmission(MemberID, SiteID)
        'Get the row with the pollID = PollID_Last, and put it at the end
        Dim drRandomPoll_Last As DataRow = dtRandomPoll_ButNotLastPoll.NewRow()

        For Each drRandomPoll_ButNotLastPoll As DataRow In dtRandomPoll_ButNotLastPoll.Rows
            Dim intPollID As Integer = drRandomPoll_ButNotLastPoll("ID")
            If intPollID = PollID_Last Then
                drRandomPoll_Last.ItemArray = drRandomPoll_ButNotLastPoll.ItemArray
                dtRandomPoll_ButNotLastPoll.Rows.Remove(drRandomPoll_ButNotLastPoll)
                dtRandomPoll_ButNotLastPoll.Rows.Add(drRandomPoll_Last)
                Exit For
            End If
        Next
        Return dtRandomPoll_ButNotLastPoll

    End Function

    Public Shared Function GetPoll_RandomAndAccess_FrontEnd_ButNotLastPoll_WithMemberSubmission(ByVal PollID_Last As Integer, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dtRandomPoll_ButNotLastPoll As DataTable = GetPoll_RandomAndAccess_FrontEnd_WithMemberSubmission(GroupIDs, MemberID, SiteID)
        'Get the row with the pollID = PollID_Last, and put it at the end
        Dim drRandomPoll_Last As DataRow = dtRandomPoll_ButNotLastPoll.NewRow()

        For Each drRandomPoll_ButNotLastPoll As DataRow In dtRandomPoll_ButNotLastPoll.Rows
            Dim intPollID As Integer = drRandomPoll_ButNotLastPoll("ID")
            If intPollID = PollID_Last Then
                drRandomPoll_Last.ItemArray = drRandomPoll_ButNotLastPoll.ItemArray
                dtRandomPoll_ButNotLastPoll.Rows.Remove(drRandomPoll_ButNotLastPoll)
                dtRandomPoll_ButNotLastPoll.Rows.Add(drRandomPoll_Last)
                Exit For
            End If
        Next
        Return dtRandomPoll_ButNotLastPoll

    End Function

    Public Shared Function GetPollList_BySearchTagIDAndStatus_FrontEnd(ByVal SearchTagID As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dtPoll As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_BySearchTagIDAndStatus_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SearchTagID", SqlDbType.Int).Value = SearchTagID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID


            Dim da As New SqlDataAdapter(sqlComm)
            sqlConn.Open()
            da.Fill(dtPoll)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dtPoll
    End Function

    Public Shared Function GetPollList_BySearchTagIDAndStatusAndAccess_FrontEnd(ByVal SearchTagID As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dtPoll As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_BySearchTagIDAndStatusAndAccess_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SearchTagID", SqlDbType.Int).Value = SearchTagID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            Dim da As New SqlDataAdapter(sqlComm)
            sqlConn.Open()
            da.Fill(dtPoll)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dtPoll
    End Function

    Public Shared Function GetPollList_BySearchTagIDAndStatus_FrontEnd_WithMemberSubmission(ByVal SearchTagID As Integer, ByVal Status As Boolean, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dtPoll As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_BySearchTagIDAndStatus_FrontEnd_WithMemberSubmission"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SearchTagID", SqlDbType.Int).Value = SearchTagID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            Dim da As New SqlDataAdapter(sqlComm)
            sqlConn.Open()
            da.Fill(dtPoll)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dtPoll
    End Function

    Public Shared Function GetPollList_BySearchTagIDAndStatusAndAccess_FrontEnd_WithMemberSubmission(ByVal SearchTagID As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dtPoll As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_SelectList_BySearchTagIDAndStatusAndAccess_FrontEnd_WithMemberSubmission"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SearchTagID", SqlDbType.Int).Value = SearchTagID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            Dim da As New SqlDataAdapter(sqlComm)
            sqlConn.Open()
            da.Fill(dtPoll)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dtPoll
    End Function

    Public Shared Function InsertPoll(ByVal SiteID As Integer, ByVal AvailableToAllSites As Boolean, ByVal Question As String, ByVal QuestionHtml As String, ByVal Description As String, ByVal AnswersRandomized As Boolean, ByVal categoryID As Integer, ByVal PublicationDate As DateTime, ByVal ExpirationDate As DateTime, ByVal Status As Boolean, ByVal metaTitle As String, ByVal metaKeywords As String, ByVal metaDescription As String, ByVal metaOther As String, ByVal groupID As String, ByVal userID As String, ByVal searchTagID As String, ByVal Version As Integer, ByVal dateTimeStamp As DateTime, ByVal authorID_member As Integer, ByVal authorID_admin As Integer, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim intPollID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_InsertPoll"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AvailableToAllSites", SqlDbType.Bit).Value = AvailableToAllSites

            sqlComm.Parameters.Add("@Question", SqlDbType.NVarChar).Value = Question
            sqlComm.Parameters.Add("@QuestionHtml", SqlDbType.NVarChar).Value = QuestionHtml

            sqlComm.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description
            sqlComm.Parameters.Add("@AnswersRandomized", SqlDbType.Bit).Value = AnswersRandomized

            If categoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID
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
            intPollID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return intPollID
    End Function

    Public Shared Function UpdatePoll_ByID(ByVal ID As Integer, ByVal SiteID As Integer, ByVal AvailableToAllSites As Boolean, ByVal Question As String, ByVal QuestionHtml As String, ByVal Description As String, ByVal AnswersRandomized As Boolean, ByVal categoryID As Integer, ByVal PublicationDate As DateTime, ByVal ExpirationDate As DateTime, ByVal Status As Boolean, ByVal metaTitle As String, ByVal metaKeywords As String, ByVal metaDescription As String, ByVal metaOther As String, ByVal groupID As String, ByVal userID As String, ByVal searchTagID As String, ByVal Version As Integer, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_UpdatePollByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AvailableToAllSites", SqlDbType.Bit).Value = AvailableToAllSites

            sqlComm.Parameters.Add("@Question", SqlDbType.NVarChar).Value = Question
            sqlComm.Parameters.Add("@QuestionHtml", SqlDbType.NVarChar).Value = QuestionHtml

            sqlComm.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description
            sqlComm.Parameters.Add("@AnswersRandomized", SqlDbType.Bit).Value = AnswersRandomized

            If categoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID
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
        Return ID
    End Function

    Public Shared Sub UpdatePoll_PollImage_ByID(ByVal ID As Integer, ByVal ThumbnailName As String, ByVal Thumbnail As Byte())

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_UpdatePoll_PollImage_ByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID

            If ThumbnailName.Length = 0 Then
                sqlComm.Parameters.Add("@ThumbnailName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ThumbnailName", SqlDbType.NVarChar).Value = ThumbnailName
            End If

            If Thumbnail Is Nothing Then
                sqlComm.Parameters.Add("@Thumbnail", SqlDbType.Binary).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Thumbnail", SqlDbType.Binary).Value = Thumbnail
            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub DeletePoll_ByID(ByVal ID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_DeletePoll_ByID"
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

    Public Shared Sub RollbackPoll(ByVal archiveID As Integer)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Poll_RollBackPoll"
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

#Region "Poll Archive"

    Public Shared Function GetPollArchive_ByArchiveIDAndSiteID(ByVal ArchiveID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollArchive_Select_ByArchiveIDAndSiteID"
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

    Public Shared Function GetPollArchiveList_ByPollID(ByVal PollID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollArchive_SelectList_ByPollID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@PollID", SqlDbType.Int).Value = PollID

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

    Public Shared Sub DeletePollArchive_ByArchiveID(ByVal ArchiveID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollArchive_Delete_ByArchiveID"
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


#End Region

#Region "Poll Answer"

    Public Shared Function GetPollAnswer_ByIDAndSiteID(ByVal ID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswer_Select_ByIDAndSiteID"
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

    Public Shared Function GetPollAnswerList_ByPollID(ByVal PollID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswer_SelectList_ByPollID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@PollID", SqlDbType.Int).Value = PollID

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

    Public Shared Function GetPollAnswerList_ByPollID_FrontEnd(ByVal PollID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswer_SelectList_ByPollID_FrontEnd"
            sqlComm.Connection = sqlConn

            Dim da As New SqlDataAdapter(sqlComm)

            sqlComm.Parameters.Add("@PollID", SqlDbType.Int).Value = PollID

            sqlConn.Open()
            da.Fill(dt)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dt
    End Function


    Public Shared Function GetPollAnswerList_ByPollID_FrontEnd_Random(ByVal PollID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswer_SelectList_ByPollID_FrontEnd_Random"
            sqlComm.Connection = sqlConn

            Dim da As New SqlDataAdapter(sqlComm)

            sqlComm.Parameters.Add("@PollID", SqlDbType.Int).Value = PollID

            sqlConn.Open()
            da.Fill(dt)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dt
    End Function


    Public Shared Function InsertPollAnswer(ByVal PollID As Integer, ByVal Answer As String, ByVal Description As String, ByVal IsCorrect As Boolean, ByVal PublicationDate As DateTime, ByVal ExpirationDate As DateTime, ByVal Status As Boolean, ByVal Version As Integer, ByVal DateCreated As DateTime, ByVal authorID_member As Integer, ByVal authorID_admin As Integer, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim intPollAnswerID As Integer = 0
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswer_InsertPollAnswer"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@PollID", SqlDbType.Int).Value = PollID

            sqlComm.Parameters.Add("@Answer", SqlDbType.NVarChar).Value = Answer
            sqlComm.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description

            sqlComm.Parameters.Add("@IsCorrect", SqlDbType.Bit).Value = IsCorrect

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

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status

            sqlComm.Parameters.Add("@Version", SqlDbType.Int).Value = Version

            sqlComm.Parameters.Add("@DateTimeStamp", SqlDbType.DateTime).Value = DateTime.Now

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
            intPollAnswerID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return intPollAnswerID
    End Function


    Public Shared Function UpdatePollAnswer_ByID(ByVal ID As Integer, ByVal PollID As Integer, ByVal Answer As String, ByVal Description As String, ByVal IsCorrect As Boolean, ByVal PublicationDate As DateTime, ByVal ExpirationDate As DateTime, ByVal Status As Boolean, ByVal Version As Integer, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswer_UpdatePollAnswer_ByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID

            sqlComm.Parameters.Add("@PollID", SqlDbType.Int).Value = PollID

            sqlComm.Parameters.Add("@Answer", SqlDbType.NVarChar).Value = Answer
            sqlComm.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description

            sqlComm.Parameters.Add("@IsCorrect", SqlDbType.Bit).Value = IsCorrect

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

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status

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
        Return ID
    End Function

    Public Shared Sub UpdatePollAnswer_PollAnswerImage_ByID(ByVal ID As Integer, ByVal ThumbnailName As String, ByVal Thumbnail As Byte())

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswer_UpdatePollAnswer_PollAnswerImage_ByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID

            If ThumbnailName.Length = 0 Then
                sqlComm.Parameters.Add("@ThumbnailName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ThumbnailName", SqlDbType.NVarChar).Value = ThumbnailName
            End If

            If Thumbnail Is Nothing Then
                sqlComm.Parameters.Add("@Thumbnail", SqlDbType.Binary).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Thumbnail", SqlDbType.Binary).Value = Thumbnail
            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Function UpdatePollAnswer_SortOrder_ByID(ByVal ID As Integer, ByVal SortOrder_New As Integer) As Integer
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswer_UpdateSortOrder_ByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID

            sqlComm.Parameters.Add("@SortOrder_New", SqlDbType.Int).Value = SortOrder_New

            sqlConn.Open()

            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return ID
    End Function

    Public Shared Sub DeletePollAnswer_ByID(ByVal ID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswer_DeletePollAnswer_ByID"
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

    Public Shared Sub RollbackPollAnswer(ByVal archiveID As Integer)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswer_RollBackPollAnswer"
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

#Region "Poll Answer Archive"

    Public Shared Function GetPollAnswerArchive_ByArchiveIDAndSiteID(ByVal ArchiveID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswerArchive_Select_ByArchiveIDAndSiteID"
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

    Public Shared Function GetPollAnswerArchiveList_ByPollAnswerID(ByVal PollAnswerID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswerArchive_SelectList_ByPollAnswerID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@PollAnswerID", SqlDbType.Int).Value = PollAnswerID

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

    Public Shared Sub DeletePollAnswerArchive_ByArchiveID(ByVal ArchiveID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollAnswerArchive_Delete_ByArchiveID"
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


#End Region

#Region "Poll Answer Submission Rollup Data"
    '    Public Shared Function GetPollSubmissionRollup_ByID(ByVal ID As Integer) As DataTable
    '        Dim dt As New DataTable()
    '        Dim sqlConn As SqlConnection = Nothing
    '        Try
    '            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    '            Dim sqlComm As New SqlCommand()
    '            sqlComm.CommandType = CommandType.StoredProcedure
    '            sqlComm.CommandText = "ss_PollSubmissionRollup_Select_ByID"
    '            sqlComm.Connection = sqlConn

    '            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID

    '            Dim da As New SqlDataAdapter(sqlComm)
    '            sqlConn.Open()
    '            da.Fill(dt)

    '        Finally
    '            If Not sqlConn Is Nothing Then
    '                sqlConn.Close()
    '            End If
    '        End Try
    '        Return dt
    '    End Function

    Public Shared Function GetPollSubmissionRollup_ByPollIDAndSiteID(ByVal PollID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollSubmissionRollup_SelectList_ByPollIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@PollID", SqlDbType.Int).Value = PollID
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

    Public Shared Function GetPollSubmissionRollup_ByPollAnswerID(ByVal PollAnswerID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollSubmissionRollup_SelectList_ByPollAnswerID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@PollAnswerID", SqlDbType.Int).Value = PollAnswerID

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

    Public Shared Function GetPollSubmissionRollup_ByMemberIDAndPollID(ByVal MemberID As Integer, ByVal PollID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollSubmissionRollup_SelectList_ByMemberIDAndPollID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@PollID", SqlDbType.Int).Value = PollID

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

    '    Public Shared Function GetPollSubmissionRollup_ByMemberIDAndPollAnswerID(ByVal MemberID As Integer, ByVal PollAnswerID As Integer) As DataTable
    '        Dim dt As New DataTable()
    '        Dim sqlConn As SqlConnection = Nothing
    '        Try
    '            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    '            Dim sqlComm As New SqlCommand()
    '            sqlComm.CommandType = CommandType.StoredProcedure
    '            sqlComm.CommandText = "ss_PollSubmissionRollup_SelectList_ByMemberIDAndPollAnswerID"
    '            sqlComm.Connection = sqlConn

    '            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
    '            sqlComm.Parameters.Add("@PollAnswerID", SqlDbType.Int).Value = PollAnswerID

    '            Dim da As New SqlDataAdapter(sqlComm)
    '            sqlConn.Open()
    '            da.Fill(dt)

    '        Finally
    '            If Not sqlConn Is Nothing Then
    '                sqlConn.Close()
    '            End If
    '        End Try
    '        Return dt
    '    End Function


    Public Shared Function InsertPollSubmissionRollup(ByVal MemberID As Integer, ByVal PollCategoryID As Integer, ByVal PollID As Integer, ByVal PollQuestion As String, ByVal PollAnswerID As Integer, ByVal PollAnswer As String, ByVal PollAnswerIsCorrect As Boolean, ByVal IpAddress As String, ByVal DateTimeStamp As DateTime) As Integer
        Dim intPollAnswerID As Integer = 0
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollSubmissionRollup_InsertPollSubmissionRollup"
            sqlComm.Connection = sqlConn

            If MemberID = Integer.MinValue Then
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            End If

            If PollCategoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@PollCategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@PollCategoryID", SqlDbType.Int).Value = PollCategoryID
            End If

            sqlComm.Parameters.Add("@PollID", SqlDbType.Int).Value = PollID
            sqlComm.Parameters.Add("@PollQuestion", SqlDbType.NVarChar).Value = PollQuestion

            sqlComm.Parameters.Add("@PollAnswerID", SqlDbType.Int).Value = PollAnswerID
            sqlComm.Parameters.Add("@PollAnswer", SqlDbType.NVarChar).Value = PollAnswer

            sqlComm.Parameters.Add("@PollAnswerIsCorrect", SqlDbType.Bit).Value = PollAnswerIsCorrect

            sqlComm.Parameters.Add("@IpAddress", SqlDbType.NVarChar).Value = IpAddress

            sqlComm.Parameters.Add("@DateTimeStamp", SqlDbType.DateTime).Value = DateTime.Now

            sqlConn.Open()
            intPollAnswerID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return intPollAnswerID
    End Function


    Public Shared Function UpdatePollSubmissionRollup_ByID(ByVal ID As Integer, ByVal MemberID As Integer, ByVal PollCategoryID As Integer, ByVal PollID As Integer, ByVal PollQuestion As String, ByVal PollAnswerID As Integer, ByVal PollAnswer As String, ByVal PollAnswerIsCorrect As Boolean, ByVal IpAddress As String) As Integer
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_PollSubmissionRollup_UpdatePollSubmissionRollup_ByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID

            If MemberID = Integer.MinValue Then
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            End If

            If PollCategoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@PollCategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@PollCategoryID", SqlDbType.Int).Value = PollCategoryID
            End If

            sqlComm.Parameters.Add("@PollID", SqlDbType.Int).Value = PollID
            sqlComm.Parameters.Add("@PollQuestion", SqlDbType.NVarChar).Value = PollQuestion

            sqlComm.Parameters.Add("@PollAnswerID", SqlDbType.Int).Value = PollAnswerID
            sqlComm.Parameters.Add("@PollAnswer", SqlDbType.NVarChar).Value = PollAnswer

            sqlComm.Parameters.Add("@PollAnswerIsCorrect", SqlDbType.Bit).Value = PollAnswerIsCorrect

            sqlComm.Parameters.Add("@IpAddress", SqlDbType.NVarChar).Value = IpAddress

            sqlConn.Open()

            sqlComm.ExecuteScalar()
        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return ID
    End Function

    '    Public Shared Sub DeletePollSubmissionRollup_ByID(ByVal ID As Integer)

    '        Dim sqlConn As SqlConnection = Nothing
    '        Try
    '            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    '            Dim sqlComm As New SqlCommand()
    '            sqlComm.CommandType = CommandType.StoredProcedure
    '            sqlComm.CommandText = "ss_PollSubmissionRollup_Delete_ByID"
    '            sqlComm.Connection = sqlConn

    '            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID

    '            sqlConn.Open()
    '            sqlComm.ExecuteNonQuery()

    '        Finally
    '            If Not sqlConn Is Nothing Then
    '                sqlConn.Close()
    '            End If
    '        End Try
    '    End Sub
#End Region
End Class
