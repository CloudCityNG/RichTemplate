<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PollCategoryListingArchive.ascx.vb"
    Inherits="Poll_PollCategoryListingArchive" %>
<asp:Repeater ID="catRepeater" runat="server">
    <HeaderTemplate>
        <div class="categoryHeader">
            <%= Resources.Poll_FrontEnd.Poll_PollCategoryListingArchive_CategoryListingLabel%></div>
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
            <a class="cat_list" href="?catid=0&archive=1"><%= Resources.Poll_FrontEnd.Poll_PollCategoryListingArchive_UnCategorized%></a>
        </li>
        <li>
            <a class="cat_list" href="default.aspx?archive=1"><%= Resources.Poll_FrontEnd.Poll_PollCategoryListingArchive_ShowAll%></a>
        </li>
        </ul>
        <br class="clear" />
    </FooterTemplate>
</asp:Repeater>
