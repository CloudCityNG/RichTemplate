Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports Telerik.Web.UI
Partial Class admin_modules_categories
    Inherits RichTemplateLanguagePage

    Private ModuleTypeID As Integer = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDelete, Resources.Category_Admin.Category_Default_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadGrid(rgCategory, "{4} {5} " & Resources.Category_Admin.Category_Default_Grid_Pager_PagerTextFormat_ItemsInDefault & " {1} " & Resources.Category_Admin.Category_Default_Grid_Pager_PagerTextFormat_PageDefault)

        If Not Request.QueryString("mtid") Is Nothing Then
            If Request.QueryString("mtid").ToString().Length > 0 Then

                ModuleTypeID = Convert.ToInt32(Request.QueryString("mtid"))

                Dim dtModules As DataTable = ModuleDAL.GetModule_ByModuleTypeIDAndSiteID_FrontEnd(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())
                If dtModules.Rows.Count > 0 Then
                    Dim drModule As DataRow = dtModules.Rows(0)

                    'Does the AdminUser have access to this module?
                    Dim listAllowModules As String() = AdminUserDAL.GetCurrentAdminUserAllowModules().Split(",")
                    If listAllowModules.Contains(ModuleTypeID.ToString()) Then


                        Dim strModuleLocation As String = drModule("moduleLocation_Admin")
                        Dim strModuleLanguageFilename_Admin As String = drModule("moduleLanguageFilename_Admin")


                        'Attach a delete confirmation to our delete button, based on the type of module
                        Dim strDeleteConfirmationMessage As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Category_Default_GridDeleteButton_ConfirmationMessage")
                        CommonWeb.SetupDeleteButton(btnDelete, strDeleteConfirmationMessage)

                        'setup the grid's AddNewEntry label
                        Dim strAddNewEntry As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Category_Default_Grid_Pager_AddNewEntry")
                        rgCategory.MasterTableView.CommandItemSettings.AddNewRecordText = strAddNewEntry

                        'set the grid's PagerTextFormat
                        Dim strPagerTextFormatItemsIn As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Category_Default_Grid_Pager_PagerTextFormat_ItemsIn")
                        Dim strPagerTextFormatPage As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Category_Default_Grid_Pager_PagerTextFormat_Page")
                        rgCategory.PagerStyle.PagerTextFormat = "{4} {5} " & strPagerTextFormatItemsIn & " {1} " & strPagerTextFormatPage

                        'setup the category module heading
                        Dim strCategoryHeading As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Category_Heading")
                        ucHeader.PageName = strCategoryHeading

                        'setup the category module body heading
                        Dim strCategoryHeadingBody As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Category_BodyHeading")
                        litModuleCategoryHeadingBody.Text = strCategoryHeadingBody

                        'setup the return to module link and literal
                        Dim strReturnToModule As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_Category_ReturnToModule")
                        litReturnToModule.Text = strReturnToModule
                        Me.aReturnToModule.HRef = strModuleLocation

                        'Set DataSource parameters
                        dsCategory.SelectParameters("SiteID").DefaultValue = SiteDAL.GetCurrentSiteID_Admin()
                        dsCategory.InsertParameters("SiteID").DefaultValue = SiteDAL.GetCurrentSiteID_Admin()
                    Else
                        'AdminUser does not have access to this module, so send them to the login page
                        Response.Redirect("/richadmin/")
                    End If

                Else
                    'Module Does for this admin, url hacked
                    Response.Redirect("/richadmin/")
                End If

            Else
                'Module Has not been specified, so redirect user to admin landing page
                Response.Redirect("/admin")
            End If
        Else
            Response.Redirect("/admin/")
        End If

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs)

        For Each grdItem As GridDataItem In rgCategory.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("CategoryID")
                CategoryDAL.DeleteCategory_ByCategoryID(intRecordId)
            End If
        Next
        rgCategory.Rebind()
    End Sub


    Protected Sub rgCategory_RowDrop(ByVal sender As Object, ByVal e As GridDragDropEventArgs)
        If String.IsNullOrEmpty(e.HtmlElement) Then
            If e.DraggedItems.Count > 0 And e.DraggedItems(0).OwnerGridID = rgCategory.ClientID Then
                If Not e.DestDataItem Is Nothing And e.DestDataItem.OwnerGridID = rgCategory.ClientID Then
                    'Then its a valid drop so reorder items in RadGrid

                    'Get the categoryID's of our Dragged Row and Destination Row
                    Dim categoryID_DraggedRow = Convert.ToInt32(e.DraggedItems(0).GetDataKeyValue("CategoryID").ToString())
                    Dim categoryID_DestRow = Convert.ToInt32(e.DestDataItem.GetDataKeyValue("CategoryID").ToString())

                    'Get the Category Rows from our dataset
                    Dim dtCategories As DataTable = CategoryDAL.GetCategoryList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())
                    Dim drDraggedItem As DataRow = Nothing
                    Dim dtDraggedItem As DataTable = CategoryDAL.GetCategory_ByCategoryID(categoryID_DraggedRow)
                    If dtDraggedItem.Rows.Count > 0 Then
                        drDraggedItem = dtDraggedItem.Rows(0)
                    End If
                    Dim drDestItem As DataRow = Nothing
                    Dim dtDestItem As DataTable = CategoryDAL.GetCategory_ByCategoryID(categoryID_DestRow)
                    If dtDestItem.Rows.Count > 0 Then
                        drDestItem = dtDestItem.Rows(0)
                    End If


                    If Not drDraggedItem Is Nothing And Not drDestItem Is Nothing Then

                        'Get the Current Rank and the New Rank, these may need updating by one depending on how your selecting dropped reference row
                        Dim drDest_OrderIndex As Integer = drDestItem("OrderIndex")
                        Dim intDestItemIndex As Integer = e.DestDataItem.ItemIndex
                        Dim intDraggedItemIndex As Integer = e.DraggedItems(0).ItemIndex


                        If e.DropPosition = GridItemDropPosition.Below And e.DestDataItem.ItemIndex < intDraggedItemIndex Then
                            drDest_OrderIndex += 1
                        ElseIf e.DropPosition = GridItemDropPosition.Above And e.DestDataItem.ItemIndex > intDraggedItemIndex Then
                            drDest_OrderIndex -= 1
                        End If

                        'Perform the acutal update of our Category rows
                        CategoryDAL.UpdateCategoryOrderIndex(categoryID_DraggedRow, drDest_OrderIndex, ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())
                        rgCategory.Rebind()
                    End If
                End If
            End If
        End If
    End Sub


End Class
