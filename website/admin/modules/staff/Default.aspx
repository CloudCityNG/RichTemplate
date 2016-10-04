<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_modules_staff_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%=Resources.Staff_Admin.Staff_Default_BodyHeading%></span><br />
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/folder.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/categories/default.aspx?mtid=12">
                                <%=Resources.Staff_Admin.Staff_Default_ManageCategories%></a>
                        </td>
                        <td style="padding-left: 10px">
                            <img src="/admin/images/folder.png" />
                        </td>
                        <td>
                            <a href="/admin/modules/staff/StaffPosition.aspx">
                                <%=Resources.Staff_Admin.Staff_Default_ManagePositions%></a>
                        </td>
                        <td style="padding-left: 10px">
                            <img src="/admin/images/process.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/module/?mtid=12">
                                <%=Resources.Staff_Admin.Staff_Default_Configure%></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadAjaxManager ID="ramStaff" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="rgStaff">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="rgStaff" />
                                <telerik:AjaxUpdatedControl ControlID="rgStaffArchive" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadTabStrip ID="rtsStaff" runat="server" MultiPageID="rmpStaff" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" PageViewID="rpvStaff" Value="0" Text="<%$ Resources:Staff_Admin, Staff_Default_ActiveRecords %>">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" PageViewID="rpvStaffArchive" Value="1" Text="<%$ Resources:Staff_Admin, Staff_Default_ArchiveRecords %>">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="rmpStaff" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
                    <telerik:RadPageView ID="rpvStaff" runat="server">
                        <telerik:RadGrid ID="rgStaff" runat="server" PageSize="20">
                            <MasterTableView DataKeyNames="staffID" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="staffID" DataType="System.Int32" ReadOnly="True"
                                        SortExpression="staffID" UniqueName="staffID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="dateTimeStamp" SortExpression="dateTimeStamp"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Staff_Admin, Staff_Default_GridDateCreated%>">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("dateTimeStamp")).ToString("d")%><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Staff_Admin.Staff_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="<%$ Resources:Staff_Admin, Staff_Default_GridFullName%>"
                                        UniqueName="fullname" SortExpression="lastName">
                                        <ItemTemplate>
                                            <div class="fLeft vcard">
                                                <a href='vCard.aspx?id=<%# Eval("staffID") %>'>
                                                    <img src="/images/vcard.gif" alt="download" width="18" />
                                                </a>
                                            </div>
                                            <div class="fLeft leftPad">
                                                <%#Eval("lastName")%>,
                                                <%#Eval("firstName")%>
                                            </div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="StaffPosition" HeaderText="<%$ Resources:Staff_Admin, Staff_Default_GridStaffPosition %>"
                                        SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Staff_Admin, Staff_Default_GridStaffPosition_NotSpecified %>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EmailAddress" HeaderText="<%$ Resources:Staff_Admin, Staff_Default_GridEmailAddress%>"
                                        SortExpression="EmailAddress" UniqueName="EmailAddress">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="editPR" DataField="staffID" ItemStyle-Width="50px"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Staff_Admin, Staff_Default_GridEdit %>">
                                        <ItemTemplate>
                                            <a id="aStaffEdit" runat="server">
                                                <%= Resources.Staff_Admin.Staff_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Staff_Admin, Staff_Default_GridDelete%>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="editAdd.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                            <%= Resources.Staff_Admin.Staff_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites1" runat="server" Text="<%$ Resources:Staff_Admin, Staff_Default_Key_AvailableToAllSites %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteLive" runat="server" Text="<%$ Resources:Staff_Admin, Staff_Default_GridDeleteButton%>"
                                    OnClick="btnDeleteLive_Click" /></div>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvStaffArchive" runat="server">
                        <telerik:RadGrid ID="rgStaffArchive" runat="server" PageSize="20">
                            <MasterTableView DataKeyNames="staffID" CommandItemDisplay="Top">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="staffID" DataType="System.Int32" ReadOnly="True"
                                        SortExpression="staffID" UniqueName="staffID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="dateTimeStamp" SortExpression="dateTimeStamp"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Staff_Admin, Staff_Default_GridDateCreated%>">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("dateTimeStamp")).ToString("d")%><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Staff_Admin.Staff_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="<%$ Resources:Staff_Admin, Staff_Default_GridFullName%>"
                                        UniqueName="fullname" SortExpression="lastName">
                                        <ItemTemplate>
                                            <div class="fLeft vcard">
                                                <a href='vCard.aspx?id=<%# Eval("staffID") %>'>
                                                    <img src="/images/vcard.gif" alt="download" width="18" />
                                                </a>
                                            </div>
                                            <div class="fLeft leftPad">
                                                <a href='<%# "StaffDetail.aspx?id=" & Eval("staffID") %>'>
                                                    <%#Eval("lastName")%>,
                                                    <%#Eval("firstName")%>
                                                </a>
                                                <%# If(Eval("EndDate") IsNot DBNull.Value AndAlso Convert.ToDateTime(Eval("EndDate")) < DateTime.Now, "<span class='gridImg' title='" & Resources.Staff_Admin.Staff_Default_Grid_Expired & "'><img src='/admin/images/expired.png'/></span>", "")%>
                                            </div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="StaffPosition" HeaderText="<%$ Resources:Staff_Admin, Staff_Default_GridStaffPosition %>"
                                        SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Staff_Admin, Staff_Default_GridStaffPosition_NotSpecified %>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EmailAddress" HeaderText="<%$ Resources:Staff_Admin, Staff_Default_GridEmailAddress%>"
                                        SortExpression="EmailAddress" UniqueName="EmailAddress">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="editPR" DataField="staffID" ItemStyle-Width="50px"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Staff_Admin, Staff_Default_GridEdit %>">
                                        <ItemTemplate>
                                            <a id="aStaffEdit" runat="server">
                                                <%= Resources.Staff_Admin.Staff_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Staff_Admin, Staff_Default_GridDelete%>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="editAdd.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                            <%= Resources.Staff_Admin.Staff_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites2" runat="server" Text="<%$ Resources:Staff_Admin, Staff_Default_Key_AvailableToAllSites %>" />
                                </div>
                                <br class="cBoth" />
                                <div class='footerIcon'>
                                    <img src='/admin/images/expired.png' />
                                    <asp:Literal ID="litKeyExpiredMessage" runat="server" Text="<%$ Resources:Staff_Admin, Staff_Default_Key_Expired %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteArchive" runat="server" Text="<%$ Resources:Staff_Admin, Staff_Default_GridDeleteButton%>"
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
