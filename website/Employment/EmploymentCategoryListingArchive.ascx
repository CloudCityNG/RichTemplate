<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmploymentCategoryListingArchive.ascx.vb"
    Inherits="Employment_EmploymentCategoryListingArchive" %>
<asp:Repeater ID="catRepeater" runat="server">
    <HeaderTemplate>
        <div class="categoryHeader">
            <%= Resources.Employment_FrontEnd.Employment_EmploymentCategoryListingArchive_CategoryListingLabel%></div>
        <ul class="ulCategoryList">
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <a class="cat_list" href="?catid=<%#(Eval("categoryID")) %>&archive=1"><asp:Literal
                ID="litCategoryName" runat="server" Text='<%#(Eval("CategoryName"))%>' /></a>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        <li id="liUnCategorized" runat="server" visible="false">
            <a class="cat_list" href="?catid=0&archive=1"><%= Resources.Employment_FrontEnd.Employment_EmploymentCategoryListingArchive_UnCategorized%></a>
        </li>
        <li>
            <a class="cat_list" href="default.aspx?archive=1"><%= Resources.Employment_FrontEnd.Employment_EmploymentCategoryListingArchive_ShowAll%></a>
        </li>
        </ul>
        <br class="clear" />
    </FooterTemplate>
</asp:Repeater>
