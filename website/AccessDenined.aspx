<%@ Page Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="AccessDenined.aspx.vb" Inherits="_AccessDenined" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <h1>
            <%=Resources.Error_FrontEnd.AccessDenined_Heading%></h1>
    </div>
    <br />
    <%=Resources.Error_FrontEnd.AccessDenined_PossibleOption%>
    <br />
    <br />
    
</asp:Content>
