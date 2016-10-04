<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EventTree.ascx.vb" Inherits="Event_EventTree" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="divCategoryList">
    <div class="categoryHeader">
        <%= Resources.Event_FrontEnd.Event_EventTree_CategoryListingLabel%></div>
    <asp:Panel id="pnlCategoryListEvent" runat="server">
        <telerik:RadTreeView ID="rtvEvent" runat="server"/>
    </asp:Panel>
</div>
