<%PNAME = "RichTemplate Modules"
%>
<!--#INCLUDE FILE="sessioncheck.asp"-->

<!--#include file="db_connection.asp"-->



<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Modules</title>
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
<script>
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
  var savevalue = ""
  //  add the content to the hidden field
  document.form1.publish.value = savevalue;  
  document.form1.html.value = html; 
  // submit the formular 
//  document.form1.submit();
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

</head>

<body topmargin="0" leftmargin="0" marginheight="0">


<!--#INCLUDE FILE="headernew.inc"-->

<p><img border="0" src="images/ims03.gif" width="400" height="198"></p>






</body>

</html>