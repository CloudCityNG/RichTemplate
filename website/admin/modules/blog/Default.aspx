<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_modules_blog_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%=Resources.Blog_Admin.Blog_Default_BodyHeading%></span><br />
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/folder.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/categories/default.aspx?mtid=1">
                                <%=Resources.Blog_Admin.Blog_Default_ManageCategories%></a>
                        </td>
                        <td style="padding-left: 10px">
                            <img src="/admin/images/process.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/module/?mtid=1">
                                <%=Resources.Blog_Admin.Blog_Default_Configure%></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadAjaxManager ID="ramBlog" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="rgBlogs">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="rgBlogs" />
                                <telerik:AjaxUpdatedControl ControlID="rgBlogsArchive" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadTabStrip ID="rtsBlog" runat="server" MultiPageID="rmpBlogs" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" PageViewID="rpvBlog" Value="0" Text="<%$ Resources:Blog_Admin, Blog_Default_ActiveRecords %>">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="rpvBlogArchive" Value="1" Text="<%$ Resources:Blog_Admin, Blog_Default_ArchiveRecords %>">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="rmpBlogs" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
                    <telerik:RadPageView ID="rpvBlog" runat="server">
                        <telerik:RadGrid ID="rgBlogs" runat="server" PageSize="20">
                            <MasterTableView DataKeyNames="blogID" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="blogID" DataType="System.Int32" ReadOnly="True"
                                        SortExpression="blogID" UniqueName="blogID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="PublicationDate" SortExpression="PublicationDate"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridPublicationDate%>">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("ViewDate")).ToString("d") %><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Blog_Admin.Blog_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="ExpirationDate" DataType="System.DateTime" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridExpirationDate%>"
                                        SortExpression="ExpirationDate" UniqueName="ExpirationDate" DataFormatString="{0:d}"
                                        HeaderStyle-Wrap="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridCategory %>"
                                        SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Blog_Admin, Blog_Default_GridUncategorized %>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Title" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridTitle%>"
                                        SortExpression="Title" UniqueName="Title">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="commentCount" SortExpression="commentCount" UniqueName="commentCount"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="comments" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridComments%>"
                                        ItemStyle-Wrap="false" SortExpression="commentCount" Visible="false">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridTemplateColumn UniqueName="editBlog" DataField="blogID" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridEdit%>">
                                        <ItemTemplate>
                                            <a id="aBlogEdit" runat="server">
                                                <%= Resources.Blog_Admin.Blog_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridDelete%>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="editAdd.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                            <%= Resources.Blog_Admin.Blog_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites1" runat="server" Text="<%$ Resources:Blog_Admin, Blog_Default_Key_AvailableToAllSites %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Blog_Admin, Blog_Default_GridDeleteButton%>"
                                    OnClick="btnDeleteLive_Click" /></div>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvBlogArchive" runat="server">
                        <telerik:RadGrid ID="rgBlogsArchive" runat="server" PageSize="20">
                            <MasterTableView DataKeyNames="blogID" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="blogID" DataType="System.Int32" ReadOnly="True"
                                        SortExpression="blogID" UniqueName="blogID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="PublicationDate" SortExpression="PublicationDate"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridPublicationDate%>">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("ViewDate")).ToString("d") %><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Blog_Admin.Blog_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="ExpirationDate" SortExpression="ExpirationDate"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridExpirationDate%>">
                                        <ItemTemplate>
                                            <%# If(Eval("ExpirationDate") IsNot DBNull.Value, Convert.ToDateTime(Eval("ExpirationDate")).ToString("d") & If(Convert.ToDateTime(Eval("ExpirationDate")) < DateTime.Now, "<span class='gridImg' title='" & Resources.Blog_Admin.Blog_Default_Grid_Expired & "'><img src='/admin/images/expired.png'/></span>", ""), "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridCategory %>"
                                        SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Blog_Admin, Blog_Default_GridUncategorized %>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Title" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridTitle%>"
                                        SortExpression="Title" UniqueName="Title">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="commentCount" SortExpression="commentCount" UniqueName="commentCount"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="comments" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridComments%>"
                                        ItemStyle-Wrap="false" SortExpression="commentCount" Visible="false">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridTemplateColumn UniqueName="editBlog" DataField="blogID" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridEdit%>">
                                        <ItemTemplate>
                                            <a id="aBlogEdit" runat="server">
                                                <%= Resources.Blog_Admin.Blog_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Blog_Admin, Blog_Default_GridDelete%>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="editAdd.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                            <%= Resources.Blog_Admin.Blog_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites2" runat="server" Text="<%$ Resources:Blog_Admin, Blog_Default_Key_AvailableToAllSites %>" />
                                </div>
                                <br class="cBoth" />
                                <div class='footerIcon'>
                                    <img src='/admin/images/expired.png' />
                                    <asp:Literal ID="litKeyExpiredMessage" runat="server" Text="<%$ Resources:Blog_Admin, Blog_Default_Key_Expired %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteArchive" runat="server" Text="<%$ Resources:Blog_Admin, Blog_Default_GridDeleteButton%>"
                                    OnClick="btnDeleteArchive_Click" />
                            </div>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
