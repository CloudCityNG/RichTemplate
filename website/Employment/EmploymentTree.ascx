<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmploymentTree.ascx.vb" Inherits="Employment_EmploymentTree" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="divCategoryList">
    <div class="categoryHeader">
        <%= Resources.Employment_FrontEnd.Employment_EmploymentTree_CategoryListingLabel%></div>
    <asp:Panel id="pnlCategoryListEmployment" runat="server">
        <telerik:RadTreeView ID="rtvEmployment" runat="server"/>
    </asp:Panel>
</div>
