<!--#INCLUDE FILE="sessioncheck.asp"-->
<%
If session("platform") = 1 then 
		version =  "MSA "&Session("revision")&""
		elseif Session("platform") = 2 then
		version = "MSSQL "&Session("revision")&"" 
		end if

PNAME = "RichTemplate 2.0 Version: "&version&""

		%> 
<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>New Page 1</title>

</head>

<body topmargin="0" leftmargin="0">
<p>
<%If Request.Querystring("mode") = "forms" then%>
<%PNAME = "Welcome - Website Content"%>

<%elseif Request.Querystring("mode") = "members" then%>
<%PNAME = "Welcome - Website Member Content"%>

<%elseif Request.Querystring("mode") = "education" then%>
<%PNAME = "Welcome - Website Education Content"%>

<%elseif Request.Querystring("mode") = "modules" then%>
<%PNAME = "Welcome - Website Modules"%>

<%elseif Request.Querystring("mode") = "images" then%>
<%PNAME = "Welcome - Images & Documents"%>

<%elseif Request.Querystring("mode") = "administration" then%>
<%PNAME = "Welcome - Website Administration"%>

<%elseif Request.Querystring("mode") = "style" then
response.redirect ("richtemplate_stylesheet.asp")%>
<%PNAME = "Welcome - Stylesheet Administration"%>

<%end if%>


<!--#INCLUDE VIRTUAL="/admin/headernew.inc"-->
<object classid="clsid:D27CDB6E-AE6D-11CF-96B8-444553540000" id="obj2" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0" border="0" width="410" height="198">
	<param name="movie" value="images/edit_modules.swf">
	<param name="quality" value="High">
	<embed src="images/edit_modules.swf" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" name="obj2" width="410" height="198"></object>
      <script type="text/javascript" src="/scripts/ieupdate.js"></script>
</p>

</body>

</html>