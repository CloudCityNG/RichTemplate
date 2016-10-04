Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data
Imports System.Security.Principal

Public Class RichTemplateCentralDAL

#Region "Bug Submission"
    Public Shared Function GetBugSubmissionList_ByApplicationID(ByVal applicationID As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString_RichTemplateCentral").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "BugSubmission_SelectList_ByApplicationID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationID

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

   

    Public Shared Function BugSubmission_Insert(ByVal ApplicationID As Integer, ByVal BugStatusID As Integer, ByVal UserID As Integer, ByVal Message As String, ByVal ContactEmail As String, ByVal ContactPhone As String, ByVal IpAddress As String, ByVal UserAgent As String) As Integer
        Dim intBugSubmissionID As Integer = 0
        Dim sqlErrorConn As SqlConnection = Nothing
        Try
            sqlErrorConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString_RichTemplateCentral").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "BugSubmission_InsertBugSubmission"
            sqlComm.Connection = sqlErrorConn

            'Add parameters
            sqlComm.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID
            sqlComm.Parameters.Add("@BugStatusID", SqlDbType.Int).Value = BugStatusID
            sqlComm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
            sqlComm.Parameters.Add("@Message", SqlDbType.NVarChar).Value = Message

            If ContactEmail.Length = 0 Then
                sqlComm.Parameters.Add("@ContactEmail", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ContactEmail", SqlDbType.NVarChar).Value = ContactEmail
            End If

            If ContactPhone.Length = 0 Then
                sqlComm.Parameters.Add("@ContactPhone", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ContactPhone", SqlDbType.NVarChar).Value = ContactPhone
            End If

            sqlComm.Parameters.Add("@IpAddress", SqlDbType.NVarChar).Value = IpAddress
            sqlComm.Parameters.Add("@UserAgent", SqlDbType.NVarChar).Value = UserAgent
            sqlComm.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateTime.Now

            sqlErrorConn.Open()
            intBugSubmissionID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlErrorConn Is Nothing Then
                sqlErrorConn.Close()
            End If
        End Try
        Return intBugSubmissionID
    End Function

    Public Shared Sub BugSubmission_Update(ByVal intBugSubmissionID As Integer, ByVal ApplicationID As Integer, ByVal BugStatusID As Integer, ByVal UserID As Integer, ByVal Message As String, ByVal ContactEmail As String, ByVal ContactPhone As String, ByVal IpAddress As String, ByVal UserAgent As String)
        Dim sqlErrorConn As SqlConnection = Nothing
        Try
            sqlErrorConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString_RichTemplateCentral").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "BugSubmission_UpdateBugSubmission"
            sqlComm.Connection = sqlErrorConn

            'Add parameters
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = intBugSubmissionID
            sqlComm.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID
            sqlComm.Parameters.Add("@BugStatusID", SqlDbType.Int).Value = BugStatusID
            sqlComm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
            sqlComm.Parameters.Add("@Message", SqlDbType.NVarChar).Value = Message

            If ContactEmail.Length = 0 Then
                sqlComm.Parameters.Add("@ContactEmail", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ContactEmail", SqlDbType.NVarChar).Value = ContactEmail
            End If

            If ContactPhone.Length = 0 Then
                sqlComm.Parameters.Add("@ContactPhone", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ContactPhone", SqlDbType.NVarChar).Value = ContactPhone
            End If

            sqlComm.Parameters.Add("@IpAddress", SqlDbType.NVarChar).Value = IpAddress
            sqlComm.Parameters.Add("@UserAgent", SqlDbType.NVarChar).Value = UserAgent
            sqlComm.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateTime.Now

            sqlErrorConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlErrorConn Is Nothing Then
                sqlErrorConn.Close()
            End If
        End Try
    End Sub


    Public Shared Function Bug_LogBug(ByVal Message As String, ByVal ContactEmail As String, ByVal ContactPhone As String) As Integer

        Dim intBugSubmissionID As Integer = 0
        'Check if the applicationID Exists
        If Not ConfigurationManager.AppSettings("ApplicationID") Is Nothing Then

            'Get ApplicationID and the inital Bug Status
            Dim intApplicationID As Integer = ConfigurationManager.AppSettings("ApplicationID")
            Dim intBugStatus As Integer = 1

            'Get User
            Dim intUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
            If intUserID = 0 Then
                'If the current user is not an admin, then check if the current user is a member
                intUserID = MemberDAL.GetCurrentMemberID()
            End If

            'Use only if using FormsAuthentication
            'Dim user As IPrincipal = HttpContext.Current.User
            'If Not user Is Nothing And user.Identity.IsAuthenticated Then
            '    intUserID = Convert.ToInt32(user.Identity.Name)
            'End If

            'Get Form Data
            Dim strMessage As String = Message
            Dim strContactEmail As String = ContactEmail
            Dim strContactPhone As String = ContactPhone


            'Get UserHostAddress and Browser
            Dim strIpAddress As String = HttpContext.Current.Request.UserHostAddress
            Dim strUserAgent As String = HttpContext.Current.Request.UserAgent

            intBugSubmissionID = BugSubmission_Insert(intApplicationID, intBugStatus, intUserID, strMessage, strContactEmail, strContactPhone, strIpAddress, strUserAgent)

        End If

        Return intBugSubmissionID

    End Function

#End Region

#Region "Bug Submission Status"
    Public Shared Function GetBugStatusList() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString_RichTemplateCentral").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "BugStatus_SelectList"
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

#Region "Error Log"
    Public Shared Function GetErrorList_ByApplicationID(ByVal applicationID As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString_RichTemplateCentral").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "Error_SelectList_ByApplicationID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationID

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

    Public Shared Function GetErrorList_ByDate(ByVal dtDate As DateTime) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString_RichTemplateCentral").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "Error_SelectList_ByDate"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@Date", SqlDbType.DateTime).Value = dtDate

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

    'Note if error handling is not setup correctly, then no errors will be logged.
    'This should be checked when you deploy the project via default.aspx?error=1
    Public Shared Sub Error_Insert(ByVal ApplicationID As Integer, ByVal UserID As Integer, ByVal URL As String, ByVal Message As String, ByVal StackTrace As String, ByVal ExceptionClass As String, ByVal IpAddress As String, ByVal UserAgent As String)
        Dim sqlErrorConn As SqlConnection = Nothing
        Try
            sqlErrorConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString_RichTemplateCentral").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "Error_InsertError"
            sqlComm.Connection = sqlErrorConn

            'Add parameters
            sqlComm.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID
            sqlComm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
            sqlComm.Parameters.Add("@URL", SqlDbType.NVarChar).Value = URL
            sqlComm.Parameters.Add("@Message", SqlDbType.NVarChar).Value = Message
            sqlComm.Parameters.Add("@StackTrace", SqlDbType.NVarChar).Value = StackTrace
            sqlComm.Parameters.Add("@ExceptionClass", SqlDbType.NVarChar).Value = ExceptionClass
            sqlComm.Parameters.Add("@IpAddress", SqlDbType.NVarChar).Value = IpAddress
            sqlComm.Parameters.Add("@UserAgent", SqlDbType.NVarChar).Value = UserAgent
            sqlComm.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateTime.Now

            'sqlErrorConn.Open()
            'sqlComm.ExecuteScalar()

        Finally
            If Not sqlErrorConn Is Nothing Then
                sqlErrorConn.Close()
            End If
        End Try
    End Sub


    Public Shared Sub Error_LogError(ByVal ex As Exception)
        'Check if the applicationID Exists
        If Not ConfigurationManager.AppSettings("ApplicationID") Is Nothing Then

            'Get ApplicationID
            Dim ApplicationID As Integer = ConfigurationManager.AppSettings("ApplicationID")

            'Get User
            Dim intUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
            If intUserID = 0 Then
                'If the current user is not an admin, then check if the current user is a member
                intUserID = MemberDAL.GetCurrentMemberID()
            End If
            'Use only if using FormsAuthentication
            'Dim user As IPrincipal = HttpContext.Current.User
            'If Not user Is Nothing And user.Identity.IsAuthenticated Then
            '    intUserID = Convert.ToInt32(user.Identity.Name)
            'End If

            'Get URL
            Dim errorURL As String = HttpContext.Current.Request.Url.AbsoluteUri

            'Get Exception Information
            Dim Message As String = ex.Message
            Dim StackTrace As String = ex.StackTrace
            Dim ExceptionClass As String = ex.GetType().FullName

            'Get UserHostAddress and Browser
            Dim IpAddress As String = HttpContext.Current.Request.UserHostAddress
            Dim UserAgent As String = HttpContext.Current.Request.UserAgent

            Error_Insert(ApplicationID, intUserID, errorURL, Message, StackTrace, ExceptionClass, IpAddress, UserAgent)

        End If
    End Sub

#End Region


End Class
