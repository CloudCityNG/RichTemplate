Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Principal
Imports System.Net.Mail
Imports System.IO
Imports System.Net.Mime

Public Class EmailDAL

#Region "Email Sending Functionality"
    Public Shared Sub SendEmail(ByVal RecipientEmailAddressList As ArrayList, ByVal EmailTypeID As Integer, ByVal SiteID As Integer, ByVal EmailData As Hashtable)
        SendEmail(RecipientEmailAddressList, EmailTypeID, SiteID, EmailData, "")
    End Sub

    Public Shared Sub SendEmail(ByVal EmailTypeID As Integer, ByVal SiteID As Integer, ByVal EmailData As Hashtable)
        SendEmail(New ArrayList(), EmailTypeID, SiteID, EmailData, "")
    End Sub

    Public Shared Sub SendEmail(ByVal EmailTypeID As Integer, ByVal SiteID As Integer, ByVal EmailData As Hashtable, ByVal Subject As String)
        SendEmail(New ArrayList(), EmailTypeID, SiteID, EmailData, Subject)
    End Sub
    Public Shared Sub SendEmail(ByVal RecipientEmailAddressList As ArrayList, ByVal EmailTypeID As Integer, ByVal SiteID As Integer, ByVal EmailData As Hashtable, ByVal Subject As String)

        ' merge the Email swapout data into our Template Body
        Dim dtEmailTemplate As DataTable = GetEmailTemplate_ByEmailTypeIDAndSiteID(EmailTypeID, SiteID)

        If dtEmailTemplate.Rows.Count > 0 Then
            Dim drEmailTemplate As DataRow = dtEmailTemplate.Rows(0)


            'This is our Combined list of Recipients and Recipients that are Administratrator, which we will only send to if it is populated due to this being an 'IsAdministratorEmail' Type Email
            Dim RecipientEmailAddressList_Combined As New ArrayList()

            'Add our RecipientEmailAddresses, as lowercase email addresses, so we do not add duplicate Administrator Email Addresses
            For Each strRecipientEmailAddress In RecipientEmailAddressList
                RecipientEmailAddressList_Combined.Add(strRecipientEmailAddress.ToString().Trim().ToLower())
            Next


            'check if this email template is active, Hence only continue if the email template is active
            Dim boolEmailTemplateActive As Boolean = Convert.ToBoolean(drEmailTemplate("active"))
            If boolEmailTemplateActive Then

                'IMPORTANT - If this email Is Sent to An Administrator, then APPEND the Administrator RecipientEmailAddress to this arraylist
                'As in some situations we may use the Recipients we passed in, especially for the case where we send an Administrator Email, but to the Employment records contact person, or the Event records Contact Persons
                'Therefore before we add a recipient we check it does not already exist
                Dim boolIsAdministrationEmail As Boolean = Convert.ToBoolean(drEmailTemplate("IsAdministrationEmail"))
                If boolIsAdministrationEmail AndAlso (Not drEmailTemplate("RecipientEmailAddress") Is DBNull.Value) Then
                    'This is an Administration Email, so check if we have the email address already in our list

                    Dim strRecipientEmailAddress As String = drEmailTemplate("RecipientEmailAddress").ToString().Trim()
                    If strRecipientEmailAddress.Length > 0 Then
                        For Each strRecipientEmailAddress_Current As String In strRecipientEmailAddress.Split(",")

                            If Not RecipientEmailAddressList_Combined.Contains(strRecipientEmailAddress_Current.Trim().ToLower()) Then
                                RecipientEmailAddressList_Combined.Add(strRecipientEmailAddress_Current.Trim().ToLower())

                            End If
                        Next
                    End If

                End If


                Dim intEmailTemplateID As Integer = Convert.ToInt32(drEmailTemplate("EmailTemplateID"))
                'Populates a Swap out data for logging
                Dim strEmailData As String = ""
                For Each de As DictionaryEntry In EmailData
                    strEmailData += de.Key.ToString() + "##" + de.Value.ToString() + ";"
                Next

                'Populate Text Email Content
                Dim bodyText As String = ""
                If Not drEmailTemplate("BodyText") Is DBNull.Value Then
                    bodyText = drEmailTemplate("BodyText")
                End If
                bodyText = mergeData(bodyText, EmailData)

                'Populate HTML Email Content
                Dim bodyHtml As String = ""
                If Not drEmailTemplate("BodyHtml") Is DBNull.Value Then
                    bodyHtml = drEmailTemplate("BodyHtml")
                End If
                bodyHtml = mergeData(bodyHtml, EmailData)

                'Get the subject and email sender, and email replyto address
                'Note we get the subject from our email template only if a customized subject has not been passed in
                If Subject.Length = 0 Then
                    Subject = drEmailTemplate("Subject")
                End If

                Dim emailSenderEmailAddress As String = drEmailTemplate("SenderEmailAddress")
                Dim emailSenderName As String = ""
                If Not drEmailTemplate("SenderName") Is DBNull.Value Then
                    emailSenderName = drEmailTemplate("SenderName")
                End If

                Dim emailReplyToEmailAddress As String = ""
                If Not drEmailTemplate("ReplyToEmailAddress") Is DBNull.Value Then
                    emailReplyToEmailAddress = drEmailTemplate("ReplyToEmailAddress")
                End If
                Dim emailReplyToName As String = ""
                If Not drEmailTemplate("ReplyToName") Is DBNull.Value Then
                    emailReplyToName = drEmailTemplate("ReplyToName")
                End If

                'Create our Smtp Client
                Dim smtp As New SmtpClient
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network
                smtp.Host = ConfigurationManager.AppSettings("EmailServer").ToString()

                'Now we can create our Mail Messages so loop through all email recipients in our list and create the 
                Dim senderMailAddress As New MailAddress(emailSenderEmailAddress, emailSenderName)
                Dim msg As New MailMessage()
                msg.From = senderMailAddress
                If emailReplyToEmailAddress.Length > 0 Then
                    msg.ReplyTo = New MailAddress(emailReplyToEmailAddress, emailReplyToName)
                End If

                msg.Subject = Subject
                msg.Body = bodyText

                If bodyHtml.Length > 0 Then
                    Dim stream As New MemoryStream(ASCIIEncoding.Default.GetBytes(bodyHtml))
                    msg.AlternateViews.Add(New AlternateView(stream, MediaTypeNames.Text.Html))
                    msg.IsBodyHtml = True
                End If

                'Loop through all recipients in our RecipientEmailAddressList Combined List and add them to our mail message
                For Each strRecipientEmailAddress As String In RecipientEmailAddressList_Combined
                    Dim dtEmailSent As DateTime = DateTime.MinValue

                    Try

                        msg.To.Clear()
                        msg.To.Add(strRecipientEmailAddress)
                        smtp.Send(msg)
                        dtEmailSent = DateTime.Now()

                    Catch ex As Exception
                        'RichTemplateCentralDAL.Error_LogError(ex)
			Throw New Exception(ex.Message)
			'Throw New Exception(ex.InnerException.Message)
                    End Try

                    'Log the attempt at sending an email
                    InsertEmailLog(strRecipientEmailAddress, emailSenderEmailAddress, emailSenderName, emailReplyToEmailAddress, emailReplyToName, intEmailTemplateID, bodyText, bodyHtml, strEmailData, dtEmailSent)
                Next

            End If

        Else
            Throw New Exception("EmailTypeID: " & EmailTypeID & " SiteID: " & SiteID & " Does not exist!")
        End If


    End Sub

    Private Shared Function mergeData(ByVal TemplateBody As String, ByVal TemplateData As Hashtable) As String
        Dim sb As New StringBuilder(TemplateBody)

        If Not TemplateData Is Nothing Then

            ' Process all change outs
            Dim dataEnum As IDictionaryEnumerator = TemplateData.GetEnumerator()

            While dataEnum.MoveNext()
                Dim changeOutName As String = dataEnum.Key
                Dim changeOutValue As String = dataEnum.Value.ToString()
                sb.Replace("(|" + changeOutName + "|)", changeOutValue)
            End While
        End If
        Return sb.ToString()
    End Function
#End Region

#Region "Email Template"

    Public Shared Function GetEmailTemplate_ByEmailTemplateIDAndSiteID(ByVal EmailTemplateID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmailTemplate_Select_ByEmailTemplateIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmailTemplateID", SqlDbType.Int).Value = EmailTemplateID
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

    Public Shared Function GetEmailTemplate_ByEmailTypeIDAndSiteID(ByVal EmailTypeID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmailTemplate_Select_ByEmailTypeIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmailTypeID", SqlDbType.Int).Value = EmailTypeID
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

    Public Shared Function GetEmailTemplate_ByEmailTypeIDAndSiteID_FrontEnd(ByVal EmailTypeID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmailTemplate_Select_ByEmailTypeIDAndSiteID_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmailTypeID", SqlDbType.Int).Value = EmailTypeID
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

    Public Shared Function GetEmailTemplateList_BySiteID(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmailTemplate_SelectList_BySiteID"
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

    Public Shared Sub InsertEmailTemplate(ByVal EmailTypeID As Integer, ByVal SiteID As Integer, ByVal Name As String, ByVal Description As String, ByVal SenderEmailAddress As String, ByVal SenderName As String, ByVal ReplyToEmailAddress As String, ByVal ReplyToName As String, ByVal Subject As String, ByVal BodyText As String, ByVal BodyHtml As String, ByVal RecipientEmailAddress As String, ByVal Active As Boolean)
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmailTemplate_InsertEmailTemplate"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmailTypeID", SqlDbType.Int).Value = EmailTypeID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            sqlComm.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name
            sqlComm.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description

            sqlComm.Parameters.Add("@SenderEmailAddress", SqlDbType.NVarChar).Value = SenderEmailAddress

            If SenderName.Length = 0 Then
                sqlComm.Parameters.Add("@SenderName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SenderName", SqlDbType.NVarChar).Value = SenderName
            End If

            If ReplyToEmailAddress.Length = 0 Then
                sqlComm.Parameters.Add("@ReplyToEmailAddress", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ReplyToEmailAddress", SqlDbType.NVarChar).Value = ReplyToEmailAddress
            End If

            If ReplyToName.Length = 0 Then
                sqlComm.Parameters.Add("@ReplyToName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ReplyToName", SqlDbType.NVarChar).Value = ReplyToName
            End If
            sqlComm.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = Subject
            sqlComm.Parameters.Add("@BodyText", SqlDbType.NVarChar).Value = BodyText

            If BodyHtml.Length = 0 Then
                sqlComm.Parameters.Add("@BodyHtml", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@BodyHtml", SqlDbType.NVarChar).Value = BodyHtml
            End If

            If RecipientEmailAddress.Length = 0 Then
                sqlComm.Parameters.Add("@RecipientEmailAddress", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@RecipientEmailAddress", SqlDbType.NVarChar).Value = RecipientEmailAddress
            End If

            sqlComm.Parameters.Add("@Active", SqlDbType.Bit).Value = Active

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateEmailTemplate_ByEmailTemplateID(ByVal EmailTemplateID As Integer, ByVal Name As String, ByVal Description As String, ByVal SenderEmailAddress As String, ByVal SenderName As String, ByVal ReplyToEmailAddress As String, ByVal ReplyToName As String, ByVal Subject As String, ByVal BodyText As String, ByVal BodyHtml As String, ByVal RecipientEmailAddress As String, ByVal Active As Boolean)
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmailTemplate_UpdateEmailTemplate_ByEmailTemplateID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmailTemplateID", SqlDbType.Int).Value = EmailTemplateID

            sqlComm.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name
            sqlComm.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description

            sqlComm.Parameters.Add("@SenderEmailAddress", SqlDbType.NVarChar).Value = SenderEmailAddress

            If SenderName.Length = 0 Then
                sqlComm.Parameters.Add("@SenderName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SenderName", SqlDbType.NVarChar).Value = SenderName
            End If

            If ReplyToEmailAddress.Length = 0 Then
                sqlComm.Parameters.Add("@ReplyToEmailAddress", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ReplyToEmailAddress", SqlDbType.NVarChar).Value = ReplyToEmailAddress
            End If

            If ReplyToName.Length = 0 Then
                sqlComm.Parameters.Add("@ReplyToName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ReplyToName", SqlDbType.NVarChar).Value = ReplyToName
            End If
            sqlComm.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = Subject
            sqlComm.Parameters.Add("@BodyText", SqlDbType.NVarChar).Value = BodyText

            If BodyHtml.Length = 0 Then
                sqlComm.Parameters.Add("@BodyHtml", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@BodyHtml", SqlDbType.NVarChar).Value = BodyHtml
            End If

            If RecipientEmailAddress.Length = 0 Then
                sqlComm.Parameters.Add("@RecipientEmailAddress", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@RecipientEmailAddress", SqlDbType.NVarChar).Value = RecipientEmailAddress
            End If

            sqlComm.Parameters.Add("@Active", SqlDbType.Bit).Value = Active

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub DeleteEmailTemplate_ByEmailTemplateID(ByVal EmailTemplateID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmailTemplate_Delete_ByEmailTemplateID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmailTemplateID", SqlDbType.Int).Value = EmailTemplateID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub
#End Region

#Region "Email Type"

    Public Shared Function GetEmailTypeList() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmailType_SelectList"
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

#Region "Email Log - Insert DataLayer"
    Public Shared Sub InsertEmailLog(ByVal RecipientEmailAddress As String, ByVal SenderEmailAddress As String, ByVal SenderName As String, ByVal ReplyToEmailAddress As String, ByVal ReplyToName As String, ByVal EmailTemplateID As Integer, ByVal BodyText As String, ByVal BodyHtml As String, ByVal EmailSwapoutData As String, ByVal DateSent As DateTime)
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_EmailLog_InsertEmailLog"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@RecipientEmailAddress", SqlDbType.NVarChar).Value = RecipientEmailAddress
            sqlComm.Parameters.Add("@SenderEmailAddress", SqlDbType.NVarChar).Value = SenderEmailAddress

            If SenderName.Length = 0 Then
                sqlComm.Parameters.Add("@SenderName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SenderName", SqlDbType.NVarChar).Value = SenderName
            End If

            If ReplyToEmailAddress.Length = 0 Then
                sqlComm.Parameters.Add("@ReplyToEmailAddress", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ReplyToEmailAddress", SqlDbType.NVarChar).Value = ReplyToEmailAddress
            End If

            If ReplyToName.Length = 0 Then
                sqlComm.Parameters.Add("@ReplyToName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ReplyToName", SqlDbType.NVarChar).Value = ReplyToName
            End If


            sqlComm.Parameters.Add("@EmailTemplateID", SqlDbType.Int).Value = EmailTemplateID
            sqlComm.Parameters.Add("@BodyText", SqlDbType.NVarChar).Value = BodyText

            If BodyHtml.Length = 0 Then
                sqlComm.Parameters.Add("@BodyHtml", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@BodyHtml", SqlDbType.NVarChar).Value = BodyHtml
            End If

            If EmailSwapoutData.Length = 0 Then
                sqlComm.Parameters.Add("@SwapoutData", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SwapoutData", SqlDbType.NVarChar).Value = EmailSwapoutData
            End If


            If DateSent = DateTime.MinValue Then
                sqlComm.Parameters.Add("@DateSent", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@DateSent", SqlDbType.SmallDateTime).Value = DateSent
            End If

            sqlComm.Parameters.Add("@DateCreated", SqlDbType.SmallDateTime).Value = DateTime.Now

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
