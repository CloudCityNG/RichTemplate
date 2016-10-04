Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports LDAP_ClassLibrary

Partial Class admin_modules_access_Default
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 8 ' Module Type: Members

    Dim intSiteID As Integer = Integer.MinValue

    Public Property UserFilter() As String
        Get
            Dim _UserFilter As String = "ALL"
            If Not ViewState("UserFilter") Is Nothing Then
                _UserFilter = ViewState("UserFilter")
            End If
            Return _UserFilter
        End Get
        Set(ByVal value As String)
            ViewState("UserFilter") = value
        End Set
    End Property

    Public Property ShowAllMembers() As Boolean
        Get
            Dim _UserFilter As Boolean = False
            If Not ViewState("ShowAllMembers") Is Nothing Then
                _UserFilter = ViewState("ShowAllMembers")
            End If
            Return _UserFilter
        End Get
        Set(ByVal value As Boolean)
            ViewState("ShowAllMembers") = value
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteMember, Resources.Member_Admin.Member_Default_GridMember_DeleteButton_ConfirmationMessage)
        CommonWeb.SetupDeleteButton(btnDeleteGroup, Resources.Member_Admin.Member_Default_GridGroup_DeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgMembers, "{4} {5} " & Resources.Member_Admin.Member_Default_GridMember_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Member_Admin.Member_Default_GridMember_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadGrid(rgGroups, "{4} {5} " & Resources.Member_Admin.Member_Default_GridGroup_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Member_Admin.Member_Default_GridGroup_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Member_Admin.Member_Default_Header

        'setup alphabet
        SetupAlphabetUserSelector()

        If Not IsPostBack Then
            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            'Initially set the All link button to selected
            lnkLetterAll.CssClass = "letter_sel"
        End If

    End Sub

    Private Sub SetupAlphabetUserSelector()

        Dim dtLetters As DataTable = LanguageDAL.GetLanguageLetters_ByLanguageCode(LanguageDAL.GetCurrentLanguageCode_BySiteID(intSiteID))

        rptLetters.DataSource = dtLetters
        rptLetters.DataBind()

    End Sub

    Protected Sub rgMembers_ItemCreated(ByVal source As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMembers.ItemCreated
        If e.Item.ItemType = GridItemType.CommandItem Then
            'NOTE Only show this if the current user is a Master Administrator
            If AdminUserDAL.GetCurrentAdminUserAccessLevel() > 2 Then
                Dim commandItem As GridCommandItem = e.Item
                Dim divViewMembers As HtmlGenericControl = commandItem.FindControl("divViewMembers")
                Dim rblViewMembers As RadioButtonList = commandItem.FindControl("rblViewMembers")

                'We have the rblViewMembers Container now, we only show this if the current AdminUser is a Master Administrator
                rblViewMembers.SelectedValue = ShowAllMembers 'Set the selected option to False as default

                divViewMembers.Visible = True
            End If
        End If
    End Sub

    Protected Sub rgMembers_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgMembers.NeedDataSource

        Dim dtMembers As DataTable
        If ShowAllMembers Then
            dtMembers = MemberDAL.GetMemberList()
        Else
            dtMembers = MemberDAL.GetMemberList_BySiteID(intSiteID)
        End If

        Dim dvMembers As New DataView(dtMembers)
        dvMembers.Sort = "LastName ASC"

        If UserFilter = "ALL" Then
            dvMembers.RowFilter = ""
        ElseIf UserFilter = "UNASSIGNED" Then
            dvMembers.RowFilter = "GroupCount = 0"
        Else
            dvMembers.RowFilter = "LastName like '" & UserFilter & "%'"
        End If

        SetSelectedLetter(UserFilter)
        rgMembers.DataSource = dvMembers

        'Check if we should show the divKeyActiveDirectory
        Dim boolSiteIncludedActiveDirectoryMembers As Boolean = dtMembers.Select("ActiveDirectory_Identifier Is Not Null And Len(Trim(ActiveDirectory_Identifier)) > 0").Length > 0
        If boolSiteIncludedActiveDirectoryMembers Then
            divKeyActiveDirectory.Visible = True
        End If

    End Sub

    Private Sub rgMembers_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMembers.ItemDataBound
        'Get the groups that the user belongs to
        If TypeOf e.Item Is GridDataItem Then
            Dim sbUserGroup As New StringBuilder
            Dim item As GridDataItem = CType(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            Dim intMemberID As Integer = Convert.ToInt32(drItem("ID"))
            Dim dtUserGroups As DataTable = GroupDAL.GetGroupList_ByMemberIDAndSiteID(intMemberID, intSiteID)
            For Each drUserGroup As DataRow In dtUserGroups.Rows()
                sbUserGroup.Append(drUserGroup("groupName") & ",")
            Next
            If sbUserGroup.Length > 0 Then
                sbUserGroup.Remove(sbUserGroup.Length - 1, 1)
            End If
            item("gname").Text = sbUserGroup.ToString()

            'Note If the user is not in any group he/she is 'in-active'
            If sbUserGroup.Length = 0 Then
                item("fullName").ForeColor = Drawing.Color.Red
                item("email").ForeColor = Drawing.Color.Red
                item("username").ForeColor = Drawing.Color.Red
                item("gname").ForeColor = Drawing.Color.Red

                'Change user image
                Dim uImage As String = String.Empty
                Dim userImage As Image = DirectCast(e.Item.FindControl("userImage"), Image)
                userImage.ImageUrl = "/admin/images/business_user_inactive.png"
            End If

            'setup Edit link
            Dim aMemberEdit As HtmlAnchor = DirectCast(e.Item.FindControl("aMemberEdit"), HtmlAnchor)
            aMemberEdit.HRef = "editAddMember.aspx?ID=" + intMemberID.ToString
        End If

    End Sub

    Protected Sub btnDeleteMember_Click(ByVal sender As Object, ByVal e As EventArgs)

        For Each grdItem As GridDataItem In rgMembers.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("ID")
                MemberDAL.DeleteMember_ByID(intRecordId)
            End If
        Next
        rgMembers.Rebind()

    End Sub

    Protected Sub rblViewMembers_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim rblViewMembers As RadioButtonList = sender

        Dim boolShowAllMembers As Boolean = Convert.ToBoolean(rblViewMembers.SelectedValue)
        ShowAllMembers = boolShowAllMembers

        rgMembers.Rebind() 'We rebind our members list
    End Sub

    Protected Sub rgGroups_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGroups.NeedDataSource
        SetSelectedLetter(UserFilter)

        Dim dtGroups As DataTable = GroupDAL.GetGroupList_BySiteID(intSiteID)
        rgGroups.DataSource = dtGroups

        'Check if we should show the divKeyActiveDirectory
        Dim boolSiteIncludedActiveDirectoryGroups As Boolean = dtGroups.Select("ActiveDirectory_Identifier Is Not Null And Len(Trim(ActiveDirectory_Identifier)) > 0").Length > 0
        If boolSiteIncludedActiveDirectoryGroups Then
            divKeyActiveDirectory.Visible = True
        End If
    End Sub

    Private Sub rgGroups_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgGroups.ItemDataBound

        Dim groupIDArray As String = Nothing

        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = CType(e.Item, GridDataItem)
            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            Dim intGroupID As Integer = Convert.ToInt32(drItem("groupID"))
            If drItem("groupActive") = False Then

                item("groupName").ForeColor = Drawing.Color.Red
                item("groupDescription").ForeColor = Drawing.Color.Red
                item("groupPassword").ForeColor = Drawing.Color.Red
                item("groupActive").ForeColor = Drawing.Color.Red

            End If

            'setup Edit link
            Dim aGroupEdit As HtmlAnchor = DirectCast(e.Item.FindControl("aGroupEdit"), HtmlAnchor)
            aGroupEdit.HRef = "editAddGroup.aspx?ID=" + intGroupID.ToString

        End If

    End Sub

    Protected Sub btnDeleteGroup_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgGroups.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("groupID")
                GroupDAL.DeleteGroup_ByGroupID(intRecordId)
            End If
        Next
        rgGroups.Rebind()
        rgMembers.Rebind()


    End Sub

    Protected Sub rep_letters_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptLetters.ItemCommand
        Dim lnkLetter As LinkButton = e.Item.FindControl("lnkLetter")

        'Filter user list based on the character
        Dim strCommand As String = lnkLetter.CommandArgument

        UserFilter = strCommand

        rgMembers.Rebind()

    End Sub

    Protected Sub lnkLetterAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLetterAll.Click
        Dim lnkLetterAll As LinkButton = sender

        'Filter user list based on the character
        Dim strCommand As String = lnkLetterAll.CommandArgument

        UserFilter = strCommand

        rgMembers.Rebind()
    End Sub

    Protected Sub lnkletterUnassigned_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLetterUnassigned.Click
        Dim lnkLetterUnassigned As LinkButton = sender

        'Filter user list based on the character
        Dim strCommand As String = lnkLetterUnassigned.CommandArgument

        UserFilter = strCommand

        rgMembers.Rebind()

    End Sub

    Private Sub SetSelectedLetter(ByVal LetterValue As String)
        'NOTE When an event is fired we loss the selected letter, so call this on RadGrid NeedDataSource and other Click Events
        lnkLetterAll.CssClass = If(LetterValue.ToUpper() = "ALL", "letter_sel", "")
        lnkLetterUnassigned.CssClass = If(LetterValue.ToUpper() = "UNASSIGNED", "letter_sel", "")
        For Each rptItem As RepeaterItem In rptLetters.Items
            Dim lnkLetter As LinkButton = rptItem.FindControl("lnkLetter")
            lnkLetter.CssClass = If(LetterValue.ToUpper() = lnkLetter.CommandArgument.ToUpper(), "letter_sel", "")
        Next
    End Sub

    Protected Sub lnkExportMembers_Click() Handles lnkExportMembers.Click
        Dim arrayColumnPairs As New ArrayList()
        arrayColumnPairs.Add(New KeyValuePair(Of String, String)("M.ID", Resources.Member_Admin.Member_Default_ExportMembers_MemberID))
        arrayColumnPairs.Add(New KeyValuePair(Of String, String)("Salutation", Resources.Member_Admin.Member_Default_ExportMembers_Salutation))
        arrayColumnPairs.Add(New KeyValuePair(Of String, String)("FirstName", Resources.Member_Admin.Member_Default_ExportMembers_FirstName))
        arrayColumnPairs.Add(New KeyValuePair(Of String, String)("LastName", Resources.Member_Admin.Member_Default_ExportMembers_LastName))
        arrayColumnPairs.Add(New KeyValuePair(Of String, String)("Email", Resources.Member_Admin.Member_Default_ExportMembers_EmailAddress))

        Dim dtNow As DateTime = DateTime.Now

        Dim strFromClause = "Members m Left Join ss_Salutation sss on sss.ID = m.SalutationID" & If(ShowAllMembers, "", " INNER JOIN ss_SiteAccess_Member sa on sa.MemberID = m.ID")
        Dim sbWhereClause As New StringBuilder()
        Select Case UserFilter
            Case "ALL"
                'Dont append anything to the where clause as we want ALL members

            Case "UNASSIGNED"
                sbWhereClause.Append("groupCount = 0")
            Case Else
                sbWhereClause.Append("LastName Like '" & UserFilter & "%'")
        End Select

        'Include either Members in this site ONLY or ALL MEMBERS
        sbWhereClause.Append(If(ShowAllMembers, "", If(sbWhereClause.Length = 0, "", " AND ") & "SiteID = " & intSiteID & " OR SiteID = 0"))

        CommonDAL.CreateDataExport(Resources.Member_Admin.Member_Default_ExportMembers_FileNamePrefix & "_" & dtNow.ToString("dd-MMM-yy"), ",", strFromClause, sbWhereClause.ToString(), "LastName ASC", arrayColumnPairs)
        SetSelectedLetter(UserFilter)
    End Sub

    Protected Sub lnkImportUsersAndGroups_Click() Handles lnkImportUsersAndGroups.Click
        Dim strError As String = LDAP.PerformActiveDirectoryTransferProcess()
        If strError = String.Empty Then
            litImportUsersAndGroupsComplete.Text = "<span style='color:Green;'>&nbsp Completed</span>"
        Else
            litImportUsersAndGroupsComplete.Text = "<span style='color:Red;'>&nbsp Completed</span>"
        End If
    End Sub

End Class
