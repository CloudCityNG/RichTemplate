<!--#INCLUDE FILE="conn.asp"---->
<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>Hillel Discussions</title>
</head>

<body marginheight=0 marginwidth=0 topmargin="0" leftmargin="0" bgcolor="#333333" background="images/bgstrip2.gif" link="#ffff00" vlink="#ffff00">
<%IF REQUEST.QUERYSTRING("id")<>""  THEN
myid = Request.querystring("id")
myDisSQL = "Select wp.catid, wp.id, wp.name, wp.message, wp.wptitle, wc.title from webcat wc, webpages wp where wc.id=" & myid & " AND wc.id=wp.catid AND wp.online=-1"
Set RS = con.execute (myDisSQL)%>

  
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
      Archived Discussion Topics</a> | <a href="discussions.asp?task=search">Search</a> |  <a href="javascript:close(window)">Close Window</a> |</b></font></center></td></tr></table>
      

      <br><br>
  

<table cellpadding="5">
<tr>
<td></td><td><font face="Arial, Helvetica, sans-serif" size="3"><font color="#FFFFFF">TOPIC:  <%=RS("TITLE")%></font></font></TD>
</TR>
<%

    
    
    WHILE NOT RS.EOF
    x=cint(RS("id"))
    %>
    
    
    
    <tr> 
      <%IF x = cint(request.querystring("expand"))  THEN %>
            <td width="25" align="middle" valign="center"><a href="discussions_archived.asp?id=<%=myid%>&close=<%=RS("id")%>">

        <img border="0" src="images/folderdown.gif"></a></td>
      <td width="100%"><font face="Arial, Helvetica, sans-serif" size="2"><a href="discussions_archived.asp?id=<%=myid%>&close=<%=RS("id")%>"><%=RS("wptitle")%> by:<%=RS("name")%></a></font></td>

      <%else%>
            <td width="25" align="middle" valign="center"><a href="discussions_archived.asp?id=<%=myid%>&expand=<%=RS("id")%>">
        </a><img border="0" src="images/folder.gif"></a></td>
      <td width="100%"><font face="Arial, Helvetica, sans-serif" size="2"><a href="discussions_archived.asp?id=<%=myid%>&expand=<%=RS("id")%>"><%=RS("wptitle")%> by:<%=RS("name")%></a></font></td>

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
    
    

    %>
</table>
<%else
myDisSQL = "Select id, title from webcat where mode_x=0"
Set RS = con.execute (myDisSQL)

    %>
<table>
<tr><td><font color="#808080" face="Verdana" size="3"><b>Hillel Discussions<b></font></td></tr>
<tr><td><center><font color="#FFFFFF" face="Verdana" size="1"><b>| <a href="discussions.asp">View
      Current Discussion Topic</a> | <a href="discussions_archived.asp">View
      Archived Discussion Topics</a> | <a href="discussions.asp?task=search">Search</a> | <a href="javascript:close(window)">Close Window</a> |</b></font></center></td></tr></table>
      
</b></font></center></td></tr></table>
<TABLE>
<br><br>
<TR>
	<TD><font color="#fffff" face="Verdana" size="3">Archived Discussion Topics</font></td>
</TR>
<%WHILE NOT RS.EOF%>
<TR>	
	<TD><font color="#808080" face="Verdana" size="1"><A HREF="discussions_archived.asp?id=<%=RS("id")%>"><%=RS("title")%></a></font></td>
</TR>
<%RS.MOVENEXT
WEND%>
</TABLE>

<%END IF%>
</body>





















