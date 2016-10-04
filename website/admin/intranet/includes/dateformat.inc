
<%
Function DateFormat(Data)
Dim WD, OD
	if not(isdate(data)) then WD = Date() else WD = cdate(data)
	OD = right("00" & cstr(month(WD)),2) & "/" & cstr(day(WD)) & "/" & cstr(year(WD))
	DateFormat = "#" & OD & "#"
End Function
%>