<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>Take A Poll</title>
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

<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">
<!-----------CODE FOR PLAYER POLL----------------------------->


<!-- #INCLUDE FILE="adovbs.inc" -->



<OBJECT RUNAT="server" PROGID="ADODB.Connection" id="cnnpoll"> </OBJECT>
<OBJECT RUNAT="server" PROGID="ADODB.Recordset" id="rsPoll" width="14" height="14"> </OBJECT>

<%
Dim SCRIPT_NAME   
SCRIPT_NAME = Request.ServerVariables("SCRIPT_NAME")

Dim strAction     
Dim iPoll         

Dim iVote         

Dim iTotalVotes  

Dim strSQL        
Dim I            



' Get action choice and format it for easy comparison
strAction = LCase(CStr(Request.QueryString("action")))

' Set to default of showing the question unless results is passed in
' indicating I should show the results.  This way I don't have to
' worry about it since strAction is always question or results.
If strAction <> "results" Then strAction = "question"


' Get poll id
iPoll = Request.QueryString("pid")

' Validate and refine iPoll
If IsNumeric(iPoll) Then 
	If iPoll > 0 Then
		iPoll = CInt(iPoll)
	Else
		'strAction = "Error: Invalid Poll Id!"
		
		' I commented out the above and let this slide so you can
		' just request poll.asp and get back something.
		iPoll = request("id")
	End If
Else
	' If it's not numeric I should just error out which I do
	' but since this script will handle multiple polls I've
	' set it up to take "all" as a parameter when displaying
	' results so you can get to see the whole set of results
	' if you want to.
	If LCase(iPoll) = "all" And strAction = "results" Then
		iPoll = "all"
	Else
		strAction = "Error: Invalid Poll Id!"
	End If
End If%>

<!--#include file="conn.asp"-->
<%
' Set the default for our SQL string.
' This is used all the time unless the whole "all" thing comes into play
strSQL = "SELECT * FROM polls WHERE id=" & iPoll & ";"

' Do whatever action is appropriate "question" or "results"
' Otherwise error out on the else option.
Select Case strAction
	Case "question"
		' Open our RS to show the choices
		rsPoll.Open strSQL, con, adOpenStatic, adLockReadOnly, adCmdText

		If Not rsPoll.EOF Then
			' Show the voting form
			' You'll need to format this to your liking
			%>
<div align="left">
			<TABLE width="100%" BORDER="1" CELLSPACING="0" CELLPADDING="0" bordercolor="#9DB5D1">
				<TR>
						<TD align="left" BGCOLOR="#ffffff" valign="top">
                          <p align="center"><img border="0" src="images/toadpoll.gif" align="left"></p>
                        </td>
						</tr>
    <tr>
      <td width="533" align="right" colspan="3" BGCOLOR="#3D4A7E" >
        <p align="center"><b><font color="#FFFFFF" face="Arial" size="3">TAKE THIS POLL</font></b>
      </td>
    </tr>
				<TR>
				<TD align="right" BGCOLOR="#C0C0C0" bordercolor="#9DB5D1"><font face="Verdana" size="2"><A HREF="<%= SCRIPT_NAME %>?poll=yes&action=results&pid=<%= iPoll %>">View Results</A><br></font></TD>
				<tr>	<TD BGCOLOR="#ffffff" ALIGN="left"><table><tr><td valign="top"></td><td><font face="Verdana" size="3"></B><FONT face="verdana" size="3" COLOR="black"><B><%= rsPoll.Fields("question").Value %></B></FONT><br><br></TD></tr></table>
</TD>
					</tr>
				
				
				
				<TR>
					<TD  BGCOLOR="#FFFFFF" ALIGN="left">
                        <p align="left">
						<%
						' Loop 1 to 5 since there are only 5 possible choices set up in the DB
						For I = 1 to 5

							If Not IsNull(rsPoll.Fields("questiontext" & I).Value) Then
								' Some spacing if needed
								If I <> 1 Then Response.Write""
								' Show choices hyperlinked to the vote portion of the script.
								%>
								<FONT face="verdana" size="1" COLOR="red">Answer:  </font><FONT face="verdana" size="1"><A HREF="<%= SCRIPT_NAME %>?poll=yes&action=results&pid=<%= iPoll %>&vote=<%= I %>"><FONT size="2" face="arial"><%= rsPoll.Fields("choice" & I).Value %></A><br><br></FONT>								<%
							End If
						Next 
						%>
                    </p>
					</TD>
				</TR>
			</TABLE>
			  <TABLE WIDTH="100%">		<tr><td colspan=2 bgcolor="#FFFFFF" align="right"><form><img src="images/print.gif" value="Print Page" onClick="myprint()"><img src="images/close.gif" value="Close Window" onClick="myclose()"> </center></form>
</td></tr></TABLE>
</div>
			<%
		Else
			Response.Write "Error: Invalid Poll Id!"
		End If
		
		rsPoll.Close

	Case "results"

		' If we're processing a vote then we need to know what it is so:
		' Get The Vote!
		iVote = Request.QueryString("vote")

		' Validate and refine iVote.  Setting to 0 if invalid!
		If IsNumeric(iVote) Then
			iVote = CInt(iVote)
							
			If Not(1 <= iVote And iVote <= 5) Then
				iVote = 0
			End If
		Else
			iVote = 0
		End If

		' If iVote = 0 or iPoll = "all" then I'm just showing the results
		' Otherwise we need to log them first
		If iVote <> 0 And iPoll <> "all" Then
			' Log results
		
			' Open our RS to record the choice
			' Notice that it's not static or read only.
			rsPoll.Open strSQL, con, adOpenKeyset, adLockPessimistic, adCmdText

			If Not rsPoll.EOF Then
				' Check to be sure they haven't already voted in this session
				' This prevents the refresh button from resubmitting the info
				' You could make this a lot more sophisticated and useful if
				' you had some reason to.  I just don't really care and do it
				' mainly for the refresh button issue
				If Session("AlreadyVoted") <> 1 Then
					rsPoll.Fields("votes" & iVote).Value = rsPoll.Fields("votes" & iVote).Value + 1
					rsPoll.Update

					' Set Flag to prevent revoting
					'Session("AlreadyVoted") = 1
				End If

			Else
				Response.Write "Error: Invalid Poll Id!"
			End If

			rsPoll.Close
		Else
			If iPoll = "all" Then
				' Override our standard SQL string to show all otherwise it'll work fine.
				strSQL = "SELECT * FROM polls ORDER BY id;"
			End If
		End If

		' I've already processed any entry we needed to and set up for all condition.
		' Time to show the results!

		' Open recordset to show results.
		rsPoll.Open strSQL, con, adOpenKeyset, adLockPessimistic, adCmdText

		If Not rsPoll.EOF Then
			' For each poll show results.
			' Normally just one, but I built it so it'd work for "all" too.
			Do While Not rsPoll.EOF
				' Tally the total votes and store it
				iTotalVotes = rsPoll.Fields("votes1").Value + _
								rsPoll.Fields("votes2").Value + _
								rsPoll.Fields("votes3").Value + _
								rsPoll.Fields("votes4").Value + _
								rsPoll.Fields("votes5").Value

				' Show Results - Format to your liking!
				%>
<div align="left">
				<TABLE width="60%" BORDER="0" CELLSPACING="0" CELLPADDING="0">
								<TR>
						<TD align="left" BGCOLOR="#ffffff" valign="top">
                          <p align="center"><img border="0" src="images/toadpoll.gif" align="left"></p>
                        </td>
						</tr>
					<TR>
						<TD BGCOLOR="#ffffff"><B><FONT face="Verdana" COLOR="#000000">Poll Results</FONT></B> <FONT face="Verdana" COLOR="#000000"> (based on <%= iTotalVotes %> votes)</FONT><br><br></TD>
					</TR>
					<TR>
						<TD BGCOLOR="#ffffff" ALIGN="left"><FONT face="arial" COLOR="black"><p><b><%= rsPoll.Fields("question").Value %></b></p><br></FONT></TD>
					</TR>
					<TR>
						<TD BGCOLOR="#ffffff" ALIGN="left">
							<%
							' Loop over choices
							For I = 1 to 5
								If Not IsNull(rsPoll.Fields("choice" & I).Value) Then
									' The math was giving me trouble when I divided 0 by 1 so I avoided the situation
									If rsPoll.Fields("votes" & I).Value = 0 Then
										%>
										<FONT face="verdana" size="2" COLOR="red"><%= rsPoll.Fields("choice" & I).Value %></FONT> <FONT face="verdana" COLOR="black"><B><%= FormatPercent(0, 1) %></B><br><br></FONT>
										<table width="100%" border="1"><tr><td bgcolor="#6898C8" width="<%= FormatPercent(0, 1) %>">&nbsp;</td><td width="100%"></td></tr></table>
										
										<%
									Else
										%>
										<FONT face="verdana" size="2" COLOR="red"><%= rsPoll.Fields("choice" & I).Value %></FONT><br>
										<table width="100%" border="1"><tr><td bgcolor="#6898C8" width="<%= FormatPercent((rsPoll.Fields("votes" & I).Value / iTotalVotes), 1) %>"><FONT face="verdana" COLOR="white"><B><%= FormatPercent((rsPoll.Fields("votes" & I).Value / iTotalVotes), 1) %></B></FONT></td><td width="100%"></td></tr></table>
										
										<%
									End If
								End If
							Next 'I
							%>
						</TD>
					</TR>
				</TABLE>
</div>
<div align="left">
  <TABLE WIDTH="100%">		<tr><td colspan=2 bgcolor="#FFFFFF" align="right"><form><img src="images/print.gif" value="Print Page" onClick="myprint()"><img src="images/close.gif" value="Close Window" onClick="myclose()"> </center></form>
</td></tr></TABLE>
</div>
	<%
				rsPoll.MoveNext
			Loop
		Else
			Response.Write "Error: Invalid Poll Id!"
		End If

		rsPoll.Close

	Case Else ' "error"
		' OK so this is pretty lame error handling, but it
		' catches most stupid things and warns the user.
		Response.Write strAction
End Select

con.Close

' I can't set the DB objects to nothing because I never created them.
' This syntax is just weird.
%>

<!-----------END CODE FOR PLAYER POLL----------------------------->       
</body>

</html>
