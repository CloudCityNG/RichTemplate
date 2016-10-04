Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class searchTags_SearchBox
    Inherits System.Web.UI.UserControl

    Dim intSiteID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()

        If Not Page.IsPostBack Then
            BindSearchTagsCheckBoxListData()
            If Not Request.QueryString("search") Is Nothing Then
                Dim strSearchTagIDList_Encoded As String = Request.QueryString("search")
                Dim strSearchTagIDList As String = CommonWeb.DecodeBase64String(strSearchTagIDList_Encoded)

                'Here we make tag selections
                For Each strSearchTagID As String In strSearchTagIDList.Split(",")

                    Dim litSearchTag As ListItem = cblSearchTags.Items.FindByValue(strSearchTagID)
                    If Not litSearchTag Is Nothing Then
                        litSearchTag.Selected = True
                    End If

                Next
            End If

        End If

    End Sub

  
    Public Sub BindSearchTagsCheckBoxListData()
        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsList_BySiteID(intSiteID)
        For Each drSearchTag As DataRow In dtSearchTags.Rows
            Dim intSearchTagID As Integer = Convert.ToInt32(drSearchTag("searchTagID"))
            Dim strSearchTagName As String = drSearchTag("SearchTagName")
            Dim strSearchTagDescription As String = ""
            If Not drSearchTag("SearchTagDescription") Is Nothing Then
                strSearchTagDescription = drSearchTag("SearchTagDescription")
            End If
            cblSearchTags.Items.Add(New ListItem(strSearchTagName & "<br/><span class='grayTextSml_10'>" & strSearchTagDescription & "</span>", intSearchTagID))

        Next
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim sbSearchTagIDList As StringBuilder = New StringBuilder()

        For Each liSearchTag As ListItem In cblSearchTags.Items
            If liSearchTag.Selected Then
                sbSearchTagIDList.Append(If(sbSearchTagIDList.Length > 0, "," & liSearchTag.Value, liSearchTag.Value))
            End If
        Next

        Dim strSearchTagIDList As String = sbSearchTagIDList.ToString()

        'we want to add this to an encoded string, to hide id's
        Dim strSearchTagIDList_Encoded As String = CommonWeb.EncodeBase64String(strSearchTagIDList)

        Response.Redirect("Default.aspx?search=" & strSearchTagIDList_Encoded)

    End Sub
End Class
