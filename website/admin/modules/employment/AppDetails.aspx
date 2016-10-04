<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="AppDetails.aspx.vb" Inherits="admin_modules_employment_AppDetails" %>
        <%@ Register TagPrefix="uc" TagName="Address" Src="~/UserController/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <uc:header id="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function valDocument1(source, arguments) {
            arguments.IsValid = getRadUpload('<%= RadUploadDocument1.ClientID %>').validateExtensions();
        }
        function valDocument2(source, arguments) {
            arguments.IsValid = getRadUpload('<%= RadUploadDocument2.ClientID %>').validateExtensions();
        }
        function valDocument3(source, arguments) {
            arguments.IsValid = getRadUpload('<%= RadUploadDocument3.ClientID %>').validateExtensions();
        }
    </script>
    <table>
        <tr>
            <td>
                <span class="callout"><%=Resources.Employment_Admin.Employment_AddEditApplicants_HeadingBody%></span><br />
                <br />
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <img src="/admin/images/back.png" />
                        </td>
                        <td>
                            <a id="aBackToRegistrations" runat="server"><%=Resources.Employment_Admin.Employment_AddEditApplicants_BackToApplicants%></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="MainContent">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <label for="txtMemberID">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_MemberID%>:</span></label>&nbsp;
                    <asp:TextBox ID="txtMemberID" runat="server" Width="30" />
                    <asp:CustomValidator ID="customValMemberExist" runat="server" Display="Dynamic" OnServerValidate="customValMemberExist_Validate"
                        CssClass="errorStyle">
                        &nbsp;<br /><b><%=Resources.Employment_Admin.Employment_AddEditApplicants_MemberDoesNotExist%></b>
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="Status">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_Status%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="Status" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="True" Text="<%$ Resources:Employment_Admin, Employment_AddEditApplicants_StatusActive %>"></asp:ListItem>
                        <asp:ListItem Value="False" Text="<%$ Resources:Employment_Admin, Employment_AddEditApplicants_StatusWithdrawn %>"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2">
                                <label>
                                    <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_Salutation%>:</span></label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlSalutation" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label for="txtFirstName">
                                    <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_FirstName%></span><span class="requiredStar">*</span></label><asp:RequiredFieldValidator
                                        ID="reqFirstName" runat="server" ErrorMessage="<%$ Resources:Employment_Admin, Employment_AddEditApplicants_RequiredMessage %>" ControlToValidate="txtFirstName"
                                        CssClass="errorStyle" Display="Dynamic" />
                            </td>
                            <td class="leftPad">
                                <label for="txtLastName">
                                    <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_LastName%></span><span class="requiredStar">*</span></label><asp:RequiredFieldValidator
                                        ID="reqLastName" runat="server" ErrorMessage="<%$ Resources:Employment_Admin, Employment_AddEditApplicants_RequiredMessage %>" ControlToValidate="txtLastName"
                                        CssClass="errorStyle" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtFirstName" runat="server" />
                            </td>
                            <td class="leftPad">
                                <asp:TextBox ID="txtLastName" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <label for="txtEmailAddress">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_EmailAddress%>:</span><span class="requiredStar">*</span>
                    </label>
                    <asp:RequiredFieldValidator ID="reqEmailAddress" runat="server" ErrorMessage="<%$ Resources:Employment_Admin, Employment_AddEditApplicants_RequiredMessage %>"
                        ControlToValidate="txtEmailAddress" CssClass="errorStyle" Display="Dynamic" /><asp:RegularExpressionValidator
                            ID="regExpEmailAddress" ControlToValidate="txtEmailAddress" ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([,])*)*"
                            ErrorMessage=" <%$ Resources:Employment_Admin, Employment_AddEditApplicants_InvalidEmail %>" Display="dynamic" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="tb200" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <label for="txtPhoneNumber">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_BestContactNumber%>:</span><span class="requiredStar">*</span>
                    </label>
                    <asp:RequiredFieldValidator ID="reqPhoneNumber" runat="server" ControlToValidate="txtPhoneNumber"
                        CssClass="errorStyle" Display="Dynamic" ErrorMessage="<%$ Resources:Employment_Admin, Employment_AddEditApplicants_RequiredMessage %>" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtPhoneNumber" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdEmploymentAddressSignup">
                    <br />
                    <uc:Address ID="ucAddress" runat="server" AddressLayout="Horizontal" AddressValidationLayout="SideBySide_AbsoluteListPosition" Required="false"/>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <label for="txtCoverLetter">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_Coverletter%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtCoverLetter" runat="server" TextMode="MultiLine" Width="400px"
                        Rows="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtResume">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_Resume%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadEditor ID="txtResume" runat="server" Width="98%" />
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtYearsExperience">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_ExpYears%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtYearsExperience" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtLastTitle">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_JobTitle%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtLastTitle" runat="server" CssClass="tb200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtEduLevel">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_EducationLevel%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtEduLevel" runat="server" TextMode="MultiLine" Rows="4" CssClass="tb200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtProjExpertise">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_ProjectExpertise%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtProjExpertise" runat="server" TextMode="MultiLine" Rows="4" CssClass="tb200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtSkills">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_Skills%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtSkills" runat="server" TextMode="MultiLine" Rows="4" CssClass="tb200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="txtSalary">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_SalaryRange%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtSalary" runat="server" CssClass="tb100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="dtStartDate">
                        <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_StartDate%>:</span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadDatePicker ID="startDate" runat="server"><DateInput DateFormat="d"></DateInput></telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <br />
                                <div id="divDocumentFileAndLocation1" runat="server" class="divDocumentFileAndLocation"
                                    visible="false">
                                    <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_CurrentlyUploadedDocuments%>:</span>
                                    <br class="cBoth" />
                                    <img id="imgFileType1" runat="server" />
                                    <span class='fileTitle'>
                                        <asp:LinkButton ID="lnkDocument1" runat="server" /></span>
                                    <span class='fileSize'><asp:Literal ID="litDocumentFileSize1" runat="server" /></span>
                                    <span class="leftPad floatL">
                                        <asp:LinkButton ID="lnkDeleteDocument1" runat="server"
                                            CausesValidation="false"><b><%=Resources.Employment_Admin.Employment_Applicants_GridDeleteButton%></b></asp:LinkButton></span>
                                    <br class="cBoth" />
                                    <br />
                                </div>
                                <div id="divDocumentFileAndLocation2" runat="server" class="divDocumentFileAndLocation"
                                    visible="false">
                                    <img id="imgFileType2" runat="server" />
                                    <span class='fileTitle'>
                                        <asp:LinkButton ID="lnkDocument2" runat="server" /></span>
                                    <span class='fileSize'><asp:Literal ID="litDocumentFileSize2" runat="server" /></span>
                                    <span class="leftPad floatL">
                                        <asp:LinkButton ID="lnkDeleteDocument2" runat="server"
                                            CausesValidation="false"><b><%=Resources.Employment_Admin.Employment_Applicants_GridDeleteButton%></b></asp:LinkButton></span>
                                    <br class="cBoth" />
                                    <br />
                                </div>
                                <div id="divDocumentFileAndLocation3" runat="server" class="divDocumentFileAndLocation"
                                    visible="false">
                                    <img id="imgFileType3" runat="server" />
                                    <span class='fileTitle'>
                                        <asp:LinkButton ID="lnkDocument3" runat="server" /></span>
                                    <span class='fileSize'><asp:Literal ID="litDocumentFileSize3" runat="server" /></span>
                                    <span class="leftPad floatL">
                                        <asp:LinkButton ID="lnkDeleteDocument3" runat="server"
                                            CausesValidation="false"><b><%=Resources.Employment_Admin.Employment_Applicants_GridDeleteButton%></b></asp:LinkButton></span>
                                    <br class="cBoth" />
                                    <br />
                                </div>

                                <span class="moduleLabel"><%=Resources.Employment_Admin.Employment_AddEditApplicants_UploadDoc%>:</span>
                                <br />
                                <span class="graySubText"><%=Resources.Employment_Admin.Employment_AddEditApplicants_UploadDoc_Message%><br />
                                   <%=Resources.Employment_Admin.Employment_AddEditApplicants_UploadDoc_Requirements%>
                                    <br />
                                   <%=Resources.Employment_Admin.Employment_AddEditApplicants_UploadDoc_MaxFileSize%></span>
                                <div id="divUploadDocumentMaxReached" runat="server" visible="false">
                                    <span class="errorStyle"><b><%=Resources.Employment_Admin.Employment_AddEditApplicants_UploadDoc_ErrorMessage%></b></span>
                                </div>
                                <div id="divUploadDocument1" runat="server"><br />
                                    <telerik:RadUpload ID="RadUploadDocument1" runat="server" AllowedFileExtensions=".csv,.doc,.docx,.gif,.txt,.jpg,.mht,.pdf,.png,.ppt,.odt,.ods,.rtf,.wpd,.wps,.xls,.xlsx,.zip"
                                        ControlObjectsVisibility="None" InitialFileInputsCount="1">
                                    </telerik:RadUpload>
                                    <asp:CustomValidator ID="customValDocument1" runat="server" Display="Dynamic" ClientValidationFunction="valDocument1"
                                        CssClass="errorStyle">
                    &nbsp;<b><%=Resources.Employment_Admin.Employment_AddEditApplicants_UploadDoc_InvalidFileType%></b>
                                    </asp:CustomValidator>
                                    <asp:CustomValidator ID="customValDocument1SizeExceeded" runat="server" Display="Dynamic"
                                        OnServerValidate="customValDocument1SizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<b><%=Resources.Employment_Admin.Employment_AddEditApplicants_UploadDoc_MaxFileSizeExceeded%></b>
                                    </asp:CustomValidator>
                                    <telerik:RadProgressManager ID="rpManagerDocument1" runat="server" />
                                    <telerik:RadProgressArea ID="rpDocument1" runat="server"  />
                                </div>
                                <div id="divUploadDocument2" runat="server"><br />
                                    <telerik:RadUpload ID="RadUploadDocument2" runat="server" AllowedFileExtensions=".csv,.doc,.docx,.gif,.txt,.jpg,.mht,.pdf,.png,.ppt,.odt,.ods,.rtf,.wpd,.wps,.xls,.xlsx,.zip"
                                        ControlObjectsVisibility="None" InitialFileInputsCount="1">
                                    </telerik:RadUpload>
                                    <asp:CustomValidator ID="customValDocument2" runat="server" Display="Dynamic" ClientValidationFunction="valDocument2"
                                        CssClass="errorStyle">
				                     &nbsp;<b><%=Resources.Employment_Admin.Employment_AddEditApplicants_UploadDoc_InvalidFileType%></b>
                                    </asp:CustomValidator>
                                    <asp:CustomValidator ID="customValDocument2SizeExceeded" runat="server" Display="Dynamic"
                                        OnServerValidate="customValDocument2SizeExceeded_Validate" CssClass="errorStyle">
				                        &nbsp;<b><%=Resources.Employment_Admin.Employment_AddEditApplicants_UploadDoc_MaxFileSizeExceeded%></b>
                                    </asp:CustomValidator>
                                    <telerik:RadProgressManager ID="rpManagerDocument2" runat="server" />
                                    <telerik:RadProgressArea ID="rpDocument2" runat="server" />
                                </div>
                                <div id="divUploadDocument3" runat="server"><br />
                                    <telerik:RadUpload ID="RadUploadDocument3" runat="server" AllowedFileExtensions=".csv,.doc,.docx,.gif,.txt,.jpg,.mht,.pdf,.png,.ppt,.odt,.ods,.rtf,.wpd,.wps,.xls,.xlsx,.zip"
                                        ControlObjectsVisibility="None" InitialFileInputsCount="1">
                                    </telerik:RadUpload>
                                    <asp:CustomValidator ID="customValDocument3" runat="server" Display="Dynamic" ClientValidationFunction="valDocument3"
                                        CssClass="errorStyle">
                     &nbsp;<b><%=Resources.Employment_Admin.Employment_AddEditApplicants_UploadDoc_InvalidFileType%></b>
                                    </asp:CustomValidator>
                                    <asp:CustomValidator ID="customValDocument3SizeExceeded" runat="server" Display="Dynamic"
                                        OnServerValidate="customValDocument3SizeExceeded_Validate" CssClass="errorStyle">
                        &nbsp;<b><%=Resources.Employment_Admin.Employment_AddEditApplicants_UploadDoc_MaxFileSizeExceeded%></b>
                                    </asp:CustomValidator>
                                    <telerik:RadProgressManager ID="rpManagerDocument3" runat="server" />
                                    <telerik:RadProgressArea ID="rpDocument3" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnAddEdit" runat="server" Text="" CausesValidation="true" />
        <asp:Button ID="cancelButton" runat="server" Text="<%$ Resources:Employment_Admin, Employment_AddEditApplicants_ButtonCancel %>" CausesValidation="false" />
        <br />
        <span class="requiredStar">*</span><%=Resources.Employment_Admin.Employment_AddEditApplicants_DenotesRequired%><br />
    </div>
</asp:Content>
