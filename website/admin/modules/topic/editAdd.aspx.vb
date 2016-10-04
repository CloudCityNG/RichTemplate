Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Partial Class admin_modules_topic_editAdd
    Inherits System.Web.UI.Page

    Dim ModuleTypeID As Integer = 14
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim viewDocuments As String() = New String() {"~/data/files"}
        Dim uploadDocuments As String() = New String() {"~/data/files"}
        Dim deleteDocuments As String() = New String() {"~/data/files"}

        Dim viewImages As String() = New String() {"~/data/images"}
        Dim uploadImages As String() = New String() {"~/data/images"}
        Dim deleteImages As String() = New String() {"~/data/images"}

        Dim viewMedia As String() = New String() {"~/data/media"}
        Dim uploadMedia As String() = New String() {"~/data/media"}
        Dim deleteMedia As String() = New String() {"~/data/media"}


        If Not Page.IsPostBack Then

            topicBody.ToolsFile = "~/editorConfig/toolbars/ToolsFileCustom.xml"
            topicBody.CssFiles.Add("~/editorConfig/css/editorStyle.css")
            topicBody.DocumentManager.ViewPaths = viewDocuments
            topicBody.DocumentManager.UploadPaths = uploadDocuments
            topicBody.DocumentManager.DeletePaths = deleteDocuments
            topicBody.ImageManager.ViewPaths = viewImages
            topicBody.ImageManager.UploadPaths = uploadImages
            topicBody.ImageManager.DeletePaths = deleteImages
            topicBody.MediaManager.ViewPaths = viewMedia
            topicBody.MediaManager.UploadPaths = uploadMedia
            topicBody.MediaManager.DeletePaths = deleteMedia

            'Load all checkbox lists and dropdowns
            BindCategoryDropDownListData()

            'Get Topic
            If Not Request.QueryString("topicID") Then
                Dim topicID As Integer = Convert.ToInt32(Request.QueryString("topicID"))

                Dim dtTopic As DataTable = TopicDAL.GetTopic_ByTopicID(topicID)
                If dtTopic.Rows.Count > 0 Then
                    Dim drTopic As DataRow = dtTopic.Rows(0)
                    topicName.Text = drTopic("Topicname")
                    topicBody.Content = drTopic("TopicBody")

                    If (drTopic("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem("Uncategorized", ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drTopic("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drTopic("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem("Uncategorized", ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem("Uncategorized", ""))
                        rcbCategoryID.SelectedValue = ""
                    End If
                    Status.SelectedValue = drTopic("Status").ToString

                    btnAddEdit.Text = "Update"
                Else
                    btnAddEdit.Text = "Add"
                    Status.SelectedValue = True


                    rcbCategoryID.Items.Add(New RadComboBoxItem("Uncategorized", ""))
                    rcbCategoryID.SelectedValue = ""
                End If
            Else
                btnAddEdit.Text = "Add"
                Status.SelectedValue = True


                rcbCategoryID.Items.Add(New RadComboBoxItem("Uncategorized", ""))
                rcbCategoryID.SelectedValue = ""
            End If
        End If


    End Sub

    Public Sub BindCategoryDropDownListData()
        'Here we bind the dropdown list to categories
        Dim dtCategory As DataTable = CategoryDAL.GetCategoryList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())

        With rcbCategoryID
            .DataSource = dtCategory
            .DataValueField = "categoryID"
            .DataTextField = "categoryName"

        End With
        rcbCategoryID.DataBind()

    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click
        addUpdateRecord()
    End Sub

    Protected Sub addUpdateRecord()
        If Request.QueryString("topicID") Is Nothing Then

            Dim strTopicName As String = topicName.Text.Trim()
            Dim strTopicBody As String = topicBody.Content.ToString()

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)
            Dim dtTimeStamp As DateTime = DateTime.Now

            Dim topicID As Integer = TopicDAL.InsertTopic(strTopicName, intCategoryID, strTopicBody, boolStatus, dtTimeStamp)

        Else
            Dim topicID As String = Request.QueryString("topicID")
            Dim strTopicName As String = topicName.Text.Trim()
            Dim strTopicBody As String = topicBody.Content.ToString()

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)
            Dim dtTimeStamp As DateTime = DateTime.Now

            TopicDAL.UpdateTopic_ByTopicID(topicID, strTopicName, intCategoryID, strTopicBody, boolStatus, dtTimeStamp)

        End If

        Response.Redirect("default.aspx")

    End Sub

    Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelButton.Click
        Response.Redirect("default.aspx")
    End Sub
End Class
