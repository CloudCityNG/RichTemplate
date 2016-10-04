<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    CodeFile="editAdd.aspx.vb" Inherits="admin_Help_editAdd" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <uc:header id="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout"><%= Resources.Help.Help_AddEdit_BodyHeading%></span><br />
    <br />
    <span class="callout"><%= Resources.Help.Help_AddEdit_Tab_Content_Heading%></span><br />
    <%= Resources.Help.Help_AddEdit_Tab_Content_SubHeading%><br />
    <br />
    <div id="MainContent">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <label for="Active">
                        <span class="moduleLabel"><%= Resources.Help.Help_AddEdit_Status%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="Active" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="True" Text="<%$ Resources:Help, Help_AddEdit_StatusActive %>" />
                        <asp:ListItem Value="False" Text="<%$ Resources:Help, Help_AddEdit_StatusArchive %>" />
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_Title">
                        <span class="moduleLabel"><%= Resources.Help.Help_AddEdit_HelpTemplateName%>:</span> <span class="requiredStar">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage=" <%$ Resources:Help,Help_AddEdit_RequiredMessage %>"
                            ControlToValidate="txt_Title" CssClass="errorStyle" Display="Dynamic" />
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txt_Title" runat="server" CssClass="tb400" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_Description">
                        <span class="moduleLabel"><%= Resources.Help.Help_AddEdit_HelpTemplateDescription%>:</span> <span class="requiredStar">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage=" <%$ Resources:Help,Help_AddEdit_RequiredMessage %>"
                            ControlToValidate="txt_Description" CssClass="errorStyle" Display="Dynamic" />
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txt_Description" runat="server" TextMode="MultiLine" Rows="4" CssClass="tb400" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_Content">
                        <span class="moduleLabel"><%= Resources.Help.Help_AddEdit_HelpTemplateContent%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:radeditor id="txt_Content" runat="server" width="98%">
                    </telerik:radeditor>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" CausesValidation="true" />
    <asp:Button ID="cancelButton" runat="server" Text="<%$ Resources:Help, Help_AddEdit_ButtonCancel %>" CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span> <%= Resources.Help.Help_AddEdit_DenotesRequired%><br />
</asp:Content>
