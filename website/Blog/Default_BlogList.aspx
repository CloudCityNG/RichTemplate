<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="Default_BlogList.aspx.vb" Inherits="Blog_Default_BlogList" %>

<%@ Register TagPrefix="uc" TagName="BlogCatListing" Src="~/Blog/BlogCategorylisting.ascx" %>
<%@ Register TagPrefix="uc" TagName="BlogCatListingArchive" Src="~/Blog/BlogCategorylistingArchive.ascx" %>
<%@ Register TagPrefix="uc" TagName="BlogRepeater" Src="~/Blog/BlogRepeater.ascx" %>
<%@ Register TagPrefix="uc" TagName="BlogDetail" Src="~/Blog/BlogDetail.ascx" %>
<%@ Register TagPrefix="uc" TagName="CommentsModule" Src="~/UserController/CommentsModule.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divAddBlog" runat="server" visible="false" class="moduleAddLink">
        <a href="SaveBlog.aspx">
            <img alt="add blog" src="/images/AddRecord.gif" /></a>
        <a href="SaveBlog.aspx">
            <%= Resources.Blog_FrontEnd.Blog_DefaultList_Add%></a>
    </div>
    <div class="divModuleContent">
        <div class="divCategoryListContainer">
            <div class="divCategoryList">
                <uc:BlogCatListing ID="ucBlogCatListing" runat="server" />
            </div>
            <div class="divCategoryList">
                <uc:BlogCatListingArchive ID="ucBlogCatListingArchive" runat="server" Visible="false" />
            </div>
        </div>
        <div class="floatL">
            <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                <asp:Literal ID="litModuleDynamicContent" runat="server" />
            </div>
            <uc:BlogRepeater ID="ucBlogRepeater" runat="server" />
            <uc:BlogDetail ID="ucBlogDetail" runat="server" Visible="false" />
        </div>
    </div>
</asp:Content>
