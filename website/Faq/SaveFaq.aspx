<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    ValidateRequest="false" AutoEventWireup="false" CodeFile="SaveFaq.aspx.vb" Inherits="Faq_SaveFaq" %>

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
                    <div class="imgLinkDiv">
                        <a id="aBack" runat="server">
                            <img src="/images/arrow_back.jpg" alt="" />&nbsp;<%=Resources.Faq_FrontEnd.Faq_SaveFaq_Back%></a>
                    </div>
                    <div id="divModuleContentMain" runat="server" class="divModuleContentMain">
                        <div class="topPad">
                            <label for="txtQuestion">
                                <span class="moduleLabel">
                                    <%=Resources.Faq_FrontEnd.Faq_SaveFaq_Question%>:</span><span class="errorStyle">*</span>
                                <asp:RequiredFieldValidator ID="reqQuestion" runat="server" ControlToValidate="txtQuestion"
                                    CssClass="errorStyle" Display="Dynamic" ErrorMessage=" <%$ Resources:Faq_FrontEnd, Faq_SaveFaq_RequiredMessage %>" />
                            </label>
                        </div>
                        <div>
                            <asp:TextBox ID="txtQuestion" runat="server" Width="400px" />
                        </div>
                        <div class="topPad">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <label for="Category">
                                            <span class="moduleLabel">
                                                <%=Resources.Faq_FrontEnd.Faq_SaveFaq_Category%>:</span></label>
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
                            <label for="txtAnswer">
                                <span class="moduleLabel">
                                    <%=Resources.Faq_FrontEnd.Faq_SaveFaq_Answer%>:</span><span class="errorStyle">*</span><asp:RequiredFieldValidator
                                        ID="reqAnswer" runat="server" ControlToValidate="txtAnswer" CssClass="errorStyle"
                                        Display="Dynamic" ErrorMessage=" <%$ Resources:Faq_FrontEnd, Faq_SaveFaq_RequiredMessage %>" />
                            </label>
                        </div>
                        <div>
                            <telerik:RadEditor ID="txtAnswer" runat="server" Width="98%">
                            </telerik:RadEditor>
                        </div>
                        <asp:TextBox ID="version" runat="server" Visible="false" />
                        <br />
                        <br />
                        <asp:Button ID="btnAddEditFaq" runat="server" OnClick="btnAddEditFaq_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Faq_FrontEnd, Faq_SaveFaq_ButtonCancel %>"
                            OnClick="btnCancel_Click" CausesValidation="false" />
                        <br />
                        <span class="errorStyle">*</span>
                        <%= Resources.Faq_FrontEnd.Faq_SaveFaq_DenotesRequired%><br />
                        <br />
                    </div>
                    <div id="divModuleContentSubmitted" runat="server" visible="false">
                        <div>
                            <h2>
                                <%=Resources.Faq_FrontEnd.Faq_SaveFaq_Submitted_Heading%></h2>
                            <%=Resources.Faq_FrontEnd.Faq_SaveFaq_Submitted_Body%><br />
                            <br />
                            <a href="Default.aspx"><%=Resources.Faq_FrontEnd.Faq_SaveFaq_Submitted_ReturnToFaqModule%></a>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
