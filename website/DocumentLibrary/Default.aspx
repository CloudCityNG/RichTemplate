<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="documentLibrary_DocumentSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divAddDocument" runat="server" visible="false" class="moduleAddLink">
        <a href="SaveDocument.aspx">
            <img alt="add document" src="/images/AddRecord.gif" /></a>
        <a href="SaveDocument.aspx">
            <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_Default_Add%></a>
    </div>
    <div id="divModuleHeadingRssFeed" runat="server" class="docRss moduleTitleRss" visible="false">
        <a class="rss" href="/RssFeedGen.aspx?rss=documentlibrary">
            <img src="/images/feed-icon-14x14.png" />
        </a>
        <a class="rss" href="/RssFeedGen.aspx?rss=documentlibrary">
            <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_Default_RSS %>
        </a>
    </div>
    <div class="divModuleContent">
        <div class="divViewBy">
            <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_Default_ViewList %>&nbsp;|&nbsp;
            <a href="DocumentTree.aspx<%= IF(Request.Params("archive") = 1, "?archive=1","") %>">
                <b>
                    <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_Default_ViewTree%></b></a>
        </div>
        <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
            <asp:Literal ID="litModuleDynamicContent" runat="server" />
        </div>
        <asp:UpdatePanel ID="upDocumentLibrary" runat="server">
            <contenttemplate>
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="lnkGo" CssClass="documentSearch">
                    <div class="floatL">
                        <b><%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_Default_DocumentSearch_Title%></b><br />
                        <asp:TextBox ID="txtTitle" runat="server" Width="150px" Height="16px" />
                    </div>
                    <div class="floatL leftPad">
                        <b><%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_Default_DocumentSearch_Category%></b><br />
                        <asp:DropDownList ID="ddlCategories" runat="server" AppendDataBoundItems="true" Width="300px" Height="22px"/>
                    </div>
                    <div id="divActiveArchive" runat="server" visible="false" class="floatL leftPad">
                        <br />
                        <asp:RadioButton ID="rdActive" runat="server" Text="<%$ Resources:DocumentLibrary_FrontEnd, DocumentLibrary_Default_DocumentSearch_StatusActive %>" GroupName="rdActiveArchive" />
                        <asp:RadioButton ID="rdArchive" runat="server" Text="<%$ Resources:DocumentLibrary_FrontEnd, DocumentLibrary_Default_DocumentSearch_StatusArchive %>" GroupName="rdActiveArchive" />
                    </div>
                    <div class="floatL leftPadDbl docSearch">
                        <br />
                        <asp:LinkButton ID="lnkGo" runat="server"><img src="/images/search_go.gif" alt="search" /></asp:LinkButton></div>
                    <br class="cBoth" />
                    <br />
                </asp:Panel>
                <div class="docLibrary">
                    <telerik:RadGrid ID="rgDocuments" runat="server" PageSize="20">
                        <MasterTableView DataKeyNames="documentID" AllowNaturalSort="false" AllowMultiColumnSorting="true">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="documentID" DataType="System.Int32" HeaderText="documentID"
                                    ReadOnly="True" SortExpression="documentID" UniqueName="documentID" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="<%$ Resources:DocumentLibrary_FrontEnd, DocumentLibrary_Default_DocumentSearchResults_Date %>" UniqueName="viewDate" Reorderable="true"
                                    ShowSortIcon="true" SortExpression="viewDate" HeaderStyle-Width="50" ItemStyle-CssClass="docDownload">
                                    <ItemTemplate>
                                        <%#Convert.ToDateTime(Eval("viewDate")).ToString("d")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="<%$ Resources:DocumentLibrary_FrontEnd, DocumentLibrary_Default_DocumentSearchResults_Download %>" SortExpression="dl" Reorderable="true"
                                    ShowSortIcon="true" HeaderStyle-CssClass="rgHeader rgSorted" HeaderStyle-Width="20" ItemStyle-CssClass="docDownload">
                                    <ItemTemplate>
                                        <a href="downloadDocument.aspx?id=<%#Eval("documentID") %><%# IF(Request.Params("archive") = 1, "&archive=1","") %>" target='_blank'>
                                            <img src="/images/save.png" width="15" height="15" />
                                        </a>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="<%$ Resources:DocumentLibrary_FrontEnd, DocumentLibrary_Default_DocumentSearchResults_DocumentTitle %>" DataField="fileTitle"
                                    UniqueName="fileTitle" Reorderable="true" ShowSortIcon="true" SortExpression="fileTitle"
                                    HeaderStyle-Width="200" ItemStyle-CssClass="docDownload">
                                    <ItemTemplate>
                                        <a href="documentDetail.aspx?id=<%#Eval("documentID") %><%# IF(Request.Params("archive") = 1, "&archive=1","") %>">
                                            <%#Eval("fileTitle")%></b></a>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:DocumentLibrary_FrontEnd, DocumentLibrary_Default_DocumentSearchResults_Category %>" EmptyDataText="Uncategorized"
                                    SortExpression="categoryName" UniqueName="categoryName" HeaderStyle-CssClass="rgHeader rgSorted"
                                    HeaderStyle-Width="175" ItemStyle-CssClass="docDownload">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="<%$ Resources:DocumentLibrary_FrontEnd, DocumentLibrary_Default_DocumentSearchResults_AverageRating %>" DataField="aveRating"
                                    UniqueName="aveRating" Reorderable="true" ShowSortIcon="true" SortExpression="aveRating"
                                    HeaderStyle-Width="125" Visible="false">
                                    <ItemTemplate>
                                        <telerik:RadRating ID="docRating" runat="server" Precision="Half" Value='<%# Eval("aveRating") %>'
                                            OnRate="docRating_Rate" AutoPostBack="true" Height="25" />
                                        &nbsp;(<%# Eval("commentCount") %>&nbsp;<%# Resources.DocumentLibrary_FrontEnd.DocumentLibrary_Default_DocumentSearchResults_CommentCount & If(Convert.ToInt32(Eval("commentCount")) = 1, "", "s")%>)
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </contenttemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
