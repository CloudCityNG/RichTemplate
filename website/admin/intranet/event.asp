<!--#INCLUDE FILE="conn.asp"-->
<%

	myEventSQL = "Select c.*, G.GROUP FROM CALENDAR_EVENT C, CALENDAR_USER_GROUPS G WHERE  C.ID =" & REQUEST.QUERYSTRING("id") & " AND C.GROUP_ID=G.ID"
	SET RS = CON.EXECUTE (myEventSQL)


%>
<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">

		<title>Event List</title>
  
		<script language="javascript" src="win_popup.js"></script>
		<script language="javascript" src="win_help.js"></script>     
<SCRIPT LANGUAGE="JavaScript">		
function myprint() {
window.parent.focus();
window.print();
}

</script>
<SCRIPT LANGUAGE="JavaScript">


<!-- Begin
function myclose() {
window.close();

}
//  End -->
</script>
	
	

</head>

<body onLoad="self.focus()">


	<table width="100%" class="viewevent" cellpadding="3" cellspacing="0">
	                              <TR><TD BGCOLOR="#3D4A7E" COLSPAN=2><CENTER><FONT FACE="VERDANA" SIZE="2" COLOR="#FFFFFF"><B>CALENDAR EVENT</B></FONT></CENTER></TD></TR>

		<tr><td colspan=2 bgcolor="#FFFFFF" align="right"><form><img src="images/print.gif" value="Print Page" onClick="myprint()"><img src="images/close.gif" value="Close Window" onClick="myclose()"> </center></form>
</td></tr>
		<tr>
			<td colspan=2 id="NavigationTop">
            <font face="arial" size="2"><B><%=RS("EVENT_NAME")%>:  <%=RS("START_MONTH")%>/<%=RS("START_DAY")%>/<%=RS("START_YEAR")%></B></font>
			</td>
		</tr>

		
		<tr>						
			<td align="right" class="vieweventheader"><font face="arial" size="2"><b>Group:</b></font></td>
			<td class="viewevent"><font face="arial" size="2"><%=RS("GROUP")%></font>
			</td>
		</tr>
		

		<tr>
		   <td align="right" class="vieweventheader">
			<font face="arial" size="2"><b>Date:</b></font>
			</td>
			<td class="viewevent"><font face="arial" size="2"><%=RS("START_MONTH")%>/<%=RS("START_DAY")%>/<%=RS("START_YEAR")%></font></td>
		</tr>

		<tr>							
			<td align="right" class="vieweventheader"><font face="arial" size="2"><b>Start Time:</b></font></td>
			<%IF RS("START_HOUR") > 12 THEN
X = RS("START_HOUR") - 12
ELSE
X = RS("START_HOUR")
END IF
IF RS("START_MINUTE")=0 THEN
Y = "00"
ELSE
Y = RS("START_MINUTE")
END IF
%>
			<td class="viewevent"><font face="arial" size="2"><%=X%>:<%=Y%><%IF RS("START_HOUR")>=12 THEN%>pm<%else%>am<%end if%></font>
   		</td>
		</tr>
			<%IF RS("END_HOUR") > 12 THEN
W = RS("END_HOUR") - 12
ELSE
W = RS("END_HOUR")
END IF
IF RS("END_MINUTE")=0 THEN
Z = "00"
ELSE
Z = RS("END_MINUTE")
END IF
%>
											
											
      <tr>
         <td align="right" class="vieweventheader"><font face="arial" size="2"><b>End Time:</b></font></td>
         <td class="viewevent"><font face="arial" size="2"><%=W%>:<%=Z%><%IF RS("END_HOUR")>=12 THEN%>pm<%else%>am<%end if%></font>
   		</td>
		</tr>

		

		<tr>
			<td align="right" class="vieweventheader"><font face="arial" size="2"><b>Type:</b></font></td>						
			<td class="viewevent"><font face="arial" size="2"><%=RS("EVENT_TYPE")%></font></td>
		</tr>						
		<tr>
		   <td align="right" class="vieweventheader"><font face="arial" size="2"><b>Description:</b></font>
			<td class="viewevent"><font face="arial" size="2"><%=RS("EVENT_DESC")%></font></td>
		</tr>
				<tr>
		   <td align="right" class="vieweventheader"><font face="arial" size="2"><b>Location:</b></font>
			<td class="viewevent"><font face="arial" size="2"><%=RS("EVENT_PLACE")%></font></td>
		</tr>
		

	</table>

   
	</body>

</html>






























