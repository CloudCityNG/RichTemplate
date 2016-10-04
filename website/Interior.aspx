<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage.master" AutoEventWireup="false"
    CodeFile="Interior.aspx.vb" Inherits="Interior" %>
<%@ Register TagPrefix="uc" TagName="CommentsWebInfo" Src="~/UserController/CommentsWebInfo.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="mainContent" runat="server"></asp:Label>
    <uc:CommentsWebInfo ID="ucCommentsWebInfo" runat="server" />
    <asp:PlaceHolder ID="RSSPlaceHolder" runat="server" Visible="false">
        <br />
        <br />
        <div>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <a href="/RssFeedGen.aspx?rss=documentlibrary">
                            <img src="/images/icons/feed-icon-14x14.png" /></a>
                    </td>
                    <td>&nbsp; <%= Resources.DocumentLibrary_FrontEnd.Interior_SubscribeToRssFeed%>
                    </td>
                </tr>
            </table>
        </div>
    </asp:PlaceHolder>
    <br /><br />
</asp:Content>
