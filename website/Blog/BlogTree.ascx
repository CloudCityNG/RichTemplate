<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BlogTree.ascx.vb" Inherits="Blog_BlogTree" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="divCategoryList">
    <div class="categoryHeader">
        <%= Resources.Blog_FrontEnd.Blog_BlogTree_CategoryListingLabel%></div>
    <asp:Panel id="pnlCategoryListBlog" runat="server">
        <telerik:RadTreeView ID="rtvBlog" runat="server" />
    </asp:Panel>
</div>
