<%
	If Session("isLoggedIn") <> True Then
		Response.Redirect "default.asp"
	End If
%>