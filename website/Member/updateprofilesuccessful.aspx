<%@ Page Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="updateprofilesuccessful.aspx.vb" Inherits="Member_UpdateProfileSuccessful" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divModuleContent">
        <table>
            <tr>
                <td>
                    <div class="divCategoryListContainer">
                        &nbsp;
                    </div>
                </td>
                <td>
                    <asp:Panel ID="authPanel" runat="server" Width="92%">
                        <div style="text-align: left; width: 400px;">
                            <b><%= Resources.Member_FrontEnd.Member_UpdateProfileSuccessful_ProfileUpdated_Body%></b>
                            <br />
                            <br />
                            <a href='/'>
                                <%= Resources.Member_FrontEnd.Member_UpdateProfileSuccessful_ProfileUpdated_Login%></a>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
