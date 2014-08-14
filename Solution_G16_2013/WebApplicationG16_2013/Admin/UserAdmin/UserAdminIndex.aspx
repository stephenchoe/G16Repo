<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BackAdmin.master" AutoEventWireup="true" CodeBehind="UserAdminIndex.aspx.cs" Inherits="WebApplicationG16_2013.Admin.UserAdmin.UserAdminIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminMainContent" runat="server">
    <asp:DropDownList ID="ddlRoles" runat="server"></asp:DropDownList><br />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
     <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><br />
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

</asp:Content>
