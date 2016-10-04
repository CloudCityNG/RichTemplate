

<form method="POST" action="richtemplate_site_admin_save.asp?action=update" name="prefForm">
	<%
		IF Request.Form("packagetype")<>"" THEN
		
		SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
		CON.OPEN ConnectionString
		
		SQL2 = "SELECT * FROM PACKAGE_TYPE WHERE PACKAGEID = "&Request.Form("packagetype")&""
		
		ELSE
		
		SQL2 = "SELECT * FROM PACKAGE_TYPE WHERE PACKAGE_SELECTED = 1"
		
		END IF
		
		Set RS2 = con.Execute(SQL2)
			%>
	
<table border="0" width="269" cellspacing="0" cellpadding="5">

	<tr>
		<td width="146" class="bodybold" height="20" align="left">RichTemplate 
		Features</td>
		<td class="bodybold" height="20" align="center">Enable/Disable</td>
	</tr>

	<tr>
		<td width="146" class="body" height="20" align="left">Administer Sections:&nbsp; </td>
		<td class="body" height="20" align="center">
		<input type="checkbox" name="ADMIN_SECTIONS" value="1" <%IF RS2("ADMIN_SECTIONS")= TRUE THEN
		RESPONSE.WRITE "CHECKED"
		END IF%>></td>
	</tr>
	<tr>
		<td width="146" class="body" height="20" align="left">Add/Delete Pages</td>
		<td class="body" height="20" align="center">
		<input type="checkbox" name="ADMIN_PAGES" value="1"<%IF RS2("ADMIN_PAGES")= TRUE THEN
		RESPONSE.WRITE "CHECKED"
		END IF%>></td>
	</tr>
	<tr>
		<td width="146" class="body" height="20" align="left">Administer Email:&nbsp; </td>
		<td class="body" height="20" align="center">
		<input type="checkbox" name="ADMIN_EMAIL" value="1"<%IF RS2("ADMIN_EMAIL")= TRUE THEN
		RESPONSE.WRITE "CHECKED"
		END IF%>></td>
	</tr>
	<tr>
		<td width="146" class="body" height="20" align="left">Administer Users:&nbsp; </td>
		<td class="body" height="20" align="center">
		<input type="checkbox" name="ADMIN_USERS" value="1"<%IF RS2("ADMIN_USERS")= TRUE THEN
		RESPONSE.WRITE "CHECKED"
		END IF%>></td>
	</tr>
	<tr>
		<td width="146" class="body" height="20" align="left">Enable MicroSites</td>
		<td class="body" height="20" align="center">
		<input type="checkbox" name="ADMIN_MICROSITES" value="1"<%IF RS2("ADMIN_MICROSITES")= TRUE THEN
		RESPONSE.WRITE "CHECKED"
		END IF%>></td>
	</tr>
	<tr>
		<td width="146" class="body" height="20" align="left">Administer All 
		Modules</td>
		<td class="body" height="20" align="center">
		<input type="checkbox" name="ADMIN_MODULES" value="1"<%IF RS2("ADMIN_MODULES")= TRUE THEN
		RESPONSE.WRITE "CHECKED"
		END IF%>></td>
	</tr>
	<%IF RS2("ADMIN_MODULES")= TRUE THEN%>
	<%
	
		SQL2 = "SELECT * FROM MODULES order by ID"
		Set RS = con.Execute(SQL2)
			do while not RS.EOF%>
	
	<tr>
		<td width="146" class="body" height="20" align="left"><%=RS("MODULENAME")%></td>
		<td class="body" height="20" align="center"><input type="checkbox" name="<%=RS("ID")%>" value="ON" <%IF RS("online")=true THEN%>checked<%end if%>></td>
	</tr>
	
	<%rs.movenext
			loop
			%>
			
<%	
	MYSQL10 = "SELECT * FROM CONFIG"
	SET RS3 = CON.EXECUTE(MYSQL10)
			IF RS3.EOF THEN
				 OPT3 = "CHECKED"
				 OPT6 = "CHECKED"
			END IF
			
			IF RS3("SITE_DEPTH") = "1" THEN
				OPT1 = "CHECKED"
			ELSEIF RS3("SITE_DEPTH") = "2" THEN
				OPT2 = "CHECKED"
			ELSEIF RS3("SITE_DEPTH") = "3" THEN
				OPT3 = "CHECKED"
			ELSEIF RS3("SITE_DEPTH") = "4" THEN
				OPT4 = "CHECKED"
			ELSEIF RS3("SITE_DEPTH") = "5" THEN
				OPT5 = "CHECKED"
			END IF
			
			IF RS3("INDEX_PAGE") = TRUE THEN
				OPT6 = "CHECKED"
			ELSEIF RS3("INDEX_PAGE") = FALSE THEN
				OPT7 = "CHECKED"
			END IF
			
				
	%>
			<tr>
				<td colspan="2" class="body">Page Level:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<input type="radio" value="2" name="SITE_DEPTH"<%=OPT2%>>2
				<input type="radio" value="3" name="SITE_DEPTH"<%=OPT3%>>3
				<input type="radio" value="4" name="SITE_DEPTH"<%=OPT4%>>4
				<input type="radio" value="5" name="SITE_DEPTH"<%=OPT5%>>5</td>
		</tr>
			<tr><td colspan="2" class="body">All content in n  index.asp?
				<input type="radio" name="INDEX_PAGE" value="YES" <%=OPT6%>>yes&nbsp;
				<input type="radio" value="NO" name="INDEX_PAGE" <%=OPT7%>>no</td></tr>
			<%END IF%>
<%If Request.Querystring("change")="yes" then%>

<td class="body"><b>Click to save changes &gt;; </td>
<%else%>
<td align="center"></td>
<%END IF%>
<td align="center"><input type="submit" value="Submit" name="B2"><input type="hidden" name="packagetype" <%If Request.Form("packagetype")<>"" then%>
value="<%=Request.Form("packagetype")%>"
<%else%>
value="<%=rs2("packageid")%>"
<%end if%>
 size="20"></td>
<tr><td colspan="2" class="body">&nbsp;</td></tr>

<tr><td colspan="2" class="body">&nbsp;</td></tr>

<tr>
<%if Session("PLATFORM") = 1 THEN%>
<%ELSE%>
<td colspan="2" class="body"><a href="../admin/richtemplate_createsite.asp">Once your new site has been configured, click here to install the RichTemplate
	</a>
</td>
<%END IF%>
</tr></table>

</form>