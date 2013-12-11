<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.uidialog.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="jquery-1.8.2.min.js"></script>
    <script src="jquery-ui-1.8.24.custom.min.js"></script>
    <link rel="stylesheet" type="text/css" href="Aristo/jquery-ui-1.8.x.all.css" />
    <script>
        $(function () {
          
            function show() {
                $("<div id='jcDialog' title='模块详情' ><iframe id='innerContent' frameborder='0' scrolling='auto' width='100%' height='540'></iframe></div>").appendTo("body");
                
                $("#jcDialog").dialog({

                    resizable: false,
                    height: 600,
                    width: 800,
                    modal: true,
                    close: function () {
                        $(this).remove();
                    },
                    overlay: {
                        backgroundColor: '#000',
                        opacity: 0.5
                    }
                });
                var url = "http://www.cnblogs.com";
                $("#innerContent").attr("src", url);
            }


            $("#btn").click(function () {
                show();
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="button" id="btn" value="button1" />
    </div>
    </form>
</body>
</html>
