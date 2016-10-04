<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Faq_Default_FaqList" %>

<%@ Register TagPrefix="uc" TagName="FaqCatListing" Src="~/Faq/FaqCategorylisting.ascx" %>
<%@ Register TagPrefix="uc" TagName="FaqCatListingArchive" Src="~/Faq/FaqCategorylistingArchive.ascx" %>
<%@ Register TagPrefix="uc" TagName="FaqRepeater" Src="~/Faq/FaqRepeater.ascx" %>
<%@ Register TagPrefix="uc" TagName="FaqDetail" Src="~/Faq/FaqDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divAddFaq" runat="server" visible="false" class="moduleAddLink">
        <a href="SaveFaq.aspx">
            <img alt="add faq" src="/images/AddRecord.gif" /></a>
        <a href="SaveFaq.aspx">
            <%= Resources.Faq_FrontEnd.Faq_DefaultList_Add%></a>
    </div>
    <div class="divModuleContent">
        <div class="divCategoryListContainer">
            <div class="divCategoryList">
                <uc:FaqCatListing ID="ucFaqCatListing" runat="server" />
            </div>
            <div class="divCategoryList">
                <uc:FaqCatListingArchive ID="ucFaqCatListingArchive" runat="server" Visible="false" />
            </div>
        </div>
        <div class="floatL">
            <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                <asp:Literal ID="litModuleDynamicContent" runat="server" />
            </div>
            <uc:FaqRepeater ID="ucFaqRepeater" runat="server" />
            <uc:FaqDetail ID="ucFaqDetail" runat="server" Visible="false" />
        </div>
    </div>
</asp:Content>
