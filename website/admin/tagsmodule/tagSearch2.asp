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
<style type="text/css">
.style8 {
	color: #DDDDDD;
}
</style>
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
 	 	
 	 	getTagName = "Select * from TAGS where tagID = "&MST&""
 	 	set RS3 = con.execute(getTagName)
 	 	tagName = RS3("tagName")
 	 	
 	 	
	 	    strSQL = "Select * from tagXref where recordType = 'webpage' and tagID = "&RS3("tagid")&""
	 	   ' response.write strSQL 
		    SET RS6 = Con.Execute(strSQL)
		    
		    
		    If Not RS6.EOF then
		    mybgcolor="#eeeeee"
%>
		    
	<table width="100%" class="contentBody">                                    
		<tr> 
       		<td colspan="2"  width="100%" height="25"><p><b>Web Page Search 
			Results for:  <%=tagName%></b></p></td></tr>
                <%count = 0%>
				<%do While not rs6.eof
					getPageInfo = "Select * from webinfo where ID = "&RS6("recordID")&""
					set RS4 = con.execute(getPageInfo)
					'response.write getpageinfo
				
				%>
        <tr>
        	<td class="contentBody">&nbsp;&nbsp;&nbsp;</td><td class="contentBody" width="100%"><a href="/web/page/<%=RS4("id")%>/sectionid/<%=rs4("sectionid")%>/interior.asp">
				<%=RS4("NAME")%></a> - 
				
				<%
				String1=rs4("message")
				String2=q1
				string3=">"
				string4="<"
				myLength=len(q1)
				Location= Instr(String1,String2)
				
				StartLocation = cint(Location)-1
					
					If StartLocation < 100 then
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
             <tr><td class="contentBody">&nbsp;</td><td class="contentBody" width="100%">
				<hr style="height: 1px" class="style8" /></td></tr>    

			<% 	
			
			RS6.MoveNext
			count=count + 1
			
			loop
			RS6.close %>
	
     </table>
		    
		    
		    
		    
		    
		    
		    
		    
	
	<%
	else%>

	<%end if%>	    
	 	 	
 	 	
 <%Next%>
 	 	
    
	



    <%if request.querystring("FileNumPosition")<10 then
    X = 0
end if

    
set rs=nothing
	%>
	</td></tr>
</table>

</body>

</html>
<%con.close
set con = nothing
%>