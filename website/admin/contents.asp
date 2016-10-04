<!--#include file="db_connection.asp"-->
<!--#INCLUDE FILE="sessioncheck.asp"-->

<%
Set Con = Server.CreateObject("Adodb.Connection")
Con.Open ConnectionString

SQL2 = "SELECT * FROM USERS WHERE ID=" & Session("userID")
Set rs2 = con.Execute(SQL2)

If not RS2.eof then
ALLOW_MODULES = RS2("ALLOW_MODULES")
ARR_ALLOW_MODULES 	= Split(ALLOW_MODULES, ", ")





Function in_array(element, arr)    
For i=0 To Ubound(arr)         
If Trim(arr(i)) = Trim(element) Then             
in_array = True            
Exit Function        
Else             
in_array = False        
End If     
Next 
End Function







End if



		%>
<html>

<head>
<meta http-equiv="Content-Language" content="en-us">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>New Page 1</title>
<base target="_self">
<link rel="stylesheet" type="text/css" href="/styles.css">
</head>

<body topmargin="0" leftmargin="0" bgcolor="#C4DAFA">
<b>TESTING!!!</b>
<table border="0" width="200" cellspacing="0" cellpadding="0" id="table1">
	
	
<%'************************ DISPLAY WEB CONTENT **********************************%>
<%IF SESSION("ALLOW_WEBCONTENT") = TRUE THEN%>
	
	<tr>
		<td><a target="_parent" href="mainpage.asp?mode=forms">
		<%If Session("mode")="forms" then
		Response.Write "<img border='0' src='images/content02.gif' width='200' height='33'>"
		Else
		Response.Write "<img border='0' src='images/content01.gif' width='200' height='33'>"
		End if%></a></td>
	</tr>
	

<%END IF%>	
<%If in_array("44",ARR_ALLOW_MODULES) Then %>  
    <tr><td><a target="_parent" href="mainpage.asp?secure_members=yes&mode=members"><%If Session("secure_members")=True then%><img border='0' src='images/membersonly02.gif' width='200' height='33'><%Else%><img border='0' src='images/membersonly01.gif' width='200' height='33'><%End If%></a></td></tr>
<%end if %>
<!--<%If in_array("31",ARR_ALLOW_MODULES) Then %>  
	<tr><td><a target="_parent" href="mainpage.asp?secure_education=yes&mode=education"><%If Session("secure_education")=True  then%><img border='0' src='images/education02.gif' width='200' height='33'><%Else%><img border='0' src='images/education01.gif' width='200' height='33'><%End If%></a></td></tr>
<%End If%>
-->
<%'************************ DISPLAY MODULES **********************************%>

	<tr>
		<td><a target="_parent" href="mainpage.asp?mode=modules">
		<%If Session("mode")="modules" then
		Response.Write "<img border='0' src='images/modules02.gif' width='200' height='33'>"
		Else
		Response.Write "<img border='0' src='images/modules01.gif' width='200' height='33'>"
		End if%></a></td>
	</tr>
	
<%'************************ DISPLAY IMAGE/FILE MANAGER **********************************%>

	<tr>
		<td><a target="_parent" href="mainpage.asp?mode=images">
		<%If Session("mode")="images" then
		Response.Write "<img border='0' src='images/imagebank02.gif' width='200' height='33'>"
		Else
		Response.Write "<img border='0' src='images/imagebank01.gif' width='200' height='33'>"
		End if%></a></td>
	</tr>
	
<%'*********************** CHECK PACKAGE TYPE START *********************** 

SQL2 = "SELECT * FROM PACKAGE_TYPE WHERE PACKAGE_SELECTED = 1"
		Set RS = con.Execute(SQL2)
		if not rs.eof then
IF RS("ADMIN_USERS")<>0 THEN%>	
	<%if Session("ACCESS_LEVEL")>2 then%>
	<tr>
		<td><a target="basefrm" href="/admin/adminusers/default.aspx">
		<%

		If Session("mode")="users" then
		Response.Write "<img border='0' src='images/adminusers02.gif' width='200' height='33'>"
		Else
		Response.Write "<img border='0' src='images/adminusers01.gif' width='200' height='33'>"
		End if%></a></td>
	</tr>
		<tr>
		<td><a target="basefrm" href="emails/default.aspx">
				<%
		If Session("mode")="emails" then
		Response.Write "<img border='0' src='images/AdminEmailButton02.gif' width='200' height='33'>"
		Else
		Response.Write "<img border='0' src='images/AdminEmailButton01.gif' width='200' height='33'>"
		End if%>
		</a></td>
	</tr>
	
	<%end if%>
	<%end if%>
<%END IF
'*********************** CHECK PACKAGE TYPE END *********************** 
%>

	
<%
'*********************** CHECK PACKAGE TYPE START *********************** 
SQL2 = "SELECT * FROM PACKAGE_TYPE WHERE PACKAGE_SELECTED = 1"
		Set RS = con.Execute(SQL2)
		if not rs.eof then
IF RS("ADMIN_EMAIL")<>0 THEN%>
		<tr>
		<td><a target="basefrm" href="emailaccess.asp?mode=email">
		
		<%
		
		If Session("mode")="email" then
		Response.Write "<img border='0' src='images/emailadmin02.gif' width='200' height='33'>"
		Else
		Response.Write "<img border='0' src='images/emailadmin01.gif' width='200' height='33'>"
		End if%></a></td>
	</tr>
	<%end if%>

<%END IF%>

	<%'************************ MICRO SITES **********************************%>
<%		SQL3 = "SELECT * FROM PACKAGE_TYPE WHERE PACKAGE_SELECTED = 1"
		Set RS = con.Execute(SQL3)
		if not rs.eof then
If RS("ADMIN_MICROSITES")<> 0 THEN

	if Session("ACCESS_LEVEL")>2 then%>
	<tr>
		<td><a target="_parent" href="mainpage.asp?mode=micro">
		<%If Session("mode")="micro" then
		Response.Write "<img border='0' src='images/microsites02.gif' width='200' height='33'>"
		Else
		Response.Write "<img border='0' src='images/microsites01.gif' width='200' height='33'>"
		End if%></a></td>
	</tr>	
		<%end if%>

<%END IF%>
<%END IF%>

<%'*********************** CHECK PACKAGE TYPE END *********************** %>

			<%if Session("ACCESS_LEVEL")>3 then%><tr>
		<td><a target="_parent" href="mainpage.asp?mode=admin">
		<%

		If Session("mode")="admin" then
		Response.Write "<img border='0' src='images/adminbutton02.gif' width='200' height='33'>"
		Else
		Response.Write "<img border='0' src='images/adminbutton01.gif' width='200' height='33'>"
		End if%></a></td>
	</tr>
	<tr><%end if%>
		<td>&nbsp;</td>

	</tr>





	
	<tr>
		<td>
		<p class="bodyNEW" align="left"><font color="#385B8F" size="1">&nbsp;&nbsp;Version: <%If session("platform") = 1 then 
		Response.Write "MSA "&Session("revision")&""
		elseif Session("platform") = 2 then
		Response.Write "MSSQL "&Session("revision")&"" 
		end if
		%> &nbsp;</font></td>

	</tr>

	
	
	
</table>
<% 'response.write (session("userid"))%>
    <p align="left" class="bodyNEW">
        &nbsp;</p>

</body>

</html>