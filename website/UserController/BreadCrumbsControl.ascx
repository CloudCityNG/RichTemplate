<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BreadCrumbsControl.ascx.vb"
    Inherits="UserController_BreadCrumbsControl" %>
<div id="divBreadCrumbs" runat="server" class="divBreadCrumbs">
<asp:Repeater ID="rptBreadCrumbs" runat="server">
<ItemTemplate><a id="aBreadCrumbs" runat="server" href="#"><asp:Literal ID="litBreadCrumbs" runat="server" /></a></ItemTemplate>
<SeparatorTemplate> » </SeparatorTemplate>
</asp:Repeater>
</div>
