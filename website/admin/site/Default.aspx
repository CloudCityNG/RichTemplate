<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    CodeFile="Default.aspx.vb" Inherits="admin_site_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%= Resources.Site_Admin.Site_Default_BodyHeading%></span><br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="rgSite" runat="server" PageSize="20">
                    <MasterTableView DataKeyNames="ID" CommandItemDisplay="None">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" ReadOnly="True"
                                SortExpression="ID" UniqueName="ID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SiteName" HeaderText="<%$ Resources:Site_Admin, Site_Default_GridSiteName %>"
                                SortExpression="SiteName" UniqueName="SiteName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Domain" HeaderText="<%$ Resources:Site_Admin, Site_Default_GridDomainName %>"
                                SortExpression="Domain" UniqueName="Domain">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CompanyName" HeaderText="<%$ Resources:Site_Admin, Site_Default_GridCompanyName %>"
                                SortExpression="CompanyName" UniqueName="CompanyName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PhoneNumber" HeaderText="<%$ Resources:Site_Admin, Site_Default_GridPhoneNumber %>"
                                SortExpression="PhoneNumber" UniqueName="PhoneNumber">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FaxNumber" HeaderText="<%$ Resources:Site_Admin, Site_Default_GridFaxNumber %>"
                                SortExpression="FaxNumber" UniqueName="FaxNumber">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EmailAddress" HeaderText="<%$ Resources:Site_Admin, Site_Default_GridEmailAddress %>"
                                SortExpression="EmailAddress" UniqueName="EmailAddress">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Site_Admin, Site_Default_GridEnableGroupsAndUsers_PublicSection %>"
                                UniqueName="Webpage_PublicSection_EnableGroupsAndUsers" SortExpression="Webpage_PublicSection_EnableGroupsAndUsers">
                                <ItemTemplate>
                                    <%#If(Convert.ToBoolean(Eval("Webpage_PublicSection_EnableGroupsAndUsers")), "<span class='activeField'>" & Resources.Site_Admin.Site_Default_GridEnableGroupsAndUsers_PublicSection_Enabled & "</span>", "<span class='inactiveField'>" & Resources.Site_Admin.Site_Default_GridEnableGroupsAndUsers_PublicSection_Disabled & "</span>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn Visible="false" HeaderText="<%$ Resources:Site_Admin, Site_Default_GridEnableGroupsAndUsers_MemberSection %>"
                                UniqueName="Webpage_MemberSection_EnableGroupsAndUsers" SortExpression="Webpage_MemberSection_EnableGroupsAndUsers">
                                <ItemTemplate>
                                    <%#If(Convert.ToBoolean(Eval("Webpage_MemberSection_EnableGroupsAndUsers")), "<span class='activeField'>" & Resources.Site_Admin.Site_Default_GridEnableGroupsAndUsers_MemberSection_Enabled & "</span>", "<span class='inactiveField'>" & Resources.Site_Admin.Site_Default_GridEnableGroupsAndUsers_MemberSection_Disabled & "</span>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Site_Admin, Site_Default_GridSiteStatus %>"
                                UniqueName="Status" SortExpression="Status">
                                <ItemTemplate>
                                    <%#If(Convert.ToBoolean(Eval("Status")), "<span class='activeField'>" & Resources.Site_Admin.Site_Default_GridSiteStatusActive & "</span>", "<span class='inactiveField'>" & Resources.Site_Admin.Site_Default_GridSiteStatusArchive & "</span>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="editSite" DataField="ID" HeaderText="<%$ Resources:Site_Admin, Site_Default_GridEdit %>">
                                <ItemTemplate>
                                    <a id="aSiteEdit" runat="server">
                                        <%= Resources.Site_Admin.Site_Default_GridEdit%></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                <a href="editAdd.aspx">
                                    <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                    <%= Resources.Site_Admin.Site_Default_Grid_AddNewEntry%></a>
                            </div>
                        </CommandItemTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
