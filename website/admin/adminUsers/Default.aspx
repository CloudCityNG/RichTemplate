<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    CodeFile="Default.aspx.vb" Inherits="admin_AdminUsers_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%= Resources.AdminUser_Admin.AdminUser_Default_BodyHeading%></span><br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="rgAdminUser" runat="server" PageSize="20">
                    <MasterTableView DataKeyNames="ID" CommandItemDisplay="Top">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" ReadOnly="True" SortExpression="ID"
                                UniqueName="ID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="user_image" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <img id="imgUser" runat="server" src="" border="0" width="16" height="16" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:AdminUser_Admin, AdminUser_Default_GridStatus %>"
                                UniqueName="status" SortExpression="status" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <asp:Literal ID="litStatus" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:AdminUser_Admin, AdminUser_Default_GridFullName%>"
                                UniqueName="fullname" SortExpression="lastName">
                                <ItemTemplate>
                                    <asp:Literal ID="litLastName" runat="server" />, <asp:Literal ID="litFirstName" runat="server" /></ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:AdminUser_Admin, AdminUser_Default_GridUserName %>"
                                UniqueName="userName" SortExpression="userName" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <asp:Literal ID="litUserName" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:AdminUser_Admin, AdminUser_Default_GridAccountType %>"
                                UniqueName="accountType" SortExpression="accountType" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <asp:Literal ID="litAccountType" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:AdminUser_Admin, AdminUser_Default_GridLoginLimit %>"
                                UniqueName="loginLimit" SortExpression="loginLimit" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <asp:Literal ID="litLoginLimit" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:AdminUser_Admin, AdminUser_Default_GridLoginCount %>"
                                UniqueName="counter" SortExpression="counter" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <asp:Literal ID="litCounter" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:AdminUser_Admin, AdminUser_Default_GridLoginExpirationDate %>"
                                UniqueName="expirationDate" SortExpression="expirationDate" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <asp:Literal ID="litExpirationDate" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="editAdminUser" DataField="UserID" HeaderText="<%$ Resources:AdminUser_Admin, AdminUser_Default_GridEdit %>">
                                <ItemTemplate>
                                    <a id="aAdminUserEdit" runat="server">
                                        <%= Resources.AdminUser_Admin.AdminUser_Default_GridEdit%></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="deleteAdminUser" DataField="UserID" HeaderText="<%$ Resources:AdminUser_Admin, AdminUser_Default_GridDelete %>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"><%= Resources.AdminUser_Admin.AdminUser_Default_GridDeleteButton%></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <div style="padding-left: 10px; height: 20px; padding-top: 5px" class="fLeft">
                                <a href="editAdd.aspx">
                                    <img style="border: 0px" alt="" src="/admin/images/user1_add.gif" width="24" height="24" />
                                    <%= Resources.AdminUser_Admin.AdminUser_Default_Grid_AddNewEntry%></a>
                            </div>
                            <div id="divViewAdminUsers" runat="server" visible="false" class="fRight">
                                <asp:RadioButtonList ID="rblViewAdminUsers" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rblViewAdminUsers_OnSelectedIndexChanged">
                                    <asp:ListItem Value="False" Text="<%$ Resources:AdminUser_Admin, AdminUser_Default_Grid_AdminUsersCurrentSite %>"></asp:ListItem>
                                    <asp:ListItem Value="True" Text="<%$ Resources:AdminUser_Admin, AdminUser_Default_Grid_AdminUsersAllSites %>"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </CommandItemTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
            </td>
        </tr>
    </table>

</asp:Content>
