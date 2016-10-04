<!--#INCLUDE FILE="config.asp"-->

<%
Dim objJetEngine

repaird_DB = Replace(Server.MapPath("\"), "website", "data") & "\Repaired_DB.mdb"

Set objJetEngine = Server.CreateObject("JRO.JetEngine")

objJetEngine.CompactDatabase _
    "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" _
        & staticDBPath  & ";", _
    "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" _
        & repaird_DB & ";"

Set objJetEngine = Nothing
%>
