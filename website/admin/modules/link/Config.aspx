<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master" AutoEventWireup="false"
    CodeFile="Config.aspx.vb" Inherits="admin_modules_link_Config" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Link Module Configuration<
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">Below you modify your link module options.</span><br />
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
                            <asp:LinkButton ID="lbReturnSave" runat="server" Text="Return to Link entries" PostBackUrl="Default.aspx"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="area1" Position="BottomRight"
                    AutoCloseDelay="30000" IsClientID="true">
                    <div style="width: 195px; padding-left: 12px">
                        <span class="callout">Comment Captcha</span><br />
                        Captcha is a challenge-response test used to ensure the form submission is generated
                        by a human and not auto-generated spam.
                    </div>
                    <img alt="approval" src="/admin/images/captcha.png" style="width: 205px; height: 151px" />
                </telerik:RadToolTip>
                <telerik:RadToolTip ID="RadToolTip2" runat="server" TargetControlID="area0" Position="BottomRight"
                    AutoCloseDelay="30000" IsClientID="true">
                    <div style="width: 195px; padding-left: 12px">
                        <span class="callout">Comment Approval</span><br />
                        Comments are published to the live site only after they have been approved by a
                        moderator in the RichTemplate.
                    </div>
                    <img alt="approval" src="/admin/images/approval.png" style="width: 205px; height: 151px" />
                </telerik:RadToolTip>
                <telerik:RadToolTip ID="RadToolTip3" runat="server" TargetControlID="area2" Position="BottomRight"
                    AutoCloseDelay="30000" IsClientID="true">
                    <div style="width: 195px; padding-left: 12px">
                        <span class="callout">AddThis Widget</span><br />
                        The AddThis social networking widget is embedded into each of your links for easy
                        content sharing.
                    </div>
                    <div style="text-align: center">
                        <img alt="addthis" src="/admin/images/addThis.png" style="width: 169px; height: 87px" /></div>
                </telerik:RadToolTip>
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                </asp:CheckBoxList>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Update" />
                <br />
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolTip1">
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
</asp:Content>
