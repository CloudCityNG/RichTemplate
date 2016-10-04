<%@ LANGUAGE = "VBSCRIPT" %>
<% Option Explicit %>
<!--#include virtual="/CODEREUSE/FormData.Class.asp"-->
<%
    'Instantiate our class defined in FormData.Class.asp
    Dim objFormData
    Set objFormData = New FormData

    'Test to see if form inputs are valid
    If objFormData.ValidInputs() then
       'No form validation errors occurred!
       'Use Server.Transfer to send the user onto the proper form validation script
       'If the user didn't specify a redirect, raise an error
       If Len(Request("Redirect")) = 0 then
          Raise vbObjectError + 1010, "Validation Error", "Redirect not specified"
       Else
          Server.Transfer Request("Redirect")
       End If
    Else
      Response.Write objFormData.ErrorMessage()
    End if

    Set objFormData = Nothing
%>
