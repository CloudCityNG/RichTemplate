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

Dim adoCon 			'Database Connection Variable
Dim rsConfiguration		'Holds the configuartion recordset
Dim strCon			'Holds the Database driver and the path and name of the database
Dim strSQL			'Holds the SQL query for the database
Dim strBgColour			'Holds the background colour
Dim strTextColour		'Holds the text colour
Dim strTextType			'Holds the font type
Dim intTextSize			'Holds the font size 
Dim intSmTextSize		'Holds the small font size
Dim strLinkColour		'Holds the link colour
Dim strTableColour		'Holds the table colour
Dim strTableBorderColour	'Holds the table border colour
Dim strVisitedLinkColour	'Holds the visited link colour
Dim strHoverLinkColour		'Holds the mouse over link colour
Dim blnMultipleVotes		'Set to true if the user can vote multiple time

blnMultipleVotes = False

'Create database connection

'Create a connection odject
Set adoCon = Server.CreateObject("ADODB.Connection")
			 
'------------- If you are having problems with the script then try using a diffrent driver or DSN by editing the lines below --------------
			 
'Database connection info and driver (if this driver does not work then comment it out and use one of the alternative drivers)
strCon = "DRIVER={Microsoft Access Driver (*.mdb)}; DBQ=" & Server.MapPath("weekly_poll.mdb")

'Database driver for Brinkster
'strCon = "DRIVER={Microsoft Access Driver (*.mdb)}; DBQ=" & Server.MapPath("/USERNAME/db/weekly_poll.mdb") 'This one is for Brinkster users place your Brinster username where you see USERNAME

'Alternative drivers faster than the generic access one above
'strCon = "Provider=Microsoft.Jet.OLEDB.3.51; Data Source=" & Server.MapPath("weekly_poll.mdb") 'This one is if you convert the database to Access 97
'strCon = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & Server.MapPath("weekly_poll.mdb")  'This one is for Access 2000/2002

'If you wish to use DSN then comment out the driver above and uncomment the line below (DSN is slower than the above drivers)
'strCon = "DSN = DSN_NAME" 'Place the DSN where you see DSN_NAME

'---------------------------------------------------------------------------------------------------------------------------------------------

'Set an active connection to the Connection object
adoCon.Open strCon



'Read in the Weekly Poll configuration
'Intialise the ADO recordset object
Set rsConfiguration = Server.CreateObject("ADODB.Recordset")

'Initialise the SQL variable with an SQL statement to get the configuration details from the database
strSQL = "SELECT tblConfiguration.* From tblConfiguration;"

'Query the database
rsConfiguration.Open strSQL, strCon

'If there is config deatils in the recordset then read them in
If NOT rsConfiguration.EOF Then

	'Read in the configuration details from the recordset
	strBgColour = rsConfiguration("bg_colour")
	strTextColour = rsConfiguration("text_colour")
	strTextType = rsConfiguration("text_type")
	intTextSize = CInt(rsConfiguration("text_size"))
	intSmTextSize = CInt(rsConfiguration("sm_text_size"))
	strLinkColour = rsConfiguration("links_colour")
	strTableColour = rsConfiguration("table_colour")
	strTableBorderColour = rsConfiguration("table_border_colour")
	strVisitedLinkColour = rsConfiguration("visited_links_colour")
	strHoverLinkColour = rsConfiguration("active_links_colour")
	blnMultipleVotes = CBool(rsConfiguration("multiple_votes"))
End If

'Reset server object
rsConfiguration.Close
Set rsConfiguration = Nothing
%>