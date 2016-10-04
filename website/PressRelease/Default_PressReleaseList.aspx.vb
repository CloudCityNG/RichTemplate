Imports System.Data

Partial Class PressRelease_Default_PressReleaseList
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 10

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        If Not Page.IsPostBack Then

            'Set the Page Title
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.PressRelease_FrontEnd.PressRelease_DefaultList_HeaderTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.PressRelease_FrontEnd.PressRelease_DefaultList_Heading)

            If Request.QueryString("id") Is Nothing Then
                'Get module information, to check if we will show its introduction
                Dim dtModule As DataTable = ModuleDAL.GetModule_ByModuleTypeIDAndSiteID_FrontEnd(ModuleTypeID, intSiteID)
                If dtModule.Rows.Count > 0 Then
                    Dim drModule As DataRow = dtModule.Rows(0)
                    If Not drModule("ModuleContentHTML") Is DBNull.Value Then
                        Dim strModuleContentHTML As String = drModule("ModuleContentHTML")

                        litModuleDynamicContent.Text = strModuleContentHTML
                        divModuleDynamicContent.Visible = True
                    End If
                Else
                    'We do not have an Active Press Release Module For the Front-End, so redirect to the homepage
                    Response.Redirect("/")
                End If
            End If

            'Check we need to show the press release archive
            Dim boolAllowArchive As Boolean = False
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()

                If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                    boolAllowArchive = True

                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_submissions" Then
                    If intMemberID > 0 Then
                        divAddPressRelease.Visible = True
                    End If
                End If
            Next

            If boolAllowArchive Then
                ' Code to hide archive if we decide to add archive press releases to this display
                ucPressReleaseCatListingArchive.Visible = True
            Else
                If Request.QueryString("archive") = 1 Then
                    ucPressReleaseCatListingArchive.Visible = False
                    ucPressReleaseRepeater.Visible = False
                    ucPressReleaseDetail.Visible = False
                End If
            End If
        End If
        If Not Request.QueryString("id") Is Nothing Then
            ucPressReleaseRepeater.Visible = False
            ucPressReleaseDetail.Visible = True
        End If

    End Sub

End Class
