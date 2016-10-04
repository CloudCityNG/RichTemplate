<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DirectoryBrowser.ascx.vb"
    Inherits="admin_DirectoryBrowser" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Panel ID="popupControl" runat="server" Style="display: none">
    <div style="border-style: solid; border-width: 1px; border-color: #000000;" id="divPanel"
        runat="server">
        <table border="0" cellpadding="5" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadTreeView ID="RadTreeView1" runat="server" Style="border: 1px solid #CBE7F5;"
                        OnNodeExpand="RadTreeView1_NodeExpand">
                    </telerik:RadTreeView>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnOK" runat="server" Text="OK" CausesValidation="false" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:TextBox ID="txtDocument" runat="server" Enabled="true"></asp:TextBox>
        </td>
        <td nowrap>&nbsp;&nbsp;<asp:ImageButton
                ID="imgBrowse" runat="server" /></td>
        <td style="padding-left:7px;">
            <asp:Label ID="txtHelpText" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
    <td colspan="3" align="center">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required"
            ControlToValidate="txtDocument"></asp:RequiredFieldValidator>
    </td>
    </tr></table>
<cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="popupControl"
    TargetControlID="imgBrowse" OnOkScript="okScript()" CancelControlID="btnCancel"
    DropShadow="true">
</cc1:ModalPopupExtender>

<script type="text/javascript">
    function okScript() {
        alert("complete");
    }
</script>

