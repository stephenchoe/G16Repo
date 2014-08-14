<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="WebApplicationG16_2013.TestPages.MemberModel.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PersonId" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:CommandField ShowEditButton="True" ShowSelectButton="True" ButtonType="Button" />
                <asp:BoundField DataField="PersonId" HeaderText="PersonId" InsertVisible="False" ReadOnly="True" SortExpression="PersonId" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="TWID" HeaderText="TWID" SortExpression="TWID" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:G16MemberContext %>" DeleteCommand="DELETE FROM [People] WHERE [PersonId] = @PersonId" InsertCommand="INSERT INTO [People] ([Name], [TWID]) VALUES (@Name, @TWID)" SelectCommand="SELECT [PersonId], [Name], [TWID] FROM [People]" UpdateCommand="UPDATE [People] SET [Name] = @Name, [TWID] = @TWID WHERE [PersonId] = @PersonId">
            <DeleteParameters>
                <asp:Parameter Name="PersonId" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="TWID" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="TWID" Type="String" />
                <asp:Parameter Name="PersonId" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </div>

        <asp:Button ID="Button1" runat="server" Text="Button" UseSubmitBehavior="False" />



    </form>
</body>
</html>
