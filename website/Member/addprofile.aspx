<%@ Page Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="addprofile.aspx.vb" Inherits="Member_AddProfile" %>

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
                    <div class="divCategoryListContainer">
                        &nbsp;
                    </div>
                </td>
                <td>
                    <asp:Panel ID="addProfilePanel" runat="server" Width="100%">
                        <table style="width: 850px">
                            <tr>
                                <td style="height: 18px;">
                                    <div class="box">
                                        <table>
                                            <tr>
                                                <td style="width: 130px">
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_Salutation%>:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSalutation" runat="server" />
                                                    <span class="errorStyle">*</span>
                                                    <asp:RequiredFieldValidator ID="reqSalutation" runat="server" ControlToValidate="ddlSalutation"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_AddProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_FirstName%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="FirstName" runat="server" Width="174px"></asp:TextBox><span class="errorStyle">
                                                        *</span>
                                                    <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ControlToValidate="FirstName"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_AddProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_LastName%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="LastName" runat="server" Width="174px"></asp:TextBox><span class="errorStyle">
                                                        *</span>
                                                    <asp:RequiredFieldValidator ID="reqLastName" runat="server" ControlToValidate="LastName"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_AddProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_Office%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOffice" runat="server" Width="174px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="divMemberAddress">
                                            <uc:Address ID="ucAddress" runat="server" AddressLayout="Vertical_TextLeft" AddressValidationLayout="SideBySide_AbsoluteListPosition"
                                                Required="false" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_AddProfile_RequiredMessage %>" />
                                        </div>
                                        <table>
                                            <tr>
                                                <td style="width: 130px">
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_PhoneDaytime%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="daytimePhone" runat="server" Width="120px"></asp:TextBox><span class="errorStyle">
                                                        *</span>
                                                    <asp:RequiredFieldValidator ID="reqDaytimePhone" runat="server" ControlToValidate="daytimePhone"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_AddProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_PhoneEvening%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="eveningPhone" runat="server" Width="120px"></asp:TextBox><span class="errorStyle">
                                                        *</span>
                                                    <asp:RequiredFieldValidator ID="reqEveningPhone" runat="server" ControlToValidate="eveningPhone"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_AddProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_Company%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCompany" runat="server" Width="120px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_Department%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDepartment" runat="server" Width="120px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_Title%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTitle" runat="server" Width="120px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <span class="floatL" style="padding-top: 2px;">
                                                        <%= Resources.Member_FrontEnd.Member_AddProfile_EmailAddress%>:</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="email" runat="server" Width="190px"></asp:TextBox><span class="errorStyle">
                                                        *</span>
                                                    <asp:RequiredFieldValidator ID="reqEmailAddress" runat="server" ControlToValidate="email"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_AddProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="regEmailAddress" runat="server" ControlToValidate="email"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_AddProfile_EmailAddress_InvalidEmail %>"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="True" />
                                                    <asp:CustomValidator ID="cusValEmailAddress" runat="server" Display="Dynamic" OnServerValidate="customValEmailAddress_Validate">
                                    <br />
                                    <%= Resources.Member_FrontEnd.Member_AddProfile_EmailAddress_AlreadyExists%><br />
                                    <%= Resources.Member_FrontEnd.Member_AddProfile_EmailAddress_AlreadyExists_UseForgotPassword%><br />
                                    <a href="default.aspx"><%= Resources.Member_FrontEnd.Member_AddProfile_EmailAddress_AlreadyExists_TryAgain%></a>
                                                    </asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <span class="floatL" style="padding-top: 2px;">
                                                        <%= Resources.Member_FrontEnd.Member_AddProfile_Language%>:</span>
                                                </td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlLanguage" runat="server" Width="155" />
                                                    <span class="errorStyle">*</span>
                                                    <asp:RequiredFieldValidator ID="reqLanguage" runat="server" ControlToValidate="ddlLanguage"
                                                        InitialValue="" CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_AddProfile_RequiredMessage %>" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 23px;">
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_PasswordNew%>:
                                                </td>
                                                <td style="height: 23px">
                                                    <asp:TextBox ID="txtPwd" runat="server" Width="174px" TextMode="Password"></asp:TextBox><span
                                                        class="errorStyle"> *</span> <span class="grayTextSml_11">(<%= Resources.Member_FrontEnd.Member_AddProfile_Password_CharacterRule%>)</span>
                                                    <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPwd"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_AddProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="regPassword" runat="server" ControlToValidate="txtPwd"
                                                        CssClass="errorStyle" Display="dynamic" ValidationExpression="[^\s]{6,12}"><%= Resources.Member_FrontEnd.Member_AddProfile_Password_CharacterRuleInvalid%></asp:RegularExpressionValidator>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_PasswordConfirm%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPasswordCfm" runat="server" Width="174px" TextMode="Password"></asp:TextBox><span
                                                        class="errorStyle"> *</span>
                                                    <asp:RequiredFieldValidator ID="reqConfirmPassword" runat="server" ControlToValidate="txtPasswordCfm"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_AddProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="compConfirmPassword" runat="server" ControlToCompare="txtPwd"
                                                        ControlToValidate="txtPasswordCfm" CssClass="errorStyle" Display="Dynamic"><%= Resources.Member_FrontEnd.Member_AddProfile_Password_NotEqual%></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_SecurityQuestion%>:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlsecurityQuestion" runat="server">
                                                    </asp:DropDownList>
                                                    <span class="errorStyle">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_SecurityAnswer%>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="securityAnswer" runat="server" Width="174"></asp:TextBox><span class="errorStyle">
                                                        *</span>
                                                    <asp:RequiredFieldValidator ID="reqSecurityAnswer" runat="server" ControlToValidate="securityAnswer"
                                                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_FrontEnd, Member_AddProfile_RequiredMessage %>"></asp:RequiredFieldValidator>
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
                                                                    <%=Resources.Member_FrontEnd.Member_AddProfile_UploadImage%>:</b>
                                                                <br />
                                                                <span class="grayTextSml_11">
                                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_UploadImage_Message%><br />
                                                                    <%= Resources.Member_FrontEnd.Member_AddProfile_UploadImage_Requirements%></span>
                                                                <asp:CustomValidator ID="customValMemberImage" runat="server" Display="Dynamic" ClientValidationFunction="valMemberImage"
                                                                    CssClass="errorStyle">
                                                        <br />
                                                        <b><%= Resources.Member_FrontEnd.Member_AddProfile_UploadImage_InvalidFileType%></b>
                                                                </asp:CustomValidator>
                                                                <asp:CustomValidator ID="customValMemberImageSizeExceeded" runat="server" Display="Dynamic"
                                                                    OnServerValidate="customValMemberImageSizeExceeded_Validate" CssClass="errorStyle">
                                                        <br />
                                                        <b><%= Resources.Member_FrontEnd.Member_AddProfile_UploadImage_MaxFileSizeExceeded%></b>
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
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <asp:Button ID="btnAddProfile" runat="server" Text="<%$ Resources:Member_FrontEnd, Member_AddProfile_ButtonAdd %>" /><br />
                                        <span class="errorStyle">*</span>
                                        <%= Resources.Member_FrontEnd.Member_AddProfile_DenotesRequired%><br />
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
