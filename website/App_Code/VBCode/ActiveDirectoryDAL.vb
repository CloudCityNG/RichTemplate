Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Principal



Public Class ActiveDirectoryDAL

#Region "Log Active Directory Action Events"

    Public Shared Sub InsertActiveDirectoryEvent_LoginEvent(ByVal ActiveDirectoryEventInformation As String, ByVal SiteID As Integer)
        InsertActiveDirectoryEvent("Active Directory Login Attempt", ActiveDirectoryEventInformation, SiteID)
    End Sub
    Private Shared Sub InsertActiveDirectoryEvent(ByVal ActiveDirectoryEventType As String, ByVal ActiveDirectoryEventInformation As String, ByVal SiteID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ActiveDirectoryEvent_InsertActiveDirectoryEvent"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ActiveDirectoryEventType", SqlDbType.NVarChar).Value = ActiveDirectoryEventType
            sqlComm.Parameters.Add("@ActiveDirectoryEventInformation", SqlDbType.NVarChar).Value = ActiveDirectoryEventInformation

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            sqlComm.Parameters.Add("@IpAddress", SqlDbType.NVarChar).Value = HttpContext.Current.Request.UserHostAddress
            sqlComm.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateTime.Now

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub


    Public Shared Sub DeleteActiveDirectory_ServiceLog_All()

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ActiveDirectoryServiceLog_DeleteAll"
            sqlComm.Connection = sqlConn

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

#End Region

End Class
