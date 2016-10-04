<%IF Request.QueryString("dir")<>"" AND Request.Querystring("dir")<>"all" then
Session("dir") = Request.QueryString("dir")
elseif Request.QueryString("dir")="all" then
Session("dir") = "all" 
else
Session("dir") = Session("dir")
end if%>
<CENTER>
<table WIDTH="100%" cellspacing="0" cellpadding="0">
    <tr>
      <td align="right" colspan="3" BGCOLOR="#3D4A7E" >
        <p align="center"><b><font color="#FFFFFF" face="Arial" size="3">VENDOR DIRECTORY</font></b>
      </td>
    </tr>
</table>

         <table cellspacing="0" cellpadding="0" height="1" width="100%">
         <tr><td height="69" width="1"></td>
         <td valign="top" height="69" width="100%">
            <table border="1" width="100%" cellpadding="0" class="mainSiteTable" cellspacing="0">
     
              <tr>
                <td width="100%" align="center" colspan="3">
                <%strFullPath = Request.ServerVariables("Path_Info")%>
                  <font size="1" face="Verdana"><a href="<%=strFullPath%>?dir=all">SHOW ALL</a>&nbsp;&nbsp;&nbsp;<a href="<%=strFullPath%>?dir=A">A</a>&nbsp;<a href="<%=strFullPath%>?dir=B">B</a>&nbsp;<a href="<%=strFullPath%>?dir=C">C</a>&nbsp;<a href="<%=strFullPath%>?dir=D">D</a>&nbsp;<a href="<%=strFullPath%>?dir=E">E</a>&nbsp;<a href="<%=strFullPath%>?dir=F">F</a>&nbsp;<a href="<%=strFullPath%>?dir=G">G</a>&nbsp;<a href="<%=strFullPath%>?dir=H">H</a>&nbsp;<a href="<%=strFullPath%>?dir=I">I</a>&nbsp;<a href="<%=strFullPath%>?dir=J">J</a>&nbsp;<a href="<%=strFullPath%>?dir=K">K</a>&nbsp;<a href="<%=strFullPath%>?dir=L">L</a>&nbsp;<a href="<%=strFullPath%>?dir=M">M</a>&nbsp;<a href="<%=strFullPath%>?dir=N">N</a>&nbsp;<a href="<%=strFullPath%>?dir=O">O</a>&nbsp;<a href="<%=strFullPath%>?dir=P">P</a>&nbsp;<a href="<%=strFullPath%>?dir=Q">Q</a>&nbsp;<a href="<%=strFullPath%>?dir=R">R</a>&nbsp;<a href="<%=strFullPath%>?dir=S">S</a>&nbsp;<a href="<%=strFullPath%>?dir=T">T</a>&nbsp;<a href="<%=strFullPath%>?dir=U">U</a>&nbsp;<a href="<%=strFullPath%>?dir=V">V</a>&nbsp;<a href="<%=strFullPath%>?dir=W">W</a>&nbsp;<a href="<%=strFullPath%>?dir=X">X</a>&nbsp;<a href="<%=strFullPath%>?dir=Y">Y</a>&nbsp;<a href="<%=strFullPath%>?dir=Z">Z</a></font>
                 </td>
              </tr>
              <tr>
                <td width="100%" colspan="3">
                
                
<%

If Session("dir")<>"" AND Session("dir")<> "all" then
myEmpSQL = "Select * FROM vendors WHERE varCompany Like '" + Session("dir") + "%' Order by varCompany"
set rs = con.execute(myEmpSQL)
if rs.eof then%>
<p><font size="1" face="Verdana">There are currently no listings for the letter <%=session("dir")%>.</font></p>
<%else
While not RS.EOF
%>
<p><font size="1" face="Verdana"><b><%=RS("varCompany")%></b>&nbsp;-&nbsp;<%=RS("varCompType")%><br>
Contact:  <%=RS("varFname")%><br>
Phone:  <%=RS("varPhone")%><br>
Email:  <a href="mailto:<%=RS("varEmail")%>"><%=RS("varEmail")%></a><br>
Address:  <%=RS("varAddress")%>, &nbsp;<%=RS("varCity")%>,&nbsp;<%=RS("varState")%>,&nbsp;<%=RS("varZip")%><br>
Web Address:  <a href="<%=RS("varWeb")%>"><%=RS("varWeb")%></a><br>
</P>
<%RS.MOVENEXT
WEND
end if
end if

IF Session("dir") = "all" then
myEmpSQL = "Select * FROM vendors Order by varCompany"
set rs = con.execute(myEmpSQL)
if rs.eof then%>
<p><font size="1" face="Verdana">There are currently no listings.</font></p>
<%else
While not RS.EOF
%>
<p><font size="1" face="Verdana"><b><%=RS("varCompany")%></b>&nbsp;-&nbsp;<%=RS("varCompType")%><br>
Contact:  <%=RS("varFname")%><br>
Phone:  <%=RS("varPhone")%><br>
Email:  <a href="mailto:<%=RS("varEmail")%>"><%=RS("varEmail")%></a><br>
Address:  <%=RS("varAddress")%>, &nbsp;<%=RS("varCity")%>,&nbsp;<%=RS("varState")%>,&nbsp;<%=RS("varZip")%><br>
Web Address:  <a href="<%=RS("varWeb")%>"><%=RS("varWeb")%></a><br>
</P>
<%RS.MOVENEXT
WEND
end if
end if
SET RS = NOTHING
SET myEmpSQL = NOTHING
%>
                
                
                
                
                
                
                
                
                </td>
              </tr>
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


</center>














