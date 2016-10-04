<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LoginControl.ascx.vb"
    Inherits="UserController_LoginControl" %>
<div id="divPublicSectionLoggedIn" runat="server" visible="false" class="divLoginControl">
    <div class="loginText">
        <ul>
            <li><a href="/member/updateprofile.aspx" class="rmLink rmRootLink"><asp:Literal ID="lit_MemberName_Public_Text"
                runat="server" /></a></li>
            <li><span id="spanLogout" runat="server" visible="false">&nbsp;|</li>
                <li><a href="/logout/" class="rmLink rmRootLink">
                    <%= Resources.Login_UserControl.LoginUserControl_Text_Logout%></a></span></li>
        </ul>
    </div>
</div>
<div id="divPublicNotLoggedIn" runat="server" visible="false" class="divLoginControl">
    <div class="loginText">
    <ul>
        <li><a href="/login/" class="rmLink rmRootLink">
            <%= Resources.Login_UserControl.LoginUserControl_Text_Login%></a></li>
    </ul>
    </div>
</div>
