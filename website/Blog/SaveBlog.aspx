<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    ValidateRequest="false" AutoEventWireup="false" CodeFile="SaveBlog.aspx.vb" Inherits="Blog_SaveBlog" %>

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
                            <img src="/images/arrow_back.jpg" alt="" />&nbsp;<%=Resources.Blog_FrontEnd.Blog_SaveBlog_Back%></a>
                    </div>
                    <div id="divModuleContentMain" runat="server" class="divModuleContentMain">
                        <div class="topPad">
                            <label for="txtTitle">
                                <span class="moduleLabel">
                                    <%=Resources.Blog_FrontEnd.Blog_SaveBlog_Title%>:</span><span class="errorStyle">*</span>
                                <asp:RequiredFieldValidator ID="reqTitle" runat="server" ControlToValidate="txtTitle"
                                    CssClass="errorStyle" Display="Dynamic" ErrorMessage=" <%$ Resources:Blog_FrontEnd, Blog_SaveBlog_RequiredMessage %>" />
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
                                                <%=Resources.Blog_FrontEnd.Blog_SaveBlog_Category%>:</span></label>
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
                            <label for="Body">
                                <span class="moduleLabel">
                                    <%=Resources.Blog_FrontEnd.Blog_SaveBlog_MainContent%>:</span><span class="errorStyle">*</span><asp:RequiredFieldValidator
                                        ID="reqMainContent" runat="server" ControlToValidate="txtBody" CssClass="errorStyle"
                                        Display="Dynamic" ErrorMessage=" <%$ Resources:Blog_FrontEnd, Blog_SaveBlog_RequiredMessage %>" />
                            </label>
                        </div>
                        <div>
                            <telerik:RadEditor ID="txtBody" runat="server" Width="98%">
                            </telerik:RadEditor>
                        </div>
                        <asp:TextBox ID="version" runat="server" Visible="false" />
                        <br />
                        <br />
                        <asp:Button ID="btnAddEditBlog" runat="server" OnClick="btnAddEditBlog_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Blog_FrontEnd, Blog_SaveBlog_ButtonCancel %>"
                            OnClick="btnCancel_Click" CausesValidation="false" />
                        <br />
                        <span class="errorStyle">*</span>
                        <%= Resources.Blog_FrontEnd.Blog_SaveBlog_DenotesRequired%><br />
                        <br />
                    </div>
                    <div id="divModuleContentSubmitted" runat="server" visible="false">
                        <div>
                            <h2>
                                <%=Resources.Blog_FrontEnd.Blog_SaveBlog_Submitted_Heading%></h2>
                            <%=Resources.Blog_FrontEnd.Blog_SaveBlog_Submitted_Body%><br />
                            <br />
                            <a href="Default.aspx"><%=Resources.Blog_FrontEnd.Blog_SaveBlog_Submitted_ReturnToBlogModule%></a>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
