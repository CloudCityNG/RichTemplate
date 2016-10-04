<%

'===== Revision Number Start =====

	Session("revision") = "Rev 5.8"

'<=================================================

'< Get IP Address for IP Login ====================
dim ip
	IP = Request.ServerVariables ("REMOTE_ADDR")

'<=================================================

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



'< Define Root Paths Start ========================
dim strRootPath
strRootPath = Server.MapPath(Request.ServerVariables("Script_Name"))
strRootPath = Left(strRootPath, Len(strRootPath) - Len(Request.ServerVariables("Script_Name")))

' SET PHYSICAL ROOT SESSION FOR USE IN SITE
Session("strRootPath") = strRootPath
dim strDynPath
strDynPath = Request.ServerVariables("HTTP_HOST")

' SET DYNAMIC ROOT SESSION FOR USE IN SITE
Session("strDynPath") = strDynPath 
dim staticDBPath
staticDBPath = Replace(Server.MapPath("\"), "website", "data") & "\richtemplate-2.0.mdb"
'<=================================================

'< Set Database Table for Demo ====================

	if Session("ClientID")="ID_01_" then
	Session("ClientID")=""
	end if
	
'<=================================================




IF PLATFORM = 1 THEN 
	
	'********************************************
	'---------- MS ACCESS CONNECTION ------------
	'********************************************
	dim mdbFile, connectionstring
	mdbFile = Replace(Server.MapPath("\"), "website", "data") & "\richtemplate-2.0.mdb" 
	ConnectionString = "Driver={Microsoft Access Driver (*.mdb)};DBQ=" & mdbfile & ";"
	
	'********************************************


ELSEIF PLATFORM = 2 THEN

	'********************************************
	'--------- MS SQL SERVER CONNECTION ---------
	'********************************************
	
	Connectionstring = 	"Provider=SQLOLEDB;" & _
			           	"Password=richTemplate;" & _
			           	"Persist Security Info=True;" & _
			           	"User ID=ssAdmin;" & _
			           	"Initial Catalog=RichTemplate;" & _
			           	"Data Source=127.0.0.1\SQLEXPRESS,10103;" & _
	                  	"Network Library=DBMSSOCN;"
						'response.write connectionstring 
	
	'********************************************


ELSE 

	RESPONSE.WRITE "YOU MUST SPECIFY A DATABASE PLATFORM IN THE db_connection.asp FILE"

END IF

%>


