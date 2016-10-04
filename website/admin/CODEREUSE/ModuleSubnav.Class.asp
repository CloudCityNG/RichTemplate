<%
	
	'Class ModuleSubnav - takes link info that is fed to it, stores in dictionary object, and then returns an html string displaying all with formatting


   Class ModuleSubnav
     
     '****************** PROPERTIES ********************
     Private objNavLinkDict  'storage container for collection of link objects     
     
     '**************************************************


     '************** INITIALIZE/TERMINATE **************
     Private Sub Class_Initialize()
		'Initialize object that will hold all the nav links
        Set objNavLinkDict= CreateObject("Scripting.Dictionary")
     End Sub

     Private Sub Class_Terminate()
        Set objNavLinkDict= Nothing    ' Clean up
     End Sub
     '**************************************************


     '************* PROPERTY LET STATMENTS *************
	 'none

     '**************************************************


     '************* PROPERTY GET STATMENTS *************
	 'none
     '**************************************************


     '********************* METHODS ********************
     Public Function AddLink(LinkName,LinkUrl,LinkTarget)
       'Adds a nav link to the objNavLinkDict object
       'if URL is blank, leave out href tag
       Dim strLinkHTML
       if LinkUrl<>"" then
	       strLinkHTML="<a href=""" & LinkUrl & """ "
    	   
    	   if LinkTarget<>"" then
		       strLinkHTML=strLinkHTML & "target=""" & LinkTarget & """ "
	  	   end if
	  	   strLinkHTML=strLinkHTML & ">" 
	   end if
	   strLinkHTML=strLinkHTML & LinkName 
	   if LinkURL<>"" then
	   	   strLinkHTML=strLinkHTML & "</a>"
	    end if
	    
       'Add the HTML to the Link dictionary
       objNavLinkDict.Add LinkName, strLinkHTML
     End Function


     Public Function GenerateNav()
		'generate nav - cycle through all links in the objNavLinkDict object and display with formmatting
		dim HTML, i, dictionaryKeys
		dictionaryKeys=objNavLinkDict.Items
		for i=0 to objNavLinkDict.count-1
			HTML = HTML & dictionaryKeys(i) & " &nbsp; | &nbsp; "
		next	
		'remove trailing spacer characters
		HTML=left(HTML,len(HTML)-10)
		'wrap in div tags
		HTML="<div class=""subnav"">" & HTML & "</div>"
		GenerateNav=HTML
     End Function


     '**************************************************
   End Class

%>