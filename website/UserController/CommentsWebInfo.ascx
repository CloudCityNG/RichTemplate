<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CommentsWebInfo.ascx.vb"
    Inherits="UserController_CommentsWebInfo" %>
<br />
<a name="addcomment"></a>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <img height="24" width="24" src="/images/business_user.png" alt="comment" />
        </td>
        <td>
            <%= Resources.Comment_UserControl.Comment_CommentsWebInfo_Heading%>
            (<asp:Literal ID="lit_CommentCount" runat="server" />)
        </td>
    </tr>
</table>
<a id="comments"></a>
<table id="tbl_AddComment" runat="server" cellspacing="0" cellpadding="0" visible="false">
    <tr>
        <td>
            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="350" Rows="3" />
            <asp:RequiredFieldValidator ID="reqComment" runat="server" ControlToValidate="txtComment"
                CssClass="errorStyle" SetFocusOnError="True"><br /><%= Resources.Comment_UserControl.Comment_CommentsWebInfo_Required%></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <table cellpadding="2" cellspacing="2" id="tbl_RatingTotal" runat="server">
                <tr>
                    <td>
                        <%= Resources.Comment_UserControl.Comment_CommentsWebInfo_Rating%>:
                    </td>
                    <td>
                        <telerik:RadRating ID="rrComment" runat="server" ItemCount="5" Value="0" SelectionMode="Continuous"
                            Precision="Half" Orientation="Horizontal" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <div id="divCaptchaSubmitComment" runat="server" visible="false">
                <br />
                <%= Resources.Comment_UserControl.Comment_CommentsWebInfo_CaptchaCode_Instructions%>
                <telerik:RadCaptcha ID="radCaptchaSubmitComment" runat="server" ErrorMessage=" <%$ Resources:Comment_UserControl, Comment_CommentsWebInfo_CaptchaCode_RequiredMessage %>"
                    Display="Dynamic" CaptchaImage-LineNoise="Low" CaptchaImage-TextChars="Letters"
                    CaptchaTextBoxLabel="" Visible="false" />
            </div>
            <br />
            <asp:Button ID="btn_AddComment" runat="server" Text="<%$ Resources:Comment_UserControl, Comment_CommentsWebInfo_AddComment %>" />
        </td>
    </tr>
</table>
<br />
<table id="tbl_CommentMessage" runat="server" visible="false" cellpadding="0" cellspacing="0">
    <tr>
        <td>
        </td>
        <td>
            <span style="color: Red;"><b>
                <%= If(_MemberID > 0, Resources.Comment_UserControl.Comment_CommentsWebInfo_CommentAdded, Resources.Comment_UserControl.Comment_CommentsWebInfo_CommentPending)%>
            </b></span>
        </td>
    </tr>
</table>
<table id="tbl_NoComments" runat="server" cellpadding="0" cellspacing="0">
    <tr>
        <td>
        </td>
        <td>
            <b>
                <%= Resources.Comment_UserControl.Comment_CommentsWebInfo_NoCommentsAdded%></b>
        </td>
    </tr>
</table>
<table id="tbl_AddComment_NotLoggedIn" runat="server" cellspacing="0" cellpadding="0"
    visible="false">
    <tr>
        <td>
            <b>
                <%= Resources.Comment_UserControl.Comment_CommentsWebInfo_LoginToAddComment%></b>
        </td>
    </tr>
</table>
<div id="divComments" runat="server">
    <table cellpadding="0" cellspacing="0">
        <asp:Repeater ID="rpt_Comment" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <img height="48" width="48" src="/images/business_user.png" alt="comment" />
                    </td>
                    <td colspan="2">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <b>
                                        <%#Container.ItemIndex + 1%>.&nbsp;</span></b>
                                </td>
                                <td style="width: 300px;">
                                    <b>
                                        <%# If( (Not Eval("FirstName") Is DBNull.Value) AndAlso (Not Eval("LastName") Is DBNull.Value) , Eval("FirstName") & " " & Eval("LastName"), Resources.Comment_UserControl.Comment_CommentsWebInfo_AnonymousSubmission)%></b>
                                    (<%# Eval("DateCreated", "{0:dd MMM yyyy}") %></span><span><%# Eval("DateCreated", "{0:hh:mm tt}")%></span>)
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <%#Eval("Comment")%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <telerik:RadRating runat="server" ItemCount="5" ReadOnly="true" Value='<%# Eval("Rating") %>'
                                        SelectionMode="Continuous" Precision="Half" Orientation="Horizontal" />
                                </td>
                            </tr>
                        </table>
                        <hr />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>
