<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="Default_StaffList.aspx.vb" Inherits="staff_Default_StaffList" %>

<%@ Register TagPrefix="RichTemplateControls" TagName="staffCatListing" Src="~/staff/staffCategoryListing_Tabs.ascx" %>
<%@ Register TagPrefix="RichTemplateControls" TagName="staffCatListingArchive" Src="~/staff/staffCategoryListingArchive_Tabs.ascx" %>
<%@ Register TagPrefix="RichTemplateControls" TagName="staffRepeater" Src="~/staff/staffRepeater.ascx" %>
<%@ Register TagPrefix="RichTemplateControls" TagName="staffDetail" Src="~/staff/staffDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divStaffList" runat="server" class="divModuleContent">
        <div id="divModuleHeadingRssFeed" runat="server" class="staffRss moduleTitleRss"
            visible="false">
            <a class="rss" href="/RssFeedGen.aspx?rss=staff">
                <img src="/images/feed-icon-14x14.png" />
            </a><a class="rss" href="/RssFeedGen.aspx?rss=staff">
                <%= Resources.Staff_FrontEnd.Staff_DefaultList_RSS %>
            </a>
        </div>
        <div id="divActiveArchive" runat="server" class="divActiveArchive" visible="false">
            <a id="aStaff_Active" runat="server" href="Default.aspx" visible="false"><b>
                <%= Resources.Staff_FrontEnd.Staff_DefaultList_Active%></b></a>
            <asp:Literal ID="litStaff_Active" runat="server" Text="<%$ Resources:Staff_FrontEnd, Staff_DefaultList_Active %>" />&nbsp;|&nbsp;<a
                id="aStaff_Archive" runat="server" href="Default.aspx?archive=1"><b><%= Resources.Staff_FrontEnd.Staff_DefaultList_Archive%></b></a>
            <asp:Literal ID="litStaff_Archive" runat="server" Text="<%$ Resources:Staff_FrontEnd, Staff_DefaultList_Archive %>"
                Visible="false" />
        </div>
        <div class="divCategoryListContainer">
            <div class="divCategoryList">
                &nbsp;
            </div>
        </div>
        <div class="floatL">
            <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                <asp:Literal ID="litModuleDynamicContent" runat="server" />
            </div>
            <RichTemplateControls:staffCatListing ID="ucStaffCatListing" runat="server" />
            <RichTemplateControls:staffCatListingArchive ID="ucStaffCatListingArchive" runat="server"
                Visible="false" />
            <RichTemplateControls:staffRepeater ID="ucStaffRepeater" runat="server" />
        </div>
    </div>
    <div style="width: 100%;">
        <RichTemplateControls:staffDetail ID="ucStaffDetail" runat="server" Visible="false" />
    </div>
</asp:Content>
