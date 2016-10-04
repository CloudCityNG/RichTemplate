<%
Sub JavaRedirect 
    url="http://"&Request.ServerVariables("server_name")&"/richadmin/?timeout=true"%>
    
    <SCRIPT language="JavaScript">
    <!--
    parent.location.href = 
    '<%=URL%>';
    //-->
    </SCRIPT>
    
<%End Sub%>

<%

Session.Timeout=40



If Session("USERNAME")="" then

	 
	Call JavaRedirect
	response.Redirect("/richadmin/")

End If

%>
