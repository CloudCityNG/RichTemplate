<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pager2.ascx.cs" Inherits="UserControl_Pager" %>
<table>
<tr>
<td>
<%if (NoOfPages != 1)
  { %>
<a href="<%=GetUrlPrevious()%>" class='<%=CurrentPage==1?"datalistSelectedItem":"datalistItem"%>' >&lt;&lt;Prev&nbsp;|</a>
</td>
<td>
<asp:DataList ID="dlPage" runat="server" RepeatDirection="Horizontal">
<ItemTemplate>
<a class='<%#CurrentPage==Convert.ToInt32(Container.DataItem)?"datalistSelectedItem":"datalistItem" %>' href="<%# GetUrl(Container.DataItem.ToString()) %>">&nbsp;<%#Container.DataItem %>&nbsp;</a></ItemTemplate>
     <ItemStyle CssClass="datalistItem" />
     <SelectedItemStyle CssClass="datalistSelectedItem" />
            </asp:DataList>
            </td>
<td>
<a href="<%=GetUrlNext()%>" class='<%=CurrentPage>NoOfPages?"datalistSelectedItem":"datalistItem" %>' >|&nbsp;Next&gt;&gt;</a>
<%} %>
</td>
</tr>
</table>