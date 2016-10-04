<%

	'Revision Number Start 
	
		Session("revision") = "Rev 6.0"


	'Get IP Address for IP Login 
	dim ip
	IP = Request.ServerVariables ("REMOTE_ADDR")


'< Select Database Platform Start =================

' 1 = MICROSOFT ACCESS
' 2 = MICROSOFT SQL SERVER
	dim platform
	PLATFORM = 2
	
	Session("platform") = PLATFORM	

'<=================================================



'< Make Site Live or Demo Start ===================

	'MAKE THIS FALSE IF THE SITE IS GOING LIVE
	'REMOVES BUG LINK
	'REMOVES DATABASE CHOICE ON BACK END
	'REMOVES PACKAGE TYPE SELECTION

	Session("demo")= false
	
'<=================================================


' SET PHYSICAL ROOT SESSION FOR USE IN SITE
Session("strRootPath") = strRootPath
dim strDynPath
strDynPath = Request.ServerVariables("HTTP_HOST")

' SET DYNAMIC ROOT SESSION FOR USE IN SITE
Session("strDynPath") = strDynPath 
dim staticDBPath
staticDBPath = Replace(Server.MapPath("\"), "website", "data") & "\richtemplate-2.0.mdb"
'<=================================================

'< Define Root Paths Start ========================
dim strRootPath
strRootPath = Server.MapPath(Request.ServerVariables("Script_Name"))
strRootPath = Left(strRootPath, Len(strRootPath) - Len(Request.ServerVariables("Script_Name")))



	'********************************************
	'--------- MS SQL SERVER CONNECTION ---------
	'********************************************
	Dim ConnectionString
	
	ConnectionString = 	"Provider=SQLOLEDB;" & _
			           	"Password=sm@rts!teAdm!n101;" & _
			           	"Persist Security Info=True;" & _
			           	"User ID=richtemplatetemplateAdmin;" & _
			           	"Initial Catalog=ss_richtemplatetemplate_com;" & _
	          		 	"Data Source=RICH-HP\SQLEXPRESS;"
	
	


	'********************************************



%>


