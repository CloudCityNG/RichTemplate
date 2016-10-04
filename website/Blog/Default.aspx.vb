Imports System.Data
Imports System.Data.SqlClient

Partial Class Blog_Default_BlogTree
    Inherits RichTemplateLanguagePage

    Dim ModuleTypeID As Integer = 1

    Dim intSiteID As Integer = Integer.MinValue
    Dim intMemberID As Integer = Integer.MinValue

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'First we set the SiteID and current MemberID
        intSiteID = SiteDAL.GetCurrentSiteID_FrontEnd()
        intMemberID = MemberDAL.GetCurrentMemberID()

        If Not IsPostBack Then

            'Set the Page Title
            Page.Header.Title = SiteDAL.GetCompanyName() + " | " + Resources.Blog_FrontEnd.Blog_DefaultTree_HeaderTitle
            CommonWeb.SetMasterPageBannerText(Me.Master, Resources.Blog_FrontEnd.Blog_DefaultTree_Heading)

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
                    'We do not have an Active Blog Module For the Front-End, so redirect to the homepage
                    Response.Redirect("/")
                End If
            End If


            'Check we need to show the comments, captcha and comment approval
            Dim boolAllowArchive As Boolean = False

            Dim dtModuleConfig As DataTable = ModuleDAL.GetModuleConfigList_ByModuleTypeIDAndSiteID(ModuleTypeID, intSiteID)
            For Each drModuleConfig As DataRow In dtModuleConfig.Rows()
                If drModuleConfig("fieldName").ToString().ToLower() = "show_archive" Then
                    boolAllowArchive = True
                ElseIf drModuleConfig("fieldName").ToString().ToLower() = "allow_online_submissions" Then
                    If intMemberID > 0 Then
                        divAddBlog.Visible = True
                    End If
                End If

            Next

            If boolAllowArchive Then
                ' Code to hide archive if we decide to add archive blogs to this display
                divActiveArchive.Visible = True

                If Request.QueryString("archive") = 1 Then
                    aBlog_Active.Visible = True
                    litBlog_Active.Visible = False

                    aBlog_Archive.Visible = False
                    litBlog_Archive.Visible = True

                Else
                    aBlog_Active.Visible = False
                    litBlog_Active.Visible = True

                    aBlog_Archive.Visible = True
                    litBlog_Archive.Visible = False
                End If
            Else
                If Request.QueryString("archive") = 1 Then
                    ucBlogTree.Visible = False
                    ucBlogTreeRepeater.Visible = False
                    ucBlogDetail.Visible = False
                End If
            End If

            If Not Request.QueryString("id") Is Nothing Then
                ucBlogTreeRepeater.Visible = False
                ucBlogDetail.Visible = True
            End If
        End If

    End Sub

End Class
