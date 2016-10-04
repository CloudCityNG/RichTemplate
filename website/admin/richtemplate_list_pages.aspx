<%@ Page Language="VB" AutoEventWireup="false" CodeFile="richtemplate_list_pages.aspx.vb"
    Inherits="admin_richtemplate_list_pages" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script language="JavaScript" src="deletefunction.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/multiplePageLoadFunctions.js"></script>
    <script language="JavaScript" src="/js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script language="JavaScript">
<!--        Begin
        function popUpp(URL) {
            day = new Date();
            id = day.getTime();
            eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=no,location=0,statusbar=0,menubar=0,resizable=0,width=500,height=500,left = 100,top = 100');");
        }
// End -->
    </script>
    <script type="text/javascript" language="JavaScript">
<!--        Begin



        function popUpp2(URL) {
            day = new Date();
            id = day.getTime();
            eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=yes,location=0,statusbar=0,menubar=0,resizable=0,width=500,height=500,left = 100,top = 100');");
        }


// End -->
    </script>
    <link rel="stylesheet" href="styles.css" type="text/css" />
    <link rel="stylesheet" href="/admin/css/RichTemplate.css" type="text/css" />
    <link href="/Skins/RichTemplate/TreeView.RichTemplate.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <asp:UpdatePanel ID="upListPages" runat="server">
        <contenttemplate>
            <telerik:RadScriptBlock runat="server" ID="scriptBlock">
                <script type="text/javascript">
            <!--

                    $(function () {
                        $(".tblWebInfos tr").hover(
   function () {
       $(this).addClass("highlight");
   },
   function () {
       $(this).removeClass("highlight");
   }
  )
                    }
)
                    function onRowDragStarted(sender, args) {
                        var controlClassName = args._domEvent.target.className;
                        if (controlClassName.indexOf("dndUpDown") < 0) {
                            //Only allows drag 'n drop from the drag 'n drop column ONLY
                            //args.set_cancel(true);
                        }
                    }

                    function onRowDragging(sender, args) {
                        var div = sender._draggingClue;
                        div.className = "divDragging";
                    }

                    -->
                </script>
            </telerik:RadScriptBlock>
            <uc:Header ID="ucHeader" runat="server" />
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td valign="top" align="center" width="100%">
                        <table border="0" width="100%" cellspacing="0" cellpadding="0" background="images/fadeback3.gif">
                            <tr>
                                <td>
                                    <table border="0" cellspacing="0" cellpadding="0" id="table1">
                                        <tr>
                                            <td class="bodybold" height="28">
                                                <font color="#FFFFFF"><b>&nbsp;<%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_SectionLabel %>:
                                                    <asp:Literal ID="lit_SectionName" runat="server" />&nbsp;</font>
                                                <asp:LinkButton ID="lnkCheckInAll" runat="server" Visible="false">
                                                        <font color="#FFFFFF"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_CheckInAllPages %></font></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <div id="divListPages" runat="server" align="center">
                            <table border="0" width="100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <div id="divPagesTree" class="divPagesTree">
                                            <div class='divItem'>
                                                <div class="lft_col_list">
                                                    <div class="dndUpDown_head">
                                                        &nbsp;</div>
                                                    <div class="webpage_head">
                                                        <b><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_WebpageName%></b>
                                                    </div>
                                                </div>
                                                <div class="ryt_col_list">
                                                    <div class="ryt_col_ctr">
                                                        <div class="vAlign">
                                                            <b><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_Archive%></b></div>
                                                    </div>
                                                    <div class="ryt_col_ctr">
                                                        <div class="vAlign">
                                                            <b><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_Live%></b></div>
                                                    </div>
                                                    <div class="ryt_col_ctr">
                                                        <div class="vAlign">
                                                            <b><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_Draft%></b></div>
                                                    </div>
                                                    <div class="ryt_col_ctr">
                                                        <div class="vAlign">
                                                            <b><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_CheckedInOut%></b></div>
                                                    </div>
                                                    <div class="ryt_col_lft">
                                                        <div class="vAlign">
                                                            <b><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_CheckedAuthor%></b></div>
                                                    </div>
                                                    <div class="ryt_col_ctr">
                                                        <div class="vAlign">
                                                            <b><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_AddSubPageHeading%></b></div>
                                                    </div>
                                                    <div class="ryt_col_ctr">
                                                        <div class="vAlign">
                                                            <b><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_DeleteHeading%></b></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style='clear: both;'>
                                            </div>
                                            <telerik:RadTreeView ID="RadTreePages" runat="server" EnableDragAndDrop="false" OnNodeDrop="RadTreePages_NodeDrop"
                                                OnClientNodeDragging="onRowDragging" EnableDragAndDropBetweenNodes="true" LoadingMessage="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_Grid_LoadingMessage %>"
                                                OnClientNodeDragStart="onRowDragStarted" DataFieldID="ID" DataFieldParentID="ParentID"
                                                DataTextField="Name" DataValueField="ID">
                                                <NodeTemplate>
                                                    <div id="divItem" runat="server" class="divItem">
                                                        <div id="divDragAndDropTreeNodeColumnEnabled" runat="server" visible="false" class="dndUpDown">
                                                            &nbsp;</div>
                                                        <div id="divDragAndDropTreeNodeColumnDisabled" runat="server" visible="false" class="dndUpDownDisabled">
                                                            &nbsp;</div>
                                                        <div class="lft_col_list">
                                                            <asp:Literal ID="lit_Spacer" runat="server" />
                                                            <div class="webpageIcon">
                                                                <div class="vAlign">
                                                                    <img id="img_WebpageIcon" runat="server" alt="webpage" />
                                                                </div>
                                                            </div>
                                                            <div class="webpage">
                                                                <div class="vAlign">
                                                                    <asp:Literal ID="lit_WebpageName" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="ryt_col_list" style="height: 30px;">
                                                            <div class="ryt_col_ctr">
                                                                <div id="divMakeLiveEnabled" runat="server" visible="false">
                                                                    <asp:LinkButton ID="lnkMakeLive" runat="server"  CssClass="vAlign"
                                                                        OnClick="lnkTakeOfflineOrMakeLive_Click" CausesValidation="false"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_MakeLive%></asp:LinkButton>
                                                                </div> 
                                                                <div id="divMakeLiveDisabled" runat="server" class="vAlign" visible="false">
                                                                    <font color="#808080"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_MakeLive%></font>
                                                                </div>
                                                                <div id="divTakeOfflineEnabled" runat="server" visible="false">
                                                                <asp:LinkButton ID="lnkTakeOffline" runat="server" CssClass="vAlign"
                                                                    OnClick="lnkTakeOfflineOrMakeLive_Click" CausesValidation="false"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_TakeOffline%></asp:LinkButton>
                                                                </div>
                                                                <div id="divTakeOfflineDisabled" runat="server" class="vAlign" visible="false">
                                                                    <font color="#808080"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_TakeOffline%></font>
                                                                </div>
                                                                &nbsp;
                                                            </div>
                                                            <div class="ryt_col_ctr">

                                                                <div id="divEditLiveEnabled" runat="server" class="vAlign" visible="false">
                                                                    <a title="Edit Live Content" href='/admin/richtemplate_page_editor.aspx?pageID=<%# DataBinder.Eval(Container.DataItem, "ID")%>'>
                                                                        <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_EditPage%></a>
                                                                </div>
                                                                <div id="divEditLiveDisabled" runat="server" class="vAlign" visible="false">
                                                                    <font color="#808080"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_EditPage%></font></div>
                                                                <div id="divEditLiveEnabled_Link" runat="server" class="vAlign" visible="false">
                                                                    <asp:LinkButton ID="lnkEditLiveEnabled_Link" runat="server" OnClick="lnkEditLiveEnabled_Link_Click" CausesValidation="false"
                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")%>'><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_EditLink%></asp:LinkButton>
                                                                </div>
                                                                <div id="divEditLiveDisabled_Link" runat="server" class="vAlign" visible="false">
                                                                    <font color="#808080"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_EditLink%></font></div>
                                                                &nbsp;
                                                            </div>
                                                            <div class="ryt_col_ctr">
                                                                <div id="divEditDraftDisabled" runat="server" class="vAlign" visible="false">
                                                                    <font color="#808080"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_EditPage%></font></div>
                                                                <div id="divEditDraftEnabled" runat="server" class="vAlign" visible="false">
                                                                    <a title="Edit Offline Content" href='/admin/richtemplate_page_editor.aspx?pageID=<%# DataBinder.Eval(Container.DataItem, "ID")%>&pageStatus=offline'>
                                                                        <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_EditPage%></a>
                                                                </div>
                                                                &nbsp;
                                                            </div>
                                                            <div class="ryt_col_ctr">
                                                                <asp:LinkButton ID="lnkCheckIn" runat="server" Visible="false" CssClass="vAlign"
                                                                    OnClick="lnkCheckIn_Click" CausesValidation="false"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_CheckInLink%></asp:LinkButton>
                                                                <div id="divCheckOut_DifferentUser" runat="server" class="vAlign" visible="false">
                                                                    <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_CheckOutLabel%></div>
                                                                <div id="divCheckIn" runat="server" class="vAlign" visible="false">
                                                                    <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_CheckInLabel%></div>
                                                                &nbsp;
                                                            </div>
                                                            <div class="ryt_col_lft">
                                                                <div class="vAlign">
                                                                    <asp:Literal ID="lit_CheckedOutAuthor" runat="server" Visible="false" />&nbsp;
                                                                </div>
                                                            </div>
                                                            <div class="ryt_col_ctr">
                                                                <div id="divAddSubPageEnabled" runat="server" class="vAlign" visible="false">
                                                                    <asp:LinkButton ID="lnkAddPage" runat="server" Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_Grid_AddSubPageLink %>" OnClick="lnkAddPage_Click" CausesValidation="false" />
                                                                </div>
                                                                <div id="divAddSubPageDisabled" runat="server" class="vAlign" visible="false">
                                                                    <font color="#808080"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_AddSubPageLabel%></font>
                                                                </div>
                                                                &nbsp;
                                                            </div>
                                                            <div class="ryt_col_ctr">
                                                                <div id="divDeleteEnabled_Section" runat="server" class="vAlign" visible="false">
                                                                    <asp:LinkButton ID="lnkDeleteSection" runat="server" OnClick="lnkDeleteSection_Click" CausesValidation="false"/>
                                                                </div>
                                                                <div id="divDeleteEnabled_Page" runat="server" class="vAlign" visible="false">
                                                                    <asp:LinkButton ID="lnkDeletePage" runat="server" OnClick="lnkDeletePage_Click" CausesValidation="false" />
                                                                </div>
                                                                <div id="divDeleteDisabled" runat="server" class="vAlign" visible="false">
                                                                    <font color="#808080"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_Grid_DeleteLabel%></font>
                                                                </div>
                                                                &nbsp;
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="cBoth">
                                                    </div>
                                                </NodeTemplate>
                                            </telerik:RadTreeView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </contenttemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="up_EditAddPage" runat="server">
        <contenttemplate>
            <asp:Panel ID="pnl_EditAddPage" runat="server" Visible="false" class="pnlEditAddPage">
                <asp:LinkButton ID="lnk_DummyEditAddPage" runat="server" Text="" />
                <telerik:RadToolTip ID="rtt_EditAddPage" runat="server" RelativeTo="BrowserWindow" ManualClose="true"
                    Sticky="true" TargetControlID="lnk_DummyEditAddPage" EnableAjaxSkinRendering="true"
                    Animation="None" Position="TopCenter" HideEvent="ManualClose" Modal="true" RenderInPageRoot="true"
                    Width="500px" Height="300px" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
                    <asp:UpdatePanel ID="up_EditAddPageInner" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdn_WebInfoID" runat="server" />
                            <asp:HiddenField ID="hdn_WebInfoParentID" runat="server" />
                            <table class="RadToolTipTable">
                                <tr class="ModalTableRowHeading">
                                    <td>
                                        <asp:LinkButton ID="lnkClose" runat="server" CssClass="img_link_right" CausesValidation="false"><img src="/admin/images/close_button.jpg" alt="Close"/></asp:LinkButton>
                                        <asp:Literal ID="litEditAddHeading" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ModalTableRowData" style="background-color:#F4F4F4;">
                                        <div id="div_ScrollerEditAddPage"  class="PopupCellInnerContentScroller">
                                            <div id="divStepOne" runat="server">
                                            <table>
                                                <tr>
                                                    <td style="width: 450px;">
                                                    <div id="divCreatePageWYSIWYG" runat="server">
                                                        <table border="0" width="100%" id="table5">
                                                            <tr>
                                                                <td class="bodybold" width="1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" width="3%">
                                                                    <img border="0" src="images/icon_link1.gif">
                                                                </td>
                                                                <td class="bodybold" height="30" style="padding: 2px;">
                                                                    <a id="lnkPageEditor" runat="server"><font size="2" color="#3054A9">
                                                                    <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_CreateWithWYSIWYG%></font> </a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" width="100%" id="table4" bgcolor="#F4F4F4">
                                                            <tr>
                                                                <td class="bodybold" width="1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" width="3%">
                                                                    <img border="0" src="images/icon_link1.gif">
                                                                </td>
                                                                <td class="bodybold" colspan="2" height="30" style="padding: 3px;">
                                                                    <font size="2" color="#3054A9"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_CreateOutsidePage%></font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="bodybold" width="1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" width="3%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" style="vertical-align: middle;">
                                                                    <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_Target%>:
                                                                </td>
                                                                <td width="76%">
                                                                    <asp:DropDownList ID="ddlTargetFrame_OutsidePage" runat="server">
                                                                        <asp:ListItem Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_Target_SameWindow %>" Value="" />
                                                                        <asp:ListItem Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_Target_NewWindow %>" Value="_blank" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="bodybold" width="1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" width="3%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" style="vertical-align: middle;">
                                                                    <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_CreateOutsidePage_LinkName%>:
                                                                </td>
                                                                <td width="76%" class="body">
                                                                    <asp:TextBox ID="txtLinkName" runat="server" Width="250" />
                                                                    <asp:RequiredFieldValidator ID="reqLinkName" runat="server" ErrorMessage=" Required"
                                                                        ControlToValidate="txtLinkName" CssClass="errorStyle" Display="Dynamic" ValidationGroup="OutsideWebPage" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="bodybold" width="1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" width="3%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" style="vertical-align: middle;">
                                                                    <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_CreateOutsidePage_LinkUrl%>:
                                                                </td>
                                                                <td width="76%" class="body">
                                                                    <asp:TextBox ID="txtLinkURL_OutsidePage" runat="server" Width="250" />
                                                                    <asp:RequiredFieldValidator ID="reqLinkURL" runat="server" ErrorMessage=" <%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_RequiredMessage %>"
                                                                        ControlToValidate="txtLinkURL_OutsidePage" CssClass="errorStyle" Display="Dynamic"
                                                                        ValidationGroup="OutsideWebPage" />
                                                                        <div class="bodybold">(http://www.richtemplate.com)</div>
                                                                </td>
                                                            </tr>
                                                            <tr id="trNavigationLayout_OutsidePage" runat="server" visible="false">
                                                                <td class="bodybold" width="1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" width="3%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" style="vertical-align: middle;">
                                                                    <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_NavigationLayout%>:
                                                                </td>
                                                                <td width="76%" class="body">
                                                                    <asp:RadioButtonList ID="rdNavigationLayout_OutsidePage" runat="server">
                                                                        <asp:ListItem Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_NavigationLayout_Column1 %>" Value="1" />
                                                                        <asp:ListItem Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_NavigationLayout_Column2 %>" Value="2" />
                                                                        <asp:ListItem Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_NavigationLayout_Column3 %>" Value="3" />
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="20" class="bodybold">
                                                                    &nbsp;
                                                                </td>
                                                                <td height="20" class="bodybold">
                                                                    &nbsp;
                                                                </td>
                                                                <td height="20" class="bodybold">
                                                                    &nbsp;
                                                                </td>
                                                                <td height="20" class="bodybold" style="padding-top: 10px;">
                                                                    <div>
                                                                        <asp:Button ID="btnCreatePage_OutsidePage" runat="server" Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_CreateOutsidePage_ButtonCreate %>"
                                                                        ValidationGroup="OutsideWebPage" Visible="false" />
                                                                        <asp:Button ID="btnUpdatePage_OutsidePage" runat="server" Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_CreateOutsidePage_ButtonUpdate %>" ValidationGroup="OutsideWebPage"
                                                                        Visible="false" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" width="100%" id="table2" bgcolor="#F4F4F4">
                                                            <tr>
                                                                <td class="bodybold" width="1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" width="3%">
                                                                    <img border="0" src="images/icon_link1.gif">
                                                                </td>
                                                                <td class="bodybold" colspan="2" height="30" style="padding: 3px;">
                                                                    <font size="2" color="#3054A9"><%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_CreateInternalPage%></font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="bodybold" width="1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" width="3%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" style="vertical-align: middle;">
                                                                    <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_Target%>
                                                                </td>
                                                                <td width="76%">
                                                                    <asp:DropDownList ID="ddlTargetFrame_InsidePage" runat="server">
                                                                        <asp:ListItem Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_Target_SameWindow %>" Value="" />
                                                                        <asp:ListItem Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_Target_NewWindow %>" Value="_blank" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr id="trNavigationLayout_InsidePage" runat="server" visible="false">
                                                                <td class="bodybold" width="1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" width="3%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" style="vertical-align: middle;">
                                                                    <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_NavigationLayout%>
                                                                </td>
                                                                <td width="76%" class="body">
                                                                    <asp:RadioButtonList ID="rdNavigationLayout_InsidePage" runat="server">
                                                                        <asp:ListItem Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_NavigationLayout_Column1 %>" Value="1" />
                                                                        <asp:ListItem Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_NavigationLayout_Column2 %>" Value="2" />
                                                                        <asp:ListItem Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_NavigationLayout_Column3 %>" Value="3" />
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="bodybold" width="1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" width="3%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" style="vertical-align: middle;">
                                                                    <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_CreateInternalPage_SelectPage%>:
                                                                </td>
                                                                <td width="76%" class="body">
                                                                    <asp:DropDownList ID="ddlSelectPage" runat="server" CssClass="ddlSelectPage" Width="255" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage=" <%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_RequiredMessage %>"
                                                                        ControlToValidate="ddlSelectPage" InitialValue="" CssClass="errorStyle" Display="Dynamic"
                                                                        ValidationGroup="InsideWebPage" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="bodybold" width="1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" width="3%">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="bodybold" style="vertical-align: middle;">
                                                                    <%= Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepOne_CreateInternalPage_ExistingUrl%>:
                                                                </td>
                                                                <td width="76%" class="body">
                                                                    <b>
                                                                        <asp:Label ID="lblLinkURL_InsidePage" runat="server" CssClass="lblLinkURL_InsidePage" /></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="20" class="bodybold">
                                                                    &nbsp;
                                                                </td>
                                                                <td height="20" class="bodybold">
                                                                    &nbsp;
                                                                </td>
                                                                <td height="20" class="bodybold">
                                                                    &nbsp;
                                                                </td>
                                                                <td height="20" class="bodybold" style="padding-top: 10px;">
                                                                    <div>
                                                                        <asp:Button ID="btnCreatePage_InsidePage" runat="server" Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_CreateInternalPage_ButtonCreate %>" ValidationGroup="InsideWebPage"
                                                                            Visible="false" />
                                                                        <asp:Button ID="btnUpdatePage_InsidePage" runat="server" Text="<%$ Resources:RichTemplate_List_Pages, RichTemplate_List_Pages_AddEditPage_StepOne_CreateInternalPage_ButtonUpdate %>" ValidationGroup="InsideWebPage"
                                                                            Visible="false" />
                                                                    </div>    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            </div>
                                            <div id="divStepTwo" runat="server" style="max-height:460px;overflow-y:auto">
                                                <table border="0" width="100%" id="table3" bgcolor="#F4F4F4">
                                                    <tr>
                                                        <td class="bodybold" width="1%">
                                                            &nbsp;
                                                        </td>
                                                        <td class="bodybold" width="3%">
                                                            <img border="0" src="images/icon_link1.gif">
                                                        </td>
                                                        <td class="bodybold" colspan="2" height="30" style="padding: 3px;">
                                                            <font size="2" color="#3054A9"><asp:Literal ID="litStepTwoHeading" runat="server" /></font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <span class="moduleLabel">
                                                                <%=Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_UserGroups_Groups%></span>
                                                            <asp:CheckBoxList ID="cblGroupList" runat="server">
                                                            </asp:CheckBoxList>
                                                            <br />
                                                            <div id="divUserGroups_Members" runat="server" visible="false">
                                                            <span class="moduleLabel">
                                                                <%=Resources.RichTemplate_List_Pages.RichTemplate_List_Pages_AddEditPage_StepTwo_UserGroups_Users%></span>
                                                            <asp:CheckBoxList ID="cblMemberList" runat="server">
                                                            </asp:CheckBoxList>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:Button ID="btnBack" runat="server" CausesValidation="false" />
                                                            &nbsp;
                                                            <asp:Button ID="btnFinish" runat="server"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </telerik:RadToolTip>
            </asp:Panel>
        </contenttemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
