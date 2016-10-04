<%@ Page Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="404.aspx.vb" Inherits="_404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <h1>
            <%=Resources.Error_FrontEnd.PageNotFound_Heading%></h1>
    </div>
    <br />
    <%=Resources.Error_FrontEnd.PageNotFound_PossibleOptions %>:
    <br />
    <br />
    <ul>
        <li>
            <%=Resources.Error_FrontEnd.PageNotFound_PossibleOptions_Line1 %></li>
        <li>
            <%=Resources.Error_FrontEnd.PageNotFound_PossibleOptions_Line2 %></li>
        <li>
            <%=Resources.Error_FrontEnd.PageNotFound_PossibleOptions_Line3 %></li></ul>
    <br />
</asp:Content>
