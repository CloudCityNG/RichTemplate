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
<title>Weekly Poll Admin Menu</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">

<!-- The Web Wiz Guide ASP Weekly Poll is written by Bruce Corkhill ©2001
     If you want your ASP Weekly Poll then goto http://www.webwizguide.info -->  

</head>

<body bgcolor="#FFFFFF" text="#000000">
<h1 align="center">Weekly Poll Admin Menu</h1>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="center">It is highly recommend for security reasons that you change the username and password.<br>
      This can be done from the <a href="configure_poll.asp" target="_self">Configure Weekly Poll Application</a> page.</td>
  </tr>
</table>
<br>
<table width="90%" border="0" cellspacing="0" cellpadding="0" align="center">
  <tr> 
    <td><a href="add_new_poll_form.asp" target="_self">Add New Weekly Poll</a><br>
      Enter a new Weekly Poll.<br>
      <br>
    </td>
  </tr>
  <tr> 
    <td><a href="weekly_poll_admin.asp" target="_self">Amend or Delete Weekly Polls</a><br>
      Amend spelling or other mistypes in Polls or delete Polls.<br>
      <br>
    </td>
  </tr>
  <tr> 
    <td height="47"><a href="configure_poll.asp" target="_self">Configure The Weekly Poll Application</a><br>
      Change the admin username and password, or set the font sizes and colours for the Weekly Poll.<br>
      <br>
    </td>
  </tr>
</table>
<div align="center"><br>
  <a href="http://www.webwizguide.info" target="_blank"><img src="web_wiz_guide.gif" width="100" height="30" border="0" alt="Web Wiz Guide!"></a> </div>
</body>
</html>
