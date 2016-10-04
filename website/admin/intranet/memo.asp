<!--#include file="loggedin.asp"-->
<!--#include file="conn.asp"-->
<%                
                  
if Request.QueryString("memo") = "add" then
varAuthor = Request.Form("strAuthor")
varAuthor = Replace(varAuthor,"'","''")
varDate = Request.Form("strDate")
varText = Request.Form("strText")
varText = Replace(varText,"'","''")
varMid = Request.Form("strMid")

if varAuthor="" or varText = "" then
else

myMemoUpdate = "Insert Into compmemoresponse (varAuthor, varDate, varText, mid) VALUES ('" + varAuthor + "'," + varDate + ",'" + varText + "'," & varMid & ")"
con.execute(myMemoUpdate)
end if
end if
%>
<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>RichPortal 2001 Memos</title>
<style>
.formRequiredInput {
	font-size : 8pt;
	font-weight : bold;
	font-family : Verdana;
	font-style : normal;
	color : #3D4A7E;
	background : #D5F0FF;
	text-decoration : none;
}
.text{
	font-size : 7pt;
	font-weight : bold;
	font-family : Verdana;
	font-style : normal;
	color : #000000;
	}
</style>
<SCRIPT LANGUAGE="JavaScript">		
function myprint() {
window.parent.focus();
window.print();
}

</script>
<SCRIPT LANGUAGE="JavaScript">


<!-- Begin
function myclose() {
window.close();

}
//  End -->
</script>
</head>

<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">


<table cellspacing="0" cellpadding="0"><tr><td valign="top"></td><td valign="top"></td></tr></table>
<%
IF REQUEST.QUERYSTRING("id")= "" then%>
<font face="Verdana" size="2"><font color="#FF0000">
There was an error retrieving memo. </font>
<%else
myMemoSQL = "SELECT * FROM compmemo WHERE ID=" & REQUEST.QUERYSTRING("id") & ""
SET RS = CON.EXECUTE(myMemoSQL)
%>
<table width="100%" bgcolor="#ffffff"  cellspacing="3" cellpadding="0">
                              <TR><TD BGCOLOR="#3D4A7E" COLSPAN=2><CENTER><FONT FACE="VERDANA" SIZE="2" COLOR="#FFFFFF"><B>MEMO ALERT</B></FONT></CENTER></TD></TR>
                        
                    

<tr>
<td align="right" bgcolor="#EFEFEF" width="133"><b><font face="Verdana" size="1">Memo Subject:</font></b></td><td width="744" bgcolor="#EFEFEF"><b><font face="Verdana" color="#000080" size="1"><%=RS("varSubject")%></font></b></td></tr>
<tr>
<td align="right" width="133" bgcolor="#F4F4F4"><b><font face="Verdana" size="1">Memo Date:</font></b></td><td width="744" bgcolor="#F4F4F4"><b><font face="Verdana" color="#000080" size="1"><%=RS("varDate")%></font></b></td>
<tr>
<td align="right" width="133" bgcolor="#EFEFEF"><b><font face="Verdana" size="1">Memo Content:</font></b></td><td width="744" bgcolor="#EFEFEF"><font face="Verdana" color="red" size="1"><%=RS("varText")%></font></td></tr>
<tr>
<td align="right" width="133" bgcolor="#F4F4F4"><b><font face="Verdana" size="1">Memo Author:</font></b></td><td width="744" bgcolor="#F4F4F4"><font face="Verdana" color="#000080" size="1"><%=RS("varAuthor")%></font></td></tr>
</table>
<%
SET RS = nothing
SET myMemoSQL = nothing%>
 
        
        
        
        
        
<%myMemoOpinionSQL = "Select * From compmemoresponse where mid=" + Request.QueryString("id") + " ORDER BY varDate desc"
SET RS = con.execute(myMemoOpinionSQL)
if not rs.eof then%>
<TABLE WIDTH="100%">

                              <TR><TD BGCOLOR="#3D4A7E" COLSPAN=2><CENTER><FONT FACE="VERDANA" SIZE="2" COLOR="#FFFFFF"><B>MEMO THREAD</B></FONT></CENTER></TD></TR>

        
    </TABLE>  
<%
end if
While not RS.eof
%>
<table width="100%" bgcolor="#ffffff"  cellspacing="3" cellpadding="0">
<tr>
<td align="left" bgcolor="#efefef"  width="883">
<font face="Verdana" size="2"><%=RS("varDate")%> - <%=RS("varAuthor")%> - <%=RS("varText")%></font><br>
</td></tr></table>
<%RS.MOVENEXT
WEND
end if
SET RS = NOTHING
SET myMemoOpinionSQL = NOTHING
CON.CLOSE%>
<TABLE WIDTH="100%">

                              <TR><TD BGCOLOR="#3D4A7E" COLSPAN=2><CENTER><FONT FACE="VERDANA" SIZE="2" COLOR="#FFFFFF"><B>RESPOND TO MEMO</B></FONT></CENTER></TD></TR>

        
    </TABLE> 
<form method="POST" action="memo.asp?memo=add&id=<%=Request.QueryString("id")%>">
<input type="hidden" value="<%=Request.QueryString("id")%>" name="strMid">
<input type="hidden" value="<%=date%>" name="strDate">
  <table border="0" width="549" cellspacing="3" cellpadding="0">

    <tr>
      <td class="text" width="168" bgcolor="#F4F4F4">
        <p align="right"><font face="Verdana" size="2"><b>Respond to Memo:</b></font></td>
      <td width="386" bgcolor="#F4F4F4"><textarea class="formRequiredInput" rows="2" name="strText" cols="43"></textarea></td>
    </tr>
    <tr>
      <td class="text" width="168" bgcolor="#F4F4F4">
        <p align="right"><font face="Verdana" size="2"><b>Name:</b></font></td>
      <td width="386" bgcolor="#F4F4F4"><input class="formRequiredInput" type="text" name="strAuthor" size="43"></td>
    </tr>
    <tr>
      <td width="120"></td>
      <td width="386"><input class="formRequiredInput" type="submit" value="Submit" class="formButtons" name="B1"></td>
    </tr>
  </table>
  <p>&nbsp;</p>
</form>
			  <TABLE WIDTH="100%">		<tr><td colspan=2 bgcolor="#FFFFFF" align="right"><form><img src="images/print.gif" value="Print Page" onClick="myprint()"><img src="images/close.gif" value="Close Window" onClick="myclose()"> </center></form>
</td></tr></TABLE>
</body>

</html>





