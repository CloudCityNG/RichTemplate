<%URL = Request.ServerVariables("server_name")%>

<%URL=Replace(URL,"www.","")
'response.write url

%>
<%'URL = "trickydns.com"%>

<html>


<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>New Page 1</title>
</head>

<frameset framespacing="0" border="0" frameborder="0" rows="89,*">
	<frame name="header" scrolling="no" noresize target="main" src="emailaccess_header.asp">
	<frame name="main" src="http://mail.<%=URL%>/Default.aspx" scrolling="auto">
	<noframes>
	<body>

	<p>This page uses frames, but your browser doesn't support them.</p>

	</body>
	</noframes>
</frameset>

</html>