<%

menuPage = Request.ServerVariables("Script_Name")
menuPage = Mid(menuPage,InstrRev(menuPage,"/") + 1)
%>
<img src="images/spacer.gif" width="1" height="8"><br>
<table cellspacing="0" cellpadding="0" border="0">
<tr>
<td width="230" nowrap>&nbsp;</td>
<td width="100%">
<% If History AND menuPage = "igallery.asp" Then %>
<table cellspacing="0" cellpadding="0" border="0">
<tr>
<td><img src="images/newlink12.gif" width=27 height=10 border=0 alt=""></td>
<td><font class="textxsm">&nbsp;- <%= IGfoot_his1 %>&nbsp;&nbsp;&nbsp;</td>
<td><img src="images/newlink8.gif" width=27 height=10 border=0 alt=""></td>
<td><font class="textxsm">&nbsp;- <%= IGfoot_his2 %>&nbsp;&nbsp;&nbsp;</td>
<td><img src="images/newlink4.gif" width=27 height=10 border=0 alt=""></td>
<td><font class="textxsm">&nbsp;- <%= IGfoot_his3 %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
</table>
<% End If %>
</td>
<td width="160" align="right" nowrap><font class="textsm">&nbsp;&nbsp;&nbsp;</font></td>
</tr>
</table>
