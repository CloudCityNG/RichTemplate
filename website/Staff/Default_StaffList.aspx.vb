Imports System.Data

Partial Class staff_Default_StaffList
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 12

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        If Not Page.IsPostBack Then

            'Set the Page Title
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Staff_FrontEnd.Staff_DefaultList_HeaderTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Staff_FrontEnd.Staff_DefaultList_Heading)

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
                    'We do not have an Active Staff Module For the Front-End, so redirect to the homepage
                    Response.Redirect("/")
                End If
            End If

            'Check we need to show the staff member archive
            Dim boolAllowArchive As Boolean = False
            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()

                If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                    boolAllowArchive = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "enable_rss" Then
                    divModuleHeadingRssFeed.Visible = True
                End If
            Next

            If boolAllowArchive Then

                'Sets up or active/archived picker
                divActiveArchive.Visible = True
                If Request.QueryString("archive") = 1 Then
                    aStaff_Active.Visible = True
                    litStaff_Active.Visible = False

                    aStaff_Archive.Visible = False
                    litStaff_Archive.Visible = True
                Else
                    aStaff_Active.Visible = False
                    litStaff_Active.Visible = True

                    aStaff_Archive.Visible = True
                    litStaff_Archive.Visible = False
                End If
            End If

            If Request.QueryString("id") <> "" Then


                ucStaffDetail.Visible = True
                ucStaffRepeater.Visible = False
                ucStaffCatListing.Visible = False
                ucStaffCatListingArchive.Visible = False

                divStaffList.Visible = False

                divActiveArchive.Visible = False

            ElseIf Request.QueryString("archive") = 1 Then
                If boolAllowArchive Then
                    ucStaffCatListing.Visible = False
                    ucStaffCatListingArchive.Visible = True
                Else
                    ucStaffCatListingArchive.Visible = False
                    ucStaffRepeater.Visible = False
                End If
            End If

        End If

    End Sub




End Class
