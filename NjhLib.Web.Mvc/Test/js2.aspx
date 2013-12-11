<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="js2.aspx.cs" Inherits="NjhLib.Web.Mvc.Test.js2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        我是从js1 过来的页面 <a href="javascript:history.back();">返回</a>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Return" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="返回" />
    </div>
    </form>
</body>
</html>
