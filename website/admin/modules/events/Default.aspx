<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_modules_event_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%=Resources.Event_Admin.Event_Default_BodyHeading%>
                </span>
                <br />
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/folder.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/categories/default.aspx?mtid=5">
                                <%=Resources.Event_Admin.Event_Default_ManageCategories%>
                            </a>
                        </td>
                        <td style="padding-left: 10px">
                            <img src="/admin/images/process.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/module/?mtid=5">
                                <%=Resources.Event_Admin.Event_Default_Configure%>
                            </a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadAjaxManager ID="ram_Event" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="rgEvents">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="rgEvents" />
                                <telerik:AjaxUpdatedControl ControlID="rgEventsArchive" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadTabStrip ID="rtsEvent" runat="server" MultiPageID="rmpEvents" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" PageViewID="rpvEvent" Value="0" Text="<%$ Resources:Event_Admin, Event_Default_ActiveRecords %>">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="rpvEventArchive" Value="1" Text="<%$ Resources:Event_Admin, Event_Default_ArchiveRecords%>">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="rmpEvents" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
                    <telerik:RadPageView ID="rpvEvent" runat="server">
                        <telerik:RadGrid ID="rgEvents" runat="server" PageSize="20">
                            <MasterTableView DataKeyNames="eventID" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="eventID" DataType="System.Int32" HeaderText="eventID"
                                        ReadOnly="True" SortExpression="eventID" UniqueName="eventID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="startDate" SortExpression="startDate" HeaderStyle-Wrap="false"
                                        HeaderText="<%$ Resources:Event_Admin, Event_Default_GridStartDate%>">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("startDate")).ToString("d")%><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Event_Admin.Event_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="endDate" DataType="System.DateTime" HeaderText="<%$ Resources:Event_Admin, Event_Default_GridEndDate%>"
                                        SortExpression="endDate" UniqueName="endDate" DataFormatString="{0:d}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:Event_Admin, Event_Default_GridCategory%>"
                                        SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Event_Admin, Event_Default_GridUncategorized%>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Title" HeaderText="<%$ Resources:Event_Admin, Event_Default_Grid_Title%>"
                                        SortExpression="Title" UniqueName="Title">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="appCount" HeaderText="appCount" SortExpression="appCount"
                                        UniqueName="appCount" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="applicants" DataNavigateUrlFields="eventID"
                                        DataNavigateUrlFormatString="Applicants.aspx?eventID={0}" HeaderText="<%$ Resources:Event_Admin, Event_Default_GridRegistration%>"
                                        SortExpression="appCount" Visible="false">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn DataField="commentCount" HeaderText="commentCount" SortExpression="commentCount"
                                        UniqueName="commentCount" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="comments" HeaderText="<%$ Resources:Event_Admin, Event_Default_GridComments%>"
                                        ItemStyle-Wrap="false" SortExpression="commentCount" Visible="false">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridTemplateColumn UniqueName="editEvent" DataField="eventID" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" HeaderText="<%$ Resources:Event_Admin, Event_Default_GridEdit%>">
                                        <ItemTemplate>
                                            <a id="aEventEdit" runat="server">
                                                <%=Resources.Event_Admin.Event_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Event_Admin, Event_Default_GridDelete%>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="editAdd.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                            <%=Resources.Event_Admin.Event_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites1" runat="server" Text="<%$ Resources:Event_Admin, Event_Default_Key_AvailableToAllSites %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteLive" runat="server" OnClick="btnDeleteLive_Click" Text="<%$ Resources:Event_Admin, Event_Default_GridDeleteButton%>" /></div>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvEventArchive" runat="server">
                        <telerik:RadGrid ID="rgEventsArchive" runat="server" PageSize="20">
                            <MasterTableView DataKeyNames="eventID" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="eventID" DataType="System.Int32" HeaderText="eventID"
                                        ReadOnly="True" SortExpression="eventID" UniqueName="eventID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="startDate" SortExpression="startDate" HeaderStyle-Wrap="false"
                                        HeaderText="<%$ Resources:Event_Admin, Event_Default_GridStartDate%>">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("startDate")).ToString("d")%><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Event_Admin.Event_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="endDate" DataType="System.DateTime" HeaderText="<%$ Resources:Event_Admin, Event_Default_GridEndDate%>"
                                        SortExpression="endDate" UniqueName="endDate" DataFormatString="{0:d}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:Event_Admin, Event_Default_GridCategory%>"
                                        SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Event_Admin, Event_Default_GridUncategorized%>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="Title" SortExpression="Title" HeaderText="<%$ Resources:Event_Admin, Event_Default_Grid_Title%>">
                                        <ItemTemplate>
                                            <%# Eval("Title") & If(Eval("ExpirationDate") IsNot DBNull.Value AndAlso Convert.ToDateTime(Eval("ExpirationDate")) < DateTime.Now, "<span class='gridImg' title='" & Resources.Event_Admin.Event_Default_Grid_Expired & "'><img src='/admin/images/expired.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="appCount" HeaderText="appCount" SortExpression="appCount"
                                        UniqueName="appCount" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="applicants" DataNavigateUrlFields="eventID"
                                        DataNavigateUrlFormatString="Applicants.aspx?eventID={0}" HeaderText="<%$ Resources:Event_Admin, Event_Default_GridRegistration%>"
                                        SortExpression="appCount" Visible="false">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn DataField="commentCount" HeaderText="commentCount" SortExpression="commentCount"
                                        UniqueName="commentCount" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="comments" HeaderText="<%$ Resources:Event_Admin, Event_Default_GridComments%>"
                                        ItemStyle-Wrap="false" SortExpression="commentCount" Visible="false">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridTemplateColumn UniqueName="editEvent" DataField="eventID" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" HeaderText="<%$ Resources:Event_Admin, Event_Default_GridEdit%>">
                                        <ItemTemplate>
                                            <a id="aEventEdit" runat="server">
                                                <%=Resources.Event_Admin.Event_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Event_Admin, Event_Default_GridDelete%>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="editAdd.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" /><%=Resources.Event_Admin.Event_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites2" runat="server" Text="<%$ Resources:Event_Admin, Event_Default_Key_AvailableToAllSites %>" />
                                </div>
                                <br class="cBoth" />
                                <div class='footerIcon'>
                                    <img src='/admin/images/expired.png' />
                                    <asp:Literal ID="litKeyExpiredMessage" runat="server" Text="<%$ Resources:Event_Admin, Event_Default_Key_Expired %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteArchive" runat="server" Text="<%$ Resources:Event_Admin, Event_Default_GridDeleteButton%>"
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
