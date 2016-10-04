Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_event_editAdd
    Inherits System.Web.UI.Page

    Dim ModuleTypeID As Integer = 7

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    End Sub


    Dim lastClickedTab As RadTab
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If Not Page.IsPostBack Then

            'Set the last tab as active
            Dim selectedValue As Integer = 0

            If Not Session("selectedEditTabRoot") Is DBNull.Value And Not Session("selectedEditTabRoot") = "" Then
                selectedValue = Session("selectedEditTabRoot")
            End If
            RadTabStrip1.SelectedIndex = selectedValue
            RadMultiPage1.SelectedIndex = selectedValue

            'Load all checkbox lists and dropdowns
            BindGroupCheckBoxListData()
            BindUserCheckBoxListData()
            BindCategoryDropDownListData()
            BindSearchTagsCheckBoxListData()

            If Not Request.QueryString("ID") Is Nothing Then

                Dim linkID As Integer = Convert.ToInt32(Request.QueryString("ID"))

                Dim dtLink As DataTable = LinkDAL.GetLink_ByLinkID(linkID)
                If dtLink.Rows.Count > 0 Then
                    Dim drLink As DataRow = dtLink.Rows(0)

                    btnAddEdit.Text = "Update"
                    'If data is found, fill textboxes

                    Me.linkname.Text = drLink("linkname").ToString()
                    Me.linkurl.Text = drLink("linkurl").ToString()
                    Me.linkdescription.Text = drLink("linkdescription").ToString()

                    If Not drLink("linkimage") Is DBNull.Value Then
                        If Not drLink("linkimage").ToString() = "" Then

                            Me.linkimage.DataValue = drLink("linkimage")
                            Me.linkimage.Visible = True
                        End If
                    End If

                    Me.Status.SelectedValue = drLink("Status").ToString
                    Me.metaTitle.Text = drLink("metaTitle").ToString()
                    Me.metaKeywords.Text = drLink("metaKeywords").ToString()
                    Me.metaDescription.Text = drLink("metaDescription").ToString()
                    Me.metaOther.Text = drLink("metaOther").ToString()

                    If (drLink("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem("Uncategorized", ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drLink("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drLink("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem("Uncategorized", ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem("Uncategorized", ""))
                        rcbCategoryID.SelectedValue = ""
                    End If

                    version.Text = drLink("Version")
                    Me.author.Text = drLink("author").ToString
                    Me.modifiedBy.Text = Session("adminID")

                    If Not drLink("ReleaseDate").ToString() = "" Then
                        Me.ReleaseDate.SelectedDate = drLink("ReleaseDate").ToString()
                    End If
                    If Not drLink("ExpirationDate").ToString() = "" Then
                        Me.ExpirationDate.SelectedDate = drLink("ExpirationDate").ToString()
                    End If

                    'Here we make group selections
                    Dim i As Integer
                    Dim items As String() = drLink("groupID").ToString().Split(",")
                    For i = 0 To items.GetUpperBound(0)
                        Dim currentCheckBox As ListItem
                        currentCheckBox = cblGroupList.Items.FindByValue(items(i).ToString())
                        If currentCheckBox IsNot Nothing Then
                            currentCheckBox.Selected = True
                        End If
                    Next

                    'Here we make user selections
                    Dim j As Integer
                    Dim items2 As String() = drLink("userID").ToString().Split(",")
                    For j = 0 To items2.GetUpperBound(0)
                        Dim currentCheckBox2 As ListItem
                        currentCheckBox2 = cblMemberList.Items.FindByValue(items2(j).ToString())
                        If currentCheckBox2 IsNot Nothing Then
                            currentCheckBox2.Selected = True
                        End If
                    Next

                    '*** Populate search tags cbl ***
                    Dim chkbx As CheckBoxList
                    chkbx = CType(cblSearchTags, CheckBoxList)

                    Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin(), linkID)

                    For Each drSearchTag In dtSearchTags.Rows
                        Dim currentCheckBox As ListItem
                        currentCheckBox = chkbx.Items.FindByValue(drSearchTag("searchTagID").ToString())
                        If currentCheckBox IsNot Nothing Then
                            currentCheckBox.Selected = True
                        End If
                    Next

                Else
                    btnAddEdit.Text = "Add"
                    Dim liAuthenticatedGroup As ListItem = cblGroupList.Items.FindByValue("0")
                    If Not liAuthenticatedGroup Is Nothing Then
                        liAuthenticatedGroup.Selected = True
                    End If
                    Me.Status.SelectedValue = True
                    Me.author.Text = Session("adminID")

                    rcbCategoryID.Items.Add(New RadComboBoxItem("Uncategorized", ""))
                    rcbCategoryID.SelectedValue = ""
                End If

            Else
                btnAddEdit.Text = "Add"
                Dim liAuthenticatedGroup As ListItem = cblGroupList.Items.FindByValue("0")
                If Not liAuthenticatedGroup Is Nothing Then
                    liAuthenticatedGroup.Selected = True
                End If
                Me.Status.SelectedValue = True
                Me.author.Text = Session("adminID")

                rcbCategoryID.Items.Add(New RadComboBoxItem("Uncategorized", ""))
                rcbCategoryID.SelectedValue = ""
            End If

        End If

    End Sub


    Public Sub BindGroupCheckBoxListData()

        Dim dtGroups As DataTable = GroupDAL.GetGroupList_BySiteID(SiteDAL.GetCurrentSiteID_Admin())

        cblGroupList.Items.Add(New ListItem("Everyone<br/><span class='graySubText'>Content avaliable to everyone</span>", "0"))

        For Each drGroup As DataRow In dtGroups.Rows
            Dim intGroupID As String = drGroup("groupID").ToString()
            Dim strGroupName As String = (drGroup("groupName").ToString() & "<br/><span class='graySubText'>") & drGroup("groupDescription").ToString() & "</span>"
            cblGroupList.Items.Add(New ListItem(strGroupName, intGroupID))
        Next

    End Sub
    Public Sub BindUserCheckBoxListData()
        Dim dtMembers As DataTable = MemberDAL.GetMemberList_BySiteID(SiteDAL.GetCurrentSiteID_Admin())
        For Each drMember In dtMembers.Rows()
            Dim intMemberID As String = drMember("id").ToString()

            Dim strSalutation_LangaugeSpecific As String = String.Empty
            If Not drMember("Salutation_LanguageProperty") Is DBNull.Value Then
                Dim strSalutation_LanguageProperty As String = drMember("Salutation_LanguageProperty")
                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
            End If

            Dim strMemberName As String = CommonWeb.FormatName(strSalutation_LangaugeSpecific, drMember("firstName").ToString(), drMember("lastName"))
            cblMemberList.Items.Add(New ListItem(strMemberName, intMemberID))
        Next

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
    Public Sub BindSearchTagsCheckBoxListData()
        Dim dtSearchTags As DataTable = SearchTagDAL.GetSearchTagsList_BySiteID(SiteDAL.GetCurrentSiteID_Admin())

        For Each drSearchTag As DataRow In dtSearchTags.Rows()
            Dim intSearchTagID As String = drSearchTag("searchTagID").ToString()
            Dim strSearchTagName As String = (drSearchTag("searchTagName").ToString() & "<br/><span class='graySubText'>") + drSearchTag("searchTagDescription").ToString() & "</span>"
            cblSearchTags.Items.Add(New ListItem(strSearchTagName, intSearchTagID))
        Next
    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click

        addUpdateRecord()
        Response.Redirect("default.aspx")


    End Sub

    Protected Sub addUpdateRecord()

        'Need to pre-pend http:// if it doesn't exist in the url
        Me.linkurl.Text = CommonWeb.FormatURL(linkurl.Text)

        If Request.QueryString("ID") Is Nothing Then

            'Read all group ceckboxes and create an array
            Dim i As Integer
            Dim chkbx As CheckBoxList
            chkbx = CType(Me.cblGroupList, CheckBoxList)
            Dim sb As StringBuilder = New StringBuilder()
            For i = 0 To chkbx.Items.Count - 1
                If chkbx.Items(i).Selected Then
                    sb.Append(chkbx.Items(i).Value & ",")
                End If
            Next
            'Create the value to be inserted by removing the last comma in sb
            Dim groupIDArray As String
            If sb.Length > 0 Then
                groupIDArray = Left(sb.ToString(), Len(sb.ToString()) - 1)
            Else
                groupIDArray = ""
            End If


            'Read all user checkboxes and create an array
            Dim j As Integer
            Dim chkbx2 As CheckBoxList
            chkbx2 = CType(Me.cblMemberList, CheckBoxList)
            Dim sb2 As StringBuilder = New StringBuilder()
            For j = 0 To chkbx2.Items.Count - 1
                If chkbx2.Items(j).Selected Then
                    sb2.Append(chkbx2.Items(j).Value & ",")
                End If
            Next
            'Create the value to be inserted by removing the last comma in sb
            Dim userIDArray As String
            If sb2.Length > 0 Then
                userIDArray = Left(sb2.ToString(), Len(sb2.ToString()) - 1)
            Else
                userIDArray = ""
            End If


            Dim strLinkName As String = linkname.Text.Trim().ToString()
            Dim strLinkURL As String = linkurl.Text.Trim().ToString()
            Dim strLinkDescription As String = linkdescription.Text.Trim().ToString()

            Dim bytesLinkImage() As Byte
            If RadUpload1.UploadedFiles.Count > 0 Then
                For Each file As UploadedFile In RadUpload1.UploadedFiles
                    ReDim bytesLinkImage(file.InputStream.Length - 1)
                    file.InputStream.Read(bytesLinkImage, 0, file.InputStream.Length)
                Next
            End If

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If

            Dim dtRelease As DateTime = DateTime.MinValue
            If Not ReleaseDate.SelectedDate.ToString() = "" Then
                dtRelease = ReleaseDate.SelectedDate
            End If
            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not ExpirationDate.SelectedDate.ToString() = "" Then
                dtExpiration = ExpirationDate.SelectedDate
            End If

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)
            Dim strMetaTitle As String = metaTitle.Text.Trim()
            Dim strMetaKeywords As String = metaKeywords.Text.Trim()
            Dim strMetaDescription As String = metaDescription.Text.Trim()
            Dim strMetaOther As String = metaOther.Text.Trim()
            Dim listGroupIDs As String = String.Empty
            Dim listMemberIDs As String = String.Empty


            Dim strSearchTagID As String = String.Empty

            Dim intVersion As Integer = 1
            Dim dtDateTimeStamp As DateTime = DateTime.Now

            Dim intAuthor As Integer = Convert.ToInt32(author.Text.Trim())
            Dim intModifiedBy As Integer = Integer.MinValue

            Dim linkID As Integer = LinkDAL.InsertLink(strLinkName, strLinkDescription, strLinkURL, bytesLinkImage, intCategoryID, dtRelease, dtExpiration, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, dtDateTimeStamp, intAuthor, intModifiedBy)


            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, linkID)
                End If
            Next

        Else

            Dim LinkID As Integer = Request.QueryString("ID")

            'Read all group ceckboxes and create an array
            Dim i As Integer
            Dim chkbx As CheckBoxList
            chkbx = CType(Me.cblGroupList, CheckBoxList)
            Dim sb As StringBuilder = New StringBuilder()
            For i = 0 To chkbx.Items.Count - 1
                If chkbx.Items(i).Selected Then
                    sb.Append(chkbx.Items(i).Value & ",")
                End If
            Next
            'Create the value to be inserted by removing the last comma in sb
            Dim groupIDArray As String
            If sb.Length > 0 Then
                groupIDArray = Left(sb.ToString(), Len(sb.ToString()) - 1)
            Else
                groupIDArray = ""
            End If


            'Read all user checkboxes and create an array
            Dim j As Integer
            Dim chkbx2 As CheckBoxList
            chkbx2 = CType(Me.cblMemberList, CheckBoxList)
            Dim sb2 As StringBuilder = New StringBuilder()
            For j = 0 To chkbx2.Items.Count - 1
                If chkbx2.Items(j).Selected Then
                    sb2.Append(chkbx2.Items(j).Value & ",")
                End If
            Next
            'Create the value to be inserted by removing the last comma in sb
            Dim userIDArray As String
            If sb2.Length > 0 Then
                userIDArray = Left(sb2.ToString(), Len(sb2.ToString()) - 1)
            Else
                userIDArray = ""
            End If




            Dim strLinkName As String = linkname.Text.Trim().ToString()
            Dim strLinkURL As String = linkurl.Text.Trim().ToString()
            Dim strLinkDescription As String = linkdescription.Text.Trim().ToString()
            Dim bytesLinkImage() As Byte
            If RadUpload1.UploadedFiles.Count > 0 Then
                For Each file As UploadedFile In RadUpload1.UploadedFiles
                    ReDim bytesLinkImage(file.InputStream.Length - 1)
                    file.InputStream.Read(bytesLinkImage, 0, file.InputStream.Length)
                Next
            End If

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If

            Dim dtRelease As DateTime = DateTime.MinValue
            If Not ReleaseDate.SelectedDate.ToString() = "" Then
                dtRelease = ReleaseDate.SelectedDate
            End If
            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not ExpirationDate.SelectedDate.ToString() = "" Then
                dtExpiration = ExpirationDate.SelectedDate
            End If

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)
            Dim strMetaTitle As String = metaTitle.Text.Trim()
            Dim strMetaKeywords As String = metaKeywords.Text.Trim()
            Dim strMetaDescription As String = metaDescription.Text.Trim()
            Dim strMetaOther As String = metaOther.Text.Trim()
            Dim listGroupIDs As String = String.Empty
            Dim listMemberIDs As String = String.Empty


            Dim strSearchTagID As String = String.Empty

            Dim intVersion As Integer = 1
            If Not IsDBNull(Version.Text.Trim()) And Version.Text.Trim() <> "" Then
                intVersion = Convert.ToInt32(Version.Text.Trim())
                intVersion = intVersion + 1
            End If

            Dim dtDateTimeStamp As DateTime = DateTime.Now

            Dim intAuthor As Integer = Convert.ToInt32(author.Text.Trim())
            Dim intModifiedBy As Integer = Integer.MinValue
            If Not Session("adminID") = "" Then
                intModifiedBy = Session("adminID")
            End If

            LinkDAL.UpdateLink_ByLinkID(LinkID, strLinkName, strLinkDescription, strLinkURL, bytesLinkImage, intCategoryID, dtRelease, dtExpiration, boolStatus, strMetaTitle, strMetaKeywords, strMetaDescription, strMetaOther, listGroupIDs, listMemberIDs, strSearchTagID, intVersion, dtDateTimeStamp, intAuthor, intModifiedBy)


            'Remove all tags before entering new list
            SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndSiteIDAndRecordID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin(), LinkID)

            'Enter all new tags
            For Each liSearchTagID As ListItem In cblSearchTags.Items
                If liSearchTagID.Selected Then
                    Dim intSearchTagID As Integer = Convert.ToInt32(liSearchTagID.Value)
                    SearchTagDAL.InsertSearchTagXRef(intSearchTagID, ModuleTypeID, LinkID)
                End If
            Next

        End If

    End Sub


    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In RadGrid1.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("archiveID")
                LinkDAL.DeleteLinkArchive_ByArchiveID(intRecordId)
            End If
        Next
        RadGrid1.Rebind()
    End Sub

  

    Protected Sub categoryLinkButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles categoryLinkButton.Click

        'Save progress and send to category page
        addUpdateRecord()
        Dim qs As String
        If Request.QueryString("ID") <> "" Then
            qs = "?ID=" & Request.QueryString("ID")
        Else
            qs = ""
        End If

        Response.Redirect("~/admin/modules/categories/default.aspx?rp=" & Request.Path.ToString.ToLower & qs & "&mtid=" & ModuleTypeID)
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn2 As LinkButton = DirectCast(sender, LinkButton)
        Response.Redirect("editAdd.aspx")
    End Sub

    Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelButton.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub ViewButton1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As LinkButton = DirectCast(sender, LinkButton)
        Dim item As GridDataItem = DirectCast(btn.NamingContainer, GridDataItem)
        Dim drvItem As DataRowView = item.DataItem
        Dim drItem As DataRow = drvItem.Row

        Dim strtxt As String = drItem("archiveID")
        Session("archiveID") = strtxt
        Response.Redirect("preview.aspx?action=restore")

    End Sub

    Public Sub RadGrid1_ItemDataBound(ByVal source As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs)
        If TypeOf e.Item Is GridDataItem Then
            Dim editLink As HyperLink = DirectCast(e.Item.FindControl("EditLink"), HyperLink)
            editLink.Attributes("href") = String.Format("preview.aspx?archiveID={0}", e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("archiveID"))
        End If
    End Sub


    Protected Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        If e.Argument = "Rebind" Then
            RadGrid1.MasterTableView.SortExpressions.Clear()
            RadGrid1.MasterTableView.GroupByExpressions.Clear()
            RadGrid1.Rebind()
        ElseIf e.Argument = "RebindAndNavigate" Then
            RadGrid1.MasterTableView.SortExpressions.Clear()
            RadGrid1.MasterTableView.GroupByExpressions.Clear()
            RadGrid1.MasterTableView.CurrentPageIndex = RadGrid1.MasterTableView.PageCount - 1
            RadGrid1.Rebind()
        End If
    End Sub

    Protected Sub RadTabStrip1_TabClick(ByVal sender As Object, ByVal e As RadTabStripEventArgs) Handles RadTabStrip1.TabClick
        Session("selectedEditTab") = e.Tab.Value

    End Sub 'RadTabStrip1_TabClick

    Protected Sub lnkAddSearchTags_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAddSearchTags.Click

        'Save progress and send to category page
        addUpdateRecord()
        Dim qs As String
        If Request.QueryString("ID") <> "" Then
            qs = "?ID=" & Request.QueryString("ID")
        Else
            qs = ""
        End If
        Response.Redirect("~/admin/modules/searchtags/default.aspx?mtid=" & ModuleTypeID.ToString() & "&rp=" & Request.Path.ToString.ToLower & qs)

    End Sub


End Class
