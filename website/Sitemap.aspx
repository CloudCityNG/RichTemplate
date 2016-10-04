<%@ Page Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage.master" AutoEventWireup="false" CodeFile="Sitemap.aspx.vb" Inherits="SiteMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div><h1><%=Resources.SiteMap_FrontEnd.SiteMap_Heading%></h1></div>
 <br />
 <telerik:RadTreeView ID="rtvSiteMap" runat="server" CssClass="SiteMap" />
</asp:Content>

