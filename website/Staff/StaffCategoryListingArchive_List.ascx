<%@ Control Language="VB" AutoEventWireup="false" CodeFile="StaffCategoryListingArchive_List.ascx.vb"
    Inherits="staff_StaffCategoryListingArchive_List" %>
<div class="divCategoryList floatL">
    <asp:Repeater ID="catRepeater" runat="server">
        <HeaderTemplate>
            <div class="categoryHeader">
               <%= Resources.Staff_FrontEnd.Staff_StaffCategoryListingArchive_List_CategoryListingLabel%></div>
            <ul class="ulCategoryList">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <a class="cat_list" href="?catid=<%#(Eval("categoryID")) %>&archive=1"><asp:Literal
                    ID="litCategoryName" runat="server" Text='<%#(Eval("CategoryName"))%>' /></a>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            <li id="liUncategorized" runat="server" visible="false">
                <a class="cat_link" href="?catid=0&archive=1"><%= Resources.Staff_FrontEnd.Staff_StaffCategoryListingArchive_List_Uncategorized%></a>
            </li>
            <li>
                <a class="cat_link" href="default.aspx?archive=1"><%= Resources.Staff_FrontEnd.Staff_StaffCategoryListingArchive_List_ShowAll%></a>
            </li>
            </ul>
            <br class="clear" />
        </FooterTemplate>
    </asp:Repeater>
</div>
