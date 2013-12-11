<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DaoJiShi.aspx.cs" Inherits="NjhLib.Web.Mvc.web.DaoJiShi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../Scripts/Jquery/jquery.js"></script>
    <script type="text/javascript">
        function clickDown() {
            var d = new Date();
            var enddate = new Date(2011, 9, 25, 23, 59, 59);
            var starTimeData = parseInt(d.valueOf());
            var endTimeData = parseInt(enddate.valueOf());
            var timeSpan = Math.floor(endTimeData - starTimeData);

            if (timeSpan > 0) {
                timeSpan = timeSpan / 1000;
                var second = Math.floor(timeSpan % 60);
                var minite = Math.floor((timeSpan / 60) % 60);
                var hour = Math.floor((timeSpan / 3600) % 24);
                var day = Math.floor((timeSpan / 3600) / 24);
                $("#t").html(day + "天" + hour + "小时" + minite + "分" + second + "秒");
            } else {
            $("#t").html("已过期。");
            }
        }
        $(function () {
            setInterval("clickDown()", 1000);
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="t">
    </div>
    </form>
</body>
</html>
