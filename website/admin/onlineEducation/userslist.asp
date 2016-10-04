<!--#INCLUDE VIRTUAL="/admin/sessioncheck.asp"-->
<!--#INCLUDE VIRTUAL="/admin/db_connection.asp"-->
<%PNAME = "Administer Online Education"
'PHELP = "../helpFiles/pageListing.asp#user"%>
<%

' **** displays error messages ****

	
	If Request.Querystring("error")="2" then
			ErrorMessage= "This Account has been disabled.  Please see you system administrator for details."
	Elseif Request.Querystring("error")="3" then
			ErrorMessage = "This user accout already exixts.  Please choose another user name."

	End if




%>
<head>

    <script>
        function confirmdelete(linkurl) {
            if (confirm("Are you sure you want to delete this record?")) {
                location.href = linkurl
            }
        }
    </script>

    <script>
        function confirmstatus(linkurl) {
            if (confirm("Are you sure you want to INACTIVATE this user and send denial email?")) {
                location.href = linkurl
            }
        }
    </script>

    <script>
        function confirmstatus2(linkurl) {
            if (confirm("Are you sure you want to ACTIVATE this user and send acceptance email?")) {
                location.href = linkurl
            }
        }
    </script>

    <link rel="stylesheet" type="text/css" href="../style_richtemplate.css">
</head>
<body topmargin="0" leftmargin="0">
    <!--#INCLUDE VIRTUAL="/admin/headernew.inc"-->
    <table border="0" width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td width="10">
                &nbsp;
            </td>
            <td>
                <table border="0" width="100%" cellspacing="0" cellpadding="0" id="table1">
                    <tr>
                        <td width="30">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="30">
                            <img border="0" src="../images/user1_add.gif" width="24" height="24">
                        </td>
                        <td>
                            <a href="usersedit.asp" class="bodybold">Add New User</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <font color="#FE1E3E"><b>
                                <%=ErrorMessage%></b></font>.</b>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="10">
                &nbsp;
            </td>
            <td>
                <table class="body" cellpadding="3" cellspacing="2" style="border-collapse: collapse"
                    bordercolor="#111111" id="AutoNumber1">
                    <tr>
                        <td width="3%" class="table-or">
                            &nbsp;
                        </td>
                        <td width="80" class="table-or">
                            <b><span class="bodywhite">First Name</span></b>
                        </td>
                        <td width="80" class="table-or">
                            <b><span class="bodywhite">Last Name</span></b>
                        </td>
                        <td width="150" class="table-or">
                            <b>Email</b>
                        </td>
                        <!-- <td width="20%" class="table-or"><b><span class="bodywhite">Email</span></b></td>-->
                        <td class="table-or" width="30">
                            <b><span class="bodywhite">Edit</span></b>
                        </td>
                        <td class="table-or" width="30">
                            <b><span class="bodywhite">Delete</span></b>
                        </td>
                        <td class="table-or" width="30">
                            <b><span class="bodywhite">Status</span></b>
                        </td>
                        <td class="table-or" width="30">
                            <b><span class="bodywhite">Action</span></b>
                        </td>
                    </tr>
                    <tr>
                        <%
SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString



	SQL2 = "SELECT * FROM onlineEducation"

	
	
	'RESPONSE.WRITE SQL2
	SET RS2 = CON.EXECUTE(SQL2)
	if not RS2.EOF then
		rs2.MoveFirst
		mybgcolor="#eeeeee"
		do while Not RS2.EOF 
                        %>
                        <td bgcolor="<%=mybgcolor%>" width="3%">
                            <img border="0" src="../images/icon_user.gif" width="16" height="16">
                        </td>
                        <%
	If RS2("verified")=True THEN 
	STATUSDISPLAY = "Active"
	STRIKESTART = ""
	STRIKEEND = ""
	ELSE
	STATUSDISPLAY = "Inactive"
	STRIKESTART = "<STRIKE>"
	STRIKEEND = "</STRIKE>"
	END IF
                        %>
                        <td bgcolor="<%=mybgcolor%>" width="80">
                            <%=STRIKESTART%><%=rs2("firstname")%><%=STRIKEEND%>&nbsp;
                        </td>
                        <td bgcolor="<%=mybgcolor%>" width="80">
                            <%=STRIKESTART%><%=rs2("lastName")%><%=STRIKEEND%>&nbsp;
                        </td>
                        <td bgcolor="<%=mybgcolor%>" width="150">
                            <%=strikestart%>
                            <%=RS2("email") %>
                            <%=STRIKEEND%>
                        </td>
                        <!--<td bgcolor="<%=mybgcolor%>" width="20%"><%=rs2("Email")%>&nbsp;</td>-->
                        <td bgcolor="<%=mybgcolor%>" width="30">
                            <a href="usersedit.asp?id=<%=rs2("ID")%>">edit</a>
                        </td>
                        <td bgcolor="<%=mybgcolor%>" width="30">
                            <a href="javascript:confirmdelete('usersdelete.asp?id=<%=rs2("ID")%>')">delete</a>
                        </td>
                        <td bgcolor="<%=mybgcolor%>" width="30">
                            <%If IsNull(RS2("verified")) then%>
                            <font color="gray">Pending</font>
                            <%ElseIf RS2("verified")=False Then%>
                            <font color="red">Inactive</font>
                            <%ELSEIF RS2("verified")=True Then%>
                            <font color="green">Active</font>
                            <%End IF%>
                        </td>
                        <td bgcolor="<%=mybgcolor%>" width="100">
                            <%If IsNull(RS2("verified")) then%>
                            <a href="javascript:confirmstatus2('userstatus.asp?id=<%=rs2("ID")%>&status=makeactive')">
                                <font color="green">Activate</font></a> | <a href="javascript:confirmstatus('userstatus.asp?id=<%=rs2("ID")%>&status=makeinactive')">
                                    <font color="red">Deny</font></a>
                            <%ElseIf RS2("verified")=False Then%>
                            <a href="javascript:confirmstatus2('userstatus.asp?id=<%=rs2("ID")%>&status=makeactive')">
                                <font color="green">Activate</font></a>
                            <%ELSEIF RS2("verified")=True Then%>
                            <a href="javascript:confirmstatus('userstatus.asp?id=<%=rs2("ID")%>&status=makeinactive')">
                                <font color="red">Deny</font></a>
                            <%End IF%>
                        </td>
                    </tr>
                    <%
  
      				if mybgcolor="#eeeeee" then
  						mybgcolor="#ffffff"
  				else
  						mybgcolor="#eeeeee"
  				end if
  		rs2.movenext
		loop
	else
	end if
	rs2.close
                    %>
                </table>
            </td>
        </tr>
    </table>
    <b>&nbsp;
        <%
Con.Close
        %>