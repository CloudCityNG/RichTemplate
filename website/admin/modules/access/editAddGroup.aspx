<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="editAddGroup.aspx.vb" Inherits="admin_modules_access_editAddGroup" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="pageTitle">
        <%=Resources.Member_Admin.Member_AddEdit_Group_BodyHeading%></span><br />
    <span class="callout">
        <%=Resources.Member_Admin.Member_AddEdit_Group_BodySubHeading%></span><br />
    <br />
    <div id="divActiveDirectoryGroupMessage" runat="server" visible="false">
        <b>
            <%= Resources.Member_Admin.Member_AddEdit_Group_ActiveDirectoryGroup_Message%></b>
    </div>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <label for="userActive">
                    <span class="moduleLabel">
                        <%=Resources.Member_Admin.Member_AddEdit_Group_Status%>:</span></label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGroupActive_ReadOnly" runat="server" visible="false">
                    <b><asp:Literal ID="litGroupActive" runat="server" /></b>
                </div>
                <div id="divGroupActive" runat="server">
                    <asp:RadioButtonList ID="groupActive" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem Value="True" Text="<%$ Resources:Member_Admin, Member_AddEdit_Group_StatusActive %>"></asp:ListItem>
                        <asp:ListItem Value="False" Text="<%$ Resources:Member_Admin, Member_AddEdit_Group_StatusArchive %>"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <label for="groupName">
                    <span class="moduleLabel">
                        <%=Resources.Member_Admin.Member_AddEdit_Group_Name%>:</span><span class="requiredStar">*</span>
                </label>
                <asp:RequiredFieldValidator ID="reqGroupName" runat="server" ErrorMessage="<%$ Resources:Member_Admin, Member_AddEdit_Group_RequiredMessage%>"
                    ControlToValidate="groupName" Display="Dynamic" CssClass="errorStyle"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGroupName_ReadOnly" runat="server" visible="false">
                    <b><asp:Literal ID="litGroupName" runat="server" /></b>
                </div>
                <div id="divGroupName" runat="server">
                    <asp:TextBox ID="groupName" runat="server" Width="250px" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <label for="groupDescription">
                    <span class="moduleLabel">
                        <%=Resources.Member_Admin.Member_AddEdit_Group_Description%>:</span>
                </label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGroupDescription_ReadOnly" runat="server" visible="false">
                    <b><asp:Literal ID="litGroupDescription" runat="server" /></b>
                </div>
                <div id="divGroupDescription" runat="server">
                    <asp:TextBox ID="groupDescription" runat="server" TextMode="MultiLine" Rows="3" Width="250px" />
                </div>
            </td>
        </tr>
        <tr id="trSiteAccess" runat="server" visible="false">
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <label for="rblSite">
                                <span class="moduleLabel">
                                    <%=Resources.Member_Admin.Member_AddEdit_Group_AssociateWithSite%>:</span></label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divAssociateWithSite" runat="server">
                                <asp:RadioButtonList ID="rblSite" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Text="<%$ Resources:Member_Admin, Member_AddEdit_Group_AssociateWithSite_ThisSiteOnly %>"
                                        Value="false" Selected="True" />
                                    <asp:ListItem Text="<%$ Resources:Member_Admin, Member_AddEdit_Group_AssociateWithSite_AllSites %>"
                                        Value="true" />
                                </asp:RadioButtonList>
                            </div>
                            <div id="divAssociateWithSite_PublicMessage" runat="server" class="divAssociateWithSite_PublicMessage"
                                visible="false">
                                <span>
                                    <img src='/admin/images/available_to_all.png' />
                                </span><span class="spanPublicModuleRecord">
                                    <%=Resources.Member_Admin.Member_AddEdit_Group_AssociateWithSite_PublicMessage%>:
                                    <asp:Literal ID="litSiteName" runat="server" /></span>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                <label for="groupPassword">
                    <span class="moduleLabel">
                        <%=Resources.Member_Admin.Member_AddEdit_Group_Password%>:</span>
                </label>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                <asp:TextBox ID="groupPassword" runat="server" />
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                <br />
                <label for="expirationDate">
                    <span class="moduleLabel">
                        <%=Resources.Member_Admin.Member_AddEdit_Group_ExpirationDate%>:</span> <span class="graySubText">
                            (<%=Resources.Member_Admin.Member_AddEdit_Group_ExpirationDate_Optional%>)</span>
                </label>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                <telerik:RadDatePicker ID="expirationDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                <br />
                <span class="graySubText">
                    <%=Resources.Member_Admin.Member_AddEdit_Group_ExpirationDate_Message%></span><br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Button ID="btnAddEdit" runat="server" />
                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Member_Admin, Member_AddEdit_Group_ButtonCancel%>"
                    CausesValidation="false" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="requiredStar">*</span>
                <%=Resources.Member_Admin.Member_AddEdit_Group_DenotesRequired%>
            </td>
        </tr>
    </table>
</asp:Content>
