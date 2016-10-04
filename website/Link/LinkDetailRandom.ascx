<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LinkDetailRandom.ascx.vb" Inherits="link_LinkDetailRandom" %>

<b>
    <asp:Literal ID="lit_LinkCategory" runat="server" /></b><br />
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
