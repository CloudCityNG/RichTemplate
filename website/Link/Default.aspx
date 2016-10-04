<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage.master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="link_Default" %>

<%@ Register TagPrefix="RichTemplateControls" TagName="linkRepeater" Src="~/link/linkRepeater.ascx" %>
<%@ Register TagPrefix="RichTemplateControls" TagName="linkDetail" Src="~/link/linkDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divActive" runat="server" style="float: right;">
        <a href="default.aspx">active</a>&nbsp;|&nbsp;<a href="default.aspx?archive=1" style="font-weight: normal;">archive</a></div>
    <div id="divArchive" runat="server" visible="false" style="float: right;">
        <a href="default.aspx" style="font-weight: normal;">active</a>&nbsp;|&nbsp;<b><a
            href="default.aspx?archive=1">archive</a></b></div>
    <div class="dynamicLargeTitle">
        Site Links</div>
    <br />
    <RichTemplateControls:linkRepeater ID="ucLinkRepeater" runat="server" />
    <RichTemplateControls:linkDetail ID="ucLinkDetail" runat="server" Visible="false" />
</asp:Content>
