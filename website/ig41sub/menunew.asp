<html>

<head>
<link rel="stylesheet" type="text/css" href="/ig41sub/style/style.css">
<base target="main">
<script type="text/javascript">
<!--
function CompactRepair(url) {
var leftPos = (screen.availWidth-500) / 2
var topPos  = (screen.availHeight-175) / 2 
var iforgot = window.open(url,'','width=500,height=175,scrollbars=no,resizable=no,status=no,toolbar=no,location=no,top=' + topPos + ',left=' + leftPos);
}
//-->
</script>
<base target="main">
</head>

<body background="/admin/images/navback_image.gif" style="background-attachment: fixed">
<%
'####################################
'## Application:   Blue-Collar Productions
'## File Name:     /ig41sub/igallery.asp
'## File Version:  i-Gallery
'## Copyright:     This code is copyrighted. Please see http://www.b-cp.com for details.
'## Notice:        This code has limited warranties. Please see http://www.b-cp.com for details.
'####################################
%>
<!--#include virtual="/ig41sub/include/sessioncheck.inc"-->
<!--#INCLUDE virtual="/ig41sub/include/globalsets.asp"-->
<!--#INCLUDE virtual="/ig41sub/include/gfxSpex.inc"-->
<!--#INCLUDE virtual="/ig41sub/include/filesize.inc"-->
<!--#INCLUDE virtual="/ig41sub/language/lang_global.inc"-->
<!--#INCLUDE virtual="/ig41sub/language/lang_igallery.inc"-->

<%
Set objF   = fsdir
Set objFC  = objF.Files
intPage = Request.Querystring("page")
If intPage <> "" Then intPage = intPage else intPage = 0
i = 1
Dim RecordsCount
RecordsCount = 0 ' set count to zero
Dim rowcount
rowcount = 1 ' set count to zero
%>

<%
'################# SUB DisplayGallery Folder & Object Set-Up #################
Sub DisplayGallery(dirfile,f1)

'##### Begin Display Folders ######
If dirFile = "DISPLAYFOLDERS" Then

strFolderName 	= f1.Name
strBaseDir 		= BaseDir
If strBaseDir <> "" Then strBaseDir = strBaseDir Else strBaseDir = ""
%>

<%
'##### End Display Folders ######


'##### Begin Display Images ######
ElseIf dirFile = "DISPLAYIMAGES" Then

strBaseDir 		= BaseDir

If strBaseDir <> "" Then strBaseDir = strBaseDir Else strBaseDir = ""
If NOT InStr(f1, "tn-") > 0 AND NOT InStr(LCase(f1), "thumbs.db") > 0 Then ' Hide Non-Thumnails & Thumbs.db In View

'##### Original Image Size ######
'FileExt = fExt(f1.Name)
'Select Case FileExt
'Case "jpg", "jpeg", "gif", "bmp", "png"
'ImagePath 		= UploadPath &"\"& strImageFolder &"\" & strimage
'If gfxSpex(Replace(f1.Path,"tn-",""), width, height, colors, strType) = True Then
'ImageWidth  = width
'ImageHeight = height
'End If
'End Select

If Nailer Then

'If ImageWidth > 145 Then
strwidth  = "145"
'strheight = Round(145*ImageHeight/ImageWidth,0)
strimagesrc = "<img class=""shadow1"" style=""border: 1px solid gray;"" src="""& URLPath&"/"&strBaseDir &"/tn-"&f1.Name&""" width="""&strwidth&"""  border=""0"">"
'Else
'strwidth = ImageWidth
'strHeight = ImageHeight
'strimagesrc = "<img style=""border: 1px solid gray;"" src="""& URLPath&"/"&strBaseDir &"/"&f1.Name&""" width="""&strwidth&""" height="""&strheight&""" border=""0"">"
'End If

Else

'If ImageWidth > 145 Then
strwidth  = "145"
'strheight = Round(145*ImageHeight/ImageWidth,0)
'Else
'strwidth = ImageWidth
'strHeight = ImageHeight
'End If

strimagesrc = "<img style=""border: 1px solid gray;"" src="""& URLPath&"/"&strBaseDir &"/"&f1.Name&""" width="""&strwidth&"""  border=""0"">"

End If

If (RecordsCount >= (intPage * RecordsPerPage)) and (RecordsCount < (intPage * RecordsPerPage) + RecordsPerPage) Then
If NOT i MOD RecordsPerRow = 0 Then
strImageName 	= f1.name 
QS 			 	= "?image="&URLSpace(Replace(strImageName,"tn-",""))&"&folder="&URLSpace(BaseDir)&"&page="&intPage
%>
<td style="padding: 5px;" <% If Session("userLevel") = "99" OR Session("userLevel") = "98" Then %>width="245"<% Else %>width="155"<% End If %> bgcolor="#f4f4f4" onMouseOver="this.bgColor='#efefef'" onMouseOut="this.bgColor='#f7f7f7'" valign="top" nowrap>
<!--#INCLUDE virtual="/ig41sub/igallery.inc"-->
</td>
<%
Else
strImageName 	= f1.name 
QS 			 	= "?image="&URLSpace(Replace(strImageName,"tn-",""))&"&folder="&URLSpace(BaseDir)&"&page="&intPage
%>
<td style="padding: 5px;" <% If Session("userLevel") = "99" OR Session("userLevel") = "98" Then %>width="245"<% Else %>width="155"<% End If %> bgcolor="#f4f4f4" onMouseOver="this.bgColor='#efefef'" onMouseOut="this.bgColor='#f7f7f7'" valign="top" nowrap>
<!--#INCLUDE virtual="/ig41sub/igallery.inc"-->
</td>
</tr>
<%
End If
End If
RecordsCount = RecordsCount + 1
If rowcount = RecordsPerRow Then rowcount = 0
rowcount = rowcount + 1
i = i + 1
End If
%>

<%
'##### End Display Images ######

End If 
End Sub
'################# End SUB DisplayGallery Folder & Object Set-Up #################
%>



<img src="/ig41sub/images/spacer.gif" width=1 height=5 border=0 alt=""><br>

<table width="170" cellspacing="0" cellpadding="0" border="0">
<tr>
<td width="19" valign="bottom"><img src="/ig41sub/images/sm-folder.gif" alt="" width="16" height="14" border="0"></td>
<td class="textsm" valign="bottom"><a target="basefrm" class="textsm" style="text-decoration:none;" href="igallery.asp"><b><%= GalleryName %></b></a></td>
</tr>
</table>

<img src="/ig41sub/images/spacer.gif" width=1 height=3 border=0 alt=""><br>

<table width="200" cellspacing="0" cellpadding="0" border="0">
<%
On Error Resume Next

folderc = 0
For Each fn1 In FileList
folderc = folderc +1
Next

currentf = 1
For Each fn In FileList
If NOT LCase(fn.Name) = "_vti_cnf" Then ' Hide Front Page "_vti_cnf" Folder In Menu

If Request("D") Then
String1 = LCase(Request("D"))
Else
String1 = "\"&LCase(Replace(Request("folder"),"/","\"))&"\"
End If
String2 = "\"&LCase(fn.Name)&"\"
'If InStr(String1,String2)>0 Then
ShowFolder = True
'Else
'ShowFolder = False
'End if

If len(fn.Name) > 30 Then 
FolderName = Left(fn.Name,30)&"..."
Else
FolderName = fn.Name
End if

Directory = fn
Directory = Replace(LCase(Directory),LCase(UploadPath),"")
Directory = URLSpace(Directory)
%>

<tr>
<td height="18" width="17"><img src="/ig41sub/images/sm-tree2.gif" alt="" width="17" height="17" border="0"></td>
<td width="18" nowrap><img src="/ig41sub/images/sm-folder<% If ShowFolder Then %>4<% Else %>5<% End If %>.gif" width=17 height=16 border=0 alt=""></td>
<td width="100%" nowrap ><a target="basefrm" style="text-decoration:none;" class="textsm" href="/ig41sub/igallery.asp?d=<%= Directory %>\"><b><%= FolderName %></b></a></td>
</tr>

<%
Set FileList1 = fn.SubFolders
For Each fn1 in FileList1
If NOT LCase(fn1.Name) = "_vti_cnf" Then ' Hide Front Page "_vti_cnf" Folder In Menu

If ShowFolder Then
If Request("D") Then
String1a = LCase(Request("D"))
Else
String1a = "\"&LCase(Replace(Request("folder"),"/","\"))&"\"
End If
String2a = "\"&LCase(fn1.Name)&"\"
'If InStr(String1a,String2a)>0 Then
ShowFolder1 = True
'Else
'ShowFolder1 = False
'End if

If len(fn1.Name) > 23 Then 
FolderName = Left(fn1.Name,23)&"..."
Else
FolderName = fn1.Name
End if

Directory1 = fn1
Directory1 = Replace(LCase(Directory1),LCase(UploadPath),"")
Directory1 = URLSpace(Directory1)
%>

<tr>
<td height="18" width="17"><% If currentf = folderc Then %><% Else %><img src="/ig41sub/images/sm-tree1.gif" width="17" height="18" border="0" alt=""><% End If %></td>
<td height="18" width="18" nowrap><img src="/ig41sub/images/sm-tree2.gif" alt="" width="17" height="17" border="0"></td>
<td width="100%" nowrap>
<table cellspacing="0" cellpadding="0" border="0">
<tr>
<td width="18" nowrap><img src="/ig41sub/images/sm-folder<% If ShowFolder1 Then %>4<% Else %>5<% End If %>.gif" width=17 height=16 border=0 alt=""></td>
<td nowrap><a target="basefrm" class="textsm" style="text-decoration:none;"  href="/ig41sub/igallery.asp?d=<%= Directory1 %>\" Title="<%= fn1.Name %>"><b><%= FolderName %></b></a></td>
</tr>
</table>
</td>
</tr>

<%
Set FileList2 = fn1.SubFolders
For Each fn2 in FileList2
If NOT LCase(fn2.Name) = "_vti_cnf" Then ' Hide Front Page "_vti_cnf" Folder In Menu

If ShowFolder Then
If Request("D") Then
String1b = LCase(Request("D"))
Else
String1b = "\"&LCase(Replace(Request("folder"),"/","\"))&"\"
End If
String2b = "\"&LCase(fn2.Name)&"\"
'If InStr(String1b,String2b)>0 Then
ShowFolder2 = True
'Else
'ShowFolder2 = False
'End if

If len(fn2.Name) > 20 Then 
FolderName = Left(fn2.Name,20)&"..."
Else
FolderName = fn2.Name
End If

Directory2 = fn2
Directory2 = Replace(LCase(Directory2),LCase(UploadPath),"")
Directory2 = URLSpace(Directory2)
%>

<tr>
<td height="18" width="17"><% If currentf = folderc Then %><img src="/ig41sub/images/spacer.gif" width="17" height="18" border="0" alt=""><% Else %><img src="/ig41sub/images/sm-tree1.gif" width="17" height="18" border="0" alt=""><% End If %></td>
<td height="18" width="18" nowrap><img src="/ig41sub/images/spacer.gif" width="18" height="18" border="0" alt=""></td>
<td width="100%" nowrap>
<table cellspacing="0" cellpadding="0" border="0">
<tr>
<td height="18" width="18" nowrap><img src="/ig41sub/images/sm-tree2.gif" alt="" width="17" height="17" border="0"></td>
<td width="18" nowrap><img src="/ig41sub/images/sm-folder<% If ShowFolder2 Then %>4<% Else %>5<% End If %>.gif" width=17 height=16 border=0 alt=""></td>
<td nowrap><a target="basefrm" class="textsm"  style="text-decoration:none;" href="/ig41sub/igallery.asp?d=<%= Directory2 %>\" Title="<%= fn2.Name %>"><b><%= FolderName %></b></a></td>
</tr>
</table>
</td>
</tr>

<%
Set FileList3 = fn2.SubFolders
For Each fn3 in FileList3
If NOT LCase(fn3.Name) = "_vti_cnf" Then ' Hide Front Page "_vti_cnf" Folder In Menu

If ShowFolder Then
If Request("D") Then
String1c = LCase(Request("D"))
Else
String1c = "\"&LCase(Replace(Request("folder"),"/","\"))&"\"
End If
String2c = "\"&LCase(fn3.Name)&"\"
'If InStr(String1c,String2c)>0 Then
ShowFolder3 = True
'Else
'ShowFolder3 = False
'End if

If len(fn3.Name) > 17 Then 
FolderName = Left(fn3.Name,17)&"..."
Else
FolderName = fn3.Name
End if

Directory3 = fn3
Directory3 = Replace(LCase(Directory3),LCase(UploadPath),"")
Directory3 = URLSpace(Directory3)
%>

<tr>
<td height="18" width="17"><% If currentf = folderc Then %><img src="/ig41sub/images/spacer.gif" width="17" height="18" border="0" alt=""><% Else %><img src="/ig41sub/images/sm-tree1.gif" width="17" height="18" border="0" alt=""><% End If %></td>
<td height="18" width="18" nowrap><img src="/ig41sub/images/spacer.gif" width="18" height="18" border="0" alt=""></td>
<td width="100%" nowrap>
<table cellspacing="0" cellpadding="0" border="0">
<tr>
<td height="18" width="18" nowrap><img src="/ig41sub/images/spacer.gif" width="18" height="18" border="0" alt=""></td>
<td height="18" width="18" nowrap><img src="/ig41sub/images/sm-tree2.gif" alt="" width="17" height="17" border="0"></td>
<td width="18" nowrap><img src="/ig41sub/images/sm-folder<% If ShowFolder3 Then %>4<% Else %>5<% End If %>.gif" width=17 height=16 border=0 alt=""></td>
<td nowrap><a target="basefrm"  style="text-decoration:none;" class="textsm" href="/ig41sub/igallery.asp?d=<%= Directory3 %>\" Title="<%= fn3.Name %>"><b><%= FolderName %></b></a></td>
</tr>
</table>
</td>
</tr>

<% 
End If
End If
Next
%>

<% 
End If
End If
Next
%>

<% 
End If
End If
Next
%>

<%
End If
currentf = currentf + 1
Next
%>
</table>

<img src="/ig41sub/images/spacer.gif" width=1 height=10 border=0 alt=""><br>






<!--
<% If Session("userLevel") = "99" Then %>

<hr size="1" color="silver">

<table cellspacing="0" cellpadding="2" border="0">
<tr>
<td><img src="/ig41sub/images/sm-users.gif" alt="" width="14" height="14" border="0"></td>
<td class="textsm"><b><%= IGmenu_ua %>:</b></td>
</tr>
</table>

<table width="200" cellspacing="0" cellpadding="0" border="0">
<tr>
<td height="17" align="right"><a class="linkxsm" href="users.asp?mode=add&mi=2"><b>Add New User</b></a></td>
<% If Request("mi") = "2" Then %>
<td width="20" style="padding-right:2px;" align="right" nowrap><img src="/ig41sub/images/sm-cal.gif" width=16 height=14 border=0 alt=""></td>
<% Else %>
<td width="20" style="padding-right:2px;" align="right" nowrap><img src="/ig41sub/images/arrow-rt.gif" width=13 height=10 border=0 alt=""></td>
<% End If %>
</tr>
<tr>
<td height="17" align="right"><a class="linkxsm" href="users.asp?mi=3"><b><%= IGmenu_mu %></b></a></td>
<% If Request("mi") = "3" Then %>
<td width="20" style="padding-right:2px;" align="right" nowrap><img src="/ig41sub/images/sm-cal.gif" width=16 height=14 border=0 alt=""></td>
<% Else %>
<td width="20" style="padding-right:2px;" align="right" nowrap><img src="/ig41sub/images/arrow-rt.gif" width=13 height=10 border=0 alt=""></td>
<% End If %>
</tr>
<tr>
<td height="17" align="right"><a class="linkxsm" href="logs.asp?mi=4"><b><%= IGmenu_ulf %></b></a></td>
<% If Request("mi") = "4" Then %>
<td width="20" style="padding-right:2px;" align="right" nowrap><img src="/ig41sub/images/sm-cal.gif" width=16 height=14 border=0 alt=""></td>
<% Else %>
<td width="20" style="padding-right:2px;" align="right" nowrap><img src="/ig41sub/images/arrow-rt.gif" width=13 height=10 border=0 alt=""></td>
<% End If %>
</tr>
</table>

<img src="/ig41sub/images/spacer.gif" width=1 height=5 border=0 alt=""><br>

<table cellspacing="0" cellpadding="2" border="0">
<tr>
<td><img src="/ig41sub/images/sm-system1.gif" alt="" width="14" height="14" border="0"></td>
<td class="textsm"><b><%= IGmenu_smngt %>:</b></td>
</tr>
</table>

<table width="200" cellspacing="0" cellpadding="0" border="0">
<tr>
<td height="17" align="right"><a class="linkxsm" href="system.asp?mi=5"><b><%= IGmenu_smaint %></b></a></td>
<% If Request("mi") = "5" Then %>
<td width="20" style="padding-right:2px;" align="right" nowrap><img src="/ig41sub/images/sm-cal.gif" width=16 height=14 border=0 alt=""></td>
<% Else %>
<td width="20" style="padding-right:2px;" align="right" nowrap><img src="/ig41sub/images/arrow-rt.gif" width=13 height=10 border=0 alt=""></td>
<% End If %>
</tr>
<tr>
<td height="17" align="right"><a class="linkxsm" href="javascript:void('0');" onClick="CompactRepair('compact-repair.asp');"><b><%= IGmenu_candr %></b></a></td>
<% If Request("mi") = "6" Then %>
<td width="20" style="padding-right:2px;" align="right" nowrap><img src="/ig41sub/images/sm-cal.gif" width=16 height=14 border=0 alt=""></td>
<% Else %>
<td width="20" style="padding-right:2px;" align="right" nowrap><img src="/ig41sub/images/arrow-rt.gif" width=13 height=10 border=0 alt=""></td>
<% End If %>
</tr>
</table>

<hr size="1" color="silver">

<% If MessageBoard Then %>
<script type="text/javascript">
<!--
var ie=document.all
var ns6=document.getElementById&&!document.all
var dragapproved=false
var z,x,y
function move(e){
if (dragapproved){
z.style.left=ns6? temp1+e.clientX-x: temp1+event.clientX-x
z.style.top=ns6? temp2+e.clientY-y : temp2+event.clientY-y
return false}}
function drags(e){
if (!ie&&!ns6)
return
var firedobj=ns6? e.target : event.srcElement
var topelement=ns6? "HTML" : "BODY"
while (firedobj.tagName!=topelement&&firedobj.className!="drag"){
firedobj=ns6? firedobj.parentNode : firedobj.parentElement}
if (firedobj.className=="drag"){
dragapproved=true
z=firedobj
temp1=parseInt(z.style.left+0)
temp2=parseInt(z.style.top+0)
x=ns6? e.clientX: event.clientX
y=ns6? e.clientY: event.clientY
document.onmousemove=move
return false}}
document.onmousedown=drags
document.onmouseup=new Function("dragapproved=false")
//-->
</script>
</div>
</div>
<% End If %><!-- ################ End i-Gallery Message Board ################# --><% End If %>
<!--include virtual="/ig41sub/searchform.asp"-->

</body></html>