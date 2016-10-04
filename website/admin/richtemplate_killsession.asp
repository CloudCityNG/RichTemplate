<% Session.Abandon
	Response.Cookies(strUniqueID & "User") = ""
 %>
<%Response.Redirect "/richadmin/"%>

