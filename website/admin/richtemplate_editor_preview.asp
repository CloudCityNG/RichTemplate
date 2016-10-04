<%@ LANGUAGE="VBSCRIPT"%> 
<%Buffer="True"
'CacheControl "Private"
%>
<!--#INCLUDE FILE="sessioncheck.asp"-->

<!--#include file="db_connection.asp"-->

<%
' get action for page name'

pageAction = Request.Querystring("pageAction")

PNAME = ""&pageAction&" Web Page"%>

<%

ID = Request.Querystring("PAGEID")
defaultpage = Request.Querystring("defaultpage")

Dim html

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

			
			If Request.Querystring("ID")>0 then
			
				
					WEBSITESECTIONSQL = "SELECT * FROM WEBINFO WHERE ID = "&REQUEST.QUERYSTRING("PAGEID")&""
'					response.write websitesectionsql
					SET RS2 = CON.EXECUTE (WEBSITESECTIONSQL)
				
					IF RS2("PENDING")=TRUE THEN
						
						HTML = RS2("MESSAGE2")
						
					ELSE
					
						HTML = RS2("MESSAGE")
					
					END IF
					
				end if

			
			
%>
<HTML><HEAD>




<script>





var editor;
// this function is  called if editor is loaded
function editOnEditorLoaded(objEditor)
{
  // save editor object for later use
  editor = objEditor;
  editor.editGetSelectionStyle("../styleshteet/style_richtemplate.css");


//editor.editSetMode("PREVIEW");


  // read the HTML content from hidden text area
  var html = document.getElementById("__editData").value;
  // write content to editor
  editor.editWrite(html);
  

}
	
	
// load the HTML content from the editor in the hidden field "html"
function transfer()
{
  // get the html content from the editor
  var html = editor.editGetHtml(); 
  var savevalue =0
  //  add the content to the hidden field
  document.form1.publish.value = savevalue;  
  document.form1.html.value = html; 
  // submit the formular 
  //document.form1.submit();
}

function transferLive()
{
  // get the html content from the editor
  var html = editor.editGetHtml(); 
  var savevalue = -1
  //  add the content to the hidden field 
  document.form1.publish.value = savevalue; 
  document.form1.html.value = html; 
  // submit the formular 
  //document.form1.submit();
}



</script>


<link rel="stylesheet" href="style_richtemplate.css" type="text/css">
</HEAD>
<BODY topmargin="0" leftmargin="0" bgcolor="#C4DAFA"  onLoad="clearPreloadPage();">
<%





'GET WEB SITE SECTIONS----------------------------------------------------->
If Request.Querystring("action")="newSection" then
WebSectionName = "[New Section]"
elseif Request.Querystring("action")="style" then
WebSectionName = "Style Sheet"
else

WEBSITESECTIONSQL = "SELECT * FROM WEBINFO WHERE ID = "&REQUEST.QUERYSTRING("PAGEID")&""
SET RS2 = CON.EXECUTE (WEBSITESECTIONSQL)
WebSectionName =RS2("NAME")
end if

%> 
	<%If Request.Querystring("action")= "newSection" then%>

		<form name=form1 action="richtemplate_page_logic.asp?task=newsection" method="post">
	
	<%elseif Request.Querystring("action") ="style" then%>
	
			<form name=form1 action="richtemplate_page_logic.asp?sectionid=<%=REQUEST.QUERYSTRING("sectionid")%>&task=style" method="post">
	<%else%>

		<form name=form1 action="richtemplate_savepage.asp?sectionid=<%=REQUEST.QUERYSTRING("sectionid")%>&ID=<%=Request.Querystring("ID")%>" method="post">
	
	<%end if%>
<!--#INCLUDE FILE="headernew.inc"-->

<table width="100%" cellspacing="0" cellpadding="0">
<tr><td width="1%" class="bodybold">&nbsp;</td>
	<td align="left" colspan=2 class="bodybold">
  <b><font face="Arial" color="#3054A7"> <%if Request.Querystring("id")<1 then%>
  <%if Request.Querystring("subpagename")<>"" then%>
  WEB SECTION: <%=ucase(websectionname)%> > ADD SUB PAGE UNDER WEB PAGE: <%=ucase(Request.Querystring("pagename"))%>

  <%ELSE%>
 ADD WEB PAGE TO WEB SECTION: <%=ucase(websectionname)%>
 <%END IF%>
 <%ELSE%>
 <%if Request.Querystring("subpagename")<>"" then%>
 WEB SECTION: <%=ucase(websectionname)%>	> WEB PAGE: <%=ucase(Request.Querystring("pagename"))%> > </font>
	<font face="Arial" color="#FFFFFF"> EDIT SUBPAGE: <%=ucase(Request.Querystring("subpagename"))%></font><font face="Arial" color="#3054A7"> 
<!-- EDIT SUBPAGE:: <I><%=ucase(Request.Querystring("subpagename"))%></I> &nbsp;&nbsp;&nbsp;UNDER WEB PAGE:: <I><%=ucase(Request.Querystring("pagename"))%></I>&nbsp;&nbsp;&nbsp;IN WEB SECTION:: <I><%=ucase(websectionname)%></I>-->

 <%else%>
 WEB SECTION: <%=ucase(websectionname)%> &gt;</font><font face="Arial" color="#FFFFFF"> 
	EDIT WEB PAGE: <%=ucase(Request.Querystring("pagename"))%></font><font face="Arial" color="#3054A7"> 
 <%end if%>
 
 <%END IF%></font></b></td></td>
</td></tr>
<tr><td bgcolor="#C4DAFA" colspan="2">&nbsp; 

<%	'IF USER IS NOT DOMAIN ADMIN OR ABOVE CHECK FOR PUBLISHING PERMISSION
	
	'IF Session("ACCESS_LEVEL")<3 then

	IF SESSION("ALLOW_PUBLISH")= true THEN
	
%><INPUT type=image  value="save" onclick="transferLive()" ID="Button2" NAME="Button2" src="../editor/design/image/Office2003/button_save01.gif" alt="Save And Publish Live to Web Site" width="120" height="22"> <img border="0" src="../editor/design/image/Office2003/separator.gif" width="5" height="20"> <%end if%>


<INPUT type="image"  value="save" onclick="transfer()" ID="Button1" NAME="Button1" src="../editor/design/image/Office2003/button_pending01.gif" width="130" height="22" alt="Save To Pending Area"> <img border="0" src="../editor/design/image/Office2003/separator.gif" width="5" height="20">

	<%if Request.Querystring("SectionID")<>"" then%>
	<a href="richtemplate_list_pages.aspx?sectionid=<%=Request.Querystring("SECTIONID")%>">
	<%else%>
	<a href="intranet/index.asp">
	<%end if%>
	<img border="0" src="../editor/design/image/Office2003/button_no_save01.gif" width="59" height="22"></a></td>
  <td width="32%" bgcolor="#C4DAFA" height="35">&nbsp;</td></td>
</td></tr>
</table>
<!-- new code starts here ----------------->
<!-- this hidden field is used to post the content -->
		<INPUT type="hidden" name="html" value="" ID="html">
		
		<%If Request.Querystring("action")="add" then%>
		
			<input type="hidden" name="id" value="-1">

		<%else%>

  			<input type="hidden" name="id" value="<%=id%>">

  		<%end if%>

		<INPUT TYPE="HIDDEN" name="defaultpage" value="<%=defaultpage%>" >
		<INPUT TYPE="HIDDEN" name="action" value="style" >

		<INPUT TYPE="hidden" name="PARENTID" value="<%=Request.Querystring("PAGEID")%>" >
		<INPUT TYPE="HIDDEN" name="PAGELEVEL" value="<%=Request.Querystring("PAGELEVEL")%>" >
		<INPUT TYPE="hidden" name="SECTIONID" value="<%=Request.Querystring("SECTIONID")%>" >
		<INPUT TYPE="hidden" name="webpagename" value="<%=Request.Querystring("pagename")%>">
<INPUT TYPE="hidden" name="publish" value="">


<!-- the content is loaded here -->
<div style="position:absolute;visibility:hidden;top:24px;height:86px;left:290px; width:569px">
<TEXTAREA  id="__editData" NAME="__editData" rows="5" cols="67">
<%
If Request.Querystring("action")="add" or Request.Querystring("action")="newSection" then
response.write"<link rel='stylesheet' href='http://"&Request.ServerVariables("server_name")&"/editorConfig/css/editorStyle.css' type='text/css'>"
end if%><%=html%></TEXTAREA>
</div>



<table border="0" width="800" id="table1" cellspacing="0" cellpadding="0">
	<tr>
		
  <table border="0" cellspacing="0" cellpadding="5" id="table2" width="100%">

		<%IF Request.Querystring("action")="newSection" then%>
    <tr>
		<td width="101" class=bodybold align="left" bgcolor="#C4DAFA">Web Section Title:</td>
		<td class=bodybold bgcolor="#C4DAFA">
		
		<input type="text" name="websection" size="34" class=input></td>
	</tr>
		<%else%>

    <tr>

    
		<td width="101" class=bodybold align="left" bgcolor="#C4DAFA">
		<p align="right">Web Page Title:&nbsp; </td>
		<td class=bodybold bgcolor="#C4DAFA">
		
	<% If Request.Querystring("action")="add" then%>
		
		<input type="text" name="pagename"	value="" size="50" class=input>
		
	<%elseif Request.Querystring("defaultpage")="1" then%>	
		
	<%=REQUEST.QUERYSTRING("PAGENAME")%> 
	<input type="hidden" name="pagename"	value="<%=REQUEST.QUERYSTRING("PAGENAME")%>" size="50" class=input>

	<%elseif Request.Querystring("action")="style" then%>
	
		STYLE SHEET EDITOR

	<%else%>
		<input type="text" name="pagename"	value="<%=REQUEST.QUERYSTRING("PAGENAME")%>" size="50" class=input>
		
  
     <%end if%>

       </td>
	</tr>
  

	</tr>
	
    <%END IF%>
    


  </table>
</TR>


	<tr>
		
<!--------------------------------------------->
<!-- set the correct path to pinEdit.html    -->
<!--------------------------------------------->
<TD valign="top">
<IFRAME id="editorFrame" style="WIDTH: 100%; HEIGHT: 600px" src="../editor/pinEdit.html" name="RichTemplate_Editor" border="0" frameborder="0"></IFRAME></TD>
</TR>


	<tr>
		<td><br>
		<!--<INPUT type=button value="save" onclick="transfer()" ID="Button1" NAME="Button1">-->

	</tr>
</table>
&nbsp;<p>&nbsp;</p>
</form>

<SCRIPT LANGUAGE="JavaScript">

<!-- Begin
function clearPreloadPage() { //DOM
if (document.getElementById){
document.getElementById('prepage').style.visibility='hidden';
}else{
if (document.layers){ //NS4
document.prepage.visibility = 'hidden';
}
else { //IE4
document.all.prepage.style.visibility = 'hidden';
}
}
}
// End -->
</SCRIPT>
<div id="prepage" style="position:absolute; width:100%; height:31px; top:195px; z-index:1; left:4px"> 
<center>
  <table width="200" border="2" cellspacing="0" cellpadding="5" bgcolor="#FFFFFF" bordercolor="#0050C0">
	<tr>
	   <td class="bodybold">
		<p align="center">The WYSIWYG editor is being loaded.<b>&nbsp; Please wait...</b></p>
		<p align="center"><img src="images/info.gif" width="18" height="18"></p></td>
	</tr>
  </table>
</center>
</div>
</body>
</html>