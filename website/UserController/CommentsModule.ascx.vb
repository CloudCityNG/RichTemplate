Imports System.Data

Partial Class UserController_CommentsModule
    Inherits System.Web.UI.UserControl

    Protected _MemberID As Integer = 0

    Private _ModuleTypeID As Integer = 0
    Private _SiteID As Integer = 0
    Private _RecordID As Integer = 0

    Private _InitialRating As Decimal = 0.0F

    Public Property InitialRating() As Decimal
        Get
            Return _InitialRating
        End Get
        Set(ByVal value As Decimal)
            _InitialRating = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        'First Check if the ModuleTypeID AND recordID is set, if not set this control to not visible, it will get set as VISIBLE once the usercontrol has been setup through 'SetupCommentModule()'
        Me.Visible = _ModuleTypeID > 0 Or _RecordID > 0

    End Sub

    Public Sub SetupCommentModule(ByVal SiteID_CurrentRecord As Integer, ByVal ModuleTypeID As Integer, ByVal RecordID As Integer, ByVal MemberID As Integer, ByVal AllowInsert As Boolean, ByVal EnablePublicCommentSubmission As Boolean)
        'Set our Comment Module Global Variables
        _ModuleTypeID = ModuleTypeID
        _SiteID = SiteID_CurrentRecord 'This is the SiteID for this record, NOT the current Site!
        _RecordID = RecordID
        _MemberID = MemberID

        If Not Page.IsPostBack Then
            LoadComments()
        End If

        'Finally only add comment if AllowInsert is allowed and user is logged in
        If Not AllowInsert Then
            tbl_AddComment_NotActive.Visible = True
        ElseIf MemberID > 0 Then
            tbl_AddComment.Visible = True
        ElseIf EnablePublicCommentSubmission Then
            'Then the Member is not Logged in, but if we do not require a member to be logged in, then we can show the AddComment AND the comment's captcha code
            divCaptchaSubmitComment.Visible = True
            radCaptchaSubmitComment.Visible = True
            tbl_AddComment.Visible = True
        Else
            tbl_AddComment_NotLoggedIn.Visible = True
        End If
    End Sub
    Private Sub LoadComments()

        Dim dtComments As DataTable = CommentDAL.GetCommentList_ByModuleTypeIDAndRecordIDAndSiteID_Front(_ModuleTypeID, _RecordID, _SiteID)
        lit_CommentCount.Text = dtComments.Rows.Count

        If dtComments.Rows.Count > 0 Then
            'Load comments into repeater
            rpt_Comment.DataSource = dtComments
            rpt_Comment.DataBind()

            divComments.Visible = True
            tbl_NoComments.Visible = False
        Else
            divComments.Visible = False
            tbl_NoComments.Visible = True
        End If

        'set the initial rating value
        rrComment.Value = InitialRating

    End Sub

    Protected Sub btn_AddComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AddComment.Click
        If Page.IsValid Then
            Dim strComment As String = txtComment.Text.Trim().Replace(Environment.NewLine, "<br />")
            Dim boolApproved As Boolean = _MemberID > 0 ' Comments for modules are automatically approved if the member is logged in
            Dim intCommentID As Integer = CommentDAL.InsertComment(strComment, rrComment.Value, _MemberID, boolApproved)
            Dim intCommentModuleID = CommentDAL.InsertCommentModule(intCommentID, _ModuleTypeID, _SiteID, _RecordID)
            tbl_AddComment.Visible = False
            tbl_CommentMessage.Visible = True

            'Reload Comments List
            LoadComments()

            'Raise Comment Added Event
            RaiseEvent CommentAdded(Me, New EventArgs)
        End If
    End Sub

    Private AllEvents As New System.ComponentModel.EventHandlerList
    Public Custom Event CommentAdded As EventHandler
        AddHandler(ByVal value As EventHandler)
            AllEvents.AddHandler("CommentAdded", value)
        End AddHandler

        RemoveHandler(ByVal value As EventHandler)
            AllEvents.RemoveHandler("CommentAdded", value)
        End RemoveHandler

        RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim value As EventHandler = CType(AllEvents("CommentAdded"), EventHandler)
            If Not value Is Nothing Then
                value.Invoke(sender, e)
            End If
        End RaiseEvent
    End Event
End Class
