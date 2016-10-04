<%
'This class generates the DataGrid Class object. It is used in conjunction with caDataGridColumn.Class.asp
'NOTE: Do not edit this page in production. It is used by multiple other pages.


'**If you are generating a syntax error at the "Class caDataGrid" line, be certain you did not
'  include your reference to this page inside an IF or CASE statement

Class caDataGrid
   'private variables
   private m_AutoColumns
   private m_arrColumns
   Private m_ConnStr
   Private m_SqlStr
   Private m_ColumnsCount
   Private m_Conn 
   Private m_RS
   Private m_HTMLOutput
   Private m_TableCssClass
   Private m_DefaultSortField 
   Private m_DebugShowSql

   'this runs when you create a reference to the caDataGrid class
   Private Sub Class_Initialize()
	   redim m_arrColumns(7,0)	'five properties and 0 rows at start
       Set m_Conn = server.createobject("adodb.connection")
       Set m_RS= server.createobject("adodb.recordset")
       m_ColumnsCount= 0
       m_AutoColumns = True
   End Sub
   
   
   
   'Properties - all writable
   Public Property Let ConnectionString(strConn)
       m_ConnStr = strConn
   End Property

   Public Property Let AutoColumns(bAutoCols)
       If bAutoCols = True or bAutoCols = False then
           m_AutoColumns = bAutoCols
       End IF
   End Property

   Public Property Let SqlString(strSql)
       m_SqlStr = strSql
   End Property
   
   Public Property Let TableCssClass(strString)
   		m_TableCssClass=strString
   	End Property

   Public Property Let DefaultSortField (strString)
   		m_DefaultSortField =strString
   	End Property

	Public Property Let DebugShowSql(strString)
		m_DebugShowSql = strString
	End Property


   'Methods for our class
   
   
     Public Function AddColumn(objColumn)
       'Adds a DataGridColumn object to the m_Columns object
       'Expects to be passed a valid objColumn instance
		
		'Had trouble using dictionary object here. Switched to arrays.
		'Add new row to array for each column, and write in properties
		
		'redim the array to have a new row
		ReDim Preserve m_arrColumns(7,m_ColumnsCount)
		'attach properties
		m_arrColumns(0,m_ColumnsCount)=objColumn.Header
		m_arrColumns(1,m_ColumnsCount)=objColumn.HeaderCssClass
		m_arrColumns(2,m_ColumnsCount)=objColumn.DataCellFormat
		m_arrColumns(3,m_ColumnsCount)=objColumn.DataCellCssClass
		m_arrColumns(4,m_ColumnsCount)=objColumn.Sortable
		m_arrColumns(5,m_ColumnsCount)=objColumn.SortField
		m_arrColumns(6,m_ColumnsCount)=objColumn.DataCellCssAlternatingClass

		'increment column count
		m_ColumnsCount=m_ColumnsCount + 1
		
		'debug line
		'response.write m_arrColumns(1,m_ColumnsCount-1)

     End Function



   Public Sub DisplayGrid
   
   		'This function opens a recordset based on inputed sql. It builds the "order by " clause based
   		'on data passed in or data in the querystring, in the case that the user has clicked on a sortable column.
   		'It checks the autoColumns value and sends the Recordset either the DisplayAutoColumns sub or 
   		'the DisplayCustomColumns sub.

   		'open connection string and retrieve recordset
   		m_Conn.Open m_ConnStr
   		
   		'DETERMINE SORTING
   		'if there is a trailing semi colon on our sql string, remove it
   		if Right(m_SqlStr,1)=";" then
   			m_SqlStr=left(m_SqlStr,len(m_SqlStr)-1)
   		end if
   		
   		'if there is a "gridOrder" variable in the querystring, use that to override the DefaultSortString
   		if Request.querystring("gridOrder")<>"" then
   			m_SqlStr=m_SqlStr & " ORDER BY " & request.querystring("gridOrder") 
   		elseif m_DefaultSortField <>"" then
   			m_SqlStr=m_SqlStr & " ORDER BY " & m_DefaultSortField 
   		end if
   		
   		'if there is a gridDir var in the querystring, then use that
   		if Request.querystring("gridDir")<>"" then
   			m_SqlStr=m_SqlStr & " " & Request.querystring("gridDir")
   		end if
   		
   		'tack on trailing semi colon
   		m_SqlStr=m_SqlStr & ";"
   		
   		'***if you have an error here - be sure you supplied a valid sql string and connection string
   		'write line for debugging
   		if m_DebugShowSql=true then
	   		response.write m_SqlStr
	   	end if
	   	
		'set the recordset	
   		set m_RS=m_Conn.Execute(m_SqlStr)
   		
   		'send the recordset to the best sub to display it based on the autoColumns value
		if m_AutoColumns=true then
			'send recordset to DisplayAutoColumns sub
			DisplayAutoColumns(m_RS)
		else
			'sent recordset to DisplayCustomColumns sub
			DisplayCustomColumns(m_RS)
		end if
		

   
   End Sub
   
   
   Private Sub DisplayAutoColumns(RS)
   
   		'This sub loops through the recordset and dumps the whole table using
   		'the field names for column headers.
   		'The only formmating applied is the TableCssClass if it was specified.
   		'Else some default formmatting is applied
   
		Response.Write "<TABLE "
		'table formmatting
		if m_TableCssClass<>"" then 
			Response.Write "Class=""" & m_TableCssClass & """"
		else
			Response.write "border=""1"" cellpadding=""5"""
		end if
		Response.write ">" & vbNewLIne 
		Response.Write "<TR>" & vbNewLine
		' RS.Fields is the collection of fields associated with the recordset...
		' The count given is correct, but since the fields are numbered
		' starting at zero, we have to subtract one to get the maximal field number:
		For fnum = 0 To RS.Fields.Count-1
    		' Naturally, each element in the Fields collection is 
    		' an ADODB.Field object. And the Field object has various
    		' properties, including...ta da!...its Name:
    		Response.Write "<TD "
    		'cell formmatting
    		if m_HeaderCellCssClass<>"" then Response.write "Class=""" & m_HeaderCellCssClass & """"
    		Response.write ">" & RS.Fields(fnum).Name & "</TD>" & vbNewLine
		Next
		Response.Write "</TR>" & vbNewLine

		' Then, if you want to also DUMP the entire table:
		' You simply do all records until you reach the end (EOF):
		Do Until RS.EOF
	    Response.Write "<TR>" & vbNewLine
	    For fnum = 0 To RS.Fields.Count-1
 	       ' again, one of the ADODB.Field properties is Value, so...
  	      Response.Write "<TD "
  	      if m_DataCellCssClass<>"" then response.write "Class=""" & m_DataCellCssClass & """"
  	      Response.write ">" & RS.Fields(fnum).Value & "</TD>" & vbNewLine
  	    Next
  		Response.Write "</TR>" & vbNewLine
    	RS.MoveNext
		Loop
		Response.Write "</TABLE>" & vbNewLine
   
   End Sub
   
   Private Sub DisplayCustomColumns(RS)
   
   		'This sub loops through the table and uses the info specified for the column field to set each column.
   		'Only columns that were set will show. So it is possible for the recordset to have unused fields
   		'if a column was not specified for them.
		
		'The cell display here is the most complicated part because it loops through the DataCellFormatField
		'to pull out the field names and provide the custom formmating specified.
   
		'GRID HEADER

   		Response.write "<table "
		if m_TableCssClass<>"" then 
			Response.Write "Class=""" & m_TableCssClass & """"
		end if
		Response.write ">" & vbNewLIne 
		Response.Write "<TR>" & vbNewLine
		'loop through array for header names
		for i=0 to m_ColumnsCount-1
			response.write "<td "
    		dim thisHeaderCellCss
    		thisHeaderCellCss=m_arrColumns(1,i)
    		if thisHeaderCellCss<>"" then Response.write "Class=""" & thisHeaderCellCss & """"
			response.write ">"
			
			response.write GetHeaderCellHTML (m_arrColumns(0,i),m_arrColumns(4,i), m_arrColumns(5,i))

			response.write "</td>"
		next
   		response.write "</tr>"
		
		
		'GRID CONTENT
   		'set recordcount value
   		dim count
   		count=0
		
   		Do Until RS.EOF
   		
   		'increment count
   		count=count+1
   		
	    Response.Write "<TR>" & vbNewLine

			'loop through columns for this row
			for i=0 to m_ColumnsCount-1
				HTML=""
				'start the cell
				response.write "<TD "
				
				'sort out the css class for the cell
				dim thisDataCellCss
				
				'if this is an even row use the dataCellCssAlternating, if odd use DataCellCss
				if count mod 2 <1 then
					thisDataCellCss=m_arrColumns(3,i) 'the datacellcss data set in the array when the column was added
				else
					thisDataCellCss=m_arrColumns(6,i)	'alternating css
				end if
  	      		if thisDataCellCss<>"" then response.write "Class=""" & thisDataCellCss & """"
  	      		response.write  ">" 
  	      		
  	      		'get the custom formatting for this cell
  	      		dim thisCellDataFormat
  	      		thisCellDataFormat = m_arrColumns(2,i)

  	      		'replace ## with proper field values
  	      		'make sure there are at least two # signs
  	      		dim poundCount
  	      		poundCount=GetSubstringCount(thisCellDataFormat, "#", False)

				do while poundCount>1
  	      		
	  	      		'pull out the first field name
	  	      		dim strBeginning, strFieldName,firstPound, secondPound
	  	      		firstPound=instr(thisCellDataFormat,"#")
	  	      		'remove everything before the first pound sign
	  	      		strBeginning=left(thisCellDataFormat,firstPound-1)
	  	      		'cut off beginnning from main string
	  	      		thisCellDataFormat=right(thisCellDataFormat,len(thisCellDataFormat)-firstPound)
	  	      		'find second pound sign
	  	      		secondPound=instr(thisCellDataFormat,"#")
	  	      		'get Field name
	  	      		strFieldName=left(thisCellDataFormat,secondPound-1)
	  	      		'cut off field name
	  	      		thisCellDataFormat=right(thisCellDataFormat,len(thisCellDataFormat)-secondPound)
  	      		
	  	      		'write string begining and cycle through the recordset fields for this field
	  	      		'response.write strBeginning
	  	      		HTML=HTML & strBeginning	
					For fnum = 0 To RS.Fields.Count-1
  	      			
  	      				if ucase(RS.Fields(fNum).Name)=ucase(strFieldName) then
  	      					dim tempField
  	      					tempField=RS.Fields(fnum).Value 
  	      					'if this is a datetype (135) then remove the seconds
  	      					if RS.Fields(fnum).Type=135 then
  	      						tempField=GetRidOfSeconds(tempField)
  	      					end if
  	      				
  	      					HTML=HTML & tempField 
  	      				end if
  	      			
  	      			next
  	      		
					'decrease poundCount by 2
					poundCount=poundCount-2

					'if there are no more sets of pound signs, then write out the rest of our string
					if poundCount<2 then
						HTML=HTML & thisCellDataFormat
					end if
	
  	      		loop
  	      		
  	      			'HTML now equals the dataCellFormat string with the #names# replaced by their database values
  	      			'if HTML includes a "function:" at the beginning then, execute the function.
					'else write the string to the page
					if instr(1,HTML,"function:") then
						dim functionCall
						functionCall=right(HTML,len(HTML)-9)
						'NOTE: IF YOU ARE DRAWING AN ERROR HERE, BE SURE YOU INCLUDED THE PROPER
						'ASP FOR YOUR FUNCTION SOMEWHERE ON THE INDEX.ASP PAGE
						'response.write functionCall
						EXECUTE(functionCall)
						
					else
						response.write HTML
					end if
  	      		
  	      		
  	      		'close up the cell
  	      		response.write "</TD>" & vbNewLine
  	    	next
  	    
  		Response.Write "</TR>" & vbNewLine
    	RS.MoveNext
		Loop
   		response.write "</table>"
   		
   		
   End Sub
   
   
   'This is a helper function.
   'It returns the number of substrings in a string
   'and is used to return the  number of pound signs in the DataCellFormat variable
   Function GetSubstringCount(strToSearch, strToLookFor, bolCaseSensative)
  		If bolCaseSensative then
    		GetSubstringCount = UBound(split(strToSearch, strToLookFor))
  		Else
    		GetSubstringCount = UBound(split(UCase(strToSearch), UCase(strToLookFor)))
  		End If
	End Function
   

	'This is a helper function. 
	'It returns a short string of HTML for the header cell when using the AutoColumns=false option
	'It is passed the Sortable and DefaultSortField data and constructs the link
	'for the header based on whether the table is displaying for the first time, or whether
	'the user is resorting a column. Columns are sorted ascending by default and descending if 
	'clicked a second time.
	Function GetHeaderCellHTML(strHeader,sortable, strSortField)
	
		dim HTML
		'run this only if we have sorting and a field to sort by
		if sortable=true and strSortField<>"" then
			'get current url 
			dim currentURL
			currentURL=Request.ServerVariables("URL")

			dim SortDirection
			'set defult sort direction
			SortDirection="asc"
			'check to see if this is second time sorting this column. If so
			'sort if in the opposite direction
			if Request.Querystring("gridDir")<>"" then
				if Request.Querystring("gridDir")="asc" then
					SortDirection="desc"
				else
					SortDirection="asc"
				end if
			end if
				

			'reconstruct the querystring minus the gridOrder and gridSort parameters ones we are using
			currentURL=currentURL & "?"
			For Each Field in Request.Querystring
				if Field<>"gridOrder" and Field<>"gridDir" then
					currentURL= currentURL & Field & "=" & Request.Querystring(Field) & "&"
				end if
			Next


			'add our parameters. Add a question mark if the page had no parameters previously
			if instr(currentURL,"?")<1 then
				currentURL=currentURL & "?gridOrder=" & strSortField & "&gridDir=" & SortDirection
			else
				currentURL=currentURL & "gridOrder=" & strSortField & "&gridDir=" & SortDirection
			end if
			
			'contstruct the HTML for the link
			HTML="<a href=""" & currentURL & """>" & strHeader & "</a>"
		
		else
			'if the field was not sortable or the sort field was not specified, set the cell
			'equal to just the text value
			HTML=strHeader
		end if
		
		'return value
		GetHeaderCellHTML=HTML
	End Function
  
   Function GetRidOfSeconds(thisDateTime)
   		
   		'convert date to string
   		dim myString
   		myString=thisDateTime & " "

		'only remove seconds if string is longer than 10 characters, else there is no time attached to it
		if len(myString)>10 then
	   		GetRidOfSeconds=left(myString,instrRev(myString,":")-1) & " " & right(myString,3)
   		else
   			GetRidOfSeconds=thisDateTime
   		end if	
   		
   end Function
   
   'this runs when we destroy our reference to caDataGrid
   Private Sub Class_Terminate()
       m_HTMLOutput = ""
       m_RS.Close
       Set m_RS= nothing
       m_Conn.close
       Set m_Conn= nothing
       Erase m_arrColumns  
   End Sub

End Class
%>