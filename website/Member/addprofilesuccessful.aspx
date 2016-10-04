<%@ Page Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="addprofilesuccessful.aspx.vb" Inherits="Member_AddProfileSuccessful" %>

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
                    <asp:Panel ID="addProfileCreated" runat="server" Width="92%">
                        <div style="text-align: left; width: 300px;">
                            <%= Resources.Member_FrontEnd.Member_AddProfileSuccessful_ProfileCreated_Body%><br />
                            <%= Resources.Member_FrontEnd.Member_AddProfileSuccessful_ProfileCreated_LoginEmailNotification%><br />
                            <br />
                            <a href="Default.aspx">
                                <%= Resources.Member_FrontEnd.Member_AddProfileSuccessful_ProfileCreated_Login%></a>
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
