<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Blog_Default_BlogTree" %>

<%@ Register TagPrefix="uc" TagName="CommentsModule" Src="~/UserController/CommentsModule.ascx" %>
<%@ Register TagPrefix="uc" TagName="BlogTree" Src="~/Blog/BlogTree.ascx" %>
<%@ Register TagPrefix="uc" TagName="BlogTreeRepeater" Src="~/Blog/BlogTreeRepeater.ascx" %>
<%@ Register TagPrefix="uc" TagName="BlogDetail" Src="~/Blog/BlogDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divAddBlog" runat="server" visible="false" class="moduleAddLink">
        <a href="SaveBlog.aspx">
            <img alt="add blog" src="/images/AddRecord.gif" /></a>
        <a href="SaveBlog.aspx">
            <%= Resources.Blog_FrontEnd.Blog_DefaultTree_Add%></a>
    </div>
    <div class="divModuleContent">
        <div id="divActiveArchive" runat="server" class="divActiveArchive" visible="false">
            <a id="aBlog_Active" runat="server" href="Default.aspx" visible="false"><b>
                <%= Resources.Blog_FrontEnd.Blog_DefaultTree_Active%></b></a>
            <asp:Literal ID="litBlog_Active" runat="server" Text="<%$ Resources:Blog_FrontEnd, Blog_DefaultTree_Active %>" />&nbsp;|&nbsp;<a
                id="aBlog_Archive" runat="server" href="Default.aspx?archive=1"><b><%= Resources.Blog_FrontEnd.Blog_DefaultTree_Archive%></b></a>
            <asp:Literal ID="litBlog_Archive" runat="server" Text="<%$ Resources:Blog_FrontEnd, Blog_DefaultTree_Archive %>"
                Visible="false" />
        </div>
        <table>
            <tr>
                <td class="categoryTree" width="220px">
                    <uc:BlogTree ID="ucBlogTree" runat="server" />
                </td>
                <td>
                    <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                        <asp:Literal ID="litModuleDynamicContent" runat="server" />
                    </div>
                    <uc:BlogTreeRepeater ID="ucBlogTreeRepeater" runat="server" />
                    <uc:BlogDetail ID="ucBlogDetail" runat="server" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
    <br class="cBoth" />
</asp:Content>
