<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.textLoobScroll.WebForm2" %>

<html>
<head>
<title>实现侧边层滑动效果</title>
</head>
<style>ul,li{margin:0;padding:0}

#scrollDiv{width:300px;height:100px;min-height:25px;line-height:25px;border:#ccc 1px solid;overflow:hidden}
#scrollDiv li{height:25px;padding-left:10px;}

</style>
<body>

<div id="scrollDiv">
  <ul>
    <li>这是公告标题的第一行</li>
    <li>这是公告标题的第二行</li>
    <li>这是公告标题的第三行</li>
    <li>这是公告标题的第四行</li>
    <li>这是公告标题的第五行</li>
    <li>这是公告标题的第六行</li>
    <li>这是公告标题的第七行</li>
    <li>这是公告标题的第八行</li>
  </ul>
</div>


<script src="../../Scripts/Jquery/jquery.js"></script>
<SCRIPT>
    (function ($) {
        $.fn.extend({
            Scroll: function (opt, callback) {
                //参数初始化
                if (!opt) var opt = {};
                var _this = this.eq(0).find("ul:first");
                var lineH = _this.find("li:first").height(), //获取行高
                     line = opt.line ? parseInt(opt.line, 10) : parseInt(this.height() / lineH, 1), //每次滚动的行数，默认为一屏，即父容器高度
                     speed = opt.speed ? parseInt(opt.speed, 10) : 500, //卷动速度，数值越大，速度越慢（毫秒）
                     timer = opt.timer ? parseInt(opt.timer, 10) : 1000; //滚动的时间间隔（毫秒）
                if (line == 0) line = 1;
                var upHeight = 0 - line * lineH;
                //滚动函数
                scrollUp = function () {
                    _this.animate({
                        marginTop: upHeight
                    }, speed, function () {
                        for (i = 1; i <= line; i++) {
                            _this.find("li:first").appendTo(_this);
                        }
                        _this.css({ marginTop: 0 });
                    });
                }
                //鼠标事件绑定
                _this.hover(function () {
                    clearInterval(timerID);
                }, function () {
                    timerID = setInterval("scrollUp()", timer);
                }).mouseout();
            }
        })
    })(jQuery);

    $("#scrollDiv").Scroll({ line: 3, speed: 0, timer: 3000 });
 </script>
</body>
</html>