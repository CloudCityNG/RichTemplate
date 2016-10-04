<!--#include virtual="/admin/db_connection.asp"-->
<!--#include virtual="/admin/sessioncheck.asp"-->


<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link href="style/editor.css" rel="stylesheet" type="text/css">
<script>
	var sLangDir=dialogArguments.oUtil.langDir;
	document.write("<scr"+"ipt src='language/"+sLangDir+"/hyperlink.js'></scr"+"ipt>");
</script>
<script>writeTitle()</script>
<script>
function GetElement(oElement,sMatchTag)
	{
	while (oElement!=null&&oElement.tagName!=sMatchTag)
		{
		if(oElement.tagName=="BODY")return null;
		oElement=oElement.parentElement;
		}
	return oElement;
	}
	
function doWindowFocus()
	{
	dialogArguments.oUtil.onSelectionChanged=new Function("realTime()");
	}
function bodyOnLoad()
	{
	window.onfocus=doWindowFocus;	
	dialogArguments.oUtil.onSelectionChanged=new Function("realTime()");

	if(dialogArguments.oUtil.obj.cmdAssetManager!="")btnAsset.style.display="block";
	if(dialogArguments.oUtil.obj.cmdFileManager!="")btnAsset.style.display="block";

	realTime()
	}
function openAsset()
	{
	if(dialogArguments.oUtil.obj.cmdAssetManager!="")
		inpURL.value=eval(dialogArguments.oUtil.obj.cmdAssetManager);
	if(dialogArguments.oUtil.obj.cmdFileManager!="")
		inpURL.value=eval(dialogArguments.oUtil.obj.cmdFileManager);	
	}
function modalDialogShow(url,width,height)
	{
	return window.showModalDialog(url,window,
		"dialogWidth:"+width+"px;dialogHeight:"+height+"px;edge:Raised;center:Yes;help:No;Resizable:Yes;Maximize:Yes");
	}
function updateList()
	{
	var oEditor=dialogArguments.oUtil.oEditor;
	
	while(inpBookmark.options.length!=0) 
		{
		inpBookmark.options.remove(inpBookmark.options(0))
		}
	for(var i=0;i<oEditor.document.anchors.length;i++)
		{
		var op = document.createElement("OPTION");
		op.text=oEditor.document.anchors[i].name;
		op.value="#"+oEditor.document.anchors[i].name;
		inpBookmark.options.add(op);		
		}
	}
function realTime()
	{
	if(!dialogArguments.oUtil.obj.checkFocus()){return;}//Focus stuff
	var oEditor=dialogArguments.oUtil.oEditor;
	var oSel=oEditor.document.selection.createRange();
	var sType=oEditor.document.selection.type;
	
	updateList();

	//If text or control is selected, Get A element if any
	if (oSel.parentElement)	oEl=GetElement(oSel.parentElement(),"A");
	else oEl=GetElement(oSel.item(0),"A");

	//Is there an A element ?
	if (oEl)
		{
		btnInsert.style.display="none";
		btnApply.style.display="block";
		btnOk.style.display="block";
		

		//~~~~~~~~~~~~~~~~~~~~~~~~
		sTmp=oEl.outerHTML;
		if(sTmp.indexOf("href")!=-1) //1.5.1
			{
			sTmp=sTmp.substring(sTmp.indexOf("href")+6);
			sTmp=sTmp.substring(0,sTmp.indexOf('"'));
			var arrTmp = sTmp.split("&amp;");
			if (arrTmp.length > 1) sTmp = arrTmp.join("&");		
			sURL=sTmp
			//sURL=oEl.href;
			}
		else
			{
			sURL=""
			}

		if(sType!="Control")
			{
			try
				{			
				var oSelRange = oEditor.document.body.createTextRange()
				oSelRange.moveToElementText(oEl)
				oSel.setEndPoint("StartToStart",oSelRange);
				oSel.setEndPoint("EndToEnd",oSelRange);
				oSel.select();
				}
			catch(e){return;}
			}
		
		inpTarget.value="";
		inpTargetCustom.value="";
		if(oEl.target=="_self" || oEl.target=="_blank" || oEl.target=="_parent")
			inpTarget.value=oEl.target;//inpTarget
		else
			inpTargetCustom.value=oEl.target;
		
		inpTitle.value="";
		if(oEl.title!=null) inpTitle.value=oEl.title;//inpTitle //1.5.1


		if(sURL.substr(0,7)=="http://")
			{
			inpType.value="http://";//inpType
			inpURL.value=sURL.substr(7);//idLinkURL

			inpBookmark.disabled=true;
			inpURL.disabled=false;
			inpType.disabled=false;
			rdoLinkTo[0].checked=true;
			rdoLinkTo[1].checked=false;
			}
		else if(sURL.substr(0,8)=="https://")
			{
			inpType.value="https://";
			inpURL.value=sURL.substr(8);

			inpBookmark.disabled=true;
			inpURL.disabled=false;
			inpType.disabled=false;
			rdoLinkTo[0].checked=true;
			rdoLinkTo[1].checked=false;
			}
		else if(sURL.substr(0,7)=="mailto:")
			{
			inpType.value="mailto:";
			inpURL.value=sURL.split(":")[1];

			inpBookmark.disabled=true;
			inpURL.disabled=false;
			inpType.disabled=false;
			rdoLinkTo[0].checked=true;
			rdoLinkTo[1].checked=false;
			}
		else if(sURL.substr(0,6)=="ftp://")
			{
			inpType.value="ftp://";
			inpURL.value=sURL.substr(6);

			inpBookmark.disabled=true;
			inpURL.disabled=false;
			inpType.disabled=false;
			rdoLinkTo[0].checked=true;
			rdoLinkTo[1].checked=false;
			}
		else if(sURL.substr(0,5)=="news:")
			{
			inpType.value="news:";
			inpURL.value=sURL.split(":")[1];

			inpBookmark.disabled=true;
			inpURL.disabled=false;
			inpType.disabled=false;
			rdoLinkTo[0].checked=true;
			rdoLinkTo[1].checked=false;
			}
		else if(sURL.substr(0,11).toLowerCase()=="javascript:")
			{
			inpType.value="javascript:";
			//inpURL.value=sURL.split(":")[1];
			inpURL.value=sURL.substr(sURL.indexOf(":")+1);

			inpBookmark.disabled=true;
			inpURL.disabled=false;
			inpType.disabled=false;
			rdoLinkTo[0].checked=true;
			rdoLinkTo[1].checked=false;
			}
		else
			{
			inpType.value="";

			if(sURL.substring(0,1)=="#")
				{
				inpBookmark.value=sURL;
				inpURL.value="";
				inpBookmark.disabled=false;
				inpURL.disabled=true;
				inpType.disabled=true;
				rdoLinkTo[0].checked=false;
				rdoLinkTo[1].checked=true;
				}
			else
				{
				inpBookmark.value=""
				inpURL.value=sURL;
				inpBookmark.disabled=true;
				inpURL.disabled=false;
				inpType.disabled=false;
				rdoLinkTo[0].checked=true;
				rdoLinkTo[1].checked=false;
				}
			}
		}
	else
		{
		btnInsert.style.display="block";
		btnApply.style.display="none";
		btnOk.style.display="none";

		inpTarget.value="";
		inpTargetCustom.value="";
		inpTitle.value="";
		
		inpType.value="";
		inpURL.value="";
		inpBookmark.value="";
		
		inpBookmark.disabled=true;
		inpURL.disabled=false;
		inpType.disabled=false;
		rdoLinkTo[0].checked=true;
		rdoLinkTo[1].checked=false;
		}			
	}

function applyHyperlink()
	{
	if(!dialogArguments.oUtil.obj.checkFocus()){return;}//Focus stuff
	var oEditor=dialogArguments.oUtil.oEditor;
	var oSel=oEditor.document.selection.createRange();
	
	dialogArguments.oUtil.obj.saveForUndo();

	var sURL;
	
	sURL = inpType.value + inpURL.value.replace('/secure/', '/secure/xxxxxxxxxxxxxxxx/');
	sURL = sURL.replace('/xxxxxxxxxxxxxxxx/xxxxxxxxxxxxxxxx/', '/xxxxxxxxxxxxxxxx/');
	
	if (rdoLinkTo[0].checked)

	    sURL = sURL

	//sURL=inpType.value + inpURL.value;
	else
	    sURL = inpBookmark.value;

	if((inpURL.value!="" && rdoLinkTo[0].checked) ||
		(inpBookmark!="" && rdoLinkTo[1].checked))
		{
		if (oSel.parentElement)
			{
			if(btnInsert.style.display=="block")
				{
				if(oSel.text=="")//If no (text) selection, then build selection using the typed URL
					{
					var oSelTmp=oSel.duplicate();
					oSel.text=sURL;
					oSel.setEndPoint("StartToStart",oSelTmp);
					oSel.select();
					}
				}
			}
		
		oSel.execCommand("CreateLink",false,sURL);

		//get A element
		if (oSel.parentElement)	oEl=GetElement(oSel.parentElement(),"A");
		else oEl=GetElement(oSel.item(0),"A");
		if(oEl)
			{
			if(inpTarget.value=="" && inpTargetCustom.value=="") oEl.removeAttribute("target",0);//target
			else 
				{
				if(inpTargetCustom.value!="")
					oEl.target=inpTargetCustom.value;
				else
					oEl.target=inpTarget.value;
				}
			
			if(inpTitle.value=="") oEl.removeAttribute("title",0);//1.5.1
			else oEl.title=inpTitle.value;
			}
			
		dialogArguments.realTime(dialogArguments.oUtil.oName);
		dialogArguments.oUtil.obj.selectElement(0);
		}
	else
		{
		oSel.execCommand("unlink");//unlink
		
		dialogArguments.realTime(dialogArguments.oUtil.oName);
		dialogArguments.oUtil.activeElement=null;
		}	
	realTime();
	}

function changeLinkTo()
	{
	if(rdoLinkTo[0].checked)
		{
		inpBookmark.disabled=true;
		inpURL.disabled=false;
		inpType.disabled=false;
		}
	else
		{
		inpBookmark.disabled=false;
		inpURL.disabled=true;
		inpType.disabled=true;
		}
	}			
</script>
</head>
<body onload="loadTxt();bodyOnLoad()" style="overflow:hidden;">

<table width=100% height=100% align=center cellpadding=0 cellspacing=0>
<tr>
<td valign=top style="padding:5;height:100%">
	<table width=100%>
	<tr>
		<td nowrap>
			Existing Page</td>
		<td width="100%">
			 <select name="targetpage" CLASS=INPUT onChange="inpURL.value = targetpage[targetpage.selectedIndex].value; targetpage.value = ''; inpURL.focus()">
    <option value="">Choose link page</option>
<%

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

'mySQL = "Select * from WEBINFO "
mySQL = "Select w1.*, w2.name AS parentName from WEBINFO w1 LEFT JOIN WEBINFO w2 ON w1.ParentId = w2.ID order by name"
'response.write mysql
set rs=con.execute(mySQL)
while not rs.eof
URLvalue = RS("PAGE_LINKNAME")

pageURL = RS("name")
    pageURL = Replace(pageURL, "-", "002D")
    pageURL = Replace(pageURL, " ", "-")
    pageURL = Replace(pageURL, "#", "%23")
    pageURL = Replace(pageURL, "(", "%28")
    pageURL = Replace(pageURL, ")", "%29")
    pageURL = Replace(pageURL, "&", "0024")
    pageURL = Replace(pageURL, "?", "003F")
    pageURL = Replace(pageURL, "+", "002B")
    pageURL = Replace(pageURL, "\", "005C")
    pageURL = Replace(pageURL, "/", "002F")
    pageURL = Replace(pageURL, "*", "000")
    pageURL = Replace(pageURL, ":", "003A")
    pageURL = Replace(pageURL, ",", "%2C")
    pageURL = Replace(pageURL, ".", "002E")


if RS("parentName") <> "" then
    parentURL = RS("parentName")
    parentURL = Replace(parentURL, "-", "002D")
    parentURL = Replace(parentURL, " ", "-")
    parentURL = Replace(parentURL, "#", "%23")
    parentURL = Replace(parentURL, "(", "%28")
    parentURL = Replace(parentURL, ")", "%29")
    parentURL = Replace(parentURL, "&", "0024")
    parentURL = Replace(parentURL, "?", "003F")
    parentURL = Replace(parentURL, "+", "002B")
    parentURL = Replace(parentURL, "\", "005C")
    parentURL = Replace(parentURL, "/", "002F")
    parentURL = Replace(parentURL, "*", "000")
    parentURL = Replace(parentURL, ":", "003A")
    parentURL = Replace(parentURL, ",", "%2C")
    parentURL = Replace(parentURL, ".", "002E")

end if

if RS("parentName") <> "" then

If RS("secure_education") = True then
    parentURL = "OnlineEducation/" & parentURL
Elseif RS("secure_members") = True then
    parentURL = "Members/" & parentURL
End if
%>
<option value="/<%=parentURL%>/<%=pageURL%>.aspx"><%=rs("Name") %></option>
<!--<option value="/web/page/<%=rs("id")%>/interior.html"><%=rs("Name") %></option>-->
<%
else

If RS("secure_education") = True then
    pageURL = "OnlineEducation/" & pageURL
Elseif RS("secure_members") = True then
    pageURL = "Members/" & pageURL
End if

%>
<option value="/<%=pageURL%>.aspx"><%=rs("Name") %></option>
<%
end if


rs.movenext
wend

rs.close
set rs=nothing
set mySQL = nothing
set URLvalue = nothing
con.close
%>


</select></td>
	</tr>
	<tr>
		<td nowrap>
			<input type="radio" value="url" name="rdoLinkTo" class="inpRdo" checked onclick="changeLinkTo()">
			<span id="txtLang" name="txtLang">Source</span>:
		</td>
		<td width="100%">
			<table cellpadding="0" cellspacing="0" width="100%">
			<tr>
			<td nowrap>			
			<select ID="inpType" NAME="inpType" class="inpSel" type="hidden">
				<option value=""></option>
				<option value="http://">http://</option>
				<option value="https://">https://</option>
				<option value="mailto:">mailto:</option>
				<option value="ftp://">ftp://</option>
				<option value="news:">news:</option>
				<option value="javascript:">javascript:</option>
			
			</select>
			</td>
			<td width="100%"><INPUT type="text" ID="inpURL" NAME="inpURL" style="width:100%" class="inpTxt"></td>
			<td><input type="button" value="" onclick="openAsset()" id="btnAsset" name="btnAsset" style="display:none;background:url('openAsset.gif');width:23px;height:18px;border:#a5acb2 1px solid;margin-left:1px;"></td>
			</tr>
			</table>		
		</td>
	</tr>
	<tr>
		<td nowrap>
			<input type="radio" value="bookmark" name="rdoLinkTo" class="inpRdo" onclick="changeLinkTo()">
			<span id="txtLang" name="txtLang">Bookmark</span>:
		</td>
		<td>
		<select name="inpBookmark" class="inpSel" disabled style="width:160px">
		</select></td>
	</tr>
	<tr>
		<td nowrap>&nbsp;<span id="txtLang" name="txtLang">Target</span>:</td>
		<td><INPUT type="text" ID="inpTargetCustom" NAME="inpTargetCustom" size=15 class="inpTxt">
		<select ID="inpTarget" NAME="inpTarget" class="inpSel">
			<option value=""></option>
			<option value="_self" id="optLang" name="optLang">Self</option>
			<option value="_blank" id="optLang" name="optLang">Blank</option>
			<option value="_parent" id="optLang" name="optLang">Parent</option>
		</select></td>
	</tr>
	<tr>
		<td nowrap>&nbsp;<span id="txtLang" name="txtLang">Title</span>:</td>
		<td><INPUT type="text" ID="inpTitle" NAME="inpTitle" style="width:160px" class="inpTxt"></td>
	</tr>
	</table>
</td>
</tr>
<tr>
<td class="dialogFooter" style="padding:6;" align="right">
	<table cellpadding=1 cellspacing=0>
	<td>
	<input type=button name=btnCancel id=btnCancel value="cancel" onclick="self.close()" class="inpBtn" onmouseover="this.className='inpBtnOver';" onmouseout="this.className='inpBtnOut'">
	</td>
	<td>
	<input type=button name=btnInsert id=btnInsert value="insert" onclick="applyHyperlink();" class="inpBtn" onmouseover="this.className='inpBtnOver';" onmouseout="this.className='inpBtnOut'">
	</td>
	<td>
	<input type=button name=btnApply id=btnApply value="apply" style="display:none" onclick="applyHyperlink()" class="inpBtn" onmouseover="this.className='inpBtnOver';" onmouseout="this.className='inpBtnOut'">
	</td>
	<td>
	<input type=button name=btnOk id=btnOk value=" ok " style="display:none;" onclick="applyHyperlink();self.close()" class="inpBtn" onmouseover="this.className='inpBtnOver';" onmouseout="this.className='inpBtnOut'">
	</td>
	</table>
</td>
</tr>
</table>

</body>
</html>