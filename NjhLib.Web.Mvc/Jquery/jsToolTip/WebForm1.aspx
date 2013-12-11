<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.jsToolTip.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/Jquery/jquery.js"></script>
    <script src="mouseoverout.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {


            var linkObj = new FixedTips("popupWindow");
            for (var i = 1; i < 9; i++) {
                var linkId = "A" + i.toString();
                linkObj.Add(linkId, {
                    ShowType: "touch",
                    HideType: "touch", //隐藏方式
                    ShowDelay: 500,
                    HideDelay: 500,
                    Fixed: { vAlign: "bottom", CustomTop: 20, CustomLeft: -5 },
                    onShow: (function (i) {
                        return function () {
                            var img = $("#popupWindow ");
                            var p1 = $("#popupWindow ");
                            var p2 = $("#popupWindow ");
                            switch (i) {
                                case 1:

                                    p2.text("多点触控能使麻烦的安装调试变成简单友好的任务吗?");
                                    break;
                                case 2:

                                    p2.text("多点触控能帮助操作员在不断变化的环境下快速安全的执行控制指令吗？");
                                    break;
                                case 3:

                                    p2.text("多点触控能非常方便的完成对工业制造过程的全面监测管理吗？");
                                    break;
                                case 4:

                                    p2.text("多点触控能让操作员快速简便地设置或者改变工业制造过程的参数和程序吗？");
                                    break;
                                case 5:

                                    p2.text("操作员能否利用交互式多点触控来更加有效地操控生产工具或者机器人？");
                                    break;
                                case 6:

                                    p2.text("操作员能否使用多点触控来更加准确地从众多过程仪表中采集到所需数据？");
                                    break;
                                case 7:

                                    p2.text("操作员能否利用多点触控快速准确地处理工业制造过程中出现的紧急故障？");
                                    break;
                                case 8:

                                    p2.text("操作员能否借助多点触控来提供迅速并且可靠的日常服务和维护工作？");
                                    break;
                            }
                        }
                    })(i)

                });
            }
        });
    </script>
</head>
<body>
    <div id="popupWindow" class="tip" style="width: 100px; height: 100px; background-color: #eee;
        display: none">
    </div>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style=" width:300px;">
                    <a href="" id="A1">链接</a>
                </td>
                <td>
                    <a href="" id="A2">链接</a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="" id="A3">链接</a>
                </td>
                <td>
                    <a href="" id="A4">链接</a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="" id="A5">链接</a>
                </td>
                <td>
                    <a href="" id="A6">链接</a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="" id="A7">链接</a>
                </td>
                <td>
                    <a href="" id="A8">链接</a>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
