<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    CodeFile="Default.aspx.vb" Inherits="admin_emails_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%= Resources.Email.Email_Default_BodyHeading%></span><br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="rgEmailTemplate" runat="server" PageSize="20">
                    <MasterTableView DataKeyNames="EmailTemplateID" CommandItemDisplay="None">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="EmailTemplateID" DataType="System.Int32" ReadOnly="True"
                                SortExpression="EmailTemplateID" UniqueName="EmailTemplateID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Name" HeaderText="<%$ Resources:Email, Email_Default_GridEmailTemplateName %>"
                                SortExpression="Name" UniqueName="Name">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Subject" HeaderText="<%$ Resources:Email, Email_Default_GridEmailSubject %>"
                                SortExpression="Subject" UniqueName="Subject">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Email, Email_Default_GridEmailStatus %>"
                                UniqueName="active" SortExpression="active" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <%#If(Convert.ToBoolean(Eval("active")), "<span class='activeField'>" & Resources.Email.Email_Default_GridEmailStatusActive & "</span>", "<span class='inactiveField'>" & Resources.Email.Email_Default_GridEmailStatusArchive & "</span>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="editEmail" DataField="EmailTemplateID" HeaderText="<%$ Resources:Email, Email_Default_GridEdit %>">
                                <ItemTemplate>
                                    <a id="aEmailTemplateEdit" runat="server">
                                        <%= Resources.Email.Email_Default_GridEdit%></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Email, Email_Default_GridDelete %>"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
