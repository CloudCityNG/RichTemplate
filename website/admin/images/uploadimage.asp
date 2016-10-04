<!--#include file="Loader.asp"-->
<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>New Page 1</title>
<TITLE>RichTemplate</TITLE>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richtemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richtemplate.com/purchase.html"
	}
}
</script>		

</head>

<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<%



	
IF REQUEST.QUERYSTRING("ToDo")="InsertImage" then
%>
<%if request.form("ToDo")="Upload Image" then%>
<%

	' print the upload file page

	Const ForReading = 1, ForWriting = 2, ForAppending = 8 
	dim fso, f, ts, fileContent, includeFile
	set fso = server.CreateObject("Scripting.FileSystemObject") 

	includeFile = Server.mapPath("upload_page.inc")

	if (fso.FileExists(includeFile)=true) Then
		set f = fso.GetFile(includeFile)
		set ts = f.OpenAsTextStream(ForReading, -2) 
		Do While not ts.AtEndOfStream
		 		fileContent = fileContent & ts.ReadLine & vbCrLf
		Loop
		fileContent = replace(fileContent, "$SCRIPTNAME", ScriptName)
		fileContent = replace(fileContent, "$NEWDIR", newdir)
		fileContent = replace(fileContent, "$NEWIMAGEDIR", newimagedirectory)

		if (bool_from_image_dir = 1) Then
			fileContent = replace(fileContent, "$$DONOTDELETE$$", "<input type=hidden value=" & NewImageDirectory & " name=newimagedir><input type=hidden name=FromImageDir value=1>")
		else
			fileContent = replace(fileContent, "$$DONOTDELETE$$", "<input type=hidden value=" & NewDir & " name=newdir>")
		End if

		response.write(fileContent)
	else
		PrintError "Template", "<B>Cannot open Upload Page file:</B> webedit_includes/upload_page.inc", "File not Found"
	End if
	


%>
<%else%>

<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}

</script>
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=javascript>

var fileWin
var prvTr
var prvTr2

// window.onload = this.focus
window.onerror = stopError

	function stopError() {
		return true;
	}

	function grey(tr) {
		prvTr2 = tr
		tr.className = 'b4';
		if ((prvTr != null) && (prvTr != tr))
			prvTr.className = '';
		prvTr = prvTr2
	}

	function ConfirmDelete(filename) {
		var deleteFile = confirm("Are you sure you wish to delete?");
		if (deleteFile == true) {
			document.location = "makeedit.asp?newdir=&ToDo=Delete&FileName=" + filename
		}
	}

	function ConfirmDeleteFolder(filename) {
		var deleteFile = confirm("Are you sure you wish to delete this folder and ALL its contents?");
		if (deleteFile == true) {
			document.location = "makeedit.asp?newdir=&isFolder=1&ToDo=Delete&FileName=" + filename
		}
	}

	function ConfirmImageDeleteFolder(filename) {
		var deleteFile = confirm("Are you sure you wish to delete this folder and ALL its contents?");
		if (deleteFile == true) {
			document.location = "makeedit.asp?newimagedir=&isFolder=1&ToDo=Delete&FileName=" + filename + "&FromImageDir=1"
		}
	}

	function ConfirmImageDelete(filename) {
		var deleteFile = confirm("Are you sure you wish to delete?");
		if (deleteFile == true) {
			document.location = "makeedit.asp?newimagedir=&ToDo=Delete&FileName=" + filename + "&FromImageDir=1"
		}
	}

	function SelectImage(ImageName) {
		window.opener.selectImage("../images/" + ImageName);
		self.close();
	}

	function SetBackgd(ImageName) {
		var setBg = confirm("Are you sure you wish to set this image as the page background image?");
		if (setBg == true) {
			window.opener.setBackgd("../images/" + ImageName);
			self.close();
		}
	}

	function getLink() {

		if (window.opener.foo.document.selection.type == "Control") {
			var oControlRange = window.opener.foo.document.selection.createRange();
			if (oControlRange(0).tagName.toUpperCase() == "IMG") {
				var oSel = oControlRange(0).parentNode;
			}
		} else {
			oSel = window.opener.foo.document.selection.createRange().parentElement();
		}

		if (oSel.tagName.toUpperCase() == "A")
		{
			document.linkForm.targetWindow.value = oSel.target
			document.linkForm.link.value = oSel.href
		}
	}

	function InsertLink() {
		targetWindow = document.linkForm.targetWindow.value;
		var linkSource = document.linkForm.link.value

		if (linkSource != "")
		{
			var oNewLink = window.opener.foo.document.createElement("<A>");
			oNewSelection = window.opener.foo.document.selection.createRange()

			if (window.opener.foo.document.selection.type == "Control")
			{
				selectedImage = window.opener.foo.document.selection.createRange()(0);
				selectedImage.width = selectedImage.width
				selectedImage.height = selectedImage.height
			}

			oNewSelection.execCommand("CreateLink",false,linkSource);

			if (window.opener.foo.document.selection.type == "Control")
			{
				oLink = oNewSelection(0).parentNode;
			} else
				oLink = oNewSelection.parentElement()

			if (targetWindow != "")
			{
				oLink.target = targetWindow;
			} else
				oLink.removeAttribute("target")

			window.opener.foo.focus();
			self.close();
		} else {
			alert("URL cannot be left blank")
			document.linkForm.link.focus()
		}
	}


	function CreateLink(LinkSource) {
		document.linkForm.link.value = LinkSource;
		document.linkForm.link.focus()
	}

	function RemoveLink() {
		if (window.opener.foo.document.selection.type == "Control")
		{
			selectedImage = window.opener.foo.document.selection.createRange()(0);
			selectedImage.width = selectedImage.width
			selectedImage.height = selectedImage.height
		}

		window.opener.foo.document.execCommand("Unlink");
		window.opener.foo.focus();
		self.close();
	}

	function getAnchors() {
		var allLinks = window.opener.foo.document.body.getElementsByTagName("A");
		for (a=0; a < allLinks.length; a++) {
				if (allLinks[a].href.toUpperCase() == "") {
					document.write("<option value=#" + allLinks[a].name + ">" + allLinks[a].name + "</option>")
				}
		}
	}

	function ViewFile(fileName) {
		
		if (fileWin) { fileWin.close() }

		var leftPos = (screen.availWidth-700) / 2
		var topPos = (screen.availHeight-500) / 2 
	 	fileWin = window.open(fileName,'fileWindow','width=700,height=500,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
		fileWin.focus()
		fileWin.location.reload(true);
	}

</script>
<%'*********************************%>
<%'sub PrintImageDir()
	' Print the contents of the directory
	' First, load the javascript functions

AllowDelete = 1
' Allow the deletion of files and folders from the File Manager, 0 = no, 1 = yes

AllowDeleteImage = 1
' Allow the deletion of images and image folders from the Image Manager, 0 = no, 1 = yes

AllowUpload = 1
' Allow users to upload files in File Manager, 0 = no, 1 = yes

AllowUploadImage = 1
' Allow users to upload images in Image Manager, 0 = no, 1 = yes

AllowRename = 1
' Allow users to rename files and folders in File Manager, 0 = no, 1 = yes

AllowRenameImage = 1
' Allow users to rename images and folders in Image Manager, 0 = no, 1 = yes	
	
URL = Request.ServerVariables("server_name")
scriptName = request.servervariables("SCRIPT_NAME")


	if CurrentImageDirectory = "" then
		CurrentImageDirectory = "/images"
		ImageDirectory = "/"
	end if

	'PrintJSCommon()

	Dim objFilename, objFSO, objFolder, objFiles, objSubfolders, i
	Set objFSO = Server.CreateObject("Scripting.FileSystemObject")
	
	
	If (objFSO.FolderExists(server.mappath(CurrentImageDirectory))=true) Then
		Set objFolder = objFSO.GetFolder(server.mappath(CurrentImageDirectory))
	else
		PrintError "Image Directory", "<B>Cannot open image directory for reading:</B> " & CurrentImageDirectory, "Directory Not Found"
	End if

	Set objFiles = objFolder.Files
	Set objSubfolders = objFolder.SubFolders

%>
<FORM METHOD=POST>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="webedit_images/1x1.gif" width="15" height="1"></td>
	<td class="heading1">Image Manager</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">Images - View, Insert, Rename, Delete or Upload<BR>
	Directories - Rename, Delete or Create directories</td>

  </tr>
  <tr>

	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	Current Working Directory: <%=CurrentImageDirectory%><br><br>
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="bevel1">
  		<tr>
		  <td>&nbsp;&nbsp;My Images and Folders</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="webedit_images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table border="0" cellspacing="0" cellpadding="0" width="98%" class="bevel2">
	  <tr><td width=30>&nbsp;</td><td class=bodybold width=29%>File Name</td><td class=bodybold width=17%>File Size (bytes)</td><td class=bodybold width=20%>Last Modified</td><td class=bodybold width=30% colspan=3>Action</td></tr>
<%
	if CurrentImageDirectory = "/" then
		CurrentImageDirectory = ""
		ImageDirectory = ""
	end if

	if (CurrentImageDirectory <> ImageDirectory) Then

	Dim previousDir
	previousDir = left(CurrentImageDirectory,inStrRev(CurrentImageDirectory,"/")-1)
%>
	<tr onMouseover="this.bgColor='lightgrey';" onMouseout="this.bgColor='';"><td width=30>&nbsp;<img src=webedit_images/icon_up.gif width=16 height=16></td><td class=body>[ <a href=<%=ScriptName%>?newimagedir=<%=(PreviousDir)%>&ToDo=PrintImageDir class=bodylink title="Move Up to Parent Directory">Up One Level</a> ]</td><td class=body>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
	<tr><td colspan=8 bgcolor=#D7D6D6><img src=webedit_images/1x1.gif width=1 height=1></td></tr>
<%
	End if

	Dim excluded_dir
	Dim DontShow

	' Display SubFolders here
	For Each objSubFolders in objFolder.subFolders
		DontShow = false
		For Each excluded_dir in DirectoryExcludes
			if (objSubFolders.name = excluded_dir) Then
				DontShow = true
			end if
		next
	if DontShow = false Then
		if AllowDeleteImage = 1 Then
			deleteLink = "<a href=javascript:ConfirmImageDeleteFolder('" & fixSpace(objSubfolders.name) & "') class=bodylink title=""Delete folder: '" & objSubfolders.name & "'"">Delete</a>"
		else
			deleteLink = "&nbsp;"
		End if

		if AllowRenameImage = 1 Then
			renameLink = "<a href=" & scriptname & "?newimagedir=" & (NewImageDirectory) & "&FromImageDir=1&isFolder=1&ToDo=Rename&FileName=" & (objSubfolders.name) & " class=bodylink title=""Rename folder: '" & objSubfolders.name & "'"">Rename</a>"
		else
			renameLink = "&nbsp;"
		end if

		'if AllowCopyImage = 1 Then
		'	copyLink = "<a href=" & scriptname & "?newimagedir=" & (NewImageDirectory) & "&FromImageDir=1&isFolder=1&ToDo=Copy&FileName=" & (objSubfolders.name) & " class=bodylink title=""Copy folder: '" & objSubfolders.name & "'"">Copy</a>"
		'else
		'	copyLink = "&nbsp;"
		'end if
%>
	<tr onMouseover="this.bgColor='lightgrey';" onMouseout="this.bgColor='';"><td width=30>&nbsp;<img src=webedit_images/icon_folder.gif width=16 height=16></td><td class=body><a href=<%=ScriptName%>?newimagedir=<%=(CurrentImageDirectory)%>/<%=(objSubFolders.name)%>&ToDo=PrintImageDir class=bodylink title="Change into: '<%=objSubfolders.name%>'"><%=objSubfolders.name%></a></td><td class=body><%=objSubfolders.size%></td><td class=body><%=objSubFolders.datelastmodified%></td><td><%=renameLink%></td><td><%=deleteLink%></td></tr>
	<tr><td colspan=8 bgcolor=#D7D6D6><img src=webedit_images/1x1.gif width=1 height=1></td></tr>
<%
	end if
	next
%>
	<tr><td colspan=9 align=center><table width=98% border=0 cellspacing=0 cellpadding=10><tr><br>
<%
ImageFileType = Array("jpg","jpeg","gif")		

	' Display image files 
	Dim x, deleteLink, renameLink, copyLink
	x = 0
	For Each objFiles in objFolder.Files
		For Each i in ImageFileType
			if (objFSO.GetExtensionName(objFiles.name) = i) Then
				if AllowDeleteImage = 1 Then
					deleteLink = "<br><a href=javascript:ConfirmImageDelete('" & (objFiles.name) & "') class=bodylink title=""Delete image: '" & objFiles.name & "'"">Delete</a>"
				else
					deleteLink = ""
				End if

				if AllowRenameImage = 1 Then
					renameLink = "<br><a href=" & ScriptName & "?newimagedir=" & (NewImageDirectory) & "&ToDo=Rename&FromImageDir=1&FileName=" & (objFiles.name) & " class=bodylink title=""Rename image: '" & objFiles.name & "'"">Rename</a>"
				else
					renameLink = ""
				end if

				if AllowCopyImage = 1 Then
					copyLink = "<br><a href=" & ScriptName & "?newimagedir=" & (NewImageDirectory) & "&ToDo=Copy&FromImageDir=1&FileName=" & (objFiles.name) & " class=bodylink title=""Copy image: '" & objFiles.name & "'"">Copy</a>"
				else
					copyLink = ""
				end if
%>
			<td width=25%><table border=0 cellspacing=0 cellpadding=0 width=100%><tr><td colspan=2 class=body><%=objFiles.name%></td></tr><tr><td width=50><img border=1 src=http://<%=URL%><%=(CurrentImageDirectory)%>/<%=(objFiles.name)%> width=90 height=90>&nbsp;</td><td width=200><a href=javascript:ViewFile('http://<%=URL%><%=(CurrentImageDirectory)%>/<%=(objFiles.name)%>') class=bodylink title="View image: '<%=objFiles.name%>'">View</a><br><a href=javascript:SelectImage("<%=(objFiles.name)%>") class=bodylink title="Insert image: '<%=objFiles.name%>' into your page"><b>Insert</b></a><br><a href=javascript:SetBackgd("<%=(objFiles.name)%>") class=bodylink title="Set image: '<%=objFiles.name%>' as backgound">Backgd</a><%=renameLink%><%=copyLink%><%=deleteLink%></td></tr><tr><td colspan=2 class=body><%=objFiles.size%> bytes</td></tr></table></td>
<%
			x = x + 1
				if (x=4) Then
					response.write("</tr><tr>")
					x = 0
				End if
			End if
		next
	next
%>
	</tr></table></td></tr>
	</table>
	</td>
	</tr>
	<tr>
		<td colspan="2"><img src="webedit_images/1x1.gif" width="1" height="10"></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td>
			<table width=98% cellpadding=0 cellspacing=0 border=0>
			<tr>
			<% if AllowUploadImage = 1 Then %><td width=100><input type=submit name="ToDo" value="Upload Image" class="Text90"></td><% end if %>
			<% if AllowCreateImageFolder = 1 Then %><td width=100><input type=submit name="ToDo" value="Create Folder" class="Text90"></td><% end if %>
			<td><input type=button name="cancel" value="Cancel" class="Text70" onClick=javascript:window.close()></td>
			<input type=hidden name="newimagedir" value="<%=NewImageDirectory%>">
			<input type=hidden name="FromImageDir" value="1">
			</tr>
	</table>
	</form>
<%end if

'End Sub ' PrintImageDir%>
<%
elseif request.querystring("ToDo") = "UploadPage" Then


maximagesize = 60720	
ImageFileType = Array("jpg","jpeg","gif")
mymsg="didn't work"
	Response.Buffer = True
	Dim success, toDofilesize, validImage
	Dim ImageFileSize
	ImageFileSize = Request.TotalBytes

	if (bool_from_image_dir = "1") Then
		toDofilesize = maximagesize
	else
		toDofilesize = maxfilesize
	End if

	if ((ImageFileSize > toDofilesize) OR (ImageFileSize < 630)) Then
		str_message = "Please select a file to upload. (No Greater than " & maxfilesize & "bytes)"
		icon = "error.gif"
	else

		' load object
		Dim load
		Set load = new Loader
		
		' calling initialize method
		load.initialize

		' File name
		Dim fileName
		fileName = LCase(load.getFileName("sourcefile"))

		' File binary data
		Dim fileData
		fileData = load.getFileData("sourcefile")
	
		' File path
		Dim filePath
		filePath = load.getFilePath("sourcefile")
	
		' File path complete
		Dim filePathComplete
		filePathComplete = load.getFilePathComplete("sourcefile")

		' File size
		Dim fileSize
		fileSize = load.getFileSize("sourcefile")
	
		' File size translated
		Dim fileSizeTranslated
		fileSizeTranslated = load.getFileSizeTranslated("sourcefile")

		' Content Type
		Dim contentType
		contentType = load.getContentType("sourcefile")

		' No. of Form elements
		Dim countElements
		countElements = load.Count

		' Value of text input field "name"
		Dim nameInput
		' nameInput = load.getValue("name")
		nameInput = filename

		bool_from_image_dir = load.getValue("FromImageDir")

		' Path where file will be uploaded
		Dim pathToFile

		if (CurrentDirectory = "") Then
			CurrentDirectory = "/"
		end if

		if (CurrentImageDirectory = "") Then
			CurrentImageDirectory = "/images"
		end if

		if (bool_from_image_dir = "1") Then
			ForceGoodInput fileName,0,1
			pathToFile = Server.mapPath(CurrentImageDirectory) & "\" & fileName
		else
			ForceGoodInput fileName,0,0
			pathToFile = Server.mapPath(CurrentDirectory) & "\" & fileName
		end if

		Dim fso
		Dim msgExists
		set fso = server.CreateObject("Scripting.FileSystemObject") 
		if (fso.FileExists(pathToFile)=true) OR (fso.FolderExists(pathToFile)=true)Then
			msgExists = "Could not upload file. A file or folder with that name already exists"
		else		
			' Uploading file data
			Dim fileUploaded
			fileUploaded = load.saveToFile ("sourcefile", pathToFile)
			
		end if
		
		' destroying load object
		Set load = Nothing

		If (fileUploaded = True) Then
			icon = "info.gif"
			str_message = fileName & " uploaded successfully."
			success = 1
		else
			icon = "error.gif"
			if msgExists = "" Then
				str_message = fileName & " could not be uploaded."
			else
				str_message = msgExists
			end if
			success = 0
		End If
	End if
			
	'PrintInfoMessage "Upload File / Image"
		
	If (bool_from_image_dir = "1") then
%>
		<input type=hidden name=ToDo value=PrintImageDir>
		<input type=hidden name=newimagedir value="<%=NewImageDirectory%>">
<%
	else
%>
		<input type=hidden name=ToDo value=PrintDir>
		<input type=hidden name=newdir value="<%=newdir%>">
<%
	End if
	
	If (success = 1) Then
%>
			  <input type="submit" name="Submit" value="OK" class="Text50">
<%
	else
	response.write mymsg
%>
			 dd <input type="button" name="Submit" value="OK" class="Text50" onClick="javascript:history.back()">
<%
	End if
%>
				</form>
				</td>
			  </tr>  
		</table>
<%
		
end if%>
<%'******************************new%>
</body>

</html>




















