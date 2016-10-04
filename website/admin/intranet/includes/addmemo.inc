
<table width="400" cellspacing="0" cellpadding="0" border="0" bordercolor="#76C9F4">
            <tr>
              <td>
                <font color="#C0C0C0" face="Verdana" size="2"><b><img border="0" src="images/MYMEMOTOP.gif" width="400" height="28"> </td>                   
            </tr>


         <tr>
         <td width="100%" valign="top">

            <table border="1" bordercolor="#2857AA" width="100%" cellpadding="0" cellspacing="0">

              <tr>
                <td width="100%">
                <TABLE width="100%">
 <%               
 				'GET MEMO TITLE AND AUTHOR--------------------------->
 				
               myMemoSql = "Select id, varDate, varAuthor, varSubject FROM compmemo"
                		Set RS2 = CON.EXECUTE(myMemoSql)
                		
                		
                		WHILE NOT RS2.EOF
%>
                	<TR><TD><img border="0" src="images/icon.gif"></TD><TD><font face="Verdana" size="1"><%=RS2("varDate")%> - <%=RS2("varSubject")%> - <%=RS2("varAuthor")%> [<a href="javascript:popUp('memo.asp?id=<%=RS2("id")%>')">MORE DETAILS</a>]</font></TD></TR>
<%
                		RS2.MOVENEXT
                		WEND
                	
%>
                	</TABLE>
                
                
                
                
                
                
                
                
                </td>
              </tr>
              <tr>
              <td align="right" bordercolor="#FFFFFF" bordercolorlight="#FFFFFF" bordercolordark="#FFFFFF"><a href="javascript: popUp('memoadd.asp?id=-1')"><img border="0" src="images/MEMOADDICON.gif"></a></td>
              </tr>
            </table>

</TD></TR>
</table>
