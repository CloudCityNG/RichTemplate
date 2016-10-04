<table class="mainHeaderTable" cellspacing="0" cellpadding="0">
            <tr>
              <td>
                <font color="#C0C0C0" face="Verdana" size="2"><b><img border="0" src="images/topleftcorner3.gif" width="7" height="25" align="top">Company Articles                    
<a href="javascript:popUp('editor.asp?id=-1')"><img src="images/addicon.gif" border="0" alt="Add Article"></a><img border="0" src="images/toprightcorner3.gif" width="7" height="25" align="top"></b></font></td>
            </tr>
</table>
         <table cellspacing="0" cellpadding="0" height="1" width="100%">
         <tr><td height="69" width="1"></td>
         <td height="69" width="100%">

          <table width="100%" cellpadding="0" cellspacing="0" class="mainSiteTable" border="1">
              
              <tr><td valign="top" colspan="3"><font face="Verdana" size="2"><b>Company
                News</b></font>
                <p align="left"><%mySQL="Select id,title, cat from messages where cat='COMPANY NEWS'"
                  Set RS = con.execute (mySQL)%>

<%if rs.eof then%>
  <FONT FACE="ARIAL" SIZE="1">There are no articles here.</font> 
  <%else%>
  <ul>
     <%while not rs.eof%>
                <li> <FONT FACE="ARIAL" SIZE="1"><a href="articles.asp?cat=<%=RS("cat")%>&id=<%=rs("id")%>"><%=RS("title")%></a></FONT></li>
    
    <%
    rs.movenext
    wend%>
    </ul><%
    END IF 
    rs.close
    SET rs = nothing
    set mySQL = nothing
    %> 
   </td> </tr>
    
                  
              <tr><td valign="top" colspan="3"><font face="Verdana" size="2"><b>Events</b></font>
               
                               <p align="left"><%mySQL="Select id,title, cat from messages where cat='UPCOMING EVENTS'"
                  Set RS = con.execute (mySQL)%>

<%if rs.eof then%>
  <FONT FACE="ARIAL" SIZE="1">There are no articles here.</font> 
  <%else%>
  <ul>
     <%while not rs.eof%>
                <li> <FONT FACE="ARIAL" SIZE="1"><a href="articles.asp?cat=<%=RS("cat")%>&id=<%=rs("id")%>"><%=RS("title")%></a></FONT></li>
    
    <%
    rs.movenext
    wend%>
    </ul><%
    END IF 
    rs.close
    SET rs = nothing
    set mySQL = nothing
    %> 
               
               
               </td></tr>
               
               <tr><td valign="top" colspan="3"><font face="Verdana" size="2"><b>Projects</b></font>
                                              <p align="left"><%mySQL="Select id,title, cat from messages where cat='RECENT PROJECTS'"
                  Set RS = con.execute (mySQL)%>

<%if rs.eof then%>
  <FONT FACE="ARIAL" SIZE="1">There are no articles here.</font> 
  <%else%>
  <ul>
     <%while not rs.eof%>
                <li> <FONT FACE="ARIAL" SIZE="1"><a href="articles.asp?cat=<%=RS("cat")%>&id=<%=rs("id")%>"><%=RS("title")%></a></FONT></li>
    
    <%
    rs.movenext
    wend%>
    </ul><%
    END IF 
    rs.close
    SET rs = nothing
    set mySQL = nothing
    %> 
               
               
               
               	</td>
               </tr>
               
               <tr><td valign="top" colspan="3"><font face="Verdana" size="2"><b>Human
                Resources</b></font>
                                                              <p align="left"><%mySQL="Select id,title, cat from messages where cat='HUMAN RESOURCES'"
                  Set RS = con.execute (mySQL)%>

<%if rs.eof then%>
  <FONT FACE="ARIAL" SIZE="1">There are no articles here.</font> 
  <%else%>
  <ul>
     <%while not rs.eof%>
                <li> <FONT FACE="ARIAL" SIZE="1"><a href="articles.asp?cat=<%=RS("cat")%>&id=<%=rs("id")%>"><%=RS("title")%></a></FONT></li>
    
    <%
    rs.movenext
    wend%>
    </ul><%
    END IF 
    rs.close
    SET rs = nothing
    set mySQL = nothing
    %> 

                
                </td>
                </tr>
                
                <tr><td valign="top" colspan="3"><font face="Verdana" size="2"><b>Financial</b></font>
                                                              <p align="left"><%mySQL="Select id,title, cat from messages where cat='FINANCIAL DEPT.'"
                  Set RS = con.execute (mySQL)%>

<%if rs.eof then%>
  <FONT FACE="ARIAL" SIZE="1">There are no articles here.</font> 
  <%else%>
  <ul>
     <%while not rs.eof%>
                <li> <FONT FACE="ARIAL" SIZE="1"><a href="articles.asp?cat=<%=RS("cat")%>&id=<%=rs("id")%>"><%=RS("title")%></a></FONT></li>
    
    <%
    rs.movenext
    wend%>
    </ul><%
    END IF 
    rs.close
    SET rs = nothing
    set mySQL = nothing
    %> 

                
                </td></tr>
                
                <tr><td valign="top" colspan="3"><font face="Verdana" size="2"><b>Policy</b></font>
                
                                                              <p align="left"><%mySQL="Select id,title, cat from messages where cat='POLICY COMMENTS'"
                  Set RS = con.execute (mySQL)%>

<%if rs.eof then%>
  <FONT FACE="ARIAL" SIZE="1">There are no articles here.</font> 
  <%else%>
  <ul>
     <%while not rs.eof%>
                <li> <FONT FACE="ARIAL" SIZE="1"><a href="articles.asp?cat=<%=RS("cat")%>&id=<%=rs("id")%>"><%=RS("title")%></a></FONT></li>
    
    <%
    rs.movenext
    wend%>
    </ul><%
    END IF 
    rs.close
    SET rs = nothing
    set mySQL = nothing
    %> 

                
                
                
                </td></tr>
                
                <tr><td valign="top" colspan="3"><font face="Verdana" size="2"><b>Press
                Releases</b></font>
                
                                                              <p align="left"><%mySQL="Select id,title, cat from messages where cat='PRESS RELEASES'"
                  Set RS = con.execute (mySQL)%>

<%if rs.eof then%>
  <FONT FACE="ARIAL" SIZE="1">There are no articles here.</font> 
  <%else%>
  <ul>
     <%while not rs.eof%>
                <li> <FONT FACE="ARIAL" SIZE="1"><a href="articles.asp?cat=<%=RS("cat")%>&id=<%=rs("id")%>"><%=RS("title")%></a></FONT></li>
    
    <%
    rs.movenext
    wend%>
    </ul><%
    END IF 
    rs.close
    SET rs = nothing
    set mySQL = nothing
    %> 

                
                </td>
                </tr>
                <tr><td valign="top" colspan="3" ><font face="Verdana" size="2"><b>Leisure</b></font>
                
                
                                                              <p align="left"><%mySQL="Select id,title, cat from messages where cat='FUN AND LEISURE'"
                  Set RS = con.execute (mySQL)%>

<%if rs.eof then%>
  <FONT FACE="ARIAL" SIZE="1">There are no articles here.</font> 
  <%else%>
   <ul>
     <%while not rs.eof%>
                <li> <FONT FACE="ARIAL" SIZE="1"><a href="articles.asp?cat=<%=RS("cat")%>&id=<%=rs("id")%>"><%=RS("title")%></a></FONT></li>
    
    <%
    rs.movenext
    wend%>
    </ul><%
    END IF 
    rs.close
    SET rs = nothing
    set mySQL = nothing
    %> 

                
                
                
                
                </td></tr>
                
                
                <tr><td valign="top" colspan="3"><font face="Verdana" size="2"><b>Web
                Resourses</b></font>
                
                                                              <p align="left"><%mySQL="Select id,title, cat from messages where cat='USEFUL WEB RESOURCES'"
                  Set RS = con.execute (mySQL)%>

<%if rs.eof then%>
  <FONT FACE="ARIAL" SIZE="1">There are no articles here.</font> 
  <%else%>
  <ul>
     <%while not rs.eof%>
                <li> <FONT FACE="ARIAL" SIZE="1"><a href="articles.asp?cat=<%=RS("cat")%>&id=<%=rs("id")%>"><%=RS("title")%></a></FONT></li>
    
    <%
    rs.movenext
    wend%>
    </ul><%
    END IF 
    rs.close
    SET rs = nothing
    set mySQL = nothing
    %> 

                
                
                
                
                </td></tr>
                <tr><td valign="top" colspan="3"><font face="Verdana" size="2"><b>FAQ's</b></font>
                
                                                              <p align="left"><%mySQL="Select id,title, cat from messages where cat='FAQS'"
                  Set RS = con.execute (mySQL)%>

<%if rs.eof then%>
  <FONT FACE="ARIAL" SIZE="1">There are no articles here.</font> 
  <%else%>
   <ul>
     <%while not rs.eof%>
                <li> <FONT FACE="ARIAL" SIZE="1"><a href="articles.asp?cat=<%=RS("cat")%>&id=<%=rs("id")%>"><%=RS("title")%></a></FONT></li>
    
    <%
    rs.movenext
    wend%>
    </ul><%
    END IF 
    rs.close
    SET rs = nothing
    set mySQL = nothing
    %> 

                
                </td></tr></table>
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