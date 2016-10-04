<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="StaffDetail.aspx.vb" Inherits="staff_staffDetail" %>

<%@ Register TagPrefix="uc" TagName="GoogleMap" Src="~/UserController/GoogleMap.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divModuleContent">
        <table width="100%">
            <tr>
                <td style="width: 220px;">
                    <div class="divCategoryListContainer">
                        &nbsp;
                    </div>
                </td>
                <td>
                    <div class="divDirStaffHead">
                        <div class="floatL">
                            <div class="divStaffFullName">
                                <b><asp:Literal ID="litSalutationFirstAndLastName" runat="server" /></b>
                            </div>
                            <div class="floatL leftPad vcard">
                                <a href='vCard.aspx?id=<%= Request.Params("id")%><%= IF( Not Request.Params("archive") Is Nothing, "&archive=" + Request.Params("archive"),"") %>'>
                                    <img src="/images/vcard.gif" alt="download" />
                                </a>
                            </div>
                        </div>
                        <div class="floatR">
                            <a class="moduleViewAll" href='<%= If(Not Request.Params("archive") Is Nothing AndAlso Convert.ToInt32(Request.Params("archive")) = 1, "Default.aspx?archive=1", "Default.aspx")%>'>
                                (View all Staff Members)</a>
                        </div>
                    </div>
                    <br class="clear" />
                    <br />
                    <div class="divStaffDetail">
                        <div class="divStaffDetailFace floatR">
                            <telerik:RadBinaryImage ID="radStaffImage" runat="server" Visible='true' ImageUrl="/images/staff_noImage_large.gif"
                                AlternateText="No Image Available" Width="100" AutoAdjustImageControlSize="false" />
                        </div>
                        <b>
                            <%= Resources.Staff_FrontEnd.Staff_StaffDetail_StartDateLabel%>
                            <asp:Literal ID="litStartDate" runat="server" /><asp:Literal ID="litEndDate" runat="server"
                                Visible="false" /></b>
                        <div id="divStaffPosition" runat="server" visible="false">
                            <b>
                                <%= Resources.Staff_FrontEnd.Staff_StaffDetail_Position%>:&nbsp;</b><asp:Literal
                                    ID="litStaffPosition" runat="server" />
                        </div>
                        <div id="divCompany" runat="server" visible="false">
                            <b>
                                <%= Resources.Staff_FrontEnd.Staff_StaffDetail_Company%>:&nbsp;</b><asp:Literal ID="litCompany"
                                    runat="server" />
                        </div>
                        <div id="divOfficePhone" runat="server" visible="false">
                            <b>
                                <%= Resources.Staff_FrontEnd.Staff_StaffDetail_Phone%>:&nbsp;</b><asp:Literal ID="litOfficePhone"
                                    runat="server" />
                        </div>
                        <div id="divMobilePhone" runat="server" visible="false">
                            <b>
                                <%= Resources.Staff_FrontEnd.Staff_StaffDetail_Mobile%>:&nbsp;</b><asp:Literal ID="litMobilePhone"
                                    runat="server" />
                        </div>
                        <div id="divEmail" runat="server" visible="false">
                            <b>
                                <%= Resources.Staff_FrontEnd.Staff_StaffDetail_Email%>:&nbsp;</b><asp:Literal ID="litEmail"
                                    runat="server" />
                        </div>
                        <div id="divPersonalURL" runat="server" visible="false">
                            <b>
                                <%= Resources.Staff_FrontEnd.Staff_StaffDetail_Homepage%>:&nbsp;</b><asp:Literal
                                    ID="litPersonalURL" runat="server" />
                        </div>
                        <div id="divFavouriteURL" runat="server" visible="false">
                            <b>
                                <%= Resources.Staff_FrontEnd.Staff_StaffDetail_FavouriteURL%>:&nbsp;</b><asp:Literal
                                    ID="litFavouriteURL" runat="server" />
                        </div>
                        <div id="divBio" runat="server" visible="false">
                            <br />
                            <b>
                                <%= Resources.Staff_FrontEnd.Staff_StaffDetail_Biography%></b><br />
                            <asp:Literal ID="litBio" runat="server" />
                        </div>
                        <div id="divOffice" runat="server" visible="false">
                            <br />
                            <b>
                                <%= Resources.Staff_FrontEnd.Staff_StaffDetail_Office%>:</b><br />
                            <asp:Literal ID="litOffice" runat="server" />
                        </div>
                        <div id="divAddress" runat="server" visible="false">
                            <br />
                            <asp:Literal ID="litAddressStreetAndCity" runat="server" /> <asp:Literal ID="litAddressStateAndZip"
                                runat="server" />
                        </div>
                        <uc:GoogleMap ID="ucGoogleMap" runat="server" Width="300px" Height="300px" />
                        <br />
                        <asp:PlaceHolder ID="addThisPlaceholder" runat="server" Visible="false">
                            <br />
                            <!-- AddThis Button BEGIN -->
                            <div class="addthis_toolbox addthis_default_style">
                                <a href="http://www.addthis.com/bookmark.php" class="addthis_button" style="text-decoration: none"
                                    addthis:url="http://<%=Request.ServerVariables("http_host") %><%=Request.Path.toString() %>?id=<%#Eval("staffID") %>">
                                    <img src="http://s7.addthis.com/static/btn/v2/lg-bookmark-en.gif" width="125" height="16"
                                        border="0" alt="Share"></a>
                            </div>
                            <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=xa-4bbf57a32e8aa403"></script>
                            <!-- AddThis Button END -->
                        </asp:PlaceHolder>
                        <br />
                        <br />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
