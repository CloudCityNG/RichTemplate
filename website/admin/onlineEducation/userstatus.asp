
<!--INCLUDE VIRTUAL="/admin/sessioncheck.asp"-->
<!--#INCLUDE VIRTUAL="/admin/db_connection.asp"-->
<!--#include virtual="/admin/CDOSYS_Config.inc"-->
<%
'redirect page if there is no id number in the querystring
if Request.Querystring("id")="" then
	Response.Redirect("userslist.asp")
else
    ID = Request.QueryString("id")
    ID = Replace(id, "'", "''")
end if

%>



<%
SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString


If Request.QueryString("status")="makeactive" Then


    getUserEmail = "Select email from OnlineEducation where ID = "&ID
    Set RS = Con.Execute(getUserEmail)
    If Not RS.EOF then
        
        sendToEmail = RS("email")
    
    End if


	editSQL = "Update OnlineEducation set Verified=1 Where ID=" & ID
	
	'Shoot out email

            strBody = "<html><body>"
            strBody = strBody & "<div style='margin: 0 auto; text-align:center;'><img src='http://" & Request.ServerVariables("http_host") & "/images/casw_web2C.gif' /></div><br/><br/>"
            strBody = strBody & "<b>Congratulations!</b>  You are now a member of the Choral Arts Online Learning Community.  Thank you so much for your interest in learning more about how music can impact learning in the schools.<br/><br/>"
            strBody = strBody & "The Online Learning Community resources enable you to access a wealth of information about music and music-integration.  New resources will become available throughout the school year, so check back regularly for new curricular tools.<br/><br/>"
            strBody = strBody & "The Online Learning Community is a <b>community</b> resource.  Please contribute to the community by sharing music-integrated lesson plans that have been successful in your classroom.<br/><br/>"
            strBody = strBody & "The Online Learning Community is a public forum.  Choral Arts reserves the right to revoke the privileges of any member using the site in any manner it deems inappropriate.<br/><br/>"
            strBody = strBody & "</body></html>"
	    

		'** NEW EMAIL SENDING CODE START
   	 	Set iMsg.Configuration = iConf
	    iMsg.From = "info@ChoralArts.org"
	    iMsg.To = sendToEmail
	    iMsg.Subject = "Choral Arts Online Learning Community Approval"
	    iMsg.HTMLBody =strbody
	    iMsg.Send

   	 	'Release server resrces.
	    Set iMsg	= Nothing
	    Set iConf	= Nothing
	    Set Flds 	= Nothing
	    
	  
'response.write strbody
'REDIRECT

	
	
	
ElseIF Request.QueryString("status")="makeinactive" Then
	


    getUserEmail = "Select email from OnlineEducation where ID = "&ID
    Set RS = Con.Execute(getUserEmail)
    If Not RS.EOF then
        
        sendToEmail = RS("email")
    
    End if


	editSQL = "Update OnlineEducation set Verified=0 Where ID=" & ID
	
	'Shoot out email

            strBody = "<html><body>"
            strBody = strBody & "<div style='margin: 0 auto; text-align:center;'><img src='http://" & Request.ServerVariables("http_host") & "/images/casw_web2C.gif' /></div><br/><br/>"
            strBody = strBody & "We apologize, but The Choral Arts Society of Washington is unable to accept your <b>Online Learning Community</b> application.  You may reapply to the Online Learning Community after 30 days.<br/><br/>"
            strBody = strBody & "The Online Learning Community is a public forum.  Choral Arts reserves the right to revoke the privileges of any member using the site in any manner it deems inappropriate.<br/><br/>"
            strBody = strBody & "</body></html>"

	    

		'** NEW EMAIL SENDING CODE START
   	 	Set iMsg.Configuration = iConf
	    iMsg.From = "info@ChoralArts.org"
	    iMsg.To = sendToEmail
	    iMsg.Subject = "Choral Arts Online Learning Community Account Notification"
	    iMsg.HTMLBody =strbody
	    iMsg.Send

   	 	'Release server resrces.
	    Set iMsg	= Nothing
	    Set iConf	= Nothing
	    Set Flds 	= Nothing
	    
	  
End If

	
Con.execute (editSQL)  
Con.close
Response.Redirect("userslist.asp")

%>
<html>
<head>
</head>
<body>
</body>
</html>