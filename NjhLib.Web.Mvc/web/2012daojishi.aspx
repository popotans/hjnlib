<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="2012daojishi.aspx.cs" Inherits="NjhLib.Web.Mvc.web._2012daojishi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript">
         function RemainTime(container,year,month,day,hh,mm,ss) {
             var d = new Date();
             d.setMonth(d.getMonth()+1);
             var now = d.valueOf();
             var d2 = new Date();
             d2.setFullYear(year, month, day);
             d2.setHours(hh, mm, ss, 000);

             var yno = d2.valueOf();
             var c = yno - d;
             var days = Math.floor(c / (3600 * 24 * 1000));
            // alert(days);
             var hours = (c - 3600 * 1000 * days * 24) / (3600 * 1000);
             var minutes = ((c - 3600 * 1000 * days * 24) - Math.floor(hours) * 3600000) / (60 * 1000);
             var sec = ((c - 3600 * 1000 * days * 24) - Math.floor(hours) * 3600000 - Math.floor(minutes) * 60 * 1000) / 1000;
             var sec2 = Math.floor(sec);
             if (sec2 < 10) sec2 = "0" + sec2;
             var rs = days + "天" + Math.floor(hours) + "小时" + Math.floor(minutes) + "分" + sec2 + "秒";
             document.getElementById(container).innerHTML = rs;
         }
         function doTime() {
             setInterval("RemainTime('time',2011,8,22,16,15,00)", 1000);
         }
    </script>
</head>
<body>
   <p id="time"></p>
   <script>       doTime();</script>
 
</body>
</html>
