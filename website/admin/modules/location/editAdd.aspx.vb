Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_location_editAdd
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Dim ModuleTypeID As Integer = 16 ' Module Type: Contact Us

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()


        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Location_Admin.Location_AddEdit_Header

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, intSiteID) Then
                Response.Redirect("/richadmin")
            End If

            'Load all checkbox lists and dropdowns
            BindCategoryDropDownListData()

            If Not Request.QueryString("ID") Is Nothing Then
                Dim intLocationID As Integer = Convert.ToInt32(Request.QueryString("ID"))
                Dim dtLocation As DataTable = LocationDAL.GetLocation_ByID(intLocationID)
                If dtLocation.Rows.Count > 0 Then

                    Dim drLocation As DataRow = dtLocation.Rows(0)

                    btnAddEdit.Text = Resources.Location_Admin.Location_AddEdit_ButtonUpdate


                    Me.txtLocation.Text = drLocation("Location").ToString()

                    If Not drLocation("Address1") Is DBNull.Value Then
                        Me.txtAddress1.Text = drLocation("Address1").ToString()
                    End If

                    If Not drLocation("Address2") Is DBNull.Value Then
                        Me.txtAddress2.Text = drLocation("Address2").ToString()
                    End If

                    If Not drLocation("Address3") Is DBNull.Value Then
                        Me.txtAddress3.Text = drLocation("Address3").ToString()
                    End If

                    If Not drLocation("City") Is DBNull.Value Then
                        Me.txtCity.Text = drLocation("City").ToString()
                    End If

                    If Not drLocation("State_Province") Is DBNull.Value Then
                        Me.txtStateProvince.Text = drLocation("State_Province").ToString()
                    End If

                    If Not drLocation("Zip") Is DBNull.Value Then
                        Me.txtZip.Text = drLocation("Zip").ToString()
                    End If

                    If (drLocation("categoryID").ToString = "") Then
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Location_Admin.Location_AddEdit_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    ElseIf rcbCategoryID.FindItemByValue(drLocation("categoryID")) IsNot Nothing Then
                        rcbCategoryID.SelectedValue = drLocation("CategoryID").ToString
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Location_Admin.Location_AddEdit_UnCategorized, ""))
                    Else
                        rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Location_Admin.Location_AddEdit_UnCategorized, ""))
                        rcbCategoryID.SelectedValue = ""
                    End If


                Else
                    'Cant find this record so send the AdminUser back to the default page
                    Response.Redirect("default.aspx")
                End If

            Else
                btnAddEdit.Text = Resources.Location_Admin.Location_AddEdit_ButtonAdd

                'Set category drop-down to 'Select'
                rcbCategoryID.Items.Add(New RadComboBoxItem(Resources.Location_Admin.Location_AddEdit_UnCategorized, ""))
                rcbCategoryID.SelectedValue = ""

            End If
        End If
    End Sub

    Public Sub BindCategoryDropDownListData()
        'Here we bind the dropdown list to categories
        Dim dtCategory As DataTable = CategoryDAL.GetCategoryList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)

        With rcbCategoryID
            .DataSource = dtCategory
            .DataValueField = "categoryID"
            .DataTextField = "categoryName"

        End With
        rcbCategoryID.DataBind()

    End Sub

    Protected Sub addUpdateLocation()

        If Request.QueryString("ID") Is Nothing Then

            Dim strLocation As String = txtLocation.Text
            Dim strAddress1 As String = txtAddress1.Text
            Dim strAddress2 As String = txtAddress2.Text
            Dim strAddress3 As String = txtAddress3.Text
            Dim strCity As String = txtCity.Text
            Dim strState_Province As String = txtStateProvince.Text
            Dim strZip As String = txtZip.Text

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If

            Dim intLocationID As Integer = LocationDAL.InsertLocation(intCategoryID, strLocation, strAddress1, strAddress2, strAddress3, strCity, strState_Province, strZip)

        Else

            Dim intLocationID As Integer = Request.QueryString("ID")

            Dim strLocation As String = txtLocation.Text
            Dim strAddress1 As String = txtAddress1.Text
            Dim strAddress2 As String = txtAddress2.Text
            Dim strAddress3 As String = txtAddress3.Text
            Dim strCity As String = txtCity.Text
            Dim strState_Province As String = txtStateProvince.Text
            Dim strZip As String = txtZip.Text

            Dim intCategoryID As Integer = Integer.MinValue
            If Not rcbCategoryID.SelectedValue = "" Then
                intCategoryID = Convert.ToInt32(rcbCategoryID.SelectedValue)
            End If

            LocationDAL.UpdateLocation_ByID(intLocationID, intCategoryID, strLocation, strAddress1, strAddress2, strAddress3, strCity, strState_Province, strZip)

        End If
    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click
        If IsValid Then
            addUpdateLocation()
            Response.Redirect("default.aspx")
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("default.aspx")
    End Sub

End Class
