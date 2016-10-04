Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class CommonDAL



    Public Shared Sub CreateDataExport(ByVal filename As String, ByVal delimiter As String, ByVal tableName As String, ByVal whereClause As String, ByVal orderbyClause As String, ByVal arrayColKeyValues As ArrayList)
        'Get the data in a datatable
        Dim dt As DataTable = GetDynamicDataTable(tableName, whereClause, orderbyClause, arrayColKeyValues)

        'Now go through each row in the datatable and add it to the HttpContext object
        Dim httpContext As HttpContext = httpContext.Current
        httpContext.Response.Clear()
        Dim sbResponse As New StringBuilder()
        For Each dc As DataColumn In dt.Columns
            sbResponse.Append(If(sbResponse.Length = 0, dc.ColumnName, delimiter & dc.ColumnName))
        Next
        httpContext.Response.Write(sbResponse.ToString())
        httpContext.Response.Write(Environment.NewLine)

        'Clear the string builder response for each row
        For Each dr As DataRow In dt.Rows
            sbResponse = New StringBuilder()
            For index As Integer = 0 To dt.Columns.Count - 1
                sbResponse.Append(If(sbResponse.Length = 0, dr(index).ToString().Replace(delimiter, String.Empty), delimiter & dr(index).ToString().Replace(delimiter, String.Empty)))
            Next
            httpContext.Response.Write(sbResponse.ToString())
            httpContext.Response.Write(Environment.NewLine)
        Next
        httpContext.Response.ContentType = "text/csv"
        httpContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" & filename & ".csv")
        httpContext.Response.End()
    End Sub

    Public Shared Function GetDynamicDataTable(ByVal tableName As String, ByVal whereClause As String, ByVal orderbyClause As String, ByVal arrayColKeyValues As ArrayList) As DataTable
        Dim dt As New DataTable()
        'Need to create the sql query
        Dim sbQuery As New StringBuilder()
        sbQuery.Append("Select ")
        For Each kvPair As KeyValuePair(Of String, String) In arrayColKeyValues
            Dim dbCol As String = kvPair.Key
            Dim dbFriendlyCol As String = kvPair.Value
            sbQuery.Append(dbCol & " AS [" & dbFriendlyCol & "], ")
        Next
        'Remove the last comma
        sbQuery.Remove(sbQuery.Length - 2, 2)
        sbQuery.Append(" From " & tableName)
        If whereClause.Length > 0 Then
            sbQuery.Append(" WHERE " & whereClause)
        End If
        If orderbyClause.Length > 0 Then
            sbQuery.Append(" ORDER BY " & orderbyClause)
        End If

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.Text
            sqlComm.CommandText = sbQuery.ToString()

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
End Class



