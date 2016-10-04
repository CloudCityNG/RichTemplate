<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/AdminRichTemplateMasterPage.master"
    AutoEventWireup="false" CodeFile="Answers.aspx.vb" Inherits="admin_modules_poll_Answers" %>

<%@ Register TagPrefix="uc" TagName="Header" Src="~/admin/usercontrols/header.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <uc:Header ID="ucHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadScriptBlock runat="server" ID="scriptBlock">
        <script language="JavaScript" src="/js/jquery-1.4.2.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            <!--

            $(function () {
                $(".rgPollAnswers tr").hover(
                    function () {
                        $(this).addClass("highlight");
                    },
                    function () {
                        $(this).removeClass("highlight");
                    }
                    )
            }
            )
            function onRowDragStarted(sender, args) {
                var controlClassName = args._domEvent.target.className;
                if (controlClassName.indexOf("dndUpDown") < 0) {
                    //Only allows drag 'n drop from the drag 'n drop column ONLY
                    args.set_cancel(true);
                }
            }

                    -->
        </script>
    </telerik:RadScriptBlock>
    <span class="callout">
        <%= Resources.Poll_Admin.Poll_Answers_BodyHeading%>
    </span>
    <br />
    <br />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <img src="/admin/images/back.png" />
            </td>
            <td>
                <a href="Default.aspx">
                    <%= Resources.Poll_Admin.Poll_Answers_BackToPolls%></a>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <b>
                    <%= Resources.Poll_Admin.Poll_Answers_Question%>:</b>&nbsp;<asp:Literal ID="litQuestion"
                        runat="server" /><span id="spanAnswersRandomized" runat="server" visible="false"
                            class="errorStyle">&nbsp;<%= Resources.Poll_Admin.Poll_Answers_AnswersRandomized%></span><br />
                <br />
                <telerik:RadGrid ID="rgPollAnswers" runat="server" PageSize="20" CssClass="rgPollAnswers">
                    <MasterTableView DataKeyNames="ID" CommandItemDisplay="Top">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-VerticalAlign="Middle" HeaderStyle-Width="25px" UniqueName="DragAndDrop">
                                <ItemTemplate>
                                    <div class="dndUpDown">
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" ReadOnly="True" SortExpression="ID"
                                UniqueName="ID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PublicationDate" DataType="System.DateTime" HeaderText="<%$ Resources:Poll_Admin, Poll_Answers_GridPublicationDate %>"
                                SortExpression="PublicationDate" UniqueName="PublicationDate" DataFormatString="{0:d}"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="ExpirationDate" SortExpression="ExpirationDate"
                                Visible="false" HeaderStyle-Wrap="false" HeaderText="<%$ Resources:Poll_Admin, Poll_Answers_GridExpirationDate%>">
                                <ItemTemplate>
                                    <%# If(Eval("ExpirationDate") IsNot DBNull.Value, Convert.ToDateTime(Eval("ExpirationDate")).ToString("d") & If(Convert.ToDateTime(Eval("ExpirationDate")) < DateTime.Now, "<span class='gridImg' title='" & Resources.Poll_Admin.Poll_Default_Grid_Expired & "'><img src='/admin/images/expired.png'/></span>", ""), "")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Answer" HeaderText="<%$ Resources:Poll_Admin, Poll_Answers_GridAnswer %>"
                                SortExpression="Answer" UniqueName="Answer" HeaderStyle-Width="300px">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Poll_Admin, Poll_Answers_GridActive %>"
                                UniqueName="IsCorrect">
                                <ItemTemplate>
                                    <%# If((Convert.ToBoolean(Eval("Status").ToString())) And (Eval("ExpirationDate") Is DBNull.Value OrElse DateTime.Compare(Convert.ToDateTime(Eval("ExpirationDate").ToString()), DateTime.Now) > 0), "<span class='activeField'>" & Resources.Poll_Admin.Poll_Answers_GridActiveTrue & "</span>", "<span class='inactiveField'>" & Resources.Poll_Admin.Poll_Answers_GridActiveFalse & "</span>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="<%$ Resources:Poll_Admin, Poll_Answers_GridAnswerIsCorrect %>"
                                UniqueName="IsCorrect">
                                <ItemTemplate>
                                    <%#If(Convert.ToBoolean(Eval("IsCorrect").ToString()), "<span class='activeField'>" & Resources.Poll_Admin.Poll_Answers_GridAnswerIsCorrectTrue & "</span>", "<span class='inactiveField'>" & Resources.Poll_Admin.Poll_Answers_GridAnswerIsCorrectFalse & "</span>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="submissionCount" SortExpression="submissionCount"
                                UniqueName="submissionCount" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridHyperLinkColumn UniqueName="submissions" DataNavigateUrlFields="PollID,ID"
                                DataNavigateUrlFormatString="Submissions.aspx?pollID={0}&pollAnswerID={1}" HeaderText="<%$ Resources:Poll_Admin, Poll_Answers_GridSubmissions %>"
                                ItemStyle-Wrap="false" ItemStyle-VerticalAlign="Middle" SortExpression="submissionCount">
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridTemplateColumn UniqueName="editPollAnswer" DataField="ID" ItemStyle-Width="50px"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Poll_Admin, Poll_Answers_GridEdit %>">
                                <ItemTemplate>
                                    <a id="aPollAnswerEdit" runat="server">
                                        <%= Resources.Poll_Admin.Poll_Answers_GridEdit%></a>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Poll_Admin, Poll_Answers_GridDelete %>">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# Eval("SiteID") = SiteDAL.GetCurrentSiteID_Admin() %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <div style="padding-left: 10px; height: 20px; padding-top: 5px">
                                <a href='<%= "editAddAnswer.aspx?pollID=" & Request.QueryString("pollID") %>'>
                                    <img style="border: 0px" alt="" src="/admin/images/AddRecord.gif" />
                                    <%= Resources.Poll_Admin.Poll_Answers_Grid_AddNewEntry%></a>
                            </div>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <ClientSettings AllowRowsDragDrop="True" ClientEvents-OnRowDragStarted="onRowDragStarted">
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" />
                        <ClientEvents OnRowDragStarted="onRowDragStarted" />
                    </ClientSettings>
                </telerik:RadGrid>
                <div id="divDragDropMessage" runat="server" class="fLeft">
                    <span class="graySubText">(use drag and drop capability on left to order poll answers)</span>
                </div>
                <div class="divFooter">
                    <div class="fRight">
                        <asp:Button ID="btnDeleteLive" runat="server" Text="<%$ Resources:Poll_Admin, Poll_Answers_GridDeleteButton %>" OnClick="btnDeleteLive_Click" /></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
