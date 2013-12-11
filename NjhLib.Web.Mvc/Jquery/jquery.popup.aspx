<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jquery.popup.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.jquerypopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .d1
        {
            width: 350px;
            height: 200px;
            background-color: Red;
            display: none;
        }
    </style>
    <script src="../Scripts/Jquery/jquery.js"></script>
    <script src="../Scripts/Jquery/jquery.tools.min.js"></script>
    <script src="../Scripts/Jquery/jquery.position.js"></script>
    <script>
        $(function () {

            //为按钮C1 绑定事件，设置遮罩属性和事件，按钮关联的显示层ID 为rel 属性标识
            var trigger1 = $("#c1").overlay(
            {
                mask: {
                    background: "#ccc",
                    opacity: "0.8",
                    loadSpeed: 200
                },
                closeOnClick: false
            }
            );

            var btnclose1click = $("#yesno input").click(function (e) {
                //alert(55);
                $(this).val("即将关闭");
                $(this).parent("div").slideUp();
                trigger1.eq(0).overlay().close();
            });

            //页面加载后即运行。
            $("#facebox").overlay({
                mask: {
                    color: "#000",

                    opacity: 0.5
                },
                closeOnClick: false,
                load: true
            });



        });
    </script>
</head>
<body>
    <input value="click" id="clc" type="button" />
    <div id="d1" style="background: red; width: 400px; height: 300px; display: none">
    </div>
    <input type="button" id="c1" rel="#yesno" value="pop1-yes or no" />
    <div id="yesno" class="d1">
        yes or no
        <input type="button" value="close" />
    </div>
    <div id="facebox" style="display: none" class="d1">
        this is starting when page is loaded.
    </div>
</body>
</html>
