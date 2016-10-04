<%@ Page Language="VB" AutoEventWireup="false" CodeFile="richtemplate_list_contents.aspx.vb"
    Inherits="admin_richtemplate_list_contents" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" href="styles.css" type="text/css" />
    <link rel="stylesheet" href="/admin/css/RichTemplate.css" type="text/css" />
    <link href="/Skins/RichTemplate/TreeView.RichTemplate.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: #c4dafa;">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <asp:UpdatePanel ID="upListPages" runat="server">
        <ContentTemplate>
            <telerik:RadScriptBlock runat="server" ID="scriptBlock">
            </telerik:RadScriptBlock>
            <div id="divContents" runat="server" visible="false">
                <table class="tblContents" width="100%" cellpadding="0" cellspacing="0" border="0">
                    <asp:Repeater ID="rptContent" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkContent" runat="server" OnClick="lnkContent_Click">
                                        <div id="divContent" runat="server" class="divRichTemplateContent" visible="false">
                                            <span class="spanContentName"><asp:Literal ID="litContentName" runat="server" /></span>
                                        </div>
                                        <div id="divContentSelected" runat="server" class="divRichTemplateContent" visible="false">
                                            <span class="spanContentName"><asp:Literal ID="litContentNameSelected" runat="server" /></span>
                                        </div>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
