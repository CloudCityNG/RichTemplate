<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InsertSpecialLink.aspx.vb"
    Inherits="admin_editorConfig_dialogs_InsertSpecialLink" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="/Skins/RichTemplate/RadEditor.RichTemplate.css" />
    <link rel="stylesheet" type="text/css" href="/admin/css/RichTemplate.css" />
    <script language="JavaScript" src="/js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/richtemplate_functions.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager1" runat="server" />
    <div style="padding-left: 15px;">
        <h3>
            Select an Internal Page or Anchor</h3>
    </div>
    <asp:UpdatePanel ID="upInsertSpecialLink" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="5">
                <tr>
                    <td valign="top">
                        <asp:RadioButton ID="rdExistingPage" runat="server" GroupName="rdExistingOrAnchor"
                            AutoPostBack="true" Checked="true" />
                    </td>
                    <td>
                        Existing Page:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlExistingPage" runat="server" Width="205" AppendDataBoundItems="true"
                            Enabled="true" CssClass="selectList" />
                    </td>
                    <td>
                        <span class="requiredStar">*</span>
                        <asp:CustomValidator ID="customValExistingPage" runat="server" Display="Dynamic"
                            OnServerValidate="customValExistingPage_Validate" CssClass="errorStyle" ErrorMessage=" Required" />
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:RadioButton ID="rdAnchorLink" runat="server" GroupName="rdExistingOrAnchor"
                            AutoPostBack="true" Checked="false" />
                    </td>
                    <td>
                        Anchor:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAnchorLink" runat="server" Width="205" AppendDataBoundItems="true"
                            Enabled="false" CssClass="selectList" />
                    </td>
                    <td>
                        <span class="requiredStar">*</span>
                        <asp:CustomValidator ID="customValAnchors" runat="server" Display="Dynamic" OnServerValidate="customValAnchors_Validate"
                            CssClass="errorStyle" ErrorMessage=" Required" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 15px;">
                        &nbsp;
                    </td>
                    <td style="width: 100px;">
                        Text:
                    </td>
                    <td>
                        <asp:TextBox ID="txtText" runat="server" Width="200" />
                    </td>
                    <td>
                        <span class="requiredStar">*</span>
                        <asp:RequiredFieldValidator ID="reqText" runat="server" ControlToValidate="txtText"
                            CssClass="errorStyle" Display="Dynamic" ErrorMessage=" Required" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 15px;">
                        &nbsp;
                    </td>
                    <td style="width: 100px;">
                        Title:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Width="200" />
                    </td>
                    <td>
                        <span class="requiredStar">*</span>
                        <asp:RequiredFieldValidator ID="reqTitle" runat="server" ControlToValidate="txtTitle"
                            CssClass="errorStyle" Display="Dynamic" ErrorMessage=" Required" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Target Page:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTargetPage" runat="server" AppendDataBoundItems="true" Width="205" CssClass="selectList" />
                    </td>
                    <td>
                        <span class="requiredStar">*</span><asp:RequiredFieldValidator ID="reqTargetPage"
                            runat="server" ControlToValidate="ddlTargetPage" CssClass="errorStyle" Display="Dynamic"
                            ErrorMessage=" Required" InitialValue="" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSubmit" runat="server" Text="OK" />
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
    </form>
</body>
</html>
