


<!--#include file="db_connection.asp"-->

<%

'DEMO INFORMATION
currentDate = FormatDateTime(Date())

set con = server.CreateObject("ADODB.Connection")
con.open ConnectionString

strUsername = Replace(Request.Form("txtUserName"), "'", "''")
strPassword = Replace(Request.Form("txtPassword"), "'", "''")  


strsql = "Select * FROM  USERS where USERNAME = '" & strUserName & "' and PASSWORD = '" & strPassword & "'"
set rs = con.Execute (strsql)


If not rs.eof then

RESPONSE.WRITE "Expiration Date = "&RS("Expiration_Date")&"<br>"
datedifference = datediff("d", currentDate, RS("Expiration_Date"))


response.write "<br>Date Difference = "&datedifference&"<br>"
	If RS("ACTIVE")=0 then
	   Response.Redirect "/richadmin/?type=error1"
	
	Elseif datedifference =< 0 then
		response.write "Status = Account In-Active - Expired"
		Response.Redirect "/richadmin/?type=error2"
	
	Elseif RS("counter") => RS("Login_Limit") then
		Response.Redirect "/richadmin/?type=error3"
		response.write "Counter = "&RS("counter")&"<br>"
		response.write "Limit = "&RS("Login_Limit")&"<br>"
		response.write "Status = Account In-Active - Too Many Logins"

	Else
		Response.Cookies("SSDEVUSERNAME") = rs("USERNAME")
		Session("USERID") 		= RS("ID")
		Session("USERNAME") 	= RS("USERNAME")
		Session("ACCESS_LEVEL")	= RS("ACCESS_LEVEL")
		Session("FIRSTNAME") 	= RS("FIRST_NAME")
		Session("ALLOW_PUBLISH")= RS("ALLOW_PUBLISH")
		Session("ALLOW_PAGEDELETE") = RS("ALLOW_PAGEDELETE")
		Session("ALLOW_SECTIONDELETE")	= RS("ALLOW_SECTIONDELETE")
		Session("ALLOW_RENAMESECTION") = RS("ALLOW_RENAMESECTION")
		SESSION("ALLOW_MODULES") = RS("ALLOW_MODULES")
		response.write session("allow_modules")
		SESSION("ALLOW_WEBCONTENT") = RS("ALLOW_WEBCONTENT")
		

		
		RESPONSE.WRITE "ALLOW_PAGEDELETE = "&SESSION("ALLOW_PAGEDELETE")&"<BR>"
		
		RESPONSE.WRITE "ALLOW_SECTIONDELETE = "&SESSION("ALLOW_SECTIONDELETE")&"<BR>"

		RESPONSE.WRITE "ALLOW_PUBLISH = "&SESSION("ALLOW_PUBLISH")&"<BR>"
		
		RESPONSE.WRITE "ALLOW_RENAMESECTION = "&SESSION("ALLOW_RENAMESECTION")&"<BR>"


		
		
		newCount = RS("counter") + 1
		response.write "newcount = "&newcount&"<br>"
		

		'----------------------------------------
		
		IF SESSION("PLATFORM") = "1" THEN
		
			currentdate =now()
			
			UpdateCounterSQL = "UPDATE USERS SET  Counter ="&newCount&",  Last_Access =now() WHERE ID = "&RS("ID")&""
		
		ELSE	
			
			UpdateCounterSQL = "UPDATE USERS SET Counter ="&newCount&", Last_Access =GETDATE()  WHERE ID = "&RS("ID")&""
		
		END IF
		
		'response.write updatecountersql
		CON.EXECUTE (UpdateCounterSQL)	
		
		If Request.Form("package")<>"" then
				
				If Request.Form("package") = 1 then
					
					'THIS IS THE LITE PACKAGE
					SetSelectionSQL = "UPDATE PACKAGE_TYPE SET PACKAGE_SELECTED = 0 WHERE PACKAGEID <> 1"
					CON.EXECUTE (SetSelectionSQL)	
					
					UpdateDemoPackageSQL = "UPDATE PACKAGE_TYPE SET PACKAGE_SELECTED = 1 , ADMIN_SECTIONS = 0, ADMIN_EMAIL = 1, ADMIN_USERS = 1, ADMIN_PAGES = 0, ADMIN_MODULES = 0 WHERE PACKAGEID = 1"
					CON.EXECUTE (UpdateDemoPackageSQL )	
					
					UpdateDemoPackageSQL = "UPDATE MODULES SET ONLINE = 0"
					CON.EXECUTE (UpdateDemoPackageSQL )	
										
				Elseif Request.Form("package") = 2 then
					
					'THIS IS THE GOLD PACKAGE
					SetSelectionSQL = "UPDATE PACKAGE_TYPE SET PACKAGE_SELECTED = 0 WHERE PACKAGEID <> 2"
					CON.EXECUTE (SetSelectionSQL)	
			
					UpdateDemoPackageSQL = "UPDATE PACKAGE_TYPE SET PACKAGE_SELECTED = 1 , ADMIN_SECTIONS = 1, ADMIN_EMAIL = 1, ADMIN_USERS = 1, ADMIN_PAGES = 1, ADMIN_MODULES = 0 WHERE PACKAGEID = 2"
					CON.EXECUTE (UpdateDemoPackageSQL )
					
					UpdateDemoPackageSQL = "UPDATE MODULES SET ONLINE = 0 WHERE MODULENAME <> 'Press Releases' AND MODULENAME <> 'Calendar Events'"
					CON.EXECUTE (UpdateDemoPackageSQL )	
					
					UpdateDemoPackageSQL2 = "UPDATE MODULES SET ONLINE = 1 WHERE MODULENAME = 'Press Releases' AND MODULENAME = 'Calendar Events'"
					CON.EXECUTE (UpdateDemoPackageSQL2)	
					
	
						
				Elseif Request.Form("package") = 3 then
				
					'THIS IS THE PLATINUM PACKAGE
					SetSelectionSQL = "UPDATE PACKAGE_TYPE SET PACKAGE_SELECTED = 0 WHERE PACKAGEID <> 3"
					CON.EXECUTE (SetSelectionSQL)	
				
					UpdateDemoPackageSQL = "UPDATE PACKAGE_TYPE SET PACKAGE_SELECTED = 1 , ADMIN_SECTIONS = 1, ADMIN_EMAIL = 1, ADMIN_USERS = 1, ADMIN_PAGES = 1, ADMIN_MODULES = 1 WHERE PACKAGEID = 3"
					CON.EXECUTE (UpdateDemoPackageSQL )	
					
					UpdateDemoPackageSQL = "UPDATE MODULES SET ONLINE = 1"
					CON.EXECUTE (UpdateDemoPackageSQL )	
					
					
					
				End if

		End if
		
		PullPkgSQL = "Select PackageID from PACKAGE_TYPE where PACKAGE_SELECTED = 1"
		set rs = con.Execute (PullPkgSQL)
			
		Session("PackageID") = RS("PackageID")
		
		
		response.write "Status = Account Active"
		
		If Request.Form("platform")<>"" then
			Session("platform") = Request.Form("platform")
			response.write ""&session("platform")&""
		End if
		
		URL = Request.ServerVariables("HTTP_HOST")
		URL = replace(URL, "www.", "")
		Session("Authenticated") = session.sessionid


		'SET GLOBALROOTPATH FOR CONFIG.JS 
		
		   Const ForReading   = 1
		   Const ForWriting   = 2
		   Const ForAppending = 8
		
			dim path, fs, file
		
			path = Server.MapPath("\") & "\editor\config\Urlconfig.js"
			
		response.write path
		   set fs = CreateObject("Scripting.FileSystemObject")
		   set file = fs.OpenTextFile(path, ForWriting)
		   file.WriteLine("var globalRootPath = '"&Replace(Server.MapPath("\"), "\", "/")&"/';")
		   file.Close() 
		
  		Response.Redirect "http://www." & URL & "/admin/mainpage.asp"
   	End if
else

  Response.Redirect "/richadmin/?type=error"
end if



rs.close
set rs = nothing


con.close
set con = nothing



%><p>&nbsp;</p>





