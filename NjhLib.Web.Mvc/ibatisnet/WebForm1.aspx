<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NjhLib.Web.Mvc.ibatisnet.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="all" />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="insert" />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="delete" />
        <asp:Button ID="Button4" runat="server" Text="search" />
        <asp:Button ID="Button5" runat="server" onclick="Button5_Click" Text="Update" />
    
    </div>
    </form>
</body>
</html>
