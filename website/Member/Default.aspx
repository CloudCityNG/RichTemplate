<%@ Page Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="Member_Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divModuleContent">
        <table>
            <tr>
                <td valign="top">
                    <div class="divNewMemberColumn">
                        <asp:Panel ID="divNewMemberPanel" runat="server" Visible="false" CssClass="divNewMemberPanel">
                            <div class="divMemberSubHeading">
                                <%= Resources.Member_FrontEnd.Member_Default_Login_NewMembers%></div>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <img src="images/User-Accounts.gif" width="24" height="24" />
                                    </td>
                                    <td style="vertical-align: middle; padding-left: 2px;">
                                        <a href="addprofile.aspx">
                                            <%= Resources.Member_FrontEnd.Member_Default_Success_NewMember%></a>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </td>
                <td valign="top">
                    <%= Resources.Member_FrontEnd.Member_Default_Introduction %>
                    <br />
                    <br />
                    <asp:Panel ID="pnlSiteAccessDeclined" runat="server" Visible="False" CssClass="error_panel">
                        <table style="width: 500px">
                            <tr>
                                <td style="width: 16px">
                                    <img src="images/warning_16.png" />
                                </td>
                                <td style="width: 484px">
                                    <span class="error_text">
                                        <%= Resources.Member_FrontEnd.Member_Default_IncorrectEmailAddress_Heading%></span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <span id="spanInvalidUsernamePassword" runat="server" visible="false">
                                        <%= Resources.Member_FrontEnd.Member_Default_UsernamePasswordInvalid%>
                                    </span><span id="spanNoAccessToThisSite" runat="server" visible="false">
                                        <%= Resources.Member_FrontEnd.Member_Default_NoAccessToThisSite%>
                                        <a id="aNoAccessToThisSite" runat="server"><asp:Literal ID="litNoAccessToThisSite"
                                            runat="server" /></a>
                                    </span>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="authenticatedPanel" runat="server" Visible="False" Width="92%">
                        <table style="width: 500px">
                            <tr>
                                <td style="text-align: left">
                                    <h2>
                                        <%= Resources.Member_FrontEnd.Member_Default_SuccessHeading%></h2>
                                    <%= Resources.Member_FrontEnd.Member_Default_SuccessBody%>
                                    <br />
                                    <br />
                                    <a href='<%= MemberHomepage %>'>
                                        <%= Resources.Member_FrontEnd.Member_Default_Success_MemberSectionMessage%></a>
                                    <br />
                                    <br />
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <img src="images/28.png" />
                                            </td>
                                            <td>
                                                &nbsp;<a href="updateprofile.aspx"><%= Resources.Member_FrontEnd.Member_Default_Success_UpdateProfileMessage%></a>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="loginPanel" runat="server" DefaultButton="submit" Visible="false"
                        Width="92%">
                        <div class="box">
                            <table class="topPad">
                                <tr>
                                    <td valign="top" width="120px">
                                        <%= Resources.Member_FrontEnd.Member_Default_Login_EmailAddress%>:
                                    </td>
                                    <td class="errorStyle" width="220px">
                                        <asp:TextBox ID="email" runat="server" Width="180px"></asp:TextBox>
                                        *
                                        <asp:RequiredFieldValidator ID="reqEmailAddress" runat="server" ControlToValidate="email"
                                            CssClass="errorStyle" Display="Dynamic" SetFocusOnError="True"><br /><%= Resources.Member_FrontEnd.Member_Default_RequiredMessage%></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="email"
                                            CssClass="errorStyle" Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><br /><%= Resources.Member_FrontEnd.Member_Default_Login_InvalidEmail%></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <%= Resources.Member_FrontEnd.Member_Default_Login_Password%>:
                                    </td>
                                    <td class="errorStyle">
                                        <asp:TextBox ID="userPassword" runat="server" Width="180px" TextMode="password"></asp:TextBox>
                                        *
                                        <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="userPassword"
                                            CssClass="errorStyle" SetFocusOnError="True"><br /><%= Resources.Member_FrontEnd.Member_Default_RequiredMessage%></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Button runat="server" Text="<%$ Resources:Member_FrontEnd, Member_Default_Login_ButtonLogin %>"
                                            ID="submit" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 69px">
                                    </td>
                                    <td class="errorStyle">
                                        *
                                        <%= Resources.Member_FrontEnd.Member_Default_DenotesRequired%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <br />
                                        <a href="ForgottenPassword.aspx">
                                            <%= Resources.Member_FrontEnd.Member_Default_ForgotYourPassword%></a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
