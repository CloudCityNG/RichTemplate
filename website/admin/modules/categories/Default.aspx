<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_modules_categories" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout"><asp:Literal ID="litModuleCategoryHeadingBody" runat="server" /></span><br />
    <br />
    <table>
        <tr>
            <td>
                <img src="/admin/images/back.png" />
            </td>
            <td>
                <a id="aReturnToModule" runat="server"><asp:Literal ID="litReturnToModule" runat="server" /></a>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxManager runat="server" ID="ramCategory" DefaultLoadingPanelID="radLoadingPanelCategory">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgCategory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgCategory" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="radLoadingPanelCategory" runat="server" Transparency="80"
        BackColor="gray">
        <img src='<%= RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Ajax.loading.gif") %>'
            alt="Loading..." style="border: 0;" />
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadGrid runat="server" ID="rgCategory" OnRowDrop="rgCategory_RowDrop" DataSourceID="dsCategory"
        PageSize="20" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowAutomaticDeletes="True">
        <MasterTableView DataSourceID="dsCategory" DataKeyNames="CategoryID" CommandItemDisplay="Top">
            <Columns>
                <telerik:GridBoundColumn DataField="CategoryID" DataType="System.Int32" ReadOnly="True"
                    SortExpression="CategoryID" UniqueName="CategoryID" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="20px" UniqueName="uImage">
                    <ItemTemplate>
                        <div class="folder">
                            &nbsp;</div>
                    </ItemTemplate>
                    <ItemStyle Width="20px"></ItemStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="CategoryName" HeaderText="<%$ Resources:Category_Admin, Category_Default_GridCategoryName %>"
                    SortExpression="CategoryName" UniqueName="CategoryName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" HeaderText="<%$ Resources:Category_Admin, Category_Default_GridCategoryDescription %>"
                    SortExpression="Description" UniqueName="Description">
                </telerik:GridBoundColumn>
                <telerik:GridEditCommandColumn HeaderText="<%$ Resources:Category_Admin, Category_Default_GridEdit %>"
                    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" EditText="<%$ Resources:Category_Admin, Category_Default_GridEdit %>">
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                    HeaderText="<%$ Resources:Category_Admin, Category_Default_GridDelete %>">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings EditFormType="Template">
                <FormTemplate>
                <asp:Panel ID="pnlCategory" runat="server" DefaultButton="btnUpdate">
                    <div style="padding: 10px">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <span class="callout">
                                        <%= Resources.Category_Admin.Category_Default_EditTemplate_Heading%></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= Resources.Category_Admin.Category_Default_EditTemplate_CategoryName%>:<span class="requiredStar">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCategoryName" runat="server" Text='<%# Bind( "categoryname" ) %>'
                                        CssClass="tb200" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtCategoryName" ID="reqCategoryName"
                                        Display="Static" runat="server" ErrorMessage=" <%$ Resources:Category_Admin, Category_Default_RequiredMessage %>"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top">
                                    <%= Resources.Category_Admin.Category_Default_EditTemplate_CategoryDescription%>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCategoryDescription" runat="server" Text='<%# Bind( "Description" ) %>'
                                        TextMode="MultiLine" CssClass="tb200" Rows="3">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdate" Text='<%# If((TypeOf(Container) is GridEditFormInsertItem), Resources.Category_Admin.Category_Default_EditTemplate_ButtonInsert, Resources.Category_Admin.Category_Default_EditTemplate_ButtonUpdate) %>'
                                        runat="server" CommandName='<%# If((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>'>
                                    </asp:Button>&nbsp;
                                    <asp:Button ID="btnCancel" Text="<%$ Resources:Category_Admin, Category_Default_EditTemplate_ButtonCancel %>"
                                        runat="server" CausesValidation="False" CommandName="Cancel"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <span class="requiredStar">*</span>
                                    <%= Resources.Category_Admin.Category_Default_EditTemplate_DenotesRequired%>
                                </td>
                            </tr>
                        </table>
                    </div>
                    </asp:Panel>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings AllowRowsDragDrop="True">
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <div class="divFooter">
        <div class="fRight">
            <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Category_Admin, Category_Default_GridDeleteButton %>" OnClick="btnDelete_Click" /></div>
    </div>
    <asp:SqlDataSource ID="dsCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        InsertCommand="ss_Category_InsertCategory" InsertCommandType="StoredProcedure"
        SelectCommand="ss_Category_SelectList_ByModuleTypeIDAndSiteID" SelectCommandType="StoredProcedure"
        UpdateCommand="ss_Category_UpdateCategory" UpdateCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter Name="ModuleTypeID" QueryStringField="mtid" />
            <asp:Parameter Name="SiteID" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="CategoryName" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="CategoryID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="CategoryName" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="SiteID" Type="Int32" />
            <asp:QueryStringParameter Name="ModuleTypeID" QueryStringField="mtid" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>
