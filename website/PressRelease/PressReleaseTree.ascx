<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PressReleaseTree.ascx.vb" Inherits="PressRelease_PressReleaseTree" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="divCategoryList">
    <div class="categoryHeader">
        <%= Resources.PressRelease_FrontEnd.PressRelease_PressReleaseTree_CategoryListingLabel %></div>
    <asp:Panel id="pnlCategoryListPressRelease" runat="server">
        <telerik:RadTreeView ID="rtvPressRelease" runat="server"/>
    </asp:Panel>
</div>
