
function addToPageLoadCallStack(func) { 
    var oldPageLoad = window.pageLoad;
    if (typeof window.pageLoad != 'function') {
        window.pageLoad = func;
    }
    else {
        window.pageLoad = function() {
            if (oldPageLoad) {
                oldPageLoad();
            }
            func();
        }
    }
}

function addToPageResizeCallStack(func) {
    var oldPageResize = window.onresize;
    if (typeof window.onresize != 'function') {
        window.onresize = func;
    }
    else {
        window.onresize = function () {
            if (oldPageResize) {
                oldPageResize();
            }
            func();
        }
    }
}