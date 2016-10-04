
Partial Class admin_usercontrols_Header
    Inherits System.Web.UI.UserControl

    Private _PageName As String = ""
    Public Property PageName() As String
        Get
            Return If(_PageName Is Nothing, "", _PageName)
        End Get
        Set(ByVal value As String)
            _PageName = value
        End Set
    End Property

    Private _PageHelpID As Integer = 0
    Public Property PageHelpID() As Integer
        Get
            Return _PageHelpID
        End Get
        Set(ByVal value As Integer)
            _PageHelpID = value
        End Set
    End Property

    Private _ShowFindBug As Boolean = True 'Default to show this item
    Public Property ShowFindBug() As Boolean
        Get
            Return _ShowFindBug
        End Get
        Set(ByVal value As Boolean)
            _ShowFindBug = value
        End Set
    End Property

    Private _ShowLogout As Boolean = True 'Default to show this item
    Public Property ShowLogout() As Boolean
        Get
            Return _ShowLogout
        End Get
        Set(ByVal value As Boolean)
            _ShowLogout = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Check if we should show page name
        spanPageName.Visible = Me.PageName.Length > 0

        'Check if we should show page help
        tdNeedHelp.Visible = PageHelpID > 0

        'Check if we should show 'Find a Bug'
        tdFindBug.Visible = ShowFindBug

        'Check if we should show 'Logout'
        tdLogout.Visible = ShowLogout

        'Also set our modal popup to automatically adjust its size when the loaded
        CommonWeb.GeneratePopupResizeJsScript(Me, New String() {"div_ScrollerNeedHelp", "div_ScrollerFindBug"}, New Integer() {800, 800}, New Integer() {80, 80}, False)


    End Sub


    Protected Sub lnkNeedHelp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkNeedHelp.Click
        'Load the help item id
        ucNeedHelp.HelpItemID = PageHelpID()
        ucNeedHelp.Show()

    End Sub

    Protected Sub lnkFindBug_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkFindBug.Click
        ucFindBug.Show()

    End Sub
End Class
