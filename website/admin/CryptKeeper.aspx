<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CryptKeeper.aspx.vb" Inherits="admin_CyptKeeper" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <b>Enter String:</b>&nbsp;<asp:TextBox ID="txtPlainText" runat="server" Width="300" /><br />
    <br />
    <asp:Button ID="btnSubmit" runat="server" Text="Generate" />
    
    </div>
    </form>
</body>
</html>
