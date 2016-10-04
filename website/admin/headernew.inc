<SCRIPT LANGUAGE="JavaScript">
<!-- Begin
function bugPop(URL) {
day = new Date();
id = day.getTime();
eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=yes,location=0,statusbar=0,menubar=0,resizable=0,width=510,height=460,left = 100,top = 100');");
}
// End -->
</script>

<SCRIPT LANGUAGE="JavaScript">
<!-- Begin
function helpPop(URL) {
day = new Date();
id = day.getTime();
eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=yes,location=0,statusbar=0,menubar=0,resizable=0,width=600,height=460,left = 100,top = 100');");
}
// End -->
</script>

<%If headerType="simple" then%>

<table width="100%" cellspacing="0" cellpadding="0" id="table1" height="28"><tr>
  <td background="/admin/images/editcontentbg.gif" width="1%" height="28" valign="bottom">
	&nbsp;</td>
	<td background="/admin/images/editcontentbg.gif" class="blueheader" height="28">
	<b><font face="Arial" color="#FFFFFF" size="2"><%=PNAME%></font></b></td>
	</tr></font>
</table>
<%else%>

<table width="100%" cellspacing="0" cellpadding="0" id="table2" height="28"><tr>
  <td background="/admin/images/editcontentbg.gif" width="1%" height="28" valign="bottom">
	&nbsp;</td>
	<td background="/admin/images/editcontentbg.gif" class="blueheader" height="28">
	<b><font face="Arial" color="#FFFFFF" size="2"><%=PNAME%></font></b></td>
	<td background="/admin/images/editcontentbg.gif" class="blueheader" height="28" width="24" valign="top">
<%If PHELP <>"" THEN%><img border="0" src="/admin/images/icon_need_help.gif" width="20" height="28"><%END IF%></td>
	<td background="/admin/images/editcontentbg.gif" class="bodybold" height="28" width="72">
<%If PHELP <>"" THEN%>
	<font color="#FFFFFF"><a href="javascript: helpPop('<%=PHELP%>')"><span style="text-decoration: none">
	<font color="#FFFFFF"> Need Help?</font></span></a></font>
<%END IF%>
	</td>
	<td background="/admin/images/editcontentbg.gif" class="bodybold"  class="blueheader" class="bodybold"height="28" width="10">
<%If PHELP <>"" THEN%>
	<img border="0" src="/admin/images/seperator.gif">
<%END IF%>
</td>
	<td background="/admin/images/editcontentbg.gif" class="blueheader" height="28" width="22" valign="top">
	<img border="0" src="/admin/images/icon_find_bug.gif" width="20" height="28"></td>
	<td background="/admin/images/editcontentbg.gif" class="bodybold" height="28" width="77" class="bodybold" >
	
	<a href="javascript: bugPop('/admin/bugreport.asp')">
	<font color="#FFFFFF"><span style="text-decoration: none">&nbsp;Find a bug?</span></font></a>
	

	</td>




	<td background="/admin/images/editcontentbg.gif" class="bodybold"  class="bodybold"  class="blueheader" class="bodybold"height="28" width="10">
	<img border="0" src="/admin/images/seperator.gif"></td>




	<td class="blueheader" height="28" width="20" background="/admin/images/editcontentbg.gif" valign="top">
	<a target="_top" href="/admin/richtemplate_killsession.asp">
	<img border="0" src="/admin/images/editcontentbg_logout.gif" width="20" height="28"></a></td>
	<td background="/admin/images/editcontentbg.gif" class="bodybold" height="28" width="83"><font color='#3055A9'>
	<a target="_top" href="/admin/richtemplate_killsession.asp">
	<font color="#FFFFFF"><span style="text-decoration: none">&nbsp;Logout</span></font></a></td></tr></font>
</table>
<%end if%>