<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="searchTags_Default" %>

<%@ Register TagPrefix="RichTemplateControls" TagName="searchBox" Src="~/searchTags/searchBox.ascx" %>
<%@ Register TagPrefix="RichTemplateControls" TagName="searchResults" Src="~/searchTags/searchResults.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divModuleDetail" style="width:100%;">
        <table width="100%">
            <tr>
                <td valign="top" style="width:220px;">
                    <RichTemplateControls:searchBox ID="searchBox1" runat="server" />
                </td>
                <td valign="top">
                    <RichTemplateControls:searchResults ID="searchResults1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
