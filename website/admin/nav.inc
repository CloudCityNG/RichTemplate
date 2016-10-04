

<%

' SHOW CURRENT WEB SITE SECTIONS
%>
<!--#INCLUDE FILE="db_connection.asp"-->
<%
'OPEN DATA---------------------------------------------------->

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

'OPEN WEBSITE SECTIONS--------------------------------------->

WEBSECTIONSQL = "SELECT * FROM WEBSECTION"
SET RS = CON.EXECUTE (WEBSECTIONSQL)

'CHECK TO SEE IF ANY EXIST------------------------------------>

	IF NOT RS.EOF THEN
	

%>

<table align="center" border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
<%
		'DISPLAY RECORDS--------------------------------------------->
			WHILE NOT RS.EOF
%>
    <tr>
    
      <td class=bodybold><a href="website.asp?sectionid=<%=RS("id")%>"><%=RS("SECTIONNAME")%></a></td>
</tr>
<%
			RS.MOVENEXT
			WEND%>
</TABLE>
<%
	ELSE
%>
<table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
    <tr>
      <td class=bodybold>There Are Currently No Web Site Sections</td>
      
    </tr>
</table>
<%
	END IF
	
	RS.CLOSE
	CON.CLOSE
	SET WEBSECTIONSQL = NOTHING
%>