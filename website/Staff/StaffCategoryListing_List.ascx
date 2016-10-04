<%@ Control Language="VB" AutoEventWireup="false" CodeFile="StaffCategoryListing_List.ascx.vb"
    Inherits="staff_StaffCategoryListing_List" %>
<div class="divCategoryList floatL">
    <asp:Repeater ID="catRepeater" runat="server">
        <HeaderTemplate>
            <div class="categoryHeader">
                <%= Resources.Staff_FrontEnd.Staff_StaffCategoryListing_List_CategoryListingLabel%></div>
            <ul class="ulCategoryList">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <a class="cat_link" href="?catid=<%#(Eval("categoryID")) %>"><asp:Literal ID="litCategoryName"
                    runat="server" Text='<%#(Eval("CategoryName"))%>' /></a>
                <asp:PlaceHolder ID="rssStaff" runat="server" Visible="false">
                    <a class="rss" href="/RssFeedGen.aspx?rss=staff&catid=<%#(Eval("categoryID")) %>">
                        <img src="/images/feed-icon-14x14.png" /></a>
                </asp:PlaceHolder>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            <li id="liUncategorized" runat="server" visible="false">
                <a class="cat_link" href="?catid=0"><%= Resources.Staff_FrontEnd.Staff_StaffCategoryListing_List_Uncategorized%></a>
                <asp:PlaceHolder ID="rssUncategorized" runat="server" Visible="false">
                    <a class="rss" href="/RssFeedGen.aspx?rss=staff&catid=0">
                        <img src="/images/feed-icon-14x14.png" /></a>
                </asp:PlaceHolder>
            </li>
            <li>
                <a class="cat_link" href="default.aspx"><%= Resources.Staff_FrontEnd.Staff_StaffCategoryListing_List_ShowAll%></a>
                <asp:PlaceHolder ID="rssShowAll" runat="server" Visible="false">
                    <a class="rss" href="/RssFeedGen.aspx?rss=staff">
                        <img src="/images/feed-icon-14x14.png" /></a>
                </asp:PlaceHolder>
            </li>
            </ul>
            <br class="clear" />
        </FooterTemplate>
    </asp:Repeater>
</div>
