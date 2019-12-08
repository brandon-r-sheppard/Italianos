<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateReservation.aspx.cs" Inherits="Italianos.User.CreateReservation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>Date</label>
            <asp:Calendar ID="Date" runat="server" OnSelectionChanged="Date_SelectionChanged" OnDayRender="Date_DayRender"></asp:Calendar><br />
            <label>Time Slot</label>
            <asp:DropDownList ID="TimeSlot" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="TimeSlot_TextChanged"></asp:DropDownList><br />
            <label>Table Number</label>
            <asp:DropDownList ID="TableNumber" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TableNumber_SelectedIndexChanged"></asp:DropDownList><br />
            <label>Appetizer</label>
            <asp:DropDownList ID="Appetizer" runat="server"></asp:DropDownList><br />
            <label>Main</label>
            <asp:DropDownList ID="Main" runat="server"></asp:DropDownList><br />
            <label>Dessert</label>
            <asp:DropDownList ID="Dessert" runat="server"></asp:DropDownList><br />
            <label>Number of Guests</label>
            <asp:TextBox ID="TxtNumOfGuests" runat="server"></asp:TextBox><br />
            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click"/>
        </div>
    </form>
</body>
</html>
