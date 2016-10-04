<%@ LANGUAGE="VBSCRIPT"%> 
<%Buffer="True"
'CacheControl "Private"

ACCESS_LEVEL = SESSION("ACCESS_LEVEL")

%>
<!--#INCLUDE VIRTUAL="/admin/sessioncheck.asp"-->
<!--#INCLUDE VIRTUAL="/admin/db_connection.asp"-->


<HTML><HEAD>
<SCRIPT language=JavaScript src="../deletefunction.js" type=text/javascript></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
<!-- Begin
function popUpp(URL) {
day = new Date();
id = day.getTime();
eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=yes,location=0,statusbar=0,menubar=0,resizable=0,width=420,height=165,left = 100,top = 100');");
}
// End -->
</script>

<script language="JavaScript">
	function submitIt(form)
	{
		form.submit();
	}
</script>


<link rel="stylesheet" href="../style_richtemplate.css" type="text/css">
<title>RichTemplate - Be The Master of Your Domain</title>
</HEAD>
<BODY topmargin="0" leftmargin="0">
<%
PNAME = "Administer Banners"
PHELP = "../helpFiles/pageListing.asp#banner"
%>

<!--#INCLUDE VIRTUAL="/admin/headernew.inc"-->
<%		
		SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
		CON.OPEN ConnectionString

		SQL2 = "SELECT * FROM WEBINFO WHERE DEFAULTPAGE = 1 ORDER BY DEFAULTPAGE DESC, RANK"
		Set RS22 = con.Execute(SQL2)
			

		do while Not RS22.EOF 
		%>

<%

'OPEN DATA---------------------------------------------------->

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString



'CHECK FOR SUCCESSFUL WEB SECTION ADDITION ------------------->

	IF REQUEST.QUERYSTRING ("success")<>"" THEN

		'RESPONSE.WRITE "<P CLASS=bodybolderror>Your Web Page Deletion Was Successful, If you Wish Edit Another Below.</P>"
	
	END IF



'CODE TO CHANGE/SET PAGE RANKING FOR SECONDARY PAGES----------->

	IF REQUEST.QUERYSTRING("posit")= "moveup"  then

		strId = Request.Querystring("pageid")
		strRank = Request.Querystring("rank")
		strNewRank = strRank - 1
	
			If strRank <= 1 then
			
			else
					
				myUpdateSQL = "Update WEBINFO SET rank = rank + 1 WHERE sectionid=" & request.querystring("SECTIONID") & " AND PARENTID ="&REQUEST.QUERYSTRING("PARENTID")&"  and defaultpage<>-1 and rank =" & strNewRank 
				con.execute(myUpdateSQL)
			
				myRankSQL = "Update WEBINFO SET rank = " & strNewRank & " WHERE id = " & strId
			
				con.execute(myRankSQL)
			end if
	end if
			
			
	IF REQUEST.QUERYSTRING("posit") = "movedown" then
		strId = Request.Querystring("pageid")
		strRank = Request.Querystring("rank")
		strNewRank = strRank + 1
	
			myRSSQL = "Select rank from WEBINFO WHERE  sectionid=" & request.querystring("SECTIONID") & " AND PARENTID ="&REQUEST.QUERYSTRING("PARENTID")&"  and defaultpage<>-1 and rank > " & strRank
			SET RS = con.execute(myRSSQL)
				IF RS.EOF then
			
			
				ELSE
			
					myUpdateSQL2 = "Update WEBINFO SET rank = rank - 1 WHERE  sectionid=" & request.querystring("SECTIONID") & "AND PARENTID ="&REQUEST.QUERYSTRING("PARENTID")&" and defaultpage<>-1 and rank =" & strNewRank 
					con.execute(myUpdateSQL2)
			
					myRankSQL2 = "Update WEBINFO SET rank = " & strNewRank & " WHERE id = " & strId
			
					con.execute(myRankSQL2)
				end if

	end if







WEBSECTIONSQL = "SELECT  * FROM WEBINFO WHERE ID="&rs22("sectionid")&" AND PAGELEVEL = 1 ORDER BY DEFAULTPAGE DESC, RANK"
SET RS = CON.EXECUTE (WEBSECTIONSQL)



	IF NOT RS.EOF THEN
		IF RS("PAGELEVEL")=1 THEN	
			SESSION("SECTIONNAME") = RS("NAME")
		END IF
%>



		

<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <td valign="top" align="center" width="100%">



<table border="0" width="100%" cellspacing="0" cellpadding="0" >
	<tr>
		<td><table border="0" cellspacing="0" cellpadding="0" id="table1">
	<tr>
		<td class="bodyboldsection" height="28" width="1020">
		<font color="#2857AA"><b>&nbsp;Section: <%=Session("sectionname")%></b></font></td>
		
		<%'*********************** CHECK RENAME SECTION PERMISSION FOR USER *********************** 
			IF SESSION("ALLOW_RENAMESECTION")= TRUE  THEN%>	

		<%end if%>
		
		<%
		'*********************** CHECK PACKAGE TYPE START *********************** 
		SQL2 = "SELECT * FROM PACKAGE_TYPE WHERE PACKAGE_SELECTED =1"
		Set RS2 = con.Execute(SQL2)
		IF RS2("ADMIN_SECTIONS")<>FALSE THEN%>
		
		
		<%'*********************** CHECK DELETE SECTION PERMISSION FOR USER *********************** 
		IF SESSION("ALLOW_SECTIONDELETE")= TRUE THEN%>	
		<%END IF
		END IF
		'*********************** CHECK DELETE SECTION PERMISSION FOR USER END *********************** 
		%>	
		</tr>
</table>
</td>
	</tr>
</table>

<div align="center">

<table border="0" width="100%" cellspacing="0" cellpadding="0">
	<tr>
		<td>
<table border="0" cellspacing="0" cellpadding="5"  class="sstable2" style="border-color: #D2E3FC; border-width: 0; ; background-color:#FFFFFF" width="100%">
<tr>

<%
		'DISPLAY RECORDS--------------------------------------------->
			WHILE NOT RS.EOF

			strRnk = RS("rank")
%>
    <tr>
    <%

    IF RS("PENDING")=TRUE THEN
    BGCOLOR = "#E3EEFF"
    ELSE 
    BGCOLOR = "#FFFFFF"
    End if%>
	
    <%IF RS("DEFAULTPAGE")= TRUE THEN
    	defaultpage=-1%>
    	<td width="60" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
		<img border="0" src="../images/icon_root_page.gif" width="16" height="16"></td>
      	<td width="228"bgcolor ="<%=bgcolor%>" class=bodybold style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; " align="left"><font color="#FF0000">Default (<%=RS("NAME")%>)</font></td>
         <td width="83" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="banner_display.asp?pageid=<%=rs("id")%>">View Banners</a></td>

		<%
						
			myRSSQL = "Select * from BANNER_MODULE WHERE  PAGEID=" & RS("ID")& ""
			SET RS55 = con.execute(myRSSQL)
	
			If Not RS55.EOF then
			bannerNotify = "Banners have been loaded"
			else
			bannerNotify = ""
			end if%>
         <td width="463" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<%response.write ""&bannerNotify&"" %> &nbsp; </td>
		

<%ELSE%>
            <%END IF%>         
		   

      <%IF RS("DEFAULTPAGE")<>1 THEN%>
      	<%
		'*********************** CHECK PACKAGE TYPE START *********************** 
		SQL2 = "SELECT * FROM PACKAGE_TYPE WHERE PACKAGE_SELECTED = 1"
		Set RS2 = con.Execute(SQL2)
		IF RS2("PACKAGEID")<> "1" THEN%>
		
      <%ELSE%>
		      
		
		
    

		
<%END IF%>
		
		
		
			     <%
			     	'SECOND LEVEL PAGES --------------------------->
			     	
			     	PAGELEVEL = 3
			     	
    				pagesql = "SELECT  * FROM WEBINFO WHERE PARENTID="&RS("ID")&"  ORDER BY  RANK"
					'response.write websectionsql
					SET RS2 = CON.EXECUTE (pagesql)
					
					WHILE NOT RS2.EOF
					
					    
    					IF RS2("PENDING")= TRUE THEN
    					
    					BGCOLOR = "#E3EEFF"
   						ELSE 
   				 		BGCOLOR = "#FFFFFF"
    					End if
    					
    					
    					strRnk = RS2("rank")
					%>
    					
			
					<TR><td width='60' bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;'>
						&nbsp;&nbsp;&nbsp;<img border='0' src='../images/icon_subpage.gif' width='16' height='16'></td>
					<td width='228' class=bodybold bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;' align="left"><font color='#3055A9' >
					&nbsp;&nbsp;&nbsp;<%=RS2("NAME")%></FONT></td>

					<td width="83" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="banner_display.asp?pageid=<%=rs2("id")%>">View Banners</a></td>
		<%
						
			myRSSQL6 = "Select * from BANNER_MODULE WHERE  PAGEID=" & RS2("ID")& ""
			SET RS56 = con.execute(myRSSQL6)
	
			If Not RS56.EOF then
			bannerNotify2 = "Banners have been loaded"
			else
			bannerNotify2 = ""
			end if%>
					<td width="463" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<%response.write ""&bannerNotify2&"" %>&nbsp;</td>

	<%'*********************** CHECK DELETE SECTION PERMISSION FOR USER *********************** 
	IF SESSION("ALLOW_PAGEDELETE")= TRUE THEN%>	
					
					
	<%else%>
	<%end if%>
		
		
		<%If RS2("PENDING")=TRUE THEN
		
				IF SESSION("ALLOW_PUBLISH")= TRUE THEN%>	
		
					<%else%>
				
					<%end if%>
			
		<%ELSE%>
		
					<%END IF%>
		
				
			     <%
			     	'THIRD LEVEL PAGES --------------------------->
			     	
			     	PAGELEVEL = 4
			     	
    				pagesql = "SELECT  * FROM WEBINFO WHERE PARENTID="&RS2("ID")&" ORDER BY  RANK"
					'response.write websectionsql
					SET RS3 = CON.EXECUTE (pagesql)
					
					WHILE NOT RS3.EOF
					
					    
    					'ASSIGN BACKGROUND COLOR DEPENDENT ON PENDING STATUS
    					
    					IF RS3("PENDING")= TRUE THEN
    					
    						BGCOLOR = "#E3EEFF"
    						
   						ELSE 
   						
   				 			BGCOLOR = "#FFFFFF"
   				 			
    					END IF
    					
    					
    					strRnk = RS3("rank")
					%>
    					
			
					<TR><td width='60' bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;'>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img border='0' src='../images/icon_subpage.gif' width='16' height='16'></td>
					<td width='228' class=bodybold bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;' align="left"><font color='#3055A9' >
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=RS3("NAME")%></FONT></td>

					<td width="83" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="banner_display.asp?pageid=<%=rs3("id")%>">View Banners</a></td>

		<%
						
			myRSSQL7 = "Select * from BANNER_MODULE WHERE  PAGEID=" & RS3("ID")& ""
			SET RS57 = con.execute(myRSSQL7)
	
			If Not RS57.EOF then
			bannerNotify3 = "Banners have been loaded"
			else
			bannerNotify3 = ""
			end if%>
					<td width="463" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<%response.write ""&bannerNotify3&"" %>&nbsp;</td>

	<%'*********************** CHECK DELETE SECTION PERMISSION FOR USER *********************** 
	IF SESSION("ALLOW_PAGEDELETE")= TRUE THEN%>	
					
					
	<%else%>
	<%end if%>
		
		
		<%If RS3("PENDING")=TRUE THEN
		
				IF SESSION("ALLOW_PUBLISH")= TRUE THEN%>	
		
					<%else%>
				
					<%end if%>
			
		<%ELSE%>
		
					<%END IF%>

	
			     <%
			     	'FOURTH LEVEL PAGES --------------------------->
			     	
			     	PAGELEVEL = 5
			     	
    				pagesql = "SELECT  * FROM WEBINFO WHERE PARENTID="&RS3("ID")&" ORDER BY  RANK"
					'response.write websectionsql
					SET RS4 = CON.EXECUTE (pagesql)
					
					WHILE NOT RS4.EOF
					
					    
    					'ASSIGN BACKGROUND COLOR DEPENDENT ON PENDING STATUS
    					
    					IF RS4("PENDING")= TRUE THEN
    					
    						BGCOLOR = "#E3EEFF"
    						
   						ELSE 
   						
   				 			BGCOLOR = "#FFFFFF"
   				 			
    					END IF
    					
    					
    					strRnk = RS4("rank")
					%>
    					
			
					<TR><td width='60' bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;'>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img border='0' src='../images/icon_subpage.gif' width='16' height='16'></td>
					<td width='228' class=bodybold bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;' align="left"><font color='#3055A9' >
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=RS4("NAME")%></FONT></td>

					<td width="83" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="banner_display.asp?pageid=<%=rs4("id")%>">View Banners</a></td>

		<%
						
			myRSSQL8 = "Select * from BANNER_MODULE WHERE  PAGEID=" & RS4("ID")& ""
			SET RS58 = con.execute(myRSSQL8)
	
			If Not RS58.EOF then
			bannerNotify4 = "Banners have been loaded"
			else
			bannerNotify4 = ""
			end if%>
					<td width="463" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<%response.write ""&bannerNotify4&"" %>&nbsp;</td>

	<%'*********************** CHECK DELETE SECTION PERMISSION FOR USER *********************** 
	IF SESSION("ALLOW_PAGEDELETE")= TRUE THEN%>	
					
					
	<%else%>
	<%end if%>
		
		
		<%If RS4("PENDING")=TRUE THEN
		
				IF SESSION("ALLOW_PUBLISH")= TRUE THEN%>	
		
					<%else%>
				
					<%end if%>
			
		<%ELSE%>
		
					<%END IF%>
			<%

			     
			     	'FIFTH LEVEL PAGES --------------------------->
			     	
			     	PAGELEVEL = 6
			     	
    				pagesql = "SELECT  * FROM WEBINFO WHERE PARENTID="&RS4("ID")&" ORDER BY  RANK"
					'response.write websectionsql
					SET RS5 = CON.EXECUTE (pagesql)
					
					WHILE NOT RS5.EOF
					
					    
    					'ASSIGN BACKGROUND COLOR DEPENDENT ON PENDING STATUS
    					
    					IF RS5("PENDING")= TRUE THEN
    					
    						BGCOLOR = "#E3EEFF"
    						
   						ELSE 
   						
   				 			BGCOLOR = "#FFFFFF"
   				 			
    					END IF
    					
    					
    					strRnk = RS5("rank")
					%>
    					
			
					<TR><td width='60' bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;'>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img border='0' src='../images/icon_subpage.gif' width='16' height='16'></td>
					<td width='228' class=bodybold bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;' align="left"><font color='#3055A9' >
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=RS5("NAME")%></FONT></td>

					<td width="83" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="banner_display.asp?pageid=<%=rs5("id")%>">View Banners</a></td>
		<%
						
			myRSSQL9 = "Select * from BANNER_MODULE WHERE  PAGEID=" & RS5("ID")& ""
			SET RS59 = con.execute(myRSSQL9)
	
			If Not RS59.EOF then
			bannerNotify5 = "Banners have been loaded"
			else
			bannerNotify5 = ""
			end if%>
					<td width="463" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<%response.write ""&bannerNotify5&"" %>&nbsp;</td>

	<%'*********************** CHECK DELETE SECTION PERMISSION FOR USER *********************** 
	IF SESSION("ALLOW_PAGEDELETE")= TRUE THEN%>	
					
					
	<%else%>
	<%end if%>
		
		
		<%If RS5("PENDING")=TRUE THEN
		
				IF SESSION("ALLOW_PUBLISH")= TRUE THEN%>	
		
					<%else%>
				
					<%end if%>
			
		<%ELSE%>
		
					<%END IF%>
			<%

' 	<--------------------------- SIXTH LEVEL PAGES --------------------------->
'	    < TO MAKE ANOTHER LEVEL COPY FROM HERE TO WHERE IT SAYS STOP BELOW >

			     	
    				pagesql = "SELECT  * FROM WEBINFO WHERE PARENTID="&RS5("ID")&" ORDER BY  RANK"
					SET RS6 = CON.EXECUTE (pagesql)
					
					WHILE NOT RS6.EOF
					
					    
    					'ASSIGN BACKGROUND COLOR DEPENDENT ON PENDING STATUS
    					
    					IF RS6("PENDING")= TRUE THEN
    					
    						BGCOLOR = "#E3EEFF"
    						
   						ELSE 
   						
   				 			BGCOLOR = "#FFFFFF"
   				 			
    					END IF
    					
    					
    					strRnk = RS6("rank")
					%>
    					
			
					<TR><td width='60' bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;'>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img border='0' src='../images/icon_subpage.gif' width='16' height='16'></td>
					<td width='228' class=bodybold bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;' align="left"><font color='#3055A9' >
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=RS6("NAME")%></FONT></td>

					<td width="83" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="banner_display.asp?pageid=<%=rs6("id")%>">View Banners</a></td>
		<%
						
			myRSSQL10 = "Select * from BANNER_MODULE WHERE  PAGEID=" & RS6("ID")& ""
			SET RS510 = con.execute(myRSSQL9)
	
			If Not RS510.EOF then
			bannerNotify6 = "Banners have been loaded"
			else
			bannerNotify6 = ""
			end if%>
					<td width="463" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<%response.write ""&bannerNotify6&"" %>&nbsp;</td>

	<%'*********************** CHECK DELETE SECTION PERMISSION FOR USER *********************** 
	IF SESSION("ALLOW_PAGEDELETE")= TRUE THEN%>	
					
					
	<%else%>
	<%end if%>
		
		
		<%If RS6("PENDING")=TRUE THEN
		
				IF SESSION("ALLOW_PUBLISH")= TRUE THEN%>	
		
					<%else%>
				
					<%end if%>
			
		<%ELSE%>
		
					<%END IF%>
			<%

			
					
					
					RS6.MOVENEXT
					WEND
					
' 	<--------------------------- SIXTH LEVEL PAGES --------------------------->
'	                   < END OF COPY SECTION FOR NEW LEVEL >				
			
			%>

    </tr>					
					<%
					RS5.MOVENEXT
					WEND
			%>

    </tr>
					<%
					
					RS4.MOVENEXT
					WEND
			%>

    </tr>
					<%
					
					RS3.MOVENEXT
					WEND
			%>

    </tr>
		
	
		
			<%

			
					RS2.MOVENEXT
					WEND
			%>

    <%END IF%>
    </tr>
<%
		

							

			RS.MOVENEXT
			WEND%>
</TABLE>
</td>
	</tr>
</table></div>
<div align="center">
<table border="0" cellspacing="0" cellpadding="5" width="100%">
    <tr>
      
     <!-- <td width="31" class=body>
		<p align="right">
		<a href="richtemplate_editor.asp?id=-1&sectionid=<%=REQUEST.QUERYSTRING("sectionid")%>">
		<img border="0" src="images/icon_add_newpage.gif" width="24" height="24"></A></td>
      
      <td width="729" class=body>
		<a href="richtemplate_editor.asp?id=-1&sectionid=<%=REQUEST.QUERYSTRING("sectionid")%>&pageAction=Add">Add 
		Web Page in the <%=SESSION("SECTIONNAME")%> Website Section</A></td>-->
      
    </tr>
</table>




</div>




<%

	ELSE
%>
<%
	END IF
	
	RS.CLOSE
	SET WEBSECTIONSQL = NOTHING
%>

</td></table>

		<%		
		rs22.movenext
		loop
		%>

</body>
</html>