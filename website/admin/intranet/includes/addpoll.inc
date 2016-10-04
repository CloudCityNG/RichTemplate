<table class="mainHeaderTable" cellspacing="0" cellpadding="0">
            <tr>
              <td>
                <font color="#C0C0C0" face="Verdana" size="2"><b><img border="0" src="images/MYPOLLSTOP.gif"></b></font></td>
            </tr>
         </table> 
         <table cellspacing="0" width="311" cellpadding="0" height="1" bordercolor="#76C9F4" border="1">
         <tr>
         <td width="100%" height="69">
<table width="100%" cellspacing="0" cellpadding="0" align="center">

              <tr>
                <td width="100%" colspan="3">
                
                
                  
                <table>
                
<%   				'GET POLL QUESTIONS--------------------------------------->


                  myPOLL = "Select question, id From polls order by question"
                  set rs = con.execute (myPOLL)
                  while not rs.eof
%>
                                   
                  <tr>
                  	<td><img src="images/icon.gif">  
                     </td>
                     <td><a href=javascript:popUp('poll.asp?id=<%=rs("id")%>')><font face="Verdana" size="1"> <%=rs("question")%></font></a>
                     </td>
                  </tr>
                  
<%
                  rs.movenext
                  wend
%>
                  </table>
              </td>
           </tr>
        </table>
      </td>
    </tr>
    <TR>
     <TD align="right" bordercolor="#FFFFFF" bordercolorlight="#FFFFFF" bordercolordark="#FFFFFF"><a href="javascript: popUp('add_poll.asp')"><img border="0" src="images/polladdicon.gif" alt="Add Poll"></a>
     </TD>
   </TR>
 </table>
          

