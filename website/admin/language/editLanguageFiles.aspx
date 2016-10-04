<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    CodeFile="editLanguageFiles.aspx.vb" Inherits="admin_Language_EditLanguageFiles" %>

<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout">
        <%= Resources.Language_Admin.Language_EditLanguageFiles_BodyHeading%></span><br />
    <br />
    <div id="MainContent">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <span class="moduleLabel">
                        <%= Resources.Language_Admin.Language_EditLanguageFiles_FrontEndOrAdmin%>:</span>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblLanguageSection" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow" AutoPostBack="true" CssClass="rblLanguageSection">
                        <asp:ListItem Value="Admin" Text="<%$ Resources:Language_Admin, Language_EditLanguageFiles_LanguageSection_Admin %>" />
                        <asp:ListItem Value="FrontEnd" Text="<%$ Resources:Language_Admin, Language_EditLanguageFiles_LanguageSection_FrontEnd %>" />
                        <asp:ListItem Value="Karamasoft" Text="<%$ Resources:Language_Admin, Language_EditLanguageFiles_LanguageSection_Karamasoft %>" />
                        <asp:ListItem Value="Telerik" Text="<%$ Resources:Language_Admin, Language_EditLanguageFiles_LanguageSection_Telerik %>" />
                        <asp:ListItem Value="UserControls" Text="<%$ Resources:Language_Admin, Language_EditLanguageFiles_LanguageSection_UserControls %>" />
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="moduleLabel">
                        <%= Resources.Language_Admin.Language_EditLanguageFiles_LanguageFile%>:</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLanguageFile" runat="server" AutoPostBack="true" />
                </td>
            </tr>
        </table>
        <br />
        <div id="divLanguageFileKeysUpdated" runat="server" class="importantStyle" visible="false">
            <b>
                <%= Resources.Language_Admin.Language_EditLanguageFiles_LanguageFileKeys_Updated%></b>
        </div>
        <div id="divLanguageFileKeys" runat="server" class="divLanguageFileKeys" visible="false">
            <div class="fLeft">
                <h3>
                    <%= Resources.Language_Admin.Language_EditLanguageFiles_LanguageFileKeys%></h3>
            </div>
            <div class="divCopyAll">
                <asp:LinkButton ID="lnkCopyAll_Text" runat="server"><span><%= Resources.Language_Admin.Language_EditLanguageFiles_LanguageFileKeys_CopyAllKeys%></span></asp:LinkButton>
                <asp:LinkButton ID="lnkCopyAll_Image" runat="server"><img src="/admin/images/arrow_forward.jpg" alt="" /></asp:LinkButton>
            </div>
            <br class="cBoth" />
            <table width="100%">
                <tr>
                    <th>
                        <%= Resources.Language_Admin.Language_EditLanguageFiles_LanguageFileKeys_LanguageKey%>
                    </th>
                    <th>
                        <%= Resources.Language_Admin.Language_EditLanguageFiles_LanguageFileKeys_BaseValue%>
                    </th>
                    <th>
                        <%= Resources.Language_Admin.Language_EditLanguageFiles_LanguageFileKeys_LangugaeSpecficValue%>
                    </th>
                </tr>
                <asp:Repeater ID="rptLanguageKeys" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Literal ID="litLanguageKey" runat="server" Text='<%# Eval("LanguageKey") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="txtBaseValue" runat="server" ReadOnly="true" Width="280" Text='<%# Eval("BaseValue") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="txtLanguageSpecificValue" runat="server" Width="280" Text='<%# If(Not Eval("LanguageSpecificValue") Is DBNull.Value, Eval("LanguageSpecificValue"), "")%>' />
                                <span class="requiredStar">*</span>
                                <asp:RequiredFieldValidator ID="reqLanguageValue" runat="server" ErrorMessage="<%$ Resources:Language_Admin, Language_EditLanguageFiles_RequiredMessage %>"
                                    ControlToValidate="txtLanguageSpecificValue" CssClass="errorStyle" Display="Dynamic"
                                    ValidationGroup="UpdateLanguageFile" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <br />
            <span class="requiredStar">*</span>
            <%= Resources.Language_Admin.Language_EditLanguageFiles_DenotesRequired%><br />
        </div>
    </div>
    <br />
    <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:Language_Admin, Language_EditLanguageFiles_ButtonUpdate %>"
        Visible="false" CausesValidation="true" ValidationGroup="UpdateLanguageFile" />
    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Language_Admin, Language_EditLanguageFiles_ButtonBack %>"
        CausesValidation="false" />
</asp:Content>
