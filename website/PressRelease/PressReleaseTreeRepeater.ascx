<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PressReleaseTreeRepeater.ascx.vb"
    Inherits="PressRelease_PressReleaseTreeRepeater" %>
<div class="divModuleRepeater">
    <div class="divModuleSortBy">
        <div>
            <%=Resources.PressRelease_FrontEnd.PressRelease_PressReleaseTreeRepeater_SortBy%>:
        </div>
        <div class="divModuleSortBy_SortFieldLabel">
            <%=Resources.PressRelease_FrontEnd.PressRelease_PressReleaseTreeRepeater_SortBy_Title%>
        </div>
        <div class="divModuleSortBy_Asc">
            <asp:ImageButton ID="imgSortUpTitle" runat="server" ImageUrl="~/images/SquareUp.gif"
                ToolTip="<%$ Resources:PressRelease_FrontEnd, PressRelease_PressReleaseTreeRepeater_SortBy_TitleAscending %>" />
        </div>
        <div class="divModuleSortBy_Desc">
            <asp:ImageButton ID="imgSortDownTitle" runat="server" ImageUrl="~/images/SquareDown.gif"
                ToolTip="<%$ Resources:PressRelease_FrontEnd, PressRelease_PressReleaseTreeRepeater_SortBy_TitleDescending %>" />
        </div>
        <div class="divModuleSortBy_Seperator">
        </div>
        <div class="divModuleSortBy_SortFieldLabel">
            <%=Resources.PressRelease_FrontEnd.PressRelease_PressReleaseTreeRepeater_SortBy_Date%>
        </div>
        <div class="divModuleSortBy_Asc">
            <asp:ImageButton ID="imgSortUpViewDate" runat="server" ImageUrl="~/images/SquareUp.gif"
                ToolTip="<%$ Resources:PressRelease_FrontEnd, PressRelease_PressReleaseTreeRepeater_SortBy_DateAscending %>" />
        </div>
        <div class="divModuleSortBy_Desc">
            <asp:ImageButton ID="imgSortDownViewDate" runat="server" ImageUrl="~/images/SquareDown.gif"
                ToolTip="<%$ Resources:PressRelease_FrontEnd, PressRelease_PressReleaseTreeRepeater_SortBy_DateDescending %>" />
        </div>
    </div>
    <br class="cBoth" />
    <br />
    <asp:Panel ID="pnlPressRelease" runat="server" Width="100%">
        <telerik:RadListView ID="rlvPressRelease" Width="97%" AllowPaging="True" runat="server"
            allowsorting="true" ItemPlaceholderID="plcHolderPressRelease" DataKeyNames="prID">
            <LayoutTemplate>
                <asp:Panel ID="plcHolderPressRelease" runat="server" Width="100%" />
                <table cellpadding="0" cellspacing="0" width="100%" style="clear: both;">
                    <tr>
                        <td>
                            <br />
                            <telerik:RadDataPager ID="rdPagerPressRelease" runat="server" PagedControlID="rlvPressRelease"
                                PageSize="10">
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
                <table cellpadding="0" cellspacing="0" style="width: 100%; clear: both;">
                    <tr>
                        <td style="padding-bottom: 10px">
                            <p class="Heading2">
                                <a href="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, Eval("externalLinkUrl"), "Default.aspx?id=" & Eval("prID") & If(request.querystring("archive") = 1, "&archive=1","") ) %>"
                                    target="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, "_blank", "") %>">
                                    <%#Eval("title")%></a>
                                <br />
                                <span class="Date">
                                    <%#Eval("viewDate", "{0:D}")%></span>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%#Eval("Body")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divModuleSearchTagList" runat="server" class="divModuleSearchTagList" visible="false">
                                <br />
                                <%=Resources.PressRelease_FrontEnd.PressRelease_PressReleaseTreeRepeater_SearchTagLabel%>:
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
                                <%=Resources.PressRelease_FrontEnd.PressRelease_PressReleaseTreeRepeater_PostedBy%>:
                                <%# If(Eval("author_username") Is DBNull.Value, Resources.PressRelease_FrontEnd.PressRelease_PressReleaseTreeRepeater_PostedByDefault, Eval("author_username"))%>
                                -
                                <%#Eval("viewdate", "{0:D}")%>
                                <%=Resources.PressRelease_FrontEnd.PressRelease_PressReleaseTreeRepeater_PostedBy_DateTimeSeperator%>
                                <%#Eval("viewdate", "{0:t}")%></div>
                            <br />
                            <asp:PlaceHolder ID="plcComments" runat="server" Visible="false">
                                <div class="floatL" runat="server" id="commentLinks">
                                    <asp:HyperLink ID="lnkCommentList" runat="server" NavigateUrl='<%#"Default.aspx?id=" & Eval("prID") & If(request.querystring("archive") = 1, "&archive=1","") & "#comments"%>'><%=Resources.PressRelease_FrontEnd.PressRelease_PressReleaseTreeRepeater_CommentsLabel%> (<%#Eval("commentCount")%>)</asp:HyperLink>
                                    <asp:PlaceHolder ID="plcAddCommentLink" runat="server" Visible="false">&nbsp;|&nbsp;<asp:HyperLink
                                        ID="lnkAddComment" runat="server" NavigateUrl='<%#"Default.aspx?id=" & Eval("prID") & If(request.querystring("archive") = 1, "&archive=1","") & "#addcomment"%>'><%=Resources.PressRelease_FrontEnd.PressRelease_PressReleaseTreeRepeater_CommentsAdd%> </asp:HyperLink></asp:PlaceHolder>
                                    &nbsp;|&nbsp;
                                </div>
                            </asp:PlaceHolder>
                            <div class="floatL">
                                <a href="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, Eval("externalLinkUrl"), "Default.aspx?id=" & Eval("prID") & If(request.querystring("archive") = 1, "&archive=1","") ) %>">
                                    <%=Resources.PressRelease_FrontEnd.PressRelease_PressReleaseTreeRepeater_PermanentLabel%></a></div>
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="plcAddThis" runat="server" Visible="false">
                        <tr>
                            <td>
                                <br />
                                <!-- AddThis Button BEGIN -->
                                <div class="addthis_toolbox addthis_default_style">
                                    <a href="http://www.addthis.com/bookmark.php" class="addthis_button" style="text-decoration: none"
                                        addthis:url="http://<%=Request.ServerVariables("http_host") %><%=Request.Path.toString() %>?id=<%#Eval("prID") %>">
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
</div>
