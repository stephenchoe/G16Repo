<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BackAdmin.master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="WebApplicationG16_2013.UserAdmin.AddUser" %>

<%@ Register Src="~/Admin/Controls/UC_UserProfile.ascx" TagPrefix="uc1" TagName="UC_UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminMainContent" runat="server">
      <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>建立新帳戶。</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">使用者名稱</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                    CssClass="text-danger" ErrorMessage="必須填寫使用者名稱欄位。" />
            </div>
        </div>
          <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                    CssClass="text-danger" ErrorMessage="必須填寫Email欄位。" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">密碼</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="必須填寫密碼欄位。" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">確認密碼</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="必須填寫確認密碼欄位。" />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="密碼和確認密碼不相符。" />
            </div>
        </div>
        <div class="form-group">
            <uc1:UC_UserProfile runat="server" ID="UC_UserProfile" />

        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="註冊" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
