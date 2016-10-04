<!--#include file="loggedin.asp"-->
<!--#include file ="includes/dateformat.inc"-->


<html>

<head>
<meta content="text/html; charset=windows-1252" http-equiv="Content-Type">
<meta content="Microsoft FrontPage 4.0" name="GENERATOR">


<title>HTML News Editor</title>
<style>
.formRequiredInput {
	font-size : 8pt;
	font-weight : bold;
	font-family : Verdana;
	font-style : normal;
	color : #3D4A7E;
	background : #D5F0FF;
	text-decoration : none;
}
.text{
	font-size : 7pt;
	font-weight : bold;
	font-family : Verdana;
	font-style : normal;
	color : #000000;
	}
</style>
</head>

<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0" bgcolor="#F4F4F4">
<%
if request.querystring ("task") = "complete" then%>
 <p align="center"><br><br><Br><font face="arial" color="#0c720e" size="2">Your Memo Has Been Sent.<a href="javascript:close('window')">CLOSE WINDOW</A></font></p>
<%
else

if request.Querystring("task")= "error" then

Response.write "<CENTER><font face=arial size=3 color=red>PLEASE FILL OUT ALL FORM FIELDS</FONT></CENTER>"
END IF
Dim id, ndate, strtitle, strauthorx, strmailx, strcat, mtype, page, style, file
Dim Con 
Dim objRecordSet
Dim lRecordsFound

%>
 <!---------------------------------------ADD AND EDIT SERVICES------------------------------------->
 <!---------------------------------------ADD AND EDIT SERVICES------------------------------------->
 <!---------------------------------------ADD AND EDIT SERVICES------------------------------------->

<%Function DateFormat(Data)
Dim WD, OD
	if not(isdate(data)) then WD = Date() else WD = cdate(data)
	OD = right("00" & cstr(month(WD)),2) & "/" & cstr(day(WD)) & "/" & cstr(year(WD))
	DateFormat = "#" & OD & "#"
End Function

	id = request("id")




	set Con = server.createobject("ADODB.Connection")
	set objRecordSet = server.createobject("ADODB.recordset")
	Con.ConnectionTimeout = 15
	Con.CommandTimeout = 30
	%>

<!--#include file ="conn.asp"-->
	<%Set objRecordSet = Con.Execute("SELECT * FROM compmemo WHERE id = " & id, lRecordsFound)
	
	If not objrecordset.eof Then 
			id = objrecordset("id")
			ndate=objrecordset("varDate")
			subj=objrecordset("varSubject")
			authorx=objrecordset("varAuthor")
			page = objrecordset("varText")
			objrecordset.close
	else
			style = "none"
			ndate = date()
			title = "Enter A Memo Title"
			authorx = "Enter Memo Author Here"
			page = ""
			objrecordset.close
	end if

	Con.close

	set objrecordset = nothing
	set Con = nothing

%>

<script ID="clientEventHandlersJS" LANGUAGE="javascript">
<!--

function Save_onclick() {
		
	SaveForm.Page.value = idContent.document.body.outerHTML;
	SaveForm.submit();
}

function Back_onclick() {
	location.href="members/default.asp"
}

function Del_onclick() {
var aux;

	aux = confirm("Delete this News ?")
	if (aux == true)
		{ location.href = "deletenews.asp?id=<%=id%>" }
}

-->
</script>
<script LANGUAGE="JavaScript">
function button_over(eButton)
	{
	eButton.style.backgroundColor = "#B5BDD6";
	eButton.style.borderColor = "darkblue darkblue darkblue darkblue";
	}
function button_out(eButton)
	{
	eButton.style.backgroundColor = "threedface";
	eButton.style.borderColor = "threedface";
	}
function button_down(eButton)
	{
	eButton.style.backgroundColor = "#8494B5";
	eButton.style.borderColor = "darkblue darkblue darkblue darkblue";
	}
function button_up(eButton)
	{
	eButton.style.backgroundColor = "#B5BDD6";
	eButton.style.borderColor = "darkblue darkblue darkblue darkblue";
	eButton = null; 
	}
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

var isHTMLMode=false

function document.onreadystatechange()
	{
  	idContent.document.designMode="On"
	}
function cmdExec(cmd,opt) 
	{
  	if (isHTMLMode){alert("Please uncheck 'Edit HTML'");return;}
  	idContent.document.execCommand(cmd,"",opt);idContent.focus();
	}
function setMode(bMode)
	{
	var sTmp;
  	isHTMLMode = bMode;
  	if (isHTMLMode){sTmp=idContent.document.body.innerHTML;idContent.document.body.innerText=sTmp;} 
	else {sTmp=idContent.document.body.innerText;idContent.document.body.innerHTML=sTmp;}
  	idContent.focus();
	}
function createLink()
	{
	if (isHTMLMode){alert("Please uncheck 'Edit HTML'");return;}
	cmdExec("CreateLink");
	}
function insertImage()
	{
	if (isHTMLMode){alert("Please uncheck 'Edit HTML'");return;}
	var sImgSrc=prompt("Insert Image File (You can use your local image file) : ", "http://www.aspalliance.com/Yusuf/Article10/sample.jpg");
	if(sImgSrc!=null) cmdExec("InsertImage",sImgSrc);
	}
function Save() 
	{
	if (isHTMLMode){alert("Please uncheck 'Edit HTML'");return;}
  	var sImgTag = idContent.document.body.all.tags("IMG");
  	var oImg;
  	for (var i = sImgTag.length - 1; i >= 0; i--) 
		{
    	oImg = sImgTag[i];
    	alert("Add your code to Upload local image file here. Image Inserted : " + oImg.src );
  		}
  	alert("Add your code to Save Document here");
  	alert("Your Document : " + idContent.document.body.innerHTML);
	}
function foreColor()
	{
	var arr = showModalDialog("includes/selcolor.htm","","font-family:Verdana; font-size:12; dialogWidth:30em; dialogHeight:34em" );
	if (arr != null) cmdExec("ForeColor",arr);	
	}
</script>

<center>
<form method="post" action="memosave.asp" name="SaveForm" enctype="multipart/form-data" onSubmit="return checkrequired(this);">
  <input type="hidden" name="id" value="<%=id%>"><input type="hidden" name="File" size="30"><input type="hidden" name="Page"
  value="   ">


<input type="hidden" name="ndate" size="10" value="<%=varDate%>">


</center>

  <div align="center">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
      <tr>
        <td align="right" width="691" colspan="2" bgcolor="#ffffff" bordercolor="#FFFFFF">
          <p align="left"><img border="0" src="images/toad.gif"></td>

<center>
    <center>
      </tr>
                              <TR><TD BGCOLOR="#3D4A7E" COLSPAN=2><CENTER><FONT FACE="VERDANA" SIZE="2" COLOR="#FFFFFF"><B>SEND A MEMO</B></FONT></CENTER></TD></TR>
                        
                        
      <tr>
        <td class="text" align="right" bgcolor="#e8e8e8" width="103">Memo Title:</td>
        <td  bgcolor="#e8e8e8">
<input type="text" name="title" value="<%=title%>" class="formRequiredInput" size="33" onFocus="this.value=''; return true">
        </td>
      </tr>
      <tr>
        <td class="text" align="right" bgcolor="#f4f4f4" width="103">Memo
          Author:</td>
        <td bgcolor="#f4f4f4">
<input type="text" name="authorx" class="formRequiredInput"  value="<%=authorx%>" size="33" onFocus="this.value=''; return true"> 
        </td>
      </tr>
    </table>
    </center>
  </div>
  </form>

<table id="tblCoolbar" width=100% cellpadding="0" cellspacing="0" bgcolor="#D4D0C8">
<tr valign="middle">

	<td colspan=16 width="535">
		<select onchange="cmdExec('formatBlock',this[this.selectedIndex].value);this.selectedIndex=0">
			<option selected>Style</option>
			<option value="Normal">Normal</option>
			<option value="Heading 1">Heading 1</option>
			<option value="Heading 2">Heading 2</option>
			<option value="Heading 3">Heading 3</option>
			<option value="Heading 4">Heading 4</option>
			<option value="Heading 5">Heading 5</option>
			<option value="Address">Address</option>
			<option value="Formatted">Formatted</option>
			<option value="Definition Term">Definition Term</option>
		</select> 
		<select id="FontSelection" onchange="cmdExec('fontname',this[this.selectedIndex].value);">
			<option selected>Font</option>
			<option value="Arial">Arial</option>
			<option value="Arial Black">Arial Black</option>
			<option value="Arial Narrow">Arial Narrow</option>
			<option value="Comic Sans MS">Comic Sans MS</option>
			<option value="Courier New">Courier New</option>
			<option value="System">System</option>
			<option value="Tahoma">Tahoma</option>
			<option value="Times New Roman">Times New Roman</option>
			<option value="Verdana">Verdana</option>
			<option value="Wingdings">Wingdings</option>
		</select> 
		<select onchange="cmdExec('fontsize',this[this.selectedIndex].value);">
			<option selected>Size</option>
			<option value="1">1</option>
			<option value="2">2</option>
			<option value="3">3</option>
			<option value="4">4</option>
			<option value="5">5</option>
			<option value="6">6</option>
			<option value="7">7</option>
			<option value="8">8</option>
			<option value="10">10</option>
			<option value="12">12</option>
			<option value="14">14</option>
		</select> 
		
	</td>
	
</tr>
<tr>

	<td width="31"><div class="cbtn" onClick="cmdExec('cut')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="1" vspace=1 align=absmiddle src="images/cut.gif" alt="Cut">
	</div></td>
	
	<td width="31"><div class="cbtn" onClick="cmdExec('copy')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="1" vspace=1 align=absmiddle src="images/copy.gif" alt="Copy">
	</div></td>	
	
	<td width="31"><div class="cbtn" onClick="cmdExec('paste')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="1" vspace=1 align=absmiddle src="images/paste.gif" alt="Paste">
	</div></td>	
	
	<td width="31"><div class="cbtn" onClick="cmdExec('bold')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="1" vspace=1 align=absmiddle src="images/bold.gif" alt="Bold">
	</div></td>
	
	<td width="31"><div class="cbtn" onClick="cmdExec('italic')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="1" vspace=1 align=absmiddle src="images/italic.gif" alt="Italic">
	</div></td>	
	
	<td width="31"><div class="cbtn" onClick="cmdExec('underline')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="1" vspace=1 align=absmiddle src="images/under.gif" alt="Underline">
	</div></td>		
		
	<td width="31"><div class="cbtn" onClick="cmdExec('justifyleft')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="1" vspace=1 align=absmiddle src="images/left.gif" alt="Justify Left">
	</div></td>
	
	<td width="31"><div class="cbtn" onClick="cmdExec('justifycenter')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="1" vspace=1 align=absmiddle src="images/center.gif" alt="Center">
	</div></td>	
	
	<td width="31"><div class="cbtn" onClick="cmdExec('justifyright')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="1" vspace=1 align=absmiddle src="images/right.gif" alt="Justify Right">
	</div></td>		
	
	<td width="33"><div class="cbtn" onClick="cmdExec('insertorderedlist')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="2" vspace=1 align=absmiddle src="images/numlist.gif" alt="Ordered List">
	</div></td>	

	<td width="33"><div class="cbtn" onClick="cmdExec('insertunorderedlist')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="2" vspace=1 align=absmiddle src="images/bullist.gif" alt="Unordered List">
	</div></td>
		
	<td width="32"><div class="cbtn" onClick="cmdExec('outdent')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="2" vspace=1 align=absmiddle src="images/deindent.gif" alt="Decrease Indent">
	</div></td>	
	
	<td width="32"><div class="cbtn" onClick="cmdExec('indent')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="2" vspace=1 align=absmiddle src="images/inindent.gif" alt="Increase Indent">
	</div></td>			
	
	<td width="32"><div class="cbtn" onClick="foreColor()" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="2" vspace=1 align=absmiddle src="images/fgcolor.gif" alt="Forecolor">
	</div></td>			

	<td width="32"><div class="cbtn" onClick="cmdExec('createLink')" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="2" vspace=1 align=absmiddle src="images/link.gif" alt="Link">
	</div></td>	
	
	<td width="32"><div class="cbtn" onClick="insertImage()" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);">
	<img hspace="2" vspace=1 align=absmiddle src="images/image.gif" alt="Image">
	</div></td>		
	
	<td width="122"><div class="cbtn" onClick="Save_onclick()" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onmouseup="button_up(this);" style="width: 122; height: 38">
	<img  hspace="2" vspace=1 align=left align=absmiddle name="Save" src="images/save.gif" alt="Save">
    <font class=text>Send Memo&nbsp;&nbsp;</font>
	</div></td>		
		
	<td width="32"></td>	
	
</tr>
</table>
<center>
<!--webbot bot="HTMLMarkup" startspan --><IFRAME id="idContent" src="memotemplate.asp?id=<%=id%>" style="HEIGHT: 40%; WIDTH: 100%"><!--webbot bot="HTMLMarkup" endspan --><!--webbot bot="HTMLMarkup" startspan --></IFRAME><!--webbot bot="HTMLMarkup" endspan -->
</center>
<p class="text"><input type="checkbox" onclick="setMode(this.checked)"> Edit HTML</p>

<script>

var otherDoc;


function showSelectionFont() {
  var f;
  f = otherDoc.queryCommandValue("FontName");
  if (!f) 
    f = "null";
  // to lower the string before comparting it - use all lower for all id's.
  f = f.toLowerCase();

  c = FontSelection.all[f];

  if ((c) && (!c.selected)) {
    c.selected = true;
  }
}

function init() {
  otherDoc = document.frames.idContent.document;
  otherDoc.body.onselect = showSelectionFont;
}

window.onload = init;

</script> 


</center>
<%end if%>
</body>
</html>
















