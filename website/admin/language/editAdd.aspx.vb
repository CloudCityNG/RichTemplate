Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO

Partial Class admin_language_editAdd
    Inherits RichTemplateLanguagePage

    Dim intSiteID As Integer = Integer.MinValue

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        intSiteID = SiteDAL.GetCurrentSiteID_Admin()

        Dim intLanguageID = Integer.MinValue
        If Not Request.QueryString("ID") Is Nothing Then
            intLanguageID = Convert.ToInt32(Request.QueryString("ID"))
        End If

        'Setup Rad Controls
        CommonWeb.SetupRadEditor(Page, txt_LanguageDescription, intSiteID)

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Language_Admin.Language_AddEdit_Header

        'Check the logged in user can view this page
        Dim intAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intAdminUserAccess < 3 Then
            Response.Redirect("~/richadmin/")
        End If

        If Not Page.IsPostBack Then

            If intLanguageID > Integer.MinValue Then

                Dim dtLanguage As DataTable = LanguageDAL.GetLanguage_ByID(intLanguageID)
                If dtLanguage.Rows.Count > 0 Then
                    Dim drLanguage As DataRow = dtLanguage.Rows(0)

                    btnAddEdit.Text = Resources.Language_Admin.Language_AddEdit_ButtonUpdate

                    txt_LanguageName.Text = drLanguage("Language")

                    txt_LanguageCode.Text = drLanguage("Code")

                    If Not drLanguage("Description") Is DBNull.Value Then
                        txt_LanguageDescription.Content = drLanguage("Description").ToString()
                    End If

                    'Populate the LanguageLetters
                    Dim sbLanguageLetters As New StringBuilder()
                    Dim dtLanguageLetters As DataTable = LanguageDAL.GetLanguageLetters_ByLanguageID(intLanguageID)
                    For Each drLanguageLetter As DataRow In dtLanguageLetters.Rows
                        sbLanguageLetters.Append(drLanguageLetter("letterLowerCase") & "; ")
                    Next
                    txt_LanguageLetters.Text = sbLanguageLetters.ToString()
                Else
                    'The languageID does not exist so redirect them to the default page
                    Response.Redirect("default.aspx")
                End If

            Else
                btnAddEdit.Text = Resources.Language_Admin.Language_AddEdit_ButtonAdd
            End If

        End If

    End Sub

    Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelButton.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub btnAddEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEdit.Click

        addUpdateRecord()

        Response.Redirect("default.aspx")
    End Sub

    Protected Sub addUpdateRecord()

        If Request.QueryString("ID") Is Nothing Then

            Dim strLanguagename As String = txt_LanguageName.Text.Trim()
            Dim strLanguageCode As String = txt_LanguageCode.Text.Trim()
            Dim strLanguageDescription As String = txt_LanguageDescription.Content.Trim()

            Dim intLanguageID As Integer = LanguageDAL.InsertLanguage(strLanguagename, strLanguageCode, strLanguageDescription)

            'Now get all the language letters and insert them into our ss_LanguageLetters Table
            Dim strLanguageLetterList As String() = txt_LanguageLetters.Text.Trim().Split(";")
            For Each strLanguageLetter As String In strLanguageLetterList
                If strLanguageLetter.Length > 0 Then
                    LanguageDAL.InsertLanguageLetters(intLanguageID, strLanguageLetter.Trim().ToLower(), strLanguageLetter.Trim().ToUpper())
                End If
            Next

        Else

            Dim intLanguageID As Integer = Convert.ToInt32(Request.QueryString("ID"))

            Dim strLanguagename As String = txt_LanguageName.Text.Trim()
            Dim strLanguageCode As String = txt_LanguageCode.Text.Trim()
            Dim strLanguageDescription As String = txt_LanguageDescription.Content.Trim()

            LanguageDAL.UpdateLanguage_ByID(intLanguageID, strLanguagename, strLanguageCode, strLanguageDescription)

            'First Delete all existing language letters for this languageID
            LanguageDAL.DeleteLanguageLetters_ByLanguageID(intLanguageID)

            'Then get all the language letters and insert them into our ss_LanguageLetters Table
            Dim strLanguageLetterList As String() = txt_LanguageLetters.Text.Trim().Split(";")
            For Each strLanguageLetter As String In strLanguageLetterList
                If strLanguageLetter.Length > 0 Then
                    LanguageDAL.InsertLanguageLetters(intLanguageID, strLanguageLetter.Trim().ToLower(), strLanguageLetter.Trim().ToUpper())
                End If
            Next
        End If

    End Sub

End Class
