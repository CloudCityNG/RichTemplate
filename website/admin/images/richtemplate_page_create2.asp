<%@ LANGUAGE="VBSCRIPT"%> 
<%Buffer="True"
'CacheControl "Private"
%>
<!--#INCLUDE FILE="check.asp"-->


<HTML><HEAD>
<link rel="stylesheet" href="style_richtemplate.css" type="text/css">
<SCRIPT ID=clientEventHandlersJS LANGUAGE=javascript>
<!--

function form1_onsubmit() {
if (form1.txtusername.value == "" || form1.txtpassword.value == "")
return false;
}

//-->
</SCRIPT>
</HEAD>
<BODY>
<table border="0" cellpadding="0" cellspacing="0" width="780"  height="100%">
    <td valign="top" align="center" width="100%">
<!--#INCLUDE FILE="header.inc"-->


<!--#include file="db_connection.asp"-->

<%Function DateFormat(Data)
Dim WD, OD
	if not(isdate(data)) then WD = Date() else WD = cdate(data)
	OD = right("00" & cstr(month(WD)),2) & "/" & cstr(day(WD)) & "/" & cstr(year(WD))
	DateFormat = "#" & OD & "#"
End Function

	id = request("id")




	set objDataCon = server.createobject("ADODB.Connection")
	set objRecordSet = server.createobject("ADODB.recordset")
	objDataCon.ConnectionTimeout = 15
	objDataCon.CommandTimeout = 30
	objDataCon.Open ConnectionString


	Set objRecordSet = objDataCon.Execute("SELECT * FROM WEBPAGE WHERE id = " & id, lRecordsFound)
	
	If not objrecordset.eof Then 
			id = objrecordset("id")
			
			If Request.Form("webpagename")="" then
			name=objrecordset("name")
			else
			name=REQUEST.FORM("webpagename")
			end if
			If Request.Form("sectionname")="" then
			sectionid=objrecordset("sectionid")
			else
			sectionid=REQUEST.FORM("sectionname")
			end if
			

			'defaultpage=objrecordset("defaultpage")
			
				if request.form("defaultpage")="on" then
					
					defaultpage=1
				
				else
				
					defaultpage=0
					
				end if
				
			
			
			page = objrecordset("message")
			
			objrecordset.close
	else
			style = "none"
			name=REQUEST.FORM("webpagename")
			sectionid=REQUEST.FORM("sectionname")
							if request.form("defaultpage")="on" then
					
					defaultpage=1
				
				else
				
					defaultpage=0
					
				end if
			page = ""
	end if

	objdatacon.close

	set objrecordset = nothing
	set objdatacon = nothing


%>

<script src=enc.js></script><script language="JavaScript1.2">
<!--
	var imageWin
	var propWin
	var inserttableWin
	var previewWin
	var modifytableWin
	var insertFormWin
	var textFieldWin
	var hiddenWin
	var buttonWin
	var checkboxWin
	var radioWin
	var charWin
	var linkWin
	var emailWin
	var anchorWin

	var selectedTD
	var selectedTR
	var selectedTBODY
	var selectedTable
	var selectedImage
	var selectedForm
	var selectedTextField
	var selectedTextArea
	var selectedHidden
	var selectedbutton
	var selectedCheckbox
	var selectedRadio
	var tableDefault
	tableDefault = 1
	var editDefault
	editDefault = 0
	var fileCache
	fileCache = 0
	var statusMode = ""
	var statusBorders = ""
	var toggle = "off"
	var borderShown = "no"
	var fooURL
	var reloaded
	var justSwitched = false
	reloaded = 0

	window.onload = doLoad
	window.onresize = resizeWin
	window.onerror = stopError
	var loaded = false

	function stopError() {
		return true;
	}

	function doLoad() {
		if (editDefault != 1)
			foo.document.designMode = 'On';

		resizeWin()
		if (fileCache == 1) {
			foo.document.location.reload(true)
		}
	}

	function fooLoad() {
		if (!justSwitched)
		{

		if ((fileCache != 1) && (editDefault == 1)) {
			loaded = true
		}

		if (loaded == true) {
			if (tableDefault != 0) {
				toggleBorders()
			}
			displayUserStyles()
			showStatus()
			fooURL = foo.location.href
		}

		if (editDefault == 1) {
			makeEditable()
		}

		loaded = true

		}
	}

	function doCommand(cmd) {
		if (isAllowed())
		{
			document.execCommand(cmd)
		}
	}

	function scrollUp() {
		top.foo.scrollBy(0,0);
	}


	function resizeWin() {
		height = 102
		if (Mode == 2)
		{ height = 71
		}
		document.all.foo.height = document.body.clientHeight-height + "px"
		document.all.foo.focus();
	}

	var Mode = "1";

	var toggleWasOn
	var fontChange
	function SwitchMode () {

		 if (Mode == "1") {
			if (borderShown == "yes") {
				toggleBorders()
				toggleWasOn = "yes"
			} else {
				toggleWasOn = "no"
			}

			toolbar_full.className = "hide";
			toolbar_code.className = "bevel3";

			// get details to replace back when done
			fontFamily = foo.document.body.style.fontFamily
			fontSize = foo.document.body.style.fontSize
			text = foo.document.body.text
			bgColor = foo.document.body.bgcolor
			background = foo.document.body.background

			// Put HTML in editor
			code = foo.document.documentElement.outerHTML

			re = /&amp;/g
			re2 = /href="http:\/\/www.richtemplate.com/g
			re3 = /src="http:\/\/www.richtemplate.com/g

			replaceHref = 'href="'
			replaceImage = 'src="'

			code = code.replace(re,'&')
			code = code.replace(re2,replaceHref)
			code= code.replace(re3,replaceImage)

			foo.document.body.innerText = code

			// nice looking source editor
			foo.document.body.style.fontFamily = "Courier"
			foo.document.body.style.fontSize = "10pt"
			foo.document.body.bgColor = '#FFFFFF';
			foo.document.body.text = '#000000';
			foo.document.body.background = '';
			fontChange = true	
			
			Mode = "2";
		} else {

			loaded = false;
			justSwitched = true
			foo.document.write(foo.document.body.innerText);
			foo.document.close()

			if (fontChange == true) {

				if (fontFamily != "") {
					foo.document.style.fontFamily = fontFamily
				} else {
					foo.document.body.style.removeAttribute("fontFamily")
				}
	
				if (fontSize != "") {
					foo.document.style.fontSize = fontSize
				} else {
					foo.document.body.style.fontSize = ""
				}

				if (bgColor != "")
				{
					foo.document.bgColor = bgColor
				} else {
					foo.document.body.removeAttribute("bgColor")
				}
				
				if (text != "")
				{
					foo.document.text = text
				} else {
					foo.document.body.removeAttribute("text")
				}
				
				if (background != "")
				{
					foo.document.body.background = background

				} else {
					foo.document.body.removeAttribute("background")
				}
			}

			toolbar_full.className = "bevel3";
			toolbar_code.className = "hide";

			Mode = "1";

			if (toggleWasOn == "yes") {
				toggleBorders()
				toggleWasOn = "no"
			}
		}
		resizeWin()
		showStatus()
	}

	function SaveHTMLPage() {
		if (Mode == "2") { 
			SwitchMode()
		}
		
		if (borderShown == "yes") {
			toggleBorders()
		}
		
		if (editDefault == 1)
		{
			revertEditable()
		}
		
		var filename = EditorForm.FileName.value
		var incext = EditorForm.FileExt.value
		var position
		var fileext
	

		position = filename.lastIndexOf(".")
		fileext = filename.substring(position+1, filename.length)


		document.EditorForm.Page.value = foo.document.body.innerHTML
	


		re = /&amp;/g
		re2 = /href="http:\/\/www.carolinastudios.org/g
		re3 = /src="http:\/\/www.carolinastudios.org/g

		replaceHref = 'href="'
		replaceImage = 'src="'

		document.EditorForm.Page.value = document.EditorForm.Page.value.replace(re,'&')
		document.EditorForm.Page.value = document.EditorForm.Page.value.replace(re2,replaceHref)
		document.EditorForm.Page.value = document.EditorForm.Page.value.replace(re3,replaceImage)
		document.EditorForm.Page.value = replaceIncludes(document.EditorForm.Page.value)

		document.EditorForm.submit(); 
		


		closePopups()

	}

	function doLink() {
		if (isAllowed())
		{
			if (isSelection()) { 
				var leftPos = (screen.availWidth-700) / 2
				var topPos = (screen.availHeight-500) / 2 
		 		linkWin = window.open('makeedit.asp?ToDo=InsertLink','','width=700,height=500,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
			} else
				return
		}
	}

	function doImage() {
		if (isAllowed())
		{

		if (isImageSelected()) {	 
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
			imageWin = window.open('makeedit.asp?ToDo=ModifyImage','','width=500,height=430,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
		} else {
			var leftPos = (screen.availWidth-700) / 2
			var topPos = (screen.availHeight-500) / 2 
			imageWin = window.open('makeedit.asp?ToDo=InsertImage','','width=700,height=500,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
		}

		}
	}

	function selectImage(image) {
			document.execCommand("InsertImage",false,image);
	}

	function setBackgd(image) {
			foo.document.body.background = image
	}

	function ModifyProperties() {
		var leftPos = (screen.availWidth-500) / 2
		var topPos = (screen.availHeight-550) / 2 
	 	propWin = window.open('makeedit.asp?ToDo=PageProperties','','width=500,height=550,scrollbars=no,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
	}

	function ShowInsertTable() {
		if (isAllowed())
		{

		var leftPos = (screen.availWidth-500) / 2
		var topPos = (screen.availHeight-400) / 2 
 		inserttableWin = window.open('makeedit.asp?ToDo=InsertTable','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);

		}
	}

	function ShowPreview(source) {
	
var	HTMLREPLACE = '<HTML>'
var HTMLREPLACE2 = '</HTML>'
var HEADREPLACE = '<HEAD>'
var HEADREPLACE2 = '</HEAD>'
var TITLEREPLACE = '<TITLE>'
var TITLEREPLACE2 = '</TITLE>'
var BODYREPLACE = '<BODY>'
var BODYREPLACE2 = '</BODY>'

		baseRef = foo.location.href
		ax=baseRef.lastIndexOf('\/')+1;
		if(ax!=-1) baseRef=baseRef.substring(0,ax);

		if (borderShown == "yes") {
			toggleBorders()
			toggleWasOn = "yes"
		} else {
			toggleWasOn = "no"
		}

		if (editDefault ==1)
		{
			revertEditable()
		}

		var previewHTML
		var page
		if (source == 1)
		{


			previewHTML = foo.document.body.innerText
			previewHTML = previewHTML.replace(HTMLREPLACE,' ')
			previewHTML = previewHTML.replace(HTMLREPLACE2,' ')
			previewHTML = previewHTML.replace(HEADREPLACE,' ')
			previewHTML = previewHTML.replace(HEADREPLACE2,' ')
			previewHTML = previewHTML.replace(TITLEREPLACE,' ')
			previewHTML = previewHTML.replace(TITLEREPLACE2,' ')
			previewHTML = previewHTML.replace(BODYREPLACE,' ')
			previewHTML = previewHTML.replace(BODYREPLACE2,' ')			
		} else {
			previewHTML = foo.document.documentElement.innerHTML
			previewHTML = previewHTML.replace(HTMLREPLACE,' ')
			previewHTML = previewHTML.replace(HTMLREPLACE2,' ')
			previewHTML = previewHTML.replace(HEADREPLACE,' ')
			previewHTML = previewHTML.replace(HEADREPLACE2,' ')
			previewHTML = previewHTML.replace(TITLEREPLACE,' ')
			previewHTML = previewHTML.replace(TITLEREPLACE2,' ')
			previewHTML = previewHTML.replace(BODYREPLACE,' ')
			previewHTML = previewHTML.replace(BODYREPLACE2,' ')			
		}

		if (editDefault ==1)
		{
			makeEditable()
		}
		
		if (toggleWasOn == "yes") {
			toggleBorders()
			toggleWasOn = "no"
		}

		var leftPos = (screen.availWidth-780) / 2
		var topPos = (screen.availHeight-580) / 2 
 		previewWin = window.open('','','width=795,height=580,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
		var pager
		var pager2
		
		previewWin.document.write("<Base href=" + baseRef + ">")
		
		pager = "<HTML><HEAD><TITLE>New</TITLE></HEAD><BODY TOPMARGIN=0 LEFTMARGIN=0 MARGINWIDTH=0 MARGINHEIGHT=0><table height='100%'><tr><td colspan=2><img src='images/demobanner.gif'></td></tr><tr><td width=125><img src='images/navspacer.gif'><br>NAVIGATION GOES HERE.</td><td width='655' height='90%'>"
		pager2 = "</td></tr><tr><td colspan=2><img src='images/demofooter.gif'></td></tr></table></BODY></HTML>"

		
		previewWin.document.write(pager)
		previewWin.document.write(previewHTML)
		previewWin.document.write(pager2)
		previewWin.document.close()
		
	}

	function ModifyTable() {
		if (isAllowed())
		{

		if (isTableSelected() || isCursorInTableCell()) {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		modifytableWin = window.open('makeedit.asp?ToDo=ModifyTable','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
		}

		}
	}

	function ModifyCell() {
		if (isAllowed())
		{

		if (isCursorInTableCell()) {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		modifytableWin = window.open('makeedit.asp?ToDo=ModifyCell','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
		}

		}
	}

	function modifyForm() {
		if (isAllowed)
		{

		if (isCursorInForm()) {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		modifyFormWin = window.open('makeedit.asp?ToDo=ModifyForm','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
		}

		}
	}

	function insertForm() {
		if (isAllowed())
		{
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		insertFormWin = window.open('makeedit.asp?ToDo=InsertForm','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
		}
	}

	function InsertChars() {
		if (isAllowed())
		{
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		charWin = window.open('makeedit.asp?ToDo=InsertChars','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
		}
	}

	function doAnchor() {
			if (isAllowed())
			{

			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
		
			if ((foo.document.selection.type == "Control") && (foo.document.selection.createRange()(0).tagName == "A") && (foo.document.selection.createRange()(0).href == ""))
			{
				anchorWin = window.open('makeedit.asp?ToDo=ModifyAnchor','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
			} else {
	 			anchorWin = window.open('makeedit.asp?ToDo=InsertAnchor','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
			}

			}
	}

	function isSelection() {
			if ((foo.document.selection.type == "Text") || (foo.document.selection.type == "Control")) {
				return true;
			} else {
				return false;
			}
	}

	function doEmail() {
		if (isAllowed())
		{
			if (isSelection()) { 
				var leftPos = (screen.availWidth-500) / 2
				var topPos = (screen.availHeight-400) / 2 
	 			emailWin = window.open('makeedit.asp?ToDo=InsertEmail','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
			} else
				return
		}
	}

	function doTextField() {
		if (isAllowed())
		{

		var leftPos = (screen.availWidth-500) / 2
		var topPos = (screen.availHeight-400) / 2 

		if (foo.document.selection.type == "Control") {
			var oControlRange = foo.document.selection.createRange();
			if (oControlRange(0).tagName.toUpperCase() == "INPUT") {
				if ((oControlRange(0).type.toUpperCase() == "TEXT") || (oControlRange(0).type.toUpperCase() == "PASSWORD")) {
					selectedTextField = foo.document.selection.createRange()(0);
					textFieldWin = window.open('makeedit.asp?ToDo=ModifyTextField','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
				}
				return true;
			}	
		} else {
			textFieldWin = window.open('makeedit.asp?ToDo=InsertTextField','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
		}

		}
	}

	function doHidden() {
		if (isAllowed())
		{

		var leftPos = (screen.availWidth-500) / 2
		var topPos = (screen.availHeight-400) / 2 

		if (foo.document.selection.type == "Control") {
			var oControlRange = foo.document.selection.createRange();
			if (oControlRange(0).tagName.toUpperCase() == "INPUT") {
				if (oControlRange(0).type.toUpperCase() == "HIDDEN") {
					selectedHidden = foo.document.selection.createRange()(0);
					hiddenWin = window.open('makeedit.asp?ToDo=ModifyHidden','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
				}
				return true;
			}	
		} else {
			hiddenWin = window.open('makeedit.asp?ToDo=InsertHidden','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
		}

		}
	}

	function doTextArea() {
		if (isAllowed())
		{

		var leftPos = (screen.availWidth-500) / 2
		var topPos = (screen.availHeight-400) / 2 

		if (foo.document.selection.type == "Control") {
			var oControlRange = foo.document.selection.createRange();
			if (oControlRange(0).tagName.toUpperCase() == "TEXTAREA") {
					selectedTextArea = foo.document.selection.createRange()(0);
					textFieldWin = window.open('makeedit.asp?ToDo=ModifyTextArea','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
				return true;
			}	
		} else {
			textFieldWin = window.open('makeedit.asp?ToDo=InsertTextArea','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
		}

		}
	}

	function doButton() {
		if (isAllowed())
		{

		var leftPos = (screen.availWidth-500) / 2
		var topPos = (screen.availHeight-400) / 2 

		if (foo.document.selection.type == "Control") {
			var oControlRange = foo.document.selection.createRange();
			if (oControlRange(0).tagName.toUpperCase() == "INPUT") {
				if ((oControlRange(0).type.toUpperCase() == "RESET") || (oControlRange(0).type.toUpperCase() == "SUBMIT") || (oControlRange(0).type.toUpperCase() == "BUTTON")) {
					selectedButton = foo.document.selection.createRange()(0);
					buttonWin = window.open('makeedit.asp?ToDo=ModifyButton','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
				}
				return true;
			}	
		} else {
			buttonWin = window.open('makeedit.asp?ToDo=InsertButton','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
		}

		}
	}

	function doCheckbox() {
		if (isAllowed())
		{

		var leftPos = (screen.availWidth-500) / 2
		var topPos = (screen.availHeight-400) / 2 

		if (foo.document.selection.type == "Control") {
			var oControlRange = foo.document.selection.createRange();
			if (oControlRange(0).tagName.toUpperCase() == "INPUT") {
				if (oControlRange(0).type.toUpperCase() == "CHECKBOX") {
					selectedCheckbox = foo.document.selection.createRange()(0);
					checkboxWin = window.open('makeedit.asp?ToDo=ModifyCheckbox','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
				}
				return true;
			}	
		} else {
			checkboxWin = window.open('makeedit.asp?ToDo=InsertCheckbox','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
		}

		}
	}

	function doRadio() {
		if (isAllowed()) {

		var leftPos = (screen.availWidth-500) / 2
		var topPos = (screen.availHeight-400) / 2 

		if (foo.document.selection.type == "Control") {
			var oControlRange = foo.document.selection.createRange();
			if (oControlRange(0).tagName.toUpperCase() == "INPUT") {
				if (oControlRange(0).type.toUpperCase() == "RADIO") {
					selectedRadio = foo.document.selection.createRange()(0);
					radioWin = window.open('makeedit.asp?ToDo=ModifyRadio','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
				}
				return true;
			}	
		} else {
			radioWin = window.open('makeedit.asp?ToDo=InsertRadio','','width=500,height=400,scrollbars=no,resizable=no,titlebar=0,top=' + topPos + ',left=' + leftPos);
		}

		}
	}

	function button_over(eButton){
		eButton.style.borderBottom = "buttonshadow solid 1px";
		eButton.style.borderLeft = "buttonhighlight solid 1px";
		eButton.style.borderRight = "buttonshadow solid 1px";
		eButton.style.borderTop = "buttonhighlight solid 1px";
	}
				
	function button_out(eButton){
		eButton.style.borderColor = "threedface";
	}

	function button_down(eButton){
		eButton.style.borderBottom = "buttonhighlight solid 1px";
		eButton.style.borderLeft = "buttonshadow solid 1px";
		eButton.style.borderRight = "buttonhighlight solid 1px";
		eButton.style.borderTop = "buttonshadow solid 1px";
	}

	function button_up(eButton){
		eButton.style.borderBottom = "buttonshadow solid 1px";
		eButton.style.borderLeft = "buttonhighlight solid 1px";
		eButton.style.borderRight = "buttonshadow solid 1px";
		eButton.style.borderTop = "buttonhighlight solid 1px";
		eButton = null; 
	}

	function cancelSave() {
		var saveFile = confirm("Are you sure you want to cancel without saving changes?");
		if (saveFile) {
			document.location = "index.asp?module=websection_list"
		}
		closePopups()
	}

	function closePopups() {
		if (imageWin) imageWin.close()
		if (propWin) propWin.close()
		if (inserttableWin) inserttableWin.close()
		if (previewWin) previewWin.close()
		if (modifytableWin) modifytableWin.close()
		if (insertFormWin) insertFormWin.close()
		if (textFieldWin) textFieldWin.close()
		if (hiddenWin) hiddenWin.close()
		if (buttonWin) buttonWin.close()
		if (checkboxWin) checkboxWin.close()
		if (radioWin) radioWin.close()
		if (charWin) charWin.close()
		if (linkWin) linkWin.close()
		if (emailWin) emailWin.close()
		if (anchorWin) anchorWin.close()
	}	
//-->
</script>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
	<td class="body" height="22">
	 <table width="100%" border="0" cellspacing="0" cellpadding="0" class="hide" align="center" id="toolbar_code">
		<tr>
		  <td class="body" height="22">
		  <table border="0" cellspacing="0" cellpadding="1">
			  <tr>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_save.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='SaveHTMLPage()' value="Save" title="Save testtry.htm" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_cancel.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='cancelSave()' value="Cancel" title="Cancel Without Saving" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_mode.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick="SwitchMode()" title="Switch Editing Mode (CODE or WYSIWYG)" class=toolbutton>
				</td>
				<td><img src="/demo/webedit_images/seperator.gif" width="2" height="20"></td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_cut.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='document.execCommand("Cut");foo.focus();' title="Cut (Ctrl+X)" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_copy.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='document.execCommand("Copy");foo.focus();' title="Copy (Ctrl+C)" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_paste.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='document.execCommand("Paste");foo.focus();' title="Paste (Ctrl+V)" class=toolbutton>
				</td>
				<td><img src="images/seperator.gif" width="2" height="20"></td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_undo.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='document.execCommand("Undo");foo.focus();' title="Undo (Ctrl+Z)" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_redo.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='document.execCommand("Redo");foo.focus();' title="Redo (Ctrl+Y)" class=toolbutton>
				</td>
				<td><img src="images/seperator.gif" width="2" height="20"></td>
				<td>
				  <input class=toolbutton onMouseDown=button_down(this); onMouseOver=button_over(this); title="Show Preview" onClick=ShowPreview(1) onMouseOut=button_out(this); type=image width="21" height="20" src="images/button_preview.gif" border=0 unselectable="on">
				</td>
				</tr>
			</table>
		  </td>
		 </tr>
	</table>
	  <table width="100%" border="0" cellspacing="0" cellpadding="0" class="bevel3" align="center" id="toolbar_full">
		<tr>
		  <td class="body" height="22">
			<table border="0" cellspacing="0" cellpadding="1">
			  <tr>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_save.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='SaveHTMLPage()' value="Save" title="Save <%=name%>" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_cancel.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='cancelSave()' value="Cancel" title="Cancel Without Saving" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_mode.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick="SwitchMode()" title="Switch Editing Mode (CODE or WYSIWYG)" class=toolbutton>
				</td>
				<td><img src="images/seperator.gif" width="2" height="20"></td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_cut.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("Cut");foo.focus();' title="Cut (Ctrl+X)" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_copy.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='document.execCommand("Copy");foo.focus();' title="Copy (Ctrl+C)" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_paste.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("Paste");foo.focus();' title="Paste (Ctrl+V)" class=toolbutton>
				</td>
				<td><img src="images/seperator.gif" width="2" height="20"></td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_undo.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='document.execCommand("Undo");foo.focus();' title="Undo (Ctrl+Z)" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_redo.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='document.execCommand("Redo");foo.focus();' title="Redo (Ctrl+Y)" class=toolbutton>
				</td>
				<td><img src="images/seperator.gif" width="2" height="20"></td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_bold.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("Bold");foo.focus();' title="Bold (Ctrl+B)" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_underline.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("Underline");foo.focus();' title="Underline (Ctrl+U)" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_italic.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("Italic");foo.focus();' title="Italic (Ctrl+I)" class=toolbutton>
				</td>
				<td><img src="images/seperator.gif" width="2" height="20"></td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_numbers.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("InsertOrderedList");foo.focus();' title="Insert Number List" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_bullets.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("InsertUnorderedList");foo.focus();' title="Insert Bullet List" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_decrease_indent.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("Outdent");foo.focus();' title="Decrease Indent" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_increase_indent.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("Indent");foo.focus();' title="Increase Indent" class=toolbutton>
				</td>
				<td><img src="images/seperator.gif" width="2" height="20"></td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_align_left.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("JustifyLeft");foo.focus();' title="Align Left" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_align_center.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("JustifyCenter");foo.focus();' title="Align Center" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_align_right.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("JustifyRight");foo.focus();' title="Align Right" class=toolbutton>
				</td>
				<td><img src="images/seperator.gif" width="2" height="20"></td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_hr.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doCommand("InsertHorizontalRule");foo.focus();' title="Insert Horizontal Line" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_link.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doLink()' title="Create / Modify HyperLink (Text or Image must be selected first)" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_anchor.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doAnchor()' title="Insert / Modify Anchor" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_email.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick='doEmail()' title="Create Email Link (Text or Image must be selected first)" class=toolbutton>
				</td>
				<td><img src="images/seperator.gif" width="2" height="20"></td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_image.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick="doImage()" title="Insert / Modify Image" class=toolbutton>
				</td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/imageUpload.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick="doUpload()" title="Upload Image" class=toolbutton>
				</td>
			  </tr>
	  		</table>
		  </td>
		</tr>
		<tr>
		  <td class="body" bgcolor="#000000"><img src="images/1x1.gif" width="1" height="1"></td>
		</tr>
		<tr>
		  <td class="body">
			<table border="0" cellspacing="1" cellpadding="1">
			  <tr>
				<td>
				  <select onChange="(isAllowed()) ? foo.document.execCommand('FontName',false,this[this.selectedIndex].value) :foo.focus();foo.focus();this.selectedIndex=0" class="Text60" unselectable="on">
			  		<option selected>Font</option>
			  		<option value="Times New Roman">Default</option>
			  		<option value="Arial">Arial</option>
			  		<option value="Verdana">Verdana</option>
			  		<option value="Tahoma">Tahoma</option>
			  		<option value="Courier New">Courier New</option>
			  		<option value="Georgia">Georgia</option>
				  </select>
				</td>
				<td>
				  <select onChange="(isAllowed()) ? foo.document.execCommand('FontSize',true,this[this.selectedIndex].value) :foo.focus();foo.focus();this.selectedIndex=0" class=Text45 unselectable="on">
			  		<option SELECTED>Size
			  		<option value="1">1
			  		<option value="2">2
			  		<option value="3">3
			  		<option value="4">4
			  		<option value="5">5
			  		<option value="6">6
			  		<option value="7">7
	  			  </select>
				</td>
				<td>
				  <select onChange="(isAllowed()) ? foo.document.execCommand('ForeColor',false,this[this.selectedIndex].text) :foo.focus();foo.focus();this.selectedIndex=0" class=text50 unselectable="on">
			  		<option SELECTED>Color
			  		<option style="background-color:#FF0000">#FF0000
			  		<option style="background-color:#FFFF00">#FFFF00
			  		<option style="background-color:#00FF00">#00FF00
			  		<option style="background-color:#00FFFF">#00FFFF
			  		<option style="background-color:#0000FF">#0000FF
			  		<option style="background-color:#FF00FF">#FF00FF
			  		<option style="background-color:#FFFFFF">#FFFFFF
			  		<option style="background-color:#F5F5F5">#F5F5F5
			  		<option style="background-color:#DCDCDC">#DCDCDC
			  		<option style="background-color:#D3D3D3">#D3D3D3
			  		<option style="background-color:#C0C0C0">#C0C0C0
			  		<option style="background-color:#A9A9A9">#A9A9A9
			  		<option style="background-color:#808080">#808080
			  		<option style="background-color:#696969">#696969
			  		<option style="background-color:#000000">#000000
			  		<option style="background-color:#2F4F4F">#2F4F4F
			  		<option style="background-color:#708090">#708090
			  		<option style="background-color:#778899">#778899
			  		<option style="background-color:#4682B4">#4682B4
			  		<option style="background-color:#4169E1">#4169E1
			  		<option style="background-color:#6495ED">#6495ED
			  		<option style="background-color:#B0C4DE">#B0C4DE
			  		<option style="background-color:#7B68EE">#7B68EE
			  		<option style="background-color:#6A5ACD">#6A5ACD
			  		<option style="background-color:#483D8B">#483D8B
			  		<option style="background-color:#191970">#191970
			  		<option style="background-color:#000080">#000080
			  		<option style="background-color:#00008B">#00008B
			  		<option style="background-color:#0000CD">#0000CD
			  		<option style="background-color:#1E90FF">#1E90FF
			  		<option style="background-color:#00BFFF">#00BFFF
			  		<option style="background-color:#87CEFA">#87CEFA
			  		<option style="background-color:#87CEEB">#87CEEB
			  		<option style="background-color:#ADD8E6">#ADD8E6
			  		<option style="background-color:#B0E0E6">#B0E0E6
			  		<option style="background-color:#F0FFFF">#F0FFFF
			  		<option style="background-color:#E0FFFF">#E0FFFF
			  		<option style="background-color:#AFEEEE">#AFEEEE
			  		<option style="background-color:#00CED1">#00CED1
			  		<option style="background-color:#5F9EA0">#5F9EA0
			  		<option style="background-color:#48D1CC">#48D1CC
			  		<option style="background-color:#00FFFF">#00FFFF
			  		<option style="background-color:#40E0D0">#40E0D0
			  		<option style="background-color:#20B2AA">#20B2AA
			  		<option style="background-color:#008B8B">#008B8B
			  		<option style="background-color:#008080">#008080
			  		<option style="background-color:#7FFFD4">#7FFFD4
			  		<option style="background-color:#66CDAA">#66CDAA
			  		<option style="background-color:#8FBC8F">#8FBC8F
			  		<option style="background-color:#3CB371">#3CB371
			  		<option style="background-color:#2E8B57">#2E8B57
			  		<option style="background-color:#006400">#006400
			  		<option style="background-color:#008000">#008000
			  		<option style="background-color:#228B22">#228B22
			  		<option style="background-color:#32CD32">#32CD32
			  		<option style="background-color:#00FF00">#00FF00
			  		<option style="background-color:#7FFF00">#7FFF00
			  		<option style="background-color:#7CFC00">#7CFC00
			  		<option style="background-color:#ADFF2F">#ADFF2F
			  		<option style="background-color:#98FB98">#98FB98
			  		<option style="background-color:#90EE90">#90EE90
			  		<option style="background-color:#00FF7F">#00FF7F
			  		<option style="background-color:#00FA9A">#00FA9A
			  		<option style="background-color:#556B2F">#556B2F
			  		<option style="background-color:#6B8E23">#6B8E23
			  		<option style="background-color:#808000">#808000
			  		<option style="background-color:#BDB76B">#BDB76B
			  		<option style="background-color:#B8860B">#B8860B
			  		<option style="background-color:#DAA520">#DAA520
			  		<option style="background-color:#FFD700">#FFD700
			  		<option style="background-color:#F0E68C">#F0E68C
			  		<option style="background-color:#EEE8AA">#EEE8AA
			  		<option style="background-color:#FFEBCD">#FFEBCD
			  		<option style="background-color:#FFE4B5">#FFE4B5
			  		<option style="background-color:#F5DEB3">#F5DEB3
			  		<option style="background-color:#FFDEAD">#FFDEAD
			  		<option style="background-color:#DEB887">#DEB887
			  		<option style="background-color:#D2B48C">#D2B48C
			  		<option style="background-color:#BC8F8F">#BC8F8F
			  		<option style="background-color:#A0522D">#A0522D
			  		<option style="background-color:#8B4513">#8B4513
			  		<option style="background-color:#D2691E">#D2691E
			  		<option style="background-color:#CD853F">#CD853F
			  		<option style="background-color:#F4A460">#F4A460
			  		<option style="background-color:#8B0000">#8B0000
			  		<option style="background-color:#800000">#800000
			  		<option style="background-color:#A52A2A">#A52A2A
			  		<option style="background-color:#B22222">#B22222
			  		<option style="background-color:#CD5C5C">#CD5C5C
			  		<option style="background-color:#F08080">#F08080
			  		<option style="background-color:#FA8072">#FA8072
			  		<option style="background-color:#E9967A">#E9967A
			  		<option style="background-color:#FFA07A">#FFA07A
			  		<option style="background-color:#FF7F50">#FF7F50
			  		<option style="background-color:#FF6347">#FF6347
			  		<option style="background-color:#FF8C00">#FF8C00
			  		<option style="background-color:#FFA500">#FFA500
			  		<option style="background-color:#FF4500">#FF4500
			  		<option style="background-color:#DC143C">#DC143C
			  		<option style="background-color:#FF0000">#FF0000
			  		<option style="background-color:#FF1493">#FF1493
			  		<option style="background-color:#FF00FF">#FF00FF
			  		<option style="background-color:#FF69B4">#FF69B4
			  		<option style="background-color:#FFB6C1">#FFB6C1
			  		<option style="background-color:#FFC0CB">#FFC0CB
			  		<option style="background-color:#DB7093">#DB7093
			  		<option style="background-color:#C71585">#C71585
			  		<option style="background-color:#800080">#800080
			  		<option style="background-color:#8B008B">#8B008B
			  		<option style="background-color:#9370DB">#9370DB
			  		<option style="background-color:#8A2BE2">#8A2BE2
			  		<option style="background-color:#4B0082">#4B0082
			  		<option style="background-color:#9400D3">#9400D3
			  		<option style="background-color:#9932CC">#9932CC
			  		<option style="background-color:#BA55D3">#BA55D3
			  		<option style="background-color:#DA70D6">#DA70D6
			  		<option style="background-color:#EE82EE">#EE82EE
			  		<option style="background-color:#DDA0DD">#DDA0DD
			  		<option style="background-color:#D8BFD8">#D8BFD8
			  		<option style="background-color:#E6E6FA">#E6E6FA
			  		<option style="background-color:#F8F8FF">#F8F8FF
			  		<option style="background-color:#F0F8FF">#F0F8FF
			  		<option style="background-color:#F5FFFA">#F5FFFA
			  		<option style="background-color:#F0FFF0">#F0FFF0
			  		<option style="background-color:#FAFAD2">#FAFAD2
			  		<option style="background-color:#FFFACD">#FFFACD
			  		<option style="background-color:#FFF8DC">#FFF8DC
			  		<option style="background-color:#FFFFE0">#FFFFE0
			  		<option style="background-color:#FFFFF0">#FFFFF0
			  		<option style="background-color:#FFFAF0">#FFFAF0
			  		<option style="background-color:#FAF0E6">#FAF0E6
			  		<option style="background-color:#FDF5E6">#FDF5E6
			  		<option style="background-color:#FAEBD7">#FAEBD7
			  		<option style="background-color:#FFE4C4">#FFE4C4 
			  		<option style="background-color:#FFDAB9">#FFDAB9
			  		<option style="background-color:#FFEFD5">#FFEFD5
			  		<option style="background-color:#FFF5EE">#FFF5EE
			  		<option style="background-color:#FFF0F5">#FFF0F5
			  		<option style="background-color:#FFE4E1">#FFE4E1
			  		<option style="background-color:#FFFAFA">#FFFAFA 
	  			  </select>
				</td>
				<td>
				  <select onChange="(isAllowed()) ? doFormat(this[this.selectedIndex].value) : foo.focus();foo.focus();this.selectedIndex=0" class="Text60" unselectable="on">
				    <option selected>Format
				    <option value="<P>">Normal
					<option value="SuperScript">SuperScript
					<option value="SubScript">SubScript
				    <option value="<H1>">H1
				    <option value="<H2>">H2
				    <option value="<H3>">H3
				    <option value="<H4>">H4
				    <option value="<H5>">H5
				    <option value="<H6>">H6
				  </select>
				</td>
				<td>
				  <select id=sStyles onChange="applyStyle(this[this.selectedIndex].value);foo.focus();this.selectedIndex=0;" class="Text90" unselectable="on">
				    <option selected>Style</option>
				    <option value="">None</option>
				  </select>
				</td>
				<td><img src="images/seperator.gif" width="2" height="20"></td>
				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_table.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick="ShowInsertTable()" title="Insert Table" class=toolbutton>
				</td>
				<td>
				  <input class=toolbutton onmousedown=button_down(this); onmouseover=button_over(this); title="Modify Table Properties" onClick=ModifyTable() onmouseout=button_out(this); type=image width="21" height="20" src="images/button_modify_table.gif" border=0 unselectable="on">
				</td>
				<td>
				  <input class=toolbutton onmousedown=button_down(this); onmouseover=button_over(this); title="Modify Cell Properties" onClick=ModifyCell() onmouseout=button_out(this); type=image width="21" height="20" src="images/button_modify_cell.gif" border=0 unselectable="on">
				</td>
				<td><img src="images/seperator.gif" width="2" height="20"></td>
				<td>	
				  <input class=toolbutton onmousedown=button_down(this); onmouseover=button_over(this); title="Insert Row Above" onClick=InsertRowAbove() onmouseout=button_out(this); type=image width="21" height="20" src="images/button_insert_row_above.gif" border=0 unselectable="on">
				</td>
				<td>
					
				  <input class=toolbutton onmousedown=button_down(this); onmouseover=button_over(this); title="Insert Row Below" onClick=InsertRowBelow() onmouseout=button_out(this); type=image width="21" height="20" src="images/button_insert_row_below.gif" border=0 unselectable="on">
				</td>
				<td>
					
				  <input class=toolbutton onmousedown=button_down(this); onmouseover=button_over(this); title="Delete Row" onClick=DeleteRow() onmouseout=button_out(this); type=image width="21" height="20" src="images/button_delete_row.gif" border=0 unselectable="on">
				</td>
				<td>
					
				  <input class=toolbutton onmousedown=button_down(this); onmouseover=button_over(this); title="Insert Column After" onClick=InsertColAfter() onmouseout=button_out(this); type=image width="21" height="20" src="images/button_insert_col_after.gif" border=0 unselectable="on">
				</td>
				<td>
					
				  <input class=toolbutton onmousedown=button_down(this); onmouseover=button_over(this); title="Insert Column Before" onClick=InsertColBefore() onmouseout=button_out(this); type=image width="21" height="20" src="images/button_insert_col_before.gif" border=0 unselectable="on">
				</td>
				<td>
					
				  <input class=toolbutton onmousedown=button_down(this); onmouseover=button_over(this); title="Delete Column" onClick=DeleteCol() onmouseout=button_out(this); type=image width="21" height="20" src="images/button_delete_col.gif" border=0 unselectable="on">
				</td>
				<td>
					
				  <input class=toolbutton onmousedown=button_down(this); onmouseover=button_over(this); title="Increase Column Span" onClick=IncreaseColspan() onmouseout=button_out(this); type=image width="21" height="20" src="images/button_increase_colspan.gif" border=0 unselectable="on">
				</td>
				<td>
					
				  <input class=toolbutton onmousedown=button_down(this); onmouseover=button_over(this); title="Decrease Column Span" onClick=DecreaseColspan() onmouseout=button_out(this); type=image width="21" height="20" src="images/button_decrease_colspan.gif" border=0 unselectable="on">
				</td>
				<td><img src="images/seperator.gif" width="2" height="20"></td>

				<td>
				  <input unselectable="on" type="image" border="0" src="images/button_chars.gif" width="21" height="20" onmouseover="button_over(this);" onmouseout="button_out(this);" onmousedown="button_down(this);" onClick="InsertChars()" title="Insert Special Characters" class=toolbutton>
				</td>
	
				<td>				
				  <input class=toolbutton onmousedown=button_down(this); onmouseover=button_over(this); title="Clean HTML Code" onClick=cleanCode() onmouseout=button_out(this); type=image width="21" height="20" src="images/button_clean_code.gif" border=0 unselectable="on">
				</td>
				<td>
				  <input class=toolbutton onMouseDown=button_down(this); onMouseOver=button_over(this); title="Show / Hide Guidelines" onClick=toggleBorders() onMouseOut=button_out(this); type=image width="21" height="20" src="images/button_show_borders.gif" border=0 unselectable="on">
				</td>
				<td>
				  <input class=toolbutton onMouseDown=button_down(this); onMouseOver=button_over(this); title="Show Preview" onClick=ShowPreview() onMouseOut=button_out(this); type=image width="21" height="20" src="images/button_preview.gif" border=0 unselectable="on">
				</td>

				<td>
				<form method="post" action="richtemplate_save.asp" name="EditorForm" enctype="multipart/form-data">
  <input type="hidden" name="id" value="<%=id%>"><input type="hidden" name="Page"
  value="   ">


<input type="hidden" name="ndate" size="10" value="<%=NDate%>">
<input type="hidden" name="title" value="My Web Page" size="56">
 					<INPUT TYPE="hidden" NAME="EditorHTML">
					<INPUT TYPE="hidden" name="ToDo" value="SavePage">
					<input type="hidden" name="newdir" value="/richtemplate/content">
					<INPUT TYPE="hidden" name="FileName" value="<%=name%>">
					<INPUT TYPE="hidden" name="FileExt" value="txt">
					<INPUT TYPE="hidden" NAME="pagename" value="<%=name%>">
					<INPUT TYPE="hidden" NAME="sectionid" value="<%=sectionid%>">
					<INPUT TYPE="hidden" NAME="defaultpage" value="<%=defaultpage%>">
  </form>
				
				
				 <!-- <FORM name="EditorForm" METHOD=POST>
					<INPUT TYPE="hidden" NAME="EditorHTML">
					<INPUT TYPE="hidden" name="ToDo" value="SavePage">
					<input type="hidden" name="newdir" value="/richtemplate/content">
					<INPUT TYPE="hidden" name="FileName" value="stuff.html">
					<INPUT TYPE="hidden" name="FileExt" value="txt">
				  </FORM>-->
				</td>
			  </tr>
			</table>
		  </td>
		</tr>
		<tr>
		  <td class="body" bgcolor="#000000"><img src="images/1x1.gif" width="1" height="1"></td>
		</tr>
	  </table>
	</td>
  </tr> 
</table>
<IFRAME id="foo" src="richtemplate_template.asp?id=<%=id%>" style="HEIGHT: 70%; WIDTH: 100%" onLoad=fooLoad();></iframe>



</td></tr></table>














</body>
</html>





























































