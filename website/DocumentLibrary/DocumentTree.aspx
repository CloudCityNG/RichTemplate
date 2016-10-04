<%@ Page Title="" Language="VB" MasterPageFile="~/Masterpages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="DocumentTree.aspx.vb" Inherits="document_DocumentTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function onMouseOver(sender, e) {
            e.get_node().unhighlight();
        }    
    </script>
    <div id="divAddDocument" runat="server" visible="false" class="moduleAddLink">
        <a href="SaveDocument.aspx">
            <img alt="add document" src="/images/AddRecord.gif" /></a>
        <a href="SaveDocument.aspx">
            <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentTree_Add%></a>
    </div>
    <div id="divModuleHeadingRssFeed" runat="server" class="docRss moduleTitleRss" visible="false">
        <a class="rss" href="/RssFeedGen.aspx?rss=documentlibrary">
            <img src="/images/feed-icon-14x14.png" />
        </a>
        <a class="rss" href="/RssFeedGen.aspx?rss=documentlibrary">
            <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentTree_RSS%>
        </a>
    </div>
    <div class="divModuleContent">
        <div class="divViewBy">
            <a href="Default.aspx<%= IF(Request.Params("archive") = 1, "?archive=1","") %>"><b>
                <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentTree_ViewList%></b></a>&nbsp;|&nbsp;
            <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentTree_ViewTree%>
        </div>
        <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
            <asp:Literal ID="litModuleDynamicContent" runat="server" />
        </div>
        <table style="width: 400px" class="tblDocumentManager">
            <tr>
                <td>
                    <telerik:RadTreeView ID="rtvDocuments" runat="server" AfterClientHighlight="UpdateStatus"
                        OnClientMouseOver="onMouseOver" ImagesBaseDir="~/images">
                    </telerik:RadTreeView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
