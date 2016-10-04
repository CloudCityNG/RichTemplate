<!--#INCLUDE VIRTUAL="/admin/sessioncheck.asp"-->
<!--#INCLUDE VIRTUAL="/admin/db_connection.asp"-->
<%PNAME = "Add / Edit Online Education"
'PHELP = "../helpFiles/pageListing.asp#user"%>
<head>
    <link rel="stylesheet" type="text/css" href="../style_richtemplate.css">
</head>
<%
SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString


if Request.Querystring("page")="go" then
    
    userID = request.querystring("id")
	firstName	 	= Request.Form("firstName")
	lastName 		= Request.Form("lastName")
	Email 			= Request.Form("email")
	occupation = Request.form("occupation")
	subjectArea = Request.form("subjectArea")
	ADDRESS			= Request.Form("ADDRESS")
	address2 = Request.form("address2")
	CITY			= Request.Form("CITY")
	state 	= Request.Form("state")
	zipCode = Request.Form("zipCode")
	
	schoolName = Request.Form("schoolName")
	password = Request.Form("password")
	
	
	securityQuestion = Request.Form("securityQuestion")
	securityAnswer = Request.Form("securityAnswer")
	verified = Request.Form("verified")
	

	firstName 	= Replace(firstName,"'","''")
	lastName 	= Replace(lastName,"'","''")
	occupation = Replace(occupation, "'", "''")
	Email 		= Replace(Email,"'","''")
	ADDRESS 	= Replace(Address,"'","''")
	ADDRESS2 	= Replace(Address2,"'","''")
	city 	= Replace(city,"'","''")
	state 	= Replace(state,"'","''")
	zipCode 	= Replace(zipCode,"'","''")
	password 	= Replace(password,"'","''")
	schoolName = replace(schoolname, "'", "''")
	

    If Request.querystring("id") <> "" then
	    editSQL = "Update onlineEducation set firstName = '"&firstName&"', lastName = '"&lastName&"', email = '"&email&"', occupation = '"&occupation&"', address = '"&address&"', address2 = '"&address2&"', city = '"&city&"', state = '"&state&"', zipCode = '"&zipCode&"', schoolname = '"&schoolName&"', password = '"&password&"', securityQuestion = '"&securityQuestion&"', securityAnswer = '"&securityAnswer&"', verified = "&verified&" where id = "&userID
	Else
	    editSQL = "Insert into onlineEducation (firstName, lastName, email, occupation, address, address2, city, state, zipcode, schoolName, password, securityQuestion, securityAnswer, verified) values ('"&firstName&"', '"&lastName&"', '"&email&"', '"&occupation&"',  '"&address&"', '"&address2&"', '"&city&"', '"&state&"', '"&zipcode&"', '"&schoolname&"', '"&password&"', '"&securityQuestion&"', '"&securityAnswer&"', "&verified&")"
	End if	
		Con.execute (editSQL)  

	
	
	
	

Con.close
Response.Redirect("userslist.asp")
end if
%>
<%
If Request.querystring("id") <> "" then
	SQL2 = "SELECT * FROM onlineEducation WHERE ID=" & Request.Querystring("id")
	Set rs2 = con.Execute(SQL2)
    firstName = RS2("firstName")
	lastName = rs2("lastName")
	Email 			= rs2("email")
	occupation = rs2("occupation")
	address2 		= rs2("address2")
	ADDRESS			= rs2("ADDRESS")
	CITY			= rs2("CITY")
	state 	= rs2("state")
	zipCode = rs2("zipCode")
	schoolName = rs2("schoolName")
	password = rs2("password")
	securityQuestion = rs2("SecurityQuestion")
	securityAnswer = rs2("securityAnswer")
	verified = rs2("verified")
	id = rs2("id")

End if

%>
<body topmargin="0" leftmargin="0">
    <form method="POST" action="usersedit.asp?page=go&id=<%=ID%>">
    <input type="hidden" value="<%=ID%>" name="UserID">
    <!--#INCLUDE VIRTUAL="/admin/headernew.inc"-->
    <table border="0" cellpadding="5" cellspacing="0" style="border-collapse: collapse"
        bordercolor="#111111" id="AutoNumber1">
        <tr>
            <td align="center" width="322" class="bodybold" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" class="bodybold" colspan="2">
                General Information
            </td>
        </tr>
       
        <tr>
            <td align="right" class="bodybold">
                <p dir="ltr">
                First Name:&nbsp;
            </td>
            <td width="192">
                <input type="text" name="firstName" size="20" value="<%=firstName%>">
            </td>
        </tr>
        <tr>
            <td align="right" class="bodybold">
                Last Name:&nbsp;&nbsp;
            </td>
            <td width="192">
                <input type="text" name="lastName" size="20" value="<%=lastName%>">
            </td>
        </tr>
          <tr>
            <td align="right" class="bodybold">
                Email:&nbsp;
            </td>
            <td width="192">
                <input type="text" name="email" size="20" value="<%=Email%>">
            </td>
        </tr>
        <tr>
            <td align="right" class="bodybold">
                I am a:&nbsp;
            </td>
            <td width="192">
                <select id="occupation" name="occupation">
                <%If occupation <> "" then %>
                <option selected="selected"><%=occupation %></option>
                <%end if %>
                    <option>Teacher</option>
                    <option>Administrator</option>
                    <option>Curriculum Specialist</option>
                    <option>Parent</option>
                    <option>Student</option>

                </select>
            </td>
        </tr>
        <tr>
            <td align="right" class="bodybold">
                Mailing Address:&nbsp;
            </td>
            <td width="192">
                <input type="text" name="ADDRESS" size="20" value="<%=ADDRESS%>">
            </td>
        </tr>
        <tr>
            <td align="right" class="bodybold">
                Address2:&nbsp;
            </td>
            <td width="192">
                <input type="text" name="address2" size="20" value="<%=ADDRESS2%>">
            </td>
        </tr>
        <tr>
            <td align="right" class="bodybold">
                City:&nbsp;
            </td>
            <td width="192">
                <input type="text" name="city" size="20" value="<%=city%>">
            </td>
        </tr>
        <tr>
            <td align="right" class="bodybold">
                State:&nbsp;
            </td>
            <td width="192">
                <input type="text" name="state" size="20" value="<%=state%>">
            </td>
        </tr>
        <tr>
            <td align="right" class="bodybold">
                Zip Code:&nbsp;
            </td>
            <td width="192">
                <input type="text" name="zipCode" size="20" value="<%=zipCode%>">
            </td>
        </tr>
        <tr>
            <td align="right" class="bodybold">
                School Name:&nbsp;
            </td>
            <td width="192">
                <input type="text" name="schoolName" size="20" value="<%=schoolName%>">
            </td>
        </tr>
       
      
        <tr>
            <td align="right" class="bodybold">
                Password:&nbsp;
            </td>
            <td width="192" class="bodybold">
                <input type="text" name="password" size="20" value="<%=password%>">
            </td>
        </tr>
        <tr>
            <td align="right" class="bodybold">
                Security Question:&nbsp;
            </td>
            <td width="192" class="bodybold">
                <select  id="securityQuestion" name="securityQuestion">
                    
                    <%If securityQuestion <> "" then %>
                    <option><%=securityQuestion %></option>
                    <%end if %>
                    
                    <%getSecurityQuestion = "Select * from securityQuestion"
                    set rs5 = con.execute(getSecurityQuestion)%>
                    <% do while not rs5.eof %>
                    <option>
                        <%=rs5("securityQuestion")%></option>
                    <%Rs5.moveNext
                    Loop %>
                </select>
            </td>
        </tr>
        <tr>
            <td align="right" class="bodybold">
                Security Answer:&nbsp;
            </td>
            <td width="192" class="bodybold">
                <input type="text" name="securityAnswer" size="20" value="<%=securityAnswer%>">
            </td>
        </tr>
        <tr>
            <td align="right" class="bodybold">
                Verified:&nbsp;
            </td>
            <td width="192" class="bodybold">
                <input type="radio" value="1" name="verified" <%IF verified = TRUE THEN%>CHECKED
                    <%END IF%>>Yes&nbsp;
                <input type="radio" name="verified" value="0" <%IF verified = FALSE THEN%>CHECKED
                    <%END IF%>>No
            </td>
        </tr>
        <tr>
            <td align="right" class="bodybold">
                &nbsp;
            </td>
            <td width="192">
                <input type="submit" value="Edit User" name="B1">
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    </form>
    <p>
        &nbsp;</p>
    <%
'rs2.close
con.close
    %>