/* New code for dynamic link generation in our Telerik Rad Editor 02/22/2011 End */
function getRadWindow() {
    if (window.radWindow) {
        return window.radWindow;
    }
    if (window.frameElement && window.frameElement.radWindow) {
        return window.frameElement.radWindow;
    }
    return null;
}

/* New code for dynamic link generation 02/22/2011 End */