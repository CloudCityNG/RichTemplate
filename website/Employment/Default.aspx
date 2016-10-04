<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Employment_Default_EmploymentList" %>

<%@ Register TagPrefix="uc" TagName="EmploymentCatListing" Src="~/Employment/EmploymentCategoryListing.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmploymentCatListingArchive" Src="~/Employment/EmploymentCategoryListingArchive.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmploymentRepeater" Src="~/Employment/EmploymentRepeater.ascx" %>
<%@ Register TagPrefix="uc" TagName="EmploymentDetail" Src="~/Employment/EmploymentDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divAddEmployment" runat="server" visible="false" class="moduleAddLink">
        <a href="SaveEmployment.aspx">
            <img alt="add employment" src="/images/AddRecord.gif" /></a>
        <a href="SaveEmployment.aspx">
            <%= Resources.Employment_FrontEnd.Employment_DefaultList_Add%></a>
    </div>
    <div class="divModuleContent">
        <div class="divCategoryListContainer">
            <div class="divCategoryList">
                <uc:EmploymentCatListing ID="ucEmploymentCatListing" runat="server" />
            </div>
            <div class="divCategoryList">
                <uc:EmploymentCatListingArchive ID="ucEmploymentCatListingArchive" runat="server"
                    Visible="false" />
            </div>
        </div>
        <div class="floatL">
            <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                <asp:Literal ID="litModuleDynamicContent" runat="server" />
            </div>
            <uc:EmploymentRepeater ID="ucEmploymentRepeater" runat="server" />
            <uc:EmploymentDetail ID="ucEmploymentDetail" runat="server" Visible="false" />
        </div>
    </div>
</asp:Content>
