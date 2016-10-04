	function popUpp(URL) {
		day = new Date();
		id = day.getTime();
		eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=yes,location=0,statusbar=0,menubar=0,resizable=0,width=420,height=165,left = 100,top = 100');");
	}

	function DeleteRecord(linkurl) {
		if (confirm("Are you sure you want to delete this community?"))  {
			location.href=linkurl
		}
	}
	
	function checkTextField(fieldname,fieldvalue) {
		bolValid=0
		if (len(fieldvalue)>1)
		{
			bolValid=1
		} 
		document.
		document.formname.fieldname.focus();
		return bolPresent
	}
	
	function checkEmail(fieldname, fieldvalue) {
	    var EmailRegExp=/^([^$@\\ ]+)@((([^$@\\ \.]+)\.)+)([A-Za-z0-9]+)$/;
	}
	

	function ValidAttachment(attach) {
		//if (attach == "") {
		//	return true
		//}
		var tempstring  
		tempstring=attach.substring(attach.length-4,attach.length);
		if (tempstring==".GIF" || tempstring==".JPG" || tempstring=="JPEG" || tempstring==".BMP" || tempstring==".PPT" || tempstring==".DOC" || tempstring==".PDF" || tempstring==".WPD" || tempstring==".gif" || tempstring==".jpg" || tempstring=="jpeg" || tempstring==".bmp" || tempstring==".ppt" || tempstring==".doc" || tempstring==".pdf" || tempstring==".wpd") {
			return true
		}
		return false
	}