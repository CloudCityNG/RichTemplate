<%PNAME = "Report a bug"%> 
<%HeaderType = "simple"%>
<html>

<head>
<meta http-equiv="Content-Language" content="en-us">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>RichTemplate 2.0 Bug Report</title>
<link rel="stylesheet" type="text/css" href="style_richtemplate.css">
</head>

<body class="bodyNEW" topmargin="0" leftmargin="0">

	<p>
<!--#INCLUDE FILE="headernew.inc"-->
<!--#INCLUDE VIRTUAL="/admin/sessioncheck.asp"-->

<!--#INCLUDE FILE="CDOSYS_Config.inc"-->



<%IF REQUEST.QUERYSTRING("PAGE")="go" then

Bug_Desc 	= Request.Form("Bug_Desc")
Email 		= Request.Form("email")
Phone 		= Request.Form("phone")
IP_Address 	= Request.ServerVariables("REMOTE_ADDR")
Referrer 	= Request.ServerVariables("HTTP_REFERER")
SS_Domain 	= Request.ServerVariables("HTTP_HOST")


	Dim mytxt
	mytxt = "A bug report has been generated for RichTemplate 2.0, domain "&SS_Domain&"<br>"
	mytxt = mytxt & "<b>Bug Description: </b>"&Bug_Desc&"<br>"
	mytxt = mytxt & "<b>RichTemplate Domain: </b>"&SS_Domain&"<br><br>"
	mytxt = mytxt & "<i>Client Information </i><br>"
	mytxt = mytxt & "<b>Email: </b>"&Email&"<br>"
	mytxt = mytxt & "<b>Phone: </b>"&Phone&"<br>"
	mytxt = mytxt & "<b>IP_Address: </b>"&IP_Address&"<br>"
	mytxt = mytxt & "<b>Referrer: </b>"&referrer&"<br>"



	

	Set iMsg.Configuration = iConf
	iMsg.From = "bugreport@richtemplate.com"
	iMsg.To = "rich@richtemplate.com"
	iMsg.CC = "rich@richtemplate.com"
	iMsg.Subject = "RichTemplate 2.0 Bug Report"
	iMsg.HTMLBody = mytxt
	iMsg.Send()
	
	'Release server resrces. 
	Set iMsg	= Nothing 	
	Set iConf	= Nothing
	Set Flds 	= Nothing


%>



<table border="0" width="490" cellspacing="0" cellpadding="6">
	<tr>
		<td>
		<p class="body"><b>Thank you for taking the time to fill out a report.&nbsp; 
		</b></p>
		<p class="body">Your input is extremely valuable to us as we refine our product.&nbsp; 
		Please be sure to take a look at our website for more information 
		about the Rich Template. <a href="http://www.richtemplate.com">www.richtemplate.com</a></p>
		<div align="center">
		<table border="0" id="table2" bgcolor="#F4F4F4" width="100%">
			<tr>
				<td class="standardText" align="left" width="116">
				<p class="body"><b>Phone:</b></td>
				<td class="standardText" width="352">
				<p class="body">(410) 740-5662</td>
			</tr>
			<tr>
				<td class="standardText" align="left" width="116">
				<p class="body"><b>Fax:</b></td>
				<td class="standardText" width="352">
				<p class="body">(443) 346-0205</td>
			</tr>
			<tr>
				<td class="standardText" vAlign="top" align="left" width="116">
				<p class="body"><b>Mailing Address:</b></td>
				<td class="standardText" width="352">
				<p class="body">5850 Waterloo Road&nbsp;<br>
				Suite 230<br>
				Columbia, MD 21045</td>
			</tr>
		</table>
		</div>
		<p class="body" align="center"><a href="javascript:window.close();">Close This Window</a></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
	</tr>
</table>


<%else%>





<form method="POST" action="bugreport.asp?page=go">
	<table border="0" width="490" id="table1" cellpadding="4" cellspacing="0">
		<tr>
			<td class="body" colspan="2" height="50">We hope you are enjoying 
			your RichTemplate 2.0 tour.&nbsp; We apologize for any errors 
			encountered during your trial.&nbsp; To improve your RichTemplate 2.0 
			experience we would like to know what errors you encountered.</td>
		</tr>
		<tr>
			<td class="body" colspan="2" bgcolor="#F4F4F4">
			<p class="bodybold">Please provide a brief description of the problem:</font></td>
		</tr>
		<tr>
			<td width="490" class="body" colspan="2" bgcolor="#F4F4F4">
			<textarea rows="8" name="Bug_Desc" cols="57"></textarea></td>
		</tr>
		<tr>
			<td width="490" class="body" colspan="2">If you would like us to contact you 
			with more information on our product, please provide us with contact 
			information below.</td>
		</tr>
		<tr>
			<td width="150" class="body">Contact Email Address:</td>
			<td width="326" class="body">
			<input type="text" name="email" size="20"></td>
		</tr>
		<tr>
			<td width="150" class="body">Contact Phone Number:</td>
			<td width="326" class="body">
			<input type="text" name="phone" size="20"></td>
		</tr>
		<tr>
			<td width="150" class="body">&nbsp;</td>
			<td width="326" class="body">&nbsp;</td>
		</tr>
		<tr>
			<td width="490" class="body" colspan="2">
			<p align="center">
			<font size="2" face="verdana, arial, helvetica, sans-serif">
	<input type="submit" value="Send Report" name="B1"></font></td>
		</tr>
		</table>
	<p>&nbsp;</p>
</form>
<%end if%>
</body>

</html>