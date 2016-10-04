<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PollRepeater.ascx.vb"
    Inherits="Poll_PollRepeater" %>
<div class="divModuleRepeater">
    <asp:UpdatePanel ID="upPoll" runat="server">
        <contenttemplate>
        <div class="divModuleSortBy">
            <div>
                <%=Resources.Poll_FrontEnd.Poll_PollRepeater_SortBy%>:
            </div>
            <div class="divModuleSortBy_SortFieldLabel">
                <%=Resources.Poll_FrontEnd.Poll_PollRepeater_SortBy_Title%>
            </div>
            <div class="divModuleSortBy_Asc">
                <asp:ImageButton ID="imgSortUpQuestion" runat="server" ImageUrl="~/images/SquareUp.gif" ToolTip="<%$ Resources:Poll_FrontEnd, Poll_PollRepeater_SortBy_TitleAscending %>" />
            </div>
            <div class="divModuleSortBy_Desc">
                <asp:ImageButton ID="imgSortDownQuestion" runat="server" ImageUrl="~/images/SquareDown.gif" ToolTip="<%$ Resources:Poll_FrontEnd, Poll_PollRepeater_SortBy_TitleDescending %>" />
            </div>
            <div class="divModuleSortBy_Seperator"></div>
            <div class="divModuleSortBy_SortFieldLabel">
                <%=Resources.Poll_FrontEnd.Poll_PollRepeater_SortBy_Date%>
            </div>
            <div class="divModuleSortBy_Asc">
                <asp:ImageButton ID="imgSortUpViewDate" runat="server" ImageUrl="~/images/SquareUp.gif" ToolTip="<%$ Resources:Poll_FrontEnd, Poll_PollRepeater_SortBy_DateAscending %>" />
            </div>
            <div class="divModuleSortBy_Desc">
                <asp:ImageButton ID="imgSortDownViewDate" runat="server" ImageUrl="~/images/SquareDown.gif" ToolTip="<%$ Resources:Poll_FrontEnd, Poll_PollRepeater_SortBy_DateDescending %>" />
            </div>
        </div>
            <br class="cBoth" />
            <br />
            <asp:Panel ID="pnlPoll" runat="server">
                <telerik:RadListView ID="rlvPoll" AllowPaging="False" runat="server" AllowSorting="true"
                    ItemPlaceholderID="plcHolderPoll" DataKeyNames="ID">
                    <LayoutTemplate>
                        <asp:Panel ID="plcHolderPoll" runat="server" />
                        <table cellpadding="0" cellspacing="0" class="cBoth">
                            <tr>
                                <td>
                                    <br />
                                    <telerik:RadDataPager ID="rdPagerPoll" runat="server" PagedControlID="rlvPoll" PageSize="10" Visible="false">
                                <Fields>
                                    <telerik:RadDataPagerButtonField FieldType="FirstPrev" />
                                    <telerik:RadDataPagerButtonField FieldType="Numeric" />
                                    <telerik:RadDataPagerButtonField FieldType="NextLast" />
                                    <telerik:RadDataPagerPageSizeField PageSizeText="<%$ Resources:RadDataPager, Pager_PageSize %>" />
                                </Fields>
                                    </telerik:RadDataPager>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" class="cBoth" style="width: 100%;">
                            <tr>
                                <td>
                                    <p class="Heading2">
                                        <a href='<%# "Default.aspx?id=" & Eval("ID") & If(request.querystring("archive") = 1, "&archive=1","") %>'>
                                            <%#Eval("QuestionHtml")%></a>
                                        <br />
                                    </p>
                                    <span class="Date"><asp:Literal ID="pollDateTime" runat="server" Visible="false" />
                                    </span>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <asp:RadioButtonList ID="rblPollAnswers" runat="server" DataValueField="ID" DataTextField="Answer"
                                        RepeatDirection="Vertical" />
                                    <span class="errorStyle"><asp:Literal ID="litPollSubmissionMsg" runat="server" /></span>
                                    <br />
                                    <asp:Button ID="btnVote" runat="server" ValidationGroup="valPoll" OnClick="btnVote_Click" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divModuleSearchTagList" runat="server" class="divModuleSearchTagList" visible="false">
                                        <br />
                                        <%=Resources.Poll_FrontEnd.Poll_PollRepeater_SearchTagLabel%>:
                                        <asp:Repeater ID="rptSearchTags" runat="server">
                                            <ItemTemplate>
                                                <a href='<%# "Default.aspx?sTag=" & HttpUtility.UrlEncode(Eval("searchTagName")) & If(request.querystring("archive") = 1, "&archive=1","") %>'>
                                                    <%# Eval("searchTagName") %></a>
                                            </ItemTemplate>
                                            <SeparatorTemplate>
                                                ,
                                            </SeparatorTemplate>
                                        </asp:Repeater>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <div class="floatL">
                                        <%=Resources.Poll_FrontEnd.Poll_PollRepeater_PostedBy%>:
                                        <%# If(Eval("author_username") Is DBNull.Value, Resources.Poll_FrontEnd.Poll_PollRepeater_PostedByDefault, Eval("author_username"))%> - <%#Eval("viewdate", "{0:D}")%> <%=Resources.Poll_FrontEnd.Poll_PollRepeater_PostedBy_DateTimeSeperator%> <%#Eval("viewdate", "{0:t}")%></div>
                                    <br />
                                    <asp:PlaceHolder ID="plcComments" runat="server" Visible="false">
                                        <div class="floatL" runat="server" id="commentLinks">
                                            <asp:HyperLink ID="lnkCommentList" runat="server" NavigateUrl='<%#"Default.aspx?id=" & Eval("ID") & If(request.querystring("archive") = 1, "&archive=1","") & "#comments"%>'><%=Resources.Poll_FrontEnd.Poll_PollRepeater_CommentsLabel%> (<%#Eval("commentCount")%>)</asp:HyperLink>
                                            <asp:PlaceHolder ID="plcAddCommentLink" runat="server" Visible="false">&nbsp;|&nbsp;<asp:HyperLink
                                                ID="lnkAddComment" runat="server" NavigateUrl='<%#"Default.aspx?id=" & Eval("ID") & If(request.querystring("archive") = 1, "&archive=1","") & "#addcomment"%>'><%=Resources.Poll_FrontEnd.Poll_PollRepeater_CommentsAdd%></asp:HyperLink></asp:PlaceHolder>
                                            &nbsp;|&nbsp;
                                        </div>
                                    </asp:PlaceHolder>
                                    <div class="floatL">
                                        <a href="<%# "Default.aspx?id=" & Eval("ID") & If(request.querystring("archive") = 1, "&archive=1","") %>"><%=Resources.Poll_FrontEnd.Poll_PollRepeater_PermanentLabel%></a></div>
                                </td>
                            </tr>
                            <asp:PlaceHolder ID="plcAddThis" runat="server" Visible="false">
                                <tr>
                                    <td>
                                        <br />
                                        <!-- AddThis Button BEGIN -->
                                        <div class="addthis_toolbox addthis_default_style">
                                            <a href="http://www.addthis.com/bookmark.php" class="addthis_button" style="text-decoration: none"
                                                addthis:url="http://<%=Request.ServerVariables("http_host") %><%=Request.Path.toString() %>?id=<%#Eval("ID") %>">
                                                <img src="http://s7.addthis.com/static/btn/v2/lg-bookmark-en.gif" width="125" height="16"
                                                    border="0" alt="Share" /></a>
                                        </div>
                                        <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=xa-4bbf57a32e8aa403"></script>
                                        <!-- AddThis Button END -->
                                        <br />
                                    </td>
                                </tr>
                            </asp:PlaceHolder>
                            <tr>
                                <td>
                                    <br />
                                    <hr />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:RadListView>
            </asp:Panel>
        </contenttemplate>
    </asp:UpdatePanel>
</div>
