<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="Search.aspx.vb" Inherits="Search" %>

<%@ Register TagPrefix="cc" Namespace="Karamasoft.WebControls.UltimateSearch" Assembly="UltimateSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divSearchInput" runat="server" class="divSearchInput">
        <div class="divSearchText">
            <%= Resources.Search_FrontEnd.Search_SearchText%>:
        </div>
        <cc:UltimateSearchInput ID="ccUltimateSearchInput" runat="server" DisplaySuggest="False"
            SearchOutputPage="/Search.aspx" SetFocus="false">
            <SearchTextBox CssClass="txtSearch"></SearchTextBox>
            <SearchTypeList ID="ccSearchTypeList">
                <Items>
                    <asp:ListItem Selected="True" Value="0" Text="<%$ Resources:UltimateSearch, UltimateSearchInput_SearchType_AllWords %>" />
                    <asp:ListItem Value="1" Text="<%$ Resources:UltimateSearch, UltimateSearchInput_SearchType_AnyWord %>" />
                    <asp:ListItem Value="2" Text="<%$ Resources:UltimateSearch, UltimateSearchInput_SearchType_ExactPhrase %>" />
                    <asp:ListItem Value="3" Text="<%$ Resources:UltimateSearch, UltimateSearchInput_SearchType_PartialMatch %>" />
                </Items>
            </SearchTypeList>
            <SearchButton ButtonType="ImageButton" Text="<%$ Resources:UltimateSearch, UltimateSearchInput_ButtonSearch %>"
                CausesValidation="false" ImageUrl="/images/btn_search.gif" CssClass="btn_search">
            </SearchButton>
        </cc:UltimateSearchInput>
    </div>
    <div class="imgLinkDiv advSearch">
        <a href="/SearchTags/">
            <img src="/images/search_page.png"><span><%= Resources.Search_FrontEnd.Search_ClickHereForAdvancedSearch%></span></a></div>
    <br class="cBoth" />
    <br />
    <cc:UltimateSearchOutput ID="ucUltimateSearchOutput" runat="server" PageSize="20">
    </cc:UltimateSearchOutput><br />
</asp:Content>
