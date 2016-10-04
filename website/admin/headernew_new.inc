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

<head>
<link rel="stylesheet" type="text/css" href="/admin/style_richtemplate.css">

<%If headerType="simple" then%>



</head>

<table width="100%" cellspacing="0" cellpadding="0" id="table1" height="28" class="headerRepeat"><tr>
  <td  width="1%" height="28">
	&nbsp;</td>
	<td   height="28">
	<%=PNAME%></td>
	</tr></font>
</table>
<%else%>

<table width="100%" cellspacing="0" cellpadding="0" id="table2" height="28">
	<tr class="headerRepeat">
  <td  width="1%" height="28">
	&nbsp;</td>
	<td   height="28">
	<%=PNAME%></td>
	<td   height="28" width="24">
<%If PHELP <>"" THEN%><img src="/admin/images/icon_need_help.gif" width="20" height="28"><%END IF%></td>
	<td  class="style5" height="28" width="72">
<%If PHELP <>"" THEN%>
	<a span class="headerlink" href="javascript: helpPop('<%=PHELP%>')">Need Help?</a>
<%END IF%>
	</td>
	<td  class="style5"   height="28" width="10">
<%If PHELP <>"" THEN%>
	<img src="/admin/images/seperator.gif">
<%END IF%>
</td>
	<td   height="28" width="22">
	<img src="/admin/images/icon_find_bug.gif" width="20" height="28"></td>
	<td   height="28" width="77"  >
	
	<a  span class="headerlink" href="javascript: bugPop('/admin/bugreport.asp')">
	&nbsp;Find a bug?</a>
	

	</td>




	<td     height="28" width="10">
	<img src="/admin/images/seperator.gif"></td>




	<td  height="28" width="20">
	
	<img src="/admin/images/editcontentbg_logout.gif" width="20" height="28"></td>
	<td  class="style1" height="28" width="83">
	<a target="_top" span class="headerlink"  href="/admin/richtemplate_killsession.asp">
	&nbsp;Logout</a></td></tr>
</table>
<%end if%>