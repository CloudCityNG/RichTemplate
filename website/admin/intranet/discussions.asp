<!--#INCLUDE FILE="conn.asp"---->
<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>Hillel Discussions</title>
</head>

<body marginheight=0 marginwidth=0 topmargin="0" leftmargin="0" bgcolor="#333333" background="images/bgstrip2.gif" link="#ffff00" vlink="#ffff00">
<%IF REQUEST.QUERYSTRING("id")<>"" THEN
myid = Request.querystring("id")
myDisSQL = "Select wp.catid, wp.id, wp.name, wp.message, wp.wptitle, wc.title from webcat wc, webpages wp where wc.id=" & myid & " AND wc.id=wp.catid AND wp.online=-1"
Set RS = con.execute (myDisSQL)

elseif request.querystring("results")="searchresults" then

	if request.querystring("searchid")<>"" then
		if Session("criteria")="name" then
	myDisSQL = "Select * from webpages wp where wp.name Like'" & "%" & Session("search") & "%" & "' ORDER BY NAME"
		END IF
		IF Session("criteria")="message" THEN
			myDisSQL = "Select * from webpages wp where wp.message Like'" & "%" & Session("search") & "%" & "' ORDER BY id"
		end if
	ELSE
    if request.form("strSearch")<>"" then
    varSearch = Request.Form("strSearch")
    Session("search")=varSearch
        end if

    if request.form("strType")<>"" then
    strCriteria = Request.Form("strType")
    Session("criteria")=strCriteria
    end if
IF strCriteria = "name" THEN
myDisSQL = "Select * from webpages wp where wp.name Like '" & "%" & varSearch & "%" & "' ORDER BY NAME"
elseif strCriteria = "message" THEN
myDisSQL = "Select * from webpages wp where wp.message Like '" & "%" & varSearch & "%" & "' ORDER BY id"
END IF
	END IF
Set RS = con.execute (myDisSQL)
end if
%>

  
<table>
<tr><td><font color="#808080" face="Verdana" size="3"><b>Hillel Discussions<b></font></td></tr>
<tr><td><center><font color="#FFFFFF" face="Verdana" size="1"><b>| 

<%myDisSQL = "Select id, title from webcat where mode_x=-1"
Set RS3 = con.execute (myDisSQL)
IF NOT RS3.EOF THEN
%>
<a href="discussions.asp?id=<%=RS3("id")%>">View
      Current Discussion Topic</a> 
      <%END IF%>
      | <a href="discussions_archived.asp">View
      Archived Discussion Topics</a> | <a href="discussions.asp?task=search">Search</a> | <a href="javascript:close(window)">Close Window</a> |</b></font></center></td></tr></table>
      
      <br><br>
<%IF REQUEST.QUERYSTRING("task")="search" then%>      

 <form method="POST" action="discussions.asp?task=search&results=searchresults">
  <table border="0" width="580" cellpadding="0">
    <tr>
      <td width="196">
        <p align="right"><font face="Verdana" color="#FFFFFF" size="2"><b>Text
        and Author Search:</b></font></td>
      <td width="374"><input type="text" name="strSearch" size="42"></td>
    </tr>
            <tr>
      <td width="196"></td>
      <td width="374"><input type="radio" value="name" checked name="strType"><font face="Verdana" color="#FFFFFF" size="1">Author  <input type="radio" value="message" name="strType">Text</font></td>
    </tr>
    <tr>
      <td width="196"></td>
      <td width="374"><input type="submit" value="Search" name="B1"></td>
    </tr>

    <tr>
      <td align="right" width="287"></td>
      
      <td align="right" width="287">
        <p align="left"><font face="Verdana" color="#FFFFFF" size="1">SEARCH TIP: Use first or last names, or single text keyword.  Do not separate words with commas, use the space bar.</font></td>
      
    </tr>
  </table>
  <p>&nbsp;</p>
</form>
</table>     <br> 
<%if request.querystring("results")="searchresults" then%> 
<table cellpadding="5">
<tr>
<td></td><td><font face="Arial, Helvetica, sans-serif" size="3"><font color="#FFFFFF">Search Results</font></font><font face="Arial, Helvetica, sans-serif" size="1"></FONT></TD>
</TR>
<%IF RS.EOF THEN%>
<tr>
<td></td><td><font face="Arial, Helvetica, sans-serif" size="2"><font color="#FFFFFF">No matches were found within your search criteria.</font></font><font face="Arial, Helvetica, sans-serif" size="1"></FONT></TD>
</TR>
<%else%>

    
    
<%    WHILE NOT RS.EOF
    x=cint(RS("id"))
    %>
    
    
    
    <tr> 
      <%IF x = cint(request.querystring("expand"))  THEN %>
            <td width="25" align="middle" valign="center"><a href="discussions.asp?task=search&results=searchresults&searchid=<%=RS("id")%>&close=<%=RS("id")%>">

        <img border="0" src="images/folderdown.gif"></a></td>
      <td width="100%"><font face="Arial, Helvetica, sans-serif" size="2"><a href="discussions.asp?task=search&results=searchresults&searchid=<%=RS("id")%>&close=<%=RS("id")%>"><%=RS("wptitle")%> by:<%=RS("name")%></a></font></td>

      <%else%>
            <td width="25" align="middle" valign="center"><a href="discussions.asp?task=search&results=searchresults&searchid=<%=RS("id")%>&expand=<%=RS("id")%>">
        </a><img border="0" src="images/folder.gif"></a></td>
      <td width="100%"><font face="Arial, Helvetica, sans-serif" size="2"><a href="discussions.asp?task=search&results=searchresults&searchid=<%=RS("id")%>&expand=<%=RS("id")%>"><%=RS("wptitle")%> by:<%=RS("name")%></a></font></td>

        <%end if%>
    </tr>
    <%
    IF x = cint(request.querystring("expand"))  THEN %>

    <tr> 
      <td width="25" align="middle" valign="center">&nbsp;</td>
      <td width="100%" bgcolor="#e8e8e8"><img src="images/redarrow.gif"><font face="Arial, Helvetica, sans-serif" size="2"><b><%=RS("wptitle")%> by:&nbsp;&nbsp;<%=RS("name")%></b><p><%=RS("message")%></p></font></td>
    </tr>
    

    <%ELSEIF x = cint(REQUEST.QUERYSTRING("close")) THEN
    
    
   END IF
   
    RS.MoveNext
    WEND 
   end if
   end if%>
   </table>
   <%else%>
   
   
 <%IF REQUEST.QUERYSTRING("task")="addedthread" THEN%> 
 <table>
<tr><td><font color="#808080" face="Verdana" size="2"><b>Thank you, your thread was submitted successfully, and is currently under review.<b></font></td></tr></table>
<%END IF%>
 

<table cellpadding="5">
<tr>
<td></td><td><font face="Arial, Helvetica, sans-serif" size="3"><font color="#FFFFFF">TOPIC:  <%=RS("TITLE")%></font></font><font face="Arial, Helvetica, sans-serif" size="1">  
  <img border="0" src="images/newthread.gif"><A HREF="discussionthread.asp?catid=<%=RS("catid")%>">[ADD THREAD]</A></FONT></TD>
</TR>


<%

    
    
    WHILE NOT RS.EOF
    x=cint(RS("id"))
    %>
    
    
    
    <tr> 
      <%IF x = cint(request.querystring("expand"))  THEN %>
            <td width="25" align="middle" valign="center"><a href="discussions.asp?id=<%=myid%>&close=<%=RS("id")%>">

        <img border="0" src="images/folderdown.gif"></a></td>
      <td width="100%"><font face="Arial, Helvetica, sans-serif" size="2"><a href="discussions.asp?id=<%=myid%>&close=<%=RS("id")%>"><%=RS("wptitle")%> by:<%=RS("name")%></a></font></td>

      <%else%>
            <td width="25" align="middle" valign="center"><a href="discussions.asp?id=<%=myid%>&expand=<%=RS("id")%>">
        </a><img border="0" src="images/folder.gif"></a></td>
      <td width="100%"><font face="Arial, Helvetica, sans-serif" size="2"><a href="discussions.asp?id=<%=myid%>&expand=<%=RS("id")%>"><%=RS("wptitle")%> by:<%=RS("name")%></a></font></td>

        <%end if%>
    </tr>
    <%
    IF x = cint(request.querystring("expand"))  THEN %>

    <tr> 
      <td width="25" align="middle" valign="center">&nbsp;</td>
      <td width="100%" bgcolor="#e8e8e8"><img src="images/redarrow.gif"><font face="Arial, Helvetica, sans-serif" size="2"><b><%=RS("wptitle")%> by:&nbsp;&nbsp;<%=RS("name")%></b><p><%=RS("message")%></p></font></td>
    </tr>
    

    <%ELSEIF x = cint(REQUEST.QUERYSTRING("close")) THEN
    
    
   END IF
   
    RS.MoveNext
    WEND
    
    

    
   
   
    

END IF%>
</table>
</body>

</html>














































