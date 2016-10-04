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
Dim strPollVote			'Holds the poll vote choice from the user
Dim strPollQuestion 		'Holds the poll question
Dim intPollIDNum		'Holds the poll ID Number
Dim intToatalPollVotes		'Holds the total number of votes cast
Dim dblPollVotePercentage	'Holds the percentage of the poll votes choice
Dim intSelectionLoopCounter	'Loop counter to display the poll selections
Dim intReadInVotesLoopCounter	'Loop counter to read in the number of votes cast
Dim blnSaveVote			'Boolean to set to false if the user has already voted


'Initialise variables
intPollIDNum = 0
intToatalPollVotes = 0
blnSaveVote = True

'If this is not a previous poll to be displayed then read in the details from the form and save the choice
If Request.QueryString("PollID") = "" Then

	'Read in the Poll ID Number
	intPollIDNum = CInt(Request.Form("id"))
	
	'Read in the Poll Vote user choice
	strPollVote = Request.Form("PollVote")
	
	
	'If a vote has been cast then record the vote
	If NOT strPollVote = "" Then
	
		'Call the Save Poll Votes Function (This must be called before any HTML is written as it sets a Cookie)
		blnSaveVote =  SavePollVotes(strPollVote, intPollIDNum)
	
	End If

'Else this is a previous poll to display so show get the poll id number to display
Else

	'Read in the Poll ID Number of the previous poll to display
	intPollIDNum = CInt(Request.QueryString("PollID"))

End If
%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>Weekly Poll Results</title>

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
<h2 align="center" class="heading">Weekly Poll</h2>
<%
      
      
'Read the Poll results from the database

'Create a connection odject
Set rsWeeklyPoll = Server.CreateObject("ADODB.Recordset")

'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT * FROM tblPolls "
If NOT intPollIDNum = 0 Then
	strSQL = strSQL & "WHERE tblPolls.id_no = " & intPollIDNum
End If
strSQL = strSQL & " ORDER By id_no DESC;"

'Query the database
rsWeeklyPoll.Open strSQL, strCon


'Check there is a weekly poll to display
If rsWeeklyPoll.EOF Then

	'If there is no weekly poll to display then display the appropriate message
	Response.Write vbCrLf & "<div align=""center""><span class=""text"">There is no Weekly Poll to display</span></div>"

'Else there is a weekly poll to write the HTML to display it
Else 	
	
	'Read in the polling question from the database
	strPollQuestion = rsWeeklyPoll("Question")
	
	'Read in the polling question ID No from the database
	intPollIDNum = CInt(rsWeeklyPoll("id_no"))
		
	'Loop round to read in the number of votes cast
	For intReadInVotesLoopCounter = 1 To 7
		
		'Read in the total number of votes cast
		intToatalPollVotes = intToatalPollVotes + CInt(rsWeeklyPoll("Votes_" & intReadInVotesLoopCounter & ""))
	Next
	
	  	
	  	
	'Display the HTML for the weekly poll	
	
	'If the user has already voted once before in this poll vote then display the approriate message
	If blnSaveVote = False Then
		'If the user has already voted then display an appriorate message
		Response.Write vbCrLf & "<div align=""center""><span class=""text"">Sorry, you have already voted in this Weeks Poll</span></div><br>"
	End If
	%>
	<a href="http://www.webwizguide.info"></a>
<table width="220" border="0" cellspacing="0" cellpadding="1" align="center" bgcolor="<% = strTableBorderColour %>">
  <tr> 
    <td> 
      <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="<% = strTableColour %>">
        <tr> 
          <td align="center"> 
            <%
	 
        Response.Write vbCrLf & "    <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""1"">"
        Response.Write vbCrLf & "      <tr>"
        
        'Display the poll question            
	Response.Write vbCrLf & "            <td align=""center"" height=""31"" class=""bold"">" & strPollQuestion & "</td>"
	
	Response.Write vbCrLf & "      </tr>"
	Response.Write vbCrLf & "     </table>"	
	
	Response.Write vbCrLf & "       <table width=""215"" border=""0"" cellspacing=""0"" cellpadding=""1"" align=""center"">"
	
	'Loop to display each of the selection choices for the poll
	For intSelectionLoopCounter = 1 To 7
	
		'If there is a Selection choice then display the HTML to show it
		If NOT rsWeeklyPoll("Selection_" & intSelectionLoopCounter) = "" Then
			
			'If there are no votes yet then format the percent by 0 otherwise the sums corse problems
			If intToatalPollVotes = 0 Then 
				dblPollVotePercentage = FormatPercent(0, 0)
				
			Else
				'Read in the the percentage of votes cast for the vote choice
				dblPollVotePercentage = FormatPercent((rsWeeklyPoll("Votes_" & intSelectionLoopCounter) / intToatalPollVotes), 0)
			End If
			
			'Display the selection choice results
			Response.Write vbCrLf & "  	     <tr>"
			Response.Write vbCrLf & "  	       <td colspan=""3"" class=""text"">" & rsWeeklyPoll("Selection_" & intSelectionLoopCounter) & "</td>"
			Response.Write vbCrLf & "  	     </tr>"
			Response.Write vbCrLf & "  	     <tr>"
			Response.Write vbCrLf & "  	     <td width=""200""><img src=""bar_graph_image.gif"" width=""" & dblPollVotePercentage & """ height=""15""></td>"
			Response.Write vbCrLf & "  	     <td width=""15"" align=""right"" class=""text"">" & dblPollVotePercentage & "</td>"
			Response.Write vbCrLf & "  	     </tr>"
			
		End If
	Next
	
	Response.Write vbCrLf & "	     </table>"
	
	'Display the total votes cast    
	Response.Write vbCrLf & "		<span class=""text"">Total Votes: " & intToatalPollVotes & "</span>"
	
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
    </td>
  </tr>
</table>
<a href="http://www.webwizguide.info"></a>
<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
  <tr>
    <td align="center" valign="bottom" height="29"><a href="previous_weekly_polls.asp?PagePosition=<% = Request.QueryString("PagePosition") %>" target="poll">Previous 
      Weekly Polls</a> </td>
  </tr>
  <tr>
    <td align="center" height="31"><a href="JavaScript:window.close()">Close Window</a></td>
  </tr>
</table>
<br>
<div align="center">
<%
'***** START WARNING - REMOVAL OR MODIFICATION OF THIS CODE WILL VIOLATE THE LICENSE AGREEMENT ****** 
  Response.Write("<a href=""http://www.webwizguide.info"" target=""_blank""><img src=""web_wiz_guide.gif"" width=""100"" height=""30"" border=""0"" alt=""Web Wiz Guide!""></a><br>")
'***** END WARNING - REMOVAL OR MODIFICATION OF THIS CODE WILL VIOLATE THE LICENSE AGREEMENT ****** 
%>
</div>
</body>
</html>
<%

'Sub function to save the users Poll Vote Choice
Private Function SavePollVotes(ByRef strPollVoteChoice, ByRef intPollIDNum)

	'Dimension variables
	Dim rsSavePollVotes 		'Database Recordset Variable
	Dim intVoteChoiceCount		'Holds the number of votes for the choice selected
	Dim intVotedIDNo		'Holds the number of any previous polls the user has voted in
	
	
	'Intialise variables
	SavePollVotes = True
	
	
	'Check the user has not already voted by reading in a cookie from there system
	'Read in the Poll ID number of the last poll the user has voted in
	intVotedIDNo = CInt(Request.Cookies("Poll")("PollID"))
	
	'Check to see if the user has voted in this poll
	If intVotedIDNo = intPollIDNum Then
	
		'If the user has already voted then return flase
		SavePollVotes = False
	
	'Else if the user has not already voted so increment the vote choice total and set a cookie on the users system
	Else
		
		'Read in the vote choice numbers from the database
		'Create a recordset odject
		Set rsSavePollVotes = Server.CreateObject("ADODB.Recordset")		
		
		'First we need to read in the present vote number so we can add 1 to it
		'Initalise the strSQL variable with an SQL statement to query the database
		strSQL = "SELECT * FROM tblPolls "
		strSQL = strSQL & " WHERE id_no =" & CInt(intPollIDNum)
		
		'Set the cursor type property of the recordset to Dynamic so we can navigate through the record set
		rsSavePollVotes.CursorType = 2
	
		'Set the Lock Type for the records so that the recordset is only locked when it is updated
		rsSavePollVotes.LockType = 3
		
		'Query the database
		rsSavePollVotes.Open strSQL, strCon
						
		
		'Read in the value of the vote choice selected form the database	
		intVoteChoiceCount = CInt(rsSavePollVotes(strPollVoteChoice))
		      	
		'Increment the vote choice by 1
		intVoteChoiceCount = intVoteChoiceCount + 1
		 		 
		    
		'Update the database with the new poll results	
		rsSavePollVotes.Fields(strPollVote) = CInt(intVoteChoiceCount)			
		rsSavePollVotes.Update
		
		'Re-run the query to read in the updated recordset from the database for the poll results
		rsSavePollVotes.Requery
						
	
		'Reset Server Objects 
		Set adoCon = Nothing
		rsSavePollVotes.Close
		Set rsSavePollVotes = Nothing 
	 	
		'If multiple votes for a poll are not allowed then save a cookie on the users system
		If blnMultipleVotes = False Then 
			'Write a cookie with the Poll ID number so the user cannot keep voting on this poll		
			'Write the cookie with the name Poll containing the value PollID
			Response.Cookies("Poll")("PollID") = intPollIDNum
			
			'Set the expiry date for 1 year
			Response.Cookies("Poll").Expires = Now() + 360
		End If

	End If
	
End Function

%>