<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SiteSelector.ascx.vb" Inherits="UserController_SiteSelector" %>
<div id="divSelectSite" runat="server" class="select_site">
<%= Resources.SiteSelector_UserControl.SiteSelector_SelectASite%>&nbsp;<asp:DropDownList ID="ddlSelectSite" runat="server" AutoPostBack="true" />
</div>
