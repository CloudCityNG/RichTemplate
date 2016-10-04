<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FaqRepeaterRotator.ascx.vb"
    Inherits="Faq_FaqRepeaterRotator" %>
<div style="width: 400px; border: solid 1px red;">
    <div style="background-color: #E33C3C;">
        <h2>
            <a style="color: Black; text-decoration: none;" id="aPrev" href="javascript:void(0);"
                onclick="$find('<%= RadRotator1.ClientID %>').showNext(Telerik.Web.UI.RotatorScrollDirection.Right);">
                &lt;&lt;<%=Resources.Faq_FrontEnd.Faq_FaqRepeaterRotator_Previous%></a> <a style="color: Black; text-decoration: none; float: right;" id="aNext"
                    href="javascript:void(0);" onclick="$find('<%= RadRotator1.ClientID %>').showNext(Telerik.Web.UI.RotatorScrollDirection.Left);">
                    <%=Resources.Faq_FrontEnd.Faq_FaqRepeaterRotator_Next%>&gt;&gt;</a>
        </h2>
    </div>
    <telerik:RadRotator ID="RadRotator1" runat="server" Width="100%" Height="170" ScrollDirection="left"
        ScrollDuration="500" FrameDuration="10000" ItemWidth="400px" ItemHeight="170px"
        RotatorType="FromCode">
        <ControlButtons LeftButtonID="aPrev" RightButtonID="aNext" />
        <ItemTemplate>
            <div style="padding: 5px 5px 5px 5px;">
                <p>
                    <b><%=Resources.Faq_FrontEnd.Faq_FaqRepeaterRotator_Question%>:
                        <%#DataBinder.Eval(Container.DataItem, "question")%></b></p>
                <p>
                    <b><%=Resources.Faq_FrontEnd.Faq_FaqRepeaterRotator_Answer%>:</b>
                    <%#DataBinder.Eval(Container.DataItem, "answer")%></p>
            </div>
        </ItemTemplate>
    </telerik:RadRotator>
    <br />
</div>
<a href="/Faq/Default.aspx"><%=Resources.Faq_FrontEnd.Faq_FaqRepeaterRotator_ReadMore%></a>