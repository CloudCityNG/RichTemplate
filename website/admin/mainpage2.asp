<!--#INCLUDE VIRTUAL="/admin/sessioncheck.asp"-->


<%If Request.Querystring("mode")="forms" then
MODE = "forms"
Session("mode")= "forms"
elseif Request.Querystring("mode")="modules" then
MODE = "modules"
Session("mode") = "modules"
elseif Request.Querystring("mode")="images" then
MODE = 	"images"
Session("mode") = "images"
elseif Request.Querystring("mode")="users" then
MODE = "users"
Session("mode") = "users"
elseif Request.Querystring("mode")="micro" then
MODE = "micro"
Session("mode") = "micro"
elseif Request.Querystring("mode")="admin" then
MODE = 	"admin"
Session("mode") = "admin"
else
MODE = "forms"
Session("mode") = "forms"
end if%>
<html>

<head>


<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>RichTemplate 2.0 Version <%If session("platform") = 1 then 
		Response.Write "MSA "&Session("revision")&""
		elseif Session("platform") = 2 then
		Response.Write "MSSQL "&Session("revision")&"" 
		end if
		%></title>






<base target="_self">
</head>

<frameset rows="50,100%" framespacing="1" border="0" frameborder="0">
	<frame name="banner" scrolling="no" noresize target="contents" src="header.asp">
	<frameset cols="200,100%,*,*,*,*,*,*">
		<frameset rows="350,350,*,*,*">
		
<%if MODE = "modules" or MODE ="admin" OR SESSION("ALLOW_WEBCONTENT") <> TRUE then%>
<frame name="treeframe" target="main" src="demoFramesetLeftFrameModules.asp" scrolling="auto" noresize>		
<%elseif MODE = "micro" then%>
<frame name="treeframe" target="main" src="micrositeNav.asp" scrolling="auto" noresize>
<%elseif MODE = "images" then%>
<frame name="treeframe" target="main" src="/ig41sub/include/menunew.asp" scrolling="auto" noresize>
<%else%>
<frame name="treeframe" target="main" src="/admin/richtemplate_list_sections.aspx" scrolling="auto" noresize>
<%end if%>

			<frame name="contents1" src="richtemplate_list_contents.aspx" scrolling="no" noresize target="_self">
		</frameset>
		
<!-- LIST MODULES -->	
		

<%IF MODE ="modules" then%>

		<frame name="basefrm" src="richtemplate_welcome.asp?mode=modules" scrolling="yes" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">


	
<!-- LIST MICRO SITE PAGES -->	

<%ELSEIF MODE="micro" then%>
	<%if Session("ACCESS_LEVEL")>2 THEN%>
		<frame name="basefrm" src="microSites/richtemplate_micromain.asp" scrolling="yes" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">

	<%ELSE%>
		<frame name="basefrm" src="microSites/richtemplate_micromain.asp" scrolling="auto" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">

	<%END IF%>
	
<%ELSEIF MODE="images" then%>
		<frame name="basefrm" src="richtemplate_welcome.asp?mode=images" scrolling="yes" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">


<%elseif MODE="admin" then%>
	<%if Session("ACCESS_LEVEL")>3 THEN%>
		<frame name="basefrm" src="richtemplate_site_admin.asp" scrolling="yes" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">
	<%end if%>

<%ELSEIF SESSION("ALLOW_WEBCONTENT") = TRUE THEN%>
		<frame name="basefrm" src="richtemplate_welcome.asp?mode=forms" scrolling="yes" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">
<%ELSE%>
		<frame name="basefrm" src="richtemplate_welcome.asp?mode=modules" scrolling="yes" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">
<%RESPONSE.WRITE SESSION("ALLOW_WEBCONTENT")%>

<%END IF%>
	</frameset>
	<noframes>
	<body style="border: 0 solid #002D96">

	<p>This page uses frames, but your browser doesn't support them.</p>

	</body>
	</noframes>


</html>