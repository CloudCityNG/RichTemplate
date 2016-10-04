<%
dim RSBODY
dim SQLBODY
Set RSBODY = Server.CreateObject("ADODB.Recordset")
SQLBODY = "SELECT * from styles"
RSBODY.Open SQLBODY, Con, 1, 3
%>