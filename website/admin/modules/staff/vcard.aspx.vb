Imports System.Data
Imports System.IO

Partial Class admin_modules_staff_vCard
    Inherits System.Web.UI.Page

    Dim ModuleTypeID As Integer = 12 ' Module Type: Staff Member

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'This is the admin, we can also load a vCard via the Preview.aspx page using request parameter archiveID
        'Note as this is admin page, we do not need to check user access, like we would do in our front-page vcard.aspx
        'However we DO NEED TO CHECK ADMIN ACCESS
        If Not Page.IsPostBack Then

            'Check Access, Does the current AdminUser have access to this module? If not Log them out and send them to login page
            If Not AdminUserDAL.CheckAdminUserModuleAccess(ModuleTypeID, SiteDAL.GetCurrentSiteID_Admin()) Then
                Response.Redirect("/richadmin")
            End If

            If Not Request.QueryString("id") Is Nothing Then
                Dim intStaffID As Integer = Convert.ToInt32(Request.QueryString("id"))
                StaffDAL.DownloadVCard_ByStaffIDAndSiteID(intStaffID, SiteDAL.GetCurrentSiteID_Admin())

            ElseIf Not Request.QueryString("archiveID") Is Nothing Then
                'Else we use a a similar function seen in the StaffDAL.DownloadVCard_ByStaffID, but we customize it to load the preview record
                Dim intStaffArchiveID As Integer = Convert.ToInt32(Request.QueryString("archiveID"))
                DownloadVCard_ByArchiveID(intStaffArchiveID, SiteDAL.GetCurrentSiteID_Admin())
            End If
        End If

    End Sub

    Public Sub DownloadVCard_ByArchiveID(ByVal intStaffArchiveID As Integer, ByVal intSiteID As Integer)
        Dim dtStaff As DataTable = StaffDAL.GetStaffArchive_ByArchiveIDAndSiteID(intStaffArchiveID, intSiteID)
        If dtStaff.Rows.Count > 0 Then
            Dim drStaff As DataRow = dtStaff.Rows(0)

            Dim strWriter As New StringWriter()
            Dim htmlWriter As New HtmlTextWriter(strWriter)

            strWriter.WriteLine("BEGIN:VCARD")
            strWriter.WriteLine("VERSION:2.1")

            Dim strSalutation_LangaugeSpecific As String = String.Empty
            If Not drStaff("Salutation_LanguageProperty") Is DBNull.Value Then
                Dim strSalutation_LanguageProperty As String = drStaff("Salutation_LanguageProperty")
                strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
            End If

            strWriter.WriteLine("N:" & drStaff("LastName") & ";" & drStaff("FirstName") & ";;" & strSalutation_LangaugeSpecific)
            strWriter.WriteLine("FN:" & drStaff("Firstname") & " " & drStaff("LastName"))

            If Not drStaff("EmailAddress") Is DBNull.Value Then
                If Not drStaff("EmailAddress") = "" Then
                    strWriter.WriteLine("EMAIL;PREF;INTERNET:" & drStaff("EmailAddress"))
                End If
            End If

            If Not drStaff("Company") Is DBNull.Value Then
                If Not drStaff("Company") = "" Then
                    strWriter.WriteLine("ORG:" & drStaff("Company"))
                End If
            End If

            If Not drStaff("OfficePhone") Is DBNull.Value Then
                If Not drStaff("OfficePhone") = "" Then
                    strWriter.WriteLine("TEL;WORK;VOICE:" & drStaff("OfficePhone"))
                End If
            End If

            If Not drStaff("MobilePhone") Is DBNull.Value Then
                If Not drStaff("MobilePhone") = "" Then
                    strWriter.WriteLine("TEL;CELL;VOICE:" & drStaff("MobilePhone"))
                End If
            End If

            If Not drStaff("personalURL") Is DBNull.Value Then
                If Not drStaff("personalURL") = "" Then
                    strWriter.WriteLine("URL;WORK:" & drStaff("personalURL"))
                End If
            End If

            If Not drStaff("StaffPosition") Is DBNull.Value Then
                If Not drStaff("StaffPosition") = "" Then
                    strWriter.WriteLine("TITLE:" & drStaff("StaffPosition"))
                    strWriter.WriteLine("ROLE:" & drStaff("StaffPosition"))
                End If
            End If


            Dim locationOffice As String = ";"
            If Not drStaff("addressOfficeNumber") Is DBNull.Value Then
                If Not drStaff("addressOfficeNumber") = "" Then
                    locationOffice = drStaff("addressOfficeNumber") & ";"
                End If
            End If

            Dim locationStreet As String = ";"
            If Not drStaff("address") Is DBNull.Value Then
                If Not drStaff("address") = "" Then
                    locationStreet = drStaff("address") & ";"
                End If
            End If

            Dim locationCity As String = ";"
            If Not drStaff("city") Is DBNull.Value Then
                If Not drStaff("city") = "" Then
                    locationCity = drStaff("city") & ";"
                End If
            End If

            Dim locationState As String = ";"
            If Not drStaff("stateName") Is DBNull.Value Then
                If Not drStaff("stateName") = "" Then
                    locationState = drStaff("stateName") & ";"
                End If
            End If

            Dim locationZip As String = ";"
            If Not drStaff("zipCode") Is DBNull.Value Then
                If Not drStaff("zipCode") = "" Then
                    locationZip = drStaff("zipCode") & ";"
                End If
            End If


            Dim locationCountry As String = ";"
            If Not drStaff("countryName") Is DBNull.Value Then
                If Not drStaff("countryName") = "" Then
                    locationCountry = drStaff("countryName") & ";"
                End If
            End If
            strWriter.WriteLine("ADR;WORK;ENCODING=QUOTED-PRINTABLE:" & ";" & locationOffice & locationStreet & locationCity & locationState & locationZip & locationCountry)

            strWriter.WriteLine("END:VCARD")


            Dim strStaffFullName As String = StrConv(strSalutation_LangaugeSpecific, vbProperCase) & StrConv(drStaff("FirstName"), vbProperCase) & StrConv(drStaff("LastName"), vbProperCase)

            'Setup the vCard response
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strStaffFullName & ".vcf")
            HttpContext.Current.Response.ContentType = "text/x-vCard; charset=utf-8; name=" & strStaffFullName + ".vcf"
            HttpContext.Current.Response.Write(strWriter.ToString())
            HttpContext.Current.Response.End()

        End If
    End Sub
End Class
