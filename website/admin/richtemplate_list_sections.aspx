<%@ Page Language="VB" AutoEventWireup="false" CodeFile="richtemplate_list_sections.aspx.vb"
    Inherits="admin_richtemplate_list_sections" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" href="styles.css" type="text/css" />
    <link rel="stylesheet" href="/admin/css/RichTemplate.css" type="text/css" />
    <link href="/Skins/RichTemplate/TreeView.RichTemplate.css" rel="stylesheet" type="text/css" />
</head>
<body class="nav_bg">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <asp:UpdatePanel ID="upListPages" runat="server">
        <ContentTemplate>
            <telerik:RadScriptBlock runat="server" ID="scriptBlock">

                <script type="text/javascript">
            <!--             
           function onRowDragStarted(sender,args) {
                var controlClassName = args._domEvent.target.className;
                if(controlClassName.indexOf("dndUpDown")<0)
                {
                    //Only allows drag 'n drop from the drag 'n drop column ONLY
                    args.set_cancel(true);
                }
            }

                    -->
                </script>
            </telerik:RadScriptBlock>
            <div id="divListSections" class="divSections" runat="server" visible="false">
                <div id="divAddSection" runat="server" class="divAddSection" visible="false">
                    <img border="0" src="images/icon_addpage.gif" width="16" height="16" />
                    <b><a id="anchorNewSection" runat="server" href="/admin/richtemplate_list_pages.aspx?sectionID=0"
                        target="basefrm" visible="false"><font color="#408FE3">
                            <%= Resources.RichTemplate_List_Sections.RichTemplate_List_Sections_NewSectionPublic%></font>
                    </a><a id="anchorNewSection_SecureMembers" runat="server" href="/admin/richtemplate_list_pages.aspx?sectionID=0&secure_members=yes"
                        target="basefrm" visible="false"><font color="#408FE3">
                            <%= Resources.RichTemplate_List_Sections.RichTemplate_List_Sections_NewSectionSecureMembers%></font>
                    </a><a id="anchorNewSection_SecureEducation" runat="server" href="/admin/richtemplate_list_pages.aspx?sectionID=0&secure_education=yes"
                        target="basefrm" visible="false"><font color="#408FE3">
                            <%= Resources.RichTemplate_List_Sections.RichTemplate_List_Sections_NewSectionSecureEducation%></font>
                    </a></b>
                </div>
                <div id="divSectionTree" runat="server" class="divSectionTree">
                    <div>
                        <img src="/images/open_folder_full_sm.png" class="rtImg rtImg_root img_link_left"
                            alt="My Website"><span class="inner_rtIn rt_root img_link_left" style="padding-top: 2px;"><a
                                href="/" target="basefrm"><font color="#3054A7"><b>&nbsp;<%= Resources.RichTemplate_List_Sections.RichTemplate_List_Sections_MyWebsite%></b></font></a></span>
                        <br class="clear_both" />
                    </div>
                    <telerik:RadTreeView ID="RadTreeSections" runat="server" EnableDragAndDrop="false"
                        OnNodeDrop="RadTreeSections_NodeDrop" EnableDragAndDropBetweenNodes="true" LoadingMessage="<%$ Resources:RichTemplate_List_Sections, RichTemplate_List_Sections_Grid_LoadingMessage %>"
                        OnClientNodeDragStart="onRowDragStarted" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
