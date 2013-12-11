<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.day7tipswindows.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/Jquery/jquery.js"></script>
    <script src="tipwindow.js"></script>
    <script src="common.js"></script>
    <link rel="Stylesheet" type="text/css"  href="tipwindow.css"/>
</head>
<body>
    <div>
    <input name="baidu" value="baidu" type="button" onclick="showTipsWindownByUrl('打开百度','http://www.baidu.com',396,210)" />
    <input name="baidu2" value="baid2u" type="button" onclick="showTipsWindown('打开百度2','sa',396,210)" />
    </div>
    <div id="sa" style=" background:#ccc;width:300px;height:300px; display:none">
    woshixuyaodakaidechuangko
    </div>
</body>
</html>
