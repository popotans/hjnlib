<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NjhLib.Web.Mvc.Jquery.jquery_nyroModal.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/55.com/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/55.com/jquery.nyroModal.custom.js" type="text/javascript"></script>
    <link href="../../Scripts/55.com/nyroModal.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("#AAjax").nyroModal();
        });
    </script>
</head>
<body>
    <table style="width: 100%; height: 100%">
        <tr>
            <td>
                <a href="javascript:" id="AAjax" class="nyroModal">AjAx</a>
            </td>
        </tr>
    </table>
</body>
</html>
