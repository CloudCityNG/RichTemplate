<%@ Page Language="VB" AutoEventWireup="false" CodeFile="richtemplate_list_images.aspx.vb"
    Inherits="admin_richtemplate_list_images" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" href="styles.css" type="text/css" />
    <link rel="stylesheet" href="/admin/css/RichTemplate.css" type="text/css" />
    <link href="/Skins/RichTemplate/TreeView.RichTemplate.css" rel="stylesheet" type="text/css" />
</head>
<body class="nav_bg">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <asp:UpdatePanel ID="upListImages" runat="server">
        <contenttemplate>
            <telerik:RadScriptBlock runat="server" ID="scriptBlock">
            </telerik:RadScriptBlock>
            <div id="divListImages" class="divImages" runat="server">
                
            </div>
        </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
