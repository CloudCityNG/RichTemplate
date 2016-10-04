<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/RichTemplateMasterPage_Modules.master"
    AutoEventWireup="false" CodeFile="Default_Choose.aspx.vb" Inherits="Blog_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divModuleContent">
        <div class="divCategoryListContainer">
            &nbsp;
        </div>
        <div class="divModuleContentMain">
            <p>
                <b>
                    <%= Resources.Blog_FrontEnd.Blog_Default_Body%></b></p>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
