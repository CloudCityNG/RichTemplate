Imports Telerik.Web.UI
Imports System.Data

Partial Class admin_AdminUsers_Default
    Inherits RichTemplateLanguagePage

    Public Property ShowAllAdminUsers() As Boolean
        Get
            Dim _UserFilter As Boolean = False
            If Not ViewState("ShowAllAdminUsers") Is Nothing Then
                _UserFilter = ViewState("ShowAllAdminUsers")
            End If
            Return _UserFilter
        End Get
        Set(ByVal value As Boolean)
            ViewState("ShowAllAdminUsers") = value
        End Set
    End Property

	Dim bAllowAllAdminUserAccess As Boolean = False
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgAdminUser, "{4} {5} " & Resources.AdminUser_Admin.AdminUser_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.AdminUser_Admin.AdminUser_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.AdminUser_Admin.AdminUser_Default_Header
        ucHeader.PageHelpID = 13 'Help Item for User Administration


        If Not Page.IsPostBack Then

        End If

        'Check the logged in user can view this page
        Dim intAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intAdminUserAccess > 2 Then
            bAllowAllAdminUserAccess = True
        ElseIf intAdminUserAccess > 1 Then
            'perhaps do something
            Dim dtCurrentAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(AdminUserDAL.GetCurrentAdminUserID)
            If dtCurrentAdminUser.Rows.Count > 0 Then
                Dim drCurrentAdminUser As DataRow = dtCurrentAdminUser.Rows(0)
                bAllowAllAdminUserAccess = drCurrentAdminUser("AllAdminUserView") IsNot DBNull.Value AndAlso Convert.ToBoolean(drCurrentAdminUser("AllAdminUserView"))
            End If
        Else
            Response.Redirect("~/richadmin/")
        End If

    End Sub


    Protected Sub rgAdminUser_ItemCreated(ByVal source As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgAdminUser.ItemCreated
        If e.Item.ItemType = GridItemType.CommandItem Then

            'NOTE Only show this if the current user is a Master Administrator
            If AdminUserDAL.GetCurrentAdminUserAccessLevel() > 2 Then
                Dim commandItem As GridCommandItem = e.Item
                Dim divViewAdminUsers As HtmlGenericControl = commandItem.FindControl("divViewAdminUsers")
                Dim rblViewAdminUsers As RadioButtonList = commandItem.FindControl("rblViewAdminUsers")

                'We have the rblViewAdminUsers Container now, we only show all AdminUsers 
                rblViewAdminUsers.SelectedValue = ShowAllAdminUsers 'Set the selected option to False as default

                divViewAdminUsers.Visible = True
            End If
        End If
    End Sub

    Protected Sub rblViewAdminUsers_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim rblViewAdminUsers As RadioButtonList = sender

        Dim boolShowAllAdminUsers As Boolean = Convert.ToBoolean(rblViewAdminUsers.SelectedValue)
        ShowAllAdminUsers = boolShowAllAdminUsers

        rgAdminUser.Rebind() 'We rebind our members list
    End Sub

    Public Sub rgAdminUser_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgAdminUser.NeedDataSource
        Dim intAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intAdminUserAccess > 0 Then
            Dim dtAdminUserList As DataTable
            If ShowAllAdminUsers Then
                dtAdminUserList = AdminUserDAL.GetAdminUserList_ByAccessLevel(intAdminUserAccess)
            Else
                dtAdminUserList = AdminUserDAL.GetAdminUserList_BySiteIDAndAccessLevel(SiteDAL.GetCurrentSiteID_Admin(), intAdminUserAccess)

                'Filter all rows if user does not have access to all AdminUsers
                If Not bAllowAllAdminUserAccess Then
                    For index = dtAdminUserList.Rows.Count - 1 To 0 Step -1
                        Dim iCurrentAdminUserID As Integer = Convert.ToInt32(dtAdminUserList.Rows(index)("ID"))
                        If Not AdminUserDAL.GetCurrentAdminUserID = iCurrentAdminUserID Then
                            dtAdminUserList.Rows.RemoveAt(index)
                        End If
                    Next
                End If
            End If
            rgAdminUser.DataSource = dtAdminUserList
        End If
    End Sub

    Protected Sub rgAdminUser_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgAdminUser.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim drvUser As DataRowView = e.Item.DataItem
            Dim drUser As DataRow = drvUser.Row

            'Now we check if this user is active, if not we strike out our text
            Dim active As Boolean = Convert.ToBoolean(drUser("active"))
            Dim strStrikeOut_Start As String = ""
            Dim strStrikeOut_End As String = ""
            Dim litStatus As Literal = DirectCast(e.Item.FindControl("litStatus"), Literal)
            If active Then
                litStatus.Text = "<span class='activeField'>" & Resources.AdminUser_Admin.AdminUser_Default_GridStatusActive & "</span>"
            Else
                strStrikeOut_Start = "<STRIKE>"
                strStrikeOut_End = "</STRIKE>"

                litStatus.Text = strStrikeOut_Start & "<span class='inactiveField'>" & Resources.AdminUser_Admin.AdminUser_Default_GridStatusArchive & "</span>" & strStrikeOut_End
            End If

            'Setup the type of user and their account type
            Dim imgUser As HtmlImage = DirectCast(e.Item.FindControl("imgUser"), HtmlImage)
            Dim intAccessLevel As Integer = Convert.ToInt32(drUser("Access_Level"))
            If intAccessLevel = 3 Then
                imgUser.Src = "/admin/images/icon_admin.gif"

            ElseIf intAccessLevel = 2 Then
                imgUser.Src = "/admin/images/icon_user_blue.gif"

            Else
                imgUser.Src = "/admin/images/icon_user_green.gif"

            End If


            'Now we set the firstname, last name, username and password
            Dim litFirstName As Literal = DirectCast(e.Item.FindControl("litFirstName"), Literal)
            litFirstName.Text = strStrikeOut_Start & drUser("First_Name").ToString() & strStrikeOut_End

            Dim litLastName As Literal = DirectCast(e.Item.FindControl("litLastName"), Literal)
            litLastName.Text = strStrikeOut_Start & drUser("Last_Name").ToString() & strStrikeOut_End

            Dim litUserName As Literal = DirectCast(e.Item.FindControl("litUserName"), Literal)
            litUserName.Text = strStrikeOut_Start & drUser("UserName").ToString() & strStrikeOut_End

            'Now we setup the account type
            Dim litAccountType As Literal = DirectCast(e.Item.FindControl("litAccountType"), Literal)
            Dim strAccountType_LanguageProperty As String = drUser("AccountType_LanguageProperty").ToString()
            Dim strAccountType_LangaugeSpecific As String = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strAccountType_LanguageProperty)

            litAccountType.Text = strStrikeOut_Start & strAccountType_LangaugeSpecific & strStrikeOut_End


            'Finally setup the Limit, Count and Expiration
            Dim litLoginLimit As Literal = DirectCast(e.Item.FindControl("litLoginLimit"), Literal)
            litLoginLimit.Text = strStrikeOut_Start & drUser("Login_Limit").ToString() & strStrikeOut_End

            Dim litCounter As Literal = DirectCast(e.Item.FindControl("litCounter"), Literal)
            litCounter.Text = strStrikeOut_Start & drUser("Counter").ToString() & strStrikeOut_End

            Dim litExpirationDate As Literal = DirectCast(e.Item.FindControl("litExpirationDate"), Literal)
            If Not drUser("Expiration_Date") Is DBNull.Value Then
                litExpirationDate.Text = strStrikeOut_Start & Convert.ToDateTime(drUser("Expiration_Date")).ToString("dd MMM yyyy") & strStrikeOut_End
            End If

            'Attach the userid to our lnkEdit and lnkDelete buttons
            Dim intUserID As Integer = Convert.ToInt32(drUser("ID"))

            Dim aAdminUserEdit As HtmlAnchor = DirectCast(e.Item.FindControl("aAdminUserEdit"), HtmlAnchor)
            aAdminUserEdit.HRef = "editAdd.aspx?ID=" + intUserID.ToString

            Dim lnkDelete As LinkButton = DirectCast(e.Item.FindControl("lnkDelete"), LinkButton)
            CommonWeb.SetupDeleteLinkButton(lnkDelete, Resources.AdminUser_Admin.AdminUser_Default_GridDeleteButton_ConfirmationMessage)
            lnkDelete.CommandArgument = intUserID.ToString()

        End If


    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnkDelete As LinkButton = DirectCast(sender, LinkButton)
        Dim intUserID As String = Convert.ToInt32(lnkDelete.CommandArgument)

        'Delete user
        AdminUserDAL.DeleteUser_ByID(intUserID.ToString())

        rgAdminUser.Rebind()
    End Sub

End Class

