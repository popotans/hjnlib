<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MouseoverImgResize.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.MouseoverImgResize" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>鼠标经过显示大图</title>
        <script type="text/javascript">
            window.onload = function () {
                var oPop = document.getElementById('pop-window');
                var oPopImg = oPop.getElementsByTagName('img')[0];
                var oImgList = document.getElementById('img-list');
                var aImgLi = oImgList.getElementsByTagName('li');
                var aImg = oImgList.getElementsByTagName('img');
                var timer;
                for (var i = 0; i < aImgLi.length; i++) {
                    aImgLi[i].index = i;

                    //给小图绑定事件
                    aImgLi[i].onmouseover = function () {

                        this.style.borderColor = "#eee";
                        var oImgLi = this;
                        var oImg = new Image();
                        var Ssrc = aImg[this.index].src;   //获取小图的资源路径
                        oImg.src = Ssrc.replace(/_b_/, "_c_");

                        //如果大图加载完成
                        oImg.onload = function () {
                            oPopImg.src = this.src;
                            timer = setTimeout(function () {
                                var xpos, ypos;
                                oPop.style.display = "block";
                                xpos = oImgLi.offsetLeft - (oPop.offsetWidth - oImgLi.offsetWidth) / 2;
                                ypos = oImgLi.offsetTop - (oPop.offsetHeight - oImgLi.offsetHeight) / 2;
                                (xpos < 0) && (xpos = 0);  //防止左溢出
                                (ypos < 0) && (ypos = 0);  //防止上溢出

                                //防止右溢出
                                (xpos > document.documentElement.offsetWidth - oPop.offsetWidth) &&
                                    (xpos = document.documentElement.offsetWidth - oPop.offsetWidth);
                                //防止下溢出
                                (ypos > document.documentElement.offsetHeight - oPop.offsetHeight) &&
                                        (ypos = document.documentElement.offsetHeight - oPop.offsetHeight);
                                oPop.style.top = ypos + 'px';
                                oPop.style.left = xpos + 'px';
                            }, 300);
                        };
                    };

                    aImgLi[i].onmouseout = function () {
                        clearTimeout(timer);
                        this.style.borderColor = "#666";
                    };
                }

                //给弹出大图框绑定事件
                oPop.onmouseover = function () {
                    if (oPop.style.display === "none") {
                        oPop.style.display = "block";
                    }
                };
                oPop.onmouseout = function () {
                    oPop.style.display = "none";
                };

            }
        </script>
        <style type="text/css">
            *{
                margin:0;
                padding:0;
            }
            a{text-decoration:none;}
            body{
                background:#1E1E1E;
                font: 12px "宋体" ;
            }
            #wrapper{margin:0px auto;width:1150px;_height:1%;overflow:hidden;}
            #img-list{list-style:none;margin:150px 0;float:left;}
            #img-list li{
                background:#090909;
                display:inline-block;
                width:200px;
                height:163px;
                float:left;
                border:1px solid #666;
                padding:5px;
                border-radius:4px;
                margin:15px 10px 0px 0px;
                box-shadow:3px 3px 5px #090909;
            }
            #img-list li a img{
                width:200px;
                height:163px;
                display:block;
                border:none;
            }
            #pop-window{
                padding:15px;
                background:url("http://wheato.com/wp-content/uploads/2011/10/winbg.png") repeat;
                position:absolute;
                border-radius:8px;
                display:none;
            }
            #pop-window a img{border:none;}
            #pop-list{
                color:#666;
                margin-top:30px;
            }
            #pop-list #pop-info{list-style:none;line-height:20px;}
            #pop-list #pop-info li a{margin:0 10px 0px 15px;color:#fff;}
            #pop-list #pop-info li a:hover {text-decoration:underline;}
            #author-info{
                clear:both;
                color:#fff;
                font-size:16px;
                margin-bottom:18px;
                text-align:center;
            }
            #author-info a{background:#eee;color:#333;padding:2px;}
        </style>
    </head>
    <body>
        <div id="wrapper">
            <ul id="img-list">
                <li><a href=""><img src="http://pic1.tu.178.com/thumb/21/60347/thumb_b_60347.jpg"/></a></li>
                <li><a href=""><img src="http://pic1.tu.178.com/thumb/21/60346/thumb_b_60346.jpg"/></a></li>
                <li><a href=""><img src="http://pic1.tu.178.com/thumb/21/60345/thumb_b_60345.jpg"/></a></li>
                <li><a href=""><img src="http://pic1.tu.178.com/thumb/21/60344/thumb_b_60344.jpg"/></a></li>
                <li><a href=""><img src="http://pic1.tu.178.com/thumb/21/60343/thumb_b_60343.jpg"/></a></li>
                <li><a href=""><img src="http://pic1.tu.178.com/thumb/21/60342/thumb_b_60342.jpg"/></a></li>
                <li><a href=""><img src="http://pic1.tu.178.com/thumb/21/60341/thumb_b_60341.jpg"/></a></li>
                <li><a href=""><img src="http://pic1.tu.178.com/thumb/21/60340/thumb_b_60340.jpg"/></a></li>
                <li><a href=""><img src="http://pic1.tu.178.com/thumb/21/60339/thumb_b_60339.jpg"/></a></li>
                <li><a href=""><img src="http://pic1.tu.178.com/thumb/21/60338/thumb_b_60338.jpg"/></a></li>
            </ul>
            <div id="pop-window">
                <div id="pop-img">
                    <a href="#"><img src="http://pic1.tu.178.com/thumb/21/60346/thumb_c_60346.jpg" /></a>
                </div>
                <div id="pop-list">
                    <ul id="pop-info">
                        <li>标签：<a href="#">新福音战士</a> | <a href="">eva</a></li>
                        <li>分辨率：<a href="#">1366 × 768</a></li>
                    </ul>
                </div>
            </div>
            <div id="author-info">
                <p>如蒙被转载请注明出处 <a href="http://wheato.com">wheato.com</a> , By — wheato , Email:wheato2010#gmail.com</p>
            </div>
        </div>
    </body>
</html>
