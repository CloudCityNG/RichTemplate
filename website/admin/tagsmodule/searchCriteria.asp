<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#INCLUDE VIRTUAL = /admin/db_connection.asp-->
<%
	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString

%>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Untitled 1</title>
<link rel="stylesheet" type="text/css" href="/css/websitestyle_richtemplate.css" />
<style type="text/css">
.style8 {
	font-size: small;
}
</style>
</head>

<body>
<!-- SEARCH FORM START -->

	
		<table border="0" cellspacing="1" cellpadding="0" id="table1" class="contentBody" style="width: 100%">
			<tr>
				<td style="height: 25px;" class="style8">
				<strong>:: Simple Search</strong></td>
			</tr>
			<tr>
				<td style="height: 25px;">
				<p align="left"><strong>Search for keyword phrase:</strong> </td>
			</tr>
		<form method="POST" name="myform" action="tagSearch.asp">
				<tr>
				<td valign="top">
				<input type="text" name="q1" size="17"><input type="submit" value="Go!" name="B1"></td>
			</tr>
		
				<tr>
				<td style="height: 25px">
				<strong>In the following locations:</strong></td>
			</tr>
			<tr>
				<td style="height: 25px;">
				<table>
			<tr>
				<td style="width: 10px">
				<input name="searchArea" type="checkbox" value="bio-sketch" /> </td>
				<td style="width: 636px">Bio-sketches</td>
			</tr>
			<tr>
				<td style="width: 10px">
				<input name="searchArea" type="checkbox" style="width: 20px" value="press" /></td>
				<td style="width: 636px">Press Releases</td>
			</tr>
			<tr>
				<td style="width: 10px">
				<input name="searchArea" type="checkbox" value="publication" /></td>
				<td style="width: 636px">Publications or Documents</td>
			</tr>
			<tr>
				<td style="width: 10px">
				<input name="searchArea" type="checkbox" value="images" /></td>
				<td style="width: 636px">Images</td>
			</tr>
			<tr>
				<td style="width: 10px">
				<input name="searchArea" type="checkbox" style="width: 20px" value="sound" /></td>
				<td style="width: 636px">Sound Clips</td>
			</tr>
			<tr>
				<td style="width: 10px">
				<input name="searchArea" type="checkbox" value="video" /></td>
				<td style="width: 636px">Video Clips</td>
			</tr>
		</table></td>
			</tr>
			</form>
		<form method="POST" name="myform" action="tagSearch3.asp">
		
			<tr>
				<td style="height: 25px;" class="style8">
				&nbsp;</td>
			</tr>
		
			<tr>
				<td style="height: 25px;" class="style8">
				<strong>:: Advanced Search</strong></td>
			</tr>
		
			<tr>
				<td style="height: 25px;">
				<strong>Major Subjects:</strong><%
		
		

		'Populate checkboxes
		getMST = "Select * From TAGS where tagType = 1"
		Set RS = Con.Execute(getMST)
		
			If Not RS.EOF then
			x = 1%>
				
				
				</td>
			</tr>
			<tr>
				<td valign="top">
				
				
				<table width="100%">
					<tr>
			
			<%Do while Not RS.EOF
			
			
			If x = 4 then
			x = 0%>
		
						<td style="width: 10px"><input name="MST" type="checkbox"value="<%=RS("tagID")%>"/></td><td class="body"><%=RS("tagName")%></td>
					</tr>

			<%Else%>

						<td style="width: 10px"><input name="MST" type="checkbox" value="<%=RS("tagID")%>"/></td><td class="body"><%=RS("tagName")%></td>

			<%End if
			x = x + 1
			RS.MoveNext
			Loop
			
if x = 1 then
%>
<td style="width: 26px"></td><td></td><td></td></tr>



<%

end if

if x = 2 then
%>


<td></td><td></td></tr>


<%
end if
if x = 3 then
%>


<td></td></tr>


<%

end if
if x = 4 then
%>

</tr>


<%

end if


%>



</table>

			<%End if
			
			Con.Close
			Set con = nothing%>	
				
				</td>
			</tr>
			<tr>
				<td valign="top">
				&nbsp;</td>
			</tr>
			<tr>
				<td style="height: 25px;">
				<strong>Programs:</strong></td>
			</tr>
			<tr>
				<td valign="top">
		
				<%
		
		SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
		CON.OPEN ConnectionString
		
		recordID = Request.Querystring("recordID")

		'Populate checkboxes
		getMST = "Select * From TAGS where tagType = 2"
		Set RS = Con.Execute(getMST)
		
			If Not RS.EOF then
			x = 1%>
				
				<table style="width: 100%">
					<tr>
			
			<%Do while Not RS.EOF
			
			
			
				
			If x = 3 then
			x = 0
				%>
		
						<td style="width: 10px"><input name="PNT" type="checkbox" value="<%=RS("tagID")%>"/></td>
			<td class="body" valign="top"><%=RS("tagName")%></td>
					</tr>

			<%Else%>

						<td style="width: 10px"><input name="PNT" type="checkbox"  value="<%=RS("tagID")%>"/></td>
					<td class="body" valign="top"><%=RS("tagName")%></td>

			<%End if
			x = x + 1
			RS.MoveNext
			Loop
			
if x = 1 then
%>
<td></td><td></td><td></td></tr>



<%

end if

if x = 2 then
%>


<td></td><td></td></tr>


<%
end if
if x = 3 then
%>


<td></td></tr>


<%

end if
if x = 4 then
%>

</tr>


<%

end if


%>



</table>

			<%End if
			
			Con.Close
			Set con = nothing%>		

		
		
		
		</td>
			</tr>
			
			<tr>
				<td valign="top">
		&nbsp;</td>
			</tr>
			
			<tr>
				<td valign="top">
		
				</td>
			</tr>
			
			<tr>
				<td valign="top">
		&nbsp;</td>
			</tr>
			
			<tr>
				<td valign="top">
		&nbsp;</td>
			</tr>
			
			<tr>
				<td valign="top">
		<input name="q1" type="hidden" value="d" />
				<input name="Submit1" type="submit" value="Go!" /></td>
			</tr>
			
			</form>
			
			
			
			</table>
	
	
<!-- SEARCH FORM END -->


</body>

</html>
