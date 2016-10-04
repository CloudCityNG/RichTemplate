<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="richadmin_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <title>RichTemplate 2.0 Web Site Administration Portal</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top: 100px;">
        <table id="table2" border="0" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td style="width: 1px; height: 51px; background-image: url('/admin/images/loginheader_left.png');
                        background-repeat: no-repeat;">
                    <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                        
                    </td>
                    <td style="width: 65px; height: 51px; background-image: url('/admin/images/loginheader_left_img_top.png');">
                    <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                    </td>
                    <td align="center" style="width: 300px; height: 51px; background-image: url(/admin/images/loginheader_top.png);
                        background-repeat: repeat-x;">
                        <div style="font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 16px; font-weight: bold;
                            padding-top: 28px; color: rgb(255, 255, 255);">
                            <%= Resources.richadmin.richadmin_Default_Heading %></div>
                    </td>
                    <td style="width: 55px; height: 51px; background-image: url('/admin/images/loginheader_top.png');">
                    <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                    </td>
                    <td style="width: 12px; height: 51px; background-image: url('/admin/images/loginheader_right.png');
                        background-repeat: no-repeat;">
                        <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                    </td>
                </tr>
                <tr>
                    <td style="border-left: 2px solid rgb(40, 87, 170); border-bottom: 2px solid rgb(40, 87, 170);">
                    <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                    </td>
                    <td style="width: 65px; height: 20px; background-image: url('/admin/images/loginheader_left_img_bottom.png');
                        background-repeat: no-repeat;">
                        <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                    </td>
                    <td>
                        <p align="center">
                            <font color="#2857aa" size="2">
                                <%=Request.Url.Host.ToString()%></font></p>
                    </td>
                    <td>
                    <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                    </td>
                    <td style="border-right: 2px solid rgb(40, 87, 170);">
                    <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                    </td>
                </tr>
                <tr>
                    <td style="border-left: 2px solid rgb(40, 87, 170); border-bottom: 2px solid rgb(40, 87, 170);">
                    <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                    </td>
                    <td>
                    <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                    </td>
                    <td>
                        <table id="table3" style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <strong><font color="#2857aa" size="2">
                                            <%= Resources.richadmin.richadmin_Default_Username%>:</font>&nbsp;&nbsp; </strong>
                                    </td>
                                    <td>
                                        <font size="1">
                                            <asp:TextBox ID="txtUsername" runat="server" Width="150px" />
                                        </font>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-top: 10px;">
                                        <strong><font color="#2857aa" size="2">
                                            <%= Resources.richadmin.richadmin_Default_Password%>:&nbsp;&nbsp;</font></strong>
                                    </td>
                                    <td style="padding-top: 10px;">
                                        <font size="1">
                                            <asp:TextBox ID="txtPassword" runat="server" Width="150px" TextMode="Password" />
                                        </font>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" height="60px">
                                        <asp:Button ID="btnLogin" runat="server" Text="<%$ Resources:richadmin, richadmin_Default_LoginButton %>" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td>
                    <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                    </td>
                    <td style="border-right: 2px solid rgb(40, 87, 170);">
                    <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="border-left: 2px solid rgb(40, 87, 170); border-right: 2px solid rgb(40, 87, 170);">
                        <a href="/richadmin/ForgottenPassword.aspx" style="text-decoration: none; float: right;">
                            <font color="#2857aa" size="1">
                                <%= Resources.richadmin.richadmin_Default_ForgotYourPassword%>&nbsp;&nbsp;
                            </font></a>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="border-left: 2px solid rgb(40, 87, 170); border-bottom: 2px solid rgb(40, 87, 170);
                        border-right: 2px solid rgb(40, 87, 170);" background="/admin/images/framebottom.gif"
                        height="20">
                        <img src="/images/blank.jpg" width="0" height="0" style="display:none;"/>
                    </td>
                </tr>
            </tbody>
        </table>
        <p align="center">
            <font color="#2857aa" face="Verdana" size="1">
                <%= Resources.richadmin.richadmin_Default_Copyright %>
                <%= DateTime.Now.Year.ToString()%>
                Rich Template</font><br />
            <font color="#2857aa" face="Verdana" size="1">&nbsp;&nbsp;<%= Resources.richadmin.richadmin_Default_Version%>:
                September 2010 &nbsp;</font>
        </p>
    </div>
    <div id="divErrorMessage" runat="server" align="center" visible="false">
        <p>
            <font size="2" face="arial" color="red"><b><asp:Literal ID="litErrorMessage" runat="server" /></b></font>
        </p>
    </div>
    </form>
</body>
</html>
