<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master" AutoEventWireup="false"
    CodeFile="editAdd.aspx.vb" Inherits="admin_modules_topic_editAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Edit Topic
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div id="MainContent">
            <span class="callout">Basic Topic Information</span><br />
            Enter the basic topic information below.<br />
            <br />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="True">Active</asp:ListItem>
                    <asp:ListItem Value="False">Archive</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <label for="topicName">
                    <span class="moduleLabel">Topic Name:</span></label><span class="requiredStar">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage=" Required"
                    ControlToValidate="topicName" CssClass="errorStyle" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="topicName" runat="server" CssClass="Text250"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <label for="Category">
                                <span class="moduleLabel">Category:</span></label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="rcbCategoryID" runat="server" />
                        </td>
                        <td class="leftPad">
                            <asp:LinkButton ID="categoryLinkButton" runat="server">Add Category</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <label for="topicBody">
                    <span class="moduleLabel">Topic Description:</span></label><span class="requiredStar">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage=" Required"
                    ControlToValidate="topicBody" CssClass="errorStyle" Display="Dynamic" />
            </td>
        </tr>
       
        <tr>
            <td>
                <telerik:RadEditor ID="topicBody" runat="server" Width="98%">
                </telerik:RadEditor>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Button ID="btnAddEdit" runat="server" Text="Add" CausesValidation="true" />
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="false" />
                <br />
                <span class="requiredStar">*</span> Denotes required information<br />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
