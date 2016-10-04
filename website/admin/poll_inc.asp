<div align="center">

<%
'check for add sub .. someone voted
if Request.QueryString("sub") = "add" then
	add_vote()
else

Dim SQL, SQL_IP
Dim all_voters, poll_id, cookie_id, expir_date, start_date
'On error resume next
'set block voting to false

	'create connection
	set con = server.CreateObject ("ADODB.Connection")
	con.Open ConnectionString
	'query to select active data...
	SQL = "SELECT * FROM poll_title, poll_vote WHERE poll_title.active = 1 AND poll_title.id = poll_vote.poll_id"
	'open recordset
	Set rs = con.execute(SQL)
	
	if not rs.EOF then
	
		'get id of the active poll
		poll_id = rs("id")
		'get number of votes
		all_voters = rs("votes")
		'expiration date for poll
		expir_date = rs("expiration_end")
		start_date = rs("expiration_start")
		'query to get IP numbers
		SQL_IP = "SELECT * FROM poll_ip_block WHERE [poll_id_ip]=" & poll_id & " AND [ip]='" & Request.ServerVariables("REMOTE_ADDR") & "'"	 
		Set rs_IP = con.Execute(SQL_IP)
			
	end if
		
	'get cookie from user
	'cookie_id = request.cookies("currpoll")
%>
 
<table width="120" border="0" cellspacing="0" cellpadding="2">

  
  <%
  'if we are at the end of recordset or expiration date is in the past means that there is no active poll
  if rs.eof or expir_date < date or start_date > date then 
  %>
  
	<tr>
	  <td bgcolor="#F0F5EF">
	    <table width="100%" border="0" cellspacing="0" cellpadding="2" class="nortxtv8">
	      <tr> 
	        <td align="center">No active poll !</td>
	      </tr>
	    </table>
	  </td>
	</tr>
  
  <%else 'if there is active poll%><%
	'we check if user has the cookie or his IP is in the database - for current poll (means that he had allready voted)
	if not cookie_id = Cstr(poll_id) then
	%>
		<tr>
		  <td bgcolor="#F0F5EF">
		    <table width="100%" border="0" cellspacing="0" cellpadding="2" class="nortxtv8">
			  <tr class="nortxtv8"> 
		        <td><%=rs.Fields("title")%></td>
		      </tr>
		      <tr class="nortxtv8"> 
		        <td>
		   			<%
					Dim b, c
					do			
						b = Clng(rs.fields("no_votes"))
						
						'if no one vote ....
					if b = "0" then
					%>
							     
					<b><%=rs.fields("answer")%></b>&nbsp;&nbsp;<font color="0000FF">0%</font><br />
					<font color="0000FF">No votes.</font><br />
					
					<% 
					 else
					 'somebody vote...
					 c = Clng(100 / all_voters * b)			
					%>				  
				  
					<b><%=rs.fields("answer")%></b>&nbsp;&nbsp;<font color="0000FF"><%= c & "%" %></font><br />
					<img src="img/vote.gif" height="6" width="<%'= 1*c %>" alt="<%=rs.Fields("no_votes")%>"><br />
					<%	
						end if
					%>  

					<%
							
					rs.movenext
					loop while not rs.eof
					%>
					<br />
					All voters: <font color="red"><b><%=all_voters%></b></font>
				</td>
		      </tr>
		    </table>
		  </td>
		</tr>
		
	<%else 'no cookie found...vote granted%>				
					
		<tr class="nortxtv8">
		  <td bgcolor="#F0F5EF">
		    <table width="100%" border="0" cellspacing="0" cellpadding="2">
		    <form name="addVote" method="POST" action="?sub=add&id=<%=poll_id%>">
		      <tr class="nortxtv8"> 
		        <td colspan="2"><%=rs.Fields("title")%></td>
		      </tr>
		      
				<%do 'create radio buttons with possible answers%>

					<tr class="nortxtv8"> 
					  <td width="15" nowrap><input type="radio" name="voteFor" value="<%=rs.Fields("answer_id")%>" /></td>
					  <td width="105"><%=rs.Fields("answer")%></td>
					</tr>
		      
		      	<%
				rs.MoveNext
				loop until rs.EOF
				%>
				  
		      <tr class="nortxtv8"> 
		        <td colspan="2" align="center">
					<a href="javascript:document.addVote.submit()">Vote</a>&nbsp;&nbsp;
					<a href="javascript:openWin('prevpoll.asp?id=<%=poll_id%>','Poll','scrollbars=yes,width=450,height=300')">Results</a>
		        </td>
		      </tr>
		      </form>
		    </table>
		  </td>
		</tr>
		
	<%end if%>	
	
  <%end if%>
  
  <%
  'last chance to vote banner
  dim calc
  if expir_date >= date and not start_date > date then
  calc = expir_date - 5
  if calc <= date then
  %>
  	<tr class="nortxtv8">
		<td bgcolor="#99CCFF" align="center"> 
		<%
		select case cstr(date - calc)
		
		case "5":
			Response.Write "Last day !"
		case "4":
			Response.Write "One day to go"
		case "3":
			Response.Write "Two days left"
		case "2":
			Response.Write "Vote, vote, vote!"
		case "1":
			Response.Write "4 days left"
		case "0":
			Response.Write "5 days left"
				
		end select
		%>
		</td>
	</tr>
  <%
  end if
  end if
  %>
  
  		<%'open recordset with inactive poll data
		Dim a
		Dim SQL_inact
		
		rs.close
		set rs = nothing
			
		SQL_inact = "SELECT * FROM poll_title WHERE active = 0 ORDER BY id DESC"
		set rs = con.Execute(SQL_inact)
						
		a = 1
		%>
		
		<%
		'there is no inactive poll in database
		if rs.eof then
		%>
		
		<%
		else
			if  rs("expiration_start") > date then%>

			<%else
		'create previous polls menu...
		%>

			
		<%
			end if
		end if
		%>
 
</table>

<script language="JavaScript">
<!-- //start hiding for older browsers
//create expand menu for previous polls

	var img1 = new Image();
	//plus image
	img1.src = "img/plus.gif";
	var img2 = new Image();
	//minus image
	img2.src = "img/minus.gif";
	
	//create expand menu				
	function doOutline() {
	  var srcId, srcElement, targetElement;
	  srcElement = window.event.srcElement;
	  if (srcElement.className.toUpperCase() == "LEVEL1" || srcElement.className.toUpperCase() == "FAQ") {
			 srcID = srcElement.id.substr(0, srcElement.id.length-1);
			 targetElement = document.all(srcID + "s");
			 srcElement = document.all(srcID + "i");
					
		if (targetElement.style.display == "none") {			
					 targetElement.style.display = "";
					 if (srcElement.className == "LEVEL1") srcElement.src = img2.src;
			} else {
					 targetElement.style.display = "none";
					 if (srcElement.className == "LEVEL1") srcElement.src = img1.src;
		 }
	  }
	}
					
	document.onclick = doOutline;
-->
</script>

<%
rs.close
set rs = nothing
con.close
set con = nothing

end if 'end check for add sub
%>

<%
sub add_vote()

Dim SQL_IP, SQL_upd, SQL_no_votes, SQL_expir, SQL_ip_block
Dim cookie_id, poll_id
on error resume next
	
	'create connection
	set con = server.CreateObject ("ADODB.Connection")
	con.Open ConnectionString
	
	'get id of the current poll and get cookie from the user
	poll_id = Request.QueryString("id")
	cookie_id = request.cookies("currpoll")

	'query for checking if users ip for surrent poll is in the database
	SQL_IP = "SELECT * FROM poll_ip_block WHERE poll_id_ip=" & poll_id & " AND ip='" & Request.ServerVariables("REMOTE_ADDR") & "'"	 
	Set rs_IP = con.Execute(SQL_IP)
	
	'if user has the cookie or he didn't check any option or his ip is in the database
	'then redirect back to the site where he came from
	If cookie_id = Cstr(poll_id) or Request.form("voteFor") = "" or not rs_IP.Eof then
		
		Response.Redirect(Request.ServerVariables("URL"))
	
	'if user didn't vote in thi poll
	else
		
		'query to add one vote to selected...
		SQL_upd = "UPDATE poll_vote SET no_votes=no_votes + 1 WHERE answer_id=" & int(Request.form("voteFor"))
		'query to update votes in title table
		SQL_no_votes = "UPDATE poll_title SET votes=votes + 1 WHERE id=" & int(poll_id)
		'query to get expiration date of the poll
		SQL_expir = "SELECT * FROM poll_title WHERE id=" & int(poll_id)
		'query to add voters ip in the database (for surrent poll)
		SQL_ip_block = "INSERT INTO poll_ip_block (poll_id_ip, ip) VALUES (" & int(poll_id) & ",'" & Request.ServerVariables("REMOTE_ADDR") & "')"
		set rs_upd = con.execute(SQL_upd)
		set rs_no_votes = con.execute(SQL_no_votes)
		set rs_expir = con.execute(SQL_expir)
		set rs_ip_block = con.execute(SQL_ip_block)
		
			'set cookie with expiration date (date in database)	
			Response.cookies("currpoll") = poll_id
			Response.Cookies("currpoll").Expires = rs_expir("expiration")
		
			'close recordset and connection...
			rs_upd.Close
			rs_no_votes.close
			rs_expir.close
			rs_ip_blokck.close
			set rs_upd = nothing
			set rs_no_votes = nothing
			set rs_expir = nothing
			set rs_ip_blokck = nothing
			con.Close
			set con = nothing
		
		'redirect back to the site where we came from
		Response.Redirect(Request.ServerVariables("URL"))
	
	end if

end sub
%>

</div>