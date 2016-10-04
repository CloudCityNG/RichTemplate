Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class EmploymentDAL

#Region "Employment"
    Public Shared Function GetEmployment_ByEmploymentID(ByVal EmploymentID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Employment_Select_ByEmploymentID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmploymentID", SqlDbType.Int).Value = EmploymentID

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

    Public Shared Function GetEmployment_ByEmploymentIDAndSiteID(ByVal EmploymentID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Employment_Select_ByEmploymentIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmploymentID", SqlDbType.Int).Value = EmploymentID
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

    Public Shared Function GetEmployment_ByEmploymentIDAndStatus_FrontEnd(ByVal employmentID As Integer, ByVal status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_Select_ByEmploymentIDAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@employmentID", SqlDbType.Int).Value = employmentID
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

    Public Shared Function GetEmployment_ByEmploymentIDAndStatusAndAccess_FrontEnd(ByVal employmentID As Integer, ByVal status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_Select_ByEmploymentIDAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@employmentID", SqlDbType.Int).Value = employmentID
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

    Public Shared Function GetEmploymentList_ByStatusAndSiteID(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByStatusAndSiteID"
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

    Public Shared Function GetEmployment_ByStatus_FrontEnd(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByStatus_FrontEnd"
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

    Public Shared Function GetEmployment_ByStatusAndAccess_FrontEnd(ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByStatusAndAccess_FrontEnd"
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

    Public Shared Function GetEmployment_ByCategoryNullAndStatus_FrontEnd(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByCategoryIDNULLAndStatus_FrontEnd"
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

    Public Shared Function GetEmployment_ByCategoryNullAndStatusAndAccess_FrontEnd(ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByCategoryIDNULLAndStatusAndAccess_FrontEnd"
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

    Public Shared Function GetEmployment_ByCategoryIDAndStatus_FrontEnd(ByVal categoryID As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByCategoryIDAndStatus_FrontEnd"
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

    Public Shared Function GetEmployment_ByCategoryIDAndStatusAndAccess_FrontEnd(ByVal categoryID As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByCategoryIDAndStatusAndAccess_FrontEnd"
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

    Public Shared Function GetEmployment_ByCategoryNullAndYearAndMonthAndStatus_FrontEnd(ByVal year As Integer, ByVal month As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByCategoryIDNULLAndYearAndMonthAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = year
            sqlComm.Parameters.Add("@month", SqlDbType.Int).Value = month
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

    Public Shared Function GetEmployment_ByCategoryNullAndYearAndMonthAndStatusAndAccess_FrontEnd(ByVal year As Integer, ByVal month As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByCategoryIDNULLAndYearAndMonthAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = year
            sqlComm.Parameters.Add("@month", SqlDbType.Int).Value = month
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

    Public Shared Function GetEmployment_ByCategoryNullAndYearAndStatus_FrontEnd(ByVal year As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByCategoryIDNULLAndYearAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = year
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

    Public Shared Function GetEmployment_ByCategoryNullAndYearAndStatusAndAccess_FrontEnd(ByVal year As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByCategoryIDNULLAndYearAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = year
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

    Public Shared Function GetEmployment_ByCategoryIDAndYearAndMonthAndStatus_FrontEnd(ByVal categoryID As Integer, ByVal year As Integer, ByVal month As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByCategoryIDAndYearAndMonthAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = categoryID
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = year
            sqlComm.Parameters.Add("@month", SqlDbType.Int).Value = month
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

    Public Shared Function GetEmployment_ByCategoryIDAndYearAndMonthAndStatusAndAccess_FrontEnd(ByVal categoryID As Integer, ByVal year As Integer, ByVal month As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByCategoryIDAndYearAndMonthAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = categoryID
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = year
            sqlComm.Parameters.Add("@month", SqlDbType.Int).Value = month
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

    Public Shared Function GetEmployment_ByCategoryIDAndYearAndStatus_FrontEnd(ByVal categoryID As Integer, ByVal year As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByCategoryIDAndYearAndStatus_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = categoryID
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = year
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

    Public Shared Function GetEmployment_ByCategoryIDAndYearAndStatusAndAccess_FrontEnd(ByVal categoryID As Integer, ByVal year As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Employment_SelectList_ByCategoryIDAndYearAndStatusAndAccess_FrontEnd"
            sqlComm.Parameters.Add("@categoryID", SqlDbType.Int).Value = categoryID
            sqlComm.Parameters.Add("@year", SqlDbType.Int).Value = year
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

    Public Shared Function GetEmploymentList_BySearchTagIDAndStatus_FrontEnd(ByVal SearchTagID As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dtEmployment As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Employment_SelectList_BySearchTagIDAndStatus_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SearchTagID", SqlDbType.Int).Value = SearchTagID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID


            Dim da As New SqlDataAdapter(sqlComm)
            sqlConn.Open()
            da.Fill(dtEmployment)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dtEmployment
    End Function

    Public Shared Function GetEmploymentList_BySearchTagIDAndStatusAndAccess_FrontEnd(ByVal SearchTagID As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dtEmployment As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Employment_SelectList_BySearchTagIDAndStatusAndAccess_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SearchTagID", SqlDbType.Int).Value = SearchTagID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            Dim da As New SqlDataAdapter(sqlComm)
            sqlConn.Open()
            da.Fill(dtEmployment)

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return dtEmployment
    End Function

    Public Shared Function GetEmployment_ActiveList_FrontEnd_ByTopN(ByVal TopN As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Employment_SelectListActive_FrontEnd_ByTopN"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@TopN", SqlDbType.Int).Value = TopN
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

    Public Shared Function GetEmployment_ActiveList_FrontEnd_ByAccessAndTopN(ByVal TopN As Integer, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Employment_SelectListActive_FrontEnd_ByAccessAndTopN"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@TopN", SqlDbType.Int).Value = TopN
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

    Public Shared Function InsertEmployment(ByVal SiteID As Integer, ByVal AvailableToAllSites As Boolean, ByVal Title As String, ByVal Summary As String, ByVal Clearance As String, ByVal SalaryMin As Decimal, ByVal SalaryMax As Decimal, ByVal SalaryNote As String, ByVal categoryID As Integer, ByVal Body As String, ByVal ExternalLinkUrl As String, ByVal Status As Boolean, ByVal metaTitle As String, ByVal metaKeywords As String, ByVal metaDescription As String, ByVal metaOther As String, ByVal groupID As String, ByVal userID As String, ByVal searchTagID As String, ByVal dateTimeStamp As String, ByVal Version As Integer, ByVal Address As String, ByVal City As String, ByVal StateID As Integer, ByVal ZipCode As String, ByVal CountryID As Integer, ByVal locationLatitude As String, ByVal locationLongitude As String, ByVal geoLocation As Boolean, ByVal ContactPerson As String, ByVal publicationDate As DateTime, ByVal expirationDate As DateTime, ByVal onlineSignup As Boolean, ByVal authorID_member As Integer, ByVal authorID_admin As Integer, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim employmentID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Employment_InsertEmployment"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AvailableToAllSites", SqlDbType.Bit).Value = AvailableToAllSites

            sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title
            sqlComm.Parameters.Add("@Summary", SqlDbType.NVarChar).Value = Summary

            If Clearance.Length = 0 Then
                sqlComm.Parameters.Add("@Clearance", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Clearance", SqlDbType.NVarChar).Value = Clearance

            End If

            If SalaryMin = Decimal.MinValue Then
                sqlComm.Parameters.Add("@SalaryMin", SqlDbType.Float).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SalaryMin", SqlDbType.Float).Value = SalaryMin
            End If

            If SalaryMax = Decimal.MinValue Then
                sqlComm.Parameters.Add("@SalaryMax", SqlDbType.Float).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SalaryMax", SqlDbType.Float).Value = SalaryMax
            End If

            If SalaryNote.Length = 0 Then
                sqlComm.Parameters.Add("@SalaryNote", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SalaryNote", SqlDbType.NVarChar).Value = SalaryNote
            End If


            If categoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID
            End If

            sqlComm.Parameters.Add("@Body", SqlDbType.NVarChar).Value = Body
            sqlComm.Parameters.Add("@externalLinkUrl", SqlDbType.NVarChar).Value = ExternalLinkUrl
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


            sqlComm.Parameters.Add("@version", SqlDbType.Int).Value = Version
            sqlComm.Parameters.Add("@dateTimeStamp", SqlDbType.SmallDateTime).Value = dateTimeStamp

            If Address.Length = 0 Then
                sqlComm.Parameters.Add("@Address", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Address", SqlDbType.NVarChar).Value = Address
            End If

            If City.Length = 0 Then
                sqlComm.Parameters.Add("@City", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@City", SqlDbType.NVarChar).Value = City
            End If

            If ZipCode.Length = 0 Then
                sqlComm.Parameters.Add("@ZipCode", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ZipCode", SqlDbType.NVarChar).Value = ZipCode
            End If

            If StateID = Integer.MinValue Then
                sqlComm.Parameters.Add("@StateID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID
            End If

            If CountryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CountryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID
            End If

            If locationLatitude.Length = 0 Then
                sqlComm.Parameters.Add("@locationLatitude", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@locationLatitude", SqlDbType.VarChar).Value = locationLatitude
            End If

            If locationLongitude.Length = 0 Then
                sqlComm.Parameters.Add("@locationLongitude", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@locationLongitude", SqlDbType.VarChar).Value = locationLongitude
            End If
            sqlComm.Parameters.Add("@geoLocation", SqlDbType.Bit).Value = geoLocation


            sqlComm.Parameters.Add("@contactPerson", SqlDbType.NVarChar).Value = ContactPerson

            If publicationDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@publicationDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@publicationDate", SqlDbType.SmallDateTime).Value = publicationDate
            End If
            If expirationDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@expirationDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@expirationDate", SqlDbType.SmallDateTime).Value = expirationDate
            End If

            sqlComm.Parameters.Add("@onlineSignup", SqlDbType.Bit).Value = onlineSignup

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
            employmentID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return employmentID
    End Function

    Public Shared Function UpdateEmployment_ByEmploymentID(ByVal employmentID As Integer, ByVal SiteID As Integer, ByVal AvailableToAllSites As Boolean, ByVal Title As String, ByVal Summary As String, ByVal Clearance As String, ByVal SalaryMin As Decimal, ByVal SalaryMax As Decimal, ByVal SalaryNote As String, ByVal categoryID As Integer, ByVal ExternalLinkUrl As String, ByVal Body As String, ByVal Status As Boolean, ByVal metaTitle As String, ByVal metaKeywords As String, ByVal metaDescription As String, ByVal metaOther As String, ByVal groupID As String, ByVal userID As String, ByVal searchTagID As String, ByVal Version As Integer, ByVal Address As String, ByVal City As String, ByVal StateID As Integer, ByVal ZipCode As String, ByVal CountryID As Integer, ByVal locationLatitude As String, ByVal locationLongitude As String, ByVal geoLocation As Boolean, ByVal ContactPerson As String, ByVal publicationDate As DateTime, ByVal expirationDate As DateTime, ByVal onlineSignup As Boolean, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Employment_UpdateEmployment"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmploymentID", SqlDbType.Int).Value = employmentID

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AvailableToAllSites", SqlDbType.Bit).Value = AvailableToAllSites

            sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title
            sqlComm.Parameters.Add("@Summary", SqlDbType.NVarChar).Value = Summary

            If Clearance.Length = 0 Then
                sqlComm.Parameters.Add("@Clearance", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Clearance", SqlDbType.NVarChar).Value = Clearance

            End If

            If SalaryMin = Decimal.MinValue Then
                sqlComm.Parameters.Add("@SalaryMin", SqlDbType.Float).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SalaryMin", SqlDbType.Float).Value = SalaryMin
            End If

            If SalaryMax = Decimal.MinValue Then
                sqlComm.Parameters.Add("@SalaryMax", SqlDbType.Float).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SalaryMax", SqlDbType.Float).Value = SalaryMax
            End If

            If SalaryNote.Length = 0 Then
                sqlComm.Parameters.Add("@SalaryNote", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SalaryNote", SqlDbType.NVarChar).Value = SalaryNote
            End If

            If categoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID
            End If

            sqlComm.Parameters.Add("@ExternalLinkUrl", SqlDbType.NVarChar).Value = ExternalLinkUrl
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


            sqlComm.Parameters.Add("@version", SqlDbType.Int).Value = Version

            If Address.Length = 0 Then
                sqlComm.Parameters.Add("@Address", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Address", SqlDbType.NVarChar).Value = Address
            End If

            If City.Length = 0 Then
                sqlComm.Parameters.Add("@City", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@City", SqlDbType.NVarChar).Value = City
            End If

            If ZipCode.Length = 0 Then
                sqlComm.Parameters.Add("@ZipCode", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ZipCode", SqlDbType.NVarChar).Value = ZipCode
            End If

            If StateID = Integer.MinValue Then
                sqlComm.Parameters.Add("@StateID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID
            End If

            If CountryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CountryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID
            End If

            If locationLatitude.Length = 0 Then
                sqlComm.Parameters.Add("@locationLatitude", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@locationLatitude", SqlDbType.VarChar).Value = locationLatitude
            End If

            If locationLongitude.Length = 0 Then
                sqlComm.Parameters.Add("@locationLongitude", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@locationLongitude", SqlDbType.VarChar).Value = locationLongitude
            End If


            sqlComm.Parameters.Add("@geoLocation", SqlDbType.Bit).Value = geoLocation

            sqlComm.Parameters.Add("@contactPerson", SqlDbType.NVarChar).Value = ContactPerson

            If publicationDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@publicationDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@publicationDate", SqlDbType.SmallDateTime).Value = publicationDate
            End If
            If expirationDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@expirationDate", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@expirationDate", SqlDbType.SmallDateTime).Value = expirationDate
            End If

            sqlComm.Parameters.Add("@onlineSignup", SqlDbType.Bit).Value = onlineSignup

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
        Return employmentID
    End Function

    Public Shared Sub UpdateEmployment_EmploymentImage_ByEmploymentID(ByVal employmentID As Integer, ByVal ThumbnailName As String, ByVal Thumbnail As Byte())

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Employment_UpdateEmployment_EmploymentImage_ByEmploymentID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@employmentID", SqlDbType.Int).Value = employmentID

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

    Public Shared Sub DeleteEmployment_ByEmploymentID(ByVal EmploymentID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Employment_Delete_ByEmploymentID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmploymentID", SqlDbType.Int).Value = EmploymentID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub RollbackEmployment(ByVal archiveID As Integer)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Employment_RollBackEmployment"
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

#Region "Employment Archive"
    Public Shared Function GetEmploymentArchive_ByArchiveIDAndSiteID(ByVal ArchiveID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmploymentArchive_Select_ByArchiveIDAndSiteID"
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

    Public Shared Function GetEmploymentArchiveList_ByEmploymentID(ByVal EmploymentID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmploymentArchive_SelectList_ByEmploymentID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmploymentID", SqlDbType.Int).Value = EmploymentID

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

    Public Shared Sub DeleteEmploymentArchive_ByArchiveID(ByVal ArchiveID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmploymentArchive_Delete_ByArchiveID"
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

    Public Shared Sub DeleteEmploymentArchive_ByEmploymentID(ByVal EmploymentID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmploymentArchive_Delete_ByEmploymentID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmploymentID", SqlDbType.Int).Value = EmploymentID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub
#End Region

#Region "Employment Submission"
    Public Shared Function GetEmploymentSubmissions_BySubIDAndSiteID(ByVal SubID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmploymentSubmission_Select_BySubIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SubID", SqlDbType.Int).Value = SubID
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

    Public Shared Function GetEmploymentSubmissions_ByEmploymentIDAndSiteID(ByVal EmploymentID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmploymentSubmission_SelectList_ByEmploymentIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmploymentID", SqlDbType.Int).Value = EmploymentID
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

    Public Shared Function InsertEmploymentSubmission(ByVal employmentID As Integer, ByVal MemberID As Integer, ByVal SalutationID As Integer, ByVal FirstName As String, ByVal LastName As String, ByVal Email As String, ByVal PhoneNumber As String, ByVal Status As Boolean, ByVal Address As String, ByVal City As String, ByVal StateID As Integer, ByVal ZipCode As String, ByVal CountryID As Integer, ByVal locationLatitude As String, ByVal locationLongitude As String, ByVal CoverLetterText As String, ByVal ResumeText As String, ByVal StartDate As DateTime, ByVal YrsExperience As String, ByVal LastTitle As String, ByVal EduLevel As String, ByVal ProjExpertise As String, ByVal Skills As String, ByVal Salary As String, ByVal IpAddress As String, ByVal DateTimeStamp As DateTime) As Integer

        Dim submissionID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmploymentSubmission_Insert"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmploymentID", SqlDbType.Int).Value = employmentID

            If MemberID = Integer.MinValue Then
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            End If

            If SalutationID = Integer.MinValue Then
                sqlComm.Parameters.Add("@SalutationID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SalutationID", SqlDbType.Int).Value = SalutationID
            End If
            sqlComm.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = FirstName
            sqlComm.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = LastName
            sqlComm.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email

            If Address.Length = 0 Then
                sqlComm.Parameters.Add("@Address", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Address", SqlDbType.NVarChar).Value = Address
            End If

            If City.Length = 0 Then
                sqlComm.Parameters.Add("@City", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@City", SqlDbType.NVarChar).Value = City
            End If

            If ZipCode.Length = 0 Then
                sqlComm.Parameters.Add("@ZipCode", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ZipCode", SqlDbType.NVarChar).Value = ZipCode
            End If

            If StateID = Integer.MinValue Then
                sqlComm.Parameters.Add("@StateID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID
            End If

            If CountryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CountryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID
            End If

            If locationLatitude.Length = 0 Then
                sqlComm.Parameters.Add("@locationLatitude", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@locationLatitude", SqlDbType.VarChar).Value = locationLatitude
            End If

            If locationLongitude.Length = 0 Then
                sqlComm.Parameters.Add("@locationLongitude", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@locationLongitude", SqlDbType.VarChar).Value = locationLongitude
            End If

            sqlComm.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = PhoneNumber
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status

            If CoverLetterText.Length = 0 Then
                sqlComm.Parameters.Add("@CoverLetterText", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CoverLetterText", SqlDbType.NVarChar).Value = CoverLetterText
            End If

            If ResumeText.Length = 0 Then
                sqlComm.Parameters.Add("@ResumeText", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ResumeText", SqlDbType.NVarChar).Value = ResumeText
            End If

            If StartDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = StartDate
            End If

            If YrsExperience.Length = 0 Then
                sqlComm.Parameters.Add("@YrsExperience", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@YrsExperience", SqlDbType.NVarChar).Value = YrsExperience
            End If

            If LastTitle.Length = 0 Then
                sqlComm.Parameters.Add("@LastTitle", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@LastTitle", SqlDbType.NVarChar).Value = LastTitle
            End If

            If EduLevel.Length = 0 Then
                sqlComm.Parameters.Add("@EduLevel", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@EduLevel", SqlDbType.NVarChar).Value = EduLevel
            End If

            If ProjExpertise.Length = 0 Then
                sqlComm.Parameters.Add("@ProjExpertise", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ProjExpertise", SqlDbType.NVarChar).Value = ProjExpertise
            End If

            If Skills.Length = 0 Then
                sqlComm.Parameters.Add("@Skills", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Skills", SqlDbType.NVarChar).Value = Skills
            End If

            If Salary.Length = 0 Then
                sqlComm.Parameters.Add("@Salary", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Salary", SqlDbType.NVarChar).Value = Salary
            End If

            sqlComm.Parameters.Add("@IpAddress", SqlDbType.VarChar).Value = IpAddress
            sqlComm.Parameters.Add("@DateTimeStamp", SqlDbType.SmallDateTime).Value = DateTimeStamp

            sqlConn.Open()
            submissionID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return submissionID
    End Function

    Public Shared Function UpdateEmploymentSubmission(ByVal submissionID As Integer, ByVal MemberID As Integer, ByVal SalutationID As Integer, ByVal FirstName As String, ByVal LastName As String, ByVal Email As String, ByVal PhoneNumber As String, ByVal Status As Boolean, ByVal Address As String, ByVal City As String, ByVal StateID As Integer, ByVal ZipCode As String, ByVal CountryID As Integer, ByVal locationLatitude As String, ByVal locationLongitude As String, ByVal CoverLetterText As String, ByVal ResumeText As String, ByVal StartDate As DateTime, ByVal YrsExperience As String, ByVal LastTitle As String, ByVal EduLevel As String, ByVal ProjExpertise As String, ByVal Skills As String, ByVal Salary As String) As Integer

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmploymentSubmission_Update"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@subID", SqlDbType.Int).Value = submissionID

            If MemberID = Integer.MinValue Then
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            End If

            If SalutationID = Integer.MinValue Then
                sqlComm.Parameters.Add("@SalutationID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SalutationID", SqlDbType.Int).Value = SalutationID
            End If

            sqlComm.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = FirstName
            sqlComm.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = LastName
            sqlComm.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email

            If Address.Length = 0 Then
                sqlComm.Parameters.Add("@Address", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Address", SqlDbType.NVarChar).Value = Address
            End If

            If City.Length = 0 Then
                sqlComm.Parameters.Add("@City", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@City", SqlDbType.NVarChar).Value = City
            End If

            If ZipCode.Length = 0 Then
                sqlComm.Parameters.Add("@ZipCode", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ZipCode", SqlDbType.NVarChar).Value = ZipCode
            End If

            If StateID = Integer.MinValue Then
                sqlComm.Parameters.Add("@StateID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID
            End If

            If CountryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CountryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID
            End If

            If locationLatitude.Length = 0 Then
                sqlComm.Parameters.Add("@locationLatitude", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@locationLatitude", SqlDbType.VarChar).Value = locationLatitude
            End If

            If locationLongitude.Length = 0 Then
                sqlComm.Parameters.Add("@locationLongitude", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@locationLongitude", SqlDbType.VarChar).Value = locationLongitude
            End If

            sqlComm.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = PhoneNumber
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status


            If CoverLetterText.Length = 0 Then
                sqlComm.Parameters.Add("@CoverLetterText", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CoverLetterText", SqlDbType.NVarChar).Value = CoverLetterText
            End If

            If ResumeText.Length = 0 Then
                sqlComm.Parameters.Add("@ResumeText", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ResumeText", SqlDbType.NVarChar).Value = ResumeText
            End If

            If StartDate = DateTime.MinValue Then
                sqlComm.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = StartDate
            End If

            If YrsExperience.Length = 0 Then
                sqlComm.Parameters.Add("@YrsExperience", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@YrsExperience", SqlDbType.NVarChar).Value = YrsExperience
            End If

            If LastTitle.Length = 0 Then
                sqlComm.Parameters.Add("@LastTitle", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@LastTitle", SqlDbType.NVarChar).Value = LastTitle
            End If

            If EduLevel.Length = 0 Then
                sqlComm.Parameters.Add("@EduLevel", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@EduLevel", SqlDbType.NVarChar).Value = EduLevel
            End If

            If ProjExpertise.Length = 0 Then
                sqlComm.Parameters.Add("@ProjExpertise", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ProjExpertise", SqlDbType.NVarChar).Value = ProjExpertise
            End If

            If Skills.Length = 0 Then
                sqlComm.Parameters.Add("@Skills", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Skills", SqlDbType.NVarChar).Value = Skills
            End If

            If Salary.Length = 0 Then
                sqlComm.Parameters.Add("@Salary", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Salary", SqlDbType.NVarChar).Value = Salary
            End If

            sqlConn.Open()
            submissionID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return submissionID
    End Function

    Public Shared Sub UpdateEmploymentSubmission_EmploymentSubmissionDocument1_ByEmploymentSubmissionID(ByVal subID As Integer, ByVal DocumentName1 As String, ByVal DocumentBytes1 As Byte())

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmploymentSubmission_UpdateEmploymentSubmission_EmploymentSubmissionDocument1_ByEmploymentSubmissionID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@subID", SqlDbType.Int).Value = subID

            If DocumentName1.Length = 0 Then
                sqlComm.Parameters.Add("@upload1Name", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@upload1Name", SqlDbType.NVarChar).Value = DocumentName1
            End If

            If DocumentBytes1 Is Nothing Then
                sqlComm.Parameters.Add("@upload1", SqlDbType.VarBinary).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@upload1", SqlDbType.VarBinary).Value = DocumentBytes1
            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateEmploymentSubmission_EmploymentSubmissionDocument2_ByEmploymentSubmissionID(ByVal subID As Integer, ByVal DocumentName2 As String, ByVal DocumentBytes2 As Byte())

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmploymentSubmission_UpdateEmploymentSubmission_EmploymentSubmissionDocument2_ByEmploymentSubmissionID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@subID", SqlDbType.Int).Value = subID

            If DocumentName2.Length = 0 Then
                sqlComm.Parameters.Add("@upload2Name", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@upload2Name", SqlDbType.NVarChar).Value = DocumentName2
            End If

            If DocumentBytes2 Is Nothing Then
                sqlComm.Parameters.Add("@upload2", SqlDbType.VarBinary).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@upload2", SqlDbType.VarBinary).Value = DocumentBytes2
            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateEmploymentSubmission_EmploymentSubmissionDocument3_ByEmploymentSubmissionID(ByVal subID As Integer, ByVal DocumentName3 As String, ByVal DocumentBytes3 As Byte())

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmploymentSubmission_UpdateEmploymentSubmission_EmploymentSubmissionDocument3_ByEmploymentSubmissionID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@subID", SqlDbType.Int).Value = subID

            If DocumentName3.Length = 0 Then
                sqlComm.Parameters.Add("@upload3Name", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@upload3Name", SqlDbType.NVarChar).Value = DocumentName3
            End If

            If DocumentBytes3 Is Nothing Then
                sqlComm.Parameters.Add("@upload3", SqlDbType.VarBinary).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@upload3", SqlDbType.VarBinary).Value = DocumentBytes3
            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub DeleteEmploymentSubmission_BySubID(ByVal SubID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmploymentSubmission_Delete_BySubID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SubID", SqlDbType.Int).Value = SubID

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



