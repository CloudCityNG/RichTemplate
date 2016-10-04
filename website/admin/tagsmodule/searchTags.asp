<!--#INCLUDE virtual="/admin/db_connection.asp"-->
<%' get action for page name'

recordID = Request.Querystring("recordID")
sectionID = Request.Querystring("sectionID")
recordType  = Request.Querystring("recordType")

If Request.Querystring("submit") = "yes" then

	MST = Request.Form("MST")
	response.write MST
	PNT = Request.Form("PNT")
	response.write PNT
	
	'Delete records
	
	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString

	delSearch = "Delete from tagXref where recordID = "&RecordID&" and recordType = '"&recordType&"'"
	'response.write delSearch
	con.execute(delSearch)


    arrMST = Split(Request.Form("MST"), ", ")

 	 	For Each MST In arrMST
		    
		    addSQL = "Insert into tagXref (tagID, recordID, recordType) Values (" & MST & ", "&recordID&", '"&recordType&"')"
			'response.write "SQL = "&addsql&"<br>"

			Con.execute (addSQL) 
    
    
    	Next

    arrPNT = Split(Request.Form("PNT"), ", ")

 	 	For Each PNT In arrPNT
		    
		    addSQL = "Insert into tagXref (tagID, recordID, recordType) Values (" & PNT & ", "&recordID&", '"&recordType&"')"

			Con.execute (addSQL) 
    
    
    	Next


Response.Redirect Session("pageRedirect")
End if



'---------------------------- ASSIGN VARIABLES FOR EACH TAG SECTION HERE ---------------------------- 

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

If Request.Querystring("recordType") = "webpage" then

	
	getName = "Select name from WebInfo where ID = "&recordID&""
	Set nameRS = con.Execute(getName)
	
		pageName = nameRS("name")
		Session("pageRedirect") = "/admin/richtemplate_list_pages.aspx?sectionID="&sectionID&""

Elseif Request.Querystring("recordType") = "press" then

	
	getName = "Select title_x from PR where ID_X = "&recordID&""
	Set nameRS = con.Execute(getName)
	
		pageName = nameRS("title_x")
		Session("pageRedirect") = "/admin/pressreleasemodule/pressreleasemodule.asp"
		
Elseif Request.Querystring("recordType") = "calendar" then

	
	getName = "Select event_name from calendar_event where ID = "&recordID&""
	Set nameRS = con.Execute(getName)
	
		pageName = nameRS("event_name")
		Session("pageRedirect") = "/admin/calendarmodule/calendar.asp"

Elseif Request.Querystring("recordType") = "jobs" then

	
	getName2 = "Select title_x from JOBS where ID_X = "&recordID&""
	Set nameRS2 = con.Execute(getName2)
	
		pageName = nameRS2("title_x")
		Session("pageRedirect") = "/admin/jobsmodule/jobsmodule.asp"
		
Elseif Request.Querystring("recordType") = "document" then

	
	getName2 = "Select fileName from fileXref where fileID = "&recordID&""
	Set nameRS2 = con.Execute(getName2)
	
		pageName = nameRS2("fileName")
		Session("pageRedirect") = "/editor2/assetmanager/assetmanager.asp"

End if
'---------------------------- ASSIGN VARIABLES FOR EACH TAG SECTION HERE ---------------------------- 


myTask="Manage"
PNAME = ""&myTask&" Search Tags for the ''"&pageName&"''"
HeaderType = "simple"%>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<link rel="stylesheet" type="text/css" href="/admin/style_richtemplate.css" />

<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Untitled 1</title>
</head>

<body style="margin: 0">
<form method="POST" action="searchTags.asp?submit=yes&recordID=<%=recordID%>&sectionID=<%=sectionID%>&recordType=<%=recordType%>">
<!--#INCLUDE virtual="/admin/headernew.inc"-->


<table style="width: 100%">
	<tr>
		<td style="width: 24px">&nbsp;</td>
		<td style="width: 124px" class="bodybold">&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td style="width: 24px">&nbsp;</td>
		<td style="width: 124px" class="bodybold" valign="top">Major Subject Tags:</td>
		<td>
		
		<%
		
		

		'Populate checkboxes
		getMST = "Select * From TAGS where tagType = 1"
		Set RS = Con.Execute(getMST)
		
			If Not RS.EOF then
			x = 1%>
				
				<table>
					<tr>
			
			<%Do while Not RS.EOF
			
			
				getTags = "Select * from tagXref where recordID = "&recordID&" and tagID = "&RS("tagID")&" and recordType = '"&recordType&"'"
				'response.write getTags
				Set RS2 = Con.execute(getTags)
	
				If not RS2.EOF then
					isChecked = "checked"
				Else
					isChecked = ""
				End if
			If x = 4 then
			x = 0%>
		
						<td><input name="MST" type="checkbox" <%=ischecked%> value="<%=RS("tagID")%>"/></td><td class="body"><%=RS("tagName")%></td>
					</tr>

			<%Else%>

						<td><input name="MST" type="checkbox" <%=ischecked%> value="<%=RS("tagID")%>"/></td><td class="body"><%=RS("tagName")%></td>

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
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td style="width: 24px">&nbsp;</td>
		<td class="bodybold" colspan="3"><hr style="height: 1px"></td>
	</tr>
	<tr>
		<td style="width: 24px">&nbsp;</td>
		<td style="width: 124px" class="bodybold" valign="top">Program Name Tags:</td>
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
				
				<table>
					<tr>
			
			<%Do while Not RS.EOF
			
			
				getTags = "Select * from tagXref where recordID = "&recordID&" and tagID = "&RS("tagID")&" and recordType = '"&recordType&"'"
				'response.write getTags
				Set RS2 = Con.execute(getTags)
	
				If not RS2.EOF then
					isChecked = "checked"
				Else
					isChecked = ""
				End if
				
			If x = 3 then
			x = 0
				%>
		
						<td><input name="PNT" type="checkbox" <%=ischecked%> value="<%=RS("tagID")%>"/></td>
			<td class="body" valign="top"><%=RS("tagName")%></td>
					</tr>

			<%Else%>

						<td><input name="PNT" type="checkbox" <%=ischecked%> value="<%=RS("tagID")%>"/></td>
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
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td style="width: 24px">&nbsp;</td>
		<td colspan="3"><hr style="height: 1px"></td>
	</tr>
	<tr>
		<td style="width: 24px">&nbsp;</td>
		<td style="width: 124px">
		<input name="Submit" type="submit" value="Assign Tags" style="height: 26px"></td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
</table>

</form>
</body>

</html>
