<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="editAdd.aspx.vb" Inherits="admin_modules_location_editAdd"
    ValidateRequest="false" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout">
        <%= Resources.Location_Admin.Location_AddEdit_BodyHeading%></span>
    <br />
    <br />
    <span class="callout">
        <%= Resources.Location_Admin.Location_AddEdit_Tab_Content_Heading%></span><br />
    <%= Resources.Location_Admin.Location_AddEdit_Tab_Content_SubHeading%><br />
    <br />
    <div id="MainContent">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <label for="Category">
                                    <span class="moduleLabel">
                                        <%= Resources.Location_Admin.Location_AddEdit_Category%>:</span></label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadComboBox ID="rcbCategoryID" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtLocation">
                        <span class="moduleLabel">
                            <%= Resources.Location_Admin.Location_AddEdit_Location%>:</span></label><span class="requiredStar">*</span>
                    <asp:RequiredFieldValidator ID="reqLocation" runat="server" ErrorMessage=" <%$ Resources:Location_Admin, Location_AddEdit_RequiredMessage %>"
                        ControlToValidate="txtLocation" CssClass="errorStyle" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtLocation" runat="server" CssClass="tb200" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtAddress1">
                        <span class="moduleLabel">
                            <%= Resources.Location_Admin.Location_AddEdit_Address1%>:</span></label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtAddress1" runat="server" CssClass="tb200" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtAddress2">
                        <span class="moduleLabel">
                            <%= Resources.Location_Admin.Location_AddEdit_Address2%>:</span></label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="tb200" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtAddress3">
                        <span class="moduleLabel">
                            <%= Resources.Location_Admin.Location_AddEdit_Address3%>:</span></label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtAddress3" runat="server" CssClass="tb200" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtCity">
                        <span class="moduleLabel">
                            <%= Resources.Location_Admin.Location_AddEdit_City%>:</span></label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtCity" runat="server" CssClass="tb200" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtStateProvince">
                        <span class="moduleLabel">
                            <%= Resources.Location_Admin.Location_AddEdit_State_Province%>:</span></label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtStateProvince" runat="server" CssClass="tb200" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtZip">
                        <span class="moduleLabel">
                            <%= Resources.Location_Admin.Location_AddEdit_Zip%>:</span></label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtZip" runat="server" CssClass="tb200" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" CausesValidation="true" />
    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Location_Admin, Location_AddEdit_ButtonCancel %>"
        CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span>
    <%= Resources.Location_Admin.Location_AddEdit_DenotesRequired%><br />
</asp:Content>
