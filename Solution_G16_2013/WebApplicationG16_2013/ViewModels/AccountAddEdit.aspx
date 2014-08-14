<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountAddEdit.aspx.cs" Inherits="WebApplicationG16_2013.ViewModels.AccountAddEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="panelAccount" runat="server">
            <h3>帳戶(Account)資料</h3>
        </asp:Panel>
        <br/>
        <asp:Panel ID="panelFuturesAccount" runat="server">
             <h3>銀行資訊(BankInfos)</h3>
            <h3>台幣出金銀行</h3>
            
            <table class="auto-style1">
                <tr>
                    <td>銀行名稱</td>
                    <td>分支機構</td>
                    <td>幣別</td>
                    <td>帳號</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td>台幣</td>
                    <td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                </tr>
               
            </table>
             <h3>台幣入金銀行</h3>
             <table class="auto-style1">
                <tr>
                    <td>銀行名稱</td>
                    <td>分支機構</td>
                    <td>幣別</td>
                    <td>帳號</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                    <td><asp:DropDownList ID="DropDownList7" runat="server">
                            <asp:ListItem Value="TWD">台幣</asp:ListItem>
                            
                        </asp:DropDownList></td>
                    <td><asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                </tr>
                  <tr>
                    <td>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                    </td>
                    <td><asp:DropDownList ID="DropDownList6" runat="server">
                            <asp:ListItem Value="TWD">台幣</asp:ListItem>
                            
                        </asp:DropDownList></td>
                    <td><asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
                </tr>
                  <tr>
                    <td>
                        <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                    </td>
                    <td><asp:DropDownList ID="DropDownList5" runat="server">
                            <asp:ListItem Value="TWD">台幣</asp:ListItem>
                            
                        </asp:DropDownList></td>
                    <td><asp:TextBox ID="TextBox12" runat="server"></asp:TextBox></td>
                </tr>
               
            </table>
             <h3>外幣出金銀行</h3>
            
            <table class="auto-style1">
                <tr>
                    <td>銀行名稱</td>
                    <td>分支機構</td>
                    <td>幣別</td>
                    <td>帳號</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem Value="CompositeForex">綜合外幣</asp:ListItem>
                            <asp:ListItem Value="USD">美元</asp:ListItem>
                        </asp:DropDownList>


                    </td>
                    <td><asp:TextBox ID="TextBox15" runat="server"></asp:TextBox></td>
                </tr>
               
            </table>
             <h3>外幣入金銀行</h3>
             <table class="auto-style1">
                <tr>
                    <td>銀行名稱</td>
                    <td>分支機構</td>
                    <td>幣別</td>
                    <td>帳號</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                    </td>
                    <td> <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem Value="CompositeForex">綜合外幣</asp:ListItem>
                            <asp:ListItem Value="USD">美元</asp:ListItem>
                        </asp:DropDownList>
</td>
                    <td><asp:TextBox ID="TextBox18" runat="server"></asp:TextBox></td>
                </tr>
                  <tr>
                    <td>
                        <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                    </td>
                    <td> <asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem Value="CompositeForex">綜合外幣</asp:ListItem>
                            <asp:ListItem Value="USD">美元</asp:ListItem>
                        </asp:DropDownList>
</td>
                    <td><asp:TextBox ID="TextBox21" runat="server"></asp:TextBox></td>
                </tr>
                  <tr>
                    <td>
                        <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
                    </td>
                    <td> <asp:DropDownList ID="DropDownList4" runat="server">
                            <asp:ListItem Value="CompositeForex">綜合外幣</asp:ListItem>
                            <asp:ListItem Value="USD">美元</asp:ListItem>
                        </asp:DropDownList>
</td>
                    <td><asp:TextBox ID="TextBox24" runat="server"></asp:TextBox></td>
                </tr>
               
            </table>
        </asp:Panel>
          <asp:Panel ID="panelStockAccount" runat="server">
        </asp:Panel>
      <br/>  <br/>
        <asp:Button ID="Button1" runat="server" Text="Delete" OnClick="Button1_Click" /> &nbsp; &nbsp; &nbsp; &nbsp;

         <asp:Button ID="Button2" runat="server" Text="新增" OnClick="Button2_Click" />
    &nbsp; &nbsp; &nbsp;<asp:Label ID="Label1" runat="server" Text=""></asp:Label></div>
    </form>
</body>
</html>
