<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="defaultWebInfo.aspx.vb" Inherits="admin_modules_comments_DefaultWebInfo" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divReturnToWebInfo" runat="server" class="img_link_right" visible="false">
        <a id="aReturnToWebInfo_Img" runat="server">
            <img src="/admin/images/arrow_back.jpg" alt="" />
        </a>
        <a id="aReturnToWebInfo" runat="server">
            <%= Resources.Comment_Admin.Comment_DefaultWebInfo_ReturnToWebpageListing%></a>
    </div>
    <div id="divViewModuleComments" runat="server" class="img_link_right" visible="false">
        <b><a href="defaultModule.aspx">
            <%= Resources.Comment_Admin.Comment_DefaultWebInfo_ViewModuleComments%></a></b>
    </div>
    <span class="callout">
        <%= Resources.Comment_Admin.Comment_DefaultWebInfo_HeadingBody%></span><br />
    <br />
    <telerik:RadGrid ID="rgComments" runat="server" PageSize="20">
        <MasterTableView DataKeyNames="ID">
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
                <telerik:GridBoundColumn DataField="PageName" HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultWebInfo_GridPageName %>"
                    SortExpression="PageName" UniqueName="PageName" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultWebInfo_GridComment %>"
                    UniqueName="Comment">
                    <ItemTemplate>
                        <%# "<span title='" & Eval("comment").ToString() & "'>" & CommonWeb.stripHTMLandLimitCharacterCount(Eval("comment").ToString(), 100) & "</span>"%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultWebInfo_GridUsername %>"
                    UniqueName="Username" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <%# If((Not Eval("FirstName") Is DBNull.Value) AndAlso (Not Eval("LastName") Is DBNull.Value), Eval("LastName") & ", " & Eval("FirstName"), Resources.Comment_Admin.Comment_DefaultWebInfo_GridUsername_AnonymousSubmission)%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn ItemStyle-Width="120px" DataField="DateCreated" HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultWebInfo_GridDateCreated %>"
                    UniqueName="DateCreated">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultWebInfo_GridApproved %>"
                    UniqueName="active" SortExpression="active" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#If(Convert.ToBoolean(Eval("active")), "<span class='activeField'>" & Resources.Comment_Admin.Comment_DefaultWebInfo_GridApprovedTrue & "</span>", "<span class='inactiveField'>" & Resources.Comment_Admin.Comment_DefaultWebInfo_GridApprovedFalse & "</span>")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultWebInfo_GridEdit %>"
                    UniqueName="edit" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href='<%# "editAddWebInfo.aspx?ID=" & Eval("id") & If (Not request.querystring("webinfoID") Is Nothing, "&webInfoID=" & request.querystring("webinfoID"),"") %>'>
                            <%# Resources.Comment_Admin.Comment_DefaultModule_GridEdit %>
                        </a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultWebInfo_GridDelete %>">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <div class="divFooter">
        <div class="fRight">
            <asp:Button ID="btnDeleteComment" runat="server" Text="<%$ Resources:Comment_Admin, Comment_DefaultWebInfo_GridDeleteButton %>" OnClick="btnDeleteComment_Click" /></div>
    </div>
</asp:Content>
