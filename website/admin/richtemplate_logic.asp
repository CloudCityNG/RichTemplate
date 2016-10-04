


<!--#include file="db_connection.asp"-->

<!--#INCLUDE FILE="sessioncheck.asp"-->

<%
Sub JavaRedirect 
	nav = "/admin/richtemplate_list_sections.aspx"
    main="intranet/index.asp"
    %>
    <SCRIPT language="JavaScript">
    <!--
    
    top.basefrm.location 	= '<%=main%>';
    top.treeframe.location 	= '<%=nav%>';
   // parent.location.href = 
    //'<%=URL%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>

		

<%
'CREATE DATA CONNECTION

SET CON = SERVER.CREATEOBJECT("ADODB.CONNECTION")
CON.OPEN ConnectionString

'MAKE EDITS TO SECTION NAME----------------------------------->

IF REQUEST.QUERYSTRING("task")="edit" THEN

			WEBSECTIONSQL2 = "UPDATE WEBSECTION SET SECTIONNAME='" + REQUEST.FORM("websection") + "' WHERE ID=" + REQUEST.FORM("sectionid") + ""
			CON.EXECUTE (WEBSECTIONSQL2)
			
			varid = request.form("sectionid")
			
			WEBPAGESQL2 ="UPDATE WEBPAGE SET NAME='" + REQUEST.FORM("websection") + "' WHERE NAME='" & request.form("renamepage") & "' and SECTIONID=" & request.form("sectionid")
			CON.EXECUTE (WEBPAGESQL2)
			CON.CLOSE
			response.redirect ("richtemplate_list_pages.aspx?success=1&id=" + varid + "")

'DELETE ENTIRE SECTION ----------------------------------->


ELSEIF REQUEST.QUERYSTRING("task")="delete" THEN


WEBSECTIONDELETESQL = "DELETE FROM  WEBSECTION WHERE ID=" & REQUEST.QUERYSTRING("id") & ""
CON.EXECUTE (WEBSECTIONDELETESQL)

WEBPAGEDELETESQL = "DELETE FROM WEBPAGE WHERE SECTIONID=" & REQUEST.QUERYSTRING("id") & ""
CON.EXECUTE (WEBPAGEDELETESQL)

SUBPAGEDELETESQL = "DELETE FROM TERTIARYPAGE WHERE SECTIONID= "& REQUEST.QUERYSTRING("id") & ""
CON.EXECUTE (SUBPAGEDELETESQL)


Call JavaRedirect

CON.CLOSE
'response.redirect ("intranet/index.asp")

ELSE
'ADD SECTION------------------------------------------>

	IF request.form("websection")="" then
		CON.CLOSE
		response.redirect ("richtemplate_list_pages.aspx?error=1")

	ELSE
		WEBSECTION = REQUEST.FORM("WEBSECTION")

		WEBSECTIONSQL ="Select SECTIONNAME FROM WEBSECTION WHERE SECTIONNAME='"&websection&"'"
		SET RS2 = CON.EXECUTE (WEBSECTIONSQL)		

		IF RS2.EOF THEN
		
			WEBSECTIONSQL2 = "INSERT INTO WEBSECTION (SECTIONNAME) VALUES ('"&WEBSECTION&"')"
			CON.EXECUTE (WEBSECTIONSQL2)
			

			MYWEBSECTIONSQL = "SELECT ID FROM WEBSECTION WHERE SECTIONNAME='"&WEBSECTION&"'"
			'RESPONSE.WRITE "<BR>"&MYWEBSECTIONSQL&"<BR>" 
			SET RS3 = CON.EXECUTE (MYWEBSECTIONSQL)

			WEBPAGESQL1 = "INSERT INTO WEBPAGE (SECTIONID, NAME, DEFAULTPAGE, MESSAGE) VALUES ("&RS3("ID")&", '"&WEBSECTION&"', -1, '"&Request.Form("HTML")&"')"
			'RESPONSE.WRITE "<BR>"&WEBPAGESQL1&"<BR>" 
			CON.EXECUTE (WEBPAGESQL1)
			

		Call JavaRedirect2

			'response.redirect ("richtemplate_list_pages.aspx?sectionid="&RS3("ID")&"")	
	
			CON.CLOSE
		ELSE

			MYWEBSECTIONSQL = "SELECT * FROM WEBSECTION WHERE SECTIONNAME='"&WEBSECTION&"'"
			'response.write mywebsectionsql
			SET RS3 = CON.EXECUTE (MYWEBSECTIONSQL)
			'response.write ""&rs3("id")&""%>
			
<%
Sub JavaRedirect2
	nav = "/admin/richtemplate_list_sections.aspx"
    main="richtemplate_list_pages.aspx?sectionid="&RS3("ID")&""
    %>
    <SCRIPT language="JavaScript">
    <!--
    top.basefrm.location 	= '<%=main%>';
    top.treeframe.location 	= '<%=nav%>';
    //-->
    </SCRIPT>
    <%
    End Sub
%>	
			
<%

			Call JavaRedirect2


			'response.redirect ("richtemplate_list_pages.aspx?sectionid="&RS3("ID")&"")	
			CON.CLOSE
		END IF
'RS2.CLOSE
'CON.CLOSE

	END IF
END IF

%>