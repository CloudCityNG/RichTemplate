
<%IF REQUEST.QUERYSTRING("news")="moredetails" THEN

		mySQL2 = "SELECT * FROM COMPNEWS WHERE ID=" & REQUEST.QUERYSTRING("ID") & ""
      SET RS2 = CON.EXECUTE (mySQL2)
%> 
<table border="0" width="100%" cellpadding="2">
<tr>
<TD bgcolor="#ffffff" width="100%"><font size="1" face="verdana"><a href="<%=Session("name")%>.asp">[BACK TO NEWS LIST]</A></FONT></TD>
</TR>
      <TR>
       
<TD bgcolor="#ffffff" width="100%">
<font size="2" face="verdana"><b><%=RS2("DATE_X")%></font> - <%=RS2("TITLE")%></b>
<P><font size="1" face="verdana"><%=RS2("MESSAGE")%>
</TD>
</TR>
<tr>
<TD bgcolor="#ffffff" width="100%"><font size="1" face="verdana"><a href="<%=Session("name")%>.asp">[BACK TO NEWS LIST]</A></FONT></TD>
</TR>
</TABLE>

<%ELSE%>

      <table border="0" width="100%" cellpadding="2">


      <!--------------------------------GET COMPNEWS INFO------------------------------------------>

      <%mySQL = "SELECT * FROM COMPNEWS WHERE GROUP_ID=" & Session("calendar") & " AND MODE_X='LIVE' ORDER BY DATE_X"
      SET RS = CON.EXECUTE (mySQL)
      WHILE NOT RS.EOF%>
      <TR>
       
<TD bgcolor="#ffffff" width="481">

<font size="1" face="verdana"><%=RS("DATE_X")%></font> - <a href="<%=Session("name")%>.asp?news=moredetails&ID=<%=RS("ID")%>"><%=RS("TITLE")%></a>

</td>
             
       
      <%RS.MoveNext
      wend
      
      %>       
</tr>


</TABLE>  
<%END IF%>