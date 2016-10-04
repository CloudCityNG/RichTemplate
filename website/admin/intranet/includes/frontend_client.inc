 <!--BEGIN Publication Nav HERE--> 
<TABLE>
	<TR>
		<TD><IMG SRC="images/companydocstop.gif"></TD>
	</TR>
	<TR>
		<TD>
		<form method="post" action="client/index.asp?module=clientevent">
			<TABLE class=sstable2>
				<TR>
					<TD WIDTH="100%" colspan="3">
					<select name=client class=formrequiredinput2>
										<option value="">Select Client</OPTION>

<%
						mySql="SELECT * FROM CLIENT ORDER BY CLIENT"
						SET RS=CON.EXECUTE (mySql)
					
							IF NOT RS.EOF THEN

									WHILE NOT RS.EOF
%>
										<option value="<%=RS("id")%>"><%=RS("client")%></OPTION>
<%
									RS.MOVENEXT
									WEND
						
							ELSE
					
%>
								<option value="0">YOU HAVE NO CLIENTS</OPTION>
<%
							END IF
%>							
						
						</SELECT><input type="submit" value="Submit" name="B1">
</td>
</TR>
<TR>
					<TD WIDTH="33%" >
                                                  <input type="radio" name="service" value="note">Check/write
                              notes        
</td>
					<TD WIDTH="33%" ><input type="radio" name="service" value="contact">
                            Add contact
</td>
					<TD WIDTH="33%" ><input type="radio" name="service" value="schedule">Schedule
                      Meeting
</td>
</tr>
<TR>
					<TD WIDTH="33%" ><input type="radio" name="service" value="search">Search
                            Docs
        
</td>
					<TD WIDTH="33%" ><input type="radio" name="service" value="upload">Upload
                            Doc
</td>
					<TD WIDTH="33%" valign="bottom" align="right" >

                        
                      
</td>
</tr>
</TABLE>
</form> 
</TD>
</TR>
</TABLE>
<!-- END Publication Nav-->
