
<%
'=================
' FileSystemObject
'=================

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'Upload Script Version 1.2
'Copyright © 2004, Yusuf Wiryonoputro. All rights reserved.
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Server.ScriptTimeout = 1007000

Class Upload

	Private nFileCount
	Private dictRequest
	'Private dictRequestFiles(5,6)
	Private dictRequestFiles()
	
	Private sAllowedTypes
	Private nMaxFileSize
	Private nErrNum
	Private sErrMsg
	
	Public Sub Recieve()
	
		nErrNum = 0
		
		sContentType = Request.ServerVariables("HTTP_CONTENT_TYPE")	
		if InStr(sContentType,"multipart/form-data")=0 Then	
			nErrNum = 1
			sErrMsg = "Form enctype is not multipart/form-data"
			Exit Sub
		End If	
			
		binData = Request.BinaryRead(Request.TotalBytes) '1
		if Request.Totalbytes > nMaxFileSize then
			nErrNum = 2
			sErrMsg = "The posted data exceeds the maximum size allowed."
'			Exit Sub
		End If	
		'binData = Request.BinaryRead(Request.TotalBytes) '2
		
		'Kalau 1 enable => tdk error
		'Kalau 1 disable, 2 enable => error		
		
		lenBinData = lenB(binData)
		set adoRs = server.CreateObject("ADODB.Recordset")
		If lenBinData>0 Then
			adoRs.Fields.Append "UploadData", 201, lenBinData
			adoRs.Open
			adoRs.AddNew
			adoRs("UploadData").AppendChunk binData
			adoRs.Update
			sData = adoRs("UploadData")'Char
		End If	
		
		arrTemp = split(sContentType,";")
		sBoundary = Split(Trim(arrTemp(1)), "=")(1)
		arrFieldValue = Split(sData,sBoundary)
				
		set dictRequest = server.CreateObject("Scripting.Dictionary")
		sBrowser = UCase(Request.ServerVariables("HTTP_USER_AGENT"))
				
		nFileCount=0
		For i=0 To UBound(arrFieldValue)
			fieldSeparate = InStr(arrFieldValue(i), Chr(13) & Chr(10) & Chr(13) & Chr(10))
			If fieldSeparate>0 Then
				fieldEnd	= fieldSeparate-3
				valueStart	= fieldSeparate+4
				valueEnd	= Len(arrFieldValue(i)) - fieldSeparate - 4 - 3
						
				sFieldRaw	= Mid(arrFieldValue(i), 3 , fieldEnd)
				sValue		= Mid(arrFieldValue(i), valueStart , valueEnd)

				If InStr(sFieldRaw,"filename=")>0 Then
				

					sLocal = getLocal(sFieldRaw)
					If InStr(sBrowser,"WIN")>0 Then
						posStart = InStrRev(sLocal, "\") + 1
						sFileName = Mid(sLocal, posStart)
					End If					
					If InStr(sBrowser,"MAC")>0 Then
						sFileName = sLocal
					End If					
					
					
					
					
					
					
					ReDim dictRequestFiles(5,6)
					dictRequestFiles(nFileCount,0) = getFieldName(sFieldRaw)
					dictRequestFiles(nFileCount,1) = sFileName
					dictRequestFiles(nFileCount,2) = sValue 'or File Data
					dictRequestFiles(nFileCount,3) = sLocal
					dictRequestFiles(nFileCount,4) = getFileType(sFieldRaw)
					dictRequestFiles(nFileCount,5) = IsAllowed(sFileName)
					
					nFileCount=nFileCount+1
				Else
					dictRequest.Add getFieldName(sFieldRaw),sValue		
				End If
			End If
		Next
	End Sub

	'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~	
	'	PRIVATE
	'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~	
	Private Function getFieldName(s)
		posStart = InStr(s, "name=") + 6 '6 krn ada tambahan "
		if InStr(s,Chr(34) & ";")>0 Then 'Chr(34) = "		
			's => Content-Disposition: form-data; name="File1"; filename="C:\Documents and Settings\Ys\My Documents\mytext.txt"
			'	  Content-Type: text/plain
			posEnd = InStr( posStart , s, Chr(34) & ";" )
		Else
			's => Content-Disposition: form-data; name="inpNewFileName"
			posEnd = inStr( posStart , s, Chr(34))		
		End If	
		getFieldName = Mid(s, posStart , posEnd - posStart)
	End Function

	Private Function getLocal(s)
		posStart = InStr(s, "filename=") + 10
		posEnd = InStr(s, Chr(34) & Chr(13) & Chr(10))
		getLocal = Mid(s, posStart, posEnd-posStart)
	End Function

	Private Function getFileType(s)
		posStart = InStr(s, "Content-Type: ")
		GetFileType = Mid(s, posStart + 14)
	End Function
	
	Private Function IsAllowed(sFileName)
		For Each Item In Split(sFileName,".")
			sExtention = Item
		Next

		IsAllowed = false
		For Each Item In Split(sAllowedTypes,"|")
			If LCase(Item) = LCase(sExtention) or LCase(Item) = "*"  Then
				IsAllowed = true
			    session("sExtention") = LCase(sExtention)
			End If
		Next
	End Function	
	
	'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~	
	'	PUBLIC
	'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~	
	Public Property Let AllowedTypes(sVal)
		sAllowedTypes = sVal
	End Property

	Public Property Let MaxFileSize(nVal)
		nMaxFileSize = nVal
	End Property

	Public Property Get ErrNum
		ErrNum = nErrNum
	End Property
	
	Public Property Get ErrMsg
		ErrMsg = sErrMsg
	End Property		
	'~~~~~~~~~~~~	
	
	Public Function RequestValue(s)
		For i=0 To nFileCount 'untuk file
			if dictRequestFiles(i,0) = s Then			
				RequestValue = dictRequestFiles(i,1)
				exit function
			End If
		Next
		RequestValue = dictRequest(s) 'utk selain file
	End Function

	Public Function RequestFileContent(s)
		if Len(CStr(nFileCount)) = 0 then 
			RequestFileContent = null
			exit function
		End If
		For i=0 To nFileCount
			if dictRequestFiles(i,0) = s Then
			
				RequestFileContent = dictRequestFiles(i,2)
				exit function
			End If
		Next
		RequestFileContent = null
	End Function
	
	Public Function RequestFileStatus(s)
		if Len(CStr(nFileCount)) = 0 then
			RequestFileStatus = null
			exit function
		End If
		For i=0 To nFileCount
			if dictRequestFiles(i,0) = s Then
				RequestFileStatus = dictRequestFiles(i,5)
				exit function
			End If
		Next
	End Function

	Public Function RequestFileType(s)
		if Len(CStr(nFileCount)) = 0 then 
			RequestFileType = null
			exit function
		End If
		For i=0 To nFileCount
			if dictRequestFiles(i,0) = s Then
			
				RequestFileType = dictRequestFiles(i,4)
				exit function
			End If
		Next
		RequestFileType = null
	End Function
	
	Public Function SaveFile(sPath,sContent)
		Set fso = server.CreateObject("Scripting.FileSystemObject")
		Set sFile = fso.CreateTextFile(sPath, True) 'Hati2
		sFile.Write(sContent)
		sFile.Close	
		Set fso = Nothing
		

	End Function
	
End Class
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
'above: Upload Script Version 1.2
'Copyright © 2003, Yusuf Wiryonoputro. All rights reserved.
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
dim sUploadedFile
sUploadedFile=""

set oFSO = server.CreateObject ("Scripting.FileSystemObject")

Set oUpload = New Upload 
Dim allowedFiles 
allowedFiles = "gif|jpg|jpeg|bmp|png"
allowedFiles = allowedFiles & "|doc|docx|htm|html|xls|xlsx|pdf|ppt|txt|csv"
allowedFiles = allowedFiles & "|mov|mp3|mid|wav|wma|swf|avi|mpeg|flv|zip|rtf|pptx"

oUpload.AllowedTypes = allowedFiles

'oUpload.AllowedTypes = "*" 'Accept all file types
oUpload.MaxFileSize = 18000000 
oUpload.Recieve()



If oUpload.ErrNum=1 Then 'DEFAULT (Form enctype is not multipart/form-data)
	ffilter=request("ffilter")'ffilter
	
	if(Len(CStr(request("inpCurrFolder")))=0) then
		currFolder = server.MapPath(arrBaseFolder(0)) 'opened folder (Physical)
	else
		currFolder = request("inpCurrFolder") 'opened folder (Physical)
	end if
			
	if(Len(CStr(request("inpFileToDelete")))<>0) then 'Delete File
		Set oFile = oFSO.GetFile(Server.MapPath(CStr(request("inpFileToDelete"))))
		delFileName = lcase(oFile.Name)
		oFile.Delete
		
'*************************************
'Delete from database start

	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString
	
	'REMOVE RELATIVE PATH FROM FILE NAME
	
	
	filePath = ""
	filePath = Request.Form("inpFileToDelete")
	filePath = Replace(filePath,delFileName,"")
	

	if delFileName <> "" then
	
		getFileID = "Select * from fileXref where fileName = '"&delFileName&"' and filePath = '"&filePath&"'"
		'Response.write getFileID
		set RS = Con.execute(getFileID)
		
		If Not RS.EOF then

		'delete tags
		delFiletag = "Delete from tagXref where recordID = "&RS("fileID")&" and recordType = 'document'"
	 	'response.write delfile
	 	con.execute(delFileTag)
	 	
		'delete file
		delFiletag = "Delete from fileXref where fileID = "&RS("fileID")&""
	 	'response.write delFileTag
	 	con.execute(delFileTag)
	 	
	 	End if
	 	
	End if	
	 	
	CON.CLOSE


'*************************************




	
	end if
	sMsg = ""
Else
	ffilter=oUpload.RequestValue("inpFilter")'ffilter
	
	'UPLOAD PROCESS HERE
	if(Len(CStr(oUpload.RequestValue("inpCurrFolder2")))=0) then
		currFolder = server.MapPath(arrBaseFolder(0)) 'opened folder (Physical)
	else		
		currFolder = oUpload.RequestValue("inpCurrFolder2") 'opened folder (Physical)
	end if

	If oUpload.ErrNum=0 Then
		If oUpload.RequestFileStatus("File1") Then
				sPath = currFolder & "\" & oUpload.RequestValue("File1")
				sContent = oUpload.RequestFileContent("File1")		
				oUpload.SaveFile sPath,sContent

				sUploadedFile=mid(sPath,InStrRev(sPath,"\")+1)'uploaded file
				



'*************************************
'Send to database start

	insFileName = oUpload.RequestValue("File1")
	
	filePath = sPath 
	vPath = server.MapPath("\")
	'response.write vpath
	filePath = Replace(filePath,insFileName,"")
	filePath = Replace(filePath, vPath,"")
	filePath = Replace(filePath, "\","/")
	

	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString
	
	fileUploadDate = now()
	sFileName = lcase(oUpload.RequestValue("File1"))
	response.Write(sExtention)

	If Instr(filePath, "/data/files/ResourceDocs/") > 0 then
		
	    fileType =  oUpload.RequestFileType("File1")
	
	 	SendToDB = "Insert Into ss_modules_resource (fileName, fileUploadDate, filePath, fileType, isDoc, status) Values ('"&sFileName&"','"&fileUploadDate&"', '"&filepath&"', '"&session("sExtention")&"', 1, 1)"
	    session("sExtention") = ""
	Else
	 	SendToDB = "Insert Into fileXref (fileName, fileUploadDate, filePath) Values ('"&sFileName&"','"&fileUploadDate&"', '"&filepath&"')"
	End if
	
	 	'SendToDB = "Insert Into fileXref (fileName, fileUploadDate, filePath) Values ('"&sFileName&"','"&fileUploadDate&"', '"&filepath&"')"
	 	'response.write sendtodb
	 	con.execute(sendToDB)
	 	
	CON.CLOSE

'Send to database end				
'*************************************





				
				
		Else
				sMsg = "The File Type is not allowed."	
		End If
	Else 'Ex. "The posted data exceeds the maximum size allowed."
		sMsg = oUpload.ErrMsg
	End If

End If
Set oUpload = Nothing



%>
