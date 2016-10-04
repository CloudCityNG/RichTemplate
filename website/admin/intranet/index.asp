<!--#INCLUDE VIRTUAL="/admin/config.inc"-->
<!--#INCLUDE VIRTUAL="/admin/db_connection.asp"-->

<%Session("name")="index"%>

 

<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 6.0">
<META HTTP-EQUIV="refresh" CONTENT="60">
<link rel="stylesheet" type="text/css" href="../style_richtemplate.css">

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

</head>

<body topmargin="0" bgcolor="#ffffff" leftmargin="0" marginwidth="0" marginheight="0">
<%
PNAME = "Welcome " &Session("FIRSTNAME")
PHELP = "/admin/helpFiles/pageListing.asp#mainpage"
%>
<!--#INCLUDE VIRTUAL="/admin/headernew.inc"-->



<table border="0" width="750" cellspacing="0" cellpadding="0">
  <tr>
    <td width="100%">

      <table border="0" width="750" cellspacing="0" cellpadding="8">
        <tr>
          <td width="445" valign="top" bgcolor="#FFFFFF">
         
<!--INCLUDE FILE ="includes/addmemo.inc"-->

	
<%
SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString
'*********************** CHECK PACKAGE TYPE START *********************** 
SQL2 = "SELECT * FROM MODULES WHERE MODULENAME ='Calendar Events'"
Set RS = con.Execute(SQL2)
if not rs.eof then
	
	IF RS("ONLINE")<>FALSE THEN
	CALENDAR_ONLINE = TRUE%>
 <!--#INCLUDE VIRTUAL="admin/calendarmodule/calendarindex.inc"-->
  <BR>
  	<%ELSE%>
 <!--#INCLUDE FILE = "includes/addnews.inc"-->
  	<%end if%>

<%END IF%>
<!--INCLUDE FILE="includes/frontend_client.inc"-->

<!--INCLUDE FILE="includes/addpub.inc"-->

<!--#INCLUDE FILE="includes/addwebsearch.inc"-->
          </td>
          <td width="305" valign="top" bgcolor="#FFFFFF">
<%IF CALENDAR_ONLINE = TRUE THEN%>         
          <!--#INCLUDE FILE = "includes/addnews.inc"-->
<%END IF%>
     
         
          <BR>
          <!--INCLUDE FILE = "includes/addcontacts.inc"-->
<!--INCLUDE FILE = "includes/addpoll.inc"-->
          </td>

        </tr>
      </table>
    </td>
 

<td valign="top">

          
          </td></tr>

          </table>

<!--INCLUDE FILE = "includes/footer.inc"-->

</body>

</html>

