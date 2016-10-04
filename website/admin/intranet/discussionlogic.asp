<!--#INCLUDE FILE="conn.asp"-->
<%Function DateFormat(Data)
Dim WD, OD
	if not(isdate(data)) then WD = Date() else WD = cdate(data)
	OD = right("00" & cstr(month(WD)),2) & "/" & cstr(day(WD)) & "/" & cstr(year(WD))
	DateFormat = "#" & OD & "#"
End Function



IF REQUEST.QUERYSTRING ("task") = "medit" then

 		
				

			jobsarchiveSQL = "UPDATE WEBCAT SET  MODE_X=-1 WHERE ID =" + REQUEST.QUERYSTRING("ID") + ""
			Con.execute (jobsarchiveSQL)

			jobsarchiveSQL2 = "UPDATE WEBCAT SET  MODE_X=0 WHERE ID <>" + REQUEST.QUERYSTRING("ID") + ""
			Con.execute (jobsarchiveSQL2)

				Response.Redirect("../index.asp?module=addcat")	

ELSEIF REQUEST.QUERYSTRING ("task") = "archive" then

       
				

			jobsarchiveSQL = "UPDATE WEBCAT SET  mode_x=0 WHERE ID =" + REQUEST.QUERYSTRING("ID") + ""
			Con.execute (jobsarchiveSQL)
	
	
				Response.Redirect("../index.asp?module=addcat")
				

ELSEIF REQUEST.QUERYSTRING("task") = "editcat" then
	 
	 varTitle = Request.Form("strTitle")
	 varTitle = Replace(varTitle,"'","''")
	 varMessage = Request.form("page")
	 varMessage = Replace(varMessage,"'","''")
	 varId = Request.Form("strId")
		catUpdateSQL = "Update WEBCAT Set title='" & varTitle & "', MESSAGE='" & varMessage & "' Where id=" & varId & ""
		Con.Execute (catUpdateSQL)
		
		Response.Redirect("../index.asp?module=addcat")
	
	

ELSEIF REQUEST.QUERYSTRING("task")="addcat" THEN

varTitle = Request.Form("strTitle")
varTitle = Replace(varTitle,"'","''") 
varMessage = Request.form("strPage")
varMessage = Replace(varMessage,"'","''")
NDATE=date()


myAddCatSQL = "Insert Into WEBCAT (Title, MESSAGE, MODE_X, NDATE) VALUES('" & varTitle & "','" & varMessage & "',-1," & DateFormat(NDATE) & ")"
con.execute (myAddCatSQL)


myModeSQL = "SELECT ID FROM WEBCAT WHERE TITLE='" & varTitle & "' AND MESSAGE='" & varMessage & "'"
SET RS = CON.EXECUTE (myModeSQL)
varID = RS("ID")

			jobsarchiveSQL2 = "UPDATE WEBCAT SET MODE_X=0 WHERE ID <>" & varID & ""
			Con.execute (jobsarchiveSQL2)
Response.Redirect("../index.asp?module=addcat")


ELSEIF REQUEST.QUERYSTRING ("task") = "add" then

              
varOnline = 0
title = Request.form("strTitle")
title = Replace(title,"'","''")
page = Request.form("strPage")
page = Replace(page,"'","''")
varName = Request.form("strName")
varName = Replace(varName,"'","''")
varEmail = Request.form("strEmail")
varEmail = Replace(varEmail,"'","''")
varCat = Request.Form("cat")

				


jobsarchiveSQL = "INSERT INTO WEBPAGES (wptitle, message, EMAIL, NAME, ONLINE, CATID) VALUES ('" & title & "','" & page & "','" & varEmail & "','" & varName & "'," & varOnline & "," & varCat & ")"
Con.execute (jobsarchiveSQL)
Response.Redirect ("discussions.asp?id="&"" & varcat & ""&"&task=addedthread")


ELSEIF REQUEST.QUERYSTRING ("task") = "edit" then

xid = Request.form("id")              
varStatus = Request.form("status")
title = Request.form("strTitle")
title = Replace(title,"'","''")
page = Request.form("strPage")
page = Replace(page,"'","''")
varName = Request.form("strName")
varName = Replace(varName,"'","''")
varEmail = Request.form("strEmail")
varEmail = Replace(varEmail,"'","''")
varCat = Request.Form("cat")
				


jobseditSQL = "Update WEBPAGES Set wptitle='" & title & "', message='" & page & "',ONLINE=" & varStatus & ",EMAIL='" & varEmail & "',NAME='" & varName & "',CATID=" & varCat & " WHERE id=" & xid & ""
Con.execute (jobseditSQL)
Response.Redirect ("../index.asp?module=webcat&catid="&""  & varCat & "")	






END IF



CON.CLOSE
%>