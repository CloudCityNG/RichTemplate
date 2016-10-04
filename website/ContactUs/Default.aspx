<%@ Page Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="ContactUs_Default"
    Title="RichTemplate Contact-Us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlForm" runat="server">
        <div>
            <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                <asp:Literal ID="litModuleDynamicContent" runat="server" />
            </div>
            <div id="divCategory" runat="server" class="topPad" visible="false">
                <strong>
                    <%=Resources.ContactUs_FrontEnd.ContactUs_Default_Category%>:</strong>&nbsp;<telerik:RadComboBox
                        ID="rcbCategoryID" runat="server" />
            </div>
        <br />
        <strong>
            <%=Resources.ContactUs_FrontEnd.ContactUs_Default_EmailAddress%>:</strong>&nbsp;<span
                class="grayTextSml_10">(<%=Resources.ContactUs_FrontEnd.ContactUs_Default_EmailAddress_OptionalMessage%>)</span>
        <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="txtEmailAddress"
            CssClass="errorStyle" Display="dynamic" SetFocusOnError="True" ValidationExpression=".*@.*\..*"
            ErrorMessage=" <%$ Resources:ContactUs_FrontEnd, ContactUs_Default_EmailAddress_InvalidMessage %>" />
        <br />
        <asp:TextBox ID="txtEmailAddress" runat="server" Width="300px" />&nbsp;
        <br />
        <br />
        <strong>
            <%=Resources.ContactUs_FrontEnd.ContactUs_Default_ContactUsMessage%>:</strong>&nbsp;<span
                class="errorStyle">*</span>&nbsp;<asp:RequiredFieldValidator ID="reqContactUsMessage"
                    runat="server" ControlToValidate="txtContactUsMessage" CssClass="errorStyle"
                    Display="Dynamic" ErrorMessage=" <%$ Resources:ContactUs_FrontEnd, ContactUs_Default_RequiredMessage %>"
                    SetFocusOnError="True" />
        <br />
        <asp:TextBox ID="txtContactUsMessage" runat="server" Width="350px" TextMode="MultiLine"
            Rows="5" />
        <div id="divRadCaptcha" runat="server" visible="false">
            <br />
            <span class="errorStyle">*</span><%=Resources.ContactUs_FrontEnd.ContactUs_Default_CaptchaCode_Instructions%><br />
            <telerik:RadCaptcha ID="radCaptchaContactUs" runat="server" ErrorMessage=" <%$ Resources:ContactUs_FrontEnd, ContactUs_Default_RequiredMessage %>"
                Display="Dynamic" CaptchaImage-LineNoise="Low" CaptchaImage-TextChars="Letters"
                CaptchaTextBoxLabel="">
            </telerik:RadCaptcha>
        </div>
        <br />
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="<%$ Resources:ContactUs_FrontEnd, ContactUs_Default_ButtonSubmit %>"
            CausesValidation="true" /><br />
        <span class="errorStyle">*
            <%=Resources.ContactUs_FrontEnd.ContactUs_Default_DenotesRequired%></span><br />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlThanks" runat="server" Visible="false">
        <div>
            <h2>
                <%=Resources.ContactUs_FrontEnd.ContactUs_Default_ThankYou_Heading%></h2>
            <br />
            <%=Resources.ContactUs_FrontEnd.ContactUs_Default_ThankYou_Line1%><br />
            <br />
            <%=Resources.ContactUs_FrontEnd.ContactUs_Default_ThankYou_Line2%>
        </div>
    </asp:Panel>
</asp:Content>
