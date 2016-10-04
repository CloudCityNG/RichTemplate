Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class HelpDAL

#Region "Help"
    
    Public Shared Function GetHelp_ByID(ByVal ID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Help_Select_ByID"
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

    Public Shared Function GetHelp_List() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Help_SelectList"
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

    Public Shared Function InsertHelp(ByVal Title As String, ByVal Description As String, ByVal HtmlContent As String, ByVal active As Boolean) As Integer

        Dim helpID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Help_InsertHelp"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title
            sqlComm.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description
            sqlComm.Parameters.Add("@HtmlContent", SqlDbType.NVarChar).Value = HtmlContent

            sqlComm.Parameters.Add("@active", SqlDbType.Bit).Value = active


            sqlConn.Open()
            helpID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return helpID
    End Function

    Public Shared Function UpdateHelp(ByVal helpID As Integer, ByVal Title As String, ByVal Description As String, ByVal HtmlContent As String, ByVal active As Boolean) As Integer

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Help_UpdateHelp_ByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = helpID

            sqlComm.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title
            sqlComm.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description
            sqlComm.Parameters.Add("@HtmlContent", SqlDbType.NVarChar).Value = HtmlContent

            sqlComm.Parameters.Add("@active", SqlDbType.Bit).Value = active


            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return helpID
    End Function

  
#End Region

End Class



