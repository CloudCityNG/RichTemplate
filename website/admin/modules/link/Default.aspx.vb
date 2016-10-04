Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_link_Default
    Inherits System.Web.UI.Page

    Dim ModuleTypeID As Integer = 7
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    End Sub



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then
            'Set the last tab as active
            Dim selectedValue As Integer = 0

            If Not Session("selectedEditTabRoot") Is DBNull.Value And Not Session("selectedEditTabRoot") = "" Then
                selectedValue = Session("selectedEditTabRoot")
            End If
            RadTabStrip1.SelectedIndex = selectedValue
            RadMultiPage1.SelectedIndex = selectedValue
        End If

    End Sub



    Protected Sub btnDeleteLive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In RadGrid1.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("linkID")
                
                LinkDAL.DeleteLink_ByLinkID(intRecordId)
                LinkDAL.DeleteLinkArchive_ByLinkID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        RadGrid1.Rebind()

    End Sub


    Protected Sub btnDeleteArchive_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In RadGrid2.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("linkID")

                LinkDAL.DeleteLink_ByLinkID(intRecordId)
                LinkDAL.DeleteLinkArchive_ByLinkID(intRecordId)
                SearchTagDAL.DeleteSearchTagsXRef_ByModuleTypeIDAndRecordID(ModuleTypeID, intRecordId)
            End If
        Next
        RadGrid2.Rebind()

    End Sub


    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn2 As LinkButton = DirectCast(sender, LinkButton)
        Session("selectedEditTab") = 0
        Response.Redirect("editAdd.aspx")
    End Sub




    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As LinkButton = DirectCast(sender, LinkButton)
        Dim item As GridDataItem = DirectCast(btn.NamingContainer, GridDataItem)
        Dim drvItem As DataRowView = item.DataItem
        Dim drItem As DataRow = drvItem.Row

        Dim strtxt As String = drItem("linkID")
        Session("selectedEditTab") = 0
        Session("linkID") = strtxt
        Response.Redirect("editAdd.aspx?ID=" & strtxt)

    End Sub



End Class
