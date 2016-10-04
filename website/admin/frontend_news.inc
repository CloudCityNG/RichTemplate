    <table height=100%  border="0" bgcolor="#ffffff" cellpadding="0" cellspacing=0 width="99%">

  <%if request.querystring("id")<>"" then%>
<tr><td colspan=2 align=right height="32"><a href="pressrelease.asp?sectionid=53" class=newslink> +  Back to Press Releases</a>&nbsp;</td></tr>
  <tr>
  <%end if%>
  <td colspan=2 valign=top>

<%if request.querystring("id")<>"" then%>
<center>
      <table border="0" width="95%" cellpadding="5">



      <!--------------------------------GET COMPNEWS INFO------------------------------------------>

      <%mySQL = "Select title_x, summ_x, desc_x FROM PR where id_x=" & request.querystring("id") & ""
      SET RS99 = CON.EXECUTE (mySQL)
      %>

<tr><TD bgcolor="#ffffff" width="100%">
<%=rs99("desc_x")%>
</td>
   
</tr>

<tr><td colspan=2 align=right class=newsheader><a href="pressrelease.asp?sectionid=53" class=newslink>Back to Press Releases</a></td></tr>
</TABLE>   
</center>
<%else%>
<center>
    


      <!--------------------------------GET COMPNEWS INFO------------------------------------------>


    <table border="0" width="95%" cellpadding="2">      
        
      <%mySQL = "Select id_x, title_x, summ_x, ndate FROM PR where mode_x='live' ORDER BY ndate desc"
      SET RS99 = CON.EXECUTE (mySQL)
      WHILE NOT RS99.EOF%>
      <TR>
   
      
<td valign="top">&nbsp;&nbsp;</td><td valign="top" class=frontendnews><%=RS99("ndate")%>- </td><TD valign="top" width="100%" class=frontendnews><a href="pressrelease.asp?sectionid=53&id=<%=RS99("id_x")%>"><%=RS99("title_x")%></a>: <%=RS99("summ_x")%></font><br>&nbsp;</td>
   
</tr>
<%RS99.MoveNext
wend%>
</TABLE>      

</center>
 <%end if
 rs99.close
 %>      
       
</td></tr></table>































































