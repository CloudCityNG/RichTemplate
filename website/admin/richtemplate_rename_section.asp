
<%PNAME = "Rename Section"%>
<%HeaderType = "simple"%>

<!--#INCLUDE FILE="sessioncheck.asp"-->



<html>

<head>

    
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

<form method="POST"  name="rename"   action="richtemplate_page_logic.asp?task=editsection&SectionID=<%=Request.Querystring("sectionID")%>">

<!--#INCLUDE FILE="headernew.inc"-->

	<table border="0" width="100%" id="table4" cellspacing="0" cellpadding="0">
		<tr>
			<td height="10"></td>
		</tr>
	</table>
	<div align="center">

	<table border="0" width="400" cellspacing="0" cellpadding="0" id="table2">
		<tr>
			<td>

	<table border="0" width="400" cellspacing="0" cellpadding="0" id="table3" style="border-left-style:solid; border-left-width:0px; border-right-style:solid; border-right-width:0px; border-bottom-style:solid; border-bottom-width:0px">
		<tr>
			<td width="102" class="bodybold">&nbsp;</td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td width="102" class="bodybold">
			<p align="right">Section Name:&nbsp;&nbsp;&nbsp; </td>
			<td><p>
			<input type="text" name="sectionname" size="28" value="<%=Request.Querystring("sectionname")%>">&nbsp; 
			<input type="submit" value="Change" name="B1"></td>
		</tr>
		<tr>
			<td width="102">&nbsp;</td>
			<td>&nbsp;</td>
		</tr>
	</table>
			</td>
		</tr>
	</table>
	</div>
	<p align="center" class="body"><a href="javascript:window.close();">Cancel</a></p>
</form>

</body>

</html>