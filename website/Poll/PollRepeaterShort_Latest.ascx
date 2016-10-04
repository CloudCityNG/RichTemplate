<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PollRepeaterShort_Latest.ascx.vb"
    Inherits="Polls_PollRepeaterShort_Latest" %>
<asp:UpdatePanel ID="upPoll" runat="server">
    <ContentTemplate>
        <div id="divPoll" runat="server" class="PollRepeater" visible="false">
            <h3>
                <%=Resources.Poll_FrontEnd.Poll_PollRepeaterShort_Latest_Heading%></h3>
            <br />
            <b><asp:Literal ID="litQuestion" runat="server" /></b><br />
            <br />
            <asp:HiddenField ID="hdnPollGuid" runat="server" />
            <asp:RadioButtonList ID="rblPollAnswers" runat="server" DataValueField="ID" DataTextField="Answer"
                RepeatDirection="Vertical" />
            <br />
            <span class="errorStyle"><asp:Literal ID="litPollSubmissionMsg" runat="server" /></span><br />
            <asp:Button ID="btnVote" runat="server" ValidationGroup="valPoll" /><br />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
