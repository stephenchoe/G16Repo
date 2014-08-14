<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MethodTest.aspx.cs" Inherits="WebApplicationG16_2013.TestPages.MemberModel.MethodTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        id:<asp:Label ID="lblId" runat="server" Text="1"></asp:Label>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" /><br/>
        <asp:Label ID="lblAccountNumber" runat="server" Text=""></asp:Label><br/>
         <asp:Label ID="lblBankCount" runat="server" Text=""></asp:Label><br/>
         <asp:Label ID="lblTWDWithdrawBank" runat="server" Text=""></asp:Label><br/>
        <br/><br/><br/>
        <asp:DetailsView ID="DetailsView1" runat="server" Height="500px" Width="500px"></asp:DetailsView>

        <br />
        <br />

    </div>
    </form>
</body>
</html>
