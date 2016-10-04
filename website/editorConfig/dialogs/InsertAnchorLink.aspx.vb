Imports System.Data
Imports System.IO

Partial Class admin_editorConfig_dialogs_InsertAnchorLink
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then

            'if not postback we load our list of bookmarks
            Dim strDynamicJavaScript_FormSubmission As String = "" & _
            "$(document).ready(function() {" & Environment.NewLine() & _
            "   var editor = $(parent.document).find('.RadEditor').get(0);" & Environment.NewLine() & _
            "   var editorContent = editor.control.get_html();" & Environment.NewLine() & _
            "   var lstAnchorList = $('#lstAnchorList').get(0);" & Environment.NewLine() & _
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
            "               var liAnchor = document.createElement('option');" & Environment.NewLine() & _
            "               liAnchor.text = anchorTagNameAttr;" & Environment.NewLine() & _
            "               liAnchor.value = anchorTagNameAttr;" & Environment.NewLine() & _
            "               if($.browser.msie)" & Environment.NewLine() & _
            "               {" & Environment.NewLine() & _
            "                   lstAnchorList.add(liAnchor);" & Environment.NewLine() & _
            "               }" & Environment.NewLine() & _
            "               else" & Environment.NewLine() & _
            "               {" & Environment.NewLine() & _
            "                   lstAnchorList.add(liAnchor, null);" & Environment.NewLine() & _
            "               }" & Environment.NewLine() & _
            "           }" & Environment.NewLine() & _
            "       }" & Environment.NewLine() & _
            "   });" & Environment.NewLine() & _
            "});" & Environment.NewLine()
            CommonWeb.JavaScript_InsertDynamicJavaScript(Me.Page, strDynamicJavaScript_FormSubmission)
        End If
    End Sub

    Protected Sub btnInsert_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnInsert.Click
        If IsValid() Then
            'Dynamically add javascript to the page to force a close and provide the needed arguments
            Dim strLinkName As String = txtAnchorName.Text.Trim()
            CreateModalSubmissionScript(strLinkName)

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

    Protected Sub CreateModalSubmissionScript(ByVal linkName As String)
        Dim strDynamicJavaScript_FormSubmission As String = "" & _
        "var currentRadWindow = getRadWindow();" & Environment.NewLine() & _
        "if(currentRadWindow != null)" & Environment.NewLine() & _
        "{" & Environment.NewLine() & _
        "   var workLink = currentRadWindow.ClientParameters;" & Environment.NewLine() & _
        "   workLink.linkName = '" & linkName & "';" & Environment.NewLine() & _
        "   currentRadWindow.close(workLink);" & Environment.NewLine() & _
        "}" & Environment.NewLine()

        CommonWeb.JavaScript_InsertDynamicJavaScript(Me.Page, strDynamicJavaScript_FormSubmission)

    End Sub

#Region "Validation"
    Protected Sub customValAnchorExists_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Check this anchor does not already exist in this page
        Dim strAnchor As String = txtAnchorName.Text.Trim().ToLower()
        Dim strHdnAnchorList() As String = hdnAnchorList.Value.Trim("|").Split("|")

        For Each strHdnAnchor As String In strHdnAnchorList
            If strAnchor = strHdnAnchor Then
                e.IsValid = False
                Exit For
            End If
        Next
    End Sub

#End Region
End Class
