<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ForgottenPassword.aspx.vb"
    Inherits="richadmin_ForgottenPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <title>RichTemplate 2.0 Web Site Administration Portal</title>
    <script type="text/javascript">
<!--
        if (self != parent) { parent.location.href = "/richadmin/"; }
-->
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divForgottenPasswordStepOne" runat="server" align="center" style="margin-top: 100px;">
        <table id="table2" width="400" border="0" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td>
                        <div style="width: 400px; height: 77px; background-image: url(/admin/images/loginheader.gif);">
                            <div style="font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 16px; font-weight: bold;
                                line-height: 77px; padding-left: 80px; color: rgb(255, 255, 255);">
                                <%= Resources.richadmin.richadmin_ForgottenPassword_Heading%></div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="table3" style="border-left: 2px solid rgb(40, 87, 170); border-right: 2px solid rgb(40, 87, 170);
                            border-width: 0px 2px 2px; border-bottom: 2px solid rgb(40, 87, 170);" width="400"
                            border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td class="body" align="left">
                                        <div style="padding: 0px 20px 20px 20px;">
                                            <font size="2" color="#0050C0" face="Verdana">
                                                <%= Resources.richadmin.richadmin_ForgottenPassword_Instructions%>
                                            </font>
                                        </div>
                                        <table id="table4" border="0" cellpadding="0" cellspacing="0" style="text-align: center;
                                            margin-left: auto; margin-right: auto;">
                                            <tbody>
                                                <tr>
                                                    <td align="right">
                                                        <strong><font color="#2857aa" size="2">
                                                            <%= Resources.richadmin.richadmin_ForgottenPassword_Username%>:</font>&nbsp;&nbsp;
                                                        </strong>
                                                    </td>
                                                    <td>
                                                        <font size="1">
                                                            <asp:TextBox ID="txtUsername" runat="server" Width="150px" />
                                                        </font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <span style="font-weight: 400"><font size="2" color="#0050C0" face="Verdana"><b>OR</b></font>
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <strong><font color="#2857aa" size="2">
                                                            <%= Resources.richadmin.richadmin_ForgottenPassword_EmailAddress%>:&nbsp;&nbsp;</font></strong>
                                                    </td>
                                                    <td>
                                                        <font size="1">
                                                            <asp:TextBox ID="txtEmailAddress" runat="server" Width="150px" />
                                                        </font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Button ID="btnLogin" runat="server" Text="<%$ Resources:richadmin, richadmin_Default_FindPasswordButton %>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <br />
                                                        <a href="/richadmin/" style="text-decoration: none;"><font color="#2857aa" size="2">
                                                            <%= Resources.richadmin.richadmin_ForgottenPassword_BackToLoginPage%>
                                                        </font></a>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td background="/admin/images/framebottom.gif" height="20">
                                        &nbsp;
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <div id="divErrorUsernameOrEmailAddressRequired" runat="server" align="center" visible="false">
            <p>
                <font size="2" face="arial" color="red"><b>
                    <%= Resources.richadmin.richadmin_ForgottenPassword_RequiredMessage%></b></font>
            </p>
        </div>
        <div id="divErrorNoUsernameExists" runat="server" align="center" visible="false">
            <p>
                <font size="2" face="arial" color="red"><b>
                    <%= Resources.richadmin.richadmin_ForgottenPassword_NoUsernameExists%></b></font>
            </p>
        </div>
        <div id="divErrorNoEmailExists" runat="server" align="center" visible="false">
            <p>
                <font size="2" face="arial" color="red"><b>
                    <%= Resources.richadmin.richadmin_ForgottenPassword_NoEmailAddressExists%></b></font>
            </p>
        </div>
    </div>
    <div id="divForgottenPasswordStepTwo" runat="server" align="center" style="margin-top: 100px;"
        visible="false">
        <table id="table1" width="400" border="0" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td>
                        <div style="width: 400px; height: 77px; background-image: url(/admin/images/loginheader_success.gif);">
                            <div style="font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 16px; font-weight: bold;
                                line-height: 77px; padding-left: 80px; color: rgb(255, 255, 255);">
                                <%= Resources.richadmin.richadmin_ForgottenPassword_Heading%></div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="table5" style="border-left: 2px solid rgb(40, 87, 170); border-right: 2px solid rgb(40, 87, 170);
                            border-width: 0px 2px 2px; border-bottom: 2px solid rgb(40, 87, 170);" width="400"
                            border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td class="body" height="20">
                                        <table id="table6" width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2" valign="top" align="center">
                                                        <font size="2" color="#0050C0" face="Verdana">
                                                            <%= Resources.richadmin.richadmin_ForgottenPassword_SuccessMessage%><br />
                                                            <asp:Literal ID="litFoundEmailAddress" runat="server" /> </font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" style="padding-top: 20px;">
                                                        <a href="/richadmin/" style="text-decoration: none;"><font color="#2857aa" size="2">
                                                            <%= Resources.richadmin.richadmin_ForgottenPassword_SuccessMessageClickToLogin%>
                                                        </font></a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td background="/admin/images/framebottom.gif" height="20">
                                        &nbsp;
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <p align="center">
        <font color="#2857aa" face="Verdana" size="1">
            <%= Resources.richadmin.richadmin_ForgottenPassword_Copyright%>
            <%= DateTime.Now.Year.ToString()%>
            RichTemplate</font><br />
        <font color="#2857aa" face="Verdana" size="1">&nbsp;&nbsp;<%= Resources.richadmin.richadmin_ForgottenPassword_Version%>:
            September 2010 &nbsp;</font>
    </p>
    </form>
</body>
</html>
