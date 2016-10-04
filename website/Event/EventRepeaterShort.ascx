<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EventRepeaterShort.ascx.vb"
    Inherits="Event_EventRepeaterShort" %>
<div class="divModuleRepeaterShort">
    <h1>
        <%= Resources.Event_FrontEnd.Event_EventRepeaterShort_Heading%></h1>
                  <p><a href="Event/Default.aspx">View All Events</a></p>
    <asp:Repeater ID="rptEvents" runat="server">
        <ItemTemplate>
            <div class="eventBox">
                <div class="calendarDate">
                    <div class="month"><%# Convert.ToDateTime(Eval("startDate")).ToString("MMM")%></div>
                    <div class="year"><%# Convert.ToDateTime(Eval("startDate")).ToString("dd") %></div>
                </div>
                <div class="event">
                    <h3>
                        <%# Convert.ToDateTime(Eval("startDate")).ToString("ddd, MMM, dd, yyyy") %></h3>
                    <p>
                        <%#CommonWeb.stripHTMLandLimitWordCount(Eval("Title"), 25)%>
                    </p>
                    <br />
                    <a class="read_more" href="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, Eval("externalLinkUrl"), "/Event/Default.aspx?id=" & Eval("eventID") ) %>"
                        target="<%# If(Not Eval("externalLinkUrl") Is DBNull.Value AndAlso Eval("externalLinkUrl").Length > 0, "_blank", "") %>">
                        <img src="/images/common/detailsButton_left.png" />
                        <div>
                            <%# Resources.Event_FrontEnd.Event_EventRepeaterShort_ReadMore  %></div>
                        <img src="/images/common/detailsButton_right.png" />
                    </a>
                </div>
                <div class="cBoth"></div>
            </div>
            
        </ItemTemplate>
    </asp:Repeater>
    <div id="divNoEventsAvailable" runat="server" visible="false">
        <span class="errorStyle">
            <%= Resources.Event_FrontEnd.Event_EventRepeaterShort_NotAvailable%>
        </span>
    </div>
</div>
