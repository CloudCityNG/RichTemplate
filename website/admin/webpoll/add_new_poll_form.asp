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




'If the session variable is False or does not exsist then redirect the user to the unauthorised user page
If Session("blnIsUserGood") = False or IsNull(Session("blnIsUserGood")) = True then
	'Redirect to unathorised user page
	Response.Redirect"unauthorised_user_page.htm"
End If
%>
<html>
<head>
<title>Add New Weekly Poll Form</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">

<!-- The Web Wiz Guide ASP Weekly Poll is written by Bruce Corkhill ©2001
    	 If you want your ASP Weekly Poll then goto http://www.webwizguide.info --> 

</head>

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

<body bgcolor="#FFFFFF" text="#000000" link="#0000CC" vlink="#0000CC" alink="#FF0000">
<div align="center"> 
  <h1>Add New Weekly Poll</h1>
  <a href="admin_menu.asp" target="_self">Return to The Weekly Poll Admin Menu</a><br>
  
  <form name="frmNewWeeklyPoll" method="post" action="add_new_weekly_poll.asp" onSubmit="return CheckPollForm();">
    <table width="450" border="1" cellspacing="0" cellpadding="0" align="center" height="160" bgcolor="#CCCCCC">
      <tr> 
        <td height="2" width="619"> 
          <table width="100%" border="0" align="center" height="23">
            <tr align="left"> 
              <td colspan="2" class="arial_sm2" height="30">*Indicates required 
                fields</td>
            </tr>
            <tr> 
              <td align="right" width="28%" height="14">Polling Question*: </td>
              <td height="14" width="72%"> 
                
        <input type="text" size="40" maxlength="200" name="Question">
              </td>
            </tr>
            <tr> 
              <td align="right" height="2" width="28%" class="arial">Choice 1*: 
              </td>
              <td height="2" width="72%"> 
                <input type="text" size="40" maxlength="50" name="choice1">
              </td>
            </tr>
            <tr> 
              <td align="right" height="2" width="28%" class="arial">Choice 2*: 
              </td>
              <td height="2" width="72%"> 
                <input type="text" size="40" maxlength="50" name="choice2">
              </td>
            </tr>
            <tr> 
              <td align="right" height="2" width="28%" class="arial">Choice 3: 
              </td>
              <td height="2" width="72%"> 
                <input type="text" size="40" maxlength="50" name="choice3">
              </td>
            </tr>
            <tr> 
              <td valign="top" align="right" height="2" width="28%" class="arial">Choice 
                4: </td>
              <td height="2" width="72%"> 
                <input type="text" size="40" maxlength="50" name="choice4">
              </td>
            </tr>
            <tr>
              <td align="right" height="2" width="28%" class="arial">Choice 5:&nbsp;</td>
              <td height="2" width="72%">
                <input type="text" size="40" maxlength="50" name="choice5">
              </td>
            </tr>
            <tr> 
              <td align="right" height="2" width="28%" class="arial">Choice 6:&nbsp;</td>
              <td height="2" width="72%">
                <input type="text" size="40" maxlength="50" name="choice6">
              </td>
            </tr>
            <tr> 
              <td align="right" height="2" width="28%" class="arial">Choice 7:&nbsp;</td>
              <td height="2" width="72%">
                <input type="text" size="40" maxlength="50" name="choice7">
              </td>
            </tr>
            <tr> 
              <td valign="top" align="right" height="54" width="28%">&nbsp; </td>
              <td height="54" width="72%"> 
                <p> 
                  <input type="reset" name="Reset" value="Clear Form">
                  <input type="submit" name="Submit" value="Add New Weekly Poll">
                </p>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </form>
</div>
</body>
</html>
