<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    CodeFile="editAdd.aspx.vb" Inherits="admin_AdminUsers_editAdd" %>

<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout">
        <%= Resources.AdminUser_Admin.AdminUser_AddEdit_BodyHeading%></span><br />
    <br />
    <telerik:RadTabStrip ID="rtsAdminUser" runat="server" MultiPageID="rmpAdminUser"
        CausesValidation="true" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" PageViewID="rpvAdminUser" Value="0" Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_Tab_AdminUser_Content %>">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="rpvAdminUserAccess" Value="1" Visible="false"
                Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_Tab_AdminUser_Access %>">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpAdminUser" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
        <telerik:RadPageView ID="rpvAdminUser" runat="server">
            <span class="pageTitle">
                <%=Resources.AdminUser_Admin.AdminUser_AddEdit_Tab_Content_Heading%></span><br />
            <span class="callout">
                <%=Resources.AdminUser_Admin.AdminUser_AddEdit_Tab_Content_SubHeading%></span><br />
            <br />
            <div id="MainContent">
                <table cellpadding="5" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="vertical-align: top;" width="600">
                            <b>
                                <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation%></b><br />
                            <table width="100%" class="tblAdminUserInformation">
                                <tr>
                                    <td align="right" width="120">
                                        <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_Salutation%>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSalutation" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_FirstName%>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName" runat="server" MaxLength="20" Width="256px" /><span
                                            class="requiredStar">*</span>
                                        <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ErrorMessage=" <%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserInformation_RequiredMessage %>"
                                            ControlToValidate="txtFirstName" CssClass="errorStyle" Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_LastName%>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName" runat="server" MaxLength="20" Width="256px" /><span
                                            class="requiredStar">*</span>
                                        <asp:RequiredFieldValidator ID="reqLastName" runat="server" ErrorMessage=" <%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserInformation_RequiredMessage %>"
                                            ControlToValidate="txtLastName" CssClass="errorStyle" Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_EmailAddress%>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="256px" /><span class="requiredStar">*</span>
                                        <asp:RequiredFieldValidator ID="reqEmailAddress" runat="server" ErrorMessage="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserInformation_RequiredMessage%>"
                                            ControlToValidate="txtEmail" CssClass="errorStyle" Display="Dynamic" />
                                        <asp:RegularExpressionValidator ID="regExpEmailAddress" ControlToValidate="txtEmail"
                                            ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                                            ErrorMessage="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserInformation_EmailAddress_InvalidEmail%>"
                                            Display="dynamic" runat="server" />
                                        <asp:CustomValidator ID="cusValEmailAddress" runat="server" Display="Dynamic" OnServerValidate="customValEmailAddress_Validate">
                    <br />
                        <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_EmailAddress_AlreadyExists%>
                                        </asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="tdAdminUserAddress">
                                        <uc:Address ID="ucAddress" runat="server" AddressLayout="Vertical_TextLeft" AddressValidationLayout="Below"
                                            Required="false" ErrorMessage=" <%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserInformation_RequiredMessage %>" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_Phone%>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPhone" runat="server" MaxLength="20" Width="256px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_UserName%>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUserName" runat="server" MaxLength="50" Width="256px" /><span
                                            class="requiredStar">*</span>
                                        <asp:RequiredFieldValidator ID="reqUserName" runat="server" ErrorMessage=" <%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserInformation_RequiredMessage %>"
                                            ControlToValidate="txtUserName" CssClass="errorStyle" Display="Dynamic" />
                                        <asp:CustomValidator ID="cusValUserName" runat="server" Display="Dynamic" OnServerValidate="cusValUserName_Validate">
                                    <br />
                                    <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_Username_AlreadyExists %>
                                        </asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_Password%>:
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="upPassword" runat="server">
                                            <ContentTemplate>
                                                <div id="divPassword_PlaceHolder" runat="server" class="divPasswordChanger">
                                                    <b><asp:Literal ID="litPassword" runat="server" Text="**********" /></b><asp:LinkButton
                                                        ID="lnkResetPassword" runat="server" CausesValidation="false" Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserInformation_PasswordReset %>" />
                                                </div>
                                                <div id="divPassword_Reset" runat="server"  class="divPasswordChanger">
                                                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" Width="256px" /><span
                                                        class="requiredStar">*</span>
                                                    <asp:RequiredFieldValidator ID="reqPassword" runat="server" ErrorMessage=" <%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserInformation_RequiredMessage %>"
                                                        ControlToValidate="txtPassword" CssClass="errorStyle" Display="Dynamic" />&nbsp;<asp:LinkButton
                                                            ID="lnkResetPasswordCancel" runat="server" CausesValidation="false" Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserInformation_PasswordResetCancel %>" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_ExpirationDate%>:
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="dtExpirationDate" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_LoginLimit%>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLoginLimit" runat="server" MaxLength="9" Width="256px" /><span
                                            class="requiredStar">*</span>
                                        <asp:RequiredFieldValidator ID="reqLoginLimit" runat="server" ErrorMessage=" <%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserInformation_RequiredMessage %>"
                                            ControlToValidate="txtLoginLimit" CssClass="errorStyle" Display="Dynamic" />
                                        <asp:CompareValidator ID="compareIntLoginLimit" runat="server" ErrorMessage=" <%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserInformation_LoginLimit_NumberRequired %>"
                                            ControlToValidate="txtLoginLimit" Operator="DataTypeCheck" Type="Integer" CssClass="errorStyle"
                                            Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_IpAddress%>:
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtIpAddress" runat="server" MaxLength="20" Width="256px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_Active%>:
                                    </td>
                                    <td valign="top">
                                        <asp:CheckBox ID="chkActive" runat="server" />
                                        <small><font face="Arial">
                                            <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_ActiveMessage%></font></small>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_Language%>:
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlLanguage" runat="server" />
                                        <span class="requiredStar">*</span>
                                        <asp:RequiredFieldValidator ID="reqLanguage" runat="server" ControlToValidate="ddlLanguage"
                                            InitialValue="" CssClass="errorStyle" Display="Dynamic" ErrorMessage=" <%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserInformation_RequiredMessage %>" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_AccessLevel%>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAccessLevel" runat="server" Width="262px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserInformation_Notes%>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="4" Width="256px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="300" style="vertical-align: top;">
                            <div id="divAdminUserPermissions" runat="server" visible="false">
                                <table class="tblAdminUserPermissions">
                                    <tr id="trSiteAccess" runat="server" visible="false">
                                        <td colspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="2">
                                                        <label for="rblSite">
                                                            <b>
                                                                <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AssociateWithSite%>:</b></label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblSite" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                            <asp:ListItem Selected="True" Value="True" Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AssociateWithSite_ThisSiteOnly %>"></asp:ListItem>
                                                            <asp:ListItem Value="False" Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AssociateWithSite_AllSites %>"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <b>
                                    <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserPermissions%></b><br />
                                <table class="tblAdminUserPermissions">
                                    <tr>
                                        <td align="right">
                                            <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserPermissions_ViewWebContent%>:
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblWebContent" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_ViewWebContentYes %>"
                                                    Value="True" />
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_ViewWebContentNo %>"
                                                    Value="False" Selected="True" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserPermissions_SectionAdd%>:&nbsp;
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblSectionAdd" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_SectionAddYes %>"
                                                    Value="True" />
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_SectionAddNo %>"
                                                    Value="False" Selected="True" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserPermissions_PageAdd%>:&nbsp;
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblPageAdd" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_PageAddYes %>"
                                                    Value="True" />
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_PageAddNo %>"
                                                    Value="False" Selected="True" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserPermissions_SectionEdit%>:&nbsp;
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblSectionEdit" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_SectionEditYes %>"
                                                    Value="True" />
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_SectionEditNo %>"
                                                    Value="False" Selected="True" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserPermissions_PageEdit%>:&nbsp;
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblPageEdit" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_PageEditYes %>"
                                                    Value="True" />
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_PageEditNo %>"
                                                    Value="False" Selected="True" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserPermissions_SectionDelete%>:&nbsp;
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblSectionDelete" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_SectionDeleteYes %>"
                                                    Value="True" />
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_SectionDeleteNo %>"
                                                    Value="False" Selected="True" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserPermissions_PageDelete%>:&nbsp;
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblPageDelete" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_PageDeleteYes %>"
                                                    Value="True" />
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_PageDeleteNo %>"
                                                    Value="False" Selected="True" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserPermissions_SectionRename%>:&nbsp;
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblSectionRename" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_SectionRenameYes %>"
                                                    Value="True" />
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_SectionRenameNo %>"
                                                    Value="False" Selected="True" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserPermissions_PageRename%>:&nbsp;
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblPageRename" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_PageRenameYes %>"
                                                    Value="True" />
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_PageRenameNo %>"
                                                    Value="False" Selected="True" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="111" align="right">
                                            <%=Resources.AdminUser_Admin.AdminUser_AddEdit_AdminUserPermissions_PublishLive%>:&nbsp;
                                        </td>
                                        <td width="142">
                                            <asp:RadioButtonList ID="rblPublishLive" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_PublishLiveYes %>"
                                                    Value="True" />
                                                <asp:ListItem Text="<%$ Resources:AdminUser_Admin, AdminUser_AddEdit_AdminUserPermissions_PublishLiveNo %>"
                                                    Value="False" Selected="True" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <div id="divModulePermission" runat="server" visible="false">
                                    <div id="divModulePermission_List" runat="server" visible="false">
                                        <b>
                                            <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AvailableUserModules%></b>
                                        <br />
                                        <br />
                                        <asp:CheckBoxList ID="cblModulesForUser" runat="server" />
                                    </div>
                                    <div id="divModulePermission_NotAvailable" runat="server" visible="false">
                                        <b class="importantStyle">
                                            <%= Resources.AdminUser_Admin.AdminUser_AddEdit_AvailableUserModules_NotAvailable%></b>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvAdminUserAccess" runat="server">
            <span class="callout">
                <%=Resources.AdminUser_Admin.AdminUser_AddEdit_Tab_AdminUser_Access_Heading%></span><br />
            <%=Resources.AdminUser_Admin.AdminUser_AddEdit_Tab_AdminUser_Access_SubHeading%><br />
            <br />
            <span class="moduleLabel">
                <%=Resources.AdminUser_Admin.AdminUser_AddEdit_Tab_AdminUser_Access_Sites%></span>
            <div id="divMasterAdministrator" runat="server" visible="false" class="requiredStar">
                <%=Resources.AdminUser_Admin.AdminUser_AddEdit_Tab_AdminUser_Access_Sites_MasterAdministrator%>
            </div>
            <asp:CheckBoxList ID="cblSiteList" runat="server">
            </asp:CheckBoxList>
		<br/>
            <div id="divAllAdminUserAccess" runat="server" visible="false">
                Allow User to View Other Admin Users: <asp:CheckBox ID="chkAllAdminUserView" runat="server" />
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" CausesValidation="true" />
    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Member_Admin, Member_AddEdit_Member_ButtonCancel%>"
        CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span>
    <%=Resources.AdminUser_Admin.AdminUser_AddEdit_DenotesRequired%>
</asp:Content>
