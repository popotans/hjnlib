<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailLink.aspx.cs" Inherits="NjhLib.Web.Mvc.CustomeControlss.MailLink1" %>

<%@ Register Assembly="NjhLib.Web.Mvc" Namespace="NjhLib.Web.Mvc.CustomeControlss"
    TagPrefix="maill" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <maill:maillink ID="Maillink1" Email="popotans@163.com" Text="send email" runat="server"></maill:maillink>
    </div>
    <div>
        <maill:AgeCollector Prompt="niejunhua" DateOfBirth="1985-10-22" ID="AgeCollector1" runat="server" />
    </div>
    </form>
</body>
</html>
