<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="Default_EmploymentTree.aspx.vb" Inherits="Employment_Default_EmploymentTree" %>

<%@ Register TagPrefix="uc" TagName="CommentsModule" Src="~/UserController/CommentsModule.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmploymentTree" Src="~/Employment/EmploymentTree.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmploymentTreeRepeater" Src="~/Employment/EmploymentTreeRepeater.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmploymentDetail" Src="~/Employment/EmploymentDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divAddEmployment" runat="server" visible="false" class="moduleAddLink">
        <a href="SaveEmployment.aspx">
            <img alt="add employment" src="/images/AddRecord.gif" /></a>
        <a href="SaveEmployment.aspx">
            <%= Resources.Employment_FrontEnd.Employment_DefaultTree_Add%></a>
    </div>
    <div class="divModuleContent">
        <div id="divActiveArchive" runat="server" class="divActiveArchive" visible="false">
            <a id="aEmployment_Active" runat="server" href="Default.aspx" visible="false"><b>
                <%= Resources.Employment_FrontEnd.Employment_DefaultTree_Active%></b></a>
            <asp:Literal ID="litEmployment_Active" runat="server" Text="<%$ Resources:Employment_FrontEnd, Employment_DefaultTree_Active %>" />&nbsp;|&nbsp;<a
                id="aEmployment_Archive" runat="server" href="Default.aspx?archive=1"><b><%= Resources.Employment_FrontEnd.Employment_DefaultTree_Archive%></b></a>
            <asp:Literal ID="litEmployment_Archive" runat="server" Text="<%$ Resources:Employment_FrontEnd, Employment_DefaultTree_Archive %>"
                Visible="false" />
        </div>
        <table>
            <tr>
                <td class="categoryTree" width="220px">
                    <uc:EmploymentTree ID="ucEmploymentTree" runat="server" />
                </td>
                <td>
                    <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                        <asp:Literal ID="litModuleDynamicContent" runat="server" />
                    </div>
                    <uc:EmploymentTreeRepeater ID="ucEmploymentTreeRepeater" runat="server" />
                    <uc:EmploymentDetail ID="ucEmploymentDetail" runat="server" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
    <br class="cBoth" />
</asp:Content>
