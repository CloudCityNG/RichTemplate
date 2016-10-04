<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_modules_suggestion_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%= Resources.Suggestion_Admin.Suggestion_Default_BodyHeading%></span><br />
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/folder.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/categories/default.aspx?mtid=13">
                                <%= Resources.Suggestion_Admin.Suggestion_Default_ManageCategories%></a>
                        </td>
                        <td style="padding-left: 10px">
                            <img src="/admin/images/process.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/module/?mtid=13">
                                <%= Resources.Suggestion_Admin.Suggestion_Default_Configure%></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadAjaxManager ID="ramSuggestion" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="rgSuggestion">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="rgSuggestion" />
                                <telerik:AjaxUpdatedControl ControlID="rgSuggestionArchive" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadTabStrip ID="rtsSuggestion" runat="server" MultiPageID="rmpSuggestion"
                    SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="<%$ Resources:Suggestion_Admin, Suggestion_Default_ActiveRecords %>"
                            PageViewID="rpvSuggestion" Selected="True" Value="0">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="<%$ Resources:Suggestion_Admin, Suggestion_Default_ArchiveRecords %>"
                            PageViewID="rpvSuggestionArchive" Value="1">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="rmpSuggestion" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
                    <telerik:RadPageView ID="rpvSuggestion" runat="server">
                        <telerik:RadGrid ID="rgSuggestion" runat="server" PageSize="20">
                            <MasterTableView DataKeyNames="ID" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" ReadOnly="True" UniqueName="ID"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="DateTimeStamp" SortExpression="DateTimeStamp"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridDateTimeStamp%>">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("DateTimeStamp")).ToString("d")%><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Suggestion_Admin.Suggestion_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="ExpirationDate" DataType="System.DateTime" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridExpirationDate%>"
                                        SortExpression="ExpirationDate" UniqueName="ExpirationDate" DataFormatString="{0:d}"
                                        HeaderStyle-Wrap="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridCategory %>"
                                        SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridUncategorized %>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="memberUsername" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridMemberUsername %>"
                                        SortExpression="memberUsername" UniqueName="memberUsername" EmptyDataText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridMemberUsername_NotAvailable %>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Suggestion" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridSuggestion %>"
                                        UniqueName="Suggestion" MaxLength="50">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="editSuggestion" DataField="ID" ItemStyle-Width="50px"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridEdit %>">
                                        <ItemTemplate>
                                            <a id="aSuggestionEdit" runat="server">
                                                <%= Resources.Suggestion_Admin.Suggestion_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridDelete %>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="editAdd.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                            <%= Resources.Suggestion_Admin.Suggestion_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites1" runat="server" Text="<%$ Resources:Suggestion_Admin, Suggestion_Default_Key_AvailableToAllSites %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteLive" runat="server" Text="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridDeleteButton %>"
                                    OnClick="btnDeleteLive_Click" /></div>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvSuggestionArchive" runat="server">
                        <telerik:RadGrid ID="rgSuggestionArchive" runat="server" PageSize="20">
                            <MasterTableView DataKeyNames="ID" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" ReadOnly="True" UniqueName="ID"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="DateTimeStamp" SortExpression="DateTimeStamp"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridDateTimeStamp%>">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("DateTimeStamp")).ToString("d")%><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Suggestion_Admin.Suggestion_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="ExpirationDate" SortExpression="ExpirationDate"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridExpirationDate%>">
                                        <ItemTemplate>
                                            <%# If(Eval("ExpirationDate") IsNot DBNull.Value, Convert.ToDateTime(Eval("ExpirationDate")).ToString("d") & If(Convert.ToDateTime(Eval("ExpirationDate")) < DateTime.Now, "<span class='gridImg' title='" & Resources.Suggestion_Admin.Suggestion_Default_Grid_Expired & "'><img src='/admin/images/expired.png'/></span>", ""), "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridCategory %>"
                                        SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridUncategorized %>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="memberUsername" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridMemberUsername %>"
                                        SortExpression="memberUsername" UniqueName="memberUsername" EmptyDataText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridMemberUsername_NotAvailable %>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Suggestion" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridSuggestion %>"
                                        UniqueName="Suggestion" MaxLength="50">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="editSuggestion" DataField="ID" ItemStyle-Width="50px"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridEdit %>">
                                        <ItemTemplate>
                                            <a id="aSuggestionEdit" runat="server">
                                                <%= Resources.Suggestion_Admin.Suggestion_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridDelete %>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="editAdd.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                            <%= Resources.Suggestion_Admin.Suggestion_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites2" runat="server" Text="<%$ Resources:Suggestion_Admin, Suggestion_Default_Key_AvailableToAllSites %>" />
                                </div>
                                <br class="cBoth" />
                                <div class='footerIcon'>
                                    <img src='/admin/images/expired.png' />
                                    <asp:Literal ID="litKeyExpiredMessage" runat="server" Text="<%$ Resources:Suggestion_Admin, Suggestion_Default_Key_Expired %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteArchive" runat="server" Text="<%$ Resources:Suggestion_Admin, Suggestion_Default_GridDeleteButton %>"
                                    OnClick="btnDeleteArchive_Click" /></div>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
