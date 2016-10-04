<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SearchResults.ascx.vb"
    Inherits="searchTags_SearchResults" %>
<asp:Literal ID="litResults" runat="server" Visible="false" />
<div class="divSearchResults">
    <telerik:RadListView ID="rlvSearchResults" Width="97%" AllowPaging="True" runat="server"
        AllowSorting="true" ItemPlaceholderID="plcHolderSearchResults">
        <LayoutTemplate>
            <asp:Panel ID="plcHolderSearchResults" runat="server" Width="100%" />
            <table cellpadding="0" cellspacing="0" width="100%" style="clear: both;">
                <tr>
                    <td>
                        <br />
                        <telerik:RadDataPager ID="rdPagerSearchResults" runat="server" PagedControlID="rlvSearchResults"
                            PageSize="20">
                                <Fields>
                                    <telerik:RadDataPagerButtonField FieldType="FirstPrev" />
                                    <telerik:RadDataPagerButtonField FieldType="Numeric" />
                                    <telerik:RadDataPagerButtonField FieldType="NextLast" />
                                    <telerik:RadDataPagerPageSizeField PageSizeText="<%$ Resources:RadDataPager, Pager_PageSize %>" />
                                </Fields>
                        </telerik:RadDataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <div id="divModuleHeading" runat="server" class="moduleLargeTitle" visible="false">
                <br />
                <hr />
                <asp:Literal ID="litModuleHeading" runat="server" /><br />
                <hr />
            </div>
            <div class="Heading2">
                <a id="aHeading" runat="server">
                    <asp:Literal ID="litRecordHeading" runat="server" /></a>
            </div>
            <div id="divDate" runat="server" class="Date" visible="false">
                <%# Convert.ToDateTime(Eval("RecordViewDate")).ToString("d")%>
            </div>
            <br class="cBoth" />
            <br />
            <div>
                <asp:Literal ID="litRecordSummary" runat="server" />
            </div>
            <br />
            <div class="divModuleSearchTagList">
                <b><%=Resources.Search_FrontEnd.SearchTags_SearchResults_Module_SearchTagLabel%>:</b>
                <asp:Repeater ID="rptSearchTags" runat="server">
                    <ItemTemplate>
                        <span>
                            <%# Eval("searchTagName") %></a></span>
                    </ItemTemplate>
                    <SeparatorTemplate>
                        ,
                    </SeparatorTemplate>
                </asp:Repeater>
            </div>
            <br class="cBoth" />
        </ItemTemplate>
        <ItemSeparatorTemplate>
            <div class="divSearchSeperator">
                &nbsp;</div>
        </ItemSeparatorTemplate>
    </telerik:RadListView>
</div>
<br />
<br />
