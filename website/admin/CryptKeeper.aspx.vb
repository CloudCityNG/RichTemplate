
Partial Class admin_CyptKeeper
    Inherits System.Web.UI.Page

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        If txtPlainText.Text.Trim().Length > 0 Then
            Dim strHashedText As String = CommonWeb.ComputeHash(txtPlainText.Text.Trim())
            txtPlainText.Text = strHashedText

        End If
    End Sub
End Class
