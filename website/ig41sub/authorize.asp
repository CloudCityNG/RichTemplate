<%@ LANGUAGE="VBSCRIPT" %>

<%
'####################################
'## Application:   Blue-Collar Productions
'## File Name:     authorize.asp
'## File Version:  i-Gallery
'## Notice:        This code has limited warranties. Please see http://www.b-cp.com for details.
'####################################
%>

<!--#INCLUDE VIRTUAL="/ig41sub/include/globalsets.asp"-->

<%


'strredirecturl     = Request("redirecturl")
'If strredirecturl <> "" Then
'strredirecturl     = Request("redirecturl")
'Else
strredirecturl = "/ig41sub/igallery.asp"
'End If

'If Request.QueryString("w") = "X" Then
strusername = "adminuser"
strusername = Encode(strusername)
strpassword = "adminpass"
strpassword = Encode(strpassword)
response.write strusername
'End If

'If Request.Form("rememberme") = "1" THEN 
'response.cookies("rememberuser")("username")= Request("txtName") 
'response.cookies("rememberuser")("password")= Request("txtPassword")
'response.cookies("rememberuser")("yesno")= Request("rememberme")
'Response.Cookies("rememberuser").Expires="Jan 1, 2010"
'Else
'response.cookies("rememberuser").Expires=Date-1
'End If

If  strusername <> "" _
AND strpassword <> "" Then
Dim DataConnection, cmdDC, RS
Dim RecordToEdit, Updated, strUserName, strPassword

Set AuthorizeConn = Server.CreateObject ("ADODB.Connection")
AuthorizeConn.Open strConnect

If strusername <> "" Then	
SQL = "SELECT * FROM IGUsers " & _
"WHERE IGUsers.username='" & strusername& _
"' AND IGUsers.password ='" & strpassword &  _
"' "

End If

Set RS = Server.CreateObject("ADODB.Recordset")
Set RS = AuthorizeConn.Execute(SQL)

If Not RS.EOF Then

Session("login") = "yes"
struserLevel = RS("userLevel")
Session("userLevel") = struserLevel
strmemberid  		= RS("memberid")
Session("memberid") = strmemberid
struserFirstName = RS("firstname")
struserLastName  = RS("lastname")
Session("userFullName")  = struserFirstName &" "& struserLastName

struseremail = RS("email")
Session("userEmail")  = struseremail

Session.Timeout = 10

struserid 		= RS("memberid")
strfirstname 	= RS("firstname")
strlastname 	= RS("lastname")
stremail 		= RS("email")
strtime 		= FormatDateTime(igalleryNow,3)
strdate 		= FormatDateTime(igalleryNow,2)

Set ConnLog = Server.CreateObject("ADODB.Connection")
Set lRS = Server.CreateObject("ADODB.Recordset")
ConnLog.Open strConnect

Set lRS.ActiveConnection = ConnLog
lRS.CursorType = adOpenStatic
lSQL = "SELECT ID, userid, firstname, lastname, email, timein, datein, total FROM Log WHERE userid = '" & struserid & "'"
lRS.Open lSQL,,,adCmdTable
if lRS.recordcount = 0 then
lRS.AddNew
lRS("userid") = struserid
lRS("firstname") = strfirstname
lRS("lastname") = strlastname
lRS("email") = stremail
lRS("timein") = strtime
lRS("datein") = strdate
lRS("total") = 0
end if
lRS("timein") = strtime
lRS("datein") = strdate
lRS("total") = CLng(lRS("total")) + 1
lRS.update
lRS.close

strredirect = strredirecturl&"?d="&URLSpace(Request("d"))
Response.Redirect strredirect

Else

Response.Redirect "login.asp?field=nof&n="&Request("redirecturl")&"&d="&URLSpace(Request("folder"))

End If
End If


Sub LogOff
Session.Abandon
strredirecturl 	= "/ig41sub/igallery.asp"
strredirect 	= strredirecturl&"?d="&URLSpace(Request("d"))
Response.Redirect strredirect
End Sub

If Request.QueryString("w") = "X" Then
If Request.Form("txtName") = "" or Request.Form("txtPassword") = "" Then
If Request.Form("txtName") = "" _
AND Request.Form("txtPassword") = "" Then
b = "both"
Else
If Request.Form("txtName") = "" Then
b = "name"
Else
If Request.Form("txtPassword") = "" Then
b = "password"
End If
End If
End If
Response.Redirect "login.asp?field="&b&"&n="&Request.Form("redirecturl")&""
Else
Verify
End If
End If

If Request.QueryString("l") = "O" Then
LogOff
End If
%>