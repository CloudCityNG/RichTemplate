<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    CodeFile="Default.aspx.vb" Inherits="admin_Help_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%= Resources.Help.Help_Default_BodyHeading%></span><br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="rgHelp" runat="server" DataSourceID="dsHelp" PageSize="20">
                    <MasterTableView DataKeyNames="ID" DataSourceID="dsHelp" CommandItemDisplay="Top">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" ReadOnly="True" SortExpression="ID"
                                UniqueName="ID">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Title" HeaderText="<%$ Resources:Help, Help_Default_GridHelpTitle %>"
                                SortExpression="Title" UniqueName="Title">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Help, Help_Default_GridHelpDescription %>"
                                UniqueName="Description" SortExpression="Description" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <asp:Literal ID="litDescription" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Help, Help_Default_GridHelpStatus %>"
                                UniqueName="active" SortExpression="active" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <%#If(Convert.ToBoolean(Eval("active")), "<span class='activeField'>" & Resources.Help.Help_Default_GridHelpStatusActive & "</span>", "<span class='inactiveField'>" & Resources.Help.Help_Default_GridHelpStatusArchive & "</span>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="editHelpItem" DataField="ID" HeaderText="<%$ Resources:Help, Help_Default_GridEdit %>">
                                <ItemTemplate>
                                    <a id="aHelpEdit" runat="server">
                                        <%= Resources.Help.Help_Default_GridEdit%></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                <a href="editAdd.aspx">
                                    <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                    <%= Resources.Help.Help_Default_Grid_AddNewEntry%></a>
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
    <asp:SqlDataSource ID="dsHelp" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="ss_Help_SelectList" SelectCommandType="StoredProcedure" />
</asp:Content>
