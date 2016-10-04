<%@ Page Language="VB" AutoEventWireup="false" CodeFile="richtemplate_top_row.aspx.vb"
    Inherits="admin_richtemplate_top_row" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="styles.css" type="text/css" />
    <link rel="stylesheet" href="/admin/css/RichTemplate.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="divAdminDefault">
            <div id="divRichTemplateLite" runat="server" visible="false" class="fLeft">
                <img border="0" src="/admin/images/richtemplate_header_lite.jpg" width="506" height="50"
                    alt="richtemplate header" />
            </div>
            <div id="divRichTemplateGold" runat="server" visible="false" class="fLeft">
                <img border="0" src="/admin/images/richtemplate_header_gold.jpg" width="506" height="50"
                    alt="richtemplate header" />
            </div>
            <div id="divRichTemplatePlatinum" runat="server" visible="false" class="fLeft">
                <img border="0" src="/admin/images/richtemplate_header_platinum.jpg" width="506" height="50"
                    alt="richtemplate header" />
            </div>
            <div id="divRichTemplateDefault" runat="server" visible="false" class="fLeft">
                <img border="0" src="/admin/images/richtemplate_header.jpg" width="506" height="50"
                    alt="richtemplate header" />
            </div>
            <div id="divSelectSite" runat="server" visible="false" class="divSelectSite">
                <%= Resources.RichTemplate_Top_Row.RichTemplate_Top_Row_CurrentSite %>: <asp:DropDownList ID="ddlSelectSite" runat="server" AutoPostBack="true" />
            </div>
            <br class="cBoth" />
        </div>
    </div>
    </form>
</body>
</html>
