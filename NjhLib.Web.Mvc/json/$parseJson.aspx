<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="$parseJson.aspx.cs" Inherits="NjhLib.Web.Mvc.json._parseJson" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.min-vsdoc.js"></script>
    <script src="../Scripts/jquery-1.4.1.min.js"></script>
    <script>
        $(function () {
            var str = '<%=this.GetStr() %>';
            var jobj = $.parseJSON(str);
            //alert(jobj);
            document.write(jobj.Name + "<br/>");
            document.write(jobj.Age + "<br/>");
            document.write(jobj.Jobs + "<br>");

            for (i = 0; i < jobj.list.length; i++){
                document.write(jobj.list[i].LeaderId + "<br>");
                document.write(jobj.list[i].LeaderName + "<br>");
            }
          

        });
    </script>
</head>
<body>
</body>
</html>
