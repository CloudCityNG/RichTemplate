Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_poll_editAddAnswer
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Dim ModuleTypeID As Integer = 9 ' Module Type: Poll

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        'Attach a delete confirmation to our delete button
        CommonWeb.SetupDeleteButton(btnDeleteHistory, Resources.Poll_Admin.Poll_AddEditAnswer_Tab_History_GridDeleteButton_ConfirmationMessage)

        'Setup Rad Controls
        CommonWeb.SetupRadDatePicker(PublicationDate, intSiteID)
        CommonWeb.SetupRadDatePicker(ExpirationDate, intSiteID)
        CommonWeb.SetupRadEditor(Page, txtAnswer, SiteDAL.GetCurrentSiteID_Admin)
        CommonWeb.SetupRadGrid(rgHistory, "{4} {5} " & Resources.Poll_Admin.Poll_AddEditAnswer_Tab_History_Grid_Pager_PagerTextFormat_ItemsIn & " {1} " & Resources.Poll_Admin.Poll_AddEditAnswer_Tab_History_Grid_Pager_PagerTextFormat_Page)
        CommonWeb.SetupRadProgressArea(rpAreaPollAnswer, intSiteID)
        CommonWeb.SetupRadUpload(RadUploadPollAnswerImage, intSiteID)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Poll_Admin.Poll_AddEditAnswer_Header
        ucHeader.PageHelpID = 7 'Help Item for Polls

        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, intSiteID) Then
                Response.Redirect("/richadmin")
            End If

            If Not Request.QueryString("ID") Is Nothing Then
                'We are updating a poll answer that corresponds to this ID
                Dim intPollAnswerID As Integer = Convert.ToInt32(Request.QueryString("ID"))
                Dim dtPollAnswer As DataTable = PollDAL.GetPollAnswer_ByIDAndSiteID(intPollAnswerID, intSiteID)
                If dtPollAnswer.Rows.Count > 0 Then

                    Dim drPollAnswer As DataRow = dtPollAnswer.Rows(0)
                    Dim intPollID As Integer = Convert.ToInt32(drPollAnswer("PollID"))
                    aBackToPollAnswers.HRef = "answers.aspx?pollID=" & intPollID
                    btnCancel.CommandArgument = intPollID

                    btnAddEdit.CommandArgument = intPollID
                    btnAddEdit.Text = Resources.Poll_Admin.Poll_AddEditAnswer_ButtonUpdate

                    Me.txtAnswer.Content = drPollAnswer("Answer").ToString()
                    Me.txtDescription.Text = drPollAnswer("Description").ToString()
                    Me.Status.SelectedValue = drPollAnswer("Status").ToString()


                    Dim boolAnswerIsCorrect As Boolean = Convert.ToBoolean(drPollAnswer("IsCorrect"))
                    If boolAnswerIsCorrect Then
                        chkAnswerIsCorrect.Checked = True
                    End If

                    If Not drPollAnswer("publicationDate").ToString() = "" Then
                        Me.PublicationDate.SelectedDate = drPollAnswer("publicationDate").ToString()
                    End If

                    If Not drPollAnswer("expirationDate").ToString() = "" Then
                        Dim dtExpirationDate As DateTime = Convert.ToDateTime(drPollAnswer("expirationDate"))
                        Me.ExpirationDate.SelectedDate = dtExpirationDate.ToString()
                        If dtExpirationDate < DateTime.Now Then
                            spanExpired.Visible = True
                        End If
                    End If

                    txtVersion.Text = drPollAnswer("Version").ToString

                    Me.pollAnswerImage.Visible = False
                    Me.lnkDeleteImage.Visible = False
                    If Not drPollAnswer("thumbnail") Is DBNull.Value Then
                        If Not drPollAnswer("thumbnail").ToString() = "" Then

                            Me.pollAnswerImage.DataValue = drPollAnswer("thumbnail")
                            Me.pollAnswerImage.Visible = True
                            Me.lnkDeleteImage.Visible = True
                        End If
                    End If


                    '*** set inital sort for the History Grid to be version descending
                    Dim gSortExpression As New GridSortExpression()
                    gSortExpression.SortOrder = GridSortOrder.Descending
                    gSortExpression.FieldName = "version"

                    rgHistory.MasterTableView.SortExpressions.AddSortExpression(gSortExpression)

                    'Finally Check if we should make this Poll/Poll Answer is READ-ONLY
                    Dim intSiteID_PollAnswer As Integer = Convert.ToInt32(drPollAnswer("SiteID"))
                    If Not intSiteID = intSiteID_PollAnswer Then
                        MakePollAnswerReadOnly(intSiteID_PollAnswer)
                    End If

                Else
                    'We have a pollAnswer, but it does not exist for this site, so send them back to the default page
                    Response.Redirect("default.aspx")

                End If

            ElseIf Not Request.QueryString("PollID") Is Nothing Then
                'we do not have a poll answer id (ID) so we must be adding a poll answer, in which case we need the pollID
                Dim intPollID As Integer = Convert.ToInt32(Request.QueryString("PollID"))
                Dim dtPoll As DataTable = PollDAL.GetPoll_ByIDAndSiteID(intPollID, intSiteID)
                If dtPoll.Rows.Count > 0 Then
                    aBackToPollAnswers.HRef = "answers.aspx?pollID=" & intPollID
                    btnAddEdit.Text = Resources.Poll_Admin.Poll_AddEditAnswer_ButtonAdd
                    btnAddEdit.CommandArgument = intPollID

                    btnCancel.CommandArgument = intPollID

                    Status.SelectedValue = True
                Else
                    'PollID has been supplied, but does not exist for this site
                    Response.Redirect("default.aspx")
                End If

            Else
                'redirect to the poll default page
                Response.Redirect("default.aspx")
            End If
        End If

    End Sub

    Private Sub MakePollAnswerReadOnly(ByVal SiteID As Integer)

        'Prevent AdminUser from updating this record
        lnkDeleteImage.Visible = False
        btnAddEdit.Visible = False

    End Sub

    Protected Sub addUpdatePollAnswer()

        If Request.QueryString("ID") Is Nothing Then

            Dim intPollID As Integer = Convert.ToInt32(btnAddEdit.CommandArgument)
            Dim strAnswer As String = Me.txtAnswer.Content.Trim().ToString()
            Dim strDescription As String = txtDescription.Text.Trim().ToString()

            Dim boolAnswerIsCorrect As Boolean = chkAnswerIsCorrect.Checked

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)

            Dim dtPublication As DateTime = DateTime.MinValue
            If Not PublicationDate.SelectedDate.ToString = "" Then
                dtPublication = PublicationDate.SelectedDate
            End If

            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not ExpirationDate.SelectedDate.ToString = "" Then
                dtExpiration = ExpirationDate.SelectedDate
            End If

            Dim intVersion As Integer = 1
            Dim dtDateTimeStamp As DateTime = DateTime.Now

            Dim intAuthorID_member As Integer = Integer.MinValue
            Dim intAuthorID_admin As Integer = Convert.ToInt32(Session("adminID"))

            Dim intModifiedID_member As Integer = Integer.MinValue
            Dim intModifiedID_admin As Integer = Integer.MinValue

            Dim intPollAnswerID As Integer = PollDAL.InsertPollAnswer(intPollID, strAnswer, strDescription, boolAnswerIsCorrect, dtPublication, dtExpiration, boolStatus, intVersion, dtDateTimeStamp, intAuthorID_member, intAuthorID_admin, intModifiedID_member, intModifiedID_admin)

            'Add poll answer image if it exists
            If RadUploadPollAnswerImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadPollAnswerImage.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesPollAnswerImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesPollAnswerImage, 0, file.InputStream.Length)
                PollDAL.UpdatePollAnswer_PollAnswerImage_ByID(intPollAnswerID, strThumbnailName, bytesPollAnswerImage)

            End If

        Else

            Dim intPollAnswerID As Integer = Request.QueryString("ID")

            Dim intPollID As Integer = Convert.ToInt32(btnAddEdit.CommandArgument)

            Dim strAnswer As String = Me.txtAnswer.Content.Trim().ToString()
            Dim strDescription As String = txtDescription.Text.Trim().ToString()

            Dim boolAnswerIsCorrect As Boolean = chkAnswerIsCorrect.Checked

            Dim boolStatus As Boolean = Convert.ToBoolean(Status.SelectedValue)

            Dim dtPublication As DateTime = DateTime.MinValue
            If Not PublicationDate.SelectedDate.ToString = "" Then
                dtPublication = PublicationDate.SelectedDate
            End If

            Dim dtExpiration As DateTime = DateTime.MinValue
            If Not ExpirationDate.SelectedDate.ToString = "" Then
                dtExpiration = ExpirationDate.SelectedDate
            End If

            Dim intVersion As Integer = 1
            If Not IsDBNull(txtVersion.Text.Trim()) And txtVersion.Text.Trim() <> "" Then
                intVersion = Convert.ToInt32(txtVersion.Text.Trim())
                intVersion = intVersion + 1
            End If

            Dim intModifiedID_member As Integer = Integer.MinValue
            Dim intModifiedID_admin As Integer = Convert.ToInt32(Session("adminID"))

            PollDAL.UpdatePollAnswer_ByID(intPollAnswerID, intPollID, strAnswer, strDescription, boolAnswerIsCorrect, dtPublication, dtExpiration, boolStatus, intVersion, intModifiedID_member, intModifiedID_admin)

            'Add poll answer image if it exists
            If RadUploadPollAnswerImage.UploadedFiles.Count > 0 Then
                Dim file As UploadedFile = RadUploadPollAnswerImage.UploadedFiles(0)
                Dim strThumbnailName As String = file.GetName
                Dim bytesPollAnswerImage(file.InputStream.Length - 1) As Byte
                file.InputStream.Read(bytesPollAnswerImage, 0, file.InputStream.Length)
                PollDAL.UpdatePollAnswer_PollAnswerImage_ByID(intPollAnswerID, strThumbnailName, bytesPollAnswerImage)

            End If

        End If
    End Sub

    Protected Sub btnAddEditPollAnswer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click
        If IsValid Then
            addUpdatePollAnswer()
            Dim intPollID As Integer = Convert.ToInt32(btnCancel.CommandArgument.ToString())
            Response.Redirect("Answers.aspx?pollID=" & intPollID)
        End If
    End Sub

    Protected Sub btnDeleteHistory_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each grdItem As GridDataItem In rgHistory.Items
            Dim chkDelete As CheckBox = DirectCast(grdItem.FindControl("chkSelect"), CheckBox)

            If chkDelete.Checked Then
                Dim intRecordId As Integer = grdItem.GetDataKeyValue("archiveID")
                PollDAL.DeletePollAnswerArchive_ByArchiveID(intRecordId)
            End If
        Next
        rgHistory.Rebind()

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim intPollID As Integer = Convert.ToInt32(btnCancel.CommandArgument.ToString())
        Response.Redirect("Answers.aspx?pollID=" & intPollID)
    End Sub

    Public Sub rgHistory_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgHistory.NeedDataSource

        If Not Request.QueryString("ID") Is Nothing Then

            Dim intPollAnswerID As Integer = Convert.ToInt32(Request.QueryString("ID"))
            Dim dtHistory As DataTable = PollDAL.GetPollAnswerArchiveList_ByPollAnswerID(intPollAnswerID)
            rgHistory.DataSource = dtHistory

        End If

    End Sub

    Public Sub rgHistory_ItemDataBound(ByVal source As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgHistory.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

            Dim drvItem As DataRowView = item.DataItem
            Dim drItem As DataRow = drvItem.Row

            Dim intArchiveID As Integer = Convert.ToInt32(drItem("archiveID"))
            Dim intVersion As Integer = Convert.ToInt32(drItem("version"))
            Dim intVersion_Current As Integer = Convert.ToInt32(txtVersion.Text)
            If intVersion = intVersion_Current Then
                e.Item.Cells(4).Text = "<b>" & Resources.Poll_Admin.Poll_AddEditAnswer_Tab_History_GridVersion_Current & " (" & intVersion & ")</b>"
            End If

            Dim aPreview As HtmlAnchor = DirectCast(e.Item.FindControl("aPreview"), HtmlAnchor)
            aPreview.HRef = String.Format("previewAnswer.aspx?archiveID={0}", intArchiveID)

        End If
    End Sub

#Region "Poll Answer Image"
    Protected Sub lnkDeleteImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteImage.Click
        Dim intPollAnswerID As Integer = Convert.ToInt32(Request.QueryString("ID"))
        Dim bytesPollAnswerImage() As Byte
        PollDAL.UpdatePollAnswer_PollAnswerImage_ByID(intPollAnswerID, String.Empty, bytesPollAnswerImage)

        'Hide the poll answer image and the delete link
        pollAnswerImage.Visible = False
        lnkDeleteImage.Visible = False
    End Sub

    Protected Sub customValPollAnswerImageSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add poll answer image if it exists, must have a file size less than 100kb, but we give them a 5kb buffer
        If RadUploadPollAnswerImage.UploadedFiles.Count > 0 Then
            Dim file As UploadedFile = RadUploadPollAnswerImage.UploadedFiles(0)
            If file.InputStream.Length > 112400 Then
                'Invalid Submission - so set the IsValid flag to false and show the tab that caused the validation error
                e.IsValid = False

                rtsPollAnswer.SelectedIndex = 0
                rpvPollAnswer.Selected = True
            End If
        End If
    End Sub

#End Region

End Class
