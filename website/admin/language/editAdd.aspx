<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    CodeFile="editAdd.aspx.vb" Inherits="admin_language_editAdd" %>

<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout">
        <%= Resources.Language_Admin.Language_AddEdit_BodyHeading%></span><br />
    <br />
    <div id="MainContent">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <label for="txt_Name">
                        <span class="moduleLabel">
                            <%= Resources.Language_Admin.Language_AddEdit_LanguageName%>:</span> <span class="requiredStar">
                                *</span>
                        <asp:RequiredFieldValidator ID="reqLanguageName" runat="server" ErrorMessage=" <%$ Resources:Language_Admin, Language_AddEdit_RequiredMessage %>"
                            ControlToValidate="txt_LanguageName" CssClass="errorStyle" Display="Dynamic" />
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txt_LanguageName" runat="server" CssClass="tb400" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_LanguageCode">
                        <span class="moduleLabel">
                            <%= Resources.Language_Admin.Language_AddEdit_LanguageCode%>:</span><span class="requiredStar">*</span>
                    </label>
                    <asp:RequiredFieldValidator ID="reqLanguageCode" runat="server" ErrorMessage=" <%$ Resources:Language_Admin, Language_AddEdit_RequiredMessage %>"
                        ControlToValidate="txt_LanguageCode" CssClass="errorStyle" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txt_LanguageCode" runat="server" CssClass="tb400" MaxLength="10" /><br />
                    <span class="grayTextSml_11">(<%= Resources.Language_Admin.Language_AddEdit_LanguageCodeListURL%>: <a href="http://sharpertutorials.com/list-of-culture-codes" target="_blank"><asp:Literal runat="server" Text="http://sharpertutorials.com/list-of-culture-codes" /></a>)</span>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_LanguageLetters">
                        <span class="moduleLabel">
                            <%= Resources.Language_Admin.Language_AddEdit_LanguageLetters%>:</span><span class="requiredStar">*</span>
                    </label>
                    <asp:RequiredFieldValidator ID="reqLanguageLetters" runat="server" ErrorMessage=" <%$ Resources:Language_Admin, Language_AddEdit_RequiredMessage %>"
                        ControlToValidate="txt_LanguageLetters" CssClass="errorStyle" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txt_LanguageLetters" runat="server" CssClass="tb400" /><br />
                    <span class="grayTextSml_11"><%= Resources.Language_Admin.Language_AddEdit_LanguageLetters_Instructions%></span>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_LanguageDescription">
                        <span class="moduleLabel">
                            <%= Resources.Language_Admin.Language_AddEdit_LanguageDescription%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadEditor ID="txt_LanguageDescription" runat="server" Width="98%">
                    </telerik:RadEditor>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" CausesValidation="true" />
    <asp:Button ID="cancelButton" runat="server" Text="<%$ Resources:Language_Admin, Language_AddEdit_ButtonCancel %>"
        CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span>
    <%= Resources.Language_Admin.Language_AddEdit_DenotesRequired%><br />
</asp:Content>
