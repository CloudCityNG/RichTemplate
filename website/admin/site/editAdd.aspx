<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    CodeFile="editAdd.aspx.vb" Inherits="admin_site_editAdd" %>

<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout">
        <%= Resources.Site_Admin.Site_AddEdit_BodyHeading%></span><br />
    <br />
    <telerik:RadTabStrip ID="rtsSite" runat="server" MultiPageID="rmpSite" CausesValidation="true"
        SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" PageViewID="rpvSite" Value="0" Text="<%$ Resources:Site_Admin, Site_AddEdit_Tab_Site_Content %>">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="rpvSiteAccess" Value="1" Visible="false"
                Text="<%$ Resources:Site_Admin, Site_AddEdit_Tab_Site_Access %>">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpSite" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
        <telerik:RadPageView ID="rpvSite" runat="server">
            <span class="callout">
                <%= Resources.Site_Admin.Site_AddEdit_Tab_Content_Heading%></span><br />
            <%= Resources.Site_Admin.Site_AddEdit_Tab_Content_SubHeading%><br />
            <br />
            <div id="MainContent">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <label for="rblStatus">
                                <span class="moduleLabel">
                                    <%= Resources.Site_Admin.Site_AddEdit_Status%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Site_Admin, Site_AddEdit_StatusActive %>" />
                                <asp:ListItem Value="False" Text="<%$ Resources:Site_Admin, Site_AddEdit_StatusArchive %>" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txt_Name">
                                <span class="moduleLabel">
                                    <%= Resources.Site_Admin.Site_AddEdit_SiteName%>:</span> <span class="requiredStar">
                                        *</span>
                                <asp:RequiredFieldValidator ID="reqSiteName" runat="server" ErrorMessage=" <%$ Resources:Site_Admin, Site_AddEdit_RequiredMessage %>"
                                    ControlToValidate="txt_Name" CssClass="errorStyle" Display="Dynamic" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txt_Name" runat="server" CssClass="tb400" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txt_SiteDescription">
                                <span class="moduleLabel">
                                    <%= Resources.Site_Admin.Site_AddEdit_SiteDescription%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadEditor ID="txt_SiteDescription" runat="server" Width="98%">
                            </telerik:RadEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txt_CompanyName">
                                <span class="moduleLabel">
                                    <%= Resources.Site_Admin.Site_AddEdit_CompanyName%>:</span><span class="requiredStar">*</span>
                            </label>
                            <asp:RequiredFieldValidator ID="reqCompanyName" runat="server" ErrorMessage=" <%$ Resources:Site_Admin, Site_AddEdit_RequiredMessage %>"
                                ControlToValidate="txt_CompanyName" CssClass="errorStyle" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txt_CompanyName" runat="server" CssClass="tb400" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txt_CompanyStatement">
                                <span class="moduleLabel">
                                    <%= Resources.Site_Admin.Site_AddEdit_CompanyStatement%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txt_CompanyStatement" runat="server" CssClass="tb400" MaxLength="500" />
                        </td>
                    </tr>
                <tr>
                    <td>
                        <label for="ddlLanguage">
                            <span class="moduleLabel">
                                <%=Resources.Site_Admin.Site_AddEdit_Language%>:</span><span class="requiredStar">*</span>
                        </label>
                        <asp:RequiredFieldValidator ID="reqLanguage" runat="server" ControlToValidate="ddlLanguage"
                            InitialValue="" CssClass="errorStyle" Display="Dynamic" ErrorMessage=" <%$ Resources:Site_Admin, Site_AddEdit_RequiredMessage %>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlLanguage" runat="server" />
                    </td>
                </tr>
                    <tr>
                        <td class="tdSiteAddress">
                            <uc:Address ID="ucAddress" runat="server" AddressLayout="Vertical_TextAbove" AddressValidationLayout="SideBySide_RelativeListPosition"
                                Required="true" ErrorMessage="<%$ Resources:Site_Admin, Site_AddEdit_RequiredMessage%>" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="txt_PhoneNumber">
                                            <span class="moduleLabel">
                                                <%= Resources.Site_Admin.Site_AddEdit_PhoneNumber%>:</span>
                                        </label>
                                    </td>
                                    <td class="leftPad">
                                        <label for="txt_FaxNumber">
                                            <span class="moduleLabel">
                                                <%= Resources.Site_Admin.Site_AddEdit_FaxNumber%>:</span>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txt_PhoneNumber" runat="server" Width="192px" />
                                    </td>
                                    <td class="leftPad">
                                        <asp:TextBox ID="txt_FaxNumber" runat="server" Width="192px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txt_EmailAddress">
                                <span class="moduleLabel">
                                    <%= Resources.Site_Admin.Site_AddEdit_EmailAddress%>:</span>
                                <asp:RegularExpressionValidator ID="regExpEmailAddress" ControlToValidate="txt_EmailAddress"
                                    ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                                    ErrorMessage="<%$ Resources:Site_Admin, Site_AddEdit_EmailAddress_InvalidEmail%>"
                                    Display="dynamic" runat="server" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txt_EmailAddress" runat="server" CssClass="tb400" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txt_DomainName">
                                <span class="moduleLabel">
                                    <%= Resources.Site_Admin.Site_AddEdit_DomainName%>:</span><span class="requiredStar">*</span>
                            </label>
                            <asp:RequiredFieldValidator ID="reqDomainName" runat="server" ErrorMessage=" <%$ Resources:Site_Admin, Site_AddEdit_RequiredMessage %>"
                                ControlToValidate="txt_DomainName" CssClass="errorStyle" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <span id="spanCautionMessageDomainName" runat="server" class="importantStyle"><%=Resources.Site_Admin.Site_AddEdit_CautionMessage%><br /></span>
                            <asp:UpdatePanel id="upChangeDomainName" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_DomainName" runat="server" CssClass="tb400" Enabled="false" />
                                    <asp:LinkButton ID="lnkAllowChangeDomainName" runat="server" Text="<%$ Resources:Site_Admin, Site_AddEdit_CautionChange %>"/>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txt_SiteIdentifier">
                                <span class="moduleLabel">
                                    <%= Resources.Site_Admin.Site_AddEdit_SiteIdentifier%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <span id="spanCautionMessageSiteIdentifier" runat="server" class="importantStyle"><%=Resources.Site_Admin.Site_AddEdit_CautionMessage%><br /></span>
                            <asp:UpdatePanel id="upChangeSiteIdentifier" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_SiteIdentifier" runat="server" CssClass="tb400" Enabled="false" />
                                    <asp:LinkButton ID="lnkAllowChangeSiteIdentifier" runat="server" Text="<%$ Resources:Site_Admin, Site_AddEdit_CautionChange %>"/>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr id="trSiteIdentifierLDAP_Label" runat="server" visible="true">
                        <td>
                            <label for="txt_SiteIdentifierLDAP">
                                <span class="moduleLabel">
                                    <%= Resources.Site_Admin.Site_AddEdit_SiteIdentifierLDAP%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr id="trSiteIdentifierLDAP_Value" runat="server" visible="true">
                        <td colspan="2">
                            <span id="spanCautionMessageSiteIdentifierLDAP" runat="server" class="importantStyle"><%=Resources.Site_Admin.Site_AddEdit_CautionMessage%><br /></span>
                            <asp:UpdatePanel id="upChangeSiteIdentifierLDAP" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_SiteIdentifierLDAP" runat="server" CssClass="tb400" Enabled="false" />
                                    <asp:LinkButton ID="lnkAllowChangeSiteIdentifierLDAP" runat="server" Text="<%$ Resources:Site_Admin, Site_AddEdit_CautionChange %>"/>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="rblGroupsAndUsersPublicSection">
                                <span class="moduleLabel">
                                    <%= Resources.Site_Admin.Site_AddEdit_GridEnableGroupsAndUsers_PublicSection%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rblGroupsAndUsersPublicSection" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Site_Admin, Site_AddEdit_GridEnableGroupsAndUsers_PublicSection_Enabled %>" />
                                <asp:ListItem Value="False" Text="<%$ Resources:Site_Admin, Site_AddEdit_GridEnableGroupsAndUsers_PublicSection_Disabled %>" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="trGroupsAndUsersMemberLabel"  runat="server" visible="false">
                        <td>
                            <label for="rblGroupsAndUsersMemberSection">
                                <span class="moduleLabel">
                                    <%= Resources.Site_Admin.Site_AddEdit_GridEnableGroupsAndUsers_MemberSection%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr id="trGroupsAndUsersMemberValue"  runat="server" visible="false">
                        <td>
                            <asp:RadioButtonList ID="rblGroupsAndUsersMemberSection" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Site_Admin, Site_AddEdit_GridEnableGroupsAndUsers_MemberSection_Enabled %>" />
                                <asp:ListItem Value="False" Text="<%$ Resources:Site_Admin, Site_AddEdit_GridEnableGroupsAndUsers_MemberSection_Disabled %>" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="trPackageType" runat="server" visible="false">
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="Category">
                                            <span class="moduleLabel">
                                                <%=Resources.Site_Admin.Site_AddEdit_PackageType%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="rcbPackageType" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="Category">
                                            <span class="moduleLabel">
                                                <%=Resources.Site_Admin.Site_AddEdit_SiteDepth%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlSiteDepth" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trThreeColumnLayout_Label" runat="server" visible="false">
                        <td>
                            <label for="rblUseThreeColumnLayout">
                                <span class="moduleLabel">
                                    <%= Resources.Site_Admin.Site_AddEdit_UseThreeColumnLayout%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr id="trThreeColumnLayout_Value" runat="server" visible="false">
                        <td>
                            <asp:RadioButtonList ID="rblUseThreeColumnLayout" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Site_Admin, Site_AddEdit_UseThreeColumnLayout_True %>" />
                                <asp:ListItem Value="False" Text="<%$ Resources:Site_Admin, Site_AddEdit_UseThreeColumnLayout_False %>" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvSiteAccess" runat="server">
            <span class="callout">
                <%=Resources.Site_Admin.Site_AddEdit_Tab_Access_Heading%></span><br />
            <%=Resources.Site_Admin.Site_AddEdit_Tab_Access_SubHeading%><br />
            <br />
            <span class="moduleLabel">
                <%=Resources.Site_Admin.Site_AddEdit_Tab_Access_AdminUsers%></span>
            <asp:CheckBoxList ID="cblAdminUserList" runat="server">
            </asp:CheckBoxList>
            <br />
            <br />
            <span class="moduleLabel">
                <%= Resources.Site_Admin.Site_AddEdit_Tab_Access_AvailableSiteModules%></span>
            <br />
            <asp:CheckBoxList ID="cblModulesForSite" runat="server" />
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" CausesValidation="true" />
    <asp:Button ID="cancelButton" runat="server" Text="<%$ Resources:Site_Admin, Site_AddEdit_ButtonCancel %>"
        CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span>
    <%= Resources.Site_Admin.Site_AddEdit_DenotesRequired%><br />
</asp:Content>
