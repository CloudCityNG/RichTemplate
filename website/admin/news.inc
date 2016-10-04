<!--#INCLUDE FILE="sessioncheck.asp"-->

<%
rem Global Script Timeout

	server.scripttimeout = 60

rem Database functions
rem only this fuctions should be changed you you change database
rem current database type: Microsoft Access

Function ConnectString()
dim path, dir, file
	file = "news.mdb"
	path = server.mappath(file)
	dir = left(path,len(path)-len(file))
	ConnectString = "DBQ=" & path & ";DefaultDir=" & dir & ";Driver={Microsoft Access Driver (*.mdb)};DriverId=25;FIL=MS Access;ImplicitCommitSync=Yes;MaxBufferSize=512;MaxScanRows=8;PageTimeout=5;SafeTransactions=0;Threads=3;UID=admin;UserCommitSync=Yes;"
End Function

Function DBUsername()
	DBUsername = "Admin"
End Function

Function DBPassword()
	DBPassword = ""
End Function

rem File storage Database functions
rem only this fuctions should be changed you you change database
rem current database type: Microsoft Access

Function FileConnectString()
dim path, dir, filename
	filename = "Files.mdb"
	path = server.mappath(filename)
	dir = right(path,len(path)-len(filename))
	FileConnectString = "DBQ=" & path & ";DefaultDir=" & dir & ";Driver={Microsoft Access Driver (*.mdb)};DriverId=25;FIL=MS Access;ImplicitCommitSync=Yes;MaxBufferSize=512;MaxScanRows=8;PageTimeout=5;SafeTransactions=0;Threads=3;UID=admin;UserCommitSync=Yes;"
End Function

Function FileOLEDBConnectString()
dim path, dir, file

	file = "Files.mdb"
	path = server.mappath(file)
	FileOLEDBConnectString = path
	
End Function

rem This function defines the connect string for the Log File database connection
rem This is a ADO connect string for an Access Database

Rem This Function formats dates to the correct format for ODBC, regardless of control pannel settings
rem This function is setted for Microsoft Access ODBC drivers

Function DateFormat(Data)
Dim WD, OD
	if not(isdate(data)) then WD = Date() else WD = cdate(data)
	OD = right("00" & cstr(month(WD)),2) & "/" & cstr(day(WD)) & "/" & cstr(year(WD))
	DateFormat = "#" & OD & "#"
End Function

%>