<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="staff_Default_StaffSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divModuleContent">
    <div id="divModuleHeadingRssFeed" runat="server" class="staffRss moduleTitleRss" visible="false">
        <a class="rss" href="/RssFeedGen.aspx?rss=staff">
            <img src="/images/feed-icon-14x14.png" />
        </a>
        <a class="rss" href="/RssFeedGen.aspx?rss=staff">
            <%= Resources.Staff_FrontEnd.Staff_DefaultSearch_RSS %>
        </a>
    </div>
        <div id="divActiveArchive" runat="server" class="divActiveArchive" visible="false">
            <a id="aStaff_Active" runat="server" href="Default.aspx" visible="false"><b>
                <%= Resources.Staff_FrontEnd.Staff_DefaultSearch_Active%></b></a>
            <asp:Literal ID="litStaff_Active" runat="server" Text="<%$ Resources:Staff_FrontEnd, Staff_DefaultSearch_Active %>" />&nbsp;|&nbsp;<a
                id="aStaff_Archive" runat="server" href="Default.aspx?archive=1"><b><%= Resources.Staff_FrontEnd.Staff_DefaultSearch_Archive%></b></a>
            <asp:Literal ID="litStaff_Archive" runat="server" Text="<%$ Resources:Staff_FrontEnd, Staff_DefaultSearch_Archive %>"
                Visible="false" />
        </div>
        <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
            <asp:Literal ID="litModuleDynamicContent" runat="server" />
        </div>
        <asp:UpdatePanel runat="server">
            <contenttemplate>
            <asp:Panel runat="server" DefaultButton="btnSearch" CssClass="pnlStaffSearch">
                <div class="divSearchParams">
                    <b><%= Resources.Staff_FrontEnd.Staff_DefaultSearch_StaffSearch_FirstName%></b><br />
                    <asp:TextBox ID="txtFirstName" runat="server" Width="200px" />
                </div>
                <div class="divSearchParams">
                    <b><%= Resources.Staff_FrontEnd.Staff_DefaultSearch_StaffSearch_LastName%></b><br />
                    <asp:TextBox ID="txtLastName" runat="server" Width="200px" />
                </div>
                <br class="clear" />
                <div class="divSearchParams">
                    <b><%= Resources.Staff_FrontEnd.Staff_DefaultSearch_StaffSearch_Company%></b><br />
                    <asp:TextBox ID="txtCompany" runat="server" Width="200px" />
                </div>
                <div class="divSearchParams">
                    <b><%= Resources.Staff_FrontEnd.Staff_DefaultSearch_StaffSearch_State%></b><br />
                    <asp:DropDownList ID="ddlStates" runat="server" AppendDataBoundItems="true" Width="205px" />
                </div>
                <br class="clear" />
                <div>
                    <b><%= Resources.Staff_FrontEnd.Staff_DefaultSearch_StaffSearch_InstructionsHeading%></b><br />
                    <span class="grayTextSml_10">(<%= Resources.Staff_FrontEnd.Staff_DefaultSearch_StaffSearch_InstructionsBody%>)</span><br />
                    <asp:ListBox ID="lbStaffPositions" runat="server" SelectionMode="Multiple" Rows="5"
                        Width="300px" />
                    <br class="clear" />
                    <br />
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Staff_FrontEnd, Staff_DefaultSearch_StaffSearch_ButtonSearch %>" />
                </div>
            </asp:Panel>
            <br />
            <div class="divStaffList">
                <telerik:RadGrid ID="rgStaff" runat="server">
                    <MasterTableView DataKeyNames="staffID" AllowNaturalSort="false" AllowMultiColumnSorting="true" PageSize="20">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="staffID" DataType="System.Int32"
                                ReadOnly="True" SortExpression="staffID" UniqueName="staffID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Staff_FrontEnd, Staff_DefaultSearch_StaffSearchResults_FullName%>" DataField="lastName" UniqueName="lastName"
                                Reorderable="true" ShowSortIcon="true" SortExpression="lastName" HeaderStyle-Width="300">
                                <ItemTemplate>
                                    <div class="vcard">
                                        <a href="vCard.aspx?id=<%#Eval("staffID") %><%# IF(Request.Params("archive") = 1, "&archive=1","") %>">
                                            <img src="/images/vcard.gif" alt="download" />
                                        </a>
                                    </div>
                                    <div class="floatL leftPad">
                                    <a href="staffDetail.aspx?id=<%#Eval("staffID") %><%# IF(Request.Params("archive") = 1, "&archive=1","") %>">
                                        <%#Eval("lastName")%>,
                                        <%#Eval("firstName")%>
                                    </a>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="company" HeaderText="<%$ Resources:Staff_FrontEnd, Staff_DefaultSearch_StaffSearchResults_Company%>" SortExpression="company"
                                UniqueName="company" HeaderStyle-CssClass="rgHeader rgSorted" HeaderStyle-Width="100">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="staffPosition" HeaderText="<%$ Resources:Staff_FrontEnd, Staff_DefaultSearch_StaffSearchResults_Position%>" SortExpression="staffPosition"
                                UniqueName="staffPosition" HeaderStyle-CssClass="rgHeader rgSorted" HeaderStyle-Width="100">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Staff_FrontEnd, Staff_DefaultSearch_StaffSearchResults_EmailAddress%>" DataField="emailAddress" UniqueName="emailAddress"
                                Reorderable="true" ShowSortIcon="true" SortExpression="emailAddress" HeaderStyle-CssClass="rgHeader rgSorted">
                                <ItemTemplate>
                                    <a href='<%# "mailto:" & Eval("emailAddress") %>'>
                                        <%#Eval("emailAddress")%>
                                    </a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </contenttemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
