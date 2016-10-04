<%@ Page Title="" Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    CodeFile="Default_PressReleaseList.aspx.vb" Inherits="PressRelease_Default_PressReleaseList" %>

<%@ Register TagPrefix="uc" TagName="PressReleaseCatListing" Src="~/PressRelease/PressReleaseCategorylisting.ascx" %>
<%@ Register TagPrefix="uc" TagName="PressReleaseCatListingArchive" Src="~/PressRelease/PressReleaseCategorylistingArchive.ascx" %>
<%@ Register TagPrefix="uc" TagName="PressReleaseRepeater" Src="~/PressRelease/PressReleaseRepeater.ascx" %>
<%@ Register TagPrefix="uc" TagName="PressReleaseDetail" Src="~/PressRelease/PressReleaseDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divAddPressRelease" runat="server" visible="false" class="moduleAddLink">
        <a href="SavePressRelease.aspx">
            <img alt="add press release" src="/images/AddRecord.gif" /></a>
        <a href="SavePressRelease.aspx">
            <%= Resources.PressRelease_FrontEnd.PressRelease_DefaultList_Add%></a>
    </div>
    <div class="divModuleContent">
        <div class="divCategoryListContainer">
            <div class="divCategoryList">
                <uc:PressReleaseCatListing ID="ucPressReleaseCatListing" runat="server" />
            </div>
            <div class="divCategoryList">
                <uc:PressReleaseCatListingArchive ID="ucPressReleaseCatListingArchive" runat="server"
                    Visible="false" />
            </div>
        </div>
        <div class="floatL">
            <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                <asp:Literal ID="litModuleDynamicContent" runat="server" />
            </div>
            <uc:PressReleaseRepeater ID="ucPressReleaseRepeater" runat="server" />
            <uc:PressReleaseDetail ID="ucPressReleaseDetail" runat="server" Visible="false" />
        </div>
    </div>
</asp:Content>
