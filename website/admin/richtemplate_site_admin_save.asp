<!--#INCLUDE FILE="sessioncheck.asp"-->

<!--#include file="db_connection.asp"-->

<%PNAME = "Administer Modules"%>
<%
Sub JavaRedirect1
    url="mainpage.asp?mode=admin"
    %>
    <SCRIPT language="JavaScript">
    <!--
    parent.location.href = 
    '<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>




<%
Set Con = Server.CreateObject("Adodb.Connection")
Con.Open ConnectionString

' IF ALL MODULES CHECK BOX IS NOT CHECKED THEN DO NOT DISPLAY ANY MODULES

SQL2 = "SELECT * FROM MODULES order by ID"
Set RS = con.Execute(SQL2)
do while not RS.EOF


if Request.Form (""&RS("id")&"") = "ON" THEN
	MYSQL="Update modules set online=1 where ID="&RS("id")&""
	'RESPONSE.WRITE MYSQL
	con.execute (MYSQL)
else
	MYSQL="Update modules set online=0 where ID="&RS("id")&" "
	'RESPONSE.WRITE MYSQL
	con.execute (MYSQL)
end if
rs.movenext

	
LOOP






SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

SITE_DEPTH = REQUEST.FORM("SITE_DEPTH")
INDEX_PAGE = REQUEST.FORM("INDEX_PAGE")




ADMIN_MODULES 	= Request.Form("ADMIN_MODULES")
IF ADMIN_MODULES = "" THEN
ADMIN_MODULES = 0
ELSE 
ADMIN_MODULES = 1
END IF

ADMIN_SECTIONS 	= Request.Form("ADMIN_SECTIONS")
IF ADMIN_SECTIONS = "" THEN
ADMIN_SECTIONS = 0
ELSE 
ADMIN_SECTIONS = 1
END IF

ADMIN_EMAIL		= Request.Form("ADMIN_EMAIL")
IF ADMIN_EMAIL = "" THEN
ADMIN_EMAIL = 0
ELSE 
ADMIN_EMAIL = 1
END IF

ADMIN_USERS		= Request.Form("ADMIN_USERS")
IF ADMIN_USERS = "" THEN
ADMIN_USERS = 0
ELSE 
ADMIN_USERS = 1
END IF


ADMIN_MICROSITES	= Request.Form("ADMIN_MICROSITES")
IF ADMIN_MICROSITES = "" THEN
ADMIN_MICROSITES = 0
ELSE 
ADMIN_MICROSITES = 1
END IF


ADMIN_PAGES		= Request.Form("ADMIN_PAGES")
IF ADMIN_PAGES = "" THEN
ADMIN_PAGES = 0
ELSE 
ADMIN_PAGES = 1
END IF

EMPTYSQL = "UPDATE PACKAGE_TYPE SET PACKAGE_SELECTED = 0"
	con.Execute (EMPTYSQL) 

UpdateSQL = "UPDATE PACKAGE_TYPE SET PACKAGE_SELECTED = 1,  ADMIN_SECTIONS = "&ADMIN_SECTIONS&", ADMIN_EMAIL = "&ADMIN_EMAIL&", ADMIN_USERS = "&ADMIN_USERS&", ADMIN_PAGES = "&ADMIN_PAGES&", ADMIN_MODULES ="&ADMIN_MODULES&", ADMIN_MICROSITES = "&ADMIN_MICROSITES&" where PackageID = "&Request.Form("packagetype")&""
	con.Execute (UpdateSQL ) 
	
'*********************************
'INSERT PAGE DEPTH AND INDEX INFO	
'*********************************

MYSQL10 = "SELECT * FROM CONFIG"
SET RS3 = CON.EXECUTE(MYSQL10)
	IF RS3.EOF THEN
	
		INSERTSQL2 = "INSERT INTO CONFIG (SITE_DEPTH, INDEX_PAGE) VALUES ('"&SITE_DEPTH&"', "&INDEX_PAGE&")"
		CON.EXECUTE(INSERTSQL2)

	ELSE
	
		UPDATESQL2 = "UPDATE CONFIG SET SITE_DEPTH = '"&SITE_DEPTH&"'"
		con.Execute (UPDATESQL2) 

	END IF
	
	
Session("PackageID") = Request.Form("packagetype")

	
Call JavaRedirect1
	
	%>