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
Dim rsAddNewRecord		'Recordset object
Dim saryInputChoice(7)		'Array to hold the 5 different choices
Dim strInputQuestion		'Holds trhe question to be placed in the database
Dim intGetChoiceLoopCounter	'Loop counter
Dim intSQLChoiceLoopCounter	'Loop counter
Dim blnAddNewPoll		'Set to false if there is a problem

'Initialise variables
blnAddNewPoll = True


'If the session variable is False or does not exsist then redirect the user to the unauthorised user page
If Session("blnIsUserGood") = False or IsNull(Session("blnIsUserGood")) = True then
	'Redirect to unathorised user page
	Response.Redirect"unauthorised_user_page.htm"
End If


'Read in user deatils from the  form
strInputQuestion = Request.Form("Question")

'Replace harmful characters entred by the user that would course an SQL error when writing to the database
strInputQuestion = Replace(strInputQuestion, "'", "''")

'Loop through the choices to read them into an array
For intGetChoiceLoopCounter = 1 To 7
	saryInputChoice(intGetChoiceLoopCounter) = Request.Form("choice" & intGetChoiceLoopCounter)
	
	'Replace harmful characters entred by the user that would course an SQL error when writing to the database
	saryInputChoice(intGetChoiceLoopCounter) = Replace(saryInputChoice(intGetChoiceLoopCounter), "'", "''")
Next


'If there is no poll question or at least two selections then set the add poll to false
If strInputQuestion = "" OR saryInputChoice(1) = "" OR saryInputChoice(2) = "" Then blnAddNewPoll = False
 
 
'If the new poll is ok then add it to the database
If blnAddNewPoll = True Then
		
	'Create a recordset odject
	Set rsAddNewRecord = Server.CreateObject("ADODB.Recordset")
			
	'Initalise the strSQL variable with the SQL string
	strSQL = "INSERT INTO tblPolls ( Question, Selection_1, Selection_2, Selection_3, Selection_4, Selection_5, Selection_6, Selection_7 )"
	strSQL = strSQL & " VALUES"
	strSQL = strSQL & "('" & strInputQuestion 
	
	'Loop through the array holding the diffrent poll choices to add them to the SQL string to be added to the database
	For intSQLChoiceLoopCounter = 1 To 7		
			strSQL = strSQL & "', '" & saryInputChoice(intSQLChoiceLoopCounter)		
	Next
	
	strSQL = strSQL & "');"
			
	'Write to the database
	adoCon.Execute(strSQL)
		 
	'Reset Sever Objects 
	Set rsAddNewRecord = Nothing
	Set strCon = Nothing
	Set adoCon = Nothing
End If

%>

<html> 
<head>
<META http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>Add New Weekly Poll</title>


<!-- The Web Wiz Guide ASP Weekly Poll is written by Bruce Corkhill ©2001
    	 If you want your ASP Weekly Poll then goto http://www.webwizguide.info --> 

	   
<body bgcolor="#FFFFFF" text="#000000" link="#0000CC" vlink="#0000CC" alink="#FF0000">
<div align="center"> 
  <h1>Add New Weekly Poll</h1>
  <br>
  <%
  If blnAddNewPoll = True Then 
  	Response.Write " The new Weekly Poll has been entered into the Database"
  Else
  	Response.Write " The new Weekly Poll has NOT entered into the Database as there are problems with it<br><br><a href=""JavaScript:history.back(1)"">Try again</a>"
  End If
  %>
  <br>
  <br>
  <br>
  <a href=".asp" target="_self"> </a><a href="admin_menu.asp" target="_self">Return 
  to The Weekly Poll Admin Menu</a><br>
  <br>
</div>
<div align="center"> <br>
</div>
</body>
</html>
