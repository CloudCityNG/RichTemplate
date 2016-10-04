<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmploymentDetail.ascx.vb"
    Inherits="Employment_EmploymentDetail" %>
<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="CommentsModule" Src="~/UserController/CommentsModule.ascx" %>
<%@ Register TagPrefix="uc" TagName="GoogleMap" Src="~/UserController/GoogleMap.ascx" %>
<script type="text/javascript">
    function valDocument1(source, arguments) {
        arguments.IsValid = getRadUpload('<%= RadUploadDocument1.ClientID %>').validateExtensions();
    }
    function valDocument2(source, arguments) {
        arguments.IsValid = getRadUpload('<%= RadUploadDocument2.ClientID %>').validateExtensions();
    }
    function valDocument3(source, arguments) {
        arguments.IsValid = getRadUpload('<%= RadUploadDocument3.ClientID %>').validateExtensions();
    }
</script>
<div class="divModuleDetail">
    <div class="floatL moduleLargeTitle">
        <asp:Literal ID="litTitle" runat="server" />
    </div>
    <div id="divEditEmployment" runat="server" class="leftPad floatL" visible="false">
        <a class="aModuleEdit" href="SaveEmployment.aspx?id=<%= Request.Params("id") %>">
            <img src="/images/icon_edit.gif" alt="edit" /></a>
    </div>
    <br class="cBoth" />
    <div id="divEmploymentItem" runat="server" class="item">
        <div class="Date">
            <i><asp:Literal ID="litEmploymentDate" runat="server" /></i>
        </div>
        <div>
            <asp:Literal ID="litBody" runat="server" />
        </div>
        <div id="divSalaryRange" runat="server" visible="false" class="clear topPad">
            <b>
                <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_Salary%></b>: <asp:Literal
                    ID="litSalaryRange" runat="server" />
        </div>
        <div id="divClearance" runat="server" visible="false" class="clear topPad">
            <b>
                <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_Clearance%></b>:
            <asp:Literal ID="litClearance" runat="server" />
        </div>
        <div id="divContactPerson" runat="server" visible="false" class="clear topPad">
            <b>
                <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_ContactPerson %></b>:
            <asp:Literal ID="litContactPerson" runat="server" />
        </div>
        <div id="divLocation" runat="server" visible="false" class="topPad">
            <asp:Literal ID="litLocation" runat="server"/>
        </div>
        <div id="divGoogleMap" runat="server" visible="false" class="topPad">
            <uc:GoogleMap ID="ucGoogleMap" runat="server" Width="300px" Height="300px" />
        </div>
    <div class="topPad floatL">
        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_PostedBy%>: <asp:Literal
            ID="litPostedBy" runat="server" /> - <asp:Literal ID="litViewDate" runat="server" />
        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_PostedBy_DateTimeSeperator%>
        <asp:Literal ID="litViewDateTime" runat="server" /></div>
    <div id="divModuleSearchTagList" runat="server" class="divModuleSearchTagList topPad cBoth" visible="false">
        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_SearchTagLabel%>:
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
                    <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest%></b>
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
                                    <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_FirstName%></span><span
                                        class="errorStyle"> *</span><asp:RequiredFieldValidator ID="reqFirstName" runat="server"
                                            ErrorMessage=" <%$ Resources:Employment_FrontEnd, Employment_EmploymentDetail_RegisterInterest_RequiredMessage %>"
                                            ControlToValidate="txtFirstName" Display="Dynamic" ValidationGroup="onlineSignup" />
                            </td>
                            <td class="leftPad">
                                <span class="moduleLabel">
                                    <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_LastName%></span><span
                                        class="errorStyle"> *</span><asp:RequiredFieldValidator ID="reqLastName" runat="server"
                                            ErrorMessage=" <%$ Resources:Employment_FrontEnd, Employment_EmploymentDetail_RegisterInterest_RequiredMessage %>"
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
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_EmailAddress%></span><span
                            class="errorStyle"> *</span><asp:RequiredFieldValidator ID="reqEmailAddress" runat="server"
                                ErrorMessage=" <%$ Resources:Employment_FrontEnd, Employment_EmploymentDetail_RegisterInterest_RequiredMessage %>"
                                ControlToValidate="txtEmailAddress" ValidationGroup="onlineSignup" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="regEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                        CssClass="errorStyle" Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ErrorMessage=" <%$ Resources:Employment_FrontEnd, Employment_EmploymentDetail_RegisterInterest_InvalidEmail %>"
                        ValidationGroup="onlineSignup" />
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
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_BestContactPhone%></span><span
                            class="errorStyle"> *</span><asp:RequiredFieldValidator ID="reqPhoneNumber" runat="server"
                                ErrorMessage=" <%$ Resources:Employment_FrontEnd, Employment_EmploymentDetail_RegisterInterest_RequiredMessage %>"
                                ControlToValidate="txtPhoneNumber" ValidationGroup="onlineSignup" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtPhoneNumber" runat="server" Width="180"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdEmploymentAddressSignup">
                    <br />
                    <uc:Address ID="ucAddress" runat="server" AddressLayout="Horizontal" AddressValidationLayout="Below"
                        Required="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span class="moduleLabel">
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_CoverLetter%>:</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtCoverLetter" runat="server" TextMode="MultiLine" Width="400px"
                        Rows="4" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span class="moduleLabel">
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_Resume%>:</span>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadEditor ID="txtResume" runat="server" Width="98%" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span class="moduleLabel">
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_YearsExperience%>:</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtYearsExperience" runat="server" Width="200px" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span class="moduleLabel">
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_LastJobTitle%>:</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtLastTitle" runat="server" Width="200px" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span class="moduleLabel">
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_EducationLevel%>:</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtEducationLevel" runat="server" TextMode="MultiLine" Width="200px"
                        Rows="4" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span class="moduleLabel">
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_ProjectExpertise%>:</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtProjExpertise" runat="server" TextMode="MultiLine" Width="200px"
                        Rows="4" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span class="moduleLabel">
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_Skills%>:</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtSkills" runat="server" TextMode="MultiLine" Width="200px" Rows="4" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span class="moduleLabel">
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_SalaryRange%>:</span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtSalary" runat="server" Width="200px" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span class="moduleLabel">
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_PossibleStartDate%>:</span>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadDatePicker ID="startDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <span class="moduleLabel">
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_UploadDoc%>:</span><br />
                    <span class="grayText">
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_UploadDoc_Message%><br />
                        <br />
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_UploadDoc_Requirements%>
                        <br />
                        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_UploadDoc_MaxFileSize%></span>
                    <div id="divUploadDocument1" runat="server">
                        <br />
                        <telerik:RadUpload ID="RadUploadDocument1" runat="server" AllowedFileExtensions=".csv,.doc,.docx,.gif,.txt,.jpg,.mht,.pdf,.png,.ppt,.odt,.ods,.rtf,.wpd,.wps,.xls,.xlsx,.zip"
                            ControlObjectsVisibility="None" InitialFileInputsCount="1">
                        </telerik:RadUpload>
                        <asp:CustomValidator ID="customValDocument1" runat="server" Display="Dynamic" ClientValidationFunction="valDocument1"
                            CssClass="errorStyle" ValidationGroup="onlineSignup">
                    &nbsp;<b><%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_UploadDoc_InvalidFileType%></b>
                        </asp:CustomValidator>
                        <asp:CustomValidator ID="customValDocument1SizeExceeded" runat="server" Display="Dynamic"
                            OnServerValidate="customValDocument1SizeExceeded_Validate" CssClass="errorStyle"
                            ValidationGroup="onlineSignup">
                        &nbsp;<b><%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_UploadDoc_MaxFileSizeExceeded%></b>
                        </asp:CustomValidator>
                        <telerik:RadProgressManager ID="rpManagerDocument1" runat="server" />
                        <telerik:RadProgressArea ID="rpDocument1" runat="server" />
                    </div>
                    <div id="divUploadDocument2" runat="server">
                        <br />
                        <telerik:RadUpload ID="RadUploadDocument2" runat="server" AllowedFileExtensions=".csv,.doc,.docx,.gif,.txt,.jpg,.mht,.pdf,.png,.ppt,.odt,.ods,.rtf,.wpd,.wps,.xls,.xlsx,.zip"
                            ControlObjectsVisibility="None" InitialFileInputsCount="1">
                        </telerik:RadUpload>
                        <asp:CustomValidator ID="customValDocument2" runat="server" Display="Dynamic" ClientValidationFunction="valDocument2"
                            CssClass="errorStyle" ValidationGroup="onlineSignup">
				                     &nbsp;<b><%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_UploadDoc_InvalidFileType%></b>
                        </asp:CustomValidator>
                        <asp:CustomValidator ID="customValDocument2SizeExceeded" runat="server" Display="Dynamic"
                            OnServerValidate="customValDocument2SizeExceeded_Validate" CssClass="errorStyle"
                            ValidationGroup="onlineSignup">
				                        &nbsp;<b><%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_UploadDoc_MaxFileSizeExceeded%></b>
                        </asp:CustomValidator>
                        <telerik:RadProgressManager ID="rpManagerDocument2" runat="server" />
                        <telerik:RadProgressArea ID="rpDocument2" runat="server" />
                    </div>
                    <div id="divUploadDocument3" runat="server">
                        <br />
                        <telerik:RadUpload ID="RadUploadDocument3" runat="server" AllowedFileExtensions=".csv,.doc,.docx,.gif,.txt,.jpg,.mht,.pdf,.png,.ppt,.odt,.ods,.rtf,.wpd,.wps,.xls,.xlsx,.zip"
                            ControlObjectsVisibility="None" InitialFileInputsCount="1">
                        </telerik:RadUpload>
                        <asp:CustomValidator ID="customValDocument3" runat="server" Display="Dynamic" ClientValidationFunction="valDocument3"
                            CssClass="errorStyle" ValidationGroup="onlineSignup">
                     &nbsp;<b><%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_UploadDoc_InvalidFileType%></b>
                        </asp:CustomValidator>
                        <asp:CustomValidator ID="customValDocument3SizeExceeded" runat="server" Display="Dynamic"
                            OnServerValidate="customValDocument3SizeExceeded_Validate" CssClass="errorStyle"
                            ValidationGroup="onlineSignup">
                        &nbsp;<b><%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_UploadDoc_MaxFileSizeExceeded%></b>
                        </asp:CustomValidator>
                        <telerik:RadProgressManager ID="rpManagerDocument3" runat="server" />
                        <telerik:RadProgressArea ID="rpDocument3" runat="server" />
                    </div>
                </td>
            </tr>
            <tr id="trRadCaptcha_Heading" runat="server" visible="false">
                <td>
                    <span class="errorStyle">*</span><%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_CaptchaCode_Instructions%>
                </td>
            </tr>
            <tr id="trRadCaptcha_Code" runat="server" visible="false">
                <td>
                    <telerik:RadCaptcha ID="radCaptchaOnlineSignup" runat="server" ErrorMessage="<%$ Resources:Employment_FrontEnd, Employment_EmploymentDetail_RegisterInterest_CaptchaCode_RequiredMessage %>"
                        Display="Dynamic" CaptchaImage-LineNoise="Low" ValidationGroup="onlineSignup"
                        CaptchaTextBoxLabel="">
                    </telerik:RadCaptcha>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Button ID="btn_Signup" runat="server" Text="<%$ Resources:Employment_FrontEnd, Employment_EmploymentDetail_RegisterInterest_ButtonApplyNow %>"
                        CausesValidation="true" ValidationGroup="onlineSignup" />
                    <asp:CustomValidator ID="customValResumeOrDocument" runat="server" Display="Dynamic"
                        OnServerValidate="customValResumeOrDocument_Validate" CssClass="errorStyle" ValidationGroup="onlineSignup">
				                        &nbsp;<br /><b><%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_ProvideResumeOrDocument%></b>
                    </asp:CustomValidator>
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
                    <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_ThankYou_Line1%></b>
            </div>
        </div>
        <br class="cBoth" />
        <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_ThankYou_Line2%>
        <br />
        <br />
        <a id="aReturnToEmploymentDetail" runat="server">
            <%=Resources.Employment_FrontEnd.Employment_EmploymentDetail_RegisterInterest_ReturnToEmploymentDetail%></a>
    </fieldset>
</asp:Panel>
<br />
<br />
</div> 