<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LinkDetail.ascx.vb" Inherits="link_LinkDetail" %>
<div style="float: right">
    <a href="Default.aspx">Return to link listings</a>
</div>
<b>
    <asp:Literal ID="lit_LinkCategory" runat="server" /></b><br />
<br />
<div id="divLink_Image" runat="server" class="logoBox" visible="false">
    <a id="aLink_Image" runat="server">
        <telerik:RadBinaryImage ID="radBinaryImage" runat="server" />
    </a>
    <br />
    <asp:Literal ID="lit_LinkDescription_Image" runat="server" />
</div>
<div id="divLink_NoImage" runat="server" visible="false">
    <a id="aLink_NoImage" runat="server">
        <asp:Literal ID="lit_LinkName" runat="server" /></a>
    <br />
    <asp:Literal ID="lit_LinkDescription_NoImage" runat="server" />
</div>
<br />
<br />
<asp:PlaceHolder ID="addThisPlaceholder" runat="server" Visible="false">
    <!-- AddThis Button BEGIN -->
    <div class="addthis_toolbox addthis_default_style">
        <a href="http://www.addthis.com/bookmark.php" class="addthis_button" style="text-decoration: none"
            addthis:url="http://<%=Request.ServerVariables("http_host") %><%=Request.Path.toString() %>?id=<%#Eval("linkID") %>">
            <img src="http://s7.addthis.com/static/btn/v2/lg-bookmark-en.gif" width="125" height="16"
                border="0" alt="Share"></a>
    </div>

    <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=xa-4bbf57a32e8aa403"></script>

    <!-- AddThis Button END -->
</asp:PlaceHolder>
