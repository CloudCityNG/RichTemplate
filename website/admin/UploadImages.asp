<%@ Language=VBScript %>
<%PNAME = "Upload Documents"%> 
<%HeaderType = "simple"%>
<!--#INCLUDE FILE="db_connection.asp"-->



<% 

Response.Expires = -1
Server.ScriptTimeout = 600
%>

<!-- #include file="freeaspupload.asp" -->

<%
            Set Con = Server.CreateObject("ADODB.CONNECTION")
            Con.Open ConnectionString



' ****************************************************
' Change the value of the variable below to the pathname
' of a directory with write permissions, for example "C:\Inetpub\wwwroot"
  Dim uploadsDirVar
  uploadsDirVar = Session("filePath")
  'uploadsDirVar = server.mappath("\data\files")
' ****************************************************
' Note: this file uploadTester.asp is just an example to demonstrate
' the capabilities of the freeASPUpload.asp class. There are no plans
' to add any new features to uploadTester.asp itself. Feel free to add
' your own code. If you are building a content management system, you
' may also want to consider this script: http://www.webfilebrowser.com/
function OutputForm()
%>

    <form name="frmSend" method="POST" enctype="multipart/form-data" action="uploadImages.asp?action=done&id=<%Request.Querystring("ID")%>" onSubmit="return onSubmitForm();">
	
	<p><b>&nbsp; Upload your Documents here: </b>(File Type: html; htm; txt; pdf; doc; avi; swf; xls; wmf;)</p>
	<p><font size="2">&nbsp; File 1: </font> <input name=attach1 type=file size=35><font size="2"><br>
    &nbsp;
    File 2: </font> <input name=attach2 type=file size=35><font size="2"><br>
    &nbsp;
    File 3: </font> <input name=attach3 type=file size=35><font size="2"><br>
    &nbsp;
    File 4: </font> <input name=attach4 type=file size=35><br>
    <br> 
    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    <input style="margin-top:4" type=submit value="Upload">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<input type=button value="Close This Window" onClick="javascript:window.close();"></p>
    </form>

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
        SaveFiles = "<B>Files uploaded:</B> "
        for each fileKey in Upload.UploadedFiles.keys
            SaveFiles = SaveFiles & Upload.UploadedFiles(fileKey).FileName & " (" & Upload.UploadedFiles(fileKey).Length & "B) "

Dim ClientImageURL





'
 
         next
         
    else
        SaveFiles = "The file name specified in the upload form does not correspond to a valid file in the system."
    end if
    

    
end function
%>

<HTML>
<HEAD>

    
<SCRIPT language="JavaScript">
   function OnSubmitForm2()
{

     	document.myform2.action ="/admin/richtemplate_filemgmt.asp?" + <%=server.urlEncode("FileURL")%>;
 		document.myform2.submit();
}

</SCRIPT>


<TITLE>Upload Images</TITLE>
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
<form method="post" name="myform2" action="richtemplate_filemgmt.asp"  target="basefrm">

  <input type="hidden" name="mode" size="1" value="OPEN">
<input type="hidden" name="language" size="1" value="EN">
<input type="hidden" name="urlabs" size="1" value="<%="http://"&Request.ServerVariables("HTTP_HOST")%>/data/files/">
<input type="hidden" name="pathabs" size="22" value="<%=Replace(Server.MapPath("\"),"\","/")%>/data/files/">
<input type="hidden" name="tech" size="1" value="asp">
<input type="hidden" name="filter" size="1" value="html;htm;txt;pdf;doc;avi;swf;xls;wmf">
</form>


</HEAD>
<%
Sub JavaRedirect 
    main="richtemplate_list_pages.aspx?SectionID="&Request.Querystring("SectionID")&""
    'returnfilepath = session("path")
    returnFilePath = Replace(session("path"),"\","/")
   'response.write returnFilePath 
   response.write session("url")
   
   %>
    <SCRIPT language="JavaScript">
    <!--
    //window.opener.top.treeframe.submit();
   //window.opener.top.basefrm.location 	= 'richtemplate_filemgmt.asp';
     	//window.opener.top.treeframe.myform2.action ="/admin/richtemplate_filemgmt.asp?" + <%=server.urlEncode("FileURL")%>;
 		window.opener.top.treeframe.myform2.submit();
   
   
    window.close(); 
   // parent.location.href = 
    //'<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>

<BODY topmargin="0" leftmargin="0">
<!--#INCLUDE FILE="headernew.inc"-->
<br>

<% 
Dim diagnostics
if Request.ServerVariables("REQUEST_METHOD") <> "POST" then
    diagnostics = TestEnvironment()
    if diagnostics<>"" then
        response.write "<div style=""margin-left:20; margin-top:30; margin-right:30; margin-bottom:30;"">"
        response.write diagnostics
        response.write "<p>After you correct this problem, reload the page."
        response.write "</div>"
            call JavaRedirect
    else
        response.write "<div style=""margin-left:10"">"
        OutputForm()
        response.write "</div>"
    end if
else
    response.write "<div style=""margin-left:10"">"
    OutputForm()
    response.write SaveFiles()
    response.write "<br><br></div>"
    call JavaRedirect
end if

%>

<!-- Please support this free script by having a link to freeaspupload.net either in this page or somewhere else in your site. -->
<p><br>

<!--- START OF HTML TO REMOVE - contains the script ratings submission -->

</p>
</BODY>
</HTML>