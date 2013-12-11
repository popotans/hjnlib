<%@ Page Language="C#" AutoEventWireup="true" Inherits="NjhLib.Web.Mvc.RegexExpression.restrictInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function keyUp() {
            var val = document.getElementById("inp1").value;
            val= val.replace(/[^/u4E00-/u9FA5]/ig, '');
            alert(val);

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="text" onkeyup="value=value.replace(/[^\u4E00-\u9FA5]/g,'')"  />
    </div>
    </form>
</body>
</html>
