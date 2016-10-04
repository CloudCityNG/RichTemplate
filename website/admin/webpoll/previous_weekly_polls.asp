<% Option Explicit %>
<% Response.Buffer = True %>
<!--#include file="common.inc" -->
<%
'****************************************************************************************
'**  Copyright Notice    
'**
'**  Web Wiz Guide ASP Weekly Poll
'**                                                              
'**  Copyright 2001-2002 Bruce Corkhill All Rights Reserved.                                
'**
'**  This program is free software; you can modify (at your own risk) any part of it 
'**  under the terms of the License that accompanies this software and use it both 
'**  privately and commercially.
'**
'**  All copyright notices must remain in tacked in the scripts and the 
'**  outputted HTML.
'**
'**  You may use parts of this program in your own private work, but you may NOT
'**  redistribute, repackage, or sell the whole or any part of this program even 
'**  if it is modified or reverse engineered in whole or in part without express 
'**  permission from the author.
'**
'**  You may not pass the whole or any part of this application off as your own work.
'**   
'**  All links to Web Wiz Guide and powered by logo's must remain unchanged and in place
'**  and must remain visible when the pages are viewed unless permission is first granted
'**  by the copyright holder.
'**
'**  This program is distributed in the hope that it will be useful,
'**  but WITHOUT ANY WARRANTY; without even the implied warranty of
'**  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE OR ANY OTHER 
'**  WARRANTIES WHETHER EXPRESSED OR IMPLIED.
'**
'**  You should have received a copy of the License along with this program; 
'**  if not, write to:- Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom.
'**    
'**
'**  No official support is available for this program but you may post support questions at: -
'**  http://www.webwizguide.info/forum
'**
'**  Support questions are NOT answered by e-mail ever!
'**
'**  For correspondence or non support questions contact: -
'**  info@webwizguide.com
'**
'**  or at: -
'**
'**  Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom
'**
'****************************************************************************************



'Dimension variables
Dim rsWeeklyPoll 		'Database Recordset Variable
Dim strPreviousPollQuestion	'Holds the previous Weekly Poll Questions
Dim intPreviousPollQuestionID	'Holds the previous weekly poll questions Id Number
Dim dtmPreviousPollQuestionDate 'Holds the date the previous weekly poll is from
Dim intRecordPositionPageNum 	'Holds the record position of the previous Poll Titles to display
Dim intRecordLoopCounter	'Loop counter for displaying the previous polling questions


'Declare constants
' ------------- Change the following line to the number of previous Poll Question Titles you wish to have displayed on each page -------

Const intRecordsPerPage = 9 	'Change this number to the number of previous Poll Question Titles to be displayed on each page

'----------------------------------------------------------------------------------------------------------------------------------------------


'Read in the previous poll question titles to display from the database
'If there is no previous titles to display from then start at the last question titles in the database
If Request.QueryString("PagePosition") = "" Then
	intRecordPositionPageNum = 1

'Else the page has been displayed before so the previous Poll Question Titles record postion is set to the Record Position number
Else
	intRecordPositionPageNum = CInt(Request.QueryString("PagePosition"))
End If	

%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>Previous Weekly Poll's</title>

<!-- The Web Wiz Guide Weekly Poll is written and produced by Bruce Corkhill ©2001
     	If you want your own Weekly Poll then goto http://www.webwizguide.info -->
     	
<style type="text/css">
<!--
.text {font-family: <% = strTextType %>; font-size: <% = intTextSize %>px; color: <% = strTextColour %>}
.bold {font-family: <% = strTextType %>; font-size: <% = intTextSize %>px; color: <% = strTextColour %>; font-weight: bold;}
.heading {font-family: <% = strTextType %>; font-size: 24px; color: <% = strTextColour %>}
a {font-family: <% = strTextType %>; font-size: <% = intTextSize %>px; color: <% = strLinkColour %>}
a:hover {font-family: <% = strTextType %>; font-size: <% = intTextSize %>px; color: <% = strHoverLinkColour %>}
a:visited {font-family: <% = strTextType %>; font-size: <% = intTextSize %>px; color: <% = strVisitedLinkColour %>}
a:visited:hover {font-family: <% = strTextType %>; font-size: <% = intTextSize %>px; color: <% = strHoverLinkColour %>}
-->
</style>
</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>">
<div align="center">
  <h2 class="heading">Previous Weekly Polls</h2>
  <table width="370" border="0" cellspacing="0" cellpadding="1">
    <%

'Create a recordset odject
Set rsWeeklyPoll = Server.CreateObject("ADODB.Recordset")

'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT * FROM tblPolls "
strSQL = strSQL & " ORDER By tblPolls.id_no DESC;"

'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
rsWeeklyPoll.CursorType = 3
	
'Set the Lock Type for the records so that the record set is only locked when it is deleteed
rsWeeklyPoll.LockType = 3

'Query the database
rsWeeklyPoll.Open strSQL, strCon

'Set the number of previous Poll Question Titles to display on each page by the constant set at the top of the script
rsWeeklyPoll.PageSize = intRecordsPerPage

'Get the previous Weekly Poll Titles record poistion to display from
If NOT rsWeeklyPoll.EOF Then rsWeeklyPoll.AbsolutePage = intRecordPositionPageNum



' loop through the previous poll questions in the database and display HTML hyperlinks to them
For intRecordLoopCounter = 1 to intRecordsPerPage

	'If there are no more previous Poll Question records left to display then exit loop
	If rsWeeklyPoll.EOF Then Exit For

	'Read in the polling questions ID number from the database
	intPreviousPollQuestionID = CInt(rsWeeklyPoll("id_no"))

	'Read in the polling question from the database
	strPreviousPollQuestion = rsWeeklyPoll("Question")
	
	'Read in the date the poll is from
	dtmPreviousPollQuestionDate = rsWeeklyPoll("Date")
	

	'Write the HTML to display links to all the previous Poll Question Titles
	Response.Write vbCrLf & "	 <tr>"
	Response.Write vbCrLf & "         <td width=""250""><a href=""weekly_poll.asp?PollID=" &  intPreviousPollQuestionID  & "&PagePosition=" & intRecordPositionPageNum & """ target=""_self"">" & strPreviousPollQuestion & "</a></td>"	
	Response.Write vbCrLf & "	  <td width=""120"" align=""right"" class=""text"">" & FormatDateTime(dtmPreviousPollQuestionDate, VbLongDate) & "</td>"
	Response.Write vbCrLf & "	 </tr>"
	
	
	'Move to the next record
	rsWeeklyPoll.MoveNext

Next



 %>
  </table>
  <br>
  <!-- Table to display next and previous links for other previous weekly poll questions -->
  <table width="370" border="0" cellspacing="1" cellpadding="1" align="center" height="21">
    <tr align="center"> 
      <td align="left" width="83"> 
        <%
      
'If the previous Poll Question Titles page number is higher than page 1 then display a back link    	
If intRecordPositionPageNum > 1 Then 
	Response.Write "	<a href=""previous_weekly_polls.asp?PagePosition=" &  intRecordPositionPageNum - 1  & """ target=""_self"">&lt;&lt;&nbsp;Prev</a>"   	     	
End If
      
      %>
      </td>
      <td width="201"> 
        <%
       
'If there are more previous Poll Question Titles to display then add a title to the other pages
If intRecordPositionPageNum > 1 or NOT rsWeeklyPoll.EOF Then
	Response.Write "	<span class=""text"">Previous Weekly Poll's</span>"
End If
       
      %>
      </td>
      <td align="right" width="76"> 
        <%
       
'If it is Not the End of the previous Poll Question Titles entries then display a next link for the next previous Poll Question Titles page      	
If NOT rsWeeklyPoll.EOF then   	
	Response.Write "	<a href=""previous_weekly_polls.asp?PagePosition=" &  intRecordPositionPageNum + 1  & """ target=""_self"">Next&nbsp;&gt;&gt;</a>"	   	
End If 


'Reset Server Objects
Set adoCon = Nothing
Set strCon = Nothing
rsWeeklyPoll.Close
Set rsWeeklyPoll = Nothing       
       
      %>
      </td>
    </tr>
  </table>
  <br>
  <a href="JavaScript:window.close()">Close Window</a><br>
  <br>
<%
'***** START WARNING - REMOVAL OR MODIFICATION OF THIS CODE WILL VIOLATE THE LICENSE AGREEMENT ****** 
  Response.Write("<a href=""http://www.webwizguide.info"" target=""_blank""><img src=""web_wiz_guide.gif"" width=""100"" height=""30"" border=""0"" alt=""Web Wiz Guide!""></a><br>")
'***** END WARNING - REMOVAL OR MODIFICATION OF THIS CODE WILL VIOLATE THE LICENSE AGREEMENT ****** 
%>
</div>
</body>
</html>