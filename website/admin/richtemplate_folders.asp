
<%PNAME = "Create a New Folder"%>
<%HeaderType = "simple"%>

<!--#INCLUDE FILE="sessioncheck.asp"-->

<%If request.Querystring("task")="newfolder" then

		nameOfFile = request.form("foldername")
        nameOfFile = replace(nameOfFile, "\", "_")
        nameOfFile = replace(nameOfFile, "/", "_")
        nameOfFile = replace(nameOfFile, ":", "_")
        nameOfFile = replace(nameOfFile, "*", "_")
        nameOfFile = replace(nameOfFile, "?", "_")
        nameOfFile = replace(nameOfFile, """", "_")
        nameOfFile = replace(nameOfFile, "<", "_")
        nameOfFile = replace(nameOfFile, ">", "_")
        nameOfFile = replace(nameOfFile, "|", "_")
        nameOfFile = replace(nameOfFile, " ", "_")
        

strPath = Session("filePath")&"/"&NameOfFile
Dim objFSO
Set objFSO = Server.CreateObject("Scripting.FileSystemObject")
Set objFSO= objFSO.CreateFolder(strPath)
set objFSO= Nothing

call JavaRedirect


end if%>

<html>

<head>
<%
Sub JavaRedirect 
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
    
<meta http-equiv="Content-Language" content="en-us">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Section Name</title>
<link rel="stylesheet" type="text/css" href="style_richtemplate.css">

<SCRIPT language="JavaScript">
<!-- Begin
function refreshMain() {
window.opener.top.treeframe.location='/admin/richtemplate_list_sections.aspx';
window.opener.top.basefrm.location='richtemplate_listpages2.asp?SectionID=<%=Request.Querystring("sectionID")%>';

//window.opener.parent.location.href='mainpage.asp';
window.close(); 
}
// End -->
</SCRIPT>

</head>

<body topmargin="0" leftmargin="0">

<form method="POST"  name="rename"   action="richtemplate_folders.asp?task=newfolder&rootPath=<%=session("filePath")%>">

<!--#INCLUDE FILE="headernew.inc"-->

	<table border="0" width="100%" id="table4" cellspacing="0" cellpadding="0">
		<tr>
			<td height="10"></td>
		</tr>
	</table>
	<div align="center">

	<table border="0" width="329" cellspacing="0" cellpadding="0" id="table2">
		<tr>
			<td width="329">

	<table border="0" width="329" cellspacing="0" cellpadding="0" id="table3" style="border-left-style:solid; border-left-width:0px; border-right-style:solid; border-right-width:0px; border-bottom-style:solid; border-bottom-width:0px">
		<tr>
			<td width="113" class="bodybold">&nbsp;</td>
			<td width="216"></td>
		</tr>
		<tr>
			<td width="113" class="bodybold">
			<p align="right">New Folder Name:&nbsp;&nbsp;&nbsp; </td>
			<td width="216"><p>
			<input type="text" name="foldername" size="20" value="<%=Request.Querystring("sectionname")%>">&nbsp; 
			<input type="submit" value="Create" name="B1"></td>
		</tr>
		</table>
			</td>
		</tr>
	</table>
	</div>
	<p align="center" class="body"><a href="javascript:window.close();">Close 
	Window</a></p>
</form>

</body>

</html>