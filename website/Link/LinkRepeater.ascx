<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LinkRepeater.ascx.vb"
    Inherits="link_LinkRepeater" %>
<asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
    <ItemTemplate>
        <div id="divLinkCategory" runat="server" visible="false">
            <b>
                <asp:Literal ID="lit_LinkCategory" runat="server" /></b><br />
            <asp:Repeater ID="rptLinks" runat="server" OnItemDataBound="rptLinks_ItemDataBound">
                <ItemTemplate>
                    <br />
                    <div id="divLink_Image" runat="server" class="logoBox" visible="false">
                        <a id="aLink_Image" runat="server">
                            <telerik:RadBinaryImage ID="radBinaryImage" runat="server" />
                        </a>
                        <br />
                        <asp:Literal ID="lit_LinkDescription_Image" runat="server" />
                    </div>
                    <div id="divLink_NoImage" runat="server" visible="false">
                        <a id="aLink_NoImage" runat="server">
                            <asp:Literal ID="lit_LinkName" runat="server" /></a>
                        <br />
                        <asp:Literal ID="lit_LinkDescription_NoImage" runat="server" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <br />
            <hr />
            <br />
            <br />
        </div>
    </ItemTemplate>
</asp:Repeater>
