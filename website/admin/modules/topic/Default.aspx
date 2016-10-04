<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="admin_modules_topic_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Topics Module
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <span class="callout">Below you can add, edit and delete topics.</span><br />
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/folder.png" />
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/admin/modules/categories/default.aspx?rp=/admin/modules/topic/&mid=10">Manage Topic Categories</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <span class="callout">Below you can manage topics.</span><br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server">
                    <Tabs>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="RadGrid1">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                            </UpdatedControls>
                        </telerik:AjaxSetting>
                    </AjaxSettings>
                </telerik:RadAjaxManager>
                <telerik:RadGrid ID="RadGrid1" runat="server" PageSize="20">
                    <MasterTableView DataKeyNames="topicID" CommandItemDisplay="Top">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="topicID" HeaderText="topicID" SortExpression="topicID"
                                UniqueName="topicID" DataType="System.Int32" ReadOnly="True" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="topicName" HeaderText="Topic Name" SortExpression="topicName"
                                UniqueName="topicName">
                            </telerik:GridBoundColumn>
                            <telerik:GridDropDownColumn DataSourceID="SqlDataSource3" ListTextField="CategoryName"
                                ListValueField="categoryID" UniqueName="categoryName" SortExpression="categoryID"
                                HeaderText="Category" DataField="categoryID" DropDownControlType="DropDownList"
                                EnableEmptyListItem="true" EmptyListItemText="Uncategorized">
                            </telerik:GridDropDownColumn>
                            <telerik:GridTemplateColumn HeaderText="Active" UniqueName="status" SortExpression="status" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#If(Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "status")) = True, "true", "<span style='color:red;'>false</span>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="editTopic" DataField="topicID" HeaderText="Edit" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"editAdd.aspx?topicID=" & Eval("topicID") %>'>Edit</asp:HyperLink></ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Delete" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <table>
                                <tr style="height: 20px; padding-top: 5px">
                                    <td style="width: 20px; padding-left: 10px;">
                                        <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                    </td>
                                    <td style="width: 100%">
                                        <a href="editAdd.aspx?">Add a new topic</a>
                                    </td>
                                </tr>
                            </table>
                        </CommandItemTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 5px; text-align: right">
                <asp:Button ID="LinkButton1" runat="server" OnClick="btnDeleteLive_Click" OnClientClick="return DeleteConfirmation();"
                    Text="Delete" />
            </td>
        </tr>
    </table>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="ss_Category_SelectList_ByModuleTypeID" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="10" Name="ModuleTypeID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
