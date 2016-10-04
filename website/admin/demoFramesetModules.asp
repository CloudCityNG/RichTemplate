




<!--#include file="db_connection.asp"-->

<%URL = Request.ServerVariables("server_name")%>


// You can find instructions for this file here:
// http://www.treeview.net

// Decide if the names are links or just the icons
USETEXTLINKS = 1  //replace 0 with 1 for hyperlinks

// Decide if the tree is to start all open or just showing the root folders
STARTALLOPEN = 0 //replace 0 with 1 to show the whole tree

ICONPATH = 'images/' //change if the gif's folder is a subfolder, for example: 'images/'


foldersTree = gFld("<b><font color=#3054A7>My Web Site Modules</b></font>", "richtemplate_listmodules.asp")

<% 

	X=1

	'**********  GET SECTION HEADINGS FROM DB  **********
	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString		
	
	
	ALLOW_MODULES = SESSION("ALLOW_MODULES")
	IF ALLOW_MODULES <>"" THEN
	


	WEBSECTIONSQL = "SELECT * FROM MODULES WHERE ONLINE = 1 and ID in (" & ALLOW_MODULES & ") ORDER By ModuleName"
	'response.write websectionsql
	SET RS = CON.EXECUTE (WEBSECTIONSQL)
	
	WHILE NOT RS.EOF%>

  	aux1 = insFld(foldersTree, gFld("<font size='1'><b>&nbsp;<%=RS("MODULENAME")%></b></font>", "<%=RS("ModuleLocation_Admin")%>"))

  	
  		<%

	RS.MOVENEXT
	
	WEND
	
	END IF%>


<%
'***************** DISPLAY ALL MODULES THAT ARE INACTIVE *****************	
	
	
	
	'**********  GET SECTION HEADINGS FROM DB  **********


	

	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString

	WEBSECTIONSQL = "SELECT * FROM MODULES WHERE ONLINE <> 1 ORDER BY MODULENAME"
	SET RS = CON.EXECUTE (WEBSECTIONSQL)
	
	WHILE NOT RS.EOF%>

  	aux1 = insFld(foldersTree, gFld("<font size='1' color='#808080'><b>&nbsp;<%=RS("MODULENAME")%></b></font>", ""))

  	
  		<%

	RS.MOVENEXT
	
	WEND

	
	
	
	
  %>