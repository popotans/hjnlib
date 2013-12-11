<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NjhLib.Web.Mvc.YuanJiao.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/55.com/DD_roundies_0.0.2a-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        DD_roundies.addRule('.div1', '20px 20px 20px 20px');
        DD_roundies.addRule('.div2', '1000px 1000px 1000px 1000px');
      
        //DD_roundies.addRule('.btn1', '20px 20px 20px 20px'); //好像对按钮无效
    </script>
</head>
<body>
<%//貌似这个圆角对按钮无效
    
     %>
    <form id="form1" runat="server">
    <div class="div1" style=" width:200px; height:100px; background-color:#523">
    
    </div>
    <div class="div2" style=" width:200px; height:200px; background-color:#ccc"></div>

  

    <input type="button" class="bt的n1" value="我的按钮" />
    </form>
</body>
</html>
