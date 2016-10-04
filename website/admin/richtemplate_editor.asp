<%@ LANGUAGE="VBSCRIPT"%> 
<%Buffer="True"

'CacheControl "Private"
%>

<%
Session("metaKeyword") 	= ""
Session("metaDesc")		= ""
Session("metaTitle")	= ""	
%>


<!--#INCLUDE FILE="sessioncheck.asp"-->

<!--#include file="db_connection.asp"-->

<%
' get action for page name'

pageAction = Request.Querystring("pageAction")

PNAME = ""&pageAction&" Web Page"
PHELP = "helpFiles/pageListing.asp#WYSIWYG"%>

<%

ID = Request.Querystring("PAGEID")
defaultpage = Request.Querystring("defaultpage")


SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

			
			If Request.Querystring("id")>0 then
			
				
					sql1 = "SELECT PENDING, MESSAGE,MESSAGE2,METADESC,metaTitle,METAKEYWORD FROM WEBINFO WHERE ID = "&REQUEST.QUERYSTRING("PAGEID")&""
					SET RS2 = CON.EXECUTE (sql1)

				
	
					IF RS2("PENDING")=FALSE THEN

						HTML = RS2("MESSAGE")
						
					ELSEIF RS2("PENDING")=TRUE THEN
					
						HTML = RS2("MESSAGE2")
						
					ELSE
					
						HTML = "AN ERROR HAS OCCURED"

					
					END IF
					
					Session("metaDesc") = RS2("metaDesc")
					Session("metaTitle")=RS2("metaTitle")
					Session("metaKeyword")=RS2("metaKeyword")
					
				end if

			
			
%>
<HTML><HEAD>
<HEAD>

<SCRIPT LANGUAGE="JavaScript">

<!-- Rollover scirpt for save buttons

image1 = new Image();
image1.src = "images/button_save02.gif";
image2 = new Image();
image2.src = "images/button_pending02.gif";
image3 = new Image();
image3.src = "images/button_no_save02.gif";
// End -->
</script>



<script>

<!--  CALL CLIENT API HERE -->

var editor;
// this function is  called if editor is loaded
function editOnEditorLoaded(objEditor)
{
  // save editor object for later use
  editor = objEditor;

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
  //document.form1.submit();
  if (document.form1.onsubmit()) { document.form1.submit();
  return false;
}


return true;

}


function transferLive()
{
  // get the html content from the editor
  var html = editor.editGetHtml(); 
  var savevalue = 1
  //  add the content to the hidden field 
  document.form1.publish.value = savevalue; 
  document.form1.html.value = html; 
  //document.form1.submit();
  if (document.form1.onsubmit()) { document.form1.submit();
  return false;
}
return true;

}

</script>


<!-- validate page name is not blank -->
<SCRIPT LANGUAGE="JavaScript">
 
function validatePage() {

if (document.form1.pagename.value.length =="") {
alert("Please enter a web page name.");
document.form1.pagename.focus();
return false;
}


return true;
}
</SCRIPT> 

<!-- validate web section is not blank -->

<SCRIPT LANGUAGE="JavaScript">
 
function validateSection() {

if (document.form1.websection.value.length =="") {
alert("Please enter a web section name.");
document.form1.websection.focus();
return false;
}


return true;
}
</SCRIPT> 
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
<!--
<div id="prepage" style="FONT-SIZE:16px; Z-INDEX:101; LEFT:0px; BACKGROUND-IMAGE:url('images/semi-transparent.gif'); WIDTH:99.9%; BACKGROUND-REPEAT:repeat; FONT-FAMILY:arial; POSITION:absolute; TOP:0px; HEIGHT:99.9%; BACKGROUND-COLOR:transparent; layer-background-color:white">
			<table class="preloaderT1" width="100%" height="100%" align="center"  onClick="return false;">
				<tr width="100%" height="100%" align="center" valign="middle">
					<td>
						<table Cellpadding="0"	Cellspacing="0" class="preloaderT2">
							<tr align="center" valign="middle">
								<td nowrap>
									<table class="preloaderT3" cellpadding="7" cellspacing="0">
										<tr align="center" valign="middle">
											<td  width="50" height="49" onClick="return false;">
												&nbsp;
											</td>
											<td nowrap align="left"  bgcolor="#FFFFFF" onClick="return false;">
												<b>Loading ...... Please wait !&nbsp;&nbsp;</b>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
-->
<link rel="stylesheet" href="styles.css" type="text/css">
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
	
		<form name=form1 action="richtemplate_page_logic.asp?task=newsection" method="post"  onSubmit="return validateSection();">
	
	<%else%>

		<form name=form1 action="richtemplate_savepage.asp?sectionid=<%=REQUEST.QUERYSTRING("sectionid")%>&ID=<%=Request.Querystring("ID")%>" method="post" onSubmit="return validatePage();">
	
	<%end if%>
<!--#INCLUDE FILE="headernew.inc"-->

<table width="100%" cellspacing="0" cellpadding="0">
<tr><td class="bodybold" colspan="2" height="25">
  <b><font face="Arial" color="#3054A7">&nbsp; <%if Request.Querystring("id")<1 then%>
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
 
 <%END IF%></font></b></td>
	</td>
</td></tr>
<tr>		
	
	<%IF Request.Querystring("action")="newSection" then


	'*************** Set pagename session for attribute area ***************

	Session("pagename") = "[Pending Save]"%>


    <tr>
		<td class=bodybold align="left" bgcolor="#C4DAFA" colspan="2" height="24">&nbsp; Web Section Title:&nbsp;
		
		<input type="text" name="websection" size="34" class=input>
	</td>
	<%if Session("ACCESS_LEVEL")>2 then%>
<tr>
	<td>
		<p class="bodybold">&nbsp;
		If display page will be other than interior.asp type name here:&nbsp;&nbsp;
		<input type="text" name="PAGE_LINKNAME" size="14" value="index.asp">
	</td>
	
	</tr>
	<%END IF%>
	</tr>
	<%else
	'*************** Set pagename session for attribute area ***************

	Session("pagename") = Request.Querystring("pagename")%>
		

    <tr>

    
		<td class=bodybold align="left" bgcolor="#C4DAFA" colspan="2" height="24">
		<p>&nbsp; Web Page Title:&nbsp; 
		
	<% If Request.Querystring("action")="add" then

	'*************** Set pagename session for attribute area ***************

	Session("pagename") = "[Pending Save]"%>
	
		
		<input type="text" name="pagename"	value="" size="50" class=input>
		
	<%elseif Request.Querystring("defaultpage")="1" then%>	
	<%=REQUEST.QUERYSTRING("PAGENAME")%> 
	<input type="hidden" name="pagename"	value="<%=REQUEST.QUERYSTRING("PAGENAME")%>" size="50" class=input>

	<%elseif Request.Querystring("action")="style" then%>
	
		STYLE SHEET EDITOR

	<%else%>
	

		<input type="text" name="pagename"	value="<%=REQUEST.QUERYSTRING("PAGENAME")%>" size="50" class=input>&nbsp;<!--Preview page:&nbsp;<a target="_blank" href = "richtemplate_preview.asp?page=<%=Request.Querystring("PageID")%>&sectionid=<%=Request.Querystring("Sectionid")%>&pagelevel=<%=RS2("pagelevel")%>"><%=Request.Querystring("pagename")%></a>-->
		
  
     <%end if%>

       &nbsp; </td>
	</tr></tr>
	<%end if%>
<tr><td bgcolor="#9EBEF5">


	<table border="0" width="374" cellspacing="0" cellpadding="0" id="table4" height="27">
		<tr>
			<td height="27" width="16" background="images/2003_toolbar_back.gif"><img border="0" src="images/2003_toolbar_left.gif" width="16" height="28"></td>
			<td height="27" width="120" background="images/2003_toolbar_back.gif">
			<%	'IF USER IS NOT DOMAIN ADMIN OR ABOVE CHECK FOR PUBLISHING PERMISSION
	
	'IF Session("ACCESS_LEVEL")<3 then

	IF SESSION("ALLOW_PUBLISH")= true THEN
	
%><a href="#" onmouseover="image1.src='images/button_save02.gif';" onmouseout="image1.src='images/button_save01.gif';">
<img name="image1" src="images/button_save01.gif" border=0 onclick="transferLive()" alt="Save this page and publish live"></a><!--<INPUT type=image  value="save" onclick="transferLive()" ID="Button2" NAME="Button2" src="images/button_save01.gif" alt="Save And Publish Live to Web Site">--><%end if%></td>
			<td height="27" width="10" background="images/2003_toolbar_back.gif">


<img border="0" src="images/seperator.gif" width="2" height="20"></td>
			<td height="27" width="130" background="images/2003_toolbar_back.gif">
<a href="#" onmouseover="image2.src='images/button_pending02.gif';" onmouseout="image2.src='images/button_pending01.gif';">
<img name="image2" src="images/button_pending01.gif" border=0 onclick="transfer()" alt="Save this page publish pending"></a><!--<INPUT type="image"  value="save" onclick="transfer()" ID="Button1" NAME="Button1" src="images/button_pending01.gif" width="130" height="22" alt="Save To Pending Area">--></td>
			<td width="10" background="images/2003_toolbar_back.gif">
			<img border="0" src="images/seperator.gif" width="2" height="20"></td>
			<td height="27" width="59" background="images/2003_toolbar_back.gif">	
			
<%if Request.Querystring("SectionID")<>"" then%>
	<a href="richtemplate_list_pages.aspx?sectionid=<%=Request.Querystring("SECTIONID")%>" onmouseover="image3.src='images/button_no_save02.gif';" onmouseout="image3.src='images/button_no_save01.gif';">
<%else%>
	<a href="richtemplate_welcome.asp?mode=forms" onmouseover="image3.src='images/button_no_save02.gif';" onmouseout="image3.src='images/button_no_save01.gif';">
<%end if%>
	<img border="0" src="images/button_no_save01.gif" width="59" height="22" name="image3" alt="Cancel"></a></td>
			<td height="27" width="13"><img border="0" src="images/2003_toolbar_right.gif" width="13" height="28"></td>
		</tr>
	</table>



	</td>
  <td width="35%" bgcolor="#9EBEF5" align="right">
	<table border="0" width="200" id="table3">
		<tr>
		
		<%If Request.Querystring("action")="newSection"  or Request.Querystring("action")= "add" then%>
				<td width="18" class="body"><img border="0" src="images/icon_pending_page.gif"></td>
				<td class="body">Status: Pending Save</td>
		<%else%>
		
		
			<td width="18" class="body">
			<%IF RS2("PENDING")<>0 THEN%>
			<img border="0" src="images/icon_pending_page.gif">
			<%else%>
			<img border="0" src="images/icon_live_page.gif">
			<%end if%></td><td class="body">
			
			<%IF RS2("PENDING")<>0 THEN
			RESPONSE.WRITE "Page Status is Pending"
			ELSE
			RESPONSE.WRITE "Page Status is Live"
			END IF%>
		

		<%end if%>
			</td>
		</tr>
	</table>
  </td></td>
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
		<INPUT TYPE="hidden" name="PAGELEVEL" value="<%=Request.Querystring("PAGELEVEL")%>"width="1" >
		<INPUT TYPE="hidden" name="SECTIONID" value="<%=Request.Querystring("SECTIONID")%>" >
		<INPUT TYPE="hidden" name="webpagename" value="<%=Request.Querystring("pagename")%>">
<INPUT TYPE="hidden" name="publish" value="">


<!-- the content is loaded here -->
<div style="position:absolute;visibility:hidden;top:24px;height:86px;left:290px; width:569px">
<TEXTAREA  id="__editData" NAME="__editData" rows="5" cols="67">
<%
If Request.Querystring("action")="add" or Request.Querystring("action")="newSection" then
response.write"<head><link rel='stylesheet' href='/editorConfig/css/editorStyle.css' type='text/css'></head><body>"

end if%><%=html%></TEXTAREA>
</div>



<table border="0" width="800" id="table1" cellspacing="0" cellpadding="0">
	<tr>
		
  <table border="0" cellspacing="0" cellpadding="5" id="table2" width="100%" >


  

	</tr>
	

    


  </table>
</TR>


	<tr>
		
<!--------------------------------------------->
<!-- set the correct path to pinEdit.html    -->
<!--------------------------------------------->
<TD valign="top"><IFRAME id="editorFrame" style="WIDTH: 100%; HEIGHT: 80%" src="../editor/pinEdit.html" name="RichTemplate_Editor" border="0" frameborder="0"></IFRAME></TD>
</TR>
</table>
</form>
</body>
</html>