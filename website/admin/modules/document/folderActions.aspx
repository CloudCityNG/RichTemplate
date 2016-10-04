<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="folderActions.aspx.vb" Inherits="admin_modules_document_FolderActions" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td colspan="2">
                    <asp:Literal ID="litFolderAction" runat="server"/>:
                    <asp:Literal ID="lit_FolderAddEditDelete" runat="server" />
                <div id="divDeleteErrorMessage" runat="server" visible="false" class="errorStyle">
                    <%=Resources.DocumentLibrary_Admin.Document_FolderActions_DeleteMessage%>
                </div>
            </td>
        </tr>
        <tr id="trFolderName" runat="server" visible="false">
            <td>
                <%=Resources.DocumentLibrary_Admin.Document_FolderActions_FolderName%>:
            </td>
            <td>
                <asp:TextBox ID="folderName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <br />
                <asp:Button ID="editAddDeleteFolder" runat="server" />
                &nbsp;&nbsp;
                <asp:Button ID="Cancel" runat="server" Text="<%$ Resources:DocumentLibrary_Admin, Document_FolderActions_ButtonCancel%>"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
