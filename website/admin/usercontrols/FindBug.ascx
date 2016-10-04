<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FindBug.ascx.vb" Inherits="admin_usercontrols_FindBug" %>
<asp:UpdatePanel ID="up_FindBug" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnl_FindBug" runat="server" Visible="false">
            <asp:LinkButton ID="lnk_Dummy_FindBug" runat="server" Text="" />
            <telerik:RadToolTip ID="rtt_FindBug" runat="server" RelativeTo="BrowserWindow" ManualClose="true"
                Sticky="true" TargetControlID="lnk_Dummy_NeedHelp" EnableAjaxSkinRendering="true"
                Animation="None" Position="Center" HideEvent="ManualClose" Modal="true" RenderInPageRoot="true"
                Width="550px" Height="300px" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
                <asp:UpdatePanel ID="up_FindBugInner" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <table class="RadToolTipTable">
                            <tr class="ModalTableRowHeading">
                                <td>
                                    <asp:LinkButton ID="lnkClose_FindBug" runat="server" CssClass="img_link_right" CausesValidation="false"><img src="/admin/images/close_button.jpg" alt="Close"/></asp:LinkButton>
                                    <%= Resources.Header_Admin.Header_FindBug_Heading%>
                                </td>
                            </tr>
                            <tr>
                                <td class="ModalTableRowData">
                                    <div id="div_ScrollerFindBug" class="PopupCellInnerContentScroller">
                                        <table>
                                            <tr>
                                                <td style="width: 500px;">
                                                    <div id="divFindBug" runat="server">
                                                        <table border="0" width="490" cellpadding="4" cellspacing="0">
                                                            <tr>
                                                                <td class="body" colspan="2" height="50">
                                                                    <%= Resources.Header_Admin.Header_FindBug_SubHeading_ErrorEncountered%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="body bodybold" colspan="2" bgcolor="#F4F4F4">
                                                                    <%= Resources.Header_Admin.Header_FindBug_ProvideDescription %>:
                                                                    <asp:RequiredFieldValidator ID="reqBugDesc" runat="server" ErrorMessage=" <%$ Resources:Header_Admin, Header_FindBug_RequiredMessage %>"
                                                                        ControlToValidate="txtBugDesc" CssClass="errorStyle" Display="Dynamic" ValidationGroup="Header_FindBug" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="490" class="body" colspan="2" bgcolor="#F4F4F4" valign="top">
                                                                    <asp:TextBox ID="txtBugDesc" runat="server" TextMode="MultiLine" Rows="4" Width="95%" /><span
                                                                        class="requiredStar" style="vertical-align: top;">*</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="490" class="body" colspan="2">
                                                                    <p class="bodybold">
                                                                        <%= Resources.Header_Admin.Header_FindBug_SubHeading_Contact%></p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="body">
                                                                    <%= Resources.Header_Admin.Header_FindBug_EmailAddress%>:
                                                                </td>
                                                                <td class="body">
                                                                    <asp:TextBox ID="txtEmail" runat="server" Width="200" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="body">
                                                                    <%= Resources.Header_Admin.Header_FindBug_PhoneNumber%>:
                                                                </td>
                                                                <td class="body">
                                                                    <asp:TextBox ID="txtPhone" runat="server" Width="200" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="body" colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="490" class="body" colspan="2">
                                                                    <p align="center">
                                                                        <font size="2" face="verdana, arial, helvetica, sans-serif">
                                                                            <asp:Button ID="btnSubmit" runat="server" Text="<%$ Resources:Header_Admin, Header_FindBug_SubmitReport %>" ValidationGroup="Header_FindBug" /></font></p>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div id="divFindBugSubmitted" runat="server" visible="false">
                                                        <table border="0" width="490" cellspacing="0" cellpadding="6">
                                                            <tr>
                                                                <td>
                                                                    <p class="body">
                                                                        <b><%= Resources.Header_Admin.Header_FindBug_BugSubmitted_Heading%></b>
                                                                    </p>
                                                                    <p class="body">
                                                                        <%= Resources.Header_Admin.Header_FindBug_BugSubmitted_SubHeading%>
                                                                        <a href="http://www.richtemplate.com">www.richtemplate.com</a></p>
                                                                    <div class="divFindBugOurContactDetails" style="">
                                                                        <table border="0" >
                                                                            <tr>
                                                                                <td class="standardText" align="left">
                                                                                    <p class="body">
                                                                                        <b><%= Resources.Header_Admin.Header_FindBug_BugSubmitted_OurPhone%>:</b></p>
                                                                                </td>
                                                                                <td class="standardText">
                                                                                    <p class="body">
                                                                                        (410) 740-5662</p>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="standardText" align="left">
                                                                                    <p class="body">
                                                                                        <b><%= Resources.Header_Admin.Header_FindBug_BugSubmitted_OurPhone%>:</b></p>
                                                                                </td>
                                                                                <td class="standardText">
                                                                                    <p class="body">
                                                                                        (443) 346-0205</p>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="standardText" valign="top" align="left">
                                                                                    <p class="body">
                                                                                        <b><%= Resources.Header_Admin.Header_FindBug_BugSubmitted_OurMailingAddress%>:</b></p>
                                                                                </td>
                                                                                <td class="standardText">
                                                                                    <p class="body">
                                                                                        5850 Waterloo Road&nbsp;<br />
                                                                                        Suite 230<br>
                                                                                        Columbia, MD 21045</p>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </telerik:RadToolTip>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
