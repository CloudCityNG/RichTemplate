<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="editAdd.aspx.vb" Inherits="admin_modules_employment_editAdd"
    ValidateRequest="false" %>

<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function valEmploymentImage(source, arguments) {
            arguments.IsValid = getRadUpload('<%= RadUploadEmploymentImage.ClientID %>').validateExtensions();
        }
    </script>
    <span class="callout">
        <%=Resources.Employment_Admin.Employment_AddEdit_BodyHeading%></span><br />
    <br />
    <telerik:RadTabStrip ID="rtsEmployment" runat="server" MultiPageID="rmpEmployment"
        SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" PageViewID="rpvEmployment" Value="0">
                <TabTemplate>
                    <%=Resources.Employment_Admin.Employment_AddEdit_Tab_Content%>
                </TabTemplate>
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Employment_Admin, Employment_AddEdit_Tab_SEO_Content %>"
                PageViewID="rpvSEO" Value="1">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Employment_Admin, Employment_AddEdit_Tab_Search_Tags %>"
                PageViewID="rpvSearchTags" Value="2">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Employment_Admin, Employment_AddEdit_Tab_Users_Groups %>"
                PageViewID="rpvUserAndGroups" Value="3" Visible="false">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Employment_Admin, Employment_AddEdit_Tab_History %>"
                PageViewID="rpvHistory" Value="4">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpEmployment" runat="server" CssClass="Windows7_MultipageBorder"
        SelectedIndex="0">
        <telerik:RadPageView ID="rpvEmployment" runat="server">
            <span class="callout">
                <%=Resources.Employment_Admin.Employment_AddEdit_Tab_Content_Heading%></span><br />
            <%=Resources.Employment_Admin.Employment_AddEdit_Tab_Content_SubHeading%><br />
            <br />
            <div id="MainContent">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <label for="Status">
                                <span class="moduleLabel">
                                    <%=Resources.Employment_Admin.Employment_AddEdit_Status%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Employment_Admin, Employment_AddEdit_StatusActive%>"></asp:ListItem>
                                <asp:ListItem Value="False" Text="<%$ Resources:Employment_Admin, Employment_AddEdit_StatusArchive%>"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="ReleaseDate">
                                            <span class="moduleLabel">
                                                <%=Resources.Employment_Admin.Employment_AddEdit_PublicationDate%>:</span></label>
                                    </td>
                                    <td class="leftPad">
                                        <label for="ExpirationDate">
                                            <span class="moduleLabel">
                                                <%=Resources.Employment_Admin.Employment_AddEdit_ExpirationDate%>:</span></label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="publicationDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                                    </td>
                                    <td class="leftPad">
                                        <telerik:RadDatePicker ID="expirationDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <span id="spanExpired" runat="server" class="errorStyle spanExpired" visible="false">
                                            <span>
                                                <img src='/admin/images/expired.png' />
                                            </span><span>
                                                <%= Resources.Employment_Admin.Employment_AddEdit_Expired%></span> </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trSiteAccess" runat="server" visible="false">
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="rblSite">
                                            <span class="moduleLabel">
                                                <%=Resources.Employment_Admin.Employment_AddEdit_AssociateWithSite%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="divAssociateWithSite" runat="server">
                                            <asp:RadioButtonList ID="rblSite" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Text="<%$ Resources:Employment_Admin, Employment_AddEdit_AssociateWithSite_ThisSiteOnly %>"
                                                    Value="false" Selected="True" />
                                                <asp:ListItem Text="<%$ Resources:Employment_Admin, Employment_AddEdit_AssociateWithSite_AllSites %>"
                                                    Value="true" />
                                            </asp:RadioButtonList>
                                        </div>
                                        <div id="divAssociateWithSite_PublicMessage" runat="server" class="divAssociateWithSite_PublicMessage"
                                            visible="false">
                                            <span>
                                                <img src='/admin/images/available_to_all.png' />
                                            </span><span class="spanPublicModuleRecord">
                                                <%=Resources.Employment_Admin.Employment_AddEdit_AssociateWithSite_PublicMessage%>:
                                                <asp:Literal ID="litSiteName" runat="server" /></span>
                                        </div>
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
                                                <%=Resources.Employment_Admin.Employment_AddEdit_Category%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="rcbCategoryID" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trOnlineSignup" runat="server" visible="false">
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="onlineSignup">
                                            <span class="moduleLabel">
                                                <%=Resources.Employment_Admin.Employment_AddEdit_AllowOnlineSignup%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="onlineSignup" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="True" Text="<%$ Resources:Employment_Admin, Employment_AddEdit_AllowOnlineSignupYes%>"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="<%$ Resources:Employment_Admin, Employment_AddEdit_AllowOnlineSignupNo%>"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtTitle">
                                <span class="moduleLabel">
                                    <%=Resources.Employment_Admin.Employment_AddEdit_Title%>:</span><span class="requiredStar">*</span>
                                <asp:RequiredFieldValidator ID="reqTitle" runat="server" ControlToValidate="txtTitle"
                                    CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Employment_Admin, Employment_AddEdit_RequiredMessage %>" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="tb400" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtSummary">
                                <span class="moduleLabel">
                                    <%=Resources.Employment_Admin.Employment_AddEdit_Summary%>:</span><span class="requiredStar">*</span><asp:RequiredFieldValidator
                                        ID="reqSummary" runat="server" ControlToValidate="txtSummary" CssClass="errorStyle"
                                        Display="Dynamic" ErrorMessage="<%$ Resources:Employment_Admin, Employment_AddEdit_RequiredMessage %>" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtSummary" runat="server" CssClass="tb400" Rows="4" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtClearance">
                                <span class="moduleLabel">
                                    <%=Resources.Employment_Admin.Employment_AddEdit_Clearance%>:</span>
                            </label>
                            <br />
                            <span class="graySubText">
                                <%=Resources.Employment_Admin.Employment_AddEdit_Clearance_Message%></span><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtClearance" runat="server" CssClass="tb400" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="135">
                                        <label for="txtSalaryMin">
                                            <span class="moduleLabel">
                                                <%=Resources.Employment_Admin.Employment_AddEdit_MinSalary%>:</span></label>
                                    </td>
                                    <td width="135" class="leftPad">
                                        <label for="txtSalaryMax">
                                            <span class="moduleLabel">
                                                <%=Resources.Employment_Admin.Employment_AddEdit_MaxSalary%>:</span></label>
                                    </td>
                                    <td>
                                        <label for="txtSalaryNote">
                                            <span class="moduleLabel">
                                                <%=Resources.Employment_Admin.Employment_AddEdit_SalaryNotes%>:</span><br />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <b>
                                            <%= New Globalization.RegionInfo(LanguageDAL.GetCurrentLanguageCode_BySiteID(SiteDAL.GetCurrentSiteID_Admin())).CurrencySymbol%></b>
                                        <asp:TextBox ID="txtSalaryMin" runat="server" Width="80px" />
                                        <asp:CompareValidator ID="valMinSalary" runat="server" ControlToValidate="txtSalaryMin"
                                            Type="Currency" ValueToCompare="0" Operator="GreaterThanEqual" Display="Dynamic"
                                            CssClass="errorStyle">
                                            <br />
                                        <%=Resources.Employment_Admin.Employment_AddEdit_MinSalary_InvalidMessage%>
                                        </asp:CompareValidator>
                                    </td>
                                    <td class="leftPad" valign="top">
                                        <b>
                                            <%= New Globalization.RegionInfo(LanguageDAL.GetCurrentLanguageCode_BySiteID(SiteDAL.GetCurrentSiteID_Admin())).CurrencySymbol%></b>
                                        <asp:TextBox ID="txtSalaryMax" runat="server" Width="80px" />
                                        <asp:CompareValidator ID="valMaxSalary" runat="server" ControlToValidate="txtSalaryMax"
                                            Type="Currency" ValueToCompare="0" Operator="GreaterThanEqual" Display="Dynamic"
                                            CssClass="errorStyle">
                                            <br />
                                        <%=Resources.Employment_Admin.Employment_AddEdit_MaxSalary_InvalidMessage%>
                                        </asp:CompareValidator>
                                    </td>
                                    <td class="leftPad" valign="top">
                                        <asp:TextBox ID="txtSalaryNote" runat="server" Width="120px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <span class="graySubText">
                                            <%=Resources.Employment_Admin.Employment_AddEdit_SalaryNotes_Message%></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <span class="moduleLabel">
                                            <%=Resources.Employment_Admin.Employment_AddEdit_UploadImage%>:</span>
                                        <br />
                                        <span class="graySubText">
                                            <%=Resources.Employment_Admin.Employment_AddEdit_UploadImage_Message%><br />
                                            <%=Resources.Employment_Admin.Employment_AddEdit_UploadImage_Requirements%></span>
                                        <asp:CustomValidator ID="customValEmploymentImage" runat="server" Display="Dynamic"
                                            ClientValidationFunction="valEmploymentImage" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Employment_Admin.Employment_AddEdit_UploadImage_InvalidFileType%></b>
                                        </asp:CustomValidator>
                                        <asp:CustomValidator ID="customValEmploymentImageSizeExceeded" runat="server" Display="Dynamic"
                                            OnServerValidate="customValEmploymentImageSizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Employment_Admin.Employment_AddEdit_UploadImage_MaxFileSizeExceeded%></b>
                                        </asp:CustomValidator>
                                        <br />
                                        <br />
                                        <telerik:RadUpload ID="RadUploadEmploymentImage" runat="server" AllowedFileExtensions=".jpg,.gif,.png"
                                            ControlObjectsVisibility="None" InitialFileInputsCount="1">
                                        </telerik:RadUpload>
                                        <telerik:RadProgressManager ID="rpManagerEmployment" runat="server" />
                                        <telerik:RadProgressArea ID="rpAreaEmployment" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadBinaryImage ID="employmentImage" runat="server" CssClass="rightPad" Visible="false" /><br />
                                        <asp:LinkButton ID="lnkDeleteImage" runat="server" Visible="false" OnClick="lnkDeleteImage_Click"
                                            CausesValidation="false"><b><%=Resources.Employment_Admin.Employment_AddEdit_UploadImage_Delete%></b></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtExternalLinkUrl">
                                <span class="moduleLabel">
                                    <%=Resources.Employment_Admin.Employment_AddEdit_ExternalWebsiteAddress%>:</span>
                                <br />
                                <span class="graySubText">
                                    <%=Resources.Employment_Admin.Employment_AddEdit_ExternalWebsiteAddress_Message%></span><br />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtExternalLinkUrl" runat="server" CssClass="tb400" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <label for="geoLocation">
                                <%=Resources.Employment_Admin.Employment_AddEdit_Geolocation%>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="geolocation" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Employment_Admin, Employment_AddEdit_GeolocationYes%>"></asp:ListItem>
                                <asp:ListItem Value="False" Text="<%$ Resources:Employment_Admin, Employment_AddEdit_GeolocationNo%>"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdEmploymentAddress">
                            <br />
                            <uc:Address ID="ucAddress" runat="server" AddressLayout="Horizontal" AddressValidationLayout="SideBySide_RelativeListPosition"
                                Required="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="Body">
                                <span class="moduleLabel">
                                    <%=Resources.Employment_Admin.Employment_AddEdit_MainContent%>:</span><span class="requiredStar">*</span><asp:RequiredFieldValidator
                                        ID="reqMainContent" runat="server" ControlToValidate="txtBody" CssClass="errorStyle"
                                        Display="Dynamic" ErrorMessage="<%$ Resources:Employment_Admin, Employment_AddEdit_RequiredMessage %>" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadEditor ID="txtBody" runat="server" Width="98%">
                            </telerik:RadEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="Link">
                                <span class="moduleLabel">
                                    <%=Resources.Employment_Admin.Employment_AddEdit_SentApplicaitionsTo%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtContactPerson" runat="server" CssClass="tb300" />
                            <asp:RegularExpressionValidator ID="regExpValSendApplicationToEmail" ControlToValidate="txtContactPerson"
                                ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                                ErrorMessage="<%$ Resources:Employment_Admin, Employment_AddEdit_InvalidEmail %>"
                                Display="dynamic" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:TextBox ID="version" runat="server" Width="35px" Visible="false" />
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvSEO" runat="server">
            <span class="callout">
                <%=Resources.Employment_Admin.Employment_AddEdit_Tab_SEO_Content_Heading%></span><br />
            <%=Resources.Employment_Admin.Employment_AddEdit_Tab_SEO_Content_SubHeading%>
            <div>
                <p>
                    <label for="metaTitle">
                        <span class="moduleLabel">
                            <%=Resources.Employment_Admin.Employment_AddEdit_Tab_SEO_Content_MetaTitle%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaTitle" runat="server" TextMode="MultiLine" Rows="3" CssClass="tb400" />
                </p>
                <p>
                    <label for="metaKeywords">
                        <span class="moduleLabel">
                            <%=Resources.Employment_Admin.Employment_AddEdit_Tab_SEO_Content_MetaKeywords%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaKeywords" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    <label for="metaDescription">
                        <span class="moduleLabel">
                            <%=Resources.Employment_Admin.Employment_AddEdit_Tab_SEO_Content_MetaDescription%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaDescription" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    &nbsp;</p>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvSearchTags" runat="server">
            <span class="callout">
                <%=Resources.Employment_Admin.Employment_AddEdit_Tab_SearchTags_Heading%></span><br />
            <%=Resources.Employment_Admin.Employment_AddEdit_Tab_SearchTags_SubHeading%><br />
            <div id="divAddSearchTags" runat="server" visible="false">
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/search_page.png" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkAddSearchTags" runat="server" OnClick="lnkAddSearchTags_Click"><%=Resources.Employment_Admin.Employment_AddEdit_Tab_SearchTags_AddSearchTags%></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:CheckBoxList ID="cblSearchTags" runat="server">
            </asp:CheckBoxList>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvUserAndGroups" runat="server">
            <span class="callout">
                <%=Resources.Employment_Admin.Employment_AddEdit_Tab_UserGroups_Heading%></span><br />
            <%=Resources.Employment_Admin.Employment_AddEdit_Tab_UserGroups_SubHeading%><br />
            <br />
            <span class="moduleLabel">
                <%=Resources.Employment_Admin.Employment_AddEdit_Tab_UserGroups_Groups%></span>
            <asp:CheckBoxList ID="cblGroupList" runat="server">
            </asp:CheckBoxList>
            <br />
            <div id="divUserGroups_Members" runat="server" visible="false">
                <span class="moduleLabel">
                    <%=Resources.Employment_Admin.Employment_AddEdit_Tab_UserGroups_Users%></span>
                <asp:CheckBoxList ID="cblMemberList" runat="server">
                </asp:CheckBoxList>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvHistory" runat="server">
            <span class="callout">
                <%= Resources.Employment_Admin.Employment_AddEdit_Tab_History_Heading%></span><br />
            <%= Resources.Employment_Admin.Employment_AddEdit_Tab_History_SubHeading%><br />
            <br />
            <telerik:RadGrid ID="rgHistory" runat="server" PageSize="20">
                <MasterTableView DataKeyNames="archiveID">
                    <RowIndicatorColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="archiveID" DataType="System.Int32" ReadOnly="True"
                            SortExpression="archiveID" UniqueName="archiveID" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="dateTimeStamp" DataType="System.DateTime" HeaderText="<%$ Resources:Event_Admin, Event_AddEdit_Tab_History_GridRevisionDate %>"
                            SortExpression="dateTimeStamp" UniqueName="dateTimeStamp" ItemStyle-Width="120px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Version" HeaderText="<%$ Resources:Event_Admin, Event_AddEdit_Tab_History_GridVersion %>"
                            SortExpression="Version" UniqueName="Version" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Title" HeaderText="<%$ Resources:Event_Admin, Event_AddEdit_Tab_History_GridTitle %>"
                            SortExpression="Title" UniqueName="Title">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" ItemStyle-Width="50px"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Event_Admin, Event_AddEdit_Tab_History_GridView %>">
                            <ItemTemplate>
                                <a id="aPreview" runat="server">
                                    <%= Resources.Event_Admin.Event_AddEdit_Tab_History_GridView%></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            HeaderText="<%$ Resources:Event_Admin, Event_AddEdit_Tab_History_GridDelete %>"
                            ItemStyle-Width="60px">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <div id="order" class="divFooterHistory orderText">
                <div class="fRight">
                    <asp:Button ID="btnDeleteHistory" runat="server" Text="<%$ Resources:Employment_Admin, Employment_AddEdit_Tab_History_DeleteButton %>"
                        OnClick="btnDeleteHistory_Click" /></div>
                <br class="cBoth" />
                <telerik:RadWindowManager ID="rwmEmploymentHistory" runat="server">
                    <Windows>
                        <telerik:RadWindow ID="UserListDialog" runat="server" Title="<%$ Resources:Employment_Admin, Employment_AddEdit_Tab_History_EditingRecord %>"
                            Height="600px" Width="1000px" Left="10px" ReloadOnShow="true" ShowContentDuringLoad="false"
                            Modal="true" />
                    </Windows>
                </telerik:RadWindowManager>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" Text="" CausesValidation="true" />
    <asp:Button ID="cancelButton" runat="server" Text="<%$ Resources:Employment_Admin, Employment_AddEdit_ButtonCancel %>"
        CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span><%=Resources.Employment_Admin.Employment_AddEdit_DenotesRequired%><br />
</asp:Content>
