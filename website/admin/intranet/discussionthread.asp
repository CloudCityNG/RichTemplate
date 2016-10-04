<!--#INCLUDE FILE="conn.asp"---->
<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 4.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>Hillel Discussions</title>
<SCRIPT LANGUAGE="JavaScript">
<!--
function deleteaudio()
{
if (confirm("Are you sure you want to delete this item?")){
	return true;
}  else
	return false;
}
//-->
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
<!--
function deleteaudio()
{
if (confirm("Are you sure you want to delete this item?")){
	return true;
}  else
	return false;
}
//-->
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript">

<!-- Begin
function checkrequired(which) {
var pass=true;
if (document.images) {
for (i=0;i<which.length;i++) {
var tempobj=which.elements[i];
if (tempobj.name.substring(0,3)=="str") {
if (((tempobj.type=="text"||tempobj.type=="textarea"||tempobj.type=="password")&&
tempobj.value=='')||(tempobj.type.toString().charAt(0)=="s"&&
tempobj.selectedIndex==0)) {
pass=false;
break;
         }
      }
   }
}
var string1=SaveForm.strEMAIL.value
if (string1.indexOf("@")==-1)
{
alert("Please input a valid email address!")
return false
}
if (!pass) {
shortFieldName=tempobj.name.substring(3,30).toUpperCase();
alert("Please make sure the "+shortFieldName+" field was properly completed.");
return false;
}
else
return true;
}
//  End -->
</script>
</head>

<body marginheight=0 marginwidth=0 topmargin="0" leftmargin="0" bgcolor="#333333" background="harvard/images/bgstrip2.gif" link="#ffff00" vlink="#ffff00">
 <!---------------------------------------ADD AND EDIT SERVICES------------------------------------->
 <!---------------------------------------ADD AND EDIT SERVICES------------------------------------->
 <!---------------------------------------ADD AND EDIT SERVICES------------------------------------->
<table>
<tr><td><font color="#808080" face="Verdana" size="3"><b>Hillel Discussions<b></font></td></tr>
<tr><td><center><font color="#FFFFFF" face="Verdana" size="1"><b>| 

<%myDisSQL = "Select id, title from webcat where mode_x=-1"
Set RS3 = con.execute (myDisSQL)
IF NOT RS3.EOF THEN
%>
<a href="discussions.asp?id=<%=RS3("id")%>">View
      Current Discussion Topic</a> 
      <%END IF%>
      | <a href="discussions_archived.asp">View
      Archived Discussion Topics</a> | <a href="javascript:close(window)">Close Window</a> |</b></font></center></td></tr></table>
      
      <br><br>
<form method="post" action="discussionlogic.asp?module=webcat&task=add" name="SaveForm" onSubmit="return checkrequired(this);">
  <input type="hidden" name="id" value="<%=id%>">


  <p><br><b><font face="verdana, arial, helvetica" color="#E8E8E8" size="3">Add
  Discussion Thread</font></b></p>
<div align="center">
  <center>
<table border="0" cellpadding="0" cellspacing="0" width="80%">
    <tr>
      
      <td bgcolor="#800000" colspan="2" width="635">

<p align="center"><font face="Verdana, Ariel, Helvetica" color="#ffffff" size="2"><b>ADD
DISCUSSION THREAD</b></font></p>

      </td>
      
    </tr>
   
      
      <input type="hidden" name="ndate" size="10" value="<%=NDate%>">
        
    <tr>
      <td bgcolor="#E8E8E8" align="right" width="77">
        <b><font face="Arial" size="1">Name:</font></b></td>
      <td bgcolor="#E8E8E8" width="556"><input type="text" name="strNAME" size="37"></td>
    </tr>
    <tr>
      <td bgcolor="#F4F4F4" align="right" width="77">
        <b><font face="Arial" size="1">Email:</font></b></td>
      <td bgcolor="#F4F4F4" width="556"><input type="text" name="strEMAIL" size="37"></td>
    </tr>
       
    <tr>
      <td align="right" bgcolor="#F4F4F4" width="77"><strong><font face="Arial" size="1">Title:</font></strong></td>
      <td bgcolor="#F4F4F4" width="556"><input type="text" name="strTitle" size="56"> </td>
      
    </tr>
       
    
<%
      IF RS3.EOF THEN
      ELSE%>
      <input type="hidden" name="cat" value="<%=RS3("id")%>"> 
      <tr>
      <td align="right" bgcolor="#F4F4F4" width="77"><strong><font face="Arial" size="1">Thread
        Topic:</font></strong></td>
      <td bgcolor="#F4F4F4" width="556"><font face="Arial" size="1">&nbsp;&nbsp;  <b><%=RS3("title")%></b></font></td>
      
    </tr>
    <%END IF%>
<input type="hidden" name="status" value="0">
    <tr>
      <td align="right" bgcolor="#F4F4F4" width="77">
      <b><font face="Arial" size="1">Page Content:</font></b>
      </td>
      <td align="right" bgcolor="#F4F4F4" width="556">
      <p align="left"><textarea rows="19" name="strPage" cols="55"></textarea>
      </td>
    </tr>
        <tr>
      <td align="right" bgcolor="#E8E8E8" width="77">
         </td>
      <td align="right" bgcolor="#E8E8E8" width="556">
      <p align="left"><input type="submit" name="submit" class="formButtons" value="Add Discussion Thread">&nbsp; <input CLASS="formButtons" Type="button" value=Cancel onClick="history.back()">
      </td>
    </tr>
  </table>
  </center>
</div>
</form>

</body>

</html>







