<!-- SEARCH FORM START -->

	
	<form method="POST" name="myform" action="search.asp?defaultpage=<%=Request.Querystring("defaultpage")%>&id=<%=Request.Querystring("id")%>&subid=<%=Request.Querystring("ID")%>&sectionID=<%=Request.Querystring("SectionID")%>&pagelevel=<%=Request.Querystring("pagelevel")%>&parentid=<%=Request.Querystring("parentid")%>">
		<table border="0" width="100%" cellspacing="1" cellpadding="0" id="table1">
			<tr>
				<td width="56">
				<p align="right"><font color="#ffffcc"><b>Search:</font></b></td>
				<td width="474"><input type="text" name="q1" size="11">
				<input type="submit" value="Go!" name="B1"></td>
			</tr>
			</table>
	</form>
	
<!-- SEARCH FORM END -->
