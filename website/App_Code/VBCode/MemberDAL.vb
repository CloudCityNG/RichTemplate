Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Principal
Imports System.IO


Public Class MemberDAL
#Region "Member"

    Public Shared Function GetCurrentMemberID() As Integer
        Dim _MemberID As Integer = 0

        '_MemberID = GetCurrentMemberID_WithoutSiteCheck()
        _MemberID = GetCurrentMemberID_WithSiteCheck()

        Return _MemberID
    End Function

    Public Shared Function GetCurrentMemberID_WithoutSiteCheck() As Integer
        Dim _MemberID As Integer = 0
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
            If (Not HttpContext.Current.Session("MemberID") Is Nothing) AndAlso (Not HttpContext.Current.Session("MemberID").ToString = "") Then
                'If HttpContext.Current.Request.IsAuthenticated Then
                'UNLIKE GetCurrentMemberID() this function doesn't check if the user user has access to anyparticular site, we use this only in the /login/default.aspx to check if a member is logged in
                _MemberID = Convert.ToInt32(HttpContext.Current.Session("MemberID").ToString())
                'End If


            End If
        End If

        'If the member is signed in, but the memberID in session is 0, then SignOut the Member
        If (_MemberID = 0) AndAlso (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Request Is Nothing) Then
            If HttpContext.Current.Request.IsAuthenticated Then
                FormsAuthentication.SignOut()
            End If
        End If

        Return _MemberID
    End Function

    Public Shared Function GetCurrentMemberID_WithSiteCheck() As Integer
        Dim _MemberID As Integer = 0
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
            If (Not HttpContext.Current.Session("MemberID") Is Nothing) AndAlso (Not HttpContext.Current.Session("MemberID").ToString = "") Then

                'Finally check this member has access to the current site, by checking if the site access contains ZERO for ALL SITES, or check the site access contains the current Site
                Dim strSiteAccessList As String() = GetCurrentMemberSiteIDs().Split(",")
                If strSiteAccessList.Contains("0") Or strSiteAccessList.Contains(SiteDAL.GetCurrentSiteID_FrontEnd().ToString()) Then
                    _MemberID = Convert.ToInt32(HttpContext.Current.Session("MemberID").ToString())
                End If

            End If
        End If
        'If the member is signed in, but the memberID in session is 0, then SignOut the Member
        If (_MemberID = 0) AndAlso (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Request Is Nothing) Then
            If HttpContext.Current.Request.IsAuthenticated Then
                FormsAuthentication.SignOut()
            End If
        End If

        Return _MemberID
    End Function

    Public Shared Sub SetCurrentMemberID(ByVal MemberID As Integer)
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
            HttpContext.Current.Session("MemberID") = MemberID.ToString()
        End If
    End Sub

    Public Shared Function GetCurrentMemberGroupIDs() As String
        Dim _GroupIDList As String = "-1" ' Default to the 'UN-Authenticated Group'
        If GetCurrentMemberID() > 0 Then
            _GroupIDList = "0" ' Set the Default to the 'AUTHENTICATED Group
            If Not HttpContext.Current.Session("MemberGroupIDs") Is Nothing Then
                _GroupIDList = HttpContext.Current.Session("MemberGroupIDs")
            End If
        End If

        Return _GroupIDList

    End Function

    Public Shared Sub SetCurrentMemberGroupIDs(ByVal MemberID As Integer, ByVal MemberGroupIDList As String)
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then

            'We want to prepend the memberGroupIDList with "-1" for 'un-authenticated group OR "0" if the member exists
            Dim intDefaultGroup As Integer = If(MemberID > 0, 0, -1)
            HttpContext.Current.Session("MemberGroupIDs") = If(MemberGroupIDList.Length = 0, intDefaultGroup.ToString(), intDefaultGroup.ToString() & "," & MemberGroupIDList)
        End If
    End Sub

    Public Shared Function GetCurrentMemberSiteIDs() As String
        Dim _SiteIDList As String = ""
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
            If Not HttpContext.Current.Session("MemberSiteIDs") Is Nothing Then
                _SiteIDList = HttpContext.Current.Session("MemberSiteIDs")
            End If
        End If
        Return _SiteIDList

    End Function

    Public Shared Sub SetCurrentMemberSiteIDs(ByVal MemberSiteIDList As String)
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then

            HttpContext.Current.Session("MemberSiteIDs") = MemberSiteIDList
        End If
    End Sub

    Public Shared Sub LoginMember(ByVal memberID As Integer, ByVal MemberGroupIDs As String, ByVal MemberSiteIDs As String, ByVal LanguageCode As String)
        'First clear the member and Admin user if logged in
        MemberDAL.LogoutCurrentMember()
        AdminUserDAL.LogoutCurrentAdminUser()

        'Set the current member and their associated Session Information
        MemberDAL.SetCurrentMemberID(memberID)

        '=> NOTE because a user sees all pages, and only has to log in once they veiw a page they don't have access to, this causes a problem
        '=> So if a user is logged in they should also beable to view pages in the 'un-authenticated
        MemberGroupIDs = "-1" & If(MemberGroupIDs.Length > 0, ",", "") & MemberGroupIDs
        MemberDAL.SetCurrentMemberGroupIDs(memberID, MemberGroupIDs)
        MemberDAL.SetCurrentMemberSiteIDs(MemberSiteIDs)

        'We dont clear the Language code, when the Member Logs out, as the member mya still want the site to be in their favourite language, rather than use the default language
        LanguageDAL.SetCurrentLanguage = LanguageCode

        'Update this members LastLogin Date
        MemberDAL.UpdateMember_LastLogin_ByMemberID(memberID, DateTime.Now())


    End Sub

    Public Shared Sub LogoutCurrentMember()
        MemberDAL.SetCurrentMemberID(0)
        MemberDAL.SetCurrentMemberSiteIDs(String.Empty)
        MemberDAL.SetCurrentMemberGroupIDs(0, String.Empty)
        FormsAuthentication.SignOut()
    End Sub

    Public Shared Sub DownloadVCard_ByMemberIDAndSiteID(ByVal intMemberID As Integer, ByVal SiteID As Integer)
        Dim dtMember As DataTable = MemberDAL.GetMember_ByMemberIDAndSiteID(intMemberID, SiteID)
        If dtMember.Rows.Count > 0 Then
            Dim drMember As DataRow = dtMember.Rows(0)

            Dim strWriter As New StringWriter()
            Dim htmlWriter As New HtmlTextWriter(strWriter)

            strWriter.WriteLine("BEGIN:VCARD")
            strWriter.WriteLine("VERSION:2.1")

            Dim strSalutation_LangaugeSpecific As String = String.Empty
            If Not drMember("Salutation_LanguageProperty") Is DBNull.Value Then
                Dim strSalutation_LanguageProperty As String = drMember("Salutation_LanguageProperty").ToString()
                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
            End If

            strWriter.WriteLine("N:" & drMember("LastName") & ";" & drMember("FirstName") & ";;" & strSalutation_LangaugeSpecific)
            strWriter.WriteLine("FN:" & drMember("Firstname") & " " & drMember("LastName"))

            If Not drMember("Email") Is DBNull.Value Then
                If Not drMember("Email") = "" Then
                    strWriter.WriteLine("EMAIL;PREF;INTERNET:" & drMember("Email"))
                End If
            End If

            If Not drMember("DaytimePhone") Is DBNull.Value Then
                If Not drMember("DaytimePhone") = "" Then
                    strWriter.WriteLine("TEL;WORK;VOICE:" & drMember("DaytimePhone"))
                End If
            End If

            If Not drMember("EveningPhone") Is DBNull.Value Then
                If Not drMember("EveningPhone") = "" Then
                    strWriter.WriteLine("TEL;CELL;VOICE:" & drMember("EveningPhone"))
                End If
            End If

            Dim locationStreet As String = ";"
            If Not drMember("address") Is DBNull.Value Then
                If Not drMember("address") = "" Then
                    locationStreet = drMember("address") & ";"
                End If
            End If

            Dim locationCity As String = ";"
            If Not drMember("city") Is DBNull.Value Then
                If Not drMember("city") = "" Then
                    locationCity = drMember("city") & ";"
                End If
            End If

            Dim locationState As String = ";"
            If Not drMember("stateName") Is DBNull.Value Then
                If Not drMember("stateName") = "" Then
                    locationState = drMember("stateName") & ";"
                End If
            End If

            Dim locationZip As String = ";"
            If Not drMember("zipCode") Is DBNull.Value Then
                If Not drMember("zipCode") = "" Then
                    locationZip = drMember("zipCode") & ";"
                End If
            End If


            Dim locationCountry As String = ";"
            If Not drMember("countryName") Is DBNull.Value Then
                If Not drMember("countryName") = "" Then
                    locationCountry = drMember("countryName") & ";"
                End If
            End If
            strWriter.WriteLine("ADR;WORK;ENCODING=QUOTED-PRINTABLE:" & ";" & locationStreet & locationCity & locationState & locationZip & locationCountry)

            strWriter.WriteLine("END:VCARD")


            Dim strMemberFullName As String = StrConv(strSalutation_LangaugeSpecific, vbProperCase) & StrConv(drMember("FirstName"), vbProperCase) & StrConv(drMember("LastName"), vbProperCase)

            'Setup the vCard response
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strMemberFullName & ".vcf")
            HttpContext.Current.Response.ContentType = "text/x-vCard; charset=utf-8; name=" & strMemberFullName + ".vcf"
            HttpContext.Current.Response.Write(strWriter.ToString())
            HttpContext.Current.Response.End()

        End If
    End Sub

    Public Shared Function GetMember_ByMemberID(ByVal memberID As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_SelectMemberByMemberID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = memberID

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

    Public Shared Function GetMember_ByMemberID_WithThumbnail(ByVal memberID As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_SelectMemberByMemberID_WithThumbnail"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = memberID

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

    Public Shared Function GetMember_ByMemberIDAndSiteID(ByVal memberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_SelectMemberByMemberIDAndSiteID"
            sqlComm.Connection = sqlConn

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

    Public Shared Function GetMember_ByMemberIDAndSiteID_WithThumbnail(ByVal memberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_SelectMemberByMemberIDAndSiteID_WithThumbnail"
            sqlComm.Connection = sqlConn
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

    Public Shared Function GetMemberList() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_SelectMemberList"
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

    Public Shared Function GetMemberList_BySiteID(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_SelectMemberList_BySiteID"
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

    Public Shared Function GetMemberList_ByEmail(ByVal email As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_SelectMemberList_ByEmail"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@email", SqlDbType.NVarChar).Value = email

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

    Public Shared Function GetMemberList_ByActiveDirectory_SID(ByVal ActiveDirectory_SID As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_SelectMemberList_ByActiveDirectory_SID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ActiveDirectory_SID", SqlDbType.NVarChar).Value = ActiveDirectory_SID

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

    Public Shared Function GetMemberList_ByEmail_FrontEnd(ByVal email As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_SelectMemberList_ByEmail_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@email", SqlDbType.NVarChar).Value = email

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

    Public Shared Function GetMemberList_ByMultipleColumns_FrontEnd(ByVal FirstName As String, ByVal LastName As String, ByVal LocationCategoryID As Integer, ByVal Location As String, ByVal SiteID As Integer) As DataTable
        Dim sbWhereClause As New StringBuilder()


        'Add the first name to the where clause if it has been set
        If FirstName.Length > 0 Then
            If sbWhereClause.Length > 0 Then
                sbWhereClause.Append(" AND ")
            End If
            sbWhereClause.Append("FirstName Like '" + FirstName + "%'")
        End If

        'Add the last name to the where clause if it has been set
        If LastName.Length > 0 Then
            If sbWhereClause.Length > 0 Then
                sbWhereClause.Append(" AND ")
            End If
            sbWhereClause.Append("LastName Like '" + LastName + "%'")
        End If

        'Add the LocationCategory to the where clause if it has been set
        If LocationCategoryID > Int32.MinValue Then
            If sbWhereClause.Length > 0 Then
                sbWhereClause.Append(" AND ")
            End If
            sbWhereClause.Append("CategoryID = " & LocationCategoryID)
        End If

        'Add the Location to the where clause if it has been set
        If Location.Length > 0 Then
            If sbWhereClause.Length > 0 Then
                sbWhereClause.Append(" AND ")
            End If
            sbWhereClause.Append("Location = '" + Location + "'")
        End If

        'Add the Site to the where clause if it has been set
        Dim sSiteJoinClause As String = ""
        If SiteID > 0 Then
            sSiteJoinClause = " And (s.ID = " & SiteID & ")"
            If sbWhereClause.Length > 0 Then
                sbWhereClause.Append(" AND ")
            End If
            sbWhereClause.Append("SiteName Is Not Null")
        End If

        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_SelectMemberList_ByMultipleColumns_FrontEnd"
            sqlComm.Parameters.Add("@SiteJoinClause", SqlDbType.NVarChar).Value = sSiteJoinClause
            sqlComm.Parameters.Add("@WhereClause", SqlDbType.NVarChar).Value = sbWhereClause.ToString()
            Dim strTest As String = sbWhereClause.ToString()
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

    Public Shared Function InsertMember(ByVal SalutationID As Integer, ByVal FirstName As String, ByVal LastName As String, ByVal Address As String, ByVal City As String, ByVal StateID As Integer, ByVal ZipCode As String, ByVal CountryID As Integer, ByVal LanguageID As Integer, ByVal LastLogin As DateTime, ByVal DaytimePhone As String, ByVal EveningPhone As String, ByVal Email As String, ByVal Password As String, ByVal SecurityQuestion As String, ByVal SecurityAnswer As String, ByVal GroupID As String, ByVal DateTimeStamp As DateTime, ByVal Active As Boolean, ByVal Company As String, ByVal CompanyDepartment As String, ByVal JobTitle As String, ByVal CompanyOffice As String, ByVal CompanyLocationID As Integer) As Integer

        Dim memberID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_InsertMember"
            sqlComm.Connection = sqlConn

            If SalutationID = Integer.MinValue Then
                sqlComm.Parameters.Add("@SalutationID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SalutationID", SqlDbType.Int).Value = SalutationID
            End If

            sqlComm.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = FirstName
            sqlComm.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = LastName

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

            If LanguageID = Integer.MinValue Then
                sqlComm.Parameters.Add("@LanguageID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@LanguageID", SqlDbType.Int).Value = LanguageID
            End If

            sqlComm.Parameters.Add("@DaytimePhone", SqlDbType.NVarChar).Value = DaytimePhone
            sqlComm.Parameters.Add("@EveningPhone", SqlDbType.NVarChar).Value = EveningPhone
            sqlComm.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email
            sqlComm.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password
            sqlComm.Parameters.Add("@SecurityQuestion", SqlDbType.NVarChar).Value = SecurityQuestion
            sqlComm.Parameters.Add("@SecurityAnswer", SqlDbType.NVarChar).Value = SecurityAnswer

            If LastLogin = DateTime.MinValue Then
                sqlComm.Parameters.Add("@LastLogin", SqlDbType.SmallDateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@LastLogin", SqlDbType.SmallDateTime).Value = LastLogin
            End If

            sqlComm.Parameters.Add("@dateTimeStamp_MemberCreated", SqlDbType.SmallDateTime).Value = DateTimeStamp
            sqlComm.Parameters.Add("@Active", SqlDbType.Bit).Value = Active
            sqlComm.Parameters.Add("@GroupID", SqlDbType.VarChar).Value = GroupID

            If Company.Length = 0 Then
                sqlComm.Parameters.Add("@Company", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Company", SqlDbType.NVarChar).Value = Company
            End If

            If CompanyDepartment.Length = 0 Then
                sqlComm.Parameters.Add("@CompanyDepartment", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CompanyDepartment", SqlDbType.NVarChar).Value = CompanyDepartment
            End If

            If JobTitle.Length = 0 Then
                sqlComm.Parameters.Add("@JobTitle", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@JobTitle", SqlDbType.NVarChar).Value = JobTitle
            End If

            If CompanyOffice.Length = 0 Then
                sqlComm.Parameters.Add("@CompanyOffice", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CompanyOffice", SqlDbType.NVarChar).Value = CompanyOffice
            End If

            If CompanyLocationID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CompanyLocationID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CompanyLocationID", SqlDbType.Int).Value = CompanyLocationID
            End If

            sqlConn.Open()
            memberID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return memberID
    End Function

    Public Shared Function UpdateMember(ByVal memberID As Integer, ByVal SalutationID As Integer, ByVal FirstName As String, ByVal LastName As String, ByVal Address As String, ByVal City As String, ByVal StateID As Integer, ByVal ZipCode As String, ByVal CountryID As Integer, ByVal LanguageID As Integer, ByVal DaytimePhone As String, ByVal EveningPhone As String, ByVal Email As String, ByVal SecurityQuestion As String, ByVal SecurityAnswer As String, ByVal GroupID As String, ByVal Active As Boolean, ByVal Company As String, ByVal CompanyDepartment As String, ByVal JobTitle As String, ByVal CompanyOffice As String, ByVal CompanyLocationID As Integer) As Integer

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_UpdateMember"
            sqlComm.Connection = sqlConn


            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = memberID

            If SalutationID = Integer.MinValue Then
                sqlComm.Parameters.Add("@SalutationID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SalutationID", SqlDbType.Int).Value = SalutationID
            End If

            sqlComm.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = FirstName
            sqlComm.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = LastName

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

            If LanguageID = Integer.MinValue Then
                sqlComm.Parameters.Add("@LanguageID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@LanguageID", SqlDbType.Int).Value = LanguageID
            End If

            sqlComm.Parameters.Add("@DaytimePhone", SqlDbType.NVarChar).Value = DaytimePhone
            sqlComm.Parameters.Add("@EveningPhone", SqlDbType.NVarChar).Value = EveningPhone
            sqlComm.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email

            sqlComm.Parameters.Add("@SecurityQuestion", SqlDbType.NVarChar).Value = SecurityQuestion
            sqlComm.Parameters.Add("@SecurityAnswer", SqlDbType.NVarChar).Value = SecurityAnswer

            sqlComm.Parameters.Add("@Active", SqlDbType.Bit).Value = Active
            sqlComm.Parameters.Add("@GroupID", SqlDbType.VarChar).Value = GroupID

            If Company.Length = 0 Then
                sqlComm.Parameters.Add("@Company", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Company", SqlDbType.NVarChar).Value = Company
            End If

            If CompanyDepartment.Length = 0 Then
                sqlComm.Parameters.Add("@CompanyDepartment", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CompanyDepartment", SqlDbType.NVarChar).Value = CompanyDepartment
            End If

            If JobTitle.Length = 0 Then
                sqlComm.Parameters.Add("@JobTitle", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@JobTitle", SqlDbType.NVarChar).Value = JobTitle
            End If

            If CompanyOffice.Length = 0 Then
                sqlComm.Parameters.Add("@CompanyOffice", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CompanyOffice", SqlDbType.NVarChar).Value = CompanyOffice
            End If

            If CompanyLocationID = Integer.MinValue Then
                sqlComm.Parameters.Add("@CompanyLocationID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CompanyLocationID", SqlDbType.Int).Value = CompanyLocationID
            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return memberID
    End Function

    Public Shared Sub UpdateMember_MemberImage_ByMemberID(ByVal MemberID As Integer, ByVal ThumbnailName As String, ByVal Thumbnail As Byte())

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_UpdateMember_MemberImage_ByMemberID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID

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

    Public Shared Sub UpdateMember_LastLogin_ByMemberID(ByVal memberID As Integer, ByVal LastLogin As DateTime)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_UpdateMember_LastLogin_ByMemberID"
            sqlComm.Connection = sqlConn


            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = memberID
            sqlComm.Parameters.Add("@LastLogin", SqlDbType.SmallDateTime).Value = LastLogin

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try

    End Sub

    Public Shared Sub UpdateMember_Password_ByMemberID(ByVal memberID As Integer, ByVal Password As String)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_UpdateMember_Password_ByMemberID"
            sqlComm.Connection = sqlConn


            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = memberID
            sqlComm.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try

    End Sub

    Public Shared Sub DeleteMember_ByID(ByVal ID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Member_Delete_ByID"
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

#End Region

#Region "MemberSecurityQuestions"
    Public Shared Function GetMemberSecurityQuestionsList() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_MemberSecurityQuestion_SelectSecurityQuestionList"
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

#Region "Member Groups"

    Public Shared Function GetMemberGroupList_ByMemberID(ByVal MemberID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_MemberGroups_SelectList_ByMemberID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID

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

    Public Shared Function GetMemberGroupList_ByMemberIDAndSiteID(ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_MemberGroups_SelectList_ByMemberIDAndSiteID"
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


    Public Shared Function InsertMemberGroup(ByVal MemberID As Integer, ByVal GroupID As Integer) As Guid

        Dim guidMemberGroupID As Guid = Guid.NewGuid()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_MemberGroups_Insert"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = guidMemberGroupID
            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@GroupID", SqlDbType.Int).Value = GroupID

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return guidMemberGroupID
    End Function


    Public Shared Sub DeleteMemberGroups_ByMemberIDAndSiteID(ByVal MemberID As Integer, ByVal SiteID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_MemberGroups_Delete_ByMemberIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub
#End Region

    Public Shared Function GetDepartmentList_Distinct(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Members_SelectDepartmentList_Distinct"
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
End Class
