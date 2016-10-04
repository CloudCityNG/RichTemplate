Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class admin_modules_Module_default
    Inherits RichTemplateLanguagePage

    Private ModuleTypeID As Integer = 0
    Private SiteID As Integer = 0
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Setup Rad Controls
        CommonWeb.SetupRadEditor(Page, radModuleContentHTML, SiteDAL.GetCurrentSiteID_Admin)

        If Not Request.QueryString("mtid") Is Nothing Then
            ModuleTypeID = Convert.ToInt32(Request.QueryString("mtid"))
        End If
        SiteID = SiteDAL.GetCurrentSiteID_Admin()
        If Not Page.IsPostBack Then

            'Pass in SiteID into GetModule_ByID as we want to prevent someone from another site, typing a random ModuleTypeID into the url, now only view a module that is part of their current viewing site
            Dim dtModule As DataTable = ModuleDAL.GetModule_ByModuleTypeIDAndSiteID_FrontEnd(ModuleTypeID, SiteID)
            If dtModule.Rows.Count > 0 Then

                'Does the AdminUser have access to this module?
                Dim listAllowModules As String() = AdminUserDAL.GetCurrentAdminUserAllowModules().Split(",")
                If listAllowModules.Contains(ModuleTypeID.ToString()) Then

                    'Check the user has access to this module
                    Dim drModule As DataRow = dtModule.Rows(0)



                    btnSubmit.Text = Resources.Module_Admin.Module_Default_ButtonUpdate


                    Dim strModuleLocation_Admin As String = drModule("moduleLocation_Admin").ToString()
                    Dim strModuleLanguageFilename_Admin As String = drModule("moduleLanguageFilename_Admin")

                    'If this module has a front-end URL we create the front-end url using the moduleLocation_FrontEnd, and the site domain(if exists)
                    If Not drModule("moduleLocation_FrontEnd") Is DBNull.Value Then
                        Dim strModuleLocation_FrontEnd As String = drModule("moduleLocation_FrontEnd").ToString().Trim()

                        'Get the site domain, which we use to show the front-end location of this module
                        Dim strDomain As String = String.Empty
                        Dim dtSite As DataTable = SiteDAL.GetSite_ByID(SiteID)
                        If dtSite.Rows.Count > 0 Then
                            Dim drSite As DataRow = dtSite.Rows(0)
                            If Not drSite("Domain") Is DBNull.Value Then
                                strDomain = drSite("Domain").ToString().Trim()
                            End If
                        End If

                        'Finally if our module location Url has a length then show the module location
                        Dim strModuleLocationUrl As String = strDomain & strModuleLocation_FrontEnd
                        If strModuleLocationUrl.Length > 0 Then
                            litModuleLocationUrl_Label.Text = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_ModuleConfiguration_ModuleLocation")
                            litModuleLocationUrl_Value.Text = strModuleLocationUrl

                            trModuleLocationUrl.Visible = True
                        End If
                    End If



                    'setup the module configuration heading
                    Dim strModuleHeading As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_ModuleConfiguration_Heading")
                    ucHeader.PageName = strModuleHeading

                    'setup the module configuration body heading
                    Dim strModuleHeadingBody As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_ModuleConfiguration_BodyHeading")
                    litBodyHeading.Text = strModuleHeadingBody

                    'setup the return to module link and literal
                    Dim strReturnToModule As String = LanguageDAL.GetResourceValueForCurrentLanuage(strModuleLanguageFilename_Admin, "_SiteWide_ModuleConfiguration_ReturnToModule")
                    litReturnToModule.Text = strReturnToModule
                    Me.aReturnToModule.HRef = strModuleLocation_Admin

                    If Not drModule("ModuleContentHTML") Is DBNull.Value Then
                        Me.radModuleContentHTML.Content = drModule("ModuleContentHTML")
                    End If

                    'Setup the Module Banner Image
                    moduleBannerImage.Visible = False
                    lnkDeleteImage.Visible = False
                    If (Not drModule("moduleBannerImage") Is DBNull.Value) AndAlso (drModule("moduleBannerImage").ToString().Length > 0) Then
                        moduleBannerImage.DataValue = drModule("moduleBannerImage")
                        moduleBannerImage.Visible = True
                        lnkDeleteImage.Visible = True
                    End If

                    hdnModuleLocation.Value = strModuleLocation_Admin

                    'Load all checkbox lists and dropdowns
                    BindModuleConfigTypeList(ModuleTypeID)

                    If cblModuleConfigTypeList.Items.Count > 0 Then

                        'Set this modules Configuration Values
                        Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteID)
                        For Each drModuleConfig As DataRow In dtModuleConfig.Rows
                            Dim intModuleConfigTypeID As Integer = Convert.ToInt32(drModuleConfig("ModuleConfigTypeID"))
                            Dim liModuleConfigType As ListItem = cblModuleConfigTypeList.Items.FindByValue(intModuleConfigTypeID)
                            If Not liModuleConfigType Is Nothing Then
                                liModuleConfigType.Selected = True
                            End If
                        Next
                        divModuleConfigTypeList.Visible = True
                    End If

                    'Finally if the admin users access level is a super administrator (i.e. AccessLevel > 2) Show all upload banner image relate panels
                    Dim intAdminUserAccessLevel As Integer = AdminUserDAL.GetCurrentAdminUserAccessLevel()
                    If intAdminUserAccessLevel > 2 Then
                        divBannerImage.Visible = True
                    End If

                Else
                    'url has been free typed (hacked)
                    Response.Redirect("/richadmin/")
                End If

            Else
                'url has been free typed (hacked)
                Response.Redirect("/richadmin/")
            End If

        End If

    End Sub

    Public Sub BindModuleConfigTypeList(ByVal ModuleTypeID As Integer)
        Dim dtModuleConfigType As DataTable = ModuleDAL.GetModuleConfigTypeList_ByModuleTypeID(ModuleTypeID)
        For Each drModuleConfigType As DataRow In dtModuleConfigType.Rows
            Dim intConfigurationID As String = drModuleConfigType("ID").ToString()
            Dim strConfigurationFieldText As String = drModuleConfigType("fieldText").ToString()
            Dim liOption As New ListItem(strConfigurationFieldText, intConfigurationID)
            cblModuleConfigTypeList.Items.Add(liOption)
        Next


    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Page.IsValid Then
            'Update Module
            If ModuleTypeID > 0 Then

                'First Delete all ModuleConfigs for this site
                ModuleDAL.DeleteModuleConfig_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteID)

                'Now update our Module Content
                Dim strModuleHtmlContent As String = radModuleContentHTML.Content
                ModuleDAL.UpdateModuleContent_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteID, strModuleHtmlContent)

                'Update Module_Config rows
                For Each liModuleConfig As ListItem In cblModuleConfigTypeList.Items
                    If liModuleConfig.Selected Then
                        Dim intModuleConfigTypeID As Integer = Convert.ToInt32(liModuleConfig.Value)
                        ModuleDAL.InsertModuleConfig(intModuleConfigTypeID, SiteID)
                    End If
                Next

                'Add module banner image if it exists
                If RadUploadModuleBannerImage.UploadedFiles.Count > 0 Then
                    Dim file As UploadedFile = RadUploadModuleBannerImage.UploadedFiles(0)
                    Dim strModuleBannerName As String = file.GetName
                    Dim bytesModuleBannerImage(file.InputStream.Length - 1) As Byte
                    file.InputStream.Read(bytesModuleBannerImage, 0, file.InputStream.Length)

                    ModuleDAL.UpdateModuleBannerImage_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteID, strModuleBannerName, bytesModuleBannerImage)

                End If

                Dim strModuleLocation_Admin As String = hdnModuleLocation.Value
                Response.Redirect(strModuleLocation_Admin)

            Else
                'Add a new Module is Not Available, getting here is impossible, but my be required later on in future, for now, if it happens redirect them to the login page
                Response.Redirect("/richadmin/")
            End If

        End If
    End Sub

#Region "Event Image"
    Protected Sub lnkDeleteImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteImage.Click
        Dim bytesModuleBannerImage() As Byte

        ModuleDAL.UpdateModuleBannerImage_ByModuleTypeIDAndSiteID(ModuleTypeID, SiteID, String.Empty, bytesModuleBannerImage)

        'Hide the module banner image and the delete link
        moduleBannerImage.Visible = False
        lnkDeleteImage.Visible = False
    End Sub

    Protected Sub customValModuleBannerImageSizeExceeded_Validate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        'Add module banner image if it exists, must have a file size less than 100kb, but we give them a 5kb buffer
        If RadUploadModuleBannerImage.UploadedFiles.Count > 0 Then
            For Each file As UploadedFile In RadUploadModuleBannerImage.UploadedFiles
                If file.InputStream.Length > 112400 Then
                    'Invalid Submission - so set the IsValid flag to false and show the tab that caused the validation error
                    e.IsValid = False

                End If
            Next
        End If
    End Sub

#End Region
End Class
