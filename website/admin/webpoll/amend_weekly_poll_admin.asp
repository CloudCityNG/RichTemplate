<% Option Explicit %>
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

'Set the response buffer to true as we maybe redirecting
Response.Buffer = True



'If the session variable is False or does not exsist then redirect the user to the unauthorised user page
If Session("blnIsUserGood") = False or IsNull(Session("blnIsUserGood")) = True then
	'Redirect to unathorised user page
	Response.Redirect"unauthorised_user_page.htm"
End If
%>
<html> 
<head>
<META http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>Amend Weekly Poll</title>

<!-- The Web Wiz Guide ASP Weekly Poll is written by Bruce Corkhill ©2001
     If you want your ASP Weekly Poll then goto http://www.webwizguide.info -->  

<!-- Check the from is filled in correctly before submitting -->
<script  language="JavaScript">
<!-- Hide from older browsers...
function CheckPollForm () {
	
	if (document.frmNewWeeklyPoll.Question.value==""){
		alert("Please enter a Polling Question");
		document.frmNewWeeklyPoll.Question.focus();
		return false;
	}
	
	if (document.frmNewWeeklyPoll.choice1.value==""){
		alert("Please enter a Choice in Choice 1");
		document.frmNewWeeklyPoll.choice1.focus();
		return false;
	}
	
	if (document.frmNewWeeklyPoll.choice2.value==""){
		alert("Please enter a Choice in Choice 2");
		document.frmNewWeeklyPoll.choice2.focus();
		return false;
	}
		
	return true
}
// -->
</script>

</head>
<body bgcolor="#FFFFFF">
<div align="center"> 
  <h1>Amend Weekly Poll</h1>
  <a href="admin_menu.asp" target="_self">Return to the Weekly Poll Admin Menu</a><br>
  <a href="weekly_poll_admin.asp?PagePosition=<% = Request.QueryString("PagePosition") %>" target="_self">Select another Weekly Poll to Amend or Delete</a><br>
</div>
<%
'Dimension variables
Dim adoRec 		'Database Recordset Variable
Dim lngPollID 	'Holds the messageID number



'Read in message number to be amended as long as it is a number entered
If IsNumeric(Request.QueryString("amend")) Then	
	lngPollID = CLng(Request.QueryString("amend"))
End If

'Create recorset object
Set adoRec = Server.CreateObject("ADODB.Recordset")
	
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT * FROM tblPolls"
strSQL = strSQL & " WHERE id_no =" & CLng(lngPollID)
	
'Query the database
adoRec.Open strSQL, adoCon
	
'If there are no rcords in the database display an error message
If adoRec.EOF Then
	'Tell the user there are no records to show
	Response.Write "There are no records with the ID number " & lngPollID & " in the database"
	Response.Write "<br>Please check back later"
	Response.End
	
'Display the Weekly Poll entry
Else
%>
<br>
<form name="frmNewWeeklyPoll" method="post" action="amend_weekly_poll.asp?PagePosition=<% = Request.QueryString("PagePosition") %>" onSubmit="return CheckPollForm();">
  <table width="450" border="1" cellspacing="0" cellpadding="0" align="center" height="160" bgcolor="#CCCCCC">
      <tr> 
        <td height="2" width="619"> 
          <table width="100%" border="0" align="center" height="23">
            <tr align="left"> 
              <td colspan="2" height="30">*Indicates required 
                fields</td>
            </tr>
            <tr> 
              <td align="right" width="28%" height="14">Polling Question*: </td>
              <td height="14" width="72%"> 
                
              
       <input type="text" size="40" maxlength="200" name="Question" value="<% = adoRec("Question") %>">
              </td>
            </tr>
            <tr> 
              <td align="right" height="2" width="28%" class="arial">Choice 1*: 
              </td>
              <td height="2" width="72%"> 
                
              <input type="text" size="40" maxlength="50" name="choice1" value="<% = adoRec("Selection_1") %>">
              </td>
            </tr>
            <tr> 
              <td align="right" height="2" width="28%" class="arial">Choice 2*: 
              </td>
              <td height="2" width="72%"> 
                
              <input type="text" size="40" maxlength="50" name="choice2" value="<% = adoRec("Selection_2") %>">
              </td>
            </tr>
            <tr> 
              <td align="right" height="2" width="28%" class="arial">Choice 3: 
              </td>
              <td height="2" width="72%"> 
                
              <input type="text" size="40" maxlength="50" name="choice3" value="<% = adoRec("Selection_3") %>">
              </td>
            </tr>
            <tr> 
              <td valign="top" align="right" height="2" width="28%" class="arial">Choice 
                4: </td>
              <td height="2" width="72%"> 
                
              <input type="text" size="40" maxlength="50" name="choice4" value="<% = adoRec("Selection_4") %>">
              </td>
            </tr>
            <tr>
              <td align="right" height="2" width="28%" class="arial">Choice 5:&nbsp;</td>
              <td height="2" width="72%">
                
              <input type="text" size="40" maxlength="50" name="choice5" value="<% = adoRec("Selection_5") %>">
              </td>
            </tr>
            <tr> 
              <td align="right" height="2" width="28%" class="arial">Choice 6:&nbsp;</td>
              <td height="2" width="72%">
                
              <input type="text" size="40" maxlength="50" name="choice6" value="<% = adoRec("Selection_6") %>">
              </td>
            </tr>
            <tr> 
              <td align="right" height="2" width="28%" class="arial">Choice 7:&nbsp;</td>
              <td height="2" width="72%">
                
              <input type="text" size="40" maxlength="50" name="choice7" value="<% = adoRec("Selection_7") %>">
              </td>
            </tr>
            <tr> 
              <td valign="top" align="right" height="54" width="28%">
              <input type="hidden" name="PollID" value="<% = lngPollID %>">
            </td>
              <td height="54" width="72%"> 
                
              <p> 
                <input type="submit" name="Submit" value="Update New Weekly Poll">
                </p>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </form>
<%
End If
%>
</body>
</html>
<%
'Reset server objects
Set adoCon = Nothing
Set strCon = Nothing
Set adoRec = Nothing
%>