<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMenuItem.aspx.cs" Inherits="Italianos.Admin.AddMenuItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>New Menu Item</h1>
            <br />
            <label>Item Name</label>
            <asp:TextBox ID="TxtItemName" runat="server"></asp:TextBox>
            <asp:Label ID="ErrName" runat="server"></asp:Label><br />
            <label>Category</label>
            <asp:DropDownList ID="DDCategory" runat="server"></asp:DropDownList><br />
            <label>Course</label>
            <asp:DropDownList ID="DDCourse" runat="server"></asp:DropDownList><br />
            <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" Text="Create Item"/>
            
        </div>
    </form>
</body>
</html>
