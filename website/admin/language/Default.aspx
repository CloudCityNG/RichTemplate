<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    CodeFile="Default.aspx.vb" Inherits="admin_language_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%= Resources.Language_Admin.Language_Default_BodyHeading%></span><br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="rgLanguage" runat="server" PageSize="20">
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
                            <telerik:GridBoundColumn DataField="Language" HeaderText="<%$ Resources:Language_Admin, Language_Default_GridLanguageName %>"
                                SortExpression="Language" UniqueName="Language">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Code" HeaderText="<%$ Resources:Language_Admin, Language_Default_GridLanguageCode %>"
                                SortExpression="Code" UniqueName="Code">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="editLanguageLanguageFiles" DataField="ID"
                                HeaderText="<%$ Resources:Language_Admin, Language_Default_GridEdit_LanguageFiles %>">
                                <ItemTemplate>
                                    <a id="aLanguageEditLanguageFiles" runat="server">
                                        <%= Resources.Language_Admin.Language_Default_GridEdit_LanguageFiles%></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="editLanguage" DataField="ID" HeaderText="<%$ Resources:Language_Admin, Language_Default_GridEdit %>">
                                <ItemTemplate>
                                    <a id="aLanguageEdit" runat="server">
                                        <%= Resources.Language_Admin.Language_Default_GridEdit%></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Language_Admin, Language_Default_GridDelete%>">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                <a href="editAdd.aspx">
                                    <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                    <%= Resources.Language_Admin.Language_Default_Grid_AddNewEntry%></a>
                            </div>
                        </CommandItemTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
                <div class="divFooter">
                    <div class="fRight">
                        <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Language_Admin, Language_Default_GridDeleteButton%>" />
                   </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
