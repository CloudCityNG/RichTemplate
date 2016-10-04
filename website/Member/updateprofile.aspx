<%@ Page Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="updateprofile.aspx.vb" Inherits="Member_UpdateProfile" %>

<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function valMemberImage(source, arguments) {
            arguments.IsValid = getRadUpload('<%= RadUploadMemberImage.ClientID %>').validateExtensions();
        }
    </script>
    <div class="divModuleContent">
        <table>
            <tr>
                <td>
                    <asp:Panel ID="updateProfilePanel" runat="server" Width="100%">
                        <div id="divActiveDirectoryMemberMessage" runat="server" visible="false">
                            <b>
                                <%= Resources.Member_FrontEnd.Member_UpdateProfile_ActiveDirectoryMember_Message %></b>
                        </div>
                        <table style="width: 850px">
                            <tr>
                                <td style="height: 18px;">
                                    <div class="box">
                                        <table>
                                            <tr id="trSalutation" runat="server">
                                                <td style="width: 130px">
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_Salutation%>:
                                                </td>
                                                <td>
                                                    <div id="divSalutation_ReadOnly" runat="server" visible="false">
                                                        <b><asp:Literal ID="litSalutation" runat="server" /></b></div>
                                                    <div id="divSalutation" runat="server">
                                                        <asp:DropDownList ID="ddlSalutation" runat="server" />
                                                        <span class="errorStyle">*</span>
                                                        <asp:RequiredFieldValidator ID="reqSalutation" runat="server" ControlToValidate="ddlSalutation"
                                                            CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px">
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_FirstName%>:
                                                </td>
                                                <td>
                                                    <div id="divFirstName_ReadOnly" runat="server" visible="false">
                                                        <b><asp:Literal ID="litFirstName" runat="server" /></b></div>
                                                    <div id="divFirstName" runat="server">
                                                        <asp:TextBox ID="FirstName" runat="server" Width="174px"></asp:TextBox><span class="errorStyle">
                                                            *</span>
                                                        <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ControlToValidate="FirstName"
                                                            CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px">
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_LastName%>:
                                                </td>
                                                <td>
                                                    <div id="divLastName_ReadOnly" runat="server" visible="false">
                                                        <b><asp:Literal ID="litLastName" runat="server" /></b></div>
                                                    <div id="divLastName" runat="server">
                                                        <asp:TextBox ID="LastName" runat="server" Width="174px"></asp:TextBox><span class="errorStyle">
                                                            *</span>
                                                        <asp:RequiredFieldValidator ID="reqLastName" runat="server" ControlToValidate="LastName"
                                                            CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px">
                                                    <span class="floatL" style="padding-top: 2px;">
                                                        <%= Resources.Member_FrontEnd.Member_UpdateProfile_EmailAddress%>:</span>
                                                </td>
                                                <td>
                                                    <div id="divEmailAddress_ReadOnly" runat="server" visible="false">
                                                        <b><asp:Literal ID="litEmailAddress" runat="server" /></b></div>
                                                    <div id="divEmailAddress" runat="server">
                                                        <asp:TextBox ID="email" runat="server" Width="190px"></asp:TextBox><span class="errorStyle">
                                                            *</span>
                                                        <asp:RequiredFieldValidator ID="reqEmailAddress" runat="server" ControlToValidate="email"
                                                            CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="email"
                                                            CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_EmailAddress_InvalidEmail %>"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="True" />
                                                        <asp:CustomValidator ID="cusValEmailAddress" runat="server" Display="Dynamic" OnServerValidate="customValEmailAddress_Validate">
                                                        <br />
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_EmailAddress_AlreadyExists%>
                                                        </asp:CustomValidator></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_Office%>:
                                                </td>
                                                <td>
                                                    <div id="divOffice_ReadOnly" runat="server" visible="false">
                                                        <b><asp:Literal ID="litOffice" runat="server" /></b>
                                                    </div>
                                                    <div id="divOffice" runat="server">
                                                        <asp:TextBox ID="txtOffice" runat="server" Width="174px"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="divMemberAddress">
                                            <uc:Address ID="ucAddress" runat="server" AddressLayout="Vertical_TextLeft" AddressValidationLayout="SideBySide_AbsoluteListPosition"
                                                Required="false" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_RequiredMessage %>" />
                                        </div>
                                        <table>
                                            <tr>
                                                <td style="width: 130px">
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_PhoneDaytime%>:
                                                </td>
                                                <td>
                                                    <div id="divDaytimePhone_ReadOnly" runat="server" visible="false">
                                                        <b><asp:Literal ID="litDaytimePhone" runat="server" /></b>
                                                    </div>
                                                    <div id="divDaytimePhone" runat="server">
                                                        <asp:TextBox ID="daytimePhone" runat="server" Width="100px"></asp:TextBox><span class="errorStyle">
                                                            *</span>
                                                        <asp:RequiredFieldValidator ID="reqDaytimePhone" runat="server" ControlToValidate="daytimePhone"
                                                            CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_PhoneEvening%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="eveningPhone" runat="server" Width="100px"></asp:TextBox><span class="errorStyle">
                                                        *</span>
                                                    <asp:RequiredFieldValidator ID="reqEveningPhone" runat="server" ControlToValidate="eveningPhone"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_Company%>:
                                                </td>
                                                <td>
                                                    <div id="divCompany_ReadOnly" runat="server" visible="false">
                                                        <b><asp:Literal ID="litCompany" runat="server" /></b>
                                                    </div>
                                                    <div id="divCompany" runat="server">
                                                        <asp:TextBox ID="txtCompany" runat="server" Width="120px"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_Department%>:
                                                </td>
                                                <td>
                                                    <div id="divDepartment_ReadOnly" runat="server" visible="false">
                                                        <b><asp:Literal ID="litDepartment" runat="server" /></b>
                                                    </div>
                                                    <div id="divDepartment" runat="server">
                                                        <asp:TextBox ID="txtDepartment" runat="server" Width="120px"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_Title%>:
                                                </td>
                                                <td>
                                                    <div id="divTitle_ReadOnly" runat="server" visible="false">
                                                        <b><asp:Literal ID="litTitle" runat="server" /></b>
                                                    </div>
                                                    <div id="divTitle" runat="server">
                                                        <asp:TextBox ID="txtTitle" runat="server" Width="120px"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <span class="floatL" style="padding-top: 2px;">
                                                        <%= Resources.Member_FrontEnd.Member_UpdateProfile_Language%>:</span>
                                                </td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlLanguage" runat="server" Width="155" />
                                                    <span class="errorStyle">*</span>
                                                    <asp:RequiredFieldValidator ID="reqLanguage" runat="server" ControlToValidate="ddlLanguage"
                                                        InitialValue="" CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_RequiredMessage %>" />
                                                </td>
                                            </tr>
                                            <tr id="trPasswordNew" runat="server">
                                                <td style="height: 23px;">
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_PasswordNew%>:
                                                </td>
                                                <td style="height: 23px">
                                                    <asp:TextBox ID="txtPwd" runat="server" Width="174px" TextMode="Password"></asp:TextBox><span
                                                        class="errorStyle"> *</span> <span class="grayTextSml_11">(<%= Resources.Member_FrontEnd.Member_UpdateProfile_Password_CharacterRule%>)</span>
                                                    <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPwd"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="regPassword" runat="server" ControlToValidate="txtPwd"
                                                        CssClass="errorStyle" Display="dynamic" ValidationExpression="[^\s]{6,12}"><%= Resources.Member_FrontEnd.Member_UpdateProfile_Password_CharacterRuleInvalid%></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr id="trPasswordConfirm" runat="server">
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_PasswordConfirm%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPasswordCfm" runat="server" Width="174px" TextMode="Password"></asp:TextBox><span
                                                        class="errorStyle"> *</span>
                                                    <asp:RequiredFieldValidator ID="reqConfirmPassword" runat="server" ControlToValidate="txtPasswordCfm"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="compConfirmPassword" runat="server" ControlToCompare="txtPwd"
                                                        ControlToValidate="txtPasswordCfm" CssClass="errorStyle" Display="Dynamic"><%= Resources.Member_FrontEnd.Member_UpdateProfile_Password_NotEqual%></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr id="trSecurityQuestion" runat="server">
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_SecurityQuestion%>:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlsecurityQuestion" runat="server">
                                                    </asp:DropDownList>
                                                    <span class="errorStyle">*</span>
                                                </td>
                                            </tr>
                                            <tr id="trSecurityAnswer" runat="server">
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_SecurityAnswer%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="securityAnswer" runat="server" Width="174"></asp:TextBox><span class="errorStyle">
                                                        *</span>
                                                    <asp:RequiredFieldValidator ID="reqSecurityAnswer" runat="server" ControlToValidate="securityAnswer"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <b>
                                                                    <%=Resources.Member_FrontEnd.Member_UpdateProfile_UploadImage%>:</b>
                                                                <br />
                                                                <span class="grayTextSml_11">
                                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_UploadImage_Message%><br />
                                                                    <%= Resources.Member_FrontEnd.Member_UpdateProfile_UploadImage_Requirements%></span>
                                                                <asp:CustomValidator ID="customValMemberImage" runat="server" Display="Dynamic" ClientValidationFunction="valMemberImage"
                                                                    CssClass="errorStyle">
                                                        <br />
                                                        <b><%= Resources.Member_FrontEnd.Member_UpdateProfile_UploadImage_InvalidFileType%></b>
                                                                </asp:CustomValidator>
                                                                <asp:CustomValidator ID="customValMemberImageSizeExceeded" runat="server" Display="Dynamic"
                                                                    OnServerValidate="customValMemberImageSizeExceeded_Validate" CssClass="errorStyle">
                                                        <br />
                                                        <b><%= Resources.Member_FrontEnd.Member_UpdateProfile_UploadImage_MaxFileSizeExceeded%></b>
                                                                </asp:CustomValidator>
                                                                <br />
                                                                <br />
                                                                <telerik:RadUpload ID="RadUploadMemberImage" runat="server" AllowedFileExtensions=".jpg,.gif,.png"
                                                                    ControlObjectsVisibility="None" InitialFileInputsCount="1">
                                                                </telerik:RadUpload>
                                                                <telerik:RadProgressManager ID="RadProgressManagerMember" runat="server" />
                                                                <telerik:RadProgressArea ID="RadProgressAreaMember" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadBinaryImage ID="memberImage" runat="server" CssClass="rightPad" Visible="false" /><br />
                                                                <asp:LinkButton ID="lnkDeleteImage" runat="server" Visible="false" OnClick="lnkDeleteImage_Click"
                                                                    CausesValidation="false"><b><%= Resources.Member_FrontEnd.Member_UpdateProfile_UploadImage_Delete%></b></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <asp:Button ID="btnUpdateProfile" runat="server" Text="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_ButtonUpdate %>" />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Member_FrontEnd, Member_UpdateProfile_ButtonCancel %>"
                                            OnClick="btnCancel_Click" CausesValidation="false" /><br />
                                        <span class="errorStyle">*</span>
                                        <%= Resources.Member_FrontEnd.Member_UpdateProfile_DenotesRequired%><br />
                                        <br />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
