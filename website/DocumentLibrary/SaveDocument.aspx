<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    ValidateRequest="false" AutoEventWireup="false" CodeFile="SaveDocument.aspx.vb"
    Inherits="DocumentLibrary_SaveDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divModuleContent">
        <table width="100%">
            <tr>
                <td>
                    <div class="divCategoryListContainer">
                        &nbsp;
                    </div>
                </td>
                <td>
                    <script type="text/javascript">
                        function valDocument(source, arguments) {
                            arguments.IsValid = getRadUpload('<%= radUpDocument.ClientID %>').validateExtensions();
                        }
                    </script>
                    <div class="imgLinkDiv">
                        <a id="aBack" runat="server">
                            <img src="/images/arrow_back.jpg" alt="" />&nbsp;<%=Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_Back%></a>
                    </div>
                    <div id="divModuleContentMain" runat="server" class="divModuleContentMain">
                        <div id="divDocumentFileAndLocation" runat="server" visible="false">
                            <div class="docImage floatL">
                                <img id="imgFileType" runat="server" />
                            </div>
                            <div class="docInfo floatL">
                                <asp:Literal ID="litDocumentFileLocation" runat="server" />
                            </div>
                            <div class="floatL leftPad">
                                <asp:Literal ID="litDocumentFileSize" runat="server" />
                            </div>
                            <br class="cBoth" />
                        </div>
                        <br />
                        <div>
                            <span class="moduleLabel">
                                <%=Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_Title%>:</span><span
                                    class="errorStyle">*</span><asp:RequiredFieldValidator ID="reqFileTitle" runat="server"
                                        ControlToValidate="txtFileTitle" CssClass="errorStyle" Display="Dynamic" ErrorMessage=" <%$ Resources:DocumentLibrary_FrontEnd, DocumentLibrary_SaveDocument_RequiredMessage %>" />
                        </div>
                        <div>
                            <asp:TextBox ID="txtFileTitle" runat="server" Width="500px"></asp:TextBox>
                        </div>
                        <br />
                        <div>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="Category">
                                            <span class="moduleLabel">
                                                <%=Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_Category%>:</span></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="rcbCategoryID" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div>
                            <span class="moduleLabel">
                                <%=Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_Description%>:</span><span
                                    class="errorStyle">*</span><asp:RequiredFieldValidator ID="reqFileDescription" runat="server"
                                        ControlToValidate="txtFileDescription" CssClass="errorStyle" Display="Dynamic"
                                        ErrorMessage=" <%$ Resources:DocumentLibrary_FrontEnd, DocumentLibrary_SaveDocument_RequiredMessage %>" />
                        </div>
                        <div>
                            <telerik:RadEditor ID="txtFileDescription" runat="server">
                            </telerik:RadEditor>
                        </div>
                        <br />
                        <div id="divUploadFile" runat="server" visible="false">
                            <span class="moduleLabel">
                                <%=Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_UploadDocument%>:</span>
                            <br />
                            <span class="grayTextSml_11">
                                <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_UploadDocument_Message%><br />
                                <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_UploadDocument_Requirements%></span>
                            <telerik:RadUpload ID="radUpDocument" runat="server" AllowedFileExtensions=".csv,.doc,.docx,.txt,.jpg,.mht,.pdf,.ppt,.odt,.ods,.rtf,.wpd,.wps,.xls,.xlsx,.zip"
                                ControlObjectsVisibility="None" InitialFileInputsCount="1">
                            </telerik:RadUpload>
                            <telerik:RadProgressManager ID="radProgressManagerDocument" runat="server" />
                            <telerik:RadProgressArea ID="radProgressAreaDocument" runat="server" />
                            <asp:CustomValidator ID="customValDocumentRequired" runat="server" Display="Dynamic"
                                OnServerValidate="customValDocumentRequired_Validate" CssClass="errorStyle">
                        &nbsp;<b><%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_UploadDocument_DocumentRequiredMessage%></b>
                            </asp:CustomValidator>
                            <asp:CustomValidator ID="customValDocument" runat="server" Display="Dynamic" ClientValidationFunction="valDocument"
                                CssClass="errorStyle">
                        &nbsp;<br /><b><%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_UploadDocument_InvalidFileType%></b>
                            </asp:CustomValidator>
                            <asp:CustomValidator ID="customValFileSizeExceeded" runat="server" Display="Dynamic"
                                OnServerValidate="customValFileSizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<br /><b><%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_UploadDocument_MaxFileSizeExceeded%></b>
                            </asp:CustomValidator>
                            <br />
                        </div>
                        <br />
                        <asp:Button ID="btnAddEditDocument" runat="server" OnClick="btnAddEditDocument_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            CausesValidation="false" />
                        <br />
                        <span class="errorStyle">*</span>
                        <%= Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_DenotesRequired%><br />
                        <br />
                    </div>
                    <div id="divModuleContentSubmitted" runat="server" visible="false">
                        <div>
                            <h2>
                                <%=Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_Submitted_Heading%></h2>
                            <%=Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_Submitted_Body%><br />
                            <br />
                            <a href="Default.aspx">
                                <%=Resources.DocumentLibrary_FrontEnd.DocumentLibrary_SaveDocument_Submitted_ReturnToDocumentModule%></a>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
