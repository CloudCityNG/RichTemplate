Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Resources
Imports System.Reflection
Imports System.Data
Imports System.Security.Cryptography
Imports Telerik.Web.UI

Public Class CommonWeb

#Region "JavaScript Functions"
    Public Shared Sub JavaScript_InsertDynamicJavaScript(ByVal currentPage As Page, ByVal strJavaScript As String)
        'If we have a valid non-empty string the create the script in the provided page
        If strJavaScript.Length > 0 Then
            ScriptManager.RegisterClientScriptBlock(currentPage, currentPage.GetType(), "dynamic_java_script", strJavaScript, True)
        End If
    End Sub
    Public Shared Sub JavaScriptRedirect_UpdateFrames(ByVal currentPage As Page, ByVal strBaseFrame As String, ByVal strTreeFrame As String, ByVal strContents As String, ByVal strBanner As String)
        'Use javascript to redirect to the main page
        Dim strJavascriptRedirect As String = ""
        strJavascriptRedirect = ""

        'include a link to the base frame if supplied
        If strBaseFrame.Length > 0 Then
            strJavascriptRedirect = strJavascriptRedirect & "top.basefrm.location = '" & strBaseFrame & "';"
        End If

        'provide a link to the strTreeFrame if supplied
        If strTreeFrame.Length > 0 Then
            strJavascriptRedirect = strJavascriptRedirect & "top.treeframe.location = '" & strTreeFrame & "';"
        End If

        'provide a link to the strContent frame if supplied
        If strContents.Length > 0 Then
            strJavascriptRedirect = strJavascriptRedirect & "top.contents.location = '" & strContents & "';"
        End If

        'provide a link to the strBanner frame if supplied
        If strBanner.Length > 0 Then
            strJavascriptRedirect = strJavascriptRedirect & "top.banner.location = '" & strBanner & "';"
        End If

        'If we have a valid non-empty string the create the script in the provided page
        If strJavascriptRedirect.Length > 0 Then
            ScriptManager.RegisterClientScriptBlock(currentPage, currentPage.GetType(), "redirect_mainpage", strJavascriptRedirect, True)
        End If

    End Sub

    Public Shared Sub GeneratePopupResizeJsScript(ByVal control As Control, ByVal listScrollerDivID As String(), ByVal listMaxHeight As Integer(), ByVal listPadding As Integer(), ByVal forceMaxHeight As Boolean)
        Dim script As String = ""
        Dim scriptPopupOnShowMethodCalls As String = ""

        'Create PopupOnShow Function Calls for each div in listScrollerDivID
        For i As Integer = 0 To listScrollerDivID.Length - 1
            Dim scrollerDivID As String = listScrollerDivID(i)
            scriptPopupOnShowMethodCalls = scriptPopupOnShowMethodCalls & String.Format( _
            "PopupOnShow_{0}('{0}');" & Environment.NewLine & "", scrollerDivID)

        Next

        'Create a function to call all the PopupOnShow function calls, as well as a window resize event that also calls these PopupOnShow function calls
        script = script & Environment.NewLine & _
"function PageLoadForModalPopups() " & Environment.NewLine & _
"{ " & Environment.NewLine & _
 scriptPopupOnShowMethodCalls & Environment.NewLine & _
"}" & Environment.NewLine & _
"function PageResizeForModalPopups()" & Environment.NewLine & _
"{ " & Environment.NewLine & _
 scriptPopupOnShowMethodCalls & Environment.NewLine & _
"};" & Environment.NewLine

        'Gets the div that requires scrolling and adjusts the height of this div so it always fits in the screen
        'Padding value of 110 includes 10px for top and bottom window padding and the reset get divided by 2 so we have 50px top and bottom for the popup area
        For i As Integer = 0 To listScrollerDivID.Length - 1
            Dim scrollerDivID As String = listScrollerDivID(i)
            Dim maxHeight As Integer = listMaxHeight(i)
            Dim padding = listPadding(i)
            script = script & _
"function PopupOnShow_" & scrollerDivID & "(senderTable)" & Environment.NewLine & _
"{" & Environment.NewLine & _
"var ctrlDivScroller = document.getElementById(senderTable);" & Environment.NewLine & _
"if (ctrlDivScroller != null)" & Environment.NewLine & _
"{" & Environment.NewLine & _
    "var currentWindowHeight = GetClientWindowHeight() -" & padding.ToString() & ";" & Environment.NewLine & _
    "if (currentWindowHeight < " & maxHeight.ToString() & ")" & Environment.NewLine & _
    "{" & Environment.NewLine & _
        "var newHeight = Math.round(currentWindowHeight); " & Environment.NewLine & _
        "if (newHeight < 0) " & Environment.NewLine & _
        "{" & Environment.NewLine & _
            "ctrlDivScroller.style." & If(forceMaxHeight, "height", "maxHeight") & " = '0px';" & Environment.NewLine & _
        "}" & Environment.NewLine & _
        "else" & Environment.NewLine & _
        "{" & Environment.NewLine & _
            "ctrlDivScroller.style." & If(forceMaxHeight, "height", "maxHeight") & " = (newHeight) + 'px';" & Environment.NewLine & _
        "}" & Environment.NewLine & _
    "}" & Environment.NewLine & _
    "else" & Environment.NewLine & _
    "{" & Environment.NewLine & _
        "var newHeight = Math.round(" & maxHeight.ToString() & ");" & Environment.NewLine & _
        "ctrlDivScroller.style." + If(forceMaxHeight, "height", "maxHeight") & " = (newHeight) + 'px';" & Environment.NewLine & _
    "}" & Environment.NewLine & _
"}" & Environment.NewLine & _
"}" & Environment.NewLine & ""
        Next


        script = script & Environment.NewLine & _
"function GetClientWindowHeight()" & Environment.NewLine & _
"{" & Environment.NewLine & _
    "var y = 0;" & Environment.NewLine & _
    "if (self.innerHeight)" & Environment.NewLine & _
    "{" & Environment.NewLine & _
        "y = self.innerHeight;" & Environment.NewLine & _
    "}" & Environment.NewLine & _
    "else if (top.document.documentElement && top.document.documentElement.clientHeight)" & Environment.NewLine & _
    "{" & Environment.NewLine & _
        "y = top.document.documentElement.clientHeight;" & Environment.NewLine & _
    "}" & Environment.NewLine & _
    "else if (top.document.body)" & Environment.NewLine & _
    "{" & Environment.NewLine & _
        "y = top.document.body.clientHeight;" & Environment.NewLine & _
    "}" & Environment.NewLine & _
    "return y;" & Environment.NewLine & _
"}"
        script = "<script type='text/javascript'> " & script & Environment.NewLine & " addToPageLoadCallStack(PageLoadForModalPopups); addToPageResizeCallStack(PageResizeForModalPopups);" & Environment.NewLine & "</script>"

        'Writes the script onto the page
        ScriptManager.RegisterStartupScript(control, control.GetType(), "Popup_LoadAndResize_Script", script, False)
    End Sub

#End Region

    Public Shared Function GetRandomCode(ByVal codeLength As Integer) As String
        Dim strCodeSet As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim sbCodeValue As New StringBuilder()
        Dim rand As New Random()
        For i As Integer = 0 To codeLength
            Dim intRandomIndex As Integer = rand.Next(strCodeSet.Length - 1)
            sbCodeValue.Append(strCodeSet.Substring(intRandomIndex, 1))
        Next
        Return sbCodeValue.ToString()
    End Function

    Public Shared Function GetRandomCode(ByVal codeLengthMin As Integer, ByVal codeLengthMax As Integer) As String

        Dim rand As New Random()
        Dim intCodeLength As Integer = rand.Next(codeLengthMin, codeLengthMax)
        Dim strCodeSet As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim sbCodeValue As New StringBuilder()

        For i As Integer = 0 To intCodeLength
            Dim intRandomIndex As Integer = rand.Next(strCodeSet.Length - 1)
            sbCodeValue.Append(strCodeSet.Substring(intRandomIndex, 1))
        Next
        Return sbCodeValue.ToString()
    End Function

    Public Shared Function encodeHyperlink(ByVal strTitle As Object) As String
        If IsDBNull(strTitle) Then
            Return String.Empty
        ElseIf String.IsNullOrEmpty(strTitle) Then
            Return String.Empty
        Else
            strTitle = Replace(strTitle, "-", "002D")
            strTitle = Replace(strTitle, "%", "101DG")
            strTitle = Replace(strTitle, " ", "-")
            strTitle = Replace(strTitle, "#", "%23")
            strTitle = Replace(strTitle, "(", "%28")
            strTitle = Replace(strTitle, ")", "%29")
            strTitle = Replace(strTitle, "&", "0024")
            strTitle = Replace(strTitle, "?", "003F")
            strTitle = Replace(strTitle, "+", "002B")
            strTitle = Replace(strTitle, "\", "005C")
            strTitle = Replace(strTitle, "/", "002F")
            'strTitle = Replace(strTitle, "*", "000")
            strTitle = Replace(strTitle, ":", "003A")
            strTitle = Replace(strTitle, ",", "%2C")
            strTitle = Replace(strTitle, ".", "002E")
            strTitle = Replace(strTitle, "'", "%27")


            Return strTitle

        End If
    End Function

    Public Shared Function decodeHyperlink(ByVal strTitle As Object) As String
        If IsDBNull(strTitle) Then
            Return String.Empty
        ElseIf String.IsNullOrEmpty(strTitle) Then
            Return String.Empty
        Else
            strTitle = Replace(strTitle, "-", " ")
            strTitle = Replace(strTitle, "101DG", "%")
            strTitle = Replace(strTitle, "002D", "-")
            strTitle = Replace(strTitle, "%23", "#")
            strTitle = Replace(strTitle, "%28", "(")
            strTitle = Replace(strTitle, "%29", ")")
            strTitle = Replace(strTitle, "0024", "&")
            strTitle = Replace(strTitle, "003F", "?")
            strTitle = Replace(strTitle, "002B", "+")
            strTitle = Replace(strTitle, "005C", "\")
            strTitle = Replace(strTitle, "002F", "/")
            'strTitle = Replace(strTitle, "000", "*")
            strTitle = Replace(strTitle, "003A", ":")
            strTitle = Replace(strTitle, "%2C", ",")
            strTitle = Replace(strTitle, "002E", ".")
            strTitle = Replace(strTitle, "%27", "'")

            Return strTitle

        End If
    End Function

    Public Shared Function FormatURL(ByVal strUrl As String) As String
        strUrl = strUrl.ToLower().Trim()
        Dim strUrlFormatted As String = strUrl
        If strUrl.Length > 0 Then
            'First check if its a local link, e.g. starts with '/'
            If Not strUrl.StartsWith("/") Then
                If Not strUrl.StartsWith("/") And Not strUrl.StartsWith("http://") And Not strUrl.StartsWith("https://") Then
                    strUrlFormatted = "http://" + strUrl
                End If
            End If
        End If

        Return strUrlFormatted
    End Function

    Public Shared Function Truncate_WordCount(ByVal stringToTruncate As String, ByVal maxWords As Integer) As String

        Dim wordCount As Integer = 0

        Dim strTruncated As String = ""
        Dim truncateArray As String() = Split(stringToTruncate, " ")

        If truncateArray.Length >= maxWords Then ' This is the number of words to display

            For x As Integer = 0 To maxWords - 1
                strTruncated = strTruncated & (truncateArray(x) & " ")
            Next

            strTruncated = strTruncated & " ..."
        Else
            strTruncated = stringToTruncate
        End If
        Return strTruncated
    End Function

    Public Shared Function stripHTML(ByVal strStrip As String) As String
        Dim cleanString As String = strStrip.ToString().Replace("<br />", System.Environment.NewLine()).Replace("<br/>", System.Environment.NewLine())
        cleanString = Regex.Replace(strStrip, "<(.|\n)*?>", String.Empty).ToString()
        Return cleanString

    End Function

    Public Shared Function stripHTMLandLimitWordCount(ByVal strStripAndLimit As String, ByVal intLimitWordCount As Integer) As String

        Dim cleanString As String = stripHTML(strStripAndLimit)
        Dim arrStripAndLimit As Array = Split(cleanString, " ")

        Dim splitStripAndLimit As String = ""
        If arrStripAndLimit.Length > intLimitWordCount Then
            For intWordIndex = 0 To intLimitWordCount - 1
                splitStripAndLimit = splitStripAndLimit & arrStripAndLimit(intWordIndex) & " "
            Next
            splitStripAndLimit = splitStripAndLimit & "..."
        Else
            splitStripAndLimit = cleanString
        End If

        Return splitStripAndLimit

    End Function

    Public Shared Function stripHTMLandLimitCharacterCount(ByVal strStripAndLimit As String, ByVal intLimitCharacterCount As Integer) As String
        Dim cleanString As String = stripHTML(strStripAndLimit)

        Dim maxLetters = 100
        'If the text is greater than the max letters allowed
        If cleanString.Length > maxLetters Then
            'First trim text to the max letters allowed
            cleanString = cleanString.Substring(0, maxLetters)
            'Get the last space character and replace it with '...'
            Dim lastSpaceIndex = cleanString.LastIndexOf(" ")
            If lastSpaceIndex > 0 Then
                cleanString = cleanString.Substring(0, lastSpaceIndex) + " ..."
            End If
        End If
        Return "<span>" & cleanString & "</span>"

    End Function

    Public Shared Function GetServerPath() As String
        'Get server path and remove last slash
        Dim strServerPath As String = HttpContext.Current.Server.MapPath("~")
        strServerPath = strServerPath.Substring(0, strServerPath.LastIndexOf("\"))
        strServerPath = Directory.GetParent(strServerPath).ToString()

        Return strServerPath

    End Function

    Public Shared Function GetFileSize(ByVal bytesDocument() As Byte) As String
        Dim sFileSize As String = "0KB"

        'File Size and Zip Link
        Dim iFileSize As Integer = bytesDocument.Length / 1024
        If iFileSize < 1024 Then
            sFileSize = FormatNumber(iFileSize, 0) & " KB"
        Else
            iFileSize = iFileSize / 1024
            sFileSize = FormatNumber(iFileSize, 2) & " MB"
        End If

        Return sFileSize
    End Function

    Public Shared Function GetFileSize_ByFilepath(ByVal strFilePath As String) As String

        'File Size
        Dim sFileSize As String = "0KB"

        Dim fiDocument As New FileInfo(strFilePath)

        Dim dFileSize As Decimal = fiDocument.Length / 1024
        If dFileSize < 1024 Then
            sFileSize = FormatNumber(dFileSize, 0) & " KB"
        Else
            dFileSize = dFileSize / 1024
            sFileSize = FormatNumber(dFileSize, 2) & " MB"
        End If

        Return sFileSize
    End Function

    Public Shared Function GetFileTypeImage_ByFilePath(ByVal strFilePath As String) As String
        Dim strFileExtension = strFilePath.Substring(strFilePath.LastIndexOf(".")).ToLower()
        Dim strFileTypeImage As String = String.Empty

        Select Case strFileExtension
            Case Is = ".csv"
                strFileTypeImage = "/images/icons/csv.gif"
            Case Is = ".doc"
                strFileTypeImage = "/images/icons/doc.gif"
            Case Is = ".docx"
                strFileTypeImage = "/images/icons/docx.gif"
            Case Is = ".gif"
                strFileTypeImage = "/images/icons/gif.gif"
            Case Is = ".jpg"
                strFileTypeImage = "/images/icons/jpg.gif"
            Case Is = ".mht"
                strFileTypeImage = "/images/icons/mht.gif"
            Case Is = ".odt"
                strFileTypeImage = "/images/icons/odt.gif"
            Case Is = ".ods"
                strFileTypeImage = "/images/icons/ods.gif"
            Case Is = ".pdf"
                strFileTypeImage = "/images/icons/pdf.gif"
            Case Is = ".png"
                strFileTypeImage = "/images/icons/png.gif"
            Case Is = ".ppt"
                strFileTypeImage = "/images/icons/ppt.gif"
            Case Is = ".pptx"
                strFileTypeImage = "/images/icons/pptx.gif"
            Case Is = ".rtf"
                strFileTypeImage = "/images/icons/rtf.gif"
            Case Is = ".txt"
                strFileTypeImage = "/images/icons/txt.gif"
            Case Is = ".xlsx"
                strFileTypeImage = "/images/icons/xlsx.gif"
            Case Is = ".xls"
                strFileTypeImage = "/images/icons/xls.gif"
            Case Is = ".vcf"
                strFileTypeImage = "/images/icons/vcf.gif"
            Case Is = ".wpd"
                strFileTypeImage = "/images/icons/wpd.gif"
            Case Is = ".wps"
                strFileTypeImage = "/images/icons/wps.gif"
            Case Is = ".zip"
                strFileTypeImage = "/images/icons/zip.gif"
            Case Else
                strFileTypeImage = "/images/icons/doc.gif"
        End Select

        Return strFileTypeImage
    End Function

    Public Shared Function GetFileContentType_ByFilePath(ByVal strFilePath As String) As String
        Dim strFileExtension = strFilePath.Substring(strFilePath.LastIndexOf(".")).ToLower()
        Dim strFileContentType As String = String.Empty

        Select Case strFileExtension
            Case Is = ".csv"
                strFileContentType = "text/csv"
            Case Is = ".doc"
                strFileContentType = "application/msword"
            Case Is = ".docx"
                strFileContentType = "/images/icons/docx.gif"
			Case Is = ".dotx"
                strFileContentType = "application/msword"
            Case Is = ".gif"
                strFileContentType = "image/gif"
            Case Is = ".jpg"
                strFileContentType = "image/jpeg"
            Case Is = ".mht"
                strFileContentType = "application/octet-stream"
            Case Is = ".odt"
                strFileContentType = "application/vnd.oasis.opendocument.text"
            Case Is = ".ods"
                strFileContentType = "application/vnd.oasis.opendocument.spreadsheet"
            Case Is = ".pdf"
                strFileContentType = "application/pdf"
            Case Is = ".png"
                strFileContentType = "image/png"
            Case Is = ".ppt"
                strFileContentType = "application/mspowerpoint"
            Case Is = ".pptx"
                strFileContentType = "application/mspowerpoint"
			Case Is = ".potx"
                strFileContentType = "application/mspowerpoint"
            Case Is = ".rtf"
                strFileContentType = "application/rtf"
            Case Is = ".txt"
                strFileContentType = "text/plain"
            Case Is = ".xls"
                strFileContentType = "application/msexcel"
            Case Is = ".xlsx"
                strFileContentType = "application/msexcel"
            Case Is = ".vcf"
                strFileContentType = "text/x-vcard"
            Case Is = ".wpd"
                strFileContentType = "application/wpd"
            Case Is = ".wps"
                strFileContentType = "application/wps"
            Case Is = ".zip"
                strFileContentType = "application/zip"
            Case Else
                strFileContentType = ""
        End Select

        Return strFileContentType
    End Function

    Public Shared Sub DownloadDocument_ByBytes(ByVal strFileName As String, ByVal bytesDocument() As Byte)
        Dim strFileContentType As String = GetFileContentType_ByFilePath(strFileName)

        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName.Replace(" ", "_"))
        HttpContext.Current.Response.ContentType = strFileContentType + "; charset=utf-8; name=" + strFileName.Replace(" ", "_")

        HttpContext.Current.Response.BinaryWrite(bytesDocument)
        HttpContext.Current.Response.End()

    End Sub

    Public Shared Sub DownloadDocument_ByFilePath(ByVal PhysicalFilePathAndFileName As String, ByVal DisplayFileName As String)

        DisplayFileName = If(DisplayFileName.LastIndexOf(".") > 0, DisplayFileName.Substring(0, DisplayFileName.LastIndexOf(".")), DisplayFileName)
        Dim strFileExtension As String = PhysicalFilePathAndFileName.Substring(PhysicalFilePathAndFileName.LastIndexOf(".")).ToLower()
        Dim strFileContentType As String = CommonWeb.GetFileContentType_ByFilePath(PhysicalFilePathAndFileName)

        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.ContentType = strFileContentType + "; charset=utf-8; name=" + DisplayFileName.Replace(" ", "_")
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + DisplayFileName.Replace(" ", "_") + strFileExtension)

        HttpContext.Current.Response.TransmitFile(PhysicalFilePathAndFileName)
        HttpContext.Current.Response.End()

    End Sub

    Public Shared Sub DownloadDocument_ByString(ByVal strFileName As String, ByVal strDocumentContent As String)
        Dim strFileContentType As String = GetFileContentType_ByFilePath(strFileName)
        Dim bytesDocument() As Byte = Encoding.UTF8.GetBytes(strDocumentContent)

        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename='" + strFileName + "'")
        HttpContext.Current.Response.ContentType = strFileContentType + "; charset=utf-8; name='" + strFileName + "'"

        HttpContext.Current.Response.BinaryWrite(bytesDocument)
        HttpContext.Current.Response.End()

    End Sub

    Public Shared Function EncodeBase64String(ByVal strBase64Decoded As String) As String
        Dim strBase64Encoded As String = ""

        If Not strBase64Decoded = "" Then

            Dim bytesBase64Decoded As Byte() = System.Text.Encoding.ASCII.GetBytes(strBase64Decoded)
            Dim check As Integer = 0
            For Each byteBase64Decoded As Byte In bytesBase64Decoded
                check = check + byteBase64Decoded
            Next
            strBase64Decoded = strBase64Decoded & "-" & check.ToString()
            strBase64Encoded = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(strBase64Decoded)).Replace("+", "-").Replace("/", "_").Replace("=", "*")
            If strBase64Encoded.EndsWith("*") Then
                strBase64Encoded = strBase64Encoded & "s"
            End If
            strBase64Encoded = HttpContext.Current.Server.UrlEncode(strBase64Encoded)
        End If

        Return strBase64Encoded
    End Function

    Public Shared Function DecodeBase64String(ByVal strBase64Encoded As String) As String
        Dim strBase64Decoded As String = ""

        strBase64Encoded = HttpContext.Current.Server.UrlDecode(strBase64Encoded)

        If strBase64Encoded.EndsWith("*s") Then
            strBase64Encoded = strBase64Encoded.Substring(0, strBase64Encoded.Length - 1)
        End If
        strBase64Encoded = strBase64Encoded.Replace("-", "+").Replace("_", "/").Replace("*", "=")

        Dim bytesBase64Encoded As Byte() = Convert.FromBase64String(strBase64Encoded)
        strBase64Decoded = System.Text.Encoding.ASCII.GetString(bytesBase64Encoded)

        Dim index As Integer = strBase64Decoded.LastIndexOf("-")
        Dim checkStr As String = strBase64Decoded.Substring(index + 1)

        Dim expected As Integer
        If Int32.TryParse(checkStr, expected) Then
            strBase64Decoded = strBase64Decoded.Substring(0, index)
            Dim bytesBase64Decoded As Byte() = System.Text.Encoding.ASCII.GetBytes(strBase64Decoded)
            Dim check As Integer = 0
            For Each byteBase64Decoded As Byte In bytesBase64Decoded
                check = check + byteBase64Decoded
            Next
            If check <> expected Then
                strBase64Decoded = ""
            End If

        End If

        Return strBase64Decoded
    End Function

    Public Shared Function FormatName(ByVal SalutationID As Integer, ByVal FirstName As String, ByVal LastName As String) As String
        Dim strSalutation_LangaugeSpecific As String = String.Empty
        If SalutationID > 0 Then
            Dim dtSalutation As DataTable = SalutationDAL.GetSalutation_ByID(SalutationID)
            If dtSalutation.Rows.Count > 0 Then
                Dim drSalutation As DataRow = dtSalutation.Rows(0)

                If Not drSalutation("Salutation_LanguageProperty") Is DBNull.Value Then
                    Dim strSalutation_LanguageProperty As String = drSalutation("Salutation_LanguageProperty").ToString()
                    strSalutation_LangaugeSpecific = LanguageDAL.GetResourceValueForCurrentLanuage("DropDownList", strSalutation_LanguageProperty)
                End If
            End If
        End If
        Return FormatName(strSalutation_LangaugeSpecific, FirstName, LastName)
    End Function

    Public Shared Function FormatName(ByVal Salutation As String, ByVal FirstName As String, ByVal LastName As String) As String
        Dim sbFormatName As New StringBuilder()
        sbFormatName.Append(Salutation)
        sbFormatName.Append(If(sbFormatName.Length = 0, FirstName, " " & FirstName))
        sbFormatName.Append(If(sbFormatName.Length = 0, LastName, " " & LastName))

        Return sbFormatName.ToString()
    End Function

    Enum AddressFormat
        OneLine = 1
        MultipleLines = 2
        MultipleLinesForHTML = 3
    End Enum

    Public Shared Function FormatAddress(ByVal strAddress As String, ByVal strCity As String, ByVal intStateID As Integer, ByVal strZipCode As String, ByVal intCountryID As Integer, ByVal Format As AddressFormat) As String

        'Set the seperation character based on the Format required
        Dim strSeperationCharacter As String = String.Empty
        Select Case Format
            Case AddressFormat.MultipleLines
                strSeperationCharacter = Environment.NewLine
            Case AddressFormat.MultipleLinesForHTML
                strSeperationCharacter = "<br/>"
            Case Else
                strSeperationCharacter = " "
        End Select

        'Get the State Code
        Dim strStateAbbreviation As String = String.Empty
        Dim dtState As DataTable = StateDAL.GetState_ByID(intStateID)
        If dtState.Rows.Count > 0 Then
            strStateAbbreviation = dtState.Rows(0)("StateAbbreviation")
        End If
        'Get the CountryName
        Dim strCountryName As String = String.Empty
        Dim dtCountry As DataTable = CountryDAL.GetCountry_ByCountryID(intCountryID)
        If dtCountry.Rows.Count > 0 Then
            strCountryName = dtCountry.Rows(0)("CountryName")
        End If

        Dim sbAddress As New StringBuilder
        If strAddress.Length > 0 Then
            sbAddress.Append(strAddress) 'Append Address

            'Append our seperation Character if City, StateCode, ZipCode, or Countryname has Length greater than 0
            If strCity.Length > 0 Or strStateAbbreviation.Length > 0 Or strZipCode.Length > 0 Or strCountryName.Length > 0 Then
                'If the format is one-line, then we must include a comma after the address
                If Format = AddressFormat.OneLine AndAlso strAddress.Length > 0 Then
                    sbAddress.Append(",")
                End If
                sbAddress.Append(strSeperationCharacter) 'Append our seperation character
            End If
        End If

        'Append the City
        If strCity.Length > 0 Then
            sbAddress.Append(strCity)
            If Format = AddressFormat.OneLine Then
                'Append a comma and the seperator if the state, zip or country has length > 0 (we are still adding to this string)
                If strStateAbbreviation.Length > 0 Or strZipCode.Length > 0 Or strCountryName.Length > 0 Then
                    sbAddress.Append(", ")
                End If
            Else
                'we are using multiple lines, where country would sit on its own line, therefore just check stateCode and zipCode length before adding comma after city
                If strStateAbbreviation.Length > 0 Or strZipCode.Length > 0 Then
                    sbAddress.Append(", ")
                ElseIf strCountryName.Length > 0 Then
                    sbAddress.Append(strSeperationCharacter)
                End If
            End If
        End If

        'Append StateCode and ZipCode
        If strStateAbbreviation.Length > 0 Then
            sbAddress.Append(strStateAbbreviation)
            'If we have more text to come, e.g. zipcode or country then append space
            If Format = AddressFormat.OneLine Then
                'Append a comma and the seperator if the zip or country has length > 0 (we are still adding to this string)
                If strZipCode.Length > 0 Then
                    sbAddress.Append(" ")
                ElseIf strCountryName.Length > 0 Then
                    sbAddress.Append(", ")
                End If
            Else
                'we are using multiple lines, where country would sit on its own line, therefore just check stateCode and zipCode length before adding comma after city
                If strZipCode.Length > 0 Then
                    sbAddress.Append(" ")
                ElseIf strCountryName.Length > 0 Then
                    sbAddress.Append(strSeperationCharacter)
                End If
            End If
        End If

        'Append Zip Code
        If strZipCode.Length > 0 Then
            sbAddress.Append(strZipCode)
            'If we have more text to come, e.g.country then append comma
            If strCountryName.Length > 0 Then
                If Format = AddressFormat.OneLine Then
                    'Append a comma and the seperator if the zip or country has length > 0 (we are still adding to this string)
                    sbAddress.Append(",")
                End If
                sbAddress.Append(strSeperationCharacter)
            End If
        End If

        If strCountryName.Length > 0 Then
            sbAddress.Append(strCountryName)
        End If

        Return sbAddress.ToString()
    End Function

    Public Shared Function FormatAddress(ByVal strAddress As String, ByVal strCity As String, ByVal strStateAbbreviation As String, ByVal strZipCode As String, ByVal strCountryName As String, ByVal Format As AddressFormat) As String
        'Set the seperation character based on the Format required
        Dim strSeperationCharacter As String = String.Empty
        Select Case Format
            Case AddressFormat.MultipleLines
                strSeperationCharacter = Environment.NewLine
            Case AddressFormat.MultipleLinesForHTML
                strSeperationCharacter = "<br/>"
            Case Else
                strSeperationCharacter = " "
        End Select

        Dim sbAddress As New StringBuilder
        If strAddress.Length > 0 Then
            sbAddress.Append(strAddress) 'Append Address

            'Append our seperation Character if City, StateCode, ZipCode, or Countryname has Length greater than 0
            If strCity.Length > 0 Or strStateAbbreviation.Length > 0 Or strZipCode.Length > 0 Or strCountryName.Length > 0 Then
                'If the format is one-line, then we must include a comma after the address
                If Format = AddressFormat.OneLine AndAlso strAddress.Length > 0 Then
                    sbAddress.Append(",")
                End If
                sbAddress.Append(strSeperationCharacter) 'Append our seperation character
            End If
        End If

        'Append the City
        If strCity.Length > 0 Then
            sbAddress.Append(strCity)
            If Format = AddressFormat.OneLine Then
                'Append a comma and the seperator if the state, zip or country has length > 0 (we are still adding to this string)
                If strStateAbbreviation.Length > 0 Or strZipCode.Length > 0 Or strCountryName.Length > 0 Then
                    sbAddress.Append(", ")
                End If
            Else
                'we are using multiple lines, where country would sit on its own line, therefore just check stateCode and zipCode length before adding comma after city
                If strStateAbbreviation.Length > 0 Or strZipCode.Length > 0 Then
                    sbAddress.Append(", ")
                ElseIf strCountryName.Length > 0 Then
                    sbAddress.Append(strSeperationCharacter)
                End If
            End If
        End If

        'Append StateCode and ZipCode
        If strStateAbbreviation.Length > 0 Then
            sbAddress.Append(strStateAbbreviation)
            'If we have more text to come, e.g. zipcode or country then append space
            If Format = AddressFormat.OneLine Then
                'Append a comma and the seperator if the zip or country has length > 0 (we are still adding to this string)
                If strZipCode.Length > 0 Then
                    sbAddress.Append(" ")
                ElseIf strCountryName.Length > 0 Then
                    sbAddress.Append(", ")
                End If
            Else
                'we are using multiple lines, where country would sit on its own line, therefore just check stateCode and zipCode length before adding comma after city
                If strZipCode.Length > 0 Then
                    sbAddress.Append(" ")
                ElseIf strCountryName.Length > 0 Then
                    sbAddress.Append(strSeperationCharacter)
                End If
            End If
        End If

        'Append Zip Code
        If strZipCode.Length > 0 Then
            sbAddress.Append(strZipCode)
            'If we have more text to come, e.g.country then append comma
            If strCountryName.Length > 0 Then
                If Format = AddressFormat.OneLine Then
                    'Append a comma and the seperator if the zip or country has length > 0 (we are still adding to this string)
                    sbAddress.Append(",")
                End If
                sbAddress.Append(strSeperationCharacter)
            End If
        End If

        If strCountryName.Length > 0 Then
            sbAddress.Append(strCountryName)
        End If

        Return sbAddress.ToString()

    End Function

#Region "Password Hash Functions"

    ' <summary>
    ' Generates a hash for the given plain text value and returns a
    ' base64-encoded result. Before the hash is computed, a random salt
    ' is generated and appended to the plain text. This salt is stored at
    ' the end of the hash value, so it can be used later for hash
    ' verification.
    ' </summary>
    ' <param name="plainText">
    ' Plaintext value to be hashed. The function does not check whether
    ' this parameter is null.
    ' </param>
    ' < name="hashAlgorithm">
    ' Name of the hash algorithm. Allowed values are: "MD5", "SHA1",
    ' "SHA256", "SHA384", and "SHA512" (if any other value is specified
    ' MD5 hashing algorithm will be used). This value is case-insensitive.
    ' </param>
    ' < name="saltBytes">
    ' Salt bytes. This parameter can be null, in which case a random salt
    ' value will be generated.
    ' </param>
    ' <returns>
    ' Hash value formatted as a base64-encoded string.
    ' </returns>

    Public Shared Function ComputeHash(ByVal PlainText As String) As String


        ' Allocate a byte array, which will hold the salt.
        Dim strSaltText As String = CommonWeb.GetRandomCode(4, 8)
        Dim saltBytes As Byte()
        saltBytes = Encoding.UTF8.GetBytes(strSaltText)

        ' Initialize a random number generator.
        Dim rng As RNGCryptoServiceProvider
        rng = New RNGCryptoServiceProvider()

        ' Fill the salt with cryptographically strong byte values.
        rng.GetNonZeroBytes(saltBytes)


        ' Make sure hashing algorithm name is specified. (Using MD5 as Default)
        Dim sHashAlgorithm As String = String.Empty
        If Not ConfigurationManager.AppSettings("HashAlgorithm") Is Nothing Then
            sHashAlgorithm = ConfigurationManager.AppSettings("HashAlgorithm").ToString().ToUpper()
        End If

        ' Return the result.
        Return ComputeHash(PlainText, sHashAlgorithm, saltBytes)
    End Function

    Private Shared Function ComputeHash(ByVal PlainText As String, ByVal HashAlgorithm As String, ByVal SaltBytes() As Byte) As String

        ' Convert plain text into a byte array.
        Dim plainTextBytes As Byte()
        plainTextBytes = Encoding.UTF8.GetBytes(PlainText)

        ' Allocate array, which will hold plain text and salt.
        Dim plainTextWithSaltBytes() As Byte = _
            New Byte(plainTextBytes.Length + SaltBytes.Length - 1) {}

        ' Copy plain text bytes into resulting array.
        Dim I As Integer
        For I = 0 To plainTextBytes.Length - 1
            plainTextWithSaltBytes(I) = plainTextBytes(I)
        Next I

        ' Append salt bytes to the resulting array.
        For I = 0 To SaltBytes.Length - 1
            plainTextWithSaltBytes(plainTextBytes.Length + I) = SaltBytes(I)
        Next I

        ' Because we support multiple hashing algorithms, we must define
        ' hash object as a common (abstract) base class. We will specify the
        ' actual hashing algorithm class later during object creation.
        Dim hash As HashAlgorithm

        ' Initialize appropriate hashing algorithm class.
        Select Case HashAlgorithm.ToUpper()

            Case "SHA1"
                hash = New SHA1Managed()

            Case "SHA256"
                hash = New SHA256Managed()

            Case "SHA384"
                hash = New SHA384Managed()

            Case "SHA512"
                hash = New SHA512Managed()

            Case Else
                hash = New MD5CryptoServiceProvider()

        End Select

        ' Compute hash value of our plain text with appended salt.
        Dim hashBytes As Byte()
        hashBytes = hash.ComputeHash(plainTextWithSaltBytes)

        ' Create array which will hold hash and original salt bytes.
        Dim hashWithSaltBytes() As Byte = _
                                   New Byte(hashBytes.Length + _
                                            SaltBytes.Length - 1) {}

        ' Copy hash bytes into resulting array.
        For I = 0 To hashBytes.Length - 1
            hashWithSaltBytes(I) = hashBytes(I)
        Next I

        ' Append salt bytes to the result.
        For I = 0 To SaltBytes.Length - 1
            hashWithSaltBytes(hashBytes.Length + I) = SaltBytes(I)
        Next I

        ' Convert result into a base64-encoded string.
        Dim hashValue As String
        hashValue = Convert.ToBase64String(hashWithSaltBytes)

        ' Return the result.
        Return hashValue
    End Function

    ' <summary>
    ' Compares a hash of the specified plain text value to a given hash
    ' value. Plain text is hashed with the same salt value as the original
    ' hash.
    ' </summary>
    ' <param name="plainText">
    ' Plain text to be verified against the specified hash. The function
    ' does not check whether this parameter is null.
    ' </param>
    ' < name="hashAlgorithm">
    ' Name of the hash algorithm. Allowed values are: "MD5", "SHA1",
    ' "SHA256", "SHA384", and "SHA512" (if any other value is specified
    ' MD5 hashing algorithm will be used). This value is case-insensitive.
    ' </param>
    ' < name="hashValue">
    ' Base64-encoded hash value produced by ComputeHash function. This value
    ' includes the original salt appended to it.
    ' </param>
    ' <returns>
    ' If computed hash mathes the specified hash the function the return
    ' value is true; otherwise, the function returns false.
    ' </returns>
    Public Shared Function VerifyHash(ByVal PlainText As String, ByVal HashValue As String) As Boolean

        ' Convert base64-encoded hash value into a byte array.
        Dim hashWithSaltBytes As Byte()
        hashWithSaltBytes = Convert.FromBase64String(HashValue)

        ' We must know size of hash (without salt).
        Dim hashSizeInBits As Integer
        Dim hashSizeInBytes As Integer

        ' Make sure hashing algorithm name is specified. (Using MD5 as Default)
        Dim sHashAlgorithm As String = String.Empty
        If Not ConfigurationManager.AppSettings("HashAlgorithm") Is Nothing Then
            sHashAlgorithm = ConfigurationManager.AppSettings("HashAlgorithm").ToString().ToUpper()
        End If

        ' Size of hash is based on the specified algorithm.
        Select Case sHashAlgorithm

            Case "SHA1"
                hashSizeInBits = 160

            Case "SHA256"
                hashSizeInBits = 256

            Case "SHA384"
                hashSizeInBits = 384

            Case "SHA512"
                hashSizeInBits = 512

            Case Else ' Must be MD5
                hashSizeInBits = 128

        End Select

        ' Convert size of hash from bits to bytes.
        hashSizeInBytes = hashSizeInBits / 8

        ' Make sure that the specified hash value is long enough.
        If (hashWithSaltBytes.Length < hashSizeInBytes) Then
            VerifyHash = False
        End If

        ' Allocate array to hold original salt bytes retrieved from hash.
        Dim saltBytes() As Byte = New Byte(hashWithSaltBytes.Length - _
                                           hashSizeInBytes - 1) {}

        ' Copy salt from the end of the hash to the new array.
        Dim I As Integer
        For I = 0 To saltBytes.Length - 1
            saltBytes(I) = hashWithSaltBytes(hashSizeInBytes + I)
        Next I

        ' Compute a new hash string.
        Dim expectedHashString As String
        expectedHashString = ComputeHash(PlainText, sHashAlgorithm, saltBytes)

        ' If the computed hash matches the specified hash,
        ' the plain text value must be correct.
        Return (HashValue = expectedHashString)
    End Function

#End Region

#Region "Home Page, MasterPages and Interior Page Functions"
    Public Shared Function GetCorporateUrl() As String
        'Dim sCorporateUrlHost As String = HttpContext.Current.Request.Url.Host
        ''Check if the host starts with 2 characters (Country-Code, followed by a dot), if so we remove the country-code so we can return the Main Sites Url
        'If sCorporateUrlHost(2) = "." Then
        '    sCorporateUrlHost = sCorporateUrlHost.Substring(3)
        'End If
        'Return HttpContext.Current.Request.Url.Scheme & "://" & sCorporateUrlHost
        Return "http://www" & ConfigurationManager.AppSettings("SiteDomainForSessionSharing").ToString()
    End Function

    Public Shared Function DoesWebPageAlreadyExist(ByVal strWebPageName As String, ByVal strWebPageName_Parent As String, ByVal intWebInfoID As Integer, ByVal intWebInfoID_Header As Integer, ByVal intWebInfoID_Footer As Integer, ByVal boolSecureMember As Boolean, ByVal boolSecureEducation As Boolean, ByVal intSiteID As Integer) As Boolean
        Dim boolWebPageAlreadyExists As Boolean = False
        'If the webpage name is for a section and it already exists at the section level/Header Level/Footer Level, then show Section Error
        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfoList_ByPageNameAndParentName(strWebPageName, strWebPageName_Parent, intWebInfoID_Header, intWebInfoID_Footer, boolSecureMember, boolSecureEducation, intSiteID)

        'Web are updating a page, this means if we have ANY WebInfo Pages with the same pagename and parent name, then check the ID in the list of returned pages is the same as our current ID
        For Each drWebInfo As DataRow In dtWebInfo.Rows
            Dim intWebInfo_CurrentRow As Integer = Convert.ToInt32(drWebInfo("ID"))
            Dim bLinkOnly As Boolean = Convert.ToBoolean(drWebInfo("LinkOnly"))
            If intWebInfoID <> intWebInfo_CurrentRow AndAlso bLinkOnly = False Then
                boolWebPageAlreadyExists = True
            End If
        Next

        'If we are updating and this page and it has the same same ID as the duplicate page found, then this is OK
        'Else if the webpage name is a sub page and it already exists for this parent, then show a Subpage error
        'If we are updating and this sub-page and it has the same same ID as the duplicate page found, then this is OK
        Return boolWebPageAlreadyExists
    End Function

    Public Shared Function DoesWebPageAlreadyExist(ByVal strWebPageName As String, ByVal intWebInfoID As Integer, ByVal intWebInfoID_Parent As Integer, ByVal boolSecureMember As Boolean, ByVal boolSecureEducation As Boolean, ByVal intSiteID As Integer) As Boolean
        Dim boolWebPageAlreadyExists As Boolean = False
        'If the webpage name is for a section and it already exists at the section level/Header Level/Footer Level, then show Section Error
        Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfoList_ByPageNameAndParentID(strWebPageName, intWebInfoID_Parent, boolSecureMember, boolSecureEducation, intSiteID)

        'Web are updating a page, this means if we have ANY WebInfo Pages with the same pagename and parent name, then check the ID in the list of returned pages is the same as our current ID
        For Each drWebInfo As DataRow In dtWebInfo.Rows
            Dim intWebInfo_CurrentRow As Integer = Convert.ToInt32(drWebInfo("ID"))
            If intWebInfoID <> intWebInfo_CurrentRow Then
                boolWebPageAlreadyExists = True
            End If
        Next

        'If we are updating and this page and it has the same same ID as the duplicate page found, then this is OK
        'Else if the webpage name is a sub page and it already exists for this parent, then show a Subpage error
        'If we are updating and this sub-page and it has the same same ID as the duplicate page found, then this is OK
        Return boolWebPageAlreadyExists
    End Function


    'This is an expensive call, so we do not check access with this call ALSO once you use this store the pageID as a at the top of the page (see \webroot\interior.aspx)
    'We need the webinfoID_Header and webinfoID_Footer, just incase we can not find the page and the page ends up being a section. If you know for sure its not a header page or footer page, then just set this WebInfoID_Header and WebInfoID_Footer to Integer.MinValue
    Public Shared Function GetCurrentPageID(ByVal WebInfoID_Header As Integer, ByVal WebInfoID_Footer As Integer, ByVal boolSecureMember As Boolean, ByVal boolSecureEducation As Boolean) As Integer
        Dim intWebInfoID As Integer = Integer.MinValue

        Dim strPageName_Parent As String = ""
        If Not HttpContext.Current.Request.QueryString("parent") Is Nothing Then
            strPageName_Parent = HttpContext.Current.Request.QueryString("parent")
        End If

        If Not HttpContext.Current.Request.QueryString("page") Is Nothing Then
            Dim strPageName As String = HttpContext.Current.Request.QueryString("page")

            intWebInfoID = GetCurrentPageID(strPageName, strPageName_Parent, WebInfoID_Header, WebInfoID_Footer, boolSecureMember, boolSecureEducation)
        End If

        Return intWebInfoID

    End Function

    'This is an expensive call, so we do not check access with this call ALSO once you use this store the pageID as a at the top of the page (see \webroot\interior.aspx)
    'We need the webinfoID_Header and webinfoID_Footer, just incase we can not find the page and the page ends up being a section. If you know for sure its not a header page or footer page, then just set this WebInfoID_Header and WebInfoID_Footer to Integer.MinValue
    Public Shared Function GetCurrentPageID(ByVal PageName As String, ByVal PageName_Parent As String, ByVal WebInfoID_Header As Integer, ByVal WebInfoID_Footer As Integer, ByVal boolSecureMember As Boolean, ByVal boolSecureEducation As Boolean) As Integer
        Dim intWebInfoID As Integer = Integer.MinValue

        Dim strPageName_Parent As String = ""
        If PageName_Parent.Length > 0 Then
            strPageName_Parent = CommonWeb.decodeHyperlink(PageName_Parent)
        End If

        If PageName.Length > 0 Then
            Dim strPageName As String = CommonWeb.decodeHyperlink(PageName.Replace(".aspx", ""))

            'Note we do not check access with this call, we will do this when we do a getWebInfoByID as the below call is already expensive
            Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfoList_ByPageNameAndParentName(strPageName, strPageName_Parent, WebInfoID_Header, WebInfoID_Footer, boolSecureMember, boolSecureEducation, SiteDAL.GetCurrentSiteID_FrontEnd())

            If dtWebInfo.Rows.Count > 0 Then
                intWebInfoID = dtWebInfo.Rows(0)("ID").ToString()
            End If
        End If

        Return intWebInfoID

    End Function

    ' We need the webinfoID_Header and webinfoID_Footer, just incase the current page is inside either the header or footer container, if so we do not want to show the header/footer container.
    ' If you are certain, the header or footer container will never appear in this lefthand sub menu, e.g. in the Members Interior page, then just set this to Integer.MinValue
    Public Shared Function GetLeftHandSubMenuLinks(ByVal WebInfoID As Integer, ByVal WebInfoID_Header As Integer, ByVal WebInfoID_Footer As Integer, ByVal boolEnableGroupsAndUsers As Boolean, ByVal strUrlPrefix As String) As String

        Dim sbLeftMenu As New StringBuilder()
        Dim sbLeftMenu_NextLevel As New StringBuilder()

        Dim intWebInfoID As Integer = WebInfoID
        If intWebInfoID > 0 Then

            'Now if the current page is not a section page get all pages that have a parentID of the current page, so we can show all pages that the user can click on
            'Then using the current webinfo ID, keep going back until the parentID is equal to the sectionID
            Dim intWebInfoId_CurrentlyProcessing As Integer = intWebInfoID
            Dim intWebInfoId_LastProcessed As Integer = intWebInfoID

            'Loop backwards until the node we will process next is the same as the sectionID
            While intWebInfoId_CurrentlyProcessing > 0
                Dim dtWebInfo_Current As DataTable = If(boolEnableGroupsAndUsers, WebInfoDAL.GetWebInfoList_ByParentIDAndAccess_FrontEnd(intWebInfoId_CurrentlyProcessing, MemberDAL.GetCurrentMemberGroupIDs, MemberDAL.GetCurrentMemberID()), WebInfoDAL.GetWebInfoList_ByParentID_FrontEnd(intWebInfoId_CurrentlyProcessing))

                sbLeftMenu.Append("<ul>")
                For Each drWebInfo_Current As DataRow In dtWebInfo_Current.Rows
                    Dim intWebInfoID_Current As Integer = Convert.ToInt32(drWebInfo_Current("ID"))
                    Dim intWebInfoParentID_Current As Integer = Convert.ToInt32(drWebInfo_Current("ParentID"))
                    Dim strWebInfoParentName_Current As String = drWebInfo_Current("ParentName").ToString()
                    Dim strWebInfoName_Current As String = drWebInfo_Current("Name").ToString()

                    'If we are populating a submenu, where the pages belong to the header container or the footer container, we set the parent name to empty string, so it will keep the header urls and footer urls in the root of the site
                    If intWebInfoParentID_Current = WebInfoID_Header Or intWebInfoParentID_Current = WebInfoID_Footer Then
                        strWebInfoParentName_Current = String.Empty
                    End If

                    'If the current page we are dealing with is either the header container page or the footer container page, due to viewing a header page or footer page, then we skip over adding this page. So both the header container and footer container would not be included in the left Sub Menu
                    If intWebInfoID_Current <> WebInfoID_Header AndAlso intWebInfoID_Current <> WebInfoID_Footer Then
                        Dim strWebInfoLinkUrl As String = ""
                        If Not drWebInfo_Current("LinkUrl") Is DBNull.Value Then
                            strWebInfoLinkUrl = drWebInfo_Current("LinkUrl").ToString()
                        End If

                        Dim strWebInfoLinkFrame As String = ""
                        If Not drWebInfo_Current("LinkFrame") Is DBNull.Value Then
                            strWebInfoLinkFrame = drWebInfo_Current("LinkFrame").ToString()
                        End If

                        Dim sbWebInfoGeneratedLink As New StringBuilder()
                        sbWebInfoGeneratedLink.Append("<a ")

                        If strWebInfoLinkUrl.Length > 0 Then
                            sbWebInfoGeneratedLink.Append("href='" & strWebInfoLinkUrl & "' ")
                            'If there is a link saved in the db then don't build a link 
                            If strWebInfoLinkFrame.Length > 0 Then
                                sbWebInfoGeneratedLink.Append("target='" & strWebInfoLinkFrame & "' ")
                            End If
                        Else
                            'Build the link 
                            strWebInfoLinkUrl = WebInfoDAL.GetWebInfoUrl(strWebInfoName_Current, strWebInfoParentName_Current, strUrlPrefix)
                            sbWebInfoGeneratedLink.Append("href='" & strWebInfoLinkUrl & "'")
                        End If
                        sbWebInfoGeneratedLink.Append(If(intWebInfoID_Current = intWebInfoID, " class='navactive'", "") & ">" & strWebInfoName_Current & "</a>")



                        'then we just recently processed the inner content for this node, so add it
                        If intWebInfoID_Current = intWebInfoId_LastProcessed AndAlso (Not sbLeftMenu_NextLevel.ToString() = "<ul></ul>") Then
                            sbLeftMenu.Append("<li class='container'>" & sbWebInfoGeneratedLink.ToString())
                            sbLeftMenu.Append(sbLeftMenu_NextLevel.ToString())
                        Else
                            sbLeftMenu.Append("<li>" & sbWebInfoGeneratedLink.ToString())
                        End If

                        sbLeftMenu.Append("</li>")
                    Else
                        'then we just recently processed the inner content for this node, so add it
                        If intWebInfoID_Current = intWebInfoId_LastProcessed Then
                            sbLeftMenu.Append(sbLeftMenu_NextLevel.ToString())
                        End If
                    End If


                Next
                sbLeftMenu.Append("</ul>")

                'Then get the webinfo we are currently processing
                Dim dtWebInfo_Parent As DataTable = If(boolEnableGroupsAndUsers, WebInfoDAL.GetWebInfo_ByIDAndAccess_FrontEnd(intWebInfoId_CurrentlyProcessing, MemberDAL.GetCurrentMemberGroupIDs(), MemberDAL.GetCurrentMemberID()), WebInfoDAL.GetWebInfo_ByID_FrontEnd(intWebInfoId_CurrentlyProcessing))
                Dim intWebInfoID_Parent As Integer = 0
                If dtWebInfo_Parent.Rows.Count > 0 Then
                    If Not dtWebInfo_Parent.Rows(0)("ParentID") Is DBNull.Value Then
                        intWebInfoID_Parent = Convert.ToInt32(dtWebInfo_Parent.Rows(0)("ParentID"))
                    End If

                End If

                intWebInfoId_LastProcessed = intWebInfoId_CurrentlyProcessing
                intWebInfoId_CurrentlyProcessing = intWebInfoID_Parent

                sbLeftMenu_NextLevel = New StringBuilder(sbLeftMenu.ToString())
                sbLeftMenu = New StringBuilder()

            End While
        End If

        'Finally if sbLeftMenu_NextLevel is empty (i.e. only contains the intial <ul></ul> tags, then replace it with an empty table, such that we get no PAGE CONTENT in the left hand nav
        If sbLeftMenu_NextLevel.Length < 10 Then
            sbLeftMenu_NextLevel.Append("<table><tr><td></td></tr></table>")
        End If
        Return sbLeftMenu_NextLevel.ToString()
    End Function

    Public Shared Sub SetupRadNavMenu_For_ThreeColumnLayout(ByRef telerikRadMenu As Telerik.Web.UI.RadMenu, ByVal hashMaxColumnCount As Hashtable)
        'Setup our RadMenu with 'DefaultGroupSettings' and its 'CollapseAnimation'
        telerikRadMenu.CssClass = "ThreeColumnMenu"
        telerikRadMenu.DefaultGroupSettings.Flow = ItemFlow.Vertical
        telerikRadMenu.DefaultGroupSettings.RepeatColumns = 3
        telerikRadMenu.DefaultGroupSettings.RepeatDirection = MenuRepeatDirection.Vertical
        telerikRadMenu.DefaultGroupSettings.ExpandDirection = ExpandDirection.Down
        telerikRadMenu.CollapseAnimation.Duration = 0
        telerikRadMenu.CollapseAnimation.Type = AnimationType.None

        'Go througth each key and get the int array
        For Each dictMaxColumnCount As DictionaryEntry In hashMaxColumnCount
            'get the parent node id, which we use to get the parent node
            Dim iWebInfoID As Integer = Convert.ToInt32(dictMaxColumnCount.Key)
            Dim rtnParent As RadMenuItem = telerikRadMenu.FindItemByValue(iWebInfoID)

            Dim iListMaxColumnCount() As Integer = dictMaxColumnCount.Value
            'Loop through each column
            ' Go through the int array finding the max number of nodes in each X (e.g for 2, 5, 4) max is 5
            Dim iColumnMaxIndex As Integer = 0
            Dim iColumnMaxValue As Integer = 0
            For iColumnIndex As Integer = 0 To iListMaxColumnCount.Count - 1
                If iListMaxColumnCount(iColumnIndex) > iColumnMaxValue Then
                    iColumnMaxValue = iListMaxColumnCount(iColumnIndex)
                    iColumnMaxIndex = iColumnIndex
                End If
            Next

            'Now we have the max column index and the number of rows in this column
            'So we go through all columns ensuring they have the same number of rows, if not add blank nodes
            For iColumnIndex As Integer = 0 To iListMaxColumnCount.Count - 1
                Dim iRowIndex As Integer = iListMaxColumnCount(iColumnIndex)

                'Only add blank nodes
                For iCurrentRowIndex As Integer = iRowIndex To iColumnMaxValue - 1
                    Dim iInsertIndex As Integer = (iColumnIndex * iColumnMaxValue) + iRowIndex

                    Dim rtnBlank As New RadMenuItem("")
                    rtnBlank.Width = 0
                    rtnParent.Items.Insert(iInsertIndex, rtnBlank)
                Next
            Next

            'get the number of columns used
            Dim iNumberOfColumnsUsed As Integer = 0

            'Finally we put an extra node at the start of each column, for our colour border
            'note we only set the text to a div if its maxColumnCount for the column is greater than 0
            Dim sColorBorder As String = "green" 'iterates between red, green, blue
            Dim sColorHex As String = "#9ea616"
            For iColumnIndex As Integer = 0 To iListMaxColumnCount.Count - 1

                'get the the appropriate insertion index

                Dim iInsertIndex As Integer = iColumnIndex * (iColumnMaxValue + 1) ' need to plus 1 on the max value as we are adding a new node to EACH column, such that all values will increase by 1


                'Create a color bar node, if required
                Dim iActualRowsForColumn As Integer = iListMaxColumnCount(iColumnIndex)
                If iActualRowsForColumn > 0 Then
                    Dim sColumnTopNodeText As String = "<div style='min-width:160px;border-top:solid 2px " & sColorHex & ";'></div>"
                    'Used this color so change it

                    Select Case sColorBorder.ToLower()
                        Case "green"
                            sColorBorder = "orange"
                            sColorHex = "#f59141"
                        Case "orange"
                            sColorBorder = "blue"
                            sColorHex = "#40b5e4"
                        Case "blue"
                            sColorBorder = "green"
                            sColorHex = "#9ea616"
                    End Select

                    'We create and add a NORMAL sized node with appropriate text
                    Dim rtnTopNode As New RadMenuItem(sColumnTopNodeText)
                    rtnParent.Items.Insert(iInsertIndex, rtnTopNode)

                    'then increment the number of coumns used
                    iNumberOfColumnsUsed = iNumberOfColumnsUsed + 1
                Else
                    'We create a blank node like before with a width of 0px
                    Dim rtnBlank As New RadMenuItem("")
                    rtnBlank.CssClass = "rmBlank"
                    rtnBlank.Width = 0
                    rtnParent.Items.Insert(iInsertIndex, rtnBlank)
                End If

            Next

            'Finally we attach a class to the parent node, that indicates the number of ACTUAL columns we use for each drop down
            rtnParent.CssClass = iNumberOfColumnsUsed & "_col"
        Next
    End Sub

    Public Shared Function GetBannerImage(ByVal PageID As Integer, ByVal SiteID As Integer) As Byte()

        Dim bytesBannerImage() As Byte

        'Get the Banner Image - By First Checking if we have a PageID for this page, if not, check if this page is a module page
        If PageID > 0 Then
            'Load the Banner by this pageID
            Dim dtWebInfo As DataTable = WebInfoDAL.GetWebInfoBannerImage_ByID(PageID)
            If dtWebInfo.Rows.Count > 0 Then
                Dim drWebInfo As DataRow = dtWebInfo.Rows(0)

                If (Not drWebInfo("BannerImage") Is DBNull.Value) AndAlso (drWebInfo("BannerImage").ToString().Length > 0) Then
                    bytesBannerImage = drWebInfo("BannerImage")
                Else
                    'If the webpage allows Inheriting the Section Banner Image, we will load that if available
                    Dim boolInheritBannerImage As Boolean = Convert.ToInt32(drWebInfo("inheritBannerImage"))
                    If (boolInheritBannerImage) AndAlso (Not drWebInfo("BannerImage_ForSection") Is DBNull.Value) AndAlso (drWebInfo("BannerImage_ForSection").ToString().Length > 0) Then
                        bytesBannerImage = drWebInfo("BannerImage_ForSection")
                    End If
                End If
            End If

        Else
            'Load the Banner based on the type of module
            Dim strModuleLocationFrontEnd As String = HttpContext.Current.Request.Url.LocalPath
            strModuleLocationFrontEnd = strModuleLocationFrontEnd.Substring(0, strModuleLocationFrontEnd.LastIndexOf("/")) & "/"
            Dim dtModule As DataTable = ModuleDAL.GetModuleList_ByModuleLocationFrontEndAndSiteID(strModuleLocationFrontEnd, SiteID)
            If dtModule.Rows.Count > 0 Then
                Dim drModule As DataRow = dtModule.Rows(0)
                If (Not drModule("ModuleBannerImage") Is DBNull.Value) AndAlso (drModule("ModuleBannerImage").ToString().Length > 0) Then
                    bytesBannerImage = drModule("ModuleBannerImage")
                End If

            End If

        End If
        Return bytesBannerImage
    End Function

    Public Shared Sub SetMasterPageBannerText(ByVal CurrentMasterPage As MasterPage, ByVal BannerText As String)
        'Set the Page Title
        Dim litBannerText As Literal
        litBannerText = CType(CurrentMasterPage.FindControl("litBannerText"), Literal)
        If Not litBannerText Is Nothing Then
            litBannerText.Text = BannerText
        End If
    End Sub

    Public Shared Function PageRequiresLoggedInMember() As Boolean
        Dim strPageName As String = HttpContext.Current.Request.Url.AbsolutePath.ToLower()

        'Check front-end pages
        Select Case strPageName
            Case "/contactus/default.aspx"
                Return False
            Case "/error.aspx"
                Return False
            Case "/login/default.aspx"
                Return False

        End Select

        'Check Admin Pages
        If strPageName.StartsWith("/admin/") Then
            Return False
        End If

        Return True
    End Function

    Public Shared Function IsSecurePage_ForMembers() As Boolean
        Dim strRequestUrl As String = HttpContext.Current.Request.Url.LocalPath.ToLower()
        If strRequestUrl.StartsWith("/member") Then
            'Check if the page does not contain the standard non-member secure pages
            If strRequestUrl.StartsWith("/member/addprofile.aspx") Then
                Return False
            ElseIf strRequestUrl.StartsWith("/member/addprofilesuccessful.aspx") Then
                Return False
            ElseIf strRequestUrl.StartsWith("/member/ForgottenPassword.aspx") Then
                Return False
            ElseIf strRequestUrl.StartsWith("/member/updateprofile.aspx") Then
                Return False
            ElseIf strRequestUrl.StartsWith("/member/updateprofilesuccessful.aspx") Then
                Return False
            Else
                Return True
            End If
        End If
        Return False

    End Function

    Public Shared Function IsSecurePage_ForEducation() As Boolean
        Dim strRequestUrl As String = HttpContext.Current.Request.Url.LocalPath.ToLower()
        If strRequestUrl.StartsWith("/education") Then
            'Check if the page does not contain the standard non-member secure pages
            If strRequestUrl.StartsWith("/education/addprofile.aspx") Then
                Return False
            ElseIf strRequestUrl.StartsWith("/education/addprofilesuccessful.aspx") Then
                Return False
            ElseIf strRequestUrl.StartsWith("/education/ForgottenPassword.aspx") Then
                Return False
            ElseIf strRequestUrl.StartsWith("/education/updateprofile.aspx") Then
                Return False
            ElseIf strRequestUrl.StartsWith("/education/updateprofilesuccessful.aspx") Then
                Return False
            Else
                Return True
            End If
        End If
        Return False

    End Function

    Public Shared Function GetBreadCrumbLinks(ByVal PageID As Integer, ByVal HomePageName As String, ByVal SiteID As Integer) As List(Of KeyValuePair(Of String, String))
        Dim listBreadCrumbs As New List(Of KeyValuePair(Of String, String))

        Dim sCurrentLinkUrl As String = HttpContext.Current.Request.Url.AbsolutePath.ToLower().Replace("default.aspx", "")
        Dim dtWebInfo As New DataTable
        If PageID > 0 Then
            dtWebInfo = WebInfoDAL.GetWebInfo_ByID(PageID)
        Else
            dtWebInfo = WebInfoDAL.GetWebInfo_ByLinkUrlAndSiteID(sCurrentLinkUrl, SiteID)
        End If

        If dtWebInfo.Rows.Count > 0 Then
            Dim drWebInfo As DataRow = dtWebInfo.Rows(0)
            Dim intWebInfoID As Integer = Convert.ToInt32(drWebInfo("ID"))
            Dim intSectionID As Integer = Convert.ToInt32(drWebInfo("SectionID"))

            Dim dtWebInfoList As DataTable = WebInfoDAL.GetWebInfoList_BySectionID(intSectionID)
            dtWebInfoList.PrimaryKey = New DataColumn() {dtWebInfoList.Columns("ID")}

            drWebInfo = dtWebInfoList.Rows.Find(intWebInfoID)
            While Not drWebInfo Is Nothing
                Dim strPageName As String = drWebInfo("Name")
                Dim strPageUrl As String = String.Empty

                If Not drWebInfo("linkURL") Is DBNull.Value Then
                    strPageUrl = drWebInfo("linkURL").ToString()
                Else
                    'Build the link 
                    Dim strPageName_Parent As String = String.Empty
                    If Not drWebInfo("parentName") Is DBNull.Value AndAlso drWebInfo("parentName") <> "-" Then
                        strPageName_Parent = drWebInfo("parentName").ToString()
                    End If
                    Dim boolSecureMembers As Boolean = Convert.ToBoolean(drWebInfo("Secure_Members"))
                    Dim boolSecureEducation As Boolean = Convert.ToBoolean(drWebInfo("Secure_Education"))
                    strPageUrl = WebInfoDAL.GetWebInfoUrl(strPageName, strPageName_Parent, If(boolSecureMembers, "Member", If(boolSecureEducation, "Education", String.Empty)))

                End If

                'Add the webpage and url to our breadcrumb list
                listBreadCrumbs.Insert(0, New KeyValuePair(Of String, String)(strPageName, strPageUrl))

                'Now Get the parent row
                If Not drWebInfo("ParentID") Is DBNull.Value AndAlso drWebInfo("parentName") <> "-" Then
                    Dim intWebInfoID_Parent As Integer = Convert.ToInt32(drWebInfo("ParentID"))
                    drWebInfo = dtWebInfoList.Rows.Find(intWebInfoID_Parent)
                Else
                    drWebInfo = Nothing ' Set the datarow to nothing so it exists the loop
                End If
            End While


        End If

        If listBreadCrumbs.Count > 0 Then
            'So we have a list of bread crumbs, in which case we also insert the HOME PAGE at the front of the list
            listBreadCrumbs.Insert(0, New KeyValuePair(Of String, String)(HomePageName, "/"))
        End If
        Return listBreadCrumbs
    End Function
    Public Shared Function GetBreadCrumbLinks(ByVal PageName As String, ByVal LinkUrl As String, ByVal HomePageName As String) As List(Of KeyValuePair(Of String, String))
        'Finally create a list of breadcrumbs and send it to our BreadCrumb Control if the current page, is contained in the rad menu
        Dim listBreadCrumbs As New List(Of KeyValuePair(Of String, String))

        'We get this RadMenu Item Url and Page Name, and work backwards untill the RadMenuItem has no parent, then once we have loaded in all bread crumbs we finally add the home Page
        'Note as we are working backwards, every new page url and name we must insert it at the 0th index, so its prepended
        listBreadCrumbs.Insert(0, New KeyValuePair(Of String, String)(PageName, LinkUrl))

        'So we have a list of bread crumbs, in which case we also insert the HOME PAGE at the front of the list
        listBreadCrumbs.Insert(0, New KeyValuePair(Of String, String)(HomePageName, "/"))
        Return listBreadCrumbs
    End Function
#End Region


#Region "Setup Delete Button with Delete Confirmation Message"
    Public Shared Sub SetupDeleteButton(ByRef btnDelete As System.Web.UI.WebControls.Button, ByVal strDeleteConfirmationMessage As String)

        'Set up the delete buttons OnClientClick function to a language specific confimation message
        btnDelete.OnClientClick = "return DeleteConfirmation('" & strDeleteConfirmationMessage & "')"

    End Sub

    Public Shared Sub SetupDeleteLinkButton(ByRef lnkDelete As System.Web.UI.WebControls.LinkButton, ByVal strDeleteConfirmationMessage As String)

        'Set up the delete buttons OnClientClick function to a language specific confimation message
        lnkDelete.OnClientClick = "return DeleteConfirmation('" & strDeleteConfirmationMessage & "')"

    End Sub

#End Region

#Region "Setup RollBack Button with RollBack Confirmation Message"

    Public Shared Sub SetupRollBackButton(ByRef btnRollBack As System.Web.UI.WebControls.Button, ByVal strRollBackConfirmationMessage As String)

        'Set up the RollBack buttons OnClientClick function to a language specific confimation message
        btnRollBack.OnClientClick = "return DeleteConfirmation('" & strRollBackConfirmationMessage & "')"

    End Sub

#End Region

#Region "Setup Telerik Controls"
    Public Shared Sub SetupRadDatePicker(ByRef telerikRadDatePicker As Telerik.Web.UI.RadDatePicker, ByVal SiteID As Integer)
        'Set up the date pickers culture settings
        '*** pull language preference from the users session, defaults to en-US
        Dim strLanguagePreference As String = LanguageDAL.GetCurrentLanguageCode_BySiteID(SiteID)
        If strLanguagePreference.Contains("-") Then
            '*** set the cultures
            telerikRadDatePicker.Culture = Globalization.CultureInfo.GetCultureInfo(strLanguagePreference)

        End If

        'Set the DateOpenButton Tooltip
        telerikRadDatePicker.DatePopupButton.ToolTip = Resources.RadDatePicker.DatePopupButton_ClickHereToOpen
    End Sub

    Public Shared Sub SetupRadEditor(ByVal currentPage As Page, ByRef telerikRadEditor As Telerik.Web.UI.RadEditor, ByVal SiteID As Integer)
        'If you choose not to pass in a tools file, we use the default ToolsFielCustom.xml
        Dim strToolsFileLocation As String = "~/editorConfig/toolbars/ToolsFileCustom.xml"
        SetupRadEditor(currentPage, telerikRadEditor, strToolsFileLocation, SiteID)
    End Sub

    Public Shared Sub SetupRadEditor(ByVal currentPage As Page, ByRef telerikRadEditor As Telerik.Web.UI.RadEditor, ByVal strToolsFileLocation As String, ByVal SiteID As Integer)
        Dim viewDocuments As String() = New String() {"~/data/files/Site_" & SiteID.ToString()}
        Dim uploadDocuments As String() = New String() {"~/data/files/Site_" & SiteID.ToString()}
        Dim deleteDocuments As String() = New String() {"~/data/files/Site_" & SiteID.ToString()}

        Dim viewImages As String() = New String() {"~/data/images/Site_" & SiteID.ToString()}
        Dim uploadImages As String() = New String() {"~/data/images/Site_" & SiteID.ToString()}
        Dim deleteImages As String() = New String() {"~/data/images/Site_" & SiteID.ToString()}

        Dim viewMedia As String() = New String() {"~/data/media/Site_" & SiteID.ToString()}
        Dim uploadMedia As String() = New String() {"~/data/media/Site_" & SiteID.ToString()}
        Dim deleteMedia As String() = New String() {"~/data/media/Site_" & SiteID.ToString()}

        telerikRadEditor.CssFiles.Clear()
        telerikRadEditor.CssFiles.Add("~/editorConfig/css/editorStyle.css")
		telerikRadEditor.ContentAreaCssFile = "~/editorConfig/css/editorContent.css"
        telerikRadEditor.DocumentManager.MaxUploadFileSize = "20971520"
		telerikRadEditor.ImageManager.MaxUploadFileSize = "20971520"
        telerikRadEditor.ExternalDialogsPath = "/editorConfig/dialogs/"
        telerikRadEditor.ToolsFile = strToolsFileLocation
		'telerikRadEditor.DocumentManager.SearchPatterns = New String() {"*.doc,*.txt,*.docx,*.xls,*.xlsx,*.pdf,*.zip,*.ppt,*.pptx"}
		 telerikRadEditor.DocumentManager.SearchPatterns = New String() {"*.doc", "*.txt", "*.docx", "*.dotx", "*.xls", "*.xlsx", "*.pdf", "*.ppt", "*.pptx", "*.potx"}
        telerikRadEditor.Language = LanguageDAL.GetCurrentLanguageCode_BySiteID(SiteID)

        If Not currentPage.IsPostBack Then
            telerikRadEditor.DocumentManager.ViewPaths = viewDocuments
            telerikRadEditor.DocumentManager.UploadPaths = uploadDocuments
            telerikRadEditor.DocumentManager.DeletePaths = deleteDocuments
            telerikRadEditor.ImageManager.ViewPaths = viewImages
            telerikRadEditor.ImageManager.UploadPaths = uploadImages
            telerikRadEditor.ImageManager.DeletePaths = deleteImages
            telerikRadEditor.MediaManager.ViewPaths = viewMedia
            telerikRadEditor.MediaManager.UploadPaths = uploadMedia
            telerikRadEditor.MediaManager.DeletePaths = deleteMedia

        End If

    End Sub

    Public Shared Sub SetupRadGrid(ByRef telerikRadGrid As Telerik.Web.UI.RadGrid, ByVal strPagerTextFormat As String)
        'Set up the rad grid, such that its handles multiple languages

        'Hide Refresh Button
        telerikRadGrid.MasterTableView.CommandItemSettings.ShowRefreshButton = False
        telerikRadGrid.EnableLinqExpressions = False

        'Set the sorting tooltips
        telerikRadGrid.SortingSettings.SortToolTip = Resources.RadGrid.GridHeader_Sort
        telerikRadGrid.SortingSettings.SortedAscToolTip = Resources.RadGrid.GridHeader_SortAscending
        telerikRadGrid.SortingSettings.SortedDescToolTip = Resources.RadGrid.GridHeader_SortDescending

        'Set the paging tooltips
        telerikRadGrid.PagerStyle.FirstPageToolTip = Resources.RadGrid.GridPager_FirstPage
        telerikRadGrid.PagerStyle.PrevPageToolTip = Resources.RadGrid.GridPager_PrevPage
        telerikRadGrid.PagerStyle.NextPageToolTip = Resources.RadGrid.GridPager_NextPage
        telerikRadGrid.PagerStyle.LastPageToolTip = Resources.RadGrid.GridPager_LastPage
        telerikRadGrid.PagerStyle.PageSizeLabelText = Resources.RadGrid.GridPage_PageSize & ":"

        telerikRadGrid.PagerStyle.PagerTextFormat = strPagerTextFormat

        telerikRadGrid.GridLines = GridLines.None
        telerikRadGrid.AllowPaging = True
        telerikRadGrid.AllowSorting = True

        telerikRadGrid.MasterTableView.AutoGenerateColumns = False

    End Sub

    Public Shared Sub SetupRadGrid(ByRef telerikRadGrid As Telerik.Web.UI.RadGrid, ByVal strAddNewEntryText As String, ByVal strPagerTextFormat As String)
        SetupRadGrid(telerikRadGrid, strPagerTextFormat)

        telerikRadGrid.MasterTableView.CommandItemSettings.AddNewRecordText = strAddNewEntryText

    End Sub

    Public Shared Sub SetupRadProgressArea(ByRef telerikRadProgressArea As Telerik.Web.UI.RadProgressArea, ByVal SiteID As Integer)
        'Set up the Rad Progress Area culture settings
        '*** pull language preference from the users session, defaults to en-US
        Dim strLanguagePreference As String = LanguageDAL.GetCurrentLanguageCode_BySiteID(SiteID)
        If strLanguagePreference.Contains("-") Then
            '*** set the cultures
            telerikRadProgressArea.Culture = Globalization.CultureInfo.GetCultureInfo(strLanguagePreference)

        End If
    End Sub

    Public Shared Sub SetupRadTimePicker(ByRef telerikRadTimePicker As Telerik.Web.UI.RadTimePicker, ByVal SiteID As Integer)
        'Set up the time pickers culture settings
        '*** pull language preference from the users session, defaults to en-US
        Dim strLanguagePreference As String = LanguageDAL.GetCurrentLanguageCode_BySiteID(SiteID)
        If strLanguagePreference.Contains("-") Then
            '*** set the cultures
            telerikRadTimePicker.Culture = Globalization.CultureInfo.GetCultureInfo(strLanguagePreference)
            telerikRadTimePicker.DateInput.Culture = Globalization.CultureInfo.GetCultureInfo(strLanguagePreference)

        End If

        'Set the DateOpenButton Tooltip and the time header
        telerikRadTimePicker.TimePopupButton.ToolTip = Resources.RadTimePicker.TimePopupButton_ClickHereToOpen
    End Sub

    Public Shared Sub SetupRadUpload(ByRef telerikRadUpload As Telerik.Web.UI.RadUpload, ByVal SiteID As Integer)
        'Set up the date pickers culture settings
        '*** pull language preference from the users session, defaults to en-US
        Dim strLanguagePreference As String = LanguageDAL.GetCurrentLanguageCode_BySiteID(SiteID)
        If strLanguagePreference.Contains("-") Then
            '*** set the cultures
            telerikRadUpload.Culture = Globalization.CultureInfo.GetCultureInfo(strLanguagePreference)
        End If
    End Sub

#End Region

#Region "Setup Ultimate Search Control"
    Public Shared Sub SetupUltimateSearchOutput(ByRef karamasoftUltimateSearchOutput As Karamasoft.WebControls.UltimateSearch.UltimateSearchOutput)
        karamasoftUltimateSearchOutput.HeaderFormat = Resources.UltimateSearch.UltimateSearchOutput_ResultsInformation_Heading & " <b>[FirstIndex]</b> - <b>[LastIndex]</b> " & Resources.UltimateSearch.UltimateSearchOutput_ResultsInformation_Seperator_PagingAndCount & " <b>[ResultCount]</b> " & Resources.UltimateSearch.UltimateSearchOutput_ResultsInformation_Seperator_CountAndSearchTerms & " <b>[SearchTerms]</b>"
        karamasoftUltimateSearchOutput.FooterFormat = Resources.UltimateSearch.UltimateSearchOutput_Paging_Heading & ": <b>[FirstPageText] [PrevPageText]</b>[PrevPageImage] [PageLinks] [NextPageImage]<b>[NextPageText] [LastPageText]</b>"
        karamasoftUltimateSearchOutput.FirstPageText = Resources.UltimateSearch.UltimateSearchOutput_Paging_FirstPage
        karamasoftUltimateSearchOutput.LastPageText = Resources.UltimateSearch.UltimateSearchOutput_Paging_LastPage
        karamasoftUltimateSearchOutput.PrevPageText = Resources.UltimateSearch.UltimateSearchOutput_Paging_PreviousPage
        karamasoftUltimateSearchOutput.NextPageText = Resources.UltimateSearch.UltimateSearchOutput_Paging_NextPage
        karamasoftUltimateSearchOutput.NoDocumentsFoundFormat = "<br/><b>" & Resources.UltimateSearch.UltimateSearchOutput_NoDocumentsFoundMessage & "</b>"

    End Sub

    Public Shared Function IsSearchIndexer() As Boolean
        Dim boolIsSearchIndexer As Boolean = False

        Dim strUltimateSearchUserAgent As String = ConfigurationManager.AppSettings("UltimateSearchUserAgent")
        Dim strRequestUserAgent As String = HttpContext.Current.Request.UserAgent
        If strUltimateSearchUserAgent = strRequestUserAgent Then
            boolIsSearchIndexer = True
        End If

        Return boolIsSearchIndexer
    End Function
#End Region


End Class
