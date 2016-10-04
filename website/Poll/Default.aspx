<%@ Page Title="" Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    CodeFile="Default.aspx.vb" Inherits="Poll_Default" EnableEventValidation="false" %>

<%@ Register TagPrefix="uc" TagName="PollCatListing" Src="~/Poll/PollCategorylisting.ascx" %>
<%@ Register TagPrefix="uc" TagName="PollCatListingArchive" Src="~/Poll/PollCategorylistingArchive.ascx" %>
<%@ Register TagPrefix="uc" TagName="PollRepeater" Src="~/Poll/PollRepeater.ascx" %>
<%@ Register TagPrefix="uc" TagName="PollDetail" Src="~/Poll/PollDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnPollGuid" runat="server" />
    <div id="divAddPoll" runat="server" visible="false" class="moduleAddLink">
        <a href="SavePoll.aspx">
            <img alt="add new poll" src="/images/AddRecord.gif" /></a>
        <a href="SavePoll.aspx">
            <%= Resources.Poll_FrontEnd.Poll_Default_Add%></a>
    </div>
    <div class="divModuleContent">
        <div class="divCategoryListContainer">
            <div class="divCategoryList">
                <uc:PollCatListing ID="ucPollCatListing" runat="server" />
            </div>
            <div class="divCategoryList">
                <uc:PollCatListingArchive ID="ucPollCatListingArchive" runat="server" Visible="false" />
            </div>
        </div>
        <div class="floatL">
            <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                <asp:Literal ID="litModuleDynamicContent" runat="server" />
            </div>
            <uc:PollRepeater ID="ucPollRepeater" runat="server" />
            <uc:PollDetail ID="ucPollDetail" runat="server" Visible="false" />
        </div>
    </div>
</asp:Content>
