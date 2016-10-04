<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_modules_access_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="pageTitle">
        <%=Resources.Member_Admin.Member_Default_Members_Heading%></span><br />
    <div class="img_link_right">
        <asp:LinkButton ID="lnkExportMembers" runat="server">
        <img src="/admin/images/icon_export_excel.jpg" alt="Export Member Data" /></asp:LinkButton>
    </div>
    <br />
    <span class="callout">
        <%=Resources.Member_Admin.Member_Default_Members_SubHeading%></span><br />
    <br />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <img src="/admin/images/process.png" alt="" />
            </td>
            <td>
                <a href="/admin/modules/module/?mtid=8">
                    <%=Resources.Member_Admin.Member_Default_Configure%></a>
            </td>
        </tr>
        <tr>
            <td>
                <img src="/admin/images/process.png" alt="" />
            </td>
            <td>
                <asp:LinkButton ID="lnkImportUsersAndGroups" runat="server" Text="Import Users and Groups"/>
                <asp:Literal ID="litImportUsersAndGroupsComplete" runat="server" Text="<span>&nbsp;(may take a few minutes)</span>" />
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="2">
        <tr>
            <asp:Repeater ID="rptLetters" runat="server">
                <ItemTemplate>
                    <td>
                        <b>
                            <asp:LinkButton runat="server" ID="lnkLetter" Text='<%#DataBinder.Eval(Container.DataItem,"letterUpperCase")%>'
                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"letterUpperCase")%>'></asp:LinkButton></b>&nbsp;
                    </td>
                </ItemTemplate>
            </asp:Repeater>
            <td>
                &nbsp;&nbsp;<b>
                    <asp:LinkButton runat="server" ID="lnkLetterAll" Text="<%$ Resources:Member_Admin, Member_Default_LettersAll%>"
                        CommandArgument="ALL" CssClass=""></asp:LinkButton></b>&nbsp;
            </td>
            <td>
                &nbsp;&nbsp;<b>
                    <asp:LinkButton runat="server" ID="lnkLetterUnassigned" Text="<%$ Resources:Member_Admin, Member_Default_LetterUnAssigned%>"
                        CommandArgument="UNASSIGNED" CssClass=""></asp:LinkButton></b>&nbsp;
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="rgMembers" runat="server" PageSize="20">
        <MasterTableView DataKeyNames="ID" CommandItemDisplay="Top">
            <RowIndicatorColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" ReadOnly="True" SortExpression="ID"
                    UniqueName="ID" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="GroupCount" SortExpression="GroupCount" UniqueName="GroupCount"
                    Visible="False">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="20px" UniqueName="uImage">
                    <ItemTemplate>
                        <asp:Image ID="userImage" runat="server" ImageUrl="/admin/images/business_user.png" /></ItemTemplate>
                    <ItemStyle Width="20px"></ItemStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="<%$ Resources:Member_Admin, Member_Default_GridMember_FullName%>"
                    UniqueName="fullname" SortExpression="lastName">
                    <ItemTemplate>
                        <%#Eval("lastname") %>
                        <%# If( (Eval("lastname").ToString().Trim().Length > 0) AndAlso (Eval("firstname").ToString().Trim().Length > 0), ", " & Eval("firstname"), Eval("firstname")) %>
                        <%# If((Not Eval("ActiveDirectory_Identifier") Is DBNull.Value) AndAlso (Eval("ActiveDirectory_Identifier").ToString().Trim().Length > 0), "<b>" & Resources.Member_Admin.Member_Default_Grid_Key_ActiveDirectory_Icon & "</b>", "")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Email" HeaderText="<%$ Resources:Member_Admin, Member_Default_GridMember_EmailAddress%>"
                    SortExpression="Email" UniqueName="Email">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="username" HeaderText="<%$ Resources:Member_Admin, Member_Default_GridMember_UserName%>"
                    SortExpression="username" UniqueName="username">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="JobTitle" HeaderText="<%$ Resources:Member_Admin, Member_Default_GridMember_Title%>"
                    SortExpression="rich" UniqueName="JobTitle">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="gName" HeaderText="<%$ Resources:Member_Admin, Member_Default_GridMember_Groups%>"
                    Visible="false" UniqueName="gName">
                    <ItemTemplate>
                        <asp:Literal ID="litGroupList" runat="server" Text=''></asp:Literal>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--                <telerik:GridBoundColumn DataField="userActive" HeaderText="Active" SortExpression="userActive"
                    UniqueName="userActive" Visible="false">
                </telerik:GridBoundColumn>--%>
                <%--                <telerik:GridTemplateColumn UniqueName="userStatus" DataField="userActive" HeaderText="Active"
                    ItemStyle-Width="35px" SortExpression="userActive">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbArchiveRestore" runat="server"></asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridTemplateColumn UniqueName="editMember" DataField="ID" HeaderText="<%$ Resources:Member_Admin, Member_Default_GridMember_Edit%>">
                    <ItemTemplate>
                        <a id="aMemberEdit" runat="server">
                            <%= Resources.Member_Admin.Member_Default_GridMember_Edit%></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                    HeaderText="<%$ Resources:Member_Admin, Member_Default_GridMember_Delete%>">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# (Eval("ActiveDirectory_Identifier").ToString().Trim().Length = 0)%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <CommandItemTemplate>
                <div style="padding-left: 10px; height: 20px; padding-top: 5px" class="fLeft">
                    <a href="editAddMember.aspx">
                        <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                        <%=Resources.Member_Admin.Member_Default_GridMember_AddNewEntry%></a>
                </div>
                <div id="divViewMembers" runat="server" visible="false" class="fRight">
                    <asp:RadioButtonList ID="rblViewMembers" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rblViewMembers_OnSelectedIndexChanged">
                        <asp:ListItem Value="False" Text="<%$ Resources:Member_Admin, Member_Default_MembersCurrentSite %>"></asp:ListItem>
                        <asp:ListItem Value="True" Text="<%$ Resources:Member_Admin, Member_Default_MembersAllSites %>"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </CommandItemTemplate>
        </MasterTableView>
    </telerik:RadGrid>
    <div style="float: right; padding-top: 5px">
        <asp:Button ID="btnDeleteMember" runat="server" Text="<%$ Resources:Member_Admin, Member_Default_GridMember_DeleteButton%>"
            OnClick="btnDeleteMember_Click" /></div>
    <br />
    <br />
    <span class="pageTitle">
        <%=Resources.Member_Admin.Member_Default_Groups_Heading%></span><br />
    <span class="callout">
        <%=Resources.Member_Admin.Member_Default_Groups_SubHeading%></span><br />
    <br />
    <telerik:RadGrid ID="rgGroups" runat="server" PageSize="10">
        <MasterTableView DataKeyNames="groupID" CommandItemDisplay="Top">
            <RowIndicatorColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="groupID" DataType="System.Int32" ReadOnly="True"
                    SortExpression="groupID" UniqueName="groupID" Visible="False">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="20px" UniqueName="gImage">
                    <ItemTemplate>
                        <asp:Image ID="groupImage" runat="server" ImageUrl="/admin/images/business_users.png" /></ItemTemplate>
                    <ItemStyle Width="20px"></ItemStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="groupName" SortExpression="groupName" HeaderStyle-Wrap="false"
                    HeaderText="<%$ Resources:Member_Admin, Member_Default_GridGroup_Name%>" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <%# Eval("groupName")%>
                        <%# If((Not Eval("ActiveDirectory_Identifier") Is DBNull.Value) AndAlso (Eval("ActiveDirectory_Identifier").ToString().Trim().Length > 0), "<b>" & Resources.Member_Admin.Member_Default_Grid_Key_ActiveDirectory_Icon & "</b>", "")%><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Member_Admin.Member_Default_GridGroup_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="groupDescription" HeaderText="<%$ Resources:Member_Admin,Member_Default_GridGroup_Description%>"
                    SortExpression="groupDescription" UniqueName="groupDescription">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="groupPassword" HeaderText="<%$ Resources:Member_Admin,Member_Default_GridGroup_Password%>"
                    SortExpression="groupPassword" UniqueName="groupPassword" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="groupActive" HeaderText="<%$ Resources:Member_Admin,Member_Default_GridGroup_Active%>"
                    SortExpression="groupActive" UniqueName="groupActive">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="editGroup" DataField="groupID" HeaderText="<%$ Resources:Member_Admin, Member_Default_GridGroup_Edit%>">
                    <ItemTemplate>
                        <a id="aGroupEdit" runat="server">
                            <%= Resources.Member_Admin.Member_Default_GridGroup_Edit%></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                    HeaderText="<%$ Resources:Member_Admin,Member_Default_GridGroup_Delete%>">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# (Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin()) AndAlso (Eval("ActiveDirectory_Identifier") Is DbNull.Value OR Eval("ActiveDirectory_Identifier").ToString().Trim().Length = 0)%>' />
                    </ItemTemplate>
                    <ItemStyle Width="35px"></ItemStyle>
                </telerik:GridTemplateColumn>
            </Columns>
            <CommandItemTemplate>
                <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                    <a href="editAddGroup.aspx">
                        <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                        <%=Resources.Member_Admin.Member_Default_GridGroup_AddNewEntry%></a>
                </div>
            </CommandItemTemplate>
        </MasterTableView>
    </telerik:RadGrid>
    <div class="divFooter">
        <div class="fLeft">
            <div id="divKeyActiveDirectory" runat="server" visible="false" class="footerIcon">
                <b>
                    <%= Resources.Member_Admin.Member_Default_Grid_Key_ActiveDirectory_Icon%>:</b>
                <asp:Literal ID="litKeyActiveDirectory" runat="server" Text="<%$ Resources:Member_Admin, Member_Default_Grid_Key_ActiveDirectory %>" />
            </div>
            <div class="footerIcon">
                <img src='/admin/images/available_to_all.png' />
                <asp:Literal ID="litKeyAvailableToAllSites" runat="server" Text="<%$ Resources:Member_Admin, Member_Default_GridGroup_Key_AvailableToAllSites %>" />
            </div>
        </div>
        <div class="fRight">
            <asp:Button ID="btnDeleteGroup" runat="server" Text="<%$ Resources:Member_Admin,Member_Default_GridGroup_DeleteButton%>"
                OnClick="btnDeleteGroup_Click" /></div>
    </div>
</asp:Content>
