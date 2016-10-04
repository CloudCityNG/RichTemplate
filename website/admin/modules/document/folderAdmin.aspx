<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="folderAdmin.aspx.vb" Inherits="admin_modules_document_FolderAdmin" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function onMouseOver(sender, e) {
            e.get_node().unhighlight();
        }    
    </script>
    <table cellpadding="0" cellspacing="0" class="fLeft">
        <tr>
            <td>
                <img src="/admin/images/folder.png" alt="" />
            </td>
            <td>
                <a href="/admin/modules/categories/default.aspx?mtid=3">
                    <%=Resources.DocumentLibrary_Admin.Document_FolderAdmin_DocumentTree_ManageCategories%></a>
            </td>
            <td class="leftPad">
                <img src="/admin/images/process.png" alt="" />
            </td>
            <td>
                <a href="/admin/modules/module/?mtid=3">
                    <%=Resources.DocumentLibrary_Admin.Document_FolderAdmin_DocumentTree_Configure%></a>
            </td>
        </tr>
    </table>
    <div class="fRight">
        <img height="20px" width="20px" style="float: left;" src="/admin/images/icon_arrow2.gif"
            alt="go back"><a href="Default.aspx" style="margin-left: 10px; float: left;"><%=Resources.DocumentLibrary_Admin.Document_FolderAdmin_BackToDocuments%></a>
    </div>
    <br class="cBoth" />
    <br />
    <table style="width: 400px" class="tblDocumentManager">
        <tr>
            <td>
                <br />
                <telerik:RadTreeView ID="rtvDocuments" runat="server" AfterClientHighlight="UpdateStatus"
                    OnClientMouseOver="onMouseOver" ImagesBaseDir="~/images" LoadingMessage="<%$ Resources:DocumentLibrary_Admin, Document_FolderAdmin_DocumentTree_LoadingMessage%>">
                </telerik:RadTreeView>
            </td>
        </tr>
    </table>
</asp:Content>
