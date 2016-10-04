<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    ValidateRequest="false" AutoEventWireup="false" CodeFile="SavePressRelease.aspx.vb"
    Inherits="PressRelease_SavePressRelease" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function valPressReleaseImage(source, arguments) {
            arguments.IsValid = getRadUpload('<%= radUploadPressReleaseImage.ClientID %>').validateExtensions();
        }
    </script>
    <div class="divModuleContent">
        <table width="100%">
            <tr>
                <td>
                    <div class="divCategoryListContainer">
                        &nbsp;
                    </div>
                </td>
                <td>
                    <div class="imgLinkDiv">
                        <a id="aBack" runat="server">
                            <img src="/images/arrow_back.jpg" alt="" />&nbsp;<%=Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_Back%></a>
                    </div>
                    <div id="divModuleContentMain" runat="server" class="divModuleContentMain">
                        <div>
                            <label for="txtTitle">
                                <span class="moduleLabel">
                                    <%=Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_Title%>:</span><span
                                        class="errorStyle">*</span>
                                <asp:RequiredFieldValidator ID="reqTitle" runat="server" ControlToValidate="txtTitle"
                                    CssClass="errorStyle" Display="Dynamic" ErrorMessage=" <%$ Resources:PressRelease_FrontEnd, PressRelease_SavePressRelease_RequiredMessage %>" />
                            </label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtTitle" runat="server" Width="400px" />
                        </div>
                        <div class="topPad">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="Category">
                                            <span class="moduleLabel">
                                                <%=Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_Category%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="rcbCategoryID" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="topPad">
                            <label for="txtSummary">
                                <span class="moduleLabel">
                                    <%=Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_ShortSummary%>:</span><span
                                        class="errorStyle">*</span><asp:RequiredFieldValidator ID="reqSummary" runat="server"
                                            ControlToValidate="txtSummary" CssClass="errorStyle" Display="Dynamic" ErrorMessage=" <%$ Resources:PressRelease_FrontEnd, PressRelease_SavePressRelease_RequiredMessage %>" />
                            </label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtSummary" runat="server" Width="400px" Rows="4" TextMode="MultiLine" />
                        </div>
                        <div class="topPad">
                            <label for="Body">
                                <span class="moduleLabel">
                                    <%=Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_MainContent%>:</span><span
                                        class="errorStyle">*</span><asp:RequiredFieldValidator ID="reqMainContent" runat="server"
                                            ControlToValidate="txtBody" CssClass="errorStyle" Display="Dynamic" ErrorMessage=" <%$ Resources:PressRelease_FrontEnd, PressRelease_SavePressRelease_RequiredMessage %>" />
                            </label>
                        </div>
                        <div>
                            <telerik:RadEditor ID="txtBody" runat="server" Width="98%">
                            </telerik:RadEditor>
                        </div>
                        <div id="divUploadImage" runat="server" class="topPad">
                            <span class="moduleLabel">
                                <%=Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_UploadImage%>:</span>
                            <br />
                            <span class="grayTextSml_11">
                                <%= Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_UploadImage_Message%><br />
                                <%= Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_UploadImage_Requirements%></span>
                            <asp:CustomValidator ID="customValPressReleaseImage" runat="server" Display="Dynamic"
                                ClientValidationFunction="valPressReleaseImage" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_UploadImage_InvalidFileType%></b>
                            </asp:CustomValidator>
                            <asp:CustomValidator ID="customValFileSizeExceeded" runat="server" Display="Dynamic"
                                OnServerValidate="customValPressReleaseImageSizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_UploadImage_MaxFileSizeExceeded%></b>
                            </asp:CustomValidator>
                            <br />
                            <br />
                            <telerik:RadUpload ID="radUploadPressReleaseImage" runat="server" AllowedFileExtensions=".jpg,.gif,.png"
                                ControlObjectsVisibility="None" InitialFileInputsCount="1">
                            </telerik:RadUpload>
                            <telerik:RadProgressManager ID="radProgressManagerPressReleaseImage" runat="server" />
                            <telerik:RadProgressArea ID="radProgressArePressReleaseImage" runat="server" />
                        </div>
                        <div id="divPressReleaseImage" runat="server" visible="false">
                            <telerik:RadBinaryImage ID="pressReleaseImage" runat="server" CssClass="rightPad"
                                Visible="false" /><br />
                            <asp:LinkButton ID="lnkDeleteImage" runat="server" Visible="false" OnClick="lnkDeleteImage_Click"
                                CausesValidation="false"><b><%=Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_UploadImage_Delete%></b></asp:LinkButton>
                        </div>
                        <asp:TextBox ID="version" runat="server" Visible="false" />
                        <br />
                        <br />
                        <asp:Button ID="btnAddEditPressRelease" runat="server" OnClick="btnAddEditPressRelease_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:PressRelease_FrontEnd, PressRelease_SavePressRelease_ButtonCancel %>"
                            OnClick="btnCancel_Click" CausesValidation="false" />
                        <br />
                        <span class="errorStyle">*</span>
                        <%= Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_DenotesRequired%><br />
                        <br />
                    </div>
                    <div id="divModuleContentSubmitted" runat="server" visible="false">
                        <div>
                            <h2>
                                <%=Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_Submitted_Heading%></h2>
                            <%=Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_Submitted_Body%><br />
                            <br />
                            <a href="Default.aspx"><%=Resources.PressRelease_FrontEnd.PressRelease_SavePressRelease_Submitted_ReturnToPressReleaseModule%></a>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
