<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EventCategoryListing.ascx.vb"
    Inherits="Event_EventCategoryListing" %>
<asp:Repeater ID="catRepeater" runat="server">
    <HeaderTemplate>
        <div class="categoryHeader">
            <%= Resources.Event_FrontEnd.Event_EventCategoryListing_CategoryListingLabel%></div>
        <ul class="ulCategoryList">
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <a class="cat_link" href="?catid=<%#(Eval("categoryID")) %>"><asp:Literal ID="litCategoryName"
                runat="server" Text='<%#(Eval("CategoryName"))%>' /></a>
            <asp:PlaceHolder ID="rssEvent" runat="server" Visible="false">
                <a class="rss" href="/RssFeedGen.aspx?rss=event&catid=<%#(Eval("categoryID")) %>">
                    <img src="/images/feed-icon-14x14.png" />
                </a>
            </asp:PlaceHolder>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        <li id="liUnCategorized" runat="server" visible="false">
            <a class="cat_link" href="?catid=0"><%= Resources.Event_FrontEnd.Event_EventCategoryListing_UnCategorized%></a>
            <asp:PlaceHolder ID="rssUnCategorized" runat="server" Visible="false">
                <a class="rss" href="/RssFeedGen.aspx?rss=event&catid=0">
                    <img src="/images/feed-icon-14x14.png" /></a>
            </asp:PlaceHolder>
        </li>
        <li>
            <a class="cat_link" href="default.aspx"><%= Resources.Event_FrontEnd.Event_EventCategoryListing_ShowAll%></a>
            <asp:PlaceHolder ID="rssShowAll" runat="server" Visible="false">
                <a class="rss" href="/RssFeedGen.aspx?rss=event">
                    <img src="/images/feed-icon-14x14.png" /></a>
            </asp:PlaceHolder>
        </li>
        </ul>
        <br class="clear" />
    </FooterTemplate>
</asp:Repeater>
