<%@ Control Language="VB" AutoEventWireup="false" CodeFile="StaffRepeater.ascx.vb"
    Inherits="staff_StaffRepeater" %>
<div class="divModuleRepeater">
    <div>
        <asp:UpdatePanel ID="upStaff" runat="server">
            <contenttemplate>
            <div class="divModuleSortBy">
                <div>
                    <%=Resources.Staff_FrontEnd.Staff_StaffRepeater_SortBy%>:
                </div>
                <div class="divModuleSortBy_SortFieldLabel">
                    <%=Resources.Staff_FrontEnd.Staff_StaffRepeater_SortBy_Name%>
                </div>
                <div class="divModuleSortBy_Asc">
                    <asp:ImageButton ID="imgSortUpName" runat="server" ImageUrl="~/images/SquareUp.gif" ToolTip="<%$ Resources:Staff_FrontEnd, Staff_StaffRepeater_SortBy_NameAscending %>" />
                </div>
                <div class="divModuleSortBy_Desc">
                    <asp:ImageButton ID="imgSortDownName" runat="server" ImageUrl="~/images/SquareDown.gif" ToolTip="<%$ Resources:Staff_FrontEnd, Staff_StaffRepeater_SortBy_NameDescending %>" />
                </div>
                <div class="divModuleSortBy_Seperator"></div>
                <div class="divModuleSortBy_SortFieldLabel">
                    <%=Resources.Staff_FrontEnd.Staff_StaffRepeater_SortBy_StaffPosition%>
                </div>
                <div class="divModuleSortBy_Asc">
                    <asp:ImageButton ID="imgSortUpPosition" runat="server" ImageUrl="~/images/SquareUp.gif" ToolTip="<%$ Resources:Staff_FrontEnd, Staff_StaffRepeater_SortBy_StaffPositionAscending %>" />
                </div>
                <div class="divModuleSortBy_Desc">
                    <asp:ImageButton ID="imgSortDownPosition" runat="server" ImageUrl="~/images/SquareDown.gif" ToolTip="<%$ Resources:Staff_FrontEnd, Staff_StaffRepeater_SortBy_StaffPositionDescending %>" />
                </div>
            </div>
            <br class="cBoth" />
                <br />
                <telerik:RadListView ID="rlvStaff" AllowPaging="True" runat="server"
                    allowsorting="true" ItemPlaceholderID="plcHolderStaff" DataKeyNames="staffID">
                    <LayoutTemplate>
                        <asp:Panel ID="plcHolderStaff" runat="server" Width="100%" />
                        <table cellpadding="0" cellspacing="0" width="100%" class="cBoth">
                            <tr>
                                <td>
                                    <br />
                                    <telerik:RadDataPager ID="RadDataPagerStaff" runat="server" PagedControlID="rlvStaff"
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
                        
                        <div class="floatL leftPad">
                            <div class="floatL" style="margin-right:10px;">
                            <telerik:RadBinaryImage ID="radStaffImage" runat="server" Visible='true' ImageUrl="/images/staff_noImage_large.gif" Width="100" AutoAdjustImageControlSize="false" />
                            </div>
                            <div class="floatL">
                                <div class="floatL">
                                    <a href="?id=<%#Eval("staffID") %><%# If(request.querystring("archive") = 1, "&archive=1","") %>">
                                        <b>
                                            <asp:Literal ID="litSalutationFirstAndLastName" runat="server" /> 
                                            <span id="spanStaffPosition" runat="server" visible="false">,
                                                <asp:Literal ID="litStaffPosition" runat="server" />
                                            </span>
                                        </b>
                                    </a>
                                </div>
                                <div class="floatL vcard">
                                    <a href="vCard.aspx?id=<%#Eval("staffID") %><%# If(request.querystring("archive") = 1, "&archive=1","") %>">
                                        <img src="/images/vcard.gif" alt="download" style="vertical-align:bottom;" />
                                    </a>
                                </div>
                                <br class="cBoth" />
                                <div id="divEmail" runat="server" visible="false">
                                    <asp:Literal ID="litEmail" runat="server" />
                                </div>
                                <div id="divBio" runat="server" visible="false">
                                    <br />
                                    <b><%=Resources.Staff_FrontEnd.Staff_StaffRepeater_Biography%></b><br />
                                    <asp:Literal ID="litBio" runat="server" />
                                </div>
                            </div>

                        </div>
                        <br class="cBoth" />
                        <br />
                    </ItemTemplate>
                </telerik:RadListView>
            </contenttemplate>
        </asp:UpdatePanel>
    </div>
</div>
