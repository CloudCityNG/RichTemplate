<%@ Page Title="Rollback" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="Preview.aspx.vb" Inherits="admin_modules_staff_preview" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<%@ Register TagPrefix="uc" TagName="GoogleMap" Src="~/UserController/GoogleMap.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" href="/admin/css/RichTemplate.css" />
    <asp:Panel runat="server" ID="pnlPreview" Width="100%">
        <div style="padding: 10px">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <fieldset class="infoPanel">
                            <div id="divExpired" runat="server" visible="false" class="errorStyle fRight">
                                <h3 class="floatL">
                                    <%= Resources.Staff_Admin.Staff_Preview_Expired%></h3>
                                <div class="floatL leftPad">
                                    <img src='/admin/images/expired.png' />
                                </div>
                            </div>
                            <b>
                                <%= Resources.Staff_Admin.Staff_Preview_InformationBox_Version%></b>:
                            <asp:Literal ID="litInformationBox_Version" runat="server" /><br />
                            <b>
                                <%= Resources.Staff_Admin.Staff_Preview_InformationBox_DateCreated%></b>:
                            <asp:Literal ID="litInformationBox_DateCreated" runat="server" /><br />
                            <b>
                                <%= Resources.Staff_Admin.Staff_Preview_InformationBox_Author%></b>:
                            <asp:Literal ID="litInformationBox_AuthorName" runat="server" /><br />
                            <b>
                                <%= Resources.Staff_Admin.Staff_Preview_InformationBox_Category%></b>:
                            <asp:Literal ID="litInformationBox_Category" runat="server" /><br />
                            <b>
                                <%= Resources.Staff_Admin.Staff_Preview_InformationBox_Status%></b>:
                            <asp:Literal ID="litInformationBox_Status" runat="server" /><br />
                            <div id="divInformationBox_StartDate" runat="server" visible="false">
                                <b>
                                    <%= Resources.Staff_Admin.Staff_Preview_InformationBox_StartDate%></b>:
                                <asp:Literal ID="litInformationBox_StartDate" runat="server" />
                            </div>
                            <div id="divInformationBox_EndDate" runat="server" visible="false">
                                <b>
                                    <%= Resources.Staff_Admin.Staff_Preview_InformationBox_EndDate%></b>:
                                <asp:Literal ID="litInformationBox_EndDate" runat="server" />
                            </div>
                        </fieldset>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <hr />
                        <br />
                        <div class="divDirStaffHead">
                            <div class="floatL">
                                <div class="divStaffFullName">
                                    <b>
                                        <asp:Literal ID="litSalutationFirstAndLastName" runat="server" /></b>
                                </div>
                            </div>
                            <div class="floatL leftPad vcard">
                                <a href='vCard.aspx?archiveID=<%= Request.Params("archiveID") %>'>
                                    <img src="/images/vcard.gif" alt="download" />
                                </a>
                            </div>
                        </div>
                        <br class="clear" />
                        <br />
                        <div class="divStaffDetail">
                            <div class="divStaffDetailFace floatR">
                                <telerik:RadBinaryImage ID="radStaffImage" runat="server" Visible='true' ImageUrl="/images/staff_noImage_large.gif"
                                    Width="100" AutoAdjustImageControlSize="false" />
                            </div>
                            <b>
                                <%= Resources.Staff_Admin.Staff_Preview_StartDateLabel%>
                                <asp:Literal ID="litStartDate" runat="server" /><asp:Literal ID="litEndDate" runat="server"
                                    Visible="false" /></b>
                            <div id="divStaffPosition" runat="server" visible="false">
                                <b>
                                    <%= Resources.Staff_Admin.Staff_Preview_Position%>:&nbsp;</b><asp:Literal ID="litStaffPosition"
                                        runat="server" />
                            </div>
                            <div id="divCompany" runat="server" visible="false">
                                <b>
                                    <%= Resources.Staff_Admin.Staff_Preview_Company%>:&nbsp;</b><asp:Literal ID="litCompany"
                                        runat="server" />
                            </div>
                            <div id="divOfficePhone" runat="server" visible="false">
                                <b>
                                    <%= Resources.Staff_Admin.Staff_Preview_Phone%>:&nbsp;</b><asp:Literal ID="litOfficePhone"
                                        runat="server" />
                            </div>
                            <div id="divMobilePhone" runat="server" visible="false">
                                <b>
                                    <%= Resources.Staff_Admin.Staff_Preview_Mobile%>:&nbsp;</b><asp:Literal ID="litMobilePhone"
                                        runat="server" />
                            </div>
                            <div id="divEmail" runat="server" visible="false">
                                <b>
                                    <%= Resources.Staff_Admin.Staff_Preview_Email%>:&nbsp;</b><asp:Literal ID="litEmail"
                                        runat="server" />
                            </div>
                            <div id="divPersonalURL" runat="server" visible="false">
                                <b>
                                    <%= Resources.Staff_Admin.Staff_Preview_Homepage%>:&nbsp;</b><asp:Literal ID="litPersonalURL"
                                        runat="server" />
                            </div>
                            <div id="divFavouriteURL" runat="server" visible="false">
                                <b>
                                    <%= Resources.Staff_Admin.Staff_Preview_FavouriteURL%>:&nbsp;</b><asp:Literal ID="litFavouriteURL"
                                        runat="server" />
                            </div>
                            <div id="divBio" runat="server" visible="false">
                                <br />
                                <b>
                                    <%= Resources.Staff_Admin.Staff_Preview_Biography%></b><br />
                                <asp:Literal ID="litBio" runat="server" />
                            </div>
                            <div id="divOffice" runat="server" visible="false">
                                <br />
                                <b>
                                    <%= Resources.Staff_Admin.Staff_Preview_Office%>:</b><br />
                                <asp:Literal ID="litOffice" runat="server" />
                            </div>
                            <div id="divAddress" runat="server" visible="false">
                                <br />
                                <asp:Literal ID="litAddressStreetAndCity" runat="server" />
                                <asp:Literal ID="litAddressStateAndZip" runat="server" />
                            </div>
                            <uc:GoogleMap ID="ucGoogleMap" runat="server" Width="300px" Height="300px" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <hr />
                        <br />
                        <br />
                        <asp:Button ID="btnRollBack" runat="server" Text="<%$ Resources:Staff_Admin, Staff_Preview_ButtonRollback %>"
                            OnClick="btnRollBack_OnClick" />
                        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Staff_Admin, Staff_Preview_ButtonCancel %>" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlConfirmation" Visible="false">
        <table>
            <tr>
                <td>
                    <span class="pageTitle">
                        <%=Resources.Staff_Admin.Staff_Preview_RollBackComplete_Heading%></span><br />
                    <span class="callout">
                        <%=Resources.Staff_Admin.Staff_Preview_RollBackComplete_Body%></span><br />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnClose" runat="server" Text="<%$ Resources:Staff_Admin, Staff_Preview_ButtonClose %>" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
