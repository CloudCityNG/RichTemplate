<%@ LANGUAGE="VBSCRIPT" %>
<% Server.ScriptTimeout = 600 %>
<!--#include file="loggedin.asp"-->
<!--#include file ="includes/dateformat.inc"-->
<!--#include file ="includes/upload.inc"-->
<%
Response.Expires = 0
Response.Buffer = TRUE
Response.Clear


Dim UploadRequest
Set UploadRequest = CreateObject("Scripting.Dictionary")

byteCount = Request.TotalBytes
RequestBin = Request.BinaryRead(byteCount)
BuildUploadRequest  RequestBin


Dim Con, FileCon
Dim objRecordSet, FileobjRecordset
Dim aux, aux1, FILEFLAG
Dim title, authorx, ndate, page, contenttype, filepathname, filename, value


	ID=UploadRequest.Item("id").Item("Value")
	title=UploadRequest.Item("title").Item("Value")
	title=replace(title, "'", "''")
	authorx=UploadRequest.Item("authorx").Item("Value")
	authorx=replace(authorx, "'", "''")
	ndate=UploadRequest.Item("ndate").Item("Value")

	if title="" then
	response.redirect "memoadd.asp?id=-1&task=error"
	end if
	if authorx="" then
	response.redirect "memoadd.asp?id=-1&task=error"
	end if
	if title="Enter A Memo Title" then
	response.redirect "memoadd.asp?id=-1&task=error"
	end if	
	if authorx="Enter Memo Author Here" then
	response.redirect "memoadd.asp?id=-1&task=error"
	end if		

	page=UploadRequest.Item("Page").Item("Value")
	page = replace(page, "'", "''")
	page = replace(page, "<BODY>", "")
	page = replace(page, "</BODY>", "")
	page = replace(page, "fuck", "####")
	page = replace(page, "cunt", "####")
	page = replace(page, "fucker", "######")
	page = replace(page, "fucking", "#######")
	page = right(page, len(page) - 2)
	
	if page="" then

	response.redirect "memoadd.asp?id=-1&task=error"

	else

	on error resume next
	contentType =UploadRequest.Item("File").Item("ContentType")
	FILEFLAG = err.number
	on error goto 0

	if FILEFLAG = 0 then
		contentType = UploadRequest.Item("File").Item("ContentType")
		filepathname = UploadRequest.Item("File").Item("FileName")
		filename = Right(filepathname,Len(filepathname)-InstrRev(filepathname,"\"))
		value = UploadRequest.Item("File").Item("Value")
	else
		filename = ""
	end if




	set Con = server.createobject("ADODB.Connection")
	set objRecordSet = server.createobject("ADODB.recordset")
	Con.ConnectionTimeout = 15
	Con.CommandTimeout = 30%>
<!--#INCLUDE FILE="conn.asp"-->

	<%
	
	if clng(id) < clng(0) then
		Set objRecordSet = Con.Execute("INSERT INTO compmemo (vardate, varSubject, varText, varauthor) VALUES (" & dateformat(ndate) & ", '" & title & "', '" & page & "', '" & authorx & "') ", lRecordsFound)

		rem get ID of new message
		Set objRecordSet = Con.Execute("SELECT * FROM compmemo WHERE varSubject = '" & title & "' and varText ='" & page & "' and varDate= " & dateformat(ndate) & " and varauthor='" & authorx & "'", lRecordsFound)
		If not objrecordset.eof Then 
			id = objrecordset("id")
		else
			id = 0	
		end if

	else
		Set objRecordSet = Con.Execute("UPDATE compmemo SET varDate = " & dateformat(ndate) & ", varSubject = '" & title & "', varText = '" & page & "', varauthor = '" & authorx & "' WHERE id = " & id, lRecordsFound)
	end if




	Con.Close

	
	set objrecordset = nothing
	set Con = nothing
	set UploadRequest = nothing


	response.redirect "memoadd.asp?id=-1&task=complete"

end if%>
<html>

<head>
<title></title>
</head>

<body>
</body>
</html>
