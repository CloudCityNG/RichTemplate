Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class DocumentDAL

#Region "Document"

    Public Shared Function GetDocument_ByDocumentID(ByVal DocumentID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_Select_ByDocumentID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@DocumentID", SqlDbType.Int).Value = DocumentID

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

    Public Shared Function GetDocument_ByDocumentIDAndSiteID(ByVal DocumentID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_Select_ByDocumentIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@DocumentID", SqlDbType.Int).Value = DocumentID
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

    Public Shared Function GetDocument_ByDocumentIDAndStatus_FrontEnd(ByVal DocumentID As Integer, ByVal status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_Select_ByDocumentIDAndStatus_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@DocumentID", SqlDbType.Int).Value = DocumentID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = status
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

    Public Shared Function GetDocument_ByDocumentIDAndStatusAndAccess_FrontEnd(ByVal DocumentID As Integer, ByVal status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_Select_ByDocumentIDAndStatusAndAccess_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@DocumentID", SqlDbType.Int).Value = DocumentID
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = status
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

    Public Shared Function GetDocument_ByCategoryNullAndStatus_FrontEnd(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Document_SelectList_ByCategoryIDNULLAndStatus_FrontEnd"
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

    Public Shared Function GetDocument_ByCategoryNullAndStatusAndAccess_FrontEnd(ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Document_SelectList_ByCategoryIDNULLAndStatusAndAccess_FrontEnd"
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

    Public Shared Function GetDocument_ByCategoryIDAndStatus_FrontEnd(ByVal categoryID As Integer, ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Document_SelectList_ByCategoryIDAndStatus_FrontEnd"
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

    Public Shared Function GetDocument_ByCategoryIDAndStatusAndAccess_FrontEnd(ByVal categoryID As Integer, ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Connection = sqlConn

            sqlComm.CommandText = "ss_Document_SelectList_ByCategoryIDAndStatusAndAccess_FrontEnd"
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

    Public Shared Function GetDocument_ByFileNameAndFilePath_FrontEnd(ByVal FileName As String, ByVal FilePath As String, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_Select_ByFileNameAndFilePath_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = FileName
            sqlComm.Parameters.Add("@FilePath", SqlDbType.NVarChar).Value = FilePath
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

    Public Shared Function GetDocument_ByFileNameAndFilePathAndAccess_FrontEnd(ByVal FileName As String, ByVal FilePath As String, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_Select_ByFileNameAndFilePathAndAccess_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = FileName
            sqlComm.Parameters.Add("@FilePath", SqlDbType.NVarChar).Value = FilePath
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

    Public Shared Function GetDocumentList_ByStatus_FrontEnd(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_SelectList_ByStatus_FrontEnd"
            sqlComm.Connection = sqlConn

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

    Public Shared Function GetDocumentList_ByStatusAndAccess_FrontEnd(ByVal Status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_SelectList_ByStatusAndAccess_FrontEnd"
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

    Public Shared Function GetDocumentList_ByStatusAndSiteID(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_SelectList_ByStatusAndSiteID"
            sqlComm.Connection = sqlConn

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

    Public Shared Function GetDocumentList_BySiteID(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_SelectList_BySiteID"
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

    Public Shared Function GetDocumentList_ForSearch_ByStatus_FrontEnd(ByVal additionalWhereClause As String, ByVal status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_SelectList_ByStatus_FrontEnd_SearchwithAdditionalWhereClause"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@additionalWhereClause", SqlDbType.NVarChar).Value = additionalWhereClause
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

    Public Shared Function GetDocumentList_ForSearch_ByStatusAndAccess_FrontEnd(ByVal additionalWhereClause As String, ByVal status As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_SelectList_ByStatusAndAccess_FrontEnd_SearchwithAdditionalWhereClause"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@additionalWhereClause", SqlDbType.NVarChar).Value = additionalWhereClause
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

    Public Shared Sub DownloadDocument_ByDocumentID(ByVal intDocumentID As Integer, ByVal intSiteID As Integer)
        Dim dtDocument As DataTable = DocumentDAL.GetDocument_ByDocumentIDAndSiteID(intDocumentID, intSiteID)
        If dtDocument.Rows.Count > 0 Then
            Dim drDocument As DataRow = dtDocument.Rows(0)

            Dim strFileName As String = drDocument("fileName").ToString()
            Dim strFilePath As String = drDocument("filePath").ToString()
            Dim strFileNameFriendly As String = drDocument("friendlyFileName")
            Dim strPhysicalFilePathAndFileName As String = CommonWeb.GetServerPath() + strFilePath.Replace("/", "\") + strFileName

            CommonWeb.DownloadDocument_ByFilePath(strPhysicalFilePathAndFileName, strFileNameFriendly)
        End If
    End Sub

    Public Shared Function InsertDocument(ByVal SiteID As Integer, ByVal AvailableToAllSites As Boolean, ByVal fileTitle As String, ByVal fileDescription As String, ByVal FileName As String, ByVal friendlyFileName As String, ByVal filePath As String, ByVal fileType As String, ByVal fileUploadDate As DateTime, ByVal PublicationDate As DateTime, ByVal ExpirationDate As DateTime, ByVal categoryID As Integer, ByVal searchTagID As String, ByVal metaTitle As String, ByVal metaKeywords As String, ByVal metaDescription As String, ByVal metaOther As String, ByVal Status As Boolean, ByVal authorID_member As Integer, ByVal authorID_admin As Integer, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim DocumentID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_InsertDocument"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AvailableToAllSites", SqlDbType.Bit).Value = AvailableToAllSites

            sqlComm.Parameters.Add("@fileTitle", SqlDbType.NVarChar).Value = fileTitle
            sqlComm.Parameters.Add("@fileDescription", SqlDbType.NVarChar).Value = fileDescription

            sqlComm.Parameters.Add("@friendlyFileName", SqlDbType.NVarChar).Value = friendlyFileName
            sqlComm.Parameters.Add("@fileName", SqlDbType.NVarChar).Value = FileName
            sqlComm.Parameters.Add("@filePath", SqlDbType.NVarChar).Value = filePath
            sqlComm.Parameters.Add("@fileType", SqlDbType.NVarChar).Value = fileType
            sqlComm.Parameters.Add("@fileUploadDate", SqlDbType.SmallDateTime).Value = fileUploadDate

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

            If categoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID
            End If

            If searchTagID.Length = 0 Then
                sqlComm.Parameters.Add("@searchTagID", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@searchTagID", SqlDbType.VarChar).Value = searchTagID
            End If

            sqlComm.Parameters.Add("@metaTitle", SqlDbType.NVarChar).Value = metaTitle
            sqlComm.Parameters.Add("@metaKeywords", SqlDbType.NVarChar).Value = metaKeywords
            sqlComm.Parameters.Add("@metaDescription", SqlDbType.NVarChar).Value = metaDescription
            sqlComm.Parameters.Add("@metaOther", SqlDbType.NVarChar).Value = metaOther

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status

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
            DocumentID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return DocumentID
    End Function

    Public Shared Function UpdateDocument_ByDocumentID(ByVal DocumentID As Integer, ByVal SiteID As Integer, ByVal AvailableToAllSites As Boolean, ByVal fileTitle As String, ByVal fileDescription As String, ByVal PublicationDate As DateTime, ByVal ExpirationDate As DateTime, ByVal categoryID As Integer, ByVal searchTagID As String, ByVal metaTitle As String, ByVal metaKeywords As String, ByVal metaDescription As String, ByVal metaOther As String, ByVal Status As Boolean, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_UpdateDocument"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@DocumentID", SqlDbType.Int).Value = DocumentID

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AvailableToAllSites", SqlDbType.Bit).Value = AvailableToAllSites

            sqlComm.Parameters.Add("@fileTitle", SqlDbType.NVarChar).Value = fileTitle
            sqlComm.Parameters.Add("@fileDescription", SqlDbType.NVarChar).Value = fileDescription

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

            If categoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryID
            End If

            If searchTagID.Length = 0 Then
                sqlComm.Parameters.Add("@searchTagID", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@searchTagID", SqlDbType.VarChar).Value = searchTagID
            End If

            sqlComm.Parameters.Add("@metaTitle", SqlDbType.NVarChar).Value = metaTitle
            sqlComm.Parameters.Add("@metaKeywords", SqlDbType.NVarChar).Value = metaKeywords
            sqlComm.Parameters.Add("@metaDescription", SqlDbType.NVarChar).Value = metaDescription
            sqlComm.Parameters.Add("@metaOther", SqlDbType.NVarChar).Value = metaOther

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status

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
        Return DocumentID
    End Function

    Public Shared Function UpdateDocument_ByDocumentID_FrontEnd(ByVal DocumentID As Integer, ByVal FileTitle As String, ByVal FileDescription As String, ByVal CategoryID As Integer, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_UpdateDocument_ByDocumentID_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@DocumentID", SqlDbType.Int).Value = DocumentID

            sqlComm.Parameters.Add("@FileTitle", SqlDbType.NVarChar).Value = FileTitle
            sqlComm.Parameters.Add("@FileDescription", SqlDbType.NVarChar).Value = FileDescription

            If CategoryID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID
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
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return DocumentID
    End Function

    Public Shared Function UpdateDocument_FilePath_ByDocumentID(ByVal DocumentID As Integer, ByVal FilePath As String) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_UpdateDocument_FilePath_ByDocumentID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@DocumentID", SqlDbType.Int).Value = DocumentID

            sqlComm.Parameters.Add("@filePath", SqlDbType.NVarChar).Value = FilePath

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return DocumentID
    End Function

    Public Shared Function UpdateDocument_AveRating_ByDocumentID(ByVal DocumentID As Integer) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_UpdateDocumentAveRating"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@DocumentID", SqlDbType.Int).Value = DocumentID

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return DocumentID
    End Function

    Public Shared Sub DeleteDocument_ByDocumentID(ByVal DocumentID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Document_Delete_ByDocumentID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@DocumentID", SqlDbType.Int).Value = DocumentID

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



