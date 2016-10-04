<%@ LANGUAGE = "VBSCRIPT" %>
<% Option Explicit %>

<B>Contents of Request.Form:</B><BR>
<TABLE BORDER=1 CELLSPACING=1>
<%
	Dim strName
	For Each strName in Request.Form
		Response.Write "<TR><TD>"
		Response.Write strName & "</TD><TD>" & Request.Form(strName)
		Response.Write "</TD></TR>"
	Next
%>
</TABLE>
<P>
<B>Contents of Request.QueryString:</B><BR>
<TABLE BORDER=1 CELLSPACING=1>
<%
	For Each strName in Request.QueryString
		Response.Write "<TR><TD>"
		Response.Write strName & "</TD><TD>" & Request.QueryString(strName)
		Response.Write "</TD></TR>"
	Next
%>
</TABLE>
<P>