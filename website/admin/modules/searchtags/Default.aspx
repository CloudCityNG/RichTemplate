<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_modules_searchtags_Default" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout">
        <%= Resources.SearchTag_Admin.SearchTag_Default_BodyHeading%></span><br />
    <br />
    <div id="divBackToModule" runat="server" visible="false">
        <table>
            <tr>
                <td>
                    <img src="/admin/images/back.png" />
                </td>
                <td>
                    <asp:LinkButton ID="lnkBackToModule" runat="server" Text="<%$ Resources:SearchTag_Admin, SearchTag_Default_Back %>" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divBackToWebPage" runat="server" visible="false">
        <table>
            <tr>
                <td>
                    <img src="/admin/images/back.png" />
                </td>
                <td>
                    <asp:LinkButton ID="lnkBackToWebPage" runat="server" Text="<%$ Resources:SearchTag_Admin, SearchTag_Default_ReturnToWebPage %>" />
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadGrid ID="rgSearchTags" runat="server" DataSourceID="dsSearchTags" AllowAutomaticInserts="True"
        AllowAutomaticUpdates="True" AllowAutomaticDeletes="True" AllowPaging="false">
        <MasterTableView DataKeyNames="searchTagID" DataSourceID="dsSearchTags" CommandItemDisplay="Top"
            AllowPaging="false">
            <RowIndicatorColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="searchTagID" DataType="System.Int32" ReadOnly="True"
                    SortExpression="searchTagID" UniqueName="searchTagID" Visible="False">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="20px">
                    <ItemTemplate>
                        <img src="/admin/images/search_page.png" /></ItemTemplate>
                    <ItemStyle Width="20px"></ItemStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SearchTagName" HeaderText="<%$ Resources:SearchTag_Admin, SearchTag_Default_GridTagName %>"
                    SortExpression="SearchTagName" UniqueName="SearchTagName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SearchTagDescription" HeaderText="<%$ Resources:SearchTag_Admin, SearchTag_Default_GridDescription %>"
                    SortExpression="SearchTagDescription" UniqueName="SearchTagDescription">
                </telerik:GridBoundColumn>
                <telerik:GridEditCommandColumn HeaderText="<%$ Resources:SearchTag_Admin, SearchTag_Default_GridEdit %>"
                    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" EditText="<%$ Resources:SearchTag_Admin, SearchTag_Default_GridEdit %>">
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                    HeaderText="<%$ Resources:SearchTag_Admin, SearchTag_Default_GridDelete %>">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings EditFormType="Template">
                <FormTemplate>
                    <div style="padding: 10px">
                        <table>
                            <tr>
                                <td>
                                    <%= Resources.SearchTag_Admin.SearchTag_Default_EditTemplate_SearchTagName%>:<span
                                        class="requiredStar">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchTagName" runat="server" Text='<%# Bind( "searchTagName" ) %>'
                                        CssClass="tb200" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtSearchTagName" ID="reqSearchTagName"
                                        Display="Static" runat="server" ErrorMessage=" <%$ Resources:SearchTag_Admin, SearchTag_Default_RequiredMessage %>"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top">
                                    <%= Resources.SearchTag_Admin.SearchTag_Default_EditTemplate_SearchTagDescription%>:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchTagDescription" runat="server" Text='<%# Bind( "searchTagDescription" ) %>'
                                        TextMode="MultiLine" CssClass="tb200" Rows="3">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdate" Text='<%# If((TypeOf(Container) is GridEditFormInsertItem), Resources.SearchTag_Admin.SearchTag_Default_EditTemplate_ButtonInsert, Resources.SearchTag_Admin.SearchTag_Default_EditTemplate_ButtonUpdate) %>'
                                        runat="server" CommandName='<%# If((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>' />&nbsp;
                                    <asp:Button ID="btnCancel" Text="<%$ Resources:SearchTag_Admin, SearchTag_Default_EditTemplate_ButtonCancel %>"
                                        runat="server" CausesValidation="False" CommandName="Cancel" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <span class="requiredStar">*</span>
                                    <%= Resources.SearchTag_Admin.SearchTag_Default_EditTemplate_DenotesRequired%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
    </telerik:RadGrid>
    <div class="divFooter">
        <div class="fRight">
            <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:SearchTag_Admin, SearchTag_Default_GridDeleteButton %>"
                OnClick="btnDelete_Click" OnClientClick="return DeleteConfirmation();" /></div>
    </div>
    <br />
    <br />
    <asp:SqlDataSource ID="dsSearchTags" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="ss_SearchTag_SelectList_BySiteID" SelectCommandType="StoredProcedure"
        InsertCommand="ss_SearchTag_InsertSearchTag" InsertCommandType="StoredProcedure"
        UpdateCommand="ss_SearchTag_UpdateSearchTag_ByID" UpdateCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="siteID" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="searchTagName" Type="String" />
            <asp:Parameter Name="searchTagDescription" Type="String" />
            <asp:Parameter Name="siteID" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="searchTagName" Type="String" />
            <asp:Parameter Name="searchTagDescription" Type="String" />
            <asp:Parameter Name="searchTagID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
