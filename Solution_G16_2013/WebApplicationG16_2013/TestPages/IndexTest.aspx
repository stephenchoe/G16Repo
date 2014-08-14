<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexTest.aspx.cs" Inherits="WebApplicationG16_2013.TestPages.IndexTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="ddlSearchWay" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtKeyWord" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddlOrderBy" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" />
         <br />
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
