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
<link rel="stylesheet" type="text/css" href="/editorConfig/css/editorStyle.css" />
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
	


 <%

   		set Con = Server.CreateObject("adodb.connection")
		con.open ConnectionString
				
		  	arrMST = Split(Request.Form("MST"), ", ")
		 	For Each MST In arrMST
		 	 	
		 	 	getTagName = "Select tagName from TAGS where tagID = "&MST&""
		 	 	Set RS2 = con.execute(GetTagName)
		 	 	
		 	 	
		 	 	if SearchTag = "" then
		 	 		SearchTag = " recordType='webpage' and tagID = "&MST&""
		 	 		tagName = RS2("tagName")
		 	 		
		 	 	Else
		 	 		SearchTag = SearchTag & " or recordType = 'webpage' and tagID = "&MST&""
		 	 		tagName = tagName & ", " & RS2("tagName")
		 	 	End if
	 	 	
	 		Next
		  
		  	arrPNT = Split(Request.Form("PNT"), ", ")
		 	For Each PNT In arrPNT
		 	 	
		 	 	getTagName = "Select tagName from TAGS where tagID = "&PNT&""
		 	 	Set RS2 = con.execute(GetTagName)
		 	 	
		 	 	
		 	 	if SearchTag = "" then
		 	 		SearchTag = " recordType='webpage' and tagID = "&PNT&""
		 	 		tagName = RS2("tagName")
		 	 		
		 	 	Else
		 	 		SearchTag = SearchTag & " or recordType = 'webpage' and tagID = "&PNT&""
		 	 		tagName = tagName & ", " & RS2("tagName")
		 	 	End if
	 	 	
	 		Next
	 		
	 	 	
	 	 	
		 	    strSQL = "Select distinct recordID from tagXref where "&searchTag&""
			    SET RS4 = Con.Execute(strSQL)
			    
			    
			    If Not RS4.EOF then
%>

	
				<table width="100%" class="contentBody">                                    
					<tr> 
            		<td colspan="2"  width="100%" height="25"><p><b>Web Page Search Results for:  <%=tagName%></b></p></td></tr>
                <%
				count = 0
				
				do While not rs4.eof
				getPageInfo = "Select * from webinfo where ID = "&rs4("recordID")&""
				Set RS = con.execute(getPageInfo)
				
				If Not RS.EOF then
				
		 	    strSQL = "Select distinct tagID from tagXref where recordID = "&RS4("recordID")&""
 				set rs6 = con.execute(strSQL)
		 	    
		 	    If Not RS6.EOF then
		 	    
		 	    	Do while not RS6.eof
		 	    	
		 	    	
		 	    		If pageTags = "" then
		 	    			pageTags = " tagID = " & RS6("tagID")
		 	    		Else
		 	    			pageTags = pageTags & " or tagID = " &RS6("tagID")
		 	    		End if
		 	    	
		 	    	RS6.MoveNext
		 	    	Loop
		 	    	
		 	    End IF
		 	    
		 	    tagNames = "Select tagName from TAGS where "&pageTags&""
		 	    set RS7 = con.execute(tagNames)
				
					do while not rs7.eof
					
						if pname = "" then
							pname = RS7("tagName")
						Else
							pname = pname & ", " & RS7("tagName")
						End if
						
						
					
					rs7.movenext
					loop
				

				%>
             <tr><td class="contentBody">&nbsp;&nbsp;&nbsp;</td><td class="contentBody" width="100%"><a href="/web/page/<%=RS("id")%>/sectionid/<%=rs("sectionid")%>/interior.asp">
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

<tr><td><td><b>Listed in the following categories:</b> <%=pname%></td></td></tr>
             <tr><td class="contentBody">&nbsp;&nbsp;&nbsp;</td><td class="contentBody" width="100%"><hr size="1"></td></tr>    
				<%pname = ""%>

			<% 	
			
			 else%>
			 
			 <tr><td></td></tr>
			 
			<% end if 
			
			RS4.MoveNext
					count=count + 1
				loop
				RS.close %>
			
			
     </table>  
     <%else%>

     
     <%end if%>   



    <!--BEGIN SEARCH BOX-->
    <%if request.querystring("FileNumPosition")<10 then
    X = 0
    
    %>
    
    
 
 <%end if%>

    		
</body>

</html>
<%con.close
set con = nothing%>