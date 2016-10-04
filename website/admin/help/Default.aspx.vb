Imports Telerik.Web.UI
Imports System.Data

Partial Class admin_Help_Default
    Inherits RichTemplateLanguagePage

    Private HelpID As Integer = 0
    Public Property NewProperty() As Integer
        Get
            Return HelpID
        End Get
        Set(ByVal value As Integer)
            HelpID = value
        End Set
    End Property



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgHelp, "{4} {5} " & Resources.Help.Help_Default_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Help.Help_Default_Grid_Pager_PagerTextFormat_Page)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Help.Help_Default_Header

        'Check the logged in user can view this page
        Dim intAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intAdminUserAccess > 1 Then
            'perhaps do something
        Else
            Response.Redirect("~/richadmin/")
        End If

        If Not Page.IsPostBack Then

        End If

    End Sub


    Protected Sub rgHelp_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgHelp.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim drvHelp As DataRowView = e.Item.DataItem
            Dim drHelp As DataRow = drvHelp.Row

            'Now we get the literal control and the rows description
            Dim description As String = drHelp("description")
            Dim litDescription As Literal = DirectCast(e.Item.FindControl("litDescription"), Literal)
            litDescription.Text = CommonWeb.Truncate_WordCount(description, 15)

            'Now we get the parent of this literal and set its title attribute so when you hover over this literal the full text shows as a tool tip
            Dim cParent As GridTableCell = DirectCast(litDescription.Parent, GridTableCell)
            cParent.Attributes.Add("title", description)

            'setup Edit link
            Dim intHelpID As Integer = Convert.ToInt32(drHelp("ID"))

            Dim aHelpEdit As HtmlAnchor = DirectCast(e.Item.FindControl("aHelpEdit"), HtmlAnchor)
            aHelpEdit.HRef = "editAdd.aspx?ID=" + intHelpID.ToString()

        End If


    End Sub

End Class

