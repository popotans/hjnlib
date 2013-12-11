<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m2.aspx.cs" Inherits="NjhLib.Web.Mvc.memcacheddmy.m2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button runat="server" ID="btnread" Text="read" />
     <asp:Button runat="server" ID="btnWrite" Text="write" />
     <asp:Literal runat="server" ID="ltr1"></asp:Literal>
    </div>
    </form>
</body>
</html>
