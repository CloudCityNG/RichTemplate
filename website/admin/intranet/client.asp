

 


<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<META HTTP-EQUIV="refresh" CONTENT="60">
<LINK HREF="style_richtemplate.css" REL="stylesheet" TYPE="text/css" TITLE="RichPortal_css">

<title>RichPortal 2002</title>
<SCRIPT LANGUAGE="JavaScript">
<!-- Begin
function popUp(URL) {
day = new Date();
id = day.getTime();
eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=yes,location=0,statusbar=0,menubar=0,resizable=0,width=550,height=550,left = 100,top = 100');");
}
// End -->
</script>
<SCRIPT LANGUAGE="JavaScript">
<!--
function deleteaudio()
{
if (confirm("Are you sure you want to delete this item?")){
	return true;
}  else
	return false;
}
//-->
</SCRIPT>
</head>

<body topmargin="0" bgcolor="#ffffff" leftmargin="0" marginwidth="0" marginheight="0">


<!--#INCLUDE FILE="conn.asp"-->


<TABLE WIDTH=780 cellspacing="0" cellpadding="0" bgcolor="#FFFFFF">
<TR>
	<TD background="images/demobannerback.gif"><img border="0" src="images/demobannerleft.gif"></TD>
</TR>
<TR>
	<TD bgcolor="#FFFFFF">Manage Clients</TD>
</TR>
</TABLE>


<table border="0" width="750" cellspacing="0" cellpadding="0">
  <tr>
    <td width="100%">
      &nbsp;
      <table border="0" width="750" cellspacing="0" cellpadding="8">
        <tr>
          <td width="445" valign="top" bgcolor="#FFFFFF">
          <center>
          
          </center>
         
<%
myClient = request.form("client")
if myClient<>"" then
Session("myclient")=myClient
end if
myService= request.form("service")
if myService<>"" then
Session("myService")=myService
end if

myContactSQL = "Select * from vendors where clientid=" & cint(Session("myClient")) & ""
set rs = con.execute (myContactSQL)

While not RS.EOF
%>
<p><font size="1" face="Verdana"><b><%=RS("varCompany")%></b>&nbsp;-&nbsp;<%=RS("varCompType")%><br>
Contact:  <%=RS("varFname")%><br>
Phone:  <%=RS("varPhone")%><br>
Email:  <a href="mailto:<%=RS("varEmail")%>"><%=RS("varEmail")%></a><br>
Address:  <%=RS("varAddress")%>, &nbsp;<%=RS("varCity")%>,&nbsp;<%=RS("varState")%>,&nbsp;<%=RS("varZip")%><br>
Web Address:  <a href="<%=RS("varWeb")%>"><%=RS("varWeb")%></a><br><br>
<a href="contactedit.asp?id=<%=RS("id")%>">Edit</a>  <a href="client.asp?task=delete&id=<%=RS("id")%>" onclick="return deleteaudio();">Delete</a>
</P>
<%RS.MOVENEXT
WEND%>

<%=myClient%>  --  <%=myService%>
          </td>
          <td width="305" valign="top" bgcolor="#FFFFFF">
         
          <!--include File = "includes/headlines.inc"-->
          <!--#INCLUDE FILE = "includes/addnews.inc"-->
          
          
          <br>
          
          
          
          
          
          
         
          <BR>
          <!--#INCLUDE FILE = "includes/addcontacts.inc"-->
          </td>

        </tr>
      </table>
    </td>
 

<td valign="top">

          
          </td></tr>

          </table>

<!--#INCLUDE FILE = "includes/footer.inc"-->

</body>

</html>

