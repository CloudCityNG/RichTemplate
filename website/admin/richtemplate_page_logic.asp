<!--#INCLUDE FILE="sessioncheck.asp"-->
<!--#include file="db_connection.asp"-->

<%
Sub JavaRedirect 
	nav = "/admin/richtemplate_list_sections.aspx"
    main="richtemplate_list_pages.aspx?SectionID="&Request.Querystring("SectionID")&""
    %>
    <SCRIPT language="JavaScript">
    <!--
    
    window.opener.top.basefrm.location 	= '<%=main%>';
    window.opener.top.treeframe.location= '<%=nav%>';
    window.close(); 
   // parent.location.href = 
    //'<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>

<%
Sub JavaRedirect2
	nav = "/admin/richtemplate_list_sections.aspx"
    main="/admin/richtemplate_welcome.asp?mode=forms"
    %>
    <SCRIPT language="JavaScript">
    <!--
    
    top.basefrm.location 	= '<%=main%>';
    top.treeframe.location 	= '<%=nav%>';
   // parent.location.href = 
    //'<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>
<%
Sub JavaRedirect3
	nav = "/admin/richtemplate_list_sections.aspx"
    main="richtemplate_list_pages.aspx?SectionID="&Request.Querystring("SectionID")&""
    %>
    <SCRIPT language="JavaScript">
    <!--
    
    top.basefrm.location 	= '<%=main%>';
    top.treeframe.location= '<%=nav%>';
    window.close(); 
   // parent.location.href = 
    //'<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>
<%
Sub JavaRedirect4
	nav = "/admin/richtemplate_list_sections.aspx"
    main="richtemplate_list_pages.aspx?SectionID="&RS3("ID")&""
    %>
    <SCRIPT language="JavaScript">
    <!--
    
    top.basefrm.location 	= '<%=main%>';
    top.treeframe.location= '<%=nav%>';
    window.close(); 
   // parent.location.href = 
    //'<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>
<%
Sub JavaRedirect5
	nav = "/admin/richtemplate_list_sections.aspx"
    main="richtemplate_list_pages.aspx?SectionID="&RS("ID")&""
    %>
    <SCRIPT language="JavaScript">
    <!--
    
    top.basefrm.location 	= '<%=main%>';
    top.treeframe.location= '<%=nav%>';
    window.close(); 
   // parent.location.href = 
    //'<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>
<%
Sub JavaRedirect6
	
	nav = "/admin/richtemplate_list_sections.aspx"
    main= "richtemplate_list_pages.aspx?SectionID="&Request.Querystring("SectionID")&""
    %>
    
    <SCRIPT language="JavaScript">
    <!--
    
    parent.opener.location = '<%=main%>';
    parent.opener.top.treeframe.location= '<%=nav%>';

	top.window.close();

    //-->
    </SCRIPT>
<%end sub%>
<%
Sub JavaRedirect7
	nav = "/admin/richtemplate_list_sections.aspx"
    main="richtemplate_list_pages.aspx?SectionID="&Request.Querystring("SectionID")&"&error=parentoffline"
    %>
    <SCRIPT language="JavaScript">
    <!--
    
    top.basefrm.location 	= '<%=main%>';
    top.treeframe.location= '<%=nav%>';
    window.close(); 
   // parent.location.href = 
    //'<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>


<%
'CREATE DATA CONNECTION

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

'DELETE PAGES  START ----------------------------------->


IF REQUEST.QUERYSTRING("task")="delete" THEN

		SELECT01 ="Select ID FROM WEBINFO WHERE PARENTID = "&REQUEST.QUERYSTRING("PAGEID")&""
		SET RS = CON.EXECUTE(SELECT01)
		
'<----------  IF THERE ARE NO SUB PAGES DELETE PAGE ---------->
		
		IF RS.EOF THEN
			
			DELETE01 = "DELETE FROM WEBINFO WHERE ID=" & REQUEST.QUERYSTRING("PAGEID") & ""
			CON.EXECUTE (DELETE01)
			
'<----------  IF THERE ARE SUB PAGES GET ID FROM PARENTID ---------->

		ELSE
			
		WHILE NOT RS.EOF
			
				SELECT02 ="Select ID FROM WEBINFO WHERE PARENTID = "&RS("ID")&""
				'response.write select02
				SET RS2 = CON.EXECUTE(SELECT01)
				
				
				ROOTID = RS2("ID")
				
				DELETE01 = "DELETE FROM WEBINFO WHERE ID=" & REQUEST.QUERYSTRING("PAGEID") & ""
				CON.EXECUTE (DELETE01)
				
				
				DELETE02 = "DELETE FROM WEBINFO WHERE ID=" & RS("ID")&""
				'response.write delete02
				CON.EXECUTE (DELETE02)
				
				WHILE NOT RS2.EOF
						
						

						SELECT03 ="Select ID FROM WEBINFO WHERE PARENTID = "&ROOTID&""
						'response.write select03
						SET RS3 = CON.EXECUTE(SELECT03)
						
						DELETE03 = "DELETE FROM WEBINFO WHERE ID=" &ROOTID&""
						CON.EXECUTE (DELETE03)
						
						WHILE NOT RS3.EOF
							
								SELECT04 ="Select ID FROM WEBINFO WHERE PARENTID = "&RS3("id")&""
								'response.write "SELECT04 = "&SELECT04 &"<BR>"
								SET RS4 = CON.EXECUTE(SELECT04)
								
								DELETE04 = "DELETE FROM WEBINFO WHERE ID=" & RS3("ID")&""
								'response.write "DELETE04 = "&DELETE04 &"<BR>"
								CON.EXECUTE (DELETE04)
								
								WHILE NOT RS4.EOF
								
									'RESPONSE.WRITE "SUB RECORD LEVEL 4 TO DELETE "&RS4("ID")&"<br>"
								
									SELECT05 ="Select ID FROM WEBINFO WHERE PARENTID = "&RS4("id")&""
									'response.write "SELECT05 = "&SELECT05 &"<BR>"
									SET RS5 = CON.EXECUTE(SELECT05)
									
									DELETE05 = "DELETE FROM WEBINFO WHERE ID=" & RS4("ID")&""
									'response.write "DELETE05 = "&DELETE05 &"<BR>"
									CON.EXECUTE (DELETE05)

									WHILE NOT RS5.EOF
									
									'	RESPONSE.WRITE "SUB RECORD LEVEL 5 TO DELETE "&RS5("ID")&"<br>"
									
										SELECT06 ="Select ID FROM WEBINFO WHERE PARENTID = "&RS5("id")&""
									'	response.write "SELECT06 = "&SELECT06 &"<BR>"
										SET RS6 = CON.EXECUTE(SELECT06)
										
										DELETE06 = "DELETE FROM WEBINFO WHERE ID=" & RS5("ID")&""
										'response.write "DELETE06 = "&DELETE06 &"<BR>"
										CON.EXECUTE (DELETE06)
											
										
																			
									
									RS5.MOVENEXT
									WEND								
								
								RS4.MOVENEXT
								WEND								
				
				
							RS3.MOVENEXT
							WEND
							
					
					RS2.MOVENEXT
					WEND
			
			
			RS.MOVENEXT
			WEND
		

			DELETE01 = "DELETE FROM WEBINFO WHERE ID=" & REQUEST.QUERYSTRING("PAGEID") & ""
			'response.write delete01
			'CON.EXECUTE (DELETE01)


		
		END IF
		

	Call JavaRedirect3

			
			
			
			
			
			
			
			
			
			
			'DELETESUBPAGESSQL = "DELETE FROM WEBINFO WHERE PARENTID

	

	'DELETE PAGES END  ------------------------------------->

	'DELETE SECTION START ---------------------------------->


ELSEIF REQUEST.QUERYSTRING("task")="deletesection" THEN

			WEBPAGEDELETESQL = "DELETE FROM WEBINFO WHERE SECTIONID=" & REQUEST.QUERYSTRING("SECTIONID") & ""
			'response.write WEBPAGEDELETESQL 
			CON.EXECUTE (WEBPAGEDELETESQL)

	Call JavaRedirect2		

	'DELETE SECTION END  ----------------------------------->




ELSEIF REQUEST.QUERYSTRING("TASK")="editsection" THEN

			WEBSECTIONUPDATE = "UPDATE WEBINFO SET NAME='" & REQUEST.FORM("SECTIONNAME") & "'  WHERE ID=" + REQUEST.QUERYSTRING("SectionID") + ""
			CON.EXECUTE (WEBSECTIONUPDATE)
			
				Call JavaRedirect
		

	'CREATE NEW SECTION START  ----------------------------->
	
	
'EDIT SYTLE SHEET START  ----------------------------->
'ADDED 5/18/05 - JH

ELSE IF REQUEST.QUERYSTRING("TASK")="style" then

'************************** MAKE ALL CHANGES IN CONFIG.JS AND PLACE IN CORRECT FOLDER **************************
	
	'get website root path
	strRootPath = Server.MapPath(Request.ServerVariables("Script_Name"))
	strRootPath = Left(strRootPath, Len(strRootPath) - Len(Request.ServerVariables("Script_Name")))
	'response.write strrootpath
	
	'pull style sheet info from editor
	html = request.Form("html")

	

	set fs = CreateObject("Scripting.FileSystemObject")
	Set file = fs.OpenTextFile(""&strRootPath&"\stylesheet\style.css", 2, true)
	file.Write(""&html&"")
	
	
	
	
	

   	file.Close
   	
call JavaRedirect2

'*******************************************************
'CREATE A NEW SECTION
'*******************************************************


ELSEIF REQUEST.QUERYSTRING("TASK")="newsection" then

			
	IF request.form("pageName")="" then

		call JavaRedirect2

	ELSE

		'ADD PAGE RANKING
				
					MYsql="SELECT TOP 1 RANK FROM WEBINFO WHERE PAGELEVEL=1 ORDER BY RANK DESC"
					'RESPONSE.WRITE MYSQL
					
							
					SET RS=CON.EXECUTE (MYsql)
					
					IF NOT RS.EOF THEN
					
					'RESPONSE.WRITE RS("RANK")
					
					IF RS("RANK")<1 OR RS("RANK")="" THEN
					
					
					'IF RS.EOF THEN
						MYRANK=1
						
						
					ELSE
						MYRANK= RS("RANK")+1
						
						
						
					END IF
					
					ELSE
					
					MYRANK=1
					
					END IF
									

	If Session("secure_education") = True then
	    myWhereClause = " and secure_education = 1"
	ElseIf Session("secure_members") = True then
	    myWhereClause = " and secure_members = 1"
	Else
	    myWhereClause = ""
	End if
	

	

		WEBSECTIONSQL ="Select * FROM WEBINFO WHERE NAME='" + REQUEST.FORM("pageName") + "'" & myWhereClause
		response.write WEBSECTIONSQL
		SET RS = CON.EXECUTE (WEBSECTIONSQL)
			IF RS.EOF THEN
			
				txtContent= request.Form("txtContent")
			  	txtContent= replace(txtContent, "'", "''")
			  	lastModified = now()



				pageName	= Request.Form("pageName")
				pageName	= replace(pageName, "'", "''")
				pageName	= replace(pageName, "&", "&amp;")
				pagename = ltrim(pagename)
				pagename = rtrim(pagename)

				
				
				searchable	= Request.Form("searchable")
				if searchable = "" then
				searchable = 0
				end if


					
    			
    			'CHECK TO SEE IF IT IS A PASSWORD PROTECTED AREA
    			
    			
    			IF Session("secure_members") Then

    			WEBSECTIONSQL2 = "INSERT INTO WEBINFO (NAME,SECURE_MEMBERS) VALUES ('"&pageName&"',1)"
    			CON.EXECUTE (WEBSECTIONSQL2)
	

				ElseIf Session("secure_education") Then

    			WEBSECTIONSQL2 = "INSERT INTO WEBINFO (NAME,SECURE_EDUCATION) VALUES ('"&pageName&"',1)"
    			CON.EXECUTE (WEBSECTIONSQL2)

	
				Else

    			WEBSECTIONSQL2 = "INSERT INTO WEBINFO (NAME) VALUES ('"&pageName&"')"
    			CON.EXECUTE (WEBSECTIONSQL2)


				End IF

    			
    			
    			
    			
    			
    			
    			
    			
    			
    			
    			
    			NEWSECTIONIDSQL = "SELECT ID FROM WEBINFO WHERE NAME='"&pageName&"'" & myWhereClause
    			SET RS3 = CON.EXECUTE (NEWSECTIONIDSQL )
    			
    				IF Request.Form("myPublish") = 1 THEN
    
    					WEBPAGESQL1 = "UPDATE WEBINFO SET LAST_MODIFIED = '"&lastModified&"', SECTIONID="&RS3("ID")&", NAME='"&pageName&"', DEFAULTPAGE=1, MESSAGE='"&txtContent&"', PAGELEVEL=1, RANK = "&MYRANK&", PENDING = 0, PAGE_LINKNAME= '"&REQUEST.FORM("PAGE_LINKNAME")&"', searchable = "&searchable&" WHERE ID= "&RS3("ID")&" "
    					
    				ELSE
			
    					WEBPAGESQL1 = "UPDATE WEBINFO SET LAST_MODIFIED = '"&lastModified&"', SECTIONID="&RS3("ID")&", NAME='"&pageName&"', DEFAULTPAGE=1, MESSAGE2='"&txtContent&"', PAGELEVEL=1, RANK = "&MYRANK&", PENDING = 1, PAGE_LINKNAME= '"&REQUEST.FORM("PAGE_LINKNAME")&"', searchable = "&searchable&" WHERE ID= "&RS3("ID")&" "

					END IF
						'response.write webpagesql1
    					CON.EXECUTE (WEBPAGESQL1)
			
				
				Call JavaRedirect4
				
			
	
				'CON.CLOSE
			ELSE
    		
    			Call JavaRedirect5
    	
    			
    			'CREATE NEW SECTION END   ----------------------------->
    
    		End IF
	END IF

ELSEIF REQUEST.QUERYSTRING("TASK")="publish" then

		getMsgSQL = "SELECT * FROM WEBINFO WHERE ID = "&Request.Querystring("pageID")&" "
    	SET RS = CON.EXECUTE (getMsgSQL)
    	
    			html = RS("message2")
			  	html = replace(html, "'", "''")



    
    	WEBPAGESQL1 = "UPDATE WEBINFO SET PENDING = 0, message = '"&html&"' WHERE ID= "&Request.Querystring("pageID")&" "
	   '	response.write webpagesql1
	   	CON.EXECUTE (WEBPAGESQL1)
		
   		'call JavaRedirect6


Elseif Request.Querystring("task") = "checkin" then

	If Request.Querystring("pageID") <> "" then
	
		upCheckout = "UPdate Webinfo set checked_out = 0 where ID = "&Request.Querystring("pageID")&""
		'response.write upCheckout
		con.execute(upCheckout) 
	
	End if
	
	JavaRedirect3
	'Response.Redirect ("richtemplate_list_pages.aspx?SectionID="&Request.Querystring("sectionID")&"")
	

Elseif Request.Querystring("task") = "checkinall" then

	If Request.Querystring("sectionID") <> "" then
	
		upCheckout = "UPdate Webinfo set checked_out = 0 where sectionID = "&Request.Querystring("sectionID")&""
		'response.write upCheckout
		con.execute(upCheckout) 
	
	End if
	
	JavaRedirect3

'*******************************************************
'PUT PAGE ONLINE - VISIBLE IN NAVIGATION
'*******************************************************

ElseIF REQUEST.QUERYSTRING("task")="online" THEN

		Select01 = "Select parentID from webinfo where ID = "& Request.Querystring("pageID")
		set RS = con.execute(select01)
		
		If Not RS.EOF then
		
			Select02 = "Select pending, ID from webinfo where ID = "&RS("parentID")
			Set RS2 = con.execute(select02)
			
			If Not RS2.EOF then
				
				If RS2("pending") = True then
					Call JavaRedirect7
				Else
					upOffline2 = "UPdate Webinfo set pending = 0 where ID = "& Request.Querystring("pageID")
					con.execute(upOffline2)
				End if
			
			End if
		Else
			
			upOffline1 = "UPdate Webinfo set pending = 0 where ID = " & Request.Querystring("pageID") 
			con.execute(upOffline1)
			
	

		End if
		

		Call JavaRedirect3





'*******************************************************
'TAKE PAGE OFFLINE - INVISIBLE TO NAVIGATION
'*******************************************************



ElseIF REQUEST.QUERYSTRING("task")="offline" THEN

		SELECT01 ="Select ID FROM WEBINFO WHERE PARENTID = "&REQUEST.QUERYSTRING("PAGEID")&""
		SET RS = CON.EXECUTE(SELECT01)
		
'<----------  IF THERE ARE NO SUB PAGES DELETE PAGE ---------->
		
		IF RS.EOF THEN
			upOffline1 = "UPdate Webinfo set pending = 1 where ID = " & Request.Querystring("pageID")
			CON.EXECUTE (upOffline1)
			
'<----------  IF THERE ARE SUB PAGES GET ID FROM PARENTID ---------->

		ELSE
			
		WHILE NOT RS.EOF
			
				SELECT02 ="Select ID FROM WEBINFO WHERE PARENTID = "&RS("ID")&""
				SET RS2 = CON.EXECUTE(SELECT01)
				
				
				upOffline2 = "UPdate Webinfo set pending = 1 where ID = " & Request.Querystring("pageID") 
				CON.EXECUTE (upOffline2)
				
				upOffline3 = "UPdate Webinfo set pending = 1 where ID = " & RS("ID")
				CON.EXECUTE (upOffline3)
				
				WHILE NOT RS2.EOF
						
						

						SELECT03 ="Select ID FROM WEBINFO WHERE PARENTID =  "&RS2("id")&""
						SET RS3 = CON.EXECUTE(SELECT03)
						
						upOffline4 = "UPdate Webinfo set pending = 1 where ID= "&RS2("id")&""
						CON.EXECUTE (upOffline4)
						
						WHILE NOT RS3.EOF
							
								SELECT04 ="Select ID FROM WEBINFO WHERE PARENTID = "&RS3("id")&""
								SET RS4 = CON.EXECUTE(SELECT04)
								
								upOffline5 = "UPdate Webinfo set pending = 1 where ID=" & RS3("ID")&""
								CON.EXECUTE (upOffline5)
								
								WHILE NOT RS4.EOF
								
								
									SELECT05 ="Select ID FROM WEBINFO WHERE PARENTID = "&RS4("id")&""
									SET RS5 = CON.EXECUTE(SELECT05)
									
									upOffline6 = "UPdate Webinfo set pending = 1 where ID=" & RS4("ID")&""
									CON.EXECUTE (upOffline6)

									WHILE NOT RS5.EOF
									
								
										SELECT06 ="Select ID FROM WEBINFO WHERE PARENTID = "&RS5("id")&""
										SET RS6 = CON.EXECUTE(SELECT06)
										
										upOffline7 = "UPdate Webinfo set pending = 1 where ID=" & RS5("ID")&""
										CON.EXECUTE (upOffline7)
																														
									
									RS5.MOVENEXT
									WEND								
								
								RS4.MOVENEXT
								WEND								
				
				
							RS3.MOVENEXT
							WEND
							
					
					RS2.MOVENEXT
					WEND
			
			
			RS.MOVENEXT
			WEND
		

		
		END IF
		


 Call JavaRedirect3


END IF



end if


CON.CLOSE
%>