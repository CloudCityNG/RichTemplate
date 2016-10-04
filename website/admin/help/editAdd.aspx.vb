Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_Help_editAdd
    Inherits RichTemplateLanguagePage

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Help.Help_AddEdit_Header

        'Check the logged in user can view this page
        Dim intAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intAdminUserAccess > 1 Then
            'perhaps do something
        Else
            Response.Redirect("~/richadmin/")
        End If

        If Not Page.IsPostBack Then

            If Not Request.QueryString("ID") Is Nothing Then

                Dim helpID As Integer = Convert.ToInt32(Request.QueryString("ID"))

                Dim dtHelpItems As DataTable = HelpDAL.GetHelp_ByID(helpID)
                If dtHelpItems.Rows.Count > 0 Then
                    Dim drHelpItem As DataRow = dtHelpItems.Rows(0)

                    btnAddEdit.Text = Resources.Help.Help_AddEdit_ButtonUpdate
                    'If data is found, fill textboxes
                    Active.SelectedValue = drHelpItem("active").ToString
                    txt_Title.Text = drHelpItem("Title")
                    txt_Description.Text = drHelpItem("Description")

                    If Not drHelpItem("HtmlContent").ToString() = "" Then
                        txt_Content.Content = drHelpItem("HtmlContent").ToString()
                    End If

                Else
                    btnAddEdit.Text = Resources.Help.Help_AddEdit_ButtonAdd
                    Active.SelectedValue = True
                End If

            Else
                btnAddEdit.Text = Resources.Help.Help_AddEdit_ButtonAdd
                Active.SelectedValue = True
            End If

        End If

    End Sub


    Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelButton.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click

        addUpdateRecord()
        Response.Redirect("default.aspx")

    End Sub

    Protected Sub addUpdateRecord()

        If Request.QueryString("ID") Is Nothing Then

            Dim strTitle As String = txt_Title.Text.Trim()
            Dim strDescription As String = txt_Description.Text.Trim()

            Dim strHtmlContent As String = txt_Content.Content.ToString()

            Dim boolActive As Boolean = Convert.ToBoolean(Active.SelectedValue)

            'Insert a new help item
            Dim helpID As Integer = HelpDAL.InsertHelp(strTitle, strDescription, strHtmlContent, boolActive)
        Else

            Dim helpID As Integer = Request.QueryString("ID")

            Dim strTitle As String = txt_Title.Text.Trim()
            Dim strDescription As String = txt_Description.Text.Trim()

            Dim strHtmlContent As String = txt_Content.Content.ToString()

            Dim boolActive As Boolean = Convert.ToBoolean(Active.SelectedValue)

            'Update the existing help item
            HelpDAL.UpdateHelp(helpID, strTitle, strDescription, strHtmlContent, boolActive)
        End If

    End Sub
End Class
