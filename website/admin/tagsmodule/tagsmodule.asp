<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#INCLUDE virtual="/admin/db_connection.asp"-->
<%' get action for page name'
myTask="Manage"


PNAME = ""&myTask&" Search Tags"
PHELP = "../helpFiles/pageListing.asp#pressreleases"%>

<!-- DELETE TAG ------------------------------------------------>

<%IF REQUEST.QUERYSTRING("task") = "delete" then

	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString
	
		'DELETE RECORD FROM DATABASE
		prdeleteSQL = "Delete FROM TAGS WHERE tagID=" + REQUEST.QUERYSTRING ("id") + ""
		Con.Execute (prdeleteSQL)

		Response.redirect "tagsmodule.asp"

	CON.CLOSE
	
%>

<!-- MODE EDIT TAG --------------------------------------------->

<%ELSEIF REQUEST.QUERYSTRING ("task") = "medit" then

	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString
              
			mode_x = "LIVE"


			prarchiveSQL = "UPDATE TAGS SET  mode_x='" + mode_x + "' WHERE tagID =" + REQUEST.QUERYSTRING("id") + ""
			Con.execute (prarchiveSQL)
			Set prarchiveSQL = nothing                 
			con.close

			Response.redirect "tagsmodule.asp"
%>

<!-- ARCHIVE PR ------------------------------------------------>

<%ELSEIF REQUEST.QUERYSTRING ("task") = "archive" then


'OPEN DATA

	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString
              
			mode_x = "ARCHIVE"

			prarchiveSQL = "UPDATE TAGS SET  mode_x='" + mode_x + "' WHERE tagID =" + REQUEST.QUERYSTRING("id") + ""
			Con.execute (prarchiveSQL)
			Set prarchiveSQL = nothing
			con.close

			Response.redirect "tagsmodule.asp"
%>


<!-- NOW ADDING PRESS RELEASES---------------------------------->

 
  <%ELSEIF REQUEST.QUERYSTRING("ID")<>"" then%>


<%

ID = Request.Querystring("id")
defaultpage = Request.Querystring("defaultpage")

Dim html

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString


'***********IF THE ID IS LESS THAN 0 IT MEANS WE ARE ADDING A RECORD

			If Request.Querystring("ID")>0 then
			
						
					WEBSITESECTIONSQL = "SELECT * FROM TAGS WHERE tagID = "&REQUEST.QUERYSTRING("ID")&""
					SET RS2 = CON.EXECUTE (WEBSITESECTIONSQL)
					
					
					myTitle = RS2("tagName")
					mySumm = RS2("tagType")
					myMode = RS2("mode_x")
			
				myTask="Edit"
			
			else

					
				myTask="Add"
					
			end if

END IF			
%>



<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Untitled 1</title>
<link rel="stylesheet" href="../style_richtemplate.css" type="text/css" />
<SCRIPT language=JavaScript src="../deletefunction.js" type=text/javascript /></SCRIPT>

</head>

<body style="margin: 0">
<!--#INCLUDE virtual="/admin/headernew.inc"-->




<table border="0" width="100%" cellspacing="0" cellpadding="0" id="table5">
	<tr>
		<td width="10">&nbsp;</td>
		<td><table border="0" width="100%" cellspacing="0" cellpadding="0" id="table3">
	<tr>
		<td width="30">
		&nbsp;</td>
		<td class="Bodybold">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="2">
		<a class="bodybold" href="tagActions.asp?id=-1">
		<font color="#3054A9" class="bodyboldsection">Add New Search Tag</font></a><font color="#3054A9">&nbsp;&nbsp;&nbsp;
		<b><%=ErrorMessage%></b>
</font>
		</td>
	</tr>
</table>
</td>
	</tr>
	
	<tr><td>&nbsp;</td><td class="bodyboldsection">&nbsp;</td></tr>
	
	<tr><td>&nbsp;</td><td class="bodyboldsection">Major Subject Tags</td></tr>
	
	<tr>
		<td width="10">&nbsp;</td>
		<td><table border="0" cellpadding="2">
        <tr class="moduleHeader">
          <td width="300" style="height: 20px" class="BlueHeader">Tag Name</td>
          <td width="30" style="height: 20px" class="BlueHeader">Edit</td>
          <td width="30" style="height: 20px" class="BlueHeader">
            <p>Delete</p>
          </td>
          <td width="30" style="height: 20px" class="BlueHeader">
            <p>Archive</p>
          </td>

        </tr>

      <!--------------------------------GET PRESS RELEASE INFO------------------------------------------>

      <%
      
      
       '**********************
       'OPEN DATA
 		'********************** 
		
		SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
		CON.OPEN ConnectionString
      
      mySQL = "Select * FROM TAGS WHERE TAGTYPE = 1 ORDER BY TAGNAME"
      SET RS = CON.EXECUTE (mySQL)
     	if not RS.EOF then
		rs.MoveFirst
		mybgcolor="#eeeeee"
		do while Not RS.EOF %>

      <TR>
   
      
	<TD bgcolor="<%=mybgcolor%>" width="300" class="body" style="height: 22px"><%=RS("tagName")%></font></td>
	<TD bgcolor="<%=mybgcolor%>" align="center" width="30" class="body" style="height: 22px">
          <a href="tagActions.asp?task=edit&id=<%=RS("tagID")%>">
			edit</a>
        </td>
	<TD bgcolor="<%=mybgcolor%>" align="center" width="30" class="body" style="height: 22px">
          <a href="javascript:confirmdelete('tagsmodule.asp?task=delete&id=<%=RS("tagID")%>')">
			delete</a>
        </td>
       
           <%if RS("mode_x")= "ARCHIVE" then%>
        <TD bgcolor="<%=mybgcolor%>" align="center" width="30" class="body" style="height: 22px">
           <a href="tagsmodule.asp?task=medit&id=<%=RS("tagID")%>">
			restore</a>
           
        </td>
         <%else%>
      <TD bgcolor="<%=mybgcolor%>" align="center" width="30" class="body" style="height: 22px">
     <a href="tagsmodule.asp?task=archive&id=<%=RS("tagID")%>">
		archive</a>
                 </td> 
                 <%end if%>
                 
     
         </TR>     
       
<%

	if mybgcolor="#eeeeee" then
  		mybgcolor="#ffffff"
  	else
  		mybgcolor="#eeeeee"
  				end if
  		rs.movenext
		loop
	else
	end if
	rs.close%>
</TABLE>    
  </td>
	</tr>
		<tr><td>&nbsp;</td><td class="bodyboldsection"></td></tr>
		<tr><td>&nbsp;</td><td class="bodyboldsection">Program Name Tags</td></tr>
	<tr>
		<td width="10">&nbsp;</td>
		<td><table border="0" cellpadding="2">
        <tr class="BlueHeader">
          <td width="300" style="height: 20px">Tag Name</td>
          <td width="30" style="height: 20px">Edit</td>
          <td width="30" style="height: 20px">
            <p>Delete</p>
          </td>
          <td width="30" style="height: 20px">
            <p>Archive</p>
          </td>

        </tr>

      <!--------------------------------GET PRESS RELEASE INFO------------------------------------------>

      <%
      
      
       '**********************
       'OPEN DATA
 		'********************** 
		
		SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
		CON.OPEN ConnectionString
      
      mySQL = "Select * FROM TAGS WHERE TAGTYPE = 2 ORDER BY TAGNAME"
      SET RS = CON.EXECUTE (mySQL)
     	if not RS.EOF then
		rs.MoveFirst
		mybgcolor="#eeeeee"
		do while Not RS.EOF %>

      <TR>
   
      
	<TD bgcolor="<%=mybgcolor%>" width="300" class="body" style="height: 22px"><%=RS("tagName")%></font></td>
	<TD bgcolor="<%=mybgcolor%>" align="center" width="30" class="body" style="height: 22px">
          <a href="tagActions.asp?task=edit&id=<%=RS("tagID")%>">
			edit</a>
        </td>
	<TD bgcolor="<%=mybgcolor%>" align="center" width="30" class="body" style="height: 22px">
          <a href="javascript:confirmdelete('tagsmodule.asp?task=delete&id=<%=RS("tagID")%>')">
			delete</a>
        </td>
       
           <%if RS("mode_x")= "ARCHIVE" then%>
        <TD bgcolor="<%=mybgcolor%>" align="center" width="30" class="body" style="height: 22px">
           <a href="tagsmodule.asp?task=medit&id=<%=RS("tagID")%>">
			restore</a>
           
        </td>
         <%else%>
      <TD bgcolor="<%=mybgcolor%>" align="center" width="30" class="body" style="height: 22px">
     <a href="tagsmodule.asp?task=archive&id=<%=RS("tagID")%>">
		archive</a>
                 </td> 
                 <%end if%>
                 
     
         </TR>     
       
<%

	if mybgcolor="#eeeeee" then
  		mybgcolor="#ffffff"
  	else
  		mybgcolor="#eeeeee"
  				end if
  		rs.movenext
		loop
	else
	end if
	rs.close%>
</TABLE>    
  </td>
	</tr>

</table>




&nbsp;
       <!--------------------------------END GET PRESS RELEASE INFO------------------------------------------>
       
       
<p>&nbsp;&nbsp;&nbsp; </p>
       
       
<%
SET RS=NOTHING
CON.CLOSE
%>




</body>

</html>
