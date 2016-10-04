<%@ Page Language="VB" AutoEventWireup="false" CodeFile="richtemplate_welcome.aspx.vb"
    Inherits="admin_richtemplate_welcome" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="styles.css" type="text/css" />
    <link rel="stylesheet" href="/admin/css/RichTemplate.css" type="text/css" />
    <script type="text/javascript" src="/js/multiplePageLoadFunctions.js"></script>
    <script language="JavaScript" src="/js/jquery-1.4.2.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <uc:Header ID="ucHeader" runat="server" />
    <div class="divWelcomeFlash">
        <object classid="clsid:D27CDB6E-AE6D-11CF-96B8-444553540000" id="obj2" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0"
            border="0" width="410" height="198">
            <param name="movie" value="/admin/images/edit_modules.swf" />
            <param name="quality" value="High" />
            <embed src="images/edit_modules.swf" pluginspage="http://www.macromedia.com/go/getflashplayer"
                type="application/x-shockwave-flash" name="obj2" width="410" height="198"></object>
        <script type="text/javascript" src="/scripts/ieupdate.js"></script>
    </div>
    </form>
</body>
</html>
