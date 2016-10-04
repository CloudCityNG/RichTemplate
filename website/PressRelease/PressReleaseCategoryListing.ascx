<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PressReleaseCategoryListing.ascx.vb"
    Inherits="PressRelease_PressReleaseCategoryListing" %>
<asp:Repeater ID="catRepeater" runat="server">
    <HeaderTemplate>
        <div class="categoryHeader">
            <%= Resources.PressRelease_FrontEnd.PressRelease_PressReleaseCategoryListing_CategoryListingLabel%></div>
        <ul class="ulCategoryList">
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <a class="cat_link" href="?catid=<%#(Eval("categoryID")) %>"><asp:Literal ID="litCategoryName"
                runat="server" Text='<%#(Eval("CategoryName"))%>' /></a>
            <asp:PlaceHolder ID="rssPressRelease" runat="server" Visible="false">
                <a class="rss" href="/RssFeedGen.aspx?rss=pressrelease&catid=<%#(Eval("categoryID")) %>">
                    <img src="/images/feed-icon-14x14.png" />
                </a>
            </asp:PlaceHolder>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        <li id="liUnCategorized" runat="server" visible="false">
            <a class="cat_link" href="?catid=0"><%= Resources.PressRelease_FrontEnd.PressRelease_PressReleaseCategoryListing_UnCategorized%></a>
            <asp:PlaceHolder ID="rssUnCategorized" runat="server" Visible="false">
                <a class="rss" href="/RssFeedGen.aspx?rss=pressrelease&catid=0">
                    <img src="/images/feed-icon-14x14.png" />
                </a>
            </asp:PlaceHolder>
        </li>
        <li>
            <a class="cat_link" href="default.aspx"><%= Resources.PressRelease_FrontEnd.PressRelease_PressReleaseCategoryListing_ShowAll%></a>
            <asp:PlaceHolder ID="rssShowAll" runat="server" Visible="false">
                <a class="rss" href="/RssFeedGen.aspx?rss=pressrelease">
                    <img src="/images/feed-icon-14x14.png" />
                </a>
            </asp:PlaceHolder>
        </li>
        </ul>
        <br class="clear" />
    </FooterTemplate>
</asp:Repeater>
