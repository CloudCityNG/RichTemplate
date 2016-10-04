/* New code for dynamic link generation in our Telerik Rad Editor 02/22/2011 End */
Telerik.Web.UI.Editor.CommandList["InsertSpecialLink"] = function (commandName, editor, args) {
    var elem = editor.getSelectedElement(); //returns the selected element.

    if (elem.tagName == "A") {
        editor.selectElement(elem);
        argument = elem;
    }
    else {
        //remove links if present from current selection - because of JS error thrown in IE
        var commandsManager = editor.get_commandsManager();
        var commandIndex = commandsManager.getCommandsToUndo().length - 1;
        commandsManager.removeCommandAt(commandIndex);

        var content = editor.getSelectionHtml();
        var link = editor.get_document().createElement("A");
        link.innerHTML = content;
        argument = link;
    }

    var myCallbackFunction = function (sender, args) {
        if (args != null) {
            var strReturnedHyperlink = String.format("<a href='{0}' title='{1}' target='{2}'>{3}</a>", args.linkUrl, args.linkTitle, args.linkTarget, args.linkText);
            editor.pasteHtml(strReturnedHyperlink);
        }
    }

    editor.showExternalDialog("/editorConfig/dialogs/InsertSpecialLink.aspx", argument, 600, 400, myCallbackFunction, null, "Insert Rich Template Link or Anchor Link", true, Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move, false, true);
};

Telerik.Web.UI.Editor.CommandList["InsertAnchorLink"] = function (commandName, editor, args) {
    var elem = editor.getSelectedElement(); //returns the selected element.

    if (elem.tagName == "A") {
        editor.selectElement(elem);
        argument = elem;
    }
    else {
        //remove links if present from current selection - because of JS error thrown in IE
        var commandsManager = editor.get_commandsManager();
        var commandIndex = commandsManager.getCommandsToUndo().length - 1;
        commandsManager.removeCommandAt(commandIndex);

        var content = editor.getSelectionHtml();
        var link = editor.get_document().createElement("A");
        link.innerHTML = content;
        argument = link;
    }

    var myCallbackFunction = function (sender, args) {
        var strReturnedAnchor = String.format("<a name='{0}'>&nbsp;</a>", args.linkName);
        editor.pasteHtml(strReturnedAnchor);
    }

    editor.showExternalDialog("/editorConfig/dialogs/InsertAnchorLink.aspx", argument, 450, 260, myCallbackFunction, null, "Insert Anchor Link", true, Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move, false, true);
};
/* New code for dynamic link generation 02/22/2011 End */