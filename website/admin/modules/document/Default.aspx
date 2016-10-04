<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_modules_document_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%=Resources.DocumentLibrary_Admin.Document_Default_BodyHeading%></span><br />
                <br />
                <table cellpadding="0" cellspacing="0" class="fLeft">
                    <tr>
                        <td>
                            <img src="/admin/images/folder.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/categories/default.aspx?mtid=3">
                                <%=Resources.DocumentLibrary_Admin.Document_Default_ManageCategories%></a>
                        </td>
                        <td class="leftPad">
                            <img src="/admin/images/process.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/module/?mtid=3">
                                <%=Resources.DocumentLibrary_Admin.Document_Default_Configure%></a>
                        </td>
                    </tr>
                </table>
                <div class="fRight">
                    <img height="20px" width="20px" style="float: left;" src="/admin/images/icon_arrow2.gif"
                        alt="go back"><a href="FolderAdmin.aspx" style="margin-left: 10px; float: left;"><%=Resources.DocumentLibrary_Admin.Document_Default_BackToFolders%></a>
                </div>
                <br class="cBoth" />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadAjaxManager ID="ramDocument" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="rgDocuments">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="rgDocuments" />
                                <telerik:AjaxUpdatedControl ControlID="rgDocumentsArchive" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadTabStrip ID="rtsDocument" runat="server" MultiPageID="rmpDocuments" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" PageViewID="rpvDocument" Value="0" Text="<%$ Resources:DocumentLibrary_Admin, Document_Default_ActiveRecords %>">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="rpvDocumentArchive" Value="1" Text="<%$ Resources:DocumentLibrary_Admin, Document_Default_ArchiveRecords %>">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="rmpDocuments" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
                    <telerik:RadPageView ID="rpvDocument" runat="server">
                        <telerik:RadGrid ID="rgDocuments" runat="server" PageSize="20">
                            <MasterTableView DataKeyNames="documentID" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="documentID" DataType="System.Int32" ReadOnly="True"
                                        SortExpression="documentID" UniqueName="documentID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="fileType" SortExpression="fileType" HeaderStyle-Width="30">
                                        <ItemTemplate>
                                            <img id="imgFileType" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="fileTitle" SortExpression="fileTitle" HeaderStyle-Wrap="false"
                                        HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridTitle%>">
                                        <ItemTemplate>
                                            <%# Eval("fileTitle")%>
                                            <%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.DocumentLibrary_Admin.Document_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridCategory%>"
                                        SortExpression="categoryName" UniqueName="fileTitle" EmptyDataText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridUncategorized %>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridFile%>"
                                        UniqueName="file">
                                        <ItemTemplate>
                                            <asp:Literal ID="litFilePathAndName" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="fileUploadDate" HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridDateUploaded%>"
                                        SortExpression="fileUploadDate" UniqueName="fileUploadDate" DataFormatString="{0: dd/MM/yyyy}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="commentCount" HeaderText="commentCount" SortExpression="commentCount"
                                        UniqueName="commentCount" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="comments" HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridComments%>"
                                        ItemStyle-Wrap="false" ItemStyle-VerticalAlign="Middle" SortExpression="commentCount"
                                        Visible="false">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridTemplateColumn HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridEdit%>"
                                        UniqueName="Edit" SortExpression="Edit" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a id="aDocumentEdit" runat="server">
                                                <%=Resources.DocumentLibrary_Admin.Document_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridDelete%>"
                                        ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="folderAdmin.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                            <%=Resources.DocumentLibrary_Admin.Document_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites1" runat="server" Text="<%$ Resources:DocumentLibrary_Admin, Document_Default_Key_AvailableToAllSites %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteLive" runat="server" OnClick="btnDeleteLive_Click" Text="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridDeleteButton%>" />
                            </div>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvDocumentArchive" runat="server">
                        <telerik:RadGrid ID="rgDocumentsArchive" runat="server">
                            <MasterTableView DataKeyNames="documentID" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="documentID" DataType="System.Int32" ReadOnly="True"
                                        SortExpression="documentID" UniqueName="documentID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="fileType" SortExpression="fileType" HeaderStyle-Width="30">
                                        <ItemTemplate>
                                            <img id="imgFileType" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="fileTitle" SortExpression="fileTitle" HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridTitle%>">
                                        <ItemTemplate>
                                            <%# Eval("fileTitle")%>
                                            <%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.DocumentLibrary_Admin.Document_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                            <%#If(Eval("ExpirationDate") IsNot DBNull.Value AndAlso Convert.ToDateTime(Eval("ExpirationDate")) < DateTime.Now, "<span class='gridImg' title='" & Resources.DocumentLibrary_Admin.Document_Default_Grid_Expired & "'><img src='/admin/images/expired.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridCategory%>"
                                        SortExpression="categoryName" UniqueName="fileTitle" EmptyDataText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridUncategorized %>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridFile%>"
                                        UniqueName="file">
                                        <ItemTemplate>
                                            <asp:Literal ID="litFilePathAndName" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="fileUploadDate" HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridDateUploaded%>"
                                        SortExpression="fileUploadDate" UniqueName="fileUploadDate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="commentCount" HeaderText="commentCount" SortExpression="commentCount"
                                        UniqueName="commentCount" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="comments" HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridComments%>"
                                        ItemStyle-Wrap="false" ItemStyle-VerticalAlign="Middle" SortExpression="commentCount"
                                        Visible="false">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridTemplateColumn HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridEdit%>"
                                        UniqueName="Edit" SortExpression="Edit" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a id="aDocumentEdit" runat="server">
                                                <%=Resources.DocumentLibrary_Admin.Document_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridDelete%>"
                                        ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="folderAdmin.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                            <%=Resources.DocumentLibrary_Admin.Document_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites2" runat="server" Text="<%$ Resources:DocumentLibrary_Admin, Document_Default_Key_AvailableToAllSites %>" />
                                </div>
                                <br class="cBoth" />
                                <div class='footerIcon'>
                                    <img src='/admin/images/expired.png' />
                                    <asp:Literal ID="litKeyExpiredMessage" runat="server" Text="<%$ Resources:DocumentLibrary_Admin, Document_Default_Key_Expired %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteArchive" runat="server" OnClick="btnDeleteArchive_Click"
                                    Text="<%$ Resources:DocumentLibrary_Admin, Document_Default_GridDeleteButton%>" />
                            </div>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
    </table>
</asp:Content>
