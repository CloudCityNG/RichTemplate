


<% @ Language="VBScript" %>
<%Response.Buffer=TRUE%>

<%URL = Request.ServerVariables("server_name")%>

<%	URL=Replace(URL,"www.","")
'response.write url%>
	
	
	
<html>


<!--#include file="db_connection.asp"-->

<!--#INCLUDE FILE="CDOSYS_Config.inc"-->

<%
Email 		= Request.Form("email")
username 	= Request.Form("username")
authPhrase 	= Request.Form("authPhrase")

If email<>"" or username<>"" then
	if email<>"" then
		searchTerm = "email"
		searchKey = email
	elseif username<>"" then
		searchKey = username
		searchTerm = "username"

	end if
else
Response.Redirect "/richadmin/"
end if

		

	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString

	if email<>"" then
	
	SQL = "Select * From users Where "&searchTerm&" = '"&SearchKey&"'"
	'response.write sql
	Set RS = CON.Execute(SQL)
	
	else 
	
	SQL = "Select * From users Where "&searchTerm&" = '"&SearchKey&"'"
	'response.write sql
	Set RS = CON.Execute(SQL)
	
	end if

	If not RS.EOF Then
	myEmail = RS("email")	

	Dim mytxt
	mytxt = "A password recovery request has been made.<br> Your RichTemplate Web Site Portal Password is: <b>"&RS("password")&"</b>"
	mytxt = mytxt & "<br><br>Please  <a href=http://"&url&"/richadmin/?pass_recovery=TRUE&UserName="&RS("USERNAME")&">click here</a> to login."
	mytxt = mytxt & "<br><br>Or copy the following url into your browsers address bar: http://"&url&"/richadmin/"
	
	Set iMsg.Configuration = iConf
	iMsg.From = "RichTemplate@"&URL&""
	iMsg.To = ""&myEmail&""
	iMsg.Subject = "RichTemplate Web Site Portal Password Recovery"
	iMsg.HTMLBody = mytxt
	iMsg.Send()
	'response.write mytxt
	
	'Release server resrces. 
	Set iMsg	= Nothing 	
	Set iConf	= Nothing
	Set Flds 	= Nothing

	x = "Your password has been sent to <br>"&RS("email")&".  <br>Please check that account and <a href='/richadmin/'>click here " & x & "</a>to login"
	
	loginImage = "images/loginheader_success.gif"
	
Else

	x = "Sorry! There is a problem verifying your account!  Please contact your System Administrator for assistance. <center><br><br><a href='/richadmin/'>Click here " & x & "</a>to go back.</center>"

	loginImage = "images/loginheader_failure.gif"
End If
%>

<head>
<title>Lost Password</title>
</head>
<link rel="stylesheet" type="text/css" href="style.css">
<body background="images/bacpic.gif">

<div align="center">

<table border="0" width="400" cellspacing="0" cellpadding="0" id="table2">
	<tr>
		<td>&nbsp;<p>&nbsp;</p>
		<p>


				<img border="0" src="<%=loginImage%>"></td>
	</tr>
	<tr>
		<td>
<body background="images/bacpic.gif">

<div align="center">

<table width="400" id="table3" cellspacing="5" cellpadding="0" style="border-left: 2px solid #2857AA; border-right: 2px solid #2857AA; border-bottom: 2px solid #2857AA">
	<tr>
		<td><font size="2" color="#3054A9" face="Verdana"><%Response.Write ""&x&""%></font></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
	</tr>
</table>

</div>

<font size="2">
</body>
</font>
		</td>
	</tr>
</table>

</div>
</body>
</html>