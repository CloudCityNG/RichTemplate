<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="StaffPosition.aspx.vb" Inherits="admin_modules_staff_StaffPosition" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout">
        <%=Resources.Staff_Admin.Staff_StaffPosition_AddEdit_Header%></span><br />
    <table>
        <tr>
            <td>
                <img src="/admin/images/back.png" />
            </td>
            <td>
                <a href="Default.aspx">
                    <%=Resources.Staff_Admin.Staff_StaffPosition_ReturnToModule%></a>
            </td>
        </tr>
    </table>
    <telerik:RadGrid runat="server" ID="rgStaffPosition" DataSourceID="dsStaffPositionList"
        PageSize="20" AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowAutomaticDeletes="True">
        <MasterTableView DataSourceID="dsStaffPositionList" DataKeyNames="StaffPositionID"
            CommandItemDisplay="Top">
            <Columns>
                <telerik:GridBoundColumn DataField="StaffPositionID" DataType="System.Int32" ReadOnly="True"
                    SortExpression="StaffPositionID" UniqueName="StaffPositionID" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="20px" UniqueName="uImage">
                    <ItemTemplate>
                        <asp:Image ID="userImage" runat="server" ImageUrl="/admin/images/folder.png" /></ItemTemplate>
                    <ItemStyle Width="20px"></ItemStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="StaffPosition" HeaderText="<%$ Resources:Staff_Admin, Staff_StaffPosition_GridStaffPosition%>"
                    SortExpression="StaffPosition" UniqueName="StaffPosition">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" HeaderText="<%$ Resources:Staff_Admin, Staff_StaffPosition_GridDescription %>"
                    SortExpression="Description" UniqueName="Description">
                </telerik:GridBoundColumn>
                <telerik:GridEditCommandColumn HeaderText="<%$ Resources:Staff_Admin, Staff_StaffPosition_GridEdit %>"
                    ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" EditText="<%$ Resources:Staff_Admin, Staff_StaffPosition_GridEdit %>">
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                    HeaderText="<%$ Resources:Staff_Admin, Staff_StaffPosition_GridDelete%>">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings EditFormType="Template">
                <FormTemplate>
                    <asp:Panel ID="pnlStaffPosition" runat="server" DefaultButton="btnUpdate">
                        <div style="padding: 10px">
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <span class="callout">
                                            <%=Resources.Staff_Admin.Staff_StaffPosition_EditTemplate_Header%></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%=Resources.Staff_Admin.Staff_StaffPosition_EditTemplate_StaffPosition%>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStaffPosition" runat="server" Text='<%# Bind("StaffPosition") %>'
                                            CssClass="tb200" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtStaffPosition" ID="reqStaffPosition"
                                            Display="Static" runat="server" ErrorMessage="<%$ Resources:Staff_Admin, Staff_StaffPosition_EditTemplate_Required%>"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        <%= Resources.Staff_Admin.Staff_StaffPosition_EditTemplate_Description%>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind( "Description" ) %>'
                                            TextMode="MultiLine" CssClass="tb200" Rows="3">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <br />
                                        <asp:Button ID="btnUpdate" Text='<%# If((TypeOf(Container) is GridEditFormInsertItem), Resources.Staff_Admin.Staff_StaffPosition_EditTemplate_ButtonInsert, Resources.Staff_Admin.Staff_StaffPosition_EditTemplate_ButtonUpdate) %>'
                                            runat="server" CommandName='<%# If((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>'>
                                        </asp:Button>&nbsp;
                                        <asp:Button ID="btnCancel" Text="<%$ Resources:Staff_Admin, Staff_StaffPosition_EditTemplate_ButtonCancel%>"
                                            runat="server" CausesValidation="False" CommandName="Cancel"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <span class="requiredStar">*</span><%=Resources.Staff_Admin.Staff_StaffPosition_EditTemplate_DenotesRequired%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <div class="divFooter">
        <div class="fRight">
            <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Staff_Admin, Staff_StaffPosition_GridDeleteButton%>"
                OnClick="btnDelete_Click" /></div>
    </div>
    <asp:SqlDataSource ID="dsStaffPositionList" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        DeleteCommand="ss_StaffPosition_Delete_ByStaffPositionID" DeleteCommandType="StoredProcedure"
        InsertCommand="ss_StaffPosition_InsertStaffPosition" InsertCommandType="StoredProcedure"
        SelectCommand="ss_StaffPosition_SelectList_BySiteID" SelectCommandType="StoredProcedure"
        UpdateCommand="ss_StaffPosition_UpdateStaffPosition_ByStaffPositionID" UpdateCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="SiteID" Type="Int32" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="StaffPositionID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="SiteID" Type="Int32" />
            <asp:Parameter Name="StaffPosition" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="StaffPositionID" Type="Int32" />
            <asp:Parameter Name="StaffPosition" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
