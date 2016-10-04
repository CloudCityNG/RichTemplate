Imports Microsoft.VisualBasic

Public Class RichTemplateLanguagePage : Inherits Page
    Protected Overrides Sub InitializeCulture()

        '*** make sure to call base class implementation
        MyBase.InitializeCulture()

        '*** pull language preference from the users session
        Dim strLanguagePreference As String = String.Empty
        If (Not HttpContext.Current Is Nothing) AndAlso (Not HttpContext.Current.Session Is Nothing) AndAlso (Not HttpContext.Current.Session("LanguagePreference") Is Nothing) Then
            'Check if we can load the language code from session
            strLanguagePreference = HttpContext.Current.Session("LanguagePreference")
        Else
            'Get the current siteID and use the LanguageDAL to get the language
            Dim intSiteID As Integer = If(Request.Url.AbsolutePath.ToLower().StartsWith("/admin/"), SiteDAL.GetCurrentSiteID_Admin(), SiteDAL.GetCurrentSiteID_FrontEnd())
            strLanguagePreference = LanguageDAL.GetCurrentLanguageCode_BySiteID(intSiteID)
        End If


        '*** set the cultures
        Me.UICulture = strLanguagePreference
        If strLanguagePreference.Contains("-") Then
            Me.Culture = strLanguagePreference

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(strLanguagePreference)

        End If
    End Sub

End Class
