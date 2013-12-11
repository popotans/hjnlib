<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QQAjAxjs.aspx.cs" Inherits="NjhLib.Web.Mvc.Test.js" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../Scripts/NjhJs.js" type="text/javascript"></script>
   

</head>
<body>
    <form id="form1" runat="server">
    <div>

        <script type="text/javascript">
        //腾讯ajax 框架的调用方式
        var ops = {
        method:"get",
        asynchronous: true,
        onSuccess:rsp,
        onFailure:rsp
        };
       Ajax.Request("http://www.china9986.com",ops);

        function rsp(response) {
           // alert(555);
            var r=response.responseText;
            document.write(r);
        }
        </script>

        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Js alert" />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="AlertRedirect" />
<a href='js2.aspx'>tojs2</a>
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click1" Text="tojs2" />
    </div>
    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem></asp:ListItem>
        <asp:ListItem></asp:ListItem>
        <asp:ListItem></asp:ListItem>
        <asp:ListItem></asp:ListItem>
    </asp:RadioButtonList>
    </form>
    <span id="time"></span>
</body>
 <script type="text/javascript">
     var k = GetQueryString("k");
     if (k) {
         alert(k);
     }
     function dd() {
         var d = new Date();
         var now = d.valueOf();
         var d2 = new Date();
         d2.setFullYear(2012, 11, 20);
         d2.setHours(11, 14, 20, 000);

         var yno = d2.valueOf();
         var c = yno-d;
         var days = Math.floor(c / (3600 * 24 * 1000));
         var hours = (c - 3600 * 1000 * days * 24) / (3600 * 1000);
         var minutes = ((c - 3600 * 1000 * days * 24) - Math.floor(hours) * 3600000) / (60 * 1000);
         var sec = ((c - 3600 * 1000 * days * 24) - Math.floor(hours) * 3600000 - Math.floor(minutes) * 60 * 1000) / 1000;
         var rs = days + "天" + Math.floor(hours) + "小时" +Math.floor( minutes) + "分" +Math.floor( sec) + "秒";
         //  alert(rs);
         document.getElementById("time").innerHTML = rs;
     }
     setInterval("dd()", 1000);
    </script>
</html>
