

<table border="0" width="100%" cellspacing="0" cellpadding="0" id="table2" class="body">
	<tr>
		<td height="25">Choose the RichTemplate Package for Deployment</td>
	</tr>
	<tr>
		<td>
		<form method="POST" name="SSPackageForm" action="richtemplate_site_admin.asp?page=go<%if Request.Querystring("page")="change" then%>
		<%Response.Write "&change=yes"%>
		<%end if%>">
			<p><select size="1" name="PackageType" onchange="javascript:SSPackageForm.submit();">
			<!--<option selected<%If Session("PackageID")="1" then%> value="1">RichTemplate Lite
			<%elseif Session("PackageID")="2" then%>	value="2">RichTemplate Gold
			<%elseif Session("PackageID")="3"then%>	value="3">RichTemplate Platinum
			<%else%>value="1">RichTemplate Lite
			<%end if%>
			</option>-->
			<option value="1">RichTemplate Lite</option>
			<option value="2">RichTemplate Gold</option>
			<option value="3">RichTemplate Platinum</option>
			</select>&nbsp; <!--<input type="submit" value="Submit" name="B1">--></p>
		</form>
	</tr>
</table>