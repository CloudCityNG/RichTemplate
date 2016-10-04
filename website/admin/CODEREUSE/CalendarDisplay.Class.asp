<%
'***************************************
' File:	  Calendar.asp
' Author: Jacob Gilley
' Email:  avis7@airmail.net
' Date:   12/18/2000
' Comments: This code is free to use and
'			modify at your discretion. I
'			provide this code "AS IS" and
'			am not responsible for any ill
'			effects this script may cause.
'****************************************

'****************************************
'Updated 1/30/2007
'Kendra Carmichael
'Updated to use CSS for layout and 
'to fit in needs of richtemplate
'****************************************


Class CalendarDisplay
	Public CalendarOutlineTableCss
	Public CalendarHeaderTableCss
	Public CalendarCellSubheadCss 
	Public CalendarCellActiveCss
	Public CalendarCellInactiveCss
	Public CalendarCellFooterCss 
	Public CalendarCellTodayCss 
	Public TodayBGColor
	Public OnDayClick
	Public OnNextMonthClick
	Public OnPrevMonthClick
	Public ShowDateSelect
	Private mdDate
	Private msToday
	Private mnDay
	Private mnMonth
	Private mnYear
	Private mnDayMonthStarts
	Private mnDaysInMonth
	Private mcolDays
	Private mbDaysInitialized
	
	Private Sub Class_Initialize()
		'css properties not currently settable. Must use class names specified here in stylesheet.
		CalendarOutlineTableCss="CalendarOutlineTableCss"
		CalendarHeaderTableCss="CalendarHeaderTableCss"
		CalendarCellSubheadCss ="CalendarCellSubheadCss "
		CalendarCellActiveCss="CalendarCellActiveCss"
		CalendarCellInactiveCss="CalendarCellInactiveCss"
		CalendarCellFooterCss="CalendarCellFooterCss"
		CalendarCellTodayCss ="CalendarCellTodayCss"
		ShowDateSelect = True
		msToday =  FormatDateTime(DateSerial(Year(Now()), Month(Now()), Day(Now())), 2)

		
		Set mcolDays = Server.CreateObject("Scripting.Dictionary")
		If Request("date") <> "" Then SetDate(Request("date")) Else SetDate(Now())

		OnDayClick = Request.ServerVariables("SCRIPT_NAME")
		OnNextMonthClick = Request.ServerVariables("SCRIPT_NAME") & "?date=" & Server.URLEncode(DateSerial(mnYear, mnMonth + 1, mnDay)) & getQuerystringVars()
		OnPrevMonthClick = Request.ServerVariables("SCRIPT_NAME") & "?date=" & Server.URLEncode(DateSerial(mnYear, mnMonth - 1, mnDay)) & getQuerystringVars()

		mbDaysInitialized = False
	End Sub
	
	
	Private Sub Class_Terminate()
		If IsObject(mcolDays) Then
			mcolDays.RemoveAll
			Set mcolDays = Nothing
		End If
	End Sub
	
	Public Property Get GetDate()
		GetDate = mdDate
	End Property
	
	Public Property Get DaysInMonth()
		DaysInMonth = mnDaysInMonth
	End Property
	
	Public Property Get WeeksInMonth()
		If (mnDayMonthStarts + mnDaysInMonth - 1) > 35 Then
			WeeksInMonth = 6
		Else
			WeeksInMonth = 5
		End If
	End Property
	
	Public Property Get Days(nIndex)
		If Not mbDaysInitialized Then InitDays()
		If mcolDays.Exists(nIndex) Then Set Days = mcolDays.Item(nIndex)
	End Property
	
	Private Function getQuerystringVars()
		dim str,Field
		str=""
		For Each Field in Request.Querystring
				if Field<>"date" then
					str= str & "&" & Field & "=" & Request.Querystring(Field)
				end if
		Next
		getQuerystringVars=str
	End Function
	
	
	Private Sub InitDays()
		Dim nDayIndex
		Dim objNewDay
		
		If mcolDays.Count > 0 Then mcolDays.RemoveAll()
		
		For nDayIndex = 1 To mnDaysInMonth
			Set objNewDay = New CalendarDay
			objNewDay.DateString = FormatDateTime(DateSerial(mnYear, mnMonth, nDayIndex),2)
			objNewDay.OnClick = OnDayClick
			
			mcolDays.Add nDayIndex, objNewDay
		Next
		
		mbDaysInitialized = True
	End Sub
	
	Public Sub SetDate(dDate)
		mdDate  = CDate(dDate)
		mnDay   = Day(dDate)
		mnMonth = Month(dDate)
		mnYear  = Year(dDate)
	
		mnDaysInMonth =  Day(DateAdd("d", -1, DateSerial(mnYear, mnMonth + 1, 1)))
		mnDayMonthStarts = WeekDay(DateAdd("d", -(Day(CDate(dDate)) - 1), CDate(dDate)))
	End Sub
	
	Public Sub Draw()
		Dim nDayCount
		Dim objDay
		
		If Not mbDaysInitialized Then InitDays()
		
		
		Send "<div name=""CalendarDiv"" id=""CalendarDiv"">"
		Send "<table class=""" & CalendarOutlineTableCss & """>"
		Send "<tr><td colspan=""7"">"
		Send "	<table width=""100%"" class=""" & CalendarHeaderTableCss & """>"
		Send "	<tr>"
		Send "	<td align=""left""><a href=""" & Replace(OnPrevMonthClick, "$date", DateSerial(mnYear, mnMonth - 1, mnDay)) & """>&nbsp;&lt;&lt;</a></td>"
		Send "	<td align=""center"">" & MonthName(mnMonth) & " " & mnYear & "</td>"
		Send "	<td align=""right""><a href=""" & Replace(OnNextMonthClick, "$date", DateSerial(mnYear, mnMonth + 1, mnDay)) & """>&gt;&gt;&nbsp;</a></td>"
		Send "	</tr>"
		Send "	</table>"
		Send "</td></tr>"
		Send "<tr>"
		Send "<td  class=""" & CalendarCellSubheadCss & """>S</td>"
		Send "<td  class=""" & CalendarCellSubheadCss & """>M</td>"
		Send "<td  class=""" & CalendarCellSubheadCss & """>T</td>"
		Send "<td  class=""" & CalendarCellSubheadCss & """>W</td>"
		Send "<td  class=""" & CalendarCellSubheadCss & """>T</td>"
		Send "<td  class=""" & CalendarCellSubheadCss & """>F</td>"
		Send "<td  class=""" & CalendarCellSubheadCss & """>S</td>"
		Send "</tr>"
		
		Send "<tr>"
		For nDayCount = 1 To mnDayMonthStarts - 1
			Send "<td class=""" & CalendarCellInactiveCss & """>&nbsp;</td>"
		Next
		
		nDayCount = nDayCount - 1
		
		For Each objDay In mcolDays.Items
		
			If nDayCount = 7 Then 
				Send "</tr><tr>"
				nDayCount = 0
			End If	
			
			Response.Write "<td "
			
			'If objDay.DateString = msToday Then 
			
			If cdate(objDay.DateString) = mdDate Then 
				Send "class=""" & CalendarCellTodayCss & """>" 
			Else 
				Send "class=""" & CalendarCellActiveCss & """>"
			End if
			objDay.Draw()
			Send "</td>"
			
			nDayCount = nDayCount + 1
		Next

		If nDayCount < 7 Then
			For nDayCount = nDayCount To 6
				Send "<td class=""" & CalendarCellInactiveCss & """>&nbsp;</td>"
			Next
		End If
			
		Send "</tr>"
		
		If ShowDateSelect Then
			Send "<tr><td colspan=""7"" class=""" & CalendarCellFooterCss & """>"
			DrawDateSelect()
			Send "</td></tr>"
		End If
		
		Send "</table>"
		Send "</div>"

	End Sub
	
	Private Sub DrawDateSelect()
		Dim nIndex

		Send "	<table border=""0"" align=""center"">"
		Send "	<form id=""frmGO"" name=""frmGO"">"
		Send "	<tr>"
		Send "	<td><select name=""month"">"
			For nIndex = 1 To 12
				Response.Write "<option value=""" & nIndex & """" 
				If nIndex = Month(mdDate) Then Response.Write " selected"
				Send ">" & MonthName(nIndex, True) & "</option>"
			Next
		Send "	</select></td>"
		Send "	<td><select name=""year"">"
			For nIndex = Year(Now()) - 4 To Year(Now()) + 6
				Response.Write "<option value=""" & nIndex & """" 
				If nIndex = Year(mdDate) Then Response.Write " selected"
				Send ">" & CStr(nIndex) & "</option>"
			Next
		Send "	</select></td>"
		Send "	<td><input type=""button"" Value=""Go"" onclick=""document.location='" & Request.ServerVariables("SCRIPT_NAME") & "?date='+this.form.month.options[this.form.month.selectedIndex].value+'%2F1%2F'+this.form.year.options[this.form.year.selectedIndex].value+'" & getQuerystringVars() & "';"" class=""btn""></td>"
		Send "	</tr>"
		Send "	</form>"
		Send "	</table>"
	End Sub
	
	Private Sub Send(sHTML)
		Response.Write sHTML & vbCrLf
	End Sub

End Class


Class CalendarDay
	Public DateString
	Public OnClick
	Private mcolActivities
	Private mbActivitiesInit
	
	Private Sub Class_Initialize()
		mbActivitiesInit = False
	End Sub
	
	Private Sub Class_Terminate()
		If IsObject(mcolActivities) Then
			mcolActivities.RemoveAll()
			Set mcolActivities = Nothing
		End If
	End Sub
	
	Private Sub InitActivities()
		Set mcolActivities = Server.CreateObject("Scripting.Dictionary")
		mbActivitiesInit = True
	End Sub
	
	Public Sub AddActivity(sActivity, sClass)
		If Not mbActivitiesInit Then InitActivities()
		'mcolActivities.Add mcolActivities.Count + 1, "bgcolor=""" & sColor & """>" & sActivity
		mcolActivities.Add mcolActivities.Count + 1, "<p class=""" & sClass & """>" & sActivity & "</p>"

	End Sub
	
	Public Sub Draw()
		Dim objActivity,HTML,bolHasItems
		bolHasItems=false
		If mbActivitiesInit Then
			For Each objActivity In mcolActivities.Items
				bolHasItems=true
				if len(objActivity)>16 then 'ie there was actual content, not just the formmating string
					HTML=HTML & objActivity	
				end if
			Next
		End If

		if bolHasItems=true then
			'if onclick is a javascript event leave alone. else server.urlencode the date
			if instr(1,onClick,"javascript:") then
				Send "<a href=""" & Replace(OnClick, "$date", DateString) & """>" & Day(DateString) & "</a><br/>"
			else
				Send "<a href=""" & Replace(OnClick, "$date", server.urlencode(DateString)) & """>" & Day(DateString) & "</a><br/>"
			end if			
		else
			Send Day(DateString) & "<br>"
		end if
			Send HTML
	End Sub

	Private Sub Send(sHTML)
		Response.Write sHTML & vbCrLf
	End Sub
End Class


%>