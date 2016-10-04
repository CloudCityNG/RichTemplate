Imports System.Data
'Imports LDAP_ClassLibrary
Imports System.Threading
Imports System.Data.SqlClient
Imports System.DirectoryServices

Partial Class GetLDAP
    Inherits System.Web.UI.Page

    Dim intSiteID As Integer = Integer.MinValue
    Const LDAP_MAX_PAGE_SIZE As Integer = 9999
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
    '    intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
    '    Response.Write(intSiteID & "<br/>")
    '    Response.Write(MemberDAL.GetCurrentMemberID() & "<br/>")

    'End Sub


    'Public Sub btnImportGroupsAndUsers_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImportGroupsAndUsers.Click
    '    'Import Active Directory Groups Users for all sites
    '    ActiveDirectoryDAL.DeleteActiveDirectory_ServiceLog_All()
    '    Dim strError As String = PerformActiveDirectoryTransferProcess()

    '    'Dim strError As String = "EMPTY"

    '    'Try
    '    '    Dim dsGroupsAndMemberGroups As DataSet = ImportActiveDirectoryUsers_PerformImport(0, Guid.NewGuid(), True)
    '    '    ImportActiveDirectoryGroups_PerformImport(dsGroupsAndMemberGroups, Guid.NewGuid())
    '    '    ImportActiveDirectoryUserGroups_PerformImport(dsGroupsAndMemberGroups, Guid.NewGuid())
    '    'Catch importException As Exception
    '    '    strError = importException.Message & "=>" & importException.StackTrace
    '    'End Try

    '    Response.Write("ERROR? " & If(strError.Length > 0, strError, "NO!"))

    'End Sub

    'Public Sub btnGetUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetUser.Click
    '    Time_SetStartTime()
    '    Dim dtUser As DataTable = LDAP.AD_GetUser(txtActiveDirectory_Identifier.Text.Trim())
    '    rptUser.DataSource = dtUser
    '    rptUser.DataBind()
    '    Time_SetEndTime()
    'End Sub

    'Public Sub btnGetUserGroups_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetUserGroups.Click
    '    Time_SetStartTime()
    '    Dim dtUserGroups As DataTable = LDAP.AD_GetUserGroups(txtActiveDirectory_Identifier.Text.Trim(), intSiteID)
    '    rptUserGroups.DataSource = dtUserGroups
    '    rptUserGroups.DataBind()
    '    Time_SetEndTime()

    'End Sub

    'Public Sub btnGetAllUsers_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetAllUsers.Click
    '    Time_SetStartTime()
    '    Dim dtUsers As DataTable = LDAP.AD_GetUserList(intSiteID, 1000)
    '    rptUserList.DataSource = dtUsers
    '    rptUserList.DataBind()
    '    Time_SetEndTime()
    'End Sub

    'Public Sub btnGetAllGroups_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetAllGroups.Click
    '    Time_SetStartTime()
    '    Dim dtGroups As DataTable = LDAP.AD_GetGroupList(intSiteID, 1000)
    '    rptGroupList.DataSource = dtGroups
    '    rptGroupList.DataBind()
    '    Time_SetEndTime()
    'End Sub

    'Private Sub Time_SetStartTime()
    '    litTimeStart.Text = DateTime.Now.ToString()
    'End Sub

    'Private Sub Time_SetEndTime()
    '    litTimeEnd.Text = DateTime.Now.ToString()
    'End Sub





    'Public Sub DBActiveDirectoryServiceLog_UpdateActiveDirectoryServiceLog_UsersImport_DateEnd(ByVal ActiveDirectoryServiceLogGUID As Guid, ByVal Users_DateEnd As DateTime, ByVal Users_Created As Integer, ByVal Users_Updated As Integer, ByVal Users_Deleted As Integer)

    '    Dim sqlConn As SqlConnection = Nothing
    '    Try
    '        sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    '        Dim sqlComm As New SqlCommand()
    '        sqlComm.CommandType = CommandType.StoredProcedure
    '        sqlComm.CommandText = "ActiveDirectoryServiceLog_UpdateActiveDirectoryServiceLog_UsersImport_DateEnd"
    '        sqlComm.Connection = sqlConn

    '        sqlComm.Parameters.Add("@ActiveDirectoryServiceLogGUID", SqlDbType.UniqueIdentifier).Value = ActiveDirectoryServiceLogGUID

    '        sqlComm.Parameters.Add("@Users_DateEnd", SqlDbType.DateTime).Value = Users_DateEnd

    '        sqlComm.Parameters.Add("@Users_Created", SqlDbType.Int).Value = Users_Created
    '        sqlComm.Parameters.Add("@Users_Updated", SqlDbType.Int).Value = Users_Updated
    '        sqlComm.Parameters.Add("@Users_Deleted", SqlDbType.Int).Value = Users_Deleted


    '        sqlConn.Open()
    '        sqlComm.ExecuteScalar()

    '    Finally
    '        If Not sqlConn Is Nothing Then
    '            sqlConn.Close()
    '        End If
    '    End Try
    'End Sub



    'Private Sub DBActiveDirectoryServiceLog_UpdateActiveDirectoryServiceLog_UsersImport_DateStart(ByVal ActiveDirectoryServiceLogGUID As Guid, ByVal Users_DateStart As DateTime)

    '    Dim sqlConn As SqlConnection = Nothing
    '    Try
    '        sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    '        Dim sqlComm As New SqlCommand()
    '        sqlComm.CommandType = CommandType.StoredProcedure
    '        sqlComm.CommandText = "ActiveDirectoryServiceLog_UpdateActiveDirectoryServiceLog_UsersImport_DateStart"
    '        sqlComm.Connection = sqlConn

    '        sqlComm.Parameters.Add("@ActiveDirectoryServiceLogGUID", SqlDbType.UniqueIdentifier).Value = ActiveDirectoryServiceLogGUID

    '        sqlComm.Parameters.Add("@Users_DateStart", SqlDbType.DateTime).Value = Users_DateStart

    '        sqlConn.Open()
    '        sqlComm.ExecuteScalar()

    '    Finally
    '        If Not sqlConn Is Nothing Then
    '            sqlConn.Close()
    '        End If
    '    End Try
    'End Sub




    'Private Function GetLdapRootEntry(ByVal IsUser As Boolean, ByVal CurrentDomain As String) As String
    '    Return CurrentDomain

    '    'Dim strActiveDirectory_LDAP_Users_Or_Groups As String = IIf(IsUser, ConfigurationManager.AppSettings("ActiveDirectory_LDAP_OU_Users").ToString(), ConfigurationManager.AppSettings("ActiveDirectory_LDAP_OU_Groups").ToString())
    '    'Dim strActiveDirectory_LDAP_UserGroup_Type As String = ConfigurationManager.AppSettings("ActiveDirectory_LDAP_UserGroup_Type").ToString()
    '    'Dim strLdapRootEntry As String = "LDAP://" & strActiveDirectory_LDAP_UserGroup_Type & "=" & strActiveDirectory_LDAP_Users_Or_Groups & "," & CurrentDomain
    '    'Return strLdapRootEntry

    'End Function

    'Private Function IsActiveDirectoryUserActive(ByVal de As DirectoryEntry) As Boolean
    '    Try
    '        'If they have no native guid
    '        If de.NativeGuid Is Nothing Then
    '            Return False
    '        End If
    '        'If they have a disabled flag
    '        Dim intFlags As Integer = de.Properties("userAccountControl").Value
    '        If Convert.ToBoolean(intFlags And &H2) Then
    '            Return False
    '        End If

    '        'If they have no firstname and lastname, this is custom
    '        If de.Properties("givenname").Value Is Nothing Then
    '            Return False
    '        ElseIf de.Properties("givenname").Value.ToString().Trim().Length = 0 Then
    '            Return False
    '        End If


    '        Return True
    '    Catch
    '        Return False
    '    End Try

    'End Function

    'Private Function GetDomainFromDistinguishedName(ByVal DistinguishedName As String) As String
    '    Dim strFormatCurrentDomain As String = String.Empty
    '    If DistinguishedName.Length > 0 Then

    '        Dim listFormatCurrentDomain As String() = DistinguishedName.Split(",")
    '        For Each strDomain As String In listFormatCurrentDomain
    '            If strDomain.StartsWith("DC=") Then
    '                strFormatCurrentDomain &= If(strFormatCurrentDomain.Length = 0, "", ".") & strDomain.Replace("DC=", "")
    '            End If

    '        Next
    '    End If

    '    Return strFormatCurrentDomain

    'End Function

    'Public Function AD_GetUserList(ByVal SiteIdentifier_LDAP As String, ByVal PageSize As Integer) As DataTable

    '    Dim listActiveDirectoryColumnsToGet As String() = {"distinguishedName", "givenName", "sn", "sAMAccountName", "mail", "company", "department", "title", "memberOf", "physicalDeliveryOfficeName", "telephonenumber", "streetaddress", "l", "st", "postalCode"}



    '    Dim dtUsers As New DataTable()
    '    For Each strColumnToGet As String In listActiveDirectoryColumnsToGet
    '        dtUsers.Columns.Add(New DataColumn(strColumnToGet))
    '    Next
    '    dtUsers.Columns.Add(New DataColumn("objectSID"))
    '    dtUsers.Columns.Add(New DataColumn("objectGUID"))
    '    dtUsers.Columns.Add(New DataColumn("domain"))
    '    dtUsers.Columns.Add(New DataColumn("groupIdList"))
    '    Dim ldapServerAndDomain As String = LDAP.AD_GetCurrentDomain(SiteIdentifier_LDAP)
    '    If ldapServerAndDomain.Length > 0 Then
    '        'Dim strLdapRootEntry As String = GetLdapRootEntry(True, ldapServerAndDomain)
    '        Dim strLdapRootEntry As String = ldapServerAndDomain
    '        Using deRootEntry As New DirectoryEntry(strLdapRootEntry)

    '            Using oSearcher As New DirectorySearcher(deRootEntry)
    '                Dim strSearcherFilter As String = "(!(userAccountControl:1.2.840.113556.1.4.803:=2))(objectClass=user)"
    '                oSearcher.Filter = "(&" & strSearcherFilter & ")"

    '                If PageSize >= 0 Then
    '                    oSearcher.PageSize = PageSize
    '                End If

    '                oSearcher.PropertiesToLoad.AddRange(listActiveDirectoryColumnsToGet)
    '                oSearcher.PropertiesToLoad.Add("objectSID")
    '                oSearcher.PropertiesToLoad.Add("objectGUID")

    '                'set timeout
    '                oSearcher.ClientTimeout = New TimeSpan(0, 0, 5, 0, 0)
    '                oSearcher.ServerPageTimeLimit = New TimeSpan(0, 0, 5, 0, 0)
    '                oSearcher.ServerTimeLimit = New TimeSpan(0, 0, 5, 0, 0)

    '                Dim oResults As SearchResultCollection = oSearcher.FindAll()
    '                For Each oResult As SearchResult In oResults

    '                    Dim de As DirectoryEntry = oResult.GetDirectoryEntry()
    '                    Dim boolIsActiveDirectoryUserActive As Boolean = IsActiveDirectoryUserActive(de)
    '                    If boolIsActiveDirectoryUserActive Then
    '                        Dim drUser As DataRow = dtUsers.NewRow()
    '                        For Each strColumnToGet As String In listActiveDirectoryColumnsToGet
    '                            drUser(strColumnToGet) = de.Properties(strColumnToGet).Value
    '                        Next
    '                        Dim byteObjectSID As Byte() = de.Properties("objectSID").Item(0)
    '                        drUser("objectSID") = New System.Security.Principal.SecurityIdentifier(byteObjectSID, 0).ToString()
    '                        drUser("objectGUID") = de.Guid.ToString()
    '                        drUser("domain") = GetDomainFromDistinguishedName(de.Properties("distinguishedName").Value)

    '                        'Get User Groups
    '                        'If we have at least one richtemplate Active Directory Group we continue, and duplicate the dtGroups schema, so we can add groups that the Active Directory User actually belongs to
    '                        Dim sbGroupIDList As New StringBuilder()
    '                        If Not oResult.Properties("memberOf") Is Nothing Then
    '                            For Each strActiveDirectory_DistinguishedName As String In oResult.Properties("memberOf")
    '                                'we have an LDAP Location of this group, we check we have it in our list of groups, if so we add it to the UsersGroups Table
    '                                strActiveDirectory_DistinguishedName = strActiveDirectory_DistinguishedName.Replace("CN=", "")
    '                                'remove everything after the comma
    '                                Dim intIndexOfComma As Integer = strActiveDirectory_DistinguishedName.IndexOf(",")
    '                                If intIndexOfComma > 0 Then
    '                                    strActiveDirectory_DistinguishedName = strActiveDirectory_DistinguishedName.Substring(0, intIndexOfComma)
    '                                End If
    '                                sbGroupIDList.Append(If(sbGroupIDList.Length = 0, strActiveDirectory_DistinguishedName.ToString(), "," & strActiveDirectory_DistinguishedName.ToString()))


    '                            Next
    '                        End If
    '                        drUser("groupIdList") = sbGroupIDList.ToString()

    '                        'Dim strUsername As String = de.Properties("sAMAccountName").Value
    '                        'If strUsername = "gdai" Or strUsername = "jbutler" Then
    '                        '    Dim strProperties As New ArrayList()
    '                        '    For Each strProperty As String In de.Properties().PropertyNames
    '                        '        strProperties.Add(strProperty & "## " & de.Properties(strProperty).Value.ToString())
    '                        '    Next
    '                        '    Dim strTest = ""
    '                        '    strTest = "test"
    '                        'End If
    '                        dtUsers.Rows.Add(drUser)

    '                    End If

    '                Next
    '            End Using

    '        End Using
    '    End If

    '    Return dtUsers

    'End Function



    'Public Shared dtMembers_SyncLock As New DataTable()

    'Public Function AD_GetGroupList(ByVal SiteIdentifier_LDAP As String, ByVal PageSize As Integer) As DataTable

    '    Dim listActiveDirectoryColumnsToGet As String() = {"name", "description", "distinguishedName"}

    '    Dim dtGroups As New DataTable()
    '    For Each strColumnToGet As String In listActiveDirectoryColumnsToGet
    '        dtGroups.Columns.Add(New DataColumn(strColumnToGet))
    '    Next
    '    dtGroups.Columns.Add(New DataColumn("objectGUID"))
    '    'Dim ldapServerAndDomain As String = AD_GetCurrentDomain(SiteIdentifier_LDAP)
    '    Dim ldapServerAndDomain As String = SiteIdentifier_LDAP
    '    If ldapServerAndDomain.Length > 0 Then

    '        'Dim strLdapRootEntry As String = GetLdapRootEntry(False, ldapServerAndDomain)
    '        Dim strLdapRootEntry As String = ldapServerAndDomain
    '        Using deRootEntry As New DirectoryEntry(strLdapRootEntry)

    '            Using oSearcher As New DirectorySearcher(deRootEntry)
    '                Dim strSearcherFilter As String = "(objectClass=group)"
    '                oSearcher.Filter = "(&" & strSearcherFilter & ")"

    '                If PageSize >= 0 Then
    '                    oSearcher.PageSize = PageSize
    '                End If

    '                oSearcher.PropertiesToLoad.AddRange(listActiveDirectoryColumnsToGet)
    '                oSearcher.PropertiesToLoad.Add("objectGUID")

    '                'set timeout
    '                oSearcher.ClientTimeout = New TimeSpan(0, 0, 5, 0, 0)
    '                oSearcher.ServerPageTimeLimit = New TimeSpan(0, 0, 5, 0, 0)
    '                oSearcher.ServerTimeLimit = New TimeSpan(0, 0, 5, 0, 0)


    '                Dim oResults As SearchResultCollection = oSearcher.FindAll()
    '                For Each oResult As SearchResult In oResults

    '                    Dim de As DirectoryEntry = oResult.GetDirectoryEntry()
    '                    Dim drGroup As DataRow = dtGroups.NewRow()
    '                    For Each strColumnToGet As String In listActiveDirectoryColumnsToGet
    '                        drGroup(strColumnToGet) = de.Properties(strColumnToGet).Value
    '                    Next
    '                    drGroup("objectGUID") = de.Guid.ToString()


    '                    'TestCode to get all properties and memberOf information - Can be removed if not required
    '                    'Dim strProperties As New ArrayList()
    '                    'For Each strProperty As String In de.Properties().PropertyNames
    '                    '    strProperties.Add(strProperty & "## " & de.Properties(strProperty).Value.ToString())
    '                    'Next
    '                    'Dim objMemberOf As New ArrayList()
    '                    'For Each o As Object In de.Properties("memberOf").Value
    '                    '    objMemberOf.Add(o)
    '                    'Next

    '                    dtGroups.Rows.Add(drGroup)

    '                Next
    '            End Using

    '        End Using
    '    End If


    '    Return dtGroups

    'End Function


    ''Before we PERFORM the actual imports we check if we can import this site based on the last time this import was performed
    ''The state objet is necessary for a TimerCallback
    'Public Function PerformActiveDirectoryTransferProcess() As String
    '    Return ImportGroupsAndUsersWithoutThreading()
    'End Function

    'Private Function ImportGroupsAndUsersWithoutThreading() As String

    '    Return ImportActiveDirectoryGroupsAndUsers(0)
    'End Function

    ''Public Sub ImportGroupsAndUsersWithThreading()
    ''    Dim dtSite As DataTable = DBSite_GetSiteList()
    ''    For Each drSite As DataRow In dtSite.Rows
    ''        Dim intSiteID As Integer = Convert.ToInt32(drSite("ID"))
    ''        Dim threadImportUsers As New Thread(AddressOf ImportActiveDirectoryGroupsAndUsers)
    ''        threadImportUsers.IsBackground = True
    ''        threadImportUsers.Start(intSiteID)
    ''    Next
    ''End Sub

    'Private Function ImportActiveDirectoryGroupsAndUsers(ByVal SiteID As Integer) As String

    '    'If we do not have a most recent ServiceLog for this site, then we do the import
    '    Dim strError As String = String.Empty

    '    Dim boolPerformActiveDirectoryImport As Boolean = False
    '    Dim dtActiveDirectoryServiceLog_MostRecent As DataTable = LDAP.DBActiveDirectoryServiceLog_GetActiveDirectoryServiceLog_MostRecent_BySiteID(SiteID)
    '    If dtActiveDirectoryServiceLog_MostRecent.Rows.Count = 0 Then
    '        boolPerformActiveDirectoryImport = True
    '    Else
    '        'Get the Service_DateStart for this most recent Active Directory Service Log
    '        Dim drActiveDirectoryServiceLog_MostRecent As DataRow = dtActiveDirectoryServiceLog_MostRecent.Rows(0)

    '        Dim dtCurrentDate As DateTime = DateTime.Now
    '        Dim dtService_DateStart As DateTime = Convert.ToDateTime(drActiveDirectoryServiceLog_MostRecent("Service_DateStart"))

    '        'First we check the import has not been run in the last 15 minutes, due to a manual import (we don't want the import process to overlap, with the previous import process for this site)
    '        Dim tsTimeSpan_CurrentDateAndServiceDateStartDifference As TimeSpan = dtCurrentDate.Subtract(dtService_DateStart)
    '        If tsTimeSpan_CurrentDateAndServiceDateStartDifference.TotalMinutes > 15 Then

    '            'We do an Active Directory Import we get the Service_PerformImportInterval_InHours, and check if we are due for an import (defaulted to import every 12 hours)
    '            Dim intService_PerformImportInterval_InHours As Integer = 12
    '            If ((Not ConfigurationManager.AppSettings("ActiveDirectory_LDAP_PerformImportInterval_InHours") Is Nothing) AndAlso (ConfigurationManager.AppSettings("ActiveDirectory_LDAP_PerformImportInterval_InHours").ToString().Length > 0)) Then
    '                intService_PerformImportInterval_InHours = Convert.ToInt32(ConfigurationManager.AppSettings("ActiveDirectory_LDAP_PerformImportInterval_InHours").ToString())
    '            End If

    '            Dim intCurrentDate_HourLevel As DateTime = New DateTime(dtCurrentDate.Year, dtCurrentDate.Month, dtCurrentDate.Day, dtCurrentDate.Hour, 0, 0)
    '            Dim intService_DateStart_HourLevel As DateTime = New DateTime(dtService_DateStart.Year, dtService_DateStart.Month, dtService_DateStart.Day, dtService_DateStart.Hour, 0, 0)

    '            Dim tsTimeSpan_CurrentDateAndServiceDateStartDifference_HourLevel As TimeSpan = intCurrentDate_HourLevel.Subtract(intService_DateStart_HourLevel)
    '            If tsTimeSpan_CurrentDateAndServiceDateStartDifference_HourLevel.TotalHours >= intService_PerformImportInterval_InHours Then
    '                boolPerformActiveDirectoryImport = True
    '            End If

    '        End If

    '    End If

    '    'If we have managed to set the boolPerformActiveDirectoryImport as true, then perform the import
    '    If boolPerformActiveDirectoryImport Then
    '        strError = ImportActiveDirectoyGroupsAndUsers_PeformImport(SiteID)
    '    End If
    '    Return strError

    'End Function

    ''Gets all Groups from Active Directory and updates our richtemplate db with any new groups, then imports Active Directory Users, and their associated groups
    'Private Function ImportActiveDirectoyGroupsAndUsers_PeformImport(ByVal SiteID As Integer) As String
    '    'First we add an ActiveDirectory_ServiceLog entry, as we are starting our import
    '    Dim strError As String = String.Empty

    '    Dim guidActiveDirectoryServiceLogGUID As Guid = Guid.NewGuid()
    '    Dim dtService_DateStart As DateTime = DateTime.Now

    '    'ADD ENTRY
    '    Try
    '        LDAP.DBActiveDirectoryServiceLog_InsertActiveDirectoryServiceLog_Service_DateStart(guidActiveDirectoryServiceLogGUID, SiteID, dtService_DateStart)

    '        'Only import Active Directory Groups if the config setting ActiveDirectory_LDAP_ImportGroups is set to true
    '        Dim boolActiveDirectory_LDAP_ImportGroups As Boolean = False
    '        If ((Not ConfigurationManager.AppSettings("ActiveDirectory_LDAP_ImportGroups") Is Nothing) AndAlso (ConfigurationManager.AppSettings("ActiveDirectory_LDAP_ImportGroups").ToString().Length > 0)) Then
    '            boolActiveDirectory_LDAP_ImportGroups = Convert.ToBoolean(ConfigurationManager.AppSettings("ActiveDirectory_LDAP_ImportGroups"))
    '        End If

    '        'Only import Active Directory Users if the config setting ActiveDirectory_LDAP_ImportUsers is set to true
    '        Dim boolActiveDirectory_LDAP_ImportUsers As Boolean = False
    '        If ((Not ConfigurationManager.AppSettings("ActiveDirectory_LDAP_ImportUsers") Is Nothing) AndAlso (ConfigurationManager.AppSettings("ActiveDirectory_LDAP_ImportUsers").ToString().Length > 0)) Then
    '            boolActiveDirectory_LDAP_ImportUsers = Convert.ToBoolean(ConfigurationManager.AppSettings("ActiveDirectory_LDAP_ImportUsers"))
    '        End If

    '        Dim dsGroupsAndMemberGroups As New DataSet()

    '        'RPRIEST CHANGE
    '        If boolActiveDirectory_LDAP_ImportGroups Then
    '            Dim dtGroups As DataTable = LDAP.DBGroup_GetGroupList_Active()
    '            For Each drGroup As DataRow In dtGroups.Rows
    '                Dim intGroupID As Integer = Convert.ToInt32(drGroup("groupID"))
    '                LDAP.DBGroup_UpdateGroupActive(intGroupID, False)
    '            Next

    '            ImportActiveDirectoryGroups_PerformImport(dsGroupsAndMemberGroups, guidActiveDirectoryServiceLogGUID)
    '            'ImportActiveDirectoryUserGroups_PerformImport(dsGroupsAndMemberGroups, guidActiveDirectoryServiceLogGUID)
    '        Else
    '            'We are not importing groups, we set all active directory created groups to in-active, incase we have existing active directory groups that should now be removed
    '            Dim dtGroups As DataTable = LDAP.DBGroup_GetGroupList_Active()
    '            For Each drGroup As DataRow In dtGroups.Rows
    '                Dim intGroupID As Integer = Convert.ToInt32(drGroup("groupID"))
    '                LDAP.DBGroup_UpdateGroupActive(intGroupID, False)
    '            Next
    '        End If
    '        If boolActiveDirectory_LDAP_ImportUsers Then
    '            'RPRIEST CHANGE
    '            'We are not importing Users, we remove all site access to the richtemplate active directory created members
    '            Dim dtMembers As DataTable = LDAP.DBMember_GetMemberList_BySiteID(SiteID)
    '            For Each drMember As DataRow In dtMembers.Rows
    '                Dim intMemberID As Integer = Convert.ToInt32(drMember("ID"))
    '                LDAP.DBMember_DeleteMemberSiteAccess_ByMemberID(intMemberID)
    '            Next
    '            dsGroupsAndMemberGroups = ImportActiveDirectoryUsers_PerformImport(SiteID, guidActiveDirectoryServiceLogGUID, boolActiveDirectory_LDAP_ImportGroups)
    '        Else
    '            'We are not importing Users, we remove all site access to the richtemplate active directory created members
    '            Dim dtMembers As DataTable = LDAP.DBMember_GetMemberList_BySiteID(SiteID)
    '            For Each drMember As DataRow In dtMembers.Rows
    '                Dim intMemberID As Integer = Convert.ToInt32(drMember("ID"))
    '                LDAP.DBMember_DeleteMemberSiteAccess_ByMemberID(intMemberID)
    '            Next
    '        End If


    '        Dim dtService_DateEnd As DateTime = DateTime.Now
    '        LDAP.DBActiveDirectoryServiceLog_UpdateActiveDirectoryServiceLog_Service_DateEnd(guidActiveDirectoryServiceLogGUID, dtService_DateEnd)

    '    Catch importException As Exception
    '        LDAP.DBActiveDirectoryServiceLog_UpdateActiveDirectoryServiceLog_Service_ErrorMessage(guidActiveDirectoryServiceLogGUID, "Error in SiteID: " & SiteID.ToString() & " => " & importException.Message, importException.StackTrace)
    '        strError = importException.Message & "=>" & importException.StackTrace
    '    End Try
    '    Return strError
    'End Function

    ''Gets all Groups from Active Directory and updates our richtemplate db with any new groups
    'Public Sub ImportActiveDirectoryGroups_PerformImport(ByVal dsGroupsAndMemberGroups As DataSet, ByVal ActiveDirectoryServiceLogGUID As Guid)

    '    Dim dtGroups_DateStart As DateTime = DateTime.Now
    '    LDAP.DBActiveDirectoryServiceLog_UpdateActiveDirectoryServiceLog_GroupsImport_DateStart(ActiveDirectoryServiceLogGUID, dtGroups_DateStart)

    '    Dim intGroupsCreated As Integer = 0
    '    Dim intGroupsUpdated As Integer = 0
    '    'NOTE the number of Groups Deleted gets incremented if we set the richtemplate group to false, and then when trying to update this richtemplate group we can not find it in our list of active directory groups
    '    ' Such that their is a RichTemplate Group previously created from Active Directory, that we can no longer see in active directory
    '    Dim intGroupsDeleted As Integer = 0
    '    Dim dtSite As DataTable = LDAP.DBSite_GetSiteList()
    '    If dtSite.Rows.Count > 0 Then
    '        'First get all groups, and put them in a hash dictionary of groups
    '        Dim dtActiveDirectoryGroups As DataTable = AD_GetGroupList(ConfigurationManager.AppSettings("ActiveDirectory_LDAP_RootDSE"), LDAP_MAX_PAGE_SIZE) ' Any value less than 0 will return all Groups
    '        Dim dictGroups As New Dictionary(Of String, DataRow)
    '        For Each drActiveDirectoryGroup As DataRow In dtActiveDirectoryGroups.Rows
    '            Dim strActiveDirectory_Identifier As String = drActiveDirectoryGroup("objectGUID").ToString()
    '            If Not dictGroups.ContainsKey(strActiveDirectory_Identifier) Then
    '                dictGroups.Add(strActiveDirectory_Identifier, drActiveDirectoryGroup)
    '            End If
    '        Next


    '        Dim hashSites As New Hashtable()
    '        For Each drSite As DataRow In dtSite.Rows
    '            Dim intSiteID As Integer = Convert.ToInt32(drSite("ID"))
    '            Dim strSiteIdentifier_LDAP As String = String.Empty
    '            If Not drSite("SiteIdentifier_LDAP") Is DBNull.Value Then
    '                strSiteIdentifier_LDAP = drSite("SiteIdentifier_LDAP").ToString().ToLower()
    '            End If
    '            If (strSiteIdentifier_LDAP.Length > 0 AndAlso (Not hashSites.ContainsKey(strSiteIdentifier_LDAP))) Then
    '                hashSites.Add(strSiteIdentifier_LDAP, drSite)
    '            End If
    '        Next
    '        'Process Each Group individually, so we don't disrupt groups, this way we can also find out which groups are no longer need and thus can mark it as active = false
    '        'Once this is complete, the only elements in our 'listGroups' should be new groups, that we should insert

    '        '===>>>> For this site, we'll use siteID 0 for all groups, with 'AvailableToAllSites = 1

    '        Dim dtGroups As DataTable = LDAP.DBGroup_GetGroupList()
    '        For Each drGroup As DataRow In dtGroups.Rows
    '            'Ensure the only groups we process are in fact groups with an ActiveDirectory_Identifier
    '            If Not drGroup("ActiveDirectory_Identifier") Is DBNull.Value Then

    '                Dim intGroupID As Integer = Convert.ToInt32(drGroup("GroupID"))
    '                Dim strActiveDirectory_Identifier As String = drGroup("ActiveDirectory_Identifier")
    '                Dim boolGroupPreviouslyActive As Boolean = Convert.ToInt32(drGroup("groupActive"))

    '                'RPRIEST CHANGE
    '                'First set the group to active = false, once processed we set it back to active
    '                'LDAP.DBGroup_UpdateGroupActive(intGroupID, False)
    '                'Now check if we have this group in our list of recently recieved active directory groups
    '                If dictGroups.ContainsKey(strActiveDirectory_Identifier) Then
    '                    ' the group we are processing is in a the list of recieved groups, so we should keep this
    '                    Dim drActiveDirectoryGroup As DataRow = dictGroups.Item(strActiveDirectory_Identifier)

    '                    Dim strGroupName As String = drActiveDirectoryGroup("name").ToString().ToLower()
    '                    Dim iSiteIdentifierIndex As Integer = strGroupName.IndexOf("_")
    '                    If iSiteIdentifierIndex > 0 Then

    '                        'Trim the groups so we get the site code prefix
    '                        Dim strSiteIdentifier As String = strGroupName.Substring(0, iSiteIdentifierIndex + 1)

    '                        If hashSites.ContainsKey(strSiteIdentifier.ToLower()) Then
    '                            Dim drSite As DataRow = hashSites(strSiteIdentifier.ToLower())

    '                            Dim iSiteID As Integer = Convert.ToInt32(drSite("ID"))

    '                            Dim strGroupDescription As String = drActiveDirectoryGroup("Description").ToString()
    '                            Dim boolAvailableToAllSites As Boolean = True ' By Default all groups are available to all sites
    '                            Dim boolActive As Boolean = True ' By Default all found groups are ACTIVE
    '                            Dim strGroup_LdapDistinguishedName As String = drActiveDirectoryGroup("DistinguishedName").ToString()

    '                            LDAP.DBGroup_UpdateGroup(intGroupID, strGroupName, strGroupDescription, boolAvailableToAllSites, boolActive, strGroup_LdapDistinguishedName)
    '                            'Since we have just updated an existing richtemplate Group, we increment the groupsUpdated integer
    '                            intGroupsUpdated = intGroupsUpdated + 1
    '                        End If
    '                    End If

    '                    'Now remove it from our list, as we no longer need this, and will speed up hash table lookup
    '                    dictGroups.Remove(strActiveDirectory_Identifier)
    '                Else
    '                    'This group exists in our richtemplate list of Active Directory Groups, but we can no longer find this in active directory.
    '                    ' If this group was previously active, then it is nolonger active, meaning this iteration of the import caused this group to be in-active, hence this group was deleted from active directory since last Active Directory Import
    '                    If boolGroupPreviouslyActive Then
    '                        intGroupsDeleted = intGroupsDeleted + 1
    '                    End If

    '                End If
    '            End If

    '        Next

    '        'RPRIEST CHANGE
    '        'Dim dictGroups_FoundGroupsFromMembers As New Dictionary(Of String, Integer)
    '        'Dim dtGroups_FoundGroupsFromMembers As DataTable = dsGroupsAndMemberGroups.Tables("Groups")
    '        'For Each drGroups_FoundGroupsFromMembers As DataRow In dtGroups_FoundGroupsFromMembers.Rows
    '        '    Dim strGroup_FromGroupsFromMembers As String = drGroups_FoundGroupsFromMembers("GroupName").ToString().ToLower()
    '        '    Dim intSiteID_FromGroupsFromMembers As String = drGroups_FoundGroupsFromMembers("SiteID")
    '        '    If Not dictGroups_FoundGroupsFromMembers.ContainsKey(strGroup_FromGroupsFromMembers) Then
    '        '        dictGroups_FoundGroupsFromMembers.Add(strGroup_FromGroupsFromMembers, intSiteID_FromGroupsFromMembers)
    '        '    End If
    '        'Next
    '        'Now the only groups in our ActiveDirectory Hashtable are the groups we do not yet have in our richtemplate, so we go through each group and add them to our richtemplatedb
    '        For Each kvGroup As KeyValuePair(Of String, DataRow) In dictGroups
    '            Dim drActiveDirectoryGroup As DataRow = kvGroup.Value

    '            Dim strActiveDirectory_Identifier As String = kvGroup.Key

    '            Dim strGroupName As String = drActiveDirectoryGroup("name").ToString().ToLower()

    '            Dim iSiteIdentifierIndex As Integer = strGroupName.IndexOf("_")
    '            If iSiteIdentifierIndex > 0 Then
    '                'Trim the groups so we get the site code prefix
    '                Dim strSiteIdentifier As String = strGroupName.Substring(0, iSiteIdentifierIndex + 1)

    '                If hashSites.ContainsKey(strSiteIdentifier.ToLower()) Then
    '                    Dim drSite As DataRow = hashSites(strSiteIdentifier.ToLower())

    '                    Dim iSiteID As Integer = Convert.ToInt32(drSite("ID"))

    '                    Dim strGroupDescription As String = drActiveDirectoryGroup("Description").ToString()
    '                    Dim strGroup_LdapDistinguishedName As String = drActiveDirectoryGroup("distinguishedName").ToString()

    '                    Dim boolAvailableToAllSites As Boolean = True ' By Default all groups are available to all sites
    '                    Dim boolActive As Boolean = True ' By Default all found groups are ACTIVE
    '                    'RichardP CHANGE
    '                    'If dictGroups_FoundGroupsFromMembers.ContainsKey(strGroupName) Then

    '                    LDAP.DBGroup_InsertGroup(strGroupName, strGroupDescription, iSiteID, boolAvailableToAllSites, boolActive, strActiveDirectory_Identifier, strGroup_LdapDistinguishedName)

    '                    'Since we have just inserted a new group for this site, we increment the groupsInserted integer
    '                    intGroupsCreated = intGroupsCreated + 1
    '                    'End If
    '                End If

    '            End If

    '        Next
    '    End If

    '    Dim dtGroups_DateEnd As DateTime = DateTime.Now
    '    LDAP.DBActiveDirectoryServiceLog_UpdateActiveDirectoryServiceLog_GroupsImport_DateEnd(ActiveDirectoryServiceLogGUID, dtGroups_DateEnd, intGroupsCreated, intGroupsUpdated, intGroupsDeleted)
    'End Sub


    'Private Function ImportActiveDirectoryUsers_PerformImport(ByVal SiteID_NotUsed As Integer, ByVal ActiveDirectoryServiceLogGUID As Guid, ByVal ActiveDirectory_LDAP_ImportGroups As Boolean) As DataSet

    '    Dim dtUsers_DateStart As DateTime = DateTime.Now
    '    DBActiveDirectoryServiceLog_UpdateActiveDirectoryServiceLog_UsersImport_DateStart(ActiveDirectoryServiceLogGUID, dtUsers_DateStart)

    '    Dim intUsersCreated As Integer = 0
    '    Dim intUsersUpdated As Integer = 0
    '    'NOTE the number of Users Deleted gets incremented if we remove the richtemplate access for this member, and then when trying to update this richtemplate member we can not find it in our list of active directory users
    '    ' Such that their is a RichTemplate Member previously created from Active Directory, that we can no longer see in active directory
    '    Dim intUsersDeleted As Integer = 0


    '    'List of Groups in the ACtive directory, which we populate and return for groups import
    '    Dim dsGroupsAndMemberGroups As New DataSet()

    '    'RPRIEST CHANGE
    '    'Dim dtGroups As New DataTable("Groups")
    '    'dtGroups.Columns.Add(New DataColumn("siteID"))
    '    'dtGroups.Columns.Add(New DataColumn("groupName"))
    '    'RPRIEST CHANGE
    '    'Dim dtMemberGroups As New DataTable("MemberGroups")
    '    'dtMemberGroups.Columns.Add(New DataColumn("MemberID"))
    '    'dtMemberGroups.Columns.Add(New DataColumn("groupName"))

    '    'dsGroupsAndMemberGroups.Tables.Add(dtGroups)
    '    'dsGroupsAndMemberGroups.Tables.Add(dtMemberGroups)


    '    Dim hashGroups_FoundGroups As New Hashtable()
    '    Dim dtGroups As DataTable = LDAP.DBGroup_GetGroupList()
    '    For Each drGroup As DataRow In dtGroups.Rows
    '        Dim strGroupName As String = drGroup("GroupName").ToString().ToLower()
    '        If Not hashGroups_FoundGroups.Contains(strGroupName) Then
    '            hashGroups_FoundGroups.Add(strGroupName, drGroup)
    '        End If
    '    Next
    '    'Get the State List, and CountryList, so we can attach an Active Directory User to a stateID and countryID
    '    Dim hashStates As New Hashtable()
    '    Dim dtStates As DataTable = LDAP.DBState_GetStateList()
    '    For Each drState As DataRow In dtStates.Rows
    '        Dim strStateAbbreviation As String = drState("StateAbbreviation").ToString()
    '        If Not hashStates.Contains(strStateAbbreviation) Then
    '            hashStates.Add(strStateAbbreviation, drState)
    '        End If
    '    Next

    '    Dim hashCountries As New Hashtable()
    '    Dim dtCountries As DataTable = LDAP.DBCountry_GetCountryList()
    '    For Each drCountry As DataRow In dtCountries.Rows
    '        Dim strCountryName As String = drCountry("CountryName").ToString()
    '        If Not hashCountries.Contains(strCountryName) Then
    '            hashCountries.Add(strCountryName, drCountry)
    '        End If
    '    Next

    '    Dim hashSites As New Hashtable()
    '    Dim dtSites As DataTable = LDAP.DBSite_GetSiteList()
    '    For Each drSite As DataRow In dtSites.Rows
    '        Dim intSiteID As Integer = Convert.ToInt32(drSite("ID"))
    '        Dim strSiteIdentifier_LDAP As String = String.Empty
    '        If Not drSite("SiteIdentifier_LDAP") Is DBNull.Value Then
    '            strSiteIdentifier_LDAP = drSite("SiteIdentifier_LDAP").ToString().ToLower()
    '        End If
    '        If (strSiteIdentifier_LDAP.Length > 0 AndAlso (Not hashSites.ContainsKey(strSiteIdentifier_LDAP))) Then
    '            hashSites.Add(strSiteIdentifier_LDAP, drSite)
    '        End If
    '    Next

    '    If hashSites.Keys.Count > 0 Then


    '        'Then get all users, and put them in a List of strings
    '        Dim dtActiveDirectoryUsers As DataTable = AD_GetUserList(ConfigurationManager.AppSettings("ActiveDirectory_LDAP_RootDSE"), LDAP_MAX_PAGE_SIZE) ' Any value less than 0 will return all Groups

    '        Dim dictUsers As New Dictionary(Of String, DataRow)
    '        For Each drActiveDirectoryUser As DataRow In dtActiveDirectoryUsers.Rows
    '            Dim strActiveDirectory_Identifier As String = drActiveDirectoryUser("objectGUID").ToString()

    '            If Not dictUsers.ContainsKey(strActiveDirectory_Identifier) Then
    '                dictUsers.Add(strActiveDirectory_Identifier, drActiveDirectoryUser)
    '            End If
    '        Next

    '        'Process Each User individually, so we don't disrupt users, this way we can also find out which users no longer need access to this site, and include no site access
    '        'Once this is complete, the only elements in our 'listUsers' should be new users for this site, which we then check if the user already exists or needs to be updated
    '        Dim dtMembers As DataTable = LDAP.DBMember_GetMemberList()
    '        For Each drMember As DataRow In dtMembers.Rows

    '            'This is just to ensure the drMember we have was in fact created with an ActiveDirectory_Identifier
    '            If Not drMember("ActiveDirectory_Identifier") Is Nothing Then

    '                Dim intMemberID As Integer = Convert.ToInt32(drMember("ID"))

    '                'First remove them from this site and remove their site access for this site
    '                LDAP.DBMember_DeleteMemberSiteAccess_ByMemberID(intMemberID)

    '                'As we have a member for this site, it must have been added by active directory, if we can no longer find this member in this particular active directory site then the Active Directory User must have been removed from this site
    '                Dim boolUserPreviouslyActive As Boolean = True

    '                'Now check if we have this member in our list of recently recieved active directory users
    '                Dim strActiveDirectory_Identifier As String = drMember("ActiveDirectory_Identifier").ToString()
    '                If dictUsers.ContainsKey(strActiveDirectory_Identifier) Then
    '                    Dim drActiveDirectoryUser As DataRow = dictUsers.Item(strActiveDirectory_Identifier)

    '                    'Now we have added site access and group access to this member, we now update some fields for this member from Active Directory Specific Fields
    '                    Dim strUsername As String = If(drActiveDirectoryUser("sAMAccountName") Is DBNull.Value, String.Empty, drActiveDirectoryUser("sAMAccountName").ToString())
    '                    Dim strFirstName As String = If(drActiveDirectoryUser("givenName") Is DBNull.Value, String.Empty, drActiveDirectoryUser("givenName").ToString())
    '                    Dim strLastName As String = If(drActiveDirectoryUser("sn") Is DBNull.Value, String.Empty, drActiveDirectoryUser("sn").ToString())
    '                    Dim strEmailAddress As String = If(drActiveDirectoryUser("mail") Is DBNull.Value, String.Empty, drActiveDirectoryUser("mail").ToString())

    '                    Dim strCompanyOffice As String = If(drActiveDirectoryUser("physicalDeliveryOfficeName") Is DBNull.Value, String.Empty, drActiveDirectoryUser("physicalDeliveryOfficeName").ToString())
    '                    Dim strTelephone As String = If(drActiveDirectoryUser("telephonenumber") Is DBNull.Value, String.Empty, drActiveDirectoryUser("telephonenumber").ToString())
    '                    Dim strStreetAddress As String = If(drActiveDirectoryUser("streetAddress") Is DBNull.Value, String.Empty, drActiveDirectoryUser("streetAddress").ToString())
    '                    Dim strCity As String = If(drActiveDirectoryUser("l") Is DBNull.Value, String.Empty, drActiveDirectoryUser("l").ToString())
    '                    Dim strStateAbbreviation As String = If(drActiveDirectoryUser("st") Is DBNull.Value, String.Empty, drActiveDirectoryUser("st").ToString())
    '                    Dim strZipCode As String = If(drActiveDirectoryUser("postalCode") Is DBNull.Value, String.Empty, drActiveDirectoryUser("postalCode").ToString())
    '                    Dim strCountryName As String = String.Empty

    '                    'From here get the stateID and CountryID
    '                    Dim intStateID As Integer = 0 ' Default the stateID to 0 (Not-Available)
    '                    If hashStates.Contains(strStateAbbreviation) Then
    '                        Dim drState As DataRow = hashStates.Item(strStateAbbreviation)
    '                        intStateID = Convert.ToInt32(drState("ID"))
    '                    End If

    '                    Dim intCountryID As Integer = Integer.MinValue ' Default the countryID to MinInteger Value
    '                    If hashCountries.Contains(strCountryName) Then
    '                        Dim drCountry As DataRow = hashCountries.Item(strCountryName)
    '                        intCountryID = Convert.ToInt32(drCountry("countryID"))
    '                    End If

    '                    Dim strCompany As String = If(drActiveDirectoryUser("company") Is DBNull.Value, String.Empty, drActiveDirectoryUser("company").ToString())
    '                    Dim strCompanyDepartment As String = If(drActiveDirectoryUser("department") Is DBNull.Value, String.Empty, drActiveDirectoryUser("department").ToString())
    '                    Dim strJobTitle As String = If(drActiveDirectoryUser("title") Is DBNull.Value, String.Empty, drActiveDirectoryUser("title").ToString())

    '                    Dim strActiveDirectory_SID As String = drActiveDirectoryUser("objectSID")


    '                    'If we are also importing Active Directory Groups Then we delete and re-add the groups for this user
    '                    'Now we want to ensure all the members groups are most recent, so first delete their current active directory created groups, then add their groups back in
    '                    Dim listSiteID_GroupBased As New List(Of Integer)

    '                    If Not drActiveDirectoryUser("groupIdList") Is DBNull.Value Then
    '                        Dim strMemberOf As String = drActiveDirectoryUser("groupIdList").ToString()
    '                        If strMemberOf.Length > 0 Then
    '                            Dim strMemberOfList As String() = strMemberOf.Split(",")
    '                            For Each strGroupName As String In strMemberOfList
    '                                Dim iSiteIdentifierIndex As Integer = strGroupName.IndexOf("_")
    '                                If iSiteIdentifierIndex > 0 Then

    '                                    'Trim the groups so we get the site code prefix
    '                                    Dim strSiteIdentifier As String = strGroupName.Substring(0, iSiteIdentifierIndex + 1)

    '                                    If hashSites.ContainsKey(strSiteIdentifier.ToLower()) Then
    '                                        Dim drSite As DataRow = hashSites(strSiteIdentifier.ToLower())

    '                                        Dim iSiteID As Integer = Convert.ToInt32(drSite("ID"))
    '                                        If Not listSiteID_GroupBased.Contains(iSiteID) Then
    '                                            listSiteID_GroupBased.Add(iSiteID)

    '                                        End If
    '                                    End If
    '                                End If

    '                            Next
    '                        End If
    '                    End If


    '                    If listSiteID_GroupBased.Count > 0 Then

    '                        LDAP.DBMember_UpdateMember(intMemberID, strFirstName, strLastName, strEmailAddress, strUsername, strStreetAddress, strCity, intStateID, strZipCode, intCountryID, strTelephone, strCompany, strCompanyDepartment, strJobTitle, strCompanyOffice, strActiveDirectory_SID)

    '                        'This member is in our list of active directory users for this site, so add their site access
    '                        For Each iSiteID_GroupBased As Integer In listSiteID_GroupBased
    '                            LDAP.DBMember_InsertMemberSiteAccess(intMemberID, iSiteID_GroupBased)
    '                        Next

    '                        'Now go though all the member groups again, this time adding the groups to the hashGroups_FoundGroups
    '                        'RPRIEST CHANGE
    '                        If ActiveDirectory_LDAP_ImportGroups Then
    '                            If Not drActiveDirectoryUser("groupIdList") Is DBNull.Value Then
    '                                Dim strMemberOf As String = drActiveDirectoryUser("groupIdList").ToString()
    '                                If strMemberOf.Length > 0 Then
    '                                    Dim strMemberOfList As String() = strMemberOf.Split(",")
    '                                    For Each strGroupName As String In strMemberOfList
    '                                        'If Not hashGroups_FoundGroups.ContainsKey(intSiteID_GroupBased & "##" & strGroupName.ToLower()) Then
    '                                        'hashGroups_FoundGroups.Add(intSiteID_GroupBased & "##" & strGroupName.ToLower(), intSiteID_GroupBased)
    '                                        strGroupName = strGroupName.ToLower()
    '                                        If hashGroups_FoundGroups.Contains(strGroupName) Then
    '                                            Dim drGroup As DataRow = hashGroups_FoundGroups(strGroupName)
    '                                            Dim iGroupID As Integer = Convert.ToInt32(drGroup("GroupID"))
    '                                            LDAP.DBMember_InsertMemberGroup(Guid.NewGuid(), intMemberID, iGroupID)
    '                                        End If

    '                                        'RPRIEST CHANGE
    '                                        'Dim iSiteIdentifierIndex As Integer = strGroupName.IndexOf("_")
    '                                        'If iSiteIdentifierIndex > 0 Then
    '                                        '    'Trim the groups so we get the site code prefix
    '                                        '    Dim strSiteIdentifier As String = strGroupName.Substring(0, iSiteIdentifierIndex + 1)

    '                                        '    If hashSites.ContainsKey(strSiteIdentifier.ToLower()) Then
    '                                        '        Dim drSite As DataRow = hashSites(strSiteIdentifier.ToLower())

    '                                        '        Dim iSiteID As Integer = Convert.ToInt32(drSite("ID"))
    '                                        '        If Not hashGroups_FoundGroups.ContainsKey(strGroupName) Then
    '                                        '            hashGroups_FoundGroups.Add(strGroupName, strGroupName.ToLower())

    '                                        '            'add this group to our dtGroup Table
    '                                        '            Dim drGroup As DataRow = dtGroups.NewRow
    '                                        '            drGroup("siteID") = iSiteID.ToString()
    '                                        '            drGroup("groupName") = strGroupName
    '                                        '            dtGroups.Rows.Add(drGroup)


    '                                        '        End If

    '                                        '        'add this group to our dtMemberGroup Table
    '                                        '        Dim drMemberGroup As DataRow = dtMemberGroups.NewRow
    '                                        '        drMemberGroup("memberID") = intMemberID
    '                                        '        drMemberGroup("groupName") = strGroupName
    '                                        '        dtMemberGroups.Rows.Add(drMemberGroup)
    '                                        '    End If
    '                                        'End If

    '                                    Next

    '                                End If
    '                            End If

    '                        End If


    '                        'Since we have this member in our richtemplate list of members for this site, and we found this member in Active Directory, we are updating the member
    '                        intUsersUpdated = intUsersUpdated + 1

    '                        'Finally remove this member from the member hashtable, such that once we have gone through all members, 
    '                        'the only ActiveDirectory Users left in this hash table are the ActiveDirectory Users that need to be either inserted or if already exist for another, then updated
    '                        dictUsers.Remove(strActiveDirectory_Identifier)
    '                    End If
    '                Else
    '                    'This group exists in our richtemplate list of Active Directory Groups, but we can no longer find this in active directory.
    '                    ' If this group was previously active, then it is nolonger active, meaning this iteration of the import caused this group to be in-active, hence this group was deleted from active directory since last Active Directory Import
    '                    If boolUserPreviouslyActive Then
    '                        intUsersDeleted = intUsersDeleted + 1
    '                    End If

    '                End If
    '            End If

    '        Next

    '        'Now the only Users in our ActiveDirectory Hashtable are the users we do not yet have in our richtemplate WITH THIS SPECIFIC SITE ACCESS
    '        'so we go through each user and add them to our richtemplatedb OR if they already exist, we update the current user so it has access to this site
    '        'Either way the user did not exist for this site previously, Otherwise we would have had it in our list of Members for tihs site in the prevous For BLock
    '        'So we are ADDING a user to this site
    '        For Each kvUser As KeyValuePair(Of String, DataRow) In dictUsers
    '            Dim strActiveDirectory_Identifier As String = kvUser.Key
    '            Dim drActiveDirectoryUser As DataRow = kvUser.Value

    '            Dim intMemberID As Integer = Integer.MinValue

    '            'Get User Fields for this member from Active Directory Specific Fields
    '            Dim strUsername As String = If(drActiveDirectoryUser("sAMAccountName") Is DBNull.Value, String.Empty, drActiveDirectoryUser("sAMAccountName").ToString())
    '            Dim strFirstName As String = If(drActiveDirectoryUser("givenName") Is DBNull.Value, String.Empty, drActiveDirectoryUser("givenName").ToString())
    '            Dim strLastName As String = If(drActiveDirectoryUser("sn") Is DBNull.Value, String.Empty, drActiveDirectoryUser("sn").ToString())
    '            Dim strEmailAddress As String = If(drActiveDirectoryUser("mail") Is DBNull.Value, String.Empty, drActiveDirectoryUser("mail").ToString())

    '            Dim strCompanyOffice As String = If(drActiveDirectoryUser("physicalDeliveryOfficeName") Is DBNull.Value, String.Empty, drActiveDirectoryUser("physicalDeliveryOfficeName").ToString())
    '            Dim strTelephone As String = If(drActiveDirectoryUser("telephonenumber") Is DBNull.Value, String.Empty, drActiveDirectoryUser("telephonenumber").ToString())
    '            Dim strStreetAddress As String = If(drActiveDirectoryUser("streetAddress") Is DBNull.Value, String.Empty, drActiveDirectoryUser("streetAddress").ToString())
    '            Dim strCity As String = If(drActiveDirectoryUser("l") Is DBNull.Value, String.Empty, drActiveDirectoryUser("l").ToString())
    '            Dim strStateAbbreviation As String = If(drActiveDirectoryUser("st") Is DBNull.Value, String.Empty, drActiveDirectoryUser("st").ToString())
    '            Dim strZipCode As String = If(drActiveDirectoryUser("postalCode") Is DBNull.Value, String.Empty, drActiveDirectoryUser("postalCode").ToString())
    '            Dim strCountryName As String = String.Empty

    '            'From here get the stateID and CountryID
    '            Dim intStateID As Integer = 0 ' Default the stateID to 0 (Not-Available)
    '            If hashStates.Contains(strStateAbbreviation) Then
    '                Dim drState As DataRow = hashStates.Item(strStateAbbreviation)
    '                intStateID = Convert.ToInt32(drState("ID"))
    '            End If

    '            Dim intCountryID As Integer = Integer.MinValue ' Default the countryID to MinInteger Value
    '            If hashCountries.Contains(strCountryName) Then
    '                Dim drCountry As DataRow = hashCountries.Item(strCountryName)
    '                intCountryID = Convert.ToInt32(drCountry("countryID"))
    '            End If

    '            Dim strCompany As String = If(drActiveDirectoryUser("company") Is DBNull.Value, String.Empty, drActiveDirectoryUser("company").ToString())
    '            Dim strCompanyDepartment As String = If(drActiveDirectoryUser("department") Is DBNull.Value, String.Empty, drActiveDirectoryUser("department").ToString())
    '            Dim strJobTitle As String = If(drActiveDirectoryUser("title") Is DBNull.Value, String.Empty, drActiveDirectoryUser("title").ToString())

    '            Dim strRandomPassword As String = Guid.NewGuid().ToString() 'This is just a random guid, that we substitute for the members password, we will never use this field however.

    '            Dim strActiveDirectory_SID As String = drActiveDirectoryUser("objectSID")

    '            Dim iLanguageID_Default As Integer = Integer.MinValue
    '            Dim listSiteID_GroupBased As New List(Of Integer)
    '            If Not drActiveDirectoryUser("groupIdList") Is DBNull.Value Then
    '                Dim strMemberOf As String = drActiveDirectoryUser("groupIdList").ToString()
    '                If strMemberOf.Length > 0 Then
    '                    Dim strMemberOfList As String() = strMemberOf.Split(",")
    '                    For Each strGroupName As String In strMemberOfList
    '                        Dim iSiteIdentifierIndex As Integer = strGroupName.IndexOf("_")
    '                        If iSiteIdentifierIndex > 0 Then

    '                            'Trim the groups so we get the site code prefix
    '                            Dim strSiteIdentifier As String = strGroupName.Substring(0, iSiteIdentifierIndex + 1)

    '                            If hashSites.ContainsKey(strSiteIdentifier.ToLower()) Then
    '                                Dim drSite As DataRow = hashSites(strSiteIdentifier.ToLower())

    '                                Dim iSiteID As Integer = Convert.ToInt32(drSite("ID"))
    '                                If Not listSiteID_GroupBased.Contains(iSiteID) Then
    '                                    listSiteID_GroupBased.Add(iSiteID)

    '                                    'Get the default language for this site
    '                                    If iLanguageID_Default = Integer.MinValue Then
    '                                        If Not drSite("LanguageID_Default") Is DBNull.Value Then
    '                                            iLanguageID_Default = Convert.ToInt32(drSite("LanguageID_Default"))
    '                                        End If
    '                                    End If

    '                                End If
    '                            End If
    '                        End If
    '                    Next
    '                End If
    '            End If

    '            If listSiteID_GroupBased.Count > 0 Then


    '                'Now we either insert this member or update this member depending on if the member already exists in our db or not
    '                SyncLock dtMembers_SyncLock
    '                    dtMembers_SyncLock = LDAP.DBMember_GetMember_ByActiveDirectory_Identifier(strActiveDirectory_Identifier)
    '                    If dtMembers_SyncLock.Rows.Count > 0 Then
    '                        Dim drMember As DataRow = dtMembers_SyncLock.Rows(0)

    '                        intMemberID = Convert.ToInt32(drMember("ID"))
    '                        LDAP.DBMember_UpdateMember(intMemberID, strFirstName, strLastName, strEmailAddress, strUsername, strStreetAddress, strCity, intStateID, strZipCode, intCountryID, strTelephone, strCompany, strCompanyDepartment, strJobTitle, strCompanyOffice, strActiveDirectory_SID)
    '                    Else
    '                        'Now we add site access for this member
    '                        Dim dtDateCreated As DateTime = DateTime.Now

    '                        intMemberID = LDAP.DBMember_InsertMember(strFirstName, strLastName, strEmailAddress, strUsername, strRandomPassword, iLanguageID_Default, strStreetAddress, strCity, intStateID, strZipCode, intCountryID, strTelephone, dtDateCreated, True, strCompany, strCompanyDepartment, strJobTitle, strCompanyOffice, strActiveDirectory_Identifier, strActiveDirectory_SID)
    '                    End If
    '                End SyncLock

    '                'Now we this member in our RichTemplate Members table, we need to assocaite sites and groups to this member

    '                'The Member already exists, but must have access to another site and not this site
    '                ' so INSERT this member's site access to include this site, get their groups, and update some fields for this member from Active Directory Specific Fields
    '                'This member is in our list of active directory users for this site, so add their site access, NO NEED TO DELETE first as we are dealing with Active Directory Users, that are already members just not with this site access
    '                'This member is in our list of active directory users for this site, so add their site access
    '                For Each iSiteID_GroupBased As Integer In listSiteID_GroupBased
    '                    LDAP.DBMember_InsertMemberSiteAccess(intMemberID, iSiteID_GroupBased)
    '                Next

    '                'Now go though all the member groups again, this time adding the groups to the hashGroups_FoundGroups
    '                If ActiveDirectory_LDAP_ImportGroups Then
    '                    If Not drActiveDirectoryUser("groupIdList") Is DBNull.Value Then
    '                        Dim strMemberOf As String = drActiveDirectoryUser("groupIdList").ToString()
    '                        If strMemberOf.Length > 0 Then
    '                            Dim strMemberOfList As String() = strMemberOf.Split(",")
    '                            For Each strGroupName As String In strMemberOfList
    '                                strGroupName = strGroupName.ToLower()
    '                                If hashGroups_FoundGroups.Contains(strGroupName) Then
    '                                    Dim drGroup As DataRow = hashGroups_FoundGroups(strGroupName)
    '                                    Dim iGroupID As Integer = Convert.ToInt32(drGroup("GroupID"))
    '                                    LDAP.DBMember_InsertMemberGroup(Guid.NewGuid(), intMemberID, iGroupID)
    '                                End If
    '                                'RPRIEST CHANGE
    '                                'Dim iSiteIdentifierIndex As Integer = strGroupName.IndexOf("_")
    '                                'If iSiteIdentifierIndex > 0 Then
    '                                '    'Trim the groups so we get the site code prefix
    '                                '    Dim strSiteIdentifier As String = strGroupName.Substring(0, iSiteIdentifierIndex + 1)

    '                                '    If hashSites.ContainsKey(strSiteIdentifier.ToLower()) Then
    '                                '        Dim drSite As DataRow = hashSites(strSiteIdentifier.ToLower())

    '                                '        Dim iSiteID As Integer = Convert.ToInt32(drSite("ID"))
    '                                '        If Not hashGroups_FoundGroups.ContainsKey(strGroupName) Then
    '                                '            hashGroups_FoundGroups.Add(strGroupName, strGroupName.ToLower())

    '                                '            'add this group to our dtGroup Table
    '                                '            Dim drGroup As DataRow = dtGroups.NewRow
    '                                '            drGroup("siteID") = iSiteID.ToString()
    '                                '            drGroup("groupName") = strGroupName
    '                                '            dtGroups.Rows.Add(drGroup)


    '                                '        End If

    '                                '        'add this group to our dtMemberGroup Table
    '                                '        Dim drMemberGroup As DataRow = dtMemberGroups.NewRow
    '                                '        drMemberGroup("memberID") = intMemberID
    '                                '        drMemberGroup("groupName") = strGroupName
    '                                '        dtMemberGroups.Rows.Add(drMemberGroup)
    '                                '    End If
    '                                'End If

    '                            Next
    '                        End If
    '                    End If
    '                End If

    '                intUsersCreated = intUsersCreated + 1
    '            End If

    '        Next

    '    End If

    '    Dim dtUsers_DateEnd As DateTime = DateTime.Now
    '    DBActiveDirectoryServiceLog_UpdateActiveDirectoryServiceLog_UsersImport_DateEnd(ActiveDirectoryServiceLogGUID, dtUsers_DateEnd, intUsersCreated, intUsersUpdated, intUsersDeleted)

    '    Return dsGroupsAndMemberGroups
    'End Function

    ''Gets all Groups from Active Directory and updates our richtemplate db with any new groups
    'Public Sub ImportActiveDirectoryUserGroups_PerformImport(ByVal dsGroupsAndMemberGroups As DataSet, ByVal ActiveDirectoryServiceLogGUID As Guid)

    '    'Populate all our groups, and their groupID
    '    Dim dtGroups As DataTable = LDAP.DBGroup_GetGroupList()
    '    Dim dictGroups As New Dictionary(Of String, Integer)
    '    For Each drGroup As DataRow In dtGroups.Rows
    '        Dim intGroupID As String = Convert.ToInt32(drGroup("groupID"))
    '        Dim strGroupName As String = drGroup("groupName").ToString().ToLower()
    '        If Not dictGroups.ContainsKey(strGroupName) Then
    '            dictGroups.Add(strGroupName, intGroupID)
    '        End If
    '    Next

    '    Dim dtMembers As DataTable = LDAP.DBMember_GetMemberList()
    '    For Each drMember As DataRow In dtMembers.Rows
    '        Dim intMemberID As Integer = Convert.ToInt32(drMember("ID"))

    '        'First remove all member groups
    '        LDAP.DBMember_DeleteMemberGroups_ByMemberID(0)
    '    Next

    '    'Go through all groups and using the members groupName, get the groupID and add it to our Members_Group table
    '    Dim dtMemberGroups As DataTable = dsGroupsAndMemberGroups.Tables("MemberGroups")
    '    For Each drMemberGroup As DataRow In dtMemberGroups.Rows
    '        Dim intMemberID As Integer = Convert.ToInt32(drMemberGroup("MemberID"))
    '        Dim strGroupName As String = drMemberGroup("groupName").ToString().ToLower()

    '        If dictGroups.ContainsKey(strGroupName) Then
    '            Dim intGroupID As Integer = dictGroups(strGroupName)

    '            'Insert member group
    '            LDAP.DBMember_InsertMemberGroup(Guid.NewGuid(), intMemberID, intGroupID)
    '        End If

    '    Next


    'End Sub

    'Private Sub DBActiveDirectoryServiceLog_UpdateActiveDirectoryServiceLog_Service_ErrorMessage(guidActiveDirectoryServiceLogGUID As Guid, p2 As String, p3 As String)
    '    Throw New NotImplementedException
    'End Sub

End Class
