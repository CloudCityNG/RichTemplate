  <table class="mainHeaderTable" cellspacing="0" cellpadding="0">
            <tr>
              <td>
                <font color="#C0C0C0" face="Verdana" size="2"><b><img border="0" src="images/topleftcorner3.gif" width="7" height="25" align="top">Company Documents                                      <img border="0" src="images/toprightcorner3.gif" width="7" height="25" align="top"></b></font></td>
            </tr>
</table>
         <table cellspacing="0" cellpadding="0" height="1" width="100%">
         <tr><td height="69" width="1"></td>
         <td height="69" width="100%">
  <table width="100%" border="1" cellspacing="0" cellpadding="0" class="mainSiteTable">
    
    <tr> 
      <td width="25" align="middle" valign="center">&nbsp;</td>
      <td width="100%">&nbsp;</td>
    </tr>
    
    <%myCatOpenSQL = "Select id, category FROM cats"
    SET RS = CON.EXECUTE (myCatOpenSQL)

    
    
    WHILE NOT RS.EOF
    x=cint(RS("id"))
    %>
    
    
    
    <tr> 
      <td width="25" align="middle" valign="center"><a href="compdocs.asp?expand=<%=RS("id")%>">
      <%IF x = cint(request.querystring("expand"))  THEN %>
      <img height="11" src="images/icon_arrow_c.gif" width="9" border="0">
      <%else%>
      <img height="11" src="images/icon_arrow_a.gif" width="9" border="0">
      <%end if%></a></td>
      <td width="100%"><font face="Arial, Helvetica, sans-serif" size="2"><a href="compdocs.asp?expand=<%=RS("id")%>"><%=RS("category")%></a></font></td>
    </tr>
    <%
    IF x = cint(request.querystring("expand"))  THEN 
      
    mySubOpenSQL = "Select id, name, catid, link FROM subs where catid=" & (RS("id")) & ""
    SET RS2 = CON.EXECUTE(mySubOpenSQL)
    
    WHILE NOT RS2.EOF
    %>
    <tr> 
      <td width="25" align="middle" valign="center">&nbsp;</td>
      <td width="100%"><img src="images/blue_dot.jpg" width="4" height="4"><img src="images/spacer.gif" width="16" height="8"><font face="Arial, Helvetica, sans-serif" size="2"><a href="<%=RS2("link")%>"><b><%=RS2("name")%></b></a></font></td>
    </tr>
    

    <%RS2.MoveNext
    WEND
    
   END IF
   
    RS.MoveNext
    WEND
    
    

    %>
    

  </table>
</td>
          <td valign="top" height="69" width="5">
          		<table width="100%" border=0 cellpadding=0 cellspacing=0 height="100%">
		<tr>
			<td background="images/schatten_u-st4-xx.gif" height="5" width=100% valign="top"><img align="textTop" border="0" src="images/schatten_o-st4-xx.gif" width="5" height="5"></td>
			<tr>
			<td width="5" background="images/schatten_r-st4-xx1.gif" height="100%"><img border="0" src="images/schatten_r-st4-xx1.gif" width="5" height="1"></td>
		</tr>
					<tr>
			<td width="5" background="images/schatten_r-st4-xx1.gif" height="5" valign="bottom"><img border="0" src="images/schatten_r-st4-xx1.gif" width="5" height="1"></td>
		</tr>
		</table>
          
          </td></tr>
         <tr><td valign="top"></td>
         <td valign="top" width="5" background="images/bottomshadowback.gif"><font size="1"><img border="0" src="images/schatten_l-st4-xx.gif" width="5" height="5"></font></td>
          <td valign="top"><img border="0" src="images/schatten_e-st4-xx.gif" width="5" height="5"></td></tr>
          </table>
