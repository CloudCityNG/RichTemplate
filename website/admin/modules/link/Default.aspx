<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="admin_modules_link_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Manage Links
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">Below you can add, edit and delete Links.</span><br />
                <br />
                <table cellpadding="0" cellspacing="0" runat="server" >
                    <tr>
                        <td>
                            <img src="/admin/images/folder.png" />
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/admin/modules/categories/default.aspx?rp=/admin/modules/link/&mid=8">Manage Link Categories</asp:LinkButton>
                        </td>
                        <td style="padding-left: 10px">
                            <img src="/admin/images/process.png" alt="" />
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="Config.aspx">Configure Link Module</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="RadGrid1">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                <telerik:AjaxUpdatedControl ControlID="RadGrid2" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Live Records" PageViewID="RadPageView1" Selected="True"
                            Value="0">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Archived Records" PageViewID="RadPageView2"
                            Value="1" >
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
                    <telerik:RadPageView ID="RadPageView1" runat="server">
                        <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource1" PageSize="20">
                            <MasterTableView DataKeyNames="linkID" DataSourceID="SqlDataSource1" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="linkID" DataType="System.Int32" HeaderText="LinkID"
                                        ReadOnly="True" SortExpression="linkID" UniqueName="linkID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="dateTimeStamp" DataType="System.DateTime" HeaderText="Submission Date"
                                        SortExpression="dateTimeStamp" UniqueName="dateTimeStamp" DataFormatString="{0:d}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDropDownColumn DataSourceID="SqlDataSource3" ListTextField="CategoryName"
                                        ListValueField="categoryID" UniqueName="categoryName" SortExpression="categoryID"
                                        HeaderText="Category" DataField="categoryID" DropDownControlType="DropDownList"
                                        EnableEmptyListItem="true" EmptyListItemText="Uncategorized">
                                    </telerik:GridDropDownColumn>
                                    <telerik:GridBoundColumn DataField="linkName" HeaderText="Link" SortExpression="linkName"
                                        UniqueName="linkName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="linkUrl" HeaderText="Link URL" SortExpression="linkUrl"
                                        UniqueName="linkUrl">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="editPR" DataField="linkID" HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Edit</asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><img style="border:0px" alt="" src="/admin/images/AddRecord.gif" /> Add a new entry</asp:LinkButton>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div style="float: right; padding-top: 20px">
                            <asp:Button ID="LinkButton1" runat="server" Text="Delete" OnClick="btnDeleteLive_Click"
                                OnClientClick="return DeleteConfirmation();" /></div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <telerik:RadGrid ID="RadGrid2" runat="server" DataSourceID="SqlDataSource2" PageSize="20">
                            <MasterTableView DataKeyNames="linkID" DataSourceID="SqlDataSource2" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="linkID" DataType="System.Int32" HeaderText="linkID"
                                        ReadOnly="True" SortExpression="linkID" UniqueName="linkID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="dateTimeStamp" DataType="System.DateTime" HeaderText="Submission Date"
                                        SortExpression="dateTimeStamp" UniqueName="dateTimeStamp" DataFormatString="{0:d}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridDropDownColumn DataSourceID="SqlDataSource3" ListTextField="CategoryName"
                                        ListValueField="categoryID" UniqueName="CategoryName" SortExpression="categoryID"
                                        HeaderText="Category" DataField="categoryID" DropDownControlType="DropDownList"
                                        EnableEmptyListItem="true" EmptyListItemText="Uncategorized">
                                    </telerik:GridDropDownColumn>
                                    <telerik:GridBoundColumn DataField="linkName" HeaderText="Link" SortExpression="linkName"
                                        UniqueName="linkName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="linkURL" HeaderText="Link URL" SortExpression="linkURL"
                                        UniqueName="linkURL">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="editRecord" DataField="linkID" HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Edit</asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><img style="border:0px" alt="" src="/admin/images/AddRecord.gif" /> Add a new entry</asp:LinkButton>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div style="float: right; padding-top: 20px">
                            <asp:Button ID="Button2" runat="server" Text="Delete" OnClick="btnDeleteArchive_Click"
                                OnClientClick="return DeleteConfirmation();" /></div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="ss_Link_SelectList_ByStatus" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="Status" Type="Boolean" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="ss_Link_SelectList_ByStatus" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="False" Name="Status" Type="Boolean" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="ss_Category_SelectList_ByModuleTypeID" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="ModuleTypeID" Type="Int32" DefaultValue="8" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
