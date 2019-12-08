<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Italianos.Admin.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Welcome: <asp:Label ID="Name" runat="server"></asp:Label>
            <asp:GridView ID="ReservationGrid" runat="server">
                <Columns>
                </Columns>
            </asp:GridView>
            <asp:Button ID="BtnCreateItem" runat="server" OnClick="BtnCreateItem_Click"/>
        </div>
    </form>
</body>
</html>
