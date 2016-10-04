<%@ Page Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Members.master" AutoEventWireup="false" 
    CodeFile="Interior.aspx.vb" Inherits="member_Interior" Debug="true"%>
<%@ Register TagPrefix="uc" TagName="CommentsWebInfo" Src="~/UserController/CommentsWebInfo.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="mainContent" runat="server"></asp:Label>
    <uc:CommentsWebInfo ID="ucCommentsWebInfo" runat="server" />
    <br /><br />
</asp:Content>
