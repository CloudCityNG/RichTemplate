<!--#INCLUDE virtual="/admin/sessioncheck.asp"-->
<!--#include virtual="/admin/db_connection.asp"-->

<%
If Request.querystring("submit") = "yes" then

	metaTitle 	= Request.Form("metaTitle")
		If metaTitle <>"" then
			Session("metaTitle") = metaTitle
		End if
	metaKeyword = Request.Form("metaKeyword")
		If metaKeyword <> "" then
			Session("metaKeyword") = metaKeyword
		End if
		
	metaDesc	= Request.Form("metaDesc")
		If metaKeyword <> "" then
			Session("metaDesc") = metaDesc
		End if	
	
Elseif Request.Querystring("pageID") = "" then%>
<%PNAME = "Edit meta information"%>
<!--#INCLUDE virtual="/admin/headernew.inc"-->
<html>
<body style="margin: 0">
	<table border="0" width="100%" id="table1" class="body">
		<tr>
			<td colspan="2" class="body">Please save the page you are working on 
			before you add meta information.<br>
			<br>
			</td>
		</tr>

		<tr>
			<td width="302" valign="top"><strong>Meta Title:</strong></td>
			<td width="67%">Page not saved</td>
		</tr>
		<tr>
			<td width="302" valign="top"><strong>Meta Keywords:</strong></td>
			<td width="67%">Page not saved</td>
		</tr>
		<tr>
			<td width="302" valign="top"><strong>Meta Description:</strong></td>
			<td width="67%">Page not saved</td>
		</tr>
		<tr>
			<td width="302">&nbsp;</td>
			<td width="67%">&nbsp;</td>
		</tr>
		<tr>
			<td width="302">
	<input type=button value="Close" onClick="javascript:window.close();"></td>
			<td width="67%">&nbsp;</td>
		</tr>
</table>
</body>
</html>
<%


Else
	
	pageID = Request.Querystring("pageID")
	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString

	getRoot = "SELECT * FROM WEBINFO WHERE ID = "&pageID&""
	SET RS3 = CON.EXECUTE(getRoot)
	If Not RS3.EOF then
		metaKeyword = RS3("metaKeyword")
		metaTitle	= RS3("metaTitle")
		metaDesc	= RS3("metaDesc")
	
	RS3.Close
	End if
	Con.Close
	
End if
%>	
<%if Request.querystring("pageID") <> "" then%>
<html>
<%PNAME = "Edit meta information"%>
<%HeaderType = "simple"%>
<head>
<meta http-equiv="Content-Language" content="en-us">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Add/Edit Meta Data Here</title>
<link rel="stylesheet" type="text/css" href="../../admin/style.css">
</head>

<body style="margin: 0">
<!--#INCLUDE virtual="/admin/headernew.inc"-->


	<table border="0" width="100%" id="table1" class="body">
	<form method="POST" action="metainfo.asp?submit=yes&pageID=<%=pageID%>" name="metaData">
		<tr>
			<td colspan="2" class="body">Enter the meta information you would like saved to 
			this page.&nbsp; <strong>NOTE </strong>- be sure to save your page 
			once you have updated the meta information.<br>
			<br>
			</td>
		</tr>

<%If Request.Querystring("submit") = "yes" then%>
<tr><td colspan="2" class="body"><br>The meta information for this page has been stored. Please remember to save your webpage to make the changes 
	live.<br><br>
	<input type=button value="Close" onClick="javascript:window.close();"></td></tr>
<%else%>
		<tr>
			<td width="302" valign="top"><strong>Meta Title:</strong></td>
			<td width="67%"><textarea rows="3" name="metaTitle" cols="26"><%=metaTitle%>
</textarea></td>
		</tr>
		<tr>
			<td width="302" valign="top"><strong>Meta Keywords:</strong></td>
			<td width="67%"><textarea rows="3" name="metaKeyword" cols="26"><%=metaKeyword%>
</textarea></td>
		</tr>
		<tr>
			<td width="302" valign="top"><strong>Meta Description:</strong></td>
			<td width="67%"><textarea rows="3" name="metaDesc" cols="26"><%=metaDesc%>
</textarea></td>
		</tr>
		<tr>
			<td width="302">&nbsp;</td>
			<td width="67%"><input type="submit" value="Update" name="B1"></td>
		</tr>
	
<%end if%>			
</form>	</table>


	<p>&nbsp;</p>


</body>

</html>
<%end if%>