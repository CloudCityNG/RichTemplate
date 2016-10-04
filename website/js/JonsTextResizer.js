
// Be sure to give a table or div the id of mainContent
// This script will resize any text within that area

function changemysize(myvalue)
// this function is called by the user clicking on a text size choice
{
    // find the div to apply the text resizing to
    var div = document.getElementById("mainContent");
    // apply the text size change
    div.style.fontSize = myvalue + "px";
    // store the text size choice into a cookie
    document.cookie = "mysize=" + myvalue;
}

function getmycookie(myname)
// this function is called by the function mydefaultsize()
// this function merely looks for any previously set cookie and then returns its value
{
    // if any cookies have been stored then
    if (document.cookie.length > 0) {
        // where does our cookie begin its existence within the array of cookies  
        mystart = document.cookie.indexOf(myname + "=");
        // if we found our cookie name within the array then
        if (mystart != -1) {
            // lets move to the end of the name thus the beginning of the value
            // the '+1' grabs the '=' symbol also
            mystart = mystart + myname.length + 1;
            // because our document is only storing a single cookie, the end of the cookie is found easily
            myend = document.cookie.length;
            // return the value of the cookie which exists after the cookie name and before the end of the cookie
            return document.cookie.substring(mystart, myend);
        }
    }
    // if we didn't find a cookie then return nothing  
    return "";
}

function mydefaultsize() {
    // this function is called by the body onload event
    // this function is used by all sub pages visited by the user after the main page
    var div = document.getElementById("mainContent");
    // call the function getmycookie() and pass it the name of the cookie we are searching for
    // if we found the cookie then
    if (getmycookie("mysize") > 0) {
        // apply the text size change	
        div.style.fontSize = getmycookie("mysize") + "px";
    }
}