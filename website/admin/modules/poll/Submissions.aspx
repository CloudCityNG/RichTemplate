<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Submissions.aspx.vb" Inherits="admin_modules_poll_Submissions" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <uc:header id="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span class="callout"><asp:Literal ID="litBodyHeaderByPollOrPollAnswer" runat="server" />
    </span>
    <br />
    <br />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <img src="/admin/images/back.png" />
            </td>
            <td>
                <a id="aBackToPollsOrPollAnswer" runat="server"><asp:Literal ID="litBackToPollsOrPollAnswers"
                    runat="server" /> </a>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <b>
                    <%= Resources.Poll_Admin.Poll_Submissions_Question%>:</b>&nbsp;<asp:Literal ID="litQuestion"
                        runat="server" />
                <div id="divPollAnswer" runat="server" visible="false">
                    <b>
                        <%= Resources.Poll_Admin.Poll_Submissions_Answer%>:</b>&nbsp;<asp:Literal ID="litAnswer"
                            runat="server" />
                </div>
                <br />
                <br />
                <telerik:RadGrid ID="rgPollSubmissions" runat="server" PageSize="20" >
                    <MasterTableView DataKeyNames="ID">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" ReadOnly="True" SortExpression="ID"
                                UniqueName="ID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="dateTimeStamp" DataType="System.DateTime" HeaderText="<%$ Resources:Poll_Admin, Poll_Submissions_GridDateTimeStamp %>"
                                SortExpression="dateTimeStamp" UniqueName="dateTimeStamp" DataFormatString="{0:d}">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="memberUsername" HeaderText="<%$ Resources:Poll_Admin, Poll_Submissions_GridMemberUsername %>" 
                                SortExpression="memberUsername" UniqueName="memberUsername" EmptyDataText="<%$ Resources:Poll_Admin, Poll_Submissions_GridMemberUsername_NotAvailable %>">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="pollAnswer" HeaderText="<%$ Resources:Poll_Admin, Poll_Submissions_GridAnswer %>"
                                SortExpression="pollAnswer" UniqueName="pollAnswer" HeaderStyle-Width="300px">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Poll_Admin, Poll_Submissions_GridAnswerIsCorrect %>"
                                UniqueName="pollAnswerIsCorrect">
                                <ItemTemplate>
                                    <%#If (Convert.ToBoolean( Eval("pollAnswerIsCorrect").ToString() ), "<span class='activeField'>" & Resources.Poll_Admin.Poll_Submissions_GridAnswerIsCorrectTrue & "</span>", "<span class='inactiveField'>" & Resources.Poll_Admin.Poll_Submissions_GridAnswerIsCorrectFalse & "</span>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
