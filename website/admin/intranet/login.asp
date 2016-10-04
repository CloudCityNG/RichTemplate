<!--#include file="dsn.asp"-->
<%
	Dim objCon
	Set objCon = Server.CreateObject("ADODB.Connection")
	objCon.open Connectionstring

	If Session("blnValidUser") = True and Session("Admin_ID") = "" Then
		Dim rsPersonIDCheck
		Set rsPersonIDCheck = Server.CreateObject("ADODB.Recordset")
		Dim strSQL
		strSQL = "SELECT * FROM styles WHERE Admin_ID = '" & Session("Admin_ID") & "';"
		rsPersonIDCheck.Open strSQL, objCon
		If rsPersonIDCheck.EOF Then
			Session("blnValidUser") = False
		Else
			Session("Admin_ID") = rsPersonIDCheck("Admin_ID")
		End If
		rsPersonIDCheck.Close
		Set rsPersonIDCheck = Nothing
	End If


	Dim strID, strPassword
	strID = Request("Admin_ID")
	strPassword = Request("Password")

	Dim rsUsers
	set rsUsers = Server.CreateObject("ADODB.Recordset")
	strSQL = "SELECT * FROM styles WHERE Admin_ID = '" & strID & "';"
	rsUsers.Open strSQL, objCon

	If rsUsers.EOF Then
		Session("Admin_ID") = Request("Admin_ID")
		Response.Redirect "default.asp?SecondTry=True"
	Else
		While Not rsUsers.EOF
			If UCase(rsUsers("Admin_Pass")) = UCase(strPassword) Then
				Session("Admin_ID") = rsUsers("Admin_ID")
				Session("isLoggedIn") = True
				Session("blnValidUser") = True
				Response.Redirect "index.asp"
			Else
				rsUsers.MoveNext
			End If
		Wend
		Session("Admin_ID") = Request("Admin_ID")
		Response.Redirect "default.asp?SecondTry=True&WrongPW=True"
	End If
%>
<!--#include file="dsn2.asp"-->