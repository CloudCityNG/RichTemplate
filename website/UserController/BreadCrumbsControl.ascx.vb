Imports System.Data

Partial Class UserController_BreadCrumbsControl
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Function BreadCrumbsAlreadySet() As Boolean
        Return rptBreadCrumbs.Items.Count > 0
    End Function

    Public Sub LoadBreadCrumbs(ByVal listBreadCrumbLinks As List(Of KeyValuePair(Of String, String)))

        'Only use this list if the bread crumbs have not yet been created, as we give presidence to the first list, as it is usually from the RadMenu, rather than secondary links from header/footer
        If rptBreadCrumbs.Items.Count = 0 Then
            rptBreadCrumbs.DataSource = listBreadCrumbLinks
            rptBreadCrumbs.DataBind()
        End If

    End Sub

    Protected Sub rptBreadCrumbs_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptBreadCrumbs.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then

            Dim kvBreadCrumbs As KeyValuePair(Of String, String) = e.Item.DataItem

            Dim litBreadCrumbs As Literal = e.Item.FindControl("litBreadCrumbs")
            Dim aBreadCrumbs As HtmlAnchor = e.Item.FindControl("aBreadCrumbs")

            litBreadCrumbs.Text = kvBreadCrumbs.Key
            aBreadCrumbs.HRef = If(Not kvBreadCrumbs.Value.Contains("#"), kvBreadCrumbs.Value, "")

        End If
    End Sub

End Class
