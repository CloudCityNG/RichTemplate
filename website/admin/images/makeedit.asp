 <%
'-----------------------------ADD LINK----------------------->


if REQUEST.QUERYSTRING("ToDo")="InsertLink" then%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richTemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richTemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
 
	<a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	

</table>
<script language=javascript>

var fileWin
var prvTr
var prvTr2

// window.onload = this.focus
window.onerror = stopError

	function stopError() {
		return true;
	}

	function grey(tr) {
		prvTr2 = tr
		tr.className = 'b4';
		if ((prvTr != null) && (prvTr != tr))
			prvTr.className = '';
		prvTr = prvTr2
	}

	function ConfirmDelete(filename) {
		var deleteFile = confirm("Are you sure you wish to delete?");
		if (deleteFile == true) {
			document.location = "makeedit.asp?newdir=&ToDo=Delete&FileName=" + filename
		}
	}

	function ConfirmDeleteFolder(filename) {
		var deleteFile = confirm("Are you sure you wish to delete this folder and ALL its contents?");

		if (deleteFile == true) {
			document.location = "makeedit.asp?newdir=&isFolder=1&ToDo=Delete&FileName=" + filename
		}
	}

	function ConfirmImageDeleteFolder(filename) {
		var deleteFile = confirm("Are you sure you wish to delete this folder and ALL its contents?");
		if (deleteFile == true) {
			document.location = " makeedit.asp?newimagedir=&isFolder=1&ToDo=Delete&FileName=" + filename + "&FromImageDir=1"
		}
	}

	function ConfirmImageDelete(filename) {
		var deleteFile = confirm("Are you sure you wish to delete?");
		if (deleteFile == true) {
			document.location = "makeedit.asp?newimagedir=&ToDo=Delete&FileName=" + filename + "&FromImageDir=1"
		}
	}

	function SelectImage(ImageName) {
		window.opener.selectImage("images/" + ImageName);
		self.close();
	}

	function SetBackgd(ImageName) {
		var setBg = confirm("Are you sure you wish to set this image as the page background image?");
		if (setBg == true) {
			window.opener.setBackgd("content/../images" + ImageName);
			self.close();
		}
	}

	function getLink() {

		if (window.opener.foo.document.selection.type == "Control") {
			var oControlRange = window.opener.foo.document.selection.createRange();
			if (oControlRange(0).tagName.toUpperCase() == "IMG") {
				var oSel = oControlRange(0).parentNode;
			}
		} else {
			oSel = window.opener.foo.document.selection.createRange().parentElement();
		}

		if (oSel.tagName.toUpperCase() == "A")
		{
			document.linkForm.targetWindow.value = oSel.target
			document.linkForm.link.value = oSel.href
		}
	}

	function InsertLink() {
		targetWindow = document.linkForm.targetWindow.value;
		var linkSource = document.linkForm.link.value

		if (linkSource != "")
		{
			var oNewLink = window.opener.foo.document.createElement("<A>");
			oNewSelection = window.opener.foo.document.selection.createRange()

			if (window.opener.foo.document.selection.type == "Control")
			{
				selectedImage = window.opener.foo.document.selection.createRange()(0);
				selectedImage.width = selectedImage.width
				selectedImage.height = selectedImage.height
			}

			oNewSelection.execCommand("CreateLink",false,linkSource);

			if (window.opener.foo.document.selection.type == "Control")
			{
				oLink = oNewSelection(0).parentNode;
			} else
				oLink = oNewSelection.parentElement()

			if (targetWindow != "")
			{
				oLink.target = targetWindow;
			} else
				oLink.removeAttribute("target")

			window.opener.foo.focus();
			self.close();
		} else {
			alert("URL cannot be left blank")
			document.linkForm.link.focus()
		}
	}

	function CreateLink(LinkSource) {
		document.linkForm.link.value = LinkSource;
		document.linkForm.link.focus()
	}

	function RemoveLink() {
		if (window.opener.foo.document.selection.type == "Control")
		{
			selectedImage = window.opener.foo.document.selection.createRange()(0);
			selectedImage.width = selectedImage.width
			selectedImage.height = selectedImage.height
		}

		window.opener.foo.document.execCommand("Unlink");
		window.opener.foo.focus();
		self.close();
	}

	function getAnchors() {
		var allLinks = window.opener.foo.document.body.getElementsByTagName("A");
		for (a=0; a < allLinks.length; a++) {
				if (allLinks[a].href.toUpperCase() == "") {
					document.write("<option value=#" + allLinks[a].name + ">" + allLinks[a].name + "</option>")
				}
		}
	}

	function ViewFile(fileName) {
		
		if (fileWin) { fileWin.close() }

		var leftPos = (screen.availWidth-700) / 2
		var topPos = (screen.availHeight-500) / 2 
	 	fileWin = window.open(fileName,'fileWindow','width=700,height=500,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
		fileWin.focus()
		fileWin.location.reload(true);
	}

</script>

	<FORM METHOD=POST name=linkForm>
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
	  <td width="15"> </td>
	  <td class="heading1">Link Manager</td>
	</tr>
	<tr>
	  <td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Insert Link&quot; to insert a link into your webpage.<br>
		Alternatively, locate  the file from the file manager below and select &quot;Get Link Location&quot;. Click &quot;Insert Link&quot; to insert the link.<br>
		Click the &quot;Cancel&quot; Button to close this window.</td>
	</tr>
	<tr>
	  <td>&nbsp;</td>
	  <td class="body">&nbsp;</td>
	</tr>
	<tr>
	  <td>&nbsp;</td>
	  <td class="body">
		<table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
		  <tr>
			<td>&nbsp;&nbsp;Link Information</td>
		  </tr>
		</table>
	  </td>
	</tr>
	<tr>
	  <td colspan="2" height="10"> </td>
	</tr>
	<tr>
	  <td>&nbsp;</td>
	  <td class="body">
		<table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
	  	  
		  <tr>
			<td class=body width="100">URL:</td>
			<td class=body>
			  <input type="text" name="link" value="" class="Text220">
			</td>
		  </tr>
		  <tr>
			<td class=body>Target Window:</td>
			<td class=body>
			  <input type="text" name="targetWindow" value="" class="Text90">
			  <select name="targetText" class="Text90" onChange="targetWindow.value = targetText[targetText.selectedIndex].value; targetText.value = ''; targetWindow.focus()">
			  <option value=""></option>
	  		  <option value="">None</option>
			  <option value=_blank>_blank</option>
			  <option value=_parent>_parent</option>
  			  <option value=_self>_self</option>
   			  <option value=_top>_top</option>
			  </select></td>
			</td>
		  </tr>
		  <tr>
		  <td class=body>Anchor:</td>
		  <td class=body>
			  <select name="targetAnchor" class="Text90" onChange="link.value = window.opener.fooURL + targetAnchor[targetAnchor.selectedIndex].value; targetAnchor.value = ''; link.focus()">
				<option value=""></option>
				<script>getAnchors()</script>
			  </select></td>
		  </tr>
		</table>
	  </td>
	</tr>
	<tr>
	  <td>&nbsp;</td>
	  <td class="body">


	  </td>
	</tr>
	<tr>
	  <td colspan="2"> </td>
	</tr>
	<tr>
	  <td>&nbsp;</td>
	  <td class="body">
	
	  </td>
	</tr>
	<tr>
		
	  <td colspan="2"> </td>
	</tr>
	<tr>
		
	  <td>&nbsp;</td>
	  <td>
		<input type="button" name="insertLink" value="Insert Link" class="Text90" onClick="javascript:InsertLink();">
		<input type="button" name="removeLink" value="Remove Link" class="Text90" onClick="javascript:RemoveLink();">
		<input type=button name="Cancel" value="Cancel" class="Text70" onClick="javascript:window.close()">
		<input type=hidden name=newdir value="">
		</td>
	</tr>
	
	  </table>
	</form>
	<script>getLink()</script>
	<br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"> </td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
'-----------------------------ADD TABLE----------------------->
ELSEIF REQUEST.QUERYSTRING("ToDo")="InsertTable" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<HEAD>
<TITLE>RichTemplate</TITLE>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
	
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=JavaScript>
window.onload = this.focus

function InsertTable() {
	error = 0
	var sel = window.opener.foo.document.selection;
	if (sel!=null) {
		var rng = sel.createRange();
	   	if (rng!=null) {
			border = document.tableForm.border.value
			columns = document.tableForm.columns.value
			padding = document.tableForm.padding.value
			rows = document.tableForm.rows.value
			spacing = document.tableForm.spacing.value
			width = document.tableForm.width.value
			bgcolor = document.tableForm.bgcolor[tableForm.bgcolor.selectedIndex].text

			if (isNaN(rows) || rows < 0 || rows == "") {
			 	alert("Rows must contain a valid, positive number")
				document.tableForm.rows.select()
				document.tableForm.rows.focus()
				error = 1
			} else if (isNaN(columns) || columns < 0 || columns == "") {
			 	alert("Columns must contain a valid, positive number")
				document.tableForm.columns.select()
				document.tableForm.columns.focus()
				error = 1
			} else if (width < 0 || width == "") {
			 	alert("Width must contain a valid, positive number")
				document.tableForm.width.select()
				document.tableForm.width.focus()
				error = 1
			} else if (isNaN(padding) || padding < 0 || padding == "") {
			 	alert("Cell Padding must contain a valid, positive number")
				document.tableForm.padding.select()
				document.tableForm.padding.focus()
				error = 1
			} else if (isNaN(spacing) || spacing < 0 || spacing == "") {
			 	alert("Cell Spacing must contain a valid, positive number")
				document.tableForm.spacing.select()
				document.tableForm.spacing.focus()
				error = 1
			} else if (isNaN(border) || border < 0 || border == "") {
			 	alert("Border must contain a valid, positive number")
				document.tableForm.border.select()
				document.tableForm.border.focus()
				error = 1
			}
			

        		if (error != 1) {
				if (bgcolor != "None") {
					bgcolor = " bgcolor = " + bgcolor
				} else {
					bgcolor = ""
				}
				
				if (window.opener.borderShown == "yes") {
					style = ' style="BORDER-RIGHT:1px dotted #BFBFBF; BORDER-TOP:1px dotted #BFBFBF; BORDER-LEFT:1px dotted #BFBFBF; BORDER-BOTTOM:1px dotted #BFBFBF;"'
				} else {
					style = ""
				}
					
        			HTMLTable = "<Table width=" + width + " border=" + border + " cellpadding=" + padding + " cellspacing=" + spacing + bgcolor + style + ">"
        
        			for (i=0; i<rows; i++) {
        				HTMLTable = HTMLTable + "<tr>"
        				for (j=0; j<columns; j++) {
        					HTMLTable = HTMLTable + "<td " + style + ">&nbsp</td>"
        				}
        			
        				HTMLTable = HTMLTable + "</tr>"
        			}
        			
        			HTMLTable = HTMLTable + "</table>"
					window.opener.foo.focus();
        			rng.pasteHTML(HTMLTable)
        		}
		}
	
	}
	
	if (error != 1) {
		self.close();
	}
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				InsertTable()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>
<form name=tableForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Insert Table</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Insert Table&quot; to insert a table into your webpage.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Insert Table into Webpage</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"> </td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
	  <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		
		
		<tr>
		  <td class="body" width="80">Rows:
			</td>
		  <td class="body" width="200">
			<input type="text" name="rows" size="2" class="Text50" value="1" maxlength="2">
		  </td>
		  <td class="body" width="80">Cell Padding:</td>
		  <td class="body">
			<input type="text" name="padding" size="2" class="Text50" value="2" maxlength="2">
		  </td>
		</tr>
		<tr>
		  <td class="body" width="80">
			Columns:</td>
		  <td class="body" width="200">
			<input type="text" name="columns" size="2" class="Text50" value="1" maxlength="2">
		  </td>
		  <td class="body" width="80">Cell Spacing:</td>
		  <td class="body">
			<input type="text" name="spacing" size="2" class="Text50" maxlength="2" value="2">
		  </td>
		</tr>
		<tr>
		  <td class="body" width="80">Width:</td>
		  <td class="body" width="200">
			<input type="text" name="width" size="2" class="Text50" maxlength="4" value="100%">
		  </td>
		  <td class="body" width="80">Border:</td>
		  <td class="body">
			<input type="text" name="border" size="2" class="Text50" maxlength="2" value="1">
		  </td>
		</tr>
		<tr>
		  <td class="body" width="80">BgColor:</td>
		  <td class="body" width="200">
		<SELECT class=text70 name=bgcolor> 
		<OPTION selected>None
		<OPTION style="BACKGROUND-COLOR: #ff0000">#FF0000<OPTION 
                    style="BACKGROUND-COLOR: #ffff00">#FFFF00<OPTION 
                    style="BACKGROUND-COLOR: #00ff00">#00FF00<OPTION 
                    style="BACKGROUND-COLOR: #00ffff">#00FFFF<OPTION 
                    style="BACKGROUND-COLOR: #0000ff">#0000FF<OPTION 
                    style="BACKGROUND-COLOR: #ff00ff">#FF00FF<OPTION 
                    style="BACKGROUND-COLOR: #ffffff">#FFFFFF<OPTION 
                    style="BACKGROUND-COLOR: #f5f5f5">#F5F5F5<OPTION 
                    style="BACKGROUND-COLOR: #dcdcdc">#DCDCDC<OPTION 
                    style="BACKGROUND-COLOR: #d3d3d3">#D3D3D3<OPTION 
                    style="BACKGROUND-COLOR: #c0c0c0">#C0C0C0<OPTION 
                    style="BACKGROUND-COLOR: #a9a9a9">#A9A9A9<OPTION 
                    style="BACKGROUND-COLOR: #808080">#808080<OPTION 
                    style="BACKGROUND-COLOR: #696969">#696969<OPTION 
                    style="BACKGROUND-COLOR: #000000">#000000<OPTION 
                    style="BACKGROUND-COLOR: #2f4f4f">#2F4F4F<OPTION 
                    style="BACKGROUND-COLOR: #708090">#708090<OPTION 
                    style="BACKGROUND-COLOR: #778899">#778899<OPTION 
                    style="BACKGROUND-COLOR: #4682b4">#4682B4<OPTION 
                    style="BACKGROUND-COLOR: #4169e1">#4169E1<OPTION 
                    style="BACKGROUND-COLOR: #6495ed">#6495ED<OPTION 
                    style="BACKGROUND-COLOR: #b0c4de">#B0C4DE<OPTION 
                    style="BACKGROUND-COLOR: #7b68ee">#7B68EE<OPTION 
                    style="BACKGROUND-COLOR: #6a5acd">#6A5ACD<OPTION 
                    style="BACKGROUND-COLOR: #483d8b">#483D8B<OPTION 
                    style="BACKGROUND-COLOR: #191970">#191970<OPTION 
                    style="BACKGROUND-COLOR: #000080">#000080<OPTION 
                    style="BACKGROUND-COLOR: #00008b">#00008B<OPTION 
                    style="BACKGROUND-COLOR: #0000cd">#0000CD<OPTION 
                    style="BACKGROUND-COLOR: #1e90ff">#1E90FF<OPTION 
                    style="BACKGROUND-COLOR: #00bfff">#00BFFF<OPTION 
                    style="BACKGROUND-COLOR: #87cefa">#87CEFA<OPTION 
                    style="BACKGROUND-COLOR: #87ceeb">#87CEEB<OPTION 
                    style="BACKGROUND-COLOR: #add8e6">#ADD8E6<OPTION 
                    style="BACKGROUND-COLOR: #b0e0e6">#B0E0E6<OPTION 
                    style="BACKGROUND-COLOR: #f0ffff">#F0FFFF<OPTION 
                    style="BACKGROUND-COLOR: #e0ffff">#E0FFFF<OPTION 
                    style="BACKGROUND-COLOR: #afeeee">#AFEEEE<OPTION 
                    style="BACKGROUND-COLOR: #00ced1">#00CED1<OPTION 
                    style="BACKGROUND-COLOR: #5f9ea0">#5F9EA0<OPTION 
                    style="BACKGROUND-COLOR: #48d1cc">#48D1CC<OPTION 
                    style="BACKGROUND-COLOR: #00ffff">#00FFFF<OPTION 
                    style="BACKGROUND-COLOR: #40e0d0">#40E0D0<OPTION 
                    style="BACKGROUND-COLOR: #20b2aa">#20B2AA<OPTION 
                    style="BACKGROUND-COLOR: #008b8b">#008B8B<OPTION 
                    style="BACKGROUND-COLOR: #008080">#008080<OPTION 
                    style="BACKGROUND-COLOR: #7fffd4">#7FFFD4<OPTION 
                    style="BACKGROUND-COLOR: #66cdaa">#66CDAA<OPTION 
                    style="BACKGROUND-COLOR: #8fbc8f">#8FBC8F<OPTION 
                    style="BACKGROUND-COLOR: #3cb371">#3CB371<OPTION 
                    style="BACKGROUND-COLOR: #2e8b57">#2E8B57<OPTION 
                    style="BACKGROUND-COLOR: #006400">#006400<OPTION 
                    style="BACKGROUND-COLOR: #008000">#008000<OPTION 
                    style="BACKGROUND-COLOR: #228b22">#228B22<OPTION 
                    style="BACKGROUND-COLOR: #32cd32">#32CD32<OPTION 
                    style="BACKGROUND-COLOR: #00ff00">#00FF00<OPTION 
                    style="BACKGROUND-COLOR: #7fff00">#7FFF00<OPTION 
                    style="BACKGROUND-COLOR: #7cfc00">#7CFC00<OPTION 
                    style="BACKGROUND-COLOR: #adff2f">#ADFF2F<OPTION 
                    style="BACKGROUND-COLOR: #98fb98">#98FB98<OPTION 
                    style="BACKGROUND-COLOR: #90ee90">#90EE90<OPTION 
                    style="BACKGROUND-COLOR: #00ff7f">#00FF7F<OPTION 
                    style="BACKGROUND-COLOR: #00fa9a">#00FA9A<OPTION 
                    style="BACKGROUND-COLOR: #556b2f">#556B2F<OPTION 
                    style="BACKGROUND-COLOR: #6b8e23">#6B8E23<OPTION 
                    style="BACKGROUND-COLOR: #808000">#808000<OPTION 
                    style="BACKGROUND-COLOR: #bdb76b">#BDB76B<OPTION 
                    style="BACKGROUND-COLOR: #b8860b">#B8860B<OPTION 
                    style="BACKGROUND-COLOR: #daa520">#DAA520<OPTION 
                    style="BACKGROUND-COLOR: #ffd700">#FFD700<OPTION 
                    style="BACKGROUND-COLOR: #f0e68c">#F0E68C<OPTION 
                    style="BACKGROUND-COLOR: #eee8aa">#EEE8AA<OPTION 
                    style="BACKGROUND-COLOR: #ffebcd">#FFEBCD<OPTION 
                    style="BACKGROUND-COLOR: #ffe4b5">#FFE4B5<OPTION 
                    style="BACKGROUND-COLOR: #f5deb3">#F5DEB3<OPTION 
                    style="BACKGROUND-COLOR: #ffdead">#FFDEAD<OPTION 
                    style="BACKGROUND-COLOR: #deb887">#DEB887<OPTION 
                    style="BACKGROUND-COLOR: #d2b48c">#D2B48C<OPTION 
                    style="BACKGROUND-COLOR: #bc8f8f">#BC8F8F<OPTION 
                    style="BACKGROUND-COLOR: #a0522d">#A0522D<OPTION 
                    style="BACKGROUND-COLOR: #8b4513">#8B4513<OPTION 
                    style="BACKGROUND-COLOR: #d2691e">#D2691E<OPTION 
                    style="BACKGROUND-COLOR: #cd853f">#CD853F<OPTION 
                    style="BACKGROUND-COLOR: #f4a460">#F4A460<OPTION 
                    style="BACKGROUND-COLOR: #8b0000">#8B0000<OPTION 
                    style="BACKGROUND-COLOR: #800000">#800000<OPTION 
                    style="BACKGROUND-COLOR: #a52a2a">#A52A2A<OPTION 
                    style="BACKGROUND-COLOR: #b22222">#B22222<OPTION 
                    style="BACKGROUND-COLOR: #cd5c5c">#CD5C5C<OPTION 
                    style="BACKGROUND-COLOR: #f08080">#F08080<OPTION 
                    style="BACKGROUND-COLOR: #fa8072">#FA8072<OPTION 
                    style="BACKGROUND-COLOR: #e9967a">#E9967A<OPTION 
                    style="BACKGROUND-COLOR: #ffa07a">#FFA07A<OPTION 
                    style="BACKGROUND-COLOR: #ff7f50">#FF7F50<OPTION 
                    style="BACKGROUND-COLOR: #ff6347">#FF6347<OPTION 
                    style="BACKGROUND-COLOR: #ff8c00">#FF8C00<OPTION 
                    style="BACKGROUND-COLOR: #ffa500">#FFA500<OPTION 
                    style="BACKGROUND-COLOR: #ff4500">#FF4500<OPTION 
                    style="BACKGROUND-COLOR: #dc143c">#DC143C<OPTION 
                    style="BACKGROUND-COLOR: #ff0000">#FF0000<OPTION 
                    style="BACKGROUND-COLOR: #ff1493">#FF1493<OPTION 
                    style="BACKGROUND-COLOR: #ff00ff">#FF00FF<OPTION 
                    style="BACKGROUND-COLOR: #ff69b4">#FF69B4<OPTION 
                    style="BACKGROUND-COLOR: #ffb6c1">#FFB6C1<OPTION 
                    style="BACKGROUND-COLOR: #ffc0cb">#FFC0CB<OPTION 
                    style="BACKGROUND-COLOR: #db7093">#DB7093<OPTION 
                    style="BACKGROUND-COLOR: #c71585">#C71585<OPTION 
                    style="BACKGROUND-COLOR: #800080">#800080<OPTION 
                    style="BACKGROUND-COLOR: #8b008b">#8B008B<OPTION 
                    style="BACKGROUND-COLOR: #9370db">#9370DB<OPTION 
                    style="BACKGROUND-COLOR: #8a2be2">#8A2BE2<OPTION 
                    style="BACKGROUND-COLOR: #4b0082">#4B0082<OPTION 
                    style="BACKGROUND-COLOR: #9400d3">#9400D3<OPTION 
                    style="BACKGROUND-COLOR: #9932cc">#9932CC<OPTION 
                    style="BACKGROUND-COLOR: #ba55d3">#BA55D3<OPTION 
                    style="BACKGROUND-COLOR: #da70d6">#DA70D6<OPTION 
                    style="BACKGROUND-COLOR: #ee82ee">#EE82EE<OPTION 
                    style="BACKGROUND-COLOR: #dda0dd">#DDA0DD<OPTION 
                    style="BACKGROUND-COLOR: #d8bfd8">#D8BFD8<OPTION 
                    style="BACKGROUND-COLOR: #e6e6fa">#E6E6FA<OPTION 
                    style="BACKGROUND-COLOR: #f8f8ff">#F8F8FF<OPTION 
                    style="BACKGROUND-COLOR: #f0f8ff">#F0F8FF<OPTION 
                    style="BACKGROUND-COLOR: #f5fffa">#F5FFFA<OPTION 
                    style="BACKGROUND-COLOR: #f0fff0">#F0FFF0<OPTION 
                    style="BACKGROUND-COLOR: #fafad2">#FAFAD2<OPTION 
                    style="BACKGROUND-COLOR: #fffacd">#FFFACD<OPTION 
                    style="BACKGROUND-COLOR: #fff8dc">#FFF8DC<OPTION 
                    style="BACKGROUND-COLOR: #ffffe0">#FFFFE0<OPTION 
                    style="BACKGROUND-COLOR: #fffff0">#FFFFF0<OPTION 
                    style="BACKGROUND-COLOR: #fffaf0">#FFFAF0<OPTION 
                    style="BACKGROUND-COLOR: #faf0e6">#FAF0E6<OPTION 
                    style="BACKGROUND-COLOR: #fdf5e6">#FDF5E6<OPTION 
                    style="BACKGROUND-COLOR: #faebd7">#FAEBD7<OPTION 
                    style="BACKGROUND-COLOR: #ffe4c4">#FFE4C4<OPTION 
                    style="BACKGROUND-COLOR: #ffdab9">#FFDAB9<OPTION 
                    style="BACKGROUND-COLOR: #ffefd5">#FFEFD5<OPTION 
                    style="BACKGROUND-COLOR: #fff5ee">#FFF5EE<OPTION 
                    style="BACKGROUND-COLOR: #fff0f5">#FFF0F5<OPTION 
                    style="BACKGROUND-COLOR: #ffe4e1">#FFE4E1<OPTION 
                    style="BACKGROUND-COLOR: #fffafa">#FFFAFA</OPTION>
			</SELECT> </td>
		  <td class="body" width="80">&nbsp;</td>
		  <td class="body">&nbsp;</td>
		</tr>
		
		
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"> </td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	<input type="button" name="insertTable" value="Insert Table" class="Text90" onClick="javascript:InsertTable();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"> </td>
		
	<td class="body" align="center" valign="bottom">
	
	  </td>
	</tr>
</table>
</body>
</html>
<%
'-----------------------------ADD ANCHOR----------------------->
ELSEIF REQUEST.QUERYSTRING("ToDo")="InsertAnchor" or REQUEST.QUERYSTRING("ToDo")="ModifyAnchor" then
%>




<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<HEAD>
<TITLE>RichTemplate</TITLE>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richTemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richTemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
  
</table>
<script language=JavaScript>
window.onload = this.focus

function InsertAnchor() {
	error = 0
	var sel = window.opener.foo.document.selection;
	if (sel!=null) {
		var rng = sel.createRange();
	   	if (rng!=null) {

			name = document.anchorForm.anchor_name.value

        	if (error != 1) {
				if (name == "") {
					alert("Anchor name cannot be left blank")
					document.anchorForm.anchor_name.focus
					error = 1
				} else {
					if (window.opener.borderShown == "yes") {
						style = ' style="BORDER-RIGHT: #000000 1px dashed; BORDER-TOP: #000000 1px dashed; BORDER-LEFT: #000000 1px dashed; WIDTH: 20px; COLOR: #FFFFCC; BORDER-BOTTOM: #000000 1px dashed; HEIGHT: 16px; BACKGROUND-COLOR: #FFFFCC"'
					} else {
						style = ""
					}
					rng.pasteHTML("<a name=" + anchorForm.anchor_name.value + style + ">")
				}
			}
		}
	}
	
	if (error != 1) {
		window.opener.foo.focus()
		self.close();
	}
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				InsertAnchor()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=anchorForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Insert Anchor</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Insert Anchor&quot; to insert an anchor in your webpage.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Insert Anchor into Webpage</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		  <tr>
		    <td class="body" width="90">Anchor Name:</td>
			<td class="body">
			  <input type="text" name="anchor_name" size="10" class="Text150" maxlength="150">
		  </td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertAnchor" value="Insert Anchor" class="Text90" onClick="javascript:InsertAnchor();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
'--------------------------------INSERT EMAIL-------------------------->


ELSEIF REQUEST.QUERYSTRING("ToDo")="InsertEmail" then%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
	
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="../imagessslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
  
</table>
<script language=JavaScript>
window.onload = this.focus

function InsertEmail() {
	error = 0
	var sel = window.opener.foo.document.selection;
	if (sel!=null) {
		var rng = sel.createRange();
	   	if (rng!=null) {

			if (window.opener.foo.document.selection.type == "Control")
				{
					selectedImage = window.opener.foo.document.selection.createRange()(0);
					selectedImage.width = selectedImage.width
					selectedImage.height = selectedImage.height
				}

			email = document.emailForm.email.value
			subject = document.emailForm.subject.value

        	if (error != 1) {

				if (email == "") {
					alert("Email address cannot be left blank")
					document.emailForm.email.focus
					error = 1
				} else {
					mailto = "mailto:" + email
					if (subject != "")
					{
						mailto = mailto + "?subject=" + subject
					}
					rng.execCommand("CreateLink",false,mailto)
				}
			}
		}
	}
	
	if (error != 1) {
		window.opener.foo.focus()
		self.close();
	}
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				InsertEmail()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=emailForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Insert Email Link</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Insert Link&quot; to create a link to email address into your webpage.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Insert Email Link  into Webpage</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		
		
		  <tr>
		    <td class="body" width="90">Email Address:</td>
			<td class="body">
			  <input type="text" name="email" size="10" class="Text150" maxlength="150">
		  </td>
		  </tr>
		  <tr>
		    <td class="body" width="80">
			Subject:</td>
			<td class="body">
			  <input type="text" name="subject" size="10" class="Text150">
		  </td>
		  </tr>
		  
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertLink" value="Insert Link" class="Text90" onClick="javascript:InsertEmail();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
'--------------------------------INSERT FORM----------------------------->
ELSEIF REQUEST.QUERYSTRING("ToDo")="InsertForm" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richTemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richTemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="../imagessslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
 
</table>
<script language=JavaScript>
window.onload = this.focus

function InsertForm() {
	error = 0
	var sel = window.opener.document.selection;
	if (sel!=null) {
		var rng = sel.createRange();
	   	if (rng!=null) {
			name = document.formForm.form_name.value
			action = document.formForm.form_action.value
			method = document.formForm.form_method[formForm.form_method.selectedIndex].text

        		if (error != 1) {

				if (method != "None") {
					method = ' method="' + method + '"'
				} else {
					method = ""
				}

				if (name != "") {
					name = ' name="' + name + '"'
				} else {
					name = ""
				}

				if (action != "") {
					action = ' action="' + action + '"'
				} else {
					action = ""
				}

				if (window.opener.borderShown == "yes") {
					style = ' style="BORDER-RIGHT:1px dotted #FF0000; BORDER-TOP:1px dotted #FF0000; BORDER-LEFT:1px dotted #FF0000; BORDER-BOTTOM:1px dotted #FF0000;"'
				} else {
					style = ""
				}

        			HTMLForm = "<form " + name + action + method + style +">&nbsp;</form>"
					window.opener.foo.focus();
         			rng.pasteHTML(HTMLForm)
        		}
		}
	
	}
	
	if (error != 1) {
		self.close();
	}
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				InsertForm()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=formForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Insert Form</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Insert Form&quot; to insert a form into your webpage.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Insert Form into Webpage</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		
		
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body">
			  <input type="text" name="form_name" size="10" class="Text90" maxlength="50">
		  </td>
		  </tr>
		  <tr>
		    <td class="body" width="80">
			Action:</td>
			<td class="body">
			  <input type="text" name="form_action" size="50" class="Text250">
		  </td>
		  </tr>
		  <tr>
		    <td class="body" width="80">Method:</td>
			<td class="body">
			  <select class=text70 name=form_method>
				<option selected>None
				<option>Post
				<option>Get</option>
				</select>
			</td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertForm" value="Insert Form" class="Text90" onClick="javascript:InsertForm();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
'--------------------------MODIFY FORM------------------------------------>
ELSEIF REQUEST.QUERYSTRING("ToDo")="ModifyForm" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richTemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richTemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="../imagessslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
  
</table>
<script language=javascript>
var myPage = window.opener;

window.onload = setValues;

var formName = myPage.selectedForm.name;
var formAction = myPage.selectedForm.action;
var formMethod = myPage.selectedForm.method;

function setValues() {

	formForm.form_name.value = formName;
	formForm.form_action.value = formAction;
	this.focus();
}

function doModify() {

	if (formForm.form_name.value != "") {
		myPage.selectedForm.name = formForm.form_name.value
	} else {
		myPage.selectedForm.removeAttribute('name',0)
	}

	if (formForm.form_action.value != "") {
		myPage.selectedForm.action = formForm.form_action.value
	} else {
		myPage.selectedForm.removeAttribute('action',0)
	}

	if (formForm.method[formForm.method.selectedIndex].text != "None") {
    	myPage.selectedForm.method = formForm.method[formForm.method.selectedIndex].text
    } else {
		myPage.selectedForm.removeAttribute('method',0)
    }
        
    window.close()
}

function printMethod() {
	if ((formMethod != undefined) && (formMethod != "")) {
		document.write('<option selected>' + formMethod)
		document.write('<option>None')
	} else {
		document.write('<option selected>None')
	}
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				doModify()
			}
	};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=formForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Modify Form Properties</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Modify Form&quot; to modify the form properties of your form.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Modify Form Properties</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
		<table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body">
			  <input type="text" name="form_name" size="10" class="Text90" maxlength="50">
		  </td>
		  </tr>
		  <tr>
		    <td class="body" width="80">
			Action:</td>
			<td class="body">
			  <input type="text" name="form_action" size="50" class="Text250">
		  </td>
		  </tr>
		  
		  
		  
		
		
		  
		  <tr>
			<td class="body" width="80">Method:</td>
			<td class="body">
			  <SELECT class=text70 name=method>
			    <script>printMethod()</script>
			    <option>Post
			    <option>Get</option>
			  </select>
		  </td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="modifyForm" value="Modify Form" class="Text90" onClick="javascript:doModify();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form>
<br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
ELSEIF REQUEST.QUERYSTRING("ToDo")="InsertTextField" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richTemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richTemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="../imagessslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	

</table>
<script language=JavaScript>
window.onload = this.focus
var error
function InsertTextField() {
	var sel = window.opener.document.selection;
	if (sel!=null) {
		var rng = sel.createRange();
	   	if (rng!=null) {
			name = document.textForm.text_name.value
			width = document.textForm.text_width.value
			max = document.textForm.text_max.value
			value = document.textForm.text_value.value
			type = document.textForm.text_type[textForm.text_type.selectedIndex].text

		error = 0
		if (isNaN(width) || width < 0) {
				alert("Character Width must contain a valid, positive number")
				error = 1
				textForm.text_width.select()
				textForm.text_width.focus()
		} else if (isNaN(max) || max < 0) {
				alert("Maximum Characters must contain a valid, positive number")
				error = 1
				textForm.text_max.select()
				textForm.text_max.focus()
		}

		if (error != 1) {
				if (value != "") {
					value = ' value="' + value + '"'
				} else {
					value = ""
				}

				if (name != "") {
					name = ' name="' + name + '"'
				} else {
					name = ""
				}

				if (width != "") {
					width = ' size="' + width + '"'
				} else {
					width = ""
				}

				if (max != "") {
					max = ' maxlength="' + max + '"'
				} else {
					max = ""
				}

        			HTMLTextField = '<input type="' + type + '"' + name + value + width + max + '>'
					window.opener.foo.focus();
         			rng.pasteHTML(HTMLTextField)
		} // End if
		} // End if

	} // End If

	if (error != 1) {
		self.close();
	}
} // End function

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				InsertTextField()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=textForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Insert Text Field</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Insert Text Field&quot; to insert a Text Field into your webpage.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Insert Text Field</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		
		
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="text_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="text_value" size="10" class="Text150">
			</td>
		  </tr>
		  <tr>
			<td class="body" width="80">Character Width:</td>
			<td class="body">
			  <input type="text" name="text_width" size="3" class="Text50" maxlength="3">
			</td>
			<td class="body" width="80">Maximum Characters:</td>
			<td class="body">
			  <input type="text" name="text_max" size="3" class="Text50" maxlength="3">
			</td>
		  </tr>
		  <tr>
			<td class="body" width="80">Type</td>
			<td class="body">
			  <select name="text_type" class=text70>
			  <option selected>Text
			  <option>Password</option>
			  </select>
			</td>
			<td class="body" width="80">&nbsp;</td>
			<td class="body">&nbsp;</td>
		  </tr>
		  
		  
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertTextField" value="Insert Text Field" class="Text90" onClick="javascript:InsertTextField();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
ELSEIF REQUEST.QUERYSTRING("ToDo")="InsertTextArea" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richTemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richTemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="../imagessslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=JavaScript>
window.onload = this.focus
var error
function InsertTextArea() {
	var sel = window.opener.document.selection;
	if (sel!=null) {
		var rng = sel.createRange();
	   	if (rng!=null) {
			name = document.textForm.text_name.value
			rows = document.textForm.text_lines.value
			cols = document.textForm.text_width.value
			value = document.textForm.text_value.value

		error = 0
		if (isNaN(cols) || cols < 0) {
				alert("Character Width must contain a valid, positive number")
				error = 1
				textForm.text_width.select()
				textForm.text_width.focus()
		} else if (isNaN(rows) || rows < 0) {
				alert("Lines must contain a valid, positive number")
				error = 1
				textForm.text_lines.select()
				textForm.text_lines.focus()
		}

		if (error != 1) {
				if (value != "") {
					value = value
				} else {
					value = ""
				}

				if (name != "") {
					name = ' name="' + name + '"'
				} else {
					name = ""
				}

				if (cols != "") {
					cols = ' cols="' + cols + '"'
				} else {
					cols = ""
				}

				if (rows != "") {
					rows = ' rows="' + rows + '"'
				} else {
					rows = ""
				}

        			HTMLTextField = '<textarea' + name + cols + rows + '>' + value + '</textarea>'
					window.opener.foo.focus();
         			rng.pasteHTML(HTMLTextField)
		} // End if
		} // End if
	} // End If

	if (error != 1) {
		self.close();
	}
} // End function

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				InsertTextArea()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=textForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Insert Text Area</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Insert Text Area&quot; to insert a Text Area into your webpage.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Insert Text Area</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		
		
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="text_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="text_value" size="10" class="Text150">
			</td>
		  </tr>
		  <tr>
			<td class="body" width="80">Character Width:</td>
			<td class="body">
			  <input type="text" name="text_width" size="3" class="Text50" maxlength="3">
			</td>
			<td class="body" width="80">Lines:</td>
			<td class="body">
			  <input type="text" name="text_lines" size="3" class="Text50" maxlength="3">
			</td>
		  </tr>
		  
		  
		  
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertTextField" value="Insert Text Area" class="Text90" onClick="javascript:InsertTextArea();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
ELSEIF REQUEST.QUERYSTRING("ToDo")="InsertHidden" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richTemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richTemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="../imagessslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	

</table>
<script language=JavaScript>
window.onload = this.focus
var error
function InsertHiddenField() {
	var sel = window.opener.document.selection;
	if (sel!=null) {
		var rng = sel.createRange();
	   	if (rng!=null) {
			name = document.hiddenForm.hidden_name.value
			value = document.hiddenForm.hidden_value.value

		error = 0

		if (error != 1) {
				if (value != "None") {
					value = ' value="' + value + '"'
				} else {
					value = ""
				}

				if (name != "") {
					name = ' name="' + name + '"'
				} else {
					name = ""
				}

				if (window.opener.borderShown == "yes") {
					style = ' style="BORDER-RIGHT: #000000 1px dashed; BORDER-TOP: #000000 1px dashed; BORDER-LEFT: #000000 1px dashed; WIDTH: 15px; COLOR: #fdadad; BORDER-BOTTOM: #000000 1px dashed; HEIGHT: 15px; BACKGROUND-COLOR: #fdadad"'
				} else {
					style = ""
				}

        			HTMLTextField = '<input type=hidden' + name + value + style + '>'
					window.opener.foo.focus();
         			rng.pasteHTML(HTMLTextField)
		} // End if
		} // End if
	} // End If

	if (error != 1) {
		self.close();
	}
} // End function

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				InsertHiddenField()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=hiddenForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Insert Hidden Field</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Insert Hidden Field&quot; to insert a Hidden Field into your webpage.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Insert Hidden Field</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		
		
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="hidden_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="hidden_value" size="10" class="Text150">
			</td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertHiddenField" value="Insert Hidden Area" class="Text120" onClick="javascript:InsertHiddenField();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		
	
	  </td>
	</tr>
</table>
</body>
</html>
<%
ELSEIF REQUEST.QUERYSTRING("ToDo")="InsertButton" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richTemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richTemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="../imagessslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
</table>
<script language=JavaScript>
window.onload = this.focus

function InsertButton() {
	error = 0
	var sel = window.opener.foo.document.selection;
	if (sel!=null) {
		var rng = sel.createRange();
	   	if (rng!=null) {

			name = document.buttonForm.button_name.value
			value = document.buttonForm.button_value.value
			type = document.buttonForm.button_type[buttonForm.button_type.selectedIndex].text

			if (value != "") {
				value = ' value="' + value + '"'
			} else {
				value = ""
			}

			if (name != "") {
				name = ' name="' + name + '"'
			} else {
				name = ""
			}

			HTMLTextField = '<input type="' + type + '"' + name + value + '>'
			rng.pasteHTML(HTMLTextField)
		} // End if
	} // End If

	if (error != 1) {
		window.opener.foo.focus();
		self.close();
	}
} // End function

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				InsertButton()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=buttonForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Insert Button</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Insert Button&quot; to insert a Button into your webpage.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Insert Button</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="button_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="button_value" size="10" class="Text150">
			</td>
		  </tr>
		  
		  <tr>
			<td class="body" width="80">Type</td>
			<td class="body">
			  <select name="button_type" class=text70>
			    <option selected>Submit
			    <option>Reset
			    <option>Button</option>
			  </select>
			</td>
			<td class="body" width="80">&nbsp;</td>
			<td class="body">&nbsp;</td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertButton" value="Insert Button" class="Text90" onClick="javascript:InsertButton();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
ELSEIF REQUEST.QUERYSTRING("ToDo")="InsertCheckbox" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richTemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richtemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="../imagessslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=JavaScript>
window.onload = this.focus
var error
function InsertCheckbox() {
	var sel = window.opener.document.selection;
	if (sel!=null) {
		var rng = sel.createRange();
	   	if (rng!=null) {
			name = document.checkboxForm.checkbox_name.value
			value = document.checkboxForm.checkbox_value.value
			checked = document.checkboxForm.checkbox_type[checkboxForm.checkbox_type.selectedIndex].text

		if (value != "") {
			value = ' value="' + value + '"'
		} else {
			value = ""
		}

		if (name != "") {
			name = ' name="' + name + '"'
		} else {
			name = ""
		}

		if (checked == "Unchecked"){
			checked = ""
		}

		HTMLTextField = '<input type=checkbox ' + checked + name + value + '>'
		window.opener.foo.focus();
		rng.pasteHTML(HTMLTextField)
		
		} // End if
	} // End If

	if (error != 1) {
		self.close();
	}
} // End function

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				InsertCheckbox()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=checkboxForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Insert CheckBox</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Insert CheckBox&quot; to insert a CheckBox into your webpage.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Insert CheckBox</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="checkbox_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="checkbox_value" size="10" class="Text150">
			</td>
		  </tr>
		  
		  <tr>
			<td class="body" width="80">Initial State:</td>
			<td class="body">
			  <select name="checkbox_type" class=text90>
				<option>Checked</option>
				<option selected>Unchecked</option>
			  </select>
			</td>
			<td class="body" width="80">&nbsp;</td>
			<td class="body">&nbsp;</td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertCheckbox" value="Insert CheckBox" class="Text90" onClick="javascript:InsertCheckbox();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		
	
	  </td>
	</tr>
</table>
</body>
</html>
<%
ELSEIF REQUEST.QUERYSTRING("ToDo")="InsertRadio" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richtemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richtemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="../imagessslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=JavaScript>
window.onload = this.focus
var error
function InsertRadio() {
	var sel = window.opener.document.selection;
	if (sel!=null) {
		var rng = sel.createRange();
	   	if (rng!=null) {
			name = document.radioForm.radio_name.value
			value = document.radioForm.radio_value.value
			checked = document.radioForm.radio_type[radioForm.radio_type.selectedIndex].text

		if (value != "") {
			value = ' value="' + value + '"'
		} else {
			value = ""
		}

		if (name != "") {
			name = ' name="' + name + '"'
		} else {
			name = ""
		}

		if (checked == "Unchecked"){
			checked = ""
		}

		HTMLTextField = '<input type=radio ' + checked + name + value + '>'
		window.opener.foo.focus();
		rng.pasteHTML(HTMLTextField)
		
		} // End if
	} // End If

	if (error != 1) {
		self.close();
	}
} // End function

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				InsertRadio()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>


<form name=radioForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Insert Radio Button</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Insert Radio Button&quot; to insert a Radio Button  into your webpage.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Insert Radio Button</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="radio_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="radio_value" size="10" class="Text150">
			</td>
		  </tr>
		  
		  <tr>
			<td class="body" width="80">Initial State:</td>
			<td class="body">
			  <select name="radio_type" class=text90>
				<option>Checked</option>
				<option selected>Unchecked</option>
			  </select>
			</td>
			<td class="body" width="80">&nbsp;</td>
			<td class="body">&nbsp;</td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertRadio" value="Insert Radio Button" class="Text120" onClick="javascript:InsertRadio();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
ELSEIF REQUEST.QUERYSTRING("ToDo")="ModifyTable" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richtemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richtemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="../imagessslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=javascript>
var myTable = window.opener;

window.onload = setValues;

var tableBgColor = myTable.selectedTable.bgColor;
var tableSpacing = myTable.selectedTable.cellSpacing;
var tablePadding = myTable.selectedTable.cellPadding;
var tableBorder = myTable.selectedTable.border;
var tableWidth = myTable.selectedTable.width;

function setValues() {

	if (tableSpacing == "") tableSpacing = 2;
	if (tablePadding == "") tablePadding = 1;
	if (tableBorder == "") tableBorder = 0;

	// tableForm.table_bgcolor.value = tableBgColor;
	tableForm.table_padding.value = tablePadding;
	tableForm.table_spacing.value = tableSpacing;
	tableForm.table_border.value = tableBorder;
	tableForm.table_width.value = tableWidth;
	this.focus();
}

function doModify() {

	var error = 0;
	if (isNaN(tableForm.table_padding.value) || tableForm.table_padding.value < 0 || tableForm.table_padding.value == "") {
		alert("Cell Padding must contain a valid, positive number")
		error = 1
		tableForm.table_padding.select()
		tableForm.table_padding.focus()
	} else if (isNaN(tableForm.table_spacing.value) || tableForm.table_spacing.value < 0 || tableForm.table_spacing.value == "") {
		alert("Cell Spacing must contain a valid, positive number")
		error = 1
		tableForm.table_spacing.select()
		tableForm.table_spacing.focus()
	} else if (isNaN(tableForm.table_border.value) || tableForm.table_border.value < 0 || tableForm.table_border.value == "") {
		alert("Border must contain a valid, positive number")
		error = 1
		tableForm.table_border.select()
		tableForm.table_border.focus()
	}

	if (error != 1) {
        	myTable.selectedTable.cellPadding = tableForm.table_padding.value
        	myTable.selectedTable.cellSpacing = tableForm.table_spacing.value
        	myTable.selectedTable.border = tableForm.table_border.value
			myTable.selectedTable.width = tableForm.table_width.value
        	if (tableForm.bgColor[tableForm.bgColor.selectedIndex].text != "None") {
        		myTable.selectedTable.bgColor = tableForm.bgColor[tableForm.bgColor.selectedIndex].text
        	} else {
        		myTable.selectedTable.removeAttribute('bgColor',0)
        	}
        
        	window.close()
	}
}

function printBgColor() {
	if ((tableBgColor != undefined) && (tableBgColor != "")) {
		document.write('<option selected style="BACKGROUND-COLOR: ' + tableBgColor + '">' + tableBgColor)
		document.write('<option>None')
	} else {
		document.write('<option selected>None')
	}
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				doModify()					
			}
	};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
		event.cancelBubble = true;
		event.returnValue = false;
		return false;
	}
};

</script>

<form name=tableForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Modify Table Properties</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Modify Table&quot; to modify the table properties of your table.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Modify Table Properties</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
		<table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		
		
		  <tr>
		    <td class="body" width="150">Background Colour:</td>
			<td class="body" width="200">
			  <SELECT class=text70 name=bgColor>
				 
				<script>printBgColor()</script>
				<OPTION style="BACKGROUND-COLOR: #ff0000">#FF0000
				<OPTION 
                    style="BACKGROUND-COLOR: #ffff00">#FFFF00
				<OPTION 
                    style="BACKGROUND-COLOR: #00ff00">#00FF00
				<OPTION 
                    style="BACKGROUND-COLOR: #00ffff">#00FFFF
				<OPTION 
                    style="BACKGROUND-COLOR: #0000ff">#0000FF
				<OPTION 
                    style="BACKGROUND-COLOR: #ff00ff">#FF00FF
				<OPTION 
                    style="BACKGROUND-COLOR: #ffffff">#FFFFFF
				<OPTION 
                    style="BACKGROUND-COLOR: #f5f5f5">#F5F5F5
				<OPTION 
                    style="BACKGROUND-COLOR: #dcdcdc">#DCDCDC
				<OPTION 
                    style="BACKGROUND-COLOR: #d3d3d3">#D3D3D3
				<OPTION 
                    style="BACKGROUND-COLOR: #c0c0c0">#C0C0C0
				<OPTION 
                    style="BACKGROUND-COLOR: #a9a9a9">#A9A9A9
				<OPTION 
                    style="BACKGROUND-COLOR: #808080">#808080
				<OPTION 
                    style="BACKGROUND-COLOR: #696969">#696969
				<OPTION 
                    style="BACKGROUND-COLOR: #000000">#000000
				<OPTION 
                    style="BACKGROUND-COLOR: #2f4f4f">#2F4F4F
				<OPTION 
                    style="BACKGROUND-COLOR: #708090">#708090
				<OPTION 
                    style="BACKGROUND-COLOR: #778899">#778899
				<OPTION 
                    style="BACKGROUND-COLOR: #4682b4">#4682B4
				<OPTION 
                    style="BACKGROUND-COLOR: #4169e1">#4169E1
				<OPTION 
                    style="BACKGROUND-COLOR: #6495ed">#6495ED
				<OPTION 
                    style="BACKGROUND-COLOR: #b0c4de">#B0C4DE
				<OPTION 
                    style="BACKGROUND-COLOR: #7b68ee">#7B68EE
				<OPTION 
                    style="BACKGROUND-COLOR: #6a5acd">#6A5ACD
				<OPTION 
                    style="BACKGROUND-COLOR: #483d8b">#483D8B
				<OPTION 
                    style="BACKGROUND-COLOR: #191970">#191970
				<OPTION 
                    style="BACKGROUND-COLOR: #000080">#000080
				<OPTION 
                    style="BACKGROUND-COLOR: #00008b">#00008B
				<OPTION 
                    style="BACKGROUND-COLOR: #0000cd">#0000CD
				<OPTION 
                    style="BACKGROUND-COLOR: #1e90ff">#1E90FF
				<OPTION 
                    style="BACKGROUND-COLOR: #00bfff">#00BFFF
				<OPTION 
                    style="BACKGROUND-COLOR: #87cefa">#87CEFA
				<OPTION 
                    style="BACKGROUND-COLOR: #87ceeb">#87CEEB
				<OPTION 
                    style="BACKGROUND-COLOR: #add8e6">#ADD8E6
				<OPTION 
                    style="BACKGROUND-COLOR: #b0e0e6">#B0E0E6
				<OPTION 
                    style="BACKGROUND-COLOR: #f0ffff">#F0FFFF
				<OPTION 
                    style="BACKGROUND-COLOR: #e0ffff">#E0FFFF
				<OPTION 
                    style="BACKGROUND-COLOR: #afeeee">#AFEEEE
				<OPTION 
                    style="BACKGROUND-COLOR: #00ced1">#00CED1
				<OPTION 
                    style="BACKGROUND-COLOR: #5f9ea0">#5F9EA0
				<OPTION 
                    style="BACKGROUND-COLOR: #48d1cc">#48D1CC
				<OPTION 
                    style="BACKGROUND-COLOR: #00ffff">#00FFFF
				<OPTION 
                    style="BACKGROUND-COLOR: #40e0d0">#40E0D0
				<OPTION 
                    style="BACKGROUND-COLOR: #20b2aa">#20B2AA
				<OPTION 
                    style="BACKGROUND-COLOR: #008b8b">#008B8B
				<OPTION 
                    style="BACKGROUND-COLOR: #008080">#008080
				<OPTION 
                    style="BACKGROUND-COLOR: #7fffd4">#7FFFD4
				<OPTION 
                    style="BACKGROUND-COLOR: #66cdaa">#66CDAA
				<OPTION 
                    style="BACKGROUND-COLOR: #8fbc8f">#8FBC8F
				<OPTION 
                    style="BACKGROUND-COLOR: #3cb371">#3CB371
				<OPTION 
                    style="BACKGROUND-COLOR: #2e8b57">#2E8B57
				<OPTION 
                    style="BACKGROUND-COLOR: #006400">#006400
				<OPTION 
                    style="BACKGROUND-COLOR: #008000">#008000
				<OPTION 
                    style="BACKGROUND-COLOR: #228b22">#228B22
				<OPTION 
                    style="BACKGROUND-COLOR: #32cd32">#32CD32
				<OPTION 
                    style="BACKGROUND-COLOR: #00ff00">#00FF00
				<OPTION 
                    style="BACKGROUND-COLOR: #7fff00">#7FFF00
				<OPTION 
                    style="BACKGROUND-COLOR: #7cfc00">#7CFC00
				<OPTION 
                    style="BACKGROUND-COLOR: #adff2f">#ADFF2F
				<OPTION 
                    style="BACKGROUND-COLOR: #98fb98">#98FB98
				<OPTION 
                    style="BACKGROUND-COLOR: #90ee90">#90EE90
				<OPTION 
                    style="BACKGROUND-COLOR: #00ff7f">#00FF7F
				<OPTION 
                    style="BACKGROUND-COLOR: #00fa9a">#00FA9A
				<OPTION 
                    style="BACKGROUND-COLOR: #556b2f">#556B2F
				<OPTION 
                    style="BACKGROUND-COLOR: #6b8e23">#6B8E23
				<OPTION 
                    style="BACKGROUND-COLOR: #808000">#808000
				<OPTION 
                    style="BACKGROUND-COLOR: #bdb76b">#BDB76B
				<OPTION 
                    style="BACKGROUND-COLOR: #b8860b">#B8860B
				<OPTION 
                    style="BACKGROUND-COLOR: #daa520">#DAA520
				<OPTION 
                    style="BACKGROUND-COLOR: #ffd700">#FFD700
				<OPTION 
                    style="BACKGROUND-COLOR: #f0e68c">#F0E68C
				<OPTION 
                    style="BACKGROUND-COLOR: #eee8aa">#EEE8AA
				<OPTION 
                    style="BACKGROUND-COLOR: #ffebcd">#FFEBCD
				<OPTION 
                    style="BACKGROUND-COLOR: #ffe4b5">#FFE4B5
				<OPTION 
                    style="BACKGROUND-COLOR: #f5deb3">#F5DEB3
				<OPTION 
                    style="BACKGROUND-COLOR: #ffdead">#FFDEAD
				<OPTION 
                    style="BACKGROUND-COLOR: #deb887">#DEB887
				<OPTION 
                    style="BACKGROUND-COLOR: #d2b48c">#D2B48C
				<OPTION 
                    style="BACKGROUND-COLOR: #bc8f8f">#BC8F8F
				<OPTION 
                    style="BACKGROUND-COLOR: #a0522d">#A0522D
				<OPTION 
                    style="BACKGROUND-COLOR: #8b4513">#8B4513
				<OPTION 
                    style="BACKGROUND-COLOR: #d2691e">#D2691E
				<OPTION 
                    style="BACKGROUND-COLOR: #cd853f">#CD853F
				<OPTION 
                    style="BACKGROUND-COLOR: #f4a460">#F4A460
				<OPTION 
                    style="BACKGROUND-COLOR: #8b0000">#8B0000
				<OPTION 
                    style="BACKGROUND-COLOR: #800000">#800000
				<OPTION 
                    style="BACKGROUND-COLOR: #a52a2a">#A52A2A
				<OPTION 
                    style="BACKGROUND-COLOR: #b22222">#B22222
				<OPTION 
                    style="BACKGROUND-COLOR: #cd5c5c">#CD5C5C
				<OPTION 
                    style="BACKGROUND-COLOR: #f08080">#F08080
				<OPTION 

                    style="BACKGROUND-COLOR: #fa8072">#FA8072
				<OPTION 
                    style="BACKGROUND-COLOR: #e9967a">#E9967A
				<OPTION 
                    style="BACKGROUND-COLOR: #ffa07a">#FFA07A
				<OPTION 
                    style="BACKGROUND-COLOR: #ff7f50">#FF7F50
				<OPTION 
                    style="BACKGROUND-COLOR: #ff6347">#FF6347
				<OPTION 
                    style="BACKGROUND-COLOR: #ff8c00">#FF8C00
				<OPTION 
                    style="BACKGROUND-COLOR: #ffa500">#FFA500
				<OPTION 
                    style="BACKGROUND-COLOR: #ff4500">#FF4500
				<OPTION 
                    style="BACKGROUND-COLOR: #dc143c">#DC143C
				<OPTION 
                    style="BACKGROUND-COLOR: #ff0000">#FF0000
				<OPTION 
                    style="BACKGROUND-COLOR: #ff1493">#FF1493
				<OPTION 
                    style="BACKGROUND-COLOR: #ff00ff">#FF00FF
				<OPTION 
                    style="BACKGROUND-COLOR: #ff69b4">#FF69B4
				<OPTION 
                    style="BACKGROUND-COLOR: #ffb6c1">#FFB6C1
				<OPTION 
                    style="BACKGROUND-COLOR: #ffc0cb">#FFC0CB
				<OPTION 
                    style="BACKGROUND-COLOR: #db7093">#DB7093
				<OPTION 
                    style="BACKGROUND-COLOR: #c71585">#C71585
				<OPTION 
                    style="BACKGROUND-COLOR: #800080">#800080
				<OPTION 
                    style="BACKGROUND-COLOR: #8b008b">#8B008B
				<OPTION 
                    style="BACKGROUND-COLOR: #9370db">#9370DB
				<OPTION 
                    style="BACKGROUND-COLOR: #8a2be2">#8A2BE2
				<OPTION 
                    style="BACKGROUND-COLOR: #4b0082">#4B0082
				<OPTION 
                    style="BACKGROUND-COLOR: #9400d3">#9400D3
				<OPTION 
                    style="BACKGROUND-COLOR: #9932cc">#9932CC
				<OPTION 
                    style="BACKGROUND-COLOR: #ba55d3">#BA55D3
				<OPTION 
                    style="BACKGROUND-COLOR: #da70d6">#DA70D6
				<OPTION 
                    style="BACKGROUND-COLOR: #ee82ee">#EE82EE
				<OPTION 
                    style="BACKGROUND-COLOR: #dda0dd">#DDA0DD
				<OPTION 
                    style="BACKGROUND-COLOR: #d8bfd8">#D8BFD8
				<OPTION 
                    style="BACKGROUND-COLOR: #e6e6fa">#E6E6FA
				<OPTION 
                    style="BACKGROUND-COLOR: #f8f8ff">#F8F8FF
				<OPTION 
                    style="BACKGROUND-COLOR: #f0f8ff">#F0F8FF
				<OPTION 
                    style="BACKGROUND-COLOR: #f5fffa">#F5FFFA
				<OPTION 
                    style="BACKGROUND-COLOR: #f0fff0">#F0FFF0
				<OPTION 
                    style="BACKGROUND-COLOR: #fafad2">#FAFAD2
				<OPTION 
                    style="BACKGROUND-COLOR: #fffacd">#FFFACD
				<OPTION 
                    style="BACKGROUND-COLOR: #fff8dc">#FFF8DC
				<OPTION 
                    style="BACKGROUND-COLOR: #ffffe0">#FFFFE0
				<OPTION 
                    style="BACKGROUND-COLOR: #fffff0">#FFFFF0
				<OPTION 
                    style="BACKGROUND-COLOR: #fffaf0">#FFFAF0
				<OPTION 
                    style="BACKGROUND-COLOR: #faf0e6">#FAF0E6
				<OPTION 
                    style="BACKGROUND-COLOR: #fdf5e6">#FDF5E6
				<OPTION 
                    style="BACKGROUND-COLOR: #faebd7">#FAEBD7
				<OPTION 
                    style="BACKGROUND-COLOR: #ffe4c4">#FFE4C4
				<OPTION 
                    style="BACKGROUND-COLOR: #ffdab9">#FFDAB9
				<OPTION 
                    style="BACKGROUND-COLOR: #ffefd5">#FFEFD5
				<OPTION 
                    style="BACKGROUND-COLOR: #fff5ee">#FFF5EE
				<OPTION 
                    style="BACKGROUND-COLOR: #fff0f5">#FFF0F5
				<OPTION 
                    style="BACKGROUND-COLOR: #ffe4e1">#FFE4E1
				<OPTION 
                    style="BACKGROUND-COLOR: #fffafa">#FFFAFA</OPTION>
			  </SELECT>
			   
			</td>
			<td class="body" width="80">Cell Padding:</td>
			<td class="body">
			  <input type="text" name="table_padding" size="2" class="Text50" maxlength="2">
		  </td>
		  </tr>
		  <tr>
			<td class="body" width="80">Border:</td>
			<td class="body">
			  <input type="text" name="table_border" size="2" class="Text50" value="1" maxlength="2">
		  </td>
			<td class="body" width="80">Cell Spacing:</td>
			<td class="body">
			  <input type="text" name="table_spacing" size="2" class="Text50" value="2" maxlength="2">
		  </td>
		  </tr>
		  <tr>
			<td class="body" width="80">Width:</td>
			<td class="body">
			  <input type="text" name="table_width" size="3" class="Text50" value="" maxlength="4">
			</td>
			<td class="body" width="80">&nbsp;</td>
			<td class="body">&nbsp;</td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="modifyTable" value="Modify Table" class="Text90" onClick="javascript:doModify();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form>
<br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
ELSEIF REQUEST.QUERYSTRING("ToDo")="ModifyCell" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richtemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richtemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="../imagessslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
  
</table>
<script language=javascript>
var myTable = window.opener;

window.onload = setValues;

var cellBgColor = myTable.selectedTD.bgColor;
var cellWidth = myTable.selectedTD.width;
var cellAlign = myTable.selectedTD.align;
var cellvAlign = myTable.selectedTD.vAlign;

var tablePadding = myTable.selectedTable.cellPadding;

function setValues() {

	tableForm.cell_width.value = cellWidth;
	this.focus();
}

function doModify() {

	var error = 0;
	if (tableForm.cell_width.value < 0) {
		alert("Cell Width must contain a valid, positive number")
		error = 1
		tableForm.cell_width.select()
		tableForm.cell_width.focus()
	}

	if (error != 1) {
        	myTable.selectedTD.width = tableForm.cell_width.value
    
        	if (tableForm.bgColor[tableForm.bgColor.selectedIndex].text != "None") {
        		myTable.selectedTD.bgColor = tableForm.bgColor[tableForm.bgColor.selectedIndex].text
        	} else {
        		myTable.selectedTD.removeAttribute('bgColor',0)
        	}

		if (tableForm.align[tableForm.align.selectedIndex].text != "None") {
        		myTable.selectedTD.align = tableForm.align[tableForm.align.selectedIndex].text
        	} else {
        		myTable.selectedTD.removeAttribute('align',0)
        	}

		if (tableForm.valign[tableForm.valign.selectedIndex].text != "None") {
        		myTable.selectedTD.vAlign = tableForm.valign[tableForm.valign.selectedIndex].text
        	} else {
        		myTable.selectedTD.removeAttribute('vAlign',0)
        	}
        
        	window.close()
	}
}

function printAlign() {
	if ((cellAlign != undefined) && (cellAlign != "")) {
		document.write('<option selected>' + cellAlign)
		document.write('<option>None')
	} else {
		document.write('<option selected>None')
	}
}

function printvAlign() {
	if ((cellvAlign != undefined) && (cellvAlign != "")) {
		document.write('<option selected>' + cellvAlign)
		document.write('<option>None')
	} else {
		document.write('<option selected>None')
	}
}

function printBgColor() {
	if ((cellBgColor != undefined) && (cellBgColor != "")) {
		document.write('<option selected style="BACKGROUND-COLOR: ' + cellBgColor + '">' + cellBgColor)
		document.write('<option>None')
	} else {
		document.write('<option selected>None')
	}
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				doModify()
			}
	};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=tableForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Modify Cell Properties</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Modify Cell&quot; to modify the cell properties of your table cell.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Modify Cell Properties</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
		<table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		
		
		  <tr>
		    <td class="body" width="150">Background Colour:</td>
			<td class="body" width="200">
			<SELECT class=text70 name=bgColor> 
		<script>printBgColor()</script>
		<OPTION style="BACKGROUND-COLOR: #ff0000">#FF0000<OPTION 
                    style="BACKGROUND-COLOR: #ffff00">#FFFF00<OPTION 
                    style="BACKGROUND-COLOR: #00ff00">#00FF00<OPTION 
                    style="BACKGROUND-COLOR: #00ffff">#00FFFF<OPTION 
                    style="BACKGROUND-COLOR: #0000ff">#0000FF<OPTION 
                    style="BACKGROUND-COLOR: #ff00ff">#FF00FF<OPTION 
                    style="BACKGROUND-COLOR: #ffffff">#FFFFFF<OPTION 
                    style="BACKGROUND-COLOR: #f5f5f5">#F5F5F5<OPTION 
                    style="BACKGROUND-COLOR: #dcdcdc">#DCDCDC<OPTION 
                    style="BACKGROUND-COLOR: #d3d3d3">#D3D3D3<OPTION 
                    style="BACKGROUND-COLOR: #c0c0c0">#C0C0C0<OPTION 
                    style="BACKGROUND-COLOR: #a9a9a9">#A9A9A9<OPTION 
                    style="BACKGROUND-COLOR: #808080">#808080<OPTION 
                    style="BACKGROUND-COLOR: #696969">#696969<OPTION 
                    style="BACKGROUND-COLOR: #000000">#000000<OPTION 
                    style="BACKGROUND-COLOR: #2f4f4f">#2F4F4F<OPTION 
                    style="BACKGROUND-COLOR: #708090">#708090<OPTION 
                    style="BACKGROUND-COLOR: #778899">#778899<OPTION 
                    style="BACKGROUND-COLOR: #4682b4">#4682B4<OPTION 
                    style="BACKGROUND-COLOR: #4169e1">#4169E1<OPTION 
                    style="BACKGROUND-COLOR: #6495ed">#6495ED<OPTION 
                    style="BACKGROUND-COLOR: #b0c4de">#B0C4DE<OPTION 
                    style="BACKGROUND-COLOR: #7b68ee">#7B68EE<OPTION 
                    style="BACKGROUND-COLOR: #6a5acd">#6A5ACD<OPTION 
                    style="BACKGROUND-COLOR: #483d8b">#483D8B<OPTION 
                    style="BACKGROUND-COLOR: #191970">#191970<OPTION 
                    style="BACKGROUND-COLOR: #000080">#000080<OPTION 
                    style="BACKGROUND-COLOR: #00008b">#00008B<OPTION 
                    style="BACKGROUND-COLOR: #0000cd">#0000CD<OPTION 
                    style="BACKGROUND-COLOR: #1e90ff">#1E90FF<OPTION 
                    style="BACKGROUND-COLOR: #00bfff">#00BFFF<OPTION 
                    style="BACKGROUND-COLOR: #87cefa">#87CEFA<OPTION 
                    style="BACKGROUND-COLOR: #87ceeb">#87CEEB<OPTION 
                    style="BACKGROUND-COLOR: #add8e6">#ADD8E6<OPTION 
                    style="BACKGROUND-COLOR: #b0e0e6">#B0E0E6<OPTION 
                    style="BACKGROUND-COLOR: #f0ffff">#F0FFFF<OPTION 
                    style="BACKGROUND-COLOR: #e0ffff">#E0FFFF<OPTION 
                    style="BACKGROUND-COLOR: #afeeee">#AFEEEE<OPTION 
                    style="BACKGROUND-COLOR: #00ced1">#00CED1<OPTION 
                    style="BACKGROUND-COLOR: #5f9ea0">#5F9EA0<OPTION 
                    style="BACKGROUND-COLOR: #48d1cc">#48D1CC<OPTION 
                    style="BACKGROUND-COLOR: #00ffff">#00FFFF<OPTION 
                    style="BACKGROUND-COLOR: #40e0d0">#40E0D0<OPTION 
                    style="BACKGROUND-COLOR: #20b2aa">#20B2AA<OPTION 
                    style="BACKGROUND-COLOR: #008b8b">#008B8B<OPTION 
                    style="BACKGROUND-COLOR: #008080">#008080<OPTION 
                    style="BACKGROUND-COLOR: #7fffd4">#7FFFD4<OPTION 
                    style="BACKGROUND-COLOR: #66cdaa">#66CDAA<OPTION 
                    style="BACKGROUND-COLOR: #8fbc8f">#8FBC8F<OPTION 
                    style="BACKGROUND-COLOR: #3cb371">#3CB371<OPTION 
                    style="BACKGROUND-COLOR: #2e8b57">#2E8B57<OPTION 
                    style="BACKGROUND-COLOR: #006400">#006400<OPTION 
                    style="BACKGROUND-COLOR: #008000">#008000<OPTION 
                    style="BACKGROUND-COLOR: #228b22">#228B22<OPTION 
                    style="BACKGROUND-COLOR: #32cd32">#32CD32<OPTION 
                    style="BACKGROUND-COLOR: #00ff00">#00FF00<OPTION 
                    style="BACKGROUND-COLOR: #7fff00">#7FFF00<OPTION 
                    style="BACKGROUND-COLOR: #7cfc00">#7CFC00<OPTION 
                    style="BACKGROUND-COLOR: #adff2f">#ADFF2F<OPTION 
                    style="BACKGROUND-COLOR: #98fb98">#98FB98<OPTION 
                    style="BACKGROUND-COLOR: #90ee90">#90EE90<OPTION 
                    style="BACKGROUND-COLOR: #00ff7f">#00FF7F<OPTION 
                    style="BACKGROUND-COLOR: #00fa9a">#00FA9A<OPTION 
                    style="BACKGROUND-COLOR: #556b2f">#556B2F<OPTION 
                    style="BACKGROUND-COLOR: #6b8e23">#6B8E23<OPTION 
                    style="BACKGROUND-COLOR: #808000">#808000<OPTION 
                    style="BACKGROUND-COLOR: #bdb76b">#BDB76B<OPTION 
                    style="BACKGROUND-COLOR: #b8860b">#B8860B<OPTION 
                    style="BACKGROUND-COLOR: #daa520">#DAA520<OPTION 
                    style="BACKGROUND-COLOR: #ffd700">#FFD700<OPTION 
                    style="BACKGROUND-COLOR: #f0e68c">#F0E68C<OPTION 
                    style="BACKGROUND-COLOR: #eee8aa">#EEE8AA<OPTION 
                    style="BACKGROUND-COLOR: #ffebcd">#FFEBCD<OPTION 
                    style="BACKGROUND-COLOR: #ffe4b5">#FFE4B5<OPTION 
                    style="BACKGROUND-COLOR: #f5deb3">#F5DEB3<OPTION 
                    style="BACKGROUND-COLOR: #ffdead">#FFDEAD<OPTION 
                    style="BACKGROUND-COLOR: #deb887">#DEB887<OPTION 
                    style="BACKGROUND-COLOR: #d2b48c">#D2B48C<OPTION 
                    style="BACKGROUND-COLOR: #bc8f8f">#BC8F8F<OPTION 
                    style="BACKGROUND-COLOR: #a0522d">#A0522D<OPTION 
                    style="BACKGROUND-COLOR: #8b4513">#8B4513<OPTION 
                    style="BACKGROUND-COLOR: #d2691e">#D2691E<OPTION 
                    style="BACKGROUND-COLOR: #cd853f">#CD853F<OPTION 
                    style="BACKGROUND-COLOR: #f4a460">#F4A460<OPTION 
                    style="BACKGROUND-COLOR: #8b0000">#8B0000<OPTION 
                    style="BACKGROUND-COLOR: #800000">#800000<OPTION 
                    style="BACKGROUND-COLOR: #a52a2a">#A52A2A<OPTION 
                    style="BACKGROUND-COLOR: #b22222">#B22222<OPTION 
                    style="BACKGROUND-COLOR: #cd5c5c">#CD5C5C<OPTION 
                    style="BACKGROUND-COLOR: #f08080">#F08080<OPTION 
                    style="BACKGROUND-COLOR: #fa8072">#FA8072<OPTION 
                    style="BACKGROUND-COLOR: #e9967a">#E9967A<OPTION 
                    style="BACKGROUND-COLOR: #ffa07a">#FFA07A<OPTION 
                    style="BACKGROUND-COLOR: #ff7f50">#FF7F50<OPTION 
                    style="BACKGROUND-COLOR: #ff6347">#FF6347<OPTION 
                    style="BACKGROUND-COLOR: #ff8c00">#FF8C00<OPTION 
                    style="BACKGROUND-COLOR: #ffa500">#FFA500<OPTION 
                    style="BACKGROUND-COLOR: #ff4500">#FF4500<OPTION 
                    style="BACKGROUND-COLOR: #dc143c">#DC143C<OPTION 
                    style="BACKGROUND-COLOR: #ff0000">#FF0000<OPTION 
                    style="BACKGROUND-COLOR: #ff1493">#FF1493<OPTION 
                    style="BACKGROUND-COLOR: #ff00ff">#FF00FF<OPTION 
                    style="BACKGROUND-COLOR: #ff69b4">#FF69B4<OPTION 
                    style="BACKGROUND-COLOR: #ffb6c1">#FFB6C1<OPTION 
                    style="BACKGROUND-COLOR: #ffc0cb">#FFC0CB<OPTION 
                    style="BACKGROUND-COLOR: #db7093">#DB7093<OPTION 
                    style="BACKGROUND-COLOR: #c71585">#C71585<OPTION 
                    style="BACKGROUND-COLOR: #800080">#800080<OPTION 
                    style="BACKGROUND-COLOR: #8b008b">#8B008B<OPTION 
                    style="BACKGROUND-COLOR: #9370db">#9370DB<OPTION 
                    style="BACKGROUND-COLOR: #8a2be2">#8A2BE2<OPTION 
                    style="BACKGROUND-COLOR: #4b0082">#4B0082<OPTION 
                    style="BACKGROUND-COLOR: #9400d3">#9400D3<OPTION 
                    style="BACKGROUND-COLOR: #9932cc">#9932CC<OPTION 
                    style="BACKGROUND-COLOR: #ba55d3">#BA55D3<OPTION 
                    style="BACKGROUND-COLOR: #da70d6">#DA70D6<OPTION 
                    style="BACKGROUND-COLOR: #ee82ee">#EE82EE<OPTION 
                    style="BACKGROUND-COLOR: #dda0dd">#DDA0DD<OPTION 
                    style="BACKGROUND-COLOR: #d8bfd8">#D8BFD8<OPTION 
                    style="BACKGROUND-COLOR: #e6e6fa">#E6E6FA<OPTION 
                    style="BACKGROUND-COLOR: #f8f8ff">#F8F8FF<OPTION 
                    style="BACKGROUND-COLOR: #f0f8ff">#F0F8FF<OPTION 
                    style="BACKGROUND-COLOR: #f5fffa">#F5FFFA<OPTION 
                    style="BACKGROUND-COLOR: #f0fff0">#F0FFF0<OPTION 
                    style="BACKGROUND-COLOR: #fafad2">#FAFAD2<OPTION 
                    style="BACKGROUND-COLOR: #fffacd">#FFFACD<OPTION 
                    style="BACKGROUND-COLOR: #fff8dc">#FFF8DC<OPTION 
                    style="BACKGROUND-COLOR: #ffffe0">#FFFFE0<OPTION 
                    style="BACKGROUND-COLOR: #fffff0">#FFFFF0<OPTION 
                    style="BACKGROUND-COLOR: #fffaf0">#FFFAF0<OPTION 
                    style="BACKGROUND-COLOR: #faf0e6">#FAF0E6<OPTION 
                    style="BACKGROUND-COLOR: #fdf5e6">#FDF5E6<OPTION 
                    style="BACKGROUND-COLOR: #faebd7">#FAEBD7<OPTION 
                    style="BACKGROUND-COLOR: #ffe4c4">#FFE4C4<OPTION 
                    style="BACKGROUND-COLOR: #ffdab9">#FFDAB9<OPTION 
                    style="BACKGROUND-COLOR: #ffefd5">#FFEFD5<OPTION 
                    style="BACKGROUND-COLOR: #fff5ee">#FFF5EE<OPTION 
                    style="BACKGROUND-COLOR: #fff0f5">#FFF0F5<OPTION 
                    style="BACKGROUND-COLOR: #ffe4e1">#FFE4E1<OPTION 
                    style="BACKGROUND-COLOR: #fffafa">#FFFAFA</OPTION>
			</SELECT> 
			</td>
			<td class="body" width="80">Cell Width:</td>
			<td class="body">
			  <input type="text" name="cell_width" size="3" class="Text50" maxlength="3">
		  </td>
		  </tr>
		  <tr>
			<td class="body" width="80">Horizontal Align:</td>
			<td class="body">
			<SELECT class=text70 name=align> 
			<script>printAlign()</script>
			<option>Left
			<option>Center
			<option>Right</option>
			</select>
		  </td>
			<td class="body" width="80">Vertical Align:</td>
			<td class="body">
			<SELECT class=text70 name=valign> 
			<script>printvAlign()</script>
			<option>Top
			<option>Middle
			<option>Bottom</option>
			</select>
		  </td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="modifyCell" value="Modify Cell" class="Text90" onClick="javascript:doModify();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form>
<br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
ELSEIF REQUEST.QUERYSTRING("ToDo")="InsertImage" then
%>
<%if request.form("ToDo")="Upload Image" then%>
<%

	' print the upload file page

	Const ForReading = 1, ForWriting = 2, ForAppending = 8 
	dim fso, f, ts, fileContent, includeFile
	set fso = server.CreateObject("Scripting.FileSystemObject") 

	includeFile = Server.mapPath("upload_page.inc")

	if (fso.FileExists(includeFile)=true) Then
		set f = fso.GetFile(includeFile)
		set ts = f.OpenAsTextStream(ForReading, -2) 
		Do While not ts.AtEndOfStream
		 		fileContent = fileContent & ts.ReadLine & vbCrLf
		Loop
		fileContent = replace(fileContent, "$SCRIPTNAME", ScriptName)
		fileContent = replace(fileContent, "$NEWDIR", newdir)
		fileContent = replace(fileContent, "$NEWIMAGEDIR", newimagedirectory)

		if (bool_from_image_dir = 1) Then
			fileContent = replace(fileContent, "$$DONOTDELETE$$", "<input type=hidden value=" & NewImageDirectory & " name=newimagedir><input type=hidden name=FromImageDir value=1>")
		else
			fileContent = replace(fileContent, "$$DONOTDELETE$$", "<input type=hidden value=" & NewDir & " name=newdir>")
		End if

		response.write(fileContent)
	else
		PrintError "Template", "<B>Cannot open Upload Page file:</B> webedit_includes/upload_page.inc", "File not Found"
	End if
	


%>
<%else%>

<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}

</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richtemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richtemplate.com/purchase.html"
	}
}
</script>		

</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="../imagessslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=javascript>

var fileWin
var prvTr
var prvTr2

// window.onload = this.focus
window.onerror = stopError

	function stopError() {
		return true;
	}

	function grey(tr) {
		prvTr2 = tr
		tr.className = 'b4';
		if ((prvTr != null) && (prvTr != tr))
			prvTr.className = '';
		prvTr = prvTr2
	}

	function ConfirmDelete(filename) {
		var deleteFile = confirm("Are you sure you wish to delete?");
		if (deleteFile == true) {
			document.location = "makeedit.asp?newdir=&ToDo=Delete&FileName=" + filename
		}
	}

	function ConfirmDeleteFolder(filename) {
		var deleteFile = confirm("Are you sure you wish to delete this folder and ALL its contents?");
		if (deleteFile == true) {
			document.location = "makeedit.asp?newdir=&isFolder=1&ToDo=Delete&FileName=" + filename
		}
	}

	function ConfirmImageDeleteFolder(filename) {
		var deleteFile = confirm("Are you sure you wish to delete this folder and ALL its contents?");
		if (deleteFile == true) {
			document.location = "makeedit.asp?newimagedir=&isFolder=1&ToDo=Delete&FileName=" + filename + "&FromImageDir=1"
		}
	}

	function ConfirmImageDelete(filename) {
		var deleteFile = confirm("Are you sure you wish to delete?");
		if (deleteFile == true) {
			document.location = "makeedit.asp?newimagedir=&ToDo=Delete&FileName=" + filename + "&FromImageDir=1"
		}
	}

	function SelectImage(ImageName) {
		window.opener.selectImage("images/" + ImageName);
		self.close();
	}

	function SetBackgd(ImageName) {
		var setBg = confirm("Are you sure you wish to set this image as the page background image?");
		if (setBg == true) {
			window.opener.setBackgd("images/" + ImageName);
			self.close();
		}
	}

	function getLink() {

		if (window.opener.foo.document.selection.type == "Control") {
			var oControlRange = window.opener.foo.document.selection.createRange();
			if (oControlRange(0).tagName.toUpperCase() == "IMG") {
				var oSel = oControlRange(0).parentNode;
			}
		} else {
			oSel = window.opener.foo.document.selection.createRange().parentElement();
		}

		if (oSel.tagName.toUpperCase() == "A")
		{
			document.linkForm.targetWindow.value = oSel.target
			document.linkForm.link.value = oSel.href
		}
	}

	function InsertLink() {
		targetWindow = document.linkForm.targetWindow.value;
		var linkSource = document.linkForm.link.value

		if (linkSource != "")
		{
			var oNewLink = window.opener.foo.document.createElement("<A>");
			oNewSelection = window.opener.foo.document.selection.createRange()

			if (window.opener.foo.document.selection.type == "Control")
			{
				selectedImage = window.opener.foo.document.selection.createRange()(0);
				selectedImage.width = selectedImage.width
				selectedImage.height = selectedImage.height
			}

			oNewSelection.execCommand("CreateLink",false,linkSource);

			if (window.opener.foo.document.selection.type == "Control")
			{
				oLink = oNewSelection(0).parentNode;
			} else
				oLink = oNewSelection.parentElement()

			if (targetWindow != "")
			{
				oLink.target = targetWindow;
			} else
				oLink.removeAttribute("target")

			window.opener.foo.focus();
			self.close();
		} else {
			alert("URL cannot be left blank")
			document.linkForm.link.focus()
		}
	}

	function CreateLink(LinkSource) {
		document.linkForm.link.value = LinkSource;
		document.linkForm.link.focus()
	}

	function RemoveLink() {
		if (window.opener.foo.document.selection.type == "Control")
		{
			selectedImage = window.opener.foo.document.selection.createRange()(0);
			selectedImage.width = selectedImage.width
			selectedImage.height = selectedImage.height
		}

		window.opener.foo.document.execCommand("Unlink");
		window.opener.foo.focus();
		self.close();
	}

	function getAnchors() {
		var allLinks = window.opener.foo.document.body.getElementsByTagName("A");
		for (a=0; a < allLinks.length; a++) {
				if (allLinks[a].href.toUpperCase() == "") {
					document.write("<option value=#" + allLinks[a].name + ">" + allLinks[a].name + "</option>")
				}
		}
	}

	function ViewFile(fileName) {
		
		if (fileWin) { fileWin.close() }

		var leftPos = (screen.availWidth-700) / 2
		var topPos = (screen.availHeight-500) / 2 
	 	fileWin = window.open(fileName,'fileWindow','width=700,height=500,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
		fileWin.focus()
		fileWin.location.reload(true);
	}

</script>
<%'*********************************%>
<%'sub PrintImageDir()
	' Print the contents of the directory
	' First, load the javascript functions

AllowDelete = 1
' Allow the deletion of files and folders from the File Manager, 0 = no, 1 = yes

AllowDeleteImage = 1
' Allow the deletion of images and image folders from the Image Manager, 0 = no, 1 = yes

AllowUpload = 1
' Allow users to upload files in File Manager, 0 = no, 1 = yes

AllowUploadImage = 1
' Allow users to upload images in Image Manager, 0 = no, 1 = yes

AllowRename = 1
' Allow users to rename files and folders in File Manager, 0 = no, 1 = yes

AllowRenameImage = 1
' Allow users to rename images and folders in Image Manager, 0 = no, 1 = yes	
	
URL = Request.ServerVariables("server_name")
scriptName = request.servervariables("SCRIPT_NAME")


	if CurrentImageDirectory = "" then
		CurrentImageDirectory = "/richtemplate/admin/images"
		ImageDirectory = "/"
	end if
	
	'********display jscommon**********
%>
	<script language=javascript>

var fileWin
var prvTr
var prvTr2

// window.onload = this.focus
window.onerror = stopError

	function stopError() {
		return true;
	}

	function ConfirmDelete(filename) {
		var deleteFile = confirm("Are you sure you wish to delete?");
		if (deleteFile == true) {
			document.location = "http://$URL$SCRIPTNAME?newdir=$NEWDIR&ToDo=Delete&FileName=" + filename
		}
	}

	function ConfirmDeleteFolder(filename) {
		var deleteFile = confirm("Are you sure you wish to delete this folder and ALL its contents?");
		if (deleteFile == true) {
			document.location = "http://$URL$SCRIPTNAME?newdir=$NEWDIR&isFolder=1&ToDo=Delete&FileName=" + filename
		}
	}

	function ConfirmImageDeleteFolder(filename) {
		var deleteFile = confirm("Are you sure you wish to delete this folder and ALL its contents?");
		if (deleteFile == true) {
			document.location = "http://$URL$SCRIPTNAME?newimagedir=$NEWIMAGEDIR&isFolder=1&ToDo=Delete&FileName=" + filename + "&FromImageDir=1"
		}
	}

	function ConfirmImageDelete(filename) {
		var deleteFile = confirm("Are you sure you wish to delete?");
		if (deleteFile == true) {
			document.location = "http://$URL$SCRIPTNAME?newimagedir=$NEWIMAGEDIR&ToDo=Delete&FileName=" + filename + "&FromImageDir=1"
		}
	}

	function SelectImage(ImageName) {
		window.opener.selectImage("/richtemplate/admin/images/" + ImageName);
		self.close();
	}

	function SetBackgd(ImageName) {
		var setBg = confirm("Are you sure you wish to set this image as the page background image?");
		if (setBg == true) {
			window.opener.setBackgd("http://$URL$CurrentImageDirectory/" + ImageName);
			self.close();
		}
	}

	function getLink() {

		if (window.opener.foo.document.selection.type == "Control") {
			var oControlRange = window.opener.foo.document.selection.createRange();
			if (oControlRange(0).tagName.toUpperCase() == "IMG") {
				var oSel = oControlRange(0).parentNode;
			}
		} else {
			oSel = window.opener.foo.document.selection.createRange().parentElement();
		}

		if (oSel.tagName.toUpperCase() == "A")
		{
			document.linkForm.targetWindow.value = oSel.target
			document.linkForm.link.value = oSel.href
		}
	}

	function InsertLink() {
		targetWindow = document.linkForm.targetWindow.value;
		var linkSource = document.linkForm.link.value

		if (linkSource != "")
		{
			var oNewLink = window.opener.foo.document.createElement("<A>");
			oNewSelection = window.opener.foo.document.selection.createRange()

			if (window.opener.foo.document.selection.type == "Control")
			{
				selectedImage = window.opener.foo.document.selection.createRange()(0);
				selectedImage.width = selectedImage.width
				selectedImage.height = selectedImage.height
			}

			oNewSelection.execCommand("CreateLink",false,linkSource);

			if (window.opener.foo.document.selection.type == "Control")
			{
				oLink = oNewSelection(0).parentNode;
			} else
				oLink = oNewSelection.parentElement()

			if (targetWindow != "")
			{
				oLink.target = targetWindow;
			} else
				oLink.removeAttribute("target")

			window.opener.foo.focus();
			self.close();
		} else {
			alert("URL cannot be left blank")
			document.linkForm.link.focus()
		}
	}

	function CreateLink(LinkSource) {
		document.linkForm.link.value = LinkSource;
		document.linkForm.link.focus()
	}

	function RemoveLink() {
		if (window.opener.foo.document.selection.type == "Control")
		{
			selectedImage = window.opener.foo.document.selection.createRange()(0);
			selectedImage.width = selectedImage.width
			selectedImage.height = selectedImage.height
		}

		window.opener.foo.document.execCommand("Unlink");
		window.opener.foo.focus();
		self.close();
	}

	function getAnchors() {
		var allLinks = window.opener.foo.document.body.getElementsByTagName("A");
		for (a=0; a < allLinks.length; a++) {
				if (allLinks[a].href.toUpperCase() == "") {
					document.write("<option value=#" + allLinks[a].name + ">" + allLinks[a].name + "</option>")
				}
		}
	}

	function ViewFile(fileName) {
		
		if (fileWin) { fileWin.close() }

		var leftPos = (screen.availWidth-700) / 2
		var topPos = (screen.availHeight-500) / 2 
	 	fileWin = window.open(fileName,'fileWindow','width=700,height=500,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
		fileWin.focus()
		fileWin.location.reload(true);
	}

</script>

<%'*****************end jscommon******************

	Dim objFilename, objFSO, objFolder, objFiles, objSubfolders, i
	Set objFSO = Server.CreateObject("Scripting.FileSystemObject")
	
	
	If (objFSO.FolderExists(server.mappath(CurrentImageDirectory))=true) Then
		Set objFolder = objFSO.GetFolder(server.mappath(CurrentImageDirectory))
		
	else
'		PrintError "Image Directory", "<B>Cannot open image directory for reading:</B> " & CurrentImageDirectory, "Directory Not Found"
	End if

	Set objFiles = objFolder.Files
	Set objSubfolders = objFolder.SubFolders

%>
<FORM METHOD=POST>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="webedit_images/1x1.gif" width="15" height="1"></td>
	<td class="heading1">Image Manager</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">Images - View, Insert, Rename, Delete or Upload<BR>
	Directories - Rename, Delete or Create directories</td>

  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	Current Working Directory: <%=CurrentImageDirectory%><br><br>
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="bevel1">
  		<tr>
		  <td>&nbsp;&nbsp;My Images and Folders</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="webedit_images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table border="0" cellspacing="0" cellpadding="0" width="98%" class="bevel2">
	  <tr><td width=30>&nbsp;</td><td class=bodybold width=29%>File Name</td><td class=bodybold width=17%>File Size (bytes)</td><td class=bodybold width=20%>Last Modified</td><td class=bodybold width=30% colspan=3>Action</td></tr>
<%
	if CurrentImageDirectory = "/" then
		CurrentImageDirectory = "/richtemplate/admin/images"
		ImageDirectory = "/"
	end if

	if (CurrentImageDirectory <> ImageDirectory) Then

	Dim previousDir
	previousDir = left(CurrentImageDirectory,inStrRev(CurrentImageDirectory,"/")-1)
%>

<%
	End if

%>
	<tr><td colspan=9 align=center><table width=98% border=0 cellspacing=0 cellpadding=10><tr><br>
<%
ImageFileType = Array("jpg","jpeg","gif")		

	' Display image files 
	Dim x, deleteLink, renameLink, copyLink
	x = 0
	For Each objFiles in objFolder.Files
		For Each i in ImageFileType
			if (objFSO.GetExtensionName(objFiles.name) = i) Then
				'if AllowDeleteImage = 1 Then
				'	deleteLink = "<br><a href=javascript:ConfirmImageDelete('" & (objFiles.name) & "') class=bodylink title=""Delete image: '" & objFiles.name & "'"">Delete</a>"
				'else
			'		deleteLink = ""
			'	End if

			'	if AllowRenameImage = 1 Then
			'		renameLink = "<br><a href=" & ScriptName & "?newimagedir=" & (NewImageDirectory) & "&ToDo=Rename&FromImageDir=1&FileName=" & (objFiles.name) & " class=bodylink title=""Rename image: '" & objFiles.name & "'"">Rename</a>"
			'	else
			'		renameLink = ""
			'	end if

			'	if AllowCopyImage = 1 Then
			'		copyLink = "<br><a href=" & ScriptName & "?newimagedir=" & (NewImageDirectory) & "&ToDo=Copy&FromImageDir=1&FileName=" & (objFiles.name) & " class=bodylink title=""Copy image: '" & objFiles.name & "'"">Copy</a>"
			'	else
			'		copyLink = ""
			'	end if
%>
			<td width=25%><table border=0 cellspacing=0 cellpadding=0 width=100%><tr><td colspan=2 class=body><%=objFiles.name%></td></tr><tr><td width=50><img border=1 src=http://<%=URL%><%=(CurrentImageDirectory)%>/<%=(objFiles.name)%> width=90 height=90>&nbsp;</td><td width=200><a href=javascript:ViewFile('http://<%=URL%><%=(CurrentImageDirectory)%>/<%=(objFiles.name)%>') class=bodylink title="View image: '<%=objFiles.name%>'">View</a><br><a href=javascript:SelectImage("<%=(objFiles.name)%>") class=bodylink title="Insert image: '<%=objFiles.name%>' into your page"><b>Insert</b></a><%=renameLink%><%=copyLink%><%=deleteLink%></td></tr><tr><td colspan=2 class=body><%=objFiles.size%> bytes</td></tr></table></td>
<%
			x = x + 1
				if (x=4) Then
					response.write("</tr><tr>")
					x = 0
				End if
			End if
		next
	next
%>
	</tr></table></td></tr>
	</table>
	</td>
	</tr>
	<tr>
		<td colspan="2"><img src="webedit_images/1x1.gif" width="1" height="10"></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td>
			<table width=98% cellpadding=0 cellspacing=0 border=0>
			<tr>
			<% if AllowUploadImage = 1 Then %><td width=100><input type=submit name="ToDo" value="Upload Image" class="Text90"></td><% end if %>
			<% if AllowCreateImageFolder = 1 Then %><td width=100><input type=submit name="ToDo" value="Create Folder" class="Text90"></td><% end if %>
			<td><input type=button name="cancel" value="Cancel" class="Text70" onClick=javascript:window.close()></td>
			<input type=hidden name="newimagedir" value="<%=NewImageDirectory%>">
			<input type=hidden name="FromImageDir" value="1">
			</tr>
		</td>
  	</tr>
	</table>
	</form>
<%end if
'End Sub ' PrintImageDir%>

<%'******************************new%>


<%
ELSEIF REQUEST.QUERYSTRING("ToDo")="ModifyImage" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richtemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richtemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	

</table>
<script language=javascript>
var myPage = window.opener;

window.onload = setValues;

// var cellBgColor = myTable.selectedTD.bgColor;
var imageWidth = myPage.selectedImage.width;
var imageHeight = myPage.selectedImage.height;
var imageAlign = myPage.selectedImage.align;
var imageBorder = myPage.selectedImage.border;
var imageAltTag = myPage.selectedImage.alt;
var imageHspace = myPage.selectedImage.hspace;
var imageVspace = myPage.selectedImage.vspace;

function setValues() {

	imageForm.image_width.value = imageWidth;
	imageForm.image_height.value = imageHeight;

	if (imageBorder == "") {
		imageBorder = "0"
	}

	imageForm.border.value = imageBorder;
	imageForm.alt_tag.value = imageAltTag;
	imageForm.hspace.value = imageHspace;
	imageForm.vspace.value = imageVspace;
	// tableForm.cell_width.value = cellWidth;
	this.focus();
}

function doModify() {

	var error = 0;
	if (isNaN(imageForm.image_width.value) || imageForm.image_width.value < 0) {
		alert("Image Width must contain a valid, positive number")
		error = 1
		imageForm.image_width.select()
		imageForm.image_width.focus()
	} else if (isNaN(imageForm.image_height.value) || imageForm.image_height.value < 0) {
		alert("Image Height must contain a valid, positive number")
		error = 1
		imageForm.image_height.select()
		imageForm.image_height.focus()
	} else if (isNaN(imageForm.border.value) || imageForm.border.value < 0 || imageForm.border.value == "") {
		alert("Image Border must contain a valid, positive number")
		error = 1
		imageForm.border.select()
		imageForm.border.focus()
	} else if (isNaN(imageForm.hspace.value) || imageForm.hspace.value < 0) {
		alert("Horizontal Spacing must contain a valid, positive number")
		error = 1
		imageForm.hspace.select()
		imageForm.hspace.focus()
	} else if (isNaN(imageForm.vspace.value) || imageForm.vspace.value < 0) {
		alert("Vertical Spacing must contain a valid, positive number")
		error = 1
		imageForm.vspace.select()
		imageForm.vspace.focus()
	}

	if (error != 1) {
        	myPage.selectedImage.width = imageForm.image_width.value
			myPage.selectedImage.height = imageForm.image_height.value
			myPage.selectedImage.alt = imageForm.alt_tag.value
			myPage.selectedImage.border = imageForm.border.value
    
	if (imageForm.hspace.value != "") {
			myPage.selectedImage.hspace = imageForm.hspace.value
	} else {
			myPage.selectedImage.removeAttribute('hspace',0)
	}

	if (imageForm.vspace.value != "") {
			myPage.selectedImage.vspace = imageForm.vspace.value
	} else {
			myPage.selectedImage.removeAttribute('vspace',0)
	}

	if (imageForm.align[imageForm.align.selectedIndex].text != "None") {
       		myPage.selectedImage.align = imageForm.align[imageForm.align.selectedIndex].text
	} else {
       		myPage.selectedImage.removeAttribute('align',0)
	}
        
    window.close()
	}
}

function printAlign() {
	if ((imageAlign != undefined) && (imageAlign != "")) {
		document.write('<option selected>' + imageAlign)
		document.write('<option>None')
	} else {
		document.write('<option selected>None')
	}
}

function printvAlign() {
	if ((imagevAlign != undefined) && (imagevAlign != "")) {
		document.write('<option selected>' + imagevAlign)
		document.write('<option>None')
	} else {
		document.write('<option selected>None')
	}
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				doModify()			
			}
}

document.onkeypress = onkeyup = function () { 				
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;				
	}
};

</script>

<form name=imageForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="_images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Modify Image Properties</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Modify Image&quot; to modify the properties of the selected Image.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Modify Image Properties</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"> </td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">  
		<table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		  <tr>
		    <td class="body" width="80">Alternate Text:</td>
			<td class="body" colspan="3">
			  <input type="text" name="alt_tag" size="50" class="Text220">
			</td>
		  </tr>
		  <tr>
			<td class="body" width="80">Image Width:</td>
			<td class="body">
			  <input type="text" name="image_width" size="3" class="Text50" maxlength="3">
		  </td>
			<td class="body" width="80">Image Height:</td>
			<td class="body">
			  <input type="text" name="image_height" size="3" class="Text50" maxlength="3">
			</td>
		  </tr>
		  <tr>
			<td class="body" width="80">Alignment:</td>
			<td class="body">
			  <SELECT class=text70 name=align>
			    <script>printAlign()</script>
			    <option>Baseline
			    <option>Top
			    <option>Middle
			    <option>Bottom
			    <option>TextTop
			    <option>ABSMiddle
			    <option>ABSBottom
			    <option>Left
			    <option>Right</option>
			  </select>
		  </td>
			<td class="body" width="80">Border:</td>
			<td class="body">
			  <input type="text" name="border" size="3" class="Text50" maxlength="3">
			</td>
		  </tr>
		  <tr>
			<td class="body" width="80">Horizontal Spacing:</td>
			<td class="body">
			  <input type="text" name="hspace" size="3" class="Text50" maxlength="3">
			</td>
			<td class="body" width="80">Vertical Spacing:</td>
			<td class="body">
			  <input type="text" name="vspace" size="3" class="Text50" maxlength="3">
			</td>
		  </tr>
		  
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"> </td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="modifyImage" value="Modify Image" class="Text90" onClick="javascript:doModify();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form>
<br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"> </td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
ELSEIF REQUEST.QUERYSTRING("ToDo")="InsertChars" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richtemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richtemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=JavaScript>
window.onload = this.focus
// var sel = window.opener.document.selection;
// var rng = sel.createRange();
var charToInsert = ""

function insertChar(charHTML) {
	charToInsert = charHTML.innerText
	charForm.char_value.value = charToInsert
}

function doChar() {
	var sel = window.opener.foo.document.selection;
	var rng = sel.createRange();
	rng.pasteHTML(charToInsert)
	self.close();
}


document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				doChar()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=charForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Insert Special Character</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Select the character you require  and click &quot;Insert Character&quot; to insert a special character into your webpage.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Insert Special Character into Webpage</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"> </td>
  </tr>
  <tr>
	<td>&nbsp;</td>

	<td class="body">
	 	  
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		
		
		  <tr>
		    <td class="body" width="80" valign="top">Character:</td>
			<td class="body">
			  <table border="0" cellspacing="0" cellpadding="5" width="160">
				<tr>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&copy;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&reg;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&#153;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&pound;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&#151;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&#133;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&divide;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&aacute;</a></td>
				</tr>
				<tr>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&yen;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&euro;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&#147;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&#148;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&#149;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&para;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&eacute;</a></td>
				  <td class=button><a href="#" onClick=insertChar(this) class=button>&uacute;</a></td>
				</tr>
			  </table>
			  			  </td>
		  </tr>
		  <tr>
		    <td class="body" width="80">
			Character to Insert:</td>
			<td class="body">
			  <input type="text" name="char_value" size="10" class="CharText" maxlength="50">
			</td>
		  </tr>
		  
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"> </td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertForm" value="Insert Character" class="Text120" onClick="javascript:doChar();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"> </td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%
ELSEIF REQUEST.QUERYSTRING("ToDo")="PageProperties" then
%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="/richtemplate/style_richtemplate.css" type="text/css">
<script>
function doPurchase() {
	if (window.opener) {
			window.opener.location = "http://www.richtemplate.com/purchase.html"
			this.close()
			} else {
			document.location = "http://www.richtemplate.com/purchase.html"
	}
}
</script>		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	  
	 <a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	

</table>
<script language=javascript>
var myPage = window.opener;

var pageTitle = myPage.foo.document.title;
var pageBgColor = myPage.foo.document.body.bgColor;
var pageLinkColor = myPage.foo.document.body.link;
var pageTextColor = myPage.foo.document.body.text;
var backgroundImage = myPage.foo.document.body.background;
var metaKeywords = ""
var metaDescription = ""
var oDescription
var oKeywords

var metaData = myPage.foo.document.getElementsByTagName('META')
for (var m = 0; m < metaData.length; m++) {
	if (metaData[m].name.toUpperCase() == "KEYWORDS") {
      metaKeywords = metaData[m].content
	  oKeywords = metaData[m]
	}
	  
	if (metaData[m].name.toUpperCase() == 'DESCRIPTION') {
      metaDescription = metaData[m].content
	  oDescription = metaData[m]
	}

}


window.onload = setValues;

function setValues() {

	pageForm.pagetitle.value = pageTitle;
	pageForm.description.value = metaDescription;
	pageForm.keywords.value = metaKeywords;
	pageForm.bgImage.value = backgroundImage;
	this.focus();
}

function doModify() {
	var bgImage = pageForm.bgImage.value
	var bgcolor = pageForm.bgcolor[pageForm.bgcolor.selectedIndex].text
	var linkcolor = pageForm.linkcolor[pageForm.linkcolor.selectedIndex].text
	var textcolor = pageForm.textcolor[pageForm.textcolor.selectedIndex].text

	if (bgImage != "") { myPage.foo.document.body.background = bgImage } else { myPage.foo.document.body.removeAttribute("background",0) }
	if (bgcolor != "None") { myPage.foo.document.body.bgColor = bgcolor } else { myPage.foo.document.body.removeAttribute("bgColor",0) }
	if (linkcolor != "None") { myPage.foo.document.body.link = linkcolor } else { myPage.foo.document.body.removeAttribute("link",0) }
	if (textcolor != "None") { myPage.foo.document.body.text = textcolor } else { myPage.foo.document.body.removeAttribute("text",0) }

	myPage.foo.document.title = pageForm.pagetitle.value
	
	var oHead = myPage.foo.document.getElementsByTagName('HEAD')

	if (oKeywords != null) {
		oKeywords.content = pageForm.keywords.value
	} else {
		var oMetaKeywords = myPage.foo.document.createElement("META");
		oMetaKeywords.name = "Keywords"
		oMetaKeywords.content = pageForm.keywords.value

		oHead(0).appendChild(oMetaKeywords)
	}

		if (oDescription != null){
			oDescription.content = pageForm.description.value
		} else {
			var oMetaDesc= myPage.foo.document.createElement("META");
			oMetaDesc.name = "Description"
			oMetaDesc.content = pageForm.description.value
			oHead(0).appendChild(oMetaDesc);
		}

	window.close()
}

function printBgColor() {
	if ((pageBgColor != undefined) && (pageBgColor != "")) {
		document.write('<option selected style="BACKGROUND-COLOR: ' + pageBgColor + '">' + pageBgColor)
		document.write('<option>None')
	} else {
		document.write('<option selected>None')
	}
}

function printLinkColor() {
	if ((pageLinkColor != undefined) && (pageLinkColor != "")) {
		document.write('<option selected style="BACKGROUND-COLOR: ' + pageLinkColor + '">' + pageLinkColor)
		document.write('<option>None')
	} else {
		document.write('<option selected>None')
	}
}

function printTextColor() {
	if ((pageTextColor != undefined) && (pageTextColor != "")) {
		document.write('<option selected style="BACKGROUND-COLOR: ' + pageTextColor + '">' + pageTextColor)
		document.write('<option>None')
	} else {
		document.write('<option selected>None')
	}
}

document.onkeydown = function () { 
	if (event.keyCode == 13) {	// ENTER
				doModify()			
	}
};

document.onkeypress = onkeyup = function () { 
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;
	}
};

</script>

<form name=pageForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Modify Page Properties</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Modify Page&quot; to modify the  properties of your page.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Modify Page Properties</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"> </td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
		<table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		  <tr>
			<td class="body" width="100">Page Title:</td>
			<td class="body">
			  <input type="text" name="pagetitle" maxlength="100" class=text220>
			</td>
		  </tr>
		  <tr>
			<td class="body" valign="top">Description:</td>
			<td class="body">
			  <textarea name="description" class="text220" rows="4"></textarea>
			</td>
		  </tr>
		  <tr>
			<td class="body">Keywords:</td>
			<td class="body">
			  <input type="text" name="keywords" maxlength="300" class=text220>
			</td>
		  </tr>
		  <tr>
			<td class="body">Background Image:</td>
			<td class="body">
			  <input type="text" name="bgImage" maxlength="300" class=text220>
			  </td>
		  </tr>
		  
		  <tr>
			<td class="body">Background Color:</td>
			<td class="body">
			  <select class=text70 name=bgcolor>
				<script>printBgColor()</script>
				<option style="BACKGROUND-COLOR: #ff0000">#FF0000
				<option style="BACKGROUND-COLOR: #ffff00">#FFFF00
				<option style="BACKGROUND-COLOR: #00ff00">#00FF00
				<option style="BACKGROUND-COLOR: #00ffff">#00FFFF
				<option style="BACKGROUND-COLOR: #0000ff">#0000FF
				<option style="BACKGROUND-COLOR: #ff00ff">#FF00FF
				<option style="BACKGROUND-COLOR: #ffffff">#FFFFFF
				<option style="BACKGROUND-COLOR: #f5f5f5">#F5F5F5
				<option style="BACKGROUND-COLOR: #dcdcdc">#DCDCDC
				<option style="BACKGROUND-COLOR: #d3d3d3">#D3D3D3
				<option style="BACKGROUND-COLOR: #c0c0c0">#C0C0C0
				<option style="BACKGROUND-COLOR: #a9a9a9">#A9A9A9
				<option style="BACKGROUND-COLOR: #808080">#808080
				<option style="BACKGROUND-COLOR: #696969">#696969
				<option style="BACKGROUND-COLOR: #000000">#000000
				<option style="BACKGROUND-COLOR: #2f4f4f">#2F4F4F
				<option style="BACKGROUND-COLOR: #708090">#708090
				<option style="BACKGROUND-COLOR: #778899">#778899
				<option style="BACKGROUND-COLOR: #4682b4">#4682B4
				<option style="BACKGROUND-COLOR: #4169e1">#4169E1
				<option style="BACKGROUND-COLOR: #6495ed">#6495ED
				<option style="BACKGROUND-COLOR: #b0c4de">#B0C4DE
				<option style="BACKGROUND-COLOR: #7b68ee">#7B68EE
				<option style="BACKGROUND-COLOR: #6a5acd">#6A5ACD
				<option style="BACKGROUND-COLOR: #483d8b">#483D8B
				<option style="BACKGROUND-COLOR: #191970">#191970
				<option style="BACKGROUND-COLOR: #000080">#000080
				<option style="BACKGROUND-COLOR: #00008b">#00008B
				<option style="BACKGROUND-COLOR: #0000cd">#0000CD
				<option style="BACKGROUND-COLOR: #1e90ff">#1E90FF
				<option style="BACKGROUND-COLOR: #00bfff">#00BFFF
				<option style="BACKGROUND-COLOR: #87cefa">#87CEFA
				<option style="BACKGROUND-COLOR: #87ceeb">#87CEEB
				<option style="BACKGROUND-COLOR: #add8e6">#ADD8E6
				<option style="BACKGROUND-COLOR: #b0e0e6">#B0E0E6
				<option style="BACKGROUND-COLOR: #f0ffff">#F0FFFF
				<option style="BACKGROUND-COLOR: #e0ffff">#E0FFFF
				<option style="BACKGROUND-COLOR: #afeeee">#AFEEEE
				<option style="BACKGROUND-COLOR: #00ced1">#00CED1
				<option style="BACKGROUND-COLOR: #5f9ea0">#5F9EA0
				<option style="BACKGROUND-COLOR: #48d1cc">#48D1CC
				<option style="BACKGROUND-COLOR: #00ffff">#00FFFF
				<option style="BACKGROUND-COLOR: #40e0d0">#40E0D0
				<option style="BACKGROUND-COLOR: #20b2aa">#20B2AA
				<option style="BACKGROUND-COLOR: #008b8b">#008B8B
				<option style="BACKGROUND-COLOR: #008080">#008080
				<option style="BACKGROUND-COLOR: #7fffd4">#7FFFD4
				<option style="BACKGROUND-COLOR: #66cdaa">#66CDAA
				<option style="BACKGROUND-COLOR: #8fbc8f">#8FBC8F
				<option style="BACKGROUND-COLOR: #3cb371">#3CB371
				<option style="BACKGROUND-COLOR: #2e8b57">#2E8B57
				<option style="BACKGROUND-COLOR: #006400">#006400
				<option style="BACKGROUND-COLOR: #008000">#008000
				<option style="BACKGROUND-COLOR: #228b22">#228B22
				<option style="BACKGROUND-COLOR: #32cd32">#32CD32
				<option style="BACKGROUND-COLOR: #00ff00">#00FF00
				<option style="BACKGROUND-COLOR: #7fff00">#7FFF00
				<option style="BACKGROUND-COLOR: #7cfc00">#7CFC00
				<option style="BACKGROUND-COLOR: #adff2f">#ADFF2F
				<option style="BACKGROUND-COLOR: #98fb98">#98FB98
				<option style="BACKGROUND-COLOR: #90ee90">#90EE90
				<option style="BACKGROUND-COLOR: #00ff7f">#00FF7F
				<option style="BACKGROUND-COLOR: #00fa9a">#00FA9A
				<option style="BACKGROUND-COLOR: #556b2f">#556B2F
				<option style="BACKGROUND-COLOR: #6b8e23">#6B8E23
				<option style="BACKGROUND-COLOR: #808000">#808000
				<option style="BACKGROUND-COLOR: #bdb76b">#BDB76B
				<option style="BACKGROUND-COLOR: #b8860b">#B8860B
				<option style="BACKGROUND-COLOR: #daa520">#DAA520
				<option style="BACKGROUND-COLOR: #ffd700">#FFD700
				<option style="BACKGROUND-COLOR: #f0e68c">#F0E68C
				<option style="BACKGROUND-COLOR: #eee8aa">#EEE8AA
				<option style="BACKGROUND-COLOR: #ffebcd">#FFEBCD
				<option style="BACKGROUND-COLOR: #ffe4b5">#FFE4B5
				<option style="BACKGROUND-COLOR: #f5deb3">#F5DEB3
				<option style="BACKGROUND-COLOR: #ffdead">#FFDEAD
				<option style="BACKGROUND-COLOR: #deb887">#DEB887
				<option style="BACKGROUND-COLOR: #d2b48c">#D2B48C
				<option style="BACKGROUND-COLOR: #bc8f8f">#BC8F8F
				<option style="BACKGROUND-COLOR: #a0522d">#A0522D
				<option style="BACKGROUND-COLOR: #8b4513">#8B4513
				<option style="BACKGROUND-COLOR: #d2691e">#D2691E
				<option style="BACKGROUND-COLOR: #cd853f">#CD853F
				<option style="BACKGROUND-COLOR: #f4a460">#F4A460
				<option style="BACKGROUND-COLOR: #8b0000">#8B0000
				<option style="BACKGROUND-COLOR: #800000">#800000
				<option style="BACKGROUND-COLOR: #a52a2a">#A52A2A
				<option style="BACKGROUND-COLOR: #b22222">#B22222
				<option style="BACKGROUND-COLOR: #cd5c5c">#CD5C5C
				<option style="BACKGROUND-COLOR: #f08080">#F08080
				<option style="BACKGROUND-COLOR: #fa8072">#FA8072
				<option style="BACKGROUND-COLOR: #e9967a">#E9967A
				<option style="BACKGROUND-COLOR: #ffa07a">#FFA07A
				<option style="BACKGROUND-COLOR: #ff7f50">#FF7F50
				<option style="BACKGROUND-COLOR: #ff6347">#FF6347
				<option style="BACKGROUND-COLOR: #ff8c00">#FF8C00
				<option style="BACKGROUND-COLOR: #ffa500">#FFA500
				<option style="BACKGROUND-COLOR: #ff4500">#FF4500
				<option style="BACKGROUND-COLOR: #dc143c">#DC143C
				<option style="BACKGROUND-COLOR: #ff0000">#FF0000
				<option style="BACKGROUND-COLOR: #ff1493">#FF1493
				<option style="BACKGROUND-COLOR: #ff00ff">#FF00FF
				<option style="BACKGROUND-COLOR: #ff69b4">#FF69B4
				<option style="BACKGROUND-COLOR: #ffb6c1">#FFB6C1
				<option style="BACKGROUND-COLOR: #ffc0cb">#FFC0CB
				<option style="BACKGROUND-COLOR: #db7093">#DB7093
				<option style="BACKGROUND-COLOR: #c71585">#C71585
				<option style="BACKGROUND-COLOR: #800080">#800080
				<option style="BACKGROUND-COLOR: #8b008b">#8B008B
				<option style="BACKGROUND-COLOR: #9370db">#9370DB
				<option style="BACKGROUND-COLOR: #8a2be2">#8A2BE2
				<option style="BACKGROUND-COLOR: #4b0082">#4B0082
				<option style="BACKGROUND-COLOR: #9400d3">#9400D3
				<option style="BACKGROUND-COLOR: #9932cc">#9932CC
				<option style="BACKGROUND-COLOR: #ba55d3">#BA55D3
				<option style="BACKGROUND-COLOR: #da70d6">#DA70D6
				<option style="BACKGROUND-COLOR: #ee82ee">#EE82EE
				<option style="BACKGROUND-COLOR: #dda0dd">#DDA0DD
				<option style="BACKGROUND-COLOR: #d8bfd8">#D8BFD8
				<option style="BACKGROUND-COLOR: #e6e6fa">#E6E6FA
				<option style="BACKGROUND-COLOR: #f8f8ff">#F8F8FF
				<option style="BACKGROUND-COLOR: #f0f8ff">#F0F8FF
				<option style="BACKGROUND-COLOR: #f5fffa">#F5FFFA
				<option style="BACKGROUND-COLOR: #f0fff0">#F0FFF0
				<option style="BACKGROUND-COLOR: #fafad2">#FAFAD2
				<option style="BACKGROUND-COLOR: #fffacd">#FFFACD
				<option style="BACKGROUND-COLOR: #fff8dc">#FFF8DC
				<option style="BACKGROUND-COLOR: #ffffe0">#FFFFE0
				<option style="BACKGROUND-COLOR: #fffff0">#FFFFF0
				<option style="BACKGROUND-COLOR: #fffaf0">#FFFAF0
				<option style="BACKGROUND-COLOR: #faf0e6">#FAF0E6
				<option style="BACKGROUND-COLOR: #fdf5e6">#FDF5E6
				<option style="BACKGROUND-COLOR: #faebd7">#FAEBD7
				<option style="BACKGROUND-COLOR: #ffe4c4">#FFE4C4
				<option style="BACKGROUND-COLOR: #ffdab9">#FFDAB9
				<option style="BACKGROUND-COLOR: #ffefd5">#FFEFD5
				<option style="BACKGROUND-COLOR: #fff5ee">#FFF5EE
				<option style="BACKGROUND-COLOR: #fff0f5">#FFF0F5
				<option style="BACKGROUND-COLOR: #ffe4e1">#FFE4E1
				<option style="BACKGROUND-COLOR: #fffafa">#FFFAFA</option>
			  </select>
			   
			</td>
		  </tr>
		
		
		  <tr>
			<td class="body">Text Color:</td>
			<td class="body">
			  <select class=text70 name=textcolor>
				 
				<script>printTextColor()</script>
				
				<option style="BACKGROUND-COLOR: #ff0000">#FF0000
				
				<option style="BACKGROUND-COLOR: #ffff00">#FFFF00
				
				<option style="BACKGROUND-COLOR: #00ff00">#00FF00
				
				<option style="BACKGROUND-COLOR: #00ffff">#00FFFF
				
				<option style="BACKGROUND-COLOR: #0000ff">#0000FF
				
				<option style="BACKGROUND-COLOR: #ff00ff">#FF00FF
				
				<option style="BACKGROUND-COLOR: #ffffff">#FFFFFF
				
				<option style="BACKGROUND-COLOR: #f5f5f5">#F5F5F5
				
				<option style="BACKGROUND-COLOR: #dcdcdc">#DCDCDC
				
				<option style="BACKGROUND-COLOR: #d3d3d3">#D3D3D3
				
				<option style="BACKGROUND-COLOR: #c0c0c0">#C0C0C0
				
				<option style="BACKGROUND-COLOR: #a9a9a9">#A9A9A9
				
				<option style="BACKGROUND-COLOR: #808080">#808080
				
				<option style="BACKGROUND-COLOR: #696969">#696969
				
				<option style="BACKGROUND-COLOR: #000000">#000000
				
				<option style="BACKGROUND-COLOR: #2f4f4f">#2F4F4F
				
				<option style="BACKGROUND-COLOR: #708090">#708090
				
				<option style="BACKGROUND-COLOR: #778899">#778899
				
				<option style="BACKGROUND-COLOR: #4682b4">#4682B4
				
				<option style="BACKGROUND-COLOR: #4169e1">#4169E1
				
				<option style="BACKGROUND-COLOR: #6495ed">#6495ED
				
				<option style="BACKGROUND-COLOR: #b0c4de">#B0C4DE
				
				<option style="BACKGROUND-COLOR: #7b68ee">#7B68EE
				
				<option style="BACKGROUND-COLOR: #6a5acd">#6A5ACD
				
				<option style="BACKGROUND-COLOR: #483d8b">#483D8B
				
				<option style="BACKGROUND-COLOR: #191970">#191970
				
				<option style="BACKGROUND-COLOR: #000080">#000080
				
				<option style="BACKGROUND-COLOR: #00008b">#00008B
				
				<option style="BACKGROUND-COLOR: #0000cd">#0000CD
				
				<option style="BACKGROUND-COLOR: #1e90ff">#1E90FF
				
				<option style="BACKGROUND-COLOR: #00bfff">#00BFFF
				
				<option style="BACKGROUND-COLOR: #87cefa">#87CEFA
				
				<option style="BACKGROUND-COLOR: #87ceeb">#87CEEB
				
				<option style="BACKGROUND-COLOR: #add8e6">#ADD8E6
				
				<option style="BACKGROUND-COLOR: #b0e0e6">#B0E0E6
				
				<option style="BACKGROUND-COLOR: #f0ffff">#F0FFFF
				
				<option style="BACKGROUND-COLOR: #e0ffff">#E0FFFF
				
				<option style="BACKGROUND-COLOR: #afeeee">#AFEEEE
				
				<option style="BACKGROUND-COLOR: #00ced1">#00CED1
				
				<option style="BACKGROUND-COLOR: #5f9ea0">#5F9EA0
				
				<option style="BACKGROUND-COLOR: #48d1cc">#48D1CC
				
				<option style="BACKGROUND-COLOR: #00ffff">#00FFFF
				
				<option style="BACKGROUND-COLOR: #40e0d0">#40E0D0
				
				<option style="BACKGROUND-COLOR: #20b2aa">#20B2AA
				
				<option style="BACKGROUND-COLOR: #008b8b">#008B8B
				
				<option style="BACKGROUND-COLOR: #008080">#008080
				
				<option style="BACKGROUND-COLOR: #7fffd4">#7FFFD4
				
				<option style="BACKGROUND-COLOR: #66cdaa">#66CDAA
				
				<option style="BACKGROUND-COLOR: #8fbc8f">#8FBC8F
				
				<option style="BACKGROUND-COLOR: #3cb371">#3CB371
				
				<option style="BACKGROUND-COLOR: #2e8b57">#2E8B57
				
				<option style="BACKGROUND-COLOR: #006400">#006400
				
				<option style="BACKGROUND-COLOR: #008000">#008000
				
				<option style="BACKGROUND-COLOR: #228b22">#228B22
				
				<option style="BACKGROUND-COLOR: #32cd32">#32CD32
				
				<option style="BACKGROUND-COLOR: #00ff00">#00FF00
				
				<option style="BACKGROUND-COLOR: #7fff00">#7FFF00
				
				<option style="BACKGROUND-COLOR: #7cfc00">#7CFC00
				
				<option style="BACKGROUND-COLOR: #adff2f">#ADFF2F
				
				<option style="BACKGROUND-COLOR: #98fb98">#98FB98
				
				<option style="BACKGROUND-COLOR: #90ee90">#90EE90
				
				<option style="BACKGROUND-COLOR: #00ff7f">#00FF7F
				
				<option style="BACKGROUND-COLOR: #00fa9a">#00FA9A
				
				<option style="BACKGROUND-COLOR: #556b2f">#556B2F
				
				<option style="BACKGROUND-COLOR: #6b8e23">#6B8E23
				
				<option style="BACKGROUND-COLOR: #808000">#808000
				
				<option style="BACKGROUND-COLOR: #bdb76b">#BDB76B
				
				<option style="BACKGROUND-COLOR: #b8860b">#B8860B
				
				<option style="BACKGROUND-COLOR: #daa520">#DAA520
				
				<option style="BACKGROUND-COLOR: #ffd700">#FFD700
				
				<option style="BACKGROUND-COLOR: #f0e68c">#F0E68C
				
				<option style="BACKGROUND-COLOR: #eee8aa">#EEE8AA
				
				<option style="BACKGROUND-COLOR: #ffebcd">#FFEBCD
				
				<option style="BACKGROUND-COLOR: #ffe4b5">#FFE4B5
				
				<option style="BACKGROUND-COLOR: #f5deb3">#F5DEB3
				
				<option style="BACKGROUND-COLOR: #ffdead">#FFDEAD
				
				<option style="BACKGROUND-COLOR: #deb887">#DEB887
				
				<option style="BACKGROUND-COLOR: #d2b48c">#D2B48C
				
				<option style="BACKGROUND-COLOR: #bc8f8f">#BC8F8F
				
				<option style="BACKGROUND-COLOR: #a0522d">#A0522D
				
				<option style="BACKGROUND-COLOR: #8b4513">#8B4513
				
				<option style="BACKGROUND-COLOR: #d2691e">#D2691E
				
				<option style="BACKGROUND-COLOR: #cd853f">#CD853F
				
				<option style="BACKGROUND-COLOR: #f4a460">#F4A460
				
				<option style="BACKGROUND-COLOR: #8b0000">#8B0000
				
				<option style="BACKGROUND-COLOR: #800000">#800000
				
				<option style="BACKGROUND-COLOR: #a52a2a">#A52A2A
				
				<option style="BACKGROUND-COLOR: #b22222">#B22222
				
				<option style="BACKGROUND-COLOR: #cd5c5c">#CD5C5C
				
				<option style="BACKGROUND-COLOR: #f08080">#F08080
				
				<option style="BACKGROUND-COLOR: #fa8072">#FA8072
				
				<option style="BACKGROUND-COLOR: #e9967a">#E9967A
				
				<option style="BACKGROUND-COLOR: #ffa07a">#FFA07A
				
				<option style="BACKGROUND-COLOR: #ff7f50">#FF7F50
				
				<option style="BACKGROUND-COLOR: #ff6347">#FF6347
				
				<option style="BACKGROUND-COLOR: #ff8c00">#FF8C00
				
				<option style="BACKGROUND-COLOR: #ffa500">#FFA500
				
				<option style="BACKGROUND-COLOR: #ff4500">#FF4500
				
				<option style="BACKGROUND-COLOR: #dc143c">#DC143C
				
				<option style="BACKGROUND-COLOR: #ff0000">#FF0000
				
				<option style="BACKGROUND-COLOR: #ff1493">#FF1493
				
				<option style="BACKGROUND-COLOR: #ff00ff">#FF00FF
				
				<option style="BACKGROUND-COLOR: #ff69b4">#FF69B4
				
				<option style="BACKGROUND-COLOR: #ffb6c1">#FFB6C1
				
				<option style="BACKGROUND-COLOR: #ffc0cb">#FFC0CB
				
				<option style="BACKGROUND-COLOR: #db7093">#DB7093
				
				<option style="BACKGROUND-COLOR: #c71585">#C71585
				
				<option style="BACKGROUND-COLOR: #800080">#800080
				
				<option style="BACKGROUND-COLOR: #8b008b">#8B008B
				
				<option style="BACKGROUND-COLOR: #9370db">#9370DB
				
				<option style="BACKGROUND-COLOR: #8a2be2">#8A2BE2
				
				<option style="BACKGROUND-COLOR: #4b0082">#4B0082
				
				<option style="BACKGROUND-COLOR: #9400d3">#9400D3
				
				<option style="BACKGROUND-COLOR: #9932cc">#9932CC
				
				<option style="BACKGROUND-COLOR: #ba55d3">#BA55D3
				
				<option style="BACKGROUND-COLOR: #da70d6">#DA70D6
				
				<option style="BACKGROUND-COLOR: #ee82ee">#EE82EE
				
				<option style="BACKGROUND-COLOR: #dda0dd">#DDA0DD
				
				<option style="BACKGROUND-COLOR: #d8bfd8">#D8BFD8
				
				<option style="BACKGROUND-COLOR: #e6e6fa">#E6E6FA
				
				<option style="BACKGROUND-COLOR: #f8f8ff">#F8F8FF
				
				<option style="BACKGROUND-COLOR: #f0f8ff">#F0F8FF
				
				<option style="BACKGROUND-COLOR: #f5fffa">#F5FFFA
				
				<option style="BACKGROUND-COLOR: #f0fff0">#F0FFF0
				
				<option style="BACKGROUND-COLOR: #fafad2">#FAFAD2
				
				<option style="BACKGROUND-COLOR: #fffacd">#FFFACD
				
				<option style="BACKGROUND-COLOR: #fff8dc">#FFF8DC
				
				<option style="BACKGROUND-COLOR: #ffffe0">#FFFFE0
				
				<option style="BACKGROUND-COLOR: #fffff0">#FFFFF0
				
				<option style="BACKGROUND-COLOR: #fffaf0">#FFFAF0
				
				<option style="BACKGROUND-COLOR: #faf0e6">#FAF0E6
				
				<option style="BACKGROUND-COLOR: #fdf5e6">#FDF5E6
				
				<option style="BACKGROUND-COLOR: #faebd7">#FAEBD7
				
				<option style="BACKGROUND-COLOR: #ffe4c4">#FFE4C4
				
				<option style="BACKGROUND-COLOR: #ffdab9">#FFDAB9
				
				<option style="BACKGROUND-COLOR: #ffefd5">#FFEFD5
				
				<option style="BACKGROUND-COLOR: #fff5ee">#FFF5EE
				
				<option style="BACKGROUND-COLOR: #fff0f5">#FFF0F5
				
				<option style="BACKGROUND-COLOR: #ffe4e1">#FFE4E1
				
				<option style="BACKGROUND-COLOR: #fffafa">#FFFAFA</option>
			  </select>
			</td>
		  </tr>
		  <tr>
			<td class="body">Link Color:</td>
			<td class="body">
			  <select class=text70 name=linkcolor>
				<script>printLinkColor()</script>
				<option style="BACKGROUND-COLOR: #ff0000">#FF0000
				<option style="BACKGROUND-COLOR: #ffff00">#FFFF00
				<option style="BACKGROUND-COLOR: #00ff00">#00FF00
				<option style="BACKGROUND-COLOR: #00ffff">#00FFFF
				<option style="BACKGROUND-COLOR: #0000ff">#0000FF
				<option style="BACKGROUND-COLOR: #ff00ff">#FF00FF
				<option style="BACKGROUND-COLOR: #ffffff">#FFFFFF
				<option style="BACKGROUND-COLOR: #f5f5f5">#F5F5F5
				<option style="BACKGROUND-COLOR: #dcdcdc">#DCDCDC
				<option style="BACKGROUND-COLOR: #d3d3d3">#D3D3D3
				<option style="BACKGROUND-COLOR: #c0c0c0">#C0C0C0
				<option style="BACKGROUND-COLOR: #a9a9a9">#A9A9A9
				<option style="BACKGROUND-COLOR: #808080">#808080
				<option style="BACKGROUND-COLOR: #696969">#696969
				<option style="BACKGROUND-COLOR: #000000">#000000
				<option style="BACKGROUND-COLOR: #2f4f4f">#2F4F4F
				<option style="BACKGROUND-COLOR: #708090">#708090
				<option style="BACKGROUND-COLOR: #778899">#778899
				<option style="BACKGROUND-COLOR: #4682b4">#4682B4
				<option style="BACKGROUND-COLOR: #4169e1">#4169E1
				<option style="BACKGROUND-COLOR: #6495ed">#6495ED
				<option style="BACKGROUND-COLOR: #b0c4de">#B0C4DE
				<option style="BACKGROUND-COLOR: #7b68ee">#7B68EE
				<option style="BACKGROUND-COLOR: #6a5acd">#6A5ACD
				<option style="BACKGROUND-COLOR: #483d8b">#483D8B
				<option style="BACKGROUND-COLOR: #191970">#191970
				<option style="BACKGROUND-COLOR: #000080">#000080
				<option style="BACKGROUND-COLOR: #00008b">#00008B
				<option style="BACKGROUND-COLOR: #0000cd">#0000CD
				<option style="BACKGROUND-COLOR: #1e90ff">#1E90FF
				<option style="BACKGROUND-COLOR: #00bfff">#00BFFF
				<option style="BACKGROUND-COLOR: #87cefa">#87CEFA
				<option style="BACKGROUND-COLOR: #87ceeb">#87CEEB
				<option style="BACKGROUND-COLOR: #add8e6">#ADD8E6
				<option style="BACKGROUND-COLOR: #b0e0e6">#B0E0E6
				<option style="BACKGROUND-COLOR: #f0ffff">#F0FFFF
				<option style="BACKGROUND-COLOR: #e0ffff">#E0FFFF
				<option style="BACKGROUND-COLOR: #afeeee">#AFEEEE
				<option style="BACKGROUND-COLOR: #00ced1">#00CED1
				<option style="BACKGROUND-COLOR: #5f9ea0">#5F9EA0
				<option style="BACKGROUND-COLOR: #48d1cc">#48D1CC
				<option style="BACKGROUND-COLOR: #00ffff">#00FFFF
				<option style="BACKGROUND-COLOR: #40e0d0">#40E0D0
				<option style="BACKGROUND-COLOR: #20b2aa">#20B2AA
				<option style="BACKGROUND-COLOR: #008b8b">#008B8B
				<option style="BACKGROUND-COLOR: #008080">#008080
				<option style="BACKGROUND-COLOR: #7fffd4">#7FFFD4
				<option style="BACKGROUND-COLOR: #66cdaa">#66CDAA
				<option style="BACKGROUND-COLOR: #8fbc8f">#8FBC8F
				<option style="BACKGROUND-COLOR: #3cb371">#3CB371
				<option style="BACKGROUND-COLOR: #2e8b57">#2E8B57
				<option style="BACKGROUND-COLOR: #006400">#006400
				<option style="BACKGROUND-COLOR: #008000">#008000
				<option style="BACKGROUND-COLOR: #228b22">#228B22
				<option style="BACKGROUND-COLOR: #32cd32">#32CD32
				<option style="BACKGROUND-COLOR: #00ff00">#00FF00
				<option style="BACKGROUND-COLOR: #7fff00">#7FFF00
				<option style="BACKGROUND-COLOR: #7cfc00">#7CFC00
				<option style="BACKGROUND-COLOR: #adff2f">#ADFF2F
				<option style="BACKGROUND-COLOR: #98fb98">#98FB98
				<option style="BACKGROUND-COLOR: #90ee90">#90EE90
				<option style="BACKGROUND-COLOR: #00ff7f">#00FF7F
				<option style="BACKGROUND-COLOR: #00fa9a">#00FA9A
				<option style="BACKGROUND-COLOR: #556b2f">#556B2F
				<option style="BACKGROUND-COLOR: #6b8e23">#6B8E23
				<option style="BACKGROUND-COLOR: #808000">#808000
				<option style="BACKGROUND-COLOR: #bdb76b">#BDB76B
				<option style="BACKGROUND-COLOR: #b8860b">#B8860B
				<option style="BACKGROUND-COLOR: #daa520">#DAA520
				<option style="BACKGROUND-COLOR: #ffd700">#FFD700
				<option style="BACKGROUND-COLOR: #f0e68c">#F0E68C
				<option style="BACKGROUND-COLOR: #eee8aa">#EEE8AA
				<option style="BACKGROUND-COLOR: #ffebcd">#FFEBCD
				<option style="BACKGROUND-COLOR: #ffe4b5">#FFE4B5
				<option style="BACKGROUND-COLOR: #f5deb3">#F5DEB3
				<option style="BACKGROUND-COLOR: #ffdead">#FFDEAD
				<option style="BACKGROUND-COLOR: #deb887">#DEB887
				<option style="BACKGROUND-COLOR: #d2b48c">#D2B48C
				<option style="BACKGROUND-COLOR: #bc8f8f">#BC8F8F
				<option style="BACKGROUND-COLOR: #a0522d">#A0522D
				<option style="BACKGROUND-COLOR: #8b4513">#8B4513
				<option style="BACKGROUND-COLOR: #d2691e">#D2691E
				<option style="BACKGROUND-COLOR: #cd853f">#CD853F
				<option style="BACKGROUND-COLOR: #f4a460">#F4A460
				<option style="BACKGROUND-COLOR: #8b0000">#8B0000
				<option style="BACKGROUND-COLOR: #800000">#800000
				<option style="BACKGROUND-COLOR: #a52a2a">#A52A2A
				<option style="BACKGROUND-COLOR: #b22222">#B22222
				<option style="BACKGROUND-COLOR: #cd5c5c">#CD5C5C
				<option style="BACKGROUND-COLOR: #f08080">#F08080
				<option style="BACKGROUND-COLOR: #fa8072">#FA8072
				<option style="BACKGROUND-COLOR: #e9967a">#E9967A
				<option style="BACKGROUND-COLOR: #ffa07a">#FFA07A
				<option style="BACKGROUND-COLOR: #ff7f50">#FF7F50
				<option style="BACKGROUND-COLOR: #ff6347">#FF6347
				<option style="BACKGROUND-COLOR: #ff8c00">#FF8C00
				<option style="BACKGROUND-COLOR: #ffa500">#FFA500
				<option style="BACKGROUND-COLOR: #ff4500">#FF4500
				<option style="BACKGROUND-COLOR: #dc143c">#DC143C
				<option style="BACKGROUND-COLOR: #ff0000">#FF0000
				<option style="BACKGROUND-COLOR: #ff1493">#FF1493
				<option style="BACKGROUND-COLOR: #ff00ff">#FF00FF
				<option style="BACKGROUND-COLOR: #ff69b4">#FF69B4
				<option style="BACKGROUND-COLOR: #ffb6c1">#FFB6C1
				<option style="BACKGROUND-COLOR: #ffc0cb">#FFC0CB
				<option style="BACKGROUND-COLOR: #db7093">#DB7093
				<option style="BACKGROUND-COLOR: #c71585">#C71585
				<option style="BACKGROUND-COLOR: #800080">#800080
				<option style="BACKGROUND-COLOR: #8b008b">#8B008B
				<option style="BACKGROUND-COLOR: #9370db">#9370DB
				<option style="BACKGROUND-COLOR: #8a2be2">#8A2BE2
				<option style="BACKGROUND-COLOR: #4b0082">#4B0082
				<option style="BACKGROUND-COLOR: #9400d3">#9400D3
				<option style="BACKGROUND-COLOR: #9932cc">#9932CC
				<option style="BACKGROUND-COLOR: #ba55d3">#BA55D3
				<option style="BACKGROUND-COLOR: #da70d6">#DA70D6
				<option style="BACKGROUND-COLOR: #ee82ee">#EE82EE
				<option style="BACKGROUND-COLOR: #dda0dd">#DDA0DD
				<option style="BACKGROUND-COLOR: #d8bfd8">#D8BFD8
				<option style="BACKGROUND-COLOR: #e6e6fa">#E6E6FA
				<option style="BACKGROUND-COLOR: #f8f8ff">#F8F8FF
				<option style="BACKGROUND-COLOR: #f0f8ff">#F0F8FF
				<option style="BACKGROUND-COLOR: #f5fffa">#F5FFFA
				<option style="BACKGROUND-COLOR: #f0fff0">#F0FFF0
				<option style="BACKGROUND-COLOR: #fafad2">#FAFAD2
				<option style="BACKGROUND-COLOR: #fffacd">#FFFACD
				<option style="BACKGROUND-COLOR: #fff8dc">#FFF8DC
				<option style="BACKGROUND-COLOR: #ffffe0">#FFFFE0
				<option style="BACKGROUND-COLOR: #fffff0">#FFFFF0
				<option style="BACKGROUND-COLOR: #fffaf0">#FFFAF0
				<option style="BACKGROUND-COLOR: #faf0e6">#FAF0E6
				<option style="BACKGROUND-COLOR: #fdf5e6">#FDF5E6
				<option style="BACKGROUND-COLOR: #faebd7">#FAEBD7
				<option style="BACKGROUND-COLOR: #ffe4c4">#FFE4C4
				<option style="BACKGROUND-COLOR: #ffdab9">#FFDAB9
				<option style="BACKGROUND-COLOR: #ffefd5">#FFEFD5
				<option style="BACKGROUND-COLOR: #fff5ee">#FFF5EE
				<option style="BACKGROUND-COLOR: #fff0f5">#FFF0F5
				<option style="BACKGROUND-COLOR: #ffe4e1">#FFE4E1
				<option style="BACKGROUND-COLOR: #fffafa">#FFFAFA</option>
			  </select>
			</td>
		  </tr>
		  
		  
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"> </td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="modifyPage" value="Modify Page" class="Text90" onClick="javascript:doModify();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form>
<br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"> </td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%ELSEIF REQUEST.QUERYSTRING("ToDo")="ModifyTextField" then%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.asp','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="style_richtemplate.css" type="text/css">
		
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">
	 
	<a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr> </td>
  </tr>
	

</table>
<script language=JavaScript>
var myPage = window.opener;
window.onload = setValues;

var textName = myPage.selectedTextField.name;
var textWidth = myPage.selectedTextField.size;
var textMax = myPage.selectedTextField.maxLength;
var textValue = myPage.selectedTextField.value;
var textType = myPage.selectedTextField.type;
var textClass = myPage.selectedTextField.className;

function setValues() {

	if (textMax == "2147483647") {
		textMax = ""
	}

	if (textWidth == "0") {
		textWidth = ""
	}

	if (textClass != "") {
		textClass = " class=" + textClass
	}

	textForm.text_max.value = textMax;
	textForm.text_value.value = textValue;
	textForm.text_name.value = textName;
	textForm.text_width.value = textWidth;
	this.focus();
}

function doModify() {
	var sel = window.opener.document.selection;
		if (sel!=null) {
			var rng = sel.createRange();
		}

		name = document.textForm.text_name.value
		width = document.textForm.text_width.value
		max = document.textForm.text_max.value
		value = document.textForm.text_value.value
		type = document.textForm.text_type[textForm.text_type.selectedIndex].text

		error = 0
		if (isNaN(width) || width < 0) {
				alert("Character Width must contain a valid, positive number")
				error = 1
				textForm.text_width.select()
				textForm.text_width.focus()
		} else if (isNaN(max) || max < 0) {
				alert("Maximum Characters must contain a valid, positive number")
				error = 1
				textForm.text_max.select()
				textForm.text_max.focus()
		}

		if (error != 1) {
				if (value != "") {
					value = ' value="' + value + '"'
				} else {
					value = ""
				}

				if (name != "") {
					name = ' name="' + name + '"'
				} else {
					name = ""
				}

				if (width != "") {
					width = ' size="' + width + '"'
				} else {
					width = ""
				}

				if (max != "") {
					max = ' maxlength="' + max + '"'
				} else {
					max = ""
				}

        			HTMLTextField = '<input type="' + type + '"' + name + value + width + max + textClass + '>'
         			myPage.selectedTextField.outerHTML = HTMLTextField
		}
    
    window.close()
}

function printType() {
	if ((textType != undefined) && (textType != "")) {
		document.write('<option selected>' + textType)
		document.write('<option>Text')
		document.write('<option>Password')
	}
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				doModify()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=textForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Modify Text Field</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Modify Text Field&quot; to modify the selected Text Field.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Modify Text Field</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		
		
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="text_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="text_value" size="10" class="Text150">
			</td>
		  </tr>
		  <tr>
			<td class="body" width="80">Character Width:</td>
			<td class="body">
			  <input type="text" name="text_width" size="3" class="Text50" maxlength="3">
			</td>
			<td class="body" width="80">Maximum Characters:</td>
			<td class="body">
			  <input type="text" name="text_max" size="3" class="Text50" maxlength="3">
			</td>
		  </tr>
		  <tr>
			<td class="body" width="80">Type</td>
			<td class="body">
			  <select name="text_type" class=text70>
			    <script>printType()</script>
			  </select>
			</td>
			<td class="body" width="80">&nbsp;</td>
			<td class="body">&nbsp;</td>
		  </tr>
		  
		  
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertTextField" value="Modify Text Field" class="Text90" onClick="javascript:doModify();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>

<%

ELSEIF REQUEST.QUERYSTRING("ToDo")="ModifyTextField" then%>


<script>
function showHelp() {
			var leftPos = (screen.availWidth-500) / 2
			var topPos = (screen.availHeight-400) / 2 
	 		showHelpWin = window.open('showhelp.aspl','','width=500,height=400,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
}
</script>
<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="style_richtemplate.css" type="text/css">
	
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">

	<a href="javascript:showHelp()"><img src="images/button_help.gif" width="60" height="20" alt="Help" border="0" hspace="5"></a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=JavaScript>
var myPage = window.opener;
window.onload = setValues;

var textName = myPage.selectedTextArea.name;
var textWidth = myPage.selectedTextArea.cols;
var textLines = myPage.selectedTextArea.rows;
var textValue = myPage.selectedTextArea.value;
var textClass = myPage.selectedTextArea.className;

function setValues() {

	// if (textMax == "2147483647") {
	// 	textMax = ""
	// }

	// if (textWidth == "0") {
	//	textWidth = ""
	// }

	if (textClass != "") {
		textClass = " class=" + textClass
	}

	textForm.text_lines.value = textLines;
	textForm.text_value.value = textValue;
	textForm.text_name.value = textName;
	textForm.text_width.value = textWidth;
	this.focus();
}

function doModify() {
	var sel = window.opener.document.selection;
		if (sel!=null) {
			var rng = sel.createRange();
		}

		name = document.textForm.text_name.value
		width = document.textForm.text_width.value
		rows = document.textForm.text_lines.value
		value = document.textForm.text_value.value

		error = 0
		if (isNaN(width) || width < 0) {
				alert("Character Width must contain a valid, positive number")
				error = 1
				textForm.text_width.select()
				textForm.text_width.focus()
		} else if (isNaN(rows) || rows < 0) {
				alert("Lines must contain a valid, positive number")
				error = 1
				textForm.text_lines.select()
				textForm.text_lines.focus()
		}

		if (error != 1) {
				if (value != "") {
					value =  value
				} else {
					value = ""
				}

				if (name != "") {
					name = ' name="' + name + '"'
				} else {
					name = ""
				}

				if (width != "") {
					width = ' cols="' + width + '"'
				} else {
					width = ""
				}

				if (rows != "") {
					rows = ' rows="' + rows + '"'
				} else {
					rows = ""
				}

        			HTMLTextField = '<textarea' + name + width + rows + '>' + value + '</textarea>'
         			myPage.selectedTextArea.outerHTML = HTMLTextField
		}
    
    window.close()
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				doModify()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=textForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Modify Text Area</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;modify Text Area&quot; to modify the selected Text Area.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Modify Text Area</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		
		
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="text_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="text_value" size="10" class="Text150">
			</td>
		  </tr>
		  <tr>
			<td class="body" width="80">Character Width:</td>
			<td class="body">
			  <input type="text" name="text_width" size="3" class="Text50" maxlength="3">
			</td>
			<td class="body" width="80">Lines:</td>
			<td class="body">
			  <input type="text" name="text_lines" size="3" class="Text50" maxlength="3">
			</td>
		  </tr>
		  
		  
		  
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertTextField" value="Modify Text Area" class="Text90" onClick="javascript:doModify();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%ELSEIF REQUEST.QUERYSTRING("ToDo")="ModifyHidden" then%>


<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="style_richtemplate.css" type="text/css">
	
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">

	<a href="javascript:showHelp()"><img src="images/button_help.gif" width="60" height="20" alt="Help" border="0" hspace="5"></a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=JavaScript>
var myPage = window.opener;
window.onload = setValues;

var hiddenName = myPage.selectedHidden.name;
var hiddenValue = myPage.selectedHidden.value;

function setValues() {

	hiddenForm.hidden_value.value = hiddenValue;
	hiddenForm.hidden_name.value = hiddenName;
	this.focus();
}

function doModify() {
	var sel = window.opener.document.selection;
		if (sel!=null) {
			var rng = sel.createRange();
		}

		name = document.hiddenForm.hidden_name.value
		value = document.hiddenForm.hidden_value.value

		if (value != "") {
			value = ' value="' + value + '"'
		} else {
			value = ""
		}

		if (name != "") {
			name = ' name="' + name + '"'
		} else {
			name = ""
		}

		HTMLTextField = '<input type=hidden' + name + value + '>'
		myPage.selectedHidden.outerHTML = HTMLTextField
    
    window.close()
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				doModify()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=hiddenForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Modify Hidden Field</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Modify Hidden Field&quot; to modify  the selected Hidden Field.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Modify Hidden Field</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="hidden_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="hidden_value" size="10" class="Text150">
			</td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="modifyHiddenField" value="Modify Hidden Area" class="Text120" onClick="javascript:doModify();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%ELSEIF REQUEST.QUERYSTRING("ToDo")="ModifyButton" then%>

<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="style_richtemplate.css" type="text/css">
	
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">

	<a href="javascript:showHelp()"><img src="images/button_help.gif" width="60" height="20" alt="Help" border="0" hspace="5"></a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	

</table>
<script language=JavaScript>
var myPage = window.opener;
window.onload = setValues;

var buttonName = myPage.selectedButton.name;
var buttonValue = myPage.selectedButton.value;
var buttonType = myPage.selectedButton.type;
var buttonClass = myPage.selectedButton.className;

function setValues() {

	if (buttonClass != "") {
		buttonClass = " class=" + buttonClass
	}

	buttonForm.button_value.value = buttonValue;
	buttonForm.button_name.value = buttonName;
	this.focus();
}

function doModify() {
	var sel = window.opener.document.selection;
		if (sel!=null) {
			var rng = sel.createRange();
		}

		name = document.buttonForm.button_name.value
		value = document.buttonForm.button_value.value
		type = document.buttonForm.button_type[buttonForm.button_type.selectedIndex].text

		if (value != "") {
			value = ' value="' + value + '"'
		} else {
			value = ""
		}

		if (name != "") {
			name = ' name="' + name + '"'
		} else {
			name = ""
		}

   		HTMLTextField = '<input type="' + type + '"' + name + value + buttonClass + '>'
   		myPage.selectedButton.outerHTML = HTMLTextField
    
    window.close()
}

function printType() {
	if ((buttonType != undefined) && (buttonType != "")) {
		document.write('<option selected>' + buttonType)
		document.write('<option>Submit')
		document.write('<option>Reset')
		document.write('<option>Button')
	}
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				doModify()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=buttonForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Modify Button</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Insert Button&quot; to insert a Button into your webpage.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Modify Button</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="button_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="button_value" size="10" class="Text150">
			</td>
		  </tr>
		  
		  <tr>
			<td class="body" width="80">Type</td>
			<td class="body">
			  <select name="button_type" class=text70>
			    <script>printType()</script>
			  </select>
			</td>
			<td class="body" width="80">&nbsp;</td>
			<td class="body">&nbsp;</td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="modifyButton" value="Modify Button" class="Text90" onClick="javascript:doModify();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%ELSEIF REQUEST.QUERYSTRING("ToDo")="ModifyCheckbox" then%>



<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="style_richtemplate.css" type="text/css">

</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">

	<a href="javascript:showHelp()"><img src="images/button_help.gif" width="60" height="20" alt="Help" border="0" hspace="5"></a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=JavaScript>
var myPage = window.opener;
window.onload = setValues;

var checkboxName = myPage.selectedCheckbox.name;
var checkboxValue = myPage.selectedCheckbox.value;
var checkboxType = myPage.selectedCheckbox.checked; // true or false
var checkboxClass = myPage.selectedCheckbox.className;

function setValues() {

	if (checkboxClass != "") {
		checkboxClass = " class=" + checkboxClass
	}

	checkboxForm.checkbox_value.value = checkboxValue;
	checkboxForm.checkbox_name.value = checkboxName;
	this.focus();
}

function doModify() {
	var sel = window.opener.document.selection;
		if (sel!=null) {
			var rng = sel.createRange();
		}

		name = document.checkboxForm.checkbox_name.value
		value = document.checkboxForm.checkbox_value.value
		type = document.checkboxForm.checkbox_type[checkboxForm.checkbox_type.selectedIndex].text

		if (value != "") {
			value = ' value="' + value + '"'
		} else {
			value = ""
		}

		if (name != "") {
			name = ' name="' + name + '"'
		} else {
			name = ""
		}

   		HTMLTextField = '<input type=checkbox' + name + value + type + checkboxClass + '>'
   		myPage.selectedCheckbox.outerHTML = HTMLTextField
    
    window.close()
}

function printType() {
	if ((checkboxType != undefined) || (checkboxType != "")) {
		if (checkboxType == false) { 
			checkboxType = "Unchecked"
		}

		if (checkboxType == true) {
			checkboxType = "Checked"
		}

		document.write('<option selected>' + checkboxType)
		document.write('<option>Checked')
		document.write('<option>Unchecked</option>')
	}
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				doModify()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=checkboxForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Modify CheckBox</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Modify CheckBox&quot; to modify the selected CheckBox.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Modify CheckBox</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="checkbox_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="checkbox_value" size="10" class="Text150">
			</td>
		  </tr>
		  
		  <tr>
			<td class="body" width="80">Initial State:</td>
			<td class="body">
			  <select name="checkbox_type" class=text90>
				<script>printType()</script>
			  </select>
			</td>
			<td class="body" width="80">&nbsp;</td>
			<td class="body">&nbsp;</td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="modifyCheckbox" value="Modify CheckBox" class="Text90" onClick="javascript:doModify();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		

	  </td>
	</tr>
</table>
</body>
</html>
<%ELSEIF REQUEST.QUERYSTRING("ToDo")="ModifyRadio" then%>

<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="style_richtemplate.css" type="text/css">
	
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">

	<a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=JavaScript>
var myPage = window.opener;
window.onload = setValues;

var radioName = myPage.selectedRadio.name;
var radioValue = myPage.selectedRadio.value;
var radioType = myPage.selectedRadio.checked; // true or false
var radioClass = myPage.selectedRadio.className;

function setValues() {

	if (radioClass != "") {
		radioClass = " class=" + radioClass
	}

	radioForm.radio_value.value = radioValue;
	radioForm.radio_name.value = radioName;
	this.focus();
}

function doModify() {
	var sel = window.opener.document.selection;
		if (sel!=null) {
			var rng = sel.createRange();
		}

		name = document.radioForm.radio_name.value
		value = document.radioForm.radio_value.value
		type = document.radioForm.radio_type[radioForm.radio_type.selectedIndex].text

		if (value != "") {
			value = ' value="' + value + '"'
		} else {
			value = ""
		}

		if (name != "") {
			name = ' name="' + name + '"'
		} else {
			name = ""
		}

   		HTMLTextField = '<input type=radio' + name + value + type + radioClass + '>'
   		myPage.selectedRadio.outerHTML = HTMLTextField
    
    window.close()
}

function printType() {
	if ((radioType != undefined) || (radioType != "")) {
		if (radioType == false) { 
			radioType = "Unchecked"
		}

		if (radioType == true) {
			radioType = "Checked"
		}

		document.write('<option selected>' + radioType)
		document.write('<option>Checked')
		document.write('<option>Unchecked</option>')
	}
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				doModify()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=radioForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Modify Radio Button</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;Modify Radio Button&quot; to modify the selected Radio Button.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Modify Radio Button</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="radio_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="radio_value" size="10" class="Text150">
			</td>
		  </tr>
		  
		  <tr>
			<td class="body" width="80">Initial State:</td>
			<td class="body">
			  <select name="radio_type" class=text90>
				<script>printType()</script>
			  </select>
			</td>
			<td class="body" width="80">&nbsp;</td>
			<td class="body">&nbsp;</td>
		  </tr>
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="modifyRadio" value="Modify Radio Button" class="Text120" onClick="javascript:doModify();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		
	
	  </td>
	</tr>
</table>
</body>
</html>
<%ELSEIF REQUEST.QUERYSTRING("ToDo")="ModifyTextArea" then%>

<HTML>
<TITLE>RichTemplate</TITLE>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" href="style_richtemplate.css" type="text/css">
	
</HEAD>
<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<table cellspacing=0 cellpadding=0 width=100% align=center border=0>
	
  <tr>
		
	<td valign=center bgcolor=#ffffff><img src="images/sslogo.gif" border=0> </td>
	<td valign=center bgcolor=#ffffff align="right">

	<a href="javascript:showHelp()">Get Help Here</a></td>
  </tr>
	
  <tr>
		
	<td bgcolor=#000000 height=1 colspan="2"><hr></td>
  </tr>
	
 
</table>
<script language=JavaScript>
var myPage = window.opener;
window.onload = setValues;

var textName = myPage.selectedTextArea.name;
var textWidth = myPage.selectedTextArea.cols;
var textLines = myPage.selectedTextArea.rows;
var textValue = myPage.selectedTextArea.value;
var textClass = myPage.selectedTextArea.className;

function setValues() {

	// if (textMax == "2147483647") {
	// 	textMax = ""
	// }

	// if (textWidth == "0") {
	//	textWidth = ""
	// }

	if (textClass != "") {
		textClass = " class=" + textClass
	}

	textForm.text_lines.value = textLines;
	textForm.text_value.value = textValue;
	textForm.text_name.value = textName;
	textForm.text_width.value = textWidth;
	this.focus();
}

function doModify() {
	var sel = window.opener.document.selection;
		if (sel!=null) {
			var rng = sel.createRange();
		}

		name = document.textForm.text_name.value
		width = document.textForm.text_width.value
		rows = document.textForm.text_lines.value
		value = document.textForm.text_value.value

		error = 0
		if (isNaN(width) || width < 0) {
				alert("Character Width must contain a valid, positive number")
				error = 1
				textForm.text_width.select()
				textForm.text_width.focus()
		} else if (isNaN(rows) || rows < 0) {
				alert("Lines must contain a valid, positive number")
				error = 1
				textForm.text_lines.select()
				textForm.text_lines.focus()
		}

		if (error != 1) {
				if (value != "") {
					value =  value
				} else {
					value = ""
				}

				if (name != "") {
					name = ' name="' + name + '"'
				} else {
					name = ""
				}

				if (width != "") {
					width = ' cols="' + width + '"'
				} else {
					width = ""
				}

				if (rows != "") {
					rows = ' rows="' + rows + '"'
				} else {
					rows = ""
				}

        			HTMLTextField = '<textarea' + name + width + rows + '>' + value + '</textarea>'
         			myPage.selectedTextArea.outerHTML = HTMLTextField
		}
    
    window.close()
}

document.onkeydown = function () { 
			if (event.keyCode == 13) {	// ENTER
				doModify()
			}
};

document.onkeypress = onkeyup = function () {
	if (event.keyCode == 13) {	// ENTER
	event.cancelBubble = true;
	event.returnValue = false;
	return false;			
	}
};

</script>

<form name=textForm>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
	<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
	  <td class="heading1">Modify Text Area</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	  <td class="body">Enter the required information and click &quot;modify Text Area&quot; to modify the selected Text Area.<br>
		Click the &quot;Cancel&quot; Button to  close this window.</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">&nbsp;</td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	  <table width="98%" border="0" cellspacing="0" cellpadding="0" class="sstable1">
  		<tr>
		    <td>&nbsp;&nbsp;Modify Text Area</td>
		</tr>
	  </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td class="body">
	 	  
	    <table border="0" cellspacing="0" cellpadding="5" width="98%" class="sstable2">
		
		
		  <tr>
		    <td class="body" width="80">Name:</td>
			<td class="body" width="200">
			  <input type="text" name="text_name" size="10" class="Text90" maxlength="50">
		  </td>
			<td class="body" width="80">Initial Value:</td>
			<td class="body">
			  <input type="text" name="text_value" size="10" class="Text150">
			</td>
		  </tr>
		  <tr>
			<td class="body" width="80">Character Width:</td>
			<td class="body">
			  <input type="text" name="text_width" size="3" class="Text50" maxlength="3">
			</td>
			<td class="body" width="80">Lines:</td>
			<td class="body">
			  <input type="text" name="text_lines" size="3" class="Text50" maxlength="3">
			</td>
		  </tr>
		  
		  
		  
	    </table>
	</td>
  </tr>
  <tr>
	<td colspan="2"><img src="images/1x1.gif" width="1" height="10"></td>
  </tr>
  <tr>
	<td>&nbsp;</td>
	<td>
	    <input type="button" name="insertTextField" value="Modify Text Area" class="Text90" onClick="javascript:doModify();">
	<input type="button" name="Submit" value="Cancel" class="Text50" onClick="javascript:window.close()">
	</td>
  </tr>
</table>
</form><br>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="15"><img src="images/1x1.gif" width="15" height="1"></td>
		
	<td class="body" align="center" valign="bottom">
		
	
	  </td>
	</tr>
</table>
</body>
</html>
<%end if%>


























































































