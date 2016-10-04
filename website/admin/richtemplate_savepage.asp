<%@ LANGUAGE="VBSCRIPT" %>
<% Server.ScriptTimeout = 600 %>
<%
Sub JavaRedirect2
	nav = "/admin/richtemplate_list_sections.aspx"
    main="richtemplate_list_pages.aspx?sectionid="&sectionid&""
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
<!--#INCLUDE FILE="sessioncheck.asp"-->
<!--#include file="db_connection.asp"-->



<%


pageID		= Request.Querystring("pageID")

page		= Request.Form("page")
Name 		= Request.Form("name")
PARENTID 	= Request.Form("PARENTID")
SECTIONID 	= REQUEST.FORM("SECTIONID")
Page		= Request.Form("page")
DefaultPage	= Request.Form("defaultpage")
myPublish 	= Request.Form("myPublish")
PAGELEVEL 	= REQUEST.FORM("PAGELEVEL")
pagename 	= Request.Form("pagename")
pagename = ltrim(pagename)
pagename = rtrim(pagename)
pagename 	= replace(pagename, "'", "''")
pagename 	= replace(pagename, "&", "&amp;")




lastModified = now()
userID 		= Session("userID")
searchable	= Request.Form("searchable")
if searchable = "" then
searchable = 0
end if



Dim txtContent

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

strDeleteWebInfoGroupsByPageID = "Delete From ss_access_webinfoGroup where WebInfoID = " & pageID
Con.Execute(strDeleteWebInfoGroupsByPageID)


	
	session("pagename") = pagename
	txtContent= request.Form("txtContent")
	
  	txtContent= replace(txtContent, "'", "''")
  	if txtContent = "" then
  	txtContent = "<p></p>"
  	End if
  	
  	
'FIX ANCHORS


  	
  	
'*********************************************************		
'Insert current record if pageID =""
'*********************************************************	

  		if clng(pageID) < clng(0) then    
  		
				
							'ADD PAGE RANKING
				
					MYsql="SELECT TOP 1 RANK FROM WEBINFO WHERE PARENTID= " & PARENTID & " ORDER BY RANK DESC"
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
									
				'	response.write MYRANK


	'******* ADD PAGE_LINKNAME TO RECORD SET *******
		
				ADDPageName = "SELECT PAGE_LINKNAME FROM WEBINFO WHERE SECTIONID="&sectionID&" AND pagelevel = 1 "
				SET RS3 = CON.EXECUTE (ADDPageName)

				PAGE_LINKNAME = RS3("PAGE_LINKNAME")
				
	'******* ADD PAGE_LINKNAME TO RECORD SET *******
				
				
				'CHECK TO SEE IF PAGE IS SAVED LIVE OR PENDING
				
					IF myPublish= "1" then
					
					
					
						IF Session("secure_members") Then
						AddWebpageSQL1 = "INSERT INTO WEBINFO (LAST_MODIFIED, NAME, PARENTID, SECTIONID, MESSAGE, DEFAULTPAGE, PENDING, RANK, PAGELEVEL, metaDesc, metaTitle, metaKeyword, PAGE_LINKNAME, author, checked_out, searchable,secure_members) VALUES ('"&lastModified&"', '" & pagename & "', " &PARENTID& ", '"&SECTIONID&"', '" & txtContent& "',0,0, "&MYRANK&", '"&PAGELEVEL&"', '"&metaDesc&"', '"&metaTitle&"', '"&metaKeyword&"','"&PAGE_LINKNAME&"', '"&session("userName")&"', 0, "&searchable&",1)"
						con.execute (AddWebpageSQL1)
						set rsInsert = con.execute ("Select @@Identity")
						PageID = rsInsert(0)

		
						ElseIf Session("secure_education") Then

						AddWebpageSQL1 = "INSERT INTO WEBINFO (LAST_MODIFIED, NAME, PARENTID, SECTIONID, MESSAGE, DEFAULTPAGE, PENDING, RANK, PAGELEVEL, metaDesc, metaTitle, metaKeyword, PAGE_LINKNAME, author, checked_out, searchable, secure_education) VALUES ('"&lastModified&"', '" & pagename & "', " &PARENTID& ", '"&SECTIONID&"', '" & txtContent& "',0,0, "&MYRANK&", '"&PAGELEVEL&"', '"&metaDesc&"', '"&metaTitle&"', '"&metaKeyword&"','"&PAGE_LINKNAME&"', '"&session("userName")&"', 0, "&searchable&",1)"
						con.execute (AddWebpageSQL1)
						set rsInsert = con.execute ("Select @@Identity")
						PageID = rsInsert(0)						
			
						Else

						AddWebpageSQL1 = "INSERT INTO WEBINFO (LAST_MODIFIED, NAME, PARENTID, SECTIONID, MESSAGE, DEFAULTPAGE, PENDING, RANK, PAGELEVEL, metaDesc, metaTitle, metaKeyword, PAGE_LINKNAME, author, checked_out, searchable) VALUES ('"&lastModified&"', '" & pagename & "', " &PARENTID& ", '"&SECTIONID&"', '" & txtContent& "',0,0, "&MYRANK&", '"&PAGELEVEL&"', '"&metaDesc&"', '"&metaTitle&"', '"&metaKeyword&"','"&PAGE_LINKNAME&"', '"&session("userName")&"', 0, "&searchable&")"
						con.execute (AddWebpageSQL1)
						set rsInsert = con.execute ("Select @@Identity")
						PageID = rsInsert(0)						


						End IF


					else
					
					
						IF Session("secure_members") Then
				
						AddWebpageSQL2 = "INSERT INTO WEBINFO (LAST_MODIFIED, NAME, PARENTID, SECTIONID, MESSAGE2, DEFAULTPAGE, PENDING, RANK, PAGELEVEL, metaDesc, metaTitle, metaKeyword, PAGE_LINKNAME, author, checked_out, searchable, secure_members) VALUES ('"&lastModified&"', '" & pagename & "', " &PARENTID&", "&SECTIONID&", '" & txtContent& "',0, 1, "&MYRANK&", '"&PAGELEVEL&"','"&metaDesc&"', '"&metaTitle&"', '"&metaKeyword&"', '"&PAGE_LINKNAME&"', '"&session("userName")&"', 0, "&searchable&",1)"
						con.execute (AddWebpageSQL2)
						set rsInsert = con.execute ("Select @@Identity")
						PageID = rsInsert(0)						

		
						ElseIf Session("secure_education") Then
				
						AddWebpageSQL2 = "INSERT INTO WEBINFO (LAST_MODIFIED, NAME, PARENTID, SECTIONID, MESSAGE2, DEFAULTPAGE, PENDING, RANK, PAGELEVEL, metaDesc, metaTitle, metaKeyword, PAGE_LINKNAME, author, checked_out, searchable, secure_education) VALUES ('"&lastModified&"', '" & pagename & "', " &PARENTID&", "&SECTIONID&", '" & txtContent& "',0, 1, "&MYRANK&", '"&PAGELEVEL&"','"&metaDesc&"', '"&metaTitle&"', '"&metaKeyword&"', '"&PAGE_LINKNAME&"', '"&session("userName")&"', 0, "&searchable&",1)"
						con.execute (AddWebpageSQL2)
						set rsInsert = con.execute ("Select @@Identity")
					    PageID = rsInsert(0)						
			
						Else
				
						AddWebpageSQL2 = "INSERT INTO WEBINFO (LAST_MODIFIED, NAME, PARENTID, SECTIONID, MESSAGE2, DEFAULTPAGE, PENDING, RANK, PAGELEVEL, metaDesc, metaTitle, metaKeyword, PAGE_LINKNAME, author, checked_out, searchable) VALUES ('"&lastModified&"', '" & pagename & "', " &PARENTID&", "&SECTIONID&", '" & txtContent& "',0, 1, "&MYRANK&", '"&PAGELEVEL&"','"&metaDesc&"', '"&metaTitle&"', '"&metaKeyword&"', '"&PAGE_LINKNAME&"', '"&session("userName")&"', 0, "&searchable&")"
						con.execute (AddWebpageSQL2)
						set rsInsert = con.execute ("Select @@Identity")
						PageID = rsInsert(0)						


						End IF

					end if
				
						Session("metaKeyword") 	= ""
						Session("metaDesc")		= ""
						Session("metaTitle")	= ""	
						
strGroupsToAdd=""
If Not Request.form("access_groups")="" Then
    strGroupsToAdd = Request.form("access_groups")
    listGroupsToAdd = Split(strGroupsToAdd,",")
    strAddWebInfoGroupsByPageID = ""
    For Each strGroup in listGroupsToAdd
        strAddWebInfoGroupsByPageID = strAddWebInfoGroupsByPageID & "Insert INTO ss_access_webinfoGroup (webinfoID, groupID) Values(" & PageID & ", " & strGroup & ") "
    Next
    If Not strAddWebInfoGroupsByPageID = "" Then
        Con.Execute(strAddWebInfoGroupsByPageID)
    End If
End If						
			
				CON.CLOSE
				SET CON = NOTHING
				SET RS	= NOTHING
				SET RS3 = NOTHING
				
				Response.Redirect "richtemplate_list_pages.aspx?sectionid="&sectionid&""
				
				
		else   
		
		
'*********************************************************		
'Update current record if pageID >0
'*********************************************************		

		'ADD PAGE_LINKNAME TO RECORD SET
		
				ADDPageName = "SELECT PAGE_LINKNAME FROM WEBINFO WHERE SECTIONID="&sectionID&" AND pagelevel = 1 "
				'response.write addpagename
				SET RS3 = CON.EXECUTE (ADDPageName)

				PAGE_LINKNAME = RS3("PAGE_LINKNAME")

				
				getMetaInfo = "Select metaDesc, metaTitle, metakeyword from webinfo where id = "&pageID&""
				set rs4 = con.execute(getMetaInfo)
				
				If Session("metaDesc") <> "" then
					metaDesc = replace(Session("metaDesc"), "'", "''")
				ElseIf RS4("metaDesc") <> "" then
					metaDesc = replace(RS4("metaDesc"), "'", "''")
				Else
					metaDesc = ""
					Session("metaDesc") = ""
				End if
				
				If Session("metaKeyword") <> "" then
					metaKeyword = replace(Session("metaKeyword"), "'", "''")
				ElseIf RS4("metaKeyword") <> "" then
					metaKeyword = replace(RS4("metaKeyword"), "'", "''")
				Else
					metaKeyword = ""
					Session("metaKeyword") = ""
				End if
				
				If Session("metaTitle") <> "" then
					metaTitle = replace(Session("metaTitle"), "'", "''")
				ElseIf RS4("metaTitle") <> "" then
					metaTitle = replace(RS4("metaTitle"), "'", "''")
				Else
					metaTitle = ""
					Session("metaTitle") = ""
				End if

		
		'CHECK TO SEE IF PAGE IS SAVED LIVE OR PENDING
				
					IF myPublish= "1" then
						UpdateWebpageSQL = "UPDATE WEBINFO SET LAST_MODIFIED = '"&lastModified&"', NAME = '" & pagename & "', sectionid = " & SECTIONID & ", MESSAGE = '" & txtContent& "', Defaultpage= '"&defaultpage&"', metaDesc = '"&metaDesc&"', metaTitle = '"&metaTitle&"', metaKeyword = '"&metaKeyword&"', PAGE_LINKNAME = '"&PAGE_LINKNAME&"', author = '"&session("username")&"', checked_out = 0, searchable = "&searchable&"  WHERE id = " & pageID & ""
					ELSE
						UpdateWebpageSQL = "UPDATE WEBINFO SET LAST_MODIFIED = '"&lastModified&"', NAME = '" & pagename & "', sectionid = " & SECTIONID & ", MESSAGE2 = '" & txtContent& "', Defaultpage= '"&defaultpage&"', metaDesc = '"&metaDesc&"', metaTitle = '"&metaTitle&"', metaKeyword = '"&metaKeyword&"', PAGE_LINKNAME = '"&PAGE_LINKNAME&"', author = '"&session("userName")&"', checked_out = 0, searchable = "&searchable&"  WHERE id = " & pageID & ""
					END IF
					
					'response.write updatewebpagesql
				con.Execute (UpdateWebpageSQL ) 
				
				Session(pageID) = ""
						Session("metaKeyword") 	= ""
						Session("metaDesc")		= ""
						Session("metaTitle")	= ""	
				
				
				
        Call JavaRedirect2
		
		end if '*+*+*+*+*+ End of Record Update *+*+*+*+*+ 


strGroupsToAdd=""
If Not Request.form("access_groups")="" Then
    strGroupsToAdd = Request.form("access_groups")
    listGroupsToAdd = Split(strGroupsToAdd,",")
    strAddWebInfoGroupsByPageID = ""
    For Each strGroup in listGroupsToAdd
        strAddWebInfoGroupsByPageID = strAddWebInfoGroupsByPageID & "Insert INTO ss_access_webinfoGroup (webinfoID, groupID) Values(" & PageID & ", " & strGroup & ") "
    Next
    If Not strAddWebInfoGroupsByPageID = "" Then
        Con.Execute(strAddWebInfoGroupsByPageID)
    End If
End If
  
				
					con.close
					set rs = nothing
					set rs3 = nothing
					set con = nothing
%>