﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.AlertBox.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>AlertBox 弹出层（信息提示框）效果</title>
<script src="res/CJL.0.1.min.js"></script>
<script src="res/AlertBox.js"></script>
</head>
<body style="width:900px; height:1000px; padding:100px;">

<style>
.lightbox{width:300px;background:#FFFFFF;border:5px solid #ccc;line-height:20px;display:none; margin:0;}
.lightbox dt{background:#f4f4f4;padding:5px;}
.lightbox dd{ padding:20px; margin:0;}
</style>
<input type="button" value="锁定屏幕" id="idOverlay" />
<input type="button" value="定位效果" id="idFixed" />
<input type="button" value="居中效果" id="idCenter" />
<select>
	<option>覆盖select测试</option>
</select>
<input type="button" value=" 打开 " id="idBoxOpen" />
<span id="idMsg"></span> <br />
<dl id="idBox" class="lightbox" style="top:10%;left:5%;">
	<dt><b>AlertBox</b> </dt>
	<dd>
		<input type="text" value="正常输入" id="idBoxTxt">
		<input type="button" value=" 关闭 " id="idBoxClose" />
	</dd>
</dl>
<script>
    (function () {

        var ab = new AlertBox("idBox"), lock = false;

        //锁定键盘
        function lockup(e) { e.preventDefault(); }
        //高亮层不锁定
        function lockout(e) { e.stopPropagation(); }

        ab.onShow = function () {
            $$("idBoxTxt").select();
            if (lock) {
                $$E.addEvent(document, "keydown", lockup);
                $$E.addEvent(this.box, "keydown", lockout);
                OverLay.show();
            }
        }
        ab.onClose = function () {
            $$("idMsg").innerHTML = $$("idBoxTxt").value;
            $$E.removeEvent(document, "keydown", lockup);
            $$E.removeEvent(this.box, "keydown", lockout);
            OverLay.close();
        }

        $$("idBoxClose").onclick = function () { ab.close(); }
        $$("idBoxOpen").onclick = function () { ab.show(); }

        $$("idCenter").onclick = function () {
            //            if (ab.center) {
            //                ab.center = false;
            //                this.value = "居中效果";
            //            } else {
            //                ab.center = true;
            //                this.value = "取消居中";
            //            }
            lock = true;
            ab.center = true;
            ab.show();
        }

        $$("idFixed").onclick = function () {
//            if (ab.fixed) {
//                ab.fixed = false;
//                this.value = "定位效果";
//            } else {
//                ab.fixed = true;
//                this.value = "取消定位";
            //            }
            ab.fixed = false;
            ab.show();
        }

        $$("idOverlay").onclick = function () {
            if (lock) {
                lock = false;
                this.value = "锁定屏幕";
            } else {
                lock = true;
                this.value = "解锁屏幕";
            }
            ab.show();
        }

    })()
</script>
<br />
<br />
<input type="button" value=" 右下角弹窗效果 " id="idBoxOpen2" />
<dl id="idBox2" class="lightbox">
	<dt><b>右下角消息框</b> </dt>
	<dd>
		<input type="button" value=" 下滚渐隐 " id="idBoxClose2" />
	</dd>
</dl>
<script>
    (function () {
        //右下角消息框
        var timer, target, current,
	ab = new AlertBox("idBox2", { fixed: true,
	    onShow: function () {
	        clearTimeout(timer); this.box.style.bottom = this.box.style.right = 0;
	    },
	    onClose: function () { clearTimeout(timer); }
	});

        function hide() {
            ab.box.style.bottom = --current + "px";
            if (current <= target) {
                ab.close();
            } else {
                timer = setTimeout(hide, 10);
            }
        }

        $$("idBoxClose2").onclick = function () {
            target = -ab.box.offsetHeight; current = 0; hide();
        }
        $$("idBoxOpen2").onclick = function () { ab.show(); }

    })()
</script>
<style>
#idBox3_1, #idBox3_2{ width:120px; height:240px; top:30px; border:1px solid #999; display:none;}
#idBox3_1 a, #idBox3_2 a{ position:absolute; bottom:-1.5em; right:0; font-size:12px;color:#00F;}
#idBox3_1 a:hover, #idBox3_2 a:hover{color:#00F;}
#idBox3_1{ left:0;}
#idBox3_2{ right:0;}
</style>
<input type="button" value=" 对联广告效果 " id="idBoxOpen3" />
<div id="idBox3_1">
	<script type="text/javascript"><!--
	    google_ad_client = "pub-0342339836871729";
	    /* 120x240, 创建于 10-10-6 */
	    google_ad_slot = "9386870680";
	    google_ad_width = 120;
	    google_ad_height = 240;
//-->
</script>
	<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
	<a href="#" id="idBoxClose3_1">关闭</a> </div>
<div id="idBox3_2">
	<script type="text/javascript"><!--
	    google_ad_client = "pub-0342339836871729";
	    /* 120x240, 创建于 10-10-6 */
	    google_ad_slot = "9386870680";
	    google_ad_width = 120;
	    google_ad_height = 240;
//-->
</script>
	<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
	<a href="#" id="idBoxClose3_2">关闭</a> </div>
<script>
    (function () {
        //对联广告效果
        var ab1 = new AlertBox("idBox3_1", { fixed: true }),
	ab2 = new AlertBox("idBox3_2", { fixed: true });

        $$("idBoxClose3_1").onclick = $$("idBoxClose3_2").onclick = function () {
            ab1.close(); ab2.close(); return false;
        }

        $$("idBoxOpen3").onclick = function () { ab1.show(); ab2.show(); }

    })()
</script>
<input type="button" value=" 拖动弹窗效果 " id="idBoxOpen4" />
<dl id="idBox4" class="lightbox">
	<dt id="idBoxTitle4"><b>点击拖动</b> </dt>
	<dd>
		<input type="button" value=" 定位效果 " id="idFixed4" />
		<input type="button" value=" 关闭 " id="idBoxClose4" />
	</dd>
</dl>
<script>
    (function () {
        //拖动弹窗
        var ab = new AlertBox("idBox4"), box = ab.box, x, y, flag = "page";

        $$("idBoxClose4").onclick = function () { ab.close(); }
        $$("idBoxOpen4").onclick = function () { ab.show(); }

        $$("idFixed4").onclick = function () {
            if (ab.fixed) {
                ab.fixed = false;
                flag = "page";
                this.value = "定位效果";
            } else {
                ab.fixed = true;
                flag = "client";
                this.value = "取消定位";
            }
            ab.show();
        }

        //拖动
        $$E.addEvent($$("idBoxTitle4"), "mousedown", Start);
        function Start(e) {
            $$E.addEvent(document, "mousemove", Move);
            $$E.addEvent(document, "mouseup", Stop);
            x = e[flag + "X"] - box.offsetLeft;
            y = e[flag + "Y"] - box.offsetTop;
            ab.show();
        }
        function Move(e) {
            box.style.left = e[flag + "X"] - x + "px";
            box.style.top = e[flag + "Y"] - y + "px";
        }
        function Stop() {
            $$E.removeEvent(document, "mousemove", Move);
            $$E.removeEvent(document, "mouseup", Stop);
            ab.show();
        }

    })()
</script>
</body>
</html>
