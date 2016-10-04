<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_modules_employment_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%=Resources.Employment_Admin.Employment_Default_BodyHeading%></span><br />
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/folder.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/categories/default.aspx?mtid=4">
                                <%=Resources.Employment_Admin.Employment_Default_ManageCategories%></a>
                        </td>
                        <td style="padding-left: 10px">
                            <img src="/admin/images/process.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/module/?mtid=4">
                                <%=Resources.Employment_Admin.Employment_Default_Configure%></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadAjaxManager ID="ram_Employment" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="rgEmployment">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="rgEmployment" />
                                <telerik:AjaxUpdatedControl ControlID="rgEmploymentArchive" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadTabStrip ID="rtsEmployment" runat="server" MultiPageID="rmpEmployment"
                    SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" PageViewID="rpvEmployment" Value="0" Text="<%$ Resources:Employment_Admin, Employment_Default_ActiveRecords %>">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="rpvEmploymentArchive" Value="1" Text="<%$ Resources:Employment_Admin, Employment_Default_ArchiveRecords %>">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="rmpEmployment" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
                    <telerik:RadPageView ID="rpvEmployment" runat="server">
                        <telerik:RadGrid ID="rgEmployment" runat="server" PageSize="20">
                            <MasterTableView DataKeyNames="employmentID" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="employmentID" DataType="System.Int32" HeaderText="employmentID"
                                        ReadOnly="True" SortExpression="employmentID" UniqueName="employmentID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="PublicationDate" SortExpression="PublicationDate"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Employment_Admin, Employment_Default_GridPublicationDate%>">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("ViewDate")).ToString("d") %><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Employment_Admin.Employment_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="expirationDate" DataType="System.DateTime" HeaderText="<%$ Resources:Employment_Admin, Employment_Default_GridExpirationDate%>"
                                        SortExpression="expirationDate" UniqueName="expirationDate" DataFormatString="{0:d}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Title" HeaderText="<%$ Resources:Employment_Admin, Employment_Default_Grid_Title%>"
                                        SortExpression="Title" UniqueName="Title">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:Employment_Admin, Employment_Default_GridCategory%>"
                                        SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Employment_Admin, Employment_Default_GridUncategorized%>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="appCount" HeaderText="appCount" SortExpression="appCount"
                                        UniqueName="appCount" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="applicants" DataNavigateUrlFields="employmentID"
                                        DataNavigateUrlFormatString="Applicants.aspx?employmentID={0}" HeaderText="<%$ Resources:Employment_Admin, Employment_Default_GridApplicants%>"
                                        SortExpression="appCount" Visible="false">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridTemplateColumn UniqueName="editEmployment" DataField="employmentID"
                                        ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="<%$ Resources:Employment_Admin, Employment_Default_GridEdit%>">
                                        <ItemTemplate>
                                            <a id="aEmploymentEdit" runat="server">
                                                <%=Resources.Employment_Admin.Employment_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Employment_Admin, Employment_Default_GridDelete%>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="editAdd.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                            <%=Resources.Employment_Admin.Employment_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites1" runat="server" Text="<%$ Resources:Employment_Admin, Employment_Default_Key_AvailableToAllSites %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteLive" runat="server" OnClick="btnDeleteLive_Click" Text="<%$ Resources:Employment_Admin, Employment_Default_GridDeleteButton%>" /></div>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvEmploymentArchive" runat="server">
                        <telerik:RadGrid ID="rgEmploymentArchive" runat="server" PageSize="20">
                            <MasterTableView DataKeyNames="employmentID" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="employmentID" DataType="System.Int32" HeaderText="employmentID"
                                        ReadOnly="True" SortExpression="employmentID" UniqueName="employmentID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="PublicationDate" SortExpression="PublicationDate"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Employment_Admin, Employment_Default_GridPublicationDate%>">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("ViewDate")).ToString("d") %><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Employment_Admin.Employment_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="ExpirationDate" SortExpression="ExpirationDate"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Employment_Admin, Employment_Default_GridExpirationDate%>">
                                        <ItemTemplate>
                                            <%# If(Eval("ExpirationDate") IsNot DBNull.Value, Convert.ToDateTime(Eval("ExpirationDate")).ToString("d") & If(Convert.ToDateTime(Eval("ExpirationDate")) < DateTime.Now, "<span class='gridImg' title='" & Resources.Employment_Admin.Employment_Default_Grid_Expired & "'><img src='/admin/images/expired.png'/></span>", ""), "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Title" HeaderText="<%$ Resources:Employment_Admin, Employment_Default_Grid_Title%>"
                                        SortExpression="Title" UniqueName="Title">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:Employment_Admin, Employment_Default_GridCategory%>"
                                        SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Employment_Admin, Employment_Default_GridUncategorized%>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="appCount" HeaderText="appCount" SortExpression="appCount"
                                        UniqueName="appCount" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="applicants" DataNavigateUrlFields="employmentID"
                                        DataNavigateUrlFormatString="Applicants.aspx?employmentID={0}" HeaderText="<%$ Resources:Employment_Admin, Employment_Default_GridApplicants%>"
                                        SortExpression="appCount" Visible="false">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridTemplateColumn UniqueName="editEmployment" DataField="employmentID"
                                        ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="<%$ Resources:Employment_Admin, Employment_Default_GridEdit%>">
                                        <ItemTemplate>
                                            <a id="aEmploymentEdit" runat="server">
                                                <%=Resources.Employment_Admin.Employment_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Employment_Admin, Employment_Default_GridDelete%>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="editAdd.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" /><%=Resources.Employment_Admin.Employment_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites2" runat="server" Text="<%$ Resources:Employment_Admin, Employment_Default_Key_AvailableToAllSites %>" />
                                </div>
                                <br class="cBoth" />
                                <div class='footerIcon'>
                                    <img src='/admin/images/expired.png' />
                                    <asp:Literal ID="litKeyExpiredMessage" runat="server" Text="<%$ Resources:Employment_Admin, Employment_Default_Key_Expired %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteArchive" runat="server" Text="<%$ Resources:Employment_Admin, Employment_Default_GridDeleteButton%>"
                                    OnClick="btnDeleteArchive_Click" /></div>
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
