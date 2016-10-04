<!--#INCLUDE FILE="sessioncheck.asp"-->

<head>
<meta http-equiv="Content-Language" content="en-us">
<base target="contents">

</head>

<%URL = Request.ServerVariables("server_name")%>
<script LANGUAGE="JavaScript">
<!--
function editProfile(page) {
	newwindow = window.open(page,'' , "toolbar=no,location=no,scrollbars=yes,resizable=yes,width=500,height=450");
}
//-->
</script>

<body style="text-align: left" topmargin="0" leftmargin="0">

<table border="0" cellpadding="0" cellspacing="0" width="100%"  height="77">

    <td valign="top" style="background-image: url('images/header2-0_repeat.gif'); background-repeat: repeat-x; padding-top: 0; background-position:  left top; " width="780">
    <img border="0" 
    <% If Session("PackageID") = "1" then%>
    src="images/richtemplate_header_lite.jpg" 
    <%Elseif Session("PackageID") = "2" then%>
    src="images/richtemplate_header_gold.jpg" 
    <%elseif Session("packageID") = "3" then%>
    src="images/richtemplate_header_platinum.jpg" 
    <%else%>
    src="images/richtemplate_header.jpg" 
    <%end if%>
    width="506" height="50"></td>
    <td valign="top" style="background-image: url('images/header2-0_repeat.gif'); background-repeat: repeat-x; padding-top: 0; background-position:  left top; " height="77">
    &nbsp;</td></tr>
</table>







<p>
&nbsp;</p>








