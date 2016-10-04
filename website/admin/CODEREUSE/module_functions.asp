<%

'This page contains subs and functions used in modules. 
'Including:
'	ModulePageHeader
'	ModuleErrorMessage
'	ExecuteSQL
'	INNOVA EDITOR FUNCTIONS
'		encodeHTML
'		CreateEditorInstance
'	PopulateDropDown
'	CreateTimeListOptions
'	CreateTimeDropDown
'	SetRecorsetValues
'	SetFormVars
'	SetQuerystringVars
'	SetCookieVars
'	PrepString
'	FileUpload functions that work with .dll on Tom's server



'Do Not Edit this page in production. It may be being called upon by multiple other pages.

	function ModulePageHeader(id,editName,listName)
		dim strHeader
		if id<>"" and isNumeric(id) and id<>"0" then
			strHeader="Edit " & editName
		elseif id=0 then
			strHeader="Add " & editName
		else
			strHeader=listName
		end if
		ModulePageHeader= "<h3 class=""module_header"">" & strHeader & "</h3>"
	end function

	function moduleErrorMessage(ErrorMessage)
		if ErrorMessage<>"" then	
			moduleErrorMessage="<p class=""error"">" & errorMessage & "</p>"
		end if
	end function


	'------------------------------------------------------------------	
	'EXECUTE SQL STATEMENT SUB
	'expects to be passed valid sql statement
	'DB connection info must be included on parent page
	
	sub ExecuteSQL(sql)
		SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
		con.open ConnectionString
		'NOTE: If you are drawing an error here, then go back to the process page and response.write your sql statement
		con.execute (sql)
		con.close	
		Set Con=nothing	
	end sub


	'------------------------------------------------------------------
	'CALENDAR DATE PICKER POPUP
	'NOTE - the admin/CODEREUSE/CalendarPopup.js file MUST be included
	'in the header of the page. 
	'For help with the calendar see source: http://www.javascripttoolbox.com/lib/calendar/
	'EXPECTS to receive valid string for textFieldName
	function CreateDatePickerInstance(textFieldName)
		'calendar image path
		dim calButtonPath
		calButtonPath="/Admin/CODEREUSE/calendar_bttn.gif"
		'create HTML
		dim HTML
			HTML=HTML & "<SCRIPT LANGUAGE=""JavaScript"">"
	HTML=HTML & "var cal" & textFieldName & " = new CalendarPopup();"
	HTML=HTML & "</SCRIPT>"
		HTML=HTML & "<A HREF=""#"""
		HTML=HTML & "onClick=""cal" & textFieldName & ".select(document.forms['form']." & textFieldName & ",'" & textFieldName & "anchor','MM/dd/yyyy'); return false;"""
   		HTML=HTML & "NAME=""" & textFieldName & "anchor"" ID=""" & textFieldName & "anchor""><img src=" & calButtonPath & " border=""0""></A>"
		'return value
		CreateDatePickerInstance=HTML
	end function



	'------------------------------------------------------------------	
	' INNOVA EDITOR FUNCTIONS
	'------------------------------------------------------------------
	'ENCODEHTML - used in creating the editor instance sub below. 
	function encodeHTML(sHTML)
		sHTML=replace(sHTML,"&","&amp;")
		sHTML=replace(sHTML,"<","&lt;")
		sHTML=replace(sHTML,">","&gt;")
		encodeHTML=sHTML
	end function
	
	'CREATEEDITORINSTANCE
	'Takes 5 parameters:
	'	instanceNum		Can be "1" or any integer. Really only matters if you have more than one
	'					editor on a page. If so, use separate instanceNum for each.
	'	fieldName		Name to give to the text area that this editor instance will be associated with
	'					This is the fieldname that will show up in the request.form collection
	'	width			Width of editor
	'	height			Height of editor
	'	fieldvalue		Content to start editor off with
	'You MUST have the Innova Editor javascript file referenced on the page
				
	function CreateEditorInstance(instanceNum,fieldname,width,height,fieldvalue)
	'DO NOT	USE THIS ONE WITH THE FORM GENERATOR - SEE BELOW
		dim HTML
		HTML="<textarea rows=""6"" name=""" & fieldname & """ id=""" & fieldname & """ cols=""50"">"
		HTML=HTML & encodeHTML(fieldvalue & " ")
		HTML=HTML & "</textarea>"
	
	
		HTML=HTML & "<script>" & Vbcrlf
		HTML=HTML & "//declare editor - must have innovaeditor" & Vbcrlf
		HTML=HTML & "var oEdit" & instanceNum & " = new InnovaEditor(""oEdit" & instanceNum & """);" & Vbcrlf

		HTML=HTML & "//hides tag selector line" & Vbcrlf
		HTML=HTML & "oEdit" & instanceNum & ".useTagSelector=false;" & Vbcrlf
	
		HTML=HTML & "//set height and width of editor" & Vbcrlf
		HTML=HTML & "oEdit" & instanceNum & ".width=""" & width & "px"";" & Vbcrlf
		HTML=HTML & "oEdit" & instanceNum & ".height=""" & height & "px"";" & Vbcrlf
		
		HTML=HTML & "//replaces textarea with editor" & Vbcrlf
		HTML=HTML & "oEdit" & instanceNum & ".REPLACE(""" & fieldname & """);" & Vbcrlf
		HTML=HTML & "</script>"
		CreateEditorInstance=HTML
	end function
	
	'THIS IS A VARIATION OF THE ABOVE MADE TO BE COMPATIBLE WITH THE FORM GENERATOR
	'IT LEAVES OFF THE TEXTAREA GENERATION PIECE
	function CreateEditorInstance2(instanceNum,fieldname,width,height)
	
		HTML=HTML & "<script>" & Vbcrlf
		HTML=HTML & "//declare editor - must have innovaeditor" & Vbcrlf
		HTML=HTML & "var oEdit" & instanceNum & " = new InnovaEditor(""oEdit" & instanceNum & """);" & Vbcrlf

		HTML=HTML & "//hides tag selector line" & Vbcrlf
		HTML=HTML & "oEdit" & instanceNum & ".useTagSelector=false;" & Vbcrlf
	
		HTML=HTML & "//set height and width of editor" & Vbcrlf
		HTML=HTML & "oEdit" & instanceNum & ".width=""" & width & "px"";" & Vbcrlf
		HTML=HTML & "oEdit" & instanceNum & ".height=""" & height & "px"";" & Vbcrlf
		
		HTML=HTML & "//replaces textarea with editor" & Vbcrlf
		HTML=HTML & "oEdit" & instanceNum & ".REPLACE(""" & fieldname & """);" & Vbcrlf
		HTML=HTML & "</script>"
		CreateEditorInstance2=HTML
	end function
	
	'------------------------------------------------------------------	
	'POPULATE SELECT BOX
	'Takes 5 parameters:
	'	formfieldname	name to give to the select list box field
	'	sql				sql to draw the options from db
	'	idfieldname		field that corresponds to the "value" of the select option
	'	textfieldname	field that corresponds to the "text" of the select option
	'		ie: <option value="idfieldname">textfieldname</option>
	'	matchingvalue	sets option as "selected" when the id value matches this.
	'					can be blank. then no options will be marked as selected.
	Sub PopulateDropDown(formfieldname,sql,idfieldname,textfieldname,matchingvalue)	
		set rs=getRS(sql)
			if not rs.eof then
				response.write "<select size=""1"" name=""" & formfieldname & """>"
				do while not rs.eof 
					response.write "<option value=""" & rs(idfieldname) & """"
						if rs(idfieldname)=matchingvalue then response.write "selected"
					response.write ">"
					response.write rs(textfieldname)
					response.write "</option>"
				rs.movenext
				loop
				rs.close
				set rs=nothing
				response.write "</select>"
			else
				response.write ="There are no records available."
			end if
	end sub	
	
	
	'------------------------------------------------------------------	
	'CREATE DROP DOWN OPTIONS
	'Like above, but only returns the option tags. For use with Form Creator Class.
	'Takes 5 parameters:
	'	sql				sql to draw the options from db
	'	idfieldname		field that corresponds to the "value" of the select option
	'	textfieldname	field that corresponds to the "text" of the select option
	'		ie: <option value="idfieldname">textfieldname</option>
	'	matchingvalue	sets option as "selected" when the id value matches this.
	'					can be blank. then no options will be marked as selected.
	Function CreateDropDownOptions(sql,idfieldname,textfieldname,matchingvalue)	
		dim HTML
			Set con= Server.CreateObject("ADODB.Connection")
			con.Open connectionString
			set rs=con.execute(sql)
			if not rs.eof then
				do while not rs.eof 
					HTML=HTML &  "<option value=""" & rs(idfieldname) & """"
						if rs(idfieldname)=matchingvalue then HTML=HTML &  "selected"
					HTML=HTML &  ">"
					HTML=HTML & rs(textfieldname)
					HTML=HTML &  "</option>"
				rs.movenext
				loop
			else
				HTML=HTML & "There are no records available."
			end if
			rs.close
			set rs=nothing
			con.close
			set con= nothing
			CreateDropDownOptions=HTML
	end function	


	'------------------------------------------------------------------	
	'CREATE TIME LIST OPTIONS 
	'THIS FUNCTION IS FOR THE CALENDAR OF EVENTS MODULE
	'EXPECTS TO BE PASSED A MATCHING VALUE. IF NO MATCHING VALUE, THEN PASS ""
	function CreateTimeListOptions(matchingValue)	
		'if matchingValue is empty, then set to noon
		if matchingValue="" then matchingValue ="12:00"
	
		'use military time for values so that data in database will be properly sortable 
		dim HTML,timeTail,milTime,regTime
		for i=0 to 23
			for j=1 to 4
				select case j
					case 1
						timeTail=":00"
					case 2
						timeTail=":15"
					case 3 
						timeTail=":30"
					case 4
						timeTail=":45"
				end select
				
				'set military time
				if i<10 then
					milTime="0" & i & timeTail
				else
					milTime=i & timeTail
				end if
				
				'set am pm
				if i<12 then
					timeTail=timeTail & " AM"
				else
					timeTail=timeTail & " PM"
				end if
				regTime=i & timeTail
				
				if i>12 then
					regTime=(i-12) & timeTail
				elseif i=0 then
					regTime="12" & timeTail
				else
					regTime=i & timetail
				end if
				
				'response.write milTime & matchingValue & "<BR>"
			HTML=HTML & "<option value=""" & milTime & """ "
			if milTime=matchingValue then HTML=HTML & "selected"
			HTML=HTML &">"  & regTime & "</option>"

			next
			

		next
		
		'return value
		CreateTimeListOptions=HTML		
	end function
	
	

	'------------------------------------------------------------------	
	'SET ALL RECORDSET VALUES
	'Takes a recordset
	'Equivalent of writing columnName=RS("columnName")
	'All columnName variables will be available on the page
	'Don't forget to close the recordset after running this function
	
	Function SetRecordsetValues(RS)
		if not RS.eof then
			'set page variables
			For Each Field in RS.Fields
				TheString = Field.Name & "=RS(""" & Field.Name & """)"
				EXECUTE(TheString)
			Next
		end if
	End function
	
	
	
	'------------------------------------------------------------------	
	'SET BLANK RECORDSET VALUES
	'Takes a recordset
	'Equivalent of above, except just initializes the variables 
	
	Function SetBlankRecordsetValues(RS)
		if not RS.eof then
			'set page variables
			For Each Field in RS.Fields
				TheString = Field.Name & "="""" "
				EXECUTE(TheString)
			Next
		end if
	End function


	
	
	'------------------------------------------------------------------	
	'SET FORM VARS		
	'This is the equivalent of writing varName=Request.Form("varName") for all 
	'Uses prepfield function below, because assumes you're going to
	'put these values into a seql string.
	
	Function SetFormVars()
			For Each Field in Request.Form
				TheString = Field & "=prepfield(Request.Form(""" & Field & """))"
				EXECUTE(TheString)
			Next
	END Function
	
	


	'------------------------------------------------------------------	
	'SET QUERYSRING VARS
	'This is the equivalent of writing varName=Request.Querystring("varName")
	Function SetQuerystringVars()
			For Each Field in Request.Querystring
				TheString= Field & "=Request.Querystring(""" & Field & """)"
				EXECUTE(TheString)
			Next
	End Function


	'------------------------------------------------------------------	
	'SET COOKIE VARS
	'Like above	
	Function SetCookieVars()	
			For Each Field in Request.Cookies
				TheString= Field & "=Request.Cookies(""" & Field & """)"
				EXECUTE(TheString)
			Next
	End Function


	'------------------------------------------------------------------	
	'PREPFIELD - Trim and replace apostrophes
	Function prepfield(field)
		prepfield=trim(replace(field & " ","'","''"))
	End function	
	
	
	
	
	
	'GET UNIQUE NAME WHEN SAVING FILE TO DISK - AVOID OVERWRITING 
		function UniqueFileName(filename, path) 
			Set objFSO=CreateObject("Scripting.FileSystemObject")
			tempfilename=""
			i=1
			'check if file is unique
			if not (objfso.FileExists(path & filename)) then
				bolUnique=1
				tempfilename=filename
			else
				'change file name until we get a unique one
				do while bolUnique=0
					tempfilename=cstr(i) & " - " & filename
					if not (objfso.FileExists(path & tempfilename)) then bolUnique=1
				i=i+1
				loop
			end if		
			set objfso=nothing			
			UniqueFileName=tempfilename
		end function

	'SAVE FILE TO SERVER - WORKS WITH UPLOAD .dll ON TOM'S SERVER
		function SaveToFile(objFile,filename,path)
			if objFile.FileName<>"" then
				set stream = Server.CreateObject("ADODB.Stream")
				stream.Open
				stream.Type = 1
				stream.Write(objFile.Value)
				stream.SaveToFile fileuploadpath & filename,2
				stream.Close
				set stream=nothing
			end if
   		end function

	
	%>