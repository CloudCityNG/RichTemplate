<!--#include file ="includes/dateformat.inc"-->
<%

Dim id, page
Dim Con 
Dim objRecordSet
Dim lRecordsFound

	id = request("id")
	



	
	

%>
	<!--#include file="conn.asp"-->
<%

	mySQL ="SELECT * FROM compmemo WHERE id = " & id
	SET objrecordset = con.execute (mySQL)
	If not objrecordset.eof Then 
			page = objrecordset("message")
			page = "<HTML><HEAD><TITLE>New</TITLE></HEAD>" & page & "</HTML>"
			objrecordset.close
	else
			page = "<HTML><HEAD><TITLE>New</TITLE></HEAD></HTML>"
	end if

	Con.close

	set objrecordset = nothing
	set Con = nothing

%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<%=page%>

