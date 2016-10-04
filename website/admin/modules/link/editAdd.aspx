<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="editAdd.aspx.vb" Inherits="admin_modules_event_editAdd"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Add/Edit Link
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:Panel ID="editPanel" runat="server">--%>
    <script type="text/javascript">
        function validateRadUpload1(source, arguments) {
            arguments.IsValid = getRadUpload('<%= RadUpload1.ClientID %>').validateExtensions();
        }
    </script>
    <span class="callout">Complete the form below to post a Link</span><br />
    <br />
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
        SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Link Content" PageViewID="RadPageView1" Value="0"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="SEO Content" PageViewID="RadPageView2" Value="1"
                Visible="false">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Users & Groups" PageViewID="RadPageView3" Value="2"
                Visible="false">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Search Tags" PageViewID="RadPageView4" Value="3"
                Visible="false">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="History" PageViewID="RadPageView5" Value="4"
                Visible="false">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" CssClass="Windows7_MultipageBorder"
        SelectedIndex="0">
        <telerik:RadPageView ID="RadPageView1" runat="server">
            <span class="callout">Basic Link Information</span><br />
            Enter the basic link information below.<br />
            <br />
            <div id="MainContent">
                <table cellpadding="0" cellspacing="0">
                    <tr runat="server" visible="false">
                        <td>
                            <label for="Status">
                                <span class="moduleLabel">Status:</span>
                            </label>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td>
                            <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="True">Active</asp:ListItem>
                                <asp:ListItem Value="False">Archive</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="ReleaseDate">
                                            <span class="moduleLabel">Release Date:</span></label>
                                    </td>
                                    <td class="leftPad">
                                        <label for="ExpirationDate">
                                            <span class="moduleLabel">Expiration Date:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="ReleaseDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                                    </td>
                                    <td class="leftPad">
                                        <telerik:RadDatePicker ID="ExpirationDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
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
                                            <span class="moduleLabel">Category:</span></label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="rcbCategoryID" runat="server" />
                                    </td>
                                    <td class="leftPad">
                                        <asp:LinkButton ID="categoryLinkButton" runat="server">Add Category</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="linkname">
                                <span class="moduleLabel">Link Name:</span><span class="requiredStar">*</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="linkname"
                                    CssClass="errorStyle" Display="Dynamic" ErrorMessage=" Required" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="linkname" runat="server" CssClass="tb400" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="linkdescription">
                                <span class="moduleLabel">Link Description:</span><span class="requiredStar">*</span><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="linkurl" CssClass="errorStyle"
                                    Display="Dynamic" ErrorMessage=" Required" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="linkdescription" runat="server" CssClass="tb400" Rows="5" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="linkurl">
                                <span class="moduleLabel">Link URL:</span><span class="requiredStar">*</span><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="linkurl" CssClass="errorStyle"
                                    Display="Dynamic" ErrorMessage=" Required" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="linkurl" runat="server" CssClass="tb400" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <span class="moduleLabel">Upload Image:</span>
                                        <br />
                                        <span class="graySubText">Use the upload boxes below to upload an image for this link
                                            if you have a
                                            <br />
                                            dynamic scroller. Accepted file types:(.jpg, .gif, .png).</span>
                                        <asp:CustomValidator ID="Customvalidator1" runat="server" Display="Dynamic" ClientValidationFunction="validateRadUpload1"
                                            CssClass="errorStyle">
                    &nbsp;Invalid File Type.
                                        </asp:CustomValidator>
                                        <br />
                                        <br />
                                        <telerik:RadUpload ID="RadUpload1" runat="server" AllowedFileExtensions=".jpg,.gif,.png"
                                            ControlObjectsVisibility="None" InitialFileInputsCount="1">
                                        </telerik:RadUpload>
                                        <telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />
                                        <telerik:RadProgressArea ID="progressArea1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadBinaryImage ID="linkimage" runat="server" CssClass="rightPad" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" visible="false">
                        <td>
                            <label for="Keywords">
                                <span class="moduleLabel">Keywords:</span>
                            </label>
                            <br />
                            <span class="graySubText">Enter a coma seperated list of keywords for more precise searching.</span><br />
                        </td>
                    </tr>
                    <tr id="Tr1" runat="server" visible="false">
                        <td>
                            <asp:TextBox ID="Keywords" runat="server" CssClass="tb400" Rows="3" TextMode="MultiLine" />
                        </td>
                    </tr>
                </table>
                <asp:TextBox ID="author" runat="server" Visible="false" />
                <asp:TextBox ID="modifiedBy" runat="server" Visible="false" />
                <asp:TextBox ID="version" runat="server" Width="35px" Visible="false" />
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView2" runat="server">
            <span class="callout">SEO Head Tag Information</span><br />
            Enter meta data to be inserted into the head section of the page for better SEO.
            <div id="Div1">
                <div>
                </div>
                <p>
                    <label for="metaTitle">
                        <span class="moduleLabel">Meta Title:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaTitle" runat="server" TextMode="MultiLine" Rows="3" CssClass="tb400" />
                </p>
                <p>
                    <label for="metaKeywords">
                        <span class="moduleLabel">Meta Keywords:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaKeywords" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    <label for="metaDescription">
                        <span class="moduleLabel">Meta Description:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaDescription" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    <label for="otherMeta">
                        <span class="moduleLabel">Other Meta Elements:</span>
                    </label>
                    <br />
                    <asp:TextBox ID="metaOther" runat="server" TextMode="MultiLine" Rows="5" CssClass="tb400" /></p>
                <p>
                    &nbsp;</p>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView3" runat="server">
            <span class="callout">Content Security Settings</span><br />
            Select who can view this content below. Individual accounts override group accounts.<br />
            <br />
            <span class="moduleLabel">Groups with access to this content</span>
            <asp:CheckBoxList ID="cblGroupList" runat="server">
            </asp:CheckBoxList>
            <br />
            <div id="divUserGroups_Members" runat="server" visible="false">
                <span class="moduleLabel">Individual Users with access to this content</span>
                <asp:CheckBoxList ID="cblMemberList" runat="server">
                </asp:CheckBoxList>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView4" runat="server">
            <span class="callout">Content Search Tags</span><br />
            Assign content search tags for advanced searching and taxonomy.<br />
            <br />
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <img src="/admin/images/search_page.png" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkAddSearchTags" runat="server" OnClick="lnkAddSearchTags_Click">Add Additional Search Tags</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <br />
            <asp:CheckBoxList ID="cblSearchTags" runat="server">
            </asp:CheckBoxList>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView5" runat="server">
            <span class="callout">Content Revision History</span><br />
            Below is a list of all revisions made to this record. You can view and even restore
            previous versions of the data.<br />
            <br />
            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                <script type="text/javascript">
                    function ShowEditForm(id, rowIndex) {
                        var grid = $find("<%= RadGrid1.ClientID %>");

                        var rowControl = grid.get_masterTableView().get_dataItems()[rowIndex].get_element();
                        grid.get_masterTableView().selectItem(rowControl, true);

                        window.radopen("Preview.aspx?archiveID=" + id, "UserListDialog");
                        return false;
                    }

                    function refreshGrid(arg) {
                        if (!arg) {
                            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                        }
                        else {
                            $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");
                        }
                    }
                </script>
            </telerik:RadCodeBlock>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource1" PageSize="20"
                OnItemDataBound="RadGrid1_ItemDataBound">
                <MasterTableView DataKeyNames="archiveID" DataSourceID="SqlDataSource1">
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
                        <telerik:GridBoundColumn DataField="dateTimeStamp" DataType="System.DateTime" HeaderText="Revsion Date"
                            SortExpression="dateTimeStamp" UniqueName="dateTimeStamp" ItemStyle-Width="120px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Version" HeaderText="Revision" SortExpression="version"
                            ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" UniqueName="version">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="linkName" HeaderText="Link Name" SortExpression="linkName"
                            UniqueName="linkName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="linkURL" HeaderText="Link URL" SortExpression="linkURL"
                            UniqueName="linkURL">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" ItemStyle-Width="50px"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="EditLink" runat="server" Text="View"></asp:HyperLink>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Delete" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <CommandItemTemplate>
                        <br />
                        <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"><img style="border:0px" alt="" src="/admin/images/AddRecord.gif" /> Add a new entry</asp:LinkButton>
                        </div>
                    </CommandItemTemplate>
                </MasterTableView>
            </telerik:RadGrid>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="ss_LinkArchive_SelectList_ByLinkID" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter Name="linkID" QueryStringField="ID" />
                </SelectParameters>
            </asp:SqlDataSource>
            <div id="order" class="orderText">
                <div style="float: right; padding-top: 20px">
                    <asp:Button ID="Button2" runat="server" Text="Delete" OnClick="btnDeleteLive_Click"
                        OnClientClick="return DeleteConfirmation();" /></div>
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
                    <Windows>
                        <telerik:RadWindow ID="UserListDialog" runat="server" Title="Editing record" Height="600px"
                            Width="1000px" Left="10px" ReloadOnShow="true" ShowContentDuringLoad="false"
                            Modal="true" />
                    </Windows>
                </telerik:RadWindowManager>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" Text="" CausesValidation="true" />
    <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span> Denotes required information<br />
    <%-- </asp:Panel>--%>
</asp:Content>
