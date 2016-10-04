<%PNAME = "Image/Document Library - Documents"
PHELP = "helpFiles/pageListing.asp#library"%>
<%


mode		= Request.Querystring("mode")
language 	= Request.Querystring("language")
urlabs 		= Request.Querystring("urlabs")
pathabs 	= Request.Querystring("pathabs")
'response.write urlabs
tech		= Request.Querystring("tech")
filter2		= Request.Querystring("filter")


if request.querystring("path") <>"" then
  		path = replace(request.querystring("path"), "/","\")
  		session("path") = path
end if

Session("URL") = "richtemplate_filemgmt.asp?mode="&mode&"&language="&language&"&urlabs="&urlabs&"&pathabs="&pathabs&"&tech="&tech&"&filter="&filter2&""
Session("URL2") = "mode="&mode&"&language="&language&"&urlabs="&urlabs&"&pathabs="&pathabs&"&tech="&tech&"&filter="&filter2&""
URL = "language="&language&"&insert=0&urlabs="&urlabs&"&pathabs="&pathabs&"&tech="&tech&""


%>


<html>
<head>
<title>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </title>
<script language="javascript" src="../editor/config/international.js"></script>
<script language="javascript" src="../editor/include/browser.js"></script>

<SCRIPT LANGUAGE="JavaScript">
<!-- Begin
function popUpp(URL) {
day = new Date();
id = day.getTime();
eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=yes,location=0,statusbar=0,menubar=0,resizable=0,width=400,height=300,left = 100,top = 100');");
}
// End -->
</script>

<SCRIPT LANGUAGE="JavaScript">
<!-- Begin
function popUpp2(URL) {
day = new Date();
id = day.getTime();
eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=yes,location=0,statusbar=0,menubar=0,resizable=0,width=400,height=150,left = 100,top = 100');");
}
// End -->
</script>
<script>
var mode = "";
var web = "";
var language = "";
var urlabs = "";
var pathabs = "";
var folderName = "";
var filter = "";
var tech = "";

// process parameters
var param = "";
try {
  param = window.location.search;
} catch(Error) {}

if(param.length > 0) {
  param = param.substring(1,param.length);
  var aArray = param.split("&");
  for(var i=0;i<aArray.length;i++) {
    var aParam = aArray[i].split("=");
    for(var j=0;j<aParam.length-1;j++) {
      if(aParam[0].toUpperCase() == "LANGUAGE") {
        language = aParam[1];
      }
      if(aParam[0].toUpperCase() == "MODE") {
        mode = aParam[1];
      }
      if(aParam[0].toUpperCase() == "TECH") {
        tech = aParam[1];
      }
      if(aParam[0].toUpperCase() == "URLABS") {
        urlabs = aParam[1];
      }
      if(aParam[0].toUpperCase() == "PATHABS") {
        pathabs = aParam[1];
      }
      if(aParam[0].toUpperCase() == "FILTER") {
        filter = aParam[1];
      }
    }
  }
}
// set title
document.title = getLanguageString(language,1001) + document.title;

function goBack()
{
  document.getElementById("filelist").contentWindow.goBack();
}

function process()
{
  var ret = "";
  var win = document.getElementById("filelist").contentWindow;

  if(mode == 'OPEN' || mode == 'IMAGE') {
    if(win.getSelectedRow() < 0) {
      alert("Please select a file !");
      return;
    }
    var type = win.getSelectedType();
    if(type == 'FOLDER') {
      win.dblClick(win.getSelectedRow(),type);
      return;
    }
    ret = win.getSelectedUrl();
  }
  if(mode == 'SAVE') {
    if(document.getElementById("txtFile").value == "") {
      alert('Please enter a file name !');
      return;
    }
    // we need the url because the user can enter the file name
    var url = win.getCurrentUrl();
    if(url.substring(url.length-1,url.length) == "/" )
			ret = url + document.getElementById("txtFile").value;
		else
			ret = url + "/" + document.getElementById("txtFile").value;
  }

  if(browser.ns) {
    if(mode == 'OPEN')
      if(filter == "swf;")
				window.opener.callbackMozilla("FLASH", ret);
      else
				window.opener.callbackMozilla("OPENDOC", ret);
    if(mode == 'SAVE')
      window.opener.callbackMozilla("SAVEDOC", ret);
    if(mode == 'IMAGE')
      window.opener.callbackMozilla("OPENIMAGE", ret);
  } else {
    window.returnValue = ret;
  }
  window.close();
}

function RowDblClick()
{
	process();  
}

function selectPath(path)
{
  document.getElementById("preview").src = path + "?rnd=" + Math.random() ;
  document.getElementById("lblPath").innerHTML = path;
}

function setFileName(name)
{
  if(mode == 'SAVE') {
    document.getElementById("txtFile").value = name;
  }
}

function load()
{

  try {
    document.getElementById("lblCancel").value = getLanguageString(language,900);
    document.getElementById("lbl5").innerHTML = getLanguageString(language,1007);
    if(mode == "OPEN")
      document.getElementById("lbl2").value = getLanguageString(language,1002);
    if(mode == "SAVE")
      document.getElementById("lbl2").value = getLanguageString(language,1003);
    if(mode == "IMAGE")
      document.getElementById("lbl2").value = getLanguageString(language,1004);
    if(mode == "SELECT") {
      document.getElementById("lbl2").value = getLanguageString(language,1002);
      document.getElementById("lblPath").innerHTML = docdir;
    }
    if(mode == "SAVE")
      document.getElementById("lbl3").innerHTML = getLanguageString(language,1005);
    document.getElementById("lbl4").innerHTML = getLanguageString(language,1006);
    //document.title = getLanguageString(language,1001) + document.title;
  } catch(Error) {}

  var url = "../editor/dialogs/asp/filelist_back.asp?";
  url = url + "mode=" + mode; 
  url = url + "&language=" + language; 
  url = url + "&urlabs=" + urlabs; 
  url = url + "&pathabs=" + pathabs; 
  url = url + "&filter=" + filter; 

  document.getElementById("filelist").src = url;
  // set mode objects dynamicly
  if(mode != "SAVE") {
    document.getElementById("txtFile").style.display = 'none';
    document.getElementById("lbl3").style.display = 'none';
  } else {
    document.getElementById("layPreviewText").style.display = 'none';
    document.getElementById("layPreview").style.display = 'none';
  }

	// parameter check
	if(pathabs == "")
		alert("pinEdit Warning (Parameter Check):\n" + "Please specify the globalRootPath parameter in config.js or the cp parameter !");
}
</script>
<script language="javascript" src="/editor/config/urlconfig.js"></script>

<script language="javascript" src="/editor/config/config.js"></script>



<script language="javascript">
var ImageURL = "mode=IMAGE&"+"language="+(language)+"&"+"urlabs="+(globalImageUrlAbsolute)+"&"+"pathabs="+(globalImagePathAbsolute)+"&"+"tech=asp&filter="+(globalDialogFilterImage)
//document.write(ImageURL)
</script>

<script language="javascript">
var FileURL = "mode=OPEN&"+"language="+(language)+"&"+"urlabs="+(globalDocumentUrlAbsolute)+"&"+"pathabs="+(globalDocumentPathAbsolute)+"&"+"tech=asp&filter="+(globalDialogFilterDoc)
//document.write(FileURL)
</script>
<%

'DELETE FILES 
IF Request.querystring("file")<>"" then

	strpathfolder = Session("filePath")


strPath = Session("filePath")&"/"&Request.querystring("file")
Dim objFSO
Set objFSO = Server.CreateObject("Scripting.FileSystemObject")

'Delete the file

if objFSO.FileExists(strPath) then
objFSO.DeleteFile(strPath) 
else
end if
set objFSO = Nothing
response.redirect "../editor/dialogs/asp/filelist_back.asp?"&session("url2")&""

end if%>
<%

'DELETE FOLDERS
IF Request.querystring("folder")<>"" then

	strFolder = Request.Querystring("folder")
	strPath = Session("filePath")

Set objFSO = Server.CreateObject("Scripting.FileSystemObject")

'Delete the file

if objFSO.FolderExists(strPath&"/"&strFolder) then
objFSO.DeleteFolder(strPath&"/"&strFolder) 
else
end if
response.redirect "../editor/dialogs/asp/filelist_back.asp?"&session("url2")&""
set objFSO = Nothing

end if%>





<SCRIPT language="JavaScript">
   function OnSubmitForm()
{

     document.myform.action ="richtemplate_filemgmt.asp?" + FileURL;
     document.myform.submit();
}

</SCRIPT>
    
<SCRIPT language="JavaScript">
   function OnSubmitForm2()
{

     document.myform2.action ="richtemplate_filemgmt.asp?"+ ImageURL;
     document.myform2.submit();
}

</SCRIPT>


<link rel="stylesheet" type="text/css" href="style_richtemplate.css">
</head>
<body style="margin-right:5px" onload="load();" topmargin="0" leftmargin="0">
<!--#INCLUDE FILE="headernew.inc"-->
     <table border="0" width="100%" cellspacing="0" cellpadding="0" id="table1">
		<tr>
    <td width="64%" height="30">
	<p class="bodybold">&nbsp; Click on an image to preview the image on the right.</td>
  	
    <td width="3%" height="30">

	<img border="0" src="images/icon_uploadimage.gif" width="24" height="24">
  	
    <td width="197">
    <p class="body"><a href="javascript: popUpp2('richtemplate_folders.asp')">
    Create New Folder Here</a></td>
 <td width="3%">
	<img border="0" src="images/icon_uploadimage.gif" width="24" height="24"></td>
    <td width="97">
	<p class="body">
	<%IF REQUEST.QUERYSTRING("MODE")="IMAGE" THEN%>
	<a href="javascript: popUpp('richtemplate_upload_images.asp?<%=SESSION("URL2")%>')">
	Upload Image</a>
	<%ELSE%>
	<a href="javascript: popUpp('uploadImages.asp')">
	Upload File</a>
	<%END IF%>

	</td>

		</tr>
		</table>
<table style="width:100%;height:400px;">
<tr>
<td style="width:100%">
<table style="border:1px solid #C0C0C0; width:100%;height:440px; padding-left: 2px; padding-right: 2px;font-family: arial; font-size:11px; font-weight:normal" border="0" cellspacing="0" cellpadding="0">
  <tr>
     <td bgcolor="#EEEEEE">
       <img src="../editor/design/image/goback.gif" onclick="goBack()" border=0 alt="Back" width="16" height="16"><table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="20px"><!--<img src="../../design/image/goback.gif" onclick="goBack()" border=0 alt="Back" width="16" height="16">--></td>
          <td width="30px"><p id='lbl5' style="font-family: arial; font-size:11px;"></p></td>
          <td width="100%" bgcolor="#EEEEEE"><p style="padding-left: 4px;font-family: arial; font-size:11px" id="lblPath"></p></td>
        </tr>
       </table>
     </td>
     <td bgcolor="#EEEEEE"></td>
     <td id="layPreviewText" bgcolor="#EEEEEE" height="20"><p id="lbl4"></p></td>
  </tr>
  <tr>
    <td style='WIDTH:400px;HEIGHT:100%' bgcolor="#EEEEEE">
      <iframe id='filelist' src="../editor/dialogs/asp/dummy.html" style='background-color: white;WIDTH:100%;HEIGHT:100%' frameborder=0 scrolling='auto'>
      </iframe>
    </td>
    <td width="1px" bgcolor="#EEEEEE"></td>
    <td id="layPreview" bgcolor="#EEEEEE">
      <iframe id='preview' style='width:100%;height:100%;' frameborder=0 name="Preview">
      Preview Pane</iframe>
    </td>
  </tr>
  <tr>
     <td colspan="3" bgcolor="#EEEEEE" height="20"></td>
  </tr>
</table>
</td>
</tr>

<tr>
<td style="height: 50px; width:100%">
&nbsp;</td>
</td>
</table>

</body>
</html>