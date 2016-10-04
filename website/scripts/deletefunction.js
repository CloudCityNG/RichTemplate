
function confirmdelete(linkurl) {
	if (confirm("Are you sure you want to delete this record?"))  {
		location.href=linkurl
	}

}

function confirmdelete2(linkurl) {
	if (confirm("CAUTION! Are you sure you want to delete this page and all pages under it?"))  {
		location.href=linkurl
	}
}



