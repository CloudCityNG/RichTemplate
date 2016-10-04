Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.IO
Imports System.Xml

Partial Class admin_language_editLanguageFiles
    Inherits RichTemplateLanguagePage

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Set the Header UserControls Title and Help Item if it exists
        ucHeader.PageName = Resources.Language_Admin.Language_EditLanguageFiles_Header

        'Check the logged in user can view this page
        Dim intAdminUserAccess As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
        If intAdminUserAccess < 3 Then
            Response.Redirect("~/richadmin/")
        End If

        If Not Page.IsPostBack Then
            If Not Request.QueryString("ID") Is Nothing Then
                Dim intLanguageID As Integer = Convert.ToInt32(Request.QueryString("ID"))

                Dim dtLanguage As DataTable = LanguageDAL.GetLanguage_ByID(intLanguageID)
                If dtLanguage.Rows.Count > 0 Then
                    Dim drLanguage As DataRow = dtLanguage.Rows(0)

                    'Intially set the first language section as selected
                    rblLanguageSection.SelectedIndex = 0
                    Dim strFrontEndOrAdmin_SelectedDirectory As String = rblLanguageSection.SelectedValue.ToUpper()
                    LoadLanguageFiles(strFrontEndOrAdmin_SelectedDirectory)
                Else
                    'The languageID does not exist so redirect them to the default page
                    Response.Redirect("default.aspx")
                End If

            Else
                'The languageID has not been specified so redirect them to the default page
                Response.Redirect("default.aspx")
            End If

        End If

    End Sub

    Private Sub ResetLabels()
        'As Such the div containing our rows of Language keys and values must be hidden, untill the admin selects a specific Language file to update (based on if they are viewing front-end Language files OR admin Language Files)
        divLanguageFileKeysUpdated.Visible = False
        divLanguageFileKeys.Visible = False
        btnUpdate.Visible = False
        divLanguageFileKeysUpdated.Visible = False
    End Sub

    Private Sub LoadLanguageFiles(ByVal LanguageFileDirectory As String)
        Dim strLanguage_RootDirectory As String = HttpContext.Current.Server.MapPath("~") & "App_GlobalResources\Languages\"
        Dim dirInfo As New DirectoryInfo(strLanguage_RootDirectory & LanguageFileDirectory)

        ddlLanguageFile.Items.Clear()
        ddlLanguageFile.Items.Add(New ListItem(Resources.Language_Admin.Language_EditLanguageFiles_LanguageFile_PleaseSelect, ""))
        For Each dirInfoLanguageFiles As DirectoryInfo In dirInfo.GetDirectories()
            Dim strDirectoryName As String = dirInfoLanguageFiles.Name
            ddlLanguageFile.Items.Add(New ListItem(strDirectoryName, LanguageFileDirectory & "\" & strDirectoryName))

        Next
    End Sub

    Private Sub LoadLanguageFileKeys(ByVal LanguageFileDirectory As String)

        Dim strLanguage_RootDirectory As String = HttpContext.Current.Server.MapPath("~") & "App_GlobalResources\Languages\"

        'Load the data for this Language file, then when done, show the Language file keys and update button
        'First load the keys from the base file
        Dim strLanguageFileName_BaseFile As String = GetLanguageBaseFileLocation(strLanguage_RootDirectory & LanguageFileDirectory)

        If strLanguageFileName_BaseFile.Length > 0 Then
            Dim dtLanguageKeyAndValues As DataTable = GenerateLanguageDataTable_FromXml(strLanguageFileName_BaseFile)

            'Now load all the key values for the language specific resource file, which we will try and match up later
            Dim intLanguageID As Integer = Convert.ToInt32(Request.Params("id"))
            Dim dtLanguage As DataTable = LanguageDAL.GetLanguage_ByID(intLanguageID)
            If dtLanguage.Rows.Count > 0 Then
                Dim drLanguage As DataRow = dtLanguage.Rows(0)
                Dim strLanguageCode As String = drLanguage("Code")

                'Get the core resource file name, and use this to check if the language specific resource file already exists, if it exists we'll use it to pre-populate existing language values
                Dim strResourceFileLanguageSpecific As String = strLanguageFileName_BaseFile.Replace(".resx", "." & strLanguageCode & ".resx")
                If File.Exists(strResourceFileLanguageSpecific) Then

                    'So we already have an existing language specific resource file, so we will use this get pre-populate the language-specific values
                    UpdateLanguageDataTable_IncludeLanguageSpecificValues(strResourceFileLanguageSpecific, dtLanguageKeyAndValues)

                End If

                'Now we have our datatable populated with the base language key, the base language value, and MAYBE/POSSIBLY a pre-populated list of the language-specific values if the language specific resource file exists
                'So now bind this to our repeater, and show the Language file keys and update button, REMEMBER THIS DATA TABLE IS NOT SORTED BY LANGUAGE KEY SO sort this table before binding it to the repeater
                Dim dvLanguageKeyAndValues As New DataView(dtLanguageKeyAndValues)
                dvLanguageKeyAndValues.Sort = "LanguageKey"
                rptLanguageKeys.DataSource = dvLanguageKeyAndValues
                rptLanguageKeys.DataBind()

                divLanguageFileKeysUpdated.Visible = False
                divLanguageFileKeys.Visible = True
                btnUpdate.Visible = True
            End If
        End If

    End Sub

    Private Function GenerateLanguageDataTable_FromXml(ByVal xmlFileLocation As String) As DataTable
        'Use the file location of the BASE language specific xml document to create this BASE xml document
        Dim xmlDocument As New XmlDocument
        xmlDocument.Load(xmlFileLocation)

        'Create a datatable with 3 columns, key, base value and language-specific value
        Dim dtLanguageKeyAndValues As New DataTable
        Dim dcLanguageKeyAndValues_LanguageKey As New DataColumn("LanguageKey", GetType(String))
        Dim dcLanguageKeyAndValues_BaseValue As New DataColumn("BaseValue", GetType(String))
        Dim dcLanguageKeyAndValues_LanguageSpecificValue As New DataColumn("LanguageSpecificValue", GetType(String))
        dtLanguageKeyAndValues.Columns.AddRange(New DataColumn() {dcLanguageKeyAndValues_LanguageKey, dcLanguageKeyAndValues_BaseValue, dcLanguageKeyAndValues_LanguageSpecificValue})
        dtLanguageKeyAndValues.PrimaryKey = New DataColumn() {dcLanguageKeyAndValues_LanguageKey}

        'Go through each <data> node in our BASE XML document to create our datarows of <data> and <value> columns
        For Each xmlNodeData As XmlNode In xmlDocument.SelectNodes("root/data")

            Dim strData As String = xmlNodeData.Attributes("name").Value
            Dim strValue As String = xmlNodeData.SelectSingleNode("value").InnerText

            Dim drLanguageKeyAndValues As DataRow = dtLanguageKeyAndValues.NewRow()
            drLanguageKeyAndValues("LanguageKey") = strData
            drLanguageKeyAndValues("BaseValue") = strValue

            dtLanguageKeyAndValues.Rows.Add(drLanguageKeyAndValues)
        Next

        Return dtLanguageKeyAndValues
    End Function

    Private Function GenerateLanguageHashTable_FromXml(ByVal xmlFileLocation As String) As Dictionary(Of String, String)
        'Use the file location of the BASE language specific xml document to create this BASE xml document
        Dim xmlDocument As New XmlDocument
        xmlDocument.Load(xmlFileLocation)

        'Go through each <data> node in our BASE XML document to build up our hashtable of <data> and <value> columns
        Dim dictLanguage As New Dictionary(Of String, String)
        For Each xmlNodeData As XmlNode In xmlDocument.SelectNodes("root/data")

            Dim strData As String = xmlNodeData.Attributes("name").Value
            Dim strValue As String = xmlNodeData.SelectSingleNode("value").InnerText

            If Not dictLanguage.ContainsKey(strData) Then
                dictLanguage(strData) = strValue
            End If
        Next

        Return dictLanguage
    End Function

    Private Sub UpdateLanguageDataTable_IncludeLanguageSpecificValues(ByVal xmlFileLocation_LanguageSpecific As String, ByRef dtLanguageKeyAndValues As DataTable)

        'Use the file location of the language specific xml document to create this language specific xml document
        Dim xmlDocument_LanguageSpecific As New XmlDocument
        xmlDocument_LanguageSpecific.Load(xmlFileLocation_LanguageSpecific)

        'For each <data> node, we get this node and try and find this node in the current Resource File DataTable, if we do we update the LanguageSpecific value for this data table
        For Each xmlNodeData As XmlNode In xmlDocument_LanguageSpecific.SelectNodes("root/data")

            Dim strData As String = xmlNodeData.Attributes("name").Value

            Dim drLanguageKeyAndValues As DataRow = dtLanguageKeyAndValues.Rows.Find(strData)
            If Not drLanguageKeyAndValues Is Nothing Then
                Dim strValue As String = xmlNodeData.SelectSingleNode("value").InnerText
                drLanguageKeyAndValues("LanguageSpecificValue") = strValue
            End If
        Next
    End Sub

    Private Function GetLanguageBaseFileLocation(ByVal LanguageFileLocation As String) As String
        Dim strLanguageFileName_BaseFile As String = String.Empty
        Dim dirInfo As New DirectoryInfo(LanguageFileLocation)

        'To determine if the resource file is a BASE File, we check if it satifies the following regular expression.
        '   ^([a-zA-Z.])+(.resx)$ -> so it can contain letters and dots (NO DASHES e.g. fr-FR!)
        Dim regexResourceFile As New Regex("^([_a-zA-Z.])+(.resx)$")
        'Load the data for this Language file, then when done, show the Language file keys and update button
        'First load the keys from the base file
        For Each fileInfo As FileInfo In dirInfo.GetFiles("*.resx")

            If regexResourceFile.IsMatch(fileInfo.Name) Then
                'Get the index of the first dot, and check the following characters are resx, so we can distinguish between the base resource file and the language-specific file
                strLanguageFileName_BaseFile = fileInfo.FullName
                Exit For
            End If

        Next

        Return strLanguageFileName_BaseFile

    End Function

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub rblLanguageSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblLanguageSection.SelectedIndexChanged

        ResetLabels()

        'We the user changes the radio button between the front-end list and the admin list etc., we must update the dropdown list of Language files
        Dim strLanguageSection_SelectedDirectory As String = rblLanguageSection.SelectedValue.ToUpper()
        If strLanguageSection_SelectedDirectory = "ADMIN" Or strLanguageSection_SelectedDirectory = "FRONTEND" Or strLanguageSection_SelectedDirectory = "KARAMASOFT" Or strLanguageSection_SelectedDirectory = "TELERIK" Or strLanguageSection_SelectedDirectory = "USERCONTROLS" Then
            'Get all Language files in the 'App_GlobalResources/Languages/***' directory (either Admin, FrontEnd, Karamasoft, Telerik, UserControls)
            LoadLanguageFiles(strLanguageSection_SelectedDirectory)
        Else
            'The user may have hacked this, as we only allow Admin, FrontEnd, Karamasoft, Telerik, UserControls, so redirect them to the admin login page
            Response.Redirect("~/richadmin/")
        End If
    End Sub

    Protected Sub ddlLanguageFile_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLanguageFile.SelectedIndexChanged

        ResetLabels()

        Dim strLanguageFile As String = ddlLanguageFile.SelectedValue
        If strLanguageFile.Length > 0 Then
            'Now we have the Language file we want to update, so load it in our repeater
            LoadLanguageFileKeys(strLanguageFile)
        End If


    End Sub

    Protected Sub lnkCopyAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCopyAll_Text.Click, lnkCopyAll_Image.Click

        For Each rptItemLanguageKey As RepeaterItem In rptLanguageKeys.Items
            Dim txtBaseValue As TextBox = rptItemLanguageKey.FindControl("txtBaseValue")
            Dim txtLanguageSpecificValue As TextBox = rptItemLanguageKey.FindControl("txtLanguageSpecificValue")

            txtLanguageSpecificValue.Text = txtBaseValue.Text.Trim()
        Next

    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        addUpdateRecord()


    End Sub

    Protected Sub addUpdateRecord()
        'Now load all the key values for the language specific resource file, which we will try and match up later
        Dim intLanguageID As Integer = Convert.ToInt32(Request.Params("id"))
        Dim dtLanguage As DataTable = LanguageDAL.GetLanguage_ByID(intLanguageID)
        If dtLanguage.Rows.Count > 0 Then
            Dim drLanguage As DataRow = dtLanguage.Rows(0)
            Dim strLanguageCode As String = drLanguage("Code")

            Dim strLanguageFileName As String = ddlLanguageFile.SelectedValue
            Dim strLanguage_RootDirectory As String = HttpContext.Current.Server.MapPath("~") & "App_GlobalResources\Languages\"

            'Load the data for this Language file, then when done, show the Language file keys and update button
            'First load the keys from the base file
            Dim strLanguageFileName_BaseFile As String = GetLanguageBaseFileLocation(strLanguage_RootDirectory & strLanguageFileName)

            If strLanguageFileName_BaseFile.Length > 0 Then

                Dim dictLanguageKeyAndValues As Dictionary(Of String, String) = GenerateLanguageHashTable_FromXml(strLanguageFileName_BaseFile)

                'Now we have the list of language data, and their DEFAULT value from the base language file.
                ' So we go through each txtLanguageSpecificValue and using its associated language key we match it up with the data in the hash table, and create the <data> xml element

                'Get the core resource file name, and use this to check if the language specific resource file already exists, if it exists we'll use it to pre-populate existing language values
                Dim strResourceFileLanguageSpecific As String = strLanguageFileName_BaseFile.Replace(".resx", "." & strLanguageCode & ".resx")

                Dim swLanguageResourceFile As StreamWriter = Nothing
                Try

                    'Open the streamWriter for this language resource file
                    swLanguageResourceFile = File.CreateText(strResourceFileLanguageSpecific)

                    'Bind the <root> element to our either NEW or EXISTING language resource file
                    swLanguageResourceFile.WriteLine("<?xml version=""1.0"" encoding=""utf-8""?>")
                    swLanguageResourceFile.WriteLine("<root>")

                    'First Build the Languages Resource Header
                    swLanguageResourceFile.WriteLine(LanguageDAL.LanguageResourceFile_Header())

                    For Each rptItemLanguageKey As RepeaterItem In rptLanguageKeys.Items
                        Dim litLanguageKey As Literal = rptItemLanguageKey.FindControl("litLanguageKey")
                        Dim strLanguageKey As String = litLanguageKey.Text

                        If Not dictLanguageKeyAndValues(strLanguageKey) Is Nothing Then
                            Dim txtLanguageSpecificValue As TextBox = rptItemLanguageKey.FindControl("txtLanguageSpecificValue")
                            'The value MUST be HTML Encoded, for example Blog_Admin has a key 'Blog_AddEdit_Tab_UsersGroups' with a value Users & Groups, we should make this Users &amp; Groups
                            Dim strLanguageSpecificValue As String = Server.HtmlEncode(txtLanguageSpecificValue.Text.Trim())
                            swLanguageResourceFile.WriteLine("  <data name=""" & strLanguageKey & """ xml:space=""preserve"">")
                            swLanguageResourceFile.WriteLine("    <value>" & strLanguageSpecificValue & "</value>")
                            swLanguageResourceFile.WriteLine("  </data>")
                        End If
                    Next

                    'Finally add the final closing </root> element
                    swLanguageResourceFile.Write("</root>")

                    'Now we have all language specific keys, so now we 
                    divLanguageFileKeysUpdated.Visible = True

                Finally
                    If Not swLanguageResourceFile Is Nothing Then
                        swLanguageResourceFile.Close()
                    End If
                End Try

            End If
        End If

    End Sub

End Class
