Imports System.Data

Partial Class admin_usercontrols_NeedHelp
    Inherits System.Web.UI.UserControl

    Private _helpItemID As Integer = 0
    Public Property HelpItemID() As Integer
        Get
            Return _helpItemID
        End Get
        Set(ByVal value As Integer)
            _helpItemID = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub Show()
        'Reset the control
        divHelp_All.Visible = False
        divHelp_Individual.Visible = False

        'Only show the individual Help item if a HelpItem ID is passed in and it has a valid row in our Help Table, else show all help items
        If HelpItemID > 0 Then
            Dim dtHelp As DataTable = HelpDAL.GetHelp_ByID(HelpItemID)
            If dtHelp.Rows.Count > 0 Then
                Dim drHelp As DataRow = dtHelp.Rows(0)
                divHelp_Individual.Visible = True
                LoadHelpItems(HelpItemID)
            Else
                divHelp_All.Visible = True
                LoadHelpItems(0)

            End If
        Else
            divHelp_All.Visible = True
            LoadHelpItems(0)

        End If


        'Show the panel and the tooltip
        pnl_NeedHelp.Visible = True
        rtt_NeedHelp.Show()
    End Sub

    Protected Sub LoadHelpItems(ByVal foundHelpItemID As Integer)
        Dim dtHelpItems As DataTable = HelpDAL.GetHelp_List()
        Dim dcPrimaryKeys(1) As DataColumn
        dcPrimaryKeys(0) = dtHelpItems.Columns("ID")
        dtHelpItems.PrimaryKey = dcPrimaryKeys




        Dim boolHelpItemSet As Boolean = False
        If foundHelpItemID > 0 Then
            Dim drFoundHelpItem As DataRow = dtHelpItems.Rows.Find(foundHelpItemID)
            If Not drFoundHelpItem Is Nothing Then
                Dim strHelpTitle As String = drFoundHelpItem("Title")
                Dim strHelpHtmlContent As String = drFoundHelpItem("HtmlContent")
                litHelpTitle.Text = strHelpTitle
                litHelpHtmlContent.Text = strHelpHtmlContent
                boolHelpItemSet = True
            End If
        End If

        If boolHelpItemSet = False Then
            litHelpTitle.Text = "** HELP ITEM NOT FOUND!!! ***"
            litHelpHtmlContent.Text = "** HELP ITEM NOT FOUND!!! ***"
        End If

        rptTableOfConents.DataSource = dtHelpItems
        rptTableOfConents.DataBind()

        'Load in the help content
        rptHelpContent.DataSource = dtHelpItems
        rptHelpContent.DataBind()


    End Sub

    Protected Sub lnkShowAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkShowAll.Click

        'Hide the individual help item and show all help items along with their table of contents
        divHelp_All.Visible = True
        divHelp_Individual.Visible = False

        'Show the modal, as it closes on post-back
        rtt_NeedHelp.Show()
    End Sub

    Public PageURL As String = HttpContext.Current.Request.Url.AbsoluteUri.ToString()
    Public Function encodeHelpItemTitle(ByVal strTitle As Object) As String

        strTitle = Replace(strTitle, "%", "-")
        strTitle = Replace(strTitle, " ", "-")
        strTitle = Replace(strTitle, "#", "-")
        strTitle = Replace(strTitle, "(", "-")
        strTitle = Replace(strTitle, ")", "-")
        strTitle = Replace(strTitle, "&", "-")
        strTitle = Replace(strTitle, "?", "-")
        strTitle = Replace(strTitle, "+", "-")
        strTitle = Replace(strTitle, "\", "-")
        strTitle = Replace(strTitle, "/", "-")
        strTitle = Replace(strTitle, "*", "-")
        strTitle = Replace(strTitle, ":", "-")
        strTitle = Replace(strTitle, ",", "-")
        strTitle = Replace(strTitle, ".", "-")
        strTitle = Replace(strTitle, "'", "-")

        Return strTitle

    End Function

End Class
