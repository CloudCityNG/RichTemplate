
function validate(myYear, myMonth, myDay)
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
      alert (myMonthNames[myMonth] + ' ' + myDay + ' ' + noValidate);
      return false;
   }

   return true;
}


function checkForm(form)
{
   //check that event has a name
   var name = form.strName.value;

   if (name == "")
   {
		alert(noEventName);
      return;
   }

   //concatenate start date
   var StartMonth = form.strStartMonth[form.strStartMonth.selectedIndex].value;
   var StartDay = form.strStartDay[form.strStartDay.selectedIndex].text;
   var StartYear = form.strStartYear[form.strStartYear.selectedIndex].text;
   form.strStartDate.value = StartMonth + "/" + StartDay + "/" + StartYear;
	    
   //concatenate end date
   var EndYear = form.strEndYear[form.strEndYear.selectedIndex].text;
   var EndMonth = form.strEndMonth[form.strEndMonth.selectedIndex].value;
   var EndDay = form.strEndDay[form.strEndDay.selectedIndex].text;
   form.strEndDate.value = EndMonth + "/" + EndDay + "/" + EndYear;

   //concatenate start time
   // var StartHour = form.strStartHour[form.strStartHour.selectedIndex].text;
   //var StartMinute = form.strStartMinute[form.strStartMinute.selectedIndex].text;

   //var sTod = "";

   //if (form.strStartTimeOfDay)
   //{
     // if (form.strStartTimeOfDay[1].checked) sTod = "PM"
      //else sTod = "AM";
   //}

   //if (StartHour == "24") StartMinute = ":00";    

   //form.strStartTime.value= "" + StartHour + "" + StartMinute + " " + sTod;
	    
   //concatenate end time
   //var EndHour = form.strEndHour[form.strEndHour.selectedIndex].text;
   //var EndMinute = form.strEndMinute[form.strEndMinute.selectedIndex].text;

   //var eTod = "";
	    
   //if (form.strEndTimeOfDay)
   //{
     // if (form.strEndTimeOfDay[1].checked) eTod = "PM"
      //else eTod = "AM";
   //}

   //if (EndHour == "24") EndMinute = ":00";    
   //form.strEndTime.value= "" + EndHour + "" + EndMinute + " " + eTod;

   //calculate date and time difference

   if (form.strStartTimeOfDay)
   {
      if (parseInt(StartHour) == 12)
      {
         if (sTod == "AM") StartHour = 0;
      } else {
         if (sTod == "PM") StartHour = parseInt(StartHour) + 12;
      }
  
      if (parseInt(EndHour) == 12)
      {
         if (eTod == "AM") EndHour = 0;
      } else {
         if (eTod == "PM") EndHour = parseInt(EndHour) + 12;
      }
   }

   var fDate = new Date(StartYear, StartMonth-1, StartDay, StartHour, StartMinute.substring(1,3), "00")
   var tDate = new Date(EndYear, EndMonth-1, EndDay, EndHour, EndMinute.substring(1,3), "00")

   if ((Date.parse(tDate)) < (Date.parse(fDate)))
   {
      alert(incDate);
      return;
   }



   delete fDate, tDate;

   if (validate (StartYear, StartMonth-1, StartDay) == false) return;
   if (validate (EndYear, EndMonth-1, EndDay) == false) return;

   //check length of Description
   strDescription = form.strDescription.value;
   len = strDescription.length;

   if (len > 255)
   {
        if (!confirm (descLengthError)) return;
        form.strDescription.value = strDescription.substr(0,255);
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


   if (form.sTimeOfDay)
   {
      form.eTimeOfDay[0].checked = form.sTimeOfDay[0].checked
      form.eTimeOfDay[1].checked = form.sTimeOfDay[1].checked
   }
}
