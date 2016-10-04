<% 	
'check for tags
SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

	tagModule = "Select * from modules where id = '8' and online = 1"
	set tagRS = con.execute(tagModule)
	If Not tagRS.EOF then
		tagModActive = True
	End if
	
CON.CLOSE
SET CON = NOTHING	
SET TAGRS = NOTHING
%>	
