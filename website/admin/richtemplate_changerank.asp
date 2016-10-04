


<!--#include file="db_connection.asp"-->


<html>


<head>





<meta http-equiv="Content-Language" content="en-us">
<%
Sub JavaRedirect 
	nav = "/admin/richtemplate_list_sections.aspx"
    main="richtemplate_list_pages.aspx?SectionID="&Request.Querystring("SectionID")&""
    %>
    <SCRIPT language="JavaScript">
    <!--
    
    top.basefrm.location 	= '<%=main%>';
    top.treeframe.location= '<%=nav%>';
    //window.close(); 
   // parent.location.href = 
    //'<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>
</head>

<body>
<%
if request.querystring("posit")<>"skip" then

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

	IF REQUEST.QUERYSTRING("posit")= "moveup"  then

		strId = Request.Querystring("pageid")
		strRank = Request.Querystring("rank")
		strNewRank = strRank - 1
	
			If strRank <= 1 then
			
			else
					
				myUpdateSQL = "Update WEBINFO SET rank = rank + 1 WHERE DEFAULTPAGE = 1 and rank =" & strNewRank 
				con.execute(myUpdateSQL)
			
				myRankSQL = "Update WEBINFO SET rank = " & strNewRank & " WHERE id = " & strId
			
				con.execute(myRankSQL)
			end if
	end if
			
			
	IF REQUEST.QUERYSTRING("posit") = "movedown" then
		strId = Request.Querystring("pageid")
		strRank = Request.Querystring("rank")
		strNewRank = strRank + 1
	
			myRSSQL = "Select rank from WEBINFO WHERE  DEFAULTPAGE = 1 and rank > " & strRank
			SET RS = con.execute(myRSSQL)
				IF RS.EOF then
			
			
				ELSE
			
					myUpdateSQL2 = "Update WEBINFO SET rank = rank - 1 WHERE DEFAULTPAGE = 1 and rank =" & strNewRank 
					con.execute(myUpdateSQL2)
			
					myRankSQL2 = "Update WEBINFO SET rank = " & strNewRank & " WHERE id = " & strId
			
					con.execute(myRankSQL2)
				end if

	end if
	end if
	call JavaRedirect

%>
<p>Changing Rank Please wait.</p>
</body>

</html>
