<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Address.ascx.vb" Inherits="UserController_Address" %>
<asp:UpdatePanel ID="upAddress" runat="server">
    <contenttemplate>
        <asp:Panel ID="pnlAddress" runat="server" class="pnlAddress">
            <div class="divAddressInner">
                <asp:HiddenField ID="hdnLatitude" runat="server" />
                <asp:HiddenField ID="hdnLongitude" runat="server" />
                <div class="divLocationStreet">
                    <div class="divLocationStreetLbl">
                        <label for="txtLocationStreet">
                            <span class="addressLabel"><%= Resources.Address_UserControl.Address_StreetAddress%>:</span>
                        </label>
                        <div id="divLocationStreet_ReadOnly" runat="server" visible="false">
                            <br class="cBoth" />
                            <b><asp:Literal ID="litLocationStreet" runat="server" /></b>
                        </div>
                        <div id="divLocationStreet" runat="server">
                            <asp:TextBox ID="txtLocationStreet" runat="server" TextMode="MultiLine" Rows="2" />
                            <div id="divReqLocationStreet" runat="server" class="errorStyle" visible="false">
                            *</div>
                            <asp:RequiredFieldValidator ID="reqLocationStreet" runat="server" ControlToValidate="txtLocationStreet"
                            CssClass="errorStyle required" Display="Dynamic" ErrorMessage=" <%$ Resources:Address_UserControl, Address_Required %>" Enabled="false"
                            Visible="false" />
                        </div>
                    </div>
                </div>
                <div class="divLocationStreetSeperator">
                </div>
                <div class="divLocationCity">
                    <label for="txtLocationCity">
                        <span class="addressLabel"><%= Resources.Address_UserControl.Address_City%>:</span>
                    </label>
                    <div id="divLocationCity_ReadOnly" runat="server" visible="false">
                        <br class="cBoth" />
                        <b><asp:Literal ID="litLocationCity" runat="server" /></b>
                    </div>
                    <div id="divLocationCity" runat="server">
                        <asp:TextBox ID="txtLocationCity" runat="server" />
                        <div id="divReqLocationCity" runat="server" class="errorStyle" visible="false">
                            *</div>
                        <asp:RequiredFieldValidator ID="reqLocationCity" runat="server" ControlToValidate="txtLocationCity"
                            CssClass="errorStyle required" Display="Dynamic" ErrorMessage=" <%$ Resources:Address_UserControl, Address_Required %>" Enabled="false"
                            Visible="false" />
                    </div>
                </div>
                <div class="divLocationCityStateZipSeperator">
                </div>
                <div class="divLocationState">
                    <label for="ddlLocationState">
                        <span class="addressLabel"><%= Resources.Address_UserControl.Address_State%>:</span>
                    </label>
                    <div id="divLocationState_ReadOnly" runat="server" visible="false">
                        <br class="cBoth" />
                        <b><asp:Literal ID="litLocationState" runat="server" /></b>
                    </div>
                    <div id="divLocationState" runat="server">
                        <asp:DropDownList ID="ddlLocationState" runat="server" AppendDataBoundItems="true" />
                        <div id="divReqLocationState" runat="server" class="errorStyle" visible="false">
                            *</div>
                        <asp:RequiredFieldValidator ID="reqLocationState" runat="server" ControlToValidate="ddlLocationState"
                            InitialValue="" CssClass="errorStyle required" Display="Dynamic" ErrorMessage=" <%$ Resources:Address_UserControl, Address_Required %>"
                            Enabled="false" Visible="false" />
                    </div>
                </div>
                <div class="divLocationCityStateZipSeperator">
                </div>
                <div class="divLocationZip">
                    <label for="txtLocationZip">
                        <span class="addressLabel"><%= Resources.Address_UserControl.Address_ZipCode%>: </span>
                    </label>
                    <div id="divLocationZip_ReadOnly" runat="server" visible="false">
                        <br class="cBoth" />
                        <b><asp:Literal ID="litLocationZip" runat="server" /></b>
                    </div>
                    <div id="divLocationZip" runat="server">
                        <asp:TextBox ID="txtLocationZip" runat="server" MaxLength="5" />
                    </div>
                </div>
                <div class="divLocationCountrySeperator">
                </div>
                <div class="divLocationCountry">
                    <label for="ddlLocationCountry">
                        <span class="addressLabel"><%= Resources.Address_UserControl.Address_Country%>:</span>
                    </label>
                    <div id="divLocationCountry_ReadOnly" runat="server" visible="false">
                        <br class="cBoth" />
                        <b><asp:Literal ID="litLocationCountry" runat="server" /></b>
                    </div>
                    <div id="divLocationCountry" runat="server">
                        <asp:DropDownList ID="ddlLocationCountry" runat="server" AppendDataBoundItems="true" />
                        <div id="divReqLocationCountry" runat="server" class="errorStyle" visible="false">
                            *</div>
                        <asp:RequiredFieldValidator ID="reqLocationCountry" runat="server" ControlToValidate="ddlLocationCountry"
                            InitialValue="" CssClass="errorStyle required" Display="Dynamic" ErrorMessage=" <%$ Resources:Address_UserControl, Address_Required %>"
                            Enabled="false" Visible="false" />
                    </div>
                </div>
            </div>
            <div id="divAddressValidationContainer" runat="server">
            <asp:Panel ID="pnlAddressValidation" runat="server" class="pnlAddressValidation">
                <div id="divAddressNotValidated" runat="server" class="importantStyle">
                    <%= Resources.Address_UserControl.Address_AddressNotValidated%></div>
                <div id="divAddressValid" runat="server" class="correctStyle" visible="false">
                    <%= Resources.Address_UserControl.Address_AddressValid%>
                </div>
                <br class="cBoth" />
                <asp:LinkButton ID="lnkAddressValidate" runat="server" Text="<%$ Resources:Address_UserControl, Address_ClickHereToValidate %>" CssClass="anchor_validate" ValidationGroup="ValidateAddress" />
                <asp:CustomValidator ID="customValAddressValidate" runat="server" Display="Dynamic" ValidationGroup="ValidateAddress"
                    OnServerValidate="customValAddressValidate_Validate" CssClass="anchor_validate">
                        <br /><b><%=Resources.Address_UserControl.Address_ClickHereToValidate_RequiredStreetAndCountry%></b>
            </asp:CustomValidator>
                <div id="divGoogleAddressPossibilities" runat="server" visible="false" class="divGoogleAddressPossibilities floatL">
                    <ul>
                        <li class="cBoth importantStyle"><%= Resources.Address_UserControl.Address_AddressListHeading%>:</li>
                        <asp:Repeater ID="rptGoogleAddresses" runat="server">
                            <ItemTemplate>
                                <li class="cBoth"><asp:Literal ID="lit_Address" runat="server" />&nbsp;
                                    <asp:LinkButton ID="lnk_Address" runat="server" OnClick="lnk_Address_Click"
                                        CausesValidation="false">(<%= Resources.Address_UserControl.Address_AddressListSelect%>)</asp:LinkButton></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <br />
                    <br class="cBoth" />
                    <asp:LinkButton ID="lnkContinueUnvalidated" runat="server" class="floatL" Text="<%$ Resources:Address_UserControl, Address_ContinueUnValidatedAddress %>"
                        CausesValidation="false" CssClass="anchor_continue_unvalidated" />
                </div>
            </asp:Panel>
            </div>
        </asp:Panel>
    </contenttemplate>
</asp:UpdatePanel>
<br class="cBoth" />
