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
%>
<!--#include file="common.inc" -->
<%
'Dimension variables
Dim rsWeeklyPoll 		'Database Recordset Variable to hold the eweekly poll recordset
Dim strPollQuestion 		'Holds the poll question
Dim intPollIDNum		'Holds the Poll ID Num
Dim intSelectionLoopCounter	'Loop counter to display the poll selections
Dim intVotedIDNo		'Holds the ID Number the last vote the user voted in if there is one
Dim blnCheckForPoll		'Set to false if there are no weekly polls in the database
Dim blnAlreadyVoted		'Set to true if the user has voted in this weeks poll
Dim intTotalPollVotes		'Holds the total number of votes cast
Dim intReadInVotesLoopCounter	'Loop counter variable
Dim dblPollVotePercentage	'Holds the percentage of poll votes


'Intilaise variables
blnCheckForPoll = True
blnAlreadyVoted = False
intPollIDNum = 1


'Create a recordset object
Set rsWeeklyPoll = Server.CreateObject("ADODB.Recordset")

'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT TOP 1 tblPolls.* FROM tblPolls ORDER By tblPolls.id_no DESC;"

'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
rsWeeklyPoll.CursorType = 2
	
'Set the Lock Type for the records so that the record set is only locked when it is deleteed
rsWeeklyPoll.LockType = 3

'Query the database
rsWeeklyPoll.Open strSQL, strCon



'Check there is a weekly poll to display
If rsWeeklyPoll.EOF Then

	'If there is no weekly poll in the database chenge the blnCheckForPoll variable to false
	blnCheckForPoll = False	

Else 
	
	'Read in the polling question from the database
	strPollQuestion = rsWeeklyPoll("Question")	
	
	'Read in the Weekly Poll ID number
	intPollIDNum = CInt(rsWeeklyPoll("id_no"))
	
	'Loop round to read in the number of votes cast
	For intReadInVotesLoopCounter = 1 To 7
		
		'Read in the total number of votes cast
		intTotalPollVotes = intTotalPollVotes + CInt(rsWeeklyPoll("Votes_" & intReadInVotesLoopCounter & ""))
	Next
	
	'Check the user has not already voted by reading in a cookie from there system
	'Read in the Poll ID number of the last poll the user has voted in
	intVotedIDNo = CInt(Request.Cookies("Poll")("PollID"))
	
	'If the user has already voted then redirect them to the results page
	If intVotedIDNo = intPollIDNum Then blnAlreadyVoted = True
	
End If
%>
<script  language="JavaScript">
<!-- Hide from older browsers...

//Function to open pop up window
function openWin(theURL,winName,features) {
  	window.open(theURL,winName,features);
}

// -->
</script>

<style type="Text/css">
<!--
.pollText {font-family: <% = strTextType %>; font-size: <% = intTextSize %>px; color: <% = strTextColour %>}
.smPollText {font-family: <% = strTextType %>; font-size: <% = intSmTextSize %>px; color: <% = strTextColour %>;}
a.Poll {font-family: <% = strTextType %>; font-size: <% = intSmTextSize %>px; color: <% = strLinkColour %>}
a.Poll:hover {font-family: <% = strTextType %>; font-size: <% = intSmTextSize %>px; color: <% = strHoverLinkColour %>}
a.Poll:visited {font-family: <% = strTextType %>; font-size: <% = intSmTextSize %>px; color: <% = strVisitedLinkColour %>}
a.Poll:visited:hover {font-family: <% = strTextType %>; font-size: <% = intSmTextSize %>px; color: <% = strHoverLinkColour %>}
-->
</style>
<table width="145" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = strTableBorderColour %>">
  <tr> 
    <td> 
      <table width="100%" border="0" cellspacing="0" cellpadding="2" bgcolor="<% = strTableColour %>">
        <form method="post" action="weekly_poll.asp" target="poll" onSubmit="window.open('', 'poll', 'toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=505,height=400')">
        <tr> 
          <td> 
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr align="center"> 
                <td colspan="2"> 
                  <table width="100%" border="0" cellspacing="0" cellpadding="3">
                    <tr> 
                      <td align="center" class="pollText" style="font-weight: bold;">Weekly Poll</td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr> 
                <td colspan="2"><span class="smPollText" style="font-size: <% = intSmTextSize + 1 %>;"><% = strPollQuestion %></span><a href="http://www.webwizguide.info"></a></td>
              </tr>
              <%
'If there is a weekly poll to display then write the HTML to display it
If blnCheckForPoll = True Then

	'Loop to display each of the selection choices for the poll
	For intSelectionLoopCounter = 1 To 7
		
		'If there is a Selection choice then display the HTML to show it
		If NOT rsWeeklyPoll("Selection_" & intSelectionLoopCounter) = "" Then
		
			If blnAlreadyVoted = False Then		
				'Display a radio button for the selection choice
				Response.Write vbCrLf & "	<tr><td colspan=""2""><span class=""smPollText""><input type=""radio"" name=""PollVote"" value=""Votes_" & intSelectionLoopCounter & """>"
					
				'Display the selection choice
				Response.Write vbCrLf & "	" & rsWeeklyPoll("Selection_" & intSelectionLoopCounter) & "</span></td></tr>"
				
			Else
			
				'If there are no votes yet then format the percent by 0 otherwise the sums corse problems
				If intTotalPollVotes = 0 Then 
					dblPollVotePercentage = FormatPercent(0, 0)
					
				Else
					'Read in the the percentage of votes cast for the vote choice
					dblPollVotePercentage = FormatPercent((rsWeeklyPoll("Votes_" & intSelectionLoopCounter) / intTotalPollVotes), 0)
				End If
				
				'Display the selection choice results
				Response.Write vbCrLf & "  	     <tr>"
				Response.Write vbCrLf & "  	       <td colspan=""2""><span  class=""smPollText"">" & rsWeeklyPoll("Selection_" & intSelectionLoopCounter) & "</span></td>"
				Response.Write vbCrLf & "  	     </tr>"
				Response.Write vbCrLf & "  	     <tr>"
				Response.Write vbCrLf & "  	     <td width=""110""><img src=""bar_graph_image.gif"" width=""" & dblPollVotePercentage & """ height=""11""></td>"
				Response.Write vbCrLf & "  	     <td width=""25"" align=""right""><span class=""smPollText"">" & dblPollVotePercentage & "</span></td>"
				Response.Write vbCrLf & "  	     </tr>"
			
			End If
		End If
	Next	
			
	%>
              <tr align="center"> 
                <td colspan="2"> 
                  <%
        'If the user has not voted then display a vote button
        If blnAlreadyVoted = False Then	
        %>
                  <input type="hidden" name="id" value="<% = intPollIDNum %>">
                  <input type="submit" value="Vote" name="submit">
                  <%
        'Display the total number of votes
        Else
	        'Display the total votes cast    
		Response.Write vbCrLf & "		<span  class=""smPollText"">Total Votes: " & intTotalPollVotes & "</span>"
	End If

'Else if there is no weekly poll display the appropriate message
Else	
	Response.Write vbCrLf & "<tr><td><span class=""pollText"">There is no Weekly Poll to display</span></td></tr>"
End If	


'Reset Server Objects
rsWeeklyPoll.Close
Set rsWeeklyPoll = Nothing 
Set adoCon = Nothing
Set strCon = Nothing
      
%>
                </td>
              </tr>
              <tr align="center"> 
                <td colspan="2"><a href="JavaScript:openWin('weekly_poll.asp?ID=<% = intPollIDNum %>','poll','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=505,height=400')" class="Poll" style="font-size: 10px; pollText-decoration: none;">View Results</a><a href="http://www.webwizguide.info"></a></td>
              </tr>
            </table>
          </td>
        </tr>
        </form>
      </table>
    </td>
  </tr>
</table>

