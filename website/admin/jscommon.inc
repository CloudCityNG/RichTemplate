<!--#INCLUDE FILE="sessioncheck.asp"-->
<script language=javascript>

var fileWin
var prvTr
var prvTr2

// window.onload = this.focus
window.onerror = stopError

	function stopError() {
		return true;
	}

	function ConfirmDelete(filename) {
		var deleteFile = confirm("Are you sure you wish to delete?");
		if (deleteFile == true) {
			document.location = "http://$URL$SCRIPTNAME?newdir=$NEWDIR&ToDo=Delete&FileName=" + filename
		}
	}

	function ConfirmDeleteFolder(filename) {
		var deleteFile = confirm("Are you sure you wish to delete this folder and ALL its contents?");
		if (deleteFile == true) {
			document.location = "http://$URL$SCRIPTNAME?newdir=$NEWDIR&isFolder=1&ToDo=Delete&FileName=" + filename
		}
	}

	function ConfirmImageDeleteFolder(filename) {
		var deleteFile = confirm("Are you sure you wish to delete this folder and ALL its contents?");
		if (deleteFile == true) {
			document.location = "http://$URL$SCRIPTNAME?newimagedir=$NEWIMAGEDIR&isFolder=1&ToDo=Delete&FileName=" + filename + "&FromImageDir=1"
		}
	}

	function ConfirmImageDelete(filename) {
		var deleteFile = confirm("Are you sure you wish to delete?");
		if (deleteFile == true) {
			document.location = "http://$URL$SCRIPTNAME?newimagedir=$NEWIMAGEDIR&ToDo=Delete&FileName=" + filename + "&FromImageDir=1"
		}
	}

	function SelectImage(ImageName) {
		window.opener.selectImage("http://$URL$CurrentImageDirectory/" + ImageName);
		self.close();
	}

	function SetBackgd(ImageName) {
		var setBg = confirm("Are you sure you wish to set this image as the page background image?");
		if (setBg == true) {
			window.opener.setBackgd("http://$URL$CurrentImageDirectory/" + ImageName);
			self.close();
		}
	}

	function getLink() {

		if (window.opener.foo.document.selection.type == "Control") {
			var oControlRange = window.opener.foo.document.selection.createRange();
			if (oControlRange(0).tagName.toUpperCase() == "IMG") {
				var oSel = oControlRange(0).parentNode;
			}
		} else {
			oSel = window.opener.foo.document.selection.createRange().parentElement();
		}

		if (oSel.tagName.toUpperCase() == "A")
		{
			document.linkForm.targetWindow.value = oSel.target
			document.linkForm.link.value = oSel.href
		}
	}

	function InsertLink() {
		targetWindow = document.linkForm.targetWindow.value;
		var linkSource = document.linkForm.link.value

		if (linkSource != "")
		{
			var oNewLink = window.opener.foo.document.createElement("<A>");
			oNewSelection = window.opener.foo.document.selection.createRange()

			if (window.opener.foo.document.selection.type == "Control")
			{
				selectedImage = window.opener.foo.document.selection.createRange()(0);
				selectedImage.width = selectedImage.width
				selectedImage.height = selectedImage.height
			}

			oNewSelection.execCommand("CreateLink",false,linkSource);

			if (window.opener.foo.document.selection.type == "Control")
			{
				oLink = oNewSelection(0).parentNode;
			} else
				oLink = oNewSelection.parentElement()

			if (targetWindow != "")
			{
				oLink.target = targetWindow;
			} else
				oLink.removeAttribute("target")

			window.opener.foo.focus();
			self.close();
		} else {
			alert("URL cannot be left blank")
			document.linkForm.link.focus()
		}
	}

	function CreateLink(LinkSource) {
		document.linkForm.link.value = LinkSource;
		document.linkForm.link.focus()
	}

	function RemoveLink() {
		if (window.opener.foo.document.selection.type == "Control")
		{
			selectedImage = window.opener.foo.document.selection.createRange()(0);
			selectedImage.width = selectedImage.width
			selectedImage.height = selectedImage.height
		}

		window.opener.foo.document.execCommand("Unlink");
		window.opener.foo.focus();
		self.close();
	}

	function getAnchors() {
		var allLinks = window.opener.foo.document.body.getElementsByTagName("A");
		for (a=0; a < allLinks.length; a++) {
				if (allLinks[a].href.toUpperCase() == "") {
					document.write("<option value=#" + allLinks[a].name + ">" + allLinks[a].name + "</option>")
				}
		}
	}

	function ViewFile(fileName) {
		
		if (fileWin) { fileWin.close() }

		var leftPos = (screen.availWidth-700) / 2
		var topPos = (screen.availHeight-500) / 2 
	 	fileWin = window.open(fileName,'fileWindow','width=700,height=500,scrollbars=yes,resizable=yes,titlebar=0,top=' + topPos + ',left=' + leftPos);
		fileWin.focus()
		fileWin.location.reload(true);
	}

</script>