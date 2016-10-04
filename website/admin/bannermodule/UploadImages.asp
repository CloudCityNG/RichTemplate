<link rel="stylesheet" type="text/css" href="../style_richtemplate.css">
<%@ Language=VBScript %>

<!--#INCLUDE VIRTUAL="/admin/db_connection.asp"-->

<!--#INCLUDE VIRTUAL="/admin/sessioncheck.asp"-->
<%PNAME = "Administer Banners"%>

<%
Sub JavaRedirect 
	nav = "/admin/richtemplate_list_sections.aspx"
    main="banner_display.asp?pageID="&Request.Querystring("pageID")&""
    %>
    <SCRIPT language="JavaScript">
    <!--
    
    window.opener.top.basefrm.location 	= '<%=main%>';
    //window.opener.top.treeframe.location= '<%=nav%>';
    //window.close(); 
   // parent.location.href = 
    //'<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>


<% 

Response.Expires = -1
Server.ScriptTimeout = 600
%>

<!-- #include file="freeaspupload.asp" -->

<%

if Request.Querystring<>"" then
Session("ID") = Request.Querystring("id")
end if
response.write session("id")

' ****************************************************
' Change the value of the variable below to the pathname
' of a directory with write permissions, for example "C:\Inetpub\wwwroot"
  Dim uploadsDirVar
'  uploadsDirVar = "C:\Inetpub\webroot\biosolids_org\website\docs"
  uploadsDirVar = server.mappath("\data\images\banners")
' ****************************************************
' Note: this file uploadTester.asp is just an example to demonstrate
' the capabilities of the freeASPUpload.asp class. There are no plans
' to add any new features to uploadTester.asp itself. Feel free to add
' your own code. If you are building a content management system, you
' may also want to consider this script: http://www.webfilebrowser.com/

function OutputForm()
%>
    <form name="frmSend" method="POST" enctype="multipart/form-data" action="uploadImages.asp?action=done&pageid=<%=Request.Querystring("pageID")%>" onSubmit="return onSubmitForm();">

	<table border="0" id="table1" cellspacing="4">
		<tr>
			<td class="bodybold" colspan="2">
			<p align="left">Upload Banner Images Here</td>
		</tr>
		<tr>
			<td width="41" class="bodybold">File 1:</td>
			<td> <input name=attach1 type=file size=35></td>
		</tr>
		<tr>
			<td width="41" class="bodybold">File 2: </td>
			<td> <input name=attach2 type=file size=35></td>
		</tr>
		<tr>
			<td width="41" class="bodybold">File 3: </td>
			<td> <input name=attach3 type=file size=35></td>
		</tr>
		<tr>
			<td width="41" class="bodybold">File 4:</td>
			<td> <input name=attach4 type=file size=35></td>
		</tr>
		<tr>
			<td width="41">&nbsp;</td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td width="41">&nbsp;</td>
			<td> 
    <input style="margin-top:4" type=submit value="Upload">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<input type=button value="Close This Window" onClick="javascript:window.close();"></td>
		</tr>
	</table>
	
    <hr>
	
    </form>

<b>

<%
end function

function TestEnvironment()
    Dim fso, fileName, testFile, streamTest
    TestEnvironment = ""
    Set fso = Server.CreateObject("Scripting.FileSystemObject")
    if not fso.FolderExists(uploadsDirVar) then
        TestEnvironment = "<B>Folder " & uploadsDirVar & " does not exist.</B><br>The value of your uploadsDirVar is incorrect. Open uploadTester.asp in an editor and change the value of uploadsDirVar to the pathname of a directory with write permissions."
        exit function
    end if
    fileName = uploadsDirVar & "\test.txt"
    on error resume next
    Set testFile = fso.CreateTextFile(fileName, true)
    If Err.Number<>0 then
        TestEnvironment = "<B>Folder " & uploadsDirVar & " does not have write permissions.</B><br>The value of your uploadsDirVar is incorrect. Open uploadTester.asp in an editor and change the value of uploadsDirVar to the pathname of a directory with write permissions."
        exit function
    end if
    Err.Clear
    testFile.Close
    fso.DeleteFile(fileName)
    If Err.Number<>0 then
        TestEnvironment = "<B>Folder " & uploadsDirVar & " does not have delete permissions</B>, although it does have write permissions.<br>Change the permissions for IUSR_<I>computername</I> on this folder."
        exit function
    end if
    Err.Clear
    Set streamTest = Server.CreateObject("ADODB.Stream")
    If Err.Number<>0 then
        TestEnvironment = "<B>The ADODB object <I>Stream</I> is not available in your server.</B><br>Check the Requirements page for information about upgrading your ADODB libraries."
        exit function
    end if
    Set streamTest = Nothing
end function

function SaveFiles
    Dim Upload, fileName, fileSize, ks, i, fileKey

    Set Upload = New FreeASPUpload
    Upload.Save(uploadsDirVar)

	' If something fails inside the script, but the exception is handled
	If Err.Number<>0 then Exit function

    SaveFiles = ""
    ks = Upload.UploadedFiles.keys
    if (UBound(ks) <> -1) then
        SaveFiles = "<B>Files uploaded:</B><br>"
        for each fileKey in Upload.UploadedFiles.keys
            SaveFiles = SaveFiles & "&nbsp;&nbsp;"&Upload.UploadedFiles(fileKey).FileName & " (" & Upload.UploadedFiles(fileKey).Length & "B)<br>"

BannerName = upload.uploadedfiles(filekey).filename

		SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
		CON.OPEN ConnectionString
		

			
	MYsql="SELECT * FROM BANNER_MODULE WHERE BANNER_NAME= '" & BANNERNAME & "'"
		SET RS3=CON.EXECUTE (MYsql)
		
		IF RS3.EOF THEN
		
		
'******************************  ADD PAGE RANKING
				
					MYsql="SELECT TOP 1 RANK FROM BANNER_MODULE WHERE PAGEID= " & REQUEST.QUERYSTRING("PAGEID") & " ORDER BY RANK DESC"
					'RESPONSE.WRITE "<BR>MYSQL = "&MYSQL&"<br>"
					SET RS=CON.EXECUTE (MYsql)
					
					IF NOT RS.EOF THEN
					'RESPONSE.WRITE "<BR>RANK = "&RS("RANK")&"<Br>"
					
					IF RS("RANK")<1 OR RS("RANK")="" THEN
					
					
					'IF RS.EOF THEN
						MYRANK=1
						
						
					ELSE
						MYRANK= RS("RANK") + 1
						
						
						
					END IF
					
					ELSE
					
					MYRANK=1
					
					END IF
									
					'response.write "<BR>myrank = "&MYRANK&"<BR>"


					set rs=nothing
		

		
     
	SQL2 = "INSERT INTO BANNER_MODULE (BANNER_NAME, PageID, RANK) VALUES ('"&BannerName&"', "&REQUEST.QUERYSTRING("PAGEID")&", "&MYRANK&")"
	'response.write "<BR>sql2 = "&SQL2&""
	Con.Execute(SQL2)
    
 		END IF
         next
         
    else
        SaveFiles = "The file name specified in the upload form does not correspond to a valid file in the system."
    end if
    
   
Call JavaRedirect

    
end function
%></b>
<HTML>
<HEAD>
<TITLE>Test Free ASP Upload</TITLE>
<style>
BODY {background-color: white;font-family:arial; font-size:12}
</style>
<script>
function onSubmitForm() {
    var formDOMObj = document.frmSend;
    if (formDOMObj.attach1.value == "" && formDOMObj.attach2.value == "" && formDOMObj.attach3.value == "" && formDOMObj.attach4.value == "" )
        alert("Please press the browse button and pick a file.")
    else
        return true;
    return false;
}
</script>

</HEAD>

<BODY topmargin="0" leftmargin="0">

<!--#INCLUDE VIRTUAL="/admin/headernew.inc"-->
<br>

<%
If Request.Querystring("pageid")<>"" then

Session("bannerID") =Request.Querystring("pageID")

End if

Dim diagnostics
if Request.ServerVariables("REQUEST_METHOD") <> "POST" then
    diagnostics = TestEnvironment()
    if diagnostics<>"" then
        response.write "<div style=""margin-left:20; margin-top:30; margin-right:30; margin-bottom:30;"">"
        response.write diagnostics
        response.write "<p>After you correct this problem, reload the page."
        response.write "</div>"
    else
       ' response.write "<div style=""margin-left:150"">"
        OutputForm()
        response.write "</div>"
    end if
else
   ' response.write "<div style=""margin-left:150"">"
    OutputForm()
    response.write "&nbsp;&nbsp;</b>"&SaveFiles()&""
    response.write "<br><br></div>"
end if
				

%>

<!-- Please support this free script by having a link to freeaspupload.net either in this page or somewhere else in your site. -->
<p><br>

<!--- START OF HTML TO REMOVE - contains the script ratings submission -->

</p>
</BODY>
</HTML>