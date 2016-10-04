<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Header.ascx.vb" Inherits="admin_usercontrols_Header" %>
<%@ Register TagPrefix="uc" TagName="NeedHelp" Src="~/admin/usercontrols/NeedHelp.ascx" %>
<%@ Register TagPrefix="uc" TagName="FindBug" Src="~/admin/usercontrols/FindBug.ascx" %>
    <script type="text/javascript" src="/js/multiplePageLoadFunctions.js"></script>
    <script language="JavaScript" src="/js/jquery-1.4.2.min.js" type="text/javascript"></script>
<div id="divEditContent" runat="server">
    <table cellpadding="0" cellspacing="0" class="tblEditContent">
        <tr>
            <td>
                <span id="spanPageName" runat="server"><b><font face="Arial" color="#FFFFFF" size="2">
                    <%=PageName%></font></b></span>
            </td>
            <td align="right">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td id="tdNeedHelp" runat="server">
                            <img border="0" src="/admin/images/icon_need_help.gif" width="20" height="28" />
                            <asp:LinkButton ID="lnkNeedHelp" runat="server" CausesValidation="false" CssClass="white bodybold"
                                Visible="true"><%= Resources.Header_Admin.Header_Header_NeedHelp%></asp:LinkButton>
                            <img class="seperator" border="0" src="/admin/images/seperator.gif" />
                        </td>
                        <td id="tdFindBug" runat="server" >
                            <img border="0" src="/admin/images/icon_find_bug.gif" width="20" height="28" />
                            <asp:LinkButton ID="lnkFindBug" runat="server" CausesValidation="false" CssClass="white bodybold"
                                Visible="true"><%= Resources.Header_Admin.Header_Header_FindABug%></asp:LinkButton>
                            <img class="seperator" border="0" src="/admin/images/seperator.gif" />
                        </td>
                        <td id="tdLogout" runat="server">
                            <a target="_top" href="/richadmin/" class="img_link">
                                <img border="0" src="/admin/images/editcontentbg_logout.gif" width="16" height="28" /></a>
                            <a target="_top" href="/richadmin/" class="white bodybold">
                                <%= Resources.Header_Admin.Header_Header_Logout%></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<span class="cBoth" />
<uc:NeedHelp ID="ucNeedHelp" runat="server" />
<uc:FindBug ID="ucFindBug" runat="server" />
