Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI


Partial Class link_LinkRepeater
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 7

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            LoadLinkCategoies()
        End If

    End Sub

    Protected Sub LoadLinkCategoies()
        Dim dtLinkCategories As DataTable = CategoryDAL.GetCategoryList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteDAL.GetCurrentSiteID_FrontEnd())

        'Also add the Un-categorized Category, for links that have not yet been categorized
        Dim drLinkCategory As DataRow = dtLinkCategories.NewRow()
        drLinkCategory("CategoryID") = 0
        drLinkCategory("CategoryName") = "Un-Categorized"
        dtLinkCategories.Rows.Add(drLinkCategory)

        rptCategories.DataSource = dtLinkCategories
        rptCategories.DataBind()

    End Sub


    
    Protected Sub rptCategories_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        Dim drvCategory As DataRowView = e.Item.DataItem
        Dim drCategory As DataRow = drvCategory.Row

        Dim intCategoryID As Integer = drCategory("CategoryID")

        'Get the status (either Active Links or Archived Links)
        Dim boolStatus As Boolean = True
        If Request.QueryString("archive") <> "" Then
            boolStatus = False
        End If

        'Get all links by a specific category
        Dim dtLinks As New DataTable()
        If intCategoryID = 0 Then
            dtLinks = LinkDAL.GetLink_ByCategoryNullAndStatus_FrontEnd(boolStatus)
        Else
            dtLinks = LinkDAL.GetLink_ByCategoryIDAndStatus_FrontEnd(intCategoryID, boolStatus)
        End If


        If dtLinks.Rows.Count > 0 Then
            'Show the div for this cateogry
            Dim divLinkCategory As HtmlGenericControl = e.Item.FindControl("divLinkCategory")
            divLinkCategory.Visible = True

            'Get the cateogry name and populate it
            Dim lit_LinkCategory As Literal = e.Item.FindControl("lit_LinkCategory")
            Dim strCategoryName As String = drCategory("CategoryName")
            lit_LinkCategory.Text = strCategoryName

            'Get the repeater and populate it
            Dim rptLinks As Repeater = e.Item.FindControl("rptLinks")
            rptLinks.DataSource = dtLinks
            rptLinks.DataBind()
        End If
    End Sub

    Protected Sub rptLinks_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        Dim drvLink As DataRowView = e.Item.DataItem
        Dim drLink As DataRow = drvLink.Row


        'check if the image exists
        Dim boolImageExists As Boolean = False
        If Not drLink("linkimage") Is DBNull.Value Then
            If Not drLink("linkimage").ToString() = "" Then
                boolImageExists = True
            End If
        End If

        'Get the Link URL
        Dim strLinkURL As String = drLink("LinkURL")

        If boolImageExists Then
            Dim divLink_Image As HtmlGenericControl = e.Item.FindControl("divLink_Image")
            Dim aLink_Image As HtmlAnchor = e.Item.FindControl("aLink_Image")
            Dim radBinaryImage As RadBinaryImage = e.Item.FindControl("radBinaryImage")
            Dim lit_LinkDescription_Image As Literal = e.Item.FindControl("lit_LinkDescription_Image")

            divLink_Image.Visible = True
            aLink_Image.HRef = strLinkURL
            radBinaryImage.DataValue = drLink("linkImage")

            lit_LinkDescription_Image.Text = drLink("LinkDescription")
        Else
            Dim divLink_NoImage As HtmlGenericControl = e.Item.FindControl("divLink_NoImage")
            Dim aLink_NoImage As HtmlAnchor = e.Item.FindControl("aLink_NoImage")
            Dim lit_LinkName As Literal = e.Item.FindControl("lit_LinkName")
            Dim lit_LinkDescription_NoImage As Literal = e.Item.FindControl("lit_LinkDescription_NoImage")

            divLink_NoImage.Visible = True
            aLink_NoImage.HRef = strLinkURL
            lit_LinkName.Text = drLink("LinkName")

            lit_LinkDescription_NoImage.Text = drLink("LinkDescription")

        End If
    End Sub
End Class
