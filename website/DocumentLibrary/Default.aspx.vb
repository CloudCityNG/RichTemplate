Imports System.IO
Imports System.Data
Imports Telerik.Web.UI

Partial Class documentLibrary_DocumentSearch
    Inherits RichTemplateLanguagePage

    Dim boolEnableGroupsAndUserAccess As Boolean = False

    Dim ModuleTypeID As Integer = 3

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgDocuments, "{4} {5} " & Resources.DocumentLibrary_FrontEnd.DocumentLibrary_Default_DocumentSearchResults_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.DocumentLibrary_FrontEnd.DocumentLibrary_Default_DocumentSearchResults_Pager_PagerTextFormat_Page)

        'Get module information, to check if we will show its introduction
        Dim dtModule As DataTable = ModuleDAL.GetModule_ByModuleTypeIDAndSiteID_FrontEnd(ModuleTypeID, intSiteID)
        If dtModule.Rows.Count > 0 Then
            Dim drModule As DataRow = dtModule.Rows(0)
            If Not drModule("ModuleContentHTML") Is DBNull.Value Then
                Dim strModuleContentHTML As String = drModule("ModuleContentHTML")

                litModuleDynamicContent.Text = strModuleContentHTML
                divModuleDynamicContent.Visible = True
            End If
        Else
            'We do not have an Active Document Module For the Front-End, so redirect to the homepage
            Response.Redirect("/")
        End If

        'Check we need to show the book this link, but only if its active
        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
        For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
            If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                divActiveArchive.Visible = True

            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_comments" Then
                'hide the aveRating column
                Dim gcAveRating As GridColumn = rgDocuments.Columns.FindByUniqueName("aveRating")
                gcAveRating.Visible = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_submissions" Then
                If intMemberID > 0 Then
                    divAddDocument.Visible = True
                End If
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_groups_and_users" Then
                boolEnableGroupsAndUserAccess = True
            ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_rss" Then
                divModuleHeadingRssFeed.Visible = True
            End If
        Next

        If Not Page.IsPostBack Then

            'Set the Page Title
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.DocumentLibrary_FrontEnd.DocumentLibrary_Default_HeaderTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.DocumentLibrary_FrontEnd.DocumentLibrary_Default_Heading)

            'Prepopulate fields from request parameters
            BindCategoryDropDownList()

            Dim boolStatus As Boolean = True
            If divActiveArchive.Visible And Request.QueryString("archive") = 1 Then
                boolStatus = False
            End If
            rdActive.Checked = boolStatus
            rdArchive.Checked = Not boolStatus


            If Not Request.Params("title") Is Nothing Then
                txtTitle.Text = Request.Params("title")
            End If

            If Not Request.Params("CategoryID") Is Nothing Then
                ddlCategories.SelectedValue = Convert.ToInt32(Request.Params("CategoryID"))
            End If

            'Sets up our document sorting
            Dim sortDate As New GridSortExpression()
            sortDate.SortOrder = GridSortOrder.Ascending
            sortDate.FieldName = "viewDate"

            Dim sortFileTitle As New GridSortExpression()
            sortFileTitle.SortOrder = GridSortOrder.Ascending
            sortFileTitle.FieldName = "fileTitle"

            Dim sortAveRating As New GridSortExpression()
            sortAveRating.SortOrder = GridSortOrder.Ascending
            sortAveRating.FieldName = "aveRating"



            rgDocuments.MasterTableView.SortExpressions.AddSortExpression(sortFileTitle)
            rgDocuments.MasterTableView.SortExpressions.AddSortExpression(sortDate)
            rgDocuments.MasterTableView.SortExpressions.AddSortExpression(sortAveRating)

            rgDocuments.Rebind()
        End If

    End Sub 'Page_Load

    Private Sub BindCategoryDropDownList()
        'Here we bind the dropdown list to categories
        Dim dtCategory As DataTable = If(boolEnableGroupsAndUserAccess, CategoryDAL.GetCategoryList_WithCount_ByModuleTypeIDAndStatusAndAccess(ModuleTypeID, intSiteID, True, "documentID", "PublicationDate", "ExpirationDate", MemberDAL.GetCurrentMemberGroupIDs(), intMemberID), CategoryDAL.GetCategoryList_WithCount_ByModuleTypeIDAndStatus(ModuleTypeID, intSiteID, True, "PublicationDate", "ExpirationDate"))

        ddlCategories.Items.Clear()
        ddlCategories.Items.Add(New ListItem("-- " + Resources.DocumentLibrary_FrontEnd.DocumentLibrary_Default_DocumentSearch_Category_AllCategories + " --", "0"))
        For Each drCategory As DataRow In dtCategory.Rows
            ddlCategories.Items.Add(New ListItem(drCategory("categoryName"), drCategory("categoryID")))
        Next
    End Sub

    Protected Sub docRating_Rate(ByVal sender As Object, ByVal e As System.EventArgs)
        'Get the documentID
        Dim docRating As RadRating = sender
        Dim cGridDataItem As GridDataItem = docRating.Parent.Parent
        Dim intDocumentID As Integer = Convert.ToInt32(rgDocuments.MasterTableView.DataKeyValues(cGridDataItem.ItemIndex)("documentID"))

        'Get the rating
        Dim decRating As Decimal = docRating.Value

        'Send to the document page, and supply documentID and rating
        Response.Redirect("DocumentDetail.aspx?id=" & intDocumentID.ToString() & If(rdArchive.Checked, "&archive=1", "") & "&r=" & decRating.ToString() & "#addcomment")
    End Sub

    Protected Sub rgDocuments_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgDocuments.NeedDataSource

        Dim boolStatus As Boolean = True
        If rdArchive.Checked Then
            boolStatus = False
        End If

        Dim strTitle As String = txtTitle.Text.Trim()

        Dim intCategoryID As Integer = ddlCategories.SelectedValue

        Dim sbDocumentList_SearchSQL As New StringBuilder()
        If strTitle.Length > 0 Then
            If sbDocumentList_SearchSQL.Length > 0 Then
                sbDocumentList_SearchSQL.Append(" AND ")
            End If
            sbDocumentList_SearchSQL.Append("FileTitle LIKE '%" + strTitle + "%'")
        End If
        If intCategoryID > 0 Then
            If sbDocumentList_SearchSQL.Length > 0 Then
                sbDocumentList_SearchSQL.Append(" AND ")
            End If
            sbDocumentList_SearchSQL.Append("ssmc.CategoryID = " & intCategoryID)
        End If

        Dim strDocumentList_SearchSQL As String = sbDocumentList_SearchSQL.ToString()
        If strDocumentList_SearchSQL.Length > 0 Then
            strDocumentList_SearchSQL = " AND " & strDocumentList_SearchSQL
        End If

        Dim dtDocuments As DataTable = If(boolEnableGroupsAndUserAccess, DocumentDAL.GetDocumentList_ForSearch_ByStatusAndAccess_FrontEnd(strDocumentList_SearchSQL, boolStatus, MemberDAL.GetCurrentMemberGroupIDs(), intMemberID, intSiteID), DocumentDAL.GetDocumentList_ForSearch_ByStatus_FrontEnd(strDocumentList_SearchSQL, boolStatus, intSiteID))

        rgDocuments.DataSource = dtDocuments
    End Sub

    Protected Sub rgDocuments_SortCommand(ByVal sender As Object, ByVal e As GridSortCommandEventArgs) Handles rgDocuments.SortCommand

        Dim sortExpr As New GridSortExpression
        Select Case e.OldSortOrder

            Case GridSortOrder.Ascending
                sortExpr.FieldName = e.SortExpression
                sortExpr.SortOrder = If(rgDocuments.MasterTableView.AllowNaturalSort, GridSortOrder.None, GridSortOrder.Descending)
                e.Item.OwnerTableView.SortExpressions.AddAt(0, sortExpr)
            Case GridSortOrder.Descending
                sortExpr.FieldName = e.SortExpression
                sortExpr.SortOrder = GridSortOrder.Ascending
                e.Item.OwnerTableView.SortExpressions.AddAt(0, sortExpr)
            Case Else
                sortExpr.FieldName = e.SortExpression
                sortExpr.SortOrder = GridSortOrder.Descending
        End Select
        e.Canceled = True
        rgDocuments.Rebind()
    End Sub

    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkGo.Click
        rgDocuments.Rebind()
    End Sub

End Class
