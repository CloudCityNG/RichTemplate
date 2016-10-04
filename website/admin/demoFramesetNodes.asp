




<!--#include file="db_connection.asp"-->

<%URL = Request.ServerVariables("server_name")%>


// You can find instructions for this file here:
// http://www.treeview.net

// Decide if the names are links or just the icons
USETEXTLINKS = 1  //replace 0 with 1 for hyperlinks

// Decide if the tree is to start all open or just showing the root folders
STARTALLOPEN = 0 //replace 0 with 1 to show the whole tree

ICONPATH = 'images/' //change if the gif's folder is a subfolder, for example: 'images/'

foldersTree = gFld("<font color=#3054A7><b>My Website</b></font>", "http://<%=URL%>")

<% 

	X=1

	'**********  GET SECTION HEADINGS FROM DB  **********

	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString
	
	
	'Check session control to see what nav we are working on session(member) is for members only, session(education) is for the 
	'education site
	
	
	If Session("secure_members")=True Then
	
		WEBSECTIONSQL = "SELECT * FROM WEBINFO WHERE PAGELEVEL =1 AND SECURE_MEMBERS=1 ORDER BY RANK"
		SET RS = CON.EXECUTE (WEBSECTIONSQL)
	
	ElseIf Session("secure_education")=True Then
	
		WEBSECTIONSQL = "SELECT * FROM WEBINFO WHERE PAGELEVEL =1 AND SECURE_EDUCATION=1 ORDER BY RANK"
		SET RS = CON.EXECUTE (WEBSECTIONSQL)
	
	Else
	
		WEBSECTIONSQL = "SELECT * FROM WEBINFO WHERE PAGELEVEL =1  AND (SECURE_MEMBERS Is Null OR SECURE_MEMBERS = 0) and (SECURE_EDUCATION IS NULL OR SECURE_EDUCATION = 0)   ORDER BY RANK"
		SET RS = CON.EXECUTE (WEBSECTIONSQL)
	
	End IF
	
	
	WHILE NOT RS.EOF%>

<%'*********************** CHECK RENAME SECTION PERMISSION FOR USER *********************** 

	IF SESSION("ALLOW_RENAMESECTION")= TRUE  THEN
	
		IF RS("PENDING")=TRUE THEN%>	

  		aux1 = insFld(foldersTree, gFld("<font size='1' color='#808080'><b>&nbsp;<%=RS("NAME")%></b></font><td><a target='_self' href='richtemplate_changerank.asp?rank=<%=RS("rank")%>&pageid=<%=rs("id")%>&sectionid=<%=RS("id")%>&posit=moveup'><img border='0' src='images/uparrow_small.gif' alt='Move Up'></a> <a target=_self href='richtemplate_changerank.asp?rank=<%=RS("rank")%>&pageid=<%=rs("id")%>&sectionid=<%=RS("id")%>&posit=movedown'><img border='0' src='images/downarrow_small.gif' alt='Move Down'></a></td>", "richtemplate_list_pages.aspx?sectionid=<%=RS("id")%>&color=1"))
	
		<%ELSEIF RS("LINKONLY")<> 0 THEN%>
		
		aux1 = insFld(foldersTree, gFld("<font size='1' color='#3055A9'><b>&nbsp;<%=RS("NAME")%></b></font><td><a target='_self' href='richtemplate_changerank.asp?rank=<%=RS("rank")%>&pageid=<%=rs("id")%>&sectionid=<%=RS("id")%>&posit=moveup'><img border='0' src='images/uparrow_small.gif' alt='Move Up'></a> <a target=_self href='richtemplate_changerank.asp?rank=<%=RS("rank")%>&pageid=<%=rs("id")%>&sectionid=<%=RS("id")%>&posit=movedown'><img border='0' src='images/downarrow_small.gif' alt='Move Down'></a></td>", "richtemplate_list__pages.aspx?sectionid=<%=RS("id")%>&color=1"))
		
		<%ELSE%>
		
		aux1 = insFld(foldersTree, gFld("<font size='1'><b>&nbsp;<%=RS("NAME")%></b></font><td><a target='_self' href='richtemplate_changerank.asp?rank=<%=RS("rank")%>&pageid=<%=rs("id")%>&sectionid=<%=RS("id")%>&posit=moveup'><img border='0' src='images/uparrow_small.gif' alt='Move Up'></a> <a target=_self href='richtemplate_changerank.asp?rank=<%=RS("rank")%>&pageid=<%=rs("id")%>&sectionid=<%=RS("id")%>&posit=movedown'><img border='0' src='images/downarrow_small.gif' alt='Move Down'></a></td>", "richtemplate_list_pages.aspx?sectionid=<%=RS("id")%>&color=1"))
		
		<%END IF%>


	<%ELSE%>
	
  		aux1 = insFld(foldersTree, gFld("<font size='1'><b>&nbsp;<%=RS("NAME")%></b></font>", "richtemplate_list_pages.aspx?sectionid=<%=RS("id")%>"))


	<%END IF%><%

	RS.MOVENEXT
	
	WEND
  %>