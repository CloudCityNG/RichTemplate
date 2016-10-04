<%@ Page Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" Debug="True" CodeFile="ForgottenPassword.aspx.vb" Inherits="Member_ForgottenPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divModuleContent">
        <table>
            <tr>
                <td>
                    <div class="divCategoryListContainer">
                        &nbsp;
                    </div>
                </td>
                <td>
                    <div id="divForgottenPassword_StepOne" runat="server">
                        <asp:Panel ID="panelForgottenPasswordError" runat="server" Visible="False" CssClass="error_panel">
                            <table style="width: 500px">
                                <tr>
                                    <td style="width: 16px">
                                        <img src="images/warning_16.png" />
                                    </td>
                                    <td style="width: 484px">
                                        <span class="error_text">
                                            <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepOne_IncorrectEmailAddress_Heading%></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <span id="spanAccountNotFound" runat="server" visible="false">
                                            <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepOne_AccountNotFound%></span>
                                        <span id="spanNoAccessToThisSite" runat="server" visible="false">
                                            <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepOne_NoAccessToThisSite%>
                                        </span>
                                        <a id="aContactUs" runat="server"><asp:Literal ID="litContactUs" runat="server" /></a>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="panelForgotPassword" runat="server" Width="100%" DefaultButton="submitStepOne">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepOne_Body%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="box">
                                            <table>
                                                <tr>
                                                    <td style="width: 124px">
                                                        <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepOne_EmailAddress%>:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmail" runat="server" Width="160"></asp:TextBox>
                                                        <span class="errorStyle">*</span>
                                                        <asp:RequiredFieldValidator ID="reqEmailAddress" runat="server" ControlToValidate="txtEmail"
                                                            CssClass="errorStyle" Display="Dynamic" SetFocusOnError="True" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_ForgottenPassword_StepOne_RequiredMessage %>" />
                                                        <asp:RegularExpressionValidator ID="regEmailAddress" runat="server" ControlToValidate="txtEmail"
                                                            CssClass="errorStyle" Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ErrorMessage="<%$ Resources:Member_FrontEnd, Member_ForgottenPassword_StepOne_InvalidEmail %>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="submitStepOne" runat="server" Text="<%$ Resources:Member_FrontEnd, Member_ForgottenPassword_StepOne_ButtonFindPassword %>"
                                                            Width="166" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div id="divForgottenPassword_StepTwo" runat="server" visible="false">
                        <asp:Panel ID="panelSecurityQuestionError" runat="server" Visible="False" CssClass="error_panel">
                            <table>
                                <tr>
                                    <td width="16px" align="left">
                                        <img src="images/warning_16.png" />
                                    </td>
                                    <td width="100%" align="left">
                                        <span class="error_text">
                                            <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepTwo_IncorrectAnswer_Heading%></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepTwo_IncorrectAnswer_Body%>
                                        <br />
                                        <asp:LinkButton ID="btn_EmailDetails1" runat="server" Text="<%$ Resources:Member_FrontEnd, Member_ForgottenPassword_StepTwo_IncorrectAnswer_EmailPassword %>" />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlSecurityQuestionAndAnswer" runat="server" Width="50%" DefaultButton="btnSubmitSecurityAnswer">
                            <table style="width: 500px">
                                <tr>
                                    <td colspan="2" style="height: 17px">
                                        <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepTwo_EnterSecurityQuestionAnswer%>
                                        <br />
                                        <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepTwo_EnterEmailAddress%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="box">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 124px">
                                                        <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepTwo_SecurityQuestion%>:
                                                    </td>
                                                    <td>
                                                        <b><asp:Literal ID="litSecurityQuestion" runat="server" /></b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepTwo_SecurityAnswer%>:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="securityAnswer" runat="server" Width="160px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqSecurityAnswer" runat="server" ControlToValidate="securityAnswer"
                                                            CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_ForgottenPassword_StepTwo_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnSubmitSecurityAnswer" runat="server" Text="<%$ Resources:Member_FrontEnd, Member_ForgottenPassword_StepTwo_ButtonFindPassword %>"
                                                            Width="166px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlResendPassword" runat="server" Width="50%" DefaultButton="btn_EmailDetails2">
                            <table style="width: 500px">
                                <tr>
                                    <td class="orangeText" colspan="2">
                                        <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepTwo_ResendPassword_Heading%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="box">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepTwo_ResendPassword_EmailSentTo%>:
                                                    </td>
                                                    <td>
                                                        <b><asp:Literal ID="litEmailAddress" runat="server" /></b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btn_EmailDetails2" runat="server" Text="<%$ Resources:Member_FrontEnd, Member_ForgottenPassword_StepTwo_EmailPassword %>"
                                                            Width="166px" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div id="divForgottenPassword_StepThree" runat="server" visible="false">
                        <table style="width: 500px">
                            <tr>
                                <td>
                                    <br />
                                    <br />
                                    <span class="orangeText">
                                        <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepThree_Heading%></span>
                                    <br />
                                    <br />
                                    <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepThree_ResendPassword_EmailSentTo%>
                                    <b><asp:Literal ID="litEmailAddress2" runat="server" /></b><br />
                                    <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepThree_CheckEmailForPassword%><br />
                                    <a href="/Member/">
                                        <%= Resources.Member_FrontEnd.Member_ForgottenPassword_StepThree_Login%></a><br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
