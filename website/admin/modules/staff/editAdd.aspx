<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="editAdd.aspx.vb" Inherits="admin_modules_staff_editAdd"
    ValidateRequest="false" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function valStaffImage(source, arguments) {
            arguments.IsValid = getRadUpload('<%= RadUploadStaffImage.ClientID %>').validateExtensions();
        }
    </script>
    <span class="callout">
        <%=Resources.Staff_Admin.Staff_Default_BodyHeading%></span><br />
    <br />
    <telerik:RadTabStrip ID="rtsStaff" runat="server" MultiPageID="rmpStaff" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" PageViewID="rpvStaffContent" Value="0" Text="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_Content %>">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_SEO%>"
                PageViewID="rpvSEO" Value="1">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_SearchTags%>"
                PageViewID="rpvSearchTags" Value="2">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_UserGroups%>"
                PageViewID="rpvUserAndGroups" Value="3" Visible="false">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_History%>"
                PageViewID="rpvHistory" Value="4">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpStaff" runat="server" CssClass="Windows7_MultipageBorder"
        SelectedIndex="0">
        <telerik:RadPageView ID="rpvStaffContent" runat="server">
            <span class="callout">
                <%=Resources.Staff_Admin.Staff_AddEdit_Tab_Content_Heading%></span><br />
            <%=Resources.Staff_Admin.Staff_AddEdit_Tab_Content_SubHeading%><br />
            <br />
            <div id="MainContent">
                <table cellpadding="0" cellspacing="0">
                    <tr runat="server">
                        <td>
                            <label for="Status">
                                <span class="moduleLabel">
                                    <%=Resources.Staff_Admin.Staff_AddEdit_Status%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td>
                            <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Staff_Admin, Staff_AddEdit_StatusActive%>"></asp:ListItem>
                                <asp:ListItem Value="False" Text="<%$ Resources:Staff_Admin, Staff_AddEdit_StatusArchive%>"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="StartDate">
                                            <span class="moduleLabel">
                                                <%=Resources.Staff_Admin.Staff_AddEdit_StartDate%>:</span></label>
                                    </td>
                                    <td class="leftPad">
                                        <label for="EndDate">
                                            <span class="moduleLabel">
                                                <%=Resources.Staff_Admin.Staff_AddEdit_EndDate%>:</span></label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="StartDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                                    </td>
                                    <td class="leftPad">
                                        <telerik:RadDatePicker ID="EndDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <span id="spanExpired" runat="server" class="errorStyle spanExpired" visible="false">
                                            <span>
                                                <img src='/admin/images/expired.png' />
                                            </span><span>
                                                <%= Resources.Staff_Admin.Staff_AddEdit_Expired%></span> </span>
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
                                                <%=Resources.Staff_Admin.Staff_AddEdit_AssociateWithSite%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="divAssociateWithSite" runat="server">
                                            <asp:RadioButtonList ID="rblSite" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Text="<%$ Resources:Staff_Admin, Staff_AddEdit_AssociateWithSite_ThisSiteOnly %>"
                                                    Value="false" Selected="True" />
                                                <asp:ListItem Text="<%$ Resources:Staff_Admin, Staff_AddEdit_AssociateWithSite_AllSites %>"
                                                    Value="true" />
                                            </asp:RadioButtonList>
                                        </div>
                                        <div id="divAssociateWithSite_PublicMessage" runat="server" class="divAssociateWithSite_PublicMessage"
                                            visible="false">
                                            <span>
                                                <img src='/admin/images/available_to_all.png' />
                                            </span><span class="spanPublicModuleRecord">
                                                <%=Resources.Staff_Admin.Staff_AddEdit_AssociateWithSite_PublicMessage%>: <asp:Literal
                                                    ID="litSiteName" runat="server" /></span>
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
                                                <%=Resources.Staff_Admin.Staff_AddEdit_Category%>:</span></label>
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
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="2">
                                        <label for="userActive">
                                            <span class="moduleLabel">
                                                <%=Resources.Staff_Admin.Staff_AddEdit_Salutation%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlSalutation" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="firstName">
                                            <span class="moduleLabel">
                                                <%=Resources.Staff_Admin.Staff_AddEdit_FirstName%></span><span class="requiredStar">*</span></label><asp:RequiredFieldValidator
                                                    ID="reqFirstName" runat="server" ErrorMessage="<%$ Resources:Staff_Admin, Staff_AddEdit_RequiredMessage%>"
                                                    ControlToValidate="firstName" CssClass="errorStyle" Display="Dynamic" />
                                    </td>
                                    <td class="leftPad">
                                        <label for="lastName">
                                            <span class="moduleLabel">
                                                <%=Resources.Staff_Admin.Staff_AddEdit_LastName%></span><span class="requiredStar">*</span></label><asp:RequiredFieldValidator
                                                    ID="reqLastName" runat="server" ErrorMessage="<%$ Resources:Staff_Admin, Staff_AddEdit_RequiredMessage%>"
                                                    ControlToValidate="lastName" CssClass="errorStyle" Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="firstName" runat="server" />
                                    </td>
                                    <td class="leftPad">
                                        <asp:TextBox ID="lastName" runat="server" />
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
                                            <%=Resources.Staff_Admin.Staff_AddEdit_UploadImage%>:</span>
                                        <br />
                                        <span class="graySubText">
                                            <%=Resources.Staff_Admin.Staff_AddEdit_UploadImage_Message%><br />
                                            <%=Resources.Staff_Admin.Staff_AddEdit_UploadImage_Requirements%></span>
                                        <asp:CustomValidator ID="customValStaffImage" runat="server" Display="Dynamic" ClientValidationFunction="valStaffImage"
                                            CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Staff_Admin.Staff_AddEdit_UploadImage_InvalidFileType%></b>
                                        </asp:CustomValidator>
                                        <asp:CustomValidator ID="customValStaffImageSizeExceeded" runat="server" Display="Dynamic"
                                            OnServerValidate="customValStaffImageSizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Staff_Admin.Staff_AddEdit_UploadImage_MaxFileSizeExceeded%></b>
                                        </asp:CustomValidator>
                                        <br />
                                        <br />
                                        <telerik:RadUpload ID="RadUploadStaffImage" runat="server" AllowedFileExtensions=".jpg,.gif,.png"
                                            ControlObjectsVisibility="None" InitialFileInputsCount="1">
                                        </telerik:RadUpload>
                                        <telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />
                                        <telerik:RadProgressArea ID="progressArea1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadBinaryImage ID="staffImage" runat="server" CssClass="rightPad" Visible="false" /><br />
                                        <asp:LinkButton ID="lnkDeleteImage" runat="server" Visible="false" OnClick="lnkDeleteImage_Click"
                                            CausesValidation="false"><b><%=Resources.Staff_Admin.Staff_AddEdit_UploadImage_Delete%></b></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <label for="company">
                                <span class="moduleLabel">
                                    <%=Resources.Staff_Admin.Staff_AddEdit_CompanyName%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="company" runat="server" CssClass="tb200" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="JobPosition">
                                            <span class="moduleLabel">
                                                <%=Resources.Staff_Admin.Staff_AddEdit_StaffPosition%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="rcbJobPosition" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <label for="emailAddress">
                                <span class="moduleLabel">
                                    <%=Resources.Staff_Admin.Staff_AddEdit_EmailAddress%>:</span><span class="requiredStar">*</span>
                            </label>
                            <asp:RequiredFieldValidator ID="reqEmailAddress" runat="server" ErrorMessage="<%$ Resources:Staff_Admin, Staff_AddEdit_RequiredMessage%>"
                                ControlToValidate="emailAddress" CssClass="errorStyle" Display="Dynamic" /><asp:RegularExpressionValidator
                                    ID="reqExpEmailAddress" ControlToValidate="emailAddress" ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                                    ErrorMessage="<%$ Resources:Staff_Admin, Staff_AddEdit_EmailAddress_InvalidEmail%>"
                                    Display="dynamic" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="emailAddress" runat="server" CssClass="tb200" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="personalurl">
                                            <span class="moduleLabel">
                                                <%=Resources.Staff_Admin.Staff_AddEdit_PersonalUrl%></span></label>
                                    </td>
                                    <td class="leftPad">
                                        <label for="favouriteurl">
                                            <span class="moduleLabel">
                                                <%=Resources.Staff_Admin.Staff_AddEdit_FavouriteUrl%></span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="personalurl" runat="server" CssClass="tb200" />
                                    </td>
                                    <td class="leftPad">
                                        <asp:TextBox ID="favouriteurl" runat="server" CssClass="tb200" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="Body">
                                <span class="moduleLabel">
                                    <%=Resources.Staff_Admin.Staff_AddEdit_Biography%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadEditor ID="Body" runat="server" Width="98%">
                            </telerik:RadEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label>
                                            <span class="moduleLabel">
                                                <%=Resources.Staff_Admin.Staff_AddEdit_OfficePhone%>:</span>
                                        </label>
                                    </td>
                                    <td class="leftPad">
                                        <label>
                                            <span class="moduleLabel">
                                                <%=Resources.Staff_Admin.Staff_AddEdit_MobilePhone%>:</span>
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="officePhone" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="leftPad">
                                        <asp:TextBox ID="mobilePhone" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <label for="geoLocation">
                                <span class="moduleLabel">
                                    <%=Resources.Staff_Admin.Staff_AddEdit_GeoLocation%>
                                </span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="geolocation" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Staff_Admin, Staff_AddEdit_GeolocationYes%>"></asp:ListItem>
                                <asp:ListItem Value="False" Text="<%$ Resources:Staff_Admin, Staff_AddEdit_GeolocationNo%>"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdStaffAddress">
                            <br />
                            <div class="divLocationOfficeNumber">
                                <div class="divLocationOfficeNumberLbl">
                                    <label for="txtAddressOfficeNumber">
                                        <span class="addressLabel">
                                            <%= Resources.Staff_Admin.Staff_AddEdit_AddressOfficeNumber%>:</span>
                                    </label>
                                    <asp:TextBox ID="txtAddressOfficeNumber" runat="server" />
                                </div>
                            </div>
                            <div class="divLocationOfficeNumberSeperator">
                            </div>
                            <uc:Address ID="ucAddress" runat="server" AddressLayout="Horizontal" AddressValidationLayout="SideBySide_RelativeListPosition"
                                Required="false" ErrorMessage="<%$ Resources:Staff_Admin, Staff_AddEdit_RequiredMessage%>" />
                        </td>
                    </tr>
                </table>
                <asp:TextBox ID="version" runat="server" Width="35px" Visible="false" />
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvSEO" runat="server">
            <span class="callout">
                <%=Resources.Staff_Admin.Staff_AddEdit_Tab_SEO_Heading%></span><br />
            <%=Resources.Staff_Admin.Staff_AddEdit_Tab_SEO_SubHeading%>
            <div>
                <p>
                    <label for="metaTitle">
                        <span class="moduleLabel">
                            <%=Resources.Staff_Admin.Staff_AddEdit_Tab_SEO_MetaTitle%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaTitle" runat="server" TextMode="MultiLine" Rows="3" CssClass="tb400" />
                </p>
                <p>
                    <label for="metaKeywords">
                        <span class="moduleLabel">
                            <%=Resources.Staff_Admin.Staff_AddEdit_Tab_SEO_MetaKeywords%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaKeywords" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    <label for="metaDescription">
                        <span class="moduleLabel">
                            <%=Resources.Staff_Admin.Staff_AddEdit_Tab_SEO_MetaDescription%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaDescription" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    &nbsp;</p>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvSearchTags" runat="server">
            <span class="callout">
                <%=Resources.Staff_Admin.Staff_AddEdit_Tab_SearchTags_Heading%></span><br />
            <%=Resources.Staff_Admin.Staff_AddEdit_Tab_SearchTags_SubHeading%><br />
            <div id="divAddSearchTags" runat="server" visible="false">
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/search_page.png" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkAddSearchTags" runat="server" OnClick="lnkAddSearchTags_Click"><%=Resources.Staff_Admin.Staff_AddEdit_Tab_SearchTags_AddSearchTags%></asp:LinkButton>
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
                <%=Resources.Staff_Admin.Staff_AddEdit_Tab_UserGroups_Heading%></span><br />
            <%=Resources.Staff_Admin.Staff_AddEdit_Tab_UserGroups_SubHeading%><br />
            <br />
            <span class="moduleLabel">
                <%=Resources.Staff_Admin.Staff_AddEdit_Tab_UserGroups_Groups%></span>
            <asp:CheckBoxList ID="cblGroupList" runat="server">
            </asp:CheckBoxList>
            <br />
            <div id="divUserGroups_Members" runat="server" visible="false">
                <span class="moduleLabel">
                    <%=Resources.Staff_Admin.Staff_AddEdit_Tab_UserGroups_Users%></span>
                <asp:CheckBoxList ID="cblMemberList" runat="server">
                </asp:CheckBoxList>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvHistory" runat="server">
            <span class="callout">
                <%= Resources.Staff_Admin.Staff_AddEdit_Tab_History_Heading%></span><br />
            <%= Resources.Staff_Admin.Staff_AddEdit_Tab_History_SubHeading%><br />
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
                        <telerik:GridBoundColumn DataField="dateTimeStamp" DataType="System.DateTime" HeaderText="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_History_GridRevisionDate %>"
                            SortExpression="dateTimeStamp" UniqueName="dateTimeStamp" ItemStyle-Width="120px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Version" HeaderText="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_History_GridVersion %>"
                            SortExpression="Version" UniqueName="Version" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_History_GridFullName%>"
                            UniqueName="fullname" SortExpression="lastName">
                            <ItemTemplate>
                                <%#Eval("lastname") %>,
                                <%#Eval("firstName")%></ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="StaffPosition" HeaderText="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_History_GridStaffPosition%>"
                            SortExpression="StaffPosition" UniqueName="StaffPosition" EmptyDataText="<%$ Resources:Staff_Admin, Staff_AddEdit_StaffPosition_NotSpecified%>">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmailAddress" HeaderText="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_History_GridEmailAddress%>"
                            SortExpression="EmailAddress" UniqueName="EmailAddress">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" ItemStyle-Width="50px"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_History_GridView %>">
                            <ItemTemplate>
                                <a id="aPreview" runat="server">
                                    <%= Resources.Staff_Admin.Staff_AddEdit_Tab_History_GridView%></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_History_GridDelete %>"
                            ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <div id="order" class="divFooterHistory orderText">
                <div class="fRight">
                    <asp:Button ID="btnDeleteHistory" runat="server" Text="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_History_DeleteButton%>"
                        OnClick="btnDeleteHistory_Click" /></div>
                <br class="cBoth" />
                <telerik:RadWindowManager ID="RadWindowManagerStaff" runat="server">
                    <Windows>
                        <telerik:RadWindow ID="UserListDialog" runat="server" Title="<%$ Resources:Staff_Admin, Staff_AddEdit_Tab_History_EditingRecord%>"
                            Height="600px" Width="1000px" Left="10px" ReloadOnShow="true" ShowContentDuringLoad="false"
                            Modal="true" />
                    </Windows>
                </telerik:RadWindowManager>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" CausesValidation="true" />
    <asp:Button ID="cancelButton" runat="server" Text="<%$ Resources:Staff_Admin, Staff_AddEdit_ButtonCancel%>"
        CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span><%=Resources.Staff_Admin.Staff_AddEdit_DenotesRequired%><br />
</asp:Content>
