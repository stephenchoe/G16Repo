<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountInputModelTest.aspx.cs" Inherits="WebApplicationG16_2013.TestPages.MemberModel.AccountInputModelTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblId" runat="server" Text=""></asp:Label>
        <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />  
    
    &nbsp;&nbsp;
    
        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />   &nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Show" OnClick="Button1_Click" />
        <br/><br/><br/>
     <asp:DetailsView ID="DetailsView1" runat="server" Height="500px" Width="500px"></asp:DetailsView>
    </div>
    </form>
</body>
</html>
