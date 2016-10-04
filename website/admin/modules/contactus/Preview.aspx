<%@ Page Title="Rollback" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="Preview.aspx.vb" Inherits="admin_modules_contactus_preview" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
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
                                    <%= Resources.ContactUs_Admin.ContactUs_Preview_Expired%></h3>
                                <div class="floatL leftPad">
                                    <img src='/admin/images/expired.png' />
                                </div>
                            </div>
                            <b>
                                <%= Resources.ContactUs_Admin.ContactUs_Preview_InformationBox_Version%></b>:
                            <asp:Literal ID="litInformationBox_Version" runat="server" /><br />
                            <b>
                                <%= Resources.ContactUs_Admin.ContactUs_Preview_InformationBox_DateCreated%></b>:
                            <asp:Literal ID="litInformationBox_DateCreated" runat="server" /><br />
                            <b>
                                <%= Resources.ContactUs_Admin.ContactUs_Preview_InformationBox_Author%></b>:
                            <asp:Literal ID="litInformationBox_AuthorName" runat="server" /><br />
                            <b>
                                <%= Resources.ContactUs_Admin.ContactUs_Preview_InformationBox_Category%></b>:
                            <asp:Literal ID="litInformationBox_Category" runat="server" /><br />
                            <b>
                                <%= Resources.ContactUs_Admin.ContactUs_Preview_InformationBox_Status%></b>:
                            <asp:Literal ID="litInformationBox_Status" runat="server" /><br />
                            <div id="divInformationBox_PublicationDate" runat="server" visible="false">
                                <b>
                                    <%= Resources.ContactUs_Admin.ContactUs_Preview_InformationBox_PublicationDate%></b>:
                                <asp:Literal ID="litInformationBox_PublicationDate" runat="server" />
                            </div>
                            <div id="divInformationBox_ExpirationDate" runat="server" visible="false">
                                <b>
                                    <%= Resources.ContactUs_Admin.ContactUs_Preview_InformationBox_ExpirationDate%></b>:
                                <asp:Literal ID="litInformationBox_ExpirationDate" runat="server" />
                            </div>
                            <b>
                                <%= Resources.ContactUs_Admin.ContactUs_Preview_InformationBox_ContributedBy%></b>:
                            <asp:Literal ID="litInformationBox_ContributedBy" runat="server" /><br />
                            <b>
                                <%= Resources.ContactUs_Admin.ContactUs_Preview_InformationBox_ContributedBy_EmailAddress%></b>:
                            <asp:Literal ID="litInformationBox_ContributedBy_EmailAddress" runat="server" /><br />
                        </fieldset>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <hr />
                        <br />
                        <div class="divModuleDetail">
                            <div class="Date">
                                <i><asp:Literal ID="litContactUsDate" runat="server" /></i></div>
                            <br class="cBoth" />
                            <div class="item">
                                <asp:Literal ID="litContactUsMessage" runat="server" /><br />
                            </div>
                            <br />
                            <div class="floatL">
                                <%=Resources.ContactUs_Admin.ContactUs_Preview_PostedBy%>:<asp:Literal ID="litPostedBy"
                                    runat="server" /> - <asp:Literal ID="litViewDate" runat="server" />
                                <%=Resources.ContactUs_Admin.ContactUs_Preview_PostedBy_DateTimeSeperator%>
                                <asp:Literal ID="litViewDateTime" runat="server" /></div>
                            <div id="divModuleSearchTagList" runat="server" class="divModuleSearchTagList" visible="false">
                                <br />
                                <%=Resources.ContactUs_Admin.ContactUs_Preview_SearchTagLabel%>:
                                <asp:Repeater ID="rptSearchTags" runat="server">
                                    <ItemTemplate>
                                        <a href='#'>
                                            <%# Eval("searchTagName") %></a>
                                    </ItemTemplate>
                                    <SeparatorTemplate>
                                        ,
                                    </SeparatorTemplate>
                                </asp:Repeater>
                            </div>
                            <br class="cBoth" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <hr />
                        <br />
                        <br />
                        <asp:Button ID="btnRollBack" runat="server" Text="<%$ Resources:ContactUs_Admin, ContactUs_Preview_ButtonRollback %>"
                            OnClick="btnRollBack_OnClick" />
                        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:ContactUs_Admin, ContactUs_Preview_ButtonCancel %>" />
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
                        <%=Resources.ContactUs_Admin.ContactUs_Preview_RollBackComplete_Heading%></span><br />
                    <span class="callout">
                        <%=Resources.ContactUs_Admin.ContactUs_Preview_RollBackComplete_Body%></span><br />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnClose" runat="server" Text="<%$ Resources:ContactUs_Admin, ContactUs_Preview_ButtonClose %>" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
