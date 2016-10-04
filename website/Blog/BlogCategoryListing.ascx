<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BlogCategoryListing.ascx.vb"
    Inherits="Blog_BlogCategoryListing" %>
<asp:Repeater ID="catRepeater" runat="server">
    <HeaderTemplate>
        <div class="categoryHeader">
            <%= Resources.Blog_FrontEnd.Blog_BlogCategoryListing_CategoryListingLabel%></div>
        <ul class="ulCategoryList">
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <a class="cat_link" href="?catid=<%#(Eval("categoryID")) %>"><asp:Literal ID="litCategoryName"
                runat="server" Text='<%#(Eval("CategoryName"))%>' /></a>
            <asp:PlaceHolder ID="rssBlog" runat="server" Visible="false">
                <a class="rss" href="/RssFeedGen.aspx?rss=blog&catid=<%#(Eval("categoryID")) %>">
                    <img src="/images/feed-icon-14x14.png" /></a>
            </asp:PlaceHolder>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        <li id="liUncategorized" runat="server" visible="false">
            <a class="cat_link" href="?catid=0"><%= Resources.Blog_FrontEnd.Blog_BlogCategoryListing_UnCategorized%></a>
            <asp:PlaceHolder ID="rssUncategorized" runat="server" Visible="false">
                <a class="rss" href="/RssFeedGen.aspx?rss=blog&catid=0">
                    <img src="/images/feed-icon-14x14.png" /></a>
            </asp:PlaceHolder>
        </li>
        <li>
            <a class="cat_link" href="default.aspx"><%= Resources.Blog_FrontEnd.Blog_BlogCategoryListing_ShowAll%></a>
            <asp:PlaceHolder ID="rssShowAll" runat="server" Visible="false">
                <a class="rss" href="/RssFeedGen.aspx?rss=blog">
                    <img src="/images/feed-icon-14x14.png" /></a>
            </asp:PlaceHolder>
        </li>
        </ul>
        <br class="clear" />
    </FooterTemplate>
</asp:Repeater>
