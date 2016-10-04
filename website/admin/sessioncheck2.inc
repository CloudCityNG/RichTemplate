
<%


Response.AddHeader "Refresh",CStr(CInt(Session.Timeout + 1) * 60)
Response.AddHeader "cache-control", "private"
Response.AddHeader "Pragma","No-Cache"
Response.Buffer = TRUE
Response.Expires = 0
Response.ExpiresAbsolute = 0

If (Session("Authenticated") <> Session.SessionID) Then
	Session("RequestedURL") = "https://" & _
	    Request.ServerVariables("SERVER_NAME") & _
		Request.ServerVariables("SCRIPT_NAME")

	Temp = Request.ServerVariables("QUERY_STRING")
	If (Not(ISNull(Temp)) AND Temp <> "") Then
		Session("RequestedURL") = Session("RequestedURL") & _
		    "?" & Temp
	End If
	Response.Redirect"https://www.mailroids.com/index.asp?type=error4"
End If

%>



