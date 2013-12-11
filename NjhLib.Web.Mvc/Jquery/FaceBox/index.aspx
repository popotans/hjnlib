<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.FaceBox.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>facebox</title>
    <script src="../../Scripts/Jquery/jquery.js" type="text/javascript"></script>
    <script src="res/facebox.js" type="text/javascript"></script>
    <link rel="Stylesheet" type="text/css" href="res/facebox.css" />
    <script type="text/javascript">
        $(function () {
            $("a[rel=facebox]").facebox({
                loadingImage:"res/loading.gif",
                closeImage: "closelabel.png"
            });
        });
    </script>
</head>
<body>
    <div>
        <a href="1.jpg" rel="facebox">显示图片</a>
    </div>
</body>
</html>
