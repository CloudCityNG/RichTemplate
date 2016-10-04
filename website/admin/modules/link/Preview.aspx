<%@ Page Title="Rollback" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master" AutoEventWireup="false"
    CodeFile="Preview.aspx.vb" Inherits="admin_modules_link_Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" href="/admin/css/RichTemplate.css" />

    <script type="text/javascript">

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

            return oWindow;
        }

        function CancelEdit() {
            GetRadWindow().close();


        }


        function RefreshParentPage() {
            GetRadWindow().BrowserWindow.location.href = GetRadWindow().BrowserWindow.location.href;
        }

    </script>

    <asp:Panel runat="server" ID="rollbackBanel" Width="100%">
        <div style="padding: 10px">
            <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Vista" DecoratedControls="All" />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="ss_LinkArchive_SelectByArchiveID" SelectCommandType="StoredProcedure" UpdateCommand="ss_Link_RollBackLink"
                UpdateCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter Name="archiveID" QueryStringField="archiveID" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="archiveID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <fieldset class="infoPanel">
                            <asp:Label ID="infoLabel" runat="server"></asp:Label>
                        </fieldset>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="recordContent" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <asp:Button ID="btnRollBack" runat="server" Text="Rollback" OnClick="btnRollBack_OnClick" />
                        &nbsp;&nbsp;<asp:Button ID="Cancel" runat="server" Text="Cancel" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="confirmationPanel" Visible="false">
        <table>
            <tr>
                <td>
                    <span class="pageTitle">Rollback Complete!</span><br />
                    <span class="callout">You have successfully restored your previous copy</span><br />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="Close" runat="server" Text="Close" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
