Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports Telerik.Web.UI
Imports Subgurim.Controles

Partial Class link_LinkDetail
    Inherits System.Web.UI.UserControl

    Dim ModuleTypeID As Integer = 7

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            'Check we need to show the book this link
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteDAL.GetCurrentSiteID_FrontEnd())
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "linkshowaddthis" Then
                    addThisPlaceholder.Visible = True
                End If
            Next

            If Not Request.QueryString("id") Is Nothing Then
                Dim linkID As String = Request.QueryString("id")

                Dim dtLink As DataTable = LinkDAL.GetLink_ByLinkID(linkID)
                If dtLink.Rows.Count > 0 Then
                    Dim drLink As DataRow = dtLink.Rows(0)

                    'Loads the Link
                    lit_LinkCategory.Text = "Un-categorized"
                    If Not drLink("CategoryID") Is DBNull.Value Then
                        Dim intLinkCategoryID = Convert.ToInt32(drLink("CategoryID"))
                        Dim dtCategory As DataTable = CategoryDAL.GetCategory_ByCategoryID(intLinkCategoryID)
                        If dtCategory.Rows.Count > 0 Then
                            Dim drCategory As DataRow = dtCategory.Rows(0)
                            Dim strCategoryName As String = drCategory("CategoryName")

                            lit_LinkCategory.Text = strCategoryName
                        End If
                    End If

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
                        divLink_Image.Visible = True
                        aLink_Image.HRef = strLinkURL
                        radBinaryImage.DataValue = drLink("linkImage")
                        lit_LinkDescription_Image.Text = drLink("LinkDescription")
                    Else
                        divLink_NoImage.Visible = True
                        aLink_NoImage.HRef = strLinkURL
                        lit_LinkName.Text = drLink("LinkName")
                        lit_LinkDescription_NoImage.Text = drLink("LinkDescription")
                    End If

                End If
            End If
        End If

    End Sub

End Class
