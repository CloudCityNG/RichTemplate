<!--#INCLUDE FILE="sessioncheck.asp"-->

<!--#include file="db_connection.asp"--><%
' get action for page name'


PNAME = "Edit Web Site Style Sheet"



'********************************************
	'EDIT STYLE SHEET
	'ADDED 5/18/05 - JH


	strRootPath = Server.MapPath(Request.ServerVariables("Script_Name"))
	strRootPath = Left(strRootPath, Len(strRootPath) - Len(Request.ServerVariables("Script_Name")))

	Set fso = CreateObject("Scripting.FileSystemObject")
	Set f = fso.OpenTextFile(""&strRootPath&"\style_richtemplate.css", "1")

	
	HTML = f.ReadLine & vbCrLf
	do while f.AtEndOfStream = false

	HTML = HTML  &  f.ReadLine & vbCrLf

	loop
	set f = nothing
	set fso = nothing
'********************************************
	%>

<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>New Page 1</title>
<link rel="stylesheet" type="text/css" href="style_richtemplate.css">
</head>

<body topmargin="0" leftmargin="0" bgcolor="#C4DAFA" class="bodybold" style="text-align: right">
<form method="POST" action="richtemplate_page_logic.asp?task=style">
<!--#INCLUDE FILE="headernew.inc"-->
<table border="0" width="100%" cellspacing="0" cellpadding="0" height="25" id="table3">
	<tr>
		<td class="bodybold" bgcolor="#C4DAFA"><font color="#3054A7">&nbsp;
EDIT YOUR WEB SITE STYLE SHEET BELOW.&nbsp; CAUTION - CHANGES TAKE PLACE 
IMMEDIATELY.</font></td>
	</tr>
</table>
<table border="0" width="100%" cellspacing="0" cellpadding="0" id="table1">
	<tr>
		<td class="bodybold" colspan="2" bgcolor="#9EBEF5">
		<table border="0" width="210" id="table4" cellspacing="0" cellpadding="0">
			<tr>
				<td>
				<img border="0" src="images/2003_toolbar_left.gif" width="16" height="28"></td>
				<td background="images/2003_toolbar_back.gif"> 
		<INPUT type=image  value="save" img border="0" src="images/button_save01.gif" width="120" height="22" name="save"></td>
				<td background="images/2003_toolbar_back.gif"><img border="0" src="../images/seperator.gif" width="2" height="20"></td>
				<td background="images/2003_toolbar_back.gif">
		<a href="/admin/richtemplate_welcome.asp?mode=forms">
		<img border="0" src="images/button_no_save01.gif" width="59" height="22"></a></td>
				<td>
				<img border="0" src="images/2003_toolbar_right.gif" width="13" height="28"></td>
			</tr>
		</table>
		</td>
	</tr>
	<tr>
		<td width="105" class="bodybold">&nbsp;</td>
		<td class="bodybold">&nbsp;</td>
	</tr>
	<tr>
		<td width="105" class="bodybold" height="16">
		<p align="right">Web Page Title:&nbsp; </td>
		<td class="bodybold" height="16"><font  color="#3054A7">&nbsp;STYLE SHEET EDITOR</font></td>
	</tr>
	</table>

	<table border="0" width="100%" cellspacing="0" cellpadding="0" id="table2">
		<tr>
			<td>&nbsp;<textarea rows="30" name="html" cols="95%" ><%=html%></textarea></td>
		</tr>
		</table>
</form>

</body>

</html>