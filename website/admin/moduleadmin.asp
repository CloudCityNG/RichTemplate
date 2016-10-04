


<!--#include file="db_connection.asp"-->

<!--#INCLUDE FILE="sessioncheck.asp"-->
<%
Sub JavaRedirect 
    url="mainpage.asp?mode=modules"
    %>
    <SCRIPT language="JavaScript">
    <!--
    parent.location.href = 
    '<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>
<%PNAME = "Administer Modules"%>
<%
Set Con = Server.CreateObject("Adodb.Connection")
Con.Open ConnectionString


if request.querystring("task")="edit" then

if request.form("pr")="ON" THEN
	MYSQL="Update modules set online=-1 where modulename='Press Releases'"
	con.execute (MYSQL)
else
	MYSQL="Update modules set online=0 where modulename='Press Releases'"
	con.execute (MYSQL)
end if


if request.form("jobs")="ON" THEN
	MYSQL="Update modules set online=-1 where modulename='Job Opportunities'"
	con.execute (MYSQL)
else
	MYSQL="Update modules set online=0 where modulename='Job Opportunities'"
	con.execute (MYSQL)
end if


if request.form("calendar")="ON" THEN
	MYSQL="Update modules set online=-1 where modulename='Calendar Events'"
	con.execute (MYSQL)
else
	MYSQL="Update modules set online=0 where modulename='Calendar Events'"
	con.execute (MYSQL)
end if


if request.form("faq")="ON" THEN
	MYSQL="Update modules set online=-1 where modulename='FAQ'"
	con.execute (MYSQL)
else
	MYSQL="Update modules set online=0 where modulename='FAQ'"
	con.execute (MYSQL)
end if



if request.form("polls")="ON" THEN
	MYSQL="Update modules set online=-1 where modulename='Polls'"
	con.execute (MYSQL)
else
	MYSQL="Update modules set online=0 where modulename='Polls'"
	con.execute (MYSQL)
end if



if request.form("employees")="ON" THEN
	MYSQL="Update modules set online=-1 where modulename='Employees'"
	con.execute (MYSQL)
else
	MYSQL="Update modules set online=0 where modulename='Employees'"
	con.execute (MYSQL)
end if



if request.form("vendors")="ON" THEN
	MYSQL="Update modules set online=-1 where modulename='Vendors'"
	con.execute (MYSQL)
else
	MYSQL="Update modules set online=0 where modulename='Vendors'"
	con.execute (MYSQL)
end if


Call JavaRedirect

end if

%>
<html>

<head>
<meta http-equiv="Content-Language" content="en-us">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Press Releases</title>
<link rel="stylesheet" type="text/css" href="style_richtemplate.css">
</head>

<body topmargin="0" leftmargin="0">
<form action="moduleadmin.asp?task=edit" method="post">
<!--#INCLUDE FILE="headernew.inc"-->

<table border="0" width="100%" id="table1">
  <tr>
    <td align="right" width="175" class="bodybold">Press Releases</td>
    <td>
    <%
    mySQL="Select online from modules where modulename='Press Releases'"
    set rs=con.execute(mySQl)
    %>
    <input type="checkbox" name="pr" value="ON" <%if rs("online")=-1 then%>checked<%end if%>>
    <%
    rs.close
    Set mySQL=nothing
    %>
    </td>
  </tr>
  <tr>
    <td align="right" width="175" class="bodybold">Job Opportunities</td>
    <td>
        <%
    mySQL="Select online from modules where modulename='Job Opportunities'"
    set rs=con.execute(mySQl)
    %>
    <input type="checkbox" name="jobs" value="ON"<%if rs("online")=-1 then%>checked<%end if%>>
        <%
    rs.close
    Set mySQL=nothing
    %>
    </td>
  </tr>
  <tr>
    <td align="right" width="175" class="bodybold">Calendar Events</td>
    <td>
            <%
    mySQL="Select online from modules where modulename='Calendar Events'"
    set rs=con.execute(mySQl)
    %>
    <input type="checkbox" name="calendar" value="ON"<%if rs("online")=-1 then%>checked<%end if%>>
    
        <%
    rs.close
    Set mySQL=nothing
    %>
    </td>
  </tr>
  <tr>
    <td align="right" width="175" class="bodybold">FAQ</td>
    <td>
            <%
    mySQL="Select online from modules where modulename='FAQ'"
    set rs=con.execute(mySQl)
    %>
    <input type="checkbox" name="faq" value="ON"<%if rs("online")=-1 then%>checked<%end if%>>
       <%
    rs.close
    Set mySQL=nothing
    %> 
    
    </td>
  </tr>
  <tr>
    <td align="right" width="175" class="bodybold">Polls</td>
    <td>
            <%
    mySQL="Select online from modules where modulename='Polls'"
    set rs=con.execute(mySQl)
    %>
    <input type="checkbox" name="polls" value="ON"<%if rs("online")=-1 then%>checked<%end if%>>
        <%
    rs.close
    Set mySQL=nothing
    %>
    
    </td>
  </tr>
  <tr>
    <td align="right" width="175" class="bodybold" height="24">Employee Contact List</td>
    <td height="24">
            <%
    mySQL="Select online from modules where modulename='Employees'"
    set rs=con.execute(mySQl)
    %>
    <input type="checkbox" name="employees" value="ON"<%if rs("online")=-1 then%>checked<%end if%>>
    
        <%
    rs.close
    Set mySQL=nothing
    %>
    </td>
  </tr>
  <tr>
    <td align="right" width="175" class="bodybold">Vendor Conact List</td>
    <td>
            <%
    mySQL="Select online from modules where modulename='Vendors'"
    set rs=con.execute(mySQl)
    %>
    <input type="checkbox" name="vendors" value="ON"<%if rs("online")=-1 then%>checked<%end if%>>
        <%
    rs.close
    Set mySQL=nothing
    %>
    
    </td>
  </tr>
  <tr>
    <td align="right" width="175">&nbsp;</td>
    <td>
            <input type="submit" value="Update" name="B1"></td>
  </tr>
</table>
</form>
</body>

</html>
<%
con.close
%>