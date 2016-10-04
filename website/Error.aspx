<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="Error.aspx.vb" Inherits="_Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        <%= Resources.Error_FrontEnd.Error_Heading%></h1>
    <br />
    <b>
        <%= Resources.Error_FrontEnd.Error_Body%></b>
</asp:Content>
