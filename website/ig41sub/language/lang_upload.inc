<%
'####################################
'## Application:   Blue-Collar Productions
'## File Name:     lang_users.inc (Language File)
'## File Version:  i-Gallery
'## Notice:        This code has limited warranties. Please see http://www.b-cp.com for details.
'####################################
%>

<%
'## upload.asp
CONST IGupld_warn1 		= "The \""File Attachment\"" field is required!  Please select an image attachment by clicking on the browse button."
CONST IGupld_warn2 		= "The \""File Type\"" you have requested is not allowed!  Supported file types are:"
CONST IGupld_upimg 		= "Upload Image"
CONST IGupld_rtn 		= "Return To Folder"
CONST IGupld_msg 		= "You are about to upload an image to the following directory in your photo gallery:"
CONST IGupld_but1 		= "Upload"
CONST IGupld_but2 		= "Reset"
CONST IGupld_aft 		= "Allowed File Types"
CONST IGupld_mus 		= "Maximum Upload Size"

'## blank.asp
CONST IGblnk_ferr1 		= "Folder Error"
CONST IGblnk_ferr2 		= "Folder Permission Error"
CONST IGblnk_ferr3 		= "The ADODB object <I>Stream</I> is not available in your server."
CONST IGblnk_ferrmsg 	= "The folder listed above does not exist.&nbsp; If you feel you have reached this page in error please contact the system administrator."
CONST IGblnk_write1 	= "The image folder you have identified in your system settings does not have <b>""Write""</b> permissions enabled.&nbsp; Correct the problem then try uploading again!"
CONST IGblnk_mod1 		= "Although the folder above has <b>""Write""</b> permission set properly it does have <b>""Modify""</b> permission.&nbsp; Check the value of your image folder which you identified in the system settings&nbsp; The image folder must have <b>""Read, Write &amp; Modify""</b> permissions.&nbsp; Correct the problem then try uploading again!"
CONST IGblnk_adodb1 	= "Check the Requirements page for information about upgrading your ADODB libraries."

'## upload-inc.asp
CONST IGupinc_ferr 		= "File Error"
CONST IGupinc_ferrmsg 	= "The file name specified in the upload form does not correspond to a valid file in your system.&nbsp; If you feel you have reached this page in error please contact the system administrator."

'## freeaspupload.asp
CONST IGfree_succ 		= "Image Uploaded Successfully!"
CONST IGfree_succmsg1 	= "The File"
CONST IGfree_succmsg2 	= "has been successfully uploaded to the server."
CONST IGfree_fail 	  	= "Image Failed To Upload!"
CONST IGfree_failmsg1 	= "The file"
CONST IGfree_failmsg2 	= "exceeded the maximum allowed upload size of"
CONST IGfree_failmsg3 	= "The upload process has been terminated."
CONST IGfree_rnamemsg1 	= "The file"
CONST IGfree_rnamemsg2 	= "was already present in the directory.&nbsp; To avoid erasing the existing image it was renamed"
CONST IGfree_fld1a 		= "Edit This Image"
CONST IGfree_fld1b 		= "Prior to uploading your next file... you might want to edit the size, title or description fields for this one?"
CONST IGfree_fld2a 		= "Sync This Image"
CONST IGfree_fld2b 		= "To make this file available in search queries use the link above to sync it with the i-Gallery database."
CONST IGfree_fld3a 		= "Upload Image"
CONST IGfree_fld3b 		= "Finished here?  Clicking on this link will reset the upload page so you can insert another file into this directory."
%>