Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class ContactUsDAL

#Region "ContactUs"
    Public Shared Function GetContactUs_ByID(ByVal ID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ContactUs_Select_ByID"
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

    Public Shared Function GetContactUs_ByIDAndSiteID(ByVal ID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ContactUs_Select_ByIDAndSiteID"
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

    Public Shared Function GetContactUsList_ByStatusAndSiteID(ByVal Status As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ContactUs_SelectList_ByStatusAndSiteID"
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

    Public Shared Function InsertContactUs(ByVal SiteID As Integer, ByVal AvailableToAllSites As Boolean, ByVal ContactUsMessage As String, ByVal EmailAddress As String, ByVal MemberID As Integer, ByVal categoryID As Integer, ByVal PublicationDate As DateTime, ByVal ExpirationDate As DateTime, ByVal Status As Boolean, ByVal metaTitle As String, ByVal metaKeywords As String, ByVal metaDescription As String, ByVal metaOther As String, ByVal groupID As String, ByVal userID As String, ByVal searchTagID As String, ByVal Version As Integer, ByVal dateTimeStamp As DateTime, ByVal authorID_member As Integer, ByVal authorID_admin As Integer, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim pressReleaseID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ContactUs_InsertContactUs"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AvailableToAllSites", SqlDbType.Bit).Value = AvailableToAllSites

            sqlComm.Parameters.Add("@ContactUsMessage", SqlDbType.NVarChar).Value = ContactUsMessage

            If EmailAddress.Length = 0 Then
                sqlComm.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = EmailAddress
            End If

            If MemberID = Integer.MinValue Then
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            End If
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
            pressReleaseID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return pressReleaseID
    End Function

    Public Shared Function UpdateContactUs_ByID(ByVal ID As Integer, ByVal SiteID As Integer, ByVal AvailableToAllSites As Boolean, ByVal ContactUsMessage As String, ByVal EmailAddress As String, ByVal MemberID As Integer, ByVal categoryID As Integer, ByVal PublicationDate As DateTime, ByVal ExpirationDate As DateTime, ByVal Status As Boolean, ByVal metaTitle As String, ByVal metaKeywords As String, ByVal metaDescription As String, ByVal metaOther As String, ByVal groupID As String, ByVal userID As String, ByVal searchTagID As String, ByVal Version As Integer, ByVal modifiedID_member As Integer, ByVal modifiedID_admin As Integer) As Integer
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ContactUs_UpdateContactUs"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AvailableToAllSites", SqlDbType.Bit).Value = AvailableToAllSites

            sqlComm.Parameters.Add("@ContactUsMessage", SqlDbType.NVarChar).Value = ContactUsMessage

            If EmailAddress.Length = 0 Then
                sqlComm.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = EmailAddress
            End If

            If MemberID = Integer.MinValue Then
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            End If

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


    Public Shared Sub UpdateContactUs_ContactUsImage_ByID(ByVal ID As Integer, ByVal ThumbnailName As String, ByVal Thumbnail As Byte())

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ContactUs_UpdateContactUs_ContactUsImage_ByID"
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

    Public Shared Sub DeleteContactUs_ByID(ByVal ID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ContactUs_Delete_ByID"
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

    Public Shared Sub RollbackContactUs(ByVal archiveID As Integer)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ContactUs_RollBackContactUs"
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

#Region "ContactUs Archive"

    Public Shared Function GetContactUsArchive_ByArchiveIDAndSiteID(ByVal ArchiveID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ContactUsArchive_Select_ByArchiveIDAndSiteID"
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

    Public Shared Function GetContactUsArchiveList_ByContactUsID(ByVal ContactUsID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ContactUsArchive_SelectList_ByContactUsID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ContactUsID", SqlDbType.Int).Value = ContactUsID

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

    Public Shared Sub DeleteContactUsArchive_ByArchiveID(ByVal ArchiveID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_ContactUsArchive_Delete_ByArchiveID"
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
End Class



