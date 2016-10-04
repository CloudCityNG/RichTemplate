
Partial Class link_Default
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("id") <> "" Then
                ucLinkDetail.Visible = True
                ucLinkRepeater.Visible = False
                divActive.Visible = False
                divArchive.Visible = False
            ElseIf Request.QueryString("archive") <> "" Then
                ucLinkDetail.Visible = False
                ucLinkRepeater.Visible = True
                divActive.Visible = False
                divArchive.Visible = True
            End If

        End If

    End Sub




End Class
