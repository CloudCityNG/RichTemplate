Imports System.Data
Imports System.Data.SqlClient

Partial Class admin_modules_link_Config
    Inherits System.Web.UI.Page


    Dim ModuleTypeID As Integer = 7
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            BindOptions()
        End If

    End Sub

    Public Sub BindOptions()
        Dim dtOptions As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin())
        For Each drOption As DataRow In dtOptions.Rows
            Dim fieldText As String = drOption("fieldText").ToString()
            Dim fieldID As String = drOption("configID").ToString()
            Dim fieldSelected As Boolean = Convert.ToBoolean(drOption("fieldValue").ToString())
            Dim liOption As New ListItem(fieldText, fieldID)
            If fieldSelected Then
                liOption.Selected = True
            End If
            CheckBoxList1.Items.Add(liOption)
        Next


    End Sub




    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        For Each MyItem As ListItem In CheckBoxList1.Items
            Dim fieldID As Integer = Convert.ToInt32(MyItem.Value)
            Dim fieldSelected As Boolean = MyItem.Selected
            'ModuleDAL.UpdateModuleConfig_ValueByConfigID(fieldID, fieldSelected) <- compile problem
        Next
        Response.Redirect("default.aspx")

    End Sub
End Class
