<%Session("news")= Request.Querystring("webfeed")
if Session("news") = "" then
Session("news") = "news"
end if
if request.querystring("webfeed")="news" then

Session("bgnews") = "#FF9C00"
else
Session("bgnews") = "#e4e4e4"
end if
if request.querystring("webfeed")="sports" then
Session("bgsports") = "#FF9C00"
else
Session("bgsports") = "#e4e4e4"
end if
if request.querystring("webfeed")="entertainment" then
Session("bgentertainment") = "#FF9C00"
else
Session("bgentertainment") = "#e4e4e4"
end if
strFullPath = Request.ServerVariables("Path_Info")
%>
<table width="311" cellspacing="0" cellpadding="0">
            <tr>
              <td>
                <img border="0" src="images/MYNEWSTOP.gif" width="311" height="28"></td>
            </tr>
         </table> 
         <table cellspacing="0" cellpadding="0">
         <tr>
         <td valign="top">

          <table border="1" width="311" cellspacing="0" cellpadding="0" id="table1" bordercolor="#2857AA">
			<tr>
				<td>          
				<table border="0" width="100%" cellspacing="0" cellpadding="0" height="10">
            <tr>
              <td width="33%" bgcolor="#D8E8FA" height="20">
                <p align="center"><b><a href="<%=strFullPath%>?webfeed=news">
				<font face="Verdana" size="1">News</font></a></b></td>
              <td width="33%" bgcolor="#D8E8FA" height="20">
                <p align="center"><b><a href="<%=strFullPath%>?webfeed=sports"><font face="Verdana" size="1">Sports</font></a></b></td>
              <td width="34%" bgcolor="#D8E8FA" height="20">
                <p align="center"><b><a href="<%=strFullPath%>?webfeed=entertainment"><font face="Verdana" size="1">Entertainment</font></a></b></td>
            </tr>
         <tr><td colspan="3">
			<%if Session("news")= "news" then%>
          <!--#Include File ="headlines.inc"-->
          <%end if%>
          <%if Session("news")= "sports" then%>
          <!--#Include File ="sports.inc"-->
          <%end if%>
          <%if Session("news")= "entertainment" then%>
          <!--#Include File ="entertainment.inc"-->
          <%end if%>
</td>
      </tr>
          
         <td colspan="3" background="images/framebottom.gif">
&nbsp;</td>
     
          
          </table></td>
			</tr>
			</table>

          </td>
          </tr>
         <tr><td valign="top">&nbsp;</td>
         <td>&nbsp;</td>
          </tr>
          </table>
          
          
          <p>&nbsp;</p>

          
          
          