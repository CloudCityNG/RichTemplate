<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="SiteNotAvailable.aspx.vb" Inherits="_SiteNotAvailable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <%= Resources.SiteNotAvailable_FrontEnd.SiteNotAvailable_Heading%></h1>
    <br />
    <b>
        <%= Resources.SiteNotAvailable_FrontEnd.SiteNotAvailable_Body%></b>
</asp:Content>
