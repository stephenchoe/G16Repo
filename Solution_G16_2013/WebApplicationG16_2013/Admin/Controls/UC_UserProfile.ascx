<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_UserProfile.ascx.cs" Inherits="WebApplicationG16_2013.Admin.Controls.UC_UserProfile" %>
 <asp:Label runat="server" AssociatedControlID="txtRealName" ID="lblRealName" >真實姓名</asp:Label>
            <div>
                <asp:TextBox runat="server" ID="txtRealName"  />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRealName" ID="rqvRealName" 
                   ErrorMessage="必須填寫真實姓名。" />
            </div>
<asp:Label runat="server" AssociatedControlID="txtRealName" ID="lblNickName" >暱稱</asp:Label>
            <div>
                <asp:TextBox runat="server" ID="txtNickName"  />
                <asp:RequiredFieldValidator runat="server" ID="rqvNickName"  ControlToValidate="txtNickName"
                   ErrorMessage="必須填寫暱稱。" />
            </div>