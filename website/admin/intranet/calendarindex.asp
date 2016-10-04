<!--#include file="loggedin.asp"-->
<!--#INCLUDE FILE="dsn.asp"-->
<head>
<SCRIPT LANGUAGE="JavaScript">
<!-- Begin
function popUp(URL) {
day = new Date();
id = day.getTime();
eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=yes,location=0,statusbar=0,menubar=0,resizable=0,width=550,height=550,left = 100,top = 100');");
}
// End -->
</script>
</head>
<%Session("name")="calendarindex"

sessname = Session ("name")


Function GetDaysInMonth(iMonth, iYear)
	Dim dTemp
	dTemp = DateAdd("d", -1, DateSerial(iYear, iMonth + 1, 1))
	GetDaysInMonth = Day(dTemp)
End Function



Function GetWeekdayMonthStartsOn(dAnyDayInTheMonth)
	Dim dTemp
	dTemp = DateAdd("d", -(Day(dAnyDayInTheMonth) - 1), dAnyDayInTheMonth)
	GetWeekdayMonthStartsOn = WeekDay(dTemp)
End Function

Function SubtractOneMonth(dDate)
	SubtractOneMonth = DateAdd("m", -1, dDate)
End Function

Function AddOneMonth(dDate)
	AddOneMonth = DateAdd("m", 1, dDate)
End Function



Dim dDate     ' Date we're displaying calendar for
Dim iDIM      ' Days In Month
Dim iDOW      ' Day Of Week that month starts on
Dim iCurrent  ' Variable we use to hold current day of month as we write table
Dim iPosition ' Variable we use to hold current position in table


' Get selected date.  There are two ways to do this.
' First check if we were passed a full date in RQS("date").
' If so use it, if not look for seperate variables, putting them togeter into a date.
' Lastly check if the date is valid...if not use today
If IsDate(Request.QueryString("date")) Then
	dDate = CDate(Request.QueryString("date"))
Else
	If IsDate(Request.QueryString("month") & "-" & Request.QueryString("day") & "-" & Request.QueryString("year")) Then
		dDate = CDate(Request.QueryString("month") & "-" & Request.QueryString("day") & "-" & Request.QueryString("year"))
	Else
		dDate = Date()
		' The annoyingly bad solution for those of you running IIS3
		If Len(Request.QueryString("month")) <> 0 Or Len(Request.QueryString("day")) <> 0 Or Len(Request.QueryString("year")) <> 0 Or Len(Request.QueryString("date")) <> 0 Then
			Response.Write "The date you picked was not a valid date.  The calendar was set to today's date.<BR><br>"
		End If
		' The elegant solution for those of you running IIS4
		'If Request.QueryString.Count <> 0 Then Response.Write "The date you picked was not a valid date.  The calendar was set to today's date.<BR><br>"
	End If
End If

'Now we've got the date.  Now get Days in the choosen month and the day of the week it starts on.
iDIM = GetDaysInMonth(Month(dDate), Year(dDate))
iDOW = GetWeekdayMonthStartsOn(dDate)

%>
<SCRIPT LANGUAGE="JavaScript">
<!-- Begin
function popUpp(URL) {
day = new Date();
id = day.getTime();
eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=yes,location=0,statusbar=0,menubar=0,resizable=0,width=550,height=375,left = 100,top = 100');");
}
// End -->
</script>

<table CELLPADDING="0" CELLSPACING="0" border=0 width="400">
<TR>
<TD><IMG SRC="images/mycalendartop.gif"></TD></TR>
</TABLE>
<table border="1" width="400" id="table1" cellspacing="0" cellpadding="0" bordercolor="#2857AA">
	<tr>
		<td>
<table border=0 CELLPADDING="0" CELLSPACING="0" bordercolor="#6acoec" width="400" id="table2">
<TR>
<td valign="top">
<TABLE BORDER=0 CELLSPACING=0 bordercolor="#6acoec" CELLPADDING=0 id="table3">
<TR>
<TD>
<TABLE BORDER=1 CELLSPACING=0 CELLPADDING=1 id="table4">
	<TR>
		<TD ALIGN="center" COLSPAN=7 bgcolor="#EBEBEB">
			<TABLE WIDTH=100% BORDER=0 CELLSPACING=0 CELLPADDING=0 id="table5">
				<TR>
					<TD ALIGN="right" BGCOLOR="#EBEBEB"><A HREF="<%=Session("name")%>.asp?date=<%= SubtractOneMonth(dDate) %>">
					<font color="#000000" size="2" face="Verdana">&lt;&lt;</font></A></TD>
					<TD ALIGN="center" BGCOLOR="#EBEBEB"><B>
					<font size="2" face="Verdana"><%= MonthName(Month(dDate)) & "  " & Year(dDate) %></font></B></TD>
					<TD ALIGN="left" BGCOLOR="#EBEBEB"><A HREF="<%=Session("name")%>.asp?date=<%= AddOneMonth(dDate) %>">
					<font color="#000000" size="2" face="Verdana">&gt;&gt;</font></A></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD ALIGN="center" BGCOLOR="#EBEBEB"><font face="Verdana" size="1"><B>Sun</B><BR><IMG SRC="./images/spacer.gif" WIDTH=25 HEIGHT=1 BORDER=0></font></TD>
		<TD ALIGN="center" BGCOLOR="#EBEBEB"><font face="Verdana" size="1"><B>Mon</B><BR><IMG SRC="./images/spacer.gif" WIDTH=25 HEIGHT=1 BORDER=0></font></TD>
		<TD ALIGN="center" BGCOLOR="#EBEBEB"><font face="Verdana" size="1"><B>Tue</B><BR><IMG SRC="./images/spacer.gif" WIDTH=25 HEIGHT=1 BORDER=0></font></TD>
		<TD ALIGN="center" BGCOLOR="#EBEBEB"><font face="Verdana" size="1"><B>Wed</B><BR><IMG SRC="./images/spacer.gif" WIDTH=25 HEIGHT=1 BORDER=0></font></TD>
		<TD ALIGN="center" BGCOLOR="#EBEBEB"><font face="Verdana" size="1"><B>Thu</B><BR><IMG SRC="./images/spacer.gif" WIDTH=25 HEIGHT=1 BORDER=0></font></TD>
		<TD ALIGN="center" BGCOLOR="#EBEBEB"><font face="Verdana" size="1"><B>Fri</B><BR><IMG SRC="./images/spacer.gif" WIDTH=25 HEIGHT=1 BORDER=0></font></TD>
		<TD ALIGN="center" BGCOLOR="#EBEBEB"><font face="Verdana" size="1"><B>Sat</B><BR><IMG SRC="./images/spacer.gif" WIDTH=25 HEIGHT=1 BORDER=0></font></TD>
	</TR>
<%
' Write spacer cells at beginning of first row if month doesn't start on a Sunday.
If iDOW <> 1 Then
	Response.Write vbTab & "<TR>" & vbCrLf
	iPosition = 1
	Do While iPosition < iDOW
		Response.Write vbTab & vbTab & "<TD>&nbsp;</TD>" & vbCrLf
		iPosition = iPosition + 1
	Loop
End If

' Write days of month in proper day slots
iCurrent = 1
iPosition = iDOW
Do While iCurrent <= iDIM
	' If we're at the begginning of a row then write TR
	If iPosition = 1 Then
		Response.Write vbTab & "<TR>" & vbCrLf
	End If
	
	
	'Function iiPosition(iPosition)	
	Dim iiPosition
	iiPosition = iPosition
	Select Case iPosition
Case 1
iiPosition = "Sunday"
Case 2
iiPosition = "Monday"
Case 3
iiPosition = "Tuesday"
Case 4
iiPosition = "Wednesday"
Case 5
iiPosition = "Thursday"
Case 6
iiPosition = "Friday"
Case 7
iiPostion = "Saturday"

End Select



	'SAVE FOR HIGHLIGHT CELL
	'AND MonthName(Month(date))=MonthName(Month(dDate))
	
	' If the day we're writing is the selected day then highlight it somehow.
	If iCurrent = Day(dDate) Then
		Response.Write vbTab & vbTab & "<TD BGCOLOR=#CDB505><FONT SIZE=""-1""><B>" & iCurrent & "</B></FONT><BR></TD>" & vbCrLf
	Else
 checkEventSQL = "SELECT START_DAY, START_MONTH, START_YEAR, END_DAY, END_MONTH, END_YEAR, GROUP_ID FROM CALENDAR_EVENT WHERE START_MONTH=" & Month(dDate) & " AND START_DAY=" & iCurrent & " AND START_YEAR=" & Year(dDate) & ""
	Set RS3 = con.execute (checkEventSQL)

	IF RS3.EOF THEN

			Response.Write vbTab & vbTab & "<TD><FONT SIZE=""-1"">" & iCurrent & "</FONT><BR></TD>" & vbCrLf

	ELSE
			groupid=RS3("group_id")
			if groupid =1 THEN
				Response.Write vbTab & vbTab & "<TD><img border='0' src='images/square_blue.gif' width='5' height='5'><A HREF=" & sessname & ".asp?date=" & Month(dDate) & "/" & iCurrent & "/" & Year(dDate) & "&dayofweek=" & iiPosition & "#calendar><FONT SIZE=""-1"">" & iCurrent & "</FONT></A><BR></TD>" & vbCrLf
				'response.write ""&groupid&""
			End If
			
			IF groupid = 2 THEN
				Response.Write vbTab & vbTab & "<TD><img border='0' src='images/square_red.gif' width='5' height='5'><A HREF=" & sessname & ".asp?date=" & Month(dDate) & "/" & iCurrent & "/" & Year(dDate) & "&dayofweek=" & iiPosition & "#calendar><FONT SIZE=""-1"">" & iCurrent & "</FONT></A><BR></TD>" & vbCrLf
				'response.write ""&groupid&""
			End If
			If groupid >2 THEN
				Response.Write vbTab & vbTab & "<TD><img border='0' src='images/square_yellow.gif' width='5' height='5'><A HREF=" & sessname & ".asp?date=" & Month(dDate) & "/" & iCurrent & "/" & Year(dDate) & "&dayofweek=" & iiPosition & "#calendar><FONT SIZE=""-1"">" & iCurrent & "</FONT></A><BR></TD>" & vbCrLf
				'response.write ""&groupid&""
			'End If

			end if

	END IF
	set rs3 = nothing
	End If
	
	' If we're at the endof a row then write /TR
	If iPosition = 7 Then
		Response.Write vbTab & "</TR>" & vbCrLf
		iPosition = 0
	End If
	
	' Increment variables
	iCurrent = iCurrent + 1
	iPosition = iPosition + 1
Loop

' Write spacer cells at end of last row if month doesn't end on a Saturday.
If iPosition <> 1 Then
	Do While iPosition <= 7
		Response.Write vbTab & vbTab & "<TD>&nbsp;</TD>" & vbCrLf
		iPosition = iPosition + 1
	Loop
	Response.Write vbTab & "</TR>" & vbCrLf
End If
%>
</TABLE>
</TD>
</TR>
</TABLE>
</td><td width="100%" valign="top">
<TABLE id="table6"><TR><TD valign="top" colspan="2"><a name="calendar"><font size="1" face="Arial"><b>Events For:  <%= MonthName(Month(dDate)) & "  " & Day(dDate) &  ", " & Year(dDate) %></b></font></TD></TR>
<TR><TD colspan="2"><img src="images/greyline.gif" width="125" height="1"></TD></TR>


<font size="1" face="Arial">




<%GetEventSQL = "SELECT G.GROUP, G.ID, C.ID,C.START_HOUR,C.START_MINUTE, C.GROUP_ID,C.EVENT_NAME FROM CALENDAR_EVENT C, CALENDAR_USER_GROUPS G WHERE C.START_MONTH=" & Month(dDate) & " AND C.START_DAY=" & Day(dDate) & " AND C.START_YEAR=" & Year(dDate) & " AND C.GROUP_ID=G.ID ORDER BY G.ID,C.START_HOUR,C.START_MINUTE"
SET RS = CON.EXECUTE(GetEventSQL)

dim lid
lid = 999999
While not RS.EOF	
	if lid <> RS("GROUP_ID").value then
		lid = RS("GROUP_ID").value
		if RS("GROUP").value = "sffdf" then
		
			IF RS("START_HOUR") > 12 THEN
				X = RS("START_HOUR") - 12
			ELSEIF RS("start_hour")=0 THEN
			X = 12
			ELSE
				X = RS("START_HOUR")
			END IF
				IF RS("START_MINUTE")=0 THEN
					Y = "00"
				ELSE
					Y = RS("START_MINUTE")
				END IF%>
			<TR><TD colspan=2><b><font size=1 face=Arial ><%=RS("GROUP")%></font></b></TR></TD>
			<TR><TD width="12"><img src="images/redarrow.gif" align="left"></td>
				<td><font size="1" face="Arial"><a href="javascript:popUp('event.asp?calsession=<%=calsession%>&id=<%=RS("id")%>')"><%=RS("EVENT_NAME")%></a> - (<%=X%>:<%=Y%><%IF RS("START_HOUR")>=12 THEN%>pm<%else%>am<%end if%>)</font></td></tr>

		<%else
		
			IF RS("START_HOUR") > 12 THEN
				X = RS("START_HOUR") - 12
			ELSEIF RS("start_hour")=0 THEN
			X = 12
			ELSE
				X = RS("START_HOUR")
			END IF
				IF RS("START_MINUTE")=0 THEN
					Y = "00"
				ELSE
					Y = RS("START_MINUTE")
				END IF%>
			<TR bgcolor="#00FFFF"><TD colspan=2 bgcolor="#EBEBEB"><b><font size=1 face=Arial ><%=RS("GROUP")%></font></b></TR></TD>
			<TR><TD width="12"><img src="images/redarrow.gif" align="left"></td>
				<td><font size="1" face="Arial"><a href="javascript:popUp('event.asp?calsession=<%=calsession%>&id=<%=RS("id")%>')"><%=RS("EVENT_NAME")%></a> - (<%=X%>:<%=Y%><%IF RS("START_HOUR")>=12 THEN%>pm<%else%>am<%end if%>)</font></td></tr>
		<%end if
	else
	
			IF RS("START_HOUR") > 12 THEN
				X = RS("START_HOUR") - 12
			ELSEIF RS("start_hour")=0 THEN
			X = 12
			ELSE
				X = RS("START_HOUR")
			END IF
				IF RS("START_MINUTE")=0 THEN
					Y = "00"
				ELSE
					Y = RS("START_MINUTE")
				END IF%>
			<TR><TD width="12"><img src="images/redarrow.gif" align="left"></td><td><font size="1" face="Arial"><a href="javascript:popUp('event.asp?calsession=<%=calsession%>&id=<%=RS("id")%>')"><%=RS("EVENT_NAME")%></a> - (<%=X%>:<%=Y%><%IF RS("START_HOUR")>=12 THEN%>pm<%else%>am<%end if%>)</font></td></tr>
	<%end if	
RS.MoveNext
Wend
%>



<tr><td colspan=2>
<FORM ACTION="<%=sessname%>.asp" METHOD=GET>

<SELECT class="formRequiredInput2" NAME="month">
<option value="<%=Month(dDate)%>"><%=MonthName(Month(dDate))%></option>
	<OPTION VALUE=1>January</OPTION>
	<OPTION VALUE=2>February</OPTION>
	<OPTION VALUE=3>March</OPTION>
	<OPTION VALUE=4>April</OPTION>
	<OPTION VALUE=5>May</OPTION>
	<OPTION VALUE=6>June</OPTION>
	<OPTION VALUE=7>July</OPTION>
	<OPTION VALUE=8>August</OPTION>
	<OPTION VALUE=9>September</OPTION>
	<OPTION VALUE=10>October</OPTION>
	<OPTION VALUE=11>November</OPTION>
	<OPTION VALUE=12>December</OPTION>
</SELECT>
<SELECT class="formRequiredInput2" NAME="day">
<option value="<%=Day(dDate)%>"><%=Day(dDate)%></option>
	<OPTION VALUE=1>1</OPTION>
	<OPTION VALUE=2>2</OPTION>
	<OPTION VALUE=3>3</OPTION>
	<OPTION VALUE=4>4</OPTION>
	<OPTION VALUE=5>5</OPTION>
	<OPTION VALUE=6>6</OPTION>
	<OPTION VALUE=7>7</OPTION>
	<OPTION VALUE=8>8</OPTION>
	<OPTION VALUE=9>9</OPTION>
	<OPTION VALUE=10>10</OPTION>
	<OPTION VALUE=11>11</OPTION>
	<OPTION VALUE=12>12</OPTION>
	<OPTION VALUE=13>13</OPTION>
	<OPTION VALUE=14>14</OPTION>
	<OPTION VALUE=15>15</OPTION>
	<OPTION VALUE=16>16</OPTION>
	<OPTION VALUE=17>17</OPTION>
	<OPTION VALUE=18>18</OPTION>
	<OPTION VALUE=19>19</OPTION>
	<OPTION VALUE=20>20</OPTION>
	<OPTION VALUE=21>21</OPTION>
	<OPTION VALUE=22>22</OPTION>
	<OPTION VALUE=23>23</OPTION>
	<OPTION VALUE=24>24</OPTION>
	<OPTION VALUE=25>25</OPTION>
	<OPTION VALUE=26>26</OPTION>
	<OPTION VALUE=27>27</OPTION>
	<OPTION VALUE=28>28</OPTION>
	<OPTION VALUE=29>29</OPTION>
	<OPTION VALUE=30>30</OPTION>
	<OPTION VALUE=31>31</OPTION>
</SELECT>
<SELECT class="formRequiredInput2" NAME="year">
<option value="<%=Year(dDate)%>"><%=Year(dDate)%></option>
	<OPTION VALUE=1990>1990</OPTION>
	<OPTION VALUE=1991>1991</OPTION>
	<OPTION VALUE=1992>1992</OPTION>
	<OPTION VALUE=1993>1993</OPTION>
	<OPTION VALUE=1994>1994</OPTION>
	<OPTION VALUE=1995>1995</OPTION>
	<OPTION VALUE=1996>1996</OPTION>
	<OPTION VALUE=1997>1997</OPTION>
	<OPTION VALUE=1998>1998</OPTION>
	<OPTION VALUE=1999>1999</OPTION>
	<OPTION VALUE=2000>2000</OPTION>
	<OPTION VALUE=2001>2001</OPTION>
	<OPTION VALUE=2002>2002</OPTION>
	<OPTION VALUE=2003>2003</OPTION>
	<OPTION VALUE=2004>2004</OPTION>
	<OPTION VALUE=2005>2005</OPTION>
	<OPTION VALUE=2006>2006</OPTION>
	<OPTION VALUE=2007>2007</OPTION>
	<OPTION VALUE=2008>2008</OPTION>
	<OPTION VALUE=2009>2009</OPTION>
	<OPTION VALUE=2010>2010</OPTION>
</SELECT><INPUT TYPE="image" src="images/go.gif" name="I1">
</FORM>
</td></tr>
</TABLE>
</td>
</tr>
<TR>
<TD ALIGN="RIGHT" COLSPAN="2" ><a href="javascript: popUpp('calendareventadd.asp')"><IMG border="0" SRC="images/calendaraddicon.gif"></a></TD></TR>
</table>







































































































































		</td>
	</tr>
</table>







































































































































<p><img border="0" src="images/square_blue.gif" width="5" height="5"></p>






































































































































