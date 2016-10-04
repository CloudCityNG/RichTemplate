Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class WebInfoDAL

#Region "Select"
    Public Shared Function GetWebInfo_ByID(ByVal WebInfoID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_Select_ByID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = WebInfoID

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

    Public Shared Function GetWebInfoBannerImage_ByID(ByVal WebInfoID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectBannerImageAndSectionBannerImage_ByID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = WebInfoID

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


    Public Shared Function GetWebInfo_ByIDAndSiteID(ByVal WebInfoID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_Select_ByIDAndSiteID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = WebInfoID
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

    Public Shared Function GetWebInfo_ByID_FrontEnd(ByVal WebInfoID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_Select_ByID_FrontEnd"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = WebInfoID

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

    Public Shared Function GetWebInfo_ByIDAndAccess_FrontEnd(ByVal WebInfoID As Integer, ByVal GroupIDs As String, ByVal MemberID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_Select_ByIDAndAccess_FrontEnd"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = WebInfoID
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID

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

    Public Shared Function GetWebInfo_ByLinkUrlAndSiteID(ByVal LinkUrl As String, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_ByLinkUrlAndSiteID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@LinkUrl", SqlDbType.NVarChar).Value = LinkUrl
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


    Public Shared Function GetWebInfoList_SectionsOnly(ByVal isSecureMembers As Boolean, ByVal isSecureEducation As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_SectionsOnly"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@secure_members", SqlDbType.Bit).Value = isSecureMembers
            sqlComm.Parameters.Add("@secure_education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList(ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_ForMembers_FrontEnd(ByVal memberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectPages_ForMembers_FrontEnd"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = memberID
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_ByParentID(ByVal parentID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_ByParentID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@parentID", SqlDbType.Int).Value = parentID

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

    Public Shared Function GetWebInfoList_ByParentID_FrontEnd(ByVal parentID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_ByParentID_FrontEnd"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@parentID", SqlDbType.Int).Value = parentID

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

    Public Shared Function GetWebInfoList_ByParentIDAndAccess_FrontEnd(ByVal parentID As Integer, ByVal GroupIDs As String, ByVal MemberID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_ByParentIDAndAccess_FrontEnd"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@parentID", SqlDbType.Int).Value = parentID
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID

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

    Public Shared Function GetWebInfoList_BySectionID(ByVal sectionID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_BySectionID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@sectionID", SqlDbType.Int).Value = sectionID

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

    Public Shared Function GetWebInfoList_SectionsOnly_FrontEnd(ByVal isSecureMembers As Boolean, ByVal isSecureEducation As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_SectionsOnly_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@secure_members", SqlDbType.Bit).Value = isSecureMembers
            sqlComm.Parameters.Add("@secure_education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_SectionsAndAccessOnly_FrontEnd(ByVal isSecureMembers As Boolean, ByVal isSecureEducation As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_SectionsAndAccessOnly_FrontEnd"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@secure_members", SqlDbType.Bit).Value = isSecureMembers
            sqlComm.Parameters.Add("@secure_education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_BySectionIDandDepth(ByVal sectionID As Integer, ByVal depth As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_BySectionIDandDepth"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@sectionID", SqlDbType.Int).Value = sectionID
            sqlComm.Parameters.Add("@depth", SqlDbType.Int).Value = depth

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

    Public Shared Function GetWebInfoList_ByPageNameAndParentName(ByVal pageName As String, ByVal parentName As String, ByVal WebInfoID_Header As Integer, ByVal WebInfoID_Footer As Integer, ByVal isSecureMember As Boolean, ByVal isSecureEducation As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_ByPageNameAndParentName"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@PageName", SqlDbType.NVarChar).Value = pageName

            If parentName.Length = 0 Then
                sqlComm.Parameters.Add("@ParentName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ParentName", SqlDbType.NVarChar).Value = parentName
            End If
            sqlComm.Parameters.Add("@WebInfoID_Header", SqlDbType.Int).Value = WebInfoID_Header
            sqlComm.Parameters.Add("@WebInfoID_Footer", SqlDbType.Int).Value = WebInfoID_Footer
            sqlComm.Parameters.Add("@Secure_Members", SqlDbType.Bit).Value = isSecureMember
            sqlComm.Parameters.Add("@Secure_Education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_ByPageNameAndParentName_FrontEnd(ByVal pageName As String, ByVal parentName As String, ByVal WebInfoID_Header As Integer, ByVal WebInfoID_Footer As Integer, ByVal isSecureMember As Boolean, ByVal isSecureEducation As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_ByPageNameAndParentName_FrontEnd"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@PageName", SqlDbType.NVarChar).Value = pageName

            If parentName.Length = 0 Then
                sqlComm.Parameters.Add("@ParentName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ParentName", SqlDbType.NVarChar).Value = parentName
            End If
            sqlComm.Parameters.Add("@WebInfoID_Header", SqlDbType.Int).Value = WebInfoID_Header
            sqlComm.Parameters.Add("@WebInfoID_Footer", SqlDbType.Int).Value = WebInfoID_Footer
            sqlComm.Parameters.Add("@Secure_Members", SqlDbType.Bit).Value = isSecureMember
            sqlComm.Parameters.Add("@Secure_Education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_ByPageNameAndParentID(ByVal pageName As String, ByVal ParentID As Integer, ByVal isSecureMember As Boolean, ByVal isSecureEducation As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_ByPageNameAndParentID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@PageName", SqlDbType.NVarChar).Value = pageName

            If ParentID = Integer.MinValue Then
                sqlComm.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = ParentID
            End If

            sqlComm.Parameters.Add("@Secure_Members", SqlDbType.Bit).Value = isSecureMember
            sqlComm.Parameters.Add("@Secure_Education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_ByPageNameAndParentID_FrontEnd(ByVal pageName As String, ByVal ParentID As Integer, ByVal isSecureMember As Boolean, ByVal isSecureEducation As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_ByPageNameAndParentID_FrontEnd"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@PageName", SqlDbType.NVarChar).Value = pageName

            If ParentID = Integer.MinValue Then
                sqlComm.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = ParentID
            End If

            sqlComm.Parameters.Add("@Secure_Members", SqlDbType.Bit).Value = isSecureMember
            sqlComm.Parameters.Add("@Secure_Education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_ByPageNameAndParentIDAndAccess_FrontEnd(ByVal pageName As String, ByVal ParentID As Integer, ByVal isSecureMember As Boolean, ByVal isSecureEducation As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_ByPageNameAndParentIDAndAccess_FrontEnd"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@PageName", SqlDbType.NVarChar).Value = pageName

            If ParentID = Integer.MinValue Then
                sqlComm.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ParentID", SqlDbType.NVarChar).Value = ParentID
            End If

            sqlComm.Parameters.Add("@Secure_Members", SqlDbType.Bit).Value = isSecureMember
            sqlComm.Parameters.Add("@Secure_Education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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


    Public Shared Function GetWebInfo_HomePage(ByVal SiteID As Integer) As String
        Dim intMemberID As Integer = MemberDAL.GetCurrentMemberID
        Dim boolWebpage_PublicSection_EnableGroupsAndUsers As Boolean = False

        Dim strHomePage As String = ""

        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(SiteID)
        If dtSite.Rows.Count > 0 Then
            Dim drSite As DataRow = dtSite.Rows(0)
            strHomePage = "http://" + drSite("Domain").ToString()
        End If

        Return strHomePage
    End Function

    Public Shared Function GetWebInfo_FirstSecurePageLinkURL_MemberSection() As String
        Dim intMemberID As Integer = MemberDAL.GetCurrentMemberID
        Dim boolWebpage_MemberSection_EnableGroupsAndUsers As Boolean = False

        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(SiteDAL.GetCurrentSiteID_FrontEnd)
        If dtSite.Rows.Count > 0 Then
            Dim drSite As DataRow = dtSite.Rows(0)
            boolWebpage_MemberSection_EnableGroupsAndUsers = Convert.ToBoolean(drSite("Webpage_MemberSection_EnableGroupsAndUsers"))
        End If

        Return GetWebInfo_FirstSecurePageLinkURL_MemberSection(intMemberID, boolWebpage_MemberSection_EnableGroupsAndUsers)
    End Function

    Public Shared Function GetWebInfo_FirstSecurePageLinkURL_MemberSection(ByVal MemberID As Integer, ByVal boolEnableGroupsAndUsers As Boolean) As String
        Dim secureLinkURL As String = String.Empty

        Dim dtSecurePage As DataTable = If(boolEnableGroupsAndUsers, GetWebInfoList_SectionsAndAccessOnly_FrontEnd(True, False, MemberDAL.GetCurrentMemberGroupIDs, MemberID, SiteDAL.GetCurrentSiteID_FrontEnd()), GetWebInfoList_SectionsOnly_FrontEnd(True, False, SiteDAL.GetCurrentSiteID_FrontEnd()))
        If dtSecurePage.Rows.Count > 0 Then
            Dim drSecurePage As DataRow = dtSecurePage.Rows(0)
            Dim strPageName As String = drSecurePage("Name").ToString()
            secureLinkURL = WebInfoDAL.GetWebInfoUrl(strPageName, String.Empty, "member")
        End If

        Return secureLinkURL
    End Function

    Public Shared Function GetWebInfoList_FrontEnd(ByVal MaxPageLevel As Integer, ByVal isSecureMembers As Boolean, ByVal isSecureEducation As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_FrontEnd"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@maxPageLevel", SqlDbType.Int).Value = MaxPageLevel
            sqlComm.Parameters.Add("@secure_members", SqlDbType.Bit).Value = isSecureMembers
            sqlComm.Parameters.Add("@secure_education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_FrontEndAndAccess(ByVal MaxPageLevel As Integer, ByVal isSecureMembers As Boolean, ByVal isSecureEducation As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_FrontEndAndAccess"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@maxPageLevel", SqlDbType.Int).Value = MaxPageLevel
            sqlComm.Parameters.Add("@secure_members", SqlDbType.Bit).Value = isSecureMembers
            sqlComm.Parameters.Add("@secure_education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_FrontEnd_WithColumLayout(ByVal MaxPageLevel As Integer, ByVal isSecureMembers As Boolean, ByVal isSecureEducation As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_FrontEnd_WithColumnLayout"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@maxPageLevel", SqlDbType.Int).Value = MaxPageLevel
            sqlComm.Parameters.Add("@secure_members", SqlDbType.Bit).Value = isSecureMembers
            sqlComm.Parameters.Add("@secure_education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_FrontEnd_WithColumLayoutAndAccess(ByVal MaxPageLevel As Integer, ByVal isSecureMembers As Boolean, ByVal isSecureEducation As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_FrontEnd_WithColumnLayoutAndAccess"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@maxPageLevel", SqlDbType.Int).Value = MaxPageLevel
            sqlComm.Parameters.Add("@secure_members", SqlDbType.Bit).Value = isSecureMembers
            sqlComm.Parameters.Add("@secure_education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_FrontEnd_ForSiteMap(ByVal MaxPageLevel As Integer, ByVal isSecureMembers As Boolean, ByVal isSecureEducation As Boolean, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_FrontEnd_ForSiteMap"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@maxPageLevel", SqlDbType.Int).Value = MaxPageLevel
            sqlComm.Parameters.Add("@secure_members", SqlDbType.Bit).Value = isSecureMembers
            sqlComm.Parameters.Add("@secure_education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function GetWebInfoList_FrontEndAndAccess_ForSiteMap(ByVal MaxPageLevel As Integer, ByVal isSecureMembers As Boolean, ByVal isSecureEducation As Boolean, ByVal GroupIDs As String, ByVal MemberID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_SelectList_FrontEndAndAccess_ForSiteMap"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@maxPageLevel", SqlDbType.Int).Value = MaxPageLevel
            sqlComm.Parameters.Add("@secure_members", SqlDbType.Bit).Value = isSecureMembers
            sqlComm.Parameters.Add("@secure_education", SqlDbType.Bit).Value = isSecureEducation
            sqlComm.Parameters.Add("@groupIDs", SqlDbType.NVarChar).Value = GroupIDs
            sqlComm.Parameters.Add("@memberID", SqlDbType.Int).Value = MemberID
            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

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

    Public Shared Function InsertWebInfo(ByVal Name As String, ByVal ParentID As Integer, ByVal SiteID As Integer, ByVal PageLevel As Integer, ByVal DefaultPage As Boolean, ByVal Message As String, ByVal Message2 As String, ByVal Author As String, ByVal Last_Modified As DateTime, ByVal Pending As Boolean, ByVal Checked_Out As Boolean, ByVal Checked_ID As String, ByVal LinkOnly As Boolean, ByVal LinkURL As String, ByVal LinkFrame As String, ByVal SectionID As Integer, ByVal MetaTitle As String, ByVal MetaDesc As String, ByVal MetaKeyword As String, ByVal Page_Linkname As String, ByVal Searchable As Boolean, ByVal Language As Integer, ByVal UrlPath As String, ByVal Secure_Members As Boolean, ByVal Secure_Education As Boolean, ByVal SearchTags As String, ByVal NavigationColumnIndex As Integer, ByVal InheritBannerImage As Boolean) As Integer

        Dim intWebInfoID As Integer = 0
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_Insert"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name
            If ParentID = Integer.MinValue Then
                sqlComm.Parameters.Add("@ParentID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ParentID", SqlDbType.Int).Value = ParentID
            End If

            sqlComm.Parameters.Add("@siteID", SqlDbType.Int).Value = SiteID

            sqlComm.Parameters.Add("@PageLevel", SqlDbType.Int).Value = PageLevel

            sqlComm.Parameters.Add("@DefaultPage", SqlDbType.Int).Value = DefaultPage

            If Message = "" Then
                sqlComm.Parameters.Add("@Message", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Message", SqlDbType.NVarChar).Value = Message
            End If

            If Message2 = "" Then
                sqlComm.Parameters.Add("@Message2", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Message2", SqlDbType.NVarChar).Value = Message2
            End If

            If Author = "" Then
                sqlComm.Parameters.Add("@Author", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Author", SqlDbType.NVarChar).Value = Author
            End If

            If Last_Modified = DateTime.MinValue Then
                sqlComm.Parameters.Add("@Last_Modified", SqlDbType.DateTime).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Last_Modified", SqlDbType.DateTime).Value = Last_Modified
            End If

            sqlComm.Parameters.Add("@Pending", SqlDbType.Bit).Value = Pending
            sqlComm.Parameters.Add("@Checked_Out", SqlDbType.Bit).Value = Checked_Out

            If Checked_ID = "" Then
                sqlComm.Parameters.Add("@Checked_ID", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Checked_ID", SqlDbType.NVarChar).Value = Checked_ID
            End If

            sqlComm.Parameters.Add("@LinkOnly", SqlDbType.Bit).Value = LinkOnly

            If LinkURL = "" Then
                sqlComm.Parameters.Add("@LinkURL", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@LinkURL", SqlDbType.NVarChar).Value = LinkURL
            End If

            If LinkFrame = "" Then
                sqlComm.Parameters.Add("@LinkFrame", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@LinkFrame", SqlDbType.NVarChar).Value = LinkFrame
            End If

            If SectionID = Integer.MinValue Then
                sqlComm.Parameters.Add("@SectionID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SectionID", SqlDbType.Int).Value = SectionID
            End If

            sqlComm.Parameters.Add("@MetaTitle", SqlDbType.NVarChar).Value = MetaTitle
            sqlComm.Parameters.Add("@MetaDesc", SqlDbType.NVarChar).Value = MetaDesc
            sqlComm.Parameters.Add("@MetaKeyword", SqlDbType.NVarChar).Value = MetaKeyword

            If Page_Linkname = "" Then
                sqlComm.Parameters.Add("@Page_LinkName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Page_LinkName", SqlDbType.NVarChar).Value = Page_Linkname
            End If

            sqlComm.Parameters.Add("@Searchable", SqlDbType.Bit).Value = Searchable

            If Language = Integer.MinValue Then
                sqlComm.Parameters.Add("@Language", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Language", SqlDbType.Int).Value = Language
            End If

            If UrlPath = "" Then
                sqlComm.Parameters.Add("@UrlPath", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@UrlPath", SqlDbType.VarChar).Value = UrlPath
            End If
            sqlComm.Parameters.Add("@Secure_members", SqlDbType.Bit).Value = Secure_Members
            sqlComm.Parameters.Add("@Secure_Education", SqlDbType.Bit).Value = Secure_Education

            If SearchTags = "" Then
                sqlComm.Parameters.Add("@SearchTags", SqlDbType.VarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@SearchTags", SqlDbType.VarChar).Value = SearchTags
            End If

            sqlComm.Parameters.Add("@NavigationColumnIndex", SqlDbType.Int).Value = NavigationColumnIndex
            sqlComm.Parameters.Add("@InheritBannerImage", SqlDbType.Bit).Value = InheritBannerImage

            sqlConn.Open()
            intWebInfoID = sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return intWebInfoID
    End Function

#End Region

#Region "Update"

    Public Shared Sub UpdateWebInfo_NameAndContent(ByVal ID As Integer, ByVal Name As String, ByVal Message As String, ByVal Message2 As String, ByVal NavigationColumnIndex As Integer, ByVal Last_Modified As DateTime, ByVal InheritBannerImage As Boolean)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_UpdateNameAndContent_ByID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID
            sqlComm.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name

            If Message = "" Then
                sqlComm.Parameters.Add("@Message", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Message", SqlDbType.NVarChar).Value = Message
            End If

            If Message2 = "" Then
                sqlComm.Parameters.Add("@Message2", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Message2", SqlDbType.NVarChar).Value = Message2
            End If

            sqlComm.Parameters.Add("@NavigationColumnIndex", SqlDbType.Int).Value = NavigationColumnIndex

            sqlComm.Parameters.Add("@Last_Modified", SqlDbType.DateTime).Value = Last_Modified
            sqlComm.Parameters.Add("@InheritBannerImage", SqlDbType.Bit).Value = InheritBannerImage
            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateWebInfo_LinkOnly(ByVal ID As Integer, ByVal LinkName As String, ByVal LinkUrl As String, ByVal LinkFrame As String, ByVal NavigationColumnIndex As Integer, ByVal Last_Modified As DateTime)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_UpdateLinkOnly_ByID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID
            sqlComm.Parameters.Add("@LinkName", SqlDbType.NVarChar).Value = LinkName
            sqlComm.Parameters.Add("@LinkUrl", SqlDbType.NVarChar).Value = LinkUrl
            sqlComm.Parameters.Add("@LinkFrame", SqlDbType.NVarChar).Value = LinkFrame

            sqlComm.Parameters.Add("@NavigationColumnIndex", SqlDbType.Int).Value = NavigationColumnIndex

            sqlComm.Parameters.Add("@Last_Modified", SqlDbType.DateTime).Value = Last_Modified

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateWebInfo_MetaInformation(ByVal ID As Integer, ByVal MetaTitle As String, ByVal MetaDesc As String, ByVal MetaKeyword As String)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_UpdateMetaInformation_ByID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = ID
            sqlComm.Parameters.Add("@MetaTitle", SqlDbType.NVarChar).Value = MetaTitle
            sqlComm.Parameters.Add("@MetaDesc", SqlDbType.NVarChar).Value = MetaDesc
            sqlComm.Parameters.Add("@MetaKeyword", SqlDbType.NVarChar).Value = MetaKeyword

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateWebInfo_Rank(ByVal webinfoID_DraggedRow As Integer, ByVal WebInfoParentID_New As Integer, ByVal WebInfoRank_New As Integer, ByVal SiteID As Integer)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_UpdateRank_ByID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = webinfoID_DraggedRow
            If WebInfoParentID_New = Integer.MinValue Then
                sqlComm.Parameters.Add("@ParentID_New", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@ParentID_New", SqlDbType.Int).Value = WebInfoParentID_New
            End If

            If WebInfoRank_New = Integer.MinValue Then
                sqlComm.Parameters.Add("@Rank_New", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@Rank_New", SqlDbType.Int).Value = WebInfoRank_New
            End If

            sqlComm.Parameters.Add("@SiteID", SqlDbType.Int).Value = SiteID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateWebInfo_Pending_ByID(ByVal webinfoID As Integer, ByVal pending As Boolean)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_UpdatePending_ByID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = webinfoID
            sqlComm.Parameters.Add("@pending", SqlDbType.Bit).Value = pending

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateWebInfo_CheckInCheckOut_ByID(ByVal webinfoID As Integer, ByVal Checked_Out As Boolean, ByVal Checked_ID As String)
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_UpdateCheckInCheckOut_ByID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = webinfoID
            sqlComm.Parameters.Add("@Checked_Out", SqlDbType.Bit).Value = Checked_Out
            sqlComm.Parameters.Add("@Checked_ID", SqlDbType.NVarChar).Value = Checked_ID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub UpdateWebInfo_BannerImage_ByID(ByVal webinfoID As Integer, ByVal BannerName As String, ByVal BannerImage As Byte())
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_UpdateBannerImage_ByID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = webinfoID

            If BannerName.Length = 0 Then
                sqlComm.Parameters.Add("@BannerName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@BannerName", SqlDbType.NVarChar).Value = BannerName
            End If

            If BannerImage Is Nothing Then
                sqlComm.Parameters.Add("@BannerImage", SqlDbType.Binary).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@BannerImage", SqlDbType.Binary).Value = BannerImage
            End If

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub


#End Region

#Region "Delete"
    Public Shared Sub DeleteWebInfo_ByID(ByVal WebInfoID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_Delete_ByID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = WebInfoID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Sub DeleteWebInfo_BySectionID(ByVal SectionID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfo_Delete_BySectionID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@SectionID", SqlDbType.Int).Value = SectionID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

#End Region

#Region "Properties"
    'Public Shared Function GetWebInfo_SectionNameByID(ByVal sectionID As Integer) As String
    '    Dim strSectionName As String = ""
    '    Dim dtSection As DataTable = GetWebInfoList_BySectionID(sectionID)
    '    If dtSection.Rows.Count > 0 Then
    '        strSectionName = dtSection.Rows(0)("Name")
    '    End If
    '    Return strSectionName
    'End Function

    Public Shared Function GetWebInfoUrl(ByVal PageName As String, ByVal PageName_Parent As String, ByVal PrependedUrl As String) As String
        Dim strWebInforUrl As String = ""

        Dim strUrlRoot As String = "/"
        'No Member section
        'If PrependedUrl.Length > 0 Then
        '    strUrlRoot = "/" & PrependedUrl & "/"
        'End If

        If PageName.Length > 0 Then

            Dim sNameLink = CommonWeb.encodeHyperlink(PageName)

            If PageName_Parent.Length > 0 Then
                PageName_Parent = CommonWeb.encodeHyperlink(PageName_Parent)
                strWebInforUrl = strUrlRoot & PageName_Parent & "/" & sNameLink & ".aspx"
            Else
                strWebInforUrl = strUrlRoot & sNameLink & ".aspx"
            End If
        Else
            strWebInforUrl = strUrlRoot
        End If

        Return strWebInforUrl
    End Function
#End Region


#Region "WebInfo Access"

    Public Shared Function GetWebInfoAccessList_ByWebInfoID(ByVal WebInfoID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAccess_SelectList_ByWebInfoID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = WebInfoID

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

    Public Shared Function GetWebInfoAccess_ByWebInfoIDAndGroupID(ByVal WebInfoID As Integer, ByVal GroupID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAccess_Select_ByWebInfoIDAndGroupID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = WebInfoID
            sqlComm.Parameters.Add("@GroupID", SqlDbType.Int).Value = GroupID

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


    Public Shared Function GetWebInfoAccessList_ForGroupsSectionAdd_ByWebInfoIDHomePageAndSiteID(ByVal WebInfoID_HomePage As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAccess_SelectList_ForGroupsSectionAdd_ByWebInfoIDHomePageAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@WebInfoID_HomePage", SqlDbType.Int).Value = WebInfoID_HomePage
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

    Public Shared Function GetWebInfoAccessList_ForGroupsSubPageAdd_ByWebInfoIDParentAndSiteID(ByVal WebInfoID_Parent As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAccess_SelectList_ForGroupsSubPageAdd_ByWebInfoIDParentAndSiteID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@WebInfoID_Parent", SqlDbType.Int).Value = WebInfoID_Parent
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

    Public Shared Function GetWebInfoAccessList_ForGroupsSectionEdit_ByWebInfoIDAndSiteID(ByVal WebInfoID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAccess_SelectList_ForGroupsSectionEdit_ByWebInfoIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = WebInfoID
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

    Public Shared Function GetWebInfoAccessList_ForGroupsSubPageEdit_ByWebInfoIDAndWebInfoIDParentAndSiteID(ByVal WebInfoID As Integer, ByVal WebInfoID_Parent As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAccess_SelectList_ForGroupsSubPageEdit_ByWebInfoIDAndWebInfoIDParentAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = WebInfoID
            sqlComm.Parameters.Add("@WebInfoID_Parent", SqlDbType.Int).Value = WebInfoID_Parent
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

    Public Shared Function GetWebInfoAccessList_ForUsersSectionAdd_ByWebInfoIDHomePageAndSiteID(ByVal WebInfoID_HomePage As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAccess_SelectList_ForUsersSectionAdd_ByWebInfoIDHomePageAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@WebInfoID_HomePage", SqlDbType.Int).Value = WebInfoID_HomePage
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

    Public Shared Function GetWebInfoAccessList_ForUsersSubPageAdd_ByWebInfoIDParentAndSiteID(ByVal WebInfoID_Parent As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAccess_SelectList_ForUsersSubPageAdd_ByWebInfoIDParentAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@WebInfoID_Parent", SqlDbType.Int).Value = WebInfoID_Parent
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

    Public Shared Function GetWebInfoAccessList_ForUsersSectionEdit_ByWebInfoIDAndSiteID(ByVal WebInfoID As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAccess_SelectList_ForUsersSectionEdit_ByWebInfoIDAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = WebInfoID
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

    Public Shared Function GetWebInfoAccessList_ForusersSubPageEdit_ByWebInfoIDAndWebInfoIDParentAndSiteID(ByVal WebInfoID As Integer, ByVal WebInfoID_Parent As Integer, ByVal SiteID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAccess_SelectList_ForUsersSubPageEdit_ByWebInfoIDAndWebInfoIDParentAndSiteID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = WebInfoID
            sqlComm.Parameters.Add("@WebInfoID_Parent", SqlDbType.Int).Value = WebInfoID_Parent
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

    Public Shared Function InsertWebInfoAccess(ByVal WebInfoID As Integer, ByVal GroupID As Integer, ByVal MemberID As Integer) As Guid

        Dim guidWebInfoAccessID As Guid = Guid.NewGuid()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAccess_Insert"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = guidWebInfoAccessID
            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = WebInfoID

            If GroupID = Integer.MinValue Then
                sqlComm.Parameters.Add("@GroupID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@GroupID", SqlDbType.Int).Value = GroupID
            End If

            If MemberID = Integer.MinValue Then
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = DBNull.Value
            Else
                sqlComm.Parameters.Add("@MemberID", SqlDbType.Int).Value = MemberID
            End If

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
        Return guidWebInfoAccessID
    End Function

    Public Shared Sub DeleteWebInfoAccess_ByWebInfoID(ByVal WebInfoID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAccess_Delete_ByWebInfoID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = WebInfoID

            sqlConn.Open()
            sqlComm.ExecuteNonQuery()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try
    End Sub

    Public Shared Function RemoveRowsWithNoExistingParentID(ByVal dtWebInfo As DataTable) As DataTable

        'Only do this function if we have rows in our DataTable, else there are no rows to remove
        If dtWebInfo.Rows.Count > 0 Then
            'Add Primary Key
            dtWebInfo.PrimaryKey = New DataColumn() {dtWebInfo.Columns("ID")}
            Dim boolNoMoreNonExistantParents As Boolean = False
            While boolNoMoreNonExistantParents = False
                boolNoMoreNonExistantParents = True
                For i As Integer = dtWebInfo.Rows.Count - 1 To 0 Step -1
                    Dim drWebInfo As DataRow = dtWebInfo.Rows(i)
                    If Not drWebInfo("ParentID") Is DBNull.Value Then
                        'So this node has a parent, check if this parent exists in our db if not remove this node
                        Dim iParentID As Integer = Convert.ToInt32(drWebInfo("ParentID"))
                        Dim drParentNode As DataRow = dtWebInfo.Rows.Find(iParentID)
                        If drParentNode Is Nothing Then
                            'Remove this node as it does not have a parent id, so its parent id must not have access therefore children dont have acces
                            dtWebInfo.Rows.RemoveAt(i)
                            boolNoMoreNonExistantParents = False
                            Exit For
                        End If
                    End If
                Next
            End While
        End If

        Return dtWebInfo
    End Function

#End Region

#Region "WebInfo Access"

    Public Shared Function HasWebInfoAdminUserAccess(ByVal UserID As Integer, ByVal WebInfoID As Integer) As Boolean
        Dim intAdminUserAccessLevel As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        Return HasWebInfoAdminUserAccess(intAdminUserAccessLevel, UserID, WebInfoID)
    End Function

    Public Shared Function HasWebInfoAdminUserAccess(ByVal AdminUserAccessLevel As Integer, ByVal UserID As Integer, ByVal WebInfoID As Integer) As Boolean
        Dim bHasWebInfoAdminUserAccess As Boolean = False

        If AdminUserAccessLevel > 2 Then
            'User is a Super Admin so by Default has access to ALL
            bHasWebInfoAdminUserAccess = True
        Else
            'User is a simple Admin User, so check the Admin User has WebInfo AdminUserAccess
            Dim dtWebInfoAdminUserAccess As DataTable = GetWebInfoAdminUserAccess(UserID, WebInfoID)
            If dtWebInfoAdminUserAccess.Rows.Count > 0 Then
                ' Note just the fact that the row exists means the Admin User has WebInfo AdminAccess For this Page
                bHasWebInfoAdminUserAccess = True
            End If
        End If

        Return bHasWebInfoAdminUserAccess

    End Function

    Public Shared Function GetWebInfoAdminUserAccess(ByVal UserID As Integer, ByVal WebInfoID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAdminUserAccess_Select"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = WebInfoID

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

    Public Shared Function GetWebInfoAdminUserAccessList_ByUserID(ByVal UserID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAdminUserAccess_SelectList_ByUserID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID

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

    Public Shared Function GetWebInfoAdminUserAccessList_ByWebInfoID(ByVal WebInfoID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAdminUserAccess_SelectList_ByWebInfoID"
            sqlComm.Connection = sqlConn
            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = WebInfoID

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


    Public Shared Sub InsertWebInfoAdminUserAccess(ByVal UserID As Integer, ByVal WebInfoID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAdminUserAccess_Insert"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID
            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = WebInfoID

            sqlConn.Open()
            sqlComm.ExecuteScalar()

        Finally
            If Not sqlConn Is Nothing Then
                sqlConn.Close()
            End If
        End Try

    End Sub

    Public Shared Sub DeleteWebInfoAdminUserAccess_ByWebInfoID(ByVal WebInfoID As Integer)

        Dim sqlConn As SqlConnection = Nothing
        Try
            sqlConn = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
            Dim sqlComm As New SqlCommand()
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandText = "ss_WebInfoAdminUserAccess_Delete_ByWebInfoID"
            sqlComm.Connection = sqlConn

            sqlComm.Parameters.Add("@WebInfoID", SqlDbType.Int).Value = WebInfoID

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
