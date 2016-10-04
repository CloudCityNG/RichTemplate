<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_modules_poll_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <span class="callout">
                    <%= Resources.Poll_Admin.Poll_Default_BodyHeading%></span><br />
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/folder.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/categories/default.aspx?mtid=9">
                                <%= Resources.Poll_Admin.Poll_Default_ManageCategories%></a>
                        </td>
                        <td style="padding-left: 10px">
                            <img src="/admin/images/process.png" alt="" />
                        </td>
                        <td>
                            <a href="/admin/modules/module/?mtid=9">
                                <%= Resources.Poll_Admin.Poll_Default_Configure%></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadAjaxManager ID="ramPoll" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="rgPoll">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="rgPoll" />
                                <telerik:AjaxUpdatedControl ControlID="rgPollArchive" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadTabStrip ID="rtsPoll" runat="server" MultiPageID="rmpPoll" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="<%$ Resources:Poll_Admin, Poll_Default_ActiveRecords %>"
                            PageViewID="rpvPoll" Value="0">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="<%$ Resources:Poll_Admin, Poll_Default_ArchiveRecords %>"
                            PageViewID="rpvPollArchive" Value="1">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="rmpPoll" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
                    <telerik:RadPageView ID="rpvPoll" runat="server">
                        <telerik:RadGrid ID="rgPoll" runat="server" PageSize="20">
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
                                    <telerik:GridTemplateColumn UniqueName="PublicationDate" SortExpression="PublicationDate"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridPublicationDate%>">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("ViewDate")).ToString("d") %><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Poll_Admin.Poll_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="ExpirationDate" DataType="System.DateTime" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridExpirationDate%>"
                                        SortExpression="ExpirationDate" UniqueName="ExpirationDate" DataFormatString="{0:d}"
                                        HeaderStyle-Wrap="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridCategory %>"
                                        SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Poll_Admin, Poll_Default_GridUncategorized %>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="QuestionHtml" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridQuestion %>"
                                        SortExpression="Question" UniqueName="Question" HeaderStyle-Width="300px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridAnswersRandomized %>"
                                        UniqueName="QuestionRandomized">
                                        <ItemTemplate>
                                            <%#If(Convert.ToBoolean(Eval("AnswersRandomized").ToString()), "<span class='activeField'>" & Resources.Poll_Admin.Poll_Default_GridAnswersRandomizedTrue & "</span>", "<span class='inactiveField'>" & Resources.Poll_Admin.Poll_Default_GridAnswersRandomizedFalse & "</span>")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="answerCount" SortExpression="answerCount" UniqueName="answerCount"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="answers" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Answers.aspx?pollID={0}"
                                        HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridAnswers %>" ItemStyle-Wrap="false"
                                        ItemStyle-VerticalAlign="Middle" SortExpression="answersCount">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn DataField="submissionCount" SortExpression="submissionCount"
                                        UniqueName="submissionCount" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="submissions" DataNavigateUrlFields="ID"
                                        DataNavigateUrlFormatString="Submissions.aspx?pollID={0}" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridSubmissions %>"
                                        ItemStyle-Wrap="false" ItemStyle-VerticalAlign="Middle" SortExpression="submissionCount">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn DataField="commentCount" SortExpression="commentCount" UniqueName="commentCount"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="comments" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridComments %>"
                                        ItemStyle-Wrap="false" SortExpression="commentCount" Visible="false">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridTemplateColumn UniqueName="editPoll" DataField="ID" ItemStyle-Width="50px"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridEdit %>">
                                        <ItemTemplate>
                                            <a id="aPollEdit" runat="server">
                                                <%= Resources.Poll_Admin.Poll_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridDelete %>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="editAdd.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                            <%= Resources.Poll_Admin.Poll_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites1" runat="server" Text="<%$ Resources:Poll_Admin, Poll_Default_Key_AvailableToAllSites %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteLive" runat="server" Text="<%$ Resources:Poll_Admin, Poll_Default_GridDeleteButton %>"
                                    OnClick="btnDeleteLive_Click" /></div>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="rpvPollArchive" runat="server">
                        <telerik:RadGrid ID="rgPollArchive" runat="server" PageSize="20">
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
                                    <telerik:GridTemplateColumn UniqueName="PublicationDate" SortExpression="PublicationDate"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridPublicationDate%>">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("ViewDate")).ToString("d") %><%# If(Convert.ToBoolean(Eval("AvailableToAllSites")), "<span class='gridImg' title='" & Resources.Poll_Admin.Poll_Default_Key_AvailableToAllSites & "'><img src='/admin/images/available_to_all.png'/></span>", "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="ExpirationDate" SortExpression="ExpirationDate"
                                        HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridExpirationDate%>">
                                        <ItemTemplate>
                                            <%# If(Eval("ExpirationDate") IsNot DBNull.Value, Convert.ToDateTime(Eval("ExpirationDate")).ToString("d") & If(Convert.ToDateTime(Eval("ExpirationDate")) < DateTime.Now, "<span class='gridImg' title='" & Resources.Poll_Admin.Poll_Default_Grid_Expired & "'><img src='/admin/images/expired.png'/></span>", ""), "")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridCategory %>"
                                        SortExpression="categoryName" UniqueName="categoryName" EmptyDataText="<%$ Resources:Poll_Admin, Poll_Default_GridUncategorized %>">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="QuestionHtml" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridQuestion %>"
                                        SortExpression="Question" UniqueName="Question" HeaderStyle-Width="300px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridAnswersRandomized %>"
                                        UniqueName="QuestionRandomized">
                                        <ItemTemplate>
                                            <%#If(Convert.ToBoolean(Eval("AnswersRandomized").ToString()), "<span class='activeField'>" & Resources.Poll_Admin.Poll_Default_GridAnswersRandomizedTrue & "</span>", "<span class='inactiveField'>" & Resources.Poll_Admin.Poll_Default_GridAnswersRandomizedFalse & "</span>")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="answerCount" SortExpression="answerCount" UniqueName="answerCount"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="answers" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Answers.aspx?pollID={0}"
                                        HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridAnswers %>" ItemStyle-Wrap="false"
                                        ItemStyle-VerticalAlign="Middle" SortExpression="answersCount">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn DataField="submissionCount" SortExpression="submissionCount"
                                        UniqueName="submissionCount" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="submissions" DataNavigateUrlFields="ID"
                                        DataNavigateUrlFormatString="Submissions.aspx?pollID={0}" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridSubmissions %>"
                                        ItemStyle-Wrap="false" ItemStyle-VerticalAlign="Middle" SortExpression="submissionCount">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn DataField="commentCount" SortExpression="commentCount" UniqueName="commentCount"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn UniqueName="comments" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridComments %>"
                                        ItemStyle-Wrap="false" SortExpression="commentCount" Visible="false">
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridTemplateColumn UniqueName="editPoll" DataField="ID" ItemStyle-Width="50px"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridEdit %>">
                                        <ItemTemplate>
                                            <a id="aPollEdit" runat="server">
                                                <%= Resources.Poll_Admin.Poll_Default_GridEdit%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Poll_Admin, Poll_Default_GridDelete %>">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                        <a href="editAdd.aspx">
                                            <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                            <%= Resources.Poll_Admin.Poll_Default_Grid_AddNewEntry%></a>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="divFooter">
                            <div class="fLeft">
                                <div class='footerIcon'>
                                    <img src='/admin/images/available_to_all.png' />
                                    <asp:Literal ID="litKeyAvailableToAllSites2" runat="server" Text="<%$ Resources:Poll_Admin, Poll_Default_Key_AvailableToAllSites %>" />
                                </div>
                                <br class="cBoth" />
                                <div class='footerIcon'>
                                    <img src='/admin/images/expired.png' />
                                    <asp:Literal ID="litKeyExpiredMessage" runat="server" Text="<%$ Resources:Poll_Admin, Poll_Default_Key_Expired %>" />
                                </div>
                            </div>
                            <div class="fRight">
                                <asp:Button ID="btnDeleteArchive" runat="server" Text="<%$ Resources:Poll_Admin, Poll_Default_GridDeleteButton %>"
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
