<%@ Page Language="VB" AutoEventWireup="false" CodeFile="richtemplate_list_modules.aspx.vb"
    Inherits="admin_richtemplate_list_modules" %>

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
    <asp:UpdatePanel ID="upListPages" runat="server">
        <ContentTemplate>
            <telerik:RadScriptBlock runat="server" ID="scriptBlock">
            </telerik:RadScriptBlock>
            <div id="divListModules" class="divModules" runat="server">
                <div id="divModuleTree" runat="server" class="divModuleTree">
                    <div>
                        <img src="/images/open_folder_full_sm.png" class="rtImg rtImg_root img_link_left"><span class="inner_rtIn rt_root img_link_left" style="padding-top: 2px;">
                            <asp:LinkButton ID="lnkModules" runat="server"><font color="#3054A7"><b>&nbsp;<%= Resources.RichTemplate_List_Modules.RichTemplate_List_Modules_MyModules%></b></font></asp:LinkButton>
                            </span>
                        <br class="clear_both" />
                    </div>
                    <telerik:RadTreeView ID="RadTreeModules" runat="server" LoadingMessage="<%$ Resources:RichTemplate_List_Modules, RichTemplate_List_Modules_Grid_LoadingMessage %>" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
