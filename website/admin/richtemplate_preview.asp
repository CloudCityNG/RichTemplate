

<html>


<head>
<%
If request.querystring("submit")="yes" then
        
     main="richtemplate_editor.asp?defaultpage=0&pagename="&Request.Querystring("pagename")&"&subpagename="&Request.Querystring("subpagename")&"&PAGEID="&Request.Querystring("pageID")&"&ID="&Request.Querystring("PageID")&"&sectionid="&REQUEST.QUERYSTRING("sectionid")&"&action=edit&pageAction=Edit&pub=yes"
    
    %>
    <SCRIPT language="JavaScript">
    <!--
    
    opener.location 	= '<%=main%>';
	self.close();

    //'<%=URL%>';
    //-->
    </SCRIPT><%
end if

%>

<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Preview Your Web Page</title>
</head>

<frameset framespacing="1" border="1" frameborder="1" rows="55,*">
	<frame name="header" scrolling="no" noresize target="main" src="richtemplate_previewheader.asp?pagename=<%=Request.Querystring("pagename")%>&subpagename=<%=Request.Querystring("subpagename")%>&pageid=<%=request.querystring("pageid")%>&page=<%=Request.Querystring("page")%>&sectionid=<%=Request.Querystring("sectionid")%>&pagelevel=<%=Request.Querystring("pagelevel")%>&pending=yes">
	<frame name="main" src="http://<%=Request.ServerVariables ("http_host")%>/web/page/<%=Request.Querystring("page")%>/sectionid/<%=Request.Querystring("Sectionid")%>/pagelevel/<%=Request.Querystring("pagelevel")%>/pending=yes/<%=Request.Querystring("PAGE_LINKNAME")%>" scrolling="auto">
	<noframes>
	<body>

	<p>This page uses frames, but your browser doesn't support them.</p>

	</body>
	</noframes>
</frameset>

</html>