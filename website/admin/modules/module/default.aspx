<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="admin_modules_Module_default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function valModuleBannerImage(source, arguments) {
            arguments.IsValid = getRadUpload('<%= RadUploadModuleBannerImage.ClientID %>').validateExtensions();
        }
    </script>
    <table>
        <tr>
            <td>
                <span class="callout"><asp:Literal ID="litBodyHeading" runat="server" /></span><br />
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <img src="/admin/images/back.png" />
                        </td>
                        <td>
                            <a id="aReturnToModule" runat="server"><asp:Literal ID="litReturnToModule" runat="server" /></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trModuleLocationUrl" runat="server" visible="false">
            <td>
                <label>
                    <span class="moduleLabel"><asp:Literal ID="litModuleLocationUrl_Label" runat="server" />:</span>
                </label>
                &nbsp;<b><asp:Literal ID="litModuleLocationUrl_Value" runat="server" /></b>
            </td>
        </tr>
        <tr>
            <td>
                <label for="txtModuleContentHTML">
                    <span class="moduleLabel">
                        <%= Resources.Module_Admin.Module_Default_ModuleContentHTML%>:</span>
                </label>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadEditor ID="radModuleContentHTML" runat="server" Width="98%">
                </telerik:RadEditor>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divBannerImage" runat="server" visible="false">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <span class="moduleLabel">
                                    <%=Resources.Module_Admin.Module_Default_ModuleBannerImage%>:</span>
                                <br />
                                <span class="graySubText">
                                    <%=Resources.Module_Admin.Module_Default_ModuleBannerImage_Message%><br />
                                    <%=Resources.Module_Admin.Module_Default_ModuleBannerImage_Requirements%></span>
                                <asp:CustomValidator ID="customValModuleBannerImage" runat="server" Display="Dynamic"
                                    ClientValidationFunction="valModuleBannerImage" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Module_Admin.Module_Default_ModuleBannerImage_InvalidFileType%></b>
                                </asp:CustomValidator>
                                <asp:CustomValidator ID="customValModuleBannerImageSizeExceeded" runat="server" Display="Dynamic"
                                    OnServerValidate="customValModuleBannerImageSizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Module_Admin.Module_Default_ModuleBannerImage_MaxFileSizeExceeded%></b>
                                </asp:CustomValidator>
                                <br />
                                <br />
                                <telerik:RadUpload ID="RadUploadModuleBannerImage" runat="server" AllowedFileExtensions=".jpg,.gif,.png"
                                    ControlObjectsVisibility="None" InitialFileInputsCount="1">
                                </telerik:RadUpload>
                                <telerik:RadProgressManager ID="rpManagerModuleBannerImage" runat="server" />
                                <telerik:RadProgressArea ID="rpAreaModuleBannerImage" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadBinaryImage ID="moduleBannerImage" runat="server" CssClass="rightPad"
                                    Visible="false" /><br />
                                <asp:LinkButton ID="lnkDeleteImage" runat="server" Visible="false" OnClick="lnkDeleteImage_Click"
                                    CausesValidation="false"><b><%=Resources.Module_Admin.Module_Default_ModuleBannerImage_Delete%></b></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadToolTip ID="rttUseCommentCaptcha" runat="server" TargetControlID="aCaptchaCode"
                    Position="BottomRight" AutoCloseDelay="30000" IsClientID="true">
                    <div style="width: 195px; padding-left: 12px">
                        <span class="callout">
                            <%= Resources.Module_Admin.Module_Default_UseCaptchaCodeHeading%></span><br />
                        <%= Resources.Module_Admin.Module_Default_UseCaptchaCodeBody%>
                    </div>
                    <img alt="approval" src="/admin/images/captcha.png" style="width: 205px; height: 151px" />
                </telerik:RadToolTip>
                <telerik:RadToolTip ID="rttUseCommentApproval" runat="server" TargetControlID="aCommentApproval"
                    Position="BottomRight" AutoCloseDelay="30000" IsClientID="true">
                    <div style="width: 195px; padding-left: 12px">
                        <span class="callout">
                            <%= Resources.Module_Admin.Module_Default_UseCommentApprovalHeading%></span><br />
                        <%= Resources.Module_Admin.Module_Default_UseCommentApprovalBody%>
                    </div>
                    <img alt="approval" src="/admin/images/approval.png" style="width: 205px; height: 151px" />
                </telerik:RadToolTip>
                <telerik:RadToolTip ID="rttAddThisWidget" runat="server" TargetControlID="aAddThisWidget"
                    Position="BottomRight" AutoCloseDelay="30000" IsClientID="true">
                    <div style="width: 195px; padding-left: 12px">
                        <span class="callout">
                            <%= Resources.Module_Admin.Module_Default_UseAddThisWidgetHeading%></span><br />
                        <%= Resources.Module_Admin.Module_Default_UseAddThisWidgetBody%>
                    </div>
                    <div style="text-align: center">
                        <img alt="addthis" src="/admin/images/addThis.png" style="width: 169px; height: 87px" /></div>
                </telerik:RadToolTip>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divModuleConfigTypeList" runat="server" class="moduleLabel" visible="false">
                    <%=Resources.Module_Admin.Module_Default_ModuleConfigurationItems%>:</div>
                <asp:CheckBoxList ID="cblModuleConfigTypeList" runat="server">
                </asp:CheckBoxList>
                <br />
                <asp:Button ID="btnSubmit" runat="server" Text="<%$ Resources:Module_Admin, Module_Default_ButtonUpdate %>" />
                <br />
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rttCommentCaptcha">
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rttCommentApproval">
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rttAddThisWidget">
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <asp:HiddenField ID="hdnModuleLocation" runat="server" />
</asp:Content>
