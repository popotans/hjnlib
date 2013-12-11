<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="duowan.aspx.cs" Inherits="NjhLib.Web.Mvc.LoadingStatus.duowan" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
<meta name="keywords" content="" />
<meta name="description" content="" />
<title>正在登录，请稍候</title>
<style rel="stylesheet" type="text/css">
.loading {
	width: 560px;
	height: 140px;
	color: #1797B3;
	background: #EDF6FA;
	border: #DBECF4 solid 1px;
	font-size: 14px;
	padding-top: 100px;
	position: absolute;
	top: 50%;
	left: 50%;
	margin: -120px 0 0 -280px
}

.loading strong {
	background: url(loadingbg.gif) no-repeat right center;
	padding-right: 40px;
	width: 120px;
	display: block;
	margin: 0 auto;
}
</style>
<script type="text/javascript">
    function go2() {
        //window.location.href= "https:\/\/udb.yy.com\/password.do";
    }
</script>
</head>
<body onload="go2()">

<div class="page" id="status" style="display:none;">
<div class="loading"><strong>正在登录，请稍候</strong></div>
</div>
<script type="text/javascript">
    function displayStatus() {
        var obj = document.getElementById("status");
        if (obj == null) {
            return;
        }
        obj.style.display = "block";
    }
    window.setInterval("displayStatus()", 1000);
</script>
</body>
</html>
