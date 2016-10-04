<%IF Request.QueryString("emp")<>"" AND Request.Querystring("emp")<>"all" then
Session("emp") = Request.QueryString("emp")
elseif Request.QueryString("emp")="all" then
Session("emp") = "all" 
else
Session("emp") = Session("emp")
end if
strFullPath = Request.ServerVariables("Path_Info")
%>
<CENTER>
<table WIDTH="100%" cellspacing="0" cellpadding="0">
    <tr>
      <td align="right" colspan="3" BGCOLOR="#3D4A7E" >
        <p align="center"><b><font color="#FFFFFF" face="Arial" size="3">EMPLOYEE DIRECTORY</font></b>
      </td>
    </tr>
</table>
         <table cellspacing="0" cellpadding="0" height="1" width="100%">
         <tr><td height="69" width="1"></td>
         <td height="69" valign="top" width="100%">
            <table border="1" width="100%" cellpadding="0" class="mainSiteTable" cellspacing="0">

                            <tr>
                <td width="100%" align="center" colspan="3">
                <%strFullPath = Request.ServerVariables("Path_Info")%>
                  <font size="1" face="Verdana"><a href="<%=strFullPath%>?emp=all">SHOW ALL</a>&nbsp;&nbsp;&nbsp;<a href="<%=strFullPath%>?emp=A">A</a>&nbsp;<a href="<%=strFullPath%>?emp=B">B</a>&nbsp;<a href="<%=strFullPath%>?emp=C">C</a>&nbsp;<a href="<%=strFullPath%>?emp=D">D</a>&nbsp;<a href="<%=strFullPath%>?emp=E">E</a>&nbsp;<a href="<%=strFullPath%>?emp=F">F</a>&nbsp;<a href="<%=strFullPath%>?emp=G">G</a>&nbsp;<a href="<%=strFullPath%>?emp=H">H</a>&nbsp;<a href="<%=strFullPath%>?emp=I">I</a>&nbsp;<a href="<%=strFullPath%>?emp=J">J</a>&nbsp;<a href="<%=strFullPath%>?emp=K">K</a>&nbsp;<a href="<%=strFullPath%>?emp=L">L</a>&nbsp;<a href="<%=strFullPath%>?emp=M">M</a>&nbsp;<a href="<%=strFullPath%>?emp=N">N</a>&nbsp;<a href="<%=strFullPath%>?emp=O">O</a>&nbsp;<a href="<%=strFullPath%>?emp=P">P</a>&nbsp;<a href="<%=strFullPath%>?emp=Q">Q</a>&nbsp;<a href="<%=strFullPath%>?emp=R">R</a>&nbsp;<a href="<%=strFullPath%>?emp=S">S</a>&nbsp;<a href="<%=strFullPath%>?emp=T">T</a>&nbsp;<a href="<%=strFullPath%>?emp=U">U</a>&nbsp;<a href="<%=strFullPath%>?emp=V">V</a>&nbsp;<a href="<%=strFullPath%>?emp=W">W</a>&nbsp;<a href="<%=strFullPath%>?emp=X">X</a>&nbsp;<a href="<%=strFullPath%>?emp=Y">Y</a>&nbsp;<a href="<%=strFullPath%>?emp=Z">Z</a></font>
                 </td>
              </tr>
              <tr>
                <td width="100%" colspan="3">
<%If Session("emp")<>"" AND Session("emp")<> "all" then
myEmpSQL = "Select * FROM employees WHERE varLname Like '" + Session("emp") + "%' Order by varLname"
set rs = con.execute(myEmpSQL)
if rs.eof then%>
<p><font size="1" face="Verdana">There are currently no listings for the letter <%=session("emp")%>.</font></p>
<%else
While not RS.EOF
%>
<p><font size="1" face="Verdana"><%=RS("varLname")%>,&nbsp;<%=RS("varFname")%><br>
<%=RS("varTitle")%><br>
<a href="mailto:<%=RS("varEmail")%>"><%=RS("varEmail")%></a><br>
<%=RS("varPhone")%></P>
<%RS.MOVENEXT
WEND
end if
end if



IF Session("emp") = "all" then
myEmpSQL = "Select * FROM employees Order by varLname"
set rs = con.execute(myEmpSQL)
if rs.eof then%>
<p><font size="1" face="Verdana">There are currently no listings.</font></p>
<%else
While not RS.EOF
%>
<p><font size="1" face="Verdana"><%=RS("varLname")%>,&nbsp;<%=RS("varFname")%><br>
<%=RS("varTitle")%><br>
<a href="mailto:<%=RS("varEmail")%>"><%=RS("varEmail")%></a><br>
<%=RS("varPhone")%></P>
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
</tr>

          </table>

</CENTER>




















