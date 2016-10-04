<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    ValidateRequest="false" AutoEventWireup="false" CodeFile="SaveEvent.aspx.vb"
    Inherits="Event_SaveEvent" %>

<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function valEventImage(source, arguments) {
            arguments.IsValid = getRadUpload('<%= radUploadEventImage.ClientID %>').validateExtensions();
        }
    </script>
    <div class="divModuleContent">
        <table width="100%">
            <tr>
                <td>
                    <div class="divCategoryListContainer">
                        &nbsp;
                    </div>
                </td>
                <td>
                    <div class="imgLinkDiv">
                        <a id="aBack" runat="server">
                            <img src="/images/arrow_back.jpg" alt="" />&nbsp;<%=Resources.Event_FrontEnd.Event_SaveEvent_Back%></a>
                    </div>
                    <div id="divModuleContentMain" runat="server" class="divModuleContentMain">
                        <div class="floatL">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="startDate">
                                            <span class="moduleLabel">
                                                <%=Resources.Event_FrontEnd.Event_SaveEvent_StartDateTime%>:</span><span class="errorStyle">*</span>
                                            <asp:RequiredFieldValidator ID="reqStartDateTime" runat="server" ControlToValidate="startDate"
                                                CssClass="errorStyle" Display="Dynamic" ErrorMessage=" <%$ Resources:Event_FrontEnd, Event_SaveEvent_RequiredMessage %>" /></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="startDate" runat="server" />
                                        &nbsp;<telerik:RadTimePicker ID="startTime" runat="server" TimeView-HeaderText="<%$ Resources:Event_FrontEnd, Event_SaveEvent_StartDateTime_TimePicker_HeaderText %>" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="">
                                        <label for="endDate">
                                            <span class="moduleLabel">
                                                <%=Resources.Event_FrontEnd.Event_SaveEvent_EndDateTime%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="">
                                        <telerik:RadDatePicker ID="endDate" runat="server" />
                                        &nbsp;<telerik:RadTimePicker ID="endTime" runat="server" TimeView-HeaderText="<%$ Resources:Event_FrontEnd, Event_SaveEvent_EndDateTime_TimePicker_HeaderText %>" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="topPad cBoth">
                            <label for="txtTitle">
                                <span class="moduleLabel">
                                    <%=Resources.Event_FrontEnd.Event_SaveEvent_Title%>:</span><span class="errorStyle">*</span>
                                <asp:RequiredFieldValidator ID="reqTitle" runat="server" ControlToValidate="txtTitle"
                                    CssClass="errorStyle" Display="Dynamic" ErrorMessage=" <%$ Resources:Event_FrontEnd, Event_SaveEvent_RequiredMessage %>" />
                            </label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtTitle" runat="server" Width="400px" />
                        </div>
                        <div class="topPad cBoth">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="Category">
                                            <span class="moduleLabel">
                                                <%=Resources.Event_FrontEnd.Event_SaveEvent_Category%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="rcbCategoryID" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="topPad cBoth">
                            <label for="txtSummary">
                                <span class="moduleLabel">
                                    <%=Resources.Event_FrontEnd.Event_SaveEvent_ShortSummary%>:</span><span class="errorStyle">*</span><asp:RequiredFieldValidator
                                        ID="reqSummary" runat="server" ControlToValidate="txtSummary" CssClass="errorStyle"
                                        Display="Dynamic" ErrorMessage=" <%$ Resources:Event_FrontEnd, Event_SaveEvent_RequiredMessage %>" />
                            </label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtSummary" runat="server" Width="400px" Rows="4" TextMode="MultiLine" />
                        </div>
                        <div class="topPad cBoth">
                            <label for="Body">
                                <span class="moduleLabel">
                                    <%=Resources.Event_FrontEnd.Event_SaveEvent_MainContent%>:</span><span class="errorStyle">*</span><asp:RequiredFieldValidator
                                        ID="reqMainContent" runat="server" ControlToValidate="txtBody" CssClass="errorStyle"
                                        Display="Dynamic" ErrorMessage=" <%$ Resources:Event_FrontEnd, Event_SaveEvent_RequiredMessage %>" />
                            </label>
                        </div>
                        <div>
                            <telerik:RadEditor ID="txtBody" runat="server" Width="98%">
                            </telerik:RadEditor>
                        </div>
                        <div class="topPad cBoth">
                            <label for="txtContactPerson">
                                <span class="moduleLabel">
                                    <%=Resources.Event_FrontEnd.Event_SaveEvent_ContactPerson%>:</span>
                                <asp:RegularExpressionValidator ID="regExpValSendApplicationToEmail" ControlToValidate="txtContactPerson"
                                    ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                                    ErrorMessage="<%$ Resources:Event_FrontEnd, Event_SaveEvent_ContactPerson_InvalidEmail %>"
                                    Display="dynamic" runat="server" />
                            </label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtContactPerson" runat="server" Width="400px" />
                        </div>
                        <div id="divAllowOnlineSignup" runat="server" visible="false" class="topPad">
                            <label for="onlineSignup">
                                <span class="moduleLabel">
                                    <%=Resources.Event_FrontEnd.Event_SaveEvent_OnlineSignup%>:</span></label>
                            <asp:RadioButtonList ID="onlineSignup" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="True" Text="<%$ Resources:Event_FrontEnd, Event_SaveEvent_OnlineSignupYes %>"></asp:ListItem>
                                <asp:ListItem Value="False" Text="<%$ Resources:Event_FrontEnd, Event_SaveEvent_OnlineSignupNo %>"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <br />
                        <div id="divUploadImage" runat="server">
                            <span class="moduleLabel">
                                <%=Resources.Event_FrontEnd.Event_SaveEvent_UploadImage%>:</span>
                            <br />
                            <span class="grayTextSml_11">
                                <%= Resources.Event_FrontEnd.Event_SaveEvent_UploadImage_Message%><br />
                                <%= Resources.Event_FrontEnd.Event_SaveEvent_UploadImage_Requirements%>
                            </span>
                            <asp:CustomValidator ID="customValEventImage" runat="server" Display="Dynamic" ClientValidationFunction="valEventImage"
                                CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Event_FrontEnd.Event_SaveEvent_UploadImage_InvalidFileType%></b>
                            </asp:CustomValidator>
                            <asp:CustomValidator ID="customValFileSizeExceeded" runat="server" Display="Dynamic"
                                OnServerValidate="customValEventImageSizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Event_FrontEnd.Event_SaveEvent_UploadImage_MaxFileSizeExceeded%></b>
                            </asp:CustomValidator>
                            <br />
                            <br />
                            <telerik:RadUpload ID="radUploadEventImage" runat="server" AllowedFileExtensions=".jpg,.gif,.png"
                                ControlObjectsVisibility="None" InitialFileInputsCount="1">
                            </telerik:RadUpload>
                            <telerik:RadProgressManager ID="radProgressManagerEventImage" runat="server" />
                            <telerik:RadProgressArea ID="radProgressAreaEventImage" runat="server" />
                        </div>
                        <div id="divEventImage" runat="server" visible="false">
                            <telerik:RadBinaryImage ID="eventImage" runat="server" CssClass="rightPad" Visible="false" /><br />
                            <asp:LinkButton ID="lnkDeleteImage" runat="server" Visible="false" OnClick="lnkDeleteImage_Click"
                                CausesValidation="false"><b><%=Resources.Event_FrontEnd.Event_SaveEvent_UploadImage_Delete%></b></asp:LinkButton>
                        </div>
                        <div class="topPad divEventAddress">
                            <uc:Address ID="ucAddress" runat="server" AddressLayout="Horizontal" AddressValidationLayout="Below"
                                Required="false" />
                        </div>
                        <asp:TextBox ID="version" runat="server" Visible="false" />
                        <br />
                        <br />
                        <asp:Button ID="btnAddEditEvent" runat="server" OnClick="btnAddEditEvent_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Event_FrontEnd, Event_SaveEvent_ButtonCancel %>"
                            OnClick="btnCancel_Click" CausesValidation="false" />
                        <br />
                        <span class="errorStyle">*</span>
                        <%= Resources.Event_FrontEnd.Event_SaveEvent_DenotesRequired%><br />
                        <br />
                    </div>
                    <div id="divModuleContentSubmitted" runat="server" visible="false">
                        <div>
                            <h2>
                                <%=Resources.Event_FrontEnd.Event_SaveEvent_Submitted_Heading%></h2>
                            <%=Resources.Event_FrontEnd.Event_SaveEvent_Submitted_Body%><br />
                            <br />
                            <a href="Default.aspx">
                                <%=Resources.Event_FrontEnd.Event_SaveEvent_Submitted_ReturnToEventModule%></a>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
