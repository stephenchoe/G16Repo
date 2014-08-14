<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestWebForm.aspx.cs" Inherits="WebApplicationG16_2013.TestWebForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

    <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
</asp:Content>
