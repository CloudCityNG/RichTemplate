<%@ LANGUAGE="VBSCRIPT"%> 
<!--#INCLUDE FILE="sessioncheck.asp"-->

<!--#include file="db_connection.asp"-->
<!--#INCLUDE VIRTUAL ="/admin/tagsmodule/tagConfig.inc"-->	


<%Buffer="True"
'CacheControl "Private"

'IDENTIFY WHAT SECTION WE ARE IN BY THE SESSION.  WE'LL POPULATE SQL STATEMENTS WITH WHERE CLAUSES....

IF Session("secure_members") Then

	myWhereClause=" and Secure_Members=1 "

ElseIf Session("secure_education") Then

	myWhereClause=" and Secure_Education=1 "

Else

	myWhereClause=""

End IF




ACCESS_LEVEL = SESSION("ACCESS_LEVEL")

'***************************
'DEFINE NUMBER OF SUBLEVELS

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

MYSQL10 = "SELECT * FROM CONFIG"
SET RS3 = CON.EXECUTE(MYSQL10)
	IF RS3.EOF THEN
	
		INSERTSQL2 = "INSERT INTO CONFIG (SITE_DEPTH, INDEX_PAGE) VALUES (2, 1)"
		CON.EXECUTE(INSERTSQL2)
		
		SUBLEVEL = 2
		
	ELSE
		
		SUBLEVEL = rs3("SITE_DEPTH")
		
	END IF
	
'***************************

%>




<HTML><HEAD>
<SCRIPT language=JavaScript src="deletefunction.js" type=text/javascript></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
<!-- Begin
function popUpp(URL) {
day = new Date();
id = day.getTime();
eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=no,location=0,statusbar=0,menubar=0,resizable=0,width=500,height=500,left = 100,top = 100');");
}
// End -->
</script>
<SCRIPT LANGUAGE="JavaScript">
<!-- Begin
function popUpp2(URL) {
day = new Date();
id = day.getTime();
eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=yes,location=0,statusbar=0,menubar=0,resizable=0,width=500,height=500,left = 100,top = 100');");
}
// End -->
</script>
<script language="JavaScript">
	function submitIt(form)
	{
		form.submit();
	}
</script>


<link rel="stylesheet" href="styles.css" type="text/css">
<title>RichTemplate - Be The Master of Your Domain</title>
<style type="text/css">
.style1 {
	font-family: verdana, arial, helvetica, sans-serif;
	font-size: 11px;
	color: #000000;
	font-weight: normal;
	TEXT-DECORATION: none;
	text-align: center;
}
</style>
</HEAD>
<BODY topmargin="0" leftmargin="0"><%





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
					
				myUpdateSQL = "Update WEBINFO SET rank = rank + 1 WHERE sectionid=" & request.querystring("SECTIONID") & " AND PARENTID ="&REQUEST.QUERYSTRING("PARENTID")&"  and defaultpage<>-1 and rank =" & strNewRank & "" & myWhereClause&""
				con.execute(myUpdateSQL)
			
				myRankSQL = "Update WEBINFO SET rank = " & strNewRank & " WHERE id = " & strId
			
				con.execute(myRankSQL)
			end if
	end if
			
			
	IF REQUEST.QUERYSTRING("posit") = "movedown" then
		strId = Request.Querystring("pageid")
		strRank = Request.Querystring("rank")
		strNewRank = strRank + 1
	
			myRSSQL = "Select rank from WEBINFO WHERE  sectionid=" & request.querystring("SECTIONID") & " AND PARENTID ="&REQUEST.QUERYSTRING("PARENTID")&"  and defaultpage<>-1 and rank > " & strRank & "" & myWhereClause&""
			SET RS = con.execute(myRSSQL)
				IF RS.EOF then
			
			
				ELSE
			
					myUpdateSQL2 = "Update WEBINFO SET rank = rank - 1 WHERE  sectionid=" & request.querystring("SECTIONID") & "AND PARENTID ="&REQUEST.QUERYSTRING("PARENTID")&" and defaultpage<>-1 and rank =" & strNewRank  & "" & myWhereClause&"" 
					con.execute(myUpdateSQL2)
			
					myRankSQL2 = "Update WEBINFO SET rank = " & strNewRank & " WHERE id = " & strId
			
					con.execute(myRankSQL2)
				end if

	end if







WEBSECTIONSQL = "SELECT  * FROM WEBINFO WHERE ID="&REQUEST.QUERYSTRING("SECTIONID")&" AND PAGELEVEL = 1  " & myWhereClause & "  ORDER BY DEFAULTPAGE DESC, RANK"
'response.write websectionsql
SET RS = CON.EXECUTE (WEBSECTIONSQL)




	IF NOT RS.EOF THEN
	PENDING = RS("PENDING")
	pageID = RS("id")
		IF RS("PAGELEVEL")=1 THEN	
			SESSION("SECTIONNAME") = RS("NAME")
		END IF
%>
<%
PNAME = "Web Page Listing For "&Session("sectionname")&" Section"

PHELP = "helpFiles/pageListing.asp#listpages"
%>


<!--#INCLUDE FILE="headernew.inc"-->

<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <td valign="top" align="center" width="100%">



<table border="0" width="100%" cellspacing="0" cellpadding="0" background="images/fadeback3.gif">
	<tr>
		<td><table border="0" cellspacing="0" cellpadding="0" id="table1">
	<tr>
		<td class="bodybold" height="28">
		<font color="#FFFFFF"><b>&nbsp;Section: <%=Session("sectionname")%></b></font>
		<font color="#FFFFFF">
		<%If Session("ACCESS_LEVEL") > 2 then%> - </font>
		<a href="/admin/richtemplate_page_logic.asp?sectionID=<%=Request.Querystring("sectionID")%>&task=checkinall">
		<font color="#FFFFFF">CHECK IN ALL PAGES IN THIS SECTION</font></a> 
		<%END IF%>
		
		</td>
		
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
<table border="0" cellspacing="0" cellpadding="5"  class="sstable2" style="border-color: #D2E3FC; border-width: 1px" width="100%">
<tr>

<%
		'DISPLAY RECORDS--------------------------------------------->
			WHILE NOT RS.EOF

			strRnk = RS("rank")
%>
    <tr>
    	<td  bgcolor="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; " width="50%">
		Web Page Name</td>
            <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; " valign="top">
        <strong>Archive</strong></td>

<%If tagModActive = True then%>
            <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; " valign="top">
        <strong>Search<br>Tags</strong></td>
<%end if%>

            <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; " valign="top">
			<b>Move Up</b></td>
            <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; " valign="top">
			<b>Move Down</b></td>
    

            <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; " valign="top">
        <b>Live</b></td>


      <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; " valign="top">

      <b>Draft</b></td>

      <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; " valign="top">

      	
<b>Checked IN/OUT</b></td>


		
      <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; " valign="top">

      	
<b>Checked Author</b></td>


		
      <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; " valign="top">

      	
<b>Add sub-page</b></td>


		
	<td class=style1 valign="top" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
		<b>Delete</b>&nbsp;</td>

            		
		
		<tr>

	
    <%IF RS("DEFAULTPAGE")= TRUE THEN
    	defaultpage=-1%>
    	<td  bgcolor="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; " width="50%"><img border="0" src="images/icon_root_page.gif" width="16" height="16"><font color="#FF0000"> Root (<%=RS("NAME")%>
    	<%If RS("pending") = true then
					Response.write" - (Offline)"
					END IF%>)</font></td>

    <%ELSE
      	defaultpage=0
      %>


 <%END IF%>
 
<%' This code takes the page offline%> 
<td width="50" class=style1 bgcolor ="<%=bgcolor%>" valign="middle" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">       
 <%If rs("pending") = true then%>
<!--<a href="richtemplate_page_logic.asp?pageid=<%=Rs("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&task=online">Make Live</a>-->
<%Else%>
<!--<a href="richtemplate_page_logic.asp?pageid=<%=Rs("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&task=offline">Take Offline</a>
-->
<%End If%>&nbsp;

 
</td>
<%If tagModActive = True then%>
            <td width="50" class=style1 bgcolor ="<%=bgcolor%>" valign="middle" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">       
<%
getTags = "Select searchTagXrefID from ss_modules_searchtags_xref where recordID = "&RS("ID")&" and recordSetID=0"
set tagRS = con.execute(getTags)
If tagRS.EOF then%>
<a href="/admin/modules/searchtags/legacy/?recordID=<%=RS("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&recordSetID=0">Add</a>
<%Else%>
<a href="/admin/modules/searchtags/legacy/?recordID=<%=RS("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&recordSetID=0">Edit</a>
<%End If%>
</td>
<%end if%>

            <td width="20" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			&nbsp;</td>
            <td width="20" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			&nbsp;</td>
    

            <td width="50" class=style1 bgcolor ="<%=bgcolor%>" valign="middle" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">       

<%IF RS("LINKONLY")<> TRUE and RS("message") <> "" and SESSION("ALLOW_PUBLISH") = TRUE THEN%>
	 	<%If RS("Checked_out") = True then%>
			<font color="#808080">Edit</font>
	<%else%>
			<a title="Edit Live Content" href="/editor2/complete/?pageID=<%=RS("ID")%>&pageAction=Edit">Edit</a>
	<%end if%>		
<%ELSE%>
	<font color="#808080">Edit</font>
<%END IF%>
			</td>


      <%IF RS("DEFAULTPAGE")<>1 THEN%>
      	<%
		'*********************** CHECK PACKAGE TYPE START *********************** 
		SQL2 = "SELECT * FROM PACKAGE_TYPE WHERE PACKAGE_SELECTED = 1"
		Set RS2 = con.Execute(SQL2)
		IF RS2("PACKAGEID")<> "1" THEN%>
		
      <td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">

<%if rs("message2") <>"" then%>
	<%IF RS("LINKONLY")<> TRUE THEN%>
	 	<%If RS("Checked_out") = True then%>
				<font color="#808080">Edit</font>
		<%else%>
			<a alt="Edit Offline Content" title="Edit Offline Content" href="/editor2/complete/?pageID=<%=Rs("ID")%>&pageAction=Edit&pageStatus=offline">Edit</a>
		<%end if%>
		
	<%ELSE%>
			<font color="#808080">Edit</font>
	<%END IF%>

<%else%>
		&nbsp;
<%end if%></td>



      <td width="60" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
		
		 	<%If RS("Checked_out") = True then%>OUT<%IF SESSION("userNAME") = RS("CHECKED_ID") THEN%><br><a href="richtemplate_page_logic.asp?pageID=<%=pageID%>&sectionID=<%=RS("sectionID")%>&TASK=checkin">CHECK IN</a>
<%END IF%>


	<%else%>
	IN
	<%end if%>
</td>
		


      <td width="80" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
		
	
<%iF RS("checked_out") = True then
	Response.write (RS("checked_ID"))
Else%>
&nbsp;
<%End If%></td>
		


      <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
		
	
<%IF SUBLEVEL < 2 AND RS("LINKONLY")<> TRUE THEN%>
		<font color="#808080">Add</font>
 <%ELSE%>
 		<a href="richtemplate_externallinks.asp?id-1=&PAGEID=<%=RS("id")%>&SECTIONID=<%=REQUEST.QUERYSTRING("SECTIONID")%>&pagename=<%=rs("name")%>&PAGELEVEL=2&action=add&pageAction=Add">
		Add</a>
<%END IF%>
		
		</td>
		
<%ELSE%>
      
		
	
		
    

		
<%END IF%>


	<td width="30" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<%IF SESSION("ALLOW_SECTIONDELETE")= TRUE THEN%>
			<a href="javascript:confirmdelete2('richtemplate_page_logic.asp?task=deletesection&SECTIONID=<%=Request.Querystring("SectionID")%>')">
Delete<%else%>&nbsp;
<%end if%></a>

</td>	

	
		
		
			     <%
			     	'SECOND LEVEL PAGES --------------------------->
			     	
			     	PAGELEVEL = 3
			     	
    				pagesql = "SELECT  * FROM WEBINFO WHERE PARENTID="&RS("ID")&"" & myWhereClause & " ORDER BY  RANK"
					'response.write websectionsql
					SET RS2 = CON.EXECUTE (pagesql)
					
					WHILE NOT RS2.EOF
					
					pageID2 = RS2("id")
					
					    
    					IF RS2("LINKONLY") = TRUE THEN
   				 			BGCOLOR = "#F0F0F0"
    					ELSEIf RS2("pending") = true then
    						bgcolor = "#f7e6e6"
    					Else
   				 			BGCOLOR = "#FFFFFF"
    					END IF
    					
    					
    					strRnk = RS2("rank")
					%>
    					
			
					<TR><td bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;' width="50%">&nbsp;&nbsp;&nbsp;<img border='0' src='images/icon_subpage.gif' width='16' height='16'><font color='#3055A9' >&nbsp;
					<%=RS2("NAME")%><%If RS2("linkonly")= TRUE THEN
					RESPONSE.WRITE" - (Link)"
					elseif RS2("pending") = true then
					Response.write" - (Offline)"
					END IF%>
</FONT></td>



<%' This code takes the page offline%> 
<td width="50" class=style1 bgcolor ="<%=bgcolor%>" valign="middle" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">       
 <%If rs2("pending") = true then%>
<a href="richtemplate_page_logic.asp?pageid=<%=Rs2("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&task=online">Make Live</a>
<%Else%>
<a href="richtemplate_page_logic.asp?pageid=<%=Rs2("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&task=offline">Take Offline</a>

<%End If%>

<%If tagModActive = True then%>
         			
            		<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">



<%
getTags = "Select searchTagXrefID from ss_modules_searchtags_xref where recordID = "&RS2("ID")&" and recordSetID = 0"
set tagRS = con.execute(getTags)
If tagRS.EOF then%>
<a href="/admin/modules/searchtags/legacy/?recordID=<%=RS2("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&recordSetID=0">Add</a>
<%Else%>
<a href="/admin/modules/searchtags/legacy/?recordID=<%=RS2("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&recordSetID=0">Edit</a>
<%End If%>
</td>
<%end if%>					
					<td width="20" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="richtemplate_listpages.asp?rank=<%=strRnk%>&SECTIONID=<%=request.querystring("sectionid")%>&pageid=<%=RS2("id")%>&PARENTID=<%=RS2("PARENTID")%>&posit=moveup"><img border="0" src="images/uparrow.gif" alt="move up"></a></td>

            <td width="20" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="richtemplate_listpages.asp?rank=<%=strRnk%>&sectionid=<%=request.querystring("sectionid")%>&pageid=<%=RS2("id")%>&PARENTID=<%=RS2("PARENTID")%>&posit=movedown">
			<img border="0" src="images/downarrow.gif" alt="move down"></a></td>
			



         			
            		<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF RS2("LINKONLY")<> TRUE and RS2("message") <> "" and SESSION("ALLOW_PUBLISH") = TRUE THEN%>
 		 	<%If RS2("Checked_out") = True then%>
			<font color="#808080">Edit</font>
	<%else%>
			<a title="Edit Live Content" href="/editor2/complete/?pageID=<%=RS2("ID")%>&pageAction=Edit">Edit</a>
	<%end if%>		
<%ELSE%>
	<font color="#808080">Edit</font>
<%END IF%>
					</td>
					
					<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%if rs2("message2") <> "" then%>
	<%IF RS2("LINKONLY")<> TRUE THEN%>
	 	<%If RS2("Checked_out") = True then%>

				<font color="#808080">Edit</font>
		<%else%>
				<a title="Edit Offline Content" href="/editor2/complete/?pageID=<%=RS2("ID")%>&pageAction=Edit&pageStatus=offline">Edit</a>
		<%end if%>	
	<%ELSE%>
			<font color="#808080">Edit</font>
	<%END IF%>
<%else%>
		&nbsp;
<%end if%>
</td>
		
				
		
			      <td width="60" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
	<%If RS2("Checked_out") = True then%>
		 	OUT<%IF SESSION("userNAME") = RS2("CHECKED_ID") THEN%><br><a href="richtemplate_page_logic.asp?pageID=<%=RS2("ID")%>&sectionID=<%=RS2("sectionID")%>&TASK=checkin">CHECK IN</a><%end if%>
	<%else%>
	IN
	<%end if%></td>
					
	
		
			      <td width="80" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%iF RS2("checked_out") = True then
	Response.write (RS2("checked_ID"))
Else%>
&nbsp;
<%End If%></td>
					
	
		
			      <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF SUBLEVEL > 2 AND RS2("LINKONLY")<> TRUE THEN%>
				     <a href="richtemplate_externallinks.asp?id-1=&PAGEID=<%=RS2("id")%>&SECTIONID=<%=REQUEST.QUERYSTRING("SECTIONID")%>&pagename=<%=rs2("name")%>&PAGELEVEL=3&action=add&pageAction=Add">
		Add</a>
<%ELSE%>
		<font color="#808080">Add</font>
<%END IF%>		
		</td>
					
	
    <td width="30" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF SESSION("ALLOW_PAGEDELETE")= TRUE THEN%>
    <a href="javascript:confirmdelete('richtemplate_page_logic.asp?task=delete&pageID=<%=RS2("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("sectionid")%>')" >Delete</a>
<%else%>&nbsp;
<%end if%></td>
		
		

		
				
			     <%
			     	'THIRD LEVEL PAGES --------------------------->
			     	
			     	PAGELEVEL = 4
			     	
    				pagesql = "SELECT  * FROM WEBINFO WHERE PARENTID="&RS2("ID")&"" & myWhereClause &" ORDER BY  RANK"
					'response.write websectionsql
					SET RS3 = CON.EXECUTE (pagesql)
					
					WHILE NOT RS3.EOF
					
					pageID3 = RS3("id")

					
					    
    					'ASSIGN BACKGROUND COLOR DEPENDENT ON PENDING STATUS
    					
  
    					IF RS3("LINKONLY") = TRUE THEN
   				 			BGCOLOR = "#F0F0F0"
    					ELSEIf RS3("pending") = true then
    						bgcolor = "#f7e6e6"
    					Else
   				 			BGCOLOR = "#FFFFFF"
    					END IF

   					
    					
    					strRnk = RS3("rank")
					%>
    					
			
					<TR><td  bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;' width="50%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img border='0' src='images/icon_subpage.gif' width='16' height='16'><font color='#3055A9'>&nbsp;<%=RS3("NAME")%>
<%If RS3("linkonly")= TRUE THEN
					RESPONSE.WRITE" - (Link)"
					elseif RS3("pending") = true then
					Response.write" - (Offline)"
					END IF%>					</FONT></td>



<%		showLive = False
		If RS3("parentID") <> "" then
			Select02 = "Select pending, ID from webinfo where ID = "&rs3("parentID") &"" & myWhereClause &""
			Set RS2Live = con.execute(select02)
			If Not RS2Live.EOF then
				If RS2Live("pending") = True then
					showLive = False
				Else
					showLive = True
				End if
			
			End if
		Else
			showLive = True
		End if
		
%>


<%' This code takes the page offline%> 
<td width="50" class=style1 bgcolor ="<%=bgcolor%>" valign="middle" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">       

<%If showLive = True then%>
	<%If rs3("pending") = true then%>
		<a href="richtemplate_page_logic.asp?pageid=<%=Rs3("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&task=online">Make Live</a>
	<%Else%>
		<a href="richtemplate_page_logic.asp?pageid=<%=Rs3("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&task=offline">Take Offline</a>
	
	<%End If%>
<%Else%>
<font color="#808080">Make Live</font>	
<%End If%>



 <%If tagModActive = True then%>        		
            		<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">


<%
getTags = "Select searchTagXrefID from ss_modules_searchtags_xref where recordID = "&RS3("ID")&" and recordSetID = 0"
set tagRS = con.execute(getTags)
If tagRS.EOF then%>
<a href="/admin/modules/searchtags/legacy/?recordID=<%=RS3("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&recordSetID=0">Add</a>
<%Else%>
<a href="/admin/modules/searchtags/legacy/?recordID=<%=RS3("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&recordSetID=0">Edit</a>
<%End If%>
</td>
<%end if%>					
					<td width="20" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="richtemplate_listpages.asp?rank=<%=strRnk%>&SECTIONID=<%=request.querystring("sectionid")%>&pageid=<%=RS3("id")%>&PARENTID=<%=RS3("PARENTID")%>&posit=moveup"><img border="0" src="images/uparrow.gif" alt="move up"></a></td>

            <td width="20" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="richtemplate_listpages.asp?rank=<%=strRnk%>&sectionid=<%=request.querystring("sectionid")%>&pageid=<%=RS3("id")%>&PARENTID=<%=RS3("PARENTID")%>&posit=movedown">
			<img border="0" src="images/downarrow.gif" alt="move down"></a></td>
			



         		
            		<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF RS3("LINKONLY")<> TRUE and RS3("message") <> ""  and SESSION("ALLOW_PUBLISH") = TRUE THEN%>
 		 	<%If RS3("Checked_out") = True then%>

			<font color="#808080">Edit</font>
	<%else%>
			<a title="Edit Live Content" href="/editor2/complete/?pageID=<%=RS3("ID")%>&pageAction=Edit">Edit</a>
	<%end if%>		
<%ELSE%>
	<font color="#808080">Edit</font>
<%END IF%>	
					</td>
					
					<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%if rs3("message2") <> "" then%>
	<%IF RS3("LINKONLY")<> TRUE THEN%>
	 	<%If RS3("Checked_out") = True then%>

				<font color="#808080">Edit</font>
		<%else%>
				<a title="Edit Offline Content" href="/editor2/complete/?pageID=<%=RS3("ID")%>&pageAction=Edit&pageStatus=offline">Edit</a>
		<%end if%>	
	<%ELSE%>

		<font color="#808080">Edit</font>
	<%END IF%>
<%else%>
		&nbsp;
<%end if%></td>
		
					
		
			      <td width="60" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
	 		 	<%If RS3("Checked_out") = True then%>
		 	OUT<%IF SESSION("userNAME") = RS3("CHECKED_ID") THEN%><br><a href="richtemplate_page_logic.asp?pageID=<%=RS3("ID")%>&sectionID=<%=RS3("sectionID")%>&TASK=checkin">CHECK IN</a><%end if%>
	<%else%>
	IN
	<%end if%></td>

					
		
			      <td width="80" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%iF RS3("checked_out") = True then
	Response.write (RS3("checked_ID"))
Else%>
&nbsp;
<%End If%></td>

					
		
			      <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF SUBLEVEL > 3 AND RS3("LINKONLY")<> TRUE THEN%> 
				      <a href="richtemplate_externallinks.asp?id-1=&PAGEID=<%=RS3("id")%>&SECTIONID=<%=REQUEST.QUERYSTRING("SECTIONID")%>&pagename=<%=rs3("name")%>&PAGELEVEL=4&action=add&pageAction=Add">
		Add</a>
<%ELSE%>
		<font color="#808080">Add</font>
<%END IF%>				
		
		</td>

	<td width="30" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF SESSION("ALLOW_PAGEDELETE")= TRUE THEN%>	
    <a href="javascript:confirmdelete('richtemplate_page_logic.asp?task=delete&pageID=<%=RS3("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("sectionid")%>')" >Delete</a>
<%else%>&nbsp;
<%end if%>
	</td>



	
			     <%
			     	'FOURTH LEVEL PAGES --------------------------->
			     	
			     	PAGELEVEL = 5
			     	
    				pagesql = "SELECT  * FROM WEBINFO WHERE PARENTID="&RS3("ID")&"" & myWhereClause &"  ORDER BY  RANK"
					'response.write websectionsql
					SET RS4 = CON.EXECUTE (pagesql)
					
					WHILE NOT RS4.EOF
					
					pageID4 = RS4("id")
					parentLiveID = RS4("parentID")
					
					    
    					'ASSIGN BACKGROUND COLOR DEPENDENT ON PENDING STATUS
  
    					IF RS4("LINKONLY") = TRUE THEN
   				 			BGCOLOR = "#F0F0F0"
    					ELSEIf RS4("pending") = true then
    						bgcolor = "#f7e6e6"
    					Else
   				 			BGCOLOR = "#FFFFFF"
    					END IF
    					
    					
    					strRnk = RS4("rank")
					%>
    					
			
					<TR><td width='50%' bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;'>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img border='0' src='images/icon_subpage.gif' width='16' height='16'><font color='#3055A9' >
					&nbsp;<%=RS4("NAME")%><%If RS4("linkonly")= TRUE THEN
					RESPONSE.WRITE" - (Link)"
					elseif RS4("pending") = true then
					Response.write" - (Offline)"
					END IF%></FONT></td>


<%		showLive = False
		If RS4("parentID") <> "" then
			Select02 = "Select pending, ID from webinfo where ID = "&rs4("parentID")
			Set RS2Live = con.execute(select02)
			If Not RS2Live.EOF then
				If RS2Live("pending") = True then
					showLive = False
				Else
					showLive = True
				End if
			
			End if
		Else
			showLive = True
		End if
		
%>



<%' This code takes the page offline%> 
		<td width="50" class=style1 bgcolor ="<%=bgcolor%>" valign="middle" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">       
<%If showLive = True then%>
	 <%If rs4("pending") = true then%>
		<a href="richtemplate_page_logic.asp?pageid=<%=Rs4("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&task=online">Make Live</a>
	<%Else%>
		<a href="richtemplate_page_logic.asp?pageid=<%=Rs4("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&task=offline">Take Offline</a>
	<%End If%>
<%Else%>
<font color="#808080">Make Live</font>	
<%End If%>





<%If tagModActive = True then%>

            		<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">



<%
getTags = "Select searchTagXrefID from ss_modules_searchtags_xref where recordID = "&RS3("ID")&" and recordSetID = 0"
set tagRS = con.execute(getTags)
If tagRS.EOF then%>
<a href="/admin/modules/searchtags/legacy/?recordID=<%=RS3("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&recordSetID=0">Add</a>
<%Else%>
<a href="/admin/modules/searchtags/legacy/?recordID=<%=RS3("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&recordSetID=0">Edit</a>
<%End If%>
</td>
<%end if%>					
					<td width="20" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="richtemplate_listpages.asp?rank=<%=strRnk%>&SECTIONID=<%=request.querystring("sectionid")%>&pageid=<%=RS4("id")%>&PARENTID=<%=RS4("PARENTID")%>&posit=moveup"><img border="0" src="images/uparrow.gif" alt="move up"></a></td>

            <td width="20" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="richtemplate_listpages.asp?rank=<%=strRnk%>&sectionid=<%=request.querystring("sectionid")%>&pageid=<%=RS4("id")%>&PARENTID=<%=RS4("PARENTID")%>&posit=movedown">
			<img border="0" src="images/downarrow.gif" alt="move down"></a></td>
			


            		<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF RS4("LINKONLY")<> TRUE  and RS4("message") <> ""  and SESSION("ALLOW_PUBLISH") = TRUE THEN%>
 		 	<%If RS4("Checked_out") = True then%>
			<font color="#808080">Edit</font>
	<%else%>
			<a title="Edit Live Content" href="/editor2/complete/?pageID=<%=RS4("ID")%>&pageAction=Edit">Edit</a>
	<%end if%>		
<%ELSE%>
	<font color="#808080">Edit</font>
<%END IF%>
					</td>
					
					<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%if rs4("message2") <>"" then%>
	<%IF RS4("LINKONLY")<> TRUE THEN%>
 		 	<%If RS4("Checked_out") = True then%>
				<font color="#808080">Edit</font>
		<%else%>
				<a title="Edit Offline Content" href="/editor2/complete/?pageID=<%=RS4("ID")%>&pageAction=Edit&pageStatus=offline">Edit</a>
		<%end if%>	
	<%ELSE%>

		<font color="#808080">Edit</font>
	<%END IF%>
<%else%>
		&nbsp;
<%end if%></td>
		
		
			      <td width="60" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
	 		 	<%If RS4("Checked_out") = True then%>
		 	OUT<%IF SESSION("userNAME") = RS4("CHECKED_ID") THEN%><br><a href="richtemplate_page_logic.asp?pageID=<%=RS4("ID")%>&sectionID=<%=RS4("sectionID")%>&TASK=checkin">CHECK IN</a><%end if%>
	<%else%>
	IN
	<%end if%></td>
    
		
			      <td width="80" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%iF RS4("checked_out") = True then
	Response.write (RS4("checked_ID"))
Else%>
&nbsp;
<%End If%></td>
    
		
			      <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF SUBLEVEL > 4 AND RS4("LINKONLY")<>TRUE THEN%>					

			      <a href="richtemplate_externallinks.asp?id-1=&PAGEID=<%=RS4("id")%>&SECTIONID=<%=REQUEST.QUERYSTRING("SECTIONID")%>&pagename=<%=rs4("name")%>&PAGELEVEL=5&action=add&pageAction=Add">

		Add</a>
<%ELSE%>
		<font color="#808080">Add</font>
<%END IF%>

		</td>
    
    <td width="30" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF SESSION("ALLOW_PAGEDELETE")= TRUE THEN%>	
    <a href="javascript:confirmdelete('richtemplate_page_logic.asp?task=delete&pageID=<%=RS4("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("sectionid")%>')" >Delete</a>
<%else%>&nbsp;
<%end if%>
	</td>
		
		
	
			<%

			     
			     	'FIFTH LEVEL PAGES --------------------------->
			     	
			     	PAGELEVEL = 6
			     	
    				pagesql = "SELECT  * FROM WEBINFO WHERE PARENTID="&RS4("ID")&"" & myWhereClause &"  ORDER BY  RANK"
					'response.write websectionsql
					SET RS5 = CON.EXECUTE (pagesql)
					
					WHILE NOT RS5.EOF
					
					pageid5 = RS5("id")
					
					    
    					'ASSIGN BACKGROUND COLOR DEPENDENT ON PENDING STATUS
    					
  
    					IF RS5("LINKONLY") = TRUE THEN
   				 			BGCOLOR = "#F0F0F0"
    					ELSEIf RS5("pending") = true then
    						bgcolor = "#f7e6e6"
    					Else
   				 			BGCOLOR = "#FFFFFF"
    					END IF

    					
    					
    					strRnk = RS5("rank")
					%>
    					
			
					<TR><td width='50%' bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;'>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img border='0' src='images/icon_subpage.gif' width='16' height='16'><font color='#3055A9' >
					&nbsp;<%=RS5("NAME")%><%If RS5("linkonly")= TRUE THEN
					RESPONSE.WRITE" - (Link)"
					elseif RS5("pending") = true then
					Response.write" - (Offline)"
					END IF%></FONT></td>
<%		showLive = False
		If RS5("parentID") <> "" then
			Select02 = "Select pending, ID from webinfo where ID = "&rs5("parentID")&"" & myWhereClause &""
			Set RS2Live = con.execute(select02)
			If Not RS2Live.EOF then
				If RS2Live("pending") = True then
					showLive = False
				Else
					showLive = True
				End if
			
			End if
		Else
			showLive = True
		End if
		
%>


<%' This code takes the page offline%> 
<td width="50" class=style1 bgcolor ="<%=bgcolor%>" valign="middle" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">       
<%If showLive = True then%>
	 <%If rs5("pending") = true then%>
		<a href="richtemplate_page_logic.asp?pageid=<%=Rs5("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&task=online">Make Live</a>
	<%Else%>
		<a href="richtemplate_page_logic.asp?pageid=<%=Rs5("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&task=offline">Take Offline</a>
	<%End If%>
<%Else%>
<font color="#808080">Make Live</font>	
<%End If%>




<%If tagModActive = True then%>

            		<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">

				
<%
getTags = "Select searchTagXrefID from ss_modules_searchtags_xref where recordID = "&RS4("ID")&" and recordSetID = 0"
set tagRS = con.execute(getTags)
If tagRS.EOF then%>
<a href="/admin/modules/searchtags/legacy/?recordID=<%=RS4("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&recordSetID=0">Add</a>
<%Else%>
<a href="/admin/modules/searchtags/legacy/?recordID=<%=RS4("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&recordSetID=0">Edit</a>
<%End If%>
</td>
<%end if%>
					<td width="20" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="richtemplate_listpages.asp?rank=<%=strRnk%>&SECTIONID=<%=request.querystring("sectionid")%>&pageid=<%=RS5("id")%>&PARENTID=<%=RS5("PARENTID")%>&posit=moveup"><img border="0" src="images/uparrow.gif" alt="move up"></a></td>

            <td width="20" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="richtemplate_listpages.asp?rank=<%=strRnk%>&sectionid=<%=request.querystring("sectionid")%>&pageid=<%=RS5("id")%>&PARENTID=<%=RS5("PARENTID")%>&posit=movedown">
			<img border="0" src="images/downarrow.gif" alt="move down"></a></td>
			


            		<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
				
<%IF RS5("LINKONLY")<> TRUE  and RS5("message") <> ""  and SESSION("ALLOW_PUBLISH") = TRUE THEN%>
 		 	<%If RS5("Checked_out") = True then%>

			<font color="#808080">Edit</font>
	<%else%>
			<a title="Edit Live Content" href="/editor2/complete/?pageID=<%=RS5("ID")%>&pageAction=Edit">Edit</a>
	<%end if%>		
<%ELSE%>
	<font color="#808080">Edit</font>
<%END IF%>
					</td>
					<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%if rs5("message2") <>"" then%>
 		 	<%If RS5("Checked_out") = True then%>
	 	<%If Session(pageID5) = True then%>
				<font color="#808080">Edit</font>
		<%else%>
				<a title="Edit Offline Content" href="/editor2/complete/?pageID=<%=RS5("ID")%>&pageAction=Edit&pageStatus=offline">Edit</a>
		<%end if%>	
	<%ELSE%>
		<font color="#808080">Edit</font>
	<%END IF%>
<%else%>
		&nbsp;
<%end if%></td>
					
			      <td width="60" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
	 		 	<%If RS5("Checked_out") = True then%>
		 	OUT<%IF SESSION("userNAME") = RS5("CHECKED_ID") THEN%><br><a href="richtemplate_page_logic.asp?pageID=<%=RS5("ID")%>&sectionID=<%=RS5("sectionID")%>&TASK=checkin">CHECK IN</a><%end if%>
	<%else%>
	IN
	<%end if%></td>
    
    
			      <td width="80" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%iF RS5("checked_out") = True then
	Response.write (RS5("checked_ID"))
Else%>
&nbsp;
<%End If%></td>
    
    
			      <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF SUBLEVEL > 5 AND RS5("LINKONLY")<> TRUE THEN%>	

				  <a href="richtemplate_externallinks.asp?id-1=&PAGEID=<%=RS5("id")%>&SECTIONID=<%=REQUEST.QUERYSTRING("SECTIONID")%>&pagename=<%=rs5("name")%>&PAGELEVEL=6&action=add&pageAction=Add">

		Add</a>
<%ELSE%>
		<font color="#808080">Add</font>
<%END IF%>
		</td>
    
    
    <td width="30" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF SESSION("ALLOW_PAGEDELETE")= TRUE THEN%>	
    <a href="javascript:confirmdelete('richtemplate_page_logic.asp?task=delete&pageID=<%=RS5("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("sectionid")%>')" >Delete</a>
<%else%>&nbsp;
<%end if%>
	</td>
		
		

			<%

' 	<--------------------------- SIXTH LEVEL PAGES --------------------------->
'	    < TO MAKE ANOTHER LEVEL COPY FROM HERE TO WHERE IT SAYS STOP BELOW >

			     	
    				pagesql = "SELECT  * FROM WEBINFO WHERE PARENTID="&RS5("ID")&"" & myWhereClause &"  ORDER BY  RANK"
					SET RS6 = CON.EXECUTE (pagesql)
					
					WHILE NOT RS6.EOF
					
					pageID6 = RS6("id")
					
					    
    					'ASSIGN BACKGROUND COLOR DEPENDENT ON PENDING STATUS
    				  
    					IF RS6("LINKONLY") = TRUE THEN
   				 			BGCOLOR = "#F0F0F0"
    					ELSEIf RS6("pending") = true then
    						bgcolor = "#f7e6e6"
    					Else
   				 			BGCOLOR = "#FFFFFF"
    					END IF
    					
    					strRnk = RS6("rank")
					%>
    					
			
					<TR><td width='50%' bgcolor ="<%=bgcolor%>" style='border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC;'>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img border='0' src='images/icon_subpage.gif' width='16' height='16'><p><font color='#3055A9' >
					&nbsp;<%=RS6("NAME")%><%If RS6("linkonly")= TRUE THEN
					RESPONSE.WRITE" - (Link)"
					elseif RS6("pending") = true then
					Response.write" - (Offline)"
					END IF%></FONT></td>



<%		showLive = False
		If RS6("parentID") <> "" then
			Select02 = "Select pending, ID from webinfo where ID = "&rs6("parentID")&"" & myWhereClause &""
			Set RS2Live = con.execute(select02)
			If Not RS2Live.EOF then
				If RS2Live("pending") = True then
					showLive = False
				Else
					showLive = True
				End if
			
			End if
		Else
			showLive = True
		End if
		
%>




<%' This code takes the page offline%> 
<td width="50" class=style1 bgcolor ="<%=bgcolor%>" valign="middle" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">       
<%If showLive = True then%>
	<%If rs6("pending") = true then%>
		<a href="richtemplate_page_logic.asp?pageid=<%=Rs6("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&task=online">Make Live</a>
	<%Else%>
		<a href="richtemplate_page_logic.asp?pageid=<%=Rs6("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&task=offline">Take Offline</a>
	<%End If%>
<%Else%>
<font color="#808080">Make Live</font>	
<%End If%>





<%If tagModActive = True then%>
            <td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">


<%
getTags = "Select searchTagXrefID from ss_modules_searchtags_xref where recordID = "&RS5("ID")&" and recordSetID=0"
set tagRS = con.execute(getTags)
If tagRS.EOF then%>
<a href="/admin/modules/searchtags/legacy/?recordID=<%=RS5("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&recordSetID=0">Add</a>
<%Else%>
<a href="/admin/modules/searchtags/legacy/?recordID=<%=RS5("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("SECTIONID")%>&recordSetID=0">Edit</a>
<%End If%>
</td>
<%end if%>
					<td width="20" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="richtemplate_listpages.asp?rank=<%=strRnk%>&SECTIONID=<%=request.querystring("sectionid")%>&pageid=<%=RS6("id")%>&PARENTID=<%=RS6("PARENTID")%>&posit=moveup"><img border="0" src="images/uparrow.gif" alt="move up"></a></td>

            <td width="20" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
			<a href="richtemplate_listpages.asp?rank=<%=strRnk%>&sectionid=<%=request.querystring("sectionid")%>&pageid=<%=RS6("id")%>&PARENTID=<%=RS6("PARENTID")%>&posit=movedown">
			<img border="0" src="images/downarrow.gif" alt="move down"></a></td>
			


            <td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF RS6("LINKONLY")<> TRUE  and RS6("message") <> "" and SESSION("ALLOW_PUBLISH") = TRUE THEN%>
 		 	<%If RS6("Checked_out") = True then%>
			<font color="#808080">Edit</font>
	<%else%>
			<a title="Edit Live Content" href="/editor2/complete/?pageID=<%=RS6("ID")%>&pageAction=Edit">Edit</a>
	<%end if%>		
<%ELSE%>
	<font color="#808080">Edit</font>
<%END IF%>
					</td>
					
					<td width="50" class=style1 valign="middle" bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">

<%if rs6("message2") <>"" then%>
	<%IF RS6("LINKONLY")<> TRUE THEN%>
 		 	<%If RS6("Checked_out") = True then%>
				<font color="#808080">Edit</font>
		<%else%>
				<a title="Edit Offline Content" href="/editor2/complete/?pageID=<%=RS6("ID")%>&pageAction=Edit&pageStatus=offline">Edit</a>
		<%end if%>	
	<%ELSE%>

		<font color="#808080">Edit</font>
	<%END IF%>
<%else%>
		&nbsp;
<%end if%></td>
		
				
			      <td width="60" class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">

				      	 		 	<%If RS6("Checked_out") = True then%>
		 	OUT<%IF SESSION("userNAME") = RS6("CHECKED_ID") THEN%><br><a href="richtemplate_page_logic.asp?pageID=<%=RS6("ID")%>&sectionID=<%=RS6("sectionID")%>&TASK=checkin">CHECK IN</a><%end if%>
	<%else%>
	IN
	<%end if%></td>
		
				
			      <td width="80" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">

				      <%iF RS6("checked_out") = True then
	Response.write (RS6("checked_ID"))
Else%>
&nbsp;
<%End If%></td>
		
				
			      <td class=style1 bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">

				      &nbsp;&nbsp;</td>
<td width="30" class=body bgcolor ="<%=bgcolor%>" style="border-left-width: 1px; border-right-width: 1px; border-top-width: 1px; border-bottom: 1px solid #D2E3FC; ">
<%IF SESSION("ALLOW_PAGEDELETE")= TRUE THEN%>	
    <a href="javascript:confirmdelete('richtemplate_page_logic.asp?task=delete&pageID=<%=RS6("ID")%>&sectionid=<%=REQUEST.QUERYSTRING("sectionid")%>')" >Delete</a>
<%else%>&nbsp;
<%end if%>
</td>
		
	
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


    <%ELSE %>
    	

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

	END IF
	
	RS.CLOSE
	SET WEBSECTIONSQL = NOTHING
	CON.CLOSE
%>



</td></table>


</body>
</html>