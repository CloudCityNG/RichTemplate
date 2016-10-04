<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_modules_Location_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%= Resources.Location_Admin.Location_Default_BodyHeading%></span><br />
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/folder.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/categories/default.aspx?mtid=16">
                                <%= Resources.Location_Admin.Location_Default_ManageCategories%></a>
                        </td>
                        <td style="padding-left: 10px">
                            <img src="/admin/images/process.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/module/?mtid=16">
                                <%= Resources.Location_Admin.Location_Default_Configure%></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="rgLocation" runat="server" PageSize="20">
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
                            <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:Location_Admin, Location_Default_GridCategory %>"
                                SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Location_Admin, Location_Default_GridUncategorized %>">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Location" HeaderText="<%$ Resources:Location_Admin, Location_Default_GridLocation %>"
                                UniqueName="Location" MaxLength="50">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Address1" HeaderText="<%$ Resources:Location_Admin, Location_Default_GridAddress1 %>"
                                UniqueName="Address1" MaxLength="50">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="City" HeaderText="<%$ Resources:Location_Admin, Location_Default_GridCity %>"
                                UniqueName="City" MaxLength="50">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="State_Province" HeaderText="<%$ Resources:Location_Admin, Location_Default_GridState_Province %>"
                                UniqueName="State_Province" MaxLength="50">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Zip" HeaderText="<%$ Resources:Location_Admin, Location_Default_GridZip %>"
                                UniqueName="Zip" MaxLength="50">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="editLocation" DataField="ID" ItemStyle-Width="50px"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Location_Admin, Location_Default_GridEdit %>">
                                <ItemTemplate>
                                    <a id="aLocationEdit" runat="server">
                                        <%= Resources.Location_Admin.Location_Default_GridEdit%></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Location_Admin, Location_Default_GridDelete %>">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                <a href="editAdd.aspx">
                                    <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                    <%= Resources.Location_Admin.Location_Default_Grid_AddNewEntry%></a>
                            </div>
                        </CommandItemTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
                <div class="divFooter">
                    <div class="fRight">
                        <asp:Button ID="btnDeleteLive" runat="server" Text="<%$ Resources:Location_Admin, Location_Default_GridDeleteButton %>"
                            OnClick="btnDeleteLive_Click" /></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
