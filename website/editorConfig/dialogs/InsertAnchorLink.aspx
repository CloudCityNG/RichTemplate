<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InsertAnchorLink.aspx.vb"
    Inherits="admin_editorConfig_dialogs_InsertAnchorLink" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="/admin/css/RichTemplate.css" />
    <link rel="stylesheet" type="text/css" href="/Skins/RichTemplate/RadEditor.RichTemplate.css" />
    <script language="JavaScript" src="/js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/richtemplate_functions.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager1" runat="server" />
    <div style="padding-left: 15px;">
        <h3>
            Insert an Anchor Link</h3>
        <asp:UpdatePanel ID="upInsertSpecialLink" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="400px">
                    <tr>
                        <td colspan="2">
                            <asp:ListBox ID="lstAnchorList" runat="server" Width="100%" Height="60px" CssClass="selectList" EnableViewState="true" AppendDataBoundItems="true" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50px;">
                            Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAnchorName" runat="server" Width="100%" />
                        </td>
                        <td style="width: 80px;">
                            <span class="requiredStar">&nbsp; *</span>
                            <asp:RequiredFieldValidator ID="reqAnchorName" runat="server" ControlToValidate="txtAnchorName"
                                CssClass="errorStyle" Display="Dynamic" ErrorMessage=" Required" />
                            <asp:CustomValidator ID="customValAnchorExists" runat="server" Display="Dynamic" OnServerValidate="customValAnchorExists_Validate"
                                CssClass="errorStyle" ErrorMessage=" Exists" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <br />
                            <br />
                            <asp:Button ID="btnInsert" runat="server" Text="Insert" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hdnAnchorList" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
