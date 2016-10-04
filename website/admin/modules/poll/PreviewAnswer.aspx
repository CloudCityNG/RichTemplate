<%@ Page Title="Rollback" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_OneColumn.master"
    AutoEventWireup="false" CodeFile="PreviewAnswer.aspx.vb" Inherits="admin_modules_poll_previewanswer" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" href="/admin/css/RichTemplate.css" />
    <asp:Panel runat="server" ID="pnlPreview" Width="100%">
        <div style="padding: 10px">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <fieldset class="infoPanel">
                            <div id="divExpired" runat="server" visible="false" class="errorStyle fRight">
                                <h3 class="floatL">
                                    <%= Resources.Poll_Admin.Poll_Preview_Expired%></h3>
                                <div class="floatL leftPad">
                                    <img src='/admin/images/expired.png' />
                                </div>
                            </div>
                            <b>
                                <%= Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_Version%></b>: <asp:Literal
                                    ID="litInformationBox_Version" runat="server" /><br />
                            <b>
                                <%= Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_DateCreated%></b>:
                            <asp:Literal ID="litInformationBox_DateCreated" runat="server" /><br />
                            <b>
                                <%= Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_Author%></b>: <asp:Literal
                                    ID="litInformationBox_AuthorName" runat="server" /><br />
                            <b>
                                <%= Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_Status%></b>: <asp:Literal
                                    ID="litInformationBox_Status" runat="server" /><br />
                            <b>
                                <%= Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_AnswerCorrect%></b>:
                            <asp:Literal ID="litInformationBox_AnswerCorrect" runat="server" /><br />
                            <div id="divInformationBox_PublicationDate" runat="server" visible="false">
                                <b>
                                    <%= Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_PublicationDate%></b>:
                                <asp:Literal ID="litInformationBox_PublicationDate" runat="server" />
                            </div>
                            <div id="divInformationBox_ExpirationDate" runat="server" visible="false">
                                <b>
                                    <%= Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_ExpirationDate%></b>:
                                <asp:Literal ID="litInformationBox_ExpirationDate" runat="server" />
                            </div>
                            <div id="divInformationBox_Description" runat="server" visible="false">
                                <b>
                                    <%= Resources.Poll_Admin.Poll_PreviewAnswer_InformationBox_Description%></b>:<br />
                                <asp:Literal ID="litInformationBox_Description" runat="server" /></div>
                        </fieldset>
                        <br />
                        <div class="errorStyle">
                            <%=Resources.Poll_Admin.Poll_PreviewAnswer_QuestionIsCurrent%></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <hr />
                        <br />
                        <div class="divModuleDetail">
                            <div class="floatL moduleLargeTitle">
                                <asp:Literal ID="litQuestion" runat="server" />
                            </div>
                            <br class="cBoth" />
                            <div class="Date">
                                <i><asp:Literal ID="litPollDate" runat="server" /> </i>
                            </div>
                            <div class="item">
                                <br />
                                <asp:UpdatePanel ID="upPollAnswers" runat="server">
                                    <contenttemplate>
                                        <asp:RadioButtonList ID="rblPollAnswers" runat="server" DataValueField="ID" DataTextField="Answer"
                                            RepeatDirection="Vertical" />
                                        <span class="errorStyle"><asp:Literal ID="litPollSubmissionMsg" runat="server" /></span>
                                    </contenttemplate>
                                </asp:UpdatePanel>
                                <br />
                                <asp:Button ID="btnVote" runat="server" ValidationGroup="valPoll" OnClick="btnVote_Click" /><br />
                                <br />
                                <div class="floatL">
                                    <%=Resources.Poll_Admin.Poll_PreviewAnswer_PostedBy%>: <asp:Literal ID="litPostedBy"
                                        runat="server" /> - <asp:Literal ID="litViewDate" runat="server" />
                                    <%=Resources.Poll_Admin.Poll_PreviewAnswer_PostedBy_DateTimeSeperator%>
                                    <asp:Literal ID="litViewDateTime" runat="server" /></div>
                                <div id="divModuleSearchTagList" runat="server" class="divModuleSearchTagList" visible="false">
                                    <br />
                                    <%=Resources.Poll_Admin.Poll_PreviewAnswer_SearchTagLabel%>:
                                    <asp:Repeater ID="rptSearchTags" runat="server">
                                        <ItemTemplate>
                                            <a href='#'>
                                                <%# Eval("searchTagName") %></a>
                                        </ItemTemplate>
                                        <SeparatorTemplate>
                                            ,
                                        </SeparatorTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <br class="cBoth" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <hr />
                        <br />
                        <br />
                        <asp:Button ID="btnRollBack" runat="server" Text="<%$ Resources:Poll_Admin, Poll_PreviewAnswer_ButtonRollback %>"
                            OnClick="btnRollBack_OnClick" />
                        &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Poll_Admin, Poll_PreviewAnswer_ButtonCancel %>" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlConfirmation" Visible="false">
        <table>
            <tr>
                <td>
                    <span class="pageTitle">
                        <%=Resources.Poll_Admin.Poll_PreviewAnswer_RollBackComplete_Heading%></span><br />
                    <span class="callout">
                        <%=Resources.Poll_Admin.Poll_PreviewAnswer_RollBackComplete_Body%></span><br />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnClose" runat="server" Text="<%$ Resources:Poll_Admin, Poll_PreviewAnswer_ButtonClose %>" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
