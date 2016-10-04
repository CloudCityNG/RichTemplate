<!--#INCLUDE FILE="../db_connection.asp"-->

<%	

			SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
			CON.OPEN ConnectionString
	
			myRSSQL = "Select * from BANNER_MODULE WHERE  PAGEID=" & request.querystring("PAGEID") & ""
			SET RS = con.execute(myRSSQL)
	

	%>
