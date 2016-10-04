<!--#INCLUDE FILE="sessioncheck.asp"-->

<%PNAME = "Preview Web Page"%>
<%HeaderType = "simple"%>
<html>

<head>


<meta http-equiv="Content-Language" content="en-us">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>New Page 1</title>
<base target="main">
<link rel="stylesheet" type="text/css" href="style_richtemplate.css">
</head>

<body topmargin="0" leftmargin="0" bgcolor="#FFFFFF" style="background-attachment: fixed">
<font color="#FFFFFF">

<table width="100%" cellspacing="0" cellpadding="0" id="table1" height="28">
	<tr>
  <td background="../../../admin/images/editcontentbg.gif" width="1%" height="28" valign="bottom">
	&nbsp;</td>
	<td background="../../../admin/images/editcontentbg.gif" class="blueheader" height="28">
	<b><font face="Arial" color="#FFFFFF" size="2"><%=PNAME%></font></b></td>
	<td background="../../../admin/images/editcontentbg.gif" class="blueheader" height="28" align="right">
	<table border="0" width="125" id="table2" cellspacing="0" cellpadding="0">
		<tr>
				<td background="../../../admin/images/editcontentbg.gif" class="blueheader" height="28" width=20>
	<img border="0" src="../../../admin/images/editcontentbg_logout.gif" width="20" height="28"></td>
	<td background="../../../admin/images/editcontentbg.gif" class="blueheader" height="28">
	<font face="Arial" size="2" color="#FFFFFF">&nbsp;<a href="javascript:top.window.close()"><font color="#FFFFFF">Close Preview</font></a></font><font color="#FFFFFF" size="2">&nbsp;&nbsp;</font></font></td>
		</tr>
	</table>

	</tr>
	</tr>
	<tr>
  <td height="28" colspan="3" align="center">
<font color="#FFFFFF" face="Arial" size="2">

	<table border="0" width="400" id="table3">
		<tr>
			<td bgcolor="#FFFFFF" width="25">
			<p align="center"><font color="#3054A9">
			<img border="0" src="images/icon_page_view.gif" width="16" height="16"></font></td>
			<td bgcolor="#FFFFFF">
<font color="#3054A9" face="Arial" size="2">

			<b><a href="richtemplate_page_logic.asp?sectionid=<%=Request.Querystring("sectionid")%>&pageid=<%=Request.querystring("pageid")%>&Task=publish">Publish Pending Page</a></b></font></td>
			<td bgcolor="#FFFFFF" width="25">
			<p align="center"><font color="#3054A9" face="Arial" size="2">

			<b>
			<img border="0" src="images/icon_edit.gif" width="16" height="16">&nbsp;
			</b></font></td>
			<td bgcolor="#FFFFFF">
<font color="#3054A9" face="Arial" size="2">

			<b>
<a target="_top" href="richtemplate_preview.asp?defaultpage=0&pagename=<%=Request.Querystring("pagename")%>&subpagename=<%=Request.Querystring("subpagename")%>&PAGEID=<%=Request.Querystring("pageID")%>&ID=<%=Request.Querystring("PageID")%>&sectionid=<%=REQUEST.QUERYSTRING("sectionid")%>&action=edit&pageAction=Edit&submit=yes">Edit Pending Page</a></b></font></td>
		</tr>
	</table>
	</font></td>
	</tr>

	</font>
</table>

</font>
<div align="center">
<p>&nbsp;</div>
</body>

</html>