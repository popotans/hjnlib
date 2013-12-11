<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.Calendar.Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>95599.cn -calendar.aspx</title>
    <script type="text/javascript" src="res/Calendar.js"></script>
    <link rel="Stylesheet" type="text/css" href="res/Calendar.css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="text"  onclick="showCalendar(this,1)" />
    </div>
    </form>
</body>
</html>
