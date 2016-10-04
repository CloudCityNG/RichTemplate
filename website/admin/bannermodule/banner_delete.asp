<!--#INCLUDE VIRTUAL="/admin/sessioncheck.asp"-->
<!--#INCLUDE VIRTUAL="/admin/db_connection.asp"-->

<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>New Page 1</title>
</head>

<body>
<% 

Whichfolder=server.mappath("\data\images\banners") &"\"  
fileName = Request.Querystring("name")
deletewhichFile = whichfolder&filename
response.write deletewhichfile

set fs = CreateObject("Scripting.FileSystemObject")
fs.DeleteFile(""&deletewhichfile&"")
   
   
SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

'****************************************
'reset rank for all images

    SQL1 = "UPDATE BANNER_MODULE SET RANK = RANK - 1 WHERE PAGEID = "&REQUEST.QUERYSTRING("PAGEID")&" AND rank >" & Request.Querystring("rank")
    CON.EXECUTE (SQL1)
    
'****************************************


	editSQL = "DELETE FROM BANNER_MODULE WHERE ID=" & Request.Querystring("bannerID")
	
	Con.execute (editSQL) 
 
Response.Redirect "banner_display.asp?pageid="&Request.Querystring("pageid")&""
   
 %>

</body>

</html>
