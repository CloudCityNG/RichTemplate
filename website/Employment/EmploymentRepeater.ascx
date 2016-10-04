<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmploymentRepeater.ascx.vb"
    Inherits="Employment_EmploymentRepeater" %>
<div class="divModuleRepeater">
    <div class="divModuleSortBy">
        <div>
            <%=Resources.Employment_FrontEnd.Employment_EmploymentRepeater_SortBy%>:
        </div>
        <div class="divModuleSortBy_SortFieldLabel">
            <%=Resources.Employment_FrontEnd.Employment_EmploymentRepeater_SortBy_Title%>
        </div>
        <div class="divModuleSortBy_Asc">
            <asp:ImageButton ID="imgSortUpTitle" runat="server" ImageUrl="~/images/SquareUp.gif"
                ToolTip="<%$ Resources:Employment_FrontEnd, Employment_EmploymentRepeater_SortBy_TitleAscending %>" />
        </div>
        <div class="divModuleSortBy_Desc">
            <asp:ImageButton ID="imgSortDownTitle" runat="server" ImageUrl="~/images/SquareDown.gif"
                ToolTip="<%$ Resources:Employment_FrontEnd, Employment_EmploymentRepeater_SortBy_TitleDescending %>" />
        </div>
        <div class="divModuleSortBy_Seperator">
        </div>
        <div class="divModuleSortBy_SortFieldLabel">
            <%=Resources.Employment_FrontEnd.Employment_EmploymentRepeater_SortBy_Date%>
        </div>
        <div class="divModuleSortBy_Asc">
            <asp:ImageButton ID="imgSortUpViewDate" runat="server" ImageUrl="~/images/SquareUp.gif"
                ToolTip="<%$ Resources:Employment_FrontEnd, Employment_EmploymentRepeater_SortBy_DateAscending %>" />
        </div>
        <div class="divModuleSortBy_Desc">
            <asp:ImageButton ID="imgSortDownViewDate" runat="server" ImageUrl="~/images/SquareDown.gif"
                ToolTip="<%$ Resources:Employment_FrontEnd, Employment_EmploymentRepeater_SortBy_DateDescending %>" />
        </div>
    </div>
    <br class="cBoth" />
    <br />
    <asp:Panel ID="pnlEmployment" runat="server">
        <telerik:RadListView ID="rlvEmployment" AllowPaging="True" runat="server" AllowSorting="true"
            ItemPlaceholderID="plcHolderEmployment" DataKeyNames="employmentID">
            <LayoutTemplate>
                <asp:Panel ID="plcHolderEmployment" runat="server" />
                <table cellpadding="0" cellspacing="0" class="cBoth">
                    <tr>
                        <td>
                            <br />
                            <telerik:RadDataPager ID="rdPagerEmployment" runat="server" PagedControlID="rlvEmployment"
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
                                <a href="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, Eval("externalLinkUrl"), "Default.aspx?id=" & Eval("employmentID") & If(request.querystring("archive") = 1, "&archive=1","") ) %>"
                                    target="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, "_blank", "") %>">
                                    <%#Eval("title")%></a>
                                <br />
                                <span class="Date"><asp:Literal ID="employmentDateTime" runat="server" Visible="false" />
                                </span>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divCityAndState" runat="server" visible="false" class="clear">
                                <asp:Literal ID="litCityAndState" runat="server" />
                            </div>
                            <br />
                            <div id="divSalaryRange" runat="server" visible="false" class="clear">
                                <b>
                                    <%=Resources.Employment_FrontEnd.Employment_EmploymentRepeater_Salary%></b>:
                                <asp:Literal ID="litSalaryRange" runat="server" />
                            </div>
                            <div>
                                <asp:Literal ID="litSummary" runat="server" />
                            </div>
                            <div id="divClearance" runat="server" visible="false" class="clear">
                                <b>
                                    <%=Resources.Employment_FrontEnd.Employment_EmploymentRepeater_Clearance%></b>:
                                <asp:Literal ID="litClearance" runat="server" />
                            </div>
                            <div id="divContactPerson" runat="server" visible="false" class="clear">
                                <b>
                                    <%=Resources.Employment_FrontEnd.Employment_EmploymentRepeater_ContactPerson %></b>:
                                <asp:Literal ID="litContactPerson" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divModuleSearchTagList" runat="server" class="divModuleSearchTagList" visible="false">
                                <br />
                                <%=Resources.Employment_FrontEnd.Employment_EmploymentRepeater_SearchTagLabel%>:
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
                                <%=Resources.Employment_FrontEnd.Employment_EmploymentRepeater_PostedBy%>:
                                <%# If(Eval("author_username") Is DBNull.Value, Resources.Employment_FrontEnd.Employment_EmploymentRepeater_PostedByDefault, Eval("author_username"))%>
                                -
                                <%#Eval("viewdate", "{0:D}")%>
                                <%=Resources.Employment_FrontEnd.Employment_EmploymentRepeater_PostedBy_DateTimeSeperator%>
                                <%#Eval("viewdate", "{0:t}")%></div>
                            <br />
                            <asp:PlaceHolder ID="plcOnlineSignup" runat="server" Visible="false">
                                <div class="floatL">
                                    <a href='<%#"Default.aspx?id=" & Eval("employmentID") & "#signup"%>'>
                                        <%=Resources.Employment_FrontEnd.Employment_EmploymentRepeater_ApplyNowLabel%></a>&nbsp;|&nbsp;
                                </div>
                            </asp:PlaceHolder>
                            <div class="floatL">
                                <a href="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, Eval("externalLinkUrl"), "Default.aspx?id=" & Eval("employmentID") & If(request.querystring("archive") = 1, "&archive=1","") ) %>">
                                    <%=Resources.Employment_FrontEnd.Employment_EmploymentRepeater_PermanentLabel%></a></div>
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="plcAddThis" runat="server" Visible="false">
                        <tr>
                            <td>
                                <br />
                                <!-- AddThis Button BEGIN -->
                                <div class="addthis_toolbox addthis_default_style">
                                    <a href="http://www.addthis.com/bookmark.php" class="addthis_button" style="text-decoration: none"
                                        addthis:url="http://<%=Request.ServerVariables("http_host") %><%=Request.Path.toString() %>?id=<%#Eval("employmentID") %>">
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
