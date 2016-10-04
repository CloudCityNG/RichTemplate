<%@ LANGUAGE="VBSCRIPT" %>

<%
If Session("userLevel") < 98 Then Response.Redirect "login.asp?n=igallery.asp"
If Session("userLevel") > 99 Then Response.Redirect "login.asp?n=igallery.asp"
%>

<!--#INCLUDE FILE="include/globalsets.asp"-->

<%
dim dbconn
Function FormatImageSize(intFileSize)
const DecimalPlaces = 1
const FileSizeBytes = 1
const FileSizeKiloByte = 1024
const FileSizeMegaByte = 1048576
const FileSizeGigaByte = 1073741824
const FileSizeTeraByte = 1099511627776
Dim strFileSize, newFilesize
If (Int(intFileSize / FileSizeTeraByte) <> 0) Then
newFilesize = Round(intFileSize / FileSizeTeraByte, DecimalPlaces)
strFileSize = newFilesize & " TB"
ElseIf (Int(intFileSize / FileSizeGigaByte) <> 0) Then
newFilesize = Round(intFileSize / FileSizeGigaByte, DecimalPlaces)
strFileSize = newFilesize & " GB"
ElseIf (Int(intFileSize / FileSizeMegaByte) <> 0) Then
newFilesize = Round(intFileSize / FileSizeMegaByte, DecimalPlaces)
strFileSize = newFilesize & " MB"
ElseIf (Int(intFileSize / FileSizeKiloByte) <> 0) Then
newFilesize = Round(intFileSize / FileSizeKiloByte, DecimalPlaces)
strFileSize = newFilesize & " KB"
ElseIf (Int(intFileSize / FileSizeBytes) <> 0) Then
newFilesize = intFilesize
strFileSize = newFilesize & " Bytes"
ElseIf Int(intFileSize) = 0 Then
strFilesize = 0 & " Bytes"
End If
FormatImageSize = strFileSize
End Function
%>

<html>
<head>


<script language="JavaScript1.2" type="text/javascript">
<!--
function ValFileField(entered, alertbox) {
with (entered){
ext=value.substr(value.lastIndexOf(".")).toLowerCase();
if (
<%
AllowedFiles 	= Replace(AllowedFiles,",",", ")
strallowedfiles = Replace(AllowedFiles,", ",",")
strallowedfiles = Replace(strallowedfiles," ,",",")
strallowedfiles = Split(strallowedfiles,",")
For a1 = LBound(strallowedfiles) To UBound(strallowedfiles)
response.write "ext!=""."&strallowedfiles(a1)&""" "&chr(38)&chr(38)&"" & VBCRLF
Next
%>ext!=""
//ext!=""&&ext!=".jpg"&&ext!=".jpeg"&&ext!=".gif"&&ext!=".bmp"&&ext!=".png"
){
if (alertbox!="") {
alert(alertbox);
}
return false;
}
else {
return true;
}}}

// Value Validation
function attachValue(entered, confirmbox) {
with (entered){
if (value==null || value=="")
{if (confirmbox!="") {alert(confirmbox);} return false;}
else {return true;}
}}

function FileValidation(thisform) {
with (thisform) {
if (Attachment.value.length == "") {
if (attachValue(Attachment,"The \"File Attachment\" field is required!  Please select an image attachment by clicking on the browse button.")==false) {Attachment.focus(); return false;};
}
else{
if (ValFileField(Attachment,"The \"File Type\" you have requested is not allowed!  Supported file types are: <%= UCase(AllowedFiles) %>")==false) {Attachment.focus(); return false;};
}}}

function setScripts() {
self.name = 'igallery';
}
//window.onload = setScripts;
//-->
</script>
<link rel="stylesheet" type="text/css" href="style/style.css">

<link rel="STYLESHEET" type="text/css" href="css/nn.css" disabled>
<link rel="stylesheet" href="/admin/style_richtemplate.css" type="text/css">
<script language="JavaScript1.2" type="text/javascript">


<!--
if (document.all) document.createStyleSheet("css/ie.css");
//-->
</script>

</head>

<body bgcolor="#FFFFFF" text="#000000" leftmargin="0" topmargin="0" marginwidth="10" marginheight="10">
<%
PNAME = "Image/Document Library - Upload Images"
'PHELP = "helpFiles/pageListing.asp#WYSIWYG"%>
<!--#INCLUDE VIRTUAL="/admin/headernew.inc"-->

<table width="760" cellspacing="0" cellpadding="0" border="0" height="80%">
<tr>
<td style="padding-left:10px;padding-top:15px;" width="530" valign="top">

<font class="text">

<%
ImageDir    = UploadPath&"\"&Request("D")
UploadDir   = ImageDir

DisplayFolder = Request("D")
DisplayFolder = Left(DisplayFolder,Len(DisplayFolder)-1)
DisplayFolder = Right(DisplayFolder,Len(DisplayFolder)-1)
%>

<form onsubmit="return FileValidation(this)" name="imageUpload" method="POST" enctype="multipart/form-data" action="upload-inc.asp?D=<%= URLSpace(ImageDir) %>" target="upload">

<table width="510" cellspacing="0" cellpadding="0" border="0">
<tr>
<td>

<table cellspacing="0" cellpadding="0" border="0">
<tr>
<td width="28"><img src="images/lg-image.gif" width=24 height=32 border=0 alt=""></td>
<td><font class="largeheader">Upload Image</font></td>
</tr>
</table>

</td>
<td align="right">

<table cellspacing="0" cellpadding="0" border="0">
<tr>
<td width="20"><img src="images/spacer.gif" width="1" height="2"><br><a class="linkmed" href="igallery.asp?D=<%= URLSpace(ImageDir) %>\"><img src="images/sm-return.gif" width="18" height="18" border="0"></a></td>
<td><a class="link" href="igallery.asp?D=<%= URLSpace(Request("D"))  %>"><b>Return To Folder</b></a></td>
</tr>
</table>

</td>
</tr>
</table>

<img src="images/spacer.gif" width=1 height=8 border=0 alt=""><br>

You are about to upload an image to the following directory in your photo gallery:<br>

<img src="images/spacer.gif" width=1 height=10 border=0 alt=""><br>

<table align="center" cellspacing="0" cellpadding="0" border="0">
<tr>
<td><img src="images/folder-open.gif" width=30 height=32 border=0 alt=""></td>
<td><font class="textmed">&nbsp;<b><%= DisplayFolder %></b></font><br></td>
</tr>
</table>

<img src="images/spacer.gif" width=1 height=5 border=0 alt=""><br>

<script language="JavaScript1.2" type="text/javascript">
<!--
function Reset1(url) {
window.open(url,"upload");
}
// -->
</script>

<table align="center" cellspacing="0" cellpadding="1">
	<tr>
<td>

<font size="1">
<input type="file" name="Attachment" size="40"><br>
<input name="attach2" type="file" size="40"><br>
<input name="attach3" type="file" size="40"><br>
<input name="attach4" type="file" size="40"><br>

</font>

</td>
<td><input style="font-size: 8.5pt;" type="submit" value="Upload"></td>
<td><input onclick="Reset1('blank.asp')" style="font-size: 8.5pt;" type="reset" value="Reset"></td>
	</tr>
	<tr>
<td>

&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
	</tr>
</table>

<img src="images/spacer.gif" width=1 height=5 border=0 alt=""><br>

<table cellspacing="0" cellpadding="1" border="0">
<tr>
<td nowrap><b><font face="Verdana" size="1">Allowed File Types:&nbsp;&nbsp;</font></td>
<td nowrap><font face="Verdana" size="1"><%= UCase(AllowedFiles) %></font></td>
</tr>
<tr>
<td nowrap><b><font face="Verdana" size="1">Maximum Upload Size:&nbsp;&nbsp;</font></td>
<td nowrap><font face="Verdana" size="1"><%= FormatImageSize(MaxUploadSize) %></font></td>
</tr></form>
</table>

<iframe name="upload" src="blank.asp?DN=yes&D=<%= URLSpace(ImageDir) %>" width="530" height="620" frameborder="0" scrolling="no"></iframe> 

</font>

</td>
</tr>
</table>

<p>

<!--#INCLUDE FILE="include/footer.asp"-->

</body>
</html>