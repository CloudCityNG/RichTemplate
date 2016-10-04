<!--#include virtual="/admin/db_connection.asp"-->
<!--#INCLUDE virtual="/admin/sessioncheck.asp"-->
<%PNAME = "Edit your website content"%>
<%


'response.write session("userid")
'Get all root level web pages
currentStatus="initialized"
If Request.Querystring("action")="newSection" then
	webAction 	= 1
	pageStatus1 = ""
	pageStatus2 = "checked"
	currentStatus="new section"
	isSecure = Session("secure_members")
elseIf Request.Querystring("action") = "add" then
	webAction 	= 2
	pageStatus1	= "checked"
	pageStatus2 = ""
	parentID = Request.QueryString("pageID")
	sectionID = Request.Querystring("sectionID")
	isSecure = Session("secure_members")
	defaultPage = 0
	pageLevel = Request.Querystring("pageLevel")
	currentStatus="add"
elseif Request.Querystring("pageID") <>"" then
	webAction 	= 3
	pageID 		= Request.Querystring("pageID")
	pageStatus	= Request.Querystring("pageStatus")
	currentStatus="edit"

	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString

	getRoot = "SELECT * FROM WEBINFO WHERE ID = "&pageID&""
	SET RS2 = CON.EXECUTE(getRoot)
	If Not RS2.EOF then
		WebSectionName 	= RS2("NAME")
		beenUpdated = true

			If pageStatus <> "offline" then
				pageContent 	= RS2("message")
				pageStatus1		= ""
				pageStatus2		= "checked"

				
			Else
				pageContent 	= RS2("message2")
				pageStatus1		= "checked"
				pageStatus2		= ""

			End if
				pageName 	= RS2("name")
				defaultpage = RS2("defaultpage")
				sectionID	= RS2("SectionID")
				pageLevel	= RS2("pageLevel")
				parentID	= RS2("parentID")
				lastModified= RS2("LAST_MODIFIED")
				lastMod		= RS2("author")
				searchable	= RS2("searchable")
				secure_members = RS2("secure_members")
				
				
				
	'Mark database for open record
		checkUp = "Update Webinfo set checked_out = 1, checked_id = '"&Session("userName")&"' where ID = "&pageID&""
		'response.write checkup
		con.execute(checkUp)				
		session(pageID) = True
			
				
				

				
	End if
	RS2.Close
	Con.Close
	

Else
response.write ("error")
	
end if

main = "/admin/richtemplate_page_logic.asp?task=checkin&pageID="&pageID&"&sectionID="&SectionID&""

%>
<html>
<head>

    <script language="Javascript">

function ValidateForm(){
	// comment out any validation that is not needed
	 // regular expression to match alphanumeric characters and spaces
    var re = /^[\w ]+$/;
    
    // validation fails if the input doesn't match the regular expression
    //if(!re.test(document.form1.pagename.value)) { alert("Error: The page name contains invalid characters!"); document.form1.pagename.focus();return false; }
	if (document.form1.pagename.value.length < 1) { alert("Please enter web page name."); return false; }
	//else if (document.form1.pagename.value.length < 1) { alert("Please enter a name for this page.");document.form1.pagename.focus(); return false; }
	else if (!document.form1["myPublish"][0].checked && !document.form1["myPublish"][1].checked) {alert("Please choose if the display status of this page."); document.form1["myPublish"][0].focus(); return false; }
	else if (document.form1.txtContent.value.length < 1) { alert("Please enter web page content."); return false; }
    return true
 }
    </script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">

    <script language="JavaScript" src='../scripts/innovaeditor.js'></script>

    <script language="JavaScript">
<!-- Script courtesy of http://www.web-source.net - Your Guide to Professional Web Site Design and Development
function load() {
var load = window.open('/editor2/complete/metaInfo.asp?pageid=<%=pageid%>&sectionid=<%=sectionid%>','','scrollbars=no,menubar=no,height=300,width=400,resizable=yes,toolbar=no,location=no,status=no');
}
// -->
    </script>

    <script language="JavaScript" src="/editor2/ieSpell.js"></script>

    <link rel="stylesheet" type="text/css" href="/editorConfig/css/editorStyle.css">
    <link rel="stylesheet" type="text/css" href="/admin/styles.css">
    <link rel="stylesheet" type="text/css" href="/admin/css/RichTemplate.css">
</head>
<body topmargin="0" leftmargin="0" rightmargin="0" bottommargin="0">
    <!--#INCLUDE virtual="/admin/headernew.inc"-->
    <%If webAction = 1 then%>
    <form name="form1" action="/admin/richtemplate_page_logic.asp?task=newsection" method="post"
    onsubmit="return ValidateForm();">
    <%elseif webAction = 2 then%>
    <form name="form1" action="/admin/richtemplate_savepage.asp?pageID=-1" method="post"
    onsubmit="return ValidateForm();">
    <%else%>
    <form name="form1" action="/admin/richtemplate_savepage.asp?pageID=<%=pageID%>" method="post"
    onsubmit="return ValidateForm();">
    <%end if%>
    <table>
        <tr>
            <td class="body">
                Web page name:
                <input type="TEXT" name="pagename" value="<%=pageName%>" size="44">&nbsp;&nbsp;&nbsp;&nbsp;
                <!--#INCLUDE VIRTUAL ="/admin/tagsmodule/tagConfig.inc"-->
                <%If tagModActive = True then%>
                <%If pageStatus <> "offline" then%>
                <%If searchable = True then
			searchCheck = "checked"
		End if%>
                <!--<input name="searchable" type="checkbox" style="width: 20px; height: 20px" value="-1"
                                <%=searchCheck%>>
                            <%if pageid = "497" then%>
                            Display QuickLinks
                            <%elseif pageid = "498" then%>
                            Display on Homepage
                            <%else%>
                            Searchable<%end if%>
                            <%end if%>-->
                <%end if%>
            </td>
            <td align="left" class="body">
                <input type="radio" value="0" <%=pagestatus1%> name="myPublish">Save page as draft<br>
                <%IF SESSION("ALLOW_PUBLISH") = TRUE THEN%>
                <input type="radio" value="1" <%=pageStatus2%> name="myPublish">Publish page live
                <%END IF%>&nbsp;
            </td>
        </tr>
        <%If isSecure Or secure_members then%>
        <tr>
            <td colspan="2">
                <span style="font-size:12px;" class="moduleLabel">Add this page to a Group:</span>
                <table border="0">
                    <tbody>
                 
                        <% 
    SET CON_Groups = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON_Groups.OPEN ConnectionString
	
	If pageID > 0 then
	    If parentID > 0 Then
            getGroups = "SELECT distinct ag.*, Case WHEN wig.webinfoID Is NULL Then 0 Else 1 END As IsPageInGroup, Case WHEN wig_p.webinfoID Is NULL Then 0 Else 1 END As IsParentPageInGroup FROM ss_access_groups ag Left Outer Join ( Select distinct webinfoID, groupID From ss_access_webinfoGroup wig WHERE wig.webinfoID = " & PageID & " ) WIG on WIG.groupID = ag.groupID Left Outer Join (  Select distinct webinfoID, groupID From ss_access_webinfoGroup wig  WHERE wig.webinfoID = " & parentID & "  ) WIG_P on WIG_P.groupID = ag.GroupID"
        Else
            getGroups = "SELECT distinct ag.*, Case WHEN wig.webinfoID Is NULL Then 0 Else 1 END As IsPageInGroup, 1 As IsParentPageInGroup FROM ss_access_groups ag Left Outer Join ( Select distinct webinfoID, groupID From ss_access_webinfoGroup wig WHERE wig.webinfoID = " & PageID & " ) WIG on WIG.groupID = ag.groupID"
        End If
   Else
    	If parentID > 0 Then
            getGroups = "SELECT distinct ag.*, 0 As IsPageInGroup, Case WHEN wig_p.webinfoID Is NULL Then 0 Else 1 END As IsParentPageInGroup FROM ss_access_groups ag Left Outer Join (  Select distinct webinfoID, groupID From ss_access_webinfoGroup wig  WHERE wig.webinfoID = " & parentID & "  ) WIG_P on WIG_P.groupID = ag.GroupID"
        Else
            getGroups = "SELECT distinct ag.*, 0 As IsPageInGroup, 1 As IsParentPageInGroup FROM ss_access_groups ag"
        End If
    End If
    SET RS_Groups = CON_Groups.EXECUTE(getGroups)
    While Not RS_Groups.EOF

                        %>
                        <tr>
                            <td>
                            <% If RS_Groups("IsPageInGroup") = 1 Then %>
                            <input type="checkbox" name="access_groups"  value="<%= RS_Groups("GroupID") %>" checked="checked"/>
                            <% Else %>
                            <input type="checkbox" name="access_groups"  value="<%= RS_Groups("GroupID") %>"/>                            
                            <% End If %>
                                
                                <label>
                                    <%= RS_Groups("GroupName") %>
                                    <% If RS_Groups("IsParentPageInGroup") = 0 Then %>
                            <span style="color:Red;">[Group not Selected in Parent Page]</span>
                            <% End If %>
                                    <br />
                                    <span class="graySubText">
                                        <%= RS_Groups("GroupDescription") %></span></label>
                            
                            </td>
                        </tr>
                        <%
    RS_Groups.MoveNext
    wend

    RS_Groups.close
    CON_Groups.Close
                        %>
                    </tbody>
                </table>
            </td>
        </tr>
        <%END IF%>
        <tr>
            <td colspan="2">
                <%If Request.Querystring("action")="add" then%>
                <input type="hidden" name="pageID" value="-1">
                <%else%>
                <input type="hidden" name="pageID" value="<%=Request.Querystring("id")%>">
                <%end if%>
                <input type="hidden" name="DEFAULTPAGE" value="<%=defaultpage%>">
                <input type="hidden" name="PARENTID" value="<%=parentID%>">
                <input type="hidden" name="PAGELEVEL" value="<%=pageLevel%>" width="1">
                <input type="hidden" name="SECTIONID" value="<%=sectionID%>">
                <input type="hidden" name="Page_linkname" value="interior.asp">
                <!--<INPUT TYPE="text" name="myPublish" value="<%=myPublish%>">-->
                <textarea id="txtContent" name="txtContent" rows="4" cols="30">
	<%
	if sHTML <> "" then
	function encodeHTML(sHTML)
		
		sHTML=replace(sHTML,"&","&amp;")
		sHTML=replace(sHTML,"<","&lt;")
		sHTML=replace(sHTML,">","&gt;")
		encodeHTML=sHTML
	end function
	end if
	
	Response.Write encodeHTML(pageContent)
	%>	
	</textarea>

                <script>
		var oEdit1 = new InnovaEditor("oEdit1");
		oEdit1.initialRefresh=true;
			
		/***************************************************
			SETTING EDITOR DIMENSION (WIDTH x HEIGHT)
		***************************************************/

		oEdit1.width="90%";//You can also use %, for example: oEdit1.width="100%"
		oEdit1.height=350;
		
		/***************************************************
			RECONFIGURE TOOLBAR BUTTONS
		***************************************************/
		
		oEdit1.features=["Save", "CustomName2","|","FullScreen","Preview","Print","Search","SpellCheck","|",
			"Cut","Copy","Paste","PasteWord","PasteText","|",
			"Undo","Redo","|","ForeColor","BackColor","|","Hyperlink","Bookmark","Image","|",		
			"Flash","Media","|",
			"Table","Guidelines","Absolute","|",			
			//"Form",
			"Characters","Line","RemoveFormat",
			"XHTMLSource",
			"ClearAll",

			"BRK",
			"StyleAndFormatting","TextFormatting","ListFormatting",
			"BoxFormatting","ParagraphFormatting","CssText","Styles","|",
			"Paragraph","FontName","FontSize","|","CustomObject",
			"Bold","Italic","Underline","Strikethrough","Superscript","Subscript","|",
			"JustifyLeft","JustifyCenter","JustifyRight","JustifyFull","|",
			"Numbering","Bullets","|","Indent","Outdent","LTR","RTL","|",
		
						
			"CustomName1"];// => Custom Button Placement

		

		
		/***************************************************
			ADDING CUSTOM BUTTONS
		***************************************************/

		oEdit1.arrCustomButtons = [["CustomName1","javascript:load()","Edit Meta Data","btnCustom1.gif"],
									["CustomName2","top.basefrm.location 	= '<%=main%>'","No Save","btnNoSave.gif"]]

		/***************************************************
			SHOWING DISABLED BUTTONS
		***************************************************/

		oEdit1.btnPrint=true;
		oEdit1.btnPasteText=true;
		oEdit1.btnFlash=true;
		oEdit1.btnMedia=true;
		oEdit1.btnLTR=true;
		oEdit1.btnRTL=true;
		oEdit1.btnSpellCheck=true;
		oEdit1.btnStrikethrough=true;
		oEdit1.btnSuperscript=true;
		oEdit1.btnSubscript=true;
		oEdit1.btnClearAll=true;
		oEdit1.btnSave=true;
		oEdit1.btnStyles=true; //Show "Styles/Style Selection" button

		/***************************************************
			APPLYING STYLESHEET 
			(Using external css file)
		***************************************************/
		
		oEdit1.css="/editorConfig/css/editorStyle.css"; //Specify external css file here

		/***************************************************
			APPLYING STYLESHEET 
			(Using predefined style rules)
		***************************************************/
		/*
		oEdit1.arrStyle = [["BODY",false,"","font-family:Verdana,Arial,Helvetica;font-size:x-small;"],
					[".ScreenText",true,"Screen Text","font-family:Tahoma;"],
					[".ImportantWords",true,"Important Words","font-weight:bold;"],
					[".Highlight",true,"Highlight","font-family:Arial;color:red;"]];
		
		If you'd like to set the default writing to "Right to Left", you can use:
		
		oEdit1.arrStyle = [["BODY",false,"","direction:rtl;unicode-bidi:bidi-override;"]];
		*/


		/***************************************************
			ENABLE ASSET MANAGER ADD-ON
		***************************************************/

		oEdit1.cmdAssetManager = "modalDialogShow('/editor2/assetmanager/assetmanager.asp',640,465)"; //Command to open the Asset Manager add-on.
		//Use relative to root path (starts with "/")

		/***************************************************
			ADDING YOUR CUSTOM LINK LOOKUP
		***************************************************/

		oEdit1.cmdInternalLink = "modelessDialogShow('links.htm',365,270)"; //Command to open your custom link lookup page.

		/***************************************************
			ADDING YOUR CUSTOM CONTENT LOOKUP
		***************************************************/

		oEdit1.cmdCustomObject = "modelessDialogShow('objects.htm',365,270)"; //Command to open your custom content lookup page.

		/***************************************************
			USING CUSTOM TAG INSERTION FEATURE
		***************************************************/
		
		oEdit1.arrCustomTag=[["First Name","{%first_name%}"],
				["Last Name","{%last_name%}"],
				["Email","{%email%}"]];//Define custom tag selection

		/***************************************************
			SETTING COLOR PICKER's CUSTOM COLOR SELECTION
		***************************************************/

		oEdit1.customColors=["#ff4500","#ffa500","#808000","#4682b4","#1e90ff","#9400d3","#ff1493","#a9a9a9"];//predefined custom colors

		/***************************************************
			SETTING EDITING MODE
			
			Possible values: 
				- "HTMLBody" (default) 
				- "XHTMLBody" 
				- "HTML" 
				- "XHTML"
		***************************************************/
		oEdit1.cmdInternalLink = "modelessDialogShow('/editor2/complete/metaData.asp?pageid=<%=pageID%>',365,270)"; //Command to open your custom link lookup page.



		oEdit1.mode="XHTMLBody";
		
		
		oEdit1.REPLACE("txtContent");
                </script>

                &nbsp;<!--<input type="submit" value="Update Page" id="btnSubmit">-->
            </td>
        </tr>
        <tr>
            <td colspan="2" class="body">
                <%if beenUpdated = True then%>
                Page last modified:
                <%=lastModified%>
                by
                <%=lastMod%>
                <%end if%>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
