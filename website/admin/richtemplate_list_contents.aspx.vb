Imports System.Data.SqlClient
Imports System.Data
Imports Telerik.Web.UI
Imports System.Drawing
Imports System.IO

Partial Class admin_richtemplate_list_contents
    Inherits RichTemplateLanguagePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Check thes user exists
        Dim intAdminUserID As Integer = AdminUserDAL.GetCurrentAdminUserID()
        If intAdminUserID > 0 Then

            Dim strIntialSelection As String = ""
            If Not Request.QueryString("sel") Is Nothing Then
                strIntialSelection = Request.QueryString("sel").ToUpper()
            End If

            'Find out what the user can access add it to the content table then bind to grid

            Dim dtContents As New DataTable()
            Dim dcContentName As New DataColumn("ContentName", GetType(String))
            Dim dcContentIsSelected As New DataColumn("ContentIsSelected", GetType(Boolean))
            Dim dcUrlMain As New DataColumn("UrlMain", GetType(String))
            Dim dcUrlNav As New DataColumn("UrlNav", GetType(String))
            Dim dcImage As New DataColumn("Image", GetType(String))
            Dim dcImageSelected As New DataColumn("ImageSelected", GetType(String))

            dtContents.Columns.AddRange(New DataColumn() {dcContentName, dcContentIsSelected, dcUrlMain, dcUrlNav, dcImage, dcImageSelected})

            Dim boolAllowWebContent As Boolean = AdminUserDAL.GetCurrentAdminUserAllowWebContent()
            If boolAllowWebContent Then

                'Load Web Site Content
                Dim drContent_Website As DataRow = dtContents.NewRow()
                drContent_Website("ContentName") = Resources.RichTemplate_List_Contents.RichTemplate_List_Contents_WebsiteContent
                drContent_Website("UrlMain") = "/admin/richtemplate_welcome.aspx?mode=forms"
                drContent_Website("UrlNav") = "/admin/richtemplate_list_sections.aspx"
                drContent_Website("Image") = "/admin/images/content_website.gif"
                drContent_Website("ImageSelected") = "/admin/images/content_website_on.gif"
                drContent_Website("ContentIsSelected") = If(strIntialSelection = "" Or strIntialSelection = "WEBSITE", True, False) 'Initally select website content, only if we have no initial selection or we specifically want to set this via query string

                dtContents.Rows.Add(drContent_Website)

                Dim drContent_MembersOnly As DataRow = dtContents.NewRow()
                drContent_MembersOnly("ContentName") = Resources.RichTemplate_List_Contents.RichTemplate_List_Contents_MembersOnlyContent
                drContent_MembersOnly("UrlMain") = "/admin/richtemplate_welcome.aspx?mode=members"
                drContent_MembersOnly("UrlNav") = "/admin/richtemplate_list_sections.aspx?secure_members=yes"
                drContent_MembersOnly("Image") = "/admin/images/content_members_only.gif"
                drContent_MembersOnly("ImageSelected") = "/admin/images/content_members_only_on.gif"
                drContent_MembersOnly("ContentIsSelected") = If(strIntialSelection = "MEMBER", True, False)

                'DO NOT SHOW THE MEMBERS SECTION, as this is an intranet site, they are first sent to the /login/default.aspx to login, once logged in they only have the PUBLIC SITE TO VIEW
                'dtContents.Rows.Add(drContent_MembersOnly)

                Dim drContent_Education As DataRow = dtContents.NewRow()
                drContent_Education("ContentName") = Resources.RichTemplate_List_Contents.RichTemplate_List_Contents_EducationContent
                drContent_Education("UrlMain") = "/admin/richtemplate_welcome.aspx?mode=education"
                drContent_Education("UrlNav") = "/admin/richtemplate_list_sections.aspx?secure_education=yes"
                drContent_Education("Image") = "/admin/images/content_education.gif"
                drContent_Education("ImageSelected") = "/admin/images/content_education_on.gif"
                drContent_Education("ContentIsSelected") = If(strIntialSelection = "EDUCATION", True, False)

                'dtContents.Rows.Add(drContent_Education)
            End If

            Dim strAllowModules As String = AdminUserDAL.GetCurrentAdminUserAllowModules()
            If strAllowModules.Length() > 0 Then
                Dim drContent_Module As DataRow = dtContents.NewRow()
                drContent_Module("ContentName") = Resources.RichTemplate_List_Contents.RichTemplate_List_Contents_WebsiteModules
                drContent_Module("UrlMain") = "/admin/richtemplate_welcome.aspx?mode=modules"
                drContent_Module("UrlNav") = "/admin/richtemplate_list_modules.aspx"
                drContent_Module("Image") = "/admin/images/website_modules.gif"
                drContent_Module("ImageSelected") = "/admin/images/website_modules_on.gif"
                drContent_Module("ContentIsSelected") = If((Not boolAllowWebContent And strIntialSelection = "") Or strIntialSelection = "MODULES", True, False) 'Initally select module content, only if (we do not allow showing website content and no initial selection) OR we specifically want to set this via query string

                dtContents.Rows.Add(drContent_Module)
            End If

            'Get admin user access level
            Dim intAccessLevel = AdminUserDAL.GetCurrentAdminUserAccessLevel()

            'Only give the ability to manage users and email if the user has an access level > 2  'e.g. is either a Domain Administrator or the Master Administrator
            If intAccessLevel > 1 Then

                'Load Image Document Library
                Dim drContent_ImageDocumentLibrary As DataRow = dtContents.NewRow()
                drContent_ImageDocumentLibrary("ContentName") = Resources.RichTemplate_List_Contents.RichTemplate_List_Contents_ImageDocumentLibrary
                drContent_ImageDocumentLibrary("UrlMain") = "/admin/richtemplate_welcome.aspx?mode=images"
                drContent_ImageDocumentLibrary("UrlNav") = "/ig41sub/include/menunew.asp"
                drContent_ImageDocumentLibrary("Image") = "/admin/images/image_document_library.gif"
                drContent_ImageDocumentLibrary("ImageSelected") = "/admin/images/image_document_library_on.gif"
                drContent_ImageDocumentLibrary("ContentIsSelected") = If((Not strAllowModules.Length() > 0 And Not boolAllowWebContent And strIntialSelection = "") Or strIntialSelection = "IMAGES", True, False) 'Initally select images content, only if (we do not allow showing website content AND we do not allow any modules and no initial selection) OR we specifically want to set this via query string
                'dtContents.Rows.Add(drContent_ImageDocumentLibrary)

                'Load Administer Website menu
                Dim drContent_WebsiteAdministration As DataRow = dtContents.NewRow()
                drContent_WebsiteAdministration("ContentName") = Resources.RichTemplate_List_Contents.RichTemplate_List_Contents_AdministerWebSite
                drContent_WebsiteAdministration("UrlMain") = "/admin/richtemplate_welcome.aspx?mode=administration"
                drContent_WebsiteAdministration("UrlNav") = "/admin/richtemplate_list_administration.aspx"
                drContent_WebsiteAdministration("Image") = "/admin/images/website_administration.gif"
                drContent_WebsiteAdministration("ImageSelected") = "/admin/images/website_administration_on.gif"
                drContent_WebsiteAdministration("ContentIsSelected") = If(strIntialSelection = "ADMINISTRATION", True, False)

                dtContents.Rows.Add(drContent_WebsiteAdministration)

            End If

            If dtContents.Rows.Count > 0 Then
                rptContent.DataSource = dtContents
                rptContent.DataBind()
                divContents.Visible = True
            Else
                divContents.Visible = False
            End If


        Else
            Response.Redirect("~/richadmin/")
        End If
    End Sub

    Private Sub dgContents_ItemDataBound(ByVal source As Object, ByVal e As RepeaterItemEventArgs) Handles rptContent.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim drvContent As DataRowView = e.Item.DataItem
            Dim drContent As DataRow = drvContent.Row

            Dim strContentName As String = drContent("ContentName")
            Dim strUrlMain As String = drContent("UrlMain")
            Dim strUrlNav As String = drContent("UrlNav")
            Dim strImage As String = drContent("image")
            Dim strImageSelected As String = drContent("imageSelected")

            'Get and set the link button
            Dim lnkContent As LinkButton = e.Item.FindControl("lnkContent")
            lnkContent.CommandArgument = strUrlMain & ";" & strUrlNav

            'Get and set the contentDiv with the correct background image
            Dim divContent As HtmlGenericControl = e.Item.FindControl("divContent")
            Dim divContentSelected As HtmlGenericControl = e.Item.FindControl("divContentSelected")

            'Get and set the Literal with the content name
            Dim litContentName As Literal = e.Item.FindControl("litContentName")
            litContentName.Text = strContentName

            Dim litContentNameSelected As Literal = e.Item.FindControl("litContentNameSelected")
            litContentNameSelected.Text = strContentName

            'Decide we would should initally show the first content as selected
            Dim boolContentIsSelected As Boolean = Convert.ToBoolean(drContent("ContentIsSelected"))
            If Not IsPostBack And boolContentIsSelected Then
                divContent.Style.Add("background-image", strImage)
                divContent.Visible = False

                divContentSelected.Style.Add("background-image", strImageSelected)
                divContentSelected.Visible = True
            Else
                divContent.Style.Add("background-image", strImage)
                divContent.Visible = True

                divContentSelected.Style.Add("background-image", strImageSelected)
                divContentSelected.Visible = False
            End If

        End If
    End Sub


    Protected Sub lnkContent_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkContent As LinkButton = sender

        Dim divContent As HtmlGenericControl = lnkContent.FindControl("divContent")
        Dim divContentSelected As HtmlGenericControl = lnkContent.FindControl("divContentSelected")

        'Hide the image And show the selected image
        divContent.Visible = False
        divContentSelected.Visible = True

        'Redirect to the appropriate url
        Dim strUrl As String() = lnkContent.CommandArgument.Split(";")
        CommonWeb.JavaScriptRedirect_UpdateFrames(Me.Page, strUrl(0), strUrl(1), String.Empty, String.Empty)

    End Sub

End Class
