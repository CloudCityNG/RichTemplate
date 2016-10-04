<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EventRepeater.ascx.vb"
    Inherits="Event_EventRepeater" %>
<div class="divModuleRepeater">
    <div class="divModuleSortBy">
        <div>
            <%=Resources.Event_FrontEnd.Event_EventRepeater_SortBy%>:
        </div>
        <div class="divModuleSortBy_SortFieldLabel">
            <%=Resources.Event_FrontEnd.Event_EventRepeater_SortBy_Title%>
        </div>
        <div class="divModuleSortBy_Asc">
            <asp:ImageButton ID="imgSortUpTitle" runat="server" ImageUrl="~/images/SquareUp.gif"
                ToolTip="<%$ Resources:Event_FrontEnd, Event_EventRepeater_SortBy_TitleAscending %>" />
        </div>
        <div class="divModuleSortBy_Desc">
            <asp:ImageButton ID="imgSortDownTitle" runat="server" ImageUrl="~/images/SquareDown.gif"
                ToolTip="<%$ Resources:Event_FrontEnd, Event_EventRepeater_SortBy_TitleDescending %>" />
        </div>
        <div class="divModuleSortBy_Seperator">
        </div>
        <div class="divModuleSortBy_SortFieldLabel">
            <%=Resources.Event_FrontEnd.Event_EventRepeater_SortBy_Date%>
        </div>
        <div class="divModuleSortBy_Asc">
            <asp:ImageButton ID="imgSortUpStartDate" runat="server" ImageUrl="~/images/SquareUp.gif"
                ToolTip="<%$ Resources:Event_FrontEnd, Event_EventRepeater_SortBy_DateAscending %>" />
        </div>
        <div class="divModuleSortBy_Desc">
            <asp:ImageButton ID="imgSortDownStartDate" runat="server" ImageUrl="~/images/SquareDown.gif"
                ToolTip="<%$ Resources:Event_FrontEnd, Event_EventRepeater_SortBy_DateDescending %>" />
        </div>
    </div>
    <br class="cBoth" />
    <br />
    <asp:Panel ID="pnlEvent" runat="server">
        <telerik:RadListView ID="rlvEvent" AllowPaging="True" runat="server" ItemPlaceholderID="plcHolderEvent"
            DataKeyNames="eventID">
            <LayoutTemplate>
                <asp:Panel ID="plcHolderEvent" runat="server" />
                <table cellpadding="0" cellspacing="0" class="cBoth">
                    <tr>
                        <td>
                            <br />
                            <telerik:RadDataPager ID="rdPagerEvent" runat="server" PagedControlID="rlvEvent"
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
                <table cellpadding="0" cellspacing="0" class="cBoth" style="width: 100%;">
                    <tr>
                        <td>
                            <p class="Heading2">
                                <a href="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, Eval("externalLinkUrl"), "Default.aspx?id=" & Eval("eventID") & If(request.querystring("archive") = 1, "&archive=1","") ) %>"
                                    target="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, "_blank", "") %>">
                                    <%#Eval("title")%></a>
                                <br />
                                <span class="Date">
                                    <asp:Label ID="eventDateTime" runat="server" Visible="false" />
                                </span>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <%#Eval("summary")%>
                            <div id="divContactPerson" runat="server" visible="false" class="clear">
                                <br />
                                <b>
                                    <%=Resources.Event_FrontEnd.Event_EventRepeater_ContactPerson%></b>:
                                <asp:Literal ID="litContactPerson" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divModuleSearchTagList" runat="server" class="divModuleSearchTagList" visible="false">
                                <br />
                                <%=Resources.Event_FrontEnd.Event_EventRepeater_SearchTagLabel%>:
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
                                <%=Resources.Event_FrontEnd.Event_EventRepeater_PostedBy%>:
                                <%# If(Eval("author_username") Is DBNull.Value, Resources.Event_FrontEnd.Event_EventRepeater_PostedByDefault, Eval("author_username"))%>
                                -
                                <%#Eval("viewdate", "{0:D}")%>
                                <%=Resources.Event_FrontEnd.Event_EventRepeater_PostedBy_DateTimeSeperator%>
                                <%#Eval("viewdate", "{0:t}")%></div>
                            <br />
                            <asp:PlaceHolder ID="plcComments" runat="server" Visible="false">
                                <div class="floatL" runat="server" id="commentLinks">
                                    <asp:HyperLink ID="lnkCommentList" runat="server" NavigateUrl='<%#"Default.aspx?id=" & Eval("eventID") & If(request.querystring("archive") = 1, "&archive=1","") & "#comments"%>'><%=Resources.Event_FrontEnd.Event_EventRepeater_CommentsLabel%> (<%#Eval("commentCount")%>)</asp:HyperLink>
                                    <asp:PlaceHolder ID="plcAddCommentLink" runat="server" Visible="false">&nbsp;|&nbsp;<asp:HyperLink
                                        ID="lnkAddComment" runat="server" NavigateUrl='<%#"Default.aspx?id=" & Eval("eventID") & If(request.querystring("archive") = 1, "&archive=1","") & "#addcomment"%>'><%=Resources.Event_FrontEnd.Event_EventRepeater_CommentsAdd%></asp:HyperLink></asp:PlaceHolder>
                                    &nbsp;|&nbsp;
                                </div>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="plcOnlineSignup" runat="server" Visible="false">
                                <div class="floatL">
                                    <a href='<%#"Default.aspx?id=" & Eval("eventID") & "#signup"%>'>
                                        <%=Resources.Event_FrontEnd.Event_EventRepeater_ApplyNowLabel%></a>&nbsp;|&nbsp;
                                </div>
                            </asp:PlaceHolder>
                            <div class="floatL">
                                <a href="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, Eval("externalLinkUrl"), "Default.aspx?id=" & Eval("eventID") & If(request.querystring("archive") = 1, "&archive=1","") ) %>">
                                    <%=Resources.Event_FrontEnd.Event_EventRepeater_PermanentLabel%></a></div>
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="plcAddThis" runat="server" Visible="false">
                        <tr>
                            <td>
                                <br />
                                <!-- AddThis Button BEGIN -->
                                <div class="addthis_toolbox addthis_default_style">
                                    <a href="http://www.addthis.com/bookmark.php" class="addthis_button" style="text-decoration: none"
                                        addthis:url="http://<%=Request.ServerVariables("http_host") %><%=Request.Path.toString() %>?id=<%#Eval("eventID") %>">
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
