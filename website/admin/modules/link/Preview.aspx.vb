
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_link_Preview
    Inherits System.Web.UI.Page

    Dim ModuleTypeID As Integer = 7
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    End Sub

    Public Sub Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Dim linkID As Integer = Convert.ToInt32(Session("linkID"))
        Response.Redirect("editAdd.aspx?id=" & linkID.ToString())
    End Sub

    Public Sub Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Close.Click
        Response.Redirect("default.aspx")
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a rollback confirmation to our rollback button
        CommonWeb.SetupRollBackButton(btnRollBack, Resources.Link_Admin.Link_Preview_RollBack_ConfirmationMessage)

        If Not Page.IsPostBack Then

            If Not IsDBNull(Session("linkID")) Then

                Dim linkID As Integer = Convert.ToInt32(Session("linkID"))
                Dim archiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))

                Dim dtLinkArchive As DataTable = LinkDAL.GetLinkArchive_ByArchiveID(archiveID)
                If dtLinkArchive.Rows.Count > 0 Then
                    Dim drLinkArchive As DataRow = dtLinkArchive.Rows(0)

                    Dim linkContent As New StringBuilder()
                    linkContent.Append("<fieldset>")
                    If Not drLinkArchive("linkName") Is Nothing Then
                        linkContent.Append("Link Name: " & drLinkArchive("linkName") & "<br/>")
                    End If
                    If Not drLinkArchive("linkURL") Is Nothing Then
                        linkContent.Append("Link URL: " & drLinkArchive("linkURL") & "<br/>")
                    End If
                    If Not drLinkArchive("linkDescription") Is Nothing Then
                        linkContent.Append("Link Description: " & drLinkArchive("linkDescription") & "<br/>")
                    End If
                    linkContent.Append("</fieldset>")
                    Me.recordContent.Text = linkContent.ToString()

                    Dim infotext As New StringBuilder()
                    infotext.Append("Archive Record Created: " & drLinkArchive("dateTimeStamp") & "<br/>")
                    infotext.Append("Version Number: " & drLinkArchive("version") & "<br />")

                    Dim authorID As Integer = drLinkArchive("author")
                    Dim dtAdminUser As DataTable = AdminUserDAL.GetAdminUser_ByID(authorID)
                    If dtAdminUser.Rows.Count > 0 Then
                        Dim drAdminUser As DataRow = dtAdminUser.Rows(0)
                        Dim authorName As String
                        authorName = drAdminUser("First_Name") & " " & drAdminUser("Last_Name")
                        infotext.Append("Author: " & authorName)
                    End If
                    Me.infoLabel.Text = infotext.ToString()
                End If

            End If
        End If

    End Sub


    Public Sub btnRollBack_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        If Not Request.QueryString("archiveID") Is Nothing Then

            Dim archiveID As String = Request.QueryString("archiveID")

            LinkDAL.RollbackLink(archiveID)
            confirmationPanel.Visible = True
            rollbackBanel.Visible = False
        End If
    End Sub

End Class
