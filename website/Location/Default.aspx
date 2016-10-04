<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="location_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br class="cBoth" />
    <div id="divModuleDynamicContent" runat="server" visible="false" class="divModuleDynamicContent">
        <asp:Literal ID="litModuleDynamicContent" runat="server" />
    </div>
    <br />
    <div class="divLocationList">
        <div class="divLetterListTop">
            <asp:Repeater ID="rptCityLettersTop" runat="server">
                <ItemTemplate>
                    &nbsp;<%# Container.DataItem.ToString() %>
                </ItemTemplate>
            </asp:Repeater>&nbsp; <a href='/location/'><%= Resources.Location_FrontEnd.Location_Default_ViewAll%></a>
        </div>
        <telerik:RadGrid ID="rgLocation" runat="server">
            <MasterTableView DataKeyNames="ID" AllowNaturalSort="false" AllowMultiColumnSorting="false"
                PageSize="50">
                <RowIndicatorColumn>
                    <HeaderStyle Width="20px" />
                </RowIndicatorColumn>
                <ExpandCollapseColumn>
                    <HeaderStyle Width="20px" />
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" ReadOnly="True" SortExpression="ID"
                        UniqueName="ID" Visible="false" ItemStyle-VerticalAlign="Top">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="categoryName" HeaderText="<%$ Resources:Location_FrontEnd, Location_Default_Grid_CategoryName %>"
                        SortExpression="categoryName" UniqueName="categoryName" ItemStyle-VerticalAlign="Top">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="location" HeaderText="<%$ Resources:Location_FrontEnd, Location_Default_Grid_Location %>"
                        SortExpression="location" UniqueName="location" ItemStyle-VerticalAlign="Top">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="<%$ Resources:Location_FrontEnd, Location_Default_Grid_Address%>"
                        DataField="address1" UniqueName="address1" Reorderable="false" ShowSortIcon="false"
                        SortExpression="" HeaderStyle-Width="300">
                        <ItemTemplate>
                            <%# If((Not Eval("Address1") Is DBNull.Value) AndAlso (Eval("Address1").ToString.Length > 0), Eval("Address1") & "<br/>", "")%>
                            <%# If((Not Eval("Address2") Is DBNull.Value) AndAlso (Eval("Address2").ToString.Length > 0), Eval("Address2") & "<br/>", "")%>
                            <%# If((Not Eval("Address3") Is DBNull.Value) AndAlso (Eval("Address3").ToString.Length > 0), Eval("Address3") & "<br/>", "")%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="city" HeaderText="<%$ Resources:Location_FrontEnd, Location_Default_Grid_City %>"
                        SortExpression="city" UniqueName="city" ItemStyle-VerticalAlign="Top">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="state_province" HeaderText="<%$ Resources:Location_FrontEnd, Location_Default_Grid_StateProvince %>"
                        SortExpression="state_province" UniqueName="state_province" ItemStyle-VerticalAlign="Top">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="zip" HeaderText="<%$ Resources:Location_FrontEnd, Location_Default_Grid_Zip %>"
                        SortExpression="zip" UniqueName="zip" ItemStyle-VerticalAlign="Top">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <div class="divLetterListBottom">
            <asp:Repeater ID="rptCityLettersBottom" runat="server">
                <ItemTemplate>
                    &nbsp;<%# Container.DataItem.ToString() %>
                </ItemTemplate>
            </asp:Repeater>&nbsp; <a href='/location/'><%= Resources.Location_FrontEnd.Location_Default_ViewAll%></a>
        </div>
    </div>
</asp:Content>
