<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Event_Default_EventTree" %>

<%@ Register TagPrefix="uc" TagName="CommentsModule" Src="~/UserController/CommentsModule.ascx" %>
<%@ Register TagPrefix="uc" TagName="EventTree" Src="~/Event/EventTree.ascx" %>
<%@ Register TagPrefix="uc" TagName="EventTreeRepeater" Src="~/Event/EventTreeRepeater.ascx" %>
<%@ Register TagPrefix="uc" TagName="EventDetail" Src="~/Event/EventDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divAddEvent" runat="server" visible="false" class="moduleAddLink">
        <a href="SaveEvent.aspx">
            <img alt="add event" src="/images/AddRecord.gif" /></a>
        <a href="SaveEvent.aspx">
            <%= Resources.Event_FrontEnd.Event_DefaultTree_Add%></a>
    </div>
    <div class="divModuleContent">
        <div id="divActiveArchive" runat="server" class="divActiveArchive" visible="false">
            <a id="aEvent_Active" runat="server" href="Default.aspx" visible="false"><b>
                <%= Resources.Event_FrontEnd.Event_DefaultTree_Active%></b></a>
            <asp:Literal ID="litEvent_Active" runat="server" Text="<%$ Resources:Event_FrontEnd, Event_DefaultTree_Active %>" />&nbsp;|&nbsp;<a
                id="aEvent_Archive" runat="server" href="Default.aspx?archive=1"><b><%= Resources.Event_FrontEnd.Event_DefaultTree_Archive%></b></a>
            <asp:Literal ID="litEvent_Archive" runat="server" Text="<%$ Resources:Event_FrontEnd, Event_DefaultTree_Archive %>"
                Visible="false" />
        </div>
        <table>
            <tr>
                <td class="categoryTree" width="220px">
                    <uc:EventTree ID="ucEventTree" runat="server" />
                </td>
                <td>
                    <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                        <asp:Literal ID="litModuleDynamicContent" runat="server" />
                    </div>
                    <uc:EventTreeRepeater ID="ucEventTreeRepeater" runat="server" />
                    <uc:EventDetail ID="ucEventDetail" runat="server" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
    <br class="cBoth" />
</asp:Content>
