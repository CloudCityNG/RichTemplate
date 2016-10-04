<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="DocumentDetail.aspx.vb" Inherits="DocumentLibrary_DocumentDetail" %>

<%@ Register TagPrefix="uc" TagName="CommentsModule" Src="~/UserController/CommentsModule.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divModuleContent">
        <table width="100%">
            <tr>
                <td style="width:220px;">
                    <div class="divCategoryListContainer">
                        &nbsp;
                    </div>
                </td>
                <td>
                    <div class="docDetail">
                        <div class="imgLinkDiv">
                            <a href="/DocumentLibrary/Default.aspx<%= IF( Not Request.Params("archive") Is Nothing, "?archive=" + Request.Params("archive"),"") %>"
                                class="moduleViewAll">(<%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentDetail_ViewAllDocuments%>)</a>
                        </div>
                        <div class="floatL">
                            <i>
                                <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentDetail_PostedBy%>:
                                <asp:Literal ID="litPostedBy" runat="server" /> - <asp:Literal ID="litViewDate" runat="server" />
                                <%=Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentDetail_PostedBy_DateTimeSeperator%>
                                <asp:Literal ID="litViewDateTime" runat="server" /> </i>
                        </div>
                        <div id="divEditDocument" runat="server" class="leftPad floatL" visible="false">
                            <a class="aModuleEdit" href="SaveDocument.aspx?id=<%= Request.Params("id") %>">
                                <img src="/images/icon_edit.gif" alt="edit" /></a>
                        </div>
                        <br class="cBoth" />
                        <br />
                        <div class="item">
                            <div class="docImage floatL">
                                <img id="imgFileType" runat="server" />
                            </div>
                            <div class="docInfo floatL">
                                <a href='downloadDocument.aspx?id=<%= Request.Params("id")%><%= IF( Not Request.Params("archive") Is Nothing, "&archive=" + Request.Params("archive"),"") %>' target='_blank'>
                                    <asp:Literal ID="litFileTitle" runat="server" /></a>
                            </div>
                            <div class="docInfo floatL leftPad">
                                <asp:Literal ID="litFileSize" runat="server" />
                            </div>
                            <div class="floatL save">
                                <a href='downloadDocument.aspx?id=<%= Request.Params("id")%><%= IF( Not Request.Params("archive") Is Nothing, "&archive=" + Request.Params("archive"),"") %>' target='_blank'>
                                    <img src="/images/save.png" alt="save" /></a>
                            </div>
                            <br class="cBoth" />
                        </div>
                        <br class="cBoth" />
                        <br />
                        <div id="divCategory" runat="server" visible="false">
                            <b>
                                <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentDetail_Category%>:</b>&nbsp;<asp:Literal
                                    ID="litCategory" runat="server" /><br />
                            <br />
                        </div>
                        <div id="divDescription" runat="server" visible="false">
                            <b>
                                <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentDetail_Description%>:</b>&nbsp;<asp:Literal
                                    ID="litDescription" runat="server" /><br />
                            <br />
                        </div>
                        <div id="divRating" runat="server" visible="false">
                            <b class="floatL">
                                <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_DocumentDetail_AverageRating%>:</b>&nbsp;<telerik:RadRating
                                    ID="radAveRating" runat="server" CssClass="docRating floatL" Precision="Half"
                                    ReadOnly="true" />
                            &nbsp; <span class="floatL grayText">(<asp:Literal ID="litCommentCount" runat="server"
                                Text="0 comments" />)</span>
                        </div>
                    </div>
                    <br class="clear" />
                    <hr />
                    <br />
                    <uc:CommentsModule ID="ucCommentsModule" runat="server" />
                    <asp:PlaceHolder ID="plcAddThis" runat="server" Visible="false">
                        <br />
                        <!-- AddThis Button BEGIN -->
                        <div class="addthis_toolbox addthis_default_style">
                            <a href="http://www.addthis.com/bookmark.php" class="addthis_button" style="text-decoration: none"
                                addthis:url="http://<%=Request.ServerVariables("http_host") %><%=Request.Path.toString() %>?id=<%# Request.Params("id") %>">
                                <img src="http://s7.addthis.com/static/btn/v2/lg-bookmark-en.gif" width="125" height="16"
                                    border="0" alt="Share" /></a>
                        </div>
                        <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=xa-4bbf57a32e8aa403"></script>
                        <!-- AddThis Button END -->
                    </asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
