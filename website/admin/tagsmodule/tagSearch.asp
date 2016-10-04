<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#INCLUDE VIRTUAL = /admin/db_connection.asp-->
<%
	SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
	CON.OPEN ConnectionString

%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Untitled 1</title>
<link rel="stylesheet" type="text/css" href="/css/websitestyle_richtemplate.css" />
</head>

<body>

<%
set Con = Server.CreateObject("adodb.connection")
con.open ConnectionString

    if request.querystring("subid")<>"" then
    Session("parentid")=request.querystring("subid")
    end if
    
 Session("sectionid")=Request.Querystring("sectionid")  
 



Function stripHTML(strtext)
 dim arysplit,i,j, strOutput
 arysplit=split(strtext,"<")
 
  if len(arysplit(0))>0 then j=1 else j=0

  for i=j to ubound(arysplit)
     if instr(arysplit(i),">") then
       arysplit(i)=mid(arysplit(i),instr(arysplit(i),">")+1)
     else
       arysplit(i)="<" & arysplit(i)
     end if
  next

  strOutput = join(arysplit, "")
  strOutput = mid(strOutput, 2-j)
  strOutput = replace(strOutput,">",">")
  strOutput = replace(strOutput,"<","<")

  stripHTML = strOutput
End Function


 
%>





	<table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF">
 
 

  <tr>
    <td bgcolor="#FFFFFF" valign="bottom" class="contentBody"><p>
	


 <%if Request("q1")<>"" then
 	q1 = Request.Form("q1")
   	q1 = replace(q1, "'", "''")
   	
   	
   	
   	

   		set Con = Server.CreateObject("adodb.connection")
		con.open ConnectionString
				


			strSQL = "Select * FROM WEBINFO WHERE "
			tmpSQL = "(message LIKE "	
				
			DefaultBoolean = "AND"
	
				Pos = 1
					While Pos > 0
      					Pos = InStr(1, q1," ")
      					If Pos = 0 Then
            				'We have hit the end
            			tmpSQL = tmpSQL & "'%" & q1 & "%'"
      					
      					Else
            				tmpSQL = tmpSQL & "'%" & Mid(q1,1,Pos) & "%' " & DefaultBoolean & " message LIKE "
            				q1= Mid(q1,Pos+1,len(q1))
      
      					End If

					Wend
					

					
 	q1 = Request.Form("q1")
   	q1 = replace(q1, "'", "''")
   	
			tmpSQL2 = "(name LIKE "	
			DefaultBoolean = "AND"
	
				Pos = 1
					While Pos > 0
      					Pos = InStr(1, q1," ")
      					If Pos = 0 Then
            				'We have hit the end
            			tmpSQL2 = tmpSQL2 & "'%" & q1 & "%'"
      					
      					Else
            				tmpSQL2 = tmpSQL2 & "'%" & Mid(q1,1,Pos) & "%' " & DefaultBoolean & " name LIKE "
            				q1= Mid(q1,Pos+1,len(q1))
      
      					End If

					Wend
					
					tmpSQL = tmpSQL & " AND SEARCHABLE = 1)"
					tmpSQL2 = tmpSQL2 & " AND SEARCHABLE = 1)"
	
				strSQL = strSQL & tmpSQL & " or " &tmpSQL2
   
				strSQL = strSQL & "  ORDER BY ID"    

				'response.write strSQL
    
    
 
				
				SET RS = Con.Execute(strSQL )
    	      

				If not RS.EOF then	
				mybgcolor="#FFFFFF"
%>

	
				<table width="100%" class="contentBody">                                    
					<tr> 
            		<td colspan="2"  width="100%" height="25"><p><b>Web Page Search Results for:  <%=Request("q1")%></b></p></td></tr>
                <%
				count = 0
				
				do While not rs.eof

				%>
             <tr><td class="contentBody">&nbsp;&nbsp;&nbsp;</td><td class="contentBody" width="100%"  bgcolor="<%=mybgcolor%>" height="20px"><a href="/web/page/<%=RS("id")%>/sectionid/<%=rs("sectionid")%>/interior.asp">
				<%=RS("NAME")%></a> - 
				
				



<%























String1=rs("message")
String2=q1
string3=">"
string4="<"
myLength=len(q1)
Location= Instr(String1,String2)
'response.write Location



StartLocation = cint(Location)-1
'response.write StartLocation
if StartLocation < 100 then
StartLocation2 = 1
Else
StartLocation2=StartLocation - 100
end if


mySummary= Mid(String1, StartLocation2, 200) 



mySearchResult = mySummary
HTMLLocation= Instr(mySearchResult,string3) 
HTMLLocation2= Instr(mySearchResult,string4) 
HTMLLocation= Instr(stripHTML(mySearchResult),string3)




if HTMLLocation>0 and HTMLLocation2<200  then
else
end if


myString = stripHTML(String1)

response.write left(myString,200)


%>



				
				
				
				</td></tr>            

			<% 	
						if mybgcolor="#FFFFFF" then
					mybgcolor="#efefef"
				else
					mybgcolor="#FFFFFF"
				end if
			

			RS.MoveNext
					count=count + 1
				loop
				RS.close %>
			
			
     </table>  
     <%else%>

     
     <%end if%>   
<%end if%>  



    <!--BEGIN SEARCH BOX-->
    <%if request.querystring("FileNumPosition")<10 then
    X = 0
    
    %>
    
    
 
<!-- pull results from search flagged areas --> 
 
<%
 
    arrSearchArea = Split(Request.Form("searchArea"), ", ")

 	 	For Each searchArea In arrSearchArea 
 	 	
 	 	
 	 	If searchArea = "bio-sketch" THEN
 	 	biosketch = true
 	 	end if
 	 	
 	 	
 	 	Next
		    

If biosketch = true then
	
 	q1 = Request.Form("q1")
   	q1 = replace(q1, "'", "''")
   	
			strSQL1 = "Select * FROM MICROSITE_DATA WHERE "
			tmpSQL3 = "(biosketch LIKE "	
			DefaultBoolean = "AND"
	
				Pos = 1
					While Pos > 0
      					Pos = InStr(1, q1," ")
      					If Pos = 0 Then
            				'We have hit the end
            			tmpSQL3 = tmpSQL3 & "'%" & q1 & "%'"
      					
      					Else
            				tmpSQL3 = tmpSQL3 & "'%" & Mid(q1,1,Pos) & "%' " & DefaultBoolean & " biosketch LIKE "
            				q1= Mid(q1,Pos+1,len(q1))
      
      					End If

					Wend
					

					tmpSQL3 = tmpSQL3 & ")"
	
				strSQL1 = strSQL1 & tmpSQL3
   
				strSQL1 = strSQL1 & "  ORDER BY id"    

				'response.write strSQL1
				SET RS = Con.Execute(strSQL1)


	if not rs.eof then
	
		
	
	%>


	<table width="100%" cellspacing="1"  id="table4" height="100%" cellpadding="3"><tr>
		<td class="contentBody" bgcolor="#FFFFFF" width="100%" height="25">
		<p align="left"><b>Bio-Sketch Search Results for: <%=Request("q1")%></b></td></tr></table>

		<table width="100%" cellspacing="1" id="table5">		
		
		<%

X = 1
mybgcolor="#FFFFFF"
	Do While Not rs.EOF
	
	
	getArchive = "Select * from Microsite_partners where partnerID = "&RS("partnerID")&" and BOLARCHIVE <> 1"

	set ARCHRS = con.execute(getArchive)
	
	
	
	IF NOT ARCHRS.EOF THEN
	folderName = ARCHRS("folderName")
		%>
		
		<tr><td  bgcolor="<%=mybgcolor%>" class="small" width="15">&nbsp;</td>
		
		<%



String1=rs("biosketch")
String2=q1
string3=">"
string4="<"
myLength=len(q1)
Location= Instr(String1,String2)
StartLocation = cint(Location)-1
	if StartLocation < 100 then
		StartLocation2 = 1
	Else
		StartLocation2=StartLocation - 100
	end if
mySummary= Mid(String1, StartLocation2, 200) 
mySearchResult = mySummary
HTMLLocation= Instr(mySearchResult,string3) 
HTMLLocation2= Instr(mySearchResult,string4) 
HTMLLocation= Instr(stripHTML(mySearchResult),string3)
if HTMLLocation>0 and HTMLLocation2<200  then
else
end if


myString = stripHTML(String1)

%>

		
		
			<td bgcolor="<%=mybgcolor%>" height ="20px" class="small" align="left"><a href="/<%=folderName%>/?ID/<%=RS("partnerid")%>"><%=RS("lastname")%>, <%=RS("firstName")%></a> - <%response.write left(myString,200)%></td></tr>
			
	<%END IF%>		


		
				<%
							if mybgcolor="#FFFFFF" then
					mybgcolor="#efefef"
				else
					mybgcolor="#FFFFFF"
				end if
			

		rs.MoveNext
	Loop%>

</table>
<%
end if

rs.close
set rs=nothing
	end if%>


<!-- Pull results from pr table -->
<%


    arrSearchArea = Split(Request.Form("searchArea"), ", ")

 	 	For Each searchArea In arrSearchArea 
 	 	
 	 	
 	 	If searchArea = "press" THEN
 	 	press = true
 	 	end if
 	 	
 	 	
 	 	Next

If press = true then

%>


 <%if Request("q1")<>"" then
 	q1 = Request.Form("q1")
   	q1 = replace(q1, "'", "''")
   	
   	
   	
   	

   		set Con = Server.CreateObject("adodb.connection")
		con.open ConnectionString
				


			strSQL = "Select * FROM PR WHERE "
			tmpSQL = "(summ_x LIKE "	
				
			DefaultBoolean = "AND"
	
				Pos = 1
					While Pos > 0
      					Pos = InStr(1, q1," ")
      					If Pos = 0 Then
            				'We have hit the end
            			tmpSQL = tmpSQL & "'%" & q1 & "%'"
      					
      					Else
            				tmpSQL = tmpSQL & "'%" & Mid(q1,1,Pos) & "%' " & DefaultBoolean & " message LIKE "
            				q1= Mid(q1,Pos+1,len(q1))
      
      					End If

					Wend
					

					
 	q1 = Request.Form("q1")
   	q1 = replace(q1, "'", "''")
   	
			tmpSQL2 = "(title_x LIKE "	
			DefaultBoolean = "AND"
	
				Pos = 1
					While Pos > 0
      					Pos = InStr(1, q1," ")
      					If Pos = 0 Then
            				'We have hit the end
            			tmpSQL2 = tmpSQL2 & "'%" & q1 & "%'"
      					
      					Else
            				tmpSQL2 = tmpSQL2 & "'%" & Mid(q1,1,Pos) & "%' " & DefaultBoolean & " title_x LIKE "
            				q1= Mid(q1,Pos+1,len(q1))
      
      					End If

					Wend

 	q1 = Request.Form("q1")
   	q1 = replace(q1, "'", "''")
   	
			tmpSQL3 = "(desc_x LIKE "	
			DefaultBoolean = "AND"
	
				Pos = 1
					While Pos > 0
      					Pos = InStr(1, q1," ")
      					If Pos = 0 Then
            				'We have hit the end
            			tmpSQL3 = tmpSQL3 & "'%" & q1 & "%'"
      					
      					Else
            				tmpSQL3 = tmpSQL3 & "'%" & Mid(q1,1,Pos) & "%' " & DefaultBoolean & " desc_x LIKE "
            				q1= Mid(q1,Pos+1,len(q1))
      
      					End If

					Wend


					
					tmpSQL = tmpSQL & ")"
					tmpSQL2 = tmpSQL2 & ")"
					tmpSQL3 = tmpSQL3 & ")"
	
				strSQL = strSQL & tmpSQL & " or " &tmpSQL2 & " or " &tmpSQL3
   
				strSQL = strSQL & " and mode_x = 'Live' ORDER BY ID_x"    

				'response.write strSQL
    
    
 
				
				SET RS = Con.Execute(strSQL )
    	      

				If not RS.EOF then	%>

	
				<table width="100%" class="contentBody" cellpadding="3">                                    
					<tr> 
            		<td colspan="2"  width="100%" height="25"><p><b>Press 
					Release Search Results for:  <%=Request("q1")%></b></p></td></tr>
                <%
				count = 0
				mybgcolor="#FFFFFF"

				do While not rs.eof
				
				


String1=rs("summ_x")
String2=q1
string3=">"
string4="<"
myLength=len(q1)
Location= Instr(String1,String2)
StartLocation = cint(Location)-1
if StartLocation < 100 then
	StartLocation2 = 1
Else
	StartLocation2=StartLocation - 100
end if
mySummary= Mid(String1, StartLocation2, 200) 
mySearchResult = mySummary
HTMLLocation= Instr(mySearchResult,string3) 
HTMLLocation2= Instr(mySearchResult,string4) 
HTMLLocation= Instr(stripHTML(mySearchResult),string3)
if HTMLLocation>0 and HTMLLocation2<200  then
else
end if
myString = stripHTML(String1)
myString = left(myString,200)

%>

				
			<tr><td></td><td bgcolor="<%=mybgcolor%>" height ="20px" class="small" align="left"><a href="/web/page/181/sectionid/181/id/<%=RS("id_x")%>/module/events/interior.asp#Position<%=RS("id_x")%>"><%=RS("title_x")%></a> - <%=mystring%></td></tr>

			<% 	
				if mybgcolor="#FFFFFF" then
					mybgcolor="#efefef"
				else
					mybgcolor="#FFFFFF"
				end if
			
				RS.MoveNext
				count=count + 1
				loop
				RS.close %>
			
			
     </table>  
     <%else%>

     
     <%end if%>   
     
     
  <%end if%>   
     
<%end if%>  











	</td></tr>
</table>

<%END IF%>





 



</body>

</html>
<%con.close
set con = nothing%>