<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Italianos.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>Email</label><br />
            <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox><asp:Label ID="LblEmail" runat="server"></asp:Label><br />
            <label>Password</label><br />
            <asp:TextBox ID="TxtPassword" TextMode="Password" runat="server"></asp:TextBox><asp:Label ID="LblPassword" runat="server"></asp:Label><br />
            <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" Text="Login"/>
        </div>
    </form>
</body>
</html>
