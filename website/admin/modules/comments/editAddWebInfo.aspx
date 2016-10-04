<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="editAddWebInfo.aspx.vb" Inherits="admin_modules_comments_editAddWebInfo" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <uc:header id="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout"><%= Resources.Comment_Admin.Comment_AddEditWebInfo_HeadingBody%></span><br />
    <br />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <label for="Status">
                                <span class="moduleLabel"><%= Resources.Comment_Admin.Comment_AddEditWebInfo_Approved%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Comment_Admin, Comment_AddEditWebInfo_ApprovedTrue %>" />
                                <asp:ListItem Value="False" Text="<%$ Resources:Comment_Admin, Comment_AddEditWebInfo_ApprovedFalse %>" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtComment">
                                <span class="moduleLabel"><%= Resources.Comment_Admin.Comment_AddEditWebInfo_Comment%>:</span><span class="requiredStar">*</span></label>
                            <asp:RequiredFieldValidator ID="reqComment" runat="server" ControlToValidate="txtComment"
                                CssClass="errorStyle" Display="Dynamic" ErrorMessage=" <%$ Resources:Comment_Admin, Comment_AddEditWebInfo_RequiredMessage %>" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="350" Rows="3" /><span
                                class="requiredStar" style="vertical-align: top;">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <span class="moduleLabel"><%= Resources.Comment_Admin.Comment_AddEditWebInfo_Rating%>:</span></label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadRating ID="rrComment" runat="server" ItemCount="5" Value="0" SelectionMode="Continuous"
                                Precision="Half" Orientation="Horizontal" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Button ID="btnEditAdd" runat="server" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Comment_Admin, Comment_AddEditWebInfo_ButtonCancel %>" CausesValidation="false" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="requiredStar">*</span> <%= Resources.Comment_Admin.Comment_AddEditWebInfo_DenotesRequired%>
            </td>
        </tr>
    </table>
</asp:Content>
