<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IE6/7/8/9的CSS HACK </title>
    <style type="text/css">
        #d1 .dc1
        {
               background-color:red\0;/* ie 8/9*/
               background-color:eeeeee\9\0;/* ie 9*/
               *background-color:blue;/* ie 6/7*/
               _background-color:orange;/* ie 6*/ 
            }
    </style>
    <!--[if ie 6]>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
    <div id="d1" style="border: #000 solid 1px; width: 400px; height: 400px;">
        <div class="dc1">
            hello
        </div>
    </div>
    </form>
</body>
</html>
