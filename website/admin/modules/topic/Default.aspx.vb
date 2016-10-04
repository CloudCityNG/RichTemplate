Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_topic_Default
    Inherits System.Web.UI.Page

    Dim ModuleTypeID As Integer = 14
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    End Sub


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then

            'Set the last tab as active
            RadTabStrip1.Tabs.Clear()
            Dim selectedCategory As Integer = -1 '-1 we show all tab, 0 we show uncategorized tab
            If Not Request.QueryString("catid") Is Nothing Then
                selectedCategory = Convert.ToInt32(Request.QueryString("catid"))
            End If

            'Populate Tabs
            Dim dtCategory As DataTable = CategoryDAL.GetCategoryList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())

            'Add the 'Show all' tab
            Dim tabShowAll As New RadTab("Show All", "")
            tabShowAll.NavigateUrl = "default.aspx"
            If selectedCategory = -1 Then
                tabShowAll.Selected = True

            End If
            RadTabStrip1.Tabs.Add(tabShowAll)

            For Each drCategory As DataRow In dtCategory.Rows
                Dim categoryName As String = drCategory("CategoryName")
                Dim categoryID As Integer = Convert.ToInt32(drCategory("CategoryID"))
                Dim tabCategory As New RadTab(categoryName, categoryID)
                tabCategory.NavigateUrl = "?catID=" & categoryID.ToString()

                If selectedCategory = categoryID Then
                    tabCategory.Selected = True
                End If

                RadTabStrip1.Tabs.Add(tabCategory)

            Next
            RadGrid1.Rebind()
        End If

    End Sub


    Protected Sub RadTabStrip1_TabClick(ByVal sender As Object, ByVal e As RadTabStripEventArgs) Handles RadTabStrip1.TabClick
        Session("selectedEditTabRoot") = e.Tab.Value
        Response.Write(e.Tab.Value)
    End Sub 'RadTabStrip1_TabClick

    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In RadGrid1.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("topicID")

                TopicDAL.DeleteTopic_ByTopicID(intRecordId)
            End If
        Next
        RadGrid1.Rebind()
    End Sub


    Protected Sub RadGrid1_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim selectedCategory As Integer = -1 '-1 we show all tab, 0 we show uncategorized tab
        If Not Request.QueryString("catid") Is Nothing Then
            selectedCategory = Convert.ToInt32(Request.QueryString("catid"))
        End If

        'Populate the RadGrid
        Dim dtTopic As DataTable
        If selectedCategory > 0 Then
            dtTopic = TopicDAL.GetTopicList_ByCategoryID(selectedCategory)
        Else
            dtTopic = TopicDAL.GetTopicList()
        End If
        RadGrid1.DataSource = dtTopic
    End Sub
End Class
