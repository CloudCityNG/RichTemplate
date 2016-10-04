
function valIDate(myYear, myMonth, myDay)
{
   var myDate = new Date (myYear, myMonth, myDay);

   var myMonthNames = new Array(12);
   myMonthNames[0] = "Jan";
   myMonthNames[1] = "Feb";
   myMonthNames[2] = "Mar";
   myMonthNames[3] = "Apr";
   myMonthNames[4] = "May";
   myMonthNames[5] = "Jun";
   myMonthNames[6] = "Jul";
   myMonthNames[7] = "Aug";
   myMonthNames[8] = "Sep";
   myMonthNames[9] = "Oct";
   myMonthNames[10] = "Nov";
   myMonthNames[11] = "Dec";

   if (myDay != myDate.getDate())
   {
      alert (myMonthNames[myMonth] + ' ' + myDay + noValIDate);
      return false;
   }

   return true;
}


function checkForm(form)
{
   //check that event has a name
   var strname = form.strName.value;

   if (strname == "")
   {
		alert(noEventName);
      return;
   }

   //concatenate start date
   var strStartMonth = form.strStartMonth[form.strStartMonth.selectedIndex].value;
   var strStartDay = form.strStartDay[form.strStartDay.selectedIndex].text;
   var strStartYear = form.strStartYear[form.strStartYear.selectedIndex].text;
   form.strStartDate.value=strStartMonth + "/" + strStartDay + "/" + strStartYear;
	    
   //concatenate end date
   var strEndYear = form.strEndYear[form.strEndYear.selectedIndex].text;
   var strEndMonth = form.strEndMonth[form.strEndMonth.selectedIndex].value;
   var strEndDay = form.strEndDay[form.strEndDay.selectedIndex].text;
   form.strEndDate.value=strEndMonth + "/" + strEndDay + "/" + strEndYear;

   //concatenate start time
   var strStartHour = form.strStartHour[form.strStartHour.selectedIndex].text;
   var strStartMinute = form.strStartMinute[form.strStartMinute.selectedIndex].text;

   var sTod = "";

   if (form.strStartTimeOfDay)
   {
      if (form.strStartTimeOfDay[1].checked)
      {
         sTod = "PM"
         if (strStartHour == "12") strStartHour = 11;
      }
      else sTod = "AM"
   }

   if (strStartHour == "24") strStartMinute = ":00";    

   form.strStartTime.value= "" + strStartHour + "" + strStartMinute + " " + sTod;
	    
   //concatenate end time
   var strEndHour = form.strEndHour[form.strEndHour.selectedIndex].text;
   var strEndMinute = form.strEndMinute[form.strEndMinute.selectedIndex].text;

   var eTod = "";
	    
   if (form.strEndTimeOfDay)
   {
      if (form.strEndTimeOfDay[1].checked)
      {
         eTod = "PM"
         if (strEndHour == "12") strEndHour = 11;
      }
      else eTod = "AM"
   }
		
   if (strEndHour == "24") strEndMinute = ":00";    
   form.strEndTime.value= "" + strEndHour + "" + strEndMinute + " " + eTod;

   //calculate date and time difference

   if (form.sTimeOfDay)
   {
      if (sTod == "PM") strStartHour = parseInt(strStartHour) + 12;
      if (eTod == "PM") strEndHour = parseInt(strEndHour) + 12;
   }

   var fDate = new Date(strStartYear, strStartMonth-1, strStartDay, strStartHour, strStartMinute.substring(1,3), "00")
   var tDate = new Date(strEndYear, strEndMonth-1, strEndDay, strEndHour, strEndMinute.substring(1,3), "00")

   if ((Date.parse(tDate)) < (Date.parse(fDate)))
   {
      alert(incDate);
      return;
   }

   delete fDate, tDate;

   if (valIDate (strStartYear, strStartMonth-1, strStartDay) == false) return;
   if (valIDate (strEndYear, strEndMonth-1, strEndDay) == false) return;

   //check length of Description
   strDescription = form.strDescription.value;
   len = strDescription.length;

   if(len > 10000)
   {
        msg ="Your Description for this event exceeds the maximum number of characters (10000). ";
        msg += "\n\nDo you wish to change this Description or submit the event anyway (the ";
        msg += "Description will be truncated to 10000 characters if you submit it now)?";
        if(!confirm(msg)) return;
        form.strDescription.value = strDescription.substr(0,10000);
   }
	
   form.submit();
}

function updateEnd(form)
{
   form.strEndYear.value = form.strStartYear[form.strStartYear.selectedIndex].value;
   form.strEndMonth.value = form.strStartMonth[form.strStartMonth.selectedIndex].value;
   form.strEndDay.value = form.strStartDay[form.strStartDay.selectedIndex].value;
	   
   form.strEndHour.value = form.strStartHour[form.strStartHour.selectedIndex].value;
   form.strEndMinute.value = form.strStartMinute[form.strStartMinute.selectedIndex].value;

   if (form.strStartTimeOfDay)
   {
      form.strEndTimeOfDay[0].checked = form.strStartTimeOfDay[0].checked
      form.strEndTimeOfDay[1].checked = form.strStartTimeOfDay[1].checked
   }
}
