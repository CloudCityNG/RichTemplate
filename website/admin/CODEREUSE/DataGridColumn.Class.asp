<% 
'This class represents column data and properties for the caDataGrid.Class.asp class.
'Properties are assigned to a column here, and then the object that this class creates is added to the collection of columns in the data grid object.


Class caDataGridColumn

'Private, class member variable
Private m_Header
Private m_HeaderCssClass
Private m_DataCellFormat
Private m_DataCellCssClass
Private m_Sortable
Private m_SortField
Private m_DataCellCssAlternatingClass
Private m_ColumnWidth

'Runs when class is first called
Sub Class_Initialize()
	'set default values
	m_Sortable=false
	m_SortField=""
	m_Header=""
	m_HeaderCssClass=""
	m_DataCellFormat=""
	m_DataCellCssClass=""
	m_DataCellCssAlternatingClass=""
	m_ColumnWidth=""
End Sub

Public Sub SayHello()

	response.write "hello"
end sub

'Runs when class is set to nothing
Sub Class_Terminate()
	'no objects to throw away
End Sub

'Set the properties.
'These property names are exposed when you instantiate the class. 
'The private member values above are set through these Let statements
   Public Property Let Header(strString)
       m_Header = strString
   End Property

   Public Property Let HeaderCssClass(strString)
		m_HeaderCssClass=strString
   End Property
   
   Public Property Let DataCellFormat(strString)
   		m_DataCellFormat=strString
   	End Property
   	
   	Public Property Let DataCellCssClass(strString)
   		m_DataCellCssClass=strString
   	End Property
   	
   	Public Property Let SortField(strString)
   		m_SortField=strString
   	End Property
   	
   	Public Property Let Sortable(bSortable)
   	   'set value only if a boolean was passed in
       If bSortable= True or bSortable= False then
           m_Sortable= bSortable
       End IF
   End Property

	Public Property Let DataCellCssAlternatingClass(strString)
		m_DataCellCssAlternatingClass=strString
	End Property

	Public Property Let ColumnWidth(strString)
		m_ColumnWidth=strString
	End Property

'Get Statements - These allow the properties to be read out as needed
     Public Property Get Header()
        Header= m_Header
     End Property

     Public Property Get HeaderCssClass()
        HeaderCssClass= m_HeaderCssClass
     End Property

     Public Property Get DataCellFormat()
        DataCellFormat= m_DataCellFormat
     End Property

     Public Property Get DataCellCssClass()
        DataCellCssClass= m_DataCellCssClass
     End Property
     
     Public Property Get Sortable()
        Sortable= m_Sortable
     End Property
     
     Public Property Get SortField()
    	SortField=m_SortField
    End Property
    
    Public Property Get DataCellCssAlternatingClass()
    	DataCellCssAlternatingClass=m_DataCellCssAlternatingClass
    End Property
    
    Public Property Get ColumnWidth()
    	ColumnWidth=m_ColumnWidth
    End Property


'Methods
'This class is mostly a holder for properties. It only has a clear method.

     Public Sub Clear()
        m_Header = ""
        m_HeaderCssClass = ""
        m_DataCellFormat = ""
        m_DataCellCssClass = ""
        m_DataCellCssAlternatingClass=""
        m_Sortable = False	'stays false because column is being resused, so null would be bad if user forgets to set it
        m_SortField=""
        m_ColumnWidth=""
     End Sub

End Class

%>