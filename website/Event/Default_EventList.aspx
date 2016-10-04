<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="Default_EventList.aspx.vb" Inherits="Event_Default_EventList" %>

<%@ Register TagPrefix="uc" TagName="EventCatListing" Src="~/Event/EventCategoryListing.ascx" %>
<%@ Register TagPrefix="uc" TagName="EventCatListingArchive" Src="~/Event/EventCategoryListingArchive.ascx" %>
<%@ Register TagPrefix="uc" TagName="EventRepeater" Src="~/Event/EventRepeater.ascx" %>
<%@ Register TagPrefix="uc" TagName="EventDetail" Src="~/Event/EventDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divAddEvent" runat="server" visible="false" class="moduleAddLink">
        <a href="SaveEvent.aspx">
            <img alt="add event" src="/images/AddRecord.gif" /></a>
        <a href="SaveEvent.aspx">
            <%= Resources.Event_FrontEnd.Event_DefaultList_Add%></a>
    </div>
    <div class="divModuleContent">
        <div class="divCategoryListContainer">
            <div class="divCategoryList">
                <uc:EventCatListing ID="ucEventCatListing" runat="server" />
            </div>
            <div class="divCategoryList">
                <uc:EventCatListingArchive ID="ucEventCatListingArchive" runat="server" Visible="false" />
            </div>
        </div>
        <div class="floatL">
            <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                <asp:Literal ID="litModuleDynamicContent" runat="server" />
            </div>
            <uc:EventRepeater ID="ucEventRepeater" runat="server" />
            <uc:EventDetail ID="ucEventDetail" runat="server" Visible="false" />
        </div>
    </div>
</asp:Content>
