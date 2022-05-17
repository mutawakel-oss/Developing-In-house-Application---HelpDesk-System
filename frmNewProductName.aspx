<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmNewProductName.aspx.cs" Inherits="frmNewProductName" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Table runat="server">
    <asp:TableRow VerticalAlign="Middle"  HorizontalAlign="Center">
    <asp:TableCell>
    
    <asp:Panel runat="server" Width="420px" Height="150">
    
    <asp:Panel runat ="server" Width="410px" Height="120" BorderColor="black" BorderStyle="Solid" BorderWidth="1">
    <asp:Table runat="server" Width ="50%">
    <asp:TableRow Width ="50%">
    <asp:TableCell Font-Size="Smaller" HorizontalAlign="Left">
    Enter product Details
    </asp:TableCell>
    <asp:TableCell></asp:TableCell>
    </asp:TableRow>
    
    </asp:Table>
    <hr />
    
    <asp:Table runat="server">
    <asp:TableRow>
    <asp:TableCell Width ="90px" Font-Bold="true" Font-Size="Smaller" HorizontalAlign="Left">
    Product Name 
    </asp:TableCell>
    <asp:TableCell>
    <asp:TextBox ID="txtProductName" runat="server" Width="300px"></asp:TextBox>
    </asp:TableCell>    
    </asp:TableRow>
    </asp:Table>
    <hr />
    <asp:Table runat="server">
    <asp:TableRow>
    <asp:TableCell>
    <asp:Button runat="server" ID = "btnSave" Text="Save"  OnClick="SaveNewProductName"/>
    <asp:Button runat="server" ID="btnCancel" Text="Cancel" />
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    </asp:Panel>
    
    </asp:Panel>
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    </div>
    </form>
</body>
</html>
