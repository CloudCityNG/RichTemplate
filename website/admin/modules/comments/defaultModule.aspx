<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="defaultModule.aspx.vb" Inherits="admin_modules_comments_defaultModule" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divReturnToModule" runat="server" class="img_link_right" visible="false">
        <a id="aReturnToModule_Img" runat="server">
            <img src="/admin/images/arrow_back.jpg" alt="" />
        </a>
        <a id="aReturnToModule" runat="server"><asp:Literal ID="litReturnToModule" runat="server" /></a>
    </div>
    <div id="divViewWebPageComments" runat="server" class="img_link_right" visible="false">
        <b><a href="defaultWebInfo.aspx">
            <%= Resources.Comment_Admin.Comment_DefaultModule_ViewWebPageComments%></a></b>
    </div>
    <span class="callout"><asp:Literal ID="litModuleCommentHeadingBody" runat="server"
        Text="<%$ Resources:Comment_Admin, Comment_DefaultModule_HeadingBodyDefault %>" /></span><br />
    <br />
    <telerik:RadGrid ID="rgModuleComments" runat="server" PageSize="20">
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
                <telerik:GridTemplateColumn HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultModule_GridModuleName %>"
                    UniqueName="moduleName" ItemStyle-Width="150px" Visible="false">
                    <ItemTemplate>
                        <%# LanguageDAL.GetResourceValueForCurrentLanuage( Eval("moduleLanguageFilename_Admin"), "_SiteWide_Comment_DefaultModule_ModuleName")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="recordID" ItemStyle-Width="60px" DataType="System.Int32"
                    HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultModule_GridRecordID %>"
                    ReadOnly="True" SortExpression="recordID" UniqueName="recordID" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultModule_GridComment %>"
                    UniqueName="Comment">
                    <ItemTemplate>
                        <%# "<span title='" & Eval("comment").ToString() & "'>" & CommonWeb.stripHTMLandLimitCharacterCount(Eval("comment").ToString(), 100) & "</span>"%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultModule_GridUsername %>"
                    UniqueName="Username" ItemStyle-Width="150px">
                    <ItemTemplate>
                    <%# If((Not Eval("FirstName") Is DBNull.Value) AndAlso (Not Eval("LastName") Is DBNull.Value), Eval("LastName") & ", " & Eval("FirstName"), Resources.Comment_Admin.Comment_DefaultModule_GridUsername_AnonymousSubmission)%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn ItemStyle-Width="120px"  DataField="DateCreated" HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultWebInfo_GridDateCreated %>"
                    UniqueName="DateCreated" DataFormatString="{0: d}">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultModule_GridApproved %>"
                    UniqueName="active" SortExpression="active" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#If(Convert.ToBoolean(Eval("active")), "<span class='activeField'>" & Resources.Comment_Admin.Comment_DefaultModule_GridApprovedTrue & "</span>", "<span class='inactiveField'>" & Resources.Comment_Admin.Comment_DefaultModule_GridApprovedFalse & "</span>")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultModule_GridEdit %>"
                    UniqueName="edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="50px">
                    <ItemTemplate>
                        <a href='<%# "editAddModule.aspx?ID=" & Eval("id") & If (Not request.querystring("mtID") Is Nothing, "&mtID=" & request.querystring("mtID") & If (Not request.querystring("recordID") Is Nothing, "&recordID=" & request.querystring("recordID") ,"") ,"") %>'>
                            <%# Resources.Comment_Admin.Comment_DefaultModule_GridEdit %>
                        </a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Comment_Admin, Comment_DefaultModule_GridDelete %>">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <div class="divFooter">
        <div class="fRight">
            <asp:Button ID="btnDeleteComment" runat="server" Text="<%$ Resources:Comment_Admin, Comment_DefaultModule_GridDeleteButton%>"
                OnClick="btnDeleteComment_Click" /></div>
    </div>
</asp:Content>
