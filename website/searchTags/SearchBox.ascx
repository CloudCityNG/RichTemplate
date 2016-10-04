<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SearchBox.ascx.vb" Inherits="searchTags_SearchBox" %>
<div class="divSearchBox">
    <b><%= Resources.Search_FrontEnd.SearchTags_SearchBox_SubjectTags_Heading%>:</b>
    <br class="cBoth" />
    <asp:CheckBoxList ID="cblSearchTags" runat="server" CellSpacing="0"
        CellPadding="20" CssClass="cblSearchTags" RepeatDirection="Vertical" RepeatColumns="1" RepeatLayout="Flow">
    </asp:CheckBoxList>
    <br class="cBoth" />
    <br />
    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Search_FrontEnd, SearchTags_SearchBox_ButtonSearch %>" />
</div>
