<%@ Page Language="VB" AutoEventWireup="false" CodeFile="richtemplate_page_editor.aspx.vb"
    Inherits="admin_richtemplate_page_editor" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="/admin/styles.css" />
    <link rel="stylesheet" type="text/css" href="/admin/css/RichTemplate.css" />
    <link href="/css/richtemplate_core.css" rel="stylesheet" type="text/css" />
    <link href="/css/RichTemplate_Module.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="/js/jquery-1.4.2.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="radScriptManager" runat="server" />
        <%-- TELERIK Editor Script - BEGIN --%>
        <script type="text/javascript" src="/editorConfig/js/TelerikToolbarCommands.js"></script>
        <link rel="stylesheet" type="text/css" href="/Skins/RichTemplate/RadEditor.RichTemplate.css" />
        <script type="text/javascript">
            function valBannerImage(source, arguments) {
                arguments.IsValid = getRadUpload('<%= RadUploadBannerImage.ClientID %>').validateExtensions();
        }
        </script>
        <%-- TELERIK Editor Script - END --%>
        <uc:Header ID="ucHeader" runat="server" />
        <div style="padding: 5px 5px 5px 5px;">
            <div class="fRight" style="margin: 10px 10px 10px 0px;">
                <div class="floatL">
                    <b>
                        <asp:LinkButton ID="lnkSaveAsDraft" runat="server" Text="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_ButtonSaveDraft %>" />
                    </b>&nbsp;|&nbsp;
                </div>
                <div id="divSaveLive" runat="server" visible="false" class="floatL">
                    <b>
                        <asp:LinkButton ID="lnkPublishLive" runat="server" Text="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_ButtonSaveLive %>" /></b>
                    &nbsp;|&nbsp;
                </div>
                <div class="floatL">
                    <b>
                        <asp:LinkButton ID="lnkCancel" runat="server" Text="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_ButtonCancel %>"
                            CausesValidation="false" /></b>
                </div>
            </div>
            <telerik:RadTabStrip ID="rtsWebInfo" runat="server" MultiPageID="rmpWebInfo" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Text="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_Tab_WebpageContent %>"
                        PageViewID="rpvWebInfo" Value="0">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_Tab_SEO %>"
                        PageViewID="rpvSEO" Value="1">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_Tab_SearchTags %>"
                        PageViewID="rpvSearchTags" Value="2">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_Tab_UsersGroups %>"
                        PageViewID="rpvUserAndGroups" Value="3" Visible="false">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_Tab_AdminUserAccess %>"
                        PageViewID="rpvAdminUserAccess" Value="4" Visible="false">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="rmpWebInfo" runat="server" CssClass="Windows7_MultipageBorder"
                SelectedIndex="0">
                <telerik:RadPageView ID="rpvWebInfo" runat="server">
                    <span class="callout">
                        <%= Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_Heading%></span><br />
                    <%= Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_SubHeading%><br />
                    <br />
                    <div id="MainContent">
                        <div>
                            <label for="txtWebPageName">
                                <span class="moduleLabel">
                                    <%= Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_WebpageName%></span></label><br />
                            <div id="divWebPageNameEnabled" runat="server" visible="true">
                                <asp:TextBox ID="txtWebPageName" runat="server" CssClass="tb300" /><span class="requiredStar">*</span>
                                <asp:RequiredFieldValidator ID="reqWebPageName" runat="server" ErrorMessage="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_RequiredMessage %>"
                                    ControlToValidate="txtWebPageName" CssClass="errorStyle" Display="Dynamic" />
                                <div>
                                    <asp:CustomValidator ID="cusValWebPageName" runat="server" Display="Dynamic" OnServerValidate="WebPageName_Validate" />
                                </div>
                            </div>
                            <div id="divWebPageNameDisabled" runat="server" visible="false">
                                <b>
                                    <asp:Literal ID="litWebPageName" runat="server" /></b>
                            </div>
                        </div>
                        <div id="divSecondLevelPage" runat="server" visible="false">
                            <span style="font-size: 12px;" class="moduleLabel">
                                <%= Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_NavigationLayout%>:</span>
                            <br />
                            <asp:RadioButtonList ID="rdNavigationLayout" runat="server">
                                <asp:ListItem Text="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_Tab_WebpageContent_NavigationLayout_Column1 %>"
                                    Value="1" Selected="True" />
                                <asp:ListItem Text="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_Tab_WebpageContent_NavigationLayout_Column2 %>"
                                    Value="2" />
                                <asp:ListItem Text="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_Tab_WebpageContent_NavigationLayout_Column3 %>"
                                    Value="3" />
                            </asp:RadioButtonList>
                        </div>
                        <div id="divBannerImage" runat="server" visible="false">
                            <br />
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <span class="moduleLabel">
                                            <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_UploadImage%>:</span>
                                        <br />
                                        <span class="graySubText"><span id="spanUploadMessageForSection" runat="server" visible="false">
                                            <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_UploadImage_Message_ForSection%>
                                        </span><span id="spanUploadMessageForPage" runat="server" visible="false">
                                            <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_UploadImage_Message_ForPage%><br />
                                            <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_UploadImage_Message_ForPage_DefaultToSectionBanner%>
                                        </span>
                                            <br />
                                            <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_UploadImage_Requirements%></span>
                                        <asp:CustomValidator ID="customValBannerImage" runat="server" Display="Dynamic" ClientValidationFunction="valBannerImage"
                                            CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_UploadImage_InvalidFileType%></b>
                                        </asp:CustomValidator>
                                        <asp:CustomValidator ID="customValBannerImageSizeExceeded" runat="server" Display="Dynamic"
                                            OnServerValidate="customValBannerImageSizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_UploadImage_MaxFileSizeExceeded%></b>
                                        </asp:CustomValidator>
                                        <br />
                                        <br />
                                        <telerik:RadUpload ID="RadUploadBannerImage" runat="server" AllowedFileExtensions=".jpg,.gif,.png"
                                            ControlObjectsVisibility="None" InitialFileInputsCount="1">
                                        </telerik:RadUpload>
                                        <telerik:RadProgressManager ID="rpManagerBanner" runat="server" />
                                        <telerik:RadProgressArea ID="rpAreaBanner" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadBinaryImage ID="bannerImage" runat="server" CssClass="rightPad" Visible="false" /><br />
                                        <span id="spanCurrentSectionBannerImageMessage" runat="server" class="importantStyle"
                                            visible="false">
                                            <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_UploadImage_CurrentSectionImage%></span>
                                        <asp:LinkButton ID="lnkDeleteImage" runat="server" Visible="false" OnClick="lnkDeleteImage_Click"
                                            CausesValidation="false"><b><%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_UploadImage_Delete%></b></asp:LinkButton>
                                        <div id="divInheritBannerImageFromSection" runat="server" visible="false">
                                            <br />
                                            <asp:CheckBox ID="chkInheritBannerImageFromSection" runat="server" />
                                            <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_UploadImage_InheritBannerImageFromSection%>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>
                        <div>
                            <br />
                            <label for="Body">
                                <span class="moduleLabel">
                                    <%= Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_WebpageContent_WebpageContent%>:</span><span
                                        class="requiredStar">*</span>
                                <asp:RequiredFieldValidator ID="reqWebPageContent" runat="server" ErrorMessage="<%$ Resources:RichTemplate_Page_Editor, RichTemplate_Page_Editor_RequiredMessage %>"
                                    ControlToValidate="txtMessage" CssClass="errorStyle" Display="Dynamic" />
                            </label>
                            <br />
                            <telerik:RadEditor ID="txtMessage" runat="server" Width="98%" />
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvSEO" runat="server">
                    <div>
                        <span style="font-size: 12px;" class="moduleLabel">
                            <%= Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_SEO_Heading%>:</span><br />
                        <table cellpadding="2" cellspacing="2" border="0">
                            <tr>
                                <td valign="top">
                                    <%= Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_SEO_MetaTitle%>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMetaTitle" runat="server" CssClass="tb400" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <%= Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_SEO_MetaKeywords%>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMetaKeywords" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <%= Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_SEO_MetaDescription%>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMetaDescription" runat="server" TextMode="MultiLine" Rows="5"
                                        CssClass="tb400" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvSearchTags" runat="server">
                    <span class="callout">
                        <%= Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_SearchTags_Heading%></span><br />
                    <%= Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_SearchTags_SubHeading%><br />
                    <div id="divAddSearchTags" runat="server" visible="false">
                        <br />
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img src="/admin/images/search_page.png" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkAddSearchTags" runat="server" OnClick="lnkAddSearchTags_Click"><%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_SearchTags_AddSearchTags%></asp:LinkButton>
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
                        <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_UserGroups_Heading%></span><br />
                    <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_UserGroups_SubHeading%><br />
                    <br />
                    <span class="moduleLabel">
                        <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_UserGroups_Groups%></span>
                    <asp:CheckBoxList ID="cblGroupList" runat="server">
                    </asp:CheckBoxList>
                    <br />
                    <div id="divUserGroups_Members" runat="server" visible="false">
                        <span class="moduleLabel">
                            <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_UserGroups_Users%></span>
                        <asp:CheckBoxList ID="cblMemberList" runat="server">
                        </asp:CheckBoxList>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView ID="rpvAdminUserAccess" runat="server">
                    <span class="callout">
                        <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_AdminUserAccess_Heading%></span><br />
                    <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_AdminUserAccess_SubHeading%><br />
                    <br />
                    <span class="moduleLabel">
                        <%=Resources.RichTemplate_Page_Editor.RichTemplate_Page_Editor_Tab_AdminUserAccess_Users%></span>
                    <asp:CheckBoxList ID="cblAdminUserList" runat="server">
                    </asp:CheckBoxList>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
    </form>
</body>
</html>
