<%
   Class FormData
     '****************** PROPERTIES ********************
     Private colRequestCol
     Private objRegExp
     Private objErrorDict
     '**************************************************


     '************** INITIALIZE/TERMINATE **************
     Private Sub Class_Initialize()
        'Determine what Request collection to use
        If Ucase(Request("Collection")) = "QUERYSTRING" then
          Set colRequestCol = Request.QueryString
        Else
          Set colRequestCol = Request.Form
        End If

        'Instantiate a regexp object
        Set objRegExp = New regexp
        objRegExp.IgnoreCase = True
        objRegExp.Global = True

        'Instantiate the error log
        Set objErrorDict = CreateObject("Scripting.Dictionary")
     End Sub

     Private Sub Class_Terminate()
        Set colRequestCol = Nothing
        Set objRegExp = Nothing
        Set objErrorDict = Nothing
     End Sub
     '**************************************************


     '************* PROPERTY GET STATMENT **************
     Public Property Get ErrorLog()
       'Return the Error Log Dictionary Object
       Set ErrorLog = objErrorDict
     End Property
     '**************************************************


     '********************* METHODS ********************
     Public Function ValidInputs()
       'Checks to see if data is valid.  If it is, returns
       'True, else returns False.  A list of errors can be
       'obtained through PrintErrors

       Dim strItem

       'Iterate through each of the form field elements
       For Each strItem in colRequestCol
         'See if there is an exclamation point.  If there is,
         'then we need to perform form validation
         If InStr(1,strItem,"!") > 0 then
           'Grab the regular expression pattern
           objRegExp.Pattern = Mid(strItem, InStr(1,strItem,"!") + 1, Len(strItem))
           If Not objRegExp.Test(colRequestCol(strItem)) then
             'Input invalid!  Append to the error log
             objErrorDict.Add EnglishName(strItem), colRequestCol(strItem).Item
           End If
         End If
       Next

       'Did we encounter any errors?
       If objErrorDict.Count > 0 then
         ValidInputs = False
       Else
         ValidInputs = True
       End If
     End Function


     Private Function EnglishName(ByVal str)
       'Check to see if there is an exclamation point: if so, hack off
       'all contents of the string to the right of the exclamation point
       If InStr(1,str,"!") > 0 then
         str = Left(str,InStr(1,str,"!") - 1)
       End If

       EnglishName = Replace(str, "_", " ")
     End Function


     Public Function ErrorMessage()
       'Returns a string containing an error message
       If objErrorDict.Count = 0 then
          ErrorMessage = "There were no validation errors."
       Else
          Dim strName
          For Each strName in objErrorDict
            ErrorMessage = ErrorMessage & "Error in " & strName & _
                " - the entry " & objErrorDict(strName) & _
                " is invalid." & vbCrLf
          Next
       End If
     End Function
     '**************************************************
   End Class
%>
