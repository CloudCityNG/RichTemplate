<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="editAdd.aspx.vb" Inherits="admin_modules_document_editAdd" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadTabStrip ID="rtsDocument" runat="server" MultiPageID="rmpDocument" CausesValidation="true"
        SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" PageViewID="rpvDocument" Value="0" Text="<%$ Resources:DocumentLibrary_Admin, Document_AddEdit_Tab_Content%>">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:DocumentLibrary_Admin, Document_AddEdit_Tab_SEO%>"
                PageViewID="rpvSEO" Value="1">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:DocumentLibrary_Admin, Document_AddEdit_Tab_SearchTags%>"
                PageViewID="rpvSearchTags" Value="2">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:DocumentLibrary_Admin, Document_AddEdit_Tab_UserGroups %>"
                PageViewID="rpvUserAndGroups" Value="3" Visible="false">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpDocument" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
        <telerik:RadPageView ID="rpvDocument" runat="server">
            <script type="text/javascript">
                function valDocument(source, arguments) {
                    arguments.IsValid = getRadUpload('<%= ruDocument.ClientID %>').validateExtensions();
                }
            </script>
            <span class="callout">
                <%=Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_Content_Heading%></span><br />
            <%=Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_Content_SubHeading%><br />
            <br />
            <table>
                <tr>
                    <td>
                        <div id="divDocumentFileAndLocation" runat="server" class="divDocumentFileAndLocation"
                            visible="false">
                            <img id="imgFileType" runat="server" />
                            <span class='fileTitle'><asp:Literal ID="litDocumentFileLocation" runat="server" /></span>
                            <span class='fileSize'><asp:Literal ID="litDocumentFileSize" runat="server" /></span>
                            <br class="cBoth" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label for="Status">
                            <span class="moduleLabel">
                                <%= Resources.DocumentLibrary_Admin.Document_AddEdit_Status%>:</span>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="status" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="True" Text="<%$ Resources:DocumentLibrary_Admin, Document_AddEdit_StatusActive %>"></asp:ListItem>
                            <asp:ListItem Value="False" Text="<%$ Resources:DocumentLibrary_Admin, Document_AddEdit_StatusArchive %>"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <span class="moduleLabel">
                                        <%=Resources.DocumentLibrary_Admin.Document_AddEdit_PublicationDate%></span>
                                </td>
                                <td style="padding-left: 10px">
                                    <span class="moduleLabel">
                                        <%=Resources.DocumentLibrary_Admin.Document_AddEdit_ExpirationDate%></span>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="publicationDate" runat="server"><DateInput DateFormat="dd/MM/yyyy"></DateInput></telerik:RadDatePicker>
                                </td>
                                <td class="leftPad">
                                    <telerik:RadDatePicker ID="expirationDate" runat="server"><DateInput DateFormat="dd/MM/yyyy"></DateInput></telerik:RadDatePicker>
                                </td>
                                <td>
                                    <span id="spanExpired" runat="server" class="errorStyle spanExpired" visible="false">
                                        <span>
                                            <img src='/admin/images/expired.png' />
                                        </span><span>
                                            <%= Resources.DocumentLibrary_Admin.Document_AddEdit_Expired%></span> </span>
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
                                            <%=Resources.DocumentLibrary_Admin.Document_AddEdit_AssociateWithSite%>:</span></label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divAssociateWithSite" runat="server">
                                        <asp:RadioButtonList ID="rblSite" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Text="<%$ Resources:DocumentLibrary_Admin, Document_AddEdit_AssociateWithSite_ThisSiteOnly %>"
                                                Value="false" Selected="True" />
                                            <asp:ListItem Text="<%$ Resources:DocumentLibrary_Admin, Document_AddEdit_AssociateWithSite_AllSites %>"
                                                Value="true" />
                                        </asp:RadioButtonList>
                                    </div>
                                    <div id="divAssociateWithSite_PublicMessage" runat="server" class="divAssociateWithSite_PublicMessage"
                                        visible="false">
                                        <span>
                                            <img src='/admin/images/available_to_all.png' />
                                        </span><span class="spanPublicModuleRecord">
                                            <%=Resources.DocumentLibrary_Admin.Document_AddEdit_AssociateWithSite_PublicMessage%>:
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
                                            <%=Resources.DocumentLibrary_Admin.Document_AddEdit_Category%>:</span></label>
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
                <tr id="trUploadFile" runat="server">
                    <td>
                        <span class="moduleLabel">
                            <%= Resources.DocumentLibrary_Admin.Document_AddEdit_UploadDocument%>:</span>
                        <br />
                        <span class="graySubText">
                            <%= Resources.DocumentLibrary_Admin.Document_AddEdit_UploadDocument_Message%><br />
                            <%= Resources.DocumentLibrary_Admin.Document_AddEdit_UploadDocument_Requirements%></span>
                        <asp:CustomValidator ID="customValDocumentRequired" runat="server" Display="Dynamic"
                            OnServerValidate="customValDocumentRequired_Validate" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.DocumentLibrary_Admin.Document_AddEdit_RequiredMessage%></b>
                        </asp:CustomValidator>
                        <asp:CustomValidator ID="customValDocument" runat="server" Display="Dynamic" ClientValidationFunction="valDocument"
                            CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.DocumentLibrary_Admin.Document_AddEdit_UploadDocument_InvalidFileType%></b>
                        </asp:CustomValidator>
                        <asp:CustomValidator ID="customValFileSizeExceeded" runat="server" Display="Dynamic"
                            OnServerValidate="customValFileSizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.DocumentLibrary_Admin.Document_AddEdit_UploadDocument_MaxFileSizeError%></b>
                        </asp:CustomValidator>
                        <br />
                        <br />
                        <telerik:RadUpload ID="ruDocument" runat="server" AllowedFileExtensions=".csv,.doc,.docx,.dotx,.txt,.jpg,.mht,.pdf,.ppt,.pptx,.potx,.odt,.ods,.rtf,.wpd,.wps,.xls,.xlsx,.zip"
                            ControlObjectsVisibility="None" InitialFileInputsCount="1">
                        </telerik:RadUpload>
                        <telerik:RadProgressManager ID="radProgressManagerDocument" runat="server" />
                        <telerik:RadProgressArea ID="radProgressAreaDocument" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="moduleLabel">
                            <%=Resources.DocumentLibrary_Admin.Document_AddEdit_Title%></span><span class="requiredStar">*</span><asp:RequiredFieldValidator
                                ID="reqDocumentTitle" runat="server" ControlToValidate="fileTitle" CssClass="errorStyle"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DocumentLibrary_Admin, Document_AddEdit_RequiredMessage%>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="fileTitle" runat="server" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">
                        <span class="moduleLabel">
                            <%=Resources.DocumentLibrary_Admin.Document_AddEdit_Description%>:</span><span class="requiredStar">*</span><asp:RequiredFieldValidator
                                ID="reqDescription" runat="server" ControlToValidate="txtDescription" CssClass="errorStyle"
                                Display="Dynamic" ErrorMessage="<%$ Resources:DocumentLibrary_Admin, Document_AddEdit_RequiredMessage%>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadEditor ID="txtDescription" runat="server" Width="98%" DocumentManager-MaxUploadFileSize="5240000">
                        </telerik:RadEditor>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvSEO" runat="server">
            <span class="callout">
                <%=Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_SEO_Heading%></span><br />
            <%=Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_SEO_SubHeading%>
            <div>
                <p>
                    <label for="metaTitle">
                        <span class="moduleLabel">
                            <%=Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_SEO_MetaTitle%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaTitle" runat="server" TextMode="MultiLine" Rows="3" CssClass="tb400" />
                </p>
                <p>
                    <label for="metaKeywords">
                        <span class="moduleLabel">
                            <%=Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_SEO_MetaKeywords%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaKeywords" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    <label for="metaDescription">
                        <span class="moduleLabel">
                            <%=Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_SEO_MetaDescription%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaDescription" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    &nbsp;</p>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvSearchTags" runat="server">
            <span class="callout">
                <%=Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_SearchTags_Heading%></span><br />
            <%=Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_SearchTags_SubHeading%><br />
            <div id="divAddSearchTags" runat="server" visible="false">
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/search_page.png" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkAddSearchTags" runat="server" OnClick="lnkAddSearchTags_Click"><%=Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_SearchTags_AddSearchTags%></asp:LinkButton>
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
                <%= Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_UserGroups_Heading%></span><br />
            <%= Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_UserGroups_SubHeading%><br />
            <br />
            <span class="moduleLabel">
                <%= Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_UserGroups_Groups%></span>
            <asp:CheckBoxList ID="cblGroupList" runat="server">
            </asp:CheckBoxList>
            <br />
            <div id="divUserGroups_Members" runat="server" visible="false">
            <span class="moduleLabel">
                <%= Resources.DocumentLibrary_Admin.Document_AddEdit_Tab_UserGroups_Users%></span>
            <asp:CheckBoxList ID="cblMemberList" runat="server">
            </asp:CheckBoxList>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />
    <span class="requiredStar">*</span><%=Resources.DocumentLibrary_Admin.Document_AddEdit_DenotesRequired%><br />
    <br />
    <asp:Button ID="btnAddEdit" runat="server" />
    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:DocumentLibrary_Admin, Document_AddEdit_ButtonCancel%>"
        CausesValidation="false" />
</asp:Content>
