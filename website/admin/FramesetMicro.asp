




<!--#include file="db_connection.asp"-->

<%URL = Request.ServerVariables("server_name")%>


// You can find instructions for this file here:
// http://www.treeview.net

// Decide if the names are links or just the icons
USETEXTLINKS = 1  //replace 0 with 1 for hyperlinks

// Decide if the tree is to start all open or just showing the root folders
STARTALLOPEN = 0 //replace 0 with 1 to show the whole tree

ICONPATH = 'images/' //change if the gif's folder is a subfolder, for example: 'images/'

foldersTree = gFld("<font color=#3054A7><b>MicroSites</b></font>", "")

<% 

	X=1

	'**********  GET SECTION HEADINGS FROM DB  **********

	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString

	WEBSECTIONSQL = "SELECT * FROM MICROSITE_PARTNERS WHERE BOLARCHIVE =0 ORDER BY LASTNAME "
	SET RS = CON.EXECUTE (WEBSECTIONSQL)
	
	WHILE NOT RS.EOF%>

  	aux1 = insFld(foldersTree, gFld("<font size='1'><b>&nbsp;<%=RS("FOLDERNAME")%></b><td> </td>", "/admin/microSites/rawForm.asp?partnerid=<%=RS("PARTNERID")%>&admin=true"))

<%

	RS.MOVENEXT
	
	WEND
  %>