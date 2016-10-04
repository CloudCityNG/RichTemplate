<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="editAdd.aspx.vb" Inherits="admin_modules_event_editAdd"
    ValidateRequest="false" %>

<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function valEventImage(source, arguments) {
            arguments.IsValid = getRadUpload('<%= RadUploadEventImage.ClientID %>').validateExtensions();
        }
    </script>
    <span class="callout">
        <%=Resources.Event_Admin.Event_AddEdit_BodyHeading%></span><br />
    <br />
    <telerik:RadTabStrip ID="rtsEvent" runat="server" MultiPageID="rmpEvents" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" PageViewID="rpvEvent" Value="0">
                <TabTemplate>
                    <%=Resources.Event_Admin.Event_AddEdit_Tab_Content%>
                </TabTemplate>
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Event_Admin, Event_AddEdit_Tab_SEO %>"
                PageViewID="rpvSEO" Value="1">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Event_Admin, Event_AddEdit_Tab_SearchTags %>"
                PageViewID="rpvSearchTags" Value="2">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Event_Admin, Event_AddEdit_Tab_UserGroups %>"
                PageViewID="rpvUserAndGroups" Value="3" Visible="false">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Event_Admin, Event_AddEdit_Tab_History %>"
                PageViewID="rpvHistory" Value="4">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpEvents" runat="server" CssClass="Windows7_MultipageBorder"
        SelectedIndex="0">
        <telerik:RadPageView ID="rpvEvent" runat="server">
            <span class="callout">
                <%=Resources.Event_Admin.Event_AddEdit_Tab_Content_Heading%></span><br />
            <%=Resources.Event_Admin.Event_AddEdit_Tab_Content_SubHeading%><br />
            <br />
            <div id="MainContent">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <label for="Status">
                                <span class="moduleLabel">
                                    <%=Resources.Event_Admin.Event_AddEdit_Status%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Event_Admin, Event_AddEdit_StatusActive%>"></asp:ListItem>
                                <asp:ListItem Value="False" Text="<%$ Resources:Event_Admin, Event_AddEdit_StatusArchive%>"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <span class="moduleLabel">
                                            <%=Resources.Event_Admin.Event_AddEdit_PublicationDate%>:</span>
                                    </td>
                                    <td class="leftPad">
                                        <span class="moduleLabel">
                                            <%=Resources.Event_Admin.Event_AddEdit_ExpirationDate%>:</span>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="publicationDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                                    </td>
                                    <td class="leftPad">
                                        <telerik:RadDatePicker ID="ExpirationDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <span id="spanExpired" runat="server" class="errorStyle spanExpired" visible="false">
                                            <span>
                                                <img src='/admin/images/expired.png' />
                                            </span><span>
                                                <%= Resources.Event_Admin.Event_AddEdit_ExpirationDateExpired%></span> </span>
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
                                                <%=Resources.Event_Admin.Event_AddEdit_AssociateWithSite%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="divAssociateWithSite" runat="server">
                                            <asp:RadioButtonList ID="rblSite" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Text="<%$ Resources:Event_Admin, Event_AddEdit_AssociateWithSite_ThisSiteOnly %>"
                                                    Value="false" Selected="True" />
                                                <asp:ListItem Text="<%$ Resources:Event_Admin, Event_AddEdit_AssociateWithSite_AllSites %>"
                                                    Value="true" />
                                            </asp:RadioButtonList>
                                        </div>
                                        <div id="divAssociateWithSite_PublicMessage" runat="server" class="divAssociateWithSite_PublicMessage"
                                            visible="false">
                                            <span>
                                                <img src='/admin/images/available_to_all.png' />
                                            </span><span class="spanPublicModuleRecord">
                                                <%=Resources.Event_Admin.Event_AddEdit_AssociateWithSite_PublicMessage%>: <asp:Literal
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
                                        <label for="startDate">
                                            <span class="moduleLabel">
                                                <%=Resources.Event_Admin.Event_AddEdit_StartDate%>:</span><span class="requiredStar">*</span>
                                            <asp:RequiredFieldValidator ID="reqStartDateTime" runat="server" ControlToValidate="startDate"
                                                CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Event_Admin, Event_AddEdit_RequiredMessage %>" /></label>
                                    </td>
                                    <td class="leftPad">
                                        <label for="endDate">
                                            <span class="moduleLabel">
                                                <%=Resources.Event_Admin.Event_AddEdit_EndDate%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="startDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                                        &nbsp;<telerik:RadTimePicker ID="startTime" runat="server" TimeView-HeaderText="<%$ Resources:Event_Admin, Event_AddEdit_StartDate_TimePicker_HeaderText %>" />
                                    </td>
                                    <td class="leftPad">
                                        <telerik:RadDatePicker ID="endDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                                        &nbsp;<telerik:RadTimePicker ID="endTime" runat="server" TimeView-HeaderText="<%$ Resources:Event_Admin, Event_AddEdit_EndDate_TimePicker_HeaderText %>" />
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
                                                <%=Resources.Event_Admin.Event_AddEdit_Category%>:</span></label>
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
                                                <%=Resources.Event_Admin.Event_AddEdit_OnlineSignup%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="onlineSignup" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="True" Text="<%$ Resources:Event_Admin, Event_AddEdit_OnlineSignupYes%>"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="<%$ Resources:Event_Admin, Event_AddEdit_OnlineSignupNo%>"></asp:ListItem>
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
                                    <%=Resources.Event_Admin.Event_AddEdit_Title%>:</span><span class="requiredStar">*</span>
                                <asp:RequiredFieldValidator ID="reqTitle" runat="server" ControlToValidate="txtTitle"
                                    CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Event_Admin, Event_AddEdit_RequiredMessage %>" />
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
                                    <%=Resources.Event_Admin.Event_AddEdit_ShortSummary%>:</span><span class="requiredStar">*</span><asp:RequiredFieldValidator
                                        ID="reqSummary" runat="server" ControlToValidate="txtSummary" CssClass="errorStyle"
                                        Display="Dynamic" ErrorMessage="<%$ Resources:Event_Admin, Event_AddEdit_RequiredMessage %>" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtSummary" runat="server" CssClass="tb400" Rows="4" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <span class="moduleLabel">
                                            <%=Resources.Event_Admin.Event_AddEdit_UploadImage%>:</span>
                                        <br />
                                        <span class="graySubText">
                                            <%=Resources.Event_Admin.Event_AddEdit_UploadImage_Message%><br />
                                            <%=Resources.Event_Admin.Event_AddEdit_UploadImage_Requirements%></span>
                                        <asp:CustomValidator ID="customValEventImage" runat="server" Display="Dynamic" ClientValidationFunction="valEventImage"
                                            CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Event_Admin.Event_AddEdit_UploadImage_InvalidFileType%></b>
                                        </asp:CustomValidator>
                                        <asp:CustomValidator ID="customValEventImageSizeExceeded" runat="server" Display="Dynamic"
                                            OnServerValidate="customValEventImageSizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Event_Admin.Event_AddEdit_UploadImage_MaxFileSizeExceeded%></b>
                                        </asp:CustomValidator>
                                        <br />
                                        <br />
                                        <telerik:RadUpload ID="RadUploadEventImage" runat="server" AllowedFileExtensions=".jpg,.gif,.png"
                                            ControlObjectsVisibility="None" InitialFileInputsCount="1">
                                        </telerik:RadUpload>
                                        <telerik:RadProgressManager ID="rpManagerEvent" runat="server" />
                                        <telerik:RadProgressArea ID="rpAreaEvent" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadBinaryImage ID="eventImage" runat="server" CssClass="rightPad" Visible="false" /><br />
                                        <asp:LinkButton ID="lnkDeleteImage" runat="server" Visible="false" OnClick="lnkDeleteImage_Click"
                                            CausesValidation="false"><b><%=Resources.Event_Admin.Event_AddEdit_UploadImage_Delete%></b></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtExternalLinkUrl">
                                <span class="moduleLabel">
                                    <%=Resources.Event_Admin.Event_AddEdit_ExternalWebsite%>:</span>
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
                                <span class="moduleLabel">
                                    <%=Resources.Event_Admin.Event_AddEdit_Geolocation%></span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="geolocation" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Event_Admin, Event_AddEdit_GeolocationYes%>"></asp:ListItem>
                                <asp:ListItem Value="False" Text="<%$ Resources:Event_Admin, Event_AddEdit_GeolocationNo%>"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdEventAddress">
                            <br />
                            <uc:Address ID="ucAddress" runat="server" AddressLayout="Horizontal" AddressValidationLayout="SideBySide_RelativeListPosition"
                                Required="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <label for="Body">
                                <span class="moduleLabel">
                                    <%=Resources.Event_Admin.Event_AddEdit_MainContent%>:</span><span class="requiredStar">*</span><asp:RequiredFieldValidator
                                        ID="reqMainContent" runat="server" ControlToValidate="txtBody" CssClass="errorStyle"
                                        Display="Dynamic" ErrorMessage="<%$ Resources:Event_Admin, Event_AddEdit_RequiredMessage %>" />
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
                                    <%=Resources.Event_Admin.Event_AddEdit_SentApplicaitionsTo%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtContactPerson" runat="server" CssClass="tb300" />
                            <asp:RegularExpressionValidator ID="regExpValSendApplicationToEmail" ControlToValidate="txtContactPerson"
                                ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                                ErrorMessage="<%$ Resources:Event_Admin, Event_AddEdit_InvalidEmail %>" Display="dynamic"
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:TextBox ID="version" runat="server" Visible="false" />
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvSEO" runat="server">
            <span class="callout">
                <%=Resources.Event_Admin.Event_AddEdit_Tab_SEO_Heading%></span><br />
            <%=Resources.Event_Admin.Event_AddEdit_Tab_SEO_SubHeading%>
            <div>
                <p>
                    <label for="metaTitle">
                        <span class="moduleLabel">
                            <%=Resources.Event_Admin.Event_AddEdit_Tab_SEO_MetaTitle%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaTitle" runat="server" TextMode="MultiLine" Rows="3" CssClass="tb400" />
                </p>
                <p>
                    <label for="metaKeywords">
                        <span class="moduleLabel">
                            <%=Resources.Event_Admin.Event_AddEdit_Tab_SEO_MetaKeywords%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaKeywords" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    <label for="metaDescription">
                        <span class="moduleLabel">
                            <%=Resources.Event_Admin.Event_AddEdit_Tab_SEO_MetaDescription%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaDescription" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    &nbsp;</p>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvSearchTags" runat="server">
            <span class="callout">
                <%=Resources.Event_Admin.Event_AddEdit_Tab_SearchTags_Heading%></span><br />
            <%=Resources.Event_Admin.Event_AddEdit_Tab_SearchTags_SubHeading%>.<br />
            <div id="divAddSearchTags" runat="server" visible="false">
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/search_page.png" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkAddSearchTags" runat="server" OnClick="lnkAddSearchTags_Click"><%=Resources.Event_Admin.Event_AddEdit_Tab_SearchTags_AddSearchTags%></asp:LinkButton>
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
                <%=Resources.Event_Admin.Event_AddEdit_Tab_UserGroups_Heading%></span><br />
            <%=Resources.Event_Admin.Event_AddEdit_Tab_UserGroups_SubHeading%><br />
            <br />
            <span class="moduleLabel">
                <%=Resources.Event_Admin.Event_AddEdit_Tab_UserGroups_Groups%></span>
            <asp:CheckBoxList ID="cblGroupList" runat="server">
            </asp:CheckBoxList>
            <br />
            <div id="divUserGroups_Members" runat="server" visible="false">
                <span class="moduleLabel">
                    <%= Resources.Event_Admin.Event_AddEdit_Tab_UserGroups_Users%></span>
                <asp:CheckBoxList ID="cblMemberList" runat="server">
                </asp:CheckBoxList>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvHistory" runat="server">
            <span class="callout">
                <%= Resources.Event_Admin.Event_AddEdit_Tab_History_Heading%></span><br />
            <%= Resources.Event_Admin.Event_AddEdit_Tab_History_SubHeading%><br />
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
                        <telerik:GridTemplateColumn HeaderText="<%$ Resources:Event_Admin, Event_AddEdit_Tab_History_GridDelete %>"
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
                    <asp:Button ID="btnDeleteHistory" runat="server" Text="<%$ Resources:Event_Admin, Event_AddEdit_Tab_History_DeleteButton %>"
                        OnClick="btnDeleteHistory_Click" /></div>
                <br class="cBoth" />
                <telerik:RadWindowManager ID="rwmEventHistory" runat="server">
                    <Windows>
                        <telerik:RadWindow ID="UserListDialog" runat="server" Title="<%$ Resources:Event_Admin, Event_AddEdit_Tab_History_EditingRecord %>"
                            Height="600px" Width="1000px" Left="10px" ReloadOnShow="true" ShowContentDuringLoad="false"
                            Modal="true" />
                    </Windows>
                </telerik:RadWindowManager>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" Text="" CausesValidation="true" />
    <asp:Button ID="cancelButton" runat="server" Text="<%$ Resources:Event_Admin, Event_AddEdit_ButtonCancel %>"
        CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span><%=Resources.Event_Admin.Event_AddEdit_DenotesRequired%><br />
</asp:Content>
