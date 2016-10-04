<!--#INCLUDE FILE="sessioncheck.asp"-->
<!--#include file="db_connection.asp"-->
<%
Sub JavaRedirect2
	nav = "/admin/richtemplate_list_sections.aspx"
    main="intranet/index.asp"
%>

<script language="JavaScript">
    <!--
    
    top.basefrm.location 	= '<%=main%>';
    top.treeframe.location 	= '<%=nav%>';
   // parent.location.href = 
    //'<%=URL%>';
    //-->
</script>

<%
    End Sub
%>
<%
SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString
If request.querystring("pageType")="new" then

	Response.Redirect "richtemplate_editor.asp?id-1=&PAGEID="&Request.Querystring("pageid")&"&SECTIONID="&Request.Querystring("SECTIONID")&"&pagename="&Request.Querystring("name")&"&PAGELEVEL="&Request.Querystring("PAGELEVEL")&"&action="&Request.Querystring("action")&"&pageAction="&Request.Querystring("pageAction")&""


ElseIf Request.Querystring("pageType")="link" then

id 			= Request.Querystring("id")
RESPONSE.WRITE ID

if Request.form("LinkSelection")=1 then 

	Name	= Request.Form("txtName")
elseif Request.Form("linkSelection") = 3 then
	Name = Request.Form("txtName")
Else
	Name 	= Request.Form("LinkName")
End if

PARENTID 	= Request.Querystring("PAGEID")

SECTIONID 	= REQUEST.Querystring("SECTIONID")
myPublish 	= 1
PAGELEVEL 	= REQUEST.querystring("PAGELEVEL")

If Request.Form("LinkSelection")=2 then
	LinkURL		= Request.Form("LinkURL1")
elseif Request.Form("LinkSelection") = 3 then
	LinkURL		= Request.Form("linkURL3")
Else
	LinkURL		= Request.Form("targetpage")
End if

LinkOnly	= 1

If Request.Form("TargetFrame1")<>"" then
	LinkFrame = Request.Form("TargetFrame1")
else
	LinkFrame= Request.Form("targetFrame2")
end if

If LinkFrame = "Page Default (None)" then
	LinkFrame = ""
end if

'*********************************************************		
'Insert current record if ID =""
'*********************************************************	


  		if id = "-1" then    
  		
				
				'ADD PAGE RANKING
				
			
			
					MYsql="SELECT TOP 1 RANK FROM WEBINFO WHERE PARENTID= " & PARENTID & " ORDER BY RANK DESC"
					SET RS=CON.EXECUTE (MYsql)
					IF NOT RS.EOF THEN
					
					
						
					'RESPONSE.WRITE RS("RANK")
					
					IF RS("RANK")<1 OR RS("RANK")="" THEN
					
					
					'IF RS.EOF THEN
						MYRANK=1
						
						
					ELSE
						MYRANK= RS("RANK")+1
						
						
						
					END IF
					
		ELSE
					
			MYRANK=1
					
		END IF
									


	'******* ADD PAGE_LINKNAME TO RECORD SET *******
				
				
				ADDPageName = "SELECT PAGE_LINKNAME FROM WEBINFO WHERE SECTIONID="&REQUEST.QUERYSTRING("SECTIONID")&" AND pagelevel = 1 "
				SET RS3 = CON.EXECUTE (ADDPageName)

				PAGE_LINKNAME = RS3("PAGE_LINKNAME")
				End if
				
	'******* ADD PAGE_LINKNAME TO RECORD SET *******
				
				IF Session("secure_members") Then
						
				    if linkframe = "" then
				        AddWebpageSQL1 = "INSERT INTO WEBINFO (NAME, PARENTID, SECTIONID, RANK, PAGELEVEL, PAGE_LINKNAME, LINKURL, LINKONLY, PENDING, secure_members) VALUES ('" & Replace(name,"'","''") & "', " &PARENTID& ", '"&SECTIONID&"',  "&MYRANK&", '"&PAGELEVEL&"','"&PAGE_LINKNAME&"','"&LINKURL&"','"&LINKONLY&"', 0, 1)"
				    else
				        AddWebpageSQL1 = "INSERT INTO WEBINFO (NAME, PARENTID, SECTIONID, RANK, PAGELEVEL, PAGE_LINKNAME, LINKURL, LINKONLY, LINKFRAME, PENDING, secure_members) VALUES ('" & Replace(name,"'","''") & "', " &PARENTID& ", '"&SECTIONID&"',  "&MYRANK&", '"&PAGELEVEL&"','"&PAGE_LINKNAME&"','"&LINKURL&"','"&LINKONLY&"', '"&LINKFRAME&"', 0, 1)"
				    end if
	
				ElseIf Session("secure_education") Then
                    if linkframe = "" then
				        AddWebpageSQL1 = "INSERT INTO WEBINFO (NAME, PARENTID, SECTIONID, RANK, PAGELEVEL, PAGE_LINKNAME, LINKURL, LINKONLY, PENDING,secure_education) VALUES ('" & Replace(name,"'","''") & "', " &PARENTID& ", '"&SECTIONID&"',  "&MYRANK&", '"&PAGELEVEL&"','"&PAGE_LINKNAME&"','"&LINKURL&"','"&LINKONLY&"', 0, 1)"
				    else
				        AddWebpageSQL1 = "INSERT INTO WEBINFO (NAME, PARENTID, SECTIONID, RANK, PAGELEVEL, PAGE_LINKNAME, LINKURL, LINKONLY, LINKFRAME, PENDING,secure_education) VALUES ('" & Replace(name,"'","''") & "', " &PARENTID& ", '"&SECTIONID&"',  "&MYRANK&", '"&PAGELEVEL&"','"&PAGE_LINKNAME&"','"&LINKURL&"','"&LINKONLY&"', '"&LINKFRAME&"', 0, 1)"
				    end if
				
				Else
			
				    if linkframe = "" then
				        AddWebpageSQL1 = "INSERT INTO WEBINFO (NAME, PARENTID, SECTIONID, RANK, PAGELEVEL, PAGE_LINKNAME, LINKURL, LINKONLY, PENDING) VALUES ('" & Replace(name,"'","''") & "', " &PARENTID& ", '"&SECTIONID&"',  "&MYRANK&", '"&PAGELEVEL&"','"&PAGE_LINKNAME&"','"&LINKURL&"','"&LINKONLY&"', 0)"
				    else
				        AddWebpageSQL1 = "INSERT INTO WEBINFO (NAME, PARENTID, SECTIONID, RANK, PAGELEVEL, PAGE_LINKNAME, LINKURL, LINKONLY, LINKFRAME, PENDING) VALUES ('" & Replace(name,"'","''") & "', " &PARENTID& ", '"&SECTIONID&"',  "&MYRANK&", '"&PAGELEVEL&"','"&PAGE_LINKNAME&"','"&LINKURL&"','"&LINKONLY&"', '"&LINKFRAME&"', 0)"
				    end if
				
				End if
				
				'response.write addwebpagesql1
				con.execute (AddWebpageSQL1)

				Response.redirect "richtemplate_list_pages.aspx?sectionid="&sectionid&""
			
			
				CON.CLOSE
				SET CON = NOTHING
				SET RS	= NOTHING
				SET RS3 = NOTHING
				
			
%>

<script language="JavaScript">
    <!--
    
    window.opener.top.basefrm.location 	= '<%=main%>';
    window.close(); 

    //'<%=URL%>';
    //-->
</script>

<%
end if

%>
<%
SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

Pname = "Add a new web page or link"

If Request.Querystring("submit")="yes" then
end if

%>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <title>New Page 2</title>
    <link rel="stylesheet" type="text/css" href="style_richtemplate.css">
</head>
<body topmargin="0" leftmargin="0">
    <!--#INCLUDE FILE="headernew.inc"-->
    <form method="POST" action="richtemplate_externallinks.asp?pagetype=link&id=-1&sectionid=<%=request.querystring("sectionid")%>&PAGEID=<%=Request.Querystring("pageid")%>&PAGELEVEL=<%=Request.Querystring("PAGELEVEL")%>&PARENTID=<%=Request.Querystring("parentid")%>&ACTION=<%=Request.Querystring("Action")%>">
    <input type="hidden" id="txtName" style="font-size: 11px; font-family: arial; height: 20;
        width: 271" name="txtName" size="60">
    <table border="0" width="500" id="table1">
        <tr>
            <td class="bodybold" colspan="4">
                Please select the type of new page you would like to create:
            </td>
        </tr>
        <tr>
            <td class="bodybold" width="1%">
                &nbsp;
            </td>
            <td class="bodybold" style="width: 3%">
                &nbsp;
            </td>
            <td class="bodybold">
                &nbsp;
            </td>
            <td width="76%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="bodybold" colspan="4" height="30">
                <table border="0" width="100%" id="table5" bgcolor="#F4F4F4" style="border: 1px solid #3055A9">
                    <tr>
                        <td class="bodybold" width="1%">
                            &nbsp;
                        </td>
                        <td class="bodybold" width="3%">
                            <img border="0" src="images/icon_link1.gif">
                        </td>
                        <td class="bodybold" height="30">
                            <a href='/admin/richtemplate_page_editor.aspx?parentID=<%=Request.Querystring("pageid")%>'>
                                <font size="2" color="#3054A9">This page will be created within the WYSIWYG editor</font></a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="bodybold" colspan="4" height="30">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="bodybold" colspan="4" height="30">
                <table border="0" width="100%" id="table4" style="border: 1px solid #3055A9" bgcolor="#F4F4F4">
                    <tr>
                        <td class="bodybold" width="1%">
                            &nbsp;
                        </td>
                        <td class="bodybold" width="3%">
                            <img border="0" src="images/icon_link1.gif">
                        </td>
                        <td class="bodybold" colspan="2" height="30">
                            <font size="2" color="#3054A9">This will be a link to a web page outside your domain</font>
                        </td>
                    </tr>
                    <tr>
                        <td class="bodybold" width="1%">
                            &nbsp;
                        </td>
                        <td class="bodybold" width="3%">
                            &nbsp;
                        </td>
                        <td class="bodybold">
                            Target Frame:
                        </td>
                        <td width="76%">
                            <select size="1" name="TargetFrame1">
                                <option selected value="">Page Default (None)</option>
                                <option value="_blank">New Window</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="bodybold" width="1%">
                            &nbsp;
                        </td>
                        <td class="bodybold" width="3%">
                            &nbsp;
                        </td>
                        <td class="bodybold">
                            Link Name:
                        </td>
                        <td width="76%" class="body">
                            <input style="font-size: 11px; font-family: arial; height: 20; width: 157" name="LinkName"
                                size="50" onchange="linkID.value = 2">
                        </td>
                    </tr>
                    <tr>
                        <td class="bodybold" width="1%">
                            &nbsp;
                        </td>
                        <td class="bodybold" width="3%">
                            &nbsp;
                        </td>
                        <td class="bodybold">
                            Link URL:
                        </td>
                        <td width="76%" class="body">
                            <input id="txtName0" style="font-size: 11px; font-family: arial; height: 20; width: 267"
                                name="LinkURL1" size="60" value="http://">
                        </td>
                    </tr>
                    <tr>
                        <td height="20" class="bodybold" colspan="4">
                            <p align="center" class="body">
                            (www.domainname.com)
                        </td>
                    </tr>
                    <tr>
                        <td height="20" class="bodybold">
                            <p class="body" align="center">
                            &nbsp;
                        </td>
                        <td height="20" class="bodybold">
                            &nbsp;
                        </td>
                        <td height="20" class="bodybold">
                            &nbsp;
                        </td>
                        <td height="20" class="bodybold">
                            <input type="submit" value="Submit" name="B2">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="30" class="bodybold" colspan="4" width="100%">
                <p align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td height="30" class="bodybold" colspan="4">
                <table border="0" width="100%" id="table3" style="border: 1px solid #3055A9" bgcolor="#F4F4F4">
                    <tr>
                        <td style="width: 1%" height="30" class="bodybold">
                            &nbsp;
                        </td>
                        <td style="width: 3%" height="30" class="bodybold">
                            <img border="0" src="images/icon_link1.gif">
                        </td>
                        <td height="30" class="bodybold" colspan="2">
                            <font size="2" color="#3054A9">This will be a link to an existing web page within your
                                domain</font>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 1%" class="bodybold">
                            &nbsp;
                        </td>
                        <td style="width: 3%" class="bodybold">
                            &nbsp;
                        </td>
                        <td style="width: 18%" class="bodybold">
                            Target Frame
                        </td>
                        <td width="76%" class="body">
                            <select size="1" name="TargetFrame2">
                                <option selected>Page Default (None)</option>
                                <option value="_blank">New Window</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 1%" class="bodybold">
                            &nbsp;
                        </td>
                        <td style="width: 3%" class="bodybold">
                            &nbsp;
                        </td>
                        <td style="width: 18%" class="bodybold">
                            Select&nbsp; Page:
                        </td>
                        <td width="76%" class="body">
                            <select name="targetpage" class="INPUT" onchange="txtUrl.value = targetpage[targetpage.selectedIndex].value;  txtName.value = targetpage[targetpage.selectedIndex].id; linkID.value = 1; txtUrl.focus()">
                                <option value="">Choose link page</option>
                                <%

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

'mySQL = "Select * from WEBINFO "
mySQL = "Select w1.*, w2.name AS parentName from WEBINFO w1 LEFT JOIN WEBINFO w2 ON w1.ParentId = w2.ID"
'response.write mysql
set rs=con.execute(mySQL)
while not rs.eof
URLvalue = RS("PAGE_LINKNAME")

pageURL = RS("name")
pageURL = Replace(pageURL, " ", "-")
if RS("parentName") <> "" then
    parentURL = RS("parentName")
    parentURL = Replace(parentURL, " ", "-")
end if

if RS("parentName") <> "" then
                                %>
                                <option value="/<%=parentURL%>/<%=pageURL%>.aspx" id="<%=RS("Name")%>">
                                    <%=rs("Name") %></option>
                                <!--<option value="/web/page/<%=rs("id")%>/interior.html"><%=rs("Name") %></option>-->
                                <%
else
                                %>
                                <option value="/<%=pageURL%>.aspx" id="<%=RS("Name")%>">
                                    <%=rs("Name") %></option>
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
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="bodybold" width="1%">
                            &nbsp;
                        </td>
                        <td class="bodybold" width="3%">
                            &nbsp;
                        </td>
                        <td class="bodybold">
                            <div id="3027" class="label40">
                                Existing URL:</div>
                        </td>
                        <td width="76%" class="body">
                            <input id="txtUrl" style="font-size: 11px; font-family: arial; height: 20; width: 271"
                                name="LinkURL2" size="60">
                        </td>
                    </tr>
                    <tr>
                        <td class="bodybold" width="1%">
                            &nbsp;
                        </td>
                        <td class="bodybold" width="3%">
                            &nbsp;
                        </td>
                        <td class="bodybold">
                            &nbsp;
                        </td>
                        <td width="76%" class="body">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="bodybold" width="1%">
                            &nbsp;
                        </td>
                        <td class="bodybold" width="3%">
                            &nbsp;
                        </td>
                        <td class="bodybold">
                            &nbsp;
                        </td>
                        <td width="76%" class="body">
                            <input type="submit" value="Submit" name="B1">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="1%">
                &nbsp;
            </td>
            <td style="width: 3%">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td width="76%">
                <input type="hidden" id="linkID" type="text" name="LinkSelection" size="20">&nbsp;
            </td>
        </tr>
        <%If Session("mode")="micro" then%>
        <tr>
            <td width="1%" colspan="4" style="width: 77%">
                <table border="0" width="100%" id="table3" style="border: 1px solid #3055A9" bgcolor="#F4F4F4">
                    <tr>
                        <td style="width: 1%" height="30" class="bodybold">
                            &nbsp;
                        </td>
                        <td style="width: 3%" height="30" class="bodybold">
                            <img border="0" src="images/icon_link1.gif">
                        </td>
                        <td height="30" class="bodybold" colspan="2">
                            <font size="2" color="#3054A9">This will be link to a MicroSite</font>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 1%" class="bodybold">
                            &nbsp;
                        </td>
                        <td style="width: 3%" class="bodybold">
                            &nbsp;
                        </td>
                        <td style="width: 18%" class="bodybold">
                            Select&nbsp; Page:
                        </td>
                        <td width="76%" class="body">
                            <select name="targetpage3" class="INPUT" onchange="txtUrl2.value = targetpage3[targetpage3.selectedIndex].value;  txtName.value = targetpage3[targetpage3.selectedIndex].id; linkID.value = 3; txtUrl2.focus()">
                                <option value="">Choose link page</option>
                                <%

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

mySQL = "SELECT * FROM MICROSITE_PARTNERS WHERE BOLARCHIVE <> 1 ORDER BY LASTNAME, FIRSTNAME"
set rs=con.execute(mySQL)
while not rs.eof
                                %>
                                <option value="/mshome/?id=<%=rs("partnerID")%>" id="<%=RS("firstName")&" "&RS("lastName")%>">
                                    <%=RS("lastName")%>,&nbsp;<%=rs("firstName")%></option>
                                <%
rs.movenext
wend

rs.close
set rs=nothing
set mySQL = nothing
set URLvalue = nothing
con.close
                                %>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="bodybold" width="1%">
                            &nbsp;
                        </td>
                        <td class="bodybold" width="3%">
                            &nbsp;
                        </td>
                        <td class="bodybold">
                            <div id="3027" class="label40">
                                Existing URL:</div>
                        </td>
                        <td width="76%" class="body">
                            <input id="txtUrl2" style="font-size: 11px; font-family: arial; height: 20; width: 271"
                                name="LinkURL3" size="60">
                        </td>
                    </tr>
                    <tr>
                        <td class="bodybold" width="1%">
                            &nbsp;
                        </td>
                        <td class="bodybold" width="3%">
                            &nbsp;
                        </td>
                        <td class="bodybold">
                            &nbsp;
                        </td>
                        <td width="76%" class="body">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="bodybold" width="1%">
                            &nbsp;
                        </td>
                        <td class="bodybold" width="3%">
                            &nbsp;
                        </td>
                        <td class="bodybold">
                            &nbsp;
                        </td>
                        <td width="76%" class="body">
                            <input type="submit" value="Submit" name="B1">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%end if%>
        <tr>
            <td width="1%">
                &nbsp;
            </td>
            <td style="width: 3%">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td width="76%">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
