<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#INCLUDE virtual="/admin/db_connection.asp"-->
<%' get action for page name'
myTask="Add"


PNAME = ""&myTask&" Search Tags"
PHELP = "../helpFiles/pageListing.asp#pressreleases"%>

<%
'ADD A NEW TAG
If Request.Querystring("submit") = "true" then
	
	tagName = Request.Form("tagName")
	tagName	= Replace(tagName,"'","''")
	tagType = Request.Form("tagType")
	
	
	If tagName <> "" and tagType <>"" then
	
		SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
		CON.OPEN ConnectionString

		insertTag = "Insert into TAGS (tagName, tagType, mode_x) Values ('"&tagName&"', "&tagType&", 'LIVE')"
		con.execute(insertTag)
		
		CON.CLOSE
		SET CON = NOTHING
		
		Response.Redirect("tagsmodule.asp")
	
	Else
	End if
End if

'GET TAG INFORMATION TO CHANGE

If Request.Querystring("task") = "edit" then

		SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
		CON.OPEN ConnectionString
		
		tagID = Request.Querystring("ID")

		updateTag = "Select * from  TAGS  where tagID = "&tagID&""
		'response.write updateTag
		Set RS = Con.Execute(updateTag)
		
		tagName = RS("tagName")
		tagType = RS("tagType")
		tagID = RS("tagID")
		
		CON.CLOSE
		SET CON = NOTHING

		'Response.Redirect("tagsmodule.asp")


End if


'CHANGE TAG INFORMATION

If Request.Querystring("action") = "change" then

	tagName = Request.Form("tagName")
	tagType = Request.Form("tagType")
	tagID = Request.Querystring("ID")

		SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
		CON.OPEN ConnectionString
	
		updateTag = "Update TAGS set tagName = '"&tagName&"', tagType = "&tagType&" where tagID = "&tagID&""
		con.execute(updateTag)
		

		CON.CLOSE
		SET CON = NOTHING
		
		Response.Redirect("tagsmodule.asp")
		

End if


%>





<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="en-us" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Untitled 1</title>
<link rel="stylesheet" href="../style_richtemplate.css" type="text/css" />

<script language="JavaScript">
<!--
function validate(form) {
    if (form.elements["tagName"].value.length<1) {alert("A tag name is required. Please enter a value."); form.elements["tagName"].focus(); return false; }
    if (form.elements["tagType"].value.length<1) {alert("A tag type is required. Please enter a value."); form.elements["tagType"].focus(); return false; }
    else
    {
	document.form1.submit();
    }
}
//-->
</script>



</head>


<body style="margin: 0">
<!--#INCLUDE virtual="/admin/headernew.inc"-->
<%If tagType <>"" then%>
<form name="form1" method="POST" action="tagActions.asp?action=change&ID=<%=tagID%>">
<%else%>
<form name="form1" method="POST" action="tagActions.asp?submit=true">
<%end if%>

<table style="width: 600px">
	<tr>
		<td style="width: 15px" class="bodybold">&nbsp;</td>
		<td class="bodyboldsection" colspan="2">Please enter your search tag below</td>
	</tr>
	<tr>
		<td style="width: 15px" class="bodybold">&nbsp;</td>
		<td class="bodybold" style="width: 57px">&nbsp;</td>
		<td class="bodybold">
		&nbsp;</td>
	</tr>
	<tr>
		<td style="width: 15px" class="bodybold">&nbsp;</td>
		<td class="bodybold" style="width: 57px">Tag Name:</td>
		<td class="bodybold">
		<input name="tagName" type="text" value="<%=tagName%>" style="width: 251px" /></td>
	</tr>
	<tr>
		<td style="width: 15px" class="bodybold">&nbsp;</td>
		<td class="bodybold" style="width: 57px">Tag Type:</td>
		<td class="bodybold">
		<select name="tagType" style="height: 22px">
		<%If tagType <>"" then%>
				<%If tagType = 1 then
					tagTypeName = "Major Subject Tags"%>
					<option value="2">Program Name Tags</option>
				<%Else
					tagTypeName = "Program Name Tags"%>
					<option value="1">Major Subject Tags</option>

				<%End if%>
			<option value="<%=tagType%>" selected><%=tagTypeName%></option>
		<%else%>
				<option value=""></option>
				<option value="1">Major Subject Tags</option>
				<option value="2">Program Name Tags</option>

		<%end if%>
		
	
		</select>
		
		
		
		</td>
	</tr>
	<tr>
		<td style="width: 15px" class="bodybold">&nbsp;</td>
		<td class="bodybold" style="width: 57px">&nbsp;</td>
		<td class="bodybold">
		<input type="button" name="Submit" onClick="javascript:validate(this.form);" value="Submit"/></td>
	</tr>
</table>
</form>

</body>

</html>
