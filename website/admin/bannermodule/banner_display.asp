<%@ LANGUAGE="VBSCRIPT"%> 
<%Buffer="True"
'CacheControl "Private"%>
<!--#INCLUDE VIRTUAL="/admin/db_connection.asp"-->

<!--#INCLUDE VIRTUAL="/admin/sessioncheck.asp"-->
<%
PNAME = "Administer Banners"
PHELP = "../helpFiles/pageListing.asp#banner"
%>



<%
	
	'CODE TO CHANGE/SET PAGE RANKING FOR SECONDARY PAGES----------->

	IF REQUEST.QUERYSTRING("posit")= "moveup"  then

		strId = Request.Querystring("pageid")
		strRank = Request.Querystring("rank")
		strNewRank = strRank - 1
	
			If strRank <= 1 then
			
			else
			
			    SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
				CON.OPEN ConnectionString
					
				myUpdateSQL = "Update BANNER_MODULE SET rank = rank + 1 WHERE PAGEID=" & request.querystring("PAGEID") & "  and rank =" & strNewRank 
				con.execute(myUpdateSQL)
			
				myRankSQL = "Update BANNER_MODULE SET rank = " & strNewRank & " WHERE id = "&Request.Querystring("bannerID")&""
			
				con.execute(myRankSQL)
			end if
	end if
			
			
	IF REQUEST.QUERYSTRING("posit") = "movedown" then
	
		strId = Request.Querystring("pageid")
		strRank = Request.Querystring("rank")
		strNewRank = strRank + 1
		
			SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
			CON.OPEN ConnectionString
	
			myRSSQL = "Select rank from BANNER_MODULE WHERE  PAGEID=" & request.querystring("PAGEID") & " and rank > " & strRank
			SET RS = con.execute(myRSSQL)
				IF RS.EOF then
			
			
				ELSE
			
					myUpdateSQL2 = "Update BANNER_MODULE SET rank = rank - 1 WHERE  PAGEID=" & request.querystring("PAGEID") & " and rank =" & strNewRank 
					con.execute(myUpdateSQL2)
			
					myRankSQL2 = "Update BANNER_MODULE SET rank = " & strNewRank & " WHERE id = "&Request.Querystring("bannerID")&""
			
					con.execute(myRankSQL2)
				end if

	end if
	
	
%>


<HTML><HEAD>
<script LANGUAGE="JavaScript">
<!--
function editProfile(page) {
	newwindow = window.open(page,'' , "toolbar=no,location=no,scrollbars=yes,resizable=yes,width=400,height=400");
}
//-->
</script>
<meta http-equiv="Content-Language" content="en-us">
<SCRIPT language=JavaScript src="../deletefunction.js" type=text/javascript></SCRIPT>

<link rel="stylesheet" href="../style_richtemplate.css" type="text/css">
<title>RichTemplate - Be The Master of Your Domain</title>
</HEAD>
<BODY topmargin="0" leftmargin="0">
<!--#INCLUDE VIRTUAL="/admin/headernew.inc"-->
<table border="0" width="780" id="table1" height="35">
	<tr>
		<td width="32"><img src="../images/icon_uploadimage.gif" align="right"></td>
		<td width="213"><font face="Verdana" size="2"><a href="javascript:editProfile('uploadimages.asp?pageid=<%=Request.Querystring("pageid")%>')">Upload Banners</a> for this page</font></td>
		<td width="25">
		<img border="0" src="../images/icon_schedule_rotation.gif" width="24" height="24"></td>
		<td><font face="Verdana" size="2"><a href="banner_rotation.asp?pageid=<%=Request.Querystring("pageid")%>">Schedule rotation</a> 
		for this page</font></td>
	</tr>
</table>

<%
 		
		SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
		CON.OPEN ConnectionString
		SQL2 = "SELECT NAME FROM WEBINFO WHERE ID = "&REQUEST.QUERYSTRING("PAGEID")&""
		'RESPONSE.WRITE SQL2
		Set RS = con.Execute(SQL2)
			
		PNAME = "Banners for "&RS("NAME")&" page"
		



		SQL2 = "SELECT * FROM BANNER_MODULE WHERE PAGEID = "&REQUEST.QUERYSTRING("PAGEID")&" ORDER BY RANK"
		'response.write sql2
		
		Set RS2 = con.Execute(SQL2)
		
		if not RS2.EOF then
		
		If RS2("ROTATION")="1" THEN 
		bannerRotation =7
		ELSEIF RS2("ROTATION")="2" THEN
		bannerRotation =7
		ELSEIF RS2("ROTATION")="3" THEN
		bannerRotation =12
		ELSEIF RS2("ROTATION")="4" THEN
		bannerRotation =12
		ELSEIF RS2("ROTATION")="5" THEN
		bannerRotation =4
		ELSEIF RS2("ROTATION")="6" THEN
		bannerRotation =1
		End if
		
		
		countBanner = 1

	
		do while Not RS2.EOF 
			'RESPONSE.WRITE COUNTBANNER	
			
			
			
			
		If countBanner <= bannerRotation then '<-- this is the number of actively displayed images in rotation
		
			mybgcolor="#eeeeee"

		else 
		
			mybgcolor="#ffffff"
			
		end if
		
		%>
	
	<table cellspacing="0" cellpadding="3" width="780" style="border: 1px solid #C0C0C0">

		<tr>
	<td width="761" align=left bgcolor="<%=mybgcolor%>" colspan="5"><font face="Verdana" size="2">&nbsp;<%If countBanner <= bannerRotation then
	response.write "In Active Rotation"
	else
	response.write "Not In Active Rotation"
	end if%> </font></td></tr>

		<tr>
	<td width="761" align=left bgcolor="<%=mybgcolor%>" colspan="5"><font face="Verdana" size="2"><img src=../../data/images/banners/<%=RS2("BANNER_NAME")%>></font></td></tr>
			<tr>
	<td width="65" align=left bgcolor="<%=mybgcolor%>"><font face="Verdana" size="2">
	
	RO: <%=RS2("rank")%>
	</font></td>
	<td width="77" align=center bgcolor="<%=mybgcolor%>">
	<table border="0" width="100%" cellspacing="1" cellpadding="0" id="table3">
		<tr>
			<td bgcolor="<%=mybgcolor%>">
			<p align="right"><a href="banner_display.asp?bannerID=<%=RS2("ID")%>&pageid=<%=Request.Querystring("pageid")%>&rank=<%=RS2("RANK")%>&posit=moveup"><img border="0" src="../images/uparrow.gif" alt="Move image up in the rotation order"></a> </td>
			<td bgcolor="<%=mybgcolor%>">
	<font face="Verdana" size="2"><a href="banner_display.asp?bannerID=<%=RS2("ID")%>&pageid=<%=Request.Querystring("pageid")%>&rank=<%=RS2("RANK")%>&posit=moveup">Move Up</a></font></td>
		</tr>
	</table>
	</td bgcolor="<%=mybgcolor%>">
	<td width="137" align=center bgcolor="<%=mybgcolor%>">
			<table border="0" width="109%" cellspacing="0" cellpadding="0" id="table4">
				<tr>
					<td width="20" bgcolor="<%=mybgcolor%>">
					<p align="right">
			<a href="banner_display.asp?bannerID=<%=RS2("ID")%>&pageid=<%=Request.Querystring("pageid")%>&rank=<%=RS2("RANK")%>&posit=movedown"><img border="0" src="../images/downarrow.gif" alt="Move image down in the rotation order"></a></td>
					<td bgcolor="<%=mybgcolor%>"><font face="Verdana" size="2">
					<a href="banner_display.asp?bannerID=<%=RS2("ID")%>&pageid=<%=Request.Querystring("pageid")%>&rank=<%=RS2("RANK")%>&posit=movedown">Move Down</a></font></td>
				</tr>
			</table>
	</td>
	<td width="355" align=left bgcolor="<%=mybgcolor%>"><font face="Verdana" size="2">File 
	Name: <%=RS2("BANNER_NAME")%></font></td>
	<td width="113" align=right bgcolor="<%=mybgcolor%>">
	<p align="center"><font face="Verdana" size="2"><a href="javascript:confirmdelete('banner_delete.asp?bannerID=<%=RS2("ID")%>&name=<%=RS2("BANNER_NAME")%>&rank=<%=rs2("rank")%>&pageid=<%=Request.Querystring("pageid")%>')">Remove Image</a></font></td>
		</tr>
	</table>
	
	
	
	<br>


<%

rs2.movenext
countBanner = countBanner +1
		'response.write countBanner
		loop
		

	else
	end if
	rs2.close%>


<p>

	<font face="Verdana" size="2">
	&nbsp;&nbsp; <a href="banner_listing.asp">

	<img border="0" src="../../images/arrow_back.jpg" width="16" height="16"> 
	Back to listing</a></font>

</p>

</body>
</html>