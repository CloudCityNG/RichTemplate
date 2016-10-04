Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Principal
Imports Microsoft.Web.Administration


Public Class SiteDAL

    Shared listOverrideSiteNotAvailable As New List(Of String)(New String() {"/SiteNotAvailable.aspx", "/richadmin/"})

#Region "Get/Set Front-End Site"

    Public Shared Function GetCurrentSiteID_FrontEnd() As Integer
        'NOTE WE GET THE SITE ID WITHOUT USING SITE IDENTIFIER, there is only 1 site identifier (localhost), and the site is determined only by the current user, if no user then siteID is the first siteID, else we return the 1st site ID the user has access to
        Dim intSiteID As Integer = GetCurrentSiteID_ProcessWithSiteIdentifier() ' -> used for representing the siteID based on the sub-domain being the siteIdentifier (when using sub domain or some custom site Identifier perhaps from LDAP)
        'Dim intSiteID As Integer = GetCurrentSiteID_ProcessWithOutSiteIdentifier() ' -> used for representing the siteID based on just the siteID in session (when using 'Select A Site' drop down list)

        ' If we can not get a valid siteID, then there must be no valid sites, in which case redirect them to the 'SiteNotAvailable' if its appropriate to do so.
        If intSiteID = Integer.MinValue Then
            Dim boolRedirectToSiteNotAvailable As Boolean = True
            Dim strCurrentPage As String = HttpContext.Current.Request.Url.AbsolutePath
            For Each strOverrideSiteNotAvailable As String In listOverrideSiteNotAvailable
                If strCurrentPage.ToLower().Contains(strOverrideSiteNotAvailable.ToLower()) Then
                    boolRedirectToSiteNotAvailable = False
                    Exit For
                End If
            Next

            If boolRedirectToSiteNotAvailable Then
                HttpContext.Current.Response.Redirect("/SiteNotAvailable.aspx")
            End If
        End If


        Return intSiteID
    End Function

    Public Shared Sub SetCurrentSiteID_FrontEnd(ByVal SiteID As Integer)
        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(SiteID)
        If dtSite.Rows.Count > 0 Then
            Dim drSite As DataRow = dtSite.Rows(0)
            Dim strCompanyName As String = drSite("CompanyName")
            Dim strSiteIdentifier As String = String.Empty

            If Not drSite("SiteIdentifier") Is DBNull.Value Then
                strSiteIdentifier = drSite("SiteIdentifier").ToString()
            End If

            SetCurrentSiteID_FrontEnd(SiteID, strCompanyName, strSiteIdentifier)
        End If
    End Sub
    Public Shared Sub SetCurrentSiteID_FrontEnd(ByVal SiteID As Integer, ByVal CompanyName As String)
        SetCurrentSiteID_FrontEnd(SiteID, CompanyName, String.Empty)
    End Sub

    Public Shared Sub SetCurrentSiteID_FrontEnd(ByVal SiteID As Integer, ByVal CompanyName As String, ByVal SiteIdentifier As String)
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then

            'Store our values into session
            HttpContext.Current.Session("SiteID") = SiteID
            HttpContext.Current.Session("SiteIdentifier") = SiteIdentifier

            SiteDAL.SetCompayName(CompanyName)

        End If
    End Sub

    Private Shared Function GetCurrentSiteID_ProcessWithOutSiteIdentifier() As Integer
        Dim intSiteID As Integer = Integer.MinValue

        If Not HttpContext.Current Is Nothing Then
            'First check if we have it in session, if not, return empty siteID
            'NOTE we set this when the admin user logs in, the admin user can change this throughout their session if they are allowed to view more than one site
            If Not HttpContext.Current.Session Is Nothing Then
                If Not HttpContext.Current.Session("SiteID") Is Nothing Then
                    intSiteID = HttpContext.Current.Session("SiteID")
                End If
            End If
        End If

        'Now if the current siteID is STILL Integer.MinValue, we get the FIRST site from our list of all sites, and if a site exists, we set the siteIdentifier and Site ID to that of the FIRST SITE
        If intSiteID = Integer.MinValue Then
            Dim dtSite_FirstSite As DataTable = SiteDAL.GetFirstSite_FrontEnd()
            If dtSite_FirstSite.Rows.Count > 0 Then
                Dim drSite_FirstSite As DataRow = dtSite_FirstSite.Rows(0)
                intSiteID = Convert.ToInt32(drSite_FirstSite("ID"))

                Dim strCompanyName As String = drSite_FirstSite("CompanyName")
                Dim strSiteIdentifier As String = drSite_FirstSite("SiteIdentifier")

                SetCurrentSiteID_FrontEnd(intSiteID, strCompanyName, strSiteIdentifier)

            End If
        End If

        Return intSiteID
    End Function


    'Things to note, we determining SiteID For Front-End Site, you may want to slightlly modify the following to functions
    ' -> 'GetCurrentSiteIdentifier()' -> is the function we use to get the current Site Identifier, this might simply be the current url e.g. http://france.template.com/pagename.aspx, OR a query string parameter e.g. http://template.com/pagename.aspx?site=france
    ' -> 'CompareSiteIdentifier()' -> is the function we may need to be changed from site-to-site, as this function tells us if our current site is the same as the site in session
    '           -> Current it just checks both our Current Site Identifier equals the Site Identifier in session e.g. if we use sub domains, our current site identifier = http://france.template.com/pagename.aspx, in session our site identifier would be http://france in session would be france OR it might simply be the siteIdentifier_Current equals siteIdentifier_InSession
    '           -> So Compare would simply check that our Current Site Identifier StartsWith our SiteIdentifier in Session
    Private Shared Function GetCurrentSiteID_ProcessWithSiteIdentifier() As Integer
        'First we check if the current SiteID in session is valid, by comparing the request parameter 'site'
        Dim intSiteID As Integer = Integer.MinValue

        'NOTE: This GetCurrentSiteIdentifier function may need to be changed from site-to-site, as this function determines our current siteIdentifier
        Dim strSiteIdentifier As String = GetCurrentSiteIdentifier()
        Dim strSiteIdentifier_InSession As String = ""
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) AndAlso (Not HttpContext.Current.Session("SiteIdentifier") Is Nothing) Then

            'Check if we have the same SiteIdentifier in session, if so simply return the current SiteID that is in session
            strSiteIdentifier_InSession = HttpContext.Current.Session("SiteIdentifier").ToString().ToLower()

            'NOTE: The CompareSiteIdentifier is the function we may need to be changed from site-to-site, as this function tells us if our current site is the same as the site in session
            If CompareSiteIdentifier(strSiteIdentifier, strSiteIdentifier_InSession) Then

                'Our Site Identifiers are correct, so we can get get the SiteID from session
                If Not HttpContext.Current.Session("SiteID") Is Nothing AndAlso HttpContext.Current.Session("SiteID").ToString().Length > 0 Then
                    intSiteID = Convert.ToInt32(HttpContext.Current.Session("SiteID"))
                End If
            End If
        End If

        'Now if the current siteID is still Integer.MinValue, we get the current site that contains the specific SiteIdentifier in the list of all sites, and if a site exists, we set the siteIdentifier and Site ID
        If intSiteID = Integer.MinValue Then
            Dim dtSite As DataTable = SiteDAL.GetSite_BySiteIdentifier_FrontEnd(strSiteIdentifier)
            If dtSite.Rows.Count > 0 Then
                Dim drSite As DataRow = dtSite.Rows(0)
                intSiteID = Convert.ToInt32(drSite("ID"))

                Dim strCompanyName As String = drSite("CompanyName")

                SetCurrentSiteID_FrontEnd(intSiteID, strCompanyName, strSiteIdentifier)

            End If
        End If

        'Now if the current siteID is STILL Integer.MinValue, we get the FIRST site from our list of all sites, and if a site exists, we set the siteIdentifier and Site ID to that of the FIRST SITE
        If intSiteID = Integer.MinValue Then
            Dim dtSite_FirstSite As DataTable = SiteDAL.GetFirstSite_FrontEnd()
            If dtSite_FirstSite.Rows.Count > 0 Then
                Dim drSite_FirstSite As DataRow = dtSite_FirstSite.Rows(0)
                intSiteID = Convert.ToInt32(drSite_FirstSite("ID"))

                Dim strCompanyName As String = drSite_FirstSite("CompanyName")

                SetCurrentSiteID_FrontEnd(intSiteID, strCompanyName, strSiteIdentifier)

            End If
        End If

        Return intSiteID

    End Function

    Private Shared Function GetCurrentSiteIdentifier() As String
        Dim strSiteIdentifier As String = "" 'Set the Current Site Identifier to the Default SiteIdentifier, this is the siteIdentifier of our First Site

        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Request Is Nothing) Then
            If Not HttpContext.Current.Request.Url Is Nothing Then
                strSiteIdentifier = HttpContext.Current.Request.Url.Host.Replace("www.", "") 'Take the current URL HOST, and if exists remove the 'www.' from the host domain
            End If
        End If

        Return strSiteIdentifier
    End Function

    Private Shared Function CompareSiteIdentifier(ByVal strSiteIdentifier_Current As String, ByVal strSiteIdentifier_InSession As String) As String
        Dim boolSiteIdentifierAreEqual As Boolean = False

        'Check if we have the same site identifiers
        boolSiteIdentifierAreEqual = If(strSiteIdentifier_Current.ToLower() = strSiteIdentifier_InSession.ToLower(), True, False)
        Return boolSiteIdentifierAreEqual
    End Function

    Public Shared Function GetCompanyName() As String
        Dim strCompanyName As String = String.Empty

        'First check if we have it in session, if not, load it from our Config db table
        If Not HttpContext.Current.Session Is Nothing Then
            If Not HttpContext.Current.Session("CompanyName") Is Nothing Then
                strCompanyName = HttpContext.Current.Session("CompanyName")
            End If
        End If

        'If we still don't have the company name, load it from our Config DB Table, and store it in session for next time
        If strCompanyName = String.Empty Then
            Dim dtSite As DataTable = SiteDAL.GetSite_ByID(SiteDAL.GetCurrentSiteID_FrontEnd())
            If dtSite.Rows.Count > 0 Then
                strCompanyName = dtSite.Rows(0)("CompanyName").ToString()
                If Not HttpContext.Current.Session Is Nothing Then
                    HttpContext.Current.Session("CompanyName") = strCompanyName
                End If
            End If
        End If

        Return strCompanyName
    End Function

    Public Shared Sub SetCompayName(ByVal CompanyName As String)
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
            HttpContext.Current.Session("CompanyName") = CompanyName
        End If
    End Sub
#End Region

#Region "Get/Set Admin Site"
    Public Shared Function GetCurrentSiteID_Admin() As Integer
        Dim intSiteID As Integer = Integer.MinValue

        If Not HttpContext.Current Is Nothing Then
            'First check if we have it in session, if not, return empty siteID
            'NOTE we set this when the admin user logs in, the admin user can change this throughout their session if they are allowed to view more than one site
            If Not HttpContext.Current.Session Is Nothing Then
                If Not HttpContext.Current.Session("SiteID_Admin") Is Nothing Then
                    intSiteID = HttpContext.Current.Session("SiteID_Admin")
                End If
            End If
        End If

        If intSiteID = Integer.MinValue Then
            'If no site ID is found we log the user out of the admin
            HttpContext.Current.Response.Redirect("/richadmin")
        End If

        Return intSiteID
    End Function

    Public Shared Sub SetCurrentSiteID_Admin(ByVal SiteID As Integer)
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
            HttpContext.Current.Session("SiteID_Admin") = SiteID
        End If

    End Sub

    Public Shared Function GetDefaultSiteIDForAdminUser(ByVal AdminUserID As Integer, ByVal AccessLevel As Integer) As Integer
        Dim intSiteID As Integer = Integer.MinValue

        'First check the if we have a SiteIdentifier, if so try and load the site by SiteIdentifier
        'Check the current AdminUser has access to this siteID
        Dim intSiteID_Current As Integer = Integer.MinValue
        Dim strSiteIdentifier As String = GetCurrentSiteIdentifier()
        Dim dtSite_Current As DataTable = SiteDAL.GetSite_BySiteIdentifier(strSiteIdentifier)
        If dtSite_Current.Rows.Count > 0 Then
            Dim drSite_Current As DataRow = dtSite_Current.Rows(0)
            intSiteID_Current = Convert.ToInt32(drSite_Current("ID"))
        End If

        If intSiteID_Current <> Integer.MinValue Then
            If AccessLevel > 2 Then 'Then the AdminUser is a Master Administration (or better)
                intSiteID = intSiteID_Current
            Else
                'Check the User has access to this SiteID, first check if they have every site access, if not check they have access to this speicifc site
                Dim dtSiteAccess_AllSites As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID_Active(0, AdminUserID) 'Check if AdminUser has access to All Sites?
                If dtSiteAccess_AllSites.Rows.Count > 0 Then
                    intSiteID = intSiteID_Current
                Else
                    Dim dtSiteAccess_ThisSite As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID_Active(intSiteID_Current, AdminUserID)
                    If dtSiteAccess_ThisSite.Rows.Count > 0 Then ' So this AdminUser has access to this Site
                        intSiteID = intSiteID_Current
                    End If
                End If

            End If
        End If

        'If the siteID is still empty return the first site in the list of sites that the AdminUser has access to
        If intSiteID = Integer.MinValue Then
            'Current SiteID is empty, either it hasn't been set or we tried to set it but the currentSite is NULL Or Not Active, so get the first site in the list of all sites that the AdminUser has access to

            'Get all Sites the AdminUser has access to
            Dim dtSiteAccess As DataTable
            If AccessLevel > 2 Then ' Then the user is the Master Administration, so just return the first Site in the list of all sites
                dtSiteAccess = SiteDAL.GetSiteList()

            Else ' The we want to return the first Site in a list of All Sites the AdminUser has access to
                'Check if this adminUser has access to All Sites, if so get all sites, otherwise just get the sites they have access to
                Dim dtSiteList_AllSites As DataTable = SiteDAL.GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID_Active(0, AdminUserID)
                If dtSiteList_AllSites.Rows.Count > 0 Then
                    dtSiteAccess = SiteDAL.GetSiteList()
                Else
                    dtSiteAccess = SiteDAL.GetSiteAccessList_ForAdminUser_ByAdminUserID_Active(AdminUserID)
                End If

            End If
            If dtSiteAccess.Rows.Count > 0 Then
                intSiteID = Convert.ToInt32(dtSiteAccess.Rows(0)("ID"))
            End If
        End If
        Return intSiteID
    End Function

#End Region

#Region "Select"

    Public Shared Function GetSite_ByID(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Site_Select_ByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetSite_BySiteIdentifier(ByVal SiteIdentifier As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Site_Select_BySiteIdentifier"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteIdentifier", SqlDbType.NVarChar).Value = SiteIdentifier

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

    Public Shared Function GetSite_BySiteIdentifier_FrontEnd(ByVal SiteIdentifier As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Site_Select_BySiteIdentifier_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteIdentifier", SqlDbType.NVarChar).Value = SiteIdentifier

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

    Public Shared Function GetSiteList() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Site_SelectList"
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

    Public Shared Function GetSiteList_FrontEnd() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Site_SelectList_FrontEnd"
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

    Public Shared Function GetFirstSite_FrontEnd() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Site_SelectFirst_FrontEnd"
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

#Region "Insert"
    Public Shared Function InsertSite(ByVal SiteName As String, ByVal SiteDescription As String, ByVal CompanyName As String, ByVal CompanyStatement As String, ByVal LanguageID_Default As Integer, ByVal Address As String, ByVal City As String, ByVal StateID As Integer, ByVal ZipCode As String, ByVal CountryID As Integer, ByVal PhoneNumber As String, ByVal FaxNumber As String, ByVal EmailAddress As String, ByVal Domain As String, ByVal PackageTypeID As Integer, ByVal SiteDepth As Integer, ByVal UseThreeColumnLayout As Boolean, ByVal EnableGroupsAndUsers_PublicSection As Boolean, ByVal EnableGroupsAndUsers_MemberSection As Boolean, ByVal WebInfoID_HomePage As Integer, ByVal WebInfoID_Header As Integer, ByVal WebInfoID_Footer As Integer, ByVal SiteIdentifier As String, ByVal SiteIdentifier_LDAP As String, ByVal Status As Boolean, ByVal DateCreated As DateTime) As Integer
        Dim intSiteID As Integer = Integer.MinValue

        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Site_InsertSite"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteName", SqlDbType.NVarChar).Value = SiteName
            If CommonWeb.stripHTML(SiteDescription).Trim() = String.Empty Then
                sqlComm.Parameters.Add("@SiteDescription", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SiteDescription", SqlDbType.NVarChar).Value = SiteDescription
            End If

            sqlComm.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = CompanyName
            If CompanyStatement = String.Empty Then
                sqlComm.Parameters.Add("@CompanyStatement", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CompanyStatement", SqlDbType.NVarChar).Value = CompanyStatement
            End If

            sqlComm.Parameters.Add("@LanguageID_Default", SqlDbType.Int).Value = LanguageID_Default

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

            If PhoneNumber = String.Empty Then
                sqlComm.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = PhoneNumber
            End If

            If FaxNumber = String.Empty Then
                sqlComm.Parameters.Add("@FaxNumber", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@FaxNumber", SqlDbType.NVarChar).Value = FaxNumber
            End If

            If EmailAddress = String.Empty Then
                sqlComm.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = EmailAddress
            End If

            sqlComm.Parameters.Add("@Domain", SqlDbType.NVarChar).Value = Domain
            sqlComm.Parameters.Add("@PackageTypeID", SqlDbType.Int).Value = PackageTypeID
            sqlComm.Parameters.Add("@SiteDepth", SqlDbType.Int).Value = SiteDepth
            sqlComm.Parameters.Add("@UseThreeColumnLayout", SqlDbType.Bit).Value = UseThreeColumnLayout
            sqlComm.Parameters.Add("@WebPage_PublicSection_EnableGroupsAndUsers", SqlDbType.Bit).Value = EnableGroupsAndUsers_PublicSection
            sqlComm.Parameters.Add("@WebPage_MemberSection_EnableGroupsAndUsers", SqlDbType.Bit).Value = EnableGroupsAndUsers_MemberSection

            If WebInfoID_HomePage = Integer.MinValue Then
                sqlComm.Parameters.Add("@WebInfoID_HomePage", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@WebInfoID_HomePage", SqlDbType.Int).Value = WebInfoID_HomePage
            End If

            If WebInfoID_Header = Integer.MinValue Then
                sqlComm.Parameters.Add("@WebInfoID_Header", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@WebInfoID_Header", SqlDbType.Int).Value = WebInfoID_Header
            End If

            If WebInfoID_Footer = Integer.MinValue Then
                sqlComm.Parameters.Add("@WebInfoID_Footer", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@WebInfoID_Footer", SqlDbType.Int).Value = WebInfoID_Footer
            End If

            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status

            sqlComm.Parameters.Add("@SiteIdentifier", SqlDbType.NVarChar).Value = SiteIdentifier

            If SiteIdentifier_LDAP = String.Empty Then
                sqlComm.Parameters.Add("@SiteIdentifier_LDAP", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SiteIdentifier_LDAP", SqlDbType.NVarChar).Value = SiteIdentifier_LDAP
            End If

            sqlComm.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateCreated

            sqlConn.Open()
            intSiteID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try

        Return intSiteID
    End Function

    Public Shared Sub InsertSiteBindingIfDoesNotExist(ByVal SiteBindingName As String)
        Try
            Dim strIIS_SiteName As String = ConfigurationManager.AppSettings("IIS_SiteName")
            Dim iisManager As New ServerManager()
            Dim iisSite As Site = iisManager.Sites(strIIS_SiteName)

            'Create the binding
            Dim iisSiteBinding As Binding = iisSite.Bindings.CreateElement()
            iisSiteBinding.Protocol = "http"
            iisSiteBinding.BindingInformation = "*:80:" & SiteBindingName

            'Before we add this binding we check if it already exists
            Dim boolBindingAlreadyExists As Boolean = False
            For Each b As Binding In iisSite.Bindings
                If b.Protocol = iisSiteBinding.Protocol AndAlso b.BindingInformation = iisSiteBinding.BindingInformation Then
                    boolBindingAlreadyExists = True
                End If
            Next

            'If the binding does not exists, we add the binding to the site and Commit These Changes to the IIS MANAGER
            If Not boolBindingAlreadyExists Then
                iisSite.Bindings.Add(iisSiteBinding)
                iisManager.CommitChanges()
            End If
        Catch ex As Exception
            Throw New Exception("run under inetput\wwwroot\ or give the website directory computername\IIS_IUSRS")
        End Try

    End Sub
#End Region

#Region "Update"

    Public Shared Sub UpdateSite_ByID(ByVal SiteID As Integer, ByVal SiteName As String, ByVal SiteDescription As String, ByVal CompanyName As String, ByVal CompanyStatement As String, ByVal LanguageID_Default As Integer, ByVal Address As String, ByVal City As String, ByVal StateID As Integer, ByVal ZipCode As String, ByVal CountryID As Integer, ByVal PhoneNumber As String, ByVal FaxNumber As String, ByVal EmailAddress As String, ByVal Domain As String, ByVal PackageTypeID As Integer, ByVal SiteDepth As Integer, ByVal UseThreeColumnLayout As Boolean, ByVal EnableGroupsAndUsers_PublicSection As Boolean, ByVal EnableGroupsAndUsers_MemberSection As Boolean, ByVal SiteIdentifier As String, ByVal SiteIdentifier_LDAP As String, ByVal Status As Boolean)
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Site_UpdateSite_ByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = SiteID

            sqlComm.Parameters.Add("@SiteName", SqlDbType.NVarChar).Value = SiteName
            If CommonWeb.stripHTML(SiteDescription).Trim() = String.Empty Then
                sqlComm.Parameters.Add("@SiteDescription", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SiteDescription", SqlDbType.NVarChar).Value = SiteDescription
            End If

            sqlComm.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = CompanyName

            If CompanyStatement = String.Empty Then
                sqlComm.Parameters.Add("@CompanyStatement", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@CompanyStatement", SqlDbType.NVarChar).Value = CompanyStatement
            End If

            sqlComm.Parameters.Add("@LanguageID_Default", SqlDbType.Int).Value = LanguageID_Default

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

            If PhoneNumber = String.Empty Then
                sqlComm.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = PhoneNumber
            End If

            If FaxNumber = String.Empty Then
                sqlComm.Parameters.Add("@FaxNumber", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@FaxNumber", SqlDbType.NVarChar).Value = FaxNumber
            End If

            If EmailAddress = String.Empty Then
                sqlComm.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = EmailAddress
            End If

            sqlComm.Parameters.Add("@Domain", SqlDbType.NVarChar).Value = Domain

            sqlComm.Parameters.Add("@SiteIdentifier", SqlDbType.NVarChar).Value = SiteIdentifier

            If SiteIdentifier_LDAP = String.Empty Then
                sqlComm.Parameters.Add("@SiteIdentifier_LDAP", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SiteIdentifier_LDAP", SqlDbType.NVarChar).Value = SiteIdentifier_LDAP
            End If

            sqlComm.Parameters.Add("@PackageTypeID", SqlDbType.Int).Value = PackageTypeID
            sqlComm.Parameters.Add("@SiteDepth", SqlDbType.Int).Value = SiteDepth
            sqlComm.Parameters.Add("@UseThreeColumnLayout", SqlDbType.Bit).Value = UseThreeColumnLayout
            sqlComm.Parameters.Add("@WebPage_PublicSection_EnableGroupsAndUsers", SqlDbType.Bit).Value = EnableGroupsAndUsers_PublicSection
            sqlComm.Parameters.Add("@WebPage_MemberSection_EnableGroupsAndUsers", SqlDbType.Bit).Value = EnableGroupsAndUsers_MemberSection
            sqlComm.Parameters.Add("@Status", SqlDbType.Bit).Value = Status

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateSite_WebInfoID_HomeAndHeaderAndFooter_ByID(ByVal siteID As Integer, ByVal WebInfoID_HomePage As Integer, ByVal WebInfoID_Header As Integer, ByVal WebInfoID_Footer As Integer)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_Site_UpdateWebInfoID_HomeAndHeaderAndFooter_ByID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = siteID
            sqlComm.Parameters.Add("@WebInfoID_HomePage", SqlDbType.Int).Value = WebInfoID_HomePage
            sqlComm.Parameters.Add("@WebInfoID_Header", SqlDbType.Int).Value = WebInfoID_Header
            sqlComm.Parameters.Add("@WebInfoID_Footer", SqlDbType.Int).Value = WebInfoID_Footer

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

#End Region


#Region "Site Access - AdminUser"

    Public Shared Function GetSiteAccessList_ForAdminUser_BySiteID_Active(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SiteAccess_AdminUser_SelectList_BySiteID_Active"
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

    Public Shared Function GetSiteAccessList_ForAdminUser_ByAdminUserID_Active(ByVal AdminUserID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SiteAccess_AdminUser_SelectList_ByAdminUserID_Active"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@AdminUserID", SqlDbType.Int).Value = AdminUserID

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

    Public Shared Function GetSiteAccessList_ForAdminUser_ByAdminUserID_IncludeAllSites_Active(ByVal AdminUserID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SiteAccess_AdminUser_SelectList_ByAdminUserID_IncludeAllSites_Active"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@AdminUserID", SqlDbType.Int).Value = AdminUserID

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

    Public Shared Function GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID(ByVal SiteID As Integer, ByVal AdminUserID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SiteAccess_AdminUser_Select_BySiteIDAndAdminUserID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AdminUserID", SqlDbType.Int).Value = AdminUserID

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

    Public Shared Function GetSiteAccess_ForAdminUser_BySiteIDAndAdminUserID_Active(ByVal SiteID As Integer, ByVal AdminUserID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SiteAccess_AdminUser_Select_BySiteIDAndAdminUserID_Active"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AdminUserID", SqlDbType.Int).Value = AdminUserID

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

    Public Shared Sub InsertSiteAccess_ForAdminUser(ByVal SiteID As Integer, ByVal AdminUserID As Integer, ByVal Allow_WebContent As Boolean, ByVal Allow_Section_Add As Boolean, ByVal Allow_Page_Add As Boolean, ByVal Allow_Section_Edit As Boolean, ByVal Allow_Page_Edit As Boolean, ByVal Allow_Section_Delete As Boolean, ByVal Allow_Page_Delete As Boolean, ByVal Allow_Section_Rename As Boolean, ByVal Allow_Page_Rename As Boolean, ByVal Allow_Publish As Boolean, ByVal Allow_Modules As String, ByVal Active As Boolean)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SiteAccess_AdminUser_Insert"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AdminUserID", SqlDbType.Int).Value = AdminUserID

            sqlComm.Parameters.Add("@Allow_WebContent", SqlDbType.Bit).Value = Allow_WebContent
            sqlComm.Parameters.Add("@Allow_Section_Add", SqlDbType.Bit).Value = Allow_Section_Add
            sqlComm.Parameters.Add("@Allow_Page_Add", SqlDbType.Bit).Value = Allow_Page_Add
            sqlComm.Parameters.Add("@Allow_Section_Edit", SqlDbType.Bit).Value = Allow_Section_Edit
            sqlComm.Parameters.Add("@Allow_Page_Edit", SqlDbType.Bit).Value = Allow_Page_Edit
            sqlComm.Parameters.Add("@Allow_Section_Delete", SqlDbType.Bit).Value = Allow_Section_Delete
            sqlComm.Parameters.Add("@Allow_Page_Delete", SqlDbType.Bit).Value = Allow_Page_Delete
            sqlComm.Parameters.Add("@Allow_Section_Rename", SqlDbType.Bit).Value = Allow_Section_Rename
            sqlComm.Parameters.Add("@Allow_Page_Rename", SqlDbType.Bit).Value = Allow_Page_Rename
            sqlComm.Parameters.Add("@Allow_Publish", SqlDbType.Bit).Value = Allow_Publish
            sqlComm.Parameters.Add("@Allow_Modules", SqlDbType.NVarChar).Value = Allow_Modules

            sqlComm.Parameters.Add("@Active", SqlDbType.Bit).Value = Active

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateSiteAccess_ForAdminUser(ByVal SiteID As Integer, ByVal AdminUserID As Integer, ByVal Allow_WebContent As Boolean, ByVal Allow_Section_Add As Boolean, ByVal Allow_Page_Add As Boolean, ByVal Allow_Section_Edit As Boolean, ByVal Allow_Page_Edit As Boolean, ByVal Allow_Section_Delete As Boolean, ByVal Allow_Page_Delete As Boolean, ByVal Allow_Section_Rename As Boolean, ByVal Allow_Page_Rename As Boolean, ByVal Allow_Publish As Boolean, ByVal Allow_Modules As String, ByVal Active As Boolean)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SiteAccess_AdminUser_Update_BySiteIDAndAdminUserID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@AdminUserID", SqlDbType.Int).Value = AdminUserID

            sqlComm.Parameters.Add("@Allow_WebContent", SqlDbType.Bit).Value = Allow_WebContent
            sqlComm.Parameters.Add("@Allow_Section_Add", SqlDbType.Bit).Value = Allow_Section_Add
            sqlComm.Parameters.Add("@Allow_Page_Add", SqlDbType.Bit).Value = Allow_Page_Add
            sqlComm.Parameters.Add("@Allow_Section_Edit", SqlDbType.Bit).Value = Allow_Section_Edit
            sqlComm.Parameters.Add("@Allow_Page_Edit", SqlDbType.Bit).Value = Allow_Page_Edit
            sqlComm.Parameters.Add("@Allow_Section_Delete", SqlDbType.Bit).Value = Allow_Section_Delete
            sqlComm.Parameters.Add("@Allow_Page_Delete", SqlDbType.Bit).Value = Allow_Page_Delete
            sqlComm.Parameters.Add("@Allow_Section_Rename", SqlDbType.Bit).Value = Allow_Section_Rename
            sqlComm.Parameters.Add("@Allow_Page_Rename", SqlDbType.Bit).Value = Allow_Page_Rename
            sqlComm.Parameters.Add("@Allow_Publish", SqlDbType.Bit).Value = Allow_Publish
            sqlComm.Parameters.Add("@Allow_Modules", SqlDbType.NVarChar).Value = Allow_Modules

            sqlComm.Parameters.Add("@Active", SqlDbType.Bit).Value = Active

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateSiteAccess_ForAdmin_LoginCounterAndLastAccess(ByVal AdminUserID As Integer, ByVal SiteID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SiteAccess_AdminUser_UpdateLoginCounterAndLastAccess"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@AdminUserID", SqlDbType.Int).Value = AdminUserID
            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub


#End Region

#Region "Site Access - Member"

    Public Shared Function GetSiteAccess_ForMember_ByMemberIDAndSiteID(ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SiteAccess_Member_Select_ByMemberIDAndSiteID"
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

    Public Shared Function GetSiteAccessList_ForMember_ByMemberID(ByVal MemberID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SiteAccess_Member_SelectList_ByMemberID"
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

    Public Shared Sub InsertSiteAccess_ForMember(ByVal SiteID As Integer, ByVal MemberID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SiteAccess_Member_Insert"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try

    End Sub

    Public Shared Sub DeleteSiteAccess_ForMember_ByMemberID(ByVal MemberID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_SiteAccess_Member_Delete_ByMemberID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID

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
