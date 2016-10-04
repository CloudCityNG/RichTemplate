<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="login_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divModuleContent">
        <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
            <asp:Literal ID="litModuleDynamicContent" runat="server" />
        </div>
        <asp:Panel ID="pnlSiteAccessDeclined" runat="server" Visible="False" CssClass="error_panel">
            <table style="width: 500px">
                <tr>
                    <td style="width: 16px">
                        <img src="images/warning_16.png" />
                    </td>
                    <td style="width: 484px">
                        <span class="error_text">
                            <%= Resources.Login_FrontEnd.Login_Default_IncorrectEmailAddress_Heading%></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span id="spanInvalidUsernamePassword" runat="server" visible="false">
                            <%= Resources.Login_FrontEnd.Login_Default_UsernamePasswordInvalid%>
                        </span><span id="spanNoAccessToThisSite" runat="server" visible="false">
                            <%= Resources.Login_FrontEnd.Login_Default_NoAccessToThisSite%>
                            <a id="aNoAccessToThisSite" runat="server"><asp:Literal ID="litNoAccessToThisSite"
                                runat="server" /></a>
                        </span>
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="loginPanel" runat="server" DefaultButton="submit">
            <div class="box">
                <table class="topPad">
                    <tr>
                        <td valign="top" width="120px">
                            <%= Resources.Login_FrontEnd.Login_Default_EmailAddress%>:
                        </td>
                        <td class="errorStyle">
                            <asp:TextBox ID="email" runat="server" Width="180px"></asp:TextBox>
                            *
                            <asp:RequiredFieldValidator ID="reqEmailAddress" runat="server" ControlToValidate="email"
                                CssClass="errorStyle" Display="Dynamic" SetFocusOnError="True"><%= Resources.Login_FrontEnd.Login_Default_RequiredMessage%></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="email"
                                CssClass="errorStyle" Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><%= Resources.Login_FrontEnd.Login_Default_InvalidEmail%></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <%= Resources.Login_FrontEnd.Login_Default_Password%>:
                        </td>
                        <td class="errorStyle">
                            <asp:TextBox ID="userPassword" runat="server" Width="180px" TextMode="password"></asp:TextBox>
                            *
                            <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="userPassword"
                                CssClass="errorStyle" SetFocusOnError="True"><%= Resources.Login_FrontEnd.Login_Default_RequiredMessage%></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button runat="server" Text="<%$ Resources:Login_FrontEnd, Login_Default_ButtonLogin %>"
                                ID="submit" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 69px">
                        </td>
                        <td class="errorStyle">
                            *
                            <%= Resources.Login_FrontEnd.Login_Default_DenotesRequired%>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
