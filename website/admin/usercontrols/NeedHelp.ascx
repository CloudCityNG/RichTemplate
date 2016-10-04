<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NeedHelp.ascx.vb" Inherits="admin_usercontrols_NeedHelp" %>
<asp:UpdatePanel ID="up_NeedHelp" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnl_NeedHelp" runat="server" Visible="false">
            <asp:LinkButton ID="lnk_Dummy_NeedHelp" runat="server" Text="" />
            <telerik:RadToolTip ID="rtt_NeedHelp" runat="server" RelativeTo="BrowserWindow" ManualClose="true"
                Sticky="true" TargetControlID="lnk_Dummy_NeedHelp" EnableAjaxSkinRendering="true"
                Animation="None" Position="Center" HideEvent="ManualClose" Modal="true" RenderInPageRoot="true"
                Width="750px" Height="300px" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
                <asp:UpdatePanel ID="up_NeedHelpInner" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <table class="RadToolTipTable">
                            <tr class="ModalTableRowHeading">
                                <td>
                                    <asp:LinkButton ID="lnkClose_NeedHelp" runat="server" CssClass="img_link_right" CausesValidation="false"><img src="/admin/images/close_button.jpg" alt="Close"/></asp:LinkButton>
                                    <%= Resources.Header_Admin.Header_NeedHelp_Heading%>
                                </td>
                            </tr>
                            <tr>
                                <td class="ModalTableRowData">
                                    <div id="div_ScrollerNeedHelp" class="PopupCellInnerContentScroller">
                                        <table width="100%">
                                            <tr>
                                                <td class="helpBackground">
                                                    <div id="div_FixedHelpImage" style="width: 80px; position: fixed;">
                                                        <img src="/admin/images/help_left2_head.gif" alt="Help" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div id="divHelp_All" runat="server" visible="false">
                                                        <table border="0" width="100%">
                                                            <tr>
                                                                <td style="border: 1px solid #BDD7F7; vertical-align: top;" class="body">
                                                                    <b><font size="2" face="Arial" color="#3054A7"><a name="top"></a><%= Resources.Header_Admin.Header_NeedHelp_TableOfContents%></font>
                                                                    </b>
                                                                    <ol>
                                                                        <asp:Repeater ID="rptTableOfConents" runat="server">
                                                                            <ItemTemplate>
                                                                                <li><a id="aHelpItemTitle" runat="server" href='<%# PageURL & "#" & encodeHelpItemTitle(Eval("Title")) %>'>
                                                                                    <asp:Literal ID="litHelpItemTitle" runat="server" Text='<%# Eval("Title") %>' />
                                                                                </a></li>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </ol>
                                                                    <p>
                                                                        &nbsp;</p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="border: 1px solid #BDD7F7; vertical-align: top;" class="bodynew">
                                                                    <p class="body">
                                                                        <table border="0" width="100%">
                                                                            <asp:Repeater ID="rptHelpContent" runat="server">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td class="body" colspan="2">
                                                                                            &nbsp;<a name='<%# encodeHelpItemTitle(Eval("Title")) %>'></a><br>
                                                                                            <a href="#top"><%= Resources.Header_Admin.Header_NeedHelp_GoToTop%></a>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="2%">
                                                                                            <img border="0" src="/admin/images/icon_help_big.gif" width="32" height="32" align="right">
                                                                                        </td>
                                                                                        <td width="160%" height="50" class="bodybold">
                                                                                            <p class="bodybold">
                                                                                                <asp:Literal ID="litHelpTitle" runat="server" Text='<%# Eval("Title") %>' />
                                                                                            </p>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="162%" colspan="2" class="body">
                                                                                            <asp:Literal ID="litHelpHtmlContent" runat="server" Text='<%# Eval("HtmlContent") %>' />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="body" colspan="2">
                                                                                            &nbsp;<hr />
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </table>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div id="divHelp_Individual" runat="server" visible="false">
                                                        <table border="0" width="100%">
                                                            <tr>
                                                                <td style="border: 1px solid #BDD7F7; vertical-align: top;" class="bodynew">
                                                                    <p class="body">
                                                                        <table border="0" width="100%">
                                                                            <tr>
                                                                                <td valign="top" class="body" colspan="2">
                                                                                    <b><font size="2" face="Arial" color="#3054A7">
                                                                                        <asp:LinkButton ID="lnkShowAll" runat="server" Text="<%$ Resources:Header_Admin, Header_NeedHelp_ShowAll %>" />
                                                                                    </font></b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="2%">
                                                                                    <img border="0" src="/admin/images/icon_help_big.gif" width="32" height="32" align="right">
                                                                                </td>
                                                                                <td width="160%" height="50" class="bodybold">
                                                                                    <p class="bodybold">
                                                                                        <asp:Literal ID="litHelpTitle" runat="server" />
                                                                                    </p>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="162%" colspan="2" class="body">
                                                                                    <asp:Literal ID="litHelpHtmlContent" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="body" colspan="2">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    </p>
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
