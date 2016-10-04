<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    CodeFile="editAdd.aspx.vb" Inherits="admin_emails_editAdd" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout">
        <%= Resources.Email.Email_AddEdit_BodyHeading%></span><br />
    <br />
    <span class="callout">
        <%= Resources.Email.Email_AddEdit_Tab_Content_Heading%></span><br />
    <%= Resources.Email.Email_AddEdit_Tab_Content_SubHeading%><br />
    <br />
    <div id="MainContent">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <label for="Active">
                        <span class="moduleLabel">
                            <%= Resources.Email.Email_AddEdit_Status%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="Active" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="True" Text="<%$ Resources:Email, Email_AddEdit_StatusActive %>" />
                        <asp:ListItem Value="False" Text="<%$ Resources:Email, Email_AddEdit_StatusArchive %>" />
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_Name">
                        <span class="moduleLabel">
                            <%= Resources.Email.Email_AddEdit_EmailTemplateName%>:</span> <span class="requiredStar">
                                *</span>
                        <asp:RequiredFieldValidator ID="reqEmailTemplateName" runat="server" ErrorMessage=" <%$ Resources:Email, Email_AddEdit_RequiredMessage %>"
                            ControlToValidate="txt_Name" CssClass="errorStyle" Display="Dynamic" />
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txt_Name" runat="server" CssClass="tb400" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_Description">
                        <span class="moduleLabel">
                            <%= Resources.Email.Email_AddEdit_EmailDescription%>:</span> <span class="requiredStar">
                                *</span>
                        <asp:RequiredFieldValidator ID="reqEmailDescription" runat="server" ErrorMessage=" <%$ Resources:Email, Email_AddEdit_RequiredMessage %>"
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
                    <label for="txt_SenderEmailAddress">
                        <span class="moduleLabel">
                            <%= Resources.Email.Email_AddEdit_EmailSenderEmailAddress%>:</span><span class="requiredStar">*</span>
                    </label>
                    <asp:RequiredFieldValidator ID="reqEmailSenderEmailAddress" runat="server" ErrorMessage=" <%$ Resources:Email, Email_AddEdit_RequiredMessage %>"
                        ControlToValidate="txt_SenderEmailAddress" CssClass="errorStyle" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regEmailSenderEmailAddress" ControlToValidate="txt_SenderEmailAddress"
                        ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                        ErrorMessage=" <%$ Resources:Email, Email_AddEdit_InvalidEmailAddress %>" Display="dynamic"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txt_SenderEmailAddress" runat="server" CssClass="tb400" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_SenderName">
                        <span class="moduleLabel">
                            <%= Resources.Email.Email_AddEdit_EmailSenderName%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txt_SenderName" runat="server" CssClass="tb400" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_ReplyToEmailAddress">
                        <span class="moduleLabel">
                            <%= Resources.Email.Email_AddEdit_EmailReplyToEmailAddress%>:</span>
                    </label>
                    <asp:RegularExpressionValidator ID="regEmailReplyToEmailAddress" ControlToValidate="txt_ReplyToEmailAddress"
                        ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                        ErrorMessage=" <%$ Resources:Email, Email_AddEdit_InvalidEmailAddress %>" Display="dynamic"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txt_ReplyToEmailAddress" runat="server" CssClass="tb400" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_ReplyToName">
                        <span class="moduleLabel">
                            <%= Resources.Email.Email_AddEdit_EmailReplyToName%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txt_ReplyToName" runat="server" CssClass="tb400" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_Subject">
                        <span class="moduleLabel">
                            <%= Resources.Email.Email_AddEdit_EmailSubject%>:</span> <span class="requiredStar">
                                *</span>
                        <asp:RequiredFieldValidator ID="reqEmailSubject" runat="server" ErrorMessage=" <%$ Resources:Email, Email_AddEdit_RequiredMessage %>"
                            ControlToValidate="txt_Subject" CssClass="errorStyle" Display="Dynamic" />
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txt_Subject" runat="server" CssClass="tb400" />
                </td>
            </tr>
            <tr id="trEmailBodyAvailableParameters" runat="server" visible="false">
                <td>
                    <span class="moduleLabel">
                        <%= Resources.Email.Email_AddEdit_EmailBody_AvailableParameters%>:</span>&nbsp;<b><asp:Literal
                            ID="litEmailBodyAvailableParameterList" runat="server" /></b>
                </td>
            </tr>
            <tr id="trRecipientEmailAddressValue" runat="server" visible="false">
                <td colspan="2">
                    <asp:TextBox ID="txt_RecipientEmailAddress" runat="server" CssClass="tb400" /><br />
                    <span class="grayTextSml_10">(<%= Resources.Email.Email_AddEdit_EmailRecipientEmailAddress_Instructions%>)</span>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_BodyText">
                        <span class="moduleLabel">
                            <%= Resources.Email.Email_AddEdit_EmailBody_Text%>:</span> <span class="requiredStar">
                                *</span>
                        <asp:RequiredFieldValidator ID="reqEmailBodyText" runat="server" ErrorMessage=" <%$ Resources:Email, Email_AddEdit_RequiredMessage %>"
                            ControlToValidate="txt_BodyText" CssClass="errorStyle" Display="Dynamic" />
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txt_BodyText" runat="server" TextMode="MultiLine" Rows="8" Width="98%" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txt_BodyHtml">
                        <span class="moduleLabel">
                            <%= Resources.Email.Email_AddEdit_EmailBody_Html%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadEditor ID="txt_BodyHtml" runat="server" Width="98%">
                    </telerik:RadEditor>
                </td>
            </tr>
            <tr id="trRecipientEmailAddressHeading" runat="server" visible="false">
                <td>
                    <label for="txt_RecipientEmailAddress">
                        <span class="moduleLabel">
                            <%= Resources.Email.Email_AddEdit_EmailRecipientEmailAddress%>:</span>
                    </label>
                    <asp:RegularExpressionValidator ID="regEmailRecipientEmailAddress" ControlToValidate="txt_RecipientEmailAddress"
                        ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                        ErrorMessage=" <%$ Resources:Email, Email_AddEdit_InvalidEmailAddress %>" Display="dynamic"
                        runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" CausesValidation="true" />
    <asp:Button ID="cancelButton" runat="server" Text="<%$ Resources:Email, Email_AddEdit_ButtonCancel %>"
        CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span>
    <%= Resources.Email.Email_AddEdit_DenotesRequired%><br />
</asp:Content>
