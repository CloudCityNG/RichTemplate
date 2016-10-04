Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class AdminUserDAL

#Region "Admin User"

    Public Shared Function CheckAdminUserModuleAccess(ByVal ModuleTypeID As Integer, ByVal SiteID As Integer) As Boolean

        Dim listModuleTypeID() As String = AdminUserDAL.GetCurrentAdminUserAllowModules().Split(",")
        If listModuleTypeID.Contains(ModuleTypeID.ToString()) Then
            Return True
        End If
        Return False

    End Function

    Public Shared Sub LoginCurrentAdminUser(ByVal AdminUserID As Integer, ByVal SiteID As Integer, ByVal AccessLevel As Integer, ByVal AllowModules As String, ByVal AllowWebContent As Boolean, ByVal LanguageCode As String)

        'First clear the member and Admin user if logged in
        MemberDAL.LogoutCurrentMember()
        AdminUserDAL.LogoutCurrentAdminUser()

        'Set the current AdminUser, and their associated Session Information
        AdminUserDAL.SetCurrentAdminUserID(AdminUserID, SiteID)
        AdminUserDAL.SetCurrentAdminUserAccessLevel(AccessLevel)
        AdminUserDAL.SetCurrentAdminUserAllowModules(AllowModules, SiteID)
        AdminUserDAL.SetCurrentAdminUserAllowWebContent(AllowWebContent)

        'We dont clear the Language code, when the AdminUser logs out, as the user may still want the site to be in their favourite language, rather than the site default language
        LanguageDAL.SetCurrentLanguage = LanguageCode
    End Sub

    Public Shared Sub LogoutCurrentAdminUser()

        'Set the current AdminUserID to ZERO and remove all associated AdminUser Session Information
        AdminUserDAL.SetCurrentAdminUserID(0, Integer.MinValue)
        AdminUserDAL.SetCurrentAdminUserAccessLevel(0)
        AdminUserDAL.SetCurrentAdminUserAllowModules(String.Empty, Integer.MinValue)
        AdminUserDAL.SetCurrentAdminUserAllowWebContent(False)
    End Sub

    Public Shared Function GetCurrentAdminUserID() As Integer
        Dim _AdminUserID As Integer = 0
        If Not HttpContext.Current.Session Is Nothing Then
            If Not HttpContext.Current.Session("adminID") Is Nothing Then
                If Not HttpContext.Current.Session("adminID").ToString = "" Then
                    _AdminUserID = Convert.ToInt32(HttpContext.Current.Session("adminID").ToString())
                End If
            End If
        End If


        Return _AdminUserID
    End Function

    Public Shared Sub SetCurrentAdminUserID(ByVal ID As Integer, ByVal SiteID As Integer)
        'Set some session variables
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
            HttpContext.Current.Session("adminID") = ID.ToString()
        End If

        'Update this users login counter and last access date, if the AdminID is Greater than 0, as we pass in 0 to this function to log out a user
        If ID > 0 Then
            AdminUserDAL.UpdateAdminUser_LoginCounterAndLastAccess(ID)
            SiteDAL.UpdateSiteAccess_ForAdmin_LoginCounterAndLastAccess(ID, SiteID)
        End If

    End Sub

    Public Shared Function GetCurrentAdminUserAccessLevel() As Integer
        'Check the logged in user can view this page
        'Check thes user exists
        Dim intAdminUserAccessLevel As Integer = 0
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        If intAdminUserID > 0 Then
            'Need to check the Users Access_Level by looking at the session variable
            If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
                If Not HttpContext.Current.Session("ACCESS_LEVEL") Is Nothing Then
                    Dim strAccessLevel As String = HttpContext.Current.Session("ACCESS_LEVEL")
                    If strAccessLevel.Length > 0 Then
                        intAdminUserAccessLevel = Convert.ToInt32(strAccessLevel)
                    End If
                End If
            End If
        End If
        Return intAdminUserAccessLevel

    End Function

    Public Shared Sub SetCurrentAdminUserAccessLevel(ByVal AccessLevel As Integer)
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
            HttpContext.Current.Session("ACCESS_LEVEL") = AccessLevel.ToString()
        End If
    End Sub

    Public Shared Function GetCurrentAdminUserAllowWebContent() As Boolean
        Dim boolAllowModules As Boolean = False
        'Check the logged in user can view this page
        'Check thes user exists
        Dim intAdminUserAccessLevel As Integer = 0
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        If intAdminUserID > 0 Then
            'Need to check the Users Allow_WebContent by looking at the session variable
            If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
                If Not HttpContext.Current.Session("ALLOW_WEBCONTENT") Is Nothing Then
                    If Not HttpContext.Current.Session("ALLOW_WEBCONTENT").ToString = "" Then
                        boolAllowModules = Convert.ToBoolean(HttpContext.Current.Session("ALLOW_WEBCONTENT").ToString())
                    End If
                End If
            End If
        End If

        Return boolAllowModules
    End Function

    Public Shared Sub SetCurrentAdminUserAllowWebContent(ByVal AllowWebContent As Boolean)
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
            HttpContext.Current.Session("ALLOW_WEBCONTENT") = AllowWebContent.ToString()
        End If
    End Sub

    Public Shared Function GetCurrentAdminUserAllowModules() As String
        Dim strAllowModules As String = String.Empty
        Dim boolAllowModules As Boolean = False
        'Check the logged in user can view this page
        'Check thes user exists
        Dim intAdminUserAccessLevel As Integer = 0
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        If intAdminUserID > 0 Then
            'Need to check the Users Allow_Modules by looking at the session variable
            If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
                If Not HttpContext.Current.Session("ALLOW_MODULES") Is Nothing Then
                    If Not HttpContext.Current.Session("ALLOW_MODULES").ToString = "" Then
                        strAllowModules = HttpContext.Current.Session("ALLOW_MODULES").ToString()
                    End If
                End If
            End If
        End If

        Return strAllowModules
    End Function

    Public Shared Sub SetCurrentAdminUserAllowModules(ByVal AllowModules As String, ByVal SiteID As Integer)
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) Then
            'Before we set the AdminUsers allowed modules, we go though all of their modules and ensure, their modules relate to ACTIVE SITE MODULES
            Dim sbModuleTypeID As New StringBuilder()

            Dim listModuleTypeID() As String = AllowModules.Replace(" ", "").Split(",") 'remove spaces in case the list is like '2, 3, 4' etc.
            Dim dtModules As DataTable = ModuleDAL.GetModuleList_BySiteID_FrontEnd(SiteID)
            For Each drModule As DataRow In dtModules.Rows
                Dim strModuleTypeID As String = drModule("ModuleTypeID").ToString()
                If listModuleTypeID.Contains(strModuleTypeID) Then
                    sbModuleTypeID.Append(If(sbModuleTypeID.Length = 0, strModuleTypeID, "," + strModuleTypeID))
                End If
            Next

            HttpContext.Current.Session("ALLOW_MODULES") = sbModuleTypeID.ToString()
        End If
    End Sub

    Public Shared Function GetAdminUser_ByID(ByVal ID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_User_Select_ByID"
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

    Public Shared Function GetAdminUser_ByIDAndSiteID(ByVal ID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_User_Select_ByIDAndSiteID"
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

    Public Shared Function GetAdminUser_ByEmailAddress(ByVal EmailAddress As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_User_Select_ByEmailAddress"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = EmailAddress

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

    Public Shared Function GetAdminUser_ByUsername(ByVal Username As String) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_User_Select_ByUsername"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@Username", SqlDbType.NVarChar).Value = Username

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

    Public Shared Function GetAdminUserList() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_User_SelectList"
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

    Public Shared Function GetAdminUserList_ByAccessLevel(ByVal AccessLevel As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_User_SelectList_ByAccessLevel"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@Access_Level", SqlDbType.Int).Value = AccessLevel

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

    Public Shared Function GetAdminUserList_BySiteIDAndAccessLevel(ByVal SiteID As Integer, ByVal AccessLevel As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_User_SelectList_BySiteIDAndAccessLevel"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID
            sqlComm.Parameters.Add("@Access_Level", SqlDbType.Int).Value = AccessLevel

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

    Public Shared Function InsertAdminUser(ByVal SalutationID As Integer, ByVal First_Name As String, ByVal Last_Name As String, ByVal UserName As String, ByVal Password As String, ByVal Access_Level As Integer, ByVal Notes As String, ByVal Admin As Boolean, ByVal Active As Boolean, ByVal Expiration_Date As DateTime, ByVal Email As String, ByVal Address As String, ByVal City As String, ByVal ZipCode As String, ByVal StateID As Integer, ByVal CountryID As Integer, ByVal LanguageID As Integer, ByVal Phone As String, ByVal Counter As Long, ByVal Login_Limit As Long, ByVal Ip_Address As String, ByVal UseSiteLevelAccess As Boolean, ByVal Allow_WebContent As Boolean, ByVal Allow_Section_Add As Boolean, ByVal Allow_Page_Add As Boolean, ByVal Allow_Section_Edit As Boolean, ByVal Allow_Page_Edit As Boolean, ByVal Allow_Section_Delete As Boolean, ByVal Allow_Page_Delete As Boolean, ByVal Allow_Section_Rename As Boolean, ByVal Allow_Page_Rename As Boolean, ByVal Allow_Publish As Boolean, ByVal Allow_Modules As String, ByVal Custom1 As String, ByVal Custom2 As String, ByVal Custom3 As String, ByVal Custom4 As String, ByVal Custom5 As String, ByVal Custom6 As String) As Integer

        Dim userID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_User_InsertUser"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SalutationID", SqlDbType.Int).Value = SalutationID
            sqlComm.Parameters.Add("@First_Name", SqlDbType.NVarChar).Value = First_Name
            sqlComm.Parameters.Add("@Last_Name", SqlDbType.NVarChar).Value = Last_Name
            sqlComm.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName

            sqlComm.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password

            sqlComm.Parameters.Add("@Access_Level", SqlDbType.Int).Value = Access_Level

            If Notes.Length = 0 Then
                sqlComm.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = Notes
            End If

            sqlComm.Parameters.Add("@Admin", SqlDbType.Bit).Value = Admin
            sqlComm.Parameters.Add("@Active", SqlDbType.Bit).Value = Active

            If Expiration_Date = DateTime.MinValue Then
                sqlComm.Parameters.Add("@Expiration_Date", SqlDbType.DateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Expiration_Date", SqlDbType.DateTime).Value = Expiration_Date
            End If

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
                sqlComm.Parameters.Add("@State", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@State", SqlDbType.Int).Value = StateID
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

            If Phone.Length = 0 Then
                sqlComm.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Phone
            End If

            If Counter = Long.MinValue Then
                sqlComm.Parameters.Add("@Counter", SqlDbType.BigInt).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Counter", SqlDbType.BigInt).Value = Counter
            End If

            If Login_Limit = Long.MinValue Then
                sqlComm.Parameters.Add("@Login_Limit", SqlDbType.BigInt).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Login_Limit", SqlDbType.BigInt).Value = Login_Limit
            End If

            If Ip_Address.Length = 0 Then
                sqlComm.Parameters.Add("@IP_Address", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@IP_Address", SqlDbType.NVarChar).Value = Ip_Address
            End If

            sqlComm.Parameters.Add("@UseSiteLevelAccess", SqlDbType.Bit).Value = UseSiteLevelAccess

            sqlComm.Parameters.Add("@Allow_WebContent_AllSites", SqlDbType.Bit).Value = Allow_WebContent
            sqlComm.Parameters.Add("@Allow_Section_Add_AllSites", SqlDbType.Bit).Value = Allow_Section_Add
            sqlComm.Parameters.Add("@Allow_Page_Add_AllSites", SqlDbType.Bit).Value = Allow_Page_Add
            sqlComm.Parameters.Add("@Allow_Section_Edit_AllSites", SqlDbType.Bit).Value = Allow_Section_Edit
            sqlComm.Parameters.Add("@Allow_Page_Edit_AllSites", SqlDbType.Bit).Value = Allow_Page_Edit
            sqlComm.Parameters.Add("@Allow_Section_Delete_AllSites", SqlDbType.Bit).Value = Allow_Section_Delete
            sqlComm.Parameters.Add("@Allow_Page_Delete_AllSites", SqlDbType.Bit).Value = Allow_Page_Delete
            sqlComm.Parameters.Add("@Allow_Section_Rename_AllSites", SqlDbType.Bit).Value = Allow_Section_Rename
            sqlComm.Parameters.Add("@Allow_Page_Rename_AllSites", SqlDbType.Bit).Value = Allow_Page_Rename
            sqlComm.Parameters.Add("@Allow_Publish_AllSites", SqlDbType.Bit).Value = Allow_Publish
            sqlComm.Parameters.Add("@Allow_Modules_AllSites", SqlDbType.NVarChar).Value = Allow_Modules

            If Custom1.Length = 0 Then
                sqlComm.Parameters.Add("@Custom1", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Custom1", SqlDbType.NVarChar).Value = Custom1
            End If

            If Custom2.Length = 0 Then
                sqlComm.Parameters.Add("@Custom2", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Custom2", SqlDbType.NVarChar).Value = Custom2
            End If

            If Custom3.Length = 0 Then
                sqlComm.Parameters.Add("@Custom3", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Custom3", SqlDbType.NVarChar).Value = Custom3
            End If

            If Custom4.Length = 0 Then
                sqlComm.Parameters.Add("@Custom4", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Custom4", SqlDbType.NVarChar).Value = Custom4
            End If

            If Custom5.Length = 0 Then
                sqlComm.Parameters.Add("@Custom5", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Custom5", SqlDbType.NVarChar).Value = Custom5
            End If

            If Custom6.Length = 0 Then
                sqlComm.Parameters.Add("@Custom6", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Custom6", SqlDbType.NVarChar).Value = Custom6
            End If

            sqlConn.Open()
            userID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return userID
    End Function

    Public Shared Function UpdateAdminUser(ByVal userID As Integer, ByVal SalutationID As Integer, ByVal First_Name As String, ByVal Last_Name As String, ByVal UserName As String, ByVal Access_Level As Integer, ByVal Notes As String, ByVal Admin As Boolean, ByVal Active As Boolean, ByVal Expiration_Date As DateTime, ByVal Email As String, ByVal Address As String, ByVal City As String, ByVal ZipCode As String, ByVal StateID As Integer, ByVal CountryID As Integer, ByVal LanguageID As Integer, ByVal Phone As String, ByVal Login_Limit As Long, ByVal Ip_Address As String, ByVal UseSiteLevelAccess As Boolean, ByVal Allow_WebContent As Boolean, ByVal Allow_Section_Add As Boolean, ByVal Allow_Page_Add As Boolean, ByVal Allow_Section_Edit As Boolean, ByVal Allow_Page_Edit As Boolean, ByVal Allow_Section_Delete As Boolean, ByVal Allow_Page_Delete As Boolean, ByVal Allow_Section_Rename As Boolean, ByVal Allow_Page_Rename As Boolean, ByVal Allow_Publish As Boolean, ByVal Allow_Modules As String, ByVal Custom1 As String, ByVal Custom2 As String, ByVal Custom3 As String, ByVal Custom4 As String, ByVal Custom5 As String, ByVal Custom6 As String) As Integer

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_User_UpdateUser"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = userID

            sqlComm.Parameters.Add("@SalutationID", SqlDbType.Int).Value = SalutationID
            sqlComm.Parameters.Add("@First_Name", SqlDbType.NVarChar).Value = First_Name
            sqlComm.Parameters.Add("@Last_Name", SqlDbType.NVarChar).Value = Last_Name
            sqlComm.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName

            sqlComm.Parameters.Add("@Access_Level", SqlDbType.Int).Value = Access_Level

            If Notes.Length = 0 Then
                sqlComm.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = Notes
            End If

            sqlComm.Parameters.Add("@Admin", SqlDbType.Bit).Value = Admin
            sqlComm.Parameters.Add("@Active", SqlDbType.Bit).Value = Active

            If Expiration_Date = DateTime.MinValue Then
                sqlComm.Parameters.Add("@Expiration_Date", SqlDbType.DateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Expiration_Date", SqlDbType.DateTime).Value = Expiration_Date
            End If

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
                sqlComm.Parameters.Add("@State", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@State", SqlDbType.Int).Value = StateID
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

            If Phone.Length = 0 Then
                sqlComm.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Phone
            End If


            If Login_Limit = Long.MinValue Then
                sqlComm.Parameters.Add("@Login_Limit", SqlDbType.BigInt).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Login_Limit", SqlDbType.BigInt).Value = Login_Limit
            End If

            If Ip_Address.Length = 0 Then
                sqlComm.Parameters.Add("@IP_Address", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@IP_Address", SqlDbType.NVarChar).Value = Ip_Address
            End If

            sqlComm.Parameters.Add("@UseSiteLevelAccess", SqlDbType.Bit).Value = UseSiteLevelAccess

            sqlComm.Parameters.Add("@Allow_WebContent_AllSites", SqlDbType.Bit).Value = Allow_WebContent
            sqlComm.Parameters.Add("@Allow_Section_Add_AllSites", SqlDbType.Bit).Value = Allow_Section_Add
            sqlComm.Parameters.Add("@Allow_Page_Add_AllSites", SqlDbType.Bit).Value = Allow_Page_Add
            sqlComm.Parameters.Add("@Allow_Section_Edit_AllSites", SqlDbType.Bit).Value = Allow_Section_Edit
            sqlComm.Parameters.Add("@Allow_Page_Edit_AllSites", SqlDbType.Bit).Value = Allow_Page_Edit
            sqlComm.Parameters.Add("@Allow_Section_Delete_AllSites", SqlDbType.Bit).Value = Allow_Section_Delete
            sqlComm.Parameters.Add("@Allow_Page_Delete_AllSites", SqlDbType.Bit).Value = Allow_Page_Delete
            sqlComm.Parameters.Add("@Allow_Section_Rename_AllSites", SqlDbType.Bit).Value = Allow_Section_Rename
            sqlComm.Parameters.Add("@Allow_Page_Rename_AllSites", SqlDbType.Bit).Value = Allow_Page_Rename
            sqlComm.Parameters.Add("@Allow_Publish_AllSites", SqlDbType.Bit).Value = Allow_Publish
            sqlComm.Parameters.Add("@Allow_Modules_AllSites", SqlDbType.NVarChar).Value = Allow_Modules

            If Custom1.Length = 0 Then
                sqlComm.Parameters.Add("@Custom1", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Custom1", SqlDbType.NVarChar).Value = Custom1
            End If

            If Custom2.Length = 0 Then
                sqlComm.Parameters.Add("@Custom2", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Custom2", SqlDbType.NVarChar).Value = Custom2
            End If

            If Custom3.Length = 0 Then
                sqlComm.Parameters.Add("@Custom3", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Custom3", SqlDbType.NVarChar).Value = Custom3
            End If

            If Custom4.Length = 0 Then
                sqlComm.Parameters.Add("@Custom4", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Custom4", SqlDbType.NVarChar).Value = Custom4
            End If

            If Custom5.Length = 0 Then
                sqlComm.Parameters.Add("@Custom5", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Custom5", SqlDbType.NVarChar).Value = Custom5
            End If

            If Custom6.Length = 0 Then
                sqlComm.Parameters.Add("@Custom6", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Custom6", SqlDbType.NVarChar).Value = Custom6
            End If
            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return userID
    End Function

    Public Shared Sub UpdateAdminUser_Password(ByVal userID As Integer, ByVal Password As String)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_User_UpdateUser_Password"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = userID

            sqlComm.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password


            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateAdminUser_AllAdminUserView(ByVal userID As Integer, ByVal AllAdminUserView As Boolean)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_User_UpdateUser_AllAdminUserView"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = userID

            sqlComm.Parameters.Add("@AllAdminUserView", SqlDbType.Bit).Value = AllAdminUserView


            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Function UpdateAdminUser_LoginCounterAndLastAccess(ByVal userID As Integer) As Integer

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_user_UpdateLoginCounterAndLastAccess"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = userID

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return userID
    End Function

    Public Shared Sub DeleteUser_ByID(ByVal ID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_User_Delete_ByID"
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

#Region "Admin Users Account Type"
    Public Shared Function GetAdminUserAccountType_List() As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_AccountType_Select_List"
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

End Class



