<%@ Page Title="" Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    CodeFile="Default.aspx.vb" Inherits="PressRelease_Default_PressReleaseTree" %>

<%@ Register TagPrefix="uc" TagName="PressReleaseTree" Src="~/PressRelease/PressReleaseTree.ascx" %>
<%@ Register TagPrefix="uc" TagName="PressReleaseTreeRepeater" Src="~/PressRelease/PressReleaseTreeRepeater.ascx" %>
<%@ Register TagPrefix="uc" TagName="PressReleaseDetail" Src="~/PressRelease/PressReleaseDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divAddPressRelease" runat="server" visible="false" class="moduleAddLink">
        <a href="SavePressRelease.aspx">
            <img alt="add press release" src="/images/AddRecord.gif" /></a>
        <a href="SavePressRelease.aspx">
            <%= Resources.PressRelease_FrontEnd.PressRelease_DefaultTree_Add%></a>
    </div>
    <div class="divModuleContent">
        <div id="divActiveArchive" runat="server" class="divActiveArchive" visible="false">
            <a id="aPressRelease_Active" runat="server" href="Default.aspx" visible="false"><b>
                <%= Resources.PressRelease_FrontEnd.PressRelease_DefaultTree_Active%></b></a>
            <asp:Literal ID="litPressRelease_Active" runat="server" Text="<%$ Resources:PressRelease_FrontEnd, PressRelease_DefaultTree_Active %>" />&nbsp;|&nbsp;<a
                id="aPressRelease_Archive" runat="server" href="Default.aspx?archive=1"><b><%= Resources.PressRelease_FrontEnd.PressRelease_DefaultTree_Archive%></b></a>
            <asp:Literal ID="litPressRelease_Archive" runat="server" Text="<%$ Resources:PressRelease_FrontEnd, PressRelease_DefaultTree_Archive %>"
                Visible="false" />
        </div>
        <table>
            <tr>
                <td class="categoryTree" width="220px">
                    <uc:PressReleaseTree ID="ucPressReleaseTree" runat="server" />
                </td>
                <td>
                    <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
                        <asp:Literal ID="litModuleDynamicContent" runat="server" />
                    </div>
                    <uc:PressReleaseTreeRepeater ID="ucPressReleaseTreeRepeater" runat="server" />
                    <uc:PressReleaseDetail ID="ucPressReleaseDetail" runat="server" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
    <br class="cBoth" />
</asp:Content>
