<%

mdbfile = "richtemplate.mdb"
'ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;DBQ=" & Server.MapPath( mdbfile ) & ";" 
ConnectionString = "Driver={Microsoft Access Driver (*.mdb)};DBQ=" & Server.MapPath( mdbfile ) & ";"
Set Con = server.createobject("adodb.connection")
con.open Connectionstring
%>