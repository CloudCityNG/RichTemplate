<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="MemberSearch.aspx.vb" Inherits="member_MemberSearch" %>

<%@ Register TagPrefix="uc" TagName="MemberDetail" Src="~/Member/MemberDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divMemberSearch" runat="server">
        <br class="cBoth" />
        <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
            <asp:Literal ID="litModuleDynamicContent" runat="server" />
        </div>
        <br />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" DefaultButton="btnSearch" CssClass="pnlMemberSearch">
                    <div class="divSearchParams">
                        <b>
                            <%= Resources.Member_FrontEnd.Member_MemberSearch_MemberSearch_FirstName%></b><br />
                        <asp:TextBox ID="txtFirstName" runat="server" Width="200px" />
                    </div>
                    <div class="divSearchParams">
                        <b>
                            <%= Resources.Member_FrontEnd.Member_MemberSearch_MemberSearch_LastName%></b><br />
                        <asp:TextBox ID="txtLastName" runat="server" Width="200px" />
                    </div>
                    <br class="clear" />
                    <div class="divSearchParams">
                        <b>
                            <%= Resources.Member_FrontEnd.Member_MemberSearch_MemberSearch_Department%></b><br />
                        <asp:DropDownList ID="ddlLocationCategory" runat="server" AppendDataBoundItems="true"
                            Width="205px" />
                    </div>
                    <div class="divSearchParams">
                        <b>
                            <%= Resources.Member_FrontEnd.Member_MemberSearch_MemberSearch_Location%></b><br />
                        <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="true" Width="205px" />
                    </div>
                    <div class="divSearchParams">
                        <b>
                            <%= Resources.Member_FrontEnd.Member_MemberSearch_MemberSearch_Site%></b><br />
                        <asp:DropDownList ID="ddlSite" runat="server" AppendDataBoundItems="true" Width="205px" />
                    </div>
                    <br class="clear" />
                    <br />
                    <div>
                        <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Member_FrontEnd, Member_MemberSearch_MemberSearch_ButtonSearch %>" />
                    </div>
                </asp:Panel>
                <br />
                <div class="divLetterListTop">
                    <asp:Repeater ID="rptMemberLettersTop" runat="server">
                        <ItemTemplate>
                            &nbsp;<%# Container.DataItem.ToString() %>
                        </ItemTemplate>
                    </asp:Repeater>
                    &nbsp;
                    <a href='/member/'>
                        <%= Resources.Member_FrontEnd.Member_MemberSearch_MemberSearch_ViewAll%></a>
                </div>
                <div class="divMemberList">
                    <telerik:RadGrid ID="rgMember" runat="server">
                        <MasterTableView DataKeyNames="ID" AllowNaturalSort="false" AllowMultiColumnSorting="true"
                            PageSize="20">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" ReadOnly="True" SortExpression="ID"
                                    UniqueName="ID" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="<%$ Resources:Member_FrontEnd, Member_MemberSearch_MemberSearchResults_FullName%>"
                                    DataField="lastName" UniqueName="lastName" Reorderable="true" ShowSortIcon="true"
                                    SortExpression="LastName" HeaderStyle-Width="300">
                                    <ItemTemplate>
                                        <div class="vcard">
                                            <a href='vCard.aspx?id=<%#Eval("id") %>'>
                                                <img src="/images/vcard.gif" alt="download" />
                                            </a>
                                        </div>
                                        <div class="floatL leftPad">
                                            <a href='?id=<%#Eval("id") %>'>
                                                <%# CommonWeb.FormatName( If( Eval("salutationID") Is DBNull.Value, Convert.ToInt32(0), Convert.ToInt32(Eval("salutationID"))) , Eval("firstName").ToString(), Eval("lastName").ToString()) %>
                                            </a>
                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="<%$ Resources:Member_FrontEnd, Member_MemberSearch_MemberSearchResults_EmailAddress%>"
                                    DataField="email" UniqueName="email" Reorderable="true" ShowSortIcon="true" SortExpression="email"
                                    HeaderStyle-CssClass="rgHeader rgSorted">
                                    <ItemTemplate>
                                        <a href='<%# "mailto:" & Eval("email") %>'>
                                            <%#Eval("email")%>
                                        </a>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="CategoryName" HeaderText="<%$ Resources:Member_FrontEnd, Member_MemberSearch_MemberSearchResults_Department%>"
                                    SortExpression="CategoryName" UniqueName="CategoryName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Location" HeaderText="<%$ Resources:Member_FrontEnd, Member_MemberSearch_MemberSearchResults_Location%>"
                                    SortExpression="Location" UniqueName="Location">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SiteName" HeaderText="<%$ Resources:Member_FrontEnd, Member_MemberSearch_MemberSearchResults_Site%>"
                                    SortExpression="SiteName" UniqueName="SiteName">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="divLetterListBottom">
                    <asp:Repeater ID="rptMemberLettersBottom" runat="server">
                        <ItemTemplate>
                            &nbsp;<%# Container.DataItem.ToString() %>
                        </ItemTemplate>
                    </asp:Repeater>
                    &nbsp;
                    <a href='/member/'>
                        <%= Resources.Member_FrontEnd.Member_MemberSearch_MemberSearch_ViewAll%></a>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <uc:MemberDetail ID="ucMemberDetail" runat="server" Visible="false" />
</asp:Content>
