<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetLDAP.aspx.vb" Inherits="GetLDAP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            LDAP TEST PAGE</h1>
        <br />
        <br />
        Time Start:
        <asp:Literal ID="litTimeStart" runat="server" />
        Time End:
        <asp:Literal ID="litTimeEnd" runat="server" />
        <br />
        User's objectGUID:
        <asp:TextBox ID="txtActiveDirectory_Identifier" runat="server" /><br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnGetUser" runat="server" Text="Get User" />
                </td>
                <td>
                    <asp:Button ID="btnGetUserGroups" runat="server" Text="Get User Groups" />
                </td>
                <td>
                    <asp:Button ID="btnGetAllUsers" runat="server" Text="Get All Users" />
                </td>
                <td>
                    <asp:Button ID="btnGetAllGroups" runat="server" Text="Get All Groups" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnImportGroupsAndUsers" runat="server" Text="Import Groups and Users" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Repeater ID="rptUser" runat="server">
                        <ItemTemplate>
                            Name:
                            <%#Eval("name")%>
                            &nbsp; Username:
                            <%#Eval("sAMAccountName")%><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
                <td valign="top">
                    <asp:Repeater ID="rptUserGroups" runat="server">
                        <ItemTemplate>
                            Group Name:
                            <%#Eval("groupName")%><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
                <td valign="top">
                    <asp:Repeater ID="rptUserList" runat="server">
                        <ItemTemplate>
                            Name:
                            <%#Eval("name")%>
                            &nbsp; Username:
                            <%#Eval("sAMAccountName")%><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
                <td valign="top">
                    <asp:Repeater ID="rptGroupList" runat="server">
                        <ItemTemplate>
                            Group Name:
                            <%#Eval("name")%><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
