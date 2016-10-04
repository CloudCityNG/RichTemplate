<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MemberDetail.ascx.vb"
    Inherits="Member_MemberDetail" %>
<div class="divDirMemberHead">
    <div class="floatL">
        <div class="divMemberFullName">
            <h1>
                <asp:Literal ID="litSalutationFirstAndLastName" runat="server" />
            </h1>
            <div id="divMemberTitle" runat="server" visible="false" class="divMemberTitle">
                <br />
                <asp:Literal ID="litTitle" runat="server">Title Goes Here</asp:Literal>
            </div>
        </div>
        <div class="floatL leftPad vcard">
            <a href='vCard.aspx?id=<%= Request.Params("id")%>'>
                <img src="/images/vcard.gif" alt="download" />
            </a>
        </div>
    </div>
    <div class="floatR">
        <a class="moduleViewAll" href="MemberSearch.aspx">(<%= Resources.Member_FrontEnd.Member_MemberDetail_ViewAllMembers%>)</a>
    </div>
</div>
<br class="clear" />
<br />
<div class="divMemberDetail">
    <div class="divMemberDetailFace floatR">
        <telerik:RadBinaryImage ID="radMemberImage" runat="server" Visible='true' ImageUrl="/images/member_noImage_large.gif"
            AlternateText="No Image Available" Width="100" AutoAdjustImageControlSize="false" />
    </div>
    <div id="divLocation" runat="server" visible="false">
        <b>
            <%= Resources.Member_FrontEnd.Member_MemberDetail_Location%></b><br />
        <asp:Literal ID="litLocation" runat="server" />
        <div id="divLocationAddress1" runat="server" visible="false">
            <asp:Literal ID="litLocationAddress1" runat="server" />
        </div>
        <div id="divLocationAddress2" runat="server" visible="false">
            <asp:Literal ID="litLocationAddress2" runat="server" />
        </div>
        <div id="divLocationAddress3" runat="server" visible="false">
            <asp:Literal ID="litLocationAddress3" runat="server" />
        </div>
        <div id="divCityStateZip" runat="server" visible="false">
            <asp:Literal ID="litCityStateZip" runat="server" />
        </div>
        <br /><br />
    </div>
    <div id="divDaytimePhone" runat="server" visible="false">
        <b>
            <%= Resources.Member_FrontEnd.Member_MemberDetail_DaytimePhone%>:&nbsp;</b><asp:Literal
                ID="litDaytimePhone" runat="server" />
    </div>
    <div id="divEveningPhone" runat="server" visible="false">
        <b>
            <%= Resources.Member_FrontEnd.Member_MemberDetail_EveningPhone%>:&nbsp;</b><asp:Literal
                ID="litEveningPhone" runat="server" />
    </div>
    <div id="divEmail" runat="server" visible="false">
        <b>
            <%= Resources.Member_FrontEnd.Member_MemberDetail_Email%>:&nbsp;</b><asp:Literal
                ID="litEmail" runat="server" />
    </div>
    <br />
    <br class="cBoth" />
</div>
