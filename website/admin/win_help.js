function HelpWindow (file,window)
{
    msgWindow=open(file + '&hf=user&ht=' + document.Help.HelpURL.value,window,'toolbar=no,location=no,directories=no,status=yes,menubar=no,resizable=yes,scrollbars=yes,width=650,height=600');
    if (msgWindow.opener == null) msgWindow.opener = self;
}     
