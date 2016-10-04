<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="editAddAnswer.aspx.vb" Inherits="admin_modules_poll_editAddAnswer"
    ValidateRequest="false" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="ralPollAnswer" runat="server" />
    <telerik:RadScriptBlock runat="server" ID="radScriptBlockPollAnswer">
        <script type="text/javascript">
            function valPollAnswerImage(source, arguments) {
                arguments.IsValid = getRadUpload('<%= radUploadPollAnswerImage.ClientID %>').validateExtensions();
            }

        </script>
    </telerik:RadScriptBlock>
    <span class="callout">
        <%= Resources.Poll_Admin.Poll_AddEditAnswer_BodyHeading%></span>
    <br />
    <br />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <img src="/admin/images/back.png" />
            </td>
            <td>
                <a id="aBackToPollAnswers" runat="server">
                    <%= Resources.Poll_Admin.Poll_AddEditAnswer_BackToPollAnswers%></a>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="rtsPollAnswer" runat="server" MultiPageID="rmpPollAnswer"
        CausesValidation="true" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="<%$ Resources:Poll_Admin, Poll_AddEditAnswer_Tab_Content %>"
                PageViewID="rpvPollAnswer" Value="0">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="<%$ Resources:Poll_Admin, Poll_AddEditAnswer_Tab_History %>"
                PageViewID="rpvHistory" Value="1">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpPollAnswer" runat="server" SelectedIndex="0" CssClass="Windows7_MultipageBorder">
        <telerik:RadPageView ID="rpvPollAnswer" runat="server">
            <span class="callout">
                <%= Resources.Poll_Admin.Poll_AddEditAnswer_Tab_Content_Heading%></span><br />
            <%= Resources.Poll_Admin.Poll_AddEditAnswer_Tab_Content_SubHeading%><br />
            <br />
            <div id="MainContent">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <label for="Status">
                                <span class="moduleLabel">
                                    <%= Resources.Poll_Admin.Poll_AddEditAnswer_Status%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="True" Text="<%$ Resources:Poll_Admin, Poll_AddEditAnswer_StatusActive %>" />
                                <asp:ListItem Value="False" Text="<%$ Resources:Poll_Admin, Poll_AddEditAnswer_StatusArchive %>" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="trMustUpdateQueryToUseThis" runat="server" visible="false">
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <span class="moduleLabel">
                                            <%= Resources.Poll_Admin.Poll_AddEditAnswer_PublicationDate%>:</span>
                                    </td>
                                    <td class="leftPad">
                                        <span class="moduleLabel">
                                            <%= Resources.Poll_Admin.Poll_AddEditAnswer_ExpirationDate%>:</span>
                                    </td>
                                    <td></td>
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
                                                <%= Resources.Poll_Admin.Poll_AddEditAnswer_ExpirationDateExpired%></span>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtAnswer">
                                <span class="moduleLabel">
                                    <%= Resources.Poll_Admin.Poll_AddEditAnswer_Answer%>:</span></label><span class="requiredStar">*</span>
                            <asp:RequiredFieldValidator ID="reqAnswer" runat="server" ErrorMessage=" <%$ Resources:Poll_Admin, Poll_AddEditAnswer_RequiredMessage %>"
                                ControlToValidate="txtAnswer" CssClass="errorStyle" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadEditor ID="txtAnswer" runat="server" Width="98%">
                            </telerik:RadEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="txtDescription">
                                <span class="moduleLabel">
                                    <%= Resources.Poll_Admin.Poll_AddEditAnswer_Description%>:</span>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="tb400" TextMode="MultiLine"
                                Rows="3" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label for="chkAnswerIsCorrect">
                                <span class="moduleLabel">
                                    <%= Resources.Poll_Admin.Poll_AddEditAnswer_AnswerIsCorrect%>:</span>
                                <asp:CheckBox ID="chkAnswerIsCorrect" runat="server" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <span class="moduleLabel">
                                            <%= Resources.Poll_Admin.Poll_AddEditAnswer_UploadImage%>:</span>
                                        <br />
                                        <span class="graySubText">
                                            <%= Resources.Poll_Admin.Poll_AddEditAnswer_UploadImage_Message%><br />
                                            <%= Resources.Poll_Admin.Poll_AddEditAnswer_UploadImage_Requirements%></span>
                                        <asp:CustomValidator ID="customValPollAnswerImage" runat="server" Display="Dynamic"
                                            ClientValidationFunction="valPollAnswerImage" CssClass="errorStyle">
                    &nbsp;<br /><b><%= Resources.Poll_Admin.Poll_AddEditAnswer_UploadImage_InvalidFileType%></b>
                                        </asp:CustomValidator>
                                        <asp:CustomValidator ID="customValPollAnswerSizeExceeded" runat="server" Display="Dynamic"
                                            OnServerValidate="customValPollAnswerImageSizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<br /><b><%= Resources.Poll_Admin.Poll_AddEditAnswer_UploadImage_MaxFileSizeExceeded%></b>
                                        </asp:CustomValidator>
                                        <br />
                                        <br />
                                        <telerik:RadUpload ID="RadUploadPollAnswerImage" runat="server" AllowedFileExtensions=".jpg,.gif,.png"
                                            ControlObjectsVisibility="None" InitialFileInputsCount="1" />
                                        <telerik:RadProgressManager ID="rpManagerPollAnswer" runat="server" />
                                        <telerik:RadProgressArea ID="rpAreaPollAnswer" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadBinaryImage ID="pollAnswerImage" runat="server" CssClass="rightPad" Visible="false" /><br />
                                        <asp:LinkButton ID="lnkDeleteImage" runat="server" Visible="false" OnClick="lnkDeleteImage_Click"
                                            CausesValidation="false"><b><%= Resources.Poll_Admin.Poll_AddEditAnswer_UploadImage_Delete%></b></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:TextBox ID="txtVersion" runat="server" Width="35px" Visible="false" />
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvHistory" runat="server">
            <span class="callout">
                <%= Resources.Poll_Admin.Poll_AddEditAnswer_Tab_History_Heading%></span><br />
            <%= Resources.Poll_Admin.Poll_AddEditAnswer_Tab_History_SubHeading%><br />
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
                        <telerik:GridBoundColumn DataField="dateTimeStamp" DataType="System.DateTime" HeaderText="<%$ Resources:Poll_Admin, Poll_AddEditAnswer_Tab_History_GridRevisionDate %>"
                            SortExpression="dateTimeStamp" UniqueName="dateTimeStamp" ItemStyle-Width="120px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Version" HeaderText="<%$ Resources:Poll_Admin, Poll_AddEditAnswer_Tab_History_GridVersion %>"
                            SortExpression="Version" UniqueName="Version" ItemStyle-Width="100px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Answer" HeaderText="<%$ Resources:Poll_Admin, Poll_AddEditAnswer_Tab_History_GridAnswer %>"
                            SortExpression="Answer" UniqueName="Answer">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateEditColumn" ItemStyle-Width="50px"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Poll_Admin, Poll_AddEditAnswer_Tab_History_GridView %>">
                            <ItemTemplate>
                                <a id="aPreview" runat="server">
                                    <%= Resources.Poll_Admin.Poll_AddEditAnswer_Tab_History_GridView%></a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="<%$ Resources:Poll_Admin, Poll_AddEditAnswer_Tab_History_GridDelete %>"
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
                    <asp:Button ID="btnDeleteHistory" runat="server" Text="<%$ Resources:Poll_Admin, Poll_AddEditAnswer_Tab_History_GridDeleteButton %>"
                        OnClick="btnDeleteHistory_Click" /></div>
                <br class="cBoth" />
                <telerik:RadWindowManager ID="rwmPollAnswerHistory" runat="server">
                    <Windows>
                        <telerik:RadWindow ID="UserListDialog" runat="server" Title="<%$ Resources:Poll_Admin, Poll_AddEditAnswer_Tab_History_EditingRecord %>"
                            Height="600px" Width="1000px" Left="10px" ReloadOnShow="true" ShowContentDuringLoad="false"
                            Modal="true" />
                    </Windows>
                </telerik:RadWindowManager>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />
    <asp:Button ID="btnAddEdit" runat="server" CausesValidation="true" />
    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Poll_Admin, Poll_AddEditAnswer_ButtonCancel %>"
        CausesValidation="false" />
    <br />
    <span class="requiredStar">*</span>
    <%= Resources.Poll_Admin.Poll_AddEditAnswer_DenotesRequired%><br />
</asp:Content>
