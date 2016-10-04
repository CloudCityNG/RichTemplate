
<%


	DIM iMsg

	Const cdoSendUsingPort = 2
	StrRichTemplateHost = "secure.emailsrvr.com"


	set iMsg = CreateObject("CDO.Message")
	set iConf = CreateObject("CDO.Configuration")

	Set Flds = iConf.Fields

	With Flds
	  	.Item("http://schemas.microsoft.com/cdo/configuration/smtpserverport") = 25 
	  	.Item("http://schemas.microsoft.com/cdo/configuration/sendusing") = cdoSendUsingPort
	 	.Item("http://schemas.microsoft.com/cdo/configuration/smtpserver") = StrRichTemplateHost
	  	.Item("http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout") = 120

		.Item("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = 1 
		.Item("http://schemas.microsoft.com/cdo/configuration/sendusername") = "form@richtemplate.com" 
		.Item("http://schemas.microsoft.com/cdo/configuration/sendpassword") = "form101!2#4" 



	  .Update
	End With




%>