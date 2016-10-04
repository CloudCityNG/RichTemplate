<!--#INCLUDE FILE="conn.asp"---->
<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>Hillel Publications</title>
</head>

<body marginheight=0 marginwidth=0 topmargin="0" leftmargin="0" bgcolor="#086308" background="images/bgstrip2.gif" link="#ffff00" vlink="#ffff00">
<%IF REQUEST.QUERYSTRING("pubtype")<>"" THEN
myid = Request.querystring("id")
IF REQUEST.QUERYSTRING("pubtype")="searchresults" then
myDisSQL = "Select * from Library where title LIKE '" & "%" & Request.form("strSearch") & "%" & "' OR KEYWORD LIKE '" & "%" & Request.form("strSearch") & "%" & "' ORDER BY title"
else
myDisSQL = "Select DOCUMENTTYPE, DOCUMENTPATH, GROUP_X, DATE_X, TITLE, ID, AUTHOR from Library where GROUP_X='" & REQUEST.QUERYSTRING("pubtype") & "'"
end if
Set RS = con.execute (myDisSQL)%>


<table>
<tr><td><font color="#e8e8e8" face="Verdana" size="3"><b>Toad Docs</b></font></td></tr>
<tr><td><center><font color="#FFFFFF" face="Verdana" size="1"><b>| <a href="publications.asp?pubtype=Sales">Sales</a>
    | <a href="publications.asp?pubtype=Marketing">Marketing</a> | <a href="publications.asp?pubtype=Human Resources">Human
    Resources</a> | <a href="publications.asp?pubtype=Legal">Legal</a>
    | <a href="publications.asp?task=search">Search</a> | <a href="javascript:close(window)">Close Window</a> |</b></font></center></td></tr></table>
      
      <br>
      <table>
<tr><td colspan="2"><font color="#ffffff" face="Verdana" size="2">Publication Key:</td></tr>
<tr><td><img src="images/worddoc.gif"></td><td><font color="#e8e8e8" face="Verdana" size="1">Word Document</font></td></tr>
<tr><td><img src="images/htmdoc.gif"></td><td><font color="#e8e8e8" face="Verdana" size="1">HTML Document</font></td></tr>
<tr><td><img src="images/pdfdoc.gif"></td><td><font color="#e8e8e8" face="Verdana" size="1">PDF Document, you must <a href="http://www.adobe.com/products/acrobat/readstep2.html" target="_blank">download Adobe Acrobat</a> to view PDF files.</font></td></tr>
<tr><td><img src="images/unknowndoc.gif"></td><td><font color="#e8e8e8" face="Verdana" size="1">Unknown Document Type</font></td></tr>
</table>
  <br>
 
 <%IF RS.EOF THEN%>
 <font color="#e8e8e8" face="Verdana" size="3"><b>There are currently no
 documents for this topic.</b></font>

<%ELSE
%>

<table cellpadding="5">
<tr>
<td></td><td><font face="Arial, Helvetica, sans-serif" size="3"><font color="#FFFFFF">
<%if request.querystring("pubtype")="searchresults" then%>SEARCH RESULTS<%ELSE%>PUBLICATION TOPIC:  <%=RS("GROUP_X")%><%END IF%></font></font><font face="Arial, Helvetica, sans-serif" size="1">  
</FONT></TD>
</TR>


<%IF NOT RS.EOF THEN

    
    
    WHILE NOT RS.EOF
    x=cint(RS("id"))
    %>
    
    
    
    <tr> 
      <%IF RS("DOCUMENTTYPE")="WRITE"  THEN %>
            <td width="25" align="middle" valign="center"><a href="publicationdoc.asp?pubtype=<%=RS("GROUP_X")%>&id=<%=RS("ID")%>">

        <img border="0" src="images/htmdoc.gif" alt="HTML Document"></a></td>
      <td width="100%"><font face="Arial, Helvetica, sans-serif" size="2"><a href="publicationdoc.asp?pubtype=<%=RS("GROUP_X")%>&id=<%=RS("ID")%>" TARGET="_BLANK"><%=RS("TITLE")%> by:<%=RS("AUTHOR")%></a></font></td>

      <%elseif RS("DOCUMENTTYPE")="UPLOAD" THEN%>
            <td width="25" align="middle" valign="center"><a href="HTTP://homepage.u7.spwh.com/admin/upload/<%=RS("DOCUMENTPATH")%>" TARGET="_BLANK">
        
        
        <%strDocType=Right((RS("DOCUMENTPATH")),3)
        
        IF strDocType="doc" then
        %>
        <img border="0" src="images/worddoc.gif" alt="Word Document">
        <%elseif strDocType="pdf" then%>
        <img border="0" src="images/pdfdoc.gif" alt="PDF Document">
        <%elseif strDocType="ppt" then%>
        <img border="0" src="images/powerpointdoc.gif" alt="Power Point Document">
        <%else%>
        <img border="0" src="images/unknowndoc.gif" alt="Unknown Document Type">
        <%end if%>
        
       </a></td>
      <td width="100%"><font face="Arial, Helvetica, sans-serif" size="2"><a href="HTTP://homepage.u7.spwh.com/admin/upload/<%=RS("DOCUMENTPATH")%>" TARGET="_BLANK"><%=RS("TITLE")%> by:<%=RS("AUTHOR")%></a></font></td>

        <%end if%>
    </tr>
    

    <%
   
    RS.MoveNext
    WEND
    
  END IF  

    
   
   
    
END IF

else%>
<table>
<tr><td><font color="#808080" face="Verdana" size="3"><b>Toad Docs</b></font></td></tr>
<tr><td><center><font color="#FFFFFF" face="Verdana" size="1"><b>| <a href="publications.asp?pubtype=Sales">Sales</a>
    | <a href="publications.asp?pubtype=Marketing">Marketing</a> | <a href="publications.asp?pubtype=Human Resources">Human
    Resources</a> | <a href="publications.asp?pubtype=Legal">Legal</a>
    | <a href="publications.asp?task=search">Search</a> | <a href="javascript:close(window)">Close Window</a> |</b></font></center></td></tr></table>
      
 <form method="POST" action="publications.asp?pubtype=searchresults">
  <table border="0" width="580" cellpadding="0">
    <tr>
      <td width="196">
        <p align="right"><font face="Verdana" color="#FFFFFF" size="2"><b>Title
        and Keyword Search:</b></font></td>
      <td width="374"><input type="text" name="strSearch" size="42"></td>
    </tr>
    <tr>
      <td width="196"></td>
      <td width="374"><input type="submit" value="Search" name="B1"></td>
    </tr>
    <tr>
      <td align="right" width="287"></td>
      
      <td align="right" width="287">
        <p align="left"><font face="Verdana" color="#FFFFFF" size="1">SEARCH TIP: Use single key words or single words within publication titles.  Do not separate words with commas, use the space bar.</font></td>
      
    </tr>
  </table>
  <p>&nbsp;</p>
</form>
</table>     <br>

<%END IF%>

</body>

</html>







































