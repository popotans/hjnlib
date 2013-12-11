<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modalDialog.aspx.cs" Inherits="NjhLib.Web.Mvc.modalDialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        window.showModelessDialog("open2.aspx?url=http://www.baidu.com", "asd", "center:no;dialogLeft:5px;dialogTop:5px;scroll:1;status:1;help:1;resizable:1;dialogWidth:0px;dialogHeight:0px;");
        setTimeout("window.close()", 0);
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
