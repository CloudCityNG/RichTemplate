<%@ Register TagPrefix="uc" Assembly="GMaps" Namespace="Subgurim.Controles" %>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="GoogleMap.ascx.vb" Inherits="UserController_GoogleMap" %>
<div id="div_GoogleMap">
    <uc:GMap ID="ucGoogleMap" runat="server" enableHookMouseWheelToZoom="false" enableServerEvents="true" />
</div>
