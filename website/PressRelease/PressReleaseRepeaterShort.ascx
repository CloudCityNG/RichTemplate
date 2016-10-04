<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PressReleaseRepeaterShort.ascx.vb"
    Inherits="PressRelease_PressReleaseRepeaterShort" %>
<div class="divModuleRepeaterShort">
    <h1>
        <%= Resources.PressRelease_FrontEnd.PressRelease_PressReleaseRepeaterShort_Heading%></h1>
        <p><a href="PressRelease/Default.aspx">View All News</a></p>
    <asp:Repeater ID="rptPressReleases" runat="server">
        <ItemTemplate>
            <div class="eventBox">
                <div class="calendarDate">
                    <div class="month"><%# Convert.ToDateTime(Eval("ViewDate")).ToString("MMM")%></div>
                    <div class="year"><%# Convert.ToDateTime(Eval("ViewDate")).ToString("dd") %></div>
                </div>
                <div class="event">
                <h3>
                    <%# Convert.ToDateTime(Eval("ViewDate")).ToString("ddd, MMM, dd, yyyy") %></h3>
                <p>
                    <%#CommonWeb.stripHTMLandLimitWordCount(Eval("Title"), 25)%>
                </p>
                <br />
                <a class="read_more" href="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, Eval("externalLinkUrl"), "/PressRelease/Default.aspx?id=" & Eval("prID") ) %>"
                    target="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, "_blank", "") %>">
                    <img src="/images/common/detailsButton_left.png" />
                    <div>
                        <%# Resources.PressRelease_FrontEnd.PressRelease_PressReleaseRepeaterShort_ReadMore  %></div>
                    <img src="/images/common/detailsButton_right.png" />
                </a>
                </div>
                <div class="cBoth">
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div id="divNoPressReleasesAvailable" runat="server" visible="false">
        <span class="errorStyle">
            <%= Resources.PressRelease_FrontEnd.PressRelease_PressReleaseRepeaterShort_NotAvailable%>
        </span>
    </div>
</div>
