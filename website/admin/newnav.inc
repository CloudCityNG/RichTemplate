<%
'OPEN DATA---------------------------------------------------->

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

IF REQUEST.QUERYSTRING("page")="" then
MYSQL = "SELECT * FROM WEBINFO where defaultpage = 1 ORDER BY RANK"
SET RS = CON.EXECUTE (MYSQL)%>

      <table border="0" cellpadding="2" cellspacing="2" width="100%" class=navtable2>

<%WHILE NOT RS.EOF%>

          <tr><td class=navcell2><A HREF="index.asp?page=<%=RS("ID")%>&sectionid=<%=rs("sectionid")%>" class=navlink2>+  <%=RS("NAME")%></td></tr>


<%RS.MOVENEXT
WEND%>
         </table>

    

	<%end if%>