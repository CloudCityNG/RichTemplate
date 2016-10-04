<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EventDetail.ascx.vb"
    Inherits="Event_EventDetail" %>
<%@ Register TagPrefix="uc" TagName="CommentsModule" Src="~/UserController/CommentsModule.ascx" %>
<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="GoogleMap" Src="~/UserController/GoogleMap.ascx" %>
<div class="divModuleDetail">
    <div class="floatL moduleLargeTitle">
        <asp:Literal ID="litTitle" runat="server" />
    </div>
    <div id="divEditEvent" runat="server" class="leftPad floatL" visible="false">
        <a class="aModuleEdit" href="SaveEvent.aspx?id=<%= Request.Params("id") %>">
            <img src="/images/icon_edit.gif" alt="edit" /></a>
    </div>
    <br class="cBoth" />
    <div id="divEventItem" runat="server" class="item">
        <div class="Date">
            <i><asp:Literal ID="litEventDateTime" runat="server" Visible="false" /></i>
        </div>
        <asp:Literal ID="litBody" runat="server" />
        <div id="divContactPerson" runat="server" visible="false" class="clear topPad">
            <b>
                <%=Resources.Event_FrontEnd.Event_EventDetail_ContactPerson%></b>: <asp:Literal ID="litContactPerson"
                    runat="server" />
        </div>
        <div id="divLocation" runat="server" visible="false" class="topPad">
            <asp:Literal ID="litLocation" runat="server" />
        </div>
        <div id="divGoogleMap" runat="server" visible="false" class="topPad">
            <uc:GoogleMap ID="ucGoogleMap" runat="server" Width="300px" Height="300px" />
        </div>
        <div class="topPad floatL">
            <%=Resources.Event_FrontEnd.Event_EventDetail_PostedBy%>: <asp:Literal ID="litPostedBy"
                runat="server" /> - <asp:Literal ID="litViewDate" runat="server" />
            <%=Resources.Event_FrontEnd.Event_EventDetail_PostedBy_DateTimeSeperator%>
            <asp:Literal ID="litViewDateTime" runat="server" /></div>
        <div id="divModuleSearchTagList" runat="server" class="divModuleSearchTagList cBoth topPad" visible="false">
            <%=Resources.Event_FrontEnd.Event_EventDetail_SearchTagLabel%>:
            <asp:Repeater ID="rptSearchTags" runat="server">
                <ItemTemplate>
                    <a href='<%# "Default.aspx?sTag=" & Eval("searchTagName") & If(request.querystring("archive") = 1, "&archive=1","") %>'>
                        <%# Eval("searchTagName") %></a>
                </ItemTemplate>
                <SeparatorTemplate>
                    ,
                </SeparatorTemplate>
            </asp:Repeater>
        </div>
        <br />
        <br />
        <uc:CommentsModule ID="ucCommentsModule" runat="server" />
        <asp:PlaceHolder ID="plcAddThis" runat="server" Visible="false">
            <br />
            <!-- AddThis Button BEGIN -->
            <div class="addthis_toolbox addthis_default_style">
                <a href="http://www.addthis.com/bookmark.php" class="addthis_button" style="text-decoration: none"
                    addthis:url="http://<%=Request.ServerVariables("http_host") %><%=Request.Path.toString() %>?id=<%# Request.Params("id") %>">
                    <img src="http://s7.addthis.com/static/btn/v2/lg-bookmark-en.gif" width="125" height="16"
                        border="0" alt="Share" /></a>
            </div>
            <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=xa-4bbf57a32e8aa403"></script>
            <!-- AddThis Button END -->
        </asp:PlaceHolder>
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
                        <%=Resources.Event_FrontEnd.Event_EventDetail_RegisterForEvent%></b>
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
                                        <%=Resources.Event_FrontEnd.Event_EventDetail_RegisterForEvent_FirstName%></span><span
                                            class="errorStyle"> *</span>
                                    <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ErrorMessage=" <%$ Resources:Event_FrontEnd, Event_EventDetail_RegisterForEvent_RequiredMessage %>"
                                        ControlToValidate="txtFirstName" Display="Dynamic" ValidationGroup="onlineSignup" />
                                </td>
                                <td class="leftPad">
                                    <span class="moduleLabel">
                                        <%=Resources.Event_FrontEnd.Event_EventDetail_RegisterForEvent_LastName%></span><span
                                            class="errorStyle"> *</span><asp:RequiredFieldValidator ID="reqLastName" runat="server"
                                                ErrorMessage=" <%$ Resources:Event_FrontEnd, Event_EventDetail_RegisterForEvent_RequiredMessage %>"
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
                            <%=Resources.Event_FrontEnd.Event_EventDetail_RegisterForEvent_EmailAddress%></span><span
                                class="errorStyle"> *</span><asp:RequiredFieldValidator ID="reqEmailAddress" runat="server"
                                    ErrorMessage=" <%$ Resources:Event_FrontEnd, Event_EventDetail_RegisterForEvent_RequiredMessage %>"
                                    ControlToValidate="txtEmailAddress" ValidationGroup="onlineSignup" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                            CssClass="errorStyle" Display="Dynamic" SetFocusOnError="True" ErrorMessage=" <%$ Resources:Event_FrontEnd, Event_EventDetail_RegisterForEvent_InvalidEmail %>"
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
                            <%=Resources.Event_FrontEnd.Event_EventDetail_RegisterForEvent_BestContactPhone%></span><span
                                class="errorStyle"> *</span><asp:RequiredFieldValidator ID="reqPhoneNumber" runat="server"
                                    ErrorMessage=" <%$ Resources:Event_FrontEnd, Event_EventDetail_RegisterForEvent_RequiredMessage %>"
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
                        <uc:Address ID="ucAddress" runat="server" AddressLayout="Horizontal" AddressValidationLayout="SideBySide_AbsoluteListPosition"
                            Required="false" />
                    </td>
                </tr>
                <tr id="trRadCaptcha" runat="server" visible="false">
                    <td>
                        <br />
                        <asp:Panel ID="pnlRadCaptcha" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <span class="errorStyle">*</span><%=Resources.Event_FrontEnd.Event_EventDetail_RegisterForEvent_CaptchaCode_Instructions%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadCaptcha ID="radCaptchaOnlineSignup" runat="server" ErrorMessage=" <%$ Resources:Event_FrontEnd, Event_EventDetail_RegisterForEvent_RequiredMessage %>"
                                            Display="Dynamic" CaptchaImage-LineNoise="Low" ValidationGroup="onlineSignup"
                                            CaptchaImage-TextChars="Letters" CaptchaTextBoxLabel="">
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
                        <asp:Button ID="btn_Signup" runat="server" Text="<%$ Resources:Event_FrontEnd, Event_EventDetail_RegisterForEvent_ButtonSignUp %>"
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
                        <%=Resources.Event_FrontEnd.Event_EventDetail_RegisterForEvent_ThankYou_Line1%></b>
                </div>
            </div>
            <br class="cBoth" />
            <%=Resources.Event_FrontEnd.Event_EventDetail_RegisterForEvent_ThankYou_Line2%>
            <br />
            <br />
            <a id="aReturnToEventDetail" runat="server">
                <%=Resources.Event_FrontEnd.Event_EventDetail_RegisterForEvent_ReturnToEventDetail%></a>
        </fieldset>
    </asp:Panel>
    <br />
    <br />
</div>
