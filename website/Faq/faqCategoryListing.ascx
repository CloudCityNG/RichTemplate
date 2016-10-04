<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FaqCategoryListing.ascx.vb"
    Inherits="Faq_FaqCategoryListing" %>
<asp:Repeater ID="catRepeater" runat="server">
    <HeaderTemplate>
        <div class="categoryHeader">
            <%= Resources.Faq_FrontEnd.Faq_FaqCategoryListing_CategoryListingLabel%></div>
        <ul class="ulCategoryList">
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <a class="cat_link" href="?catid=<%#(Eval("categoryID")) %>"><asp:Literal ID="litCategoryName"
                runat="server" Text='<%#(Eval("CategoryName"))%>' /> </a>
            <asp:PlaceHolder ID="rssFaq" runat="server" Visible="false">
                <a class="rss" href="/RssFeedGen.aspx?rss=faq&catid=<%#(Eval("categoryID")) %>">
                    <img src="/images/feed-icon-14x14.png" />
                </a>
            </asp:PlaceHolder>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        <li id="liUnCategorized" runat="server" visible="false">
            <a class="cat_link" href="?catid=0"><%= Resources.Faq_FrontEnd.Faq_FaqCategoryListing_UnCategorized%></a>
            <asp:PlaceHolder ID="rssUnCategorized" runat="server" Visible="false">
                <a class="rss" href="/RssFeedGen.aspx?rss=faq&catid=0">
                    <img src="/images/feed-icon-14x14.png" />
                </a>
            </asp:PlaceHolder>
        </li>
        <li>
            <a class="cat_link" href="default.aspx"><%= Resources.Faq_FrontEnd.Faq_FaqCategoryListing_ShowAll%></a>
            <asp:PlaceHolder ID="rssShowAll" runat="server" Visible="false">
                <a class="rss" href="/RssFeedGen.aspx?rss=faq">
                    <img src="/images/feed-icon-14x14.png" />
                </a>
            </asp:PlaceHolder>
        </li>
        </ul>
        <br class="clear" />
    </FooterTemplate>
</asp:Repeater>
