      <!-- Start Of Moreover.com News Javascript Code -->
  
<head>
  
    <style type='text/css'>
	
	.morehl {
		font-family: Verdana, geneva, ariel, san-serif !important;
		font-size: 10px !important;
		color: #2CDB505 !important;
		font-weight: bold !important;
		font-style: normal !important;
		text-decoration: none !important;
	}	
	
	A:link.morehl, A:vlink.morehl, A:alink.morehl {
		color: #5C85AD !important;
	}
	
	.moresrc {
		font-family: Verdana, geneva, ariel, san-serif !important;
		font-size: 9px !important;
		color: #333333 !important;
		font-weight: normal !important;
		font-style: normal !important;
		text-decoration: none !important;
	}
	
	A:link.moresrc, A:vlink.moresrc, A:alink.moresrc {
		color: #333333 !important;
	}
	
	.moreti {
		font-family: Verdana, geneva, ariel, san-serif;
		font-size: 9px;
		color: #333333;
		font-weight: normal;
		font-style: normal;
		text-decoration: none;
	}
	
	.morehlt {
		font-family: Verdana, geneva, ariel, san-serif;
		font-size: 10px;
		color: #E4E7E6 !important;
		font-weight: bold;
		font-style: normal;
		text-decoration: none;
	}	
	</style>
  
</head>
  
  
  <!-- Start Of Moreover.com News Javascript Code -->
  
  <SCRIPT LANGUAGE="Javascript">
  <!--
  // the array global_article is used to allow multiple categories
  var global_article = new Array();
  var global_article_counter = 0;
  var article = null;
  var early_exit = true;
  var moreover_text = 0;
  
  function load_wizard()
    {
    
    var newwin = window.open("","clone","resizable,scrollbars");
    document.forms.moreover_clone.submit();
    return true;
    }
  // -->
  
  </SCRIPT>
  <SCRIPT LANGUAGE="JavaScript" src="http://p.moreover.com/cgi-local/page?query=baseball%20football%20basketball%20soccer%20nascar%20boxing&n=5&o=js">
  </SCRIPT>
  <SCRIPT LANGUAGE="Javascript">
  <!--
  // load global_article array with articles from category
  if (article != null && (article.length > 0))
    {
    early_exit = false;
    for (var article_counter = 0; article_counter < article.length; article_counter++)
      {
      global_article[global_article_counter] = article[article_counter];
      global_article[global_article_counter].url += "&w=2003155";
      
      global_article[global_article_counter].url += "' TARGET='_blank";
      global_article[global_article_counter].document_url += "' TARGET='_blank";
      global_article_counter++;
      }
    }
  // -->
  </SCRIPT>
  <SCRIPT LANGUAGE="Javascript">
  <!--
  if (global_article.length == 0)
      {
      if (early_exit)
        {
        document.write("<CENTER>Please reload this page to view the headlines</CENTER>");
        }
      else
        {
        document.write("<CENTER>Sorry, no articles matched your search criteria</CENTER>");
        early_exit = true;
        }
      }
  // -->
  </SCRIPT>
  <table cellpadding=4 cellspacing=0 border=0 width='307'><tr bgcolor='#ffffff'><td>
  
  
  <SCRIPT LANGUAGE="Javascript">
  <!--
  if (!early_exit)
  {
  var wizard_brand			= "moreover";
  var webfeed_heading 			= "";
  var width 				= "300";
  var numberofarticles 			=  global_article.length;
  var cluster_border 			= "0";
  var time_display 			= "Yes";
  var cell_spacing 			= "0";
  var cell_padding 			= "1";
  var time 					=  new Array(global_article.length);
 
  
  // Start loop for articles
  for (var counter=0; counter < numberofarticles; counter++)
    {
	
	if ((counter == (global_article.length - 1)) && moreover_text == 1) 
      {  
      time_display = "No";
      }
	
	// Print out the headline
    document.write("<a href='"+global_article[counter].url+"' class='morehl'>");
    document.write(global_article[counter].headline_text+"...</a><br>");
	
    
      // Print out the source
        if ((counter != (global_article.length - 1)) || moreover_text != 1)
        {
		
		document.write("<A HREF='"+global_article[counter].document_url+"' + class='moresrc'>");
        document.write(global_article[counter].source+"</A>");
        
		}
    
	
    // Print out reg/sub/prem if appropriate
    if (global_article[counter].access_status == "sub" || global_article[counter].access_status == "reg" || global_article[counter].access_status == "prem")
      {
      document.write("<span class='moreti'>&nbsp;</span><A HREF='"+global_article[counter].access_registration+"' class='moresrc'>");
 	  document.write(global_article[counter].access_status+"</A>");
      }
	  
    // Print out the harvest time
    if (time_display == "Yes")
      {
      // Make a new date object
      time[counter] = new Date(global_article[counter].harvest_time);
      time[counter].setHours(time[counter].getHours() - (time[counter].getTimezoneOffset() / 60 ));
	  document.write("<span class='moreti'> &nbsp;"+time[counter].toString()+"<br>&nbsp;<br></span>");
      } 

    } // End of article loop
  

  // Start of clone button code //
  // NOTE: DO NOT REMOVE any of the code in this section //
  document.write("<FORM METHOD='POST' ACTION='http://www.moreover.com/home1/apache/hosts/moreover/WWWDocs/cgi-local/wizard_welcome.pl' target='clone' name='moreover_clone'>");
  document.write("<INPUT TYPE='hidden' NAME='parent_code' VALUE='2003155'>");
  document.write("<INPUT TYPE='hidden' NAME='clone_system' VALUE='c'>");
  document.write("<INPUT TYPE='hidden' NAME='system' VALUE='c'>");
  document.write("<INPUT TYPE='hidden' NAME='heading_font_size' VALUE=''><INPUT TYPE='hidden' NAME='headline_bgcolor' VALUE='D4E5E5'>")
  document.write("<INPUT TYPE='hidden' NAME='webfeed_heading' VALUE=''><INPUT TYPE='hidden' NAME='cluster_width' VALUE='300'>")
  document.write("<INPUT TYPE='hidden' NAME='cluster_name' VALUE='query=baseball%20football%20basketball%20soccer%20nascar%20boxing&o=js'><INPUT TYPE='hidden' NAME='source_time_underline' VALUE='false'>")
  document.write("<INPUT TYPE='hidden' NAME='time_display' VALUE='Yes'><INPUT TYPE='hidden' NAME='heading_display' VALUE='Yes'>")
  document.write("<INPUT TYPE='hidden' NAME='headline_fgcolor' VALUE='2B6D73'><INPUT TYPE='hidden' NAME='cluster_cellspacing' VALUE='0'>")
  document.write("<INPUT TYPE='hidden' NAME='number_of_headlines' VALUE='5'><INPUT TYPE='hidden' NAME='source_time_fgcolor' VALUE='333333'>")
  document.write("<INPUT TYPE='hidden' NAME='headline_font_size' VALUE='10'><INPUT TYPE='hidden' NAME='search_keywords' VALUE='baseball football basketball soccer nascar boxing'>")
  document.write("<INPUT TYPE='hidden' NAME='source_time_font_size' VALUE='9'><INPUT TYPE='hidden' NAME='headline_italic' VALUE='false'>")
  document.write("<INPUT TYPE='hidden' NAME='headline_font' VALUE='Verdana, geneva, ariel, san-serif'><INPUT TYPE='hidden' NAME='source_time_italic' VALUE='false'>")
  document.write("<INPUT TYPE='hidden' NAME='source_time_font' VALUE='Verdana, geneva, ariel, san-serif'><INPUT TYPE='hidden' NAME='wizard_brand' VALUE='moreover'>")
  document.write("<INPUT TYPE='hidden' NAME='cluster_cellpadding' VALUE='1'><INPUT TYPE='hidden' NAME='headline_bold' VALUE='true'>")
  document.write("<INPUT TYPE='hidden' NAME='cluster_border' VALUE='0'><INPUT TYPE='hidden' NAME='source_time_bold' VALUE='false'>")
  document.write("<INPUT TYPE='hidden' NAME='headline_underline' VALUE='true'><INPUT TYPE='hidden' NAME='cluster_layout' VALUE='<BR>'>")

  document.write("</FORM>");
  // End of clone button code //

  // ************************************************************************************
  // This code is subject to the copyright and warranty restrictions detailed at 
  // http://www.moreover.com/wizard_copyright.html
  // Copyright 2000 Moreover.com Inc. All rights reserved.
  // *************************************************************************************
  } 
  // -->
  </SCRIPT>
<SCRIPT LANGUAGE="Javascript" SRC="http://p.moreover.com/dr/wizard/wizard_text.js"></SCRIPT>

  </td></tr></table>
  <!-- End Of Moreover.com News Javascript Code -->
 

  <!-- End Of Moreover.com News Javascript Code -->

