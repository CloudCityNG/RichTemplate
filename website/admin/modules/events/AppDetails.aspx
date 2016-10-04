<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="AppDetails.aspx.vb" Inherits="admin_modules_employment_AppDetails" %>

<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <uc:header id="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout"><%=Resources.Event_Admin.Event_AddEditApplicants_HeadingBody%></span><br />
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/back.png" />
                        </td>
                        <td>
                            <a id="aBackToRegistrations" runat="server"><%=Resources.Event_Admin.Event_AddEditApplicants_BackToApplicants%></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="MainContent">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <label for="txtMemberID">
                        <span class="moduleLabel"><%=Resources.Event_Admin.Event_AddEditApplicants_MemberID%>:</span></label>&nbsp;
                    <asp:TextBox ID="txtMemberID" runat="server" Width="30" />
                    <asp:CustomValidator ID="customValMemberExist" runat="server" Display="Dynamic" OnServerValidate="customValMemberExist_Validate"
                        CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Event_Admin.Event_AddEditApplicants_MemberDoesNotExist%></b>
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="Status">
                        <span class="moduleLabel"><%=Resources.Event_Admin.Event_AddEditApplicants_Status%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="True" Text="<%$ Resources:Event_Admin, Event_AddEditApplicants_StatusActive %>" />
                        <asp:ListItem Value="False" Text="<%$ Resources:Event_Admin, Event_AddEditApplicants_StatusCancelled %>" />
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2">
                                <label>
                                    <span class="moduleLabel"><%=Resources.Event_Admin.Event_AddEditApplicants_Salutation%>:</span></label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlSalutation" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label for="txtFirstName">
                                    <span class="moduleLabel"><%=Resources.Event_Admin.Event_AddEditApplicants_FirstName%></span><span class="requiredStar">*</span></label><asp:RequiredFieldValidator
                                        ID="reqFirstName" runat="server" ErrorMessage=" <%$ Resources:Event_Admin, Event_AddEditApplicants_Required %>" ControlToValidate="txtFirstName"
                                        CssClass="errorStyle" Display="Dynamic" />
                            </td>
                            <td class="leftPad">
                                <label for="txtLastName">
                                    <span class="moduleLabel"><%=Resources.Event_Admin.Event_AddEditApplicants_LastName%></span><span class="requiredStar">*</span></label><asp:RequiredFieldValidator
                                        ID="reqLastName" runat="server" ErrorMessage=" <%$ Resources:Event_Admin, Event_AddEditApplicants_Required %>" ControlToValidate="txtLastName"
                                        CssClass="errorStyle" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtFirstName" runat="server" />
                            </td>
                            <td class="leftPad">
                                <asp:TextBox ID="txtLastName" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtEmailAddress">
                        <span class="moduleLabel"><%=Resources.Event_Admin.Event_AddEditApplicants_EmailAddress%>:</span><span class="requiredStar">*</span>
                    </label>
                    <asp:RequiredFieldValidator ID="reqEmailAddress" runat="server" ErrorMessage=" <%$ Resources:Event_Admin, Event_AddEditApplicants_Required %>"
                        ControlToValidate="txtEmailAddress" CssClass="errorStyle" Display="Dynamic" /><asp:RegularExpressionValidator
                            ID="regExpEmailAddress" ControlToValidate="txtEmailAddress" ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                            ErrorMessage=" <%$ Resources:Event_Admin, Event_AddEditApplicants_InvalidEmail %>" Display="dynamic" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="tb200" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtPhoneNumber">
                        <span class="moduleLabel"><%=Resources.Event_Admin.Event_AddEditApplicants_BestContactNumber%>:</span><span class="requiredStar">*</span>
                    </label>
                    <asp:RequiredFieldValidator ID="reqPhoneNumber" runat="server" ControlToValidate="txtPhoneNumber"
                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Event_Admin, Event_AddEditApplicants_Required %>" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtPhoneNumber" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdEventAddressSignup">
                    <br />
                    <uc:Address ID="ucAddress" runat="server" AddressLayout="Horizontal" AddressValidationLayout="SideBySide_AbsoluteListPosition" Required="false" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnAddEdit" runat="server" Text="" CausesValidation="true" />
        <asp:Button ID="cancelButton" runat="server" Text="<%$ Resources:Event_Admin, Event_AddEditApplicants_ButtonCancel %>" CausesValidation="false" />
        <br />
        <span class="requiredStar">*</span> <%=Resources.Event_Admin.Event_AddEditApplicants_DenotesRequired%><br />
    </div>
</asp:Content>
