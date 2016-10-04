
<!--#INCLUDE VIRTUAL="/admin/sessioncheck.asp"-->
<!--#INCLUDE VIRTUAL="/admin/db_connection.asp"-->
<%
'redirect page if there is no id number in the querystring
if Request.Querystring("id")="" then
	Response.Redirect("userslist.asp")
end if

%>



<%
SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString


	editSQL = "DELETE FROM onlineEducation WHERE ID=" & Request.Querystring("id")
	
Con.execute (editSQL)  
Con.close
Response.Redirect("userslist.asp")

%>
<html>
<head>
</head>
<body>
</body>
</html>