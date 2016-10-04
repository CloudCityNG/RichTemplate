
<!--#INCLUDE VIRTUAL="/admin/db_connection.asp"-->

<!--#INCLUDE VIRTUAL="/admin/sessioncheck.asp"-->
<%PNAME = "Administer Banners"
PHELP = "../helpFiles/pageListing.asp#banner"%>

<%	


	if Request.Querystring("submit")="go" then
	
	rotation_date = Request.Form("rotation_date")
	if rotation_date ="" then
	rotation_date = "null"
	ELSE 
	ROTATION_DATE = "#" & ROTATION_DATE & "#"
	end if
	

	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString
	
	myUpdateSQL2 = "Update BANNER_MODULE SET ROTATION = "&Request.Form("BannerOnLoad")&", ROTATION_DATE = "&ROTATION_DATE&" WHERE  PAGEID=" & request.querystring("PAGEID") & "" 
	response.write myupdatesql2
	con.execute(myUpdateSQL2)
	
	response.redirect "banner_display.asp?pageid="&Request.Querystring("pageID")&""
	
	End if
	%>

<html>

<head>
<meta http-equiv="Content-Language" content="en-us">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>New Page 1</title>
<link rel="stylesheet" href="../style_richtemplate.css" type="text/css">
</head>

<body topmargin="0" leftmargin="0">

<!--#INCLUDE VIRTUAL="/admin/headernew.inc"-->
<form method="POST" action="banner_rotation.asp?submit=go&pageid=<%=Request.querystring("pageid")%>">

<table border="0" width="600" id="table1">
	<tr>
		<td colspan="4">
		<p class="bodybold">Specify Rotation</td>
	</tr>
	
	<%
			SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
			CON.OPEN ConnectionString
	
			myRSSQL = "Select * from BANNER_MODULE WHERE  PAGEID=" & request.querystring("PAGEID") & ""
			SET RS = con.execute(myRSSQL)
			
			IF RS.EOF THEN 
			
			ROTATION = 6

			ELSE
			
			ROTATION = RS("ROTATION")

			END IF
			
			%>
	
	<tr>
		<td width="53" class="body">
		<p align="right">
		<input type="radio" value="6" name="BannerOnLoad"
		<%If ROTATION="6" then
		Response.write "checked"
		End if%> checked
		
		></td>
		<td width="93" class="body">No Rotation</td>
		<td width="26" class="bodybold"><font color="#961D30"><%If ROTATION="6" then%><img border="0" src="/admin/images/icon_warning.gif" width="24" height="24">
		</font></td>
		<td width="411" class="bodybold"><font color="#961D30">You must upload images before you can set rotation.
		<%end if%></font></td>
	</tr>


	<tr>
		<td width="53" align="right" class="body">
		<input type="radio" value="1" name="BannerOnLoad"
		<%If ROTATION=1 then
		Response.write "checked"
		End if%>
		
		></td>
		<td width="93" class="body">Every page load </td>
		<td width="440" class="body" colspan="2"><font color="#666666">(7 images in 
		rotation)</font></td>
	</tr>
	<tr>
		<td width="53" align="right" class="body">
		<input type="radio" value="2" name="BannerOnLoad"
		<%If ROTATION=2 then
		Response.write "checked"
		End if%>></td>
		<td width="93" class="body">Daily</td>
		<td width="440" class="body" colspan="2"><font color="#666666">(7 images in 
		rotation)</font></td>
	</tr>
	<tr>
		<td width="53" align="right" class="body">
		<input type="radio" value="3" name="BannerOnLoad"	
		<%If ROTATION=3 then
		Response.write "checked"
		End if%>></td>
		<td width="93" class="body">Weekly</td>
		<td width="440" class="body" colspan="2"><font color="#666666">(12 images in 
		rotation)</font></td>
	</tr>
	<tr>
		<td width="53" align="right" class="body">
		<input type="radio" value="4" name="BannerOnLoad"
		<%If ROTATION=4 then
		Response.write "checked"
		End if%>></td>
		<td width="93" class="body">Monthly</td>
		<td width="440" class="body" colspan="2"><font color="#666666">(12 images in 
		rotation)</font></td>
	</tr>
	<tr>
		<td width="53" align="right" class="body">
		<input type="radio" value="5" name="BannerOnLoad"
		<%If ROTATION=5 then
		Response.write "checked"
		End if%>></td>
		<td width="93" class="body">Quarterly</td>
		<td width="440" class="body" colspan="2"><font color="#666666">(4 images in 
		rotation)</font></td>
	</tr>
	<tr>
		<td width="53" align="right" class="body">&nbsp;</td>
		<td width="93" class="body">&nbsp;</td>
		<td width="440" class="body" colspan="2">&nbsp;</td>
	</tr>
	<tr>
		<td width="53" align="right" class="body">&nbsp;</td>
		<td width="93" class="body"><input type="submit" value="Submit" name="B1"></td>
		<td width="440" class="body" colspan="2">&nbsp;</td>
	</tr>
	</table>

	<p>&nbsp;</p>
</form>

</body>

</html>