<!--#INCLUDE FILE="../admin/sessioncheck.asp"-->

<%If Request.Querystring("mode")="forms" then
Session("mode")= "forms"
elseif Request.Querystring("mode")="modules" then
Session("mode") = "modules"
elseif Request.Querystring("mode")="images" then
Session("mode") = "images"
elseif Request.Querystring("mode")="users" then
Session("mode") = "users"
elseif Request.Querystring("mode")="microsites" then
Session("mode") = "microsites"
else
Session("mode") = "forms"
end if%>
<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>RichTemplate 2.0</title>
<base target="_self">
</head>

<frameset rows="76,100%" framespacing="1" border="0" frameborder="0">
	<frame name="banner" scrolling="no" noresize target="contents" src="../admin/header.asp">
	<frameset cols="200,100%,*,*,*,*,*">
		<frameset rows="350,350,*,*">
		
<%if request.querystring("mode")="modules" then%>
<frame name="treeframe" target="main" src="demoFramesetLeftFrameModules.asp" scrolling="auto" noresize>		
<%elseif request.querystring("mode") = "micro" then%>
<frame name="treeframe" target="main" src="micrositeNav.asp" scrolling="auto" noresize>
<%else%>
<frame name="treeframe" target="main" src="/admin/richtemplate_list_sections.aspx" scrolling="auto" noresize>
<%end if%>

			<frame name="contents1" src="../menu22/contents.asp" scrolling="no" noresize target="_self">
		</frameset>
<%if request.querystring("mode")="modules" then%>
	<%if Session("ACCESS_LEVEL")>2 THEN%>
		<frame name="basefrm" src="../admin/moduleadmin.asp" scrolling="yes" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">

	<%ELSE%>
		<frame name="basefrm" src="../admin/richtemplate_listmodules.asp" scrolling="auto" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">

	<%END IF%>
	<%ELSEIF REQUEST.QUERYSTRING("mode")="images" then%>
		<frame name="basefrm" src="../admin/imageandfilelisting.asp" scrolling="yes" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">

<%ELSE%>
		<frame name="basefrm" src="../admin/intranet/index.asp" scrolling="yes" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">

<%END IF%>
<%if request.querystring("mode")="admin" then%>
	<%if Session("ACCESS_LEVEL")>2 THEN%>
		<frame name="basefrm" src="../admin/richtemplate_site_admin.asp" scrolling="yes" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">

	<%ELSE%>
		<frame name="basefrm" src="../admin/richtemplate_site_admin.asp" scrolling="auto" style="border-left: 1px solid #3155A8; border-right-width: 1px; border-top-width: 1px; border-bottom-width: 1px; padding-left: 1px">

	<%END IF%>
	</frameset>
	<noframes>
	<body style="border: 0 solid #002D96">

	<p>This page uses frames, but your browser doesn't support them.</p>

	</body>
	</noframes>
</frameset>

</html>