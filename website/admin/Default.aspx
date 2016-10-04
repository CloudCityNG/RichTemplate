<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RichTemplate 2.0</title>
    <link rel="stylesheet" href="styles.css" type="text/css" />
    <link rel="stylesheet" href="/admin/css/RichTemplate.css" type="text/css" />
    <base target="_self" />
</head>
<%
    Dim boolAllowWebContent As Boolean = AdminUserDAL.GetCurrentAdminUserAllowWebContent()
    Dim strAllowModules As String = AdminUserDAL.GetCurrentAdminUserAllowModules()
%>
<frameset rows="50,100%" framespacing="1" border="0" frameborder="0">
	    <frame name="banner" scrolling="no" noresize src="/admin/richtemplate_top_row.aspx"/>
	    <frameset cols="200,100%,*,*,*,*,*,*">
		    <frameset rows="350,350,*,*,*">
                <% If boolAllowWebContent Then%>
                    <frame name="treeframe" target="main" src="/admin/richtemplate_list_sections.aspx" scrolling="auto" noresize>
                    
                <% ElseIf strAllowModules.Length > 0 Then %>
                    <frame name="treeframe" target="main" src="/admin/richtemplate_list_modules.aspx" scrolling="auto" noresize>
                <% Else%>
                    <frame name="treeframe" target="main" src="/admin/richtemplate_list_images.aspx" scrolling="auto" noresize>
                <% End If%>
                <frame name="contents" src="richtemplate_list_contents.aspx" scrolling="no" noresize target="_self">
		    </frameset>
            <% If boolAllowWebContent Then%>
                <frame name="basefrm" src="richtemplate_welcome.aspx?mode=forms" scrolling="yes" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">
            <% Else%>
                <frame name="basefrm" src="richtemplate_welcome.aspx?mode=modules" scrolling="yes" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">
            <% End If%>
        </frameset>
    </frameset>
<noframes>
    <body>
        <h3>
            <span class="leftPad"><%= Resources.Default_Admin.Default_Admin_FramesNotSupported %></span>
        </h3>
    </body>
</noframes>
</html>
