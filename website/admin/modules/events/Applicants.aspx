<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Applicants.aspx.vb" Inherits="admin_modules_event_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%=Resources.Event_Admin.Event_Applicants_BodyHeading%></span><br />
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/back.png" />
                        </td>
                        <td>
                            <a href="Default.aspx">
                                <%=Resources.Event_Admin.Event_Applicants_BackToEvents%></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="rgEventRegistrations" runat="server" PageSize="20">
                    <MasterTableView DataKeyNames="subID" CommandItemDisplay="Top">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="subID" DataType="System.Int32" HeaderText="subID"
                                ReadOnly="True" SortExpression="subID" UniqueName="subID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="eventID" DataType="System.Int32" HeaderText="eventID"
                                ReadOnly="True" SortExpression="eventID" UniqueName="eventID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Event_Admin, Event_Applicants_GridSubmissionDate %>"
                                UniqueName="dateTimeStamp" SortExpression="dateTimeStamp" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <%# Convert.ToDateTime(Eval("dateTimeStamp")).ToString("d") %><%#If(Convert.ToBoolean(Eval("Status")), "", " <span class='errorStyle'>(" & Resources.Event_Admin.Event_Applicants_GridSubmissionDate_Cancelled & ")</span>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Event_Admin, Event_Applicants_GridMemberID %>"
                                UniqueName="memberID" SortExpression="memberID" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <%#If(Eval("MemberID") Is DBNull.Value, "", Eval("MemberID"))%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Event_Admin, Event_Applicants_GridName %>"
                                UniqueName="fullname" SortExpression="lastName">
                                <ItemTemplate>
                                    <%#Eval("lastname") %>,
                                    <%#Eval("firstName")%></ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="phoneNumber" HeaderText="<%$ Resources:Event_Admin, Event_Applicants_GridPhoneNumber %>"
                                SortExpression="phoneNumber" UniqueName="phoneNumber">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="email" HeaderText="<%$ Resources:Event_Admin, Event_Applicants_GridEmailAddress %>"
                                SortExpression="email" UniqueName="email">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="applicants" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Event_Admin, Event_Applicants_GridEdit %>"
                                SortExpression="appCount">
                                <ItemTemplate>
                                    <a href='<%# "AppDetails.aspx?eventID=" & Request.QueryString("eventID") & "&subID=" & Eval("SubID") %>'>
                                        <%=Resources.Event_Admin.Event_Applicants_GridEdit%></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Event_Admin, Event_Applicants_GridDelete %>">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                <a href='<%# "appDetails.aspx?eventID=" & Request.QueryString("eventID") %>'>
                                    <img alt="" src="/admin/images/AddRecord.gif" /><%=Resources.Event_Admin.Event_Applicants_Grid_AddNewEntry%></a>
                            </div>
                        </CommandItemTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
                <div class="divFooter">
                    <div class="fRight">
                        <asp:Button ID="btnDeleteRegistration" runat="server" Text="<%$ Resources:Event_Admin, Event_Applicants_GridDeleteButton %>" OnClick="btnDeleteRegistration_Click" /></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
