<%@ Page Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" Title="RichTemplate Suggestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divModuleContent">
        <table width="100%">
            <tr>
                <td>
                    <div class="divCategoryListContainer">
                        &nbsp;
                    </div>
                </td>
                <td>
                    <asp:Panel ID="pnlForm" runat="server">
                        <div>
                            <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                                <asp:Literal ID="litModuleDynamicContent" runat="server" />
                            </div>
                            <div id="divCategory" runat="server">
                                <br />
                                <br />
                                <strong>
                                    <%=Resources.Suggestion_FrontEnd.Suggestion_Default_Category%>:</strong>&nbsp;<telerik:RadComboBox
                                        ID="rcbCategoryID" runat="server" />
                            </div>
                            <br />
                            <strong>
                                <%=Resources.Suggestion_FrontEnd.Suggestion_Default_EmailAddress%>:</strong>&nbsp;<span
                                    class="grayTextSml_10">(<%=Resources.Suggestion_FrontEnd.Suggestion_Default_EmailAddress_OptionalMessage%>)</span>
                            <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="txtEmailAddress"
                                CssClass="errorStyle" Display="dynamic" SetFocusOnError="True" ValidationExpression=".*@.*\..*"
                                ErrorMessage=" <%$ Resources:Suggestion_FrontEnd, Suggestion_Default_EmailAddress_InvalidMessage %>" />
                            <br />
                            <asp:TextBox ID="txtEmailAddress" runat="server" Width="300px" />&nbsp;
                            <br />
                            <br />
                            <strong><%=Resources.Suggestion_FrontEnd.Suggestion_Default_Suggestion%>:</strong>&nbsp;<span class="errorStyle">*</span>&nbsp;<asp:RequiredFieldValidator
                                ID="reqSuggestion" runat="server" ControlToValidate="txtSuggestion" CssClass="errorStyle"
                                Display="Dynamic" ErrorMessage=" <%$ Resources:Suggestion_FrontEnd, Suggestion_Default_RequiredMessage %>"
                                SetFocusOnError="True" />
                            <br />
                            <asp:TextBox ID="txtSuggestion" runat="server" Width="300px" TextMode="MultiLine"
                                Rows="5" />
                            <div id="divRadCaptcha" runat="server" visible="false">
                                <br />
                                <span class="errorStyle">*</span><%=Resources.Suggestion_FrontEnd.Suggestion_Default_CaptchaCode_Instructions%><br />
                                <telerik:RadCaptcha ID="radCaptchaOnlineSignup" runat="server" ErrorMessage=" <%$ Resources:Suggestion_FrontEnd, Suggestion_Default_RequiredMessage %>"
                                    Display="Dynamic" CaptchaImage-LineNoise="Low" CaptchaImage-TextChars="Letters"
                                    CaptchaTextBoxLabel="">
                                </telerik:RadCaptcha>
                            </div>
                            <br />
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="<%$ Resources:Suggestion_FrontEnd, Suggestion_Default_ButtonSubmit %>"
                                CausesValidation="true" /><br />
                            <span class="errorStyle">*
                                <%=Resources.Suggestion_FrontEnd.Suggestion_Default_DenotesRequired%></span><br />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlThanks" runat="server" Visible="false">
                        <div>
                            <h2>
                                <%=Resources.Suggestion_FrontEnd.Suggestion_Default_ThankYou_Heading%></h2>
                            <br />
                            <%=Resources.Suggestion_FrontEnd.Suggestion_Default_ThankYou_Line1%><br />
                            <br />
                            <%=Resources.Suggestion_FrontEnd.Suggestion_Default_ThankYou_Line2%>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
