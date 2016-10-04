<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="editAdd.aspx.vb" Inherits="admin_modules_event_editAdd"
    ValidateRequest="false" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout">
        <%= Resources.Faq_Admin.Faq_AddEdit_BodyHeading%></span><br />
    <br />
    <telerik:RadTabStrip ID="rtsFaq" runat="server" MultiPageID="rmpFaq" SelectedIndex="0"
        CausesValidation="true">
        <Tabs>
            <telerik:RadTab runat="server" Text="<%$ Resources:Faq_Admin, Faq_AddEdit_Tab_Content %>"
                PageViewID="rpvFaqs" Value="0" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Faq_Admin, Faq_AddEdit_Tab_SEO %>"
                PageViewID="rpvSEO" Value="1">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Faq_Admin, Faq_AddEdit_Tab_SearchTags %>"
                PageViewID="rpvSearchTags" Value="2">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Faq_Admin, Faq_AddEdit_Tab_UserGroups %>"
                PageViewID="rpvUserAndGroups" Value="3" Visible="false">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Faq_Admin, Faq_AddEdit_Tab_History %>"
                PageViewID="rpvHistory" Value="4">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpFaq" runat="server" CssClass="Windows7_MultipageBorder"
        SelectedIndex="0">
        <telerik:RadPageView ID="rpvFaqs" runat="server">
            <span class="callout">
                <%= Resources.Faq_Admin.Faq_AddEdit_Tab_Content_Heading%></span><br />
            <%= Resources.Faq_Admin.Faq_AddEdit_Tab_Content_SubHeading%><br />
            <br />
            <div id="MainContent">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <label for="Status">
                                <span class="moduleLabel">
                                    <%= Resources.Faq_Admin.Faq_AddEdit_Status%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Faq_Admin, Faq_AddEdit_StatusActive %>" />
                                <asp:ListItem Value="False" Text="<%$ Resources:Faq_Admin, Faq_AddEdit_StatusArchive %>" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="PublicationDate">
                                            <span class="moduleLabel">
                                                <%= Resources.Faq_Admin.Faq_AddEdit_PublicationDate%>:</span></label>
                                    </td>
                                    <td class="leftPad">
                                        <label for="ExpirationDate">
                                            <span class="moduleLabel">
                                                <%= Resources.Faq_Admin.Faq_AddEdit_ExpirationDate%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="PublicationDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                                    </td>
                                    <td class="leftPad">
                                        <telerik:RadDatePicker ID="ExpirationDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <span id="spanExpired" runat="server" class="errorStyle spanExpired" visible="false">
                                            <span>
                                                <img src='/admin/images/expired.png' />
                                            </span><span>
                                                <%= Resources.Faq_Admin.Faq_AddEdit_ExpirationDateExpired%></span> </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trSiteAccess" runat="server" visible="false">
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="rblSite">
                                            <span class="moduleLabel">
                                                <%=Resources.Faq_Admin.Faq_AddEdit_AssociateWithSite%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="divAssociateWithSite" runat="server">
                                            <asp:RadioButtonList ID="rblSite" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Text="<%$ Resources:Faq_Admin, Faq_AddEdit_AssociateWithSite_ThisSiteOnly %>"
                                                    Value="false" Selected="True" />
                                                <asp:ListItem Text="<%$ Resources:Faq_Admin, Faq_AddEdit_AssociateWithSite_AllSites %>"
                                                    Value="true" />
                                            </asp:RadioButtonList>
                                        </div>
                                        <div id="divAssociateWithSite_PublicMessage" runat="server" class="divAssociateWithSite_PublicMessage"
                                            visible="false">
                                            <span>
                                                <img src='/admin/images/available_to_all.png' />
                                            </span><span class="spanPublicModuleRecord">
                                                <%=Resources.Faq_Admin.Faq_AddEdit_AssociateWithSite_PublicMessage%>: <asp:Literal
                                                    ID="litSiteName" runat="server" /></span>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="Category">
                                            <span class="moduleLabel">
                                                <%= Resources.Faq_Admin.Faq_AddEdit_Category%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="rcbCategoryID" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtQuestion">
                                <span class="moduleLabel">
                                    <%= Resources.Faq_Admin.Faq_AddEdit_Question%>:</span><span class="requiredStar">*</span>
                                <asp:RequiredFieldValidator ID="reqQuestion" runat="server" ControlToValidate="txtQuestion"
                                    CssClass="errorStyle" Display="Dynamic" ErrorMessage=" <%$ Resources:Faq_Admin, Faq_AddEdit_RequiredMessage %>" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtQuestion" runat="server" CssClass="tb400" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtAnswer">
                                <span class="moduleLabel">
                                    <%= Resources.Faq_Admin.Faq_AddEdit_Answer%>:</span><span class="requiredStar">*</span><asp:RequiredFieldValidator
                                        ID="reqAnswer" runat="server" ControlToValidate="txtAnswer" CssClass="errorStyle"
                                        Display="Dynamic" ErrorMessage=" <%$ Resources:Faq_Admin, Faq_AddEdit_RequiredMessage %>" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadEditor ID="txtAnswer" runat="server" Width="98%">
                            </telerik:RadEditor>
                        </td>
                    </tr>
                </table>
                <asp:TextBox ID="version" runat="server" Width="35px" Visible="false" />
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvSEO" runat="server">
            <span class="callout">
                <%= Resources.Faq_Admin.Faq_AddEdit_Tab_SEO_Heading%></span><br />
            <%= Resources.Faq_Admin.Faq_AddEdit_Tab_SEO_SubHeading%>
            <div>
                <p>
                    <label for="metaTitle">
                        <span class="moduleLabel">
                            <%= Resources.Faq_Admin.Faq_AddEdit_Tab_SEO_MetaTitle%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaTitle" runat="server" TextMode="MultiLine" Rows="3" CssClass="tb400" />
                </p>
                <p>
                    <label for="metaKeywords">
                        <span class="moduleLabel">
                            <%= Resources.Faq_Admin.Faq_AddEdit_Tab_SEO_MetaKeywords%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaKeywords" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    <label for="metaDescription">
                        <span class="moduleLabel">
                            <%= Resources.Faq_Admin.Faq_AddEdit_Tab_SEO_MetaDescription%>:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaDescription" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    &nbsp;</p>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvSearchTags" runat="server">
            <span class="callout">
                <%= Resources.Faq_Admin.Faq_AddEdit_Tab_SearchTags_Heading%></span><br />
            <%= Resources.Faq_Admin.Faq_AddEdit_Tab_SearchTags_SubHeading%><br />
            <div id="divAddSearchTags" runat="server" visible="false">
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/search_page.png" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkAddSearchTags" runat="server" OnClick="lnkAddSearchTags_Click"><%= Resources.Faq_Admin.Faq_AddEdit_Tab_SearchTags_AddSearchTags%></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:CheckBoxList ID="cblSearchTags" runat="server">
            </asp:CheckBoxList>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvUserAndGroups" runat="server">
            <span class="callout">
                <%= Resources.Faq_Admin.Faq_AddEdit_Tab_UserGroups_Heading%></span><br />
            <%= Resources.Faq_Admin.Faq_AddEdit_Tab_UserGroups_SubHeading%><br />
            <br />
            <span class="moduleLabel">
                <%= Resources.Faq_Admin.Faq_AddEdit_Tab_UserGroups_Groups%></span>
            <asp:CheckBoxList ID="cblGroupList" runat="server">
            </asp:CheckBoxList>
            <br />
            <div id="divUserGroups_Members" runat="server" visible="false">
                <span class="moduleLabel">
                    <%= Resources.Faq_Admin.Faq_AddEdit_Tab_UserGroups_Users%></span>
                <asp:CheckBoxList ID="cblMemberList" runat="server">
                </asp:CheckBoxList>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvHistory" runat="server">
            <span class="callout">
                <%= Resources.Faq_Admin.Faq_AddEdit_Tab_History_Heading%></span><br />
            <%= Resources.Faq_Admin.Faq_AddEdit_Tab_History_SubHeading%><br />
            <br />
            <telerik:RadGrid ID="rgHistory" runat="server" PageSize="20">
                <MasterTableView DataKeyNames="archiveID">
                    <RowIndicatorColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="archiveID" DataType="System.Int32" ReadOnly="True"
                            SortExpression="archiveID" UniqueName="archiveID" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="dateTimeStamp" DataType="System.DateTime" HeaderText="<%$ Resources:Faq_Admin, Faq_AddEdit_Tab_History_GridRevisionDate %>"
                            SortExpression="dateTimeStamp" UniqueName="dateTimeStamp" ItemStyle-Width="120px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Version" HeaderText="<%$ Resources:Faq_Admin, Faq_AddEdit_Tab_History_GridVersion %>"
                            SortExpression="Version" UniqueName="Version" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Question" HeaderText="<%$ Resources:Faq_Admin, Faq_AddEdit_Tab_History_GridQuestion %>"
                            SortExpression="Question" UniqueName="Question">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" ItemStyle-Width="50px"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Faq_Admin, Faq_AddEdit_Tab_History_GridView %>">
                            <ItemTemplate>
                                <a id="aPreview" runat="server">
                                    <%= Resources.Faq_Admin.Faq_AddEdit_Tab_History_GridView%></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="<%$ Resources:Faq_Admin, Faq_AddEdit_Tab_History_GridDelete %>"
                            ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <div id="order" class="divFooterHistory orderText">
                <div class="fRight">
                    <asp:Button ID="btnDeleteHistory" runat="server" Text="<%$ Resources:Faq_Admin, Faq_AddEdit_Tab_History_DeleteButton %>"
                        OnClick="btnDeleteHistory_Click" /></div>
                <br class="cBoth" />
                <telerik:RadWindowManager ID="rwmFaqHistory" runat="server">
                    <Windows>
                        <telerik:RadWindow ID="UserListDialog" runat="server" Title="<%$ Resources:Faq_Admin, Faq_AddEdit_Tab_History_EditingRecord %>"
                            Height="600px" Width="1000px" Left="10px" ReloadOnShow="true" ShowContentDuringLoad="false"
                            Modal="true" />
                    </Windows>
                </telerik:RadWindowManager>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" CausesValidation="true" />
    <asp:Button ID="cancelButton" runat="server" Text="<%$ Resources:Faq_Admin, Faq_AddEdit_ButtonCancel %>"
        CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span>
    <%= Resources.Faq_Admin.Faq_AddEdit_DenotesRequired%><br />
</asp:Content>
