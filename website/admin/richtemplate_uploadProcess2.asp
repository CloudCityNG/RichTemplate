<%
Sub JavaRedirect 
    main="richtemplate_list_pages.aspx?SectionID="&Request.Querystring("SectionID")&""
    %>
    <SCRIPT language="JavaScript">
    <!--
    
    window.opener.top.basefrm.location 	= '<%=Session("URL")%>';
   // window.close(); 
   // parent.location.href = 
    //'<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>

<%
'Adjust this depending on the size of the files you'll
'be expecting; longer timeout for larger files!
Server.ScriptTimeout = 5400

Const ForWriting = 2
Const TristateTrue = -1
CrLf = Chr(13) & Chr(10)

'This function retreives a field's name
Function GetFieldName(infoStr)
	sPos = InStr(infoStr, "name=")
	EndPos = InStr(sPos + 6, infoStr, Chr(34) & ";")
	If EndPos = 0 Then
		EndPos = inStr(sPos + 6, infoStr, Chr(34))
	End If
	GetFieldName = Mid(infoStr, sPos + 6, endPos - (sPos + 6))
End Function

'This function retreives a file field's filename
Function GetFileName(infoStr)
	sPos = InStr(infoStr, "filename=")
	EndPos = InStr(infoStr, Chr(34) & CrLf)
	GetFileName = Mid(infoStr, sPos + 10, EndPos - (sPos + 10))
End Function

'This function retreives a file field's MIME type
Function GetFileType(infoStr)
	sPos = InStr(infoStr, "Content-Type: ")
	GetFileType = Mid(infoStr, sPos + 14)
End Function

'Yank the file (and anything else) that was posted
PostData = ""
Dim biData
biData = Request.BinaryRead(Request.TotalBytes)

'Careful! It's binary! So, let's change it into something a bit more manageable.
For nIndex = 1 to LenB(biData)
  PostData = PostData & Chr(AscB(MidB(biData,nIndex,1)))
Next

' Having used BinaryRead, the Request.Form collection is no longer available to us. 
' So, we have to parse the request variables ourselves!
' First, let's find that encoding type
ContentType = Request.ServerVariables("HTTP_CONTENT_TYPE")
ctArray = Split(ContentType, ";")

'File posts only work well when the encoding is 
'"multipart/form-data", so let's check for that!
If Trim(ctArray(0)) = "multipart/form-data" Then
	ErrMsg = ""
	' grab the form boundary...
	bArray = Split(Trim(ctArray(1)), "=")
	Boundary = Trim(bArray(1))
	'Now use that to split up all the variables!
	FormData = Split(PostData, Boundary)
	'Extract the information for each variable and its data
	Dim myRequest, myRequestFiles(9, 3) 
	Set myRequest = CreateObject("Scripting.Dictionary")
	FileCount = 0
	For x = 0 to UBound(FormData)
		'Two CrLfs mark the end of the information about this field; everything after that is the value
		InfoEnd = InStr(FormData(x), CrLf & CrLf)
		If InfoEnd > 0 Then
			'Get info for this field, minus stuff at the end
			varInfo = Mid(FormData(x), 3, InfoEnd - 3)
			'Get value for this field, being sure to skip CrLf pairs at the start and the CrLf at the end
			varValue = Mid(FormData(x), InfoEnd + 4, Len(FormData(x)) - InfoEnd - 7)
			'Is this a file?
			If (InStr(varInfo, "filename=") > 0) Then
				'Place it into our files array
				'(While this supports more than one file uploaded at a time we only consider the
				' single file case in this example)
				myRequestFiles(FileCount, 0) = GetFieldName(varInfo)
				myRequestFiles(FileCount, 1) = varValue
				myRequestFiles(FileCount, 2) = GetFileName(varInfo)
				myRequestFiles(FileCount, 3) = GetFileType(varInfo)
				FileCount = FileCount + 1
			Else
				'It's a regular field
				myRequest.add GetFieldName(varInfo), varValue
			End If
		End If
	Next
Else
	ErrMsg = "Wrong encoding type!"
End If 

'Save the actual posted file
'If supporting more than one file, turn this into a loop!

Set lf = server.createObject("Scripting.FileSystemObject")

'Use the filename that came with the file
'At this point, you need to determine what sort of
'client sent the file. Macintoshes only send the file
'name, with no path information, while Windows clients
'send the entire path of the file that was selected
BrowserType = UCase(Request.ServerVariables( "HTTP_USER_AGENT"))
If (InStr(BrowserType, "WIN") > 0) Then
	'It's Windows; yank the filename off the end!
	sPos = InStrRev(myRequestFiles(0, 2), "\")
	fName = Mid(myRequestFiles(0, 2), sPos + 1)
End If
If (InStr(BrowserType, "MAC") > 0) Then
	'It's a Mac. Simple.
	'(Mac filenames can contain characters that are 
	'illegal under Windows, so look out for that!)
	fName = myRequestFiles(0, 2)
End If

on error resume next
SavePath =  Request.QueryString("path") & "\" + fName
Set SaveFile = lf.CreateTextFile(SavePath, True)

if err.number > 0 then
  ErrMsg = err.Description + ": " + SavePath
else
	SaveFile.Write(myRequestFiles(0, 1))
	SaveFile.Close
end if

' do we have to insert the image after upload ?
insert = Request.QueryString("insert")


'IIS may hang if you don't explicitly return SOMETHING.
'So, redirect to another page or provide some kind of
'feedback below...
%>


<html>
<% If ErrMsg <> "" Then %> 
  <body onload="alert('File uploaded successfully !');window.close()">
<% else %>
<%  if insert = "1" then %>
      <body onload="alert('File uploaded successfully !');window.close()">
              <%Call JavaRedirect%>

<%  else %>
      <body onload="alert('File uploaded successfully !');window.close()">
              <%Call JavaRedirect%>

<%  end if %>
<% end if %>
</body>
</html>

