<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="editAddMember.aspx.vb" Inherits="admin_modules_access_editAddMember" %>

<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function valMemberImage(source, arguments) {
            arguments.IsValid = getRadUpload('<%= RadUploadMemberImage.ClientID %>').validateExtensions();
        }
    </script>
    <span class="callout">
        <%= Resources.Member_Admin.Member_AddEdit_Member_BodyHeading%></span><br />
    <br />
    <telerik:RadTabStrip ID="rtsMember" runat="server" MultiPageID="rmpMember" CausesValidation="true"
        SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" PageViewID="rpvMember" Value="0" Text="<%$ Resources:Member_Admin, Member_AddEdit_Tab_Member_Content %>">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="rpvMemberAccess" Value="1" Visible="false"
                Text="<%$ Resources:Member_Admin, Member_AddEdit_Tab_Member_Access %>">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpMember" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
        <telerik:RadPageView ID="rpvMember" runat="server">
            <span class="pageTitle">
                <%=Resources.Member_Admin.Member_AddEdit_Tab_Member_Content_Heading%></span><br />
            <span class="callout">
                <%=Resources.Member_Admin.Member_AddEdit_Tab_Member_Content_SubHeading%></span><br />
            <br />
            <div id="divActiveDirectoryMemberMessage" runat="server" visible="false">
                <b>
                    <%= Resources.Member_Admin.Member_AddEdit_Member_ActiveDirectoryMember_Message %></b>
            </div>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <label for="userActive">
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_Status%>:</span></label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="status" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="True" Text="<%$ Resources:Member_Admin, Member_AddEdit_Member_StatusActive %>"></asp:ListItem>
                            <asp:ListItem Value="False" Text="<%$ Resources:Member_Admin, Member_AddEdit_Member_StatusArchive %>"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr id="trSalutation_Label" runat="server">
                                <td>
                                    <label for="userActive">
                                        <span class="moduleLabel">
                                            <%=Resources.Member_Admin.Member_AddEdit_Member_Salutation%>:</span></label>
                                </td>
                            </tr>
                            <tr id="trSalutation_Value" runat="server">
                                <td>
                                    <div id="divSalutation_ReadOnly" runat="server" visible="false">
                                        <b><asp:Literal ID="litSalutation" runat="server" /></b></div>
                                    <div id="divSalutation" runat="server">
                                        <asp:DropDownList ID="ddlSalutation" runat="server" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="firstName">
                                        <span class="moduleLabel">
                                            <%=Resources.Member_Admin.Member_AddEdit_Member_FirstName%></span><span class="requiredStar">*</span></label><asp:RequiredFieldValidator
                                                ID="reqFirstName" runat="server" ErrorMessage="<%$ Resources:Member_Admin, Member_AddEdit_Member_RequiredMessage%>"
                                                ControlToValidate="firstName" CssClass="errorStyle" Display="Dynamic" />
                                </td>
                                <td class="leftPad">
                                    <label for="lastName">
                                        <span class="moduleLabel">
                                            <%=Resources.Member_Admin.Member_AddEdit_Member_LastName%></span><span class="requiredStar">*</span></label><asp:RequiredFieldValidator
                                                ID="reqLastName" runat="server" ErrorMessage="<%$ Resources:Member_Admin, Member_AddEdit_Member_RequiredMessage%>"
                                                ControlToValidate="lastName" CssClass="errorStyle" Display="Dynamic" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divFirstName_ReadOnly" runat="server" visible="false">
                                        <b><asp:Literal ID="litFirstName" runat="server" /></b></div>
                                    <div id="divFirstName" runat="server">
                                        <asp:TextBox ID="firstName" runat="server" /></div>
                                </td>
                                <td class="leftPad">
                                    <div id="divLastName_ReadOnly" runat="server" visible="false">
                                        <b><asp:Literal ID="litLastName" runat="server" /></b></div>
                                    <div id="divLastName" runat="server">
                                        <asp:TextBox ID="lastName" runat="server" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="emailAddress">
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_EmailAddress%>:</span><span class="requiredStar">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="reqEmailAddress" runat="server" ErrorMessage="<%$ Resources:Member_Admin, Member_AddEdit_Member_RequiredMessage%>"
                            ControlToValidate="emailAddress" CssClass="errorStyle" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regExpEmailAddress" ControlToValidate="emailAddress"
                            ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                            ErrorMessage="<%$ Resources:Member_Admin, Member_AddEdit_Member_EmailAddress_InvalidEmail%>"
                            Display="dynamic" runat="server" />
                        <asp:CustomValidator ID="cusValEmailAddress" runat="server" Display="Dynamic" OnServerValidate="customValEmailAddress_Validate">
                    <br />
                        <%= Resources.Member_Admin.Member_AddEdit_Member_EmailAddress_AlreadyExists%>
                        </asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divEmailAddress_ReadOnly" runat="server" visible="false">
                            <b><asp:Literal ID="litEmailAddress" runat="server" /></b></div>
                        <div id="divEmailAddress" runat="server">
                            <asp:TextBox ID="emailAddress" runat="server" CssClass="tb200" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="ddlLocation">
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_Location%>:</span>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlLocation" runat="server" />
                    </td>
                </tr>
                <tr id="trUsernameLabel" runat="server" visible="false">
                    <td>
                        <label>
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_Username%>:</span>
                        </label>
                    </td>
                </tr>
                <tr id="trUsernameValue" runat="server" visible="false">
                    <td>
                        <b><asp:Literal ID="litUsername" runat="server" /></b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="ddlLanguage">
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_Language%>:</span><span class="requiredStar">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="reqLanguage" runat="server" ControlToValidate="ddlLanguage"
                            InitialValue="" CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_Admin, Member_AddEdit_Member_RequiredMessage%>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlLanguage" runat="server" />
                    </td>
                </tr>
                <tr id="trPassword_Label" runat="server">
                    <td>
                        <label for="password">
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_Password%>:</span><span class="requiredStar">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="reqPassword" runat="server" ErrorMessage="<%$ Resources:Member_Admin, Member_AddEdit_Member_RequiredMessage%>"
                            ControlToValidate="txtPassword" CssClass="errorStyle" Display="Dynamic" Enabled="false" />
                    </td>
                </tr>
                <tr id="trPassword_Value" runat="server">
                    <td>
                        <asp:UpdatePanel ID="upPassword" runat="server">
                            <ContentTemplate>
                                <div id="divPassword_PlaceHolder" runat="server" class="divPasswordChanger">
                                    <b><asp:Literal ID="litPassword" runat="server" Text="**********" /></b><asp:LinkButton
                                        ID="lnkResetPassword" runat="server" CausesValidation="false" Text="<%$ Resources:Member_Admin, Member_AddEdit_Member_PasswordReset %>" />
                                </div>
                                <div id="divPassword_Reset" runat="server" class="divPasswordChanger">
                                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" Width="256px" />&nbsp;<asp:LinkButton
                                        ID="lnkResetPasswordCancel" runat="server" CausesValidation="false" Text="<%$ Resources:Member_Admin, Member_AddEdit_Member_PasswordResetCancel %>" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr id="trSecurityQuestion_Label" runat="server">
                    <td>
                        <label>
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_SecutityQuestion%>:</span><span class="requiredStar">*</span>
                        </label>
                    </td>
                </tr>
                <tr id="trSecurityQuestion_Value" runat="server">
                    <td>
                        <asp:DropDownList ID="ddlsecurityQuestion" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trSecurityAnswer_Label" runat="server">
                    <td>
                        <label>
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_SecutityAnswer%>:</span><span class="requiredStar">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="reqSecurityAnswer" runat="server" ControlToValidate="securityAnswer"
                            CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Member_Admin, Member_AddEdit_Member_RequiredMessage%>"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="trSecurityAnswer_Value" runat="server">
                    <td>
                        <asp:TextBox ID="securityAnswer" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <span class="moduleLabel">
                                        <%=Resources.Member_Admin.Member_AddEdit_Member_UploadImage%>:</span>
                                    <br />
                                    <span class="graySubText">
                                        <%=Resources.Member_Admin.Member_AddEdit_Member_UploadImage_Message%><br />
                                        <%=Resources.Member_Admin.Member_AddEdit_Member_UploadImage_Requirements%></span>
                                    <asp:CustomValidator ID="customValMemberImage" runat="server" Display="Dynamic" ClientValidationFunction="valMemberImage"
                                        CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Member_Admin.Member_AddEdit_Member_UploadImage_InvalidFileType%></b>
                                    </asp:CustomValidator>
                                    <asp:CustomValidator ID="customValMemberImageSizeExceeded" runat="server" Display="Dynamic"
                                        OnServerValidate="customValMemberImageSizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Member_Admin.Member_AddEdit_Member_UploadImage_MaxFileSizeExceeded%></b>
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
                                        CausesValidation="false"><b><%=Resources.Member_Admin.Member_AddEdit_Member_UploadImage_Delete%></b></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_Office%>:</span>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divOffice_ReadOnly" runat="server" visible="false">
                            <b><asp:Literal ID="litOffice" runat="server" /></b>
                        </div>
                        <div id="divOffice" runat="server">
                            <asp:TextBox ID="txtOffice" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="tdMemberAddress">
                        <uc:Address ID="ucAddress" runat="server" AddressLayout="Vertical_TextAbove" AddressValidationLayout="SideBySide_AbsoluteListPosition"
                            Required="false" ErrorMessage="<%$ Resources:Member_Admin, Member_AddEdit_Member_RequiredMessage%>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_Company%>:</span>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divCompany_ReadOnly" runat="server" visible="false">
                            <b><asp:Literal ID="litCompany" runat="server" /></b>
                        </div>
                        <div id="divCompany" runat="server">
                            <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_Department%>:</span>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divDepartment_ReadOnly" runat="server" visible="false">
                            <b><asp:Literal ID="litDepartment" runat="server" /></b>
                        </div>
                        <div id="divDepartment" runat="server">
                            <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_Title%>:</span>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divTitle_ReadOnly" runat="server" visible="false">
                            <b><asp:Literal ID="litTitle" runat="server" /></b>
                        </div>
                        <div id="divTitle" runat="server">
                            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <label>
                                        <span class="moduleLabel">
                                            <%=Resources.Member_Admin.Member_AddEdit_Member_PhoneDaytime%>:</span>
                                    </label>
                                </td>
                                <td class="leftPad">
                                    <label>
                                        <span class="moduleLabel">
                                            <%=Resources.Member_Admin.Member_AddEdit_Member_PhoneEvening%>:</span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divDaytimePhone_ReadOnly" runat="server" visible="false">
                                        <b><asp:Literal ID="litDaytimePhone" runat="server" /></b>
                                    </div>
                                    <div id="divDaytimePhone" runat="server">
                                        <asp:TextBox ID="daytimePhone" runat="server" Width="100px"></asp:TextBox>
                                    </div>
                                </td>
                                <td class="leftPad">
                                    <asp:TextBox ID="eveningPhone" runat="server" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td>
                        <label for="groupID">
                            <span class="moduleLabel">
                                <%=Resources.Member_Admin.Member_AddEdit_Member_Groups%>:</span>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBoxList ID="cblGroupList" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvMemberAccess" runat="server">
            <span class="callout">
                <%=Resources.Member_Admin.Member_AddEdit_Tab_Member_Access_Heading%></span><br />
            <%=Resources.Member_Admin.Member_AddEdit_Tab_Member_Access_SubHeading%><br />
            <br />
            <div id="divActiveDirectoryMemberMessage_SiteAccess" runat="server" visible="false">
                <b>
                    <%= Resources.Member_Admin.Member_AddEdit_Member_ActiveDirectoryMember_Message %></b>
            </div>
            <span class="moduleLabel">
                <%=Resources.Member_Admin.Member_AddEdit_Tab_Member_Acess_Sites%></span>
            <div id="divSiteList_ReadOnly" runat="server" visible="false">
                <b><asp:Literal ID="litSiteAccess" runat="server" /></b>
            </div>
            <div id="divSiteList" runat="server">
                <asp:CheckBoxList ID="cblSiteList" runat="server">
                </asp:CheckBoxList>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" />
    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Member_Admin, Member_AddEdit_Member_ButtonCancel%>"
        CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span>
    <%=Resources.Member_Admin.Member_AddEdit_Member_DenotesRequired%>
</asp:Content>
