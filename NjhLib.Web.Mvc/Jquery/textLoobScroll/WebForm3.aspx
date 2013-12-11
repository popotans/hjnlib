<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.textLoobScroll.WebForm3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<script type="text/javascript" src="jquery.js"></script>
<title>jQuery单行新闻滚动</title>
<style type="text/css">
body{ margin:0; padding:0; font-size:12px}
ul,li{margin:0;padding:0}
#test{width:150px; border:1px solid #000;height:25px; line-height:25px; overflow:hidden; margin:0 auto}
</style>
<script src="../../Scripts/Jquery/jquery.js" type="text/javascript"></script>
<script type="text/javascript">
    function testMove() {
        $("#test ul").animate({ marginTop: "-=25" }, 500, function () {
            $(this).css({ marginTop: "0px" }).find("li:first").appendTo(this)
        })
    }
    $(function () {
        setInterval(testMove, 3000)
    });
</script>
</head>
<body>
<div id="test">
	<ul>
    <li>这是公告标题的第一行</li>
    <li>这是公告标题的第二行</li>
	 <li>这是公告标题的第三行</li>
	 <li>这是公告标题的第四行</li>
	 <li>这是公告标题的第五行</li>
  </ul>
</div>
</body>
</html>