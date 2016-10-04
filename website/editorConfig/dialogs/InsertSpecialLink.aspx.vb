Imports System.Data
Imports System.IO

Partial Class admin_editorConfig_dialogs_InsertSpecialLink
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            BindWebInfoPages()
            BindAnchorLinks()
            BindTargetPageList()

            'Prepopulate the text
            Dim strDynamicJavaScript As String = "" & _
        "$(document).ready(function() {" & Environment.NewLine() & _
        "   var editor = $(parent.document).find('.RadEditor').get(0);" & Environment.NewLine() & _
        "   var editorContent = editor.control.get_html();" & Environment.NewLine() & _
        "   var hdnAnchorList = $('#hdnAnchorList').get(0);" & Environment.NewLine() & _
        "   hdnAnchorList.value = '';" & Environment.NewLine() & _
        "   $('a', '<div>' + editorContent + '</div>').each( function()" & Environment.NewLine() & _
        "   {" & Environment.NewLine() & _
        "       var anchorTagNameAttr = $(this).attr('name');" & Environment.NewLine() & _
        "       if( anchorTagNameAttr != null)" & Environment.NewLine() & _
        "       {" & Environment.NewLine() & _
        "           if(anchorTagNameAttr.length > 0)" & Environment.NewLine() & _
        "           {" & Environment.NewLine() & _
        "               var hdnAnchorList_current = hdnAnchorList.value" & Environment.NewLine() & _
        "               hdnAnchorList.value = hdnAnchorList_current + '|' + anchorTagNameAttr;" & Environment.NewLine() & _
        "           }" & Environment.NewLine() & _
        "       }" & Environment.NewLine() & _
        "   });" & Environment.NewLine() & _
        "   var currentRadWindow = getRadWindow();" & Environment.NewLine() & _
        "   if(currentRadWindow != null)" & Environment.NewLine() & _
        "   {" & Environment.NewLine() & _
        "      var workLink = currentRadWindow.ClientParameters;" & Environment.NewLine() & _
        "      $('#txtText').val(workLink.innerHTML);" & Environment.NewLine() & _
        "   }" & Environment.NewLine() & _
        "});" & Environment.NewLine()
            CommonWeb.JavaScript_InsertDynamicJavaScript(Me.Page, strDynamicJavaScript)
        End If
    End Sub

    Public Sub BindWebInfoPages()

            'Setup the internal webpages
            Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfoList(SiteDAL.GetCurrentSiteID_Admin())

            'Clear the items in this dropdown
            ddlExistingPage.Items.Clear()
            'Insert the initial item
            ddlExistingPage.Items.Add(New ListItem("--Choose a Page--", ""))
            'Add the pages to this drop down
            For Each drWebInfo As DataRow In dtWebInfo.Rows
                Dim strWebInfoName As String = drWebInfo("Name")

                Dim strPageUrl As String = "/" & strWebInfoName.Replace(" ", "-") & ".aspx"
                Dim strParentUrl As String = ""
                If Not drWebInfo("Parentname") Is DBNull.Value Then
                    strParentUrl = drWebInfo("ParentName").ToString().Replace(" ", "-")
                End If

                'If the parent name exists pre-pend this name to the page url
                If strParentUrl.Length > 0 Then
                    strPageUrl = "/" & strParentUrl & strPageUrl
                End If

                Dim intPageLevel As Integer = drWebInfo("PageLevel")
                While intPageLevel > 1
                    strWebInfoName = "-- " & strWebInfoName
                    intPageLevel = intPageLevel - 1
                End While
                ddlExistingPage.Items.Add(New ListItem(strWebInfoName, strPageUrl))
            Next
    End Sub

    Public Sub BindAnchorLinks()
        'Only bind the anchor links if the selected index is 0, as the user has already selected an items
        If ddlAnchorLink.SelectedIndex <= 0 Then
            ddlAnchorLink.Items.Clear()
            'Insert the initial item
            ddlAnchorLink.Items.Add(New ListItem("--Choose an Anchor--", ""))
            Dim strHdnAnchorList() As String = hdnAnchorList.Value.Trim("|").Split("|")
            For Each strHdnAnchor As String In strHdnAnchorList
                ddlAnchorLink.Items.Add(New ListItem(strHdnAnchor, "#" & strHdnAnchor))
            Next
        End If

    End Sub

    Public Sub BindTargetPageList()
        ddlTargetPage.Items.Clear()
        ddlTargetPage.Items.Add(New ListItem("--Choose Target Page--", ""))
        ddlTargetPage.Items.Add(New ListItem("Same Window", "_self"))
        ddlTargetPage.Items.Add(New ListItem("New Window", "_blank"))
    End Sub

#Region "Events"

    Protected Sub rdExistingOrAnchor_OnCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rdExistingPage.CheckedChanged, rdAnchorLink.CheckedChanged
        If rdExistingPage.Checked Then
            ddlExistingPage.Enabled = True
            ddlAnchorLink.Enabled = False
        Else
            ddlExistingPage.Enabled = False
            ddlAnchorLink.Enabled = True

            'Also bind all anchor links, as they will not yet be added
            BindAnchorLinks()
        End If

    End Sub

    Protected Sub btnSubmit_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        If IsValid() Then
            'Dynamically add javascript to the page to force a close and provide the needed arguments
            Dim strLinkText As String = txtText.Text.Trim()
            Dim strLinkTitle As String = txtTitle.Text.Trim()
            Dim strLinkUrl As String = ""
            If rdExistingPage.Checked Then
                strLinkUrl = ddlExistingPage.SelectedValue
            Else
                strLinkUrl = ddlAnchorLink.SelectedValue
            End If
            Dim strLinkTarget As String = ddlTargetPage.SelectedValue
            CreateModalSubmissionScript(strLinkText, strLinkTitle, strLinkUrl, strLinkTarget)

        End If
    End Sub

    Protected Sub btnCancel_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        'Dynamically add javascript to the page to force a close
        Dim strDynamicJavaScript_FormCancel As String = "" & _
    "var currentRadWindow = getRadWindow();" & Environment.NewLine() & _
    "if(currentRadWindow != null)" & Environment.NewLine() & _
    "{" & Environment.NewLine() & _
    "   currentRadWindow.close(null);" & Environment.NewLine() & _
    "}" & Environment.NewLine()

        CommonWeb.JavaScript_InsertDynamicJavaScript(Me.Page, strDynamicJavaScript_FormCancel)
    End Sub
#End Region

#Region "Validation"
    Protected Sub customValExistingPage_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'If the user has selected an existing page, check the user has selected an existing page from the drop down list
        If rdExistingPage.Checked Then
            If ddlExistingPage.SelectedValue = "" Then
                e.IsValid = False
            End If
        End If
    End Sub

    Protected Sub customValAnchors_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'If the user has selected an anchored link, check the user has selected an anchor link from the drop down list
        If rdAnchorLink.Checked Then
            If ddlAnchorLink.SelectedValue = "" Then
                e.IsValid = False
            End If
        End If
    End Sub
#End Region

    Protected Sub CreateModalSubmissionScript(ByVal linkText As String, ByVal linkTitle As String, ByVal linkUrl As String, ByVal linkTarget As String)
        Dim strDynamicJavaScript_FormSubmission As String = "" & _
        "var currentRadWindow = getRadWindow();" & Environment.NewLine() & _
        "if(currentRadWindow != null)" & Environment.NewLine() & _
        "{" & Environment.NewLine() & _
        "   var workLink = currentRadWindow.ClientParameters;" & Environment.NewLine() & _
        "   workLink.linkText = '" & linkText & "';" & Environment.NewLine() & _
        "   workLink.linkTitle = '" & linkTitle & "';" & Environment.NewLine() & _
        "   workLink.linkUrl = '" & linkUrl & "';" & Environment.NewLine() & _
        "   workLink.linkTarget = '" & linkTarget & "';" & Environment.NewLine() & _
        "   currentRadWindow.close(workLink);" & Environment.NewLine() & _
        "}" & Environment.NewLine()

        CommonWeb.JavaScript_InsertDynamicJavaScript(Me.Page, strDynamicJavaScript_FormSubmission)

    End Sub
End Class
