<%@ Page Title="Rollback" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="Preview.aspx.vb" Inherits="admin_modules_event_preview" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="GoogleMap" Src="~/UserController/GoogleMap.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" href="/admin/css/RichTemplate.css" />
    <asp:Panel runat="server" ID="pnlPreview" Width="100%">
        <div style="padding: 10px">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <fieldset class="infoPanel">
                            <div id="divExpired" runat="server" visible="false" class="errorStyle fRight">
                                <h3 class="floatL">
                                    <%= Resources.Event_Admin.Event_Preview_Expired%></h3>
                                <div class="floatL leftPad">
                                    <img src='/admin/images/expired.png' />
                                </div>
                            </div>
                            <b>
                                <%= Resources.Event_Admin.Event_Preview_InformationBox_Version%></b>:
                            <asp:Literal ID="litInformationBox_Version" runat="server" /><br />
                            <b>
                                <%= Resources.Event_Admin.Event_Preview_InformationBox_DateCreated%></b>:
                            <asp:Literal ID="litInformationBox_DateCreated" runat="server" /><br />
                            <b>
                                <%= Resources.Event_Admin.Event_Preview_InformationBox_Author%></b>:
                            <asp:Literal ID="litInformationBox_AuthorName" runat="server" /><br />
                            <b>
                                <%= Resources.Event_Admin.Event_Preview_InformationBox_Category%></b>:
                            <asp:Literal ID="litInformationBox_Category" runat="server" /><br />
                            <b>
                                <%= Resources.Event_Admin.Event_Preview_InformationBox_Status%></b>:
                            <asp:Literal ID="litInformationBox_Status" runat="server" /><br />
                            <div id="divInformationBox_PublicationDate" runat="server" visible="false">
                                <b>
                                    <%= Resources.Event_Admin.Event_Preview_InformationBox_PublicationDate%></b>:
                                <asp:Literal ID="litInformationBox_PublicationDate" runat="server" />
                            </div>
                            <div id="divInformationBox_ExpirationDate" runat="server" visible="false">
                                <b>
                                    <%= Resources.Event_Admin.Event_Preview_InformationBox_ExpirationDate%></b>:
                                <asp:Literal ID="litInformationBox_ExpirationDate" runat="server" />
                            </div>
                            <div id="divInformationBox_Summary" runat="server" visible="false">
                                <b>
                                    <%= Resources.Event_Admin.Event_Preview_InformationBox_Summary%></b>:<br />
                                <asp:Literal ID="litInformationBox_Summary" runat="server" /></div>
                        </fieldset>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divExternalEvent" runat="server" visible="false">
                            <b><span class="errorStyle">
                                <%= Resources.Event_Admin.Event_Preview_ExternalEvent%></span></b>:<br />
                            <asp:Literal ID="litExternalURL" runat="server" /><br />
                            <br />
                            <%= Resources.Event_Admin.Event_Preview_ExternalEvent_NotChoosenPreview%>
                        </div>
                        <br />
                        <hr />
                        <br />
                        <div class="divModuleDetail" runat="server">
                            <div class="floatL moduleLargeTitle">
                                <asp:Literal ID="litTitle" runat="server" />
                            </div>
                            <br class="cBoth" />
                            <div id="divEventItem" runat="server" class="item">
                                <div class="Date">
                                    <i>
                                        <asp:Literal ID="litEventDateTime" runat="server" />
                                    </i>
                                </div>
                                <asp:Literal ID="litBody" runat="server" />
                                <div id="divContactPerson" runat="server" visible="false" class="clear">
                                    <br />
                                        <%=Resources.Event_FrontEnd.Event_EventDetail_ContactPerson%>:
                                    <asp:Literal ID="litContactPerson" runat="server" />
                                </div>
                                <br />
                                <asp:Literal ID="litLocation" runat="server" Visible="false" /><br />
                                <uc:GoogleMap ID="ucGoogleMap" runat="server" Width="300px" Height="300px" />
                                <br />
                                <div class="floatL">
                                    <%=Resources.Event_Admin.Event_Preview_PostedBy%>:
                                    <asp:Literal ID="litPostedBy" runat="server" />
                                    -
                                    <asp:Literal ID="litViewDate" runat="server" />
                                    <%=Resources.Event_Admin.Event_Preview_PostedBy_DateTimeSeperator%>
                                    <asp:Literal ID="litViewDateTime" runat="server" /></div>
                                <div id="divModuleSearchTagList" runat="server" class="divModuleSearchTagList" visible="false">
                                    <br />
                                    <%=Resources.Event_Admin.Event_Preview_SearchTagLabel%>:
                                    <asp:Repeater ID="rptSearchTags" runat="server">
                                        <ItemTemplate>
                                            <a href='#'>
                                                <%# Eval("searchTagName") %></a>
                                        </ItemTemplate>
                                        <SeparatorTemplate>
                                            ,
                                        </SeparatorTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <br class="cBoth" />
                            <asp:Panel ID="signUpPanel" runat="server" Visible="false">
                                <a id="signup" name="signup"></a>
                                <br />
                                <fieldset>
                                    <div class="divSignupHeading">
                                        <div>
                                            <img src="/images/calendar.png" />
                                        </div>
                                        <div>
                                            <b>
                                                <%=Resources.Event_Admin.Event_Preview_RegisterForEvent%></b>
                                        </div>
                                    </div>
                                    <br class="cBoth" />
                                    <table>
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td colspan="2">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td class="leftPad">
                                                            <span class="moduleLabel">
                                                                <%=Resources.Event_Admin.Event_Preview_RegisterForEvent_FirstName%></span><span class="errorStyle">
                                                                    *</span><asp:RequiredFieldValidator ID="reqFirstName" runat="server" ErrorMessage=" <%$ Resources:Event_Admin, Event_Preview_RegisterForEvent_RequiredMessage %>"
                                                                        ControlToValidate="txtFirstName" Display="Dynamic" ValidationGroup="onlineSignup" />
                                                        </td>
                                                        <td class="leftPad">
                                                            <span class="moduleLabel">
                                                                <%=Resources.Event_Admin.Event_Preview_RegisterForEvent_LastName%></span><span class="errorStyle">
                                                                    *</span><asp:RequiredFieldValidator ID="reqLastName" runat="server" ErrorMessage=" <%$ Resources:Event_Admin, Event_Preview_RegisterForEvent_RequiredMessage %>"
                                                                        ControlToValidate="txtLastName" Display="Dynamic" ValidationGroup="onlineSignup" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSalutation" runat="server" />
                                                        </td>
                                                        <td class="leftPad">
                                                            <asp:TextBox ID="txtFirstName" runat="server" Width="100px" />
                                                        </td>
                                                        <td class="leftPad">
                                                            <asp:TextBox ID="txtLastName" runat="server" Width="100px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <span class="moduleLabel">
                                                    <%=Resources.Event_Admin.Event_Preview_RegisterForEvent_EmailAddress%></span><span
                                                        class="errorStyle"> *</span><asp:RequiredFieldValidator ID="reqEmailAddress" runat="server"
                                                            ErrorMessage=" <%$ Resources:Event_Admin, Event_Preview_RegisterForEvent_RequiredMessage %>"
                                                            ControlToValidate="txtEmailAddress" ValidationGroup="onlineSignup" Display="Dynamic" />
                                                <asp:RegularExpressionValidator ID="regEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                                                    CssClass="errorStyle" Display="Dynamic" SetFocusOnError="True" ErrorMessage=" <%$ Resources:Event_Admin, Event_Preview_RegisterForEvent_InvalidEmail %>"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="onlineSignup" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtEmailAddress" runat="server" Width="180" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <span class="moduleLabel">
                                                    <%=Resources.Event_Admin.Event_Preview_RegisterForEvent_BestContactPhone%></span><span
                                                        class="errorStyle"> *</span><asp:RequiredFieldValidator ID="reqPhoneNumber" runat="server"
                                                            ErrorMessage=" <%$ Resources:Event_Admin, Event_Preview_RegisterForEvent_RequiredMessage %>"
                                                            ControlToValidate="txtPhoneNumber" ValidationGroup="onlineSignup" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtPhoneNumber" runat="server" Width="180"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdEventAddressSignup">
                                                <br />
                                                <uc:Address ID="ucAddress" runat="server" AddressLayout="Horizontal" AddressValidationLayout="Below" Required="false" />
                                            </td>
                                        </tr>
                                        <tr id="trRadCaptcha" runat="server" visible="false">
                                            <td>
                                                <br />
                                                <asp:Panel ID="pnlRadCaptcha" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <span class="errorStyle">*</span><%=Resources.Event_Admin.Event_Preview_RegisterForEvent_CaptchaCode_Instructions%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadCaptcha ID="radCaptchaOnlineSignup" runat="server" ErrorMessage=" <%$ Resources:Event_Admin, Event_Preview_RegisterForEvent_RequiredMessage %>"
                                                                    Display="Dynamic" CaptchaImage-LineNoise="Low" ValidationGroup="onlineSignup"
                                                                    CaptchaTextBoxLabel="" CaptchaImage-TextChars="Letters">
                                                                </telerik:RadCaptcha>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <asp:Button ID="btn_Signup" runat="server" Text="<%$ Resources:Event_Admin, Event_Preview_RegisterForEvent_ButtonSignUp %>"
                                                    CausesValidation="true" ValidationGroup="onlineSignup" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <br />
                                <br />
                            </asp:Panel>
                            <asp:Panel ID="confirmationPanel" runat="server" Visible="false">
                                <fieldset style="width: 500px">
                                    <div class="divSignupHeading">
                                        <div>
                                            <img src="/images/calendar.png" />
                                        </div>
                                        <div>
                                            <b>
                                                <%=Resources.Event_Admin.Event_Preview_RegisterForEvent_ThankYou_Line1%></b>
                                        </div>
                                    </div>
                                    <br class="cBoth" />
                                    <%=Resources.Event_Admin.Event_Preview_RegisterForEvent_ThankYou_Line2%>
                                    <br />
                                    <br />
                                    <a id="aReturnToEventDetail" runat="server">
                                        <%=Resources.Event_Admin.Event_Preview_RegisterForEvent_ReturnToEventDetail%></a>
                                </fieldset>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <hr />
                        <br />
                        <br />
                        <asp:Button ID="btnRollBack" runat="server" Text="<%$ Resources:Event_Admin, Event_Preview_ButtonRollback %>"
                            OnClick="btnRollBack_OnClick" />
                        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Event_Admin, Event_Preview_ButtonCancel %>" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlConfirmation" Visible="false">
        <table>
            <tr>
                <td>
                    <span class="pageTitle">
                        <%=Resources.Event_Admin.Event_Preview_RollBackComplete_Heading%></span><br />
                    <span class="callout">
                        <%=Resources.Event_Admin.Event_Preview_RollBackComplete_Body%></span><br />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnClose" runat="server" Text="<%$ Resources:Event_Admin, Event_Preview_ButtonClose %>" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
